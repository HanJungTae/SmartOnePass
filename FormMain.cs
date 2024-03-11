using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using SmartOnePass.CRT;
using CST.Utill;
using System.Diagnostics;
using System.Text;
using CheckNetControl;

namespace SmartOnePass
{
    public struct stLobbyId
    {
        public string strUkrGid;
        public string strUkrDid;
        public string strLobbyName;
    }

    public struct stKeyDirection
    {
        public byte byUkrGid;
        public byte byUkrDid;
        public byte byElGid;
        public byte byElDid;
        public bool bUkrConfirm;
        public bool bElConfirm;
        public string strKeyId;
    }

    public struct stCrtCall
    {
        public byte byUkrGid;
        public byte byUkrDid;
        public byte byElGid;
        public byte byElDid;
        public string strCarId;
        public int nTakeOn;         // 탑승자가 탑승하는 Index
        public string strTakeOn;    // 탑승자가 탑승하는 층: ex) B3 or B1
        public int[] nArTakeOff;
        public int nTakeOff;        // 탑승자의 목적층 Index
        public string strTakeOff;   // 탑승자의 목적층: ex) 9, 11
        public int nDelayTime;
        public int nKeyCnt;
        public string strKeyId;
        public string[] strArKeyId;
        public bool bUkrConfirmFlag;    // UKR 인증 Flag
        public bool bElConfirmFlag;     // 엘리베이터 리더기 인증 Flag (두개의 Flag를 이용하여 방향성 체크)
        // bUkrConfirmFlag가 셋팅이 되고, bElConfirmFlag가 셋팅되어야 방향성이 맞게(외부에서 내부) 되었음.
        // bElConfirmFlag가 셋팅이 되고, bUkrConfirmFlag가 셋팅이 되면 방향성이 안맞음(내부에서 외부)
        public bool bElCallFlag;    // 엘리베니터 호출 Flag => True가 될때 엘리베이터 호출을 함.
        public string strLobbyName; // 로그에 남기기위해 로비 이름을 기록함.
    }

    public struct stCrtCallInfo
    {
        public string strLBName;
        public byte byUkrGid;
        public byte byUkrDid;
        public byte[] byElGid;
        public byte byElDid;
        public string strCrtId;
        public int nDelayTime;
        public string strTakeOn;
        public string strCarConfig;
        public bool bElUse; // 엘리베이터 리더기 사용여부(방향성 체크 여부) True ==> 엘리베이터 리더기를 이용하여 방샹성을 체크
        //                                              False ==> 엘리베이터 리더기를 사용하지 않고, 로비에서 엘리베이터 바로 호출
        public List<string> KeyId;      // 인증된 키정보
    }

    public struct stStateInfo
    {
        public byte byGid;
        public byte byDid;
        public int nStateCnt;
    }

    public struct stKeyInfo
    {
        public string strDong;
        public string strHo;
        public string strKeySn;
        public string strKeyId;
    }

    // 2021-04-22 추가
    public struct stKmsDBKeyId
    {
        public string strKeyId;
        public bool nCheckFlag;
    }

    // 2021-04-22 추가
    public struct stKmsDBKeyIdInfo
    {
        public string strGid;
        public string strDid;
        public string strLobbyName;
        public List<stKmsDBKeyId> listKmsDBKeyId;
    }

    // 2021-04-22 추가
    public struct stUksKeyCount
    {
        public byte byGid;
        public byte byDid;
        public string strLobbyName;
        public int nRegIdCnt;
        public int nCurIdCnt;
        public int nRetryCntKeyCnt; // 키 개수 조회 Retry 회수 조절
        public int nRetryCntKeyList;    // 키 리스트 조회 Retry 회수 조절
    }

    // 2021-04-22 추가 ==> 공동현관에 등록된 Key Id와 비교하여, 공동현관에 KeyId 키 ID를 등록할 때만 사용
    public struct stRegisterKeyId
    {
        public byte byGid;
        public byte byDid;
        public List<string> listKeyId;
    }

    // 2021-08-23 추가 ==> Thread를 중복 생성하지 않고, 관리하기 위한 구조체
    //public struct stThreadInfo
    //{
    //    public byte byGid;
    //    public byte byDid;
    //    public Thread hUkrSendThread;
    //    public Thread hUkrStateThread;
    //    public Thread hUkrKeyCountRequestThread;
    //    public Thread hUkrKeyListRequestThread;
    //}

    public partial class FormMain : Form
    {
        /////////////////////////////////////////////////////////////////////////////
        
        private string m_strDBIp = "";
        private string m_strDBName = "";
        private string m_strDBId = "";
        private string m_strDBPw = "";

        private string m_strCrtIp = "";
        private int m_nCrtPort = 0;

        private int m_nCrtSubUse = 0;
        private string m_strCrtSubIp = "";
        private int m_nCrtSubPort = 0;
        private int m_nCrtSubCarStart = 0;

        // 2023-12-02 ==> 개포 퍼스티어 아이파크에서 Crt 서버가 4대로 인해 추가 하였음.
        private int m_nCrtThirdUse = 0;
        private string m_strCrtThirdIp = "";
        private int m_nCrtThirdPort = 0;
        private int m_nCrtThirdCarStart = 0;
        private int m_nCrtSubCarCount = 0;

        private int m_nCrtForthUse = 0;
        private string m_strCrtForthIp = "";
        private int m_nCrtForthPort = 0;
        private int m_nCrtForthCarStart = 0;
        private int m_nCrtThirdCarCount = 0;

        private int m_nCarCount = 0;

        private int m_nDifSvrUse = 0; //종합수신제어기 서버를 사용하는지

        private string m_strDirectionFlag = "";  // 방향성 설정
        // Value= 0이면 방향성이 없음, 
        // Value= 1이면 방향성이 있음(즉엘리베이터 앞에 리더기가 있음), 
        private string m_strTakeOffFlag = "";    // 목적층 설정
        // Value가 0이면 목적층이 없음, 1이면 목적층이 있음   

        private UC_Map[] m_ucMap;

        private List<string> m_listDong = new List<string>();

        private Networkcs m_server = null;

        // 웹서버 연동
        private Networkcs m_webClient = new Networkcs();

        private List<DevInfo> m_listUkrInfo = new List<DevInfo>();
        private List<stStateInfo> m_listStateInfo = new List<stStateInfo>();
        private byte m_bySeq = 0x00;

        ///////////////////////////////////////////////////////////////
        // 엘리베이터 Call 멤버 변수
        private List<stCrtCallInfo> m_listCrtCallInfo = new List<stCrtCallInfo>();
        private List<stCrtCall> m_listCrtCall = new List<stCrtCall>();
        private List<stKeyDirection> m_listKeyDirection = new List<stKeyDirection>();
        private CRTBase m_hCrt;
        private CRTBase m_hCrtSub;
        private CRTBase m_hCrtThird;
        private CRTBase m_hCrtForth;

        //private bool m_useTakeoff;
        //private Log m_logCrt = new Log("LogCrt");
        //private Log m_logMainList = new Log("Log");
        ///////////////////////////////////////////////////////////////

        ///////////////////////////////////////////////////////////////
        // 듀얼 아이 카드 리더기 연동
        private DualiControl m_dualI = new DualiControl();

        ///////////////////////////////////////////////////////////////
        // Mysql 연동
        private MySqlDB m_mysql = new MySqlDB();
        private MySqlDB m_mysql2 = new MySqlDB();   //==>  개포 퍼스티어 현장에서 DB Select에 과부하로 인해, 등록키 정보가 있는지에 대해, 조사할 때는 Query문 사용 객체를 따로 분리함.

        // 리셋 타이머 ==> 2023-12-26 수정
        private System.Windows.Forms.Timer m_ukrResetTimer = new System.Windows.Forms.Timer();
        TimerUkrReset m_ukrResetInfo = new TimerUkrReset();

        // 2021-07-02 동버튼 생성
        public Button[] mBtnDong;

        // 윈도우 메시지 관련 변수
        public const int WM_REQALIVE = 0x0400 + 5000;
        public const int WM_RESSERVER = 0x0400 + 5001;
        public const int WM_RESCLIENT = 0x0400 + 5002;

        // 등록기
        private DevInfo m_BleRegister;
        // private int _appType;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(int hWnd, uint Msg, uint wParam, uint lParam);

        // 2023-04-04 웹서버 연동변수 추가
        private string m_strWebIp = "";
        private int m_nWebPort = 0;

        // Log List를 Que 활용
        private List<string> m_myLogListQue = new List<string>();

        private List<UkrState> m_ukrState = new List<UkrState>();
        private List<UkrRegDelCheck> m_ukrRegDelCheck = new List<UkrRegDelCheck>();

        private void SendMsgAPI(string a_strProcess, uint a_nMsg)
        {
            int hWnd = 0;
            hWnd = (int)FindWindow(null, a_strProcess);

            if (hWnd > 0)
            {
                SendMessage(hWnd, a_nMsg, 0, 0);
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_REQALIVE)
            {
                SendMsgAPI("ONEPASS MANAGER", WM_RESCLIENT);
            }
        }

        // 멤버 변수 속성 처리
        public String DBIp
        {
            get { return m_strDBIp; }
            set { m_strDBIp = value; }
        }

        public String DBName
        {
            get { return m_strDBName; }
            set { m_strDBName = value; }
        }

        public String DBId
        {
            get { return m_strDBId; }
            set { m_strDBId = value; }
        }

        public String DBPw
        {
            get { return m_strDBPw; }
            set { m_strDBPw = value; }
        }
        ///////////////////////////////////////////////////////////////

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // 파일에서 DB 정보 읽기
            LoadConfig();

            this.DBName = "kms";
            this.DBId = "upisadm";
            this.DBPw = "upis";

            ///////////////////////////////////////////////////////////////
            // 화면 해상도에 맞게 변경
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = new Point(0, 0);

            m_mysql.MySqlCon(this.DBIp, this.DBName, this.DBId, this.DBPw);
            m_mysql2.MySqlCon(this.DBIp, this.DBName, this.DBId, this.DBPw);
            Program.m_mysql = m_mysql;

            // LogList를 관리하는 Thread 생성
            Thread logListThread = new Thread(MyLogListQueThread);
            logListThread.Start();

            // CRT 접속
            CrtConnection();

            //////////////////////////////////////////////////////////
            // 기본 Table 생성
            TblCreate();

            //////////////////////////////////////////////////////////
            // Table 초기화
            TblInit();

            // 2021-07-02 UI 변경
            // LobbyInfo 
            CreateUCListLobbyInfo();
            CreateUCListEvent();
            CreateUCListMaster();
            CreateUCListReg();
            CreateUCListDel();
            ////////////////////////////////////////////////////////
            // 동 정보 읽기를 읽어 버튼 생성
            LoadDongInfo();
            CreateButtonView();
            CreateLobbyInfo();
            

            /////////////////////////////////////////////////////////
            // 로비 이름과 ID 읽기
            LoadLobbyId();
            ///////////////////////////////////////////////////////

            ////////////////////////////////////////////////////////
            //Delegate 함수 설정(엘레베이터 호출 테스트를 하기 위함)
            Program.g_fnKeyConfirm += KeyConfirm;

            // 등록기
            m_BleRegister = null;

            //  Key 등록 정보 Header 정보 Update
            lv_key_info.OwnerDraw = true;

            // 2021-08-23 DB 로그 정리
            Thread hThreadLogClear = new Thread(new ParameterizedThreadStart(LogClearThread));
            hThreadLogClear.Start();

            // 2022-04-21 NFC 기능추가에 따른 UI 변경 추가( 2023=> 4월에 코드를 확인 하니, NFC 기능이 추가 되지 않았음)
            if(Program.g_nPhoneNfcUse == 1)
            {
                // 컨트롤러 보이게하기와 텍스트 변경
                btn_keyid_load.Text = "NFC 읽기";   // TAG 읽기 버튼의 Text 변경
                nfc_lbl_info.Visible = true;
                nfc_lbl_id.Visible = true;
                txt_reg_nfcid.Visible = true;
                
                // Key_Info_Master의 ListView Header변경
                ListHeadChange();

                // Key_Info_Master의 테이블의 Column 변경
                //string strQry = "show Columns from kms.Key_Info_Master Like 'Nfc_Id';";
                
            }

            // 2023-04-04 원격에서 App=>Web서버=>SKS 서버로 문열림이 가능하게 기능 추가
            if (Program.g_nRemoteUKROpen == 1)
            {
                string strLog = "";
                if (WebConnection(m_strWebIp, m_nWebPort))
                {
                    m_webClient.m_WebRecvDataProc = RecvWebData;   // 2023-04-04 웹서버 연동 데이터 수신
                    strLog = string.Format("웹 서버 접속 성공 / {0} : {1}", m_strWebIp, m_nWebPort);
                }
                else
                {
                    strLog = string.Format("웹 서버 접속 실패 / {0} : {1}", m_strWebIp, m_nWebPort);
                }

                this.LogPrint(strLog);
                Program.doDBLogKmsWeb("", strLog, "");
            }

            if (Program.g_nHanInParking == 1)
            {
                혜인에스앤에스Key정보동기화ToolStripMenuItem.Visible = true;
            }

            // 개포 퍼스티어에 전기차 충전 등록
            if (Program.g_nChargerUse == 1)
            {
                cb_charger_normal.Visible = true;
                cb_charger_portable.Visible = true;
                //lblCarNum.Visible = true;
                //txtCarNum.Visible = true;

                // key_Info_Master에 차량정보를 저장할 수 있는 Column(CarNum) 존재 확인
                string strQry = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'kms'  AND TABLE_NAME = 'Key_Info_Master'  AND COLUMN_NAME = 'CarNum';";
                if(!m_mysql.IsData(strQry))  // CarNum Column이 존재하지 않으면, CarNum 추가
                {
                    strQry = "ALTER TABLE Key_Info_Master ADD COLUMN CarNum VARCHAR(16);";
                    m_mysql.MySqlExec(strQry);
                }
            }
            else
            {
                cb_charger_normal.Checked = false;
                cb_charger_portable.Checked = false;
            }

            ////////////////////////////////////////////////////////
            // 서버 Start(모든 DB의 데이터가 Loading이 끝이 나고 실행하게 바꿈)
            m_server = new Networkcs();
            m_server.Port = 31100;
            m_server.m_MyRecvDataProc = RecvDataProc;   // 2020-08-13 수정 메시지 큐를 사용하지 않고, 델리게이트를 이용하여 메소드 호출
            m_server.ServerStart();

            // 2020-05-06 Queue를 Event로 처리하던 것을 Thread로 수정
            Thread hThreadDataRecv = new Thread(new ParameterizedThreadStart(DevDataRecvThread));
            hThreadDataRecv.Start();

            // 상태체크 Thread
            Thread hThreadUksState = new Thread(new ParameterizedThreadStart(UkrStateDbExec));
            hThreadUksState.Start();

            // Reg와 Del 테이블에 데이터가 있는지를 체크하는 Thread
            Thread hThreadUksDelRegCheck = new Thread(new ParameterizedThreadStart(UkrDelRegCheckThread));
            hThreadUksDelRegCheck.Start();

            // 리셋 타이머
            m_ukrResetTimer.Interval = 1000;    // 1초마다 타이머 이벤트 발생
            m_ukrResetTimer.Tick += Timer_UkrReset;
            RESET_DATA resetData = JsonSerializer.Instance.LoadAndDeserialize<RESET_DATA>(Application.StartupPath, "Timer");
            m_ukrResetInfo.m_bFlag = resetData.UseTimer;
            m_ukrResetInfo.m_strHour = resetData.AM_Hour.ToString();
            m_ukrResetInfo.m_strMin = resetData.AM_Minute.ToString();
            m_ukrResetTimer.Start();


            //2023-12-22 추가
            // 콜백 함수 등록
            Program.mMainCb.SetCallBackUkrKeyIdClear(UkrKeyIdClear);   // 동기화 Form에서 KeyID 전체 전송시 Callback
            Program.mMainCb.SetCallBackUkrReset(UkrReset);
            Program.mMainCb.SetCallBackTimerUkrReset(UkrTimerReset);
            
        }

        private void ListHeadChange()
        {
            lv_key_info.Columns[0].Width = 10;
            lv_key_info.Columns[0].Text = "";
            lv_key_info.Columns[1].Width = 65;
            lv_key_info.Columns[1].Text = "동";
            lv_key_info.Columns[2].Width = 65;
            lv_key_info.Columns[2].Text = "호";
            lv_key_info.Columns[3].Width = 100;
            lv_key_info.Columns[3].Text = "Key Sn";
            lv_key_info.Columns[4].Width = 135;
            lv_key_info.Columns[4].Text = "Key ID";

            if (lv_key_info.Columns.Count == 5) // NFC ID Header가 추가 되어 있지 않으면
            {
                ColumnHeader header = new ColumnHeader();
                header.Width = 135;
                header.Text = "NFC ID";
                lv_key_info.Columns.Add(header);
            }
        }

