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
    public partial class FormPassword : Form
    {
        readonly string PASSWORD = "chamsule";

        private bool m_bPasswordFlag = false;

        public FormPassword()
        {
            InitializeComponent();

            this.txtboxPw.Text = "";
            this.txtboxPw.PasswordChar = '*';
        }

        private void btnPw_Click(object sender, EventArgs e)
        {
            if (txtboxPw.Text == PASSWORD)
            {
                Program.g_bpassword = true;
                this.Close();
            }
            else 
            { 
                MessageBox.Show("암호가 틀렸습니다.");
                m_bPasswordFlag = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!m_bPasswordFlag)   // Flag 처리를 하지 않으면 메세지 창에서 Enter를 누르면 KeyUp 이벤트를 계속해서 발생
                {
                    btnPw_Click(sender, e);
                    return;
                }

                m_bPasswordFlag = false;
            }
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void btnPw_MouseDown(object sender, MouseEventArgs e)
        {
            btnPw.BackgroundImage = Properties.Resources.BtnRegister_Click_xxx;
        }

        private void btnPw_MouseHover(object sender, EventArgs e)
        {
            btnPw.BackgroundImage = Properties.Resources.BtnRegister_Over_xxx;
        }

        private void btnPw_MouseLeave(object sender, EventArgs e)
        {
            btnPw.BackgroundImage = Properties.Resources.BtnRegister_Normal_xxx;
        }

        private void btnPw_MouseUp(object sender, MouseEventArgs e)
        {
            btnPw.BackgroundImage = Properties.Resources.BtnRegister_Click_xxx;
        }

        private void btnExit_MouseDown(object sender, MouseEventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.btn_close_click;
        }

        private void btnExit_MouseHover(object sender, EventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.btn_close_over;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.btn_close_normal;
        }

        private void btnExit_MouseUp(object sender, MouseEventArgs e)
        {
            btnExit.BackgroundImage = Properties.Resources.btn_close_normal;
        }
    }
}
