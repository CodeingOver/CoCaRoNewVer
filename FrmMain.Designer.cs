namespace CoCaRo
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlVfrm = new System.Windows.Forms.Panel();
            this.BtnPlay = new System.Windows.Forms.Button();
            this.BtnMutiplayer = new System.Windows.Forms.Button();
            this.BtnHuongdan = new System.Windows.Forms.Button();
            this.PnlVfrm.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlVfrm
            // 
            this.PnlVfrm.Controls.Add(this.BtnHuongdan);
            this.PnlVfrm.Controls.Add(this.BtnMutiplayer);
            this.PnlVfrm.Controls.Add(this.BtnPlay);
            this.PnlVfrm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlVfrm.Location = new System.Drawing.Point(0, 0);
            this.PnlVfrm.Name = "PnlVfrm";
            this.PnlVfrm.Size = new System.Drawing.Size(1202, 703);
            this.PnlVfrm.TabIndex = 0;
            // 
            // BtnPlay
            // 
            this.BtnPlay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPlay.Location = new System.Drawing.Point(682, 160);
            this.BtnPlay.Name = "BtnPlay";
            this.BtnPlay.Size = new System.Drawing.Size(194, 77);
            this.BtnPlay.TabIndex = 0;
            this.BtnPlay.TabStop = false;
            this.BtnPlay.Text = "Chơi đơn";
            this.BtnPlay.UseVisualStyleBackColor = true;
            this.BtnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // BtnMutiplayer
            // 
            this.BtnMutiplayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMutiplayer.Location = new System.Drawing.Point(682, 276);
            this.BtnMutiplayer.Name = "BtnMutiplayer";
            this.BtnMutiplayer.Size = new System.Drawing.Size(194, 77);
            this.BtnMutiplayer.TabIndex = 0;
            this.BtnMutiplayer.TabStop = false;
            this.BtnMutiplayer.Text = "Hai người chơi";
            this.BtnMutiplayer.UseVisualStyleBackColor = true;
            this.BtnMutiplayer.Click += new System.EventHandler(this.BtnMutiplayer_Click);
            // 
            // BtnHuongdan
            // 
            this.BtnHuongdan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnHuongdan.Location = new System.Drawing.Point(682, 395);
            this.BtnHuongdan.Name = "BtnHuongdan";
            this.BtnHuongdan.Size = new System.Drawing.Size(194, 77);
            this.BtnHuongdan.TabIndex = 0;
            this.BtnHuongdan.TabStop = false;
            this.BtnHuongdan.Text = "Hướng dẫn";
            this.BtnHuongdan.UseVisualStyleBackColor = true;
            this.BtnHuongdan.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1202, 703);
            this.Controls.Add(this.PnlVfrm);
            this.Name = "FrmMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.PnlVfrm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlVfrm;
        private System.Windows.Forms.Button BtnPlay;
        private System.Windows.Forms.Button BtnHuongdan;
        private System.Windows.Forms.Button BtnMutiplayer;
    }
}