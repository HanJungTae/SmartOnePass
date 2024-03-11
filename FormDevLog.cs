using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartOnePass
{
    public partial class FormDevLog : Form
    {
        private MySqlDB m_mysql;

        public FormDevLog()
        {
            InitializeComponent();
        }

        public void SetMySql(MySqlDB a_mysql)
        {
            m_mysql = a_mysql;
        }

        private void FormDevLog_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            //////////////////////////////////////
            // 콤보 박스 로비 이름 Load
            string _strLBName = "";

            string _strQry = string.Format("SELECT Lobby_Name FROM Dong_Lobby group by Lobby_Name;");

            List<string> _listLBName = new List<string>();
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                cb_lb_name.InvokeIfNeeded(() => cb_lb_name.Items.Add(_strLBName));
            }
            cb_lb_name.InvokeIfNeeded(() => cb_lb_name.EndUpdate());

            Program.CreateRoundRectRgn((Control)cb_lb_name);
            Program.CreateRoundRectRgn((Control)lv_log);

            lv_log.OwnerDraw = true;

            // 행 높이 조절
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 17);
            lv_log.SmallImageList = imgList;
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string _strLBName = "", _strQry = "", _strDate = "", _strComment = "";

            cb_lb_name.InvokeIfNeeded(() => _strLBName = cb_lb_name.Text);

            if (_strLBName == "")
            {
                _strQry = string.Format("SELECT LogDate, LobbyName, Comment FROM kms.Log_KmsDev Order by Num desc limit 1000 ;");
            }
            else
            {
                _strQry = string.Format("SELECT LogDate, LobbyName, Comment FROM kms.Log_KmsDev where LobbyName = '{0}' Order by Num desc limit 1000 ;", _strLBName);
            }

            List<string> _listLBName = new List<string>();
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 3);

            lv_log.InvokeIfNeeded(() => lv_log.Items.Clear());
            foreach (string[] _str in _qryList)
            {
                _strDate = _str[0];
                _strLBName = _str[1];
                _strComment = _str[2];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_strDate);
                _lvi.SubItems.Add(_strLBName);
                _lvi.SubItems.Add(_strComment);

                lv_log.InvokeIfNeeded(() => lv_log.Items.Add(_lvi));
            }
            lv_log.InvokeIfNeeded(() => lv_log.EndUpdate());
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_search_MouseDown(object sender, MouseEventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_search_click;
        }

        private void btn_search_MouseHover(object sender, EventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_search_over;
        }

        private void btn_search_MouseLeave(object sender, EventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_search_normal;
        }

        private void btn_search_MouseUp(object sender, MouseEventArgs e)
        {
            btn_search.BackgroundImage = Properties.Resources.btn_search_normal;
        }

        private void btn_close_MouseDown(object sender, MouseEventArgs e)
        {
            btn_close.BackgroundImage = Properties.Resources.btn_close_click;
        }

        private void btn_close_MouseHover(object sender, EventArgs e)
        {
            btn_close.BackgroundImage = Properties.Resources.btn_close_over;

        }

        private void btn_close_MouseLeave(object sender, EventArgs e)
        {
            btn_close.BackgroundImage = Properties.Resources.btn_close_normal;
        }

        private void btn_close_MouseUp(object sender, MouseEventArgs e)
        {
            btn_close.BackgroundImage = Properties.Resources.btn_close_normal;
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
