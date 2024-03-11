namespace SmartOnePass
{
    partial class FormCrtCall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCrtCall));
            this.customRoundGroupBox1 = new SmartOnePass.CustomRoundGroupBox();
            this.txbDong = new System.Windows.Forms.TextBox();
            this.cb_lb_name = new System.Windows.Forms.ComboBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_lb_ho = new System.Windows.Forms.ComboBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cb_lb_keyid = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.customRoundGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customRoundGroupBox1
            // 
            this.customRoundGroupBox1.Controls.Add(this.txbDong);
            this.customRoundGroupBox1.Controls.Add(this.cb_lb_name);
            this.customRoundGroupBox1.Controls.Add(this.btn_Close);
            this.customRoundGroupBox1.Controls.Add(this.label5);
            this.customRoundGroupBox1.Controls.Add(this.label1);
            this.customRoundGroupBox1.Controls.Add(this.label4);
            this.customRoundGroupBox1.Controls.Add(this.cb_lb_ho);
            this.customRoundGroupBox1.Controls.Add(this.btn_search);
            this.customRoundGroupBox1.Controls.Add(this.label2);
            this.customRoundGroupBox1.Controls.Add(this.label3);
            this.customRoundGroupBox1.Controls.Add(this.cb_lb_keyid);
            this.customRoundGroupBox1.Location = new System.Drawing.Point(5, 5);
            this.customRoundGroupBox1.Name = "customRoundGroupBox1";
            this.customRoundGroupBox1.Size = new System.Drawing.Size(558, 86);
            this.customRoundGroupBox1.TabIndex = 1;
            this.customRoundGroupBox1.TabStop = false;
            // 
            // txbDong
            // 
            this.txbDong.Location = new System.Drawing.Point(267, 18);
            this.txbDong.Name = "txbDong";
            this.txbDong.Size = new System.Drawing.Size(64, 21);
            this.txbDong.TabIndex = 12;
            // 
            // cb_lb_name
            // 
            this.cb_lb_name.FormattingEnabled = true;
            this.cb_lb_name.Location = new System.Drawing.Point(80, 20);
            this.cb_lb_name.Name = "cb_lb_name";
            this.cb_lb_name.Size = new System.Drawing.Size(121, 20);
            this.cb_lb_name.TabIndex = 2;
            this.cb_lb_name.SelectedIndexChanged += new System.EventHandler(this.cb_lb_name_SelectedIndexChanged);
            // 
            // btn_Close
            // 
            this.btn_Close.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_close_normal;
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Close.FlatAppearance.BorderSize = 0;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(443, 49);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btn_Close.Size = new System.Drawing.Size(99, 23);
            this.btn_Close.TabIndex = 11;
            this.btn_Close.Text = "종료";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.MouseLeave += new System.EventHandler(this.btn_Close_MouseLeave);
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            this.btn_Close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_Close_MouseDown);
            this.btn_Close.MouseHover += new System.EventHandler(this.btn_Close_MouseHover);
            this.btn_Close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_Close_MouseUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "로비 이름";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(244, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "동";
            // 
            // cb_lb_ho
            // 
            this.cb_lb_ho.FormattingEnabled = true;
            this.cb_lb_ho.Location = new System.Drawing.Point(360, 19);
            this.cb_lb_ho.Name = "cb_lb_ho";
            this.cb_lb_ho.Size = new System.Drawing.Size(64, 20);
            this.cb_lb_ho.TabIndex = 4;
            this.cb_lb_ho.SelectedIndexChanged += new System.EventHandler(this.cb_lb_ho_SelectedIndexChanged);
            // 
            // btn_search
            // 
            this.btn_search.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_el_call_normal;
            this.btn_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.Color.White;
            this.btn_search.Location = new System.Drawing.Point(443, 18);
            this.btn_search.Name = "btn_search";
            this.btn_search.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btn_search.Size = new System.Drawing.Size(99, 23);
            this.btn_search.TabIndex = 8;
            this.btn_search.Text = "호출";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.MouseLeave += new System.EventHandler(this.btn_search_MouseLeave);
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            this.btn_search.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_search_MouseDown);
            this.btn_search.MouseHover += new System.EventHandler(this.btn_search_MouseHover);
            this.btn_search.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_search_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(337, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(204, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "키 아이디";
            // 
            // cb_lb_keyid
            // 
            this.cb_lb_keyid.FormattingEnabled = true;
            this.cb_lb_keyid.Location = new System.Drawing.Point(267, 52);
            this.cb_lb_keyid.Name = "cb_lb_keyid";
            this.cb_lb_keyid.Size = new System.Drawing.Size(157, 20);
            this.cb_lb_keyid.TabIndex = 6;
            this.cb_lb_keyid.SelectedIndexChanged += new System.EventHandler(this.cb_lb_keyid_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(17, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "업체";
            // 
            // FormCrtCall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 102);
            this.Controls.Add(this.customRoundGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCrtCall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "엘레베이터 호출 점검";
            this.Load += new System.EventHandler(this.FormCrtCall_Load);
            this.customRoundGroupBox1.ResumeLayout(false);
            this.customRoundGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_lb_name;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_lb_keyid;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_lb_ho;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.TextBox txbDong;
        private CustomRoundGroupBox customRoundGroupBox1;
        private System.Windows.Forms.Label label5;
    }
}