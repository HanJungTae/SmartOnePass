namespace SmartOnePass
{
    partial class FormType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormType));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.imageGroupBox2 = new SmartOnePass.CustomImageGroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.imageGroupBox1 = new SmartOnePass.CustomImageGroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.imageGroupBox2.SuspendLayout();
            this.imageGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnSet);
            this.groupBox3.Location = new System.Drawing.Point(13, 161);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(259, 78);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "건설사 추가";
            this.groupBox3.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(7, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(130, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.Visible = false;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(88, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "추가";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSet
            // 
            this.btnSet.Enabled = false;
            this.btnSet.Location = new System.Drawing.Point(7, 47);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 23);
            this.btnSet.TabIndex = 3;
            this.btnSet.Text = "설정";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Visible = false;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // imageGroupBox2
            // 
            this.imageGroupBox2.Controls.Add(this.comboBox2);
            this.imageGroupBox2.Icon = ((System.Drawing.Icon)(resources.GetObject("imageGroupBox2.Icon")));
            this.imageGroupBox2.Location = new System.Drawing.Point(13, 85);
            this.imageGroupBox2.Name = "imageGroupBox2";
            this.imageGroupBox2.Size = new System.Drawing.Size(259, 54);
            this.imageGroupBox2.TabIndex = 5;
            this.imageGroupBox2.TabStop = false;
            this.imageGroupBox2.Text = " 건설사";
            // 
            // comboBox2
            // 
            this.comboBox2.Enabled = false;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(7, 20);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(246, 20);
            this.comboBox2.TabIndex = 0;
            // 
            // imageGroupBox1
            // 
            this.imageGroupBox1.Controls.Add(this.comboBox1);
            this.imageGroupBox1.ForeColor = System.Drawing.Color.White;
            this.imageGroupBox1.Icon = ((System.Drawing.Icon)(resources.GetObject("imageGroupBox1.Icon")));
            this.imageGroupBox1.Location = new System.Drawing.Point(13, 13);
            this.imageGroupBox1.Name = "imageGroupBox1";
            this.imageGroupBox1.Size = new System.Drawing.Size(259, 54);
            this.imageGroupBox1.TabIndex = 4;
            this.imageGroupBox1.TabStop = false;
            this.imageGroupBox1.Text = " 타입";
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "공동현관+주차위치",
            "공동현관",
            "자동 주차위치",
            "공동현관+주차위치(카태그)",
            "자동주차위치(카태그)",
            "공동현관+주차위치 조회"});
            this.comboBox1.Location = new System.Drawing.Point(6, 20);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(246, 20);
            this.comboBox1.TabIndex = 0;
            // 
            // FormType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 157);
            this.Controls.Add(this.imageGroupBox2);
            this.Controls.Add(this.imageGroupBox1);
            this.Controls.Add(this.groupBox3);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormType";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "설절 타입 확인";
            this.Load += new System.EventHandler(this.FormType_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.imageGroupBox2.ResumeLayout(false);
            this.imageGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSet;
        private CustomImageGroupBox imageGroupBox1;
        private CustomImageGroupBox imageGroupBox2;
    }
}