        private void LogClearThread(object o)
        {
            int nLoop = 10000;
            int nCompareCnt = 10000;

            while (Program.g_bDataRecv)
            {
                if (++nLoop > (60 * 60))    // 1시간 마다 체크(
                //if (++nLoop > (60 * 60 * 24))    // 24시간 마다 체크(
                {
                    int nCnt = GetLogCnt(Program.TBL_NAME_LOG_KMSDEV);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_KMSDEV);
                    }

                    nCnt = GetLogCnt(Program.TBL_NAME_LOG_DEVSTATE);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_DEVSTATE);
                    }

                    nCnt = GetLogCnt(Program.TBL_NAME_LOG_KMSKEYREGDEL);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_KMSKEYREGDEL);
                    }
                    nCnt = GetLogCnt(Program.TBL_NAME_LOG_KMSHOMENET);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_KMSHOMENET);
                    }
                    nCnt = GetLogCnt(Program.TBL_NAME_LOG_KMSCRT);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_KMSCRT);
                    }

                    nCnt = GetLogCnt(Program.TBL_NAME_LOG_KMSWEB);
                    if (nCnt > nCompareCnt)
                    {
                        doLogClear(Program.TBL_NAME_LOG_KMSWEB);
                    }

                    nLoop = 0;
                }

                Thread.Sleep(1000);
            }
        }

        private int GetLogCnt(string a_strTblName)
        {
            string _strQry = string.Format("SELECT count(*) FROM {0};", a_strTblName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
            foreach (string[] _str in _qryList)
            {
                return int.Parse(_str[0]);
            }

            return 0;
        }

        private void doLogClear(string a_strTblName)
        {
            string strQry = "";
            if (Program.IsTable("_" + a_strTblName))   // 테이블이 있으면
            {
                strQry = string.Format("Drop Table _{0};", a_strTblName);
                m_mysql.MySqlExec(strQry);
            }

            strQry = string.Format("ALTER TABLE {0} RENAME _{0};", a_strTblName);
            m_mysql.MySqlExec(strQry);

            strQry = string.Format("CREATE TABLE IF NOT EXISTS `{0}` LIKE `_{0}`;", a_strTblName);
            m_mysql.MySqlExec(strQry);
        }

        private void CreateUCListLobbyInfo()
        {

            ColumnHeader[] uclvcolumnHeaders1 = new ColumnHeader[3];
            this.uclvcolumnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader1.Text = "";
            this.uclvcolumnHeader1.Width = 2;

            this.uclvcolumnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader2.Text = "";
            this.uclvcolumnHeader2.Width = 95;

            this.uclvcolumnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader3.Text = "정보";
            this.uclvcolumnHeader3.Width = 170;

            uclvcolumnHeaders1[0] = uclvcolumnHeader1;
            uclvcolumnHeaders1[1] = uclvcolumnHeader2;
            uclvcolumnHeaders1[2] = uclvcolumnHeader3;

            uc_lv_lb_UkrInfo.SetTitle("로비 정보");
            uc_lv_lb_UkrInfo.SetColumns(uclvcolumnHeaders1);
            uc_lv_lb_UkrInfo.SetHeaderBackColor(Color.FromArgb(239, 249, 254));

            uc_lv_lb_UkrInfo.Font = new Font("돋움", 9, FontStyle.Regular);
            
        }

        private void CreateUCListEvent()
        {
            ColumnHeader[] uclvcolumnHeaders1 = new ColumnHeader[3];
            this.uclvcolumnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader4.Text = "";
            this.uclvcolumnHeader4.Width = 5;

            this.uclvcolumnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader5.Text = "시간";
            this.uclvcolumnHeader5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader5.Width = 150;

            this.uclvcolumnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader6.Text = "이벤트";
            this.uclvcolumnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.uclvcolumnHeader6.Width = 630;

            uclvcolumnHeaders1[0] = uclvcolumnHeader4;
            uclvcolumnHeaders1[1] = uclvcolumnHeader5;
            uclvcolumnHeaders1[2] = uclvcolumnHeader6;

            uc_lv_evt_log.SetTitle("이벤트");
            uc_lv_evt_log.SetColumns(uclvcolumnHeaders1);
            uc_lv_evt_log.SetBackground(Properties.Resources.List_Bg);
            uc_lv_evt_log.SetListDock();
            uc_lv_evt_log.SetHeaderBackColor(Color.FromArgb(239, 249, 254));

        }

        private void CreateUCListMaster()
        {
            ColumnHeader[] uclvcolumnHeaders1 = new ColumnHeader[5];
            this.uclvcolumnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader7.Text = "";
            this.uclvcolumnHeader7.Width = 5;

            this.uclvcolumnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader8.Text = "동";
            this.uclvcolumnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader8.Width = 80;

            this.uclvcolumnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader9.Text = "호";
            this.uclvcolumnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader9.Width = 80;

            this.uclvcolumnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader10.Text = "KeySn";
            this.uclvcolumnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader10.Width = 110;

            this.uclvcolumnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader11.Text = "Key ID";
            this.uclvcolumnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader11.Width = 240;
            
            uclvcolumnHeaders1[0] = uclvcolumnHeader7;
            uclvcolumnHeaders1[1] = uclvcolumnHeader8;
            uclvcolumnHeaders1[2] = uclvcolumnHeader9;
            uclvcolumnHeaders1[3] = uclvcolumnHeader10;
            uclvcolumnHeaders1[4] = uclvcolumnHeader11;

            uc_lv_lb_master.SetTitle("등록 키 정보(Master Table)", 10.0F, System.Drawing.FontStyle.Bold);
            uc_lv_lb_master.SetColumns(uclvcolumnHeaders1);
            uc_lv_lb_master.SetBackground(Properties.Resources.OUC_List_Bg3);
            uc_lv_lb_master.SetHeaderBackColor(Color.FromArgb(239, 249, 254));
        }

        private void CreateUCListReg()
        {
            ColumnHeader[] uclvcolumnHeaders1 = new ColumnHeader[5];
            this.uclvcolumnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader12.Text = "";
            this.uclvcolumnHeader12.Width = 5;

            this.uclvcolumnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader13.Text = "동";
            this.uclvcolumnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader13.Width = 80;

            this.uclvcolumnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader14.Text = "호";
            this.uclvcolumnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader14.Width = 80;

            this.uclvcolumnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader15.Text = "KeySn";
            this.uclvcolumnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader15.Width = 110;

            this.uclvcolumnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader16.Text = "Key ID";
            this.uclvcolumnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader16.Width = 240;

            uclvcolumnHeaders1[0] = uclvcolumnHeader12;
            uclvcolumnHeaders1[1] = uclvcolumnHeader13;
            uclvcolumnHeaders1[2] = uclvcolumnHeader14;
            uclvcolumnHeaders1[3] = uclvcolumnHeader15;
            uclvcolumnHeaders1[4] = uclvcolumnHeader16;

            uc_lv_lb_reg.SetTitle("키 등록 테이블 정보(Reg Table)", 10.0F, System.Drawing.FontStyle.Bold);
            uc_lv_lb_reg.SetColumns(uclvcolumnHeaders1);
            uc_lv_lb_reg.SetBackground(Properties.Resources.OUC_List_Bg3);
            uc_lv_lb_reg.SetHeaderBackColor(Color.FromArgb(239, 249, 254));
            
        }

        private void CreateUCListDel()
        {
            ColumnHeader[] uclvcolumnHeaders1 = new ColumnHeader[5];
            this.uclvcolumnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader17.Text = "";
            this.uclvcolumnHeader17.Width = 5;

            this.uclvcolumnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader18.Text = "동";
            this.uclvcolumnHeader18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader18.Width = 80;

            this.uclvcolumnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader19.Text = "호";
            this.uclvcolumnHeader19.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader19.Width = 80;

            this.uclvcolumnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader20.Text = "KeySn";
            this.uclvcolumnHeader20.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader20.Width = 110;

            this.uclvcolumnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.uclvcolumnHeader21.Text = "Key ID";
            this.uclvcolumnHeader21.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uclvcolumnHeader21.Width = 240;

            uclvcolumnHeaders1[0] = uclvcolumnHeader17;
            uclvcolumnHeaders1[1] = uclvcolumnHeader18;
            uclvcolumnHeaders1[2] = uclvcolumnHeader19;
            uclvcolumnHeaders1[3] = uclvcolumnHeader20;
            uclvcolumnHeaders1[4] = uclvcolumnHeader21;

            uc_lv_lb_del.SetTitle("키 삭제 테이블 정보(Del Table)", 10.0F, System.Drawing.FontStyle.Bold);
            uc_lv_lb_del.SetColumns(uclvcolumnHeaders1);
            uc_lv_lb_del.SetBackground(Properties.Resources.OUC_List_Bg3);
            uc_lv_lb_del.SetHeaderBackColor(Color.FromArgb(239, 249, 254));
        }

        // 웹서버에 접속
        public bool WebConnection(string strWebIp, int nWebPort)
        {
            return m_webClient.WebConnection(strWebIp, nWebPort);
        }

        public void CrtConnection()
        {
            // E/V 정보 읽기
            LoadCrtInfo();

            //////////////////////////////////////////////////////
            // Crt 서버 접속
            switch (Program.g_CrtType.ToLower())
            {
                case "ots":
                    {
                        m_hCrt = new Crt_OTS();

                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_OTS();
                        break;
                    }
                case "hyundai":
                    {
                        m_hCrt = new Crt_Hyundai();

                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_Hyundai();

                        if (m_nCrtThirdUse == 1)
                        {
                            m_hCrtThird = new Crt_Hyundai();
                        }

                        if(m_nCrtForthUse == 1)
                        {
                            m_hCrtForth = new Crt_Hyundai();
                        }

                        break;
                    }
                case "hyundai_high":    // 현대 엘리베이터 고층
                    {
                        m_hCrt = new Crt_Hyundai_High();
                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_Hyundai_High();

                        if (m_nCrtThirdUse == 1)
                        {
                            m_hCrtThird = new Crt_Hyundai_High();
                            m_hCrtForth = new Crt_Hyundai_High();
                        }
                    }
                    break;
                case "thyssen":
                    {
                        m_hCrt = new Crt_ThyssenKrupp();
                        
                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_ThyssenKrupp();

                        // 상태 체크 하기 위한 설정
                        m_hCrt.SetCarCount(m_nCarCount, m_strTakeOffFlag);
                        break;
                    }
                case "power":
                    {
                        m_hCrt = new Crt_Power();

                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_Power();
                        break;
                    }
                case "han":
                    {
                        m_hCrt = new Crt_Mitsubishi();

                        if (m_nCrtSubUse == 1) // CRT 서버가 두 대 인경우
                            m_hCrtSub = new Crt_Mitsubishi();
                        break;
                    }
            }

            m_hCrt.SetLogCallback(LogSave);

            if (m_hCrt.CrtConnection(m_strCrtIp, m_nCrtPort))
            {
                this.LogPrint(string.Format("{0} 엘리베이터 서버 접속 성공 / {1} : {2}", Program.g_CrtType, m_strCrtIp, m_nCrtPort));

                // DB에 Crt Log 저장
                string strCommnet = string.Format("{0} Crt 서버 접속 성공 / {1} : {2}", Program.g_CrtType, m_strCrtIp, m_nCrtPort);
                Program.doDBLogKmsCrt("MainCrt", "MainCrt", "성공", strCommnet, "");
            }
            else
            {
                this.LogPrint(string.Format("{0} 엘리베이터 서버 접속 실패 / {1} : {2} ", Program.g_CrtType, m_strCrtIp, m_nCrtPort));

                // DB에 Crt Log 저장
                string strCommnet = string.Format("{0} Crt 서버 접속 실패 / {1} : {2} ", Program.g_CrtType, m_strCrtIp, m_nCrtPort);
                Program.doDBLogKmsCrt("MainCrt", "MainCrt", "실패", strCommnet, "");
            }

            if (m_nCrtSubUse == 1)
            {
                m_hCrtSub.SetLogCallback(LogSave);

                if (m_hCrtSub.CrtConnection(m_strCrtSubIp, m_nCrtSubPort))
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 서브 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtSubIp, m_nCrtSubPort, m_nCrtSubCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt 서브 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtSubIp, m_nCrtSubPort, m_nCrtSubCarStart);
                    Program.doDBLogKmsCrt("SubCrt", "SubCrt", "성공", strCommnet, "");
                }
                else
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 서브 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtSubIp, m_nCrtSubPort, m_nCrtSubCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt 서브 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtSubIp, m_nCrtSubPort, m_nCrtSubCarStart);
                    Program.doDBLogKmsCrt("SubCrt", "SubCrt", "실패", strCommnet, "");
                }
            }

            if (m_nCrtThirdUse == 1)
            {
                m_hCrtThird.SetLogCallback(LogSave);

                if (m_hCrtThird.CrtConnection(m_strCrtThirdIp, m_nCrtThirdPort))
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 Third 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtThirdIp, m_nCrtThirdPort, m_nCrtThirdCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt Third 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtThirdIp, m_nCrtThirdPort, m_nCrtThirdCarStart);
                    Program.doDBLogKmsCrt("ThirdCrt", "ThirdCrt", "성공", strCommnet, "");
                }
                else
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 Third 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtThirdIp, m_nCrtThirdPort, m_nCrtThirdCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt Third 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtThirdIp, m_nCrtThirdPort, m_nCrtThirdCarStart);
                    Program.doDBLogKmsCrt("ThirdCrt", "ThirdCrt", "실패", strCommnet, "");
                }
            }

            if (m_nCrtForthUse == 1)
            {
                m_hCrtForth.SetLogCallback(LogSave);

                if (m_hCrtForth.CrtConnection(m_strCrtForthIp, m_nCrtForthPort))
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 Forth 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtForthIp, m_nCrtForthPort, m_nCrtForthCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt Forth 서버 접속 성공 / {1} : {2} / CAR START : {3}", Program.g_CrtType, m_strCrtForthIp, m_nCrtForthPort, m_nCrtForthCarStart);
                    Program.doDBLogKmsCrt("ForthCrt", "ForthCrt", "성공", strCommnet, "");
                }
                else
                {
                    this.LogPrint(string.Format("{0} 엘리베이터 Forth 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtForthIp, m_nCrtForthPort, m_nCrtForthCarStart));

                    // DB에 Crt Log 저장
                    string strCommnet = string.Format("{0} Crt Forth 서버 접속 실패 / {1} : {2}  / CAR START : {3}", Program.g_CrtType, m_strCrtForthIp, m_nCrtForthPort, m_nCrtForthCarStart);
                    Program.doDBLogKmsCrt("ForthCrt", "ForthCrt", "실패", strCommnet, "");
                }
            }

            LogPrint("엘리베이터 재 접속 확인 간격 : 10분");
        }
        private void CrtFirstReconnection()
        {
            string strCommnet = "";
            if (m_hCrt.IsCrtConnected() == true) // 현재 엘리베이터 서버에 접속해 있으면 재접속을 하지 않음.
            {
                return;
            }

            if (m_hCrt.CrtConnection() == true)
            {
                LogSave(string.Format("{0} 엘리베이터 서버 재 접속 성공", Program.g_CrtType));
                LogPrint(string.Format("{0} 엘리베이터 서버 재 접속 성공", Program.g_CrtType));

                // DB에 Crt Log 저장
                strCommnet = string.Format("{0} 엘리베이터 서버 재 접속 성공", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "성공", strCommnet, "");
            }
            else
            {
                LogPrint(string.Format("{0} 엘리베이터 서버 재 접속 실패", Program.g_CrtType));

                // DB에 Crt Log 저장
                strCommnet = string.Format("{0} 엘리베이터 서버 재 접속 실패", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "실패", strCommnet, "");
            }
        }

        private void CrtSubReconnection()
        {
            string strCommnet = ""; // SubCrt가 설정 되지 않으면, 재접속을 하지 않음.
            if (m_nCrtSubUse != 1)
                return;

            
            if (m_hCrtSub.IsCrtConnected() == true) // 현재 엘리베이터 서버에 접속해 있으면 재접속을 하지 않음.
            {
                return;
            }

            if (m_hCrtSub.CrtConnection() != true)
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Sub) 재 접속 실패", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Sub) 재 접속 실패", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "실패", strCommnet, "");
            }
            else
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Sub) 재 접속 성공", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Sub) 재 접속 성공", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "성공", strCommnet, "");

            }
        }

        private void CrtThirdReconnection()
        {
            string strCommnet = ""; // ThirdCrt가 설정 되지 않으면, 재접속을 하지 않음.
            if (m_nCrtThirdUse != 1)
                return;


            if (m_hCrtThird.IsCrtConnected() == true) // 현재 엘리베이터 서버에 접속해 있으면 재접속을 하지 않음.
            {
                return;
            }

            if (m_hCrtThird.CrtConnection() != true)
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Third) 재 접속 실패", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Third) 재 접속 실패", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "실패", strCommnet, "");
            }
            else
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Third) 재 접속 성공", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Third) 재 접속 성공", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "성공", strCommnet, "");

            }
        }

        private void CrtForthReconnection()
        {
            string strCommnet = ""; // ForthCrt가 설정 되지 않으면, 재접속을 하지 않음.
            if (m_nCrtForthUse != 1)
                return;


            if (m_hCrtForth.IsCrtConnected() == true) // 현재 엘리베이터 서버에 접속해 있으면 재접속을 하지 않음.
            {
                return;
            }

            if (m_hCrtForth.CrtConnection() != true)
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Forth) 재 접속 실패", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Forth) 재 접속 실패", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "실패", strCommnet, "");
            }
            else
            {
                LogPrint(string.Format("{0} 엘리베이터 서버(Forth) 재 접속 성공", Program.g_CrtType));

                strCommnet = string.Format("{0} 엘리베이터 서버(Forth) 재 접속 성공", Program.g_CrtType);
                Program.doDBLogKmsCrt("Crt", "Crt", "성공", strCommnet, "");

            }
        }

        public void CrtReconnection()
        {
            CrtFirstReconnection();
            CrtSubReconnection();
            CrtThirdReconnection();
            CrtForthReconnection();
        }

        // 2023-04-04 Web 서버 재접속
        public void WebReconnection()
        {
            string strCommnet = "";
            if (m_webClient.IsWebConnected() == true) // 현재 웹서버에 접속해 있으면, 재접속을 하지 않음
            {
                return;
            }

            if (WebConnection(m_strWebIp, m_nWebPort) == true)
            {
                LogSave(string.Format("웹 서버 재 접속 성공"));
                LogPrint(string.Format("웹 서버 재 접속 성공"));

                // DB에 Web Log 저장
                strCommnet = string.Format("서버 재 접속 성공");
                Program.doDBLogKmsWeb("", strCommnet, "");
            }
            else
            {
                LogPrint(string.Format("웹 서버 재 접속 실패"));

                // DB에 Crt Log 저장
                strCommnet = string.Format("웹 서버 재 접속 실패");
                Program.doDBLogKmsWeb("", strCommnet, "");
            }
        }


        // 기본 테이블 생성
        private void TblCreate()
        {
            if (!Program.IsTable(Program.TBL_NAME_CARNUM))
            {
                Program.CreateTable(Program.TBL_CREATE_CARNUM);
            }

            if (!Program.IsTable(Program.TBL_NAME_DONG_LOBBY))
            {
                Program.CreateTable(Program.TBL_CREATE_DONG_LOBBY);
            }

            if (!Program.IsTable(Program.TBL_NAME_KEY_INFO_MASTER))
            {
                Program.CreateTable(Program.TBL_CREATE_KEY_INFO_MASTER);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_CRT_INFO))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_CRT_INFO);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_ID))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_ID);
            }

            if (!Program.IsTable(Program.TBL_NAME_UKR_STATE_INFO))
            {
                Program.CreateTable(Program.TBL_CREATE_UKR_STATE_INFO);
            }

            if (!Program.IsTable(Program.TBL_NAME_UKR_CONNECT_INFO))
            {
                Program.CreateTable(Program.TBL_CREATE_UKR_CONNECT_INFO);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_INFO))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_INFO);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_RESET))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_RESET);

                string _strQry = string.Format("SELECT LobbyName FROM Lobby_Info;");

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                for (int i = 0; i < _qryList.Count; i++)
                {
                    _strQry = string.Format("INSERT INTO Lobby_Reset(LobbyName, Flag)  VALUES('{0}', 0);", _qryList[i]);
                    m_mysql.MySqlExec(_strQry);
                }
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_RESET_TIMER))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_RESET_TIMER);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOBBY_SYNC))
            {
                Program.CreateTable(Program.TBL_CREATE_LOBBY_SYNC);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSDEV))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_KMS_DEV);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_DEVSTATE))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_DEV_STATE);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSKEYREGDEL))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_KEY_REG_DEL);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSHOMENET))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_HOMENET);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSCRT))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_CRT);
            }

            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSWEB))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_WEB);
            }

            /*
            if (!Program.IsTable(Program.TBL_NAME_UKR_SENSITIVITY))
            {
                Program.CreateTable(Program.TBL_CREATE_UKR_SENSITIVITY);

                string _strQry = string.Format("SELECT LobbyName FROM Lobby_Info;");

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                for (int i = 0; i < _qryList.Count; i++)
                {
                    _strQry = string.Format("INSERT INTO UKR_Sensitivity(LobbyName, SetFlag)  VALUES('{0}', 0);", _qryList[i]);
                    m_mysql.MySqlExec(_strQry);
                }
            }
            else
            {
                string _strQry = string.Format("SELECT UKR_Sensitivity FROM LobbyName;");
                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
                if (_qryList.Count == 0)
                {
                    _strQry = string.Format("SELECT LobbyName FROM Lobby_Info;");

                    _qryList.Clear();
                    _qryList = m_mysql.MySqlSelect(_strQry, 1);

                    for (int i = 0; i < _qryList.Count; i++)
                    {
                        _strQry = string.Format("INSERT INTO UKR_Sensitivity(LobbyName, SetFlag)  VALUES('{0}', 0);", _qryList[i]);
                        m_mysql.MySqlExec(_strQry);
                    }
                }
            }
            */

            if(Program.g_nCrtCallDongHo == 1)
            {
                if (!Program.IsTable(Program.TBL_NAME_DONG_CRT_CARID))
                {
                    Program.CreateTable(Program.TBL_CREATE_DONG_CRT_CARID);
                    InsertDongCrtCarID();   // 주의: 동로비에 데이터가 있어야지만, 정보가 삽입이 됨.
                }
            }

            // 혜인 시스템과 연동을 하기 위해서 테이블을 만들어줌
            if (Program.g_nHanInParking == 1)
            {
                if (!Program.IsTable(Program.TBL_NAME_KEY_INFO_HAEIN))
                {
                    Program.CreateTable(Program.TBL_CREATE_KEY_INFO_HAEIN);
                    InsertKeyInfoHeain();
                    //View 테이블이 생성이 되어야함.
                    Program.CreateTable(Program.VIEW_CREATE_KEY_INFO_HAEIN);
                }
            }

            // 코오롱 베니트와 연동으로 인해 생겨난 테이블로, 테이블이 없으면 테이블 생성을 한다.
            //입주자 출입 기록을 남김, 3개월 마다 한번씩 백업을 함.
            if (!Program.IsTable(Program.TBL_NAME_LOG_KMSUKR))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_KMSUKR);
            }

            // 2023-12-22 셋팅팀 요청(현장 Config파일을 DB에 백업할 수 있게 만들어줌)
            // 기존 테이블이 존재하면 삭제를 하고 테이블을 다시 만듬.
            if (Program.IsTable(Program.TBL_NAME_BACKUP_CONFIG))
            {
                string strQry = string.Format("Drop TABLE kms.Backup_Config; ");
                m_mysql.MySqlExec(strQry);
            }
            Program.CreateTable(Program.TBL_CREATE_BACKUP_CONFIG);
            doConfigSave();

            if (!Program.IsTable(Program.TBL_NAME_LOG_ENTRYRECORD))
            {
                Program.CreateTable(Program.TBL_CREATE_LOG_ENTRYRECORD);
            }

            /////////////////////////////////////////////////////
        }

        // 기본 테이블 생성
        private void TblInit()
        {
            string _strQry = "";
            _strQry = string.Format("UPDATE Lobby_Reset SET Flag = 0;");
            m_mysql.MySqlExec(_strQry);
        }


        // 테이블 삭제

        // 2023-07-20 이진베이 현장 때문에 추가했음(동호콜을 할 때, Car가 그룹으로 되어 있으면 사용을 함)
        // 주의 동로비에 데이터가 있어야지만, 정보가 삽입이 됨.
        private void InsertDongCrtCarID()
        {
            string strQry = string.Format("INSERT INTO kms.Dong_Crt_CarID (Dong, Ho) Select Dong, Ho from kms.Dong_Lobby where Dong != 9999 group by Dong, Ho;");
            try
            {
                m_mysql.MySqlExec(strQry);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("InsertDongCrtCarID() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
                Console.WriteLine("strQry = " + strQry, System.Drawing.Color.Red);

                LogSave("InsertDongCrtCarID() 메소드 예외: " + ex.Message.ToString());
                LogSave("strQry = " + strQry);
            }
        }

        // 2023-07-26 Key_Info_Master에서 데이터를 읽어서 Key_Info_Haein 테이블에 삽입
        // 삽입을 하고나면, View 테이블에 자동으로 입력이 되어야함.
        private void InsertKeyInfoHeain()
        {
            string strQry = string.Format("Select Dong, Ho, Key_Sn, Key_Id from kms.Key_Info_Master order by Dong, Ho;");

            List<string[]> qryList = m_mysql.MySqlSelect(strQry, 4);

            foreach (string[] str in qryList)
            {
                InsertKeyInfoHeain(str[0], str[1], str[2], str[3]);
            }
        }

        private void InsertKeyInfoHeain(string strDong, string strHo, string strKeySn, string strKeyId)
        {
            string strDecode = m_dualI.Demodulation(strKeyId);

            string strQry = string.Format("Insert Into {0}(Dong, Ho, Key_Sn, Key_Cham, Key_Haein) values({1}, {2}, '{3}', '{4}', '{5}');",
                            Program.TBL_NAME_KEY_INFO_HAEIN, strDong, strHo, strKeySn, strKeyId, strDecode);

            m_mysql.MySqlExec(strQry);
            Thread.Sleep(3);
        }

        private void UpdateKeyInfoHeain(string strDong, string strHo, string strKeySn, string strKeyId)
        {
            string strDecode = m_dualI.Demodulation(strKeyId);

            string strQry = string.Format("Update {0} Set Dong = {1}, Ho = {2}, Key_Sn = '{3}', Key_Cham='{4}', Key_Haein = '{5}' where Key_cham = '{4}';",
                Program.TBL_NAME_KEY_INFO_HAEIN, strDong, strHo, strKeySn, strKeyId, strDecode);

            m_mysql.MySqlExec(strQry);
            Thread.Sleep(3);
        }
        


        public int GetStringToInt(string a_str)
        {
            string _strFirst = "", _strSecond = "";
            int _nRet = 0;

            _strFirst = a_str.Substring(0, 1);
            _strSecond = a_str.Substring(1, 1);

            _strFirst = _strFirst.ToUpper();
            _strSecond = _strSecond.ToUpper();

            switch (_strFirst)
            {
                case "A":
                    _nRet = 16 * 10;
                    break;
                case "B":
                    _nRet = 16 * 11;
                    break;
                case "C":
                    _nRet = 16 * 12;
                    break;
                case "D":
                    _nRet = 16 * 13;
                    break;
                case "E":
                    _nRet = 16 * 14;
                    break;
                case "F":
                    _nRet = 16 * 15;
                    break;
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    _nRet = 16 * int.Parse(_strFirst);
                    break;
                default:
                    break;

            }

            switch (_strSecond)
            {
                case "A":
                    _nRet = _nRet + 10;
                    break;
                case "B":
                    _nRet = _nRet + 11;
                    break;
                case "C":
                    _nRet = _nRet + 12;
                    break;
                case "D":
                    _nRet = _nRet + 13;
                    break;
                case "E":
                    _nRet = _nRet + 14;
                    break;
                case "F":
                    _nRet = _nRet + 15;
                    break;
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    _nRet = _nRet + int.Parse(_strSecond);
                    break;
                default:
                    break;
            }

            return _nRet;
        }

        private void LoadLobbyId()
        {
            string _strQry = string.Format("Select li.Lobby_GID, li.Lobby_DID, li.Lobby_Name, lf.Reg_Tbl, lf.Del_Tbl from kms.Lobby_Info as lf, kms.Lobby_ID as li where lf.LobbyName = li.Lobby_Name  order by li.Lobby_GID asc;");
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 5);

            foreach (string[] _str in _qryList)
            {
                stLobbyId _st = new stLobbyId();
                UkrState us = new UkrState();
                UkrRegDelCheck urdc = new UkrRegDelCheck();

                _st.strUkrGid = _str[0];
                _st.strUkrDid = _str[1];
                _st.strLobbyName = _str[2];

                us.m_strGid = _str[0];
                us.m_strDid = _str[1];

                urdc.m_strGid = _str[0];
                urdc.m_strDid = _str[1];
                urdc.m_strRegTblName = _str[3];
                urdc.m_strDelTblName = _str[4];
                urdc.m_bCheck = false;

                m_ukrState.Add(us);
                m_ukrRegDelCheck.Add(urdc);

                Program.m_listLobbyId.Add(_st);
            }
        }

        private void LoadDongInfo()
        {
            string _strQry = "Select substring_index(LobbyName, '_', 1) from kms.Lobby_Info group by substring_index(LobbyName, '_', 1);";

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                /*
                int _nDong = int.Parse(string.Format("{0}", _str[0]));
                if (_nDong != 9999)   // 9999동은 제외
                    m_listDong.Add(_nDong);
                */
                m_listDong.Add(_str[0].ToString());
            }
        }

        private void CreateButtonView() // 동 정보를 버튼으로 생성
        {
            mBtnDong = new Button[m_listDong.Count];
            int nCnt = 0;
            foreach (string _strDong in m_listDong)
            {
                if (_strDong == "Register" || _strDong == "register")
                {

                    continue;  // 등록기는 버튼으로 표시하지 않는다.
                }

                mBtnDong[nCnt] = new Button();
                mBtnDong[nCnt].Name = _strDong.ToString();
                mBtnDong[nCnt].Parent = splitContainer3.Panel1;
                mBtnDong[nCnt].Location = new Point(70, 80 + (nCnt) * 60);
                mBtnDong[nCnt].Size = new Size(130, 40);
                mBtnDong[nCnt].Font = new Font("돋움", 14, FontStyle.Bold);
                if (_strDong == "Register" || _strDong == "register")
                {
                    
                    mBtnDong[nCnt].TextAlign = ContentAlignment.MiddleRight;
                }
                
                mBtnDong[nCnt].Click += new EventHandler(btndong_click);
                mBtnDong[nCnt].MouseDown += new MouseEventHandler(btndong_down);
                mBtnDong[nCnt].MouseUp += new MouseEventHandler(btndong_up);
                mBtnDong[nCnt].MouseHover += new EventHandler(btndong_mouseHover);
                mBtnDong[nCnt].MouseLeave += new EventHandler(btndong_mouseLeave);

                mBtnDong[nCnt].Text = _strDong.ToString();
                mBtnDong[nCnt].BackColor = System.Drawing.Color.White;
                mBtnDong[nCnt].BackgroundImage = Properties.Resources.Btn_Dong_Normal;
                mBtnDong[nCnt].BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                mBtnDong[nCnt].TabStop = false;
                mBtnDong[nCnt].TabStop = false;
                mBtnDong[nCnt].FlatStyle = FlatStyle.Flat;
                mBtnDong[nCnt].FlatAppearance.BorderSize = 0;
                mBtnDong[nCnt].Enabled = true;
                mBtnDong[nCnt].Show();

                nCnt++;
            }

        }

        public void btndong_click(object sender, EventArgs e)
        {
            Control ctrl = sender as Control;
            if (ctrl != null)
            {
                for (int i = 0; i < m_ucMap.Length; i++)
                {
                    if (m_ucMap[i].getDong().ToString() == ctrl.Name)
                    {
                        //MessageBox.Show("Name : " + ctrl.Name + ", Text : " + ctrl.Text);

                        m_ucMap[i].Show();
                        this.splitContainer2.Dock = DockStyle.Fill;
                        this.splitContainer2.Panel1.AutoScroll = true;

                        BtnLobbyClick(ctrl.Name);
                    }
                    else
                    {
                        m_ucMap[i].Hide();
                    }
                }
            }
        }

        // 동버튼 마우스 Down 처리
        public void btndong_down(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = System.Drawing.Color.White;
            btn.BackgroundImage = Properties.Resources.Btn_Dong_Click;
            btn.Invalidate();
        }

        // 동버튼 마우스 Up 처리
        public void btndong_up(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = System.Drawing.Color.White;
            btn.BackgroundImage = Properties.Resources.Btn_Dong_Normal;
            btn.Invalidate();
        }
        
        // 동버튼 마우스 Over 처리
        public void btndong_mouseHover(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = System.Drawing.Color.White;
            btn.BackgroundImage = Properties.Resources.Btn_Dong_Over;
            btn.Invalidate();
        }
        // 동버튼 마우스 Levae 처리
        public void btndong_mouseLeave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = System.Drawing.Color.White;
            btn.BackgroundImage = Properties.Resources.Btn_Dong_Normal;
            btn.Invalidate();
        }

        private void CreateLobbyInfo()  // 2021-06-29 수정
        {
            m_ucMap = new UC_Map[m_listDong.Count];

            int nLoopCnt = 0;
            
            foreach (string _strDongName in m_listDong)
            {
                Console.WriteLine("DongName = {0}", _strDongName);
                this.splitContainer2.Panel1.Focus();
                m_ucMap[nLoopCnt] = new UC_Map();
                splitContainer2.Panel1.AutoScroll = true;
                m_ucMap[nLoopCnt].Parent = splitContainer4.Panel1;

                m_ucMap[nLoopCnt].Dock = DockStyle.Fill;

                List<LobbyBtnInfo> _listLBName = GatewayView(_strDongName);
                m_ucMap[nLoopCnt].CreateLobbyName(_listLBName);
                m_ucMap[nLoopCnt].lobbyBtnClick += new UC_Map.LobbyBtnClick(BtnLobbyClick);
                m_ucMap[nLoopCnt].setDong(_strDongName);

                if (nLoopCnt != 0)
                    m_ucMap[nLoopCnt].Hide();

                nLoopCnt++;
            }

        }

        private void LoadCrtInfo()
        {
            string _strQry = "SELECT Lobby_Name, UKR_GID, UKR_DID, UKR_EL_GID, UKR_EL_DID, EL_ID, Delay_Time, Take_On, EL_USE, Car_Config FROM Lobby_Crt_Info;";

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 10);

            m_listCrtCallInfo.Clear();

            foreach (string[] _str in _qryList)
            {
                stCrtCallInfo _stCrt = new stCrtCallInfo();
                _stCrt.byElGid = new byte[4];

                _stCrt.strLBName = _str[0];
                _stCrt.byUkrGid = Convert.ToByte(Convert.ToInt32(_str[1], 16));
                _stCrt.byUkrDid = Convert.ToByte(Convert.ToInt32(_str[2], 16));
                _stCrt.byElGid[0] = 0x00;   // 초기화
                _stCrt.byElGid[1] = 0x00;   // 초기화
                _stCrt.byElGid[2] = 0x00;   // 초기화
                _stCrt.byElGid[3] = 0x00;   // 초기화

                string[] _strSplit = _str[3].Split('-');
                for (int i = 0; i < _strSplit.Length; i++)
                {
                    _stCrt.byElGid[i] = Convert.ToByte(Convert.ToInt32(_strSplit[i], 16));
                }

                _stCrt.byElDid = Convert.ToByte(Convert.ToInt32(_str[4], 16));
                _stCrt.strCrtId = _str[5];
                _stCrt.nDelayTime = (int)(float.Parse(_str[6]) * 1000);
                _stCrt.strTakeOn = _str[7];
                if (_str[8] == "1")
                    _stCrt.bElUse = true;
                else
                    _stCrt.bElUse = false;
                _stCrt.strCarConfig = _str[9];

                _stCrt.KeyId = new List<string>();

                m_listCrtCallInfo.Add(_stCrt);
            }
        }

        private void tv_dong_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string strNodeKey = e.Node.Name;

            if (string.IsNullOrEmpty(strNodeKey))
                return;

            //Console.WriteLine("선택된 노드 키 : " + strNodeKey);

            for (int i = 0; i < m_listDong.Count; i++)
            {
                if (m_listDong[i] == strNodeKey)
                {
                    m_ucMap[i].Show();
                    this.splitContainer2.Dock = DockStyle.Fill;
                    this.splitContainer2.Panel1.AutoScroll = true;
                }
                else
                {
                    m_ucMap[i].Hide();
                }
            }
        }
        
        private List<LobbyBtnInfo> GatewayView(string a_strDongName) // 2021-06-29 수정
        {
            string _strQry = string.Format("SELECT substring(Lobby_Name,length(Lobby_Name)) as Grp FROM kms.Dong_Lobby where Lobby_Name like '{0}%' group by Grp order by Lobby_Name, substring(Lobby_Name,length(Lobby_Name)), Num asc;", a_strDongName);
            
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
            int nHeightIdx = 0, nWightIdx = 0;
            List<LobbyBtnInfo> _listLBName = new List<LobbyBtnInfo>();

            if (a_strDongName == "9999")    // 9999동이면 데이터를 만들지 않음
                return _listLBName;

            if (_qryList.Count >= 5) // 버튼이 가로로 5개 이상이 되면, 가로 시작이 처음 부터 되어야 한다.
            {
                nWightIdx = -1;
            }

            foreach (string[] _str in _qryList)
            {
                _strQry = string.Format("SELECT Lobby_Name, substring(Lobby_Name,length(Lobby_Name)) as Grp FROM Dong_Lobby where substring(Lobby_Name,length(Lobby_Name)) = '{1}' and Lobby_Name like '{0}%' group by Lobby_Name order by substring(Lobby_Name,length(Lobby_Name)), Num asc;", a_strDongName, _str[0]);
                
                Console.WriteLine("strQry = {0}", _strQry);
                List<string[]> _qryList2 = m_mysql.MySqlSelect(_strQry, 2);
                nHeightIdx = 0; // 버튼 가로 세로 정렬
                if (m_mysql.IsData(_strQry))
                {
                    ++nWightIdx;    // 버튼 가로 세로 정렬(기본값 설정)
                }
                foreach (string[] _str2 in _qryList2)
                {
                    LobbyBtnInfo lobbyBtnInfo = new LobbyBtnInfo();
                    lobbyBtnInfo.strDong = a_strDongName;
                    lobbyBtnInfo.strLobbyName = string.Format("{0}", _str2[0]);
                    lobbyBtnInfo.strLobbyGroup = string.Format("{0}", _str2[1]);

                    lobbyBtnInfo.nPosHeightIdx = ++nHeightIdx;  // 버튼 가로 세로 정렬
                    lobbyBtnInfo.nPosWightIdx = nWightIdx;      // 버튼 가로 세로 정렬

                    _listLBName.Add(lobbyBtnInfo);

                }
            }

            /*
            foreach (LobbyBtnInfo lobbyBtnInfo in _listLBName)
            {
                Console.WriteLine("로비 이름: {0}, 그룹: {1}, hPos: {2}, wPos: {3}" , lobbyBtnInfo.strLobbyName, lobbyBtnInfo.strLobbyGroup, lobbyBtnInfo.nPosHeightIdx, lobbyBtnInfo.nPosWightIdx);
            }
            */

            return _listLBName;
        }

        private List<string> GetLBName(string a_strDong, string a_strHo)
        {
            string _strQry = string.Format("Select Lobby_Name From Dong_Lobby where Dong = {0} and Ho = {1};", a_strDong, a_strHo);

            List<string> _listLBName = new List<string>();

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _listLBName.Add(string.Format("{0}", _str[0]));
            }

            return _listLBName;
        }

        // 로비 버튼 클릭
        //private Object thisLock = new Object();
        private void BtnLobbyClick(string a_strLBName)
        {
            ///////////////////////////////////////
            // 로비 정보를 클릭하면, 로비 정보, Reg, Del, Key_Info_Master 
            // 정보를 변경함.
            this.BtnEnable(false);
            {
                LVLobbyInfo(a_strLBName);
                LVRegTblView(a_strLBName);
                LVDelTblView(a_strLBName);
                LVMasterTblView(a_strLBName);
            }
            this.BtnEnable(true);
        }

        private void BtnEnable(bool a_nFlag)
        {
            for (int i = 0; i < m_listDong.Count; i++)
            {
                m_ucMap[i].BtnEnable(a_nFlag);
            }
        }

        private void LVLobbyInfo(string a_strLBName)
        {
            string strLBGid = "", strLBDid = "", strLBIp = "", strConnectionTime = "", strStateRecvTime = "", strLBReg = "", strLBDel = "";
            /*
            // LobbyIp 읽기
            string strQry = string.Format("Select ukr.IPAddr,  ukr.ConnectTime from kms.UKR_Connect_Info as ukr, kms.Lobby_ID li where li.Lobby_Name = '{0}' and (li.Lobby_GID = ukr.GID and li.Lobby_DID = ukr.DID);", a_strLBName);
            List<string[]> qryList = m_mysql.MySqlSelect(strQry, 2);
            foreach (string[] _str in qryList)
            {
                strLBIp = _str[0];
                strConnectionTime = _str[1];
            }

            // 상태 응답 시간 읽기
            strQry = string.Format("Select state.StateRecvTime from kms.UKR_State_Info as state, kms.Lobby_ID li where li.Lobby_Name = '{0}' and (li.Lobby_GID = state.GID and li.Lobby_DID = state.DID);", a_strLBName);
            qryList.Clear();
            qryList = m_mysql.MySqlSelect(strQry, 1);
            foreach (string[] _str in qryList)
            {
                strStateRecvTime = _str[0];
            }

            // Reg, Del 정보 읽기
            strQry = string.Format("SELECT Reg_Tbl, Del_Tbl FROM kms.Lobby_Info where LobbyName = '{0}';", a_strLBName);

            qryList.Clear();
            qryList = m_mysql.MySqlSelect(strQry, 2);

            foreach (string[] _str in qryList)
            {
                strLBReg = _str[0];
                strLBDel = _str[1];
            }

            strQry = string.Format("Select Lobby_Gid, Lobby_Did From kms.Lobby_ID where Lobby_Name = '{0}';", a_strLBName);
            qryList.Clear();
            qryList = m_mysql.MySqlSelect(strQry, 2);

            foreach (string[] _str in qryList)
            {
                strLBGid = _str[0];
                strLBDid = _str[1];
            }
            */

            string strQry = string.Format("Select ukr.IPAddr,  ukr.ConnectTime, state.StateRecvTime from kms.UKR_Connect_Info as ukr, kms.Lobby_ID li, kms.UKR_State_Info as state where(li.Lobby_Name = '{0}') and(li.Lobby_GID = ukr.GID and li.Lobby_DID = ukr.DID) and(li.Lobby_GID = state.GID and li.Lobby_DID = state.DID); ", a_strLBName);
            List<string[]> qryList = m_mysql.MySqlSelect(strQry, 3);
            
            foreach (string[] _str in qryList)
            {
                strLBIp = _str[0];
                strConnectionTime = _str[1];
                strStateRecvTime = _str[2];
            }
            qryList.Clear();

            strQry = string.Format("Select loin.Reg_Tbl, loin.Del_Tbl, li.Lobby_GID, li.Lobby_DID from kms.Lobby_ID li, kms.Lobby_Info as loin where(li.Lobby_Name = '{0}' and loin.LobbyName = '{0}'); ", a_strLBName);
            qryList = m_mysql.MySqlSelect(strQry, 4);
            foreach (string[] _str in qryList)
            {
                strLBReg = _str[0];
                strLBDel = _str[1];
                strLBGid = _str[2];
                strLBDid = _str[3];
            }

            uc_lv_lb_UkrInfo.doListClear();

            ListViewItem _lvi1 = new ListViewItem("");
            _lvi1.SubItems.Add("로비 ID");
            if (strLBGid == "")
                return;
            string _strValue = "GID:" + Convert.ToInt32(strLBGid, 16).ToString() + "(0x" + strLBGid + ")" + "  DID:" + Convert.ToInt32(strLBDid, 16).ToString() + "(0x" + strLBDid + ")";
            _lvi1.SubItems.Add(_strValue);

            ListViewItem _lvi2 = new ListViewItem("");
            _lvi2.SubItems.Add("로비 IP");
            _lvi2.SubItems.Add(strLBIp);

            ListViewItem _lvi3 = new ListViewItem("");
            _lvi3.SubItems.Add("Reg Table");
            _lvi3.SubItems.Add(strLBReg);

            ListViewItem _lvi4 = new ListViewItem("");
            _lvi4.SubItems.Add("Del Table");
            _lvi4.SubItems.Add(strLBDel);

            ListViewItem _lvi5 = new ListViewItem("");
            _lvi5.SubItems.Add("접속 시간");
            _lvi5.SubItems.Add(strConnectionTime);

            ListViewItem _lvi6 = new ListViewItem("");
            _lvi6.SubItems.Add("상태 응답 시간");
            _lvi6.SubItems.Add(strStateRecvTime);

            List<ListViewItem> lvis = new List<ListViewItem>();
            lvis.Add(_lvi1);
            lvis.Add(_lvi2);
            lvis.Add(_lvi3);
            lvis.Add(_lvi4);
            lvis.Add(_lvi5);
            lvis.Add(_lvi6);
            
            uc_lv_lb_UkrInfo.InvokeIfNeeded(() => uc_lv_lb_UkrInfo.SetListData(lvis));
        }

        private void LVRegTblView(string a_strLBName)
        {
            string _strQry = string.Format("SELECT Dong, Ho, Key_Sn, Key_Id FROM Reg_{0};", a_strLBName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

            string _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "";

            uc_lv_lb_reg.InvokeIfNeeded(() => uc_lv_lb_reg.doListClear());
            List<ListViewItem> lvi = new List<ListViewItem>();
            foreach (string[] _str in _qryList)
            {
                _strDong = _str[0];
                _strHo = _str[1];
                _strKeySn = _str[2];
                _strKeyId = _str[3];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_strDong);
                _lvi.SubItems.Add(_strHo);
                _lvi.SubItems.Add(_strKeySn);
                _lvi.SubItems.Add(_strKeyId);

                lvi.Add(_lvi);
            }
            uc_lv_lb_reg.InvokeIfNeeded(() => uc_lv_lb_reg.SetListData(lvi));
        }

        private void LVDelTblView(string a_strLBName)
        {
            string _strQry = string.Format("SELECT Dong, Ho, Key_Sn, Key_Id FROM Del_{0};", a_strLBName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

            string _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "";

            uc_lv_lb_del.InvokeIfNeeded(() => uc_lv_lb_del.doListClear());
            List<ListViewItem> lvi = new List<ListViewItem>();
            foreach (string[] _str in _qryList)
            {
                _strDong = _str[0];
                _strHo = _str[1];
                _strKeySn = _str[2];
                _strKeyId = _str[3];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_strDong);
                _lvi.SubItems.Add(_strHo);
                _lvi.SubItems.Add(_strKeySn);
                _lvi.SubItems.Add(_strKeyId);

                
                lvi.Add(_lvi);
            }
            uc_lv_lb_del.InvokeIfNeeded(() => uc_lv_lb_del.SetListData(lvi));
        }

        private void LVMasterTblView(string a_strLBName)
        {
            string _strQry = string.Format("select K.Dong, K.Ho, K.Key_Sn, K.Key_Id From Key_Info_Master AS K INNER JOIN Dong_Lobby AS D ON K.Dong = D.Dong and K.Ho = D.Ho and D.Lobby_Name = '{0}' ;", a_strLBName);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

            string _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "";

            uc_lv_lb_master.InvokeIfNeeded(() => uc_lv_lb_master.doListClear());
            List<ListViewItem> lvi = new List<ListViewItem>();
            foreach (string[] _str in _qryList)
            {
                _strDong = _str[0];
                _strHo = _str[1];
                _strKeySn = _str[2];
                _strKeyId = _str[3];

                ListViewItem _lvi = new ListViewItem("");
                _lvi.SubItems.Add(_strDong);
                _lvi.SubItems.Add(_strHo);
                _lvi.SubItems.Add(_strKeySn);
                _lvi.SubItems.Add(_strKeyId);

                
                lvi.Add(_lvi);
            }
            uc_lv_lb_master.InvokeIfNeeded(() => uc_lv_lb_master.SetListData(lvi));
        }

        private void menu_sync_setting_Click(object sender, EventArgs e)
        {
            FormPassword frmPw = new FormPassword();
            frmPw.ShowDialog();

            if (Program.g_bpassword == true)
            {
                Program.g_bpassword = false;

                FormSyncInfo _frmSync = new FormSyncInfo();
                _frmSync.SetMySql(m_mysql);
                _frmSync.ShowDialog();
            }
        }

        private void menu_regdel_log_Click(object sender, EventArgs e)
        {
            FormRegDelLog _frmLog = new FormRegDelLog();
            _frmLog.SetMySql(m_mysql);
            _frmLog.ShowDialog();

        }

        private void menu_Test_Crt_Click(object sender, EventArgs e)
        {
            FormCrtCall _frmTestCrt = new FormCrtCall();
            _frmTestCrt.Owner = this;
            _frmTestCrt.SetMySql(m_mysql);
            _frmTestCrt.ShowDialog();
        }

        private void CrtCallLogtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCrtCallLog _frmLog = new FormCrtCallLog();
            _frmLog.SetMySql(m_mysql);
            _frmLog.ShowDialog();
        }

        private void btn_keyid_load_Click(object sender, EventArgs e)
        {
            if (Program.g_nRegisterDeviceType == 1)
            {
                KeyIdRequest(); // 신규등록기에 Key ID 요청
            }
            else
            {   // 듀얼아이에 Key Id 요청
                m_dualI.ReadCard4();
                txt_reg_keysn.Text = m_dualI.StrCardSn;
                txt_reg_keyid.Text = m_dualI.StrCardId;
            }
            Program.g_nRegistingKeyType = 1;    // 2021-04-21 추가. 키등록시 Key_Type에 값을 추가 하기 위한 변수
        }

        private void btn_keyId_list_reg_Click(object sender, EventArgs e)
        {
            string strDong = "", strHo = "", strKeySn = "", strKeyId = "";

            txt_reg_dong.InvokeIfNeeded(() => strDong = txt_reg_dong.Text);
            txt_reg_ho.InvokeIfNeeded(() => strHo = txt_reg_ho.Text);
            txt_reg_keysn.InvokeIfNeeded(() => strKeySn = txt_reg_keysn.Text);
            txt_reg_keyid.InvokeIfNeeded(() => strKeyId = txt_reg_keyid.Text);

            /////////////////////////////////////////////
            // 입력 값 공백 제거
            strDong = strDong.Trim();
            strHo = strHo.Trim();
            strKeySn = strKeySn.Trim();
            strKeyId = strKeyId.Trim();

            /////////////////////////////////////////////
            // 입력 값이 비어있는지 확인
            if ((strDong == "") && (strHo == ""))
            {
                MessageBox.Show("동, 호 정보를 입력해주세요.");
                return;
            }
            else if (strDong == "")
            {
                MessageBox.Show("동 정보를 입력해주세요.");
                return;
            }
            else if (strHo == "")
            {
                MessageBox.Show("호 정보를 입력해주세요.");
                return;
            }
            if ((strKeySn == "") && (strKeyId == ""))
            {
                MessageBox.Show("Key Sn 정보와 Key Id 정보를 입력해주세요.");
                return;
            }
            else if (strKeyId == "")
            {
                MessageBox.Show("Key Id 정보를 입력해주세요.");
                return;
            }
            else if (strKeySn == "")
            {
                MessageBox.Show("Key Sn 정보를 입력해주세요.");
                return;
            }

            /////////////////////////////////////////////
            // 입력 형식 확인
            if (strKeySn.Length != 8)
            {
                MessageBox.Show("Key Sn 형식이 잘못되었습니다.\n\n숫자 8 자리 입력");
                return;
            }
            else if (strKeyId.Length != 16)
            {
                MessageBox.Show("Key Id 형식이 잘못되었습니다.");
                return;
            }

            if ((strKeySn.Substring(0, 1).Equals("S")) || (strKeySn.Substring(0, 1).Equals("s")))
            {
                MessageBox.Show("Key Sn 문자열을 제외한 숫자 8자리를 입력해주세요.");
                return;
            }

            /////////////////////////////////////////////
            // List에 같은 키 정보가 있는지 확인
            string strDongComp = "", strHoComp = "", strKeySnComp = "", strKeyIdComp = "";

            for (int i = 0; i < lv_key_info.Items.Count; i++)
            {
                lv_key_info.InvokeIfNeeded(() => strDongComp = lv_key_info.Items[i].SubItems[1].Text);
                lv_key_info.InvokeIfNeeded(() => strHoComp = lv_key_info.Items[i].SubItems[2].Text);
                lv_key_info.InvokeIfNeeded(() => strKeySnComp = lv_key_info.Items[i].SubItems[3].Text);
                lv_key_info.InvokeIfNeeded(() => strKeyIdComp = lv_key_info.Items[i].SubItems[4].Text);

                strKeyIdComp = strKeyIdComp.ToLower();
                strKeyId = strKeyId.ToLower();

                if ((strDong == strDongComp) &&
                    (strHo == strHoComp) &&
                    (strKeySn == strKeySnComp) &&
                    (strKeyId == strKeyIdComp))
                {
                    MessageBox.Show("똑같은 키 정보가 있습니다.");
                    return;
                }
            }

            ListViewItem _lvi = new ListViewItem("");
            _lvi.SubItems.Add(strDong);
            _lvi.SubItems.Add(strHo);
            _lvi.SubItems.Add(strKeySn);
            _lvi.SubItems.Add(strKeyId);

            lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Add(_lvi));

            //////////////////////////////////////////
            // 입력정보 지우기
            txt_reg_dong.Text = string.Empty;
            txt_reg_ho.Text = string.Empty;
            txt_reg_keysn.Text = string.Empty;
            txt_reg_keyid.Text = string.Empty;

            // 과금형 카드 등록 테스트시 사용코드
            //txt_reg_ho.Text = (int.Parse(txt_reg_ho.Text) + 1).ToString();
            //txt_reg_keysn.Text = string.Format("{0:D8}", (int.Parse(txt_reg_keysn.Text) + 1).ToString().PadLeft(8, '0'));
        }

        private void menu_reg_key_all_Click(object sender, EventArgs e)
        {
            FormRegKeyAll _frmRegAll = new FormRegKeyAll();
            _frmRegAll.ShowDialog();
        }

        ////////////////////////////////////////////////////////////
        // 키 등록 리스트 우클릭
        private void lv_key_info_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button.Equals(MouseButtons.Right))
            {
                ListViewItem _lvi = new ListViewItem();
                int nSelIdx = 0;
                lv_key_info.InvokeIfNeeded(() => nSelIdx = lv_key_info.FocusedItem.Index);
                lv_key_info.InvokeIfNeeded(() => _lvi = lv_key_info.Items[nSelIdx]);

                if (MessageBox.Show("Key Data를 삭제 하시겠습니까?", "삭제 확인", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Remove(_lvi));
            }
        }

        private void btn_key_reg_Click(object sender, EventArgs e)
        {
            int nLoop = 0;
            List<stKeyInfo> _listKeyInfo = new List<stKeyInfo>();

            lv_key_info.Invoke(new MethodInvoker(delegate
            {
                nLoop = lv_key_info.Items.Count;

                for (int i = 0; i < nLoop; i++)
                {
                    stKeyInfo _stKeyInfo = new stKeyInfo();

                    _stKeyInfo.strDong = lv_key_info.Items[i].SubItems[1].Text;
                    _stKeyInfo.strHo = lv_key_info.Items[i].SubItems[2].Text;
                    _stKeyInfo.strKeySn = lv_key_info.Items[i].SubItems[3].Text;
                    _stKeyInfo.strKeyId = lv_key_info.Items[i].SubItems[4].Text;

                    _listKeyInfo.Add(_stKeyInfo);
                }
                lv_key_info.Items.Clear();
            }));

            // 2023-10-10 개포 전기차 13.56(등록 추가)
            if (cb_charger_normal.Checked == true &&
                cb_charger_portable.Checked == true)
            {
                MessageBox.Show("과금형 카드 또는 이동형 카드 1개만 등록해 주세요.");
                _listKeyInfo.Clear();
                return;
            }
            else if (cb_charger_normal.Checked == true &&
                cb_charger_portable.Checked == false)
            {
                DialogResult result = MessageBox.Show("과금형 카드로 등록이 됩니다. 다시한번 확인해 주세요. 계속 진행하시겠습니까?", 
                    "과금형 카드", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    /*
                    if (isKeyIDOverLap(_listKeyInfo, 9))  // Key ID가 중복이 되면 등록을 하지 않는다.
                    {
                        MessageBox.Show("KeyID가 중복이 되어 등록이 안되었습니다. 로그를 확인해 주세요.");
                        return;
                    }
                    */

                    string strCarNum = "";
                    txtCarNum.Invoke(new MethodInvoker(delegate
                    {
                        strCarNum = txtCarNum.Text;
                    }));

                    KeyInfoMasterInsert(_listKeyInfo, 9);
                    
                    _listKeyInfo.Clear();
                    return;
                }
                else if (result == DialogResult.No)
                {
                    _listKeyInfo.Clear();
                    return;
                }
            }
            else if (cb_charger_normal.Checked == false &&
                cb_charger_portable.Checked == true)
            {
                DialogResult result = MessageBox.Show("이동형 카드로 등록이 됩니다. 다시한번 확인해 주세요. 계속 진행하시겠습니까?",
                    "이동형 카드", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    /*
                    if (isKeyIDOverLap(_listKeyInfo, 10))// Key ID가 1개라도 중복이 되면, 등록하지 않는다.
                    {  
                        MessageBox.Show("KeyID가 중복이 되어 등록이 안되었습니다. 로그를 확인해 주세요.");
                        return;
                    }
                    */

                    string strCarNum = "";
                    txtCarNum.Invoke(new MethodInvoker(delegate
                    {
                        strCarNum = txtCarNum.Text;
                    }));

                    KeyInfoMasterInsert(_listKeyInfo, 10);
                    
                    _listKeyInfo.Clear();
                    return;
                }
                else if (result == DialogResult.No)
                {
                    _listKeyInfo.Clear();
                    return;
                }
            }
            else
            {

                MessageBox.Show("키등록을 시작 합니다.");

                //KeyInfoMasterDel(_listKeyInfo);

                Thread thKey_reg = new Thread(() => KeyInsertThread(_listKeyInfo));

                thKey_reg.IsBackground = true;

                thKey_reg.Start();
            }
        }

        public void KeyInsertThread(List<stKeyInfo> a_listKeyInfo)
        {
            RegTblCheck_del(a_listKeyInfo);

            KeyInfoMasterInsert(a_listKeyInfo);

            RegTblInsert(a_listKeyInfo);
        }

        private void btn_key_del_Click(object sender, EventArgs e)
        {
            int nLoop = 0;

            lv_key_info.InvokeIfNeeded(() => nLoop = lv_key_info.Items.Count);

            List<stKeyInfo> _listKeyInfo = new List<stKeyInfo>();
            for (int i = 0; i < nLoop; i++)
            {
                stKeyInfo _stKeyInfo = new stKeyInfo();

                lv_key_info.InvokeIfNeeded(() => _stKeyInfo.strDong = lv_key_info.Items[i].SubItems[1].Text);
                lv_key_info.InvokeIfNeeded(() => _stKeyInfo.strHo = lv_key_info.Items[i].SubItems[2].Text);
                lv_key_info.InvokeIfNeeded(() => _stKeyInfo.strKeySn = lv_key_info.Items[i].SubItems[3].Text);
                lv_key_info.InvokeIfNeeded(() => _stKeyInfo.strKeyId = lv_key_info.Items[i].SubItems[4].Text);

                _listKeyInfo.Add(_stKeyInfo);
            }

            MessageBox.Show("키 삭제를 시작 합니다.");

            // Del_에 데이터를 입력하고,
            // Key_Info_Master에 데이터를 삭제하고,
            // Delete_Key_Info에 데이터를 저장함.

            KeyInfoDel(_listKeyInfo);
            lv_key_info.InvokeIfNeeded(() => lv_key_info.Items.Clear());
        }

        ////////////////////////////////////////////////////////////////////
        // Del_에 데이터를 입력하고,
        // Key_Info_Master에 데이터를 삭제하고, 
        // Delete_Key_Info에 데이터를 저장함.            
        private void KeyInfoDel(List<stKeyInfo> a_listKeyInfo)
        {
            string _strQry = "", _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "", _strTime = "";
            try
            {
                foreach (stKeyInfo _st in a_listKeyInfo)
                {
                    _strDong = _st.strDong;
                    _strHo = _st.strHo;
                    _strKeySn = _st.strKeySn;
                    _strKeyId = _st.strKeyId;
                    _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // 1. Del_에 삭제할 데이터 입력
                    List<string> _listLBName = GetLBName(_strDong, _strHo);
                    foreach (string _strLBName in _listLBName)
                    {
                        _strQry = string.Format("Insert Into Del_{0}(Dong, Ho, Key_Sn, Key_Id, State) values({1}, {2}, '{3}', '{4}', 0);",
                            _strLBName, _strDong, _strHo, _strKeySn, _strKeyId);

                        m_mysql.MySqlExec(_strQry);
                        Thread.Sleep(10);
                    }

                    // 2. Key_Info_Master에서 Key 데이터 삭제
                    _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Sn='{2}' and Key_Id='{3}';",
                                    _strDong, _strHo, _strKeySn, _strKeyId);
                    m_mysql.MySqlExec(_strQry);
                    Thread.Sleep(10);

                    // 3. Delete_Key_Info에 삭제 데이터 저장함.
                    _strQry = string.Format("Insert Into Delete_Key_Info(Dong, Ho, Key_Sn, Key_Id, DelDate) values({0}, {1}, '{2}', '{3}','{4}');",
                        _strDong, _strHo, _strKeySn, _strKeyId, _strTime);
                    m_mysql.MySqlExec(_strQry);
                    Thread.Sleep(10);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("KeyInfoDel() 예외 발생: " + ex.Message.ToString());
                Console.WriteLine("strQry: " + _strQry);

                LogSave("KeyInfoDel() 예외 발생: " + ex.Message.ToString());
                LogSave("strQry: " + _strQry);
            }

        }

        ////////////////////////////////////////////////////////////////////
        // 새로 등록되는 Key_Info_Master Tbl 정보를 변경하고
        // 기존에 있던 정보를 Del Table에 옮김(9999동 9999호는 Del_Tbl에 옮기지 않음).
        // 그리고 Key 관리(Log)를 위해 삭제하는 정보를 Delete_Key_Info에 Data를 넣음.
        private void KeyInfoMasterDel(List<stKeyInfo> a_listKeyInfo)
        {
            string _strQry = "", _strDong = "", _strHo = "", _strKeySn = "", _strKeyId = "", _strTime = "";
            // 1. 삭제할 키의 동, 호 정보를 찾음.
            List<stKeyInfo> _listDelInfo = new List<stKeyInfo>();
            foreach (stKeyInfo _st in a_listKeyInfo)
            {
                bool _bFlag = true;

                foreach (stKeyInfo _del in _listDelInfo)
                {
                    if ((_st.strDong == _del.strDong) &&
                        (_st.strHo == _del.strHo))
                    {
                        _bFlag = false;
                        break;
                    }
                }

                if (_bFlag == true)
                {
                    _listDelInfo.Add(_st);
                }
            }

            // 2. 삭제할 키데이터를 Del Tbl에 넣음.
            foreach (stKeyInfo _del in _listDelInfo)
            {
                if ((_del.strDong == "9999") ||
                    (_del.strHo == "9999"))
                    continue;

                _strQry = string.Format("Select Dong, Ho, Key_Sn, Key_Id From Key_Info_Master where Dong = {0} and Ho = {1};", _del.strDong, _del.strHo);

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

                foreach (string[] _str in _qryList)
                {
                    _strDong = _str[0];
                    _strHo = _str[1];
                    _strKeySn = _str[2];
                    _strKeyId = _str[3];

                    List<string> _listLBName = GetLBName(_strDong, _strHo);

                    foreach (string _strLBName in _listLBName)
                    {
                        _strQry = string.Format("Insert Into Del_{0}(Dong, Ho, Key_Sn, Key_Id, State) values({1}, {2}, '{3}', '{4}', 0);",
                            _strLBName, _strDong, _strHo, _strKeySn, _strKeyId);

                        m_mysql.MySqlExec(_strQry);
                    }
                }
            }

            // 3. Delete_Key_Info 삭제할 Data 삽입
            foreach (stKeyInfo _del in _listDelInfo)
            {
                if ((_del.strDong == "9999") ||
                    (_del.strHo == "9999"))
                    continue;

                _strQry = string.Format("Select Dong, Ho, Key_Sn, Key_Id From Key_Info_Master where Dong = {0} and Ho = {1};", _del.strDong, _del.strHo);

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

                foreach (string[] _str in _qryList)
                {
                    _strDong = _str[0];
                    _strHo = _str[1];
                    _strKeySn = _str[2];
                    _strKeyId = _str[3];
                    _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    _strQry = string.Format("Insert Into Delete_Key_Info(Dong, Ho, Key_Sn, Key_Id, DelDate) values({0}, {1}, '{2}', '{3}','{4}');",
                        _strDong, _strHo, _strKeySn, _strKeyId, _strTime);

                    m_mysql.MySqlExec(_strQry);

                }
            }

            // 4. Key_Info_Master에서 기존 Key 정보 삭제
            foreach (stKeyInfo _del in _listDelInfo)
            {
                if ((_del.strDong == "9999") ||
                    (_del.strHo == "9999"))
                    continue;

                _strQry = string.Format("Select Dong, Ho, Key_Sn, Key_Id From Key_Info_Master where Dong = {0} and Ho = {1};", _del.strDong, _del.strHo);

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 4);

                foreach (string[] _str in _qryList)
                {
                    _strDong = _str[0];
                    _strHo = _str[1];
                    _strKeySn = _str[2];
                    _strKeyId = _str[3];

                    _strQry = string.Format("Delete FRom Key_Info_Master where Dong={0} and Ho={1} and Key_Sn='{2}' and Key_Id='{3}';",
                                _strDong, _strHo, _strKeySn, _strKeyId);
                    m_mysql.MySqlExec(_strQry);
                }
            }

        }

        ////////////////////////////////////////////////////////////////////
        // Key_Info_Master에 Key Data 삽입
        private void KeyInfoMasterInsert(List<stKeyInfo> a_listKeyInfo)
        {
            KeyInfoMasterInsert(a_listKeyInfo, Program.g_nRegistingKeyType);
            /*
            string _strQry = "", _strTime = "";
            foreach (stKeyInfo _st in a_listKeyInfo)
            {
                if (isKeyID(_st.strKeyId))   // Key ID가 있으면, Update
                {
                    UpdateKeyId(_st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);

                    if (Program.g_nHanInParking == 1)
                    {
                        UpdateKeyInfoHeain(_st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);
                    }
                }
                else
                {
                    _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    // 2021-04-20 수정, Key_Info_Master에서 Key_type을 추가 하여, 수정함.
                    if (isKeyInfoMasterFieldTypeCheck())    // Key_Info_Master에 Key_Type Column 있으면
                    {
                        _strQry = string.Format("Insert Into Key_Info_Master(Dong, Ho, Key_Sn, Key_Id, RegDate, Key_type) values({0}, {1}, '{2}', '{3}', '{4}', {5});",
                            _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId, _strTime, Program.g_nRegistingKeyType);
                    }
                    else // Key_Info_Master에 Key_Type Column 없으면, 
                    {
                        _strQry = string.Format("Insert Into Key_Info_Master(Dong, Ho, Key_Sn, Key_Id, RegDate) values({0}, {1}, '{2}', '{3}', '{4}');",
                            _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId, _strTime);
                    }
                    m_mysql.MySqlExec(_strQry);

                    if (Program.g_nHanInParking == 1)
                    {
                        InsertKeyInfoHeain(_st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);
                    }

                }
            }
            */
        }

        ////////////////////////////////////////////////////////////////////
        // Key_Info_Master에 Key Data 삽입(개포 과금형 콘센트로 추가 됨.
        private void KeyInfoMasterInsert(List<stKeyInfo> a_listKeyInfo, int nType)  // nType이 9이면, Key_Info_Master에 과금형 카드이고, nType 10이면 Key_Portable_Charger에 추가 됨.
        {
            string _strQry = "", _strTime = "";
            foreach (stKeyInfo _st in a_listKeyInfo)
            {
                _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // 2021-04-20 수정, Key_Info_Master에서 Key_type을 추가 하여, 수정함.
                if (nType != 10) // 이동형 카드 Type이 아니면, 
                {
                    if (isKeyID("Key_Info_Master", _st.strKeyId))   // Key ID가 있으면, Update
                    {
                        _strQry = string.Format("Update kms.Key_Info_Master Set Dong = {0}, Ho = {1}, Key_Sn = '{2}', RegDate = '{3}' where Key_Id = '{4}';", _st.strDong, _st.strHo, _st.strKeySn, _strTime, _st.strKeyId);

                        if (Program.g_nHanInParking == 1)
                        {
                            UpdateKeyInfoHeain(_st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);
                        }
                    }
                    else
                    {
                        if (isKeyInfoMasterFieldTypeCheck())    // Key_Info_Master에 Key_Type Column 있으면
                        {
                            _strQry = string.Format("Insert Into Key_Info_Master(Dong, Ho, Key_Sn, Key_Id, RegDate, Key_type) values({0}, {1}, '{2}', '{3}', '{4}', {5});",
                                _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId, _strTime, nType);
                        }
                        else // Key_Info_Master에 Key_Type Column 없으면, 
                        {
                            _strQry = string.Format("Insert Into Key_Info_Master(Dong, Ho, Key_Sn, Key_Id, RegDate) values({0}, {1}, '{2}', '{3}', '{4}');",
                                _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId, _strTime);
                        }

                        if (Program.g_nHanInParking == 1)
                        {
                            InsertKeyInfoHeain(_st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);
                        }
                    }
                }
                else  // 이동형 카드 Type이면, 
                {
                    if (isKeyID("Key_Portable_Charger", _st.strKeyId))   // Key ID가 있으면, Update
                    {

                    }
                    else
                    {
                        _strQry = string.Format("Insert Into kms.Key_Portable_Charger(Dong, Ho, Key_Sn, Key_Id, RegDate) values({0}, {1}, '{2}', '{3}', '{4}');",
                            _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId, _strTime);
                    }
                }

                if (_strQry != "")
                    m_mysql.MySqlExec(_strQry);
            }
        }

        private bool isKeyID(string strTblName, string strKeyId)
        {
            string _strQry = "";
            // 컬럼 개수를 알수 없어서 컬럼 개수를 먼저 구함.

            _strQry = string.Format("select count(*) from kms.{0} where Key_Id = '{1}';", strTblName, strKeyId);
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
            int nCnt = 0;

            foreach (string[] _str in _qryList)
            {
                nCnt = int.Parse(_str[0]);
            }

            if (nCnt == 0)
                return false;

            return true;
        }

        ////////////////////////////////////////////////////////////////////
        // 전기차 충전 카드 중복 확인
        private bool isKeyIDOverLap(List<stKeyInfo> keyListInfo, int nType)  // Key ID가 중복이 되면, return true;
        {
            foreach (stKeyInfo st in keyListInfo)
            {
                string strQry = "";
                if (nType == 9)
                {
                    strQry = string.Format("select Key_Id from kms.Key_Info_Master where Key_Id = '{0}';", st.strKeyId);
                }
                else if (nType == 10)
                {
                    strQry = string.Format("select Key_Id from kms.Key_Portable_Charger where Key_Id = '{0}';", st.strKeyId);
                }

                if (strQry == "")   // 문제가 있는 것으로 true로 리턴함.
                    return true;


                if (m_mysql.IsData(strQry))
                {
                    string strLog = "";
                    int nDong = 0, nHo = 0;
                    if (nType == 9)
                    {
                        Program.GetDongHo(st.strKeyId, ref nDong, ref nHo);
                        strLog = string.Format("{0}동 {1}호 KeyID:{2}가 Key_Info_Master에서 중복입니다. 확인 후 다시 등록해 주세요.", nDong, nHo, st.strKeyId);
                    }
                    else if (nType == 10)
                    {
                        Program.GetDongHo(st.strKeyId, ref nDong, ref nHo, 10);
                        strLog = string.Format("{0}동 {1}호 KeyID:{2}가 Key_Portable_Charger에서 중복입니다. 확인 후 다시 등록해 주세요.", nDong, nHo, st.strKeyId);
                    }

                    LogPrint(strLog);

                    return true;
                }
            }
            
            // 중복 되는 데이터가 없어야, false를 리턴함. false가 리턴이 되어야 KeyID가 등록이 됨.
            return false;
        }

        ////////////////////////////////////////////////////////////////////
        // Reg Table에 키데이터 등록
        private void RegTblInsert(List<stKeyInfo> a_listKeyInfo)
        {
            string _strQry = "";
            foreach (stKeyInfo _st in a_listKeyInfo)
            {
                if (_st.strKeyId == "" | _st.strKeyId == null)
                    continue;

                List<string> _listLBName = GetLBName(_st.strDong, _st.strHo);

                foreach (string _strLBName in _listLBName)
                {
                    for (int i = 0; i < 2; i++) // Reg Tbl에 키를 두번 등록함.
                    {
                        _strQry = string.Format("Insert Into Reg_{0}(Dong, Ho, Key_Sn, Key_Id, State) values({1}, {2}, '{3}', '{4}', 0);",
                            _strLBName, _st.strDong, _st.strHo, _st.strKeySn, _st.strKeyId);
                        m_mysql.MySqlExec(_strQry);
                    }
                }
            }

        }

        /////////////////////////////////////////////////////////
        // Reg Table에서 키데이터 불량 제거
        private void RegTblCheck_del(List<stKeyInfo> a_listKeyInfo)
        {
            string _strQry = "";
            foreach (stKeyInfo _st in a_listKeyInfo)
            {
                if (_st.strKeyId == "" | _st.strKeyId == null)
                    continue;

                List<string> _listLBName = GetLBName(_st.strDong, _st.strHo);

                foreach (string _strLBName in _listLBName)
                {
                    for (int i = 0; i < 2; i++) // Reg Tbl에 키를 두번 등록함.
                    {
                        _strQry = string.Format("select Dong,Ho From Reg_{0} where Key_Id='' or Key_Id=null;", _strLBName);
                        List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);
                        foreach (string[] _str in _qryList)
                        {
                            _strQry = string.Format("delete from Reg_{0} where Dong={1} and Ho={2};", _strLBName, _str[0], _str[1]);
                        }
                    }
                }
            }
        }

        ///////////////////////////////////////////////////
        // Data 수신 Event
        // 2020-05-06 한정태 Thread 기능으로 변경
        // 2020-08-13 코드를 수정하여 Queue 기능이 있지만, Enqueu를 하지 않아 실제로는 Dequeue를 사용하지 않음.
        public void DevDataRecvThread(Object o)
        {
            // Log 저장
            while (Program.g_bDataRecv)
            {
                try
                {
                    if (m_server.OPRecvData.Count > 0)
                    {
                        OPDevPtrl opDevRecv = m_server.OPRecvData.Dequeue();
                        // 2020-05-06 추가 Dequeue에 대한 로그
                        DoLogDequeue(opDevRecv, m_server.OPRecvData.Count);

                        RecvDataProc(opDevRecv);

                    }
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("DevDataRecvThread() 예외 발생 : " + ex.Message.ToString());
                    LogSave("DevDataRecvThread() 예외 발생 : " + ex.Message.ToString());

                }

                Thread.Sleep(10);
            }
        }

        private void RecvDataProc(OPDevPtrl a_OpDevData)
        {
            try
            {
                switch (a_OpDevData.byType)
                {
                    case 0xE0:  // 이벤트 
                        switch (a_OpDevData.byCmd)
                        {
                            case 0x01:  // 접속 이벤트
                            case 0x07:  // Test를 위해
                                DeviceAdd(a_OpDevData);
                                break;
                            case 0x02:  // 출입 인증 이벤트(엘리베이터를 호출 해야함.)
                                // 2초간 대기 후 인증 데이터가 들어오는지 확인
                                KeyConfirm(a_OpDevData);

                                ////////////////////////////////////
                                // 예외 사항 체크
                                // 중요) 2021-08-10 테스트중 발견
                                // 서버를 Reset을 한 상태에서, 공동현관이 인증이 되면, 
                                // 서버에 접속 패킷 정보를 보내지 않고, 세션을 유지한다. 그래서, 
                                // 서버에서 강제로 공동현관과 접속을 끊고, 공동현관이 새로 접속하게 한다.
                                // 바로 공동현관 리더기와 소켓을 끊으니까, 간혹 공동현관이 접속 이벤트를 보내지 않을 때가 있어, 
                                // 5초 후에 공동현관이 리셋을 할 수 있게, 리셋 명령어를 전송한다.
                                if (!isLobbyId(a_OpDevData.byGid, a_OpDevData.byDid))
                                {
                                    Thread th = new Thread(() => UkrResetThread(a_OpDevData));
                                    th.IsBackground = true;
                                    th.Start();
                                }
                                break;
                            case 0x03:  // 2023-04-04 추가. 출입 인증 실패시 키데이터가 Key_Info_Master에 있으면, 
                                        // 문열림 진행 후 Reg 테이블에 데이터 삽입하여, 자동 등록
                                        // Key ID는 공동현관 리더기에서 암호화를 하여 전달함.
                                {
                                    //MessageBox.Show("출입인증실패");
                                    string strKeyID = "";
                                    if (a_OpDevData.byData.Length == 10)
                                    {
                                        for (int j = 0; j < 8; j++)
                                            strKeyID = strKeyID + a_OpDevData.byData[j + 2].ToString("X2"); // 공동현관 리더기에서 암호화가 된, Key ID를 전송한다.
                                    }

                                    // 공동현관에서 받은 데이터가 정상적으면, 문열림 신호를 보냄.
                                    if (UkrLobbyCheckOpen(a_OpDevData.byGid.ToString("X2"), a_OpDevData.byDid.ToString("X2"), strKeyID))
                                    {
                                        // Reg 테이블에 key 데이터 삽입
                                        List<stKeyInfo> st_KeyInfos = new List<stKeyInfo>();
                                        stKeyInfo st_KeyInfo = new stKeyInfo();

                                        string strQry = string.Format("Select Dong, Ho, Key_Sn, Key_Id From kms.Key_Info_Master where Key_Id = '{0}';", strKeyID);

                                        List<string[]> _qryList = m_mysql.MySqlSelect(strQry, 4);

                                        foreach (string[] _str in _qryList)
                                        {
                                            st_KeyInfo.strDong = _str[0];
                                            st_KeyInfo.strHo = _str[1];
                                            st_KeyInfo.strKeySn = _str[2];
                                            st_KeyInfo.strKeyId = _str[3];

                                        }

                                        st_KeyInfos.Add(st_KeyInfo);
                                        RegTblInsert(st_KeyInfos);

                                        string strLog = string.Format("{0}동, {1}호 ,KeyID: {2}, KeySn: {3} => Reg 테이블 등록",
                                            st_KeyInfo.strDong, st_KeyInfo.strHo, st_KeyInfo.strKeyId, st_KeyInfo.strKeySn);

                                        LogPrint(strLog);
                                    }
                                }
                                break;
                        }
                        break;
                    case 0xC0:  // 명령어 응답
                        switch (a_OpDevData.byCmd)
                        {
                            case 0x01:  // 상태 응답
                                StateCheckRes(a_OpDevData);
                                break;
                            case 0x02:  // Reset 요청 응답
                                ResponseReset(a_OpDevData);
                                break;
                            case 0x03:  // 키 ID 등록 응답
                                KeyIdRegister(a_OpDevData);
                                break;
                            case 0x04:  // 키 ID 삭제 응답
                                KeyIdDelete(a_OpDevData);
                                break;
                            case 0x05:  // 키 ID 리스트 응답
                                
                                break;

                            case 0x06:// 문열림 데이터 응답
                                {
                                    KeyConfirm(a_OpDevData);

                                    string strGid = "", strDid = "";
                                    strGid = a_OpDevData.byData[0].ToString("X2");
                                    strDid = a_OpDevData.byData[1].ToString("X2");

                                    string strKeyId = "";
                                    if (a_OpDevData.byData.Length == 10)
                                    {
                                        for (int j = 0; j < 8; j++)
                                            strKeyId = strKeyId + a_OpDevData.byData[j + 2].ToString("X2");
                                    }

                                    string strLog = string.Format("로비:{0} ,KeyID: {1} 서버 문열림 응답", Program.GetLobbyName(strGid, strDid), strKeyId);
                                    LogPrint(strLog);
                                }

                                break;
                            case 0x08: // BLE 등록 ID 응답(스마트폰에 요청)
                                if (a_OpDevData.byOpt == 0x01 ||
                                    a_OpDevData.byOpt == 0x04)
                                {
                                    BleKeyIdRegister(a_OpDevData);
                                }
                                else
                                {
                                    BleKeyIdRegisterFail(a_OpDevData);
                                }
                                break;
                        }
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("RecvDataProc()에서 예외 발생:" + ex.Message.ToString());
                LogSave("RecvDataProc()에서 예외 발생:" + ex.Message.ToString());
            }
        }

        // 문열림 신호 전송
        private bool UkrLobbyCheckOpen(string strGid, string strDid, string strKeyID)
        {
            if (IsLobbyKeyId(strGid, strDid, "", strKeyID))
            {
                // 로비정보와 KeyID 정보가 맞으면, 문열림 전송
                byte[] byGid = GetStringToHex(strGid);
                byte[] byDid = GetStringToHex(strDid);

                // UKR에 문열림 신호를 전송시 ID를 입력할지, 안할지를 선택함.
                if (Program.g_nSetOpenID == 1)
                {
                    SendUkrOpen(byGid[0], byDid[0], strKeyID, 0x06, 0x02);   // 2023-04-04 서버에서 보낸 key 데이터로 응답을 하기 때문에, 암호화가 적용된 key ID를 전송해야함.
                }
                else
                {
                    SendUkrOpen(byGid[0], byDid[0], "", 0x06, 0x01);   // 2023-04-04 서버에서 보낸 key 데이터로 응답을 하기 때문에, 암호화가 적용된 key ID를 전송해야함.
                }

                string strLog = string.Format("로비:{0} ,KeyID: {1} 서버 문열림", Program.GetLobbyName(strGid, strDid), strKeyID);

                LogPrint(strLog);

                return true;
            }

            return false;
        }


        // 2023-04-04 추가(웹서버에서 데이터를 수신함)
        private void RecvWebData(string strData)
        {
           // MessageBox.Show(strData);

            // 샘플 패킷
            // Major=3840&KeyID=1234567890123456
            string[] strArr = strData.Split('&');
            string[] strUkrId = strArr[0].Split('=');
            string[] strKeyId = strArr[1].Split('=');
            strKeyId[1] = strKeyId[1].Replace("\0", "");

            int num = int.Parse(strUkrId[1]);
            string str = num.ToString("X4");

            string strGid = str.Substring(0, 2);
            string strDid = str.Substring(2, 2);

            //  웹에서 데이터를 받을 때, 암호화가 된 데이터를 받는다.
            if (!UkrLobbyCheckOpen(strGid, strDid, strKeyId[1]))
            {
                // ToDo 로그기록 남김(Ukr ID와 KeyID가 매칭이 되지 않음)
            }
        }

        // UKR ID와 KeyID를 비교하여, UKR에 적용 또는 등록이 되어야 되는 데이터인지를 비교하는 메소드
        private bool IsLobbyKeyId(string strGid, string strDid, string strKeyID, string strEncrypID)
        {
            string strQry = "";

            strQry = "select ma.Dong, ma.Ho, ma.Key_Id, ma.Key_ID, dolo.Lobby_Name, lbid.Lobby_GID, lbid.Lobby_DID " ;
            strQry = strQry + " from kms.Key_Info_Master as ma, kms.Dong_Lobby as dolo, kms.Lobby_ID as lbid " ;
            strQry = strQry + string.Format(" where (ma.Key_Id = '{0}' or ma.Key_Id = '{1}') ", strKeyID, strEncrypID);
            strQry = strQry + " and (ma.Dong = dolo.Dong and ma.Ho = dolo.Ho) ";
            strQry = strQry + " and (dolo.Lobby_Name = lbid.Lobby_Name) ";
            strQry = strQry + string.Format(" and (lbid.Lobby_GID = '{0}' and lbid.Lobby_DID = '{1}');", strGid, strDid);

            bool bRet = Program.isDBData(strQry, 7);
            return bRet;
        }

        // 2023-04-04 공동현관 문열림 보냄.
        private void SendUkrOpen(byte byGid, byte byDid, string strKeyId, byte byCmd, byte byOpt)
        {
            try
            {
                Socket socket = null;
                foreach (DevInfo devInfo in m_listUkrInfo)
                {
                    if ((devInfo.byGid == byGid) && (devInfo.byDid == byDid))
                    {

                        socket = devInfo.socket;
                        break;
                    }
                }

                if (socket == null)
                {
                    // ToDo 로그 출력
                    return;
                }

                byte[] byPacket = MakeProtocol(byGid, byDid, strKeyId, byCmd, byOpt);
                if (byPacket == null)
                {
                    // ToDo 로그 출력
                    return;
                }

                if (Program.IsConnected(socket))
                {
                    socket.Send(byPacket);
                    Program.doDBLogKmsWeb(string.Format("{0:X2},{1:X2}", byGid, byDid), "Send Data = ", BitConverter.ToString(byPacket).Replace('-', ' '));
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("SendUkrOpen() 예외 발생: Ukr ID = "+ string.Format("{0:X2}", "{1:X2}", byGid, byDid) + "   " +ex.Message.ToString());
                LogSave("SendUkrOpen() 예외 발생: Ukr ID = " + string.Format("{0:X2}", "{1:X2}", byGid, byDid) + "   " + ex.Message.ToString());
            }
        }

        private Boolean isLobbyId(byte a_byGid, byte a_byDid)
        {

            foreach (DevInfo devInfo in m_listUkrInfo)
            {
                if ((a_byGid == devInfo.byGid) &&
                    (a_byDid == devInfo.byDid))
                {
                    return true;
                }
            }

            return false;
        }

        // 소켓 세션 관리를 하기 위해, 이벤트 또는 응답 메시지를 받으면, 소켓을 업데이트 한다.(2023-11-21)
        private void DeviceUpdate(byte a_byGid, byte a_byDid, Socket a_socket)
        {
            int nIdx = 0;
            foreach (DevInfo devInfo in m_listUkrInfo)
            {
                if ((devInfo.byGid == a_byGid) &&
                    (devInfo.byDid == a_byDid))
                {
                    if (nIdx >= m_listUkrInfo.Count)    // 예외 처리(오퍼플로어)
                        return;

                    devInfo.socket = a_socket;
                    m_listUkrInfo[nIdx] = devInfo;
                    
                    break;
                }
                nIdx++;
            }
        }

        private void DeviceAdd(OPDevPtrl a_OpDevData)
        {
            DevInfo _devInfo = new DevInfo();

            _devInfo.socket = a_OpDevData.socket;
            _devInfo.byGid = a_OpDevData.byGid;
            _devInfo.byDid = a_OpDevData.byDid;
            _devInfo.nStateCnt = 0;

            int nIdx = 0;
            foreach (DevInfo devInfo in m_listUkrInfo)
            {
                if ((devInfo.byGid == _devInfo.byGid) &&
                    (devInfo.byDid == _devInfo.byDid))
                {
                    //this.LogPrint("DeviceAdd()");
                    DeviceDel(devInfo.socket);
                    break;
                }
                nIdx++;
            }

            if (_devInfo.byGid == 0xFF)
                m_BleRegister = _devInfo;

            ////////////////////////////////
            // Data Send Thread 생성
            Thread hThread1= new Thread(new ParameterizedThreadStart(UkrSendThread));
            hThread1.Start(_devInfo);

            Thread hThread2 = new Thread(new ParameterizedThreadStart(UkrStateThread));
            hThread2.Start(_devInfo);

            m_listUkrInfo.Add(_devInfo);
            StateAdd(_devInfo);

            string _strLobbyName = Program.GetLobbyName(_devInfo.byGid.ToString("X2"), _devInfo.byDid.ToString("X2"));

            string _strLobbyIp = string.Format("{0}.{1}.{2}.{3}", a_OpDevData.byData[4].ToString(), a_OpDevData.byData[5].ToString(), a_OpDevData.byData[6].ToString(), a_OpDevData.byData[7].ToString());

            string _strLog = "";
            if (_devInfo.byGid != 0xFF)
            {
                _devInfo.LobbyName = _strLobbyName;
                _strLog = string.Format("{0} 로비 접속 : Gid:{1}(0x{2}), Did:{3}(0x{4}), IP:{5}",
                    _strLobbyName, _devInfo.byGid, _devInfo.byGid.ToString("X2"), _devInfo.byDid, _devInfo.byDid.ToString("X2"), _strLobbyIp);
            }
            else
            {
                _strLog = string.Format("등록기 접속 : Gid:{0}, Did:{1}, IP:{2}", _devInfo.byGid.ToString("X2"), _devInfo.byDid.ToString("X2"), _strLobbyIp);
            }
            this.LogPrint(_strLog);
            this.LogSave(_strLog);

            // 버전 기록 추가(2023-04-11 기존에는 공동현관 리더기가 접속했을 때, 버전 기록을 남기지 않았어, 기능 추가시에 기존 테이블에 버전 정보를 남기게 추가함.)
            string strVersion = string.Format("V{0:X2}.{1:X2}", a_OpDevData.byData[2], a_OpDevData.byData[3]);
            
            ////////////////////////////////
            // 로비 정보(IP) Update
            UkrIpDBUpdate(_devInfo.byGid, _devInfo.byDid, _strLobbyIp, strVersion);
        }

        private void StateAdd(DevInfo a_devInfo)
        {
            stStateInfo _stStateInfo = new stStateInfo();
            _stStateInfo.byGid = a_devInfo.byGid;
            _stStateInfo.byDid = a_devInfo.byDid;
            _stStateInfo.nStateCnt = 0;

            m_listStateInfo.Add(_stStateInfo);
        }

        private void DeviceDel(OPDevPtrl a_OpDevData)
        {
            DeviceDel(a_OpDevData.socket);
        }

        private void DeviceDel(Socket socket)
        {
            int nIdx = 0;
            foreach (DevInfo devInfo in m_listUkrInfo)
            {
                if ((devInfo.socket == socket))
                {
                    devInfo.socket.Close(0);
                    StateDel(devInfo.byGid, devInfo.byDid);
                    m_listUkrInfo.RemoveAt(nIdx);

                    string _strLobbyName = Program.GetLobbyName(devInfo.byGid.ToString("X2"), devInfo.byDid.ToString("X2"));

                    string _strLog = string.Format("{0} 로비 해제 : Gid:{1}(0x{2}), Did:{3}(0x{4})", _strLobbyName, devInfo.byGid, devInfo.byGid.ToString("X2"), devInfo.byDid,devInfo.byDid.ToString("X2"));
                    this.LogPrint(_strLog);

                    break;
                }
                nIdx++;
            }
        }

        private void StateDel(byte a_byGid, byte a_byDid)
        {
            int nIdx = 0;
            foreach (stStateInfo _st in m_listStateInfo)
            {
                if ((_st.byGid == a_byGid) &&
                    (_st.byDid == a_byDid))
                {
                    m_listStateInfo.RemoveAt(nIdx);
                    break;
                }
                nIdx++;
            }
        }

        // 공동현관 접속시 IP 정보와 접속 시간을 DB에 남김.
        private void UkrIpDBUpdate(byte a_byGid, byte a_byDid, string a_strLobbyIp, string a_strVersion)
        {
            string strQry = "", strTime = "";

            strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strTime = strTime +"(" + a_strVersion + ")";
        	
	        //1. 새로 접속하는 단말기 기록 확인
            strQry = string.Format("select IPAddr from kms.UKR_Connect_Info where Gid='{0}' and Did = '{1}';", a_byGid.ToString("X2"), a_byDid.ToString("X2"));
	        
            bool bRet = Program.isDBData(strQry, 1);

            if (bRet)   // 데이터 있음.
            {
                strQry = string.Format("Update kms.UKR_Connect_Info Set IPAddr = '{0}', ConnectTime = '{1}' where GID = '{2}' and DID = '{3}';", a_strLobbyIp, strTime, a_byGid.ToString("X2"), a_byDid.ToString("X2"));
            }
            else  // 데이터 없음.
            {
                strQry = string.Format("INSERT INTO kms.UKR_Connect_Info(GID,DID,IPAddr,ConnectTime)VALUES('{0}','{1}','{2}','{3}');", a_byGid.ToString("X2"), a_byDid.ToString("X2"), a_strLobbyIp, strTime);
            }

            m_mysql.MySqlExec(strQry, "");
        	
	        //2. 접속을 하면 상태 체크 테이블에 IP기와 Gid 등록
            strQry = string.Format("SELECT IPAddr FROM kms.UKR_State_Info where Gid='{0}' and Did='{1}';", a_byGid.ToString("X2"), a_byDid.ToString("X2"));
            bRet = Program.isDBData(strQry, 1);
            if (bRet)// 데이터 있음.
            {
                strQry = string.Format("Update kms.UKR_State_Info Set IPAddr = '{0}' where GID = '{1}' and DID = '{2}';", a_strLobbyIp, a_byGid.ToString("X2"), a_byDid.ToString("X2"));
            }
            else // 데이터 없음.
            {
                strQry = string.Format("INSERT INTO kms.UKR_State_Info(GID,DID,IPAddr)VALUES('{0}','{1}','{2}');", a_byGid.ToString("X2"), a_byDid.ToString("X2"), a_strLobbyIp);
            }
            m_mysql.MySqlExec(strQry, "");            

        }

        private void UkrDelRegCheckThread(object o)
        {
            string strQry = "";
            while (true)
            {
                strQry = "";
                for (int i = 0; i < m_ukrRegDelCheck.Count; i++)
                {
                    UkrRegDelCheck ukrRegDelCheck = m_ukrRegDelCheck[i];
                    strQry = strQry + string.Format(" SELECT '{0}' AS source FROM kms.{0}", ukrRegDelCheck.m_strRegTblName);
                    if (i == m_ukrRegDelCheck.Count-1) // 마지막 공동현관이면
                    {
                        strQry = strQry + string.Format(" UNION SELECT '{0}' AS source FROM kms.{0} ", ukrRegDelCheck.m_strDelTblName);
                    }
                    else
                    {
                        strQry = strQry + string.Format(" UNION SELECT '{0}' AS source FROM kms.{0} UNION ", ukrRegDelCheck.m_strDelTblName);
                    }
                }

                strQry = strQry + ";";
                //Console.WriteLine("============================================================");
                //Console.WriteLine("strQry = {0}", strQry);

                List<string[]> qryList = m_mysql2.MySqlSelect(strQry, 1);

                // 1. 체크할 Reg, Del 테이블 초기화 함.
                for (int i = 0; i < m_ukrRegDelCheck.Count; i++)
                {
                    m_ukrRegDelCheck[i].m_bCheck = false;
                }

                foreach (string[] str in qryList)
                {
                    // 2. 데이터 있는 것만 true로 바꿈.
                    for (int i = 0; i < m_ukrRegDelCheck.Count; i++)
                    {
                        if ((m_ukrRegDelCheck[i].m_strRegTblName == str[0]) ||
                            (m_ukrRegDelCheck[i].m_strDelTblName == str[0]))
                        {
                            m_ukrRegDelCheck[i].m_bCheck = true;
                            break;
                        }
                    }
                }

                Thread.Sleep(1000 * 10);
            }
        }

        // 키 등록, 삭제, Reset, 동기화, 제어 셋팅값 전달 쓰레드
        private void UkrSendThread(object o)
        {
            DevInfo _UKR = (DevInfo)o;
            string _strGid = "", _strDid = "", _strLBName = "", _strRegTblName = "", _strDelTblName = "";
            _strGid = _UKR.byGid.ToString("X2");
            _strDid = _UKR.byDid.ToString("X2");

            Console.WriteLine("UkrSendThread() Thread[Send 체크 쓰레드] 시작 ==> Gid:{0} Did:{1}", _strGid, _strDid);

            // 1. 로비 이름을 읽음
            string _strQry = string.Format("SELECT na.Lobby_Name, info.Reg_Tbl, info.Del_Tbl FROM Lobby_ID AS na INNER JOIN Lobby_Info AS info ON na.Lobby_Name = info.LobbyName and na.Lobby_GID = '{0}' and na.Lobby_DID = '{1}';", _strGid, _strDid);
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 3);

            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                _strRegTblName = _str[1];
                _strDelTblName = _str[2];
            }

            int nSyncCnt = 0;
            //Console.WriteLine("Gid:" + _strGid + " Did:" + _strDid + " LobbyName = " + _strLBName + " / " + _strRegTblName + " / " + _strDelTblName);

            if (_UKR.socket == null)
            {
                Console.WriteLine("UkrSendThread() Socket이 Null 입니다");
                return;
            }

            _UKR.bReset = false;

            while (Program.IsConnected(_UKR.socket))
            {
                //Console.WriteLine(string.Format("2 => {0} :: Program.IsConnected(_UKR.socket) = {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Program.IsConnected(_UKR.socket)));
                //DB에 로비 네임에 관련된 데이타가 없을 경우
                if (_strDid.Length == 0 || _strRegTblName.Length == 0 || _strDelTblName.Length == 0)
                {
                    Thread.Sleep(1000);
                    continue;
                }

                // 개포 퍼스티어 현장 추가

                // 1. 기기에 리셋을 요청 한다
                /*
                if (_UKR.bReset)
                    RequestReset(_UKR);

                Thread.Sleep(1000);
                */

                // 개포 퍼스티어 현장 과부하로 인해 수정
                // Reg_Del에 데이터가 있을때만 DelTblCheck, RegTblCheck를 함.
                if (!isDelRegCheck(_strGid, _strDid))
                {
                    // 동기화 체크(Reg와 Del 테이블에 데이터가 없을 때만)
                    // 1. Snyc Tbl Check
                    if (++nSyncCnt > 4)    // 20 ==> 개포 퍼스티어는 현장으로 인해 MainThread값에 따라서 동기화 체크 시간 변경
                    {
                        if (SyncTblCheck(_UKR, _strLBName))
                        {
                            continue;
                        }

                        nSyncCnt = 0;
                    }
                    Thread.Sleep(1000 * 5);
                    continue;
                } 
                
                // 2. Del Tab Check(Del Table에 값이 있으면 True
                if (DelTblCheck(_UKR, _strDelTblName))
                {
                    nSyncCnt = 0;
                    Thread.Sleep(1000);
                    continue;
                }
                Thread.Sleep(100);

                // 3. Reg Tbl Check
                if (RegTblCheck(_UKR, _strRegTblName))
                {
                    nSyncCnt = 0;
                    Thread.Sleep(1000);
                    continue;
                }

                Thread.Sleep(1000 * Program.g_nMainThreadTime);
            }


            Console.WriteLine("UkrSendThread() Thread[Send 체크 쓰레드] 종료 ==> Gid:{0} Did:{1}", _strGid, _strDid);
            _UKR.socket.Close();
        }

        private bool isDelRegCheck(string a_strGid, string a_strDid)
        {
            for (int i = 0; i < m_ukrRegDelCheck.Count; i++)
            {
                if (m_ukrRegDelCheck[i].m_strGid == a_strGid &&
                    m_ukrRegDelCheck[i].m_strDid == a_strDid)
                {
                    return m_ukrRegDelCheck[i].m_bCheck;
                }
            }

            return false;
        }

        private void UkrStateThread(object o)
        {
            DevInfo _UKR = (DevInfo)o;
            //Console.WriteLine("UkrStateThread() Thread[상태 체크 쓰레드] 시작 ==> " + "Gid:" + _UKR.byGid.ToString("X2") + " Did:" + _UKR.byDid.ToString("X2"));

            // 접속후 1분 뒤에 동시에 상태 체크를 하고 있어서, 상태체크 시간을 분할 함.(개포 퍼스티어 현장에서 공동현관이 동시에 접속을 하니, 과부하가 걸림)
            int randomValue = GenerateRandomValueInRange(30, 61);
            Console.WriteLine("randomValue = {0}", randomValue);
            Thread.Sleep(1000 * randomValue);

            // ToDo 상태 체크는 UkrConnect와 상관없이 보내면...
            Console.WriteLine("{0}", string.Format("UkrStateThread() Check, LobbyName = ", _UKR.LobbyName));
            Console.WriteLine("{0}", string.Format("Program.IsConnected(_UKR.socket) = ", Program.IsConnected(_UKR.socket)));

            while (Program.IsConnected(_UKR.socket))
            {
                Thread.Sleep(1000 * Program.g_nStateThreadTime);    // 90초마다 상태 체크를 보낸다.
                StateCheck(_UKR);
                Console.WriteLine("{0}", string.Format("UkrStateThread()2 Check, LobbyName = ", _UKR.LobbyName));
            }

            // 상태 체크를 보내지 못하면, 상태를 0으로 함.
            doDBUkrStateInfoValue(_UKR.byGid.ToString("X2"), _UKR.byDid.ToString("X2"), 0);

            Console.WriteLine("UkrStateThread() Thread[상태 체크 쓰레드] 종료 ==> " + "Gid:" + _UKR.byGid.ToString("X2") + " Did:" + _UKR.byDid.ToString("X2"));
        }

        private int GenerateRandomValueInRange(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        public void SendUkrKeyRequest(DevInfo a_UKR)
        {
            Console.WriteLine("SendUkrKeyRequest()");

            byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, "", 0x05, 0x01);
            if (_byPacket == null)
                return;

            if (Program.IsConnected(a_UKR.socket))
            {
                a_UKR.socket.Send(_byPacket);
                Program.doDBLogKmsDev(a_UKR.LobbyName, "Send Data = ", _byPacket);
            }
        }

        // 리셋 타이머 제어
        /*
        public void ResetTimer(bool start)
        {
            if (start)
                _resetTimer.SetTimer(ref m_listUkrInfo);
            else
                _resetTimer.Stop();
        }
        */

        // UKR 리셋
        private void RequestReset(DevInfo a_UKR)
        {
            try
            {
                //this.LogPrint("RequestReset 진입");
                // 연결 되지 않은 경우
                if (Program.IsConnected(a_UKR.socket) == false)
                {
                    return;
                }

                byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, "", 0x02, 0x01);

                // 패킷을 생성 하지 못 한 경우
                if (_byPacket == null)
                {
                    return;
                }

                // 패킷을 보내지 못한 경우{
                if (a_UKR.socket.Send(_byPacket) <= 0)
                {
                    return;
                }

                // 리셋 요청 성공
                this.LogPrint(string.Format("{0} 리셋 요청 : Gid:{1}(0x{1:X2}), Did:{2}(0x{2:X2})", a_UKR.LobbyName, a_UKR.byGid, a_UKR.byDid));

                a_UKR.bReset = false;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Reset 예외 발생: " + ex.Message.ToString());
                LogSave("Reset 예외 발생" + ex.Message.ToString());
            }
        }

        // UKR 리셋 응답
        private void ResponseReset(OPDevPtrl a_OpDevData)
        {
            foreach (DevInfo dev in m_listUkrInfo)
            {
                if (dev.byGid != a_OpDevData.byGid || dev.byDid != a_OpDevData.byDid)
                    continue;

                this.LogPrint(string.Format("{0} 리셋 응답 : Gid:{1}(0x{1:X2}), Did:{2}(0x{2:X2})", dev.LobbyName, dev.byGid, dev.byDid));
            }
        }

        private bool DelTblCheck(DevInfo a_UKR, string a_strDelName)
        {
            try
            {
                string _strQry = string.Format("SELECT Key_Id FROM {0} order by Num asc limit 0,1;", a_strDelName);
                string _strKeyId = "";

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                if (_qryList.Count <= 0)
                    return false;

                foreach (string[] _str in _qryList)
                {
                    _strKeyId = _str[0];
                }

                byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, _strKeyId, 0x04, 0x01);
                if (_byPacket == null)
                    return true;

                if (Program.IsConnected(a_UKR.socket))
                {
                    a_UKR.socket.Send(_byPacket);
                    Program.doDBLogKmsDev(a_UKR.LobbyName, "Send Data = ", _byPacket);
                }

                Thread.Sleep(1000 * 2); // 2초 대기
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("DelTblCheck() 예외 발생: " + ex.Message.ToString());
                LogSave("DelTblCheck() 예외 발생" + ex.Message.ToString());
            }
            return true;
        }

        private bool RegTblCheck(DevInfo a_UKR, string a_strRegName)
        {
            try
            {
                string _strQry = string.Format("SELECT Key_Id FROM {0} order by Num asc limit 0,1;", a_strRegName);
                string _strKeyId = "";

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                if (_qryList.Count <= 0)
                    return false;

                foreach (string[] _str in _qryList)
                {
                    // 2020-04-07 한정태 수정
                    // 사옹자가 Key_Id에 공백만을 넣었을 때, MakeProtocol에서 Null이 리턴이 된다. 
                    // 그래서 공백만이 있을 때는 데이터를 삭제한다.
                    //_strKeyId = _str[0];
                    _strKeyId = _str[0].Trim();
                    if (_strKeyId.Length == 0)
                    {
                        //RegTableWhiteSpaceDate(a_strRegName);
                        _strQry = string.Format("Delete From {0} where Key_Id = '{1}';", a_strRegName, _str[0]);
                        m_mysql.MySqlExec(_strQry);
                        return true;
                    }
                }

                byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, _strKeyId, 0x03, 0x01);
                if (_byPacket == null)
                    return true;

                if (Program.IsConnected(a_UKR.socket))
                {
                    a_UKR.socket.Send(_byPacket);
                    Program.doDBLogKmsDev(a_UKR.LobbyName, "Send Data = ", _byPacket);
                }

                Thread.Sleep(1000 * 2); // 2초 대기
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("RegTblCheck() 예외 발생: " + ex.Message.ToString());
                LogSave("RegTblCheck() 예외 발생" + ex.Message.ToString());
            }

            return true;
        }

        // 2021-05-04 구조 변경
        // Sync 테이블 체크 후 -> 전체 삭제 -> 30초 Sleep을 하면 시스템 공동현관 리더기와 연결된 곳이 문제 될 수 가 있음.
        // 응답후에 Reg 테이블에 데이터 삽입을 Thread로 함.
        private bool SyncTblCheck(DevInfo a_UKR, string a_strLBName)
        {
            try
            {
                string _strQry = string.Format("SELECT Flag FROM Lobby_Sync where LobbyName = '{0}';", a_strLBName);
                string _strFlag = "";

                List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                if (_qryList.Count < 0)
                    return false;

                foreach (string[] _str in _qryList)
                {
                    _strFlag = _str[0];
                }

                if (_strFlag == "1")
                {

                    //전체 삭제 송신
                    byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, "", 0x04, 0x02);
                    if (_byPacket == null)
                        return true;

                    if (Program.IsConnected(a_UKR.socket))
                    {
                        a_UKR.socket.Send(_byPacket);
                        Program.doDBLogKmsDev(a_UKR.LobbyName, "Send Data = ", _byPacket);

                        Program.doDBLogKmsKeyRegDel("Del", a_strLBName, "", "", "전체 삭제 요청");
                    }

                    // 주의) 2021-08-12 기본 로직을 수정함.
                    // 기존에는 전체 삭제 후에 응답이 있을 때만, Flag 값을 변경을 했는데, 응답이 없는 경우 Flag가 계속 1로 되어 있어서
                    // 시스템에 영향이 있는 것 같아서, 전체 삭제를 보낸 후에 Flag 값은 0으로 Update를 하고, 
                    // 응답이 있을 경우. Reg 테이블에 데이터를 삽입한다.
                    _strQry = string.Format("Update kms.Lobby_Sync Set Flag = 0 where LobbyName = '{0}';", a_strLBName);
                    m_mysql.MySqlExec(_strQry);
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("SyncTblCheck() 예외 발생: " + ex.Message.ToString());
                LogSave("SyncTblCheck() 예외 발생" + ex.Message.ToString());
            }
            return false;
        }

        private void StateCheck(DevInfo a_UKR)
        {
            int nStateCnt = 0;

            for (int i = 0; i < m_listStateInfo.Count; i++)
            {
                stStateInfo _stStateInfo = m_listStateInfo[i];

                if ((_stStateInfo.byGid == a_UKR.byGid) &&
                    (_stStateInfo.byDid == a_UKR.byDid))
                {
                    _stStateInfo.nStateCnt++;
                    m_listStateInfo[i] = _stStateInfo;
                    nStateCnt = _stStateInfo.nStateCnt;
                    break;
                }
            }

            string _strLobbyName = Program.GetLobbyName(a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"));

            if (nStateCnt == 5)
            {
                // 경고문
                string _strLog = string.Format("{0} 로비 상태체크 응답 5회 없음. Gid:{1}, Did:{0}", _strLobbyName, a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"));
                this.LogPrint(_strLog);

                doDBUkrStateInfoValue(a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"), 0);
            }

            if (nStateCnt > 10)   // 10회 이상 응답이 없음.
            {
                // UKR 삭제
                string _strLog = string.Format("{0} 로비 상태체크 응답 10회 없음. Gid:{1}, Did:{0}", _strLobbyName, a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"));
                this.LogPrint(_strLog);

                doDBUkrStateInfoValue(a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"), 0);
                DeviceDel(a_UKR.socket);
            }

            // 상태 체크 Send 
            byte[] _byPacket = MakeProtocol(a_UKR.byGid, a_UKR.byDid, "", 0x01, 0x12);
            if (_byPacket == null)
                return;

            if (Program.IsConnected(a_UKR.socket))
            {
                a_UKR.socket.Send(_byPacket);

                // Log_DevState 기록에 남김(보낸 기록)
                //Program.doDBLogKmsDevState(_strLobbyName, "Send Data = ", _byPacket);

                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Ukr_State_Info에 상태 체크를 보낸 시간을 남김.(응답이 없을 때 5회 이상에는 doDBUkrStateInfoValue()메서드에서 Value를 변경함.)
                doDBUkrStateInfoSendTime(a_UKR.byGid.ToString("X2"), a_UKR.byDid.ToString("X2"), strTime);
            }

            return;
        }

        // 2021-04-21 MakeProtocol() 함수 추가 => 공동현관에 Key List가 추가가 되어 메소드 오버라이딩을 함.
        public byte[] MakeProtocol(byte a_byGid, byte a_byDid, string a_strKeyId, byte a_byCmd, byte a_byOpt)
        {
            return MakeProtocol(a_byGid, a_byDid, a_strKeyId, a_byCmd, a_byOpt, 0, 0);
        }

        public byte[] MakeProtocol(byte a_byGid, byte a_byDid, string a_strKeyId, byte a_byCmd, byte a_byOpt, int a_nStartAddr, int a_nCnt)
        {
            try
            {
                byte[] _by = new byte[256];
                byte[] _byRet;
                int _nPacketSize = 0;
                byte[] _byKeyId = new byte[8];
                byte _byCrc = 0x00;

                if (a_strKeyId != "")
                {
                    if (a_strKeyId.Length != 16)
                    {
                        //Console.WriteLine("MakeProtocal Fail => Gid: " + a_byGid.ToString("X2") + " Did: " + a_byDid.ToString("X2") +
                        //                " Cmd: " + a_byCmd.ToString("X2") + " Opt: " + a_byOpt.ToString("X2") +
                        //                " Key Id:" + a_strKeyId);
                        return null;
                    }

                    _byKeyId = GetStringToHex(a_strKeyId);
                }

                _by[0] = 0x02;
                _by[1] = 0x33;
                _by[2] = m_bySeq;
                _by[3] = 0x00;      // Type
                _by[4] = a_byCmd;   // Cmd
                _by[5] = a_byOpt;   // Opt
                switch (a_byCmd)
                {
                    case 0x05:
                        switch (a_byOpt)    // 2021-04-21 추가 공동현관 리더기에 등록된 키 개수 요청
                        {
                            case 0x01:
                                {
                                    _nPacketSize = 15;
                                    _by[6] = 0x06;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;
                                    _by[9] = 0x00;
                                    _by[10] = 0x00;
                                    _by[11] = 0x00;
                                    _by[12] = 0x00;

                                    for (int i = 1; i < 13; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[13] = _byCrc;
                                    _by[14] = 0x03;
                                }
                                break;
                            case 0x02:  // 2021-04-21 추가 공동현관 리더기에 등록된 키 리스트 요청
                                {
                                    _nPacketSize = 15;
                                    _by[6] = 0x06;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;
                                    _by[9] = (byte)(a_nStartAddr / 256);
                                    _by[10] = (byte)(a_nStartAddr % 256);
                                    _by[11] = (byte)(a_nCnt / 256);
                                    _by[12] = (byte)(a_nCnt % 256);

                                    for (int i = 1; i < 13; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[13] = _byCrc;
                                    _by[14] = 0x03;
                                }
                                break;
                        }
                        break;
                    case 0x01:  // 상태 요청 응답
                        switch (a_byOpt)
                        {
                            case 0x12:
                                {
                                    _nPacketSize = 11;
                                    _by[6] = 0x02;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;

                                    for (int i = 1; i < 9; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[9] = _byCrc;
                                    _by[10] = 0x03;
                                }
                                break;
                        }
                        break;
                    case 0x02:
                        {
                            // reset
                            _nPacketSize = 11;
                            _by[6] = 0x02;      //LEN
                            _by[7] = a_byGid;
                            _by[8] = a_byDid;

                            for (int i = 1; i < 9; i++)
                                _byCrc = (byte)(_byCrc ^ _by[i]);

                            _by[9] = _byCrc;
                            _by[10] = 0x03;
                            break;
                        }
                    case 0x03:  // 키 등록
                        {
                            switch (a_byOpt)
                            {
                                case 0x01:
                                    {
                                        _nPacketSize = 19;
                                        _by[6] = 0x0A;  // Len
                                        _by[7] = a_byGid;
                                        _by[8] = a_byDid;
                                        _by[9] = _byKeyId[0];
                                        _by[10] = _byKeyId[1];
                                        _by[11] = _byKeyId[2];
                                        _by[12] = _byKeyId[3];
                                        _by[13] = _byKeyId[4];
                                        _by[14] = _byKeyId[5];
                                        _by[15] = _byKeyId[6];
                                        _by[16] = _byKeyId[7];

                                        for (int i = 1; i < 17; i++)
                                        {
                                            _byCrc = (byte)(_byCrc ^ _by[i]);
                                        }
                                        _by[17] = _byCrc;
                                        _by[18] = 0x03;
                                    }
                                    break;
                            }
                        }
                        break;

                    case 0x04:  // 키 삭제
                        switch (a_byOpt)
                        {
                            case 0x01:  // 키 삭제
                                {
                                    _nPacketSize = 19;
                                    _by[6] = 0x0A;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;
                                    _by[9] = _byKeyId[0];
                                    _by[10] = _byKeyId[1];
                                    _by[11] = _byKeyId[2];
                                    _by[12] = _byKeyId[3];
                                    _by[13] = _byKeyId[4];
                                    _by[14] = _byKeyId[5];
                                    _by[15] = _byKeyId[6];
                                    _by[16] = _byKeyId[7];

                                    for (int i = 1; i < 17; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[17] = _byCrc;
                                    _by[18] = 0x03;
                                }
                                break;
                            case 0x02:  // 키 전체 삭제
                                {
                                    _nPacketSize = 11;
                                    _by[6] = 0x02;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;

                                    for (int i = 1; i < 9; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[9] = _byCrc;
                                    _by[10] = 0x03;
                                }
                                break;
                        }
                        break;
                    case 0x08: // BLE 모듈 등록기

                        switch (a_byOpt)
                        {
                            case 0x01:  // BLE 등록시
                            if (Program.g_nSPKTypeUse == 1) // SPK 등록기 모듈을 사용할 때
                            {
                                _nPacketSize = 11;
                                _by[6] = 0x02;  // Len
                                _by[7] = 0xff;  // Gid: 0xff로 고정
                                _by[8] = 0x00;

                                for (int i = 1; i < 9; i++)
                                {
                                    _byCrc = (byte)(_byCrc ^ _by[i]);
                                }
                                _by[9] = _byCrc;
                                _by[10] = 0x03;
                            }
                            else
                            {
                                // SPK 등록기 사용안할 때
                                _nPacketSize = 13;
                                _by[6] = 0x04;  // Len
                                _by[7] = 0xff;  // Gid: 0xff로 고정
                                _by[8] = 0x00;

                                _by[9] = (byte)(Program.g_appType / 100);
                                _by[10] = (byte)(Program.g_appType % 100);

                                for (int i = 1; i < 11; i++)
                                {
                                    _byCrc = (byte)(_byCrc ^ _by[i]);
                                }
                                _by[11] = _byCrc;
                                _by[12] = 0x03;
                            }
                            break;
                            case 0x04:  // Key 등록시
                            {
                                _nPacketSize = 11;
                                _by[6] = 0x02;  // Len
                                _by[7] = 0xff;  // Gid: 0xff로 고정
                                _by[8] = 0x00;

                                for (int i = 1; i < 9; i++)
                                {
                                    _byCrc = (byte)(_byCrc ^ _by[i]);
                                }
                                _by[9] = _byCrc;
                                _by[10] = 0x03;
                            }
                            break;
                        }      
                        break;
                    case 0x09: // BLE 키 등록 완료
                        _nPacketSize = 11;
                        _by[6] = 0x02;  // Len
                        _by[7] = a_byGid;
                        _by[8] = a_byDid;

                        for (int i = 1; i < 9; i++)
                            _byCrc = (byte)(_byCrc ^ _by[i]);

                        _by[9] = _byCrc;
                        _by[10] = 0x03;
                        break;
                    case 0x06: // 2023-04-04 공동현관 리더기 문열림 추가
                        switch (a_byOpt)
                        {
                            case 0x01:
                                {
                                    _nPacketSize = 11;
                                    _by[6] = 0x02;      //LEN
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;

                                    for (int i = 1; i < 9; i++)
                                        _byCrc = (byte)(_byCrc ^ _by[i]);

                                    _by[9] = _byCrc;
                                    _by[10] = 0x03;
                                }
                                break;
                            case 0x02:
                                {
                                    _nPacketSize = 19;
                                    _by[6] = 0x0A;  // Len
                                    _by[7] = a_byGid;
                                    _by[8] = a_byDid;
                                    _by[9] = _byKeyId[0];
                                    _by[10] = _byKeyId[1];
                                    _by[11] = _byKeyId[2];
                                    _by[12] = _byKeyId[3];
                                    _by[13] = _byKeyId[4];
                                    _by[14] = _byKeyId[5];
                                    _by[15] = _byKeyId[6];
                                    _by[16] = _byKeyId[7];

                                    for (int i = 1; i < 17; i++)
                                    {
                                        _byCrc = (byte)(_byCrc ^ _by[i]);
                                    }
                                    _by[17] = _byCrc;
                                    _by[18] = 0x03;
                                }
                                break;
                        }
                        break;
                }

                _byRet = new byte[_nPacketSize];
                Buffer.BlockCopy(_by, 0, _byRet, 0, _nPacketSize);

                this.m_bySeq++;
                return _byRet;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("MakeProtocal Fail : Msg:" + ex.Message.ToString());
                Console.WriteLine("Gid: " + a_byGid.ToString("X2") + " Did: " + a_byDid.ToString("X2") +
                                " Cmd" + a_byCmd.ToString("X2") + " Opt: " + a_byOpt.ToString("X2") +
                                " Key Id:" + a_strKeyId);

                LogSave("MakeProtocal Fail : Msg:" + ex.Message.ToString());
                LogSave("Gid: " + a_byGid.ToString("X2") + " Did: " + a_byDid.ToString("X2") +
                                " Cmd" + a_byCmd.ToString("X2") + " Opt: " + a_byOpt.ToString("X2") +
                                " Key Id:" + a_strKeyId);


                return null;
            }
        }

        private byte[] GetStringToHex(string a_str)
        {
            if (a_str.Length % 2 != 0)
                return null;

            byte[] _byRet = new byte[a_str.Length / 2];

            for (int i = 0; i < a_str.Length; i = i + 2)
            {
                _byRet[i / 2] = Convert.ToByte(GetStringToInt(a_str.Substring(i, 2)));
            }

            return _byRet;
        }

        ///////////////////////////////////////////////////////////
        // 상태 요청 응답
        private void StateCheckRes(OPDevPtrl a_OpDevData)
        {

            for (int i = 0; i < m_listStateInfo.Count; i++)
            {
                stStateInfo _stStateInfo = m_listStateInfo[i];

                if ((_stStateInfo.byGid == a_OpDevData.byGid) &&
                    (_stStateInfo.byDid == a_OpDevData.byDid))
                {
                    _stStateInfo.nStateCnt = 0;
                    m_listStateInfo[i] = _stStateInfo;

                    string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    doDBUkrStateInfoRecvTime(_stStateInfo.byGid.ToString("X2"), _stStateInfo.byDid.ToString("X2"), strTime, 1);
                    //doDBUkrStateInfoValue(_stStateInfo.byGid.ToString("X2"), _stStateInfo.byDid.ToString("X2"), 1);

                    break;
                }
            }

            return;
        }

        /// <summary>
        /// 스마트 폰에 등록 요청을 하면, 성공적인 BLE Key ID 결과를 응답함.
        /// </summary>
        /// <param name="a_OpDevData"></param>
        private void BleKeyIdRegister(OPDevPtrl a_OpDevData)
        {
            Console.WriteLine("BleKeyIdRegister() ");

            string data = BitConverter.ToString(a_OpDevData.byData, 2).Replace("-", "");

            if (a_OpDevData.byOpt == 0x01) // BLE로 Key ID를 받을 때
            {
                txt_reg_keysn.InvokeIfNeeded(() =>
                    {
                        txt_reg_keysn.Text = data.Substring(16, 8);
                    });
            }


            txt_reg_keyid.InvokeIfNeeded(() =>
                {
                    DualiControl d = new DualiControl();

                    string key = "";

                    key = d.GetSignString(a_OpDevData.byData[9])
                                + d.GetSignString(a_OpDevData.byData[8])
                                + d.GetSignString(a_OpDevData.byData[7])
                                + d.GetSignString(a_OpDevData.byData[6])
                                + d.GetSignString(a_OpDevData.byData[5])
                                + d.GetSignString(a_OpDevData.byData[4])
                                + d.GetSignString(a_OpDevData.byData[3])
                                + d.GetSignString(a_OpDevData.byData[2]);
                    txt_reg_keyid.Text = key.ToUpper();
                });

            if (Program.g_nRegisterDeviceType == 1)
                return;

            byte[] _byPacket = MakeProtocol(m_BleRegister.byGid, m_BleRegister.byDid, "", 0x09, 0x01);  // BLE 키 등록 완료를 보냄.(주의 FMCW 프로토콜과 혼용이 되어 버렸음.)

            // 패킷을 생성 하지 못 한 경우
            if (_byPacket == null)
                return;

            // 패킷을 보내지 못한 경우
            if (m_BleRegister.socket.Send(_byPacket) <= 0)
                return;
        }

        /// <summary>
        /// 스마트 폰에 등록 요청을 할면, 실패 결과를 응답 받음
        /// </summary>
        private void BleKeyIdRegisterFail(OPDevPtrl a_OpDevData)
        {
            MessageBox.Show("등록 ID가 없습니다. ID 확인 후 재 등록 하시십오.");

            byte[] _byPacket = MakeProtocol(m_BleRegister.byGid, m_BleRegister.byDid, "", 0x09, 0x02);

            // 패킷을 생성 하지 못 한 경우
            if (_byPacket == null)
                return;

            // 패킷을 보내지 못한 경우
            if (m_BleRegister.socket.Send(_byPacket) <= 0)
                return;
        }

        ///////////////////////////////////////////////////////////
        // 키 ID 등록 응답
        private void KeyIdRegister(OPDevPtrl a_OpDevData)
        {
            string _strQry = "", _strGid = "", _strDid = "", _strKeyId = "", _strKeySn = "";
            string _strDong = "", _strHo = "", _strLBName = "", _strRegTblName = "";

            _strGid = a_OpDevData.byGid.ToString("X2");
            _strDid = a_OpDevData.byDid.ToString("X2");
            _strKeyId = BitConverter.ToString(a_OpDevData.byData, 2).Replace("-", "");

            //  로비 이름 정보를 읽음
            _strQry = string.Format("SELECT na.Lobby_Name, info.Reg_Tbl FROM Lobby_ID AS na INNER JOIN Lobby_Info AS info ON na.Lobby_Name = info.LobbyName and na.Lobby_GID = '{0}' and na.Lobby_DID = '{1}';", _strGid, _strDid);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                _strRegTblName = _str[1];
            }

            // Dong, Ho 정보를 읽음
            _qryList.Clear();
            _strQry = string.Format("SELECT Dong, Ho, Key_Sn FROM {0} where Key_Id = '{1}';", _strRegTblName, _strKeyId);

            _qryList = m_mysql.MySqlSelect(_strQry, 3);

            foreach (string[] _str in _qryList)
            {
                _strDong = _str[0];
                _strHo = _str[1];
                _strKeySn = _str[2];
            }

            switch (a_OpDevData.byOpt)
            {
                case 0x01:  // Key ID 등록 성공
                case 0x02:  // Key ID가 기존에 등록 되어 있음
                    // Key ID 등록 요청 Log를 남김
                    string strComment = string.Format("Key ID: {0}, Key Sn: {1}", _strKeyId, _strKeySn);
                    Program.doDBLogKmsKeyRegDel("Reg", _strLBName, _strDong, _strHo, strComment);

                    // Reg Tbl에 데이터 삭제
                    // 2020-04-15 공백이 들어간 ID는 키가 등록이 되나, 삭제가 되지 않아서
                    // 기존 Query문에서 Delete From Reg_301_1_A where Key_Id = '1234567890321654'; 에서
                    // like 구문으로 수정
                    _strQry = string.Format("Delete From {0} where Key_Id like '%{1}%' limit 1;", _strRegTblName, _strKeyId);

                    m_mysql.MySqlExec(_strQry);

                    string _strLobbyName = Program.GetLobbyName(_strGid, _strDid);
                    string _strLog = string.Format("{0} 키 등록 성공 : {1}동, {2}호, KeySn:{3}, KeyId:{4}", _strLobbyName, _strDong, _strHo, _strKeySn, _strKeyId);
                    this.LogPrint(_strLog);

                    break;
                case 0x03:  // Key ID 등록 실패
                    {
                        //Console.WriteLine("Key Register Fail ==> Gid: " + a_OpDevData.byGid.ToString("X2") + " Did: " + a_OpDevData.byDid.ToString("X2") +
                        //                " Cmd" + a_OpDevData.byCmd.ToString("X2") + " Opt: " + a_OpDevData.byOpt.ToString("X2") +
                        //                " Key Id:" + _strKeyId);
                    }
                    break;
            }
        }

        ///////////////////////////////////////////////////////////
        // 키 ID 삭제 응답
        private void KeyIdDelete(OPDevPtrl a_OpDevData)
        {
            string _strQry = "", _strGid = "", _strDid = "", _strKeyId = "", _strKeySn = "";
            string _strDong = "", _strHo = "", _strLBName = "", _strDelTblName = "";
            string _strLobbyName = "", _strLog = "";

            _strGid = a_OpDevData.byGid.ToString("X2");
            _strDid = a_OpDevData.byDid.ToString("X2");

            //  로비 이름 정보를 읽음
            _strQry = string.Format("SELECT na.Lobby_Name, info.Del_Tbl FROM Lobby_ID AS na INNER JOIN Lobby_Info AS info ON na.Lobby_Name = info.LobbyName and na.Lobby_GID = '{0}' and na.Lobby_DID = '{1}';", _strGid, _strDid);
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                _strLBName = _str[0];
                _strDelTblName = _str[1];
            }

            switch (a_OpDevData.byOpt)
            {
                case 0x01:
                case 0x02:
                    {
                        _strKeyId = BitConverter.ToString(a_OpDevData.byData, 2).Replace("-", "");

                        // Dong, Ho 정보를 읽음
                        _qryList.Clear();
                        _strQry = string.Format("SELECT Dong, Ho, Key_Sn FROM {0} where Key_Id = '{1}';", _strDelTblName, _strKeyId);
                        _qryList = m_mysql.MySqlSelect(_strQry, 3);

                        foreach (string[] _str in _qryList)
                        {
                            _strDong = _str[0];
                            _strHo = _str[1];
                            _strKeySn = _str[2];
                        }

                        // Key ID 삭제 요청 Log를 남김
                        string strComment = string.Format("Key ID: {0}, Key Sn: {1}", _strKeyId, _strKeySn);
                        Program.doDBLogKmsKeyRegDel("Del", _strLBName, _strDong, _strHo, strComment);

                        // Del Tbl에 데이터 삭제
                        _strQry = string.Format("Delete From {0} where Key_Id = '{1}' limit 1;", _strDelTblName, _strKeyId);
                        m_mysql.MySqlExec(_strQry);

                        _strLobbyName = Program.GetLobbyName(_strGid, _strDid);
                        _strLog = string.Format("{0} 키 삭제 성공 : {1}동, {2}호, KeySn:{3}, KeyId:{4}", _strLobbyName, _strDong, _strHo, _strKeySn, _strKeyId);
                        this.LogPrint(_strLog);
                    }
                    break;
                case 0x03:
                    //Console.WriteLine("Key Delete Fail ==> Gid: " + a_OpDevData.byGid.ToString("X2") + " Did: " + a_OpDevData.byDid.ToString("X2") +
                    //                " Cmd" + a_OpDevData.byCmd.ToString("X2") + " Opt: " + a_OpDevData.byOpt.ToString("X2") +
                    //                " Key Id:" + _strKeyId);
                    break;
                case 0x04:
                    {
                        // All Delete 응답

                        _strLobbyName = Program.GetLobbyName(_strGid, _strDid);
                        _strLog = string.Format("{0} 키 전체 삭제 성공 ", _strLobbyName);
                        this.LogPrint(_strLog);

                        // 전체 삭제 응답 로그 기록
                        Program.doDBLogKmsKeyRegDel("Del", _strLBName, "", "", "전체 삭제 응답");

                        // 동기화 테이블 데이터가 1이면 동기화 작업을 진행하고, 
                        // 0이면 삭제 까지만 함.
                        // 동기화 작업 진행(Reg 테이블에 데이터 삽입)
                        _strQry = string.Format("select Flag from kms.Lobby_Sync  where LobbyName = '{0}' and Flag = 1;", _strLBName);
                        if(m_mysql.IsData(_strQry))
                        {
                            _strQry = string.Format("Update Lobby_Sync Set Flag = 0 where LobbyName = '{0}';", _strLBName);
                            m_mysql.MySqlExec(_strQry);

                            Thread th = new Thread(() => KeySyncExec(_strLBName));
                            th.Start();
                        }
                    }
                    break;
                case 0x05:
                    //Console.WriteLine("Key All Delete Fail ==> Gid: " + a_OpDevData.byGid.ToString("X2") + " Did: " + a_OpDevData.byDid.ToString("X2") +
                    //                " Cmd" + a_OpDevData.byCmd.ToString("X2") + " Opt: " + a_OpDevData.byOpt.ToString("X2"));
                    break;
            }
        }

        private void UkrListKeyRegister(object o)
        {
            stRegisterKeyId _stRegisterKeyId = (stRegisterKeyId)o;
            
            // 1. 패킷을 만듬.
            byte[] _byPacket = new byte[256];
            foreach (string _strKeyId in _stRegisterKeyId.listKeyId)
            {
                _byPacket = MakeProtocol(_stRegisterKeyId.byGid, _stRegisterKeyId.byDid, _strKeyId, 0x03, 0x01);
            }
            if (_byPacket == null)
                return;

            // 2. 소켓을 찾음
            Socket _socket = null;
            string strLobbyName = "";
            foreach (DevInfo devInfo in m_listUkrInfo)
            {
                if ((devInfo.byGid == _stRegisterKeyId.byGid) &&
                    (devInfo.byDid == _stRegisterKeyId.byDid))
                {
                    _socket = devInfo.socket;
                    strLobbyName = Program.GetLobbyName(devInfo.byGid.ToString("X2"), devInfo.byDid.ToString("X2"));
                    break;
                }
            }

            // 3. 데이터 전송
            if (Program.IsConnected(_socket))
            {
                _socket.Send(_byPacket);
                
                Program.doDBLogKmsDev(strLobbyName, "Send Data = ", _byPacket);
            }

            // 4. 다음 등록을 위해 3초 슬립
                Thread.Sleep(3 * 1000);
            
        }

        ////////////////////////////////////////////////////////////
        // 2021-04-21 추가
        // 키 리스트 요청
        public void SendKeyIdListRequest(byte a_byGid, byte a_byDid, int a_nStartAddr, int a_nCnt)
        {
            Console.WriteLine("SendKeyIdListRequest() a_nStartAddr = {0}, a_nCnt = {1}", a_nStartAddr, a_nCnt);
            Thread.Sleep(3 * 1000); // // 공동현관 리더기에서의 응답 시간 때문에 3초간의 Term을 둠.

            // 1. 패킷을 만듬.
            byte[] _byPacket = MakeProtocol(a_byGid, a_byDid, "", 0x05, 0x02, a_nStartAddr, a_nCnt);
	        if (_byPacket == null)
		        return;

            // 2. 소켓을 찾음
            Socket _socket = null;
            foreach (DevInfo devInfo in m_listUkrInfo)
	        {
		        if ((devInfo.byGid == a_byGid) &&
			        (devInfo.byDid == a_byDid))
		        {
			        _socket = devInfo.socket;
			        break;
		        }
	        }

            // 3. 데이터 전송
            if (Program.IsConnected(_socket))
            {
                _socket.Send(_byPacket);
                string strLobbyName = Program.GetLobbyName(a_byGid.ToString("X2"), a_byDid.ToString("X2"));
                Program.doDBLogKmsDev(strLobbyName, "Send Data = ", _byPacket);
            }
        }

        //////////////////////////////////////////////////////////////
        // 키 동기화 작업
        private void KeySyncExec(string a_strLBName)
        {
            try
            {
                string strDong = "", strHo = "", strKeySn = "", strKeyId = "";
                string strQry = string.Format("select Distinct M.Key_Id, M.Dong, M.Ho, M.Key_Sn from kms.Key_Info_Master as M,  kms.Dong_Lobby as D where D.Lobby_Name = '{0}' and D.Dong = M.Dong and D.Ho = M.Ho;", a_strLBName);

                List<string[]> _qryList = m_mysql.MySqlSelect(strQry, 4);

                foreach (string[] _str in _qryList)
                {
                    strKeyId = _str[0];
                    strDong = _str[1];
                    strHo = _str[2];
                    strKeySn = _str[3];

                    strQry = string.Format("Insert Into Reg_{0}(Dong, Ho, Key_Sn, Key_Id, State) values({1},{2},'{3}','{4}',0);"
                                , a_strLBName, strDong, strHo, strKeySn, strKeyId);
                    m_mysql.MySqlExec(strQry);
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("KeySyncExec() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
                LogSave("KeySyncExec() 메소드 예외: " + ex.Message.ToString());
            }
        }



        // 출입 이력 등록
        private void Log_EntryRecord(string a_strDong, string a_strHo, string a_strLobbyName, string a_strKeySn, string a_strKeyId)
        {
            // 분기별 1일 이면, backup 생성
            string strQry = "";
            string str = System.DateTime.Now.ToString("MM-dd");
            if (str == "01-01" ||
                str == "04-01" ||
                str == "07-01" ||
                str == "10-01")
            {
                string strTblName = string.Format("{0}_{1}", System.DateTime.Now.ToString("yyyy_MM"), Program.TBL_NAME_LOG_ENTRYRECORD);

                if (!Program.IsTable(strTblName))
                {
                    // Backup 생성
                    strQry = string.Format("ALTER TABLE kms.Log_EntryRecord RENAME TO  kms.{0};", strTblName);
                    m_mysql2.MySqlExec(strQry);

                    Program.CreateTable(Program.TBL_CREATE_LOG_ENTRYRECORD);
                }
            }

            if (a_strDong == "")
                a_strDong = "0";
            if (a_strHo == "")
                a_strHo = "0";

            strQry = string.Format("Insert kms.Log_EntryRecord(EntryTime, Dong, Ho, LobbyName, KeySn, KeyId) values('{0}', {1}, {2}, '{3}', '{4}','{5}');",
                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), a_strDong, a_strHo, a_strLobbyName, a_strKeySn, a_strKeyId);
            m_mysql2.MySqlExec(strQry);
        }

        ///////////////////////////////////////////////////////////
        // 키 인증 이벤트
        private void KeyConfirm(OPDevPtrl a_OpDevData)
        {
            try
            {
                string strGid = "", strDid = "", strLobbyName = "", strKeyId = "";
                string strDong = "", strHo = "";

                if (m_strDirectionFlag == "0")
                {
                    strGid = a_OpDevData.byData[0].ToString("X2");
                    strDid = a_OpDevData.byData[1].ToString("X2");
                    List<string> listKeyId = new List<string>();

                    if (a_OpDevData.byData.Length == 10)
                    {
                        for (int j = 0; j < 8; j++)
                            strKeyId = strKeyId + a_OpDevData.byData[j + 2].ToString("X2");
                    }

                    listKeyId.Add(strKeyId);
                    strLobbyName = Program.GetLobbyName(strGid, strDid);

                    stCrtCall stCrtCall = new stCrtCall(); // 주의) 엘리베이터의 기본 데이터는 여기서 만들지만, 현장 환경에 대한 검사는 Ev Call 이전에 검사를 한번 한다.

                    stCrtCall.byUkrGid = a_OpDevData.byData[0];
                    stCrtCall.byUkrDid = a_OpDevData.byData[1];
                    stCrtCall.byElGid = 0x00;
                    stCrtCall.byElDid = 0x00;
                    stCrtCall.strKeyId = strKeyId;
                    stCrtCall.nTakeOn = GetTakeOn(stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                    //죽동 현장 현통 목적층 호출이 없는 현장을 위한 임시 코드 추후 예외처리 코딩 필요
                    stCrtCall.nTakeOff = GetTakeOff(stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);// _stCrtCall.nTakeOn;// GetTakeOff(_stCrtCall.byUkrGid, _stCrtCall.byUkrDid, _stCrtCall.strKeyId);
                    stCrtCall.strCarId = GetCarId(stCrtCall.byUkrGid, stCrtCall.byUkrDid);
                    stCrtCall.nDelayTime = GetDelayTime(stCrtCall.byUkrGid, stCrtCall.byUkrDid);
                    stCrtCall.bUkrConfirmFlag = false;
                    stCrtCall.bElCallFlag = false;
                    stCrtCall.bElConfirmFlag = false;
                    stCrtCall.strLobbyName = Program.GetLobbyName(stCrtCall.byUkrGid.ToString("X2"), stCrtCall.byUkrDid.ToString("X2"));

                    string strLog = string.Format("출입 인증({0}): Gid:{1}(0x{2}), Did:{3}(0x{4}), KeyId:{5}",
                        strLobbyName, stCrtCall.byUkrGid, strGid, stCrtCall.byUkrDid, strDid, strKeyId);

                    this.LogPrint(strLog);


                    // 2020-04-14 한정태 수정(로그 기록남기는 것을 수정함)
                    // Crt 폴더에 로그 기록을 남김
                    string strQry = string.Format("select Dong, Ho from Key_Info_Master where Key_Id = '{0}';", strKeyId);

                    List<string[]> qryList = m_mysql.MySqlSelect(strQry, 2);

                    foreach (string[] str in qryList)
                    {
                        strDong = str[0];
                        strHo = str[1];
                    }

                    strLog = string.Format("출입 인증({0}): Gid:{1}(0x{2}), Did:{3}(0x{4}), KeyId:{5}, Dong = {6}, Ho = {7}",
                        strLobbyName, stCrtCall.byUkrGid, strGid, stCrtCall.byUkrDid, strDid, strKeyId, strDong, strHo);

                    this.LogSave(strLog);

                    // 2024-01-15 코오롱 건설 연동을 위해, 출입 기록 이력 데이터 삽입
                    Log_EntryRecord(strDong, strHo, strLobbyName, Program.GetKeySn(strKeyId), strKeyId);

                    Thread _h = new Thread(new ParameterizedThreadStart(CrtCall));

                    _h.IsBackground = true;

                    _h.Start(stCrtCall);
                }
                /////////////////////////////////////////////////////////////////////////////////
                /////////////////////////////////////////////////////////////////////////////////
                // 엘리베이터 리더기가 있어 방향을 체크할 때 사용하는코드
                else if (m_strDirectionFlag == "1")
                {
                    //1. 방향체크
                    // 인증 키 아이디
                    for (int i = 0; i < 8; i++)
                    {
                        strKeyId = strKeyId + a_OpDevData.byData[i + 2].ToString("X2");
                    }

                    // 인증된 공동현관 정보를 찾는다
                    for (int i = 0; i < m_listCrtCallInfo.Count; i++)
                    {
                        if (a_OpDevData.byData[0] != m_listCrtCallInfo[i].byUkrGid)
                            continue;

                        // 인증된 공동현관이 엘리베이터용 인지 출입구 용인지 확인 한다
                        if (m_listCrtCallInfo[i].bElUse == true)
                        {
                            this.LogPrint("출입구용 리더기");

                            // 출입구용 리더기 일 경우
                            // 연동되는 엘리베이터용 리더기를 찾는다.
                            for (int j = 0; j < m_listCrtCallInfo.Count; j++)
                            {
                                bool bFlag = false; // true 이면 루틴적용이 되고, False이면 Continu;
                                if (m_listCrtCallInfo[i].byElGid[0] == m_listCrtCallInfo[j].byUkrGid)
                                    bFlag = true;

                                if (m_listCrtCallInfo[i].byElGid[1] == m_listCrtCallInfo[j].byUkrGid)
                                    bFlag = true;

                                if (m_listCrtCallInfo[i].byElGid[2] == m_listCrtCallInfo[j].byUkrGid)
                                    bFlag = true;

                                if (m_listCrtCallInfo[i].byElGid[3] == m_listCrtCallInfo[j].byUkrGid)
                                    bFlag = true;

                                if (!bFlag)
                                    continue;

                                // 엘리베이터용 리더기에 인증된 키 아이디가 있는지 확인 한다.
                                for (int k = 0; k < m_listCrtCallInfo[j].KeyId.Count; k++)
                                {
                                    // 인증된 아이디가 있으면
                                    // 키를 삭제하고
                                    // 외출 중 이므로 호출 하지 않는다.
                                    if (m_listCrtCallInfo[j].KeyId[k] == strKeyId)
                                    {
                                        m_listCrtCallInfo[j].KeyId.RemoveAll(x => x.Equals(strKeyId));
                                        return;
                                    }
                                }
                                // 인증된 아이디가 있는 경우
                                // 엘리베이터를 호출
                                // 들어오고 있는 중이면 엘리베이터 호출을 한다.
                                stCrtCall stCrtCall = new stCrtCall();

                                stCrtCall.byUkrGid = a_OpDevData.byData[0];
                                stCrtCall.byUkrDid = a_OpDevData.byData[1];
                                stCrtCall.byElGid = 0x00;
                                stCrtCall.byElDid = 0x00;
                                stCrtCall.strKeyId = strKeyId;
                                stCrtCall.nTakeOn = GetTakeOn(stCrtCall.byUkrGid, stCrtCall.byUkrDid,ref stCrtCall.strTakeOn);
                                //죽동 현장 현통 목적층 호출이 없는 현장을 위한 임시 코드 추후 예외처리 코딩 필요
                                stCrtCall.nTakeOff = GetTakeOff(stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);// _stCrtCall.nTakeOn;// GetTakeOff(_stCrtCall.byUkrGid, _stCrtCall.byUkrDid, _stCrtCall.strKeyId);
                                stCrtCall.strCarId = GetCarId(stCrtCall.byUkrGid, stCrtCall.byUkrDid);
                                stCrtCall.nDelayTime = GetDelayTime(stCrtCall.byUkrGid, stCrtCall.byUkrDid);
                                stCrtCall.bUkrConfirmFlag = false;
                                stCrtCall.bElCallFlag = false;
                                stCrtCall.bElConfirmFlag = false;

                                strGid = a_OpDevData.byData[0].ToString("X2");
                                strDid = a_OpDevData.byData[1].ToString("X2");

                                string strLog = string.Format("출입 인증({0}): Gid:{1}, Did:{2}, KeyId:{3}", Program.GetLobbyName(strGid, strDid), strGid, strDid, strKeyId);

                                this.LogPrint(strLog);

                                Thread _h = new Thread(new ParameterizedThreadStart(CrtCall));

                                _h.IsBackground = true;

                                _h.Start(stCrtCall);
                            }
                        }
                        else
                        {
                            // 엘리베이터용 리더기 일 경우
                            // 인증된 키 아이디를 리스트에 저장한다.
                            this.LogPrint(string.Format("엘리베이터용 리더기 / {0}", a_OpDevData.byData[0]));
                            m_listCrtCallInfo[i].KeyId.Add(strKeyId);

                            return;
                        }
                    }
                }

                // 2023-10-25 코오롱 베니트현장 연동으로 출입 히스토리를 남김
                Program.doDBLogKmsUkr(Program.GetLobbyName(strGid, strDid), strKeyId);
                
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("KeyConfirm()에서 예외 발생 : " + ex.Message.ToString());
                LogSave("KeyConfirm()에서 예외 발생 : " + ex.Message.ToString());
            }
        }


        private int GetTakeOn(byte a_byGid, byte a_byDid, ref string strTakeOn)
        {
            strTakeOn = "";
            string _strTakeOn = "", _strCarConfig = "";
            string _strQry = string.Format("select Take_On, Car_Config from Lobby_Crt_Info where UKR_GID = '{0}' and UKR_DID = '{1}';", a_byGid.ToString("X2"), a_byDid.ToString("X2"));

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                _strTakeOn = _str[0];
                _strCarConfig = _str[1];
            }

            string[] _strArFloor = GetArFloor(_strCarConfig);

            int _nRet = 0;
            for (int i = 0; i < _strArFloor.Length; i++)
            {
                _nRet++;
                if (_strTakeOn == _strArFloor[i])
                {
                    strTakeOn = _strTakeOn;
                    break;
                }
            }

            return _nRet;
        }

        // 2023-08-17추가 DL 남양주 뉴타운 현장에서 공동현관리더기 1대에서, 엘리베이터 호기가 나눠지고, 구성이 틀릴때 
        // 사용하는 함.
        private int GetTakeOn(int a_nCarID, byte a_byGid, byte a_byDid, ref string strTakeOn)
        {
            string _strTakeOn = "", _strCarConfig = "";
            string _strQry = string.Format("select Take_On, Car_Config from Lobby_Crt_Info where UKR_GID = '{0}' and UKR_DID = '{1}' and EL_ID = {2};", a_byGid.ToString("X2"), a_byDid.ToString("X2"), a_nCarID);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                _strTakeOn = _str[0];
                _strCarConfig = _str[1];
            }

            if (_strTakeOn == "")
            {
                return GetTakeOn(a_byGid, a_byDid,ref strTakeOn);
            }

            string[] _strArFloor = GetArFloor(_strCarConfig);

            int _nRet = 0;
            for (int i = 0; i < _strArFloor.Length; i++)
            {
                _nRet++;
                if (_strTakeOn == _strArFloor[i])
                {
                    strTakeOn = _strTakeOn;
                    break;
                }
            }

            return _nRet;
        }


        //////////////////////////////////////
        // 엘리베이터 층구성 배열 분할
        private string[] GetArFloor(string a_strCarConfig)
        {
            string[] _ar = a_strCarConfig.Split('-');
            return _ar;
        }

        private Boolean isKeyInfoMasterFieldTypeCheck() // 코드를 정상적으로 해야함.
        {
            
            string _strQry = "";
            // 컬럼 개수를 알수 없어서 컬럼 개수를 먼저 구함.

            _strQry = string.Format("SELECT COUNT(*) FROM information_schema.columns  WHERE table_schema = 'kms'   AND table_name = 'Key_Info_Master'  AND column_name = 'key_type';");
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
            int nCnt = 0;

            foreach (string[] _str in _qryList)
            {
                nCnt = int.Parse(_str[0]);
            }

            if (nCnt == 0)
                return false;

            return true;
        }

        // 키 Type 체크(Key_Type 컬럼이 있는지도 체크 한다.)
        private Boolean isKeyTypeCheck(string a_strKeyId)
        {
            if (Program.g_nKeyTypeUse == 0) // Key_Type을 사용하지 않게 Config를 설정하면, 목적층을 호출하지 않음.
                return false;

            if (!isKeyInfoMasterFieldTypeCheck())
                return false;

            string _strQry = "";
            // Key_Type이 있으면, Key_type 값을 읽어 온다.
            _strQry = string.Format("Select Key_type From kms.Key_Info_Master where Key_Id = '{0}';", a_strKeyId);
            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);
            string strKeyType = "";

            foreach (string[] _str in _qryList)
            {
                strKeyType = _str[0];
            }

            if (strKeyType == "1"  || strKeyType == "3")
            {
                return true;
            }

            return false;
        }


        //////////////////////////////////////
        // 목적층 찾기
        private int GetTakeOff(byte a_byGid, byte a_byDid, string a_strKeyId, ref string strTakeOff)
        {
            strTakeOff = "";
            // 2021-04-19 수정. 과천 SK현장으로 인해, BLE 키는 목적층을 호출하고, 스마트폰은 목적층을 호출하지 않음.
            // 목적층 호출이 셋팅(m_strTakeOffFlag=1)이 되어 있으면, 목적층 호출을 하고, 
            // 목적층 호출이 셋팅 되어 있지 않으면(m_strTakeOffFlag=0)서 g_nKeyTypeUse=1로 셋팅이 되어 있으면, Key_Info_master에서 Field를 읽어
            // Key_type이 있을 때, 값이 RF키/BLE 키(value = 1,3)이면 이면, 목적층 호출을 함.
            // Field에 Key_type이 없으면, 목적층 호출을 하지 않음.

            // 쌍용 건설 추가(2023-08-10)
            // 필드 타입이 있는지와 과 Key_Type 값 체크
            // Key Type이 BLE 키인지 확인 ==> 스마트폰 타입(3)이 아니면 목적층을 호출하지 않음.
            // KeyType:1 => RF 키 or BLE 키
            // KeyType:2 => 스마트폰
            // KeyType:3 => 스마트폰에서 원격으로 E/V 목적층 호출할 수 있게 셋팅
            if (!isKeyTypeCheck(a_strKeyId))    // Key_Type이 없거나 셋팅이 되어 있지 않으면, 
            {
                if (m_strTakeOffFlag == "0")   // 목적층을 사용하지 않음
                {
                    return 0;
                }
            }

            string _strQry = "", _strHo = "", _strCarConfig = ""; ;
            int _nRet = 0, _nLoop = 0;

            _nLoop = 0;
            _strQry = string.Format("select Ho from Key_Info_Master where Key_Id = '{0}';", a_strKeyId);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strHo = _str[0];
            }

            // 예외 처리
            if (_strHo == "")
                return 0;

            // 2020-08-13 신촌 힐스테이트에서 마스터 카드 사용시 목적층이 최상층으로 호출이됨.
            // 마스터 카드 사용시에는 목적층을 0로 하여 호출
            if (int.Parse(_strHo) == 9999)
                return 0;


            _strQry = string.Format("select Car_Config from Lobby_Crt_Info where UKR_GID = '{0}' and UKR_DID = '{1}';", a_byGid.ToString("X2"), a_byDid.ToString("X2"));

            _qryList.Clear();
            _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strCarConfig = _str[0];
            }

            if (_strCarConfig == "")
            {
                return int.Parse(strTakeOff);
            }

            string[] _strArFloor = GetArFloor(_strCarConfig);

            for (int j = 0; j < _strArFloor.Length; j++)
            {
                _nLoop++;
                if ((int.Parse(_strHo) / 100).ToString() == _strArFloor[j])
                {
                    strTakeOff = _strArFloor[j];
                    break;
                }
            }

            _nRet = _nLoop;

            return _nRet;
        }

        private int GetTakeOff(int a_nCarID, byte a_byGid, byte a_byDid, string a_strKeyId, ref string strTakeOff)
        {
            strTakeOff = "";
            // 2021-04-19 수정. 과천 SK현장으로 인해, BLE 키는 목적층을 호출하고, 스마트폰은 목적층을 호출하지 않음.
            // 목적층 호출이 셋팅(m_strTakeOffFlag=1)이 되어 있으면, 목적층 호출을 하고, 
            // 목적층 호출이 셋팅 되어 있지 않으면(m_strTakeOffFlag=0)서 g_nKeyTypeUse=1로 셋팅이 되어 있으면, Key_Info_master에서 Field를 읽어
            // Key_type이 있을 때, 값이 RF키/BLE 키(value = 1,3)이면 이면, 목적층 호출을 함.
            // Field에 Key_type이 없으면, 목적층 호출을 하지 않음.

            // 쌍용 건설 추가(2023-08-10)
            // 필드 타입이 있는지와 과 Key_Type 값 체크
            // Key Type이 BLE 키인지 확인 ==> 스마트폰 타입(3)이 아니면 목적층을 호출하지 않음.
            // KeyType:1 => RF 키 or BLE 키
            // KeyType:2 => 스마트폰
            // KeyType:3 => 스마트폰에서 원격으로 E/V 목적층 호출할 수 있게 셋팅
            if (!isKeyTypeCheck(a_strKeyId))    // Key_Type이 없거나 셋팅이 되어 있지 않으면, 
            {
                if (m_strTakeOffFlag == "0")   // 목적층을 사용하지 않음
                {
                    return 0;
                }
            }

            string _strQry = "", _strHo = "", _strCarConfig = ""; ;
            int _nRet = 0, _nLoop = 0;

            _nLoop = 0;
            _strQry = string.Format("select Ho from Key_Info_Master where Key_Id = '{0}';", a_strKeyId);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strHo = _str[0];
            }

            // 2020-08-13 신촌 힐스테이트에서 마스터 카드 사용시 목적층이 최상층으로 호출이됨.
            // 마스터 카드 사용시에는 목적층을 0로 하여 호출
            if (int.Parse(_strHo) == 9999)
                return 0;


            _strQry = string.Format("select Car_Config from Lobby_Crt_Info where UKR_GID = '{0}' and UKR_DID = '{1}' and EL_ID = {2};", a_byGid.ToString("X2"), a_byDid.ToString("X2"), a_nCarID);

            _qryList.Clear();
            _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strCarConfig = _str[0];
            }

            string[] _strArFloor = GetArFloor(_strCarConfig);

            for (int j = 0; j < _strArFloor.Length; j++)
            {
                _nLoop++;
                if ((int.Parse(_strHo) / 100).ToString() == _strArFloor[j])
                {
                    strTakeOff = _strArFloor[j];
                    break;
                }
            }

            _nRet = _nLoop;

            return _nRet;
        }

        ///////////////////////////////////////////////////
        // 엘리베이터 호기 찾기
        private string GetCarId(byte a_byGid, byte a_byDid)
        {
            string _strCarId = "";
            for (int i = 0; i < m_listCrtCallInfo.Count; i++)
            {
                stCrtCallInfo _st = m_listCrtCallInfo[i];

                if ((_st.byUkrGid == a_byGid) &&
                    (_st.byUkrDid == a_byDid))
                {
                    _strCarId = _st.strCrtId; // 호기 정보
                    break;
                }
            }
            return _strCarId;
        }

        private int GetDelayTime(byte a_byGid, byte a_byDid)
        {
            int delayTime = 0;

            for (int i = 0; i < m_listCrtCallInfo.Count; i++)
            {
                stCrtCallInfo _st = m_listCrtCallInfo[i];

                if ((_st.byUkrGid == a_byGid) &&
                    (_st.byUkrDid == a_byDid))
                {
                    return delayTime = _st.nDelayTime; // 호출 딜레이
                }
            }
            return delayTime;
        }

        //////////////////////////////////////////////////////
        // 동호수로 E/V Car 찾기
        // 호기가 2개가 구성이 되어 있어도, 첫번째 Car만 찾음.
        private int GetCarIdFromDongHo(int a_nIdx, int a_nDong, int a_nHo, int a_nCarId)
        {
            int nRetCarId = 0;
            string _strQry = "";
            _strQry = string.Format("SELECT CarId FROM upis.Ele_Car_Info where Dong = {0} and Ho = {1}  order by CarId limit {2},1;", a_nDong, a_nHo, a_nIdx);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                nRetCarId = int.Parse(_str[0]);
                break;
            }

            // 예외 처리 ==> upis.Ele_Car_Info 테이블이 없거나, 테이블 안에 데이터가 없으면, 
            // 파라미터로 받은 CarID를 리턴함.
            if (nRetCarId == 0) 
            {
                nRetCarId = a_nCarId;
            }

            return nRetCarId;
        }

        
        //////////////////////////////////////////////////////
        // 2023-07-20: kms.Dong_Crt_CarID 테이블 참조(동호 Call이고, 그룹으로 구성이 되어 있으면 참조함)
        private string GetCarIdFromDongHoGroup(int a_nDong, int a_nHo)
        {
            string strRetCarId = "";
            string _strQry = "";
            _strQry = string.Format("SELECT EV_CarID FROM kms.Dong_Crt_CarID where Dong = {0} and Ho = {1} limit 0,1;", a_nDong, a_nHo);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                strRetCarId = _str[0];
                break;
            }
            return strRetCarId;
        }

        private void CrtCall(object a_o)
        {
            stCrtCall stCrtCall = (stCrtCall)a_o;

            string[] strCrtId = stCrtCall.strCarId.Split('-');

            Thread.Sleep(stCrtCall.nDelayTime);

            bool bCrtCallRet = false;
            bool bCrtCallRetSub = false;
            bool bCrtCallRetThird = false;
            bool bCrtCallRetForth = false;

            int nDong = 0;
            int nHo = 0;

            int nRetCarNum = 0;
            string strRetCarNum = "";   // 2023-07-20 생성: DongHo Call시 사용

            int nLoopCnt = strCrtId.Length;

            for (int i = 0; i < nLoopCnt; i++)
            {
                try
                {

                    switch (Program.g_CrtType)
                    {
                        case "OTS":
                            {
                                //OTIS버젼
                                Program.GetDongHo(stCrtCall.strKeyId, ref nDong, ref nHo);
                                // 2023-08-17 오티스 엘리베이터는 호기 정보가 없지만, DL 뉴타운 현장처럼 구성이 되면, 호기 정보가 필요하다. 
                                // 그래서, Config파일에서 DongHoCall이 셋팅이 되어야하고, upis.Ele_Car_Info 테이블이 구성이 되어야 한다.
                                if (Program.g_nEv_CarConfig_Use == 1)
                                {
                                    int carId = GetCarIdFromDongHo(i, nDong, nHo, int.Parse(strCrtId[i]));
                                    stCrtCall.nTakeOn = GetTakeOn(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                                    stCrtCall.nTakeOff = GetTakeOff(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);
                                }

                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff), stCrtCall.nDelayTime);

                                nLoopCnt = 1; // OTS 구조는 그룹카 일때도 패킷을 한번만 보내면 되기 때문에 루프 카운트를 조절함.

                                break;
                            }
                        case "Hyundai": //현대 통신 버젼
                        case "Hyundai_High":    // 현대 엘리베이터 고층
                            {
                                List<int> listCarId = new List<int>();  // 2023-07-20 이진베이 현장에서 요청으로 변경.. DongHo Call시 엘리베이터가 여려개가 
                                                                        // 묶여 있어서, upis.Ele_Car_Info에서 호기 정보를 가져왔어, 리스트화 시킴
                                int car = 0;
                                car = nRetCarNum = int.Parse(strCrtId[i]);

                                Program.GetDongHo(stCrtCall.strKeyId, ref nDong, ref nHo);

                                if (Program.g_nCrtCallDongHo == 1)
                                {
                                    // Master 키일 경우 동/호를 사용하지 않고, UKR ID로 CarID 결정
                                    if (nDong == 9999 && nHo == 9999)
                                    {
                                        listCarId.Add(car);
                                    }
                                    else
                                    {
                                        if (!(nDong == 0 && nHo == 0))  // 2023-04-04 추가: 만약 동호수가 0동 0호 이면 코드 위에서 결정된 호기를 그대로 사용한다.
                                        {
                                            if (Program.g_nCrtCallDongHoGroup != 1)
                                            {
                                                car = nRetCarNum = GetCarIdFromDongHo(i, nDong, nHo, car);    // 2020-08-12 UKR이 한대 일때, E/V가 두대일 경우 동호수로 Car 호출
                                                listCarId.Add(car);
                                            }
                                            else
                                            {
                                                strRetCarNum = GetCarIdFromDongHoGroup(nDong, nHo);
                                                string[] strArCarId = strRetCarNum.Split('-');
                                                foreach (string strCarId in strArCarId)
                                                {
                                                    if(strCarId != "")  // 데이터가 있을 때만, 리스트에 추가함.
                                                        listCarId.Add(int.Parse(strCarId));
                                                }
                                            }
                                        }
                                    }
                                }
                                else // 동호 Call이 아닐때
                                {
                                    listCarId.Add(car);
                                }

                                // CRT서버가 한대 인 경우
                                foreach(int carId in listCarId)
                                {
                                    nRetCarNum = carId;
                                    
                                    // 2023-08-17 DL 대림 남양주 뉴타운 현장으로 인해, Lobby_Crt_Info 테이블에서 CarConfig를 별도로 관리하고, 
                                    // Lobby_Name과 UKR_GID, DID가 중복이 됨.
                                    if (Program.g_nEv_CarConfig_Use == 1)
                                    {
                                        stCrtCall.nTakeOn = GetTakeOn(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                                        stCrtCall.nTakeOff = GetTakeOff(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);
                                    }

                                    if (m_nCrtSubUse == 0)  // Crt가 첫번째만 사용
                                    {
                                        bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                        Thread.Sleep(200);
//                                        break;  // E/V 그룹중에 한대만 호출한다. ==> 전체 호출하게 수정
                                    }
                                    else
                                    {
                                        if (m_nCrtThirdUse == 0 && m_nCrtForthUse == 0) // 개포 퍼스티어 아이파크 현장 이전에 사용하는 코드 Crt 서버가 2대가 있을 때.
                                        {
                                            if (carId < m_nCrtSubCarStart)  //첫번째 Crt 호출
                                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                            else
                                            {
                                                // carId : 23 / carstart = 23
                                                // 보조 CRT의 1번 호출
                                                // carId = (carId+1) - carstart
                                                // carId = (23+1) - 23 = 1    
                                                // 수정 창원 현장에서 Update 하고 테스트를 하니 Sub는 1호기 부터 시작하는 것이 아니라, 순차적으로 진행이 됨.
                                                if (Program.g_strSubCrtCarFlow == "0")
                                                {
                                                    int carTemp = (carId + 1) - m_nCrtSubCarStart;
                                                    bCrtCallRetSub = m_hCrtSub.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carTemp), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                                else
                                                {   // 창원태영은 이것이 적용이 되어야 함.
                                                    bCrtCallRetSub = m_hCrtSub.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                            }
                                        }
                                        else // Crt 서버가 3대 이상이면(개포 퍼스티어 아이파크 현장 부터 추가가 됨)
                                        {
                                            if (carId < m_nCrtSubCarStart)  // 첫번째 Crt
                                            {
                                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                            }
                                            else if (carId < m_nCrtThirdCarStart)  // 두번째 Crt
                                            {
                                                if (Program.g_strSubCrtCarFlow == "0")
                                                {
                                                    int carTemp = (carId + 1) - m_nCrtSubCarStart;
                                                    bCrtCallRetSub = m_hCrtSub.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carTemp), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                                else
                                                {   // 창원태영은 이것이 적용이 되어야 함.
                                                    bCrtCallRetSub = m_hCrtSub.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                            }
                                            else if (carId < m_nCrtForthCarStart)  // 세번째 Crt
                                            {
                                                if (Program.g_strSubCrtCarFlow == "0")
                                                {
                                                    int carTemp = (carId + 1) - (m_nCrtThirdCarStart);
                                                    bCrtCallRetThird = m_hCrtThird.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carTemp), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                                else
                                                {   // 창원태영은 이것이 적용이 되어야 함.
                                                    bCrtCallRetThird = m_hCrtThird.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                            }
                                            else // 네번째 Crt
                                            {
                                                if (Program.g_strSubCrtCarFlow == "0")
                                                {
                                                    int carTemp = (carId + 1) - (m_nCrtForthCarStart);
                                                    bCrtCallRetForth = m_hCrtForth.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carTemp), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                                else
                                                {   // 창원태영은 이것이 적용이 되어야 함.
                                                    bCrtCallRetThird = m_hCrtForth.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(carId), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                                }
                                            }
                                        }
                                    }
                                }

                                if (Program.g_nCrtCallDongHoGroup == 1)
                                {
                                    // 이진베이 현장은 list에 merge를 하여, 엘리베이터를 호출하기 때문에, 한번 호출 하고 Loop 종료를 해야함.
                                    // Loop 문들 종료 하기 위해 i값을 증가 시킴
                                    i = nLoopCnt; 
                                }

                                break;
                            }
                        case "Thyssen":
                            {
                                // 2023-08-17 DL 대림 남양주 뉴타운 현장으로 인해, Lobby_Crt_Info 테이블에서 CarConfig를 별도로 관리하고, 
                                // Lobby_Name과 UKR_GID, DID가 중복이 됨.
                                if (Program.g_nEv_CarConfig_Use == 1)
                                {
                                    stCrtCall.nTakeOn = GetTakeOn(int.Parse(strCrtId[i]), stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                                    stCrtCall.nTakeOff = GetTakeOff(int.Parse(strCrtId[i]), stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);
                                }

                                Program.GetDongHo(stCrtCall.strKeyId, ref nDong, ref nHo);
                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(int.Parse(strCrtId[i])), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff), stCrtCall.nDelayTime);
                                break;
                            }
                        case "Power":
                            {
                                //OTIS와 프로토콜 형태 동일
                                Program.GetDongHo(stCrtCall.strKeyId, ref nDong, ref nHo);
                                // 2023-08-17 오티스 엘리베이터는 호기 정보가 없지만, DL 뉴타운 현장처럼 구성이 되면, 호기 정보가 필요하다. 
                                // 그래서, Config파일에서 DongHoCall이 셋팅이 되어야하고, upis.Ele_Car_Info 테이블이 구성이 되어야 한다.
                                if (Program.g_nEv_CarConfig_Use == 1)
                                {
                                    int carId = GetCarIdFromDongHo(i, nDong, nHo, int.Parse(strCrtId[i]));
                                    stCrtCall.nTakeOn = GetTakeOn(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                                    stCrtCall.nTakeOff = GetTakeOff(carId, stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);
                                }
                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff), stCrtCall.nDelayTime);
                                break;
                            }
                        case "Han":
                            {
                                // 2023-08-17 DL 대림 남양주 뉴타운 현장으로 인해, Lobby_Crt_Info 테이블에서 CarConfig를 별도로 관리하고, 
                                // Lobby_Name과 UKR_GID, DID가 중복이 됨.
                                if (Program.g_nEv_CarConfig_Use == 1)
                                {
                                    stCrtCall.nTakeOn = GetTakeOn(int.Parse(strCrtId[i]), stCrtCall.byUkrGid, stCrtCall.byUkrDid, ref stCrtCall.strTakeOn);
                                    stCrtCall.nTakeOff = GetTakeOff(int.Parse(strCrtId[i]), stCrtCall.byUkrGid, stCrtCall.byUkrDid, stCrtCall.strKeyId, ref stCrtCall.strTakeOff);
                                }

                                Program.GetDongHo(stCrtCall.strKeyId, ref nDong, ref nHo);
                                bCrtCallRet = m_hCrt.CrtCall(stCrtCall.strLobbyName, nDong, nHo, Convert.ToByte(int.Parse(strCrtId[i])), Convert.ToByte(stCrtCall.nTakeOn), Convert.ToByte(stCrtCall.nTakeOff));
                                break;
                            }
                    }
                }
                catch (System.Exception ex)
                {
                    LogPrint("CrtCall() 예외 발생:" + ex.Message.ToString());
                }
                Thread.Sleep(100);
            }

            if (m_nCrtSubUse == 0)
            {
                if (bCrtCallRet)
                {
                    if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                    {
                        LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn );
                    }
                    else // 목적 층 사용
                    {
                        LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                    }
                    
                }
                else
                { // 접속했을 때와 안했을 때를 구분하여 로그 출력
                    if (!m_hCrt.IsCrtConnected())
                    {
                        // Crt 서버에 접속 불량시 로그 출력 후 재접속
                        LogPrint(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                        m_hCrt.CrtConnection();

                        // DB에 Log 저장(엘리베이터 호출 실패)
                        string strCommnet = string.Format(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                        Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                    }
                    else
                    {
                        // E/V 호출 실패 
                        LogPrint(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                        m_hCrt.CrtConnection();

                        // DB에 Log 저장(엘리베이터 호출 실패)
                        string strCommnet = string.Format(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                        Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                    }

                }
            }
            else
            {
                if (m_nCrtThirdUse == 0 && m_nCrtForthUse == 0) // Crt가 두대만 있을 때...
                {
                    if (nRetCarNum < m_nCrtSubCarStart)  // 메인 Crt 서버 로그기록
                    {
                        if (bCrtCallRet)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        { // DoDo 수정해야..함... 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrt.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                                m_hCrt.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrt.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }

                        }
                    }
                    else // 서버 Crt 서버 로그 기록
                    {
                        if (bCrtCallRetSub)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + "(Sub) / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + "(Sub) / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        { // DoDo 수정해야..함... 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrtSub.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버(Sub) 접속 불량(E/V 호출 실패)");
                                m_hCrtSub.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버(Sub) 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "(Sub) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrtSub.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "(Sub) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                        }
                    }
                }
                else // Crt가 세대 이상일 때
                {
                    if (nRetCarNum < m_nCrtSubCarStart)  // 첫번째 Crt
                    {
                        if (bCrtCallRet)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + " / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        { // DoDo 수정해야..함... 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrt.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                                m_hCrt.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrt.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }

                        }
                    }
                    else if (nRetCarNum < m_nCrtThirdCarStart)  // 두번째 Crt
                    {
                        if (bCrtCallRetSub)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + "(Sub) / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + "(Sub) / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        { // DoDo 수정해야..함... 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrtSub.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버(Sub) 접속 불량(E/V 호출 실패)");
                                m_hCrtSub.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버(Sub) 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "(Sub) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrtSub.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "(Sub) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                        }
                    }
                    else if (nRetCarNum < m_nCrtForthCarStart)  // 세번째 Crt
                    {
                        if (bCrtCallRetThird)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + "(Third) / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + "(Third) / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        {   // 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrtThird.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버(Third) 접속 불량(E/V 호출 실패)");
                                m_hCrtThird.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버(Third) 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "(Third) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrtThird.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "(Third) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                        }
                    }
                    else // 네번째 Crt
                    {
                        if (bCrtCallRetForth)
                        {
                            if (stCrtCall.strTakeOff == "")    // 목적층 사용안함
                            {
                                LogPrint(Program.g_CrtType + "(Forth) / On(탑승층) :" + stCrtCall.strTakeOn);
                            }
                            else // 목적 층 사용
                            {
                                LogPrint(Program.g_CrtType + "(Forth) / On(탑승층) :" + stCrtCall.strTakeOn + " / Off(목적층) :" + stCrtCall.strTakeOff);
                            }

                        }
                        else
                        {   // 접속했을 때와 안했을 때를 구분하여....
                            if (m_hCrtForth.IsCrtConnected())
                            {
                                // Crt 서버에 접속 불량시 로그 출력 후 재접속
                                LogPrint(Program.g_CrtType + " Crt 서버(Forth) 접속 불량(E/V 호출 실패)");
                                m_hCrtForth.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + " Crt 서버(Forth) 접속 불량(E/V 호출 실패)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                            else
                            {
                                // E/V 호출 실패 
                                LogPrint(Program.g_CrtType + "(Forth) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                m_hCrtForth.CrtConnection();

                                // DB에 Log 저장(엘리베이터 호출 실패)
                                string strCommnet = string.Format(Program.g_CrtType + "(Forth) E/V 호출 실패(호기, 탑승층 확인 필요)");
                                Program.doDBLogKmsCrt(stCrtCall.strLobbyName, nDong.ToString(), nHo.ToString(), strCommnet, "");
                            }
                        }
                    }
                }
            }

            lock (m_listCrtCall)
            {
                m_listCrtCall.Remove(stCrtCall);

            }

            // 구조체 초기화
            if ((stCrtCall.bElCallFlag == true) &&
                (stCrtCall.bElConfirmFlag == true) &&
                (stCrtCall.bUkrConfirmFlag == true))
            {
                m_listCrtCall.Remove(stCrtCall);
            }
        }

        public void LogPrint(string a_strLog)
        {
            m_myLogListQue.Add(a_strLog);
            
        }

        public void MyLogListQueThread()
        {
            while (true)
            {
                while (m_myLogListQue.Count > 0) // Queue가 비어 있지 않음
                {
                    string strLog = m_myLogListQue[0];
                    m_myLogListQue.RemoveAt(0);
                    if (m_myLogListQue == null)
                    {
                        continue;
                    }

                    if (strLog == "")
                    {
                        continue;
                    }

                    string _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    ListViewItem _lvi = new ListViewItem("");
                    _lvi.SubItems.Add(_strTime);
                    _lvi.SubItems.Add(strLog);

                    List<ListViewItem> listViewItems = new List<ListViewItem>();
                    listViewItems.Add(_lvi);
                    uc_lv_evt_log.InvokeIfNeeded(() => uc_lv_evt_log.SetListData(listViewItems));

                    this.LogMainListSave(_strTime + "       " + strLog);

                    Console.WriteLine(strLog);

                    Thread.Sleep(1);
                }

                Thread.Sleep(100); // Queu가 비어 있으면.
            }
        }

        public void LogFileSave(string a_strLog)
        {
            string _strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.LogMainListSave(_strTime + "       " + a_strLog);

            Console.WriteLine(a_strLog);
        }

        public void LogSave(string a_strLog)
        {
            //DB및 파일 저장
            Program.m_logCrt.SetLogFile("Crt", a_strLog);
        }

        public void LogMainListSave(string a_strLog)
        {
            Program.m_logMainList.SetLogFile("", a_strLog);
        }

        private void btn_keyId_list_del_Click(object sender, EventArgs e)
        {

            int nLoop = 0;

            lv_key_info.InvokeIfNeeded(() => nLoop = lv_key_info.Items.Count);


            for (int i = 0; i < nLoop; i++)
            {
                bool _bFlag = false;
                lv_key_info.InvokeIfNeeded(() => _bFlag = lv_key_info.Items[i].Checked);

                if (_bFlag)
                {
                    lv_key_info.InvokeIfNeeded(() => lv_key_info.Items[i].Remove());
                    lv_key_info.InvokeIfNeeded(() => nLoop = lv_key_info.Items.Count);
                    i = -1;
                }
            }
        }

        private void ToolStripMenuItem_ReLoad_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        private void ToolStripMenuItem_CrtReConnection_Click(object sender, EventArgs e)
        {
            //////////////////////////////////////////////////////
            // Crt 서버 접속
            if (m_hCrt.CrtConnection(m_strCrtIp, m_nCrtPort))
            {
                this.LogPrint("엘리베이터 서버 접속 성공:" + "IP:" + m_strCrtIp + ", Port: " + m_nCrtPort.ToString());

                string strCommnet = "엘리베이터 서버 접속 성공:" + "IP:" + m_strCrtIp + ", Port: " + m_nCrtPort.ToString();
                Program.doDBLogKmsCrt("MainCrt", "MainCrt", "성공", strCommnet, "");
            }
            else
            {
                this.LogPrint("엘리베이터 서버 접속 실패: " + "IP:" + m_strCrtIp + ", Port: " + m_nCrtPort.ToString());

                string strCommnet = "엘리베이터 서버 접속 실패: " + "IP:" + m_strCrtIp + ", Port: " + m_nCrtPort.ToString();
                Program.doDBLogKmsCrt("MainCrt", "MainCrt", "실패", strCommnet, "");
            }

            if (m_hCrtSub == null)
                return;

            if (m_hCrtSub.CrtConnection(m_strCrtSubIp, m_nCrtSubPort))
            {
                this.LogPrint("엘리베이터 서버 접속 성공:" + "IP:" + m_strCrtSubIp + ", Port: " + m_nCrtSubPort.ToString());

                string strCommnet = "엘리베이터 서버 접속 성공:" + "IP:" + m_strCrtSubIp + ", Port: " + m_nCrtSubPort.ToString();
                Program.doDBLogKmsCrt("SubCrt", "SubCrt", "성공", strCommnet, "");
            }
            else
            {
                this.LogPrint("엘리베이터 서버 접속 실패: " + "IP:" + m_strCrtSubIp + ", Port: " + m_nCrtSubPort.ToString());
                string strCommnet = "엘리베이터 서버 접속 실패: " + "IP:" + m_strCrtSubIp + ", Port: " + m_nCrtSubPort.ToString();
                Program.doDBLogKmsCrt("SubCrt", "SubCrt", "실패", strCommnet, "");
            }
        }

        private void LoadConfig()
        {
            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\Config.ini");

            foreach (string _line in lines)
            {
                string[] _str = _line.Split('=');

                string _case = _str[0].ToLower();

                switch (_case)
                {
                    case "dbip":
                        this.DBIp = _str[1];
                        break;
                    case "crt":
                        Program.g_CrtType = _str[1];
                        break;
                    case "crtip":
                        m_strCrtIp = _str[1];
                        break;
                    case "subcrtuse":
                        m_nCrtSubUse = int.Parse(_str[1]);
                        break;
                    case "subip":
                        m_strCrtSubIp = _str[1];
                        break;
                    case "crtport":
                        m_nCrtPort = int.Parse(_str[1]);
                        break;
                    case "subport":
                        m_nCrtSubPort = int.Parse(_str[1]);
                        break;
                    case "subcarstart":
                        m_nCrtSubCarStart = int.Parse(_str[1]);
                        break;
                    case "carcount":
                        m_nCarCount = int.Parse(_str[1]);
                        break;
                    case "thiredcrtuse":
                        m_nCrtThirdUse = int.Parse(_str[1]);
                    break;
                  
                    case "thirdip":
                        m_strCrtThirdIp = _str[1];
                        break;
                    case "thirdport":
                        m_nCrtThirdPort = int.Parse(_str[1]);
                    break;
                    case "thirdcarstart":
                        m_nCrtThirdCarStart = int.Parse(_str[1]);
                    break;
                    case "subcarcount":
                        m_nCrtSubCarCount = int.Parse(_str[1]);
                    break;
                    case "forthcrtuse":
                        m_nCrtForthUse = int.Parse(_str[1]);
                    break;
                    case "forthip":
                        m_strCrtForthIp = _str[1];
                    break;
                    case "forthport":
                        m_nCrtForthPort = int.Parse(_str[1]);
                    break;
                    case "forthcarstart":
                        m_nCrtForthCarStart = int.Parse(_str[1]);
                    break;
                    case "thirdcarcount":
                        m_nCrtThirdCarCount = int.Parse(_str[1]);
                    break;
                    case "direction_set":
                        m_strDirectionFlag = _str[1];
                        break;
                    case "takeoff_set":
                        m_strTakeOffFlag = _str[1];
                        break;
                    case "subcrtcarflow":
                        Program.g_strSubCrtCarFlow = _str[1];
                        break;
                    case "crtcalldongho":
                        Program.g_nCrtCallDongHo = int.Parse(_str[1]);
                        break;
                    case "spktypeuse":
                        Program.g_nSPKTypeUse = int.Parse(_str[1]);
                        break;
                    case "difsvruse":
                        m_nDifSvrUse = int.Parse(_str[1]);
                        break;
                    case "keytypeuse":
                        Program.g_nKeyTypeUse = int.Parse(_str[1]);
                        break;
                    case "registerdevicetype":
                        Program.g_nRegisterDeviceType = int.Parse(_str[1]);
                        break;
                    case "phonenfcuse":
                        Program.g_nPhoneNfcUse = int.Parse(_str[1]);
                        break;
                    case "remoteukropen":
                        Program.g_nRemoteUKROpen = int.Parse(_str[1]);
                        break;
                    case "setopenid":
                        Program.g_nSetOpenID = int.Parse(_str[1]);
                        break;
                    case "webip":
                        m_strWebIp = _str[1];
                        break;
                    case "webport":
                        m_nWebPort = int.Parse(_str[1]);
                        break;
                    case "crtcalldonghogroup":
                        Program.g_nCrtCallDongHoGroup = int.Parse(_str[1]);
                        break;
                    case "hanin_parking":
                        Program.g_nHanInParking = int.Parse(_str[1]);
                        break;
                    case "ev_carconfig_use":
                        Program.g_nEv_CarConfig_Use = int.Parse(_str[1]);
                        break;
                    case "chargeruse":
                        Program.g_nChargerUse = int.Parse(_str[1]);
                        break;
                    case "mainthreadtime":
                        Program.g_nMainThreadTime = int.Parse(_str[1]);
                        break;
                    case "statethreadtime":
                        Program.g_nStateThreadTime = int.Parse(_str[1]);
                        break;
                    default:
                        break;
                }
            }

            // 건설사 타입 읽어 오기
            ConstructionCompany company = JsonSerializer.Instance.LoadAndDeserialize<ConstructionCompany>(Application.StartupPath, "Company");

            Program.g_appType = company.Type;
            /*
            Console.WriteLine("ThiredCrtUse="+m_nCrtThirdUse.ToString());
            Console.WriteLine("ThirdIp=" + m_strCrtThirdIp.ToString());
            Console.WriteLine("ThirdPort=" + m_nCrtThirdPort.ToString());
            Console.WriteLine("ThirdCarStart=" + m_nCrtThirdCarStart.ToString().ToString());
            Console.WriteLine("SubCarCount=" + m_nCrtSubCarCount.ToString().ToString());

            Console.WriteLine("ForthCrtUse=" + m_nCrtForthUse.ToString());
            Console.WriteLine("ForthIp=" + m_strCrtForthIp.ToString());
            Console.WriteLine("ForthPort=" + m_nCrtForthPort.ToString());
            Console.WriteLine("ForthCarStart=" + m_nCrtForthCarStart.ToString());
            Console.WriteLine("ThirdCarCount=" + m_nCrtThirdCarCount.ToString());
            */
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormPassword frmPw = new FormPassword();
            frmPw.ShowDialog();

            if (Program.g_bpassword == true)
            {
                Program.g_bpassword = false;

                //Clear All Socket
                foreach (DevInfo devInfo in m_listUkrInfo)
                    devInfo.socket.Close();

                Program.g_bDataRecv = false;

                this.LogFileSave("사용자로 인해 프로그램 종료.");

                Process.GetCurrentProcess().Kill();
            }
            else
            {
                e.Cancel = true;
            }

        }

        /*
        private void lv_lb_master_ItemActivate(object sender, EventArgs e)
        {
            txt_reg_dong.Text = lv_lb_master.SelectedItems[0].SubItems[1].Text;
            txt_reg_ho.Text = lv_lb_master.SelectedItems[0].SubItems[2].Text;
            txt_reg_keysn.Text = lv_lb_master.SelectedItems[0].SubItems[3].Text;
            txt_reg_keyid.Text = lv_lb_master.SelectedItems[0].SubItems[4].Text;
        }
        */

        

        // 리셋 정보 불러 오기
        private void ToolReset_Click(object sender, EventArgs e)
        {
            FormReset reset = new FormReset(this);

            reset.SetMySql(m_mysql);
            reset.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Connection Test");

            if (m_hCrt.IsCrtConnected())
            {
                Console.WriteLine("m_hCrt.IsCrtConnect() = true");
            }
            else
            {
                Console.WriteLine("m_hCrt.IsCrtConnect() = false");
            }
        }

        private void KeyDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 2020-04-14 키삭제 루틴 추가
            // 기존에 MainForm에서 키 삭제를 할 수 있는 루틴을 메뉴로 뺌
            FormKeyDelete _frmKeyDelete = new FormKeyDelete();
            _frmKeyDelete.ShowDialog();
        }

        private void KeyIdRequest() // 신규등록기에 Key ID 요청
        {
            if (m_BleRegister == null)
            {
                MessageBox.Show("접속 된 등록기가 없습니다. 등록기 접속상태 확인 필요합니다.");
                return;
            }

            byte[] _byPacket = MakeProtocol(0xff, 0x00, "", 0x08, 0x04);    // 등록기는 ID 고정

            // 패킷을 생성 하지 못 한 경우
            if (_byPacket == null)
                return;

            if (!Program.IsConnected(m_BleRegister.socket))
            {
                MessageBox.Show("등록기가 연결 되지 않았습니다. 연결 후 다시 시도하세요");
                return;
            }

            // 패킷을 보내지 못한 경우
            if (m_BleRegister.socket.Send(_byPacket) <= 0)
                return;

            // Key ID 요청 로그 남김
            string strLobbyName = Program.GetLobbyName(m_BleRegister.byGid.ToString("X2"), m_BleRegister.byDid.ToString("X2"));
            Program.doDBLogKmsDev(strLobbyName, "Send Data = ", _byPacket);

            this.LogPrint("키 ID(인증번호) 요청");
        }

        // BLE로 ID 읽기
        private void btn_bleid_load_Click(object sender, EventArgs e)
        {
            if (m_BleRegister == null)
            {
                MessageBox.Show("접속 된 등록기가 없습니다. 등록기 접속상태 확인 필요합니다.");
                return;
            }

            byte[] _byPacket = MakeProtocol(m_BleRegister.byGid, m_BleRegister.byDid, "", 0x08, 0x01);

            // 패킷을 생성 하지 못 한 경우
            if (_byPacket == null)
                return;

            if (!Program.IsConnected(m_BleRegister.socket))
            {
                MessageBox.Show("등록기가 연결 되지 않았습니다. 연결 후 다시 시도하세요");
                return;
            }

            // 패킷을 보내지 못한 경우
            if (m_BleRegister.socket.Send(_byPacket) <= 0)
                return;

            // Key ID 요청 로그 남김
            string strLobbyName = Program.GetLobbyName(m_BleRegister.byGid.ToString("X2"), m_BleRegister.byDid.ToString("X2"));
            Program.doDBLogKmsDev(strLobbyName, "Send Data = ", _byPacket);

            Program.g_nRegistingKeyType = 2;    // 2021-04-21 추가. 키등록시 Key_Type에 값을 추가 하기 위한 변수
            this.LogPrint("식별번호 및 인증번호 요청");
        }

        // 2020-05-06 추가
        public void DoLogDequeue(OPDevPtrl a_opDevPtrl)
        {
            // 상태 체크 데이터는 로그로 남기지 않는다. 이유: 상태체크 데이터가 너무 많음. 
            if (a_opDevPtrl.byType == 0xC0 &&
                a_opDevPtrl.byCmd == 0x01 &&
                a_opDevPtrl.byOpt == 0x12)
            {
                return;
            }

            string _strData = "";
            for (int i = 0; i < a_opDevPtrl.byLen; i++)
            {
                _strData = _strData + string.Format("{0} ", a_opDevPtrl.byData[i].ToString("X2"));

            }

            string _strLog = string.Format("Dequeue Data: Gid:{0}, Did:{1}, Type:{2}, Cmd:{3}, Opt:{4}, Len:{5}, Data:{6}",
                a_opDevPtrl.byGid.ToString("X2"), a_opDevPtrl.byDid.ToString("X2"), a_opDevPtrl.byType.ToString("X2"),
                a_opDevPtrl.byCmd.ToString("X2"), a_opDevPtrl.byOpt.ToString("X2"), a_opDevPtrl.byLen.ToString("X2"),
                _strData);

            this.LogSave(_strLog);
        }

        // 2020-05-18 추가
        public void DoLogDequeue(OPDevPtrl a_opDevPtrl, int a_nSize)
        {
            // 상태 체크 데이터는 로그로 남기지 않는다. 이유: 상태체크 데이터가 너무 많음. 
            if (a_opDevPtrl.byType == 0xC0 &&
                a_opDevPtrl.byCmd == 0x01 &&
                a_opDevPtrl.byOpt == 0x12)
            {
                return;
            }

            string _strData = "";
            for (int i = 0; i < a_opDevPtrl.byLen; i++)
            {
                _strData = _strData + string.Format("{0} ", a_opDevPtrl.byData[i].ToString("X2"));

            }

            string _strLog = string.Format("Dequeue Data: Gid:{0}, Did:{1}, Type:{2}, Cmd:{3}, Opt:{4}, Len:{5}, Data:{6}, Queue Size = {7}",
                a_opDevPtrl.byGid.ToString("X2"), a_opDevPtrl.byDid.ToString("X2"), a_opDevPtrl.byType.ToString("X2"),
                a_opDevPtrl.byCmd.ToString("X2"), a_opDevPtrl.byOpt.ToString("X2"), a_opDevPtrl.byLen.ToString("X2"),
                _strData, a_nSize);

            this.LogSave(_strLog);
        }

        

        private void config정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigInit _frmConfig = new FormConfigInit();
            _frmConfig.Show();
        }

        private void OnePassSettingTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormType _frmType = new FormType();
            _frmType.Show();
        }

        private void btn_keyid_load_MouseDown(object sender, MouseEventArgs e)
        {
            btn_keyid_load.BackColor = System.Drawing.Color.White;
            btn_keyid_load.BackgroundImage = Properties.Resources.BtnTag_Click_xxx;
            btn_keyid_load.Invalidate();
        }

        private void btn_keyid_load_MouseHover(object sender, EventArgs e)
        {
            btn_keyid_load.BackColor = System.Drawing.Color.White;
            btn_keyid_load.BackgroundImage = Properties.Resources.BtnTag_Over_xxx;
            btn_keyid_load.Invalidate();
        }

        private void btn_keyid_load_MouseLeave(object sender, EventArgs e)
        {
            btn_keyid_load.BackColor = System.Drawing.Color.White;
            btn_keyid_load.BackgroundImage = Properties.Resources.BtnTag_Normal_xxx;
            btn_keyid_load.Invalidate();
        }

        private void btn_keyid_load_MouseUp(object sender, MouseEventArgs e)
        {
            btn_keyid_load.BackColor = System.Drawing.Color.White;
            btn_keyid_load.BackgroundImage = Properties.Resources.BtnTag_Normal_xxx;
            btn_keyid_load.Invalidate();
        }

        private void btn_keyId_list_del_MouseDown(object sender, MouseEventArgs e)
        {
            btn_keyId_list_del.BackColor = System.Drawing.Color.White;
            btn_keyId_list_del.BackgroundImage = Properties.Resources.BtnDel_Click_xxx;
            btn_keyId_list_del.Invalidate();
        }

        private void btn_keyId_list_del_MouseHover(object sender, EventArgs e)
        {
            btn_keyId_list_del.BackColor = System.Drawing.Color.White;
            btn_keyId_list_del.BackgroundImage = Properties.Resources.BtnDel_Over_xxx;
            btn_keyId_list_del.Invalidate();
        }

        private void btn_keyId_list_del_MouseLeave(object sender, EventArgs e)
        {
            btn_keyId_list_del.BackColor = System.Drawing.Color.White;
            btn_keyId_list_del.BackgroundImage = Properties.Resources.BtnDel_Normal_xxx;
            btn_keyId_list_del.Invalidate();
        }

        private void btn_keyId_list_del_MouseUp(object sender, MouseEventArgs e)
        {
            btn_keyId_list_del.BackColor = System.Drawing.Color.White;
            btn_keyId_list_del.BackgroundImage = Properties.Resources.BtnDel_Normal_xxx;
            btn_keyId_list_del.Invalidate();
        }

        private void btn_bleid_load_MouseDown(object sender, MouseEventArgs e)
        {
            btn_bleid_load.BackColor = System.Drawing.Color.White;
            btn_bleid_load.BackgroundImage = Properties.Resources.BtnBle_Click_xxx;
            btn_bleid_load.Invalidate();
        }

        private void btn_bleid_load_MouseHover(object sender, EventArgs e)
        {
            btn_bleid_load.BackColor = System.Drawing.Color.White;
            btn_bleid_load.BackgroundImage = Properties.Resources.BtnBle_Over_xxx;
            btn_bleid_load.Invalidate();
        }

        private void btn_bleid_load_MouseLeave(object sender, EventArgs e)
        {
            btn_bleid_load.BackColor = System.Drawing.Color.White;
            btn_bleid_load.BackgroundImage = Properties.Resources.BtnBle_noraml_xxx;
            btn_bleid_load.Invalidate();
        }

        private void btn_bleid_load_MouseUp(object sender, MouseEventArgs e)
        {
            btn_bleid_load.BackColor = System.Drawing.Color.White;
            btn_bleid_load.BackgroundImage = Properties.Resources.BtnBle_noraml_xxx;
            btn_bleid_load.Invalidate();
        }

        private void btn_keyId_list_reg_MouseDown(object sender, MouseEventArgs e)
        {
            btn_keyId_list_reg.BackColor = System.Drawing.Color.White;
            btn_keyId_list_reg.BackgroundImage = Properties.Resources.BtnAdd_Click_xxx;
            btn_keyId_list_reg.Invalidate();
        }

        private void btn_keyId_list_reg_MouseHover(object sender, EventArgs e)
        {
            btn_keyId_list_reg.BackColor = System.Drawing.Color.White;
            btn_keyId_list_reg.BackgroundImage = Properties.Resources.BtnAdd_Over_xxx;
            btn_keyId_list_reg.Invalidate();
        }

        private void btn_keyId_list_reg_MouseLeave(object sender, EventArgs e)
        {
            btn_keyId_list_reg.BackColor = System.Drawing.Color.White;
            btn_keyId_list_reg.BackgroundImage = Properties.Resources.BtnAdd_Normal_xxx;
            btn_keyId_list_reg.Invalidate();
        }

        private void btn_keyId_list_reg_MouseUp(object sender, MouseEventArgs e)
        {
            btn_keyId_list_reg.BackColor = System.Drawing.Color.White;
            btn_keyId_list_reg.BackgroundImage = Properties.Resources.BtnAdd_Normal_xxx;
            btn_keyId_list_reg.Invalidate();
        }

        private void btn_key_reg_MouseDown(object sender, MouseEventArgs e)
        {
            btn_key_reg.BackColor = System.Drawing.Color.White;
            btn_key_reg.BackgroundImage = Properties.Resources.BtnRegister_Click_xxx;
            btn_key_reg.Invalidate();
        }

        private void btn_key_reg_MouseHover(object sender, EventArgs e)
        {
            btn_key_reg.BackColor = System.Drawing.Color.White;
            btn_key_reg.BackgroundImage = Properties.Resources.BtnRegister_Over_xxx;
            btn_key_reg.Invalidate();
        }

        private void btn_key_reg_MouseLeave(object sender, EventArgs e)
        {
            btn_key_reg.BackColor = System.Drawing.Color.White;
            btn_key_reg.BackgroundImage = Properties.Resources.BtnRegister_Normal_xxx;
            btn_key_reg.Invalidate();
        }

        private void btn_key_reg_MouseUp(object sender, MouseEventArgs e)
        {
            btn_key_reg.BackColor = System.Drawing.Color.White;
            btn_key_reg.BackgroundImage = Properties.Resources.BtnRegister_Normal_xxx;
            btn_key_reg.Invalidate();
        }

        private void DevLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDevLog _frmLog = new FormDevLog();
            _frmLog.SetMySql(m_mysql);
            _frmLog.ShowDialog();
        }

        private void mwSensitivitytoolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSensitivity _frmSensitivity = new FormSensitivity();
            _frmSensitivity.SetMySql(m_mysql);
            _frmSensitivity.ShowDialog();
        }

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.DrawBackground();
                // Draw the header text.
                using (Font headerFont =
                            new Font("돋움", 11))
                {
                    Color bgColor = Color.FromArgb(239, 249, 254);
                    e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);

                    e.Graphics.DrawLine(new Pen(Color.FromArgb(210, 210, 210)), new Point(e.Bounds.X, 0), new Point(e.Bounds.X, e.Bounds.Height));  // Header 세로 라인
                }
            }
            return;
        }


        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        //공동현관 리더기에 상태 체크를 보낸 시간을 남김
        private void doDBUkrStateInfoSendTime(string a_strGid, string a_strDid, string a_strSendTime)
        {
            //string strQry = string.Format("Update kms.UKR_State_Info Set StateSendTime = '{0}' where GID = '{1}' and DID = '{2}';", a_strSendTime, a_strGid, a_strDid);
            //m_mysql.MySqlExec(strQry, "");
            for (int i = 0; i < m_ukrState.Count; i++)
            {
                if (m_ukrState[i].m_strGid == a_strGid &&
                    m_ukrState[i].m_strDid == a_strDid)
                {
                    m_ukrState[i].m_strSendTime = a_strSendTime;
                }
            }
        }

        //공동현관 리더기에 상태 체크를 받은 시간을 남김
        private void doDBUkrStateInfoRecvTime(string a_strGid, string a_strDid, string a_strRecvTime, int a_nValue)
        {
            //string strQry = string.Format("Update kms.UKR_State_Info Set StateRecvTime = '{0}', State = {1} where GID = '{2}' and DID = '{3}';", a_strRecvTime, a_nValue, a_strGid, a_strDid);
            //m_mysql.MySqlExec(strQry, "");
            for (int i = 0; i < m_ukrState.Count; i++)
            {
                if (m_ukrState[i].m_strGid == a_strGid &&
                    m_ukrState[i].m_strDid == a_strDid)
                {
                    m_ukrState[i].m_strRecvTime = a_strRecvTime;
                    m_ukrState[i].m_nValue = a_nValue;
                }
            }
        }

        // 개포 퍼스티어 현장에서 Query문을 반복 실행하니, 문제가 있어 과부하가 발생하여 쿼리문을 한번에 실행하게 변경
        private void UkrStateDbExec(Object o)
        {
            string strState = "", strSendTime = "", strRecvTime = "", strGid = "", strDid = "";
            string strQry = "";
            Thread.Sleep(1000 * 120);   // 초기 실행은 2분 후에 실행
            //Thread.Sleep(1000 * 10);   // 초기 실행은 2분 후에 실행
            while (true)
            {
                strState = " State = CASE " ;
                strSendTime = " StateSendTime = CASE ";
                strRecvTime = " StateRecvTime = CASE ";
                strGid = ""; strDid = ""; strQry = "";
                for (int i = 0; i < m_ukrState.Count; i++)
                {
                    strState = strState + string.Format(" WHEN GID = '{0}' and DID = '{1}' THEN {2}", m_ukrState[i].m_strGid, m_ukrState[i].m_strDid, m_ukrState[i].m_nValue);
                    //if(!(m_ukrState[i].m_strSendTime == "" || m_ukrState[i].m_strSendTime == null))
                        strSendTime = strSendTime + string.Format(" WHEN GID = '{0}' and DID = '{1}' THEN '{2}'", m_ukrState[i].m_strGid, m_ukrState[i].m_strDid, m_ukrState[i].m_strSendTime);

                    //if (!(m_ukrState[i].m_strRecvTime != "" || m_ukrState[i].m_strRecvTime == null))
                        strRecvTime = strRecvTime + string.Format(" WHEN GID = '{0}' and DID = '{1}' THEN '{2}'", m_ukrState[i].m_strGid, m_ukrState[i].m_strDid, m_ukrState[i].m_strRecvTime);

                    strGid = strGid + string.Format("'{0}',", m_ukrState[i].m_strGid);
                    strDid = strDid + string.Format("'{0}',", m_ukrState[i].m_strDid);

                    //string strQry = string.Format("Update kms.UKR_State_Info Set StateSendTime = '{0}', StateRecvTime = '{1}', State = {2} where GID = '{3}' and DID = '{4}';", m_ukrState[i].m_strSendTime, m_ukrState[i].m_strRecvTime, m_ukrState[i].m_nValue, m_ukrState[i].m_strGid, m_ukrState[i].m_strDid);
                    //m_mysql.MySqlExec(strQry, "");
                    //Thread.Sleep(5);
                }

                strState = strState + " ELSE State ";
                if(strSendTime.Length > 22)
                    strSendTime = strSendTime + " ELSE StateSendTime ";
                if(strRecvTime.Length > 22)
                    strRecvTime = strRecvTime + " ELSE StateRecvTime ";
                strGid = "(" + strGid.Substring(0, strGid.Length - 1) + ")";
                strDid = "(" + strDid.Substring(0, strDid.Length - 1) + ")";
                strQry = string.Format("UPDATE kms.UKR_State_Info SET {0} END, {1} END, {2} END WHERE GID IN{3} and DID IN{4};", 
                                       strState, strSendTime, strRecvTime, strGid, strDid);

                
                //strQry = string.Format("UPDATE kms.UKR_State_Info SET {0} END WHERE GID IN{1} and DID IN{2};",
                //    strState, strGid, strDid);
                
                //Console.WriteLine("============================================");
                //Console.WriteLine("strQry = " + strQry);

                m_mysql.MySqlExec(strQry, "");

                Thread.Sleep(1000 * 90);    // 90초마드 실행
            }
        }

        // 상태 체크 결과를 남김(상태 체크 응답이 있으면, 1이고, 5회 이상 상태 체크를 보내고, 응답이 없으면, 0으로 남음)
        private void doDBUkrStateInfoValue(string a_strGid, string a_strDid, int a_nValue)
        {
            string strQry = string.Format("Update kms.UKR_State_Info Set State = {0} where GID = '{1}' and DID = '{2}';", a_nValue, a_strGid, a_strDid);
            m_mysql.MySqlExec(strQry, "");
        }

        // 공동현관 리더기가 접속이벤트를 보내지 않고, 인증정보를 먼저 보냈을 때, 키 등록/삭제등이 진행 되지가 않아,
        // 공동현관이 재접속할 수 있게 Reset 명령어를 보낸다.
        private void UkrResetThread(OPDevPtrl a_OpDevData)
        {
            Thread.Sleep(5 * 1000); // 5초후 실행
            
            DevInfo devInfo = new DevInfo();
            devInfo.byGid = a_OpDevData.byGid;
            devInfo.byDid = a_OpDevData.byDid;
            devInfo.socket = a_OpDevData.socket;

            RequestReset(devInfo);
            string strLobbyName = Program.GetLobbyName(a_OpDevData.byGid.ToString("X2"), a_OpDevData.byDid.ToString("X2"));
            Program.doDBLogKmsDev(strLobbyName, "Force Reset Request", null);
        }

        // 암호화 작업
        public string Encryption(string str)
        {
            //public int HexToString(string a_strInput, byte[] a_byOutput)

            DualiControl d = new DualiControl();

            byte[] byID = Program.StrToByteArray(str);

            string key = "";

            key = d.GetSignString(byID[7])
                        + d.GetSignString(byID[6])
                        + d.GetSignString(byID[5])
                        + d.GetSignString(byID[4])
                        + d.GetSignString(byID[3])
                        + d.GetSignString(byID[2])
                        + d.GetSignString(byID[1])
                        + d.GetSignString(byID[0]);
            return key.ToUpper();
        }
        
        private void CheckStateProc()
        {
            List<string[]> pingDataLog = new List<string[]>();

            foreach (UC_Map _map in m_ucMap)
            {
                foreach (Control _BTN in _map.BTN)
                {
                    string _strLBIp = "";
                    
                        // UKR_Connect_Info에서 Ping 테스트를 Lobby_Info로 변경 => 2023-07-13 기술지원본부 요청
                        //string _strQry = string.Format("SELECT ukr.IPAddr FROM kms.UKR_Connect_Info as ukr, kms.Lobby_ID li where li.Lobby_Name = '{0}' and ( ukr.GID = li.Lobby_GID and ukr.DID = li.Lobby_DID);;", _BTN.Name);
                        string _strQry = string.Format("Select LobbyIp from kms.Lobby_Info where LobbyName = '{0}';", _BTN.Name);

                        List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

                        foreach (string[] _str in _qryList)
                        {
                            _strLBIp = _str[0];
                        }

                        bool bPingResult = false;
                        string[] str = new string[1];

                        // 1. Lobby_Info에 IP 가 있는지 검사 => 없으면 하늘색 
                        if (_strLBIp.Length == 0)
                        {
                            this.Invoke(new MethodInvoker(delegate()
                            {
                                LogPrint(string.Format("Lobby [{0}] 기기 IP 정보 없음(Table: Lobby_Info)", _BTN.Name));
                                // 공동현관 리더기 정보가 없음.
                                _BTN.BackgroundImage = Properties.Resources.Btn_Lobby_Blue_xxx;
                                _BTN.Tag = false;

                                str[0] = string.Format("Lobby [{0}] 기기 IP 정보 없음(Table: Lobby_Info)", _BTN.Name);
                                pingDataLog.Add(str);
                            }));
                        }
                        else {
                            // 2. Ping 체크 => 핑안되면, 주황색
                            CheckStatus _chkStatus = new CheckStatus(1000);
                            bPingResult = _chkStatus.CheckIpPingAsync(_strLBIp);
                            if (!bPingResult)
                            {
                                this.Invoke(new MethodInvoker(delegate()
                                {
                                    _BTN.BackgroundImage = Properties.Resources.Btn_Lobby_Not_xxx;// 주황
                                    _BTN.Tag = false;

                                    str[0] = string.Format("Lobby [{0}] IP = {1} -> PING RESULT={2}", _BTN.Name, _strLBIp, bPingResult);
                                    LogPrint(str[0]);
                                    pingDataLog.Add(str);
                                }));
                            }
                            else
                            {
                                // 3. 핑이 성공이면, State 0체크 => 상태가0이면 핑크색
                                _strQry = string.Format("Select st.State, st.StateSendTime, st.StateRecvTime From kms.UKR_State_Info as st, Kms.Lobby_ID as lo where (st.GID=lo.Lobby_GID and st.DID=lo.Lobby_DID) and (lo.Lobby_Name = '{0}');", _BTN.Name);

                                List<string[]> _qryList1 = m_mysql.MySqlSelect(_strQry, 3);
                                string strState = "", strSendTime = "", strRecvTime = "";
                                foreach (string[] _str in _qryList1)
                                {
                                    strState= _str[0];
                                    strSendTime = _str[1];
                                    strRecvTime = _str[2];
                                }

                                this.Invoke(new MethodInvoker(delegate()
                                {
                                    if (strState != "1")    // 상태 체크가 안됨.
                                    {
                                        _BTN.BackgroundImage = Properties.Resources.Btn_Lobby_Pink_xxx;// 핑크색
                                        _BTN.Tag = false;

                                        str[0] = string.Format("Lobby [{0}] IP = {1} -> Ping = 정상, State = 비정상", _BTN.Name, _strLBIp, strState);
                                        LogPrint(str[0]);
                                        pingDataLog.Add(str);
                                    }
                                    else
                                    {
                                        _BTN.BackgroundImage = Properties.Resources.Btn_Lobby_Normal_xxx;// 핑크색
                                        _BTN.Tag = true;

                                        // 상태 체크가 정상
                                        str[0] = string.Format("Lobby [{0}] IP = {1} -> Ping, State = 정상", _BTN.Name, _strLBIp);
                                        LogPrint(str[0]);
                                        pingDataLog.Add(str);
                                    }
                                }));
                            }
                        }

                }
            }

            // CSV 파일 생성 및 데이터 작성
            DateTime date = DateTime.Now;
            string folderPath = Application.StartupPath + "\\PingLog\\"; // 저장할 폴더 경로

            // 폴더가 없으면 폴더 생성
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            // 권한 부여
            DirectoryInfo directoryInfo = new DirectoryInfo(folderPath);
            DirectorySecurity directorySecurity = directoryInfo.GetAccessControl();
            directorySecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.Write, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
            directoryInfo.SetAccessControl(directorySecurity);

            string filePath = Path.Combine(folderPath, date.ToString("yyyy-MM-dd_HH시mm분ss초") + ".csv"); // 저장할 파일 경로

            using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                foreach (string[] row in pingDataLog)
                {
                    writer.WriteLine(string.Join(",", row));
                }
            }
        }

        private void 결과초기화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (UC_Map _map in m_ucMap)
            {
                foreach (Control _BTN in _map.BTN)
                {
                    _BTN.BackgroundImage = Properties.Resources.Btn_Lobby_Normal_xxx;
                    _BTN.Tag = true;
                }
            }
        }

        // Ping 체크시 상태를 UI로 표시 또는 파일로 저장(2023--7-14 기술지원본부 요청)
        private void pING테스트ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Thread _thCheckState = null;
            if (_thCheckState == null)
            {
                _thCheckState = new Thread(this.CheckStateProc)
                {
                    IsBackground = true,
                    Name = "_thCheckState Thread"
                };
                _thCheckState.Start();
            }
        }

        private void 혜인에스앤에스Key정보동기화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            // 1. 기존 테이블 삭제
            string strQry = string.Format("Drop Table {0};", Program.TBL_NAME_KEY_INFO_HAEIN);
            m_mysql.MySqlExec(strQry);

            // 2. 뷰테이블 삭제
            strQry = string.Format("Drop VIEW {0};", Program.VIEW_NAME_KEY_INFO_HAEIN);
            m_mysql.MySqlExec(strQry);

            // 3. 생성
            if (!Program.IsTable(Program.TBL_NAME_KEY_INFO_HAEIN))
            {
                Program.CreateTable(Program.TBL_CREATE_KEY_INFO_HAEIN);
                InsertKeyInfoHeain();
                //View 테이블이 생성이 되어야함.
                Program.CreateTable(Program.VIEW_CREATE_KEY_INFO_HAEIN);
            }
            else
            {
                MessageBox.Show("View 테이블이 삭제가 되지 않아 동기화 실패함.");
            }
        }

        private void nfc_lbl_id_Click(object sender, EventArgs e)
        {

        }

        private void txt_reg_nfcid_TextChanged(object sender, EventArgs e)
        {

        }

        private void cb_charger_normal_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void 전체KeyData삽입ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPassword frmPw = new FormPassword();
            frmPw.ShowDialog();

            if (Program.g_bpassword == true)
            {

                foreach (stLobbyId lobbyId in Program.m_listLobbyId)
                {
                    string strQry = string.Format("INSERT INTO kms.Reg_{0} (Dong, Ho, Key_Sn, Key_Id, State) Select ki.Dong, ki.Ho, ki.Key_Sn, ki.Key_Id, 0 as State from kms.Dong_Lobby as dl, kms.Key_Info_Master as ki where dl.Lobby_Name = '{0}' and (dl.Dong = ki.Dong and dl.Ho = ki.Ho);", lobbyId.strLobbyName);

                    m_mysql.MySqlExec(strQry, "");
                    Thread.Sleep(5);
                }
            }

        }

        private void 부분KeyData삽입ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPassword frmPw = new FormPassword();
            frmPw.ShowDialog();

            if (Program.g_bpassword == true)
            {
                Program.g_bpassword = false;

                FormSyncInfo _frmSync = new FormSyncInfo();
                _frmSync.SetMySql(m_mysql);
                _frmSync.ShowDialog();
            }
        }
        private void UkrReset(List<string> a_listLobbyName)
        {
            List<DevInfo> listDevInfo = new List<DevInfo>();

            foreach (string lobbyName in a_listLobbyName)
            {
                Console.WriteLine("공동현관 리더기 Reset : " + lobbyName);

                foreach (DevInfo devInfo in m_listUkrInfo)
                {
                    if (devInfo.LobbyName.ToLower() == lobbyName.ToLower())
                    {
                        //RequestReset(devInfo);
                        listDevInfo.Add(devInfo);
                        break;
                    }

                }
            }

            Thread hThread = new Thread(new ParameterizedThreadStart(UkrResetSendThread));
            hThread.Start(listDevInfo);
        }

        private void UkrTimerReset(TimerUkrReset a_tur)
        {
            Console.WriteLine("Timer Reset Cliekt ");
            m_ukrResetInfo = a_tur; // Timer 폼에서 정보를 받아. Timer_UkrRest에서 reset을 함.
        }

        private void Timer_UkrReset(object sender, EventArgs e)
        {
            try
            {
                if (!m_ukrResetInfo.m_bFlag)
                    return;

                if (m_ukrResetInfo.m_strHour == "" ||
                    m_ukrResetInfo.m_strMin == "")
                    return;
                

                Console.WriteLine(string.Format("{0} ==> m_listUkrInfo Cnt = {1}", System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_listUkrInfo.Count));
                if (DateTime.Now.Hour == int.Parse(m_ukrResetInfo.m_strHour) && 
                        DateTime.Now.Minute == int.Parse(m_ukrResetInfo.m_strMin) && 
                        DateTime.Now.Second == 0)
                {
                    //ResetFunction();

                    Thread hThread = new Thread(new ParameterizedThreadStart(UkrResetSendThread));
                    hThread.Start(m_listUkrInfo);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Timer_UkrReset() 예외: " + ex.Message.ToString());
            }
        }

        private void UkrResetSendThread(object o)
        {
            try
            {
                List<DevInfo> listDevInfo = (List<DevInfo>)o;

                foreach (DevInfo devInfo in listDevInfo)
                {
                    RequestReset(devInfo);

                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UkrResetSendThread() :: " + ex.Message.ToString());
            }
        }

        private void UkrKeyIdClear(List<string> a_listLobbyName)
        {
            foreach (string lobbyName in a_listLobbyName)
            {
                Console.WriteLine("공동현관 리더기 KeyID 전체 삭제 : " + lobbyName);

                byte byGid = 0x00, byDid = 0x00;
                Socket socket = null;
                foreach (DevInfo devInfo in m_listUkrInfo)
                {
                    if (devInfo.LobbyName.ToLower() == lobbyName.ToLower())
                    {
                        byGid = devInfo.byGid;
                        byDid = devInfo.byDid;
                        socket = devInfo.socket;
                        break;
                    }

                }

                //전체 삭제 송신
                byte[] byPacket = MakeProtocol(byGid, byDid, "", 0x04, 0x02);
                if (byPacket == null)
                    continue;

                if (socket == null)
                    continue;

                if (Program.IsConnected(socket))
                {
                    socket.Send(byPacket);
                    this.LogPrint(string.Format("{0} 전체 삭제 : Gid:{1}(0x{2}), Did:{3}(0x{4}) ", lobbyName, byGid, byGid.ToString("X2"), byDid, byDid.ToString("X2")));

                    Program.doDBLogKmsDev(lobbyName, "Send Data = ", byPacket);

                    Program.doDBLogKmsKeyRegDel("Del", lobbyName, "", "", "전체 삭제 요청");
                }
            }
        }


        private void doConfigSave()
        {
            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\Config.ini");

            foreach (string line in lines)
            {
                string strQry = string.Format("Insert Into kms.Backup_Config (Comment) values ('{0}');", line);
                m_mysql.MySqlExec(strQry);

                Thread.Sleep(10);
            }
        }
    }

    public class UkrState
    {
        public string m_strGid;
        public string m_strDid;
        public string m_strSendTime;
        public string m_strRecvTime;
        public int m_nValue;
    }

    public class UkrRegDelCheck
    {
        public string m_strGid;
        public string m_strDid;
        public string m_strRegTblName;
        public string m_strDelTblName;
        public bool m_bCheck;
    }

    public class TimerUkrReset
    {
        public bool m_bFlag;
        public string m_strHour;
        public string m_strMin;
        public string m_strSen;
    }

    public static class ControlExtensions
    {
        public static void InvokeIfNeeded(this Control control, Action action)
        {
            if (control.InvokeRequired)
                control.Invoke(action);
            else
                action();
        }

        public static void InvokeIfNeeded<T>(this Control control, Action<T> action, T arg)
        {
            if (control.InvokeRequired)
                control.Invoke(action, arg);
            else
                action(arg);
        }
    }

    class Log
    {
        private DateTime date;
        string strFilePath;

        public Log()
        {
            date = DateTime.Now;
            strFilePath = Application.StartupPath + "\\Log\\";
            CreateDirectory(strFilePath);
        }

        public Log(string a_strFilePath)
        {
            date = DateTime.Now;
            strFilePath = Application.StartupPath + "\\" + a_strFilePath + "\\";
            CreateDirectory(strFilePath);
        }

        private void CreateDirectory(string strDirName)
        {
            DirectoryInfo dir = new DirectoryInfo(strDirName);

            if (dir.Exists == false)
                dir.Create();
        }

        public void SetLogFile(string a_strName, string a_strLog)
        {
            // 2020-04-14 Log 파일 저장시 파일 예외 발생으로 Program Down ==> try Catch 적용
            try
            {
                date = DateTime.Now;
                string strFileNmae = strFilePath + date.ToShortDateString() + ".txt";

                StreamWriter sw = new StreamWriter(strFileNmae, true);
                sw.Write("[" + date.ToLongTimeString() + "]" + a_strName + " :: " + a_strLog + "\r\n");
                sw.Close();
            }
            catch
            {
            }
        }

    }
}