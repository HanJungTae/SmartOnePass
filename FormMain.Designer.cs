namespace SmartOnePass
{
    partial class FormMain
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.splitContainer6 = new System.Windows.Forms.SplitContainer();
            this.cb_charger_portable = new System.Windows.Forms.CheckBox();
            this.cb_charger_normal = new System.Windows.Forms.CheckBox();
            this.txtCarNum = new System.Windows.Forms.TextBox();
            this.lblCarNum = new System.Windows.Forms.Label();
            this.txt_reg_nfcid = new System.Windows.Forms.TextBox();
            this.nfc_lbl_id = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_key_reg = new System.Windows.Forms.Button();
            this.btn_bleid_load = new System.Windows.Forms.Button();
            this.lv_key_info = new System.Windows.Forms.ListView();
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.btn_keyId_list_del = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_reg_keyid = new System.Windows.Forms.TextBox();
            this.btn_keyId_list_reg = new System.Windows.Forms.Button();
            this.txt_reg_dong = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btn_keyid_load = new System.Windows.Forms.Button();
            this.txt_reg_ho = new System.Windows.Forms.TextBox();
            this.txt_reg_keysn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nfc_lbl_info = new System.Windows.Forms.Label();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.splitContainer8 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.동기화ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.전체KeyData삽입ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.부분KeyData삽입ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
            this.설정ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_ReLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItem_CrtReConnection = new System.Windows.Forms.ToolStripMenuItem();
            this.config정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SyncSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_sync_setting = new System.Windows.Forms.ToolStripMenuItem();
            this.점검ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Test_Crt = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolReset = new System.Windows.Forms.ToolStripMenuItem();
            this.네트워크테스트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pING테스트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.결과초기화ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.출입통제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_reg_key_all = new System.Windows.Forms.ToolStripMenuItem();
            this.키삭제ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.KeyDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.등록삭제확인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_regdel_log = new System.Windows.Forms.ToolStripMenuItem();
            this.DevLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CrtCallLogtoolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.OnePassSettingTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.제어ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mwSensitivitytoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.uc_lv_lb_UkrInfo = new SmartOnePass.UC_ListView();
            this.uc_lv_evt_log = new SmartOnePass.UC_ListView();
            this.uc_lv_lb_master = new SmartOnePass.UC_ListView();
            this.uc_lv_lb_reg = new SmartOnePass.UC_ListView();
            this.uc_lv_lb_del = new SmartOnePass.UC_ListView();
            this.splitContainer6.Panel1.SuspendLayout();
            this.splitContainer6.Panel2.SuspendLayout();
            this.splitContainer6.SuspendLayout();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.splitContainer8.Panel1.SuspendLayout();
            this.splitContainer8.Panel2.SuspendLayout();
            this.splitContainer8.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer6
            // 
            this.splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer6.Location = new System.Drawing.Point(0, 0);
            this.splitContainer6.Name = "splitContainer6";
            this.splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            this.splitContainer6.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer6.Panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("splitContainer6.Panel1.BackgroundImage")));
            this.splitContainer6.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer6.Panel1.Controls.Add(this.cb_charger_portable);
            this.splitContainer6.Panel1.Controls.Add(this.cb_charger_normal);
            this.splitContainer6.Panel1.Controls.Add(this.txtCarNum);
            this.splitContainer6.Panel1.Controls.Add(this.lblCarNum);
            this.splitContainer6.Panel1.Controls.Add(this.txt_reg_nfcid);
            this.splitContainer6.Panel1.Controls.Add(this.nfc_lbl_id);
            this.splitContainer6.Panel1.Controls.Add(this.label2);
            this.splitContainer6.Panel1.Controls.Add(this.btn_key_reg);
            this.splitContainer6.Panel1.Controls.Add(this.btn_bleid_load);
            this.splitContainer6.Panel1.Controls.Add(this.lv_key_info);
            this.splitContainer6.Panel1.Controls.Add(this.label6);
            this.splitContainer6.Panel1.Controls.Add(this.btn_keyId_list_del);
            this.splitContainer6.Panel1.Controls.Add(this.label7);
            this.splitContainer6.Panel1.Controls.Add(this.txt_reg_keyid);
            this.splitContainer6.Panel1.Controls.Add(this.btn_keyId_list_reg);
            this.splitContainer6.Panel1.Controls.Add(this.txt_reg_dong);
            this.splitContainer6.Panel1.Controls.Add(this.label9);
            this.splitContainer6.Panel1.Controls.Add(this.btn_keyid_load);
            this.splitContainer6.Panel1.Controls.Add(this.txt_reg_ho);
            this.splitContainer6.Panel1.Controls.Add(this.txt_reg_keysn);
            this.splitContainer6.Panel1.Controls.Add(this.label8);
            this.splitContainer6.Panel1.Controls.Add(this.nfc_lbl_info);
            // 
            // splitContainer6.Panel2
            // 
            this.splitContainer6.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer6.Size = new System.Drawing.Size(516, 707);
            this.splitContainer6.SplitterDistance = 293;
            this.splitContainer6.TabIndex = 0;
            // 
            // cb_charger_portable
            // 
            this.cb_charger_portable.BackgroundImage = global::SmartOnePass.Properties.Resources.check_bg;
            this.cb_charger_portable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cb_charger_portable.ForeColor = System.Drawing.Color.White;
            this.cb_charger_portable.Location = new System.Drawing.Point(404, 161);
            this.cb_charger_portable.Name = "cb_charger_portable";
            this.cb_charger_portable.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cb_charger_portable.Size = new System.Drawing.Size(139, 23);
            this.cb_charger_portable.TabIndex = 22;
            this.cb_charger_portable.Text = "전기차 이동형 카드";
            this.cb_charger_portable.UseVisualStyleBackColor = true;
            this.cb_charger_portable.Visible = false;
            // 
            // cb_charger_normal
            // 
            this.cb_charger_normal.BackgroundImage = global::SmartOnePass.Properties.Resources.check_bg;
            this.cb_charger_normal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cb_charger_normal.Checked = true;
            this.cb_charger_normal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_charger_normal.ForeColor = System.Drawing.Color.White;
            this.cb_charger_normal.Location = new System.Drawing.Point(254, 161);
            this.cb_charger_normal.Name = "cb_charger_normal";
            this.cb_charger_normal.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.cb_charger_normal.Size = new System.Drawing.Size(148, 23);
            this.cb_charger_normal.TabIndex = 22;
            this.cb_charger_normal.Text = "전기차 과금형 카드";
            this.cb_charger_normal.UseVisualStyleBackColor = true;
            this.cb_charger_normal.Visible = false;
            this.cb_charger_normal.CheckedChanged += new System.EventHandler(this.cb_charger_normal_CheckedChanged);
            // 
            // txtCarNum
            // 
            this.txtCarNum.Location = new System.Drawing.Point(201, 71);
            this.txtCarNum.Name = "txtCarNum";
            this.txtCarNum.Size = new System.Drawing.Size(109, 21);
            this.txtCarNum.TabIndex = 20;
            this.txtCarNum.Visible = false;
            this.txtCarNum.TextChanged += new System.EventHandler(this.txt_reg_nfcid_TextChanged);
            // 
            // lblCarNum
            // 
            this.lblCarNum.AutoSize = true;
            this.lblCarNum.Location = new System.Drawing.Point(140, 78);
            this.lblCarNum.Name = "lblCarNum";
            this.lblCarNum.Size = new System.Drawing.Size(61, 12);
            this.lblCarNum.TabIndex = 19;
            this.lblCarNum.Text = "차량 번호:";
            this.lblCarNum.Visible = false;
            this.lblCarNum.Click += new System.EventHandler(this.nfc_lbl_id_Click);
            // 
            // txt_reg_nfcid
            // 
            this.txt_reg_nfcid.Location = new System.Drawing.Point(200, 126);
            this.txt_reg_nfcid.Name = "txt_reg_nfcid";
            this.txt_reg_nfcid.Size = new System.Drawing.Size(109, 21);
            this.txt_reg_nfcid.TabIndex = 20;
            this.txt_reg_nfcid.Visible = false;
            this.txt_reg_nfcid.TextChanged += new System.EventHandler(this.txt_reg_nfcid_TextChanged);
            // 
            // nfc_lbl_id
            // 
            this.nfc_lbl_id.AutoSize = true;
            this.nfc_lbl_id.Location = new System.Drawing.Point(144, 133);
            this.nfc_lbl_id.Name = "nfc_lbl_id";
            this.nfc_lbl_id.Size = new System.Drawing.Size(49, 12);
            this.nfc_lbl_id.TabIndex = 19;
            this.nfc_lbl_id.Text = "NFC ID:";
            this.nfc_lbl_id.Visible = false;
            this.nfc_lbl_id.Click += new System.EventHandler(this.nfc_lbl_id_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(516, 55);
            this.label2.TabIndex = 17;
            this.label2.Text = "키 ID 등록";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_key_reg
            // 
            this.btn_key_reg.BackColor = System.Drawing.Color.Transparent;
            this.btn_key_reg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_key_reg.BackgroundImage")));
            this.btn_key_reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_key_reg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_key_reg.ForeColor = System.Drawing.Color.White;
            this.btn_key_reg.Location = new System.Drawing.Point(445, 195);
            this.btn_key_reg.Margin = new System.Windows.Forms.Padding(0);
            this.btn_key_reg.Name = "btn_key_reg";
            this.btn_key_reg.Padding = new System.Windows.Forms.Padding(20, 3, 0, 0);
            this.btn_key_reg.Size = new System.Drawing.Size(100, 30);
            this.btn_key_reg.TabIndex = 14;
            this.btn_key_reg.Text = "등록";
            this.btn_key_reg.UseVisualStyleBackColor = false;
            this.btn_key_reg.Click += new System.EventHandler(this.btn_key_reg_Click);
            this.btn_key_reg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_key_reg_MouseDown);
            this.btn_key_reg.MouseLeave += new System.EventHandler(this.btn_key_reg_MouseLeave);
            this.btn_key_reg.MouseHover += new System.EventHandler(this.btn_key_reg_MouseHover);
            this.btn_key_reg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_key_reg_MouseUp);
            // 
            // btn_bleid_load
            // 
            this.btn_bleid_load.BackColor = System.Drawing.Color.Transparent;
            this.btn_bleid_load.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_bleid_load.BackgroundImage")));
            this.btn_bleid_load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_bleid_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_bleid_load.ForeColor = System.Drawing.Color.White;
            this.btn_bleid_load.Location = new System.Drawing.Point(117, 195);
            this.btn_bleid_load.Margin = new System.Windows.Forms.Padding(0);
            this.btn_bleid_load.Name = "btn_bleid_load";
            this.btn_bleid_load.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_bleid_load.Size = new System.Drawing.Size(100, 30);
            this.btn_bleid_load.TabIndex = 16;
            this.btn_bleid_load.Text = "Ble 읽기";
            this.btn_bleid_load.UseVisualStyleBackColor = false;
            this.btn_bleid_load.Click += new System.EventHandler(this.btn_bleid_load_Click);
            this.btn_bleid_load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_bleid_load_MouseDown);
            this.btn_bleid_load.MouseLeave += new System.EventHandler(this.btn_bleid_load_MouseLeave);
            this.btn_bleid_load.MouseHover += new System.EventHandler(this.btn_bleid_load_MouseHover);
            this.btn_bleid_load.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_bleid_load_MouseUp);
            // 
            // lv_key_info
            // 
            this.lv_key_info.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_key_info.CheckBoxes = true;
            this.lv_key_info.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22});
            this.lv_key_info.FullRowSelect = true;
            this.lv_key_info.GridLines = true;
            this.lv_key_info.HideSelection = false;
            this.lv_key_info.Location = new System.Drawing.Point(9, 244);
            this.lv_key_info.Name = "lv_key_info";
            this.lv_key_info.OwnerDraw = true;
            this.lv_key_info.Size = new System.Drawing.Size(495, 49);
            this.lv_key_info.TabIndex = 0;
            this.lv_key_info.UseCompatibleStateImageBehavior = false;
            this.lv_key_info.View = System.Windows.Forms.View.Details;
            this.lv_key_info.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.listView_DrawColumnHeader);
            this.lv_key_info.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.lv_key_info.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.listView_DrawSubItem);
            this.lv_key_info.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_key_info_MouseClick);
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "";
            this.columnHeader18.Width = 20;
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
            this.columnHeader21.Width = 115;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Key ID";
            this.columnHeader22.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader22.Width = 200;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "동:";
            // 
            // btn_keyId_list_del
            // 
            this.btn_keyId_list_del.BackColor = System.Drawing.Color.Transparent;
            this.btn_keyId_list_del.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_keyId_list_del.BackgroundImage")));
            this.btn_keyId_list_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_keyId_list_del.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keyId_list_del.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_keyId_list_del.ForeColor = System.Drawing.Color.White;
            this.btn_keyId_list_del.Location = new System.Drawing.Point(333, 195);
            this.btn_keyId_list_del.Margin = new System.Windows.Forms.Padding(0);
            this.btn_keyId_list_del.Name = "btn_keyId_list_del";
            this.btn_keyId_list_del.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_keyId_list_del.Size = new System.Drawing.Size(100, 30);
            this.btn_keyId_list_del.TabIndex = 6;
            this.btn_keyId_list_del.Text = "키 ID 삭제";
            this.btn_keyId_list_del.UseVisualStyleBackColor = false;
            this.btn_keyId_list_del.Click += new System.EventHandler(this.btn_keyId_list_del_Click);
            this.btn_keyId_list_del.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_keyId_list_del_MouseDown);
            this.btn_keyId_list_del.MouseLeave += new System.EventHandler(this.btn_keyId_list_del_MouseLeave);
            this.btn_keyId_list_del.MouseHover += new System.EventHandler(this.btn_keyId_list_del_MouseHover);
            this.btn_keyId_list_del.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_keyId_list_del_MouseUp);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 12);
            this.label7.TabIndex = 1;
            this.label7.Text = "호:";
            // 
            // txt_reg_keyid
            // 
            this.txt_reg_keyid.Location = new System.Drawing.Point(64, 161);
            this.txt_reg_keyid.Name = "txt_reg_keyid";
            this.txt_reg_keyid.Size = new System.Drawing.Size(109, 21);
            this.txt_reg_keyid.TabIndex = 3;
            // 
            // btn_keyId_list_reg
            // 
            this.btn_keyId_list_reg.BackColor = System.Drawing.Color.Transparent;
            this.btn_keyId_list_reg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_keyId_list_reg.BackgroundImage")));
            this.btn_keyId_list_reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_keyId_list_reg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keyId_list_reg.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_keyId_list_reg.ForeColor = System.Drawing.Color.White;
            this.btn_keyId_list_reg.Location = new System.Drawing.Point(225, 195);
            this.btn_keyId_list_reg.Margin = new System.Windows.Forms.Padding(0);
            this.btn_keyId_list_reg.Name = "btn_keyId_list_reg";
            this.btn_keyId_list_reg.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_keyId_list_reg.Size = new System.Drawing.Size(100, 30);
            this.btn_keyId_list_reg.TabIndex = 5;
            this.btn_keyId_list_reg.Text = "키 ID 추가";
            this.btn_keyId_list_reg.UseVisualStyleBackColor = false;
            this.btn_keyId_list_reg.Click += new System.EventHandler(this.btn_keyId_list_reg_Click);
            this.btn_keyId_list_reg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_keyId_list_reg_MouseDown);
            this.btn_keyId_list_reg.MouseLeave += new System.EventHandler(this.btn_keyId_list_reg_MouseLeave);
            this.btn_keyId_list_reg.MouseHover += new System.EventHandler(this.btn_keyId_list_reg_MouseHover);
            this.btn_keyId_list_reg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_keyId_list_reg_MouseUp);
            // 
            // txt_reg_dong
            // 
            this.txt_reg_dong.Location = new System.Drawing.Point(35, 71);
            this.txt_reg_dong.Name = "txt_reg_dong";
            this.txt_reg_dong.Size = new System.Drawing.Size(54, 21);
            this.txt_reg_dong.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 168);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(46, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "Key ID:";
            // 
            // btn_keyid_load
            // 
            this.btn_keyid_load.BackColor = System.Drawing.Color.Transparent;
            this.btn_keyid_load.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_keyid_load.BackgroundImage")));
            this.btn_keyid_load.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_keyid_load.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_keyid_load.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_keyid_load.ForeColor = System.Drawing.Color.White;
            this.btn_keyid_load.Location = new System.Drawing.Point(9, 195);
            this.btn_keyid_load.Margin = new System.Windows.Forms.Padding(0);
            this.btn_keyid_load.Name = "btn_keyid_load";
            this.btn_keyid_load.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btn_keyid_load.Size = new System.Drawing.Size(100, 30);
            this.btn_keyid_load.TabIndex = 4;
            this.btn_keyid_load.Text = "Tag 읽기";
            this.btn_keyid_load.UseVisualStyleBackColor = false;
            this.btn_keyid_load.Click += new System.EventHandler(this.btn_keyid_load_Click);
            this.btn_keyid_load.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_keyid_load_MouseDown);
            this.btn_keyid_load.MouseLeave += new System.EventHandler(this.btn_keyid_load_MouseLeave);
            this.btn_keyid_load.MouseHover += new System.EventHandler(this.btn_keyid_load_MouseHover);
            this.btn_keyid_load.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_keyid_load_MouseUp);
            // 
            // txt_reg_ho
            // 
            this.txt_reg_ho.Location = new System.Drawing.Point(35, 101);
            this.txt_reg_ho.Name = "txt_reg_ho";
            this.txt_reg_ho.Size = new System.Drawing.Size(54, 21);
            this.txt_reg_ho.TabIndex = 1;
            // 
            // txt_reg_keysn
            // 
            this.txt_reg_keysn.Location = new System.Drawing.Point(64, 131);
            this.txt_reg_keysn.Name = "txt_reg_keysn";
            this.txt_reg_keysn.Size = new System.Drawing.Size(75, 21);
            this.txt_reg_keysn.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 12);
            this.label8.TabIndex = 2;
            this.label8.Text = "Key Sn:";
            // 
            // nfc_lbl_info
            // 
            this.nfc_lbl_info.AutoSize = true;
            this.nfc_lbl_info.Font = new System.Drawing.Font("굴림", 9.5F, System.Drawing.FontStyle.Bold);
            this.nfc_lbl_info.ForeColor = System.Drawing.Color.Red;
            this.nfc_lbl_info.Location = new System.Drawing.Point(108, 71);
            this.nfc_lbl_info.Name = "nfc_lbl_info";
            this.nfc_lbl_info.Size = new System.Drawing.Size(443, 52);
            this.nfc_lbl_info.TabIndex = 21;
            this.nfc_lbl_info.Text = "* BLE 읽기를 한 후 꼭 \"원패스키 등록기\"에서 \r\n스마트폰 NFC를 읽으세요. \r\n* 스마트폰의 NFC를 카드모드 또는 기본모드로 활성화 해" +
    "야 합니다. \r\n* 안드로이드폰만 NFC를 사용할 수 있습니다.";
            this.nfc_lbl_info.Visible = false;
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer7.Panel1.Controls.Add(this.uc_lv_lb_master);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.splitContainer8);
            this.splitContainer7.Size = new System.Drawing.Size(516, 410);
            this.splitContainer7.SplitterDistance = 128;
            this.splitContainer7.TabIndex = 0;
            // 
            // splitContainer8
            // 
            this.splitContainer8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer8.Location = new System.Drawing.Point(0, 0);
            this.splitContainer8.Name = "splitContainer8";
            this.splitContainer8.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            this.splitContainer8.Panel1.Controls.Add(this.uc_lv_lb_reg);
            // 
            // splitContainer8.Panel2
            // 
            this.splitContainer8.Panel2.BackColor = System.Drawing.Color.MintCream;
            this.splitContainer8.Panel2.Controls.Add(this.uc_lv_lb_del);
            this.splitContainer8.Size = new System.Drawing.Size(516, 278);
            this.splitContainer8.SplitterDistance = 132;
            this.splitContainer8.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.AllowDrop = true;
            this.splitContainer2.Panel1.AutoScroll = true;
            this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer2.Size = new System.Drawing.Size(1020, 707);
            this.splitContainer2.SplitterDistance = 258;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.AutoScroll = true;
            this.splitContainer3.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer3.Panel1.BackgroundImage = global::SmartOnePass.Properties.Resources.OUC_List_Bg;
            this.splitContainer3.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer3.Panel1.Controls.Add(this.pictureBox1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.uc_lv_lb_UkrInfo);
            this.splitContainer3.Size = new System.Drawing.Size(258, 707);
            this.splitContainer3.SplitterDistance = 532;
            this.splitContainer3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::SmartOnePass.Properties.Resources.Dong_Title_Bg;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.InitialImage = global::SmartOnePass.Properties.Resources.Dong_Title_Bg;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(258, 50);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // splitContainer4
            // 
            this.splitContainer4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer4.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer4.Panel1.Controls.Add(this.menuStrip1);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.uc_lv_evt_log);
            this.splitContainer4.Size = new System.Drawing.Size(758, 707);
            this.splitContainer4.SplitterDistance = 533;
            this.splitContainer4.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem5,
            this.toolStripMenuItem7,
            this.toolStripMenuItem13,
            this.toolStripMenuItem15,
            this.toolStripMenuItem17,
            this.toolStripMenuItem22});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(758, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "설정";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem1.Text = "설정";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.ToolStripMenuItem_ReLoad_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem2.Text = "Config 파일 Load";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem3.Text = "엘리베이터 서버 접속";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.ToolStripMenuItem_CrtReConnection_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(190, 22);
            this.toolStripMenuItem4.Text = "Config 정보";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.config정보ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem6});
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(55, 20);
            this.toolStripMenuItem5.Text = "동기화";
            this.toolStripMenuItem5.Visible = false;
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItem6.Text = "동기화 설정";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.menu_sync_setting_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10,
            this.동기화ToolStripMenuItem});
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem7.Text = "점검";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem8.Text = "엘레베이터 호출";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.menu_Test_Crt_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem9.Text = "리셋";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.ToolReset_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem12});
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItem10.Text = "네트워크테스트";
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem11.Text = "PING 테스트";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.pING테스트ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem12.Text = "결과 초기화";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.결과초기화ToolStripMenuItem_Click);
            // 
            // 동기화ToolStripMenuItem
            // 
            this.동기화ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.전체KeyData삽입ToolStripMenuItem,
            this.부분KeyData삽입ToolStripMenuItem});
            this.동기화ToolStripMenuItem.Name = "동기화ToolStripMenuItem";
            this.동기화ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.동기화ToolStripMenuItem.Text = "동기화";
            // 
            // 전체KeyData삽입ToolStripMenuItem
            // 
            this.전체KeyData삽입ToolStripMenuItem.Name = "전체KeyData삽입ToolStripMenuItem";
            this.전체KeyData삽입ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.전체KeyData삽입ToolStripMenuItem.Text = "전체 KeyData 삽입";
            this.전체KeyData삽입ToolStripMenuItem.Click += new System.EventHandler(this.전체KeyData삽입ToolStripMenuItem_Click);
            // 
            // 부분KeyData삽입ToolStripMenuItem
            // 
            this.부분KeyData삽입ToolStripMenuItem.Name = "부분KeyData삽입ToolStripMenuItem";
            this.부분KeyData삽입ToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.부분KeyData삽입ToolStripMenuItem.Text = "부분 KeyData 삽입";
            this.부분KeyData삽입ToolStripMenuItem.Click += new System.EventHandler(this.부분KeyData삽입ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem14});
            this.toolStripMenuItem13.Enabled = false;
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(67, 20);
            this.toolStripMenuItem13.Text = "출입통제";
            this.toolStripMenuItem13.Visible = false;
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(142, 22);
            this.toolStripMenuItem14.Text = "일괄 키 등록";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem16});
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(90, 20);
            this.toolStripMenuItem15.Text = "키 삭제, 조회";
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(141, 22);
            this.toolStripMenuItem16.Text = "키 삭제,조회";
            this.toolStripMenuItem16.Click += new System.EventHandler(this.KeyDeleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem18,
            this.toolStripMenuItem19,
            this.toolStripMenuItem20,
            this.toolStripMenuItem21});
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(73, 20);
            this.toolStripMenuItem17.Text = "로그 View";
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem18.Text = "등록/삭제 Log";
            this.toolStripMenuItem18.Click += new System.EventHandler(this.menu_regdel_log_Click);
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem19.Text = "Dev Log";
            this.toolStripMenuItem19.Click += new System.EventHandler(this.DevLogToolStripMenuItem_Click);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem20.Text = "Crt 호출 Log";
            this.toolStripMenuItem20.Click += new System.EventHandler(this.CrtCallLogtoolStripMenuItem1_Click);
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(166, 22);
            this.toolStripMenuItem21.Text = "원패스 설정 타입";
            this.toolStripMenuItem21.Click += new System.EventHandler(this.OnePassSettingTypeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem23});
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Size = new System.Drawing.Size(43, 20);
            this.toolStripMenuItem22.Text = "제어";
            this.toolStripMenuItem22.Visible = false;
            // 
            // toolStripMenuItem23
            // 
            this.toolStripMenuItem23.Name = "toolStripMenuItem23";
            this.toolStripMenuItem23.Size = new System.Drawing.Size(157, 22);
            this.toolStripMenuItem23.Text = "M/W 감도 조절";
            this.toolStripMenuItem23.Click += new System.EventHandler(this.mwSensitivitytoolStripMenuItem_Click);
            // 
            // 설정ToolStripMenuItem
            // 
            this.설정ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItem_ReLoad,
            this.ToolStripMenuItem_CrtReConnection,
            this.config정보ToolStripMenuItem,
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem});
            this.설정ToolStripMenuItem.Name = "설정ToolStripMenuItem";
            this.설정ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.설정ToolStripMenuItem.Text = "설정";
            // 
            // ToolStripMenuItem_ReLoad
            // 
            this.ToolStripMenuItem_ReLoad.Name = "ToolStripMenuItem_ReLoad";
            this.ToolStripMenuItem_ReLoad.Size = new System.Drawing.Size(245, 22);
            this.ToolStripMenuItem_ReLoad.Text = "Config 파일 Load";
            this.ToolStripMenuItem_ReLoad.Click += new System.EventHandler(this.ToolStripMenuItem_ReLoad_Click);
            // 
            // ToolStripMenuItem_CrtReConnection
            // 
            this.ToolStripMenuItem_CrtReConnection.Name = "ToolStripMenuItem_CrtReConnection";
            this.ToolStripMenuItem_CrtReConnection.Size = new System.Drawing.Size(245, 22);
            this.ToolStripMenuItem_CrtReConnection.Text = "엘리베이터 서버 접속";
            this.ToolStripMenuItem_CrtReConnection.Click += new System.EventHandler(this.ToolStripMenuItem_CrtReConnection_Click);
            // 
            // config정보ToolStripMenuItem
            // 
            this.config정보ToolStripMenuItem.Name = "config정보ToolStripMenuItem";
            this.config정보ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.config정보ToolStripMenuItem.Text = "Config 정보";
            this.config정보ToolStripMenuItem.Click += new System.EventHandler(this.config정보ToolStripMenuItem_Click);
            // 
            // 혜인에스앤에스Key정보동기화ToolStripMenuItem
            // 
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem.Name = "혜인에스앤에스Key정보동기화ToolStripMenuItem";
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem.Size = new System.Drawing.Size(245, 22);
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem.Text = "혜인에스앤에스 Key정보 동기화";
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem.Visible = false;
            this.혜인에스앤에스Key정보동기화ToolStripMenuItem.Click += new System.EventHandler(this.혜인에스앤에스Key정보동기화ToolStripMenuItem_Click);
            // 
            // menu_SyncSetting
            // 
            this.menu_SyncSetting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_sync_setting});
            this.menu_SyncSetting.Name = "menu_SyncSetting";
            this.menu_SyncSetting.Size = new System.Drawing.Size(55, 20);
            this.menu_SyncSetting.Text = "동기화";
            this.menu_SyncSetting.Visible = false;
            // 
            // menu_sync_setting
            // 
            this.menu_sync_setting.Name = "menu_sync_setting";
            this.menu_sync_setting.Size = new System.Drawing.Size(138, 22);
            this.menu_sync_setting.Text = "동기화 설정";
            this.menu_sync_setting.Click += new System.EventHandler(this.menu_sync_setting_Click);
            // 
            // 점검ToolStripMenuItem
            // 
            this.점검ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Test_Crt,
            this.ToolReset,
            this.네트워크테스트ToolStripMenuItem});
            this.점검ToolStripMenuItem.Name = "점검ToolStripMenuItem";
            this.점검ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.점검ToolStripMenuItem.Text = "점검";
            // 
            // menu_Test_Crt
            // 
            this.menu_Test_Crt.Name = "menu_Test_Crt";
            this.menu_Test_Crt.Size = new System.Drawing.Size(162, 22);
            this.menu_Test_Crt.Text = "엘레베이터 호출";
            this.menu_Test_Crt.Click += new System.EventHandler(this.menu_Test_Crt_Click);
            // 
            // ToolReset
            // 
            this.ToolReset.Name = "ToolReset";
            this.ToolReset.Size = new System.Drawing.Size(162, 22);
            this.ToolReset.Text = "리셋";
            this.ToolReset.Click += new System.EventHandler(this.ToolReset_Click);
            // 
            // 네트워크테스트ToolStripMenuItem
            // 
            this.네트워크테스트ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pING테스트ToolStripMenuItem,
            this.결과초기화ToolStripMenuItem});
            this.네트워크테스트ToolStripMenuItem.Name = "네트워크테스트ToolStripMenuItem";
            this.네트워크테스트ToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.네트워크테스트ToolStripMenuItem.Text = "네트워크테스트";
            // 
            // pING테스트ToolStripMenuItem
            // 
            this.pING테스트ToolStripMenuItem.Name = "pING테스트ToolStripMenuItem";
            this.pING테스트ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.pING테스트ToolStripMenuItem.Text = "PING 테스트";
            this.pING테스트ToolStripMenuItem.Click += new System.EventHandler(this.pING테스트ToolStripMenuItem_Click);
            // 
            // 결과초기화ToolStripMenuItem
            // 
            this.결과초기화ToolStripMenuItem.Name = "결과초기화ToolStripMenuItem";
            this.결과초기화ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.결과초기화ToolStripMenuItem.Text = "결과 초기화";
            this.결과초기화ToolStripMenuItem.Click += new System.EventHandler(this.결과초기화ToolStripMenuItem_Click);
            // 
            // 출입통제ToolStripMenuItem
            // 
            this.출입통제ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_reg_key_all});
            this.출입통제ToolStripMenuItem.Enabled = false;
            this.출입통제ToolStripMenuItem.Name = "출입통제ToolStripMenuItem";
            this.출입통제ToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.출입통제ToolStripMenuItem.Text = "출입통제";
            this.출입통제ToolStripMenuItem.Visible = false;
            // 
            // menu_reg_key_all
            // 
            this.menu_reg_key_all.Name = "menu_reg_key_all";
            this.menu_reg_key_all.Size = new System.Drawing.Size(142, 22);
            this.menu_reg_key_all.Text = "일괄 키 등록";
            this.menu_reg_key_all.Click += new System.EventHandler(this.menu_reg_key_all_Click);
            // 
            // 키삭제ToolStripMenuItem
            // 
            this.키삭제ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KeyDeleteToolStripMenuItem});
            this.키삭제ToolStripMenuItem.Name = "키삭제ToolStripMenuItem";
            this.키삭제ToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.키삭제ToolStripMenuItem.Text = "키 삭제";
            // 
            // KeyDeleteToolStripMenuItem
            // 
            this.KeyDeleteToolStripMenuItem.Name = "KeyDeleteToolStripMenuItem";
            this.KeyDeleteToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.KeyDeleteToolStripMenuItem.Text = "키 삭제";
            this.KeyDeleteToolStripMenuItem.Click += new System.EventHandler(this.KeyDeleteToolStripMenuItem_Click);
            // 
            // 등록삭제확인ToolStripMenuItem
            // 
            this.등록삭제확인ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_regdel_log,
            this.DevLogToolStripMenuItem,
            this.CrtCallLogtoolStripMenuItem1,
            this.OnePassSettingTypeToolStripMenuItem});
            this.등록삭제확인ToolStripMenuItem.Name = "등록삭제확인ToolStripMenuItem";
            this.등록삭제확인ToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.등록삭제확인ToolStripMenuItem.Text = "로그 View";
            // 
            // menu_regdel_log
            // 
            this.menu_regdel_log.Name = "menu_regdel_log";
            this.menu_regdel_log.Size = new System.Drawing.Size(166, 22);
            this.menu_regdel_log.Text = "등록/삭제 Log";
            this.menu_regdel_log.Click += new System.EventHandler(this.menu_regdel_log_Click);
            // 
            // DevLogToolStripMenuItem
            // 
            this.DevLogToolStripMenuItem.Name = "DevLogToolStripMenuItem";
            this.DevLogToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.DevLogToolStripMenuItem.Text = "Dev Log";
            this.DevLogToolStripMenuItem.Click += new System.EventHandler(this.DevLogToolStripMenuItem_Click);
            // 
            // CrtCallLogtoolStripMenuItem1
            // 
            this.CrtCallLogtoolStripMenuItem1.Name = "CrtCallLogtoolStripMenuItem1";
            this.CrtCallLogtoolStripMenuItem1.Size = new System.Drawing.Size(166, 22);
            this.CrtCallLogtoolStripMenuItem1.Text = "Crt 호출 Log";
            this.CrtCallLogtoolStripMenuItem1.Click += new System.EventHandler(this.CrtCallLogtoolStripMenuItem1_Click);
            // 
            // OnePassSettingTypeToolStripMenuItem
            // 
            this.OnePassSettingTypeToolStripMenuItem.Name = "OnePassSettingTypeToolStripMenuItem";
            this.OnePassSettingTypeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.OnePassSettingTypeToolStripMenuItem.Text = "원패스 설정 타입";
            this.OnePassSettingTypeToolStripMenuItem.Click += new System.EventHandler(this.OnePassSettingTypeToolStripMenuItem_Click);
            // 
            // 제어ToolStripMenuItem
            // 
            this.제어ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mwSensitivitytoolStripMenuItem});
            this.제어ToolStripMenuItem.Name = "제어ToolStripMenuItem";
            this.제어ToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.제어ToolStripMenuItem.Text = "제어";
            // 
            // mwSensitivitytoolStripMenuItem
            // 
            this.mwSensitivitytoolStripMenuItem.Name = "mwSensitivitytoolStripMenuItem";
            this.mwSensitivitytoolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.mwSensitivitytoolStripMenuItem.Text = "M/W 감도 조절";
            this.mwSensitivitytoolStripMenuItem.Click += new System.EventHandler(this.mwSensitivitytoolStripMenuItem_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer6);
            this.splitContainer1.Size = new System.Drawing.Size(1540, 707);
            this.splitContainer1.SplitterDistance = 1020;
            this.splitContainer1.TabIndex = 0;
            // 
            // uc_lv_lb_UkrInfo
            // 
            this.uc_lv_lb_UkrInfo.BackColor = System.Drawing.Color.White;
            this.uc_lv_lb_UkrInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uc_lv_lb_UkrInfo.BackgroundImage")));
            this.uc_lv_lb_UkrInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uc_lv_lb_UkrInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_lv_lb_UkrInfo.Location = new System.Drawing.Point(0, 0);
            this.uc_lv_lb_UkrInfo.Margin = new System.Windows.Forms.Padding(4);
            this.uc_lv_lb_UkrInfo.Name = "uc_lv_lb_UkrInfo";
            this.uc_lv_lb_UkrInfo.Size = new System.Drawing.Size(258, 171);
            this.uc_lv_lb_UkrInfo.TabIndex = 0;
            // 
            // uc_lv_evt_log
            // 
            this.uc_lv_evt_log.BackColor = System.Drawing.Color.White;
            this.uc_lv_evt_log.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uc_lv_evt_log.BackgroundImage")));
            this.uc_lv_evt_log.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uc_lv_evt_log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_lv_evt_log.Location = new System.Drawing.Point(0, 0);
            this.uc_lv_evt_log.Margin = new System.Windows.Forms.Padding(4);
            this.uc_lv_evt_log.Name = "uc_lv_evt_log";
            this.uc_lv_evt_log.Size = new System.Drawing.Size(758, 170);
            this.uc_lv_evt_log.TabIndex = 1;
            // 
            // uc_lv_lb_master
            // 
            this.uc_lv_lb_master.BackColor = System.Drawing.Color.White;
            this.uc_lv_lb_master.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uc_lv_lb_master.BackgroundImage")));
            this.uc_lv_lb_master.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uc_lv_lb_master.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_lv_lb_master.Location = new System.Drawing.Point(0, 0);
            this.uc_lv_lb_master.Margin = new System.Windows.Forms.Padding(4);
            this.uc_lv_lb_master.Name = "uc_lv_lb_master";
            this.uc_lv_lb_master.Size = new System.Drawing.Size(516, 128);
            this.uc_lv_lb_master.TabIndex = 0;
            // 
            // uc_lv_lb_reg
            // 
            this.uc_lv_lb_reg.BackColor = System.Drawing.Color.White;
            this.uc_lv_lb_reg.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uc_lv_lb_reg.BackgroundImage")));
            this.uc_lv_lb_reg.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uc_lv_lb_reg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_lv_lb_reg.Location = new System.Drawing.Point(0, 0);
            this.uc_lv_lb_reg.Margin = new System.Windows.Forms.Padding(4);
            this.uc_lv_lb_reg.Name = "uc_lv_lb_reg";
            this.uc_lv_lb_reg.Size = new System.Drawing.Size(516, 132);
            this.uc_lv_lb_reg.TabIndex = 1;
            // 
            // uc_lv_lb_del
            // 
            this.uc_lv_lb_del.BackColor = System.Drawing.Color.White;
            this.uc_lv_lb_del.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("uc_lv_lb_del.BackgroundImage")));
            this.uc_lv_lb_del.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.uc_lv_lb_del.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_lv_lb_del.Location = new System.Drawing.Point(0, 0);
            this.uc_lv_lb_del.Margin = new System.Windows.Forms.Padding(4);
            this.uc_lv_lb_del.Name = "uc_lv_lb_del";
            this.uc_lv_lb_del.Size = new System.Drawing.Size(516, 142);
            this.uc_lv_lb_del.TabIndex = 0;
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1540, 707);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "스마트원패스 시스템 V1.863";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.splitContainer6.Panel1.ResumeLayout(false);
            this.splitContainer6.Panel1.PerformLayout();
            this.splitContainer6.Panel2.ResumeLayout(false);
            this.splitContainer6.ResumeLayout(false);
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            this.splitContainer7.ResumeLayout(false);
            this.splitContainer8.Panel1.ResumeLayout(false);
            this.splitContainer8.Panel2.ResumeLayout(false);
            this.splitContainer8.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer6;
        private System.Windows.Forms.ListView lv_key_info;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem 설정ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_ReLoad;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItem_CrtReConnection;
        private System.Windows.Forms.ToolStripMenuItem config정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_SyncSetting;
        private System.Windows.Forms.ToolStripMenuItem menu_sync_setting;
        private System.Windows.Forms.ToolStripMenuItem 점검ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_Test_Crt;
        private System.Windows.Forms.ToolStripMenuItem ToolReset;
        private System.Windows.Forms.ToolStripMenuItem 네트워크테스트ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pING테스트ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 결과초기화ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 출입통제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_reg_key_all;
        private System.Windows.Forms.ToolStripMenuItem 키삭제ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem KeyDeleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 등록삭제확인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_regdel_log;
        private System.Windows.Forms.ToolStripMenuItem DevLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OnePassSettingTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 제어ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader1;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader2;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader3;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.SplitContainer splitContainer8;
        private UC_ListView uc_lv_evt_log;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader4;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader5;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader6;
        private System.Windows.Forms.Button btn_bleid_load;
        private System.Windows.Forms.Button btn_key_reg;
        private System.Windows.Forms.Button btn_keyId_list_del;
        private System.Windows.Forms.TextBox txt_reg_keyid;
        private System.Windows.Forms.TextBox txt_reg_dong;
        private System.Windows.Forms.Button btn_keyid_load;
        private System.Windows.Forms.TextBox txt_reg_keysn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_reg_ho;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_keyId_list_reg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader7;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader8;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader9;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader10;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader11;
        private UC_ListView uc_lv_lb_reg;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader12;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader13;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader14;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader15;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader16;
        private UC_ListView uc_lv_lb_del;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader17;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader18;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader19;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader20;
        private System.Windows.Forms.ColumnHeader uclvcolumnHeader21;
        private System.Windows.Forms.Label label2;
        private UC_ListView uc_lv_lb_UkrInfo;
        private UC_ListView uc_lv_lb_master;
        private System.Windows.Forms.ToolStripMenuItem mwSensitivitytoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CrtCallLogtoolStripMenuItem1;
        private System.Windows.Forms.TextBox txt_reg_nfcid;
        private System.Windows.Forms.Label nfc_lbl_id;
        private System.Windows.Forms.Label nfc_lbl_info;
        private System.Windows.Forms.ToolStripMenuItem 혜인에스앤에스Key정보동기화ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.CheckBox cb_charger_normal;
        private System.Windows.Forms.CheckBox cb_charger_portable;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem15;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem16;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem17;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem18;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem19;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem20;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem21;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem22;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem23;
        private System.Windows.Forms.TextBox txtCarNum;
        private System.Windows.Forms.Label lblCarNum;
        private System.Windows.Forms.ToolStripMenuItem 동기화ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 전체KeyData삽입ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 부분KeyData삽입ToolStripMenuItem;
    }
}

