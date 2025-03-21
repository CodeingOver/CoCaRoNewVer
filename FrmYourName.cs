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
    public partial class FrmYourName : Form
    {
        public string PlayerName { get; private set; }
        public FrmYourName()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            PlayerName = TxtName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
