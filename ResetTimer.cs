using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CST.Utill;

namespace SmartOnePass
{
    /// <summary>
    /// 오전, 오후 시간 별로 리셋 진행 하는 타이머
    /// <para>SetTimer()  : 시작 폴더의 Timer.json 파일로 부터 설정 정보를 읽어오고 타이머 시작</para>
    /// <para>리셋 진행 중 접속이 끊어 진 리더기는 리셋을 하지 못한다.</para>
    /// </summary>
    class ResetTimer
    {
        private RESET_DATA m_timerData;

        // 1분 마다 시간 확인 하는 타이머
        private Timer m_checkTimer;

        // 설정한 시간이 되면, 10초 간격으로 리셋 Flag 변경 하는 타이머
        private Timer m_resetTimer;

        // 현재 리셋 진행 중인 UKR
        private int m_currentUKR;

        private List<DevInfo> m_listUkr;

        private FormMain m_parent;

        // 엘리베이터 리셋
        private int m_crtReset;

        public ResetTimer(FormMain parent)
        {
            m_parent = parent;

            m_checkTimer = new Timer();

            m_checkTimer.Interval = 1000 * 10;// 10초에 한번씩
            //_checkTimer.Interval = 1000 * 1;// 1초에 한번씩

            m_checkTimer.Tick += new EventHandler(CheckResetTime);


            m_resetTimer = new Timer();

            m_resetTimer.Interval = 1000 * 10;

            m_resetTimer.Tick += new EventHandler(Reset);


            m_crtReset = 0;
        }

        // 타이머 설정 및 시작
        public void SetTimer(ref List<DevInfo> ukrList)
        {
            m_timerData = JsonSerializer.Instance.LoadAndDeserialize<RESET_DATA>(Application.StartupPath, "Timer");

            m_parent.LogPrint(string.Format("타이머 : {0} ", m_timerData.UseTimer));

            m_checkTimer.Stop();

            m_currentUKR = 0;

            m_checkTimer.Start();

            if (m_timerData.UseTimer == false)
            {
                return;
            }

            if (m_timerData.UseAM == true)
                m_parent.LogPrint(string.Format("타이머 시간 / 오전 / {0} 시 {1} 분", m_timerData.AM_Hour, m_timerData.AM_Minute));

            if (m_timerData.UseFM == true)
                m_parent.LogPrint(string.Format("타이머 시간 / 오후 / {0} 시 {1} 분", m_timerData.FM_Hour, m_timerData.FM_Minute));

            m_listUkr = ukrList;

            m_resetTimer.Stop();

        }

        // 설정된 시간 확인
        private void CheckResetTime(object sender, EventArgs e)
        {
            // 엘리베이터 리셋
            // _crtReset는 10초에 1씩 증가, 10분마다 재 접속 하도록 한다.
            m_crtReset++;

            if (m_crtReset > 60)
            {
                m_crtReset = 0;

                m_parent.CrtReconnection();

                if (Program.g_nRemoteUKROpen == 1)
                {
                    m_parent.WebReconnection();
                }
            }

            //2020-04-14 타이머를 사용 할 경우에만 리셋 타이며가 동작 하도록 조건문 추가 : 최준혁
            if (m_timerData.UseTimer == true)
            {
                // 오전 타이머 확인
                if (m_timerData.UseAM)
                {
                    if (DateTime.Now.Hour == m_timerData.AM_Hour && DateTime.Now.Minute == m_timerData.AM_Minute)
                    {
                        m_currentUKR = m_listUkr.Count - 1;

                        m_resetTimer.Start();

                        m_checkTimer.Stop();

                        m_parent.LogPrint(string.Format(" 리셋 시작 / 오전 / {0} : {1} ", DateTime.Now.Hour, DateTime.Now.Minute));

                        // 엘리베이터 재접속 
                        m_parent.CrtConnection();

                        return;
                    }
                }

                // 오후 타이머 확인
                if (m_timerData.UseFM)
                {
                    if (DateTime.Now.Hour == m_timerData.FM_Hour && DateTime.Now.Minute == m_timerData.FM_Minute)
                    {
                        m_currentUKR = m_listUkr.Count - 1;

                        m_resetTimer.Start();

                        m_checkTimer.Stop();

                        // 엘리베이터 재접속
                        m_parent.CrtConnection();

                        m_parent.LogPrint(string.Format(" 리셋 시작 / 오후 / {0} : {1} ", DateTime.Now.Hour, DateTime.Now.Minute));
                    }
                }
            }

            //Console.WriteLine(" Tick : {0} : {1} : {2}", DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
        }

        // 리셋
        private void Reset(object sender, EventArgs e)
        {
            // 뒤에서 부터 리셋을 시작 하는 이유
            // 앞 에서 시작 시, 리셋 하면 리스트에서 삭제되고
            // 재 접속 시 다시 리스트 뒤에 추가되어 리셋이 또 될 수있으므로
            // 뒤에서부터 앞으로 진행 하면서 리셋 한다.

            lock (m_listUkr)
            {
                if (m_listUkr.Count == 0)
                    return;

                if (m_currentUKR < 0)
                {
                    m_resetTimer.Stop();

                    m_checkTimer.Start();

                    return;
                }

                m_listUkr[m_currentUKR].bReset = true;

                m_currentUKR--;
            }
        }

        // 시작
        public void Start()
        {
            m_checkTimer.Start();

            m_resetTimer.Start();
        }

        // 정지
        public void Stop()
        {
            m_checkTimer.Stop();

            m_resetTimer.Stop();
        }
    }
}
