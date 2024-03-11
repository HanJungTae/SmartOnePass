namespace SmartOnePass
{
    partial class FormReset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormReset));
            this.customRoundGroupBox2 = new SmartOnePass.CustomRoundGroupBox();
            this._chbUseTimer = new System.Windows.Forms.CheckBox();
            this._chbRetry = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._btnTimerStop = new System.Windows.Forms.Button();
            this._chbTimerAM = new System.Windows.Forms.CheckBox();
            this._btnTimerStart = new System.Windows.Forms.Button();
            this._chbTimerFM = new System.Windows.Forms.CheckBox();
            this._btnTimerSet = new System.Windows.Forms.Button();
            this._cmbHourAM = new System.Windows.Forms.ComboBox();
            this._cmbHourFM = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this._cmbMinuteAM = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._cmbMinuteFM = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.customRoundGroupBox1 = new SmartOnePass.CustomRoundGroupBox();
            this._btnAllReset = new System.Windows.Forms.Button();
            this._cbxLobbyName = new System.Windows.Forms.ComboBox();
            this._btnReset = new System.Windows.Forms.Button();
            this.customRoundGroupBox2.SuspendLayout();
            this.customRoundGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // customRoundGroupBox2
            // 
            this.customRoundGroupBox2.Controls.Add(this._chbUseTimer);
            this.customRoundGroupBox2.Controls.Add(this._chbRetry);
            this.customRoundGroupBox2.Controls.Add(this.label1);
            this.customRoundGroupBox2.Controls.Add(this._btnTimerStop);
            this.customRoundGroupBox2.Controls.Add(this._chbTimerAM);
            this.customRoundGroupBox2.Controls.Add(this._btnTimerStart);
            this.customRoundGroupBox2.Controls.Add(this._chbTimerFM);
            this.customRoundGroupBox2.Controls.Add(this._btnTimerSet);
            this.customRoundGroupBox2.Controls.Add(this._cmbHourAM);
            this.customRoundGroupBox2.Controls.Add(this._cmbHourFM);
            this.customRoundGroupBox2.Controls.Add(this.label5);
            this.customRoundGroupBox2.Controls.Add(this._cmbMinuteAM);
            this.customRoundGroupBox2.Controls.Add(this.label4);
            this.customRoundGroupBox2.Controls.Add(this.label2);
            this.customRoundGroupBox2.Controls.Add(this._cmbMinuteFM);
            this.customRoundGroupBox2.Controls.Add(this.label3);
            this.customRoundGroupBox2.ForeColor = System.Drawing.Color.White;
            this.customRoundGroupBox2.Location = new System.Drawing.Point(5, 79);
            this.customRoundGroupBox2.Name = "customRoundGroupBox2";
            this.customRoundGroupBox2.Size = new System.Drawing.Size(460, 104);
            this.customRoundGroupBox2.TabIndex = 7;
            this.customRoundGroupBox2.TabStop = false;
            this.customRoundGroupBox2.Text = "리셋 타이머 설정   ";
            // 
            // _chbUseTimer
            // 
            this._chbUseTimer.AutoSize = true;
            this._chbUseTimer.ForeColor = System.Drawing.Color.White;
            this._chbUseTimer.Location = new System.Drawing.Point(10, 31);
            this._chbUseTimer.Name = "_chbUseTimer";
            this._chbUseTimer.Size = new System.Drawing.Size(88, 16);
            this._chbUseTimer.TabIndex = 14;
            this._chbUseTimer.Text = "타이머 사용";
            this._chbUseTimer.UseVisualStyleBackColor = true;
            this._chbUseTimer.CheckedChanged += new System.EventHandler(this._chbUseTimer_CheckedChanged);
            // 
            // _chbRetry
            // 
            this._chbRetry.AutoSize = true;
            this._chbRetry.ForeColor = System.Drawing.Color.White;
            this._chbRetry.Location = new System.Drawing.Point(319, 20);
            this._chbRetry.Name = "_chbRetry";
            this._chbRetry.Size = new System.Drawing.Size(108, 16);
            this._chbRetry.TabIndex = 10;
            this._chbRetry.Text = "실패 시 재 전송";
            this._chbRetry.UseVisualStyleBackColor = true;
            this._chbRetry.Visible = false;
            this._chbRetry.CheckedChanged += new System.EventHandler(this._chbRetry_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "시간";
            // 
            // _btnTimerStop
            // 
            this._btnTimerStop.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_stop_normal;
            this._btnTimerStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._btnTimerStop.FlatAppearance.BorderSize = 0;
            this._btnTimerStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTimerStop.ForeColor = System.Drawing.Color.White;
            this._btnTimerStop.Location = new System.Drawing.Point(248, 15);
            this._btnTimerStop.Name = "_btnTimerStop";
            this._btnTimerStop.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this._btnTimerStop.Size = new System.Drawing.Size(94, 25);
            this._btnTimerStop.TabIndex = 13;
            this._btnTimerStop.Text = "정지";
            this._btnTimerStop.UseVisualStyleBackColor = true;
            this._btnTimerStop.Visible = false;
            this._btnTimerStop.MouseLeave += new System.EventHandler(this._btnTimerStop_MouseLeave);
            this._btnTimerStop.Click += new System.EventHandler(this._btnTimerStop_Click);
            this._btnTimerStop.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnTimerStop_MouseDown);
            this._btnTimerStop.MouseHover += new System.EventHandler(this._btnTimerStop_MouseHover);
            this._btnTimerStop.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnTimerStop_MouseUp);
            this._btnTimerStop.EnabledChanged += new System.EventHandler(this._btnTimerStop_EnabledChanged);
            // 
            // _chbTimerAM
            // 
            this._chbTimerAM.AutoSize = true;
            this._chbTimerAM.ForeColor = System.Drawing.Color.White;
            this._chbTimerAM.Location = new System.Drawing.Point(265, 20);
            this._chbTimerAM.Name = "_chbTimerAM";
            this._chbTimerAM.Size = new System.Drawing.Size(48, 16);
            this._chbTimerAM.TabIndex = 0;
            this._chbTimerAM.Text = "오전";
            this._chbTimerAM.UseVisualStyleBackColor = true;
            this._chbTimerAM.Visible = false;
            this._chbTimerAM.CheckedChanged += new System.EventHandler(this._chbTimerAM_CheckedChanged);
            // 
            // _btnTimerStart
            // 
            this._btnTimerStart.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_start_normal;
            this._btnTimerStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._btnTimerStart.FlatAppearance.BorderSize = 0;
            this._btnTimerStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTimerStart.ForeColor = System.Drawing.Color.White;
            this._btnTimerStart.Location = new System.Drawing.Point(357, 15);
            this._btnTimerStart.Name = "_btnTimerStart";
            this._btnTimerStart.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this._btnTimerStart.Size = new System.Drawing.Size(94, 25);
            this._btnTimerStart.TabIndex = 12;
            this._btnTimerStart.Text = "시작";
            this._btnTimerStart.UseVisualStyleBackColor = true;
            this._btnTimerStart.Visible = false;
            this._btnTimerStart.MouseLeave += new System.EventHandler(this._btnTimerStart_MouseLeave);
            this._btnTimerStart.Click += new System.EventHandler(this._btnTimerStart_Click);
            this._btnTimerStart.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnTimerStart_MouseDown);
            this._btnTimerStart.MouseHover += new System.EventHandler(this._btnTimerStart_MouseHover);
            this._btnTimerStart.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnTimerStart_MouseUp);
            this._btnTimerStart.EnabledChanged += new System.EventHandler(this._btnTimerStart_EnabledChanged);
            // 
            // _chbTimerFM
            // 
            this._chbTimerFM.AutoSize = true;
            this._chbTimerFM.ForeColor = System.Drawing.Color.White;
            this._chbTimerFM.Location = new System.Drawing.Point(256, 22);
            this._chbTimerFM.Name = "_chbTimerFM";
            this._chbTimerFM.Size = new System.Drawing.Size(48, 16);
            this._chbTimerFM.TabIndex = 1;
            this._chbTimerFM.Text = "오후";
            this._chbTimerFM.UseVisualStyleBackColor = true;
            this._chbTimerFM.Visible = false;
            this._chbTimerFM.CheckedChanged += new System.EventHandler(this._chbTimerFM_CheckedChanged);
            // 
            // _btnTimerSet
            // 
            this._btnTimerSet.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_setting_save_normal;
            this._btnTimerSet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._btnTimerSet.FlatAppearance.BorderSize = 0;
            this._btnTimerSet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnTimerSet.ForeColor = System.Drawing.Color.White;
            this._btnTimerSet.Location = new System.Drawing.Point(235, 65);
            this._btnTimerSet.Name = "_btnTimerSet";
            this._btnTimerSet.Padding = new System.Windows.Forms.Padding(14, 2, 0, 0);
            this._btnTimerSet.Size = new System.Drawing.Size(94, 25);
            this._btnTimerSet.TabIndex = 11;
            this._btnTimerSet.Text = "설정 저장";
            this._btnTimerSet.UseVisualStyleBackColor = true;
            this._btnTimerSet.MouseLeave += new System.EventHandler(this._btnTimerSet_MouseLeave);
            this._btnTimerSet.Click += new System.EventHandler(this._btnTimerSet_Click);
            this._btnTimerSet.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnTimerSet_MouseDown);
            this._btnTimerSet.MouseHover += new System.EventHandler(this._btnTimerSet_MouseHover);
            this._btnTimerSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnTimerSet_MouseUp);
            // 
            // _cmbHourAM
            // 
            this._cmbHourAM.Enabled = false;
            this._cmbHourAM.FormattingEnabled = true;
            this._cmbHourAM.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this._cmbHourAM.Location = new System.Drawing.Point(61, 68);
            this._cmbHourAM.Name = "_cmbHourAM";
            this._cmbHourAM.Size = new System.Drawing.Size(40, 20);
            this._cmbHourAM.TabIndex = 2;
            this._cmbHourAM.Text = "1";
            // 
            // _cmbHourFM
            // 
            this._cmbHourFM.Enabled = false;
            this._cmbHourFM.FormattingEnabled = true;
            this._cmbHourFM.Items.AddRange(new object[] {
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24"});
            this._cmbHourFM.Location = new System.Drawing.Point(319, 20);
            this._cmbHourFM.Name = "_cmbHourFM";
            this._cmbHourFM.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._cmbHourFM.Size = new System.Drawing.Size(40, 20);
            this._cmbHourFM.TabIndex = 3;
            this._cmbHourFM.Text = "13";
            this._cmbHourFM.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(433, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "분";
            this.label5.Visible = false;
            // 
            // _cmbMinuteAM
            // 
            this._cmbMinuteAM.Enabled = false;
            this._cmbMinuteAM.FormattingEnabled = true;
            this._cmbMinuteAM.Items.AddRange(new object[] {
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"});
            this._cmbMinuteAM.Location = new System.Drawing.Point(130, 68);
            this._cmbMinuteAM.Name = "_cmbMinuteAM";
            this._cmbMinuteAM.Size = new System.Drawing.Size(40, 20);
            this._cmbMinuteAM.TabIndex = 4;
            this._cmbMinuteAM.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(176, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "분";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(107, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "시";
            // 
            // _cmbMinuteFM
            // 
            this._cmbMinuteFM.Enabled = false;
            this._cmbMinuteFM.FormattingEnabled = true;
            this._cmbMinuteFM.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50"});
            this._cmbMinuteFM.Location = new System.Drawing.Point(395, 21);
            this._cmbMinuteFM.Name = "_cmbMinuteFM";
            this._cmbMinuteFM.Size = new System.Drawing.Size(40, 20);
            this._cmbMinuteFM.TabIndex = 7;
            this._cmbMinuteFM.Text = "0";
            this._cmbMinuteFM.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(355, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "시";
            this.label3.Visible = false;
            // 
            // customRoundGroupBox1
            // 
            this.customRoundGroupBox1.Controls.Add(this._btnAllReset);
            this.customRoundGroupBox1.Controls.Add(this._cbxLobbyName);
            this.customRoundGroupBox1.Controls.Add(this._btnReset);
            this.customRoundGroupBox1.ForeColor = System.Drawing.Color.White;
            this.customRoundGroupBox1.Location = new System.Drawing.Point(5, 5);
            this.customRoundGroupBox1.Name = "customRoundGroupBox1";
            this.customRoundGroupBox1.Size = new System.Drawing.Size(460, 60);
            this.customRoundGroupBox1.TabIndex = 6;
            this.customRoundGroupBox1.TabStop = false;
            this.customRoundGroupBox1.Text = "리셋 ";
            // 
            // _btnAllReset
            // 
            this._btnAllReset.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_all_reset_normal;
            this._btnAllReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._btnAllReset.FlatAppearance.BorderSize = 0;
            this._btnAllReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnAllReset.ForeColor = System.Drawing.Color.White;
            this._btnAllReset.Location = new System.Drawing.Point(356, 22);
            this._btnAllReset.Name = "_btnAllReset";
            this._btnAllReset.Padding = new System.Windows.Forms.Padding(12, 2, 0, 0);
            this._btnAllReset.Size = new System.Drawing.Size(94, 25);
            this._btnAllReset.TabIndex = 3;
            this._btnAllReset.Text = "전체리셋";
            this._btnAllReset.UseVisualStyleBackColor = true;
            this._btnAllReset.MouseLeave += new System.EventHandler(this._btnAllReset_MouseLeave);
            this._btnAllReset.Click += new System.EventHandler(this._btnAllReset_Click);
            this._btnAllReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnAllReset_MouseDown);
            this._btnAllReset.MouseHover += new System.EventHandler(this._btnAllReset_MouseHover);
            this._btnAllReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnAllReset_MouseUp);
            // 
            // _cbxLobbyName
            // 
            this._cbxLobbyName.FormattingEnabled = true;
            this._cbxLobbyName.Location = new System.Drawing.Point(75, 24);
            this._cbxLobbyName.Name = "_cbxLobbyName";
            this._cbxLobbyName.Size = new System.Drawing.Size(163, 20);
            this._cbxLobbyName.TabIndex = 1;
            // 
            // _btnReset
            // 
            this._btnReset.BackgroundImage = global::SmartOnePass.Properties.Resources.btn_reset_normal;
            this._btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._btnReset.FlatAppearance.BorderSize = 0;
            this._btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnReset.ForeColor = System.Drawing.Color.White;
            this._btnReset.Location = new System.Drawing.Point(259, 22);
            this._btnReset.Name = "_btnReset";
            this._btnReset.Padding = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this._btnReset.Size = new System.Drawing.Size(94, 25);
            this._btnReset.TabIndex = 2;
            this._btnReset.Text = "리셋";
            this._btnReset.UseVisualStyleBackColor = true;
            this._btnReset.MouseLeave += new System.EventHandler(this._btnReset_MouseLeave);
            this._btnReset.Click += new System.EventHandler(this._btnReset_Click);
            this._btnReset.MouseDown += new System.Windows.Forms.MouseEventHandler(this._btnReset_MouseDown);
            this._btnReset.MouseHover += new System.EventHandler(this._btnReset_MouseHover);
            this._btnReset.MouseUp += new System.Windows.Forms.MouseEventHandler(this._btnReset_MouseUp);
            // 
            // FormReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 189);
            this.Controls.Add(this.customRoundGroupBox2);
            this.Controls.Add(this.customRoundGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormReset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "리셋 ";
            this.Load += new System.EventHandler(this.FormReset_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormReset_FormClosed);
            this.customRoundGroupBox2.ResumeLayout(false);
            this.customRoundGroupBox2.PerformLayout();
            this.customRoundGroupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _cbxLobbyName;
        private System.Windows.Forms.Button _btnReset;
        private System.Windows.Forms.Button _btnAllReset;
        private System.Windows.Forms.CheckBox _chbTimerFM;
        private System.Windows.Forms.CheckBox _chbTimerAM;
        private System.Windows.Forms.ComboBox _cmbHourFM;
        private System.Windows.Forms.ComboBox _cmbHourAM;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox _cmbMinuteFM;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cmbMinuteAM;
        private System.Windows.Forms.CheckBox _chbRetry;
        private System.Windows.Forms.Button _btnTimerSet;
        private System.Windows.Forms.Button _btnTimerStop;
        private System.Windows.Forms.Button _btnTimerStart;
        private System.Windows.Forms.CheckBox _chbUseTimer;
        private CustomRoundGroupBox customRoundGroupBox1;
        private CustomRoundGroupBox customRoundGroupBox2;
    }
}