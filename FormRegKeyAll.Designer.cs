namespace SmartOnePass
{
    partial class FormRegKeyAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegKeyAll));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txb_IPAddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Reg = new System.Windows.Forms.Button();
            this.btn_FileOpen = new System.Windows.Forms.Button();
            this.rb_ConEveryTime = new System.Windows.Forms.RadioButton();
            this.rb_ConOnce = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lv_PacketList = new System.Windows.Forms.ListView();
            this.bw_KeyControl = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txbPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txb_IPAddr);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 57);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "출입통제 서버 정보";
            // 
            // txbPort
            // 
            this.txbPort.Location = new System.Drawing.Point(324, 24);
            this.txbPort.Name = "txbPort";
            this.txbPort.Size = new System.Drawing.Size(100, 21);
            this.txbPort.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT";
            // 
            // txb_IPAddr
            // 
            this.txb_IPAddr.Location = new System.Drawing.Point(66, 24);
            this.txb_IPAddr.Name = "txb_IPAddr";
            this.txb_IPAddr.Size = new System.Drawing.Size(194, 21);
            this.txb_IPAddr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Close);
            this.groupBox2.Controls.Add(this.btn_Reg);
            this.groupBox2.Controls.Add(this.btn_FileOpen);
            this.groupBox2.Controls.Add(this.rb_ConEveryTime);
            this.groupBox2.Controls.Add(this.rb_ConOnce);
            this.groupBox2.Location = new System.Drawing.Point(483, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(427, 57);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "등록 관리";
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(346, 20);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(75, 23);
            this.btn_Close.TabIndex = 4;
            this.btn_Close.Text = "종료";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Reg
            // 
            this.btn_Reg.Location = new System.Drawing.Point(265, 20);
            this.btn_Reg.Name = "btn_Reg";
            this.btn_Reg.Size = new System.Drawing.Size(75, 23);
            this.btn_Reg.TabIndex = 3;
            this.btn_Reg.Text = "등록";
            this.btn_Reg.UseVisualStyleBackColor = true;
            this.btn_Reg.Click += new System.EventHandler(this.btn_Reg_Click);
            // 
            // btn_FileOpen
            // 
            this.btn_FileOpen.Location = new System.Drawing.Point(184, 20);
            this.btn_FileOpen.Name = "btn_FileOpen";
            this.btn_FileOpen.Size = new System.Drawing.Size(75, 23);
            this.btn_FileOpen.TabIndex = 2;
            this.btn_FileOpen.Text = "파일 열기";
            this.btn_FileOpen.UseVisualStyleBackColor = true;
            this.btn_FileOpen.Click += new System.EventHandler(this.btn_FileOpen_Click);
            // 
            // rb_ConEveryTime
            // 
            this.rb_ConEveryTime.AutoSize = true;
            this.rb_ConEveryTime.Location = new System.Drawing.Point(107, 25);
            this.rb_ConEveryTime.Name = "rb_ConEveryTime";
            this.rb_ConEveryTime.Size = new System.Drawing.Size(71, 16);
            this.rb_ConEveryTime.TabIndex = 1;
            this.rb_ConEveryTime.TabStop = true;
            this.rb_ConEveryTime.Text = "개별접속";
            this.rb_ConEveryTime.UseVisualStyleBackColor = true;
            // 
            // rb_ConOnce
            // 
            this.rb_ConOnce.AutoSize = true;
            this.rb_ConOnce.Location = new System.Drawing.Point(6, 25);
            this.rb_ConOnce.Name = "rb_ConOnce";
            this.rb_ConOnce.Size = new System.Drawing.Size(95, 16);
            this.rb_ConOnce.TabIndex = 0;
            this.rb_ConOnce.TabStop = true;
            this.rb_ConOnce.Text = "전체접속유지";
            this.rb_ConOnce.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lv_PacketList);
            this.groupBox3.Location = new System.Drawing.Point(12, 75);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(898, 466);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "패킷리스트";
            // 
            // lv_PacketList
            // 
            this.lv_PacketList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lv_PacketList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_PacketList.Location = new System.Drawing.Point(3, 17);
            this.lv_PacketList.Name = "lv_PacketList";
            this.lv_PacketList.Size = new System.Drawing.Size(892, 446);
            this.lv_PacketList.TabIndex = 0;
            this.lv_PacketList.UseCompatibleStateImageBehavior = false;
            // 
            // bw_KeyControl
            // 
            this.bw_KeyControl.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw_KeyControl_DoWork);
            // 
            // FormRegKeyAll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 553);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormRegKeyAll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "출입통제 일괄 등록";
            this.Load += new System.EventHandler(this.FormRegKeyAll_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txb_IPAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Reg;
        private System.Windows.Forms.Button btn_FileOpen;
        private System.Windows.Forms.RadioButton rb_ConEveryTime;
        private System.Windows.Forms.RadioButton rb_ConOnce;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView lv_PacketList;
        private System.ComponentModel.BackgroundWorker bw_KeyControl;
    }
}