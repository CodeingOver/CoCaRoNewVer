namespace CoCaRo
{
    partial class FrmMulCaroGame
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
            this.LblPlayer = new System.Windows.Forms.Label();
            this.BtnNewGame = new System.Windows.Forms.Button();
            this.BtnRestart = new System.Windows.Forms.Button();
            this.BtnBack = new System.Windows.Forms.Button();
            this.TxtChatBox = new System.Windows.Forms.TextBox();
            this.TxtChatBoxText = new System.Windows.Forms.TextBox();
            this.BtnSend = new System.Windows.Forms.Button();
            this.LblTurn = new System.Windows.Forms.Label();
            this.LblGamestart = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1702, 112);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cờ ca rô";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LblPlayer
            // 
            this.LblPlayer.AutoSize = true;
            this.LblPlayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPlayer.Location = new System.Drawing.Point(1051, 56);
            this.LblPlayer.Name = "LblPlayer";
            this.LblPlayer.Size = new System.Drawing.Size(96, 25);
            this.LblPlayer.TabIndex = 2;
            this.LblPlayer.Text = "Đang chờ";
            // 
            // BtnNewGame
            // 
            this.BtnNewGame.Location = new System.Drawing.Point(179, 36);
            this.BtnNewGame.Name = "BtnNewGame";
            this.BtnNewGame.Size = new System.Drawing.Size(120, 45);
            this.BtnNewGame.TabIndex = 3;
            this.BtnNewGame.Text = "Game mới";
            this.BtnNewGame.UseVisualStyleBackColor = true;
            this.BtnNewGame.Visible = false;
            this.BtnNewGame.Click += new System.EventHandler(this.BtnNewGame_Click);
            // 
            // BtnRestart
            // 
            this.BtnRestart.Location = new System.Drawing.Point(22, 36);
            this.BtnRestart.Name = "BtnRestart";
            this.BtnRestart.Size = new System.Drawing.Size(120, 45);
            this.BtnRestart.TabIndex = 3;
            this.BtnRestart.Text = "Chơi lại";
            this.BtnRestart.UseVisualStyleBackColor = true;
            this.BtnRestart.Click += new System.EventHandler(this.BtnNewGame_Click);
            // 
            // BtnBack
            // 
            this.BtnBack.Location = new System.Drawing.Point(327, 36);
            this.BtnBack.Name = "BtnBack";
            this.BtnBack.Size = new System.Drawing.Size(120, 45);
            this.BtnBack.TabIndex = 3;
            this.BtnBack.Text = "Quay lại";
            this.BtnBack.UseVisualStyleBackColor = true;
            this.BtnBack.Click += new System.EventHandler(this.BtnBack_Click);
            // 
            // TxtChatBox
            // 
            this.TxtChatBox.Location = new System.Drawing.Point(1214, 14);
            this.TxtChatBox.Multiline = true;
            this.TxtChatBox.Name = "TxtChatBox";
            this.TxtChatBox.ReadOnly = true;
            this.TxtChatBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TxtChatBox.Size = new System.Drawing.Size(206, 67);
            this.TxtChatBox.TabIndex = 4;
            this.TxtChatBox.TabStop = false;
            // 
            // TxtChatBoxText
            // 
            this.TxtChatBoxText.Location = new System.Drawing.Point(1214, 87);
            this.TxtChatBoxText.Name = "TxtChatBoxText";
            this.TxtChatBoxText.Size = new System.Drawing.Size(206, 22);
            this.TxtChatBoxText.TabIndex = 5;
            this.TxtChatBoxText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtChatBoxText_KeyPress);
            // 
            // BtnSend
            // 
            this.BtnSend.Location = new System.Drawing.Point(1426, 86);
            this.BtnSend.Name = "BtnSend";
            this.BtnSend.Size = new System.Drawing.Size(50, 26);
            this.BtnSend.TabIndex = 6;
            this.BtnSend.Text = "Gửi";
            this.BtnSend.UseVisualStyleBackColor = true;
            this.BtnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // LblTurn
            // 
            this.LblTurn.AutoSize = true;
            this.LblTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTurn.Location = new System.Drawing.Point(714, 56);
            this.LblTurn.Name = "LblTurn";
            this.LblTurn.Size = new System.Drawing.Size(96, 25);
            this.LblTurn.TabIndex = 7;
            this.LblTurn.Text = "Đang chờ";
            // 
            // LblGamestart
            // 
            this.LblGamestart.AutoSize = true;
            this.LblGamestart.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblGamestart.Location = new System.Drawing.Point(471, 83);
            this.LblGamestart.Name = "LblGamestart";
            this.LblGamestart.Size = new System.Drawing.Size(237, 25);
            this.LblGamestart.TabIndex = 7;
            this.LblGamestart.Text = "Đang chờ người chơi khác";
            // 
            // FrmMulCaroGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1702, 703);
            this.Controls.Add(this.LblGamestart);
            this.Controls.Add(this.LblTurn);
            this.Controls.Add(this.BtnSend);
            this.Controls.Add(this.TxtChatBoxText);
            this.Controls.Add(this.TxtChatBox);
            this.Controls.Add(this.BtnBack);
            this.Controls.Add(this.BtnRestart);
            this.Controls.Add(this.BtnNewGame);
            this.Controls.Add(this.LblPlayer);
            this.Controls.Add(this.label1);
            this.Name = "FrmMulCaroGame";
            this.Text = "CaroGame";
            this.Load += new System.EventHandler(this.FrmCaroGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblPlayer;
        private System.Windows.Forms.Button BtnNewGame;
        private System.Windows.Forms.Button BtnRestart;
        private System.Windows.Forms.Button BtnBack;
        private System.Windows.Forms.TextBox TxtChatBox;
        private System.Windows.Forms.TextBox TxtChatBoxText;
        private System.Windows.Forms.Button BtnSend;
        private System.Windows.Forms.Label LblTurn;
        private System.Windows.Forms.Label LblGamestart;
    }
}

