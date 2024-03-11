using System;
using SmartOnePass.CRT;
using System.Threading;

namespace SmartOnePass
{
    class Crt_OTS : CRTBase
    {
        private object _lock = new object();

        public override bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byTakeOn, byte a_byTakeOff, float delyTime)
        {
            try
            {
                int nRetryCount = 0;
                string strPacket = "Type=LOBBY&Dong=" + a_nDong + "&Ho=" + a_nHo + "&Floor=" + a_byTakeOn + "&Destination=" + a_byTakeOff;
                strPacket = strPacket.Length.ToString("D8") + strPacket;

                while (nRetryCount < 3)
                {
                    if (this.IsCrtConnected())
                    {
                        byte[] byData = System.Text.Encoding.ASCII.GetBytes(strPacket);
                        int ret = m_hSocket.Send(byData);

                        if (m_fnLogPrint != null)
                        {
                            m_fnLogPrint(string.Format("Send Call Packet: {0} , Dong = {1}, Ho = {2}", strPacket, a_nDong, a_nHo));
                        }

                        // DB에 Log 저장(엘리베이터 호출 정상)
                        string strCommnet = string.Format("탑승층 호출: Take On:{0}, Off:{1}", a_byTakeOn, a_byTakeOff);
                        Program.doDBLogKmsCrt(a_strLobbyName, a_nDong.ToString(), a_nHo.ToString(), strCommnet, strPacket);

                        if (a_byTakeOff == 0)
                            return true;

                        // 목적층이 있을 때, 호출
                        Thread th = new Thread(() => TakeOffCallThread(a_strLobbyName, a_nDong, a_nHo, a_byTakeOn, a_byTakeOff));

                        th.IsBackground = true;

                        th.Start();

                        return true;
                    }
                    else
                    {
                        if (m_fnLogPrint != null)
                            m_fnLogPrint("엘리베이터 서버와 연결이 끊어 졌습니다.");
                    }

                    nRetryCount++;
                }
                return false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtCall() 예외 발생:" + ex.Message.ToString());
                return false;
            }
        }

        // 엘리베이터 호출 후 일정 딜레이 후 다시 호출 하도록 한다.
        // 일단 딜레이는 5초
        private void TakeOffCallThread(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byTakeOn, byte a_byTakeOff)
        {
            Thread.Sleep(5 * 1000);

            try
            {
                int nRetryCount = 0;
                string strPacket = "Type=LOBBY&Dong=" + a_nDong + "&Ho=" + a_nHo + "&Floor=" + a_byTakeOn + "&Destination=" + a_byTakeOff;
                strPacket = strPacket.Length.ToString("D8") + strPacket;

                while (nRetryCount < 5)
                {
                    lock (_lock)
                    {
                        if (this.IsCrtConnected())
                        {
                            byte[] byData = System.Text.Encoding.ASCII.GetBytes(strPacket);
                            int ret = m_hSocket.Send(byData);

                            if (ret > 0)
                            {
                                if (m_fnLogPrint != null)
                                    m_fnLogPrint(string.Format("Send TakeOff Packet: {0}", strPacket));

                                // DB에 Log 저장(엘리베이터 호출 정상(목적층))
                                string strCommnet = string.Format("목적층 호출: Take On:{0}, Off:{1}", a_byTakeOff, a_byTakeOff);
                                Program.doDBLogKmsCrt(a_strLobbyName, a_nDong.ToString(), a_nHo.ToString(), strCommnet, strPacket);
                            }
                            return;
                        }
                        else
                        {
                            if (m_fnLogPrint != null)
                                m_fnLogPrint("엘리베이터 서버와 연결이 끊어 졌습니다.");
                        }
                    }

                    nRetryCount++;
                }
                return;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtCall() 예외 발생:" + ex.Message.ToString());
                return;
            }
        }
    }
}
