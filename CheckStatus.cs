using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;

namespace CheckNetControl
{
    class CheckStatus
    {
        public ManualResetEvent allDone = new ManualResetEvent(false);

        //private bool m_bWaitReply = false;
        private bool m_bReplySuccess = false;
        private Ping pingSender = new Ping();
        private int m_nTimeOut = 1000;
        private bool m_bUseLog = false;

        //-------------------------------------
        //생성자
        //-------------------------------------
        public CheckStatus()
        {
            pingSender.PingCompleted += new PingCompletedEventHandler(pingCompleted);
        }

        public CheckStatus(int a_nTimeOut)
        {
            pingSender.PingCompleted += new PingCompletedEventHandler(pingCompleted);
            m_nTimeOut = a_nTimeOut;
        }

        public bool CheckIpPingAsync(string strIp)
        {
            int _nSuccessCount = 0;

            for (int i = 0; i < 2; i++)
            {
                PingOptions options = new PingOptions(64, true);
                IPAddress ipAddress = IPAddress.Parse(strIp);

                string dataPing = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(dataPing);

                allDone.Reset();

                try
                {
                    pingSender.SendAsync(ipAddress, m_nTimeOut, buffer, options);
                }
                catch
                {
                    m_bReplySuccess = false;
                }
                allDone.WaitOne();

                buffer = null;
                if (m_bUseLog)
                {
                    Console.WriteLine("Ping Test Complete");
                }

                if (m_bReplySuccess == true)
                {
                    _nSuccessCount++;
                }
            }

            if (_nSuccessCount == 0)
            {
                return false;
            }
            return true;
        }

        public void pingCompleted(object sender, PingCompletedEventArgs e)
        {
            if (e.Reply.Status == IPStatus.Success)
            {
                if (m_bUseLog)
                {
                    Console.WriteLine(e.Reply.Address.ToString() + " +++");
                }
                m_bReplySuccess = true;
            }
            else
            {
                if (m_bUseLog)
                {
                    Console.WriteLine(e.Reply.Address.ToString() + " ---");
                }
                m_bReplySuccess = false;
            }
            //m_bWaitReply = false;
            allDone.Set();
        }

        public bool CheckIpPing(string strIp)
        {
            try
            {
                string dataPing = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] buffer = Encoding.ASCII.GetBytes(dataPing);
                //IP Address 할당 
                IPAddress ipAddress = IPAddress.Parse(strIp);
                PingOptions options = new PingOptions();

                // Use the default Ttl value which is 128,
                // but change the fragmentation behavior.
                options.DontFragment = true;

                int SuccessCount = 0;
                for (int i = 0; i < 3; i++)
                {
                    PingReply reply = pingSender.Send(ipAddress, m_nTimeOut, buffer, options);

                    if (reply.Status == IPStatus.Success)
                    {
                        SuccessCount++;
                        Console.WriteLine("Reply from {0}: bytes={1} time={2}ms TTL={3}", reply.Address, reply.Buffer.Length, reply.RoundtripTime, reply.Options.Ttl);
                    }
                    else
                    {
                        // Ping 실패시 강제 Exception
                        //throw new Exception();
                    }
                    Thread.Sleep(500);
                }
                if (SuccessCount == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Connect Fail... : " + ex);
                string errMsg = string.Format("호스트 {0}패킷 송신에 실패하였습니다..({1})", strIp, ex.Message);
                Console.WriteLine(errMsg);
                return false;
            }
        }
    }
}
