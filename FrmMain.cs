using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCaRo
{
    public partial class FrmMain : Form
    {
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
            
        }
    }
}
