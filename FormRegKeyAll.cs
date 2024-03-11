using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace SmartOnePass
{
    public partial class FormRegKeyAll : Form
    {
        private string[] m_Readlines;
        private bool m_bLoadFileEnable = false;
        private bool m_bSendPacketEnable = false;

        //연동 시 사용할 네트워크 멤버
        Socket m_hSocket = null;
        private string m_strGateWayIp = "";
        private int m_nGateWayPort = 0;

        //LogClass
        private Log m_logGateWay = new Log();
        
        // Log 기록 추가
        private Log m_logException = new Log("LogCrt");

        public FormRegKeyAll()
        {
            InitializeComponent();
        }

        private void FormRegKeyAll_Load(object sender, EventArgs e)
        {
            rb_ConOnce.Checked = true;
            SetListViewInit(lv_PacketList);
            bw_KeyControl.RunWorkerAsync();
            this.BringToFront();
        }

        private void btn_FileOpen_Click(object sender, EventArgs e)
        {
            LoadListFile();
        }

        private void btn_Reg_Click(object sender, EventArgs e)
        {
            if (m_bSendPacketEnable == false)
            {
                m_bSendPacketEnable = true;
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            bw_KeyControl.Dispose();
            this.Dispose();
        }

        // Crt 서버 접속
        public bool GWConnection()
        {
            try
            {
                if (m_hSocket != null)
                    m_hSocket.Close();

                m_hSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse(this.m_strGateWayIp), this.m_nGateWayPort);

                m_hSocket.Connect(_ipep);

                //소켓 커넥션이 완료되면 리시브 이벤트를 등록한다.
                if (m_hSocket.Connected == true)
                {
                    SocketAsyncEventArgs _receiveArgs = new SocketAsyncEventArgs();
                    byte[] _byBuffer = new byte[1024];
                    _receiveArgs.SetBuffer(_byBuffer, 0, 1024);

                    _receiveArgs.UserToken = m_hSocket;
                    _receiveArgs.Completed += new EventHandler<SocketAsyncEventArgs>(Recive_Completed);
                    m_hSocket.ReceiveAsync(_receiveArgs);
                }

                return m_hSocket.Connected;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtConnection() 예외 발생: " + ex.Message.ToString());

                m_logException.SetLogFile("예외 발생", "CrtConnection() 예외 발생: " + ex.Message.ToString());

                return false;
            }
        }

        public bool GWRegKey(string a_strPacket)
        {
            try
            {
                string strPacket = a_strPacket;
                Console.WriteLine("Send Packet = " + strPacket);

                if (m_hSocket.Connected)
                {
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(strPacket);
                    m_hSocket.Send(byData);
                    m_logGateWay.SetLogFile("Send Success", strPacket);
                }
                else
                {
                    m_logGateWay.SetLogFile("Send Fail[Socket Close]", strPacket);
                    return false;
                }

                return true;
            }
            catch (System.Exception ex)
            {
                m_logGateWay.SetLogFile("Send Fail[GWRegKey() 예외 발생]", ex.Message.ToString());
                Console.WriteLine("GWRegKey() 예외 발생:" + ex.Message.ToString());

                m_logException.SetLogFile("예외 발생", "GWRegKey() 예외 발생:" + ex.Message.ToString());

                return false;
            }
        }

        private void Recive_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                Socket _socket = (Socket)sender;
                if (_socket.Connected && e.BytesTransferred > 0)
                {
                    byte[] _byBuffer = e.Buffer;

                    string _strData = Encoding.Default.GetString(_byBuffer, 0, e.BytesTransferred);
                    Console.WriteLine("Recv Packet = " + _strData);
                    m_logGateWay.SetLogFile("Recv Success", _strData);

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

                m_logException.SetLogFile("예외 발생", "Recieve_Completed() 메소드 예외: " + ex.Message.ToString());
            }
        }

        private void bw_KeyControl_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (m_bLoadFileEnable == true)
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        btn_FileOpen.Enabled = false;
                        lv_PacketList.Items.Clear();
                        foreach (string _line in m_Readlines)
                        {
                            //Console.WriteLine("\t" + _line);
                            //등록
                            lv_PacketList.Items.Insert(lv_PacketList.Items.Count, new ListViewItem(new string[] { (lv_PacketList.Items.Count + 1).ToString(), _line ,""}));
                            Thread.Sleep(5);
                        }
                        btn_FileOpen.Enabled = true;
                    }));
                    m_bLoadFileEnable = false;
                }
                else
                {
                    if (m_bSendPacketEnable == true)
                    {
                        //전송작업 진행
                        //테스트 코드
                        this.Invoke(new MethodInvoker(delegate()
                        {
                            rb_ConOnce.Enabled = false;
                            rb_ConEveryTime.Enabled = false;
                            btn_Reg.Enabled = false;
                            if (txb_IPAddr.Text.Length == 0 || txbPort.Text.Length == 0)
                            {
                                MessageBox.Show("접속 정보를 확인해 주십시오.");
                                btn_Reg.Enabled = true;
                                rb_ConOnce.Enabled = true;
                                rb_ConEveryTime.Enabled = true;
                                m_bSendPacketEnable = false;
                                return;
                            }
                            else
                            {
                                m_strGateWayIp = txb_IPAddr.Text;
                                m_nGateWayPort = int.Parse(txbPort.Text);
                            }
                            if(rb_ConOnce.Checked == true)
                            {
                                GWConnection();
                            }

                            for (int i = 0; i < lv_PacketList.Items.Count; i++)
                            {
                                lv_PacketList.Items[i].SubItems[2].Text = "";
                            }
                        }));
                        for (int i = 0; i < lv_PacketList.Items.Count; i++)
                        {
                            if (rb_ConEveryTime.Checked == true)
                            {
                                GWConnection();
                            }
                            if (m_hSocket == null || Program.IsConnected(m_hSocket) == false)
                            {
                                m_logGateWay.SetLogFile("Send Fail", "소켓오류가 발생하였습니다.");
                                m_hSocket.Close();
                                MessageBox.Show("데이타 전송에 실패하였습니다.");
                                break;
                            }

                            this.Invoke(new MethodInvoker(delegate()
                            {
                                //Console.WriteLine(lv_PacketList.Items[i].SubItems[1].Text + lv_PacketList.Items[i].SubItems[2].Text);
                                GWRegKey(lv_PacketList.Items[i].SubItems[1].Text);
                                lv_PacketList.Items[i].SubItems[2].Text = "O";
                            }));
                            Thread.Sleep(500);
                            if (rb_ConEveryTime.Checked == true)
                            {
                                m_hSocket.Close();
                            }
                            Thread.Sleep(500);
                        }
                        if (m_hSocket != null)
                            m_hSocket.Close();

                        this.Invoke(new MethodInvoker(delegate()
                        {
                            rb_ConOnce.Enabled = true;
                            rb_ConEveryTime.Enabled = true;
                            btn_Reg.Enabled = true;
                        }));
                        m_bSendPacketEnable = false;
                    }
                }
                Thread.Sleep(10);
            }
        }

        private void SetListViewInit(ListView a_listView)
        {
            a_listView.FullRowSelect = true;
            a_listView.GridLines = true;
            a_listView.View = View.Details;

            a_listView.Columns.Add("Idx", 80, HorizontalAlignment.Center);
            a_listView.Columns.Add("등록 패킷", (a_listView.Width - 200 - SystemInformation.VerticalScrollBarWidth - 5), HorizontalAlignment.Center);
            a_listView.Columns.Add("등록 진행", 120, HorizontalAlignment.Center);
        }

        private void LoadListFile()
        {
            FileDialog fileDlg = new OpenFileDialog();
            fileDlg.InitialDirectory = Application.StartupPath;
            fileDlg.Filter = "모든 파일 (*.*)|*.*|리스트 파일|*.txt";
            fileDlg.RestoreDirectory = true;
            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                
                string strFileName = fileDlg.FileName;

                m_Readlines = System.IO.File.ReadAllLines(strFileName);
                if (m_Readlines.Length == 0)
                {
                    //읽은 데이타가 없으면
                    MessageBox.Show("데이타가 없습니다.");
                }
                else
                {
                    m_bLoadFileEnable = true;
                }
            }
        }
    }

}
