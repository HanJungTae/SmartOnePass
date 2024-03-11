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
    public partial class FormSyncInfo : Form
    {
        private MySqlDB m_mysql;

        public FormSyncInfo()
        {
            InitializeComponent();
        }

        private void FormSyncInfo_Load(object sender, EventArgs e)
        {
            SyncTblLoad();
            progressBar.Hide();
        }
        
        public void SetMySql(MySqlDB a_mysql)
        {
            m_mysql = a_mysql;
        }

        private void SyncTblLoad()
        {
            
            string strQry = "Select LobbyName from kms.Lobby_Info where LobbyName != 'register';";

            List<string[]> qryList = m_mysql.MySqlSelect(strQry, 1);

            lv_sync_info.Invoke(new MethodInvoker(delegate
            {
                lv_sync_info.Items.Clear();
                foreach (string[] str in qryList)
                {
                    ListViewItem _lvi = new ListViewItem("");
                    _lvi.SubItems.Add(str[0]);

                    lv_sync_info.Items.Add(_lvi);
                }

                lv_sync_info.EndUpdate();
            }));
        }

        private void btn_sync_set_Click(object sender, EventArgs e)
        {
            Thread hThread = new Thread(new ThreadStart(LobbySyncApply));
            hThread.Start();
        }

        private void btn_sync_allselect_Click(object sender, EventArgs e)
        {
            lv_sync_info.Invoke(new MethodInvoker(delegate
            {
                for (int i = 0; i < lv_sync_info.Items.Count; i++)
                {
                    lv_sync_info.Items[i].Checked = false;
                }
            }));

        }

        private void LobbySyncApply()
        {
            lv_sync_info.Invoke(new MethodInvoker(delegate
            {
                for (int i = 0; i < lv_sync_info.Items.Count; i++)
                {
                    if (lv_sync_info.Items[i].Checked)
                    {
                        string strLBName = lv_sync_info.Items[i].SubItems[1].Text;

                        string strQry = string.Format("INSERT INTO kms.Reg_{0} (Dong, Ho, Key_Sn, Key_Id, State) Select ki.Dong, ki.Ho, ki.Key_Sn, ki.Key_Id, 0 as State from kms.Dong_Lobby as dl, kms.Key_Info_Master as ki where dl.Lobby_Name = '{0}' and (dl.Dong = ki.Dong and dl.Ho = ki.Ho);", strLBName);

                        m_mysql.MySqlExec(strQry, "");
                        Thread.Sleep(5);

                    }

                }
                
            }));
        }

        private void btn_sync_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_ukr_keyid_alldelete_Click(object sender, EventArgs e)
        {
            lv_sync_info.Invoke(new MethodInvoker(delegate
            {
                List<string> list = new List<string>();
                for (int i = 0; i < lv_sync_info.Items.Count; i++)
                {
                    if (lv_sync_info.Items[i].Checked)
                    {
                        string strLBName = lv_sync_info.Items[i].SubItems[1].Text;
                        list.Add(strLBName);
                    }
                }

                if (list.Count > 0)
                {
                    Program.mMainCb.doCallUkrIdDelete(list);
                }
            }));
        }

        private void btn_sync_set_MouseLeave(object sender, EventArgs e)
        {
            btn_sync_set.BackgroundImage = Properties.Resources.BtnBasic_Normal_xxx;
        }

        private void btn_sync_set_MouseHover(object sender, EventArgs e)
        {
            btn_sync_set.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_ukr_keyid_alldelete_MouseHover(object sender, EventArgs e)
        {
            btn_ukr_keyid_alldelete.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_ukr_keyid_alldelete_MouseLeave(object sender, EventArgs e)
        {
            btn_ukr_keyid_alldelete.BackgroundImage = Properties.Resources.BtnBasic_Normal_xxx;
        }


        private void btn_sync_allselect_MouseHover(object sender, EventArgs e)
        {
            btn_sync_allselect.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_sync_allselect_MouseLeave(object sender, EventArgs e)
        {
            btn_sync_allselect.BackgroundImage = Properties.Resources.BtnBasic_Normal_xxx;
        }


        private void btn_sync_close_MouseHover(object sender, EventArgs e)
        {
            btn_sync_close.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_sync_close_MouseLeave(object sender, EventArgs e)
        {
            btn_sync_close.BackgroundImage = Properties.Resources.BtnBasic_Normal_xxx;
        }

        private void btn_sync_set_MouseDown(object sender, MouseEventArgs e)
        {
            btn_sync_set.BackgroundImage = Properties.Resources.BtnBasic_Click_xxx;
        }

        private void btn_ukr_keyid_alldelete_MouseDown(object sender, MouseEventArgs e)
        {
            btn_ukr_keyid_alldelete.BackgroundImage = Properties.Resources.BtnBasic_Click_xxx;
        }

        private void btn_sync_allselect_MouseDown(object sender, MouseEventArgs e)
        {
            btn_sync_allselect.BackgroundImage = Properties.Resources.BtnBasic_Click_xxx;
        }

        private void btn_sync_close_MouseDown(object sender, MouseEventArgs e)
        {
            btn_sync_close.BackgroundImage = Properties.Resources.BtnBasic_Click_xxx;
        }

        private void btn_sync_set_MouseUp(object sender, MouseEventArgs e)
        {
            btn_sync_set.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_ukr_keyid_alldelete_MouseUp(object sender, MouseEventArgs e)
        {
            btn_ukr_keyid_alldelete.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_sync_allselect_MouseUp(object sender, MouseEventArgs e)
        {
            btn_sync_allselect.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

        private void btn_sync_close_MouseUp(object sender, MouseEventArgs e)
        {
            btn_sync_close.BackgroundImage = Properties.Resources.BtnBasic_Over_xxx;
        }

    }
}
