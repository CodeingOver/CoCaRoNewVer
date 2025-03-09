using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmMain : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        private FrmConnect connectForm;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            // Xóa các điều khiển hiện tại trong PnlVfrm
            VisiableButton(false);

            // Tạo một instance của FrmCaroGame
            FrmCaroGame caro = new FrmCaroGame();
            caro.TopLevel = false;
            caro.Dock = DockStyle.Fill; // Đặt form con để lấp đầy panel

            caro.FormClosed += (s, args) => VisiableButton(true);

            // Thêm form con vào PnlVfrm và hiển thị nó
            this.PnlVfrm.Controls.Add(caro);
            caro.Show();
        }

        public void VisiableButton(bool isVisiable)
        {
            this.BtnPlay.Visible = isVisiable;
            this.BtnMutiplayer.Visible = isVisiable;
            this.BtnHuongdan.Visible = isVisiable;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1220, 750); // Thiết lập kích cỡ của Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Ngăn người dùng thay đổi kích cỡ của Form
            this.MaximizeBox = false; // Tùy chọn: Ẩn nút phóng to

            this.StartPosition = FormStartPosition.Manual; // Đặt vị trí khởi tạo của Form là thủ công
            this.SetDesktopLocation(100, 0); // Đặt vị trí của Form
        }

        private void BtnMutiplayer_Click(object sender, EventArgs e)
        {
            //connect to server
            VisiableButton(false);
            connectForm = new FrmConnect();

            connectForm.TopLevel = false;

            connectForm.ConnectClicked += ConnectForm_ConnectClicked;
            connectForm.FormClosed += (s, args) => VisiableButton(true);

            this.PnlVfrm.Controls.Add(connectForm);

            connectForm.StartPosition = FormStartPosition.Manual; // Đặt vị trí khởi tạo của Form là thủ công
            connectForm.SetDesktopLocation(400, 200); // Đặt vị trí của Form

            connectForm.Show();

        }
        
        private async void ConnectForm_ConnectClicked(object sender, ConnectEventArgs e)
        {
            string IPserver = e.IPserver;
            int port = e.Port;
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(IPserver, port);

                stream = client.GetStream();

                MessageBox.Show("Connected to server.");
                connectForm.Close();


                VisiableButton(false);

                FrmMulCaroGame Mulcaro = new FrmMulCaroGame(client, stream);
                Mulcaro.TopLevel = false;
                Mulcaro.Dock = DockStyle.Fill;

                Mulcaro.FormClosed += (s, args) => DisconnectServer();
                Mulcaro.FormClosed += (s, args) => VisiableButton(true);

                this.PnlVfrm.Controls.Add(Mulcaro);
                Mulcaro.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisconnectServer()
        {
            if (client != null)
            {
                stream.Close();
                client.Close();
                MessageBox.Show("Disconnected from server.");
            }
        }

        private void BtnHuongdan_Click(object sender, EventArgs e)
        {
            VisiableButton(false);

            FrmHuongdan huongdan = new FrmHuongdan();
            huongdan.TopLevel = false;
            huongdan.Dock = DockStyle.Fill;

            huongdan.FormClosed += (s, args) => VisiableButton(true);

            this.PnlVfrm.Controls.Add(huongdan);
            huongdan.Show();
        }

    }
}
