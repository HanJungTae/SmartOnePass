namespace SmartOnePass
{
    partial class FormCrtCallLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCrtCallLog));
            this.lv_log = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.customRoundGroupBox1 = new SmartOnePass.CustomRoundGroupBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.cb_lb_name = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.customRoundGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_log
            // 
            this.lv_log.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lv_log.FullRowSelect = true;
            this.lv_log.GridLines = true;
            this.lv_log.Location = new System.Drawing.Point(12, 83);
            this.lv_log.Name = "lv_log";
            this.lv_log.Size = new System.Drawing.Size(1255, 414);
            this.lv_log.TabIndex = 0;
            this.lv_log.UseCompatibleStateImageBehavior = false;
            this.lv_log.View = System.Windows.Forms.View.Details;
            this.lv_log.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.lv_log.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.lv_log.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 10;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "시간";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "로비이름";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 88;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "동";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "호";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Comment";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 281;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "로그";
            this.columnHeader7.Width = 581;
            // 
            // customRoundGroupBox1
            // 
            this.customRoundGroupBox1.Controls.Add(this.btn_search);
            this.customRoundGroupBox1.Controls.Add(this.btn_close);
            this.customRoundGroupBox1.Controls.Add(this.cb_lb_name);
            this.customRoundGroupBox1.Controls.Add(this.label3);
            this.customRoundGroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customRoundGroupBox1.Location = new System.Drawing.Point(12, 5);
            this.customRoundGroupBox1.Name = "customRoundGroupBox1";
            this.customRoundGroupBox1.Size = new System.Drawing.Size(1255, 62);
            this.customRoundGroupBox1.TabIndex = 2;
            this.customRoundGroupBox1.TabStop = false;
            // 
            // btn_search
            // 
            this.btn_search.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_search_normal;
            this.btn_search.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_search.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btn_search.FlatAppearance.BorderSize = 0;
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.ForeColor = System.Drawing.Color.White;
            this.btn_search.Location = new System.Drawing.Point(429, 22);
            this.btn_search.Margin = new System.Windows.Forms.Padding(0);
            this.btn_search.Name = "btn_search";
            this.btn_search.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btn_search.Size = new System.Drawing.Size(99, 25);
            this.btn_search.TabIndex = 2;
            this.btn_search.Text = "조회";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.MouseLeave += new System.EventHandler(this.btn_search_MouseLeave);
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            this.btn_search.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_search_MouseDown);
            this.btn_search.MouseHover += new System.EventHandler(this.btn_search_MouseHover);
            this.btn_search.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_search_MouseUp);
            // 
            // btn_close
            // 
            this.btn_close.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_close_normal;
            this.btn_close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_close.FlatAppearance.BorderSize = 0;
            this.btn_close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_close.ForeColor = System.Drawing.Color.White;
            this.btn_close.Location = new System.Drawing.Point(558, 22);
            this.btn_close.Name = "btn_close";
            this.btn_close.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btn_close.Size = new System.Drawing.Size(99, 25);
            this.btn_close.TabIndex = 3;
            this.btn_close.Text = "닫기";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.MouseLeave += new System.EventHandler(this.btn_close_MouseLeave);
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            this.btn_close.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_close_MouseDown);
            this.btn_close.MouseHover += new System.EventHandler(this.btn_close_MouseHover);
            this.btn_close.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_close_MouseUp);
            // 
            // cb_lb_name
            // 
            this.cb_lb_name.FormattingEnabled = true;
            this.cb_lb_name.Location = new System.Drawing.Point(93, 22);
            this.cb_lb_name.Name = "cb_lb_name";
            this.cb_lb_name.Size = new System.Drawing.Size(121, 20);
            this.cb_lb_name.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(30, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "로비 이름";
            // 
            // FormCrtCallLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1273, 509);
            this.Controls.Add(this.customRoundGroupBox1);
            this.Controls.Add(this.lv_log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCrtCallLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "엘리베이터 호출 기록 확인";
            this.Load += new System.EventHandler(this.FormCrtCallLog_Load);
            this.customRoundGroupBox1.ResumeLayout(false);
            this.customRoundGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_log;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.ComboBox cb_lb_name;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Label label3;
        private CustomRoundGroupBox customRoundGroupBox1;
    }
}