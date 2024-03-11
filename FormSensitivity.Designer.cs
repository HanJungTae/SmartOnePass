namespace SmartOnePass
{
    partial class FormSensitivity
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSensitivity));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.customRoundGroupBox1 = new SmartOnePass.CustomRoundGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_dong_name = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rb_total = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.rb_dong = new System.Windows.Forms.RadioButton();
            this.btnSend = new System.Windows.Forms.Button();
            this.rb_each = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.cb_lb_name = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_distance_value = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_sensitivity_value = new System.Windows.Forms.ComboBox();
            this.cb_speed_value = new System.Windows.Forms.ComboBox();
            this.customRoundGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(8, 211);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(533, 216);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.listView1.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.listView1.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 8;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "로비 이름";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 119;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "감도 거리";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 135;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "감지 감도";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 121;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "반응 속도";
            this.columnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader5.Width = 126;
            // 
            // customRoundGroupBox1
            // 
            this.customRoundGroupBox1.Controls.Add(this.label6);
            this.customRoundGroupBox1.Controls.Add(this.cb_dong_name);
            this.customRoundGroupBox1.Controls.Add(this.label4);
            this.customRoundGroupBox1.Controls.Add(this.rb_total);
            this.customRoundGroupBox1.Controls.Add(this.label3);
            this.customRoundGroupBox1.Controls.Add(this.rb_dong);
            this.customRoundGroupBox1.Controls.Add(this.btnSend);
            this.customRoundGroupBox1.Controls.Add(this.rb_each);
            this.customRoundGroupBox1.Controls.Add(this.label5);
            this.customRoundGroupBox1.Controls.Add(this.cb_lb_name);
            this.customRoundGroupBox1.Controls.Add(this.label2);
            this.customRoundGroupBox1.Controls.Add(this.cb_distance_value);
            this.customRoundGroupBox1.Controls.Add(this.label1);
            this.customRoundGroupBox1.Controls.Add(this.cb_sensitivity_value);
            this.customRoundGroupBox1.Controls.Add(this.cb_speed_value);
            this.customRoundGroupBox1.Location = new System.Drawing.Point(8, 4);
            this.customRoundGroupBox1.Name = "customRoundGroupBox1";
            this.customRoundGroupBox1.Size = new System.Drawing.Size(533, 192);
            this.customRoundGroupBox1.TabIndex = 1;
            this.customRoundGroupBox1.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(212, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(300, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "(0x04~0x10까지, 0x04가 가장 빠름, 0x10이 가장 느림)";
            // 
            // cb_dong_name
            // 
            this.cb_dong_name.FormattingEnabled = true;
            this.cb_dong_name.Location = new System.Drawing.Point(138, 20);
            this.cb_dong_name.Name = "cb_dong_name";
            this.cb_dong_name.Size = new System.Drawing.Size(121, 20);
            this.cb_dong_name.TabIndex = 1;
            this.cb_dong_name.SelectedIndexChanged += new System.EventHandler(this.cb_dong_name_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(212, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(316, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "(0x04~0x10까지, 0x04가 가장 민감, 0x10이 가장 둔감 함)";
            // 
            // rb_total
            // 
            this.rb_total.AutoSize = true;
            this.rb_total.Checked = true;
            this.rb_total.ForeColor = System.Drawing.Color.White;
            this.rb_total.Location = new System.Drawing.Point(15, 22);
            this.rb_total.Name = "rb_total";
            this.rb_total.Size = new System.Drawing.Size(47, 16);
            this.rb_total.TabIndex = 0;
            this.rb_total.TabStop = true;
            this.rb_total.Text = "전체";
            this.rb_total.UseVisualStyleBackColor = true;
            this.rb_total.CheckedChanged += new System.EventHandler(this.rb_total_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(213, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(272, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "(0x02~0x08까지, 0x02가 가까움, 0x08이 가장 멂)";
            // 
            // rb_dong
            // 
            this.rb_dong.AutoSize = true;
            this.rb_dong.ForeColor = System.Drawing.Color.White;
            this.rb_dong.Location = new System.Drawing.Point(85, 22);
            this.rb_dong.Name = "rb_dong";
            this.rb_dong.Size = new System.Drawing.Size(47, 16);
            this.rb_dong.TabIndex = 0;
            this.rb_dong.Text = "동별";
            this.rb_dong.UseVisualStyleBackColor = true;
            this.rb_dong.CheckedChanged += new System.EventHandler(this.rb_dong_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_send_normal;
            this.btnSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(15, 155);
            this.btnSend.Name = "btnSend";
            this.btnSend.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.btnSend.Size = new System.Drawing.Size(88, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "전송";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.MouseLeave += new System.EventHandler(this.btnSend_MouseLeave);
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            this.btnSend.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnSend_MouseDown);
            this.btnSend.MouseHover += new System.EventHandler(this.btnSend_MouseHover);
            this.btnSend.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnSend_MouseUp);
            // 
            // rb_each
            // 
            this.rb_each.AutoSize = true;
            this.rb_each.ForeColor = System.Drawing.Color.White;
            this.rb_each.Location = new System.Drawing.Point(290, 22);
            this.rb_each.Name = "rb_each";
            this.rb_each.Size = new System.Drawing.Size(47, 16);
            this.rb_each.TabIndex = 0;
            this.rb_each.Text = "개별";
            this.rb_each.UseVisualStyleBackColor = true;
            this.rb_each.CheckedChanged += new System.EventHandler(this.rb_each_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(16, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "반응 속도:";
            // 
            // cb_lb_name
            // 
            this.cb_lb_name.FormattingEnabled = true;
            this.cb_lb_name.Location = new System.Drawing.Point(343, 20);
            this.cb_lb_name.Name = "cb_lb_name";
            this.cb_lb_name.Size = new System.Drawing.Size(121, 20);
            this.cb_lb_name.TabIndex = 1;
            this.cb_lb_name.SelectedIndexChanged += new System.EventHandler(this.cb_lb_name_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "감지 감도:";
            // 
            // cb_distance_value
            // 
            this.cb_distance_value.FormattingEnabled = true;
            this.cb_distance_value.Items.AddRange(new object[] {
            "0x02",
            "0x03",
            "0x04",
            "0x05",
            "0x06",
            "0x07",
            "0x08"});
            this.cb_distance_value.Location = new System.Drawing.Point(85, 54);
            this.cb_distance_value.Name = "cb_distance_value";
            this.cb_distance_value.Size = new System.Drawing.Size(121, 20);
            this.cb_distance_value.TabIndex = 1;
            this.cb_distance_value.SelectedIndexChanged += new System.EventHandler(this.cb_distance_value_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "감지 거리:";
            // 
            // cb_sensitivity_value
            // 
            this.cb_sensitivity_value.FormattingEnabled = true;
            this.cb_sensitivity_value.Items.AddRange(new object[] {
            "0x04",
            "0x05",
            "0x06",
            "0x07",
            "0x08",
            "0x09",
            "0x0A",
            "0x0B",
            "0x0C",
            "0x0D",
            "0x0E",
            "0x0F",
            "0x10"});
            this.cb_sensitivity_value.Location = new System.Drawing.Point(85, 88);
            this.cb_sensitivity_value.Name = "cb_sensitivity_value";
            this.cb_sensitivity_value.Size = new System.Drawing.Size(121, 20);
            this.cb_sensitivity_value.TabIndex = 1;
            this.cb_sensitivity_value.SelectedIndexChanged += new System.EventHandler(this.cb_sensitivity_value_SelectedIndexChanged);
            // 
            // cb_speed_value
            // 
            this.cb_speed_value.FormattingEnabled = true;
            this.cb_speed_value.Items.AddRange(new object[] {
            "0x04",
            "0x05",
            "0x06",
            "0x07",
            "0x08",
            "0x09",
            "0x0A",
            "0x0B",
            "0x0C",
            "0x0D",
            "0x0E",
            "0x0F",
            "0x10"});
            this.cb_speed_value.Location = new System.Drawing.Point(85, 123);
            this.cb_speed_value.Name = "cb_speed_value";
            this.cb_speed_value.Size = new System.Drawing.Size(121, 20);
            this.cb_speed_value.TabIndex = 1;
            this.cb_speed_value.SelectedIndexChanged += new System.EventHandler(this.cb_speed_value_SelectedIndexChanged);
            // 
            // FormSensitivity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(549, 439);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.customRoundGroupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSensitivity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "감도 조절";
            this.Load += new System.EventHandler(this.FormSensitivity_Load);
            this.customRoundGroupBox1.ResumeLayout(false);
            this.customRoundGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_total;
        private System.Windows.Forms.RadioButton rb_each;
        private System.Windows.Forms.RadioButton rb_dong;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_distance_value;
        private System.Windows.Forms.ComboBox cb_lb_name;
        private System.Windows.Forms.ComboBox cb_dong_name;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cb_speed_value;
        private System.Windows.Forms.ComboBox cb_sensitivity_value;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private CustomRoundGroupBox customRoundGroupBox1;
    }
}