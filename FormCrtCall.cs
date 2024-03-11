using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace SmartOnePass
{
    public partial class FormCrtCall : Form
    {
        private MySqlDB m_mysql;

        public FormCrtCall()
        {
            InitializeComponent();
        }

        public void SetMySql(MySqlDB a_mysql)
        {
            m_mysql = a_mysql;
        }

        private void FormCrtCall_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            label5.Text = "Crt 업체: " + Program.g_CrtType;

            //////////////////////////////////////
            // 콤보 박스 로비 이름 Load
            string _strLBName = "";

            string _strQry = string.Format("SELECT Lobby_Name FROM Dong_Lobby group by Lobby_Name;");

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                cb_lb_name.InvokeIfNeeded(() => cb_lb_name.Items.Add(_strLBName));
            }
            cb_lb_name.InvokeIfNeeded(() => cb_lb_name.EndUpdate());

            Program.CreateRoundRectRgn((Control)cb_lb_name);
            Program.CreateRoundRectRgn((Control)txbDong);
            Program.CreateRoundRectRgn((Control)cb_lb_ho);
            Program.CreateRoundRectRgn((Control)cb_lb_keyid);
        }

        private void cb_lb_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _strLBName = "", _strQry = "";

            cb_lb_name.InvokeIfNeeded(() => _strLBName = cb_lb_name.Text);

            _strQry = string.Format("SELECT Dong, Ho FROM Dong_Lobby where Lobby_Name = '{0}' Order by Ho asc;", _strLBName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            cb_lb_ho.InvokeIfNeeded(() => cb_lb_ho.Items.Clear());
            cb_lb_keyid.InvokeIfNeeded(() => cb_lb_keyid.Items.Clear());
            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                txbDong.InvokeIfNeeded(() => txbDong.Text = _strLBName);

                _strLBName = _str[1];
                cb_lb_ho.InvokeIfNeeded(() => cb_lb_ho.Items.Add(_strLBName));
            }
            cb_lb_ho.InvokeIfNeeded(() => cb_lb_ho.EndUpdate());
        }

        private void cb_lb_ho_SelectedIndexChanged(object sender, EventArgs e)
        {
            string _strLBName = "", _strLBNameDong = "", _strQry = "";

            cb_lb_name.InvokeIfNeeded(() => _strLBName = cb_lb_ho.Text);
            txbDong.InvokeIfNeeded(() => _strLBNameDong = txbDong.Text);

            _strQry = string.Format("SELECT Key_Id FROM Key_Info_Master where Dong = '{0}' and Ho = '{1}' Order by Key_Id asc;", _strLBNameDong, _strLBName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            cb_lb_keyid.InvokeIfNeeded(() => cb_lb_keyid.Items.Clear());
            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                cb_lb_keyid.InvokeIfNeeded(() => cb_lb_keyid.Items.Add(_strLBName));
            }
            cb_lb_keyid.InvokeIfNeeded(() => cb_lb_keyid.EndUpdate());
        }

        private void cb_lb_keyid_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            byte nGid = 0, nDid = 0;
            byte[] byTemp;
            string _strLBName = "", _strQry = "";

            //Get KeyID
            cb_lb_keyid.InvokeIfNeeded(() => _strLBName = cb_lb_keyid.Text);
            byTemp = StringToByteArray(_strLBName);

            //Get GID/DID
            cb_lb_name.InvokeIfNeeded(() => _strLBName = cb_lb_name.Text);
            _strQry = string.Format("SELECT Lobby_GID, Lobby_DID FROM Lobby_ID where Lobby_Name = '{0}';", _strLBName);
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                nGid = byte.Parse(_str[0], System.Globalization.NumberStyles.AllowHexSpecifier);
                nDid = byte.Parse(_str[1], System.Globalization.NumberStyles.AllowHexSpecifier);
            }

            OPDevPtrl DevTestOP = new OPDevPtrl();
            DevTestOP.byData = new byte[byTemp.Length + 2];

            DevTestOP.byData[0] = nGid;
            DevTestOP.byData[1] = nDid;

            Array.Copy(byTemp, 0, DevTestOP.byData, 2, byTemp.Length);

            Program.g_fnKeyConfirm(DevTestOP);
        }

        public byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_search_MouseDown(object sender, MouseEventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_el_call_click;
        }

        private void btn_search_MouseHover(object sender, EventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_el_call_over;
        }

        private void btn_search_MouseLeave(object sender, EventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_el_call_normal;
        }

        private void btn_search_MouseUp(object sender, MouseEventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_el_call_normal;
        }

        private void btn_Close_MouseDown(object sender, MouseEventArgs e)
        {
            btn_Close.BackgroundImage = Properties.Resources.btn_close_click;
        }

        private void btn_Close_MouseHover(object sender, EventArgs e)
        {
            btn_Close.BackgroundImage = Properties.Resources.btn_close_over;
        }

        private void btn_Close_MouseLeave(object sender, EventArgs e)
        {
            btn_Close.BackgroundImage = Properties.Resources.btn_close_normal;
        }

        private void btn_Close_MouseUp(object sender, MouseEventArgs e)
        {
            btn_Close.BackgroundImage = Properties.Resources.btn_close_normal;
        }
    }
}
    