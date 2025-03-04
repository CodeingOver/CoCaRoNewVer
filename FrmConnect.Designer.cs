namespace CoCaRo
{
    partial class FrmConnect
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
            this.label1 = new System.Windows.Forms.Label();
            this.TxtPort = new System.Windows.Forms.TextBox();
            this.BtnBack = new System.Windows.Forms.Button();
            this.BtnConnect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtHostname = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Port:";
            // 
            // TxtPort
            // 
            this.TxtPort.Location = new System.Drawing.Point(175, 72);
            this.TxtPort.Name = "TxtPort";
            this.TxtPort.Size = new System.Drawing.Size(179, 22);
            this.TxtPort.TabIndex = 1;
            // 
            // BtnBack
            // 
            this.BtnBack.Location = new System.Drawing.Point(278, 146);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(140, 57);
            this.BtnBack.TabIndex = 2;
            this.BtnBack.Text = "Quay lại";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // BtnConnect
            // 
            this.BtnConnect.Location = new System.Drawing.Point(103, 146);
            this.BtnConnect.Name = "BtnConnect";
            this.BtnConnect.Size = new System.Drawing.Size(140, 57);
            this.BtnConnect.TabIndex = 2;
            this.BtnConnect.Text = "Kết nối";
            this.BtnConnect.UseVisualStyleBackColor = true;
            this.BtnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Host Name:";
            // 
            // TxtHostname
            // 
            this.TxtHostname.Location = new System.Drawing.Point(175, 32);
            this.TxtHostname.Name = "TxtHostname";
            this.TxtHostname.Size = new System.Drawing.Size(179, 22);
            this.TxtHostname.TabIndex = 1;
            // 
            // FrmConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 257);
            this.Controls.Add(this.BtnConnect);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.TxtHostname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtPort);
            this.Controls.Add(this.label1);
            this.Name = "FrmConnect";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FrmConnect_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtPort;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.Button BtnConnect;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtHostname;
    }
}