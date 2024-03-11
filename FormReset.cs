using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CST.Utill;

namespace SmartOnePass
{
    public struct RESET_DATA
    {
        bool    m_useTimer;

        bool    m_useRetry;

        bool    m_useAM;
        bool    m_useFM;

        int     m_amHour;
        int     m_amMinute;

        int     m_fmHour;
        int     m_fmMinute;

        public bool UseTimer
        {
            get { return m_useTimer; }
            set { m_useTimer = value; }
        }

        public bool UseFailToRetry 
        {
            get { return m_useRetry; }
            set { m_useRetry = value; }
        }

        public bool UseAM
        {
            get { return m_useAM; }
            set { m_useAM = value; }
        }

        public bool UseFM
        {
            get { return m_useFM; }
            set { m_useFM = value; }
        }

        public int AM_Hour
        {
            get { return m_amHour; }
            set { m_amHour = value; }
        }

        public int AM_Minute
        {
            get { return m_amMinute; }
            set { m_amMinute = value; }
        }

        public int FM_Hour
        {
            get { return m_fmHour; }
            set { m_fmHour = value; }
        }

        public int FM_Minute
        {
            get { return m_fmMinute; }
            set { m_fmMinute = value; }
        }
    }

    public partial class FormReset : Form
    {
        private FormMain _parent;

        private MySqlDB m_mysql;

        private RESET_DATA m_resetData;

        // 접속한 리스트 정보
        // 리셋폼을 열기전에 접속한 리더기에 한 해서만 가능
        // 실시간으로 업데이트 되지 않는다.

        // 전체 로비 이름
        private List<string[]> m_listLobbyName = new List<string[]>();

        // Log 기록 추가
        private Log m_logException = new Log("LogCrt");

        public FormReset(FormMain parent)
        {
            InitializeComponent();

            _parent = parent;
        }

        public void SetMySql(MySqlDB mysql)
        {
            m_mysql = mysql;
        }

        private void FormReset_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            // 로비 이름 전부를 가져 온다.
            string strQry = string.Format("SELECT l.Lobby_GID, l.Lobby_DID, l.Lobby_Name FROM Lobby_ID l, Dong_Lobby d WHERE l.Lobby_Name = d.Lobby_Name GROUP BY Lobby_Name;");

            m_listLobbyName = m_mysql.MySqlSelect(strQry, 3);

            // 로비 이름들을 콤보박스에 추가
            _cbxLobbyName.InvokeIfNeeded(() =>
                {
                    foreach (string[] str in m_listLobbyName)
                        _cbxLobbyName.Items.Add(str[2]);

                    _cbxLobbyName.EndUpdate();
                });

            // 리셋 타이머 정보
            m_resetData = JsonSerializer.Instance.LoadAndDeserialize<RESET_DATA>(Application.StartupPath, "Timer");

            _chbUseTimer.Checked = m_resetData.UseTimer;

            if (m_resetData.UseTimer)
                _btnTimerStart.Enabled = false;

            _chbTimerAM.Checked = m_resetData.UseAM;
            _chbTimerFM.Checked = m_resetData.UseFM;

            _cmbHourAM.Text = m_resetData.AM_Hour.ToString();
            _cmbMinuteAM.Text = m_resetData.AM_Minute.ToString();

            _cmbHourFM.Text = m_resetData.FM_Hour.ToString();
            _cmbMinuteFM.Text = m_resetData.FM_Minute.ToString();


            Program.CreateRoundRectRgn((Control)_cbxLobbyName);
            Program.CreateRoundRectRgn((Control)_cmbHourAM);
            Program.CreateRoundRectRgn((Control)_cmbMinuteAM);
            Program.CreateRoundRectRgn((Control)_cmbHourFM);
            Program.CreateRoundRectRgn((Control)_cmbMinuteFM);
        }

