using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SmartOnePass
{
    public class Networkcs
    {
        // 2020-05-06 수정 삭제
        //public delegate void MyEventHandlerDevDataRecv();

        //public event MyEventHandlerDevDataRecv EvtDevDataRecv;

        // 2020-08-12 델리게이트 추가
        public delegate void MyRecvDataProc(OPDevPtrl a_OpDevData); // 2020-08-13 UKR에서 수신데이터를 바로 처리하기 위해 델리게이트를 만듬
        public delegate void MyWebDataProce(string strData);    // 2023-04-04 추가
        public MyRecvDataProc m_MyRecvDataProc;
        public MyWebDataProce m_WebRecvDataProc;


        private Log m_logCrt = new Log("LogCrt");


        private Socket m_socket = null;
        private int m_nPort;
        private Queue<OPDevPtrl> m_OPRecvData = new Queue<OPDevPtrl>();

        // 연결된 공동현관 리더기들의 세션
        private List<Socket> _clients = new List<Socket>();

        // Web 서버 연동을 위한 소켓
        private Socket m_ClientSocket = null;

        public int Port
        {
            get { return m_nPort; }
            set { m_nPort = value; }
        }

        public Socket SOCKET
        {
            get { return m_socket; }
            set { m_socket = value; }
        }

        public Queue<OPDevPtrl> OPRecvData
        {
            get { return m_OPRecvData; }
        }

        public List<Socket> Clients
        {
            get { return _clients; }
        }

        public void ServerStart()
        {
            IPEndPoint _ipep = new IPEndPoint(IPAddress.Any, this.Port);
            this.SOCKET = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            this.SOCKET.Bind(_ipep);
            this.SOCKET.Listen(1024);

            SocketAsyncEventArgs _args = new SocketAsyncEventArgs();
            _args.Completed += new EventHandler<SocketAsyncEventArgs>(Accept_Competed);
            this.SOCKET.AcceptAsync(_args);

        }

        public void ServerEnd()
        {
            _clients.Clear();

            this.SOCKET.Close();
        }

        public void SendAsync(Socket a_socket, byte [] a_byData)
        {
            Socket _socket = a_socket;

            SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();

            _sendArgs.SetBuffer(a_byData, 0, a_byData.Length);
            //_sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Send_Complete);
            //_sendArgs.UserToken = a_byData;

            string _strData = "";
            for (int i = 0; i < a_byData.Length; i++)
            {
                _strData = _strData + string.Format("{0:X2} ", a_byData[i]);
            }
            //Console.WriteLine("[Send]" + _strData);

            //_socket.SendAsync(_sendArgs);
            _socket.Send(a_byData);
        }

        private void Accept_Competed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _socket = e.AcceptSocket;

                SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();

                byte[] _byBuffer = new byte[1024];
                _receiveArgs.SetBuffer(_byBuffer, 0, 1024);
                _receiveArgs.UserToken = _socket;
                _receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recive_Completed);
                _socket.ReceiveAsync(_receiveArgs);

                _clients.Add(_socket);

                e.AcceptSocket = null;
                m_socket.AcceptAsync(e);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Accept_Competed() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);

                m_logCrt.SetLogFile("Crt", "Accept_Competed() 메소드 예외: " + ex.Message.ToString());
            }

        }

        private void Send_Complete(object sender, SocketAsyncEventArgs e)
        {
            //Socket _socket = (Socket)sender;
            //byte[] _data = (byte[])e.UserToken;
            //_socket.SendAsync(_sendArgs);
            //_socket.Send(_data);
        }

        byte m_byPreSeq = 0x00;
        byte m_byPreType = 0x00;
        byte m_byPreCmd = 0x00;
        byte m_byPreOpt = 0x00;
        byte m_byPreGid = 0x00;
        byte m_byPreDid = 0x00;

        private void Recive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _socket = (Socket)sender;
                if (_socket.Connected && e.BytesTransferred > 0)
                {
                    byte[] _byBuffer = e.Buffer;

                    string _strData = "";
                    for (int i = 0; i < e.BytesTransferred; i++)
                    {
                        _strData = _strData + string.Format("{0:X2} ", _byBuffer[i]);
                        //StartCode Check
                        if (_byBuffer[i] == 0x02 && i + 6 <= e.BytesTransferred)
                        {
                            //MAC & LEN check
                            if (_byBuffer[i + 1] == 0x33 && (i + _byBuffer[i + 6] + 9) <=  e.BytesTransferred)
                            {
                                byte _byLen = _byBuffer[i + 6];
                                byte[] _byPacket = new byte[_byLen + 9];
                                Array.Copy(_byBuffer, i, _byPacket, 0, _byLen + 9);

                                // 2021-04-21 추가
                                // 주의) Ack를 보냈는데도, 공동현관 리더기에서 같은 데이터를 반복해써 보내는 경우가 있다. 
                                // 그래서) Pre변수를 사용하여, 수신받은 Seq, Type, Cmd, Opt, Gid, Did가 같으면, 데이터 파싱을 하지 않고 continue를 한다.
                                if(m_byPreSeq == _byBuffer[i + 2] &&
                                    m_byPreType == _byBuffer[i + 3] &&
                                    m_byPreCmd == _byBuffer[i + 4] &&
                                    m_byPreOpt == _byBuffer[i + 5] &&
                                    m_byPreGid == _byBuffer[i + 7] &&
                                     m_byPreDid == _byBuffer[i + 8])
                                {
                                    if (m_byPreGid != 0xff) // 등록기 일때는 이전 패킷과 같은 지를 체크 하지 않는다.(2021-11-05)
                                    {
                                        continue;
                                    }
                                }

                                m_byPreSeq = _byBuffer[i + 2];
                                m_byPreType = _byBuffer[i + 3];
                                m_byPreCmd = _byBuffer[i + 4];
                                m_byPreOpt = _byBuffer[i + 5];
                                m_byPreGid = _byBuffer[i + 7];
                                 m_byPreDid = _byBuffer[i + 8];


                                //Console.WriteLine("[LEN]" + _byLen.ToString("X2"));

                                // 2020-05-06 상태 체크를 제외한 데이터 로그 처리

                                DoLogRecvData(_byPacket);
                                /*
                                if (m_byPreCmd == 0x02)
                                    return;
                                */

                                OPProtocal _opPrtl = new OPProtocal();
                                OPDevPtrl _opDevPrtl = _opPrtl.PrtcParsing(_socket, _byPacket, _byLen + 9);
                                if (!(_opDevPrtl.byCmd == 0x00 &&
                                    _opDevPrtl.byGid == 0x00 &&
                                    _opDevPrtl.byDid == 0x00))
                                {
                                    // 2020-08-12 큐를 사용해도 E/V 호출 Delay증상이 발생해서, 큐를 사용하지 않고 바로 
                                    // 이벤트 처리
                                    m_MyRecvDataProc(_opDevPrtl);
                                    /*
                                    // 인큐 로그 추가
                                    DoLogEnqueue(_opDevPrtl);

                                    this.OPRecvData.Enqueue(_opDevPrtl);
                                    */
                                }
                                
                            }
                        }
                    }
                    

                    if (_socket.Connected)
                    {
                        e.SetBuffer(_byBuffer, 0, 1024);
                        _socket.ReceiveAsync(e);
                    }
                }
            }
            catch (System.Exception ex)
            {
               Console.WriteLine("Recieve_Completed() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
               m_logCrt.SetLogFile("Crt", "Recieve_Completed() 메소드 예외: " + ex.Message.ToString());
            }
        }

        // 2020-05-06 추가
        public void DoLogEnqueue(OPDevPtrl a_opDevPtrl)
        {
            // 상태 체크 데이터는 로그로 남기지 않는다. 이유: 상태체크 데이터가 너무 많음. 
            if(a_opDevPtrl.byType == 0xC0 &&
                a_opDevPtrl.byCmd == 0x01 && 
                a_opDevPtrl.byOpt == 0x12){
                return;
            }
                

            string _strData = "";
            for (int i = 0; i < a_opDevPtrl.byLen; i++)
            {
                _strData = _strData + string.Format("{0} ", a_opDevPtrl.byData[i].ToString("X2"));

            }

            string _strLog = string.Format("Eequeue Data: Gid:{0}, Did:{1}, Type:{2}, Cmd:{3}, Opt:{4}, Len:{5}, Data:{6}",
                a_opDevPtrl.byGid.ToString("X2"), a_opDevPtrl.byDid.ToString("X2"), a_opDevPtrl.byType.ToString("X2"),
                a_opDevPtrl.byCmd.ToString("X2"), a_opDevPtrl.byOpt.ToString("X2"), a_opDevPtrl.byLen.ToString("X2"),
                _strData);

            //DB및 파일 저장
            m_logCrt.SetLogFile("Crt", _strLog);
        }

        // 2020-05-06 추가
        public void DoLogRecvData(byte[] a_byPacket)
        {
            // 상태 체크 데이터는 로그로 남기지 않는다. 이유: 상태체크 데이터가 너무 많음. 
            if ((a_byPacket[3] == 0xC0 || a_byPacket[3] == 0x06)&&
                a_byPacket[4] == 0x01 &&
                a_byPacket[5] == 0x12){
            
                return;
            }

            string _strData = "";
            for (int i = 0; i < a_byPacket.Length; i++)
            {
                _strData = _strData + string.Format("{0} ", a_byPacket[i].ToString("X2"));

            }

            string _strLog = string.Format("Recevice Data: {0}", _strData);

            //DB및 파일 저장
            m_logCrt.SetLogFile("Crt", _strLog);
        }

        // 2023-04-04 추가
        public void DoLogRecvData(string strPacket)
        {
            string _strLog = string.Format("Recevice Data: {0}", strPacket);

            //DB및 파일 저장
            m_logCrt.SetLogFile("Crt", _strLog);
        }

        // 2023-04-04 추가
        public bool WebConnection(string strIp, int nPort)
        {
            try
            {
                /*
                 private Socket m_ClientSocket = null;
                private int m_nClientPort = 0;
                private string m_strClientIp = "";
                 
                 */
                if (m_ClientSocket != null)
                    m_ClientSocket.Close();

                m_ClientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse(strIp), nPort);

                m_ClientSocket.Connect(_ipep);

                //소켓 커넥션이 완료되면 리시브 이벤트를 등록한다.
                if (Program.IsConnected(m_ClientSocket))
                {
                    SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
                    byte[] _byBuffer = new byte[1024];
                    _receiveArgs.SetBuffer(_byBuffer, 0, 1024);

                    _receiveArgs.UserToken = m_ClientSocket;
                    _receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recive_WebData);
                    m_ClientSocket.ReceiveAsync(_receiveArgs);
                }

                return Program.IsConnected(m_ClientSocket);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("WebConnection() 예외 발생: " + ex.Message.ToString());
                return false;
            }
        }

        private void Recive_WebData(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _socket = (Socket)sender;
                if (_socket.Connected && e.BytesTransferred > 0)
                {
                    byte[] _byBuffer = e.Buffer;

                    string _strData = "";

                    if (e.BytesTransferred > 0)
                    {
                        _strData = System.Text.Encoding.UTF8.GetString(_byBuffer);

                        DoLogRecvData(_strData);
                        m_WebRecvDataProc(_strData);
                    }

                    if (_socket.Connected)
                    {
                        e.SetBuffer(_byBuffer, 0, 1024);
                        _socket.ReceiveAsync(e);
                    }
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Recive_WebData() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
                m_logCrt.SetLogFile("Crt", "Recive_WebData() 메소드 예외: " + ex.Message.ToString());
            }
        }

        public bool IsWebConnected()
        {
            return Program.IsConnected(m_ClientSocket);
        }
    }
}
