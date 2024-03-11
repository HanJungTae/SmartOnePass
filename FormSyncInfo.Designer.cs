namespace SmartOnePass
{
    partial class FormSyncInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSyncInfo));
            this.lv_sync_info = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btn_sync_set = new System.Windows.Forms.Button();
            this.btn_sync_close = new System.Windows.Forms.Button();
            this.btn_ukr_keyid_alldelete = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.btn_sync_allselect = new System.Windows.Forms.Button();
            this.customRoundGroupBox1 = new SmartOnePass.CustomRoundGroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.customRoundGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_sync_info
            // 
            this.lv_sync_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lv_sync_info.CheckBoxes = true;
            this.lv_sync_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lv_sync_info.Dock = System.Windows.Forms.DockStyle.Left;
            this.lv_sync_info.FullRowSelect = true;
            this.lv_sync_info.GridLines = true;
            this.lv_sync_info.Location = new System.Drawing.Point(0, 0);
            this.lv_sync_info.Name = "lv_sync_info";
            this.lv_sync_info.Size = new System.Drawing.Size(167, 452);
            this.lv_sync_info.TabIndex = 0;
            this.lv_sync_info.UseCompatibleStateImageBehavior = false;
            this.lv_sync_info.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 30;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "로비 이름";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // btn_sync_set
            // 
            this.btn_sync_set.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_sync_set.BackgroundImage")));
            this.btn_sync_set.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sync_set.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sync_set.ForeColor = System.Drawing.Color.White;
            this.btn_sync_set.Location = new System.Drawing.Point(186, 26);
            this.btn_sync_set.Name = "btn_sync_set";
            this.btn_sync_set.Size = new System.Drawing.Size(93, 41);
            this.btn_sync_set.TabIndex = 1;
            this.btn_sync_set.Text = "동기화";
            this.btn_sync_set.UseVisualStyleBackColor = true;
            this.btn_sync_set.MouseLeave += new System.EventHandler(this.btn_sync_set_MouseLeave);
            this.btn_sync_set.Click += new System.EventHandler(this.btn_sync_set_Click);
            this.btn_sync_set.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_sync_set_MouseDown);
            this.btn_sync_set.MouseHover += new System.EventHandler(this.btn_sync_set_MouseHover);
            this.btn_sync_set.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_sync_set_MouseUp);
            // 
            // btn_sync_close
            // 
            this.btn_sync_close.BackgroundImage = global::SmartOnePass.Properties.Resources.BtnBasic_Normal_xxx;
            this.btn_sync_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sync_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sync_close.ForeColor = System.Drawing.Color.White;
            this.btn_sync_close.Location = new System.Drawing.Point(186, 197);
            this.btn_sync_close.Name = "btn_sync_close";
            this.btn_sync_close.Size = new System.Drawing.Size(93, 41);
            this.btn_sync_close.TabIndex = 3;
            this.btn_sync_close.Text = "닫기";
            this.btn_sync_close.UseVisualStyleBackColor = true;
            this.btn_sync_close.MouseLeave += new System.EventHandler(this.btn_sync_close_MouseLeave);
            this.btn_sync_close.Click += new System.EventHandler(this.btn_sync_close_Click);
            this.btn_sync_close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_sync_close_MouseDown);
            this.btn_sync_close.MouseHover += new System.EventHandler(this.btn_sync_close_MouseHover);
            this.btn_sync_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_sync_close_MouseUp);
            // 
            // btn_ukr_keyid_alldelete
            // 
            this.btn_ukr_keyid_alldelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_ukr_keyid_alldelete.BackgroundImage")));
            this.btn_ukr_keyid_alldelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_ukr_keyid_alldelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ukr_keyid_alldelete.ForeColor = System.Drawing.Color.White;
            this.btn_ukr_keyid_alldelete.Location = new System.Drawing.Point(186, 83);
            this.btn_ukr_keyid_alldelete.Name = "btn_ukr_keyid_alldelete";
            this.btn_ukr_keyid_alldelete.Size = new System.Drawing.Size(93, 41);
            this.btn_ukr_keyid_alldelete.TabIndex = 4;
            this.btn_ukr_keyid_alldelete.Text = "공동현관 Key 전체 삭제";
            this.btn_ukr_keyid_alldelete.UseVisualStyleBackColor = true;
            this.btn_ukr_keyid_alldelete.MouseLeave += new System.EventHandler(this.btn_ukr_keyid_alldelete_MouseLeave);
            this.btn_ukr_keyid_alldelete.Click += new System.EventHandler(this.btn_ukr_keyid_alldelete_Click);
            this.btn_ukr_keyid_alldelete.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_ukr_keyid_alldelete_MouseDown);
            this.btn_ukr_keyid_alldelete.MouseHover += new System.EventHandler(this.btn_ukr_keyid_alldelete_MouseHover);
            this.btn_ukr_keyid_alldelete.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_ukr_keyid_alldelete_MouseUp);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(46, 317);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(328, 23);
            this.progressBar.TabIndex = 5;
            this.progressBar.Visible = false;
            // 
            // btn_sync_allselect
            // 
            this.btn_sync_allselect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_sync_allselect.BackgroundImage")));
            this.btn_sync_allselect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sync_allselect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_sync_allselect.ForeColor = System.Drawing.Color.White;
            this.btn_sync_allselect.Location = new System.Drawing.Point(186, 140);
            this.btn_sync_allselect.Name = "btn_sync_allselect";
            this.btn_sync_allselect.Size = new System.Drawing.Size(93, 41);
            this.btn_sync_allselect.TabIndex = 6;
            this.btn_sync_allselect.Text = "전체선택해제";
            this.btn_sync_allselect.UseVisualStyleBackColor = true;
            this.btn_sync_allselect.MouseLeave += new System.EventHandler(this.btn_sync_allselect_MouseLeave);
            this.btn_sync_allselect.Click += new System.EventHandler(this.btn_sync_allselect_Click);
            this.btn_sync_allselect.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_sync_allselect_MouseDown);
            this.btn_sync_allselect.MouseHover += new System.EventHandler(this.btn_sync_allselect_MouseHover);
            this.btn_sync_allselect.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_sync_allselect_MouseUp);
            // 
            // customRoundGroupBox1
            // 
            this.customRoundGroupBox1.Controls.Add(this.label5);
            this.customRoundGroupBox1.Controls.Add(this.label1);
            this.customRoundGroupBox1.Controls.Add(this.label4);
            this.customRoundGroupBox1.Controls.Add(this.btn_search);
            this.customRoundGroupBox1.Controls.Add(this.label2);
            this.customRoundGroupBox1.Controls.Add(this.label3);
            this.customRoundGroupBox1.Location = new System.Drawing.Point(173, 0);
            this.customRoundGroupBox1.Name = "customRoundGroupBox1";
            this.customRoundGroupBox1.Size = new System.Drawing.Size(116, 260);
            this.customRoundGroupBox1.TabIndex = 7;
            this.customRoundGroupBox1.TabStop = false;
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
            // FormSyncInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(291, 452);
            this.Controls.Add(this.btn_sync_allselect);
            this.Controls.Add(this.btn_ukr_keyid_alldelete);
            this.Controls.Add(this.btn_sync_close);
            this.Controls.Add(this.btn_sync_set);
            this.Controls.Add(this.lv_sync_info);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.customRoundGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormSyncInfo";
            this.Text = "동기화 정보";
            this.Load += new System.EventHandler(this.FormSyncInfo_Load);
            this.customRoundGroupBox1.ResumeLayout(false);
            this.customRoundGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_sync_info;
        private System.Windows.Forms.Button btn_sync_set;
        private System.Windows.Forms.Button btn_sync_close;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btn_ukr_keyid_alldelete;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button btn_sync_allselect;
        private CustomRoundGroupBox customRoundGroupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}