        // 선택 리셋 ==> 수정(2023-10-17) 기존에는 Thrad를 활용해서 Flag 세팅을 했는데, 
        // 수정된 버전(1.6) 버전에서는 바로 리셋 명령어를 보냄.
        private void _btnReset_Click(object sender, EventArgs e)
        {
            _cbxLobbyName.Invoke(new MethodInvoker(delegate
            {
                string strLobbyName = _cbxLobbyName.Text;

                if(strLobbyName == "")
                    return;

                List<string> list = new List<string>();
                list.Add(strLobbyName);

                if (list.Count > 0)
                {
                    Program.mMainCb.doCallUkrReset(list);
                }
            }));
            
        }

        // 전체 리셋
        private void _btnAllReset_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (string[] lobbyName in m_listLobbyName)
            {
                list.Add(lobbyName[2]);
            }

            if (list.Count > 0)
            {
                Program.mMainCb.doCallUkrReset(list);
            }
        }

        // 폼 닫기
        private void FormReset_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        // 타이머 사용 유무
        private void _chbUseTimer_CheckedChanged(object sender, EventArgs e)
        {
            m_resetData.UseTimer = _chbUseTimer.Checked;
        }

        // 실패 시 재 전송
        private void _chbRetry_CheckedChanged(object sender, EventArgs e)
        {
            m_resetData.UseFailToRetry = _chbRetry.Checked;
        }

        // 오전 타이머
        private void _chbTimerAM_CheckedChanged(object sender, EventArgs e)
        {
            if (_chbTimerAM.Checked)
            {
                _cmbHourAM.Enabled = true;
                _cmbMinuteAM.Enabled = true;

                m_resetData.UseAM = true;
            }
            else
            {
                _cmbHourAM.Enabled = false;
                _cmbMinuteAM.Enabled = false;

                m_resetData.UseAM = false;
            }
        }

        // 오후 타이머
        private void _chbTimerFM_CheckedChanged(object sender, EventArgs e)
        {
            if (_chbTimerFM.Checked)
            {
                _cmbHourFM.Enabled = true;
                _cmbMinuteFM.Enabled = true;

                m_resetData.UseFM = true;
            }
            else
            {
                _cmbHourFM.Enabled = false;
                _cmbMinuteFM.Enabled = false;

                m_resetData.UseFM = false;
            }
        }

        // 설정 저장
        private void _btnTimerSet_Click(object sender, EventArgs e)
        {
            TimerUkrReset tur = new TimerUkrReset();
            tur.m_bFlag = _chbUseTimer.Checked;
            tur.m_strHour = _cmbHourAM.Text;
            tur.m_strMin = _cmbMinuteAM.Text;

            Program.mMainCb.doCallTimerUkrReset(tur);

            int outTemp;

            if (int.TryParse(_cmbHourAM.Text, out outTemp))
                m_resetData.AM_Hour = outTemp;

            if (int.TryParse(_cmbMinuteAM.Text, out outTemp))
                m_resetData.AM_Minute = outTemp;

            m_resetData.UseTimer = _chbUseTimer.Checked;

            JsonSerializer.Instance.SerializeAndSave<RESET_DATA>(m_resetData, "Timer");

            MessageBox.Show("타이머 설정이 저장 되었습니다");

            this.Close();
            
        }

        // 타이머 정지
        private void _btnTimerStop_Click(object sender, EventArgs e)
        {
            //_parent.ResetTimer(false);

            //_btnTimerStart.Enabled = true;

            //_btnTimerStop.Enabled = false;

            //_btnTimerStop.ForeColor = Color.White;
            //_btnTimerStart.ForeColor = Color.White;

            //_parent.LogPrint(" 리셋 타이머가 정지 되었습니다.");
        }

        // 타이머 시작
        private void _btnTimerStart_Click(object sender, EventArgs e)
        {

            // 타이머 설정을 다시 읽어 온다.
            /*
            _parent.ResetTimer(true);

            _btnTimerStop.Enabled = true;

            _btnTimerStart.Enabled = false;

            _btnTimerStop.ForeColor = Color.White;
            _btnTimerStart.ForeColor = Color.White;

            _parent.LogPrint(" 리셋 타이머를 시작 합니다.");
            */
        }

        private void _btnReset_MouseDown(object sender, MouseEventArgs e)
        {
            _btnReset.BackgroundImage = Properties.Resources.btn_reset_click;
        }

        private void _btnReset_MouseHover(object sender, EventArgs e)
        {
            _btnReset.BackgroundImage = Properties.Resources.btn_reset_over;
        }

        private void _btnReset_MouseLeave(object sender, EventArgs e)
        {
            _btnReset.BackgroundImage = Properties.Resources.btn_reset_normal;
        }

        private void _btnReset_MouseUp(object sender, MouseEventArgs e)
        {
            _btnReset.BackgroundImage = Properties.Resources.btn_reset_normal;
        }

        private void _btnAllReset_MouseDown(object sender, MouseEventArgs e)
        {
            _btnAllReset.BackgroundImage = Properties.Resources.btn_all_reset_click;
        }

        private void _btnAllReset_MouseHover(object sender, EventArgs e)
        {
            _btnAllReset.BackgroundImage = Properties.Resources.btn_all_reset_over;
        }

        private void _btnAllReset_MouseLeave(object sender, EventArgs e)
        {
            _btnAllReset.BackgroundImage = Properties.Resources.btn_all_reset_normal;
        }

        private void _btnAllReset_MouseUp(object sender, MouseEventArgs e)
        {
            _btnAllReset.BackgroundImage = Properties.Resources.btn_all_reset_normal;
        }

        private void _btnTimerSet_MouseDown(object sender, MouseEventArgs e)
        {
            _btnTimerSet.BackgroundImage = Properties.Resources.btn_setting_save_click;
        }

        private void _btnTimerSet_MouseHover(object sender, EventArgs e)
        {
            _btnTimerSet.BackgroundImage = Properties.Resources.btn_setting_save_over;
        }

        private void _btnTimerSet_MouseLeave(object sender, EventArgs e)
        {
            _btnTimerSet.BackgroundImage = Properties.Resources.btn_setting_save_normal;
        }

        private void _btnTimerSet_MouseUp(object sender, MouseEventArgs e)
        {
            _btnTimerSet.BackgroundImage = Properties.Resources.btn_setting_save_normal;
        }

        private void _btnTimerStop_MouseDown(object sender, MouseEventArgs e)
        {
            _btnTimerStop.BackgroundImage = Properties.Resources.btn_stop_click;
        }

        private void _btnTimerStop_MouseHover(object sender, EventArgs e)
        {
            _btnTimerStop.BackgroundImage = Properties.Resources.btn_stop_over;
        }

        private void _btnTimerStop_MouseLeave(object sender, EventArgs e)
        {
            _btnTimerStop.BackgroundImage = Properties.Resources.btn_stop_normal;
        }

        private void _btnTimerStop_MouseUp(object sender, MouseEventArgs e)
        {
            _btnTimerStop.BackgroundImage = Properties.Resources.btn_stop_normal;
        }

        private void _btnTimerStart_MouseDown(object sender, MouseEventArgs e)
        {
            _btnTimerStart.BackgroundImage = Properties.Resources.btn_start_click;
        }

        private void _btnTimerStart_MouseHover(object sender, EventArgs e)
        {
            _btnTimerStart.BackgroundImage = Properties.Resources.btn_start_over;
        }

        private void _btnTimerStart_MouseLeave(object sender, EventArgs e)
        {
            _btnTimerStart.BackgroundImage = Properties.Resources.btn_start_normal;
        }

        private void _btnTimerStart_MouseUp(object sender, MouseEventArgs e)
        {
            _btnTimerStart.BackgroundImage = Properties.Resources.btn_start_normal;
        }

        private void _btnTimerStop_EnabledChanged(object sender, EventArgs e)
        {
            _btnTimerStart.ForeColor = Color.White;
            _btnTimerStop.ForeColor = Color.White;
        }

        private void _btnTimerStart_EnabledChanged(object sender, EventArgs e)
        {
            _btnTimerStart.ForeColor = Color.White;
            _btnTimerStop.ForeColor = Color.White;
        }
    }
}
