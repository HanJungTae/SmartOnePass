using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SmartOnePass
{
    public partial class FormSensitivity : Form
    {
        private MySqlDB m_mysql;

        Color m_headerBgColor;

        public FormSensitivity()
        {
            InitializeComponent();
        }

        public void SetMySql(MySqlDB a_mysql)
        {
            m_mysql = a_mysql;
        }

        public void SetHeaderBackColor(Color a_rgb)
        {
            listView1.OwnerDraw = true;
            m_headerBgColor = a_rgb;
        }

        private void FormSensitivity_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            LoadComboDong();
            LoadComboLobbyName();

            cb_dong_name.Enabled = false;
            cb_lb_name.Enabled = false;

            LoadSensitivityLobbyName(1, "", "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);

            this.SetHeaderBackColor(Color.FromArgb(239, 249, 254));
            Program.CreateRoundRectRgn((Control)cb_dong_name);
            Program.CreateRoundRectRgn((Control)cb_lb_name);
            Program.CreateRoundRectRgn((Control)cb_distance_value);
            Program.CreateRoundRectRgn((Control)cb_sensitivity_value);
            Program.CreateRoundRectRgn((Control)cb_speed_value);
            Program.CreateRoundRectRgn((Control)listView1);
        }

        private void LoadComboDong()
        {
            //////////////////////////////////////
            // 콤보 박스 동 이름 Load
            string _strDongName = "";

            string _strQry = string.Format("Select Dong from Dong_Lobby group by Dong;");

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strDongName = _str[0];
                if (_strDongName == "9999")
                    continue;

                cb_dong_name.InvokeIfNeeded(() => cb_dong_name.Items.Add(_strDongName));
            }
            cb_dong_name.InvokeIfNeeded(() => cb_dong_name.EndUpdate());
        }
        private void LoadComboLobbyName()
        {
            //////////////////////////////////////
            // 콤보 박스 로비 이름 Load
            string _strLBName = "";

            string _strQry = string.Format("SELECT Lobby_Name FROM Dong_Lobby group by Lobby_Name;");

            List<string[]> _qryList1 = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList1)
            {
                _strLBName = _str[0];
                cb_lb_name.InvokeIfNeeded(() => cb_lb_name.Items.Add(_strLBName));
            }
            cb_lb_name.InvokeIfNeeded(() => cb_lb_name.EndUpdate());
        }

        private void cb_dong_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox) sender;
            string _strDong = combo.Text;

            LoadSensitivityLobbyName(2, _strDong, "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
        }

        private void rb_total_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                cb_dong_name.Enabled = false;
                cb_lb_name.Enabled = false;
                LoadSensitivityLobbyName(1, "", "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }

            
        }

        private void rb_dong_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                cb_dong_name.Enabled = true;
                cb_lb_name.Enabled = false;
                LoadSensitivityLobbyName(2, cb_dong_name.Text, "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }
        }

        private void rb_each_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;

            if (rb.Checked)
            {
                cb_dong_name.Enabled = false;
                cb_lb_name.Enabled = true;
                LoadSensitivityLobbyName(3, "", cb_lb_name.Text, cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }
        }

        private void LoadSensitivityLobbyName(int a_nType, string a_strDong, string a_strLobbyName, string a_strDistance, string a_strSensitivity, string a_strSpeed)
        {
            string _strQry = "";
            if (a_nType == 1)   // 전체
            {
                _strQry = "SELECT LobbyName FROM kms.UKR_Sensitivity;";
            }
            else if (a_nType == 2)  // 동별
            {
                if (a_strDong == "")
                    return;

                _strQry = string.Format("SELECT LobbyName FROM kms.UKR_Sensitivity where LobbyName like '%{0}%';", a_strDong);
            }
            else if (a_nType == 3)  // 개별
            {
                if (a_strLobbyName == "")
                    return;

                _strQry = string.Format("SELECT LobbyName FROM kms.UKR_Sensitivity where LobbyName = '{0}';", a_strLobbyName);
            }
            else // 예외
            {
                _strQry = "SELECT LobbyName FROM kms.UKR_Sensitivity;";
            }

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            listView1.InvokeIfNeeded(() => listView1.Items.Clear());
            foreach (string[] _str in _qryList)
            {

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_str[0]);

                _lvi.SubItems.Add(a_strDistance);
                _lvi.SubItems.Add(a_strSensitivity);
                _lvi.SubItems.Add(a_strSpeed);
                    

                listView1.InvokeIfNeeded(() => listView1.Items.Add(_lvi));
            }

            listView1.InvokeIfNeeded(() => listView1.EndUpdate());
           
        }

        private void cb_lb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            string strLobbyName = combo.Text;

            LoadSensitivityLobbyName(3, "", strLobbyName, cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
        }

        private void cb_distance_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSensitivityLobbyName();
        }

        private void cb_sensitivity_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSensitivityLobbyName();
        }

        private void cb_speed_value_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSensitivityLobbyName();
        }

        private void LoadSensitivityLobbyName()
        {
            if (rb_total.Checked)
            {
                LoadSensitivityLobbyName(1, "", "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }
            else if (rb_dong.Checked)
            {
                LoadSensitivityLobbyName(2, cb_dong_name.Text, "", cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }
            else if (rb_each.Checked)
            {
                LoadSensitivityLobbyName(3, "", cb_lb_name.Text, cb_distance_value.Text, cb_sensitivity_value.Text, cb_speed_value.Text);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            Boolean bFlag = false;
            // 리스트 뷰 데이터읽음.
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                string strLobbyName = listView1.Items[i].SubItems[1].Text.Replace("0x", "");
                string strDistance = listView1.Items[i].SubItems[2].Text.Replace("0x", "");
                string strSensitivity = listView1.Items[i].SubItems[3].Text.Replace("0x", "");
                string strSpeed = listView1.Items[i].SubItems[4].Text.Replace("0x", "");

                Console.WriteLine("{0}, {1}, {2}, {3}", strLobbyName, strDistance, strSensitivity, strSpeed);

                // DB Flag Setting
                if (strLobbyName == "" ||
                    strDistance == "" ||
                    strSensitivity == "" ||
                    strSpeed == "")
                    continue;   // 빈 값이 있으면, 쿼리 실행을 안함.

                string _strQry = string.Format("Update kms.UKR_Sensitivity Set Distance = '{0}', Sensitivity = '{1}', Speed = '{2}', SetFlag = 1 where LobbyName = '{3}';", strDistance, strSensitivity, strSpeed, strLobbyName);

                m_mysql.MySqlExec(_strQry);
                bFlag = true;


                Thread.Sleep(10);
            }

            if(bFlag)
                MessageBox.Show("감도 조절 값이 DB에 셋팅 되었습니다.");
            
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
                    //Color bgColor = Color.FromArgb(239, 249, 254);
                    e.Graphics.FillRectangle(new SolidBrush(m_headerBgColor), e.Bounds);
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

        private void btnSend_MouseDown(object sender, MouseEventArgs e)
        {
            btnSend.BackgroundImage = Properties.Resources.btn_send_click;
        }

        private void btnSend_MouseHover(object sender, EventArgs e)
        {
            btnSend.BackgroundImage = Properties.Resources.btn_send_over;
        }

        private void btnSend_MouseLeave(object sender, EventArgs e)
        {
            btnSend.BackgroundImage = Properties.Resources.btn_send_normal;
        }

        private void btnSend_MouseUp(object sender, MouseEventArgs e)
        {
            btnSend.BackgroundImage = Properties.Resources.btn_send_normal;
        }
    }
}
