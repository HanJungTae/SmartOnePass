using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartOnePass
{
    public partial class UC_Map : UserControl
    {
        public delegate void LobbyBtnClick(string a_strLBName);
        public event LobbyBtnClick lobbyBtnClick;

        public Button[] BTN;
        private string m_strDong;

        public UC_Map()
        {
            InitializeComponent();
        }

        public void setDong(string a_strDong)
        {
            m_strDong = a_strDong;
        }

        public string getDong()
        {
            return m_strDong;
        }

        private void UC_Map_Load(object sender, EventArgs e)
        {

        }

        public void CreateLobbyName(List<LobbyBtnInfo> a_listLBName)  // 2021-06-21
        {
            BTN = new Button[a_listLBName.Count];

            Console.WriteLine("CreateLobbyName = {0}", a_listLBName.Count);
            int startLocation = 0;

            foreach (LobbyBtnInfo lobbyBtnInfo in a_listLBName)
            {
                // 2024-03-11 예외 처리: 동원개발 서면 주상복합 현장에서 세로로 출입이 21개가 있어,
                // 세로로 출입을 하는 위치에 대한 인덱싱을 예외 처리를 할 수 있게 수정하였음.
                int nHeightCompareCnt = 9;
                if(lobbyBtnInfo.nPosHeightIdx > nHeightCompareCnt)
                {
                    lobbyBtnInfo.nPosWightIdx = lobbyBtnInfo.nPosWightIdx + (lobbyBtnInfo.nPosHeightIdx - 1) / 9;
                    lobbyBtnInfo.nPosHeightIdx = lobbyBtnInfo.nPosHeightIdx - (nHeightCompareCnt * ((lobbyBtnInfo.nPosHeightIdx - 1) / 9));
                }
                    
                Console.WriteLine("이름:" + lobbyBtnInfo.strLobbyName + "      width Idx:" + lobbyBtnInfo.nPosWightIdx + "      height Idx:" + lobbyBtnInfo.nPosHeightIdx);
                switch (lobbyBtnInfo.nPosWightIdx)
                {
                    case 1:
                        startLocation = 150;
                        break;
                    case 2:
                        startLocation = 100;
                        break;
                    case 3:
                        startLocation = 50;
                        break;
                    case 4:
                        startLocation = 10;
                        break;
                    default:
                        startLocation = 10;
                        break;
                }
            }

            int nCnt = 0;
            foreach(LobbyBtnInfo lobbyBtnInfo in a_listLBName)
            {
                BTN[nCnt] = new Button();
                BTN[nCnt].Name = lobbyBtnInfo.strLobbyName;
                BTN[nCnt].Parent = this;
                BTN[nCnt].Location = new Point(startLocation + (lobbyBtnInfo.nPosWightIdx * 150), 50 + (lobbyBtnInfo.nPosHeightIdx) * 70);
                BTN[nCnt].Size = new Size(130, 30);
                BTN[nCnt].Font = new Font("돋움", 9, FontStyle.Bold);
                
                BTN[nCnt].BackColor = System.Drawing.Color.Transparent;

                BTN[nCnt].Click += new EventHandler(btn_click);
                BTN[nCnt].MouseDown += new MouseEventHandler(btn_down);
                BTN[nCnt].MouseUp += new MouseEventHandler(btn_up);
                BTN[nCnt].MouseHover += new EventHandler(btn_mouseHover);
                BTN[nCnt].MouseLeave += new EventHandler(btn_mouseLeave);

                BTN[nCnt].Text = lobbyBtnInfo.strLobbyName;
                BTN[nCnt].BackgroundImage = Properties.Resources.Btn_Lobby_Normal_xxx;
                BTN[nCnt].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                BTN[nCnt].TabStop = false;
                BTN[nCnt].FlatStyle = FlatStyle.Flat;
                BTN[nCnt].FlatAppearance.BorderSize = 0;
                BTN[nCnt].Tag = true;
                nCnt++;
            }
        }

        public void SetLBStateChange(string a_strLBName, int a_nState)
        {
            string _strLBName = "";
            foreach (Control _ctrl in BTN)
            {
                _strLBName = _ctrl.Text;

                if(_strLBName == a_strLBName)
                {
                    if (a_nState == 0)   // 통신이 끊어 졌음.(Ping도 안됨)
                    {
                        _ctrl.BackColor = System.Drawing.Color.Gray;
                    }
                    else if(a_nState == 1)  // 통신이 정상
                    {
                        _ctrl.BackColor = System.Drawing.Color.GreenYellow;
                            
                    }
                    else if (a_nState == 2) // 통신 이상 Ping은 됨.
                    {
                        _ctrl.BackColor = System.Drawing.Color.LightGray;
                    }

                    _ctrl.Update();
                    break;
                }
            }
        }

        public void btn_click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl != null)
            {
                //MessageBox.Show("Name : " + ctrl.Name + ", Text : " + ctrl.Text);
                lobbyBtnClick(ctrl.Name.ToString());
            }
        }

        // 로비 버튼 마우스 Up
        public void btn_up(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Boolean bFlag = (Boolean)btn.Tag;
            if (bFlag)
            {
                btn.BackColor = System.Drawing.Color.White;
                btn.BackgroundImage = Properties.Resources.Btn_Lobby_Normal_xxx;
                btn.Invalidate();
            }
        }

        // 로비 버튼 마우스 down
        public void btn_down(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Boolean bFlag = (Boolean)btn.Tag;
            if (bFlag)
            {
                btn.BackColor = System.Drawing.Color.White;
                btn.BackgroundImage = Properties.Resources.Btn_Lobby_Click_xxx;
                btn.Invalidate();
            }
            
        }

        // 로비 버튼 마우스 Over
        public void btn_mouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Boolean bFlag = (Boolean)btn.Tag;
            if (bFlag)
            {
                btn.BackColor = System.Drawing.Color.Transparent;
                btn.BackColor = System.Drawing.Color.White;
                btn.BackgroundImage = Properties.Resources.Btn_Lobby_Over_xxx;
                btn.Invalidate();
            }
        }

        // 로비 버튼 마우스 Leave
        public void btn_mouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Boolean bFlag = (Boolean)btn.Tag;
            if (bFlag)
            {
                btn.BackColor = System.Drawing.Color.White;
                btn.BackgroundImage = Properties.Resources.Btn_Lobby_Normal_xxx;
                btn.Invalidate();
            }
        }

        public void BtnEnable(bool a_nFlag)
        {
            for (int i = 0; i < BTN.Length; i++)
            {
                BTN[i].Enabled = a_nFlag;
            }
        }

    }
}
