using System.Drawing;

namespace Connect4
{
    partial class Menu
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
            this.button1v1 = new System.Windows.Forms.Button();
            this.ConnectFourLabel = new System.Windows.Forms.Label();
            this.buttonOnline = new System.Windows.Forms.Button();
            this.button1vIA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1v1
            // 
            this.button1v1.Location = new System.Drawing.Point(356, 154);
            this.button1v1.Name = "button1v1";
            this.button1v1.Size = new System.Drawing.Size(98, 30);
            this.button1v1.TabIndex = 0;
            this.button1v1.Text = "1 v 1";
            this.button1v1.UseVisualStyleBackColor = true;
            this.button1v1.Click += new System.EventHandler(this.button1v1_Click);
            // 
            // ConnectFourLabel
            // 
            this.ConnectFourLabel.AutoSize = true;
            this.ConnectFourLabel.Font = new System.Drawing.Font("Impact", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ConnectFourLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(130)))), ((int)(((byte)(184)))));
            this.ConnectFourLabel.Location = new System.Drawing.Point(223, 29);
            this.ConnectFourLabel.Name = "ConnectFourLabel";
            this.ConnectFourLabel.Size = new System.Drawing.Size(390, 80);
            this.ConnectFourLabel.TabIndex = 1;
            this.ConnectFourLabel.Text = "Connect Four";
            // 
            // buttonOnline
            // 
            this.buttonOnline.Location = new System.Drawing.Point(356, 218);
            this.buttonOnline.Name = "buttonOnline";
            this.buttonOnline.Size = new System.Drawing.Size(98, 30);
            this.buttonOnline.TabIndex = 2;
            this.buttonOnline.Text = "1 v 1 ONLINE";
            this.buttonOnline.UseVisualStyleBackColor = true;
            this.buttonOnline.Click += new System.EventHandler(this.buttonOnline_Click);
            // 
            // button1vIA
            // 
            this.button1vIA.Location = new System.Drawing.Point(356, 284);
            this.button1vIA.Name = "button1vIA";
            this.button1vIA.Size = new System.Drawing.Size(98, 30);
            this.button1vIA.TabIndex = 3;
            this.button1vIA.Text = "1 v IA";
            this.button1vIA.UseVisualStyleBackColor = true;
            this.button1vIA.Click += new System.EventHandler(this.button1vIA_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(76)))), ((int)(((byte)(117)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1vIA);
            this.Controls.Add(this.buttonOnline);
            this.Controls.Add(this.ConnectFourLabel);
            this.Controls.Add(this.button1v1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Menu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Connect 4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1v1;
        private System.Windows.Forms.Label ConnectFourLabel;
        private System.Windows.Forms.Button buttonOnline;
        private System.Windows.Forms.Button button1vIA;
    }
}

