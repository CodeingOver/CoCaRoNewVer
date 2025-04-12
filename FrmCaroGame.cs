using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmCaroGame : Form
    {
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
        private int player = 1;
        private bool isGameOver = false;
        public FrmCaroGame()
        {
            InitializeComponent();
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
                if (player == 1)
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
                int playerCheck = pos.player;

                if (playerCheck == -1)
                {
                    LblPlayer.ForeColor = Color.Green;
                    LblPlayer.Text = "Player: O turn";
                }
                else if (playerCheck == 1)
                {
                    LblPlayer.ForeColor = Color.Red;
                    LblPlayer.Text = "Player: X turn";
                }
                // Gọi hàm kiểm tra chiến thắng với vị trí i, j
                if (CheckWin(i, j) == 1)
                {
                    LblPlayer.Text = "Player O wins!";
                    GameEnd();
                }
                else if (CheckWin(i, j) == -1)
                {
                    LblPlayer.Text = "Player X wins!";
                    GameEnd();
                }

                clickedButton.Enabled = false;

                //player = -player;

                //bot o day
                MessageBox.Show(EvaluatePosition(i,j).ToString());
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

            player = 1;
            LblPlayer.ForeColor = Color.Green;
            LblPlayer.Text = "Player: O turn";
            BtnRestart.Visible = true;
            InitializeButtons();
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
            isGameOver = true;
        }

        private void BtnNewGame_Click(object sender, EventArgs e)
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
            player = 1;
            LblPlayer.Text = "Player: O turn";
            LblPlayer.ForeColor = Color.Green;

            BtnNewGame.Visible = false;
            BtnRestart.Visible = true;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private int EvaluateBoard()
        //{
        //    int score = 0;

        //    // Duyệt qua tất cả các ô trên bàn cờ
        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            if (((Button_pos)btn[i, j].Tag).player != 0)
        //            {
        //                score += EvaluatePosition(i, j);
        //            }
        //        }
        //    }

        //    return score;
        //}

        private int EvaluatePosition(int i, int j)
        {
            int score = 0;

            // Đánh giá theo 4 hướng: ngang, dọc, chéo chính, chéo phụ
            score += EvaluateDirection(i, j); 

            return score;
        }

        private int EvaluateDirection(int i, int j)
        {
            int score = 0;
            Button_pos[] pos = new Button_pos[5];

            // Check horizontal direction
            for (int k = 0; k < 5; k++)
            {
                if (j + k < width)
                {
                    pos[k] = (Button_pos)btn[i, j + k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                if (j - k >= 0)
                {
                    pos[k] = (Button_pos)btn[i, j - k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            // Check vertical direction
            for (int k = 0; k < 5; k++)
            {
                if (i + k < height)
                {
                    pos[k] = (Button_pos)btn[i + k, j].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                if (i - k >= 0)
                {
                    pos[k] = (Button_pos)btn[i - k, j].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            // Check main diagonal direction
            for (int k = 0; k < 5; k++)
            {
                if (i + k < height && j + k < width)
                {
                    pos[k] = (Button_pos)btn[i + k, j + k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                if (i - k >= 0 && j - k >= 0)
                {
                    pos[k] = (Button_pos)btn[i - k, j - k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            // Check secondary diagonal direction
            for (int k = 0; k < 5; k++)
            {
                if (i + k < height && j - k >= 0)
                {
                    pos[k] = (Button_pos)btn[i + k, j - k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            for (int k = 0; k < 5; k++)
            {
                if (i - k >= 0 && j + k < width)
                {
                    pos[k] = (Button_pos)btn[i - k, j + k].Tag;
                    if (pos[k].player != 1)
                    {
                        break;
                    }
                    if (pos[k].player == 1)
                    {
                        score++;
                    }
                }
            }

            return score;
        }

        //private int Minimax(int depth, int alpha, int beta, bool maximizingPlayer)
        //{
        //    if (depth == 0 || IsGameOver())
        //    {
        //        return EvaluateBoard();
        //    }

        //    if (maximizingPlayer)
        //    {
        //        int maxEval = int.MinValue;
        //        foreach (var move in GetAvailableMoves())
        //        {
        //            MakeMove(move, 1);
        //            int eval = Minimax(depth - 1, alpha, beta, false);
        //            UndoMove(move);
        //            maxEval = Math.Max(maxEval, eval);
        //            alpha = Math.Max(alpha, eval);
        //            if (beta <= alpha)
        //            {
        //                break;
        //            }
        //        }
        //        return maxEval;
        //    }
        //    else
        //    {
        //        int minEval = int.MaxValue;
        //        foreach (var move in GetAvailableMoves())
        //        {
        //            MakeMove(move, -1);
        //            int eval = Minimax(depth - 1, alpha, beta, true);
        //            UndoMove(move);
        //            minEval = Math.Min(minEval, eval);
        //            beta = Math.Min(beta, eval);
        //            if (beta <= alpha)
        //            {
        //                break;
        //            }
        //        }
        //        return minEval;
        //    }
        //}

        //private List<Point> GetAvailableMoves()
        //{
        //    var moves = new List<Point>();
        //    for (int i = 0; i < height; i++)
        //    {
        //        for (int j = 0; j < width; j++)
        //        {
        //            if (((Button_pos)btn[i, j].Tag).player == 0)
        //            {
        //                moves.Add(new Point(i, j));
        //            }
        //        }
        //    }
        //    return moves;
        //}

        //private void MakeMove(Point move, int player)
        //{
        //    btn[move.X, move.Y].Tag = new Button_pos { x = move.X, y = move.Y, player = player };
        //}

        //private void UndoMove(Point move)
        //{
        //    btn[move.X, move.Y].Tag = new Button_pos { x = move.X, y = move.Y, player = 0 };
        //}

        //private bool IsGameOver()
        //{
        //    if (isGameOver)
        //    {
        //        return true;
        //    }

        //    return false;
        //}


    }
}
