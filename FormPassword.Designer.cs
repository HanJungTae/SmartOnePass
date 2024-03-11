namespace SmartOnePass
{
    partial class FormPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPassword));
            this.btnPw = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtboxPw = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnPw
            // 
            this.btnPw.BackgroundImage = global::SmartOnePass.Properties.Resources.BtnRegister_Normal_xxx;
            this.btnPw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnPw.FlatAppearance.BorderSize = 0;
            this.btnPw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPw.ForeColor = System.Drawing.Color.White;
            this.btnPw.Location = new System.Drawing.Point(12, 104);
            this.btnPw.Name = "btnPw";
            this.btnPw.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btnPw.Size = new System.Drawing.Size(75, 23);
            this.btnPw.TabIndex = 1;
            this.btnPw.Text = "종료";
            this.btnPw.UseVisualStyleBackColor = true;
            this.btnPw.MouseLeave += new System.EventHandler(this.btnPw_MouseLeave);
            this.btnPw.Click += new System.EventHandler(this.btnPw_Click);
            this.btnPw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPw_KeyUp);
            this.btnPw.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnPw_MouseDown);
            this.btnPw.MouseHover += new System.EventHandler(this.btnPw_MouseHover);
            this.btnPw.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnPw_MouseUp);
            // 
            // btnExit
            // 
            this.btnExit.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_close_normal;
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(107, 104);
            this.btnExit.Name = "btnExit";
            this.btnExit.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "취소";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.MouseLeave += new System.EventHandler(this.btnExit_MouseLeave);
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            this.btnExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseDown);
            this.btnExit.MouseHover += new System.EventHandler(this.btnExit_MouseHover);
            this.btnExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnExit_MouseUp);
            // 
            // txtboxPw
            // 
            this.txtboxPw.Location = new System.Drawing.Point(12, 67);
            this.txtboxPw.Name = "txtboxPw";
            this.txtboxPw.Size = new System.Drawing.Size(170, 21);
            this.txtboxPw.TabIndex = 0;
            this.txtboxPw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPw_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(11, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "암호를 입력해주세요.";
            // 
            // FormPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 150);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtboxPw);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnPw);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "암호 입력";
            this.Load += new System.EventHandler(this.FormPassword_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPw;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtboxPw;
        private System.Windows.Forms.Label label1;
    }
}