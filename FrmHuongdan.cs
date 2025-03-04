using System;
using System.Drawing;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmHuongdan : Form
    {
        public FrmHuongdan()
        {
            InitializeComponent();
        }

        private void FrmHuongdan_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1220, 750); // Thiết lập kích cỡ của Form
            this.FormBorderStyle = FormBorderStyle.FixedSingle; // Ngăn người dùng thay đổi kích cỡ của Form
            this.MaximizeBox = false; // Tùy chọn: Ẩn nút phóng to

            this.StartPosition = FormStartPosition.Manual; // Đặt vị trí khởi tạo của Form là thủ công
            this.SetDesktopLocation(100, 0); // Đặt vị trí của Form

            this.FormBorderStyle = FormBorderStyle.None;

        }

        private void BtnQuaylai_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
