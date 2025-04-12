using System;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmMain : Form
    {
        private FrmConnect connectForm;
        public FrmMain()
        {
            InitializeComponent();
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            VisiableButton(false);

            FrmCaroGame caro = new FrmCaroGame
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };

            caro.FormClosed += (s, args) => VisiableButton(true);

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
            this.Size = new Size(1220, 750);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            this.StartPosition = FormStartPosition.Manual;
            this.SetDesktopLocation(100, 0);
        }

        private void BtnMutiplayer_Click(object sender, EventArgs e)
        {
            VisiableButton(false);
            connectForm = new FrmConnect
            {
                TopLevel = false
            };

            connectForm.ConnectClicked += ConnectForm_ConnectClicked;
            connectForm.FormClosed += (s, args) => VisiableButton(true);

            this.PnlVfrm.Controls.Add(connectForm);

            connectForm.StartPosition = FormStartPosition.Manual;
            connectForm.SetDesktopLocation(400, 200);

            connectForm.Show();
        }

        private async void ConnectForm_ConnectClicked(object sender, ConnectEventArgs e)
        {
            string IPserver = e.IPserver;
            int port = e.Port;
            TcpClient client = null;
            NetworkStream stream = null;
            try
            {
                client = new TcpClient();
                await client.ConnectAsync(IPserver, port);

                stream = client.GetStream();

                MessageBox.Show("Connected to server.");
                connectForm.Close();

                VisiableButton(false);

                FrmMulCaroGame Mulcaro = new FrmMulCaroGame(client, stream)
                {
                    TopLevel = false,
                    Dock = DockStyle.Fill
                };

                Mulcaro.FormClosed += (s, args) => DisconnectServer(client, stream);
                Mulcaro.FormClosed += (s, args) => VisiableButton(true);

                this.PnlVfrm.Controls.Add(Mulcaro);
                Mulcaro.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                client?.Close();
            }
        }

        private void DisconnectServer(TcpClient client, NetworkStream stream)
        {
            if (stream != null)
            {
                stream.Close();
            }
            if (client != null)
            {
                client.Close();
                MessageBox.Show("Disconnected from server.");
            }
        }

        private void BtnHuongdan_Click(object sender, EventArgs e)
        {
            VisiableButton(false);

            FrmHuongdan huongdan = new FrmHuongdan
            {
                TopLevel = false,
                Dock = DockStyle.Fill
            };

            huongdan.FormClosed += (s, args) => VisiableButton(true);

            this.PnlVfrm.Controls.Add(huongdan);
            huongdan.Show();
        }
    }
}
