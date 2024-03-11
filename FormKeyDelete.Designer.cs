namespace SmartOnePass
{
    partial class FormKeyDelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormKeyDelete));
            this.lv_key_info = new System.Windows.Forms.ListView();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_Explanation = new System.Windows.Forms.Label();
            this.btn_keyId__del = new System.Windows.Forms.Button();
            this.txt_del_dong = new System.Windows.Forms.TextBox();
            this.btn_keyid_load = new System.Windows.Forms.Button();
            this.txt_del_ho = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv_key_info
            // 
            this.lv_key_info.CheckBoxes = true;
            this.lv_key_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader1,
            this.columnHeader2});
            this.lv_key_info.FullRowSelect = true;
            this.lv_key_info.GridLines = true;
            this.lv_key_info.Location = new System.Drawing.Point(12, 122);
            this.lv_key_info.Name = "lv_key_info";
            this.lv_key_info.Size = new System.Drawing.Size(706, 257);
            this.lv_key_info.TabIndex = 1;
            this.lv_key_info.UseCompatibleStateImageBehavior = false;
            this.lv_key_info.View = System.Windows.Forms.View.Details;
            this.lv_key_info.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.lv_key_info.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.lv_key_info.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "";
            this.columnHeader18.Width = 30;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "동";
            this.columnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader19.Width = 80;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "호";
            this.columnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader20.Width = 80;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Key Sn";
            this.columnHeader21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader21.Width = 100;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Key ID";
            this.columnHeader22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader22.Width = 200;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "키 타입";
            this.columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader1.Width = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbl_Explanation);
            this.groupBox1.Controls.Add(this.btn_keyId__del);
            this.groupBox1.Controls.Add(this.txt_del_dong);
            this.groupBox1.Controls.Add(this.btn_keyid_load);
            this.groupBox1.Controls.Add(this.txt_del_ho);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(706, 104);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // lbl_Explanation
            // 
            this.lbl_Explanation.AutoSize = true;
            this.lbl_Explanation.ForeColor = System.Drawing.Color.White;
            this.lbl_Explanation.Location = new System.Drawing.Point(14, 57);
            this.lbl_Explanation.Name = "lbl_Explanation";
            this.lbl_Explanation.Size = new System.Drawing.Size(0, 12);
            this.lbl_Explanation.TabIndex = 14;
            // 
            // btn_keyId__del
            // 
            this.btn_keyId__del.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_keyId__del.BackgroundImage")));
            this.btn_keyId__del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_keyId__del.FlatAppearance.BorderSize = 0;
            this.btn_keyId__del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keyId__del.Font = new System.Drawing.Font("굴림", 8F);
            this.btn_keyId__del.ForeColor = System.Drawing.Color.White;
            this.btn_keyId__del.Location = new System.Drawing.Point(423, 26);
            this.btn_keyId__del.Name = "btn_keyId__del";
            this.btn_keyId__del.Padding = new System.Windows.Forms.Padding(18, 2, 0, 0);
            this.btn_keyId__del.Size = new System.Drawing.Size(88, 23);
            this.btn_keyId__del.TabIndex = 13;
            this.btn_keyId__del.Text = "키 ID 삭제";
            this.btn_keyId__del.UseVisualStyleBackColor = true;
            this.btn_keyId__del.MouseLeave += new System.EventHandler(this.btn_keyId__del_MouseLeave);
            this.btn_keyId__del.Click += new System.EventHandler(this.btn_keyId__del_Click);
            this.btn_keyId__del.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_keyId__del_MouseDown);
            this.btn_keyId__del.MouseHover += new System.EventHandler(this.btn_keyId__del_MouseHover);
            this.btn_keyId__del.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_keyId__del_MouseUp);
            // 
            // txt_del_dong
            // 
            this.txt_del_dong.Location = new System.Drawing.Point(41, 28);
            this.txt_del_dong.Name = "txt_del_dong";
            this.txt_del_dong.Size = new System.Drawing.Size(54, 21);
            this.txt_del_dong.TabIndex = 7;
            // 
            // btn_keyid_load
            // 
            this.btn_keyid_load.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_keyid_load.BackgroundImage")));
            this.btn_keyid_load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_keyid_load.FlatAppearance.BorderSize = 0;
            this.btn_keyid_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keyid_load.ForeColor = System.Drawing.Color.White;
            this.btn_keyid_load.Location = new System.Drawing.Point(225, 26);
            this.btn_keyid_load.Name = "btn_keyid_load";
            this.btn_keyid_load.Padding = new System.Windows.Forms.Padding(10, 1, 0, 0);
            this.btn_keyid_load.Size = new System.Drawing.Size(88, 23);
            this.btn_keyid_load.TabIndex = 11;
            this.btn_keyid_load.Text = "조회";
            this.btn_keyid_load.UseVisualStyleBackColor = true;
            this.btn_keyid_load.MouseLeave += new System.EventHandler(this.btn_keyid_load_MouseLeave);
            this.btn_keyid_load.Click += new System.EventHandler(this.btn_keyid_load_Click);
            this.btn_keyid_load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_keyid_load_MouseDown);
            this.btn_keyid_load.MouseHover += new System.EventHandler(this.btn_keyid_load_MouseHover);
            this.btn_keyid_load.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_keyid_load_MouseUp);
            // 
            // txt_del_ho
            // 
            this.txt_del_ho.Location = new System.Drawing.Point(149, 28);
            this.txt_del_ho.Name = "txt_del_ho";
            this.txt_del_ho.Size = new System.Drawing.Size(54, 21);
            this.txt_del_ho.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(14, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "동:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(124, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "호:";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "차량 번호";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 120;
            // 
            // FormKeyDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 391);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lv_key_info);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormKeyDelete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "키 삭제";
            this.Load += new System.EventHandler(this.FormKeyDelete_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_key_info;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_keyId__del;
        private System.Windows.Forms.TextBox txt_del_dong;
        private System.Windows.Forms.Button btn_keyid_load;
        private System.Windows.Forms.TextBox txt_del_ho;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Explanation;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
    }
}