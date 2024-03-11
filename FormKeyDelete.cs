using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SmartOnePass
{
    public partial class FormKeyDelete : Form
    {
        ///////////////////////////////////////////////////////////////
        // Mysql 연동
        private MySqlDB m_mysql = new MySqlDB();

        private string m_strDBIp = "";
        private string m_strDBName = "";
        private string m_strDBId = "";
        private string m_strDBPw = "";

        // 멤버 변수 속성 처리
        public String DBIp
        {
            get { return m_strDBIp; }
            set { m_strDBIp = value; }
        }

        public String DBName
        {
            get { return m_strDBName; }
            set { m_strDBName = value; }
        }

        public String DBId
        {
            get { return m_strDBId; }
            set { m_strDBId = value; }
        }

        public String DBPw
        {
            get { return m_strDBPw; }
            set { m_strDBPw = value; }
        }

        public FormKeyDelete()
        {
            InitializeComponent();
        }

        private void FormKeyDelete_Load(object sender, EventArgs e)
        {
            // 파일에서 DB 정보 읽기
            LoadConfig();

            this.DBName = "kms";
            this.DBId = "upisadm";
            this.DBPw = "upis";

            m_mysql.MySqlCon(this.DBIp, this.DBName, this.DBId, this.DBPw);

            lbl_Explanation.Text = "1. 삭제 할 키가 있으면, 동, 호를 입력 후 조회 버튼을 클릭하세요." + Environment.NewLine + "2. 삭제할 키를 List에서 체크한 후 키 ID 삭제 버튼을 클릭하세요.";

            this.BackColor = Color.FromArgb(0, 92, 170);

            lv_key_info.OwnerDraw = true;

            // 행 높이 조절
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);
            lv_key_info.SmallImageList = imgList;

            //if (Program.g_nChargerUse == 0) // 전기차 충전이 아니면.. ==:> 차랑 번호를 적용하면, if문 살려야함.
            {
                lv_key_info.Columns.RemoveAt(6);
            }
        }


        private void LoadConfig()
        {
            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\Config.ini");

            foreach (string _line in lines)
            {
                string[] _str = _line.Split('=');

                string _case = _str[0].ToLower();

                switch (_case)
                {
                    case "dbip":
                        this.DBIp = _str[1];
                        break;
                    default:
                        break;
                }
            }
        }

        private void btn_keyid_load_Click(object sender, EventArgs e)
        {
            if (Program.g_nChargerUse == 0)
            {
                KeyIDDelete();  // 전기차 충전 카드를 사용하지 않음.
            }
            else
            {
                KeyIDDeleteandType(); // 전기차 충전 카드를 사용함.
            }
            
        }

        private void KeyIDDelete()
        {
            string _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "", _strRegDate;
            _strDong = txt_del_dong.Text;
            _strHo = txt_del_ho.Text;

            string _strQry = "";

            if (_strDong.Trim().Length == 0 && _strHo.Trim().Length == 0)
            {
                _strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate from Key_Info_Master;");
            }
            else if (_strDong.Trim().Length == 0)
            {
                _strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate from Key_Info_Master where Ho = {0};", _strHo);
            }
            else if (_strHo.Trim().Length == 0)
            {
                _strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate from Key_Info_Master where Dong = {0};", _strDong);
            }
            else
            {
                _strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate from Key_Info_Master where Dong = {0} and Ho = {1};", _strDong, _strHo);
            }

            if (_strQry.Length == 0)
                return;

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 5);

            lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Clear());
            foreach (string[] _str in _qryList)
            {
                _strDong = _str[0];
                _strHo = _str[1];
                _strKeySn = _str[2];
                _strKeyId = _str[3];
                _strRegDate = _str[4];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_strDong);
                _lvi.SubItems.Add(_strHo);
                _lvi.SubItems.Add(_strKeySn);
                _lvi.SubItems.Add(_strKeyId);
                //_lvi.SubItems.Add(_strRegDate);

                lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Add(_lvi));
            }
        }

        // 전기차 충전 카드를 사용함.
        private void KeyIDDeleteandType()
        {
            string strDong = "", strHo = "", strKeySn = "", strKeyId = "", strRegDate = "", strType = "", strCarNum = "";
            strDong = txt_del_dong.Text;
            strHo = txt_del_ho.Text;

            string strQry = "";
            // 과금형 카드 정보 조회
            if (strDong.Trim().Length == 0 && strHo.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, Key_Type, CarNum from Key_Info_Master;");
            }
            else if (strDong.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, Key_Type, CarNum from Key_Info_Master where Ho = {0};", strHo);
            }
            else if (strHo.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, Key_Type, CarNum from Key_Info_Master where Dong = {0};", strDong);
            }
            else
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, Key_Type, CarNum from Key_Info_Master where Dong = {0} and Ho = {1};", strDong, strHo);
            }

            if (strQry.Length == 0)
                return;

            List<string[]> _qryList = m_mysql.MySqlSelect(strQry, 7);

            lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Clear());
            foreach (string[] str in _qryList)
            {
                strDong = str[0];
                strHo = str[1];
                strKeySn = str[2];
                strKeyId = str[3];
                strRegDate = str[4];
                if (str[5] == "9")
                {
                    strType = Program.CHARGER_NORMAL_CARD;
                } else
                {
                    strType = "";
                }
                strCarNum = str[6];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(strDong);
                _lvi.SubItems.Add(strHo);
                _lvi.SubItems.Add(strKeySn);
                _lvi.SubItems.Add(strKeyId);
                //_lvi.SubItems.Add(strRegDate);
                _lvi.SubItems.Add(strType);
                _lvi.SubItems.Add(strCarNum);

                lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Add(_lvi));
            }


            // 이동형 카드 정보 조회
            strDong = txt_del_dong.Text;
            strHo = txt_del_ho.Text;
            if (strDong.Trim().Length == 0 && strHo.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, CarNum from kms.Key_Portable_Charger;");
            }
            else if (strDong.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, CarNum from kms.Key_Portable_Charger where Ho = {0};", strHo);
            }
            else if (strHo.Trim().Length == 0)
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, CarNum from kms.Key_Portable_Charger where Dong = {0};", strDong);
            }
            else
            {
                strQry = string.Format("select Dong, Ho, Key_Sn, Key_ID, RegDate, CarNum from kms.Key_Portable_Charger where Dong = {0} and Ho = {1};", strDong, strHo);
            }

            if (strQry.Length == 0)
                return;

            _qryList.Clear();
            _qryList = m_mysql.MySqlSelect(strQry, 6);
            strType = "";

            foreach (string[] str in _qryList)
            {
                strDong = str[0];
                strHo = str[1];
                strKeySn = str[2];
                strKeyId = str[3];
                strRegDate = str[4];
                strType = Program.CHARGER_PORTABLE_CARD;
                strCarNum = str[5];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(strDong);
                _lvi.SubItems.Add(strHo);
                _lvi.SubItems.Add(strKeySn);
                _lvi.SubItems.Add(strKeyId);
                //_lvi.SubItems.Add(strRegDate);
                _lvi.SubItems.Add(strType);
                _lvi.SubItems.Add(strCarNum);

                lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Add(_lvi));
            }
        }


        private void btn_keyId__del_Click(object sender, EventArgs e)
        {
            int nLoop = 0;

            lv_key_info.InvokeIfNeeded(() => nLoop = lv_key_info.Items.Count);

            List<stKeyInfo> _listKeyInfo = new List<stKeyInfo>();
            for (int i = 0; i < nLoop; i++)
            {
                if (lv_key_info.Items[i].Checked == true)
                {
                    // 2023-10-10 수정: 전기차 충전 때문에 추가 함.
                    string strType = "", strKeyID = "", strKeySn = "", strDong = "", strHo = "", strQry = "";
                    stKeyInfo _stKeyInfo = new stKeyInfo();
                    lv_key_info.InvokeIfNeeded(() => strDong = _stKeyInfo.strDong = lv_key_info.Items[i].SubItems[1].Text);
                    lv_key_info.InvokeIfNeeded(() => strHo = _stKeyInfo.strHo = lv_key_info.Items[i].SubItems[2].Text);
                    lv_key_info.InvokeIfNeeded(() => strKeySn = _stKeyInfo.strKeySn = lv_key_info.Items[i].SubItems[3].Text);
                    lv_key_info.InvokeIfNeeded(() => strKeyID = _stKeyInfo.strKeyId = lv_key_info.Items[i].SubItems[4].Text);


                    if (Program.g_nChargerUse == 1)
                    {
   
                        // Type을 읽어서, 충전일반카드나, 이동형카드는 삭제만 한다.
                        lv_key_info.InvokeIfNeeded(() => strType = lv_key_info.Items[i].SubItems[5].Text);

                        if (strType == Program.CHARGER_NORMAL_CARD)
                        {

                            strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Id='{2}';", strDong, strHo, strKeyID);

                            m_mysql.MySqlExec(strQry);
                            Thread.Sleep(10);

                            strQry = string.Format("Insert Into Delete_Key_Info(Dong, Ho, Key_Sn, Key_Id, DelDate) values({0}, {1}, '{2}', '{3}','{4}');",
                                strDong, strHo, strKeySn, strKeyID, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            m_mysql.MySqlExec(strQry);
                            Thread.Sleep(10);

                            continue;
                        }
                        else if (strType == Program.CHARGER_PORTABLE_CARD)
                        {
                            //strQry = string.Format("Delete FRom kms.Key_Portable_Charger where Key_Id='{1}';", strKeyID);
                            strQry = string.Format("Delete FRom kms.Key_Portable_Charger where Dong={0} and Ho={1} and Key_Id='{2}';", strDong, strHo, strKeyID);
                            m_mysql.MySqlExec(strQry);
                            Thread.Sleep(10);

                            strQry = string.Format("Insert Into Delete_Key_Info(Dong, Ho, Key_Sn, Key_Id, DelDate) values({0}, {1}, '{2}', '{3}','{4}');",
                                strDong, strHo, strKeySn, strKeyID, System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            m_mysql.MySqlExec(strQry);
                            Thread.Sleep(10);
                            continue;
                        }
                    }

                    
                    // 일반형 충전카드나, 이동형 충전카드가 아니면, 기존 로직대로 진행함.
                    _listKeyInfo.Add(_stKeyInfo);

                }
            }

            if (_listKeyInfo.Count <= 0)
            {
                MessageBox.Show("삭제 할 키가 없습니다. 삭제 할 키를 체크 후 진행하세요");
                return;
            }
            MessageBox.Show("키 삭제를 시작 합니다.");

            // Del_에 데이터를 입력하고,
            // Key_Info_Master에 데이터를 삭제하고,
            // Delete_Key_Info에 데이터를 저장함.
            KeyInfoDel(_listKeyInfo);

            lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Clear());
        }

        ////////////////////////////////////////////////////////////////////
        // Del_에 데이터를 입력하고,
        // Key_Info_Master에 데이터를 삭제하고, 
        // Delete_Key_Info에 데이터를 저장함.            
        private void KeyInfoDel(List<stKeyInfo> a_listKeyInfo)
        {
            string _strQry = "", _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "", _strTime = "";
            try
            {
                foreach (stKeyInfo _st in a_listKeyInfo)
                {
                    _strDong = _st.strDong;
                    _strHo = _st.strHo;
                    _strKeySn = _st.strKeySn;
                    _strKeyId = _st.strKeyId;
                    _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // 1. Del_에 삭제할 데이터 입력
                    List<string> _listLBName = GetLBName(_strDong, _strHo);
                    foreach (string _strLBName in _listLBName)
                    {
                        if (_strKeyId.Length != 16)
                        {
                            continue;
                        }

                        _strQry = string.Format("Insert Into Del_{0}(Dong, Ho, Key_Sn, Key_Id, State) values({1}, {2}, '{3}', '{4}', 0);",
                            _strLBName, _strDong, _strHo, _strKeySn, _strKeyId);

                        m_mysql.MySqlExec(_strQry);

                        //_strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        //Console.WriteLine(_strTime);

                        Thread.Sleep(10);
                    }

                    // 2. Key_Info_Master에서 Key 데이터 삭제
                    if (_strKeySn.Length != 0 && _strKeyId.Length != 0)
                    {
                        _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Sn='{2}' and Key_Id='{3}';",
                                        _strDong, _strHo, _strKeySn, _strKeyId);
                    }
                    else if (_strKeySn.Length != 0 && _strKeyId.Length == 0)    // Key ID가 없을 때.
                    {
                        _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Id is null;",
                                        _strDong, _strHo);
                    }
                    else if (_strKeySn.Length == 0 && _strKeyId.Length == 0)    // Key ID가 없을 때.
                    {
                        _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Id is null;",
                                        _strDong, _strHo);
                    }
                    else if (_strKeySn.Length == 0 && _strKeyId.Length != 0)
                    {
                        _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Id='{2}';",
                                        _strDong, _strHo, _strKeyId);
                    }
                    else  // 에러 방지를 위해 조건이 안맞을 때에는 
                    {
                        _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Sn='{2}' and Key_Id='{3}';",
                                        _strDong, _strHo, _strKeySn, _strKeyId);
                    }



                    m_mysql.MySqlExec(_strQry);
                    Thread.Sleep(10);

                    // 3. Delete_Key_Info에 삭제 데이터 저장함.
                    _strQry = string.Format("Insert Into Delete_Key_Info(Dong, Ho, Key_Sn, Key_Id, DelDate) values({0}, {1}, '{2}', '{3}','{4}');",
                        _strDong, _strHo, _strKeySn, _strKeyId, _strTime);

                    m_mysql.MySqlExec(_strQry);
                    Thread.Sleep(10);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("KeyInfoDel() 예외 발생: " + ex.Message.ToString());
                Console.WriteLine("strQry: " + _strQry);
            }

        }

        private List<string> GetLBName(string a_strDong, string a_strHo)
        {
            string _strQry = string.Format("Select Lobby_Name From Dong_Lobby where Dong = {0} and Ho = {1};", a_strDong, a_strHo);

            List<string> _listLBName = new List<string>();

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _listLBName.Add(string.Format("{0}", _str[0]));
            }

            return _listLBName;
        }

        private void btn_keyid_load_MouseDown(object sender, MouseEventArgs e)
        {
            btn_keyid_load.BackgroundImage = Properties.Resources.btn_search_click;
        }

        private void btn_keyid_load_MouseHover(object sender, EventArgs e)
        {
            btn_keyid_load.BackgroundImage = Properties.Resources.btn_search_over;
        }

        private void btn_keyid_load_MouseLeave(object sender, EventArgs e)
        {
            btn_keyid_load.BackgroundImage = Properties.Resources.btn_search_normal;
        }

        private void btn_keyid_load_MouseUp(object sender, MouseEventArgs e)
        {
            btn_keyid_load.BackgroundImage = Properties.Resources.btn_search_normal;
        }

        private void btn_keyId__del_MouseDown(object sender, MouseEventArgs e)
        {
            btn_keyId__del.BackgroundImage = Properties.Resources.btn_key_del_click;
        }

        private void btn_keyId__del_MouseHover(object sender, EventArgs e)
        {
            btn_keyId__del.BackgroundImage = Properties.Resources.btn_key_del_over;
        }

        private void btn_keyId__del_MouseLeave(object sender, EventArgs e)
        {
            btn_keyId__del.BackgroundImage = Properties.Resources.btn_key_del_normal;
        }

        private void btn_keyId__del_MouseUp(object sender, MouseEventArgs e)
        {
            btn_keyId__del.BackgroundImage = Properties.Resources.btn_key_del_normal;
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.DrawBackground();
                // Draw the header text.
                using (Font headerFont =
                            new Font("돋움", 11))
                {
                    Color bgColor = Color.FromArgb(239, 249, 254);
                    e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);

                    e.Graphics.DrawLine(new Pen(Color.FromArgb(210, 210, 210)), new Point(e.Bounds.X, 0), new Point(e.Bounds.X, e.Bounds.Height));  // Header 세로 라인

                }
            }
            return;
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
