using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmMulCaroGame : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private string playerName = "Player";
        DateTime TimeNow;

        //private Timer timer;
        //O là player 1, X là player -1
        struct Button_pos
        {
            public int x;
            public int y;
            public int player;
        }
        private Button[,] btn;
        private readonly int width = 40;
        private readonly int height = 20;
        private int player = -2;

        private bool YourTurn;
        private int isStart = -1;
        public FrmMulCaroGame(TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            this.client = client;
            this.stream = stream;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblGamestart.Text = DateTime.Now.ToString("HH:mm:ss");

        }

        private void InitializeButtons()
        {
            btn = new Button[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    btn[i, j] = new Button
                    {
                        Size = new Size(30, 30),
                        Location = new Point(j * 30, (i * 30) + 100),
                        TabStop = false,
                        Image = Properties.Resources.O_icon,
                        Tag = new Button_pos { x = i, y = j, player = 0 } // Lưu trữ vị trí i, j trong thuộc tính Tag
                    };
                    btn[i, j].Click += new EventHandler(Button_Click); // Gán sự kiện Click
                    btn[i, j].MouseEnter += new EventHandler(Button_MouseEnter); // Gán sự kiện MouseEnter
                    btn[i, j].MouseLeave += new EventHandler(Button_MouseLeave); // Gán sự kiện MouseLeave
                    this.Controls.Add(btn[i, j]);
                }
            }
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            Button hoveredButton = sender as Button;
            if (hoveredButton != null && player == 1)
            {
                hoveredButton.BackgroundImage = Properties.Resources.O_icon;
                hoveredButton.ImageAlign = ContentAlignment.MiddleCenter;
                hoveredButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else if (hoveredButton != null && player == -1)
            {
                hoveredButton.BackgroundImage = Properties.Resources.icons8_x_50;
                hoveredButton.ImageAlign = ContentAlignment.MiddleCenter;
                hoveredButton.BackgroundImageLayout = ImageLayout.Stretch;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if(isStart <= 1)
                {
                    LblGamestart.Text = "Chưa đủ người chơi";
                    MessageBox.Show(isStart.ToString());
                    return;
                }

                if (YourTurn == false)
                {
                    LblTurn.Text = "Chờ tới lượt của bạn";
                    return;
                }

                else if (player == 1)
                {
                    clickedButton.BackgroundImage = Properties.Resources.O_icon;
                    clickedButton.ImageAlign = ContentAlignment.MiddleCenter;
                    clickedButton.BackgroundImageLayout = ImageLayout.Stretch;

                    int Save_i = ((Button_pos)clickedButton.Tag).x;
                    int Save_j = ((Button_pos)clickedButton.Tag).y;
                    clickedButton.Tag = new Button_pos { x = Save_i, y = Save_j, player = 1 };
                }
                else if (player == -1)
                {
                    clickedButton.BackgroundImage = Properties.Resources.icons8_x_50;
                    clickedButton.ImageAlign = ContentAlignment.MiddleCenter;
                    clickedButton.BackgroundImageLayout = ImageLayout.Stretch;

                    int Save_i = ((Button_pos)clickedButton.Tag).x;
                    int Save_j = ((Button_pos)clickedButton.Tag).y;
                    clickedButton.Tag = new Button_pos { x = Save_i, y = Save_j, player = -1 };
                }

                // Lấy vị trí i, j từ thuộc tính Tag
                Button_pos pos = (Button_pos)clickedButton.Tag;
                int i = pos.x;
                int j = pos.y;

                //Gọi hàm kiểm tra chiến thắng với vị trí i, j
                if (CheckWin(i, j) == 1)
                {
                    LblPlayer.ForeColor = Color.Green;
                    LblPlayer.Text = "O wins!";
                    GameEnd();
                }
                else if (CheckWin(i, j) == -1)
                {
                    LblPlayer.ForeColor = Color.Red;
                    LblPlayer.Text = "X wins!";
                    GameEnd();
                }

                YourTurn = false;
                LblTurn.Text = "Chờ tới lượt của bạn";

                clickedButton.Enabled = false;
                SendData(i, j, player);

            }
        }

        private void SendData(int i,int j,int player)
        {
            string message = "MOVE|" +playerName+"|"+ i + "|" + j + "|" + player+"|OK";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            if(TxtChatBoxText.Text == "")
            {
                return;
            }
            string message = "CHAT|"+playerName+"|"+TxtChatBoxText.Text;
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);

            TimeNow = DateTime.Now;
            AppendTextToChatBox(TimeNow.ToString("HH:mm:ss") + " ["+playerName+"]: " + TxtChatBoxText.Text);

            TxtChatBoxText.Text = "";
        }

        private async Task ReceiveData()
        {
            byte[] data = new byte[1024];
            while(true)
            {
                try
                {
                    int bytes =await stream.ReadAsync(data, 0, data.Length);
                    if(bytes == 0)
                    {
                        Invoke(new Action(() => AppendTextToChatBox("Server has disconnected")));
                        break;
                    }

                    string message = Encoding.UTF8.GetString(data, 0, bytes);
                    string[] arr = message.Split('|');
                    if (arr[0] == "MOVE")
                    {
                        string chatMessageName = arr[1];
                        int i = int.Parse(arr[2]);
                        int j = int.Parse(arr[3]);
                        int player = int.Parse(arr[4]);
                        Invoke(new Action(() => HandleMove(i, j, player)));
                        if (arr[5]== "OK")
                        {
                            YourTurn = true;
                            LblTurn.Text = "Lượt của bạn";
                        }
                    }
                    else if (arr[0] == "CHAT")
                    {
                        string chatMessageName = arr[1];
                        string chatMessage = arr[2];
                        TimeNow = DateTime.Now;
                        Invoke(new Action(() => AppendTextToChatBox(TimeNow.ToString("HH:mm:ss") + " [" + chatMessageName + "]: " + chatMessage)));
                    }
                    else if(arr[0] == "YOURTICK")
                    {
                        if(arr[1] == "O")
                        {
                            player = 1;
                            Invoke(new Action(() => LblPlayer.ForeColor = Color.Green));
                            Invoke(new Action(() => LblPlayer.Text = "Bạn là: O "));
                        }
                        else if (arr[1] == "X")
                        {
                            player = -1;
                            Invoke(new Action(() => LblPlayer.ForeColor = Color.Red));
                            Invoke(new Action(() => LblPlayer.Text = "Bạn là: X "));
                        }

                        if(arr[2] == "1")
                        {
                            YourTurn = true;
                            Invoke(new Action(() => LblTurn.Text = "Lượt của bạn"));
                        }
                        else if (arr[2] == "2")
                        {
                            YourTurn = false;
                            Invoke(new Action(() => LblTurn.Text = "Chờ lượt của bạn"));
                        }

                        Invoke(new Action(() => LblGamestart.Text = "Game bắt đầu"));
                    }
                    else if(arr[0] == "CLIENT")
                    {
                        isStart = int.Parse(arr[1]);
                        if (isStart == 2)
                        {
                            Invoke(new Action(() => LblGamestart.Text = "Đã đủ 2 người chơi"));
                            Invoke(new Action(() => BtnNewGame.PerformClick()));
                        }
                        else if (isStart == 1)
                        {
                            Invoke(new Action(() => LblGamestart.Text = "Chờ người chơi thứ 2"));
                        }
                    }
                    else if(arr[0] == "NEWGAME")
                    {
                        if(arr[1] == "OK")
                        {
                            if(MessageBox.Show("Người chơi yêu cầu làm ván mới", "Thông báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                Invoke(new Action(() => SendNewGame()));
                                Invoke(new Action(() => NewGame()));
                            }
                        }
                        else if (arr[1] == "OKOK")
                        {
                            Invoke(new Action(() => NewGame()));
                        }
                    }


                }
                catch
                {
                    break;
                }
            }
            Invoke(new Action(()=> this.Close()));
        }

        private void HandleMove(int i, int j, int player)
        {
            if (player == 1)
            {
                btn[i, j].BackgroundImage = Properties.Resources.O_icon;
                btn[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                btn[i, j].BackgroundImageLayout = ImageLayout.Stretch;

                int Save_i = ((Button_pos)btn[i, j].Tag).x;
                int Save_j = ((Button_pos)btn[i, j].Tag).y;
                btn[i, j].Tag = new Button_pos { x = Save_i, y = Save_j, player = 1 };

                btn[i, j].Enabled = false;
            }
            else if (player == -1)
            {
                btn[i, j].BackgroundImage = Properties.Resources.icons8_x_50;
                btn[i, j].ImageAlign = ContentAlignment.MiddleCenter;
                btn[i, j].BackgroundImageLayout = ImageLayout.Stretch;

                int Save_i = ((Button_pos)btn[i, j].Tag).x;
                int Save_j = ((Button_pos)btn[i, j].Tag).y;
                btn[i, j].Tag = new Button_pos { x = Save_i, y = Save_j, player = -1 };

                btn[i, j].Enabled = false;
            }
            if (CheckWin(i, j) == 1)
            {
                LblPlayer.ForeColor = Color.Green;
                LblPlayer.Text = "O wins!";
                GameEnd();
            }
            else if (CheckWin(i, j) == -1)
            {
                LblPlayer.ForeColor = Color.Red;
                LblPlayer.Text = "X wins!";
                GameEnd();
            }
        }

        private void FrmCaroGame_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1220, 750); // Thiết lập kích cỡ của Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Ngăn người dùng thay đổi kích cỡ của Form
            this.MaximizeBox = false; // Tùy chọn: Ẩn nút phóng to

            this.StartPosition = FormStartPosition.Manual; // Đặt vị trí khởi tạo của Form là thủ công
            this.SetDesktopLocation(100, 0); // Đặt vị trí của Form

            this.FormBorderStyle = FormBorderStyle.None;

            BtnRestart.Visible = true;
            InitializeButtons();

            FrmYourName frmYourName = new FrmYourName();
            if (frmYourName.ShowDialog() == DialogResult.OK)
            {
                playerName = frmYourName.PlayerName;
            }

            _ = Task.Run(() => ReceiveData());
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button hoveredButton = sender as Button;
            if (hoveredButton != null && hoveredButton.Enabled == true)
            {
                hoveredButton.BackgroundImage = null;
            }
        }

        private int CheckWin(int i, int j)
        {
            // Logic kiểm tra chiến thắng
            int win = 0;

            Button_pos[] pos = new Button_pos[5];

            Button_pos buttonPos = (Button_pos)btn[i, j].Tag;
            int Checkplayer = buttonPos.player;
            if(Checkplayer == 1)//Đang check O
            {
                int W_left = 0;
                int W_right = 0;
                int H_up = 0;
                int H_down = 0;
                int MDia_up = 0;
                int MDia_down = 0;
                int SDia_up = 0;
                int SDia_down = 0;

                if (j+4 < width)//Check hàng ngang đi qua phải
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i, j + k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            W_right++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if(j-4 > 0)//Check hàng ngang đi qua trái
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i, j - k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            W_left++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if ((W_left+W_right)-1 >= 5)
                {
                    win = 1;
                    return win;
                }

                if (i + 4 < height)//Check hàng dọc đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            H_down++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if (i - 4 > 0)//Check hàng dọc đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            H_up++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if ((H_up + H_down) - 1 >= 5)
                {
                    win = 1;
                    return win;
                }

                if (i + 4 < height && j + 4 < width)//Check chéo chính đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j + k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            MDia_down++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if (i - 4 > 0 && j - 4 > 0)//Check chéo chính đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j - k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            MDia_up++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if ((MDia_up + MDia_down) - 1 >= 5)
                {
                    win = 1;
                    return win;
                }

                if (i + 4 < height && j - 4 > 0)//Check chéo phụ đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j - k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            SDia_down++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if (i - 4 > 0 && j + 4 < width)//Check chéo phụ đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j + k].Tag;
                        if (pos[k].player != 1)
                        {
                            break;
                        }
                        if (pos[k].player == 1)
                        {
                            SDia_up++;
                        }
                        if (k == 4)
                        {
                            win = 1;
                        }
                    }
                }

                if ((SDia_up + SDia_down) - 1 >= 5)
                {
                    win = 1;
                    return win;
                }
            }

            else if (Checkplayer == -1)//Đang check X
            {
                int W_left = 0;
                int W_right = 0;
                int H_up = 0;
                int H_down = 0;
                int MDia_up = 0;
                int MDia_down = 0;
                int SDia_up = 0;
                int SDia_down = 0;

                if (j + 4 < width)//Check hàng ngang đi qua phải
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i, j + k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            W_right++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if (j - 4 > 0)//Check hàng ngang đi qua trái
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i, j - k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            W_left++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if ((W_left + W_right) - 1 >= 5)
                {
                    win = -1;
                    return win;
                }

                if (i + 4 < height)//Check hàng dọc đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            H_down++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if (i - 4 > 0)//Check hàng dọc đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if(pos[k].player == -1)
                        {
                            H_up++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if ((H_up + H_down) - 1 >= 5)
                {
                    win = -1;
                    return win;
                }

                if (i + 4 < height && j + 4 < width)//Check chéo chính đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j + k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            MDia_down++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if (i - 4 > 0 && j - 4 > 0)//Check chéo chính đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j - k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            MDia_up++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if ((MDia_up + MDia_down) - 1 >= 5)
                {
                    win = -1;
                    return win;
                }

                if (i + 4 < height && j - 4 > 0)//Check chéo phụ đi xuống
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i + k, j - k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            SDia_down++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if (i - 4 > 0 && j + 4 < width)//Check chéo phụ đi lên
                {
                    for (int k = 0; k < 5; k++)
                    {
                        pos[k] = (Button_pos)btn[i - k, j + k].Tag;
                        if (pos[k].player != -1)
                        {
                            break;
                        }
                        if (pos[k].player == -1)
                        {
                            SDia_up++;
                        }
                        if (k == 4)
                        {
                            win = -1;
                        }
                    }
                }

                if ((SDia_up + SDia_down) - 1 >= 5)
                {
                    win = -1;
                    return win;
                }
            }
            return win;
        }

        private void GameEnd()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    btn[i, j].Enabled = false;
                }
            }
            BtnNewGame.Visible = true;
            BtnRestart.Visible = false;
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
        {
            string message = "NEWGAME|OK";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void SendNewGame()
        {
            string message = "NEWGAME|OKOK";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }

        private void NewGame()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    btn[i, j].BackgroundImage = null;
                    btn[i, j].Enabled = true;
                    btn[i, j].Tag = new Button_pos { x = i, y = j, player = 0 };
                }
            }

            BtnNewGame.Visible = false;
            BtnRestart.Visible = true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TxtChatBoxText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                BtnSend.PerformClick();
                e.Handled = true;
            }
        }

        private void AppendTextToChatBox(string text)
        {
            TxtChatBox.AppendText(text + Environment.NewLine);
            TxtChatBox.SelectionStart = TxtChatBox.Text.Length;
            TxtChatBox.ScrollToCaret();
        }

    }
}
