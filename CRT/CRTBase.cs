using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace SmartOnePass.CRT
{
    public struct CRT_CALL
    {
        public byte TakeOn;
        public byte TakeOff;
        public int Dong;
        public int Ho;
    }

    public struct CRT_STATE
    {
        // 현재 절대 층
        public byte Floor;

        // 방향 -> Up / Down / Stop
        public byte Direction;

        // 엘리베이터 상태 -> 자동, 전용, 점검, 파킹
        public byte State;

        // 엘리베이터 문 상태 -> 열림 / 닫힘
        public byte DoorState;
    }

    public enum CRT_TYPE
    {
        Hyundai = 0,
        Thyssen = 1,
        OTS = 2
    }

    class CRTBase
    {
        public virtual bool CrtCall(string a_strLobbyName, byte a_byCrtId, byte a_byTakeOn) { return true; }

        public virtual bool CrtCall(string a_strLobbyName, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff)
        {
            Console.WriteLine("[BASE]");
            return true; 
        }

        public virtual bool CrtCall(string a_strLobbyName, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff, float delyTime) { return true; }

        public virtual bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff, float delyTime) { return true; }

        public virtual bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byTakeOn, byte a_byTakeOff) { return true; }

        public virtual bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff) { return true; }

        public virtual bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byTakeOn, byte a_byTakeOff, float delyTime) { return true; }

        public delegate void LogPrint(String a_strLog);

        protected LogPrint  m_fnLogPrint    = null;

        protected Socket    m_hSocket       = null;

        protected string    m_strCrtIp      = "";

        protected int       m_nCrtPort      = 0;

        private bool        m_bTakeOffUse = false;

        protected Dictionary<int, List<CRT_CALL>> m_CallList;

        public string CrtIp
        {
            get { return m_strCrtIp; }
            set { m_strCrtIp = value; }
        }

        public int CrtPort
        {
            get { return m_nCrtPort; }
            set { m_nCrtPort = value; }
        }

        // 엘리베이터 정보
        public void SetCarCount(int count, string takeOffFlag)
        {
            if (takeOffFlag == "1")
                m_bTakeOffUse = true;

            m_CallList = new Dictionary<int, List<CRT_CALL>>();

            for (int i = 1; i <= count; i++)
            {
                List<CRT_CALL> list = new List<CRT_CALL>();

                m_CallList.Add(i, list);
            }
        }

        public void SetLogCallback(LogPrint a_fnLogPrint)
        {
            m_fnLogPrint = a_fnLogPrint;
        }

        public bool IsCrtConnected()
        {
            return Program.IsConnected(m_hSocket);
        }

        public bool CrtConnection()
        {
            try
            {
                if (m_hSocket != null)
                    m_hSocket.Close();

                m_hSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse(this.CrtIp), this.CrtPort);

                m_hSocket.Connect(_ipep);

                // 만약 Crt 서버에 접속하여 상태체크가 필요하다면 아래의 구문을 살려야함.
                /*
                SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
                _args.RemoteEndPoint = _ipep;
                _args.Completed += new EventHandler<SocketAsyncEventArgs>(Connected_Completed);   
                */
                
                //소켓 커넥션이 완료되면 리시브 이벤트를 등록한다.
                if (this.IsCrtConnected() == true)
                {
                    SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
                    byte[] _byBuffer = new byte[1024];
                    _receiveArgs.SetBuffer(_byBuffer, 0, 1024);

                    _receiveArgs.UserToken = m_hSocket;
                    _receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recive_Completed);
                    m_hSocket.ReceiveAsync(_receiveArgs);
                }

                return this.IsCrtConnected();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtConnection() 예외 발생: " + ex.Message.ToString());
                return false;
            }
        }

        public bool CrtConnection(string a_strIp, int a_nPort)
        {
            this.CrtIp = a_strIp;
            this.CrtPort = a_nPort;

            return this.CrtConnection();
        }

        public virtual void MessageResolver(byte[] message, int length)
        {

        }

        private void Recive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _socket = (Socket)sender;
                if (this.IsCrtConnected() && e.BytesTransferred > 0)
                {
                    byte[] _byBuffer = e.Buffer;

                    string _strData = Encoding.Default.GetString(_byBuffer, 0, e.BytesTransferred);

                    string strMsg = "";

                    for (int i = 0; i < e.BytesTransferred; i++)
                        strMsg = strMsg + " " + _byBuffer[i].ToString("X2");
                    /*
                    if (m_fnLogPrint != null && _byBuffer[3] != 0x01)
                        m_fnLogPrint("Recv Packet = " + strMsg);

                    */
                    if (m_bTakeOffUse == false) // 목적층 호출 사용 여부
                        return;
                   

                    if (this.IsCrtConnected())
                    {
                        e.SetBuffer(_byBuffer, 0, 1024);
                        _socket.ReceiveAsync(e);

                        MessageResolver(_byBuffer, e.BytesTransferred);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Recieve_Completed() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
            }
        }
    }
}
