namespace Connect4
{
    partial class HostYJoin
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
            this.btn_Host = new System.Windows.Forms.Button();
            this.btn_Join = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Host
            // 
            this.btn_Host.Location = new System.Drawing.Point(170, 185);
            this.btn_Host.Name = "btn_Host";
            this.btn_Host.Size = new System.Drawing.Size(293, 106);
            this.btn_Host.TabIndex = 0;
            this.btn_Host.Text = "Jugador 1 (Rojo)";
            this.btn_Host.UseVisualStyleBackColor = true;
            this.btn_Host.Click += new System.EventHandler(this.btn_Host_Click);
            // 
            // btn_Join
            // 
            this.btn_Join.Location = new System.Drawing.Point(170, 309);
            this.btn_Join.Name = "btn_Join";
            this.btn_Join.Size = new System.Drawing.Size(293, 106);
            this.btn_Join.TabIndex = 1;
            this.btn_Join.Text = "Jugador 2 (Azul)";
            this.btn_Join.UseVisualStyleBackColor = true;
            this.btn_Join.Click += new System.EventHandler(this.btn_Join_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(51, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(531, 45);
            this.label1.TabIndex = 2;
            this.label1.Text = "ELIJA QUE DESEA HACER";
            // 
            // HostYJoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.ClientSize = new System.Drawing.Size(641, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Join);
            this.Controls.Add(this.btn_Host);
            this.Name = "HostYJoin";
            this.Text = "HostYJoin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Host;
        private System.Windows.Forms.Button btn_Join;
        private System.Windows.Forms.Label label1;
    }
}