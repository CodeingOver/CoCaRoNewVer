using System;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmConnect : Form
    {
        public event EventHandler<ConnectEventArgs> ConnectClicked;

        public FrmConnect()
        {
            InitializeComponent();
        }

        private void FrmConnect_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Ngăn người dùng thay đổi kích cỡ của Form
            this.MaximizeBox = false; // Tùy chọn: Ẩn nút phóng to

            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (TxtHostname.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên máy chủ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (TxtPort.Text == "")
            {
                MessageBox.Show("Vui lòng nhập cổng kết nối", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string IPserver = TxtHostname.Text;
            int port = int.Parse(TxtPort.Text);

            ConnectClicked?.Invoke(this, new ConnectEventArgs(IPserver, port));
            
        }
    }
    public class ConnectEventArgs : EventArgs
    {
        public int Port { get; set; }
        public string IPserver { get; set; }

        public ConnectEventArgs(string ipserver, int port)
        {
            Port = port;
            IPserver = ipserver;
        }
    }
}
