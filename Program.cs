using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Diagnostics;
using System.Drawing;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SmartOnePass
{
    public class DevInfo
    {
        public Socket socket;
        public byte byGid;
        public byte byDid;
        public int nStateCnt;
        public bool bReset;
        public string LobbyName;
        
    }

    public class LobbyBtnInfo   // 공동현관 리더기 버튼을 동적으로 활용하기 위해, 그룹으로 나눔.
    {
        public string strDong;
        public string strLobbyName;
        public string strLobbyGroup;
        public int nPosHeightIdx;
        public int nPosWightIdx;
    }

    public class LobbyBtnGroupPos   // 공동현관 리더기 버튼이 위치 할 높이 인덱싱
    {
        public string strLobbyGroup;
        public int nHeightIdex;
    }


    static class Program
    {
        ////////////////////////////////////////////////////////
        // 테이블 생성 변수
        public const string TBL_NAME_CARNUM = "CarNum";
        public const string TBL_NAME_DONG_LOBBY = "Dong_Lobby";
        public const string TBL_NAME_KEY_INFO_MASTER = "Key_Info_Master";
        public const string TBL_NAME_LOBBY_CRT_INFO = "Lobby_Crt_Info";
        public const string TBL_NAME_LOBBY_ID = "Lobby_ID";
        public const string TBL_NAME_UKR_STATE_INFO = "UKR_State_Info";
        public const string TBL_NAME_UKR_CONNECT_INFO = "UKR_Connect_Info";
        public const string TBL_NAME_LOBBY_INFO = "Lobby_Info";
        public const string TBL_NAME_LOBBY_RESET = "Lobby_Reset";
        public const string TBL_NAME_LOBBY_RESET_TIMER = "Lobby_Reset_Timer";
        public const string TBL_NAME_LOBBY_SYNC = "Lobby_Sync";
        public const string TBL_NAME_LOG_KMSDEV = "Log_KmsDev";
        public const string TBL_NAME_LOG_DEVSTATE = "Log_DevState";
        public const string TBL_NAME_LOG_KMSKEYREGDEL = "Log_KmsKeyRegDel";
        public const string TBL_NAME_LOG_KMSHOMENET = "Log_KmsHomenet";
        public const string TBL_NAME_LOG_KMSCRT = "Log_KmsCrt";
        public const string TBL_NAME_LOG_KMSWEB = "Log_KmsWeb";
        public const string TBL_NAME_UKR_SENSITIVITY = "UKR_Sensitivity";
        public const string TBL_NAME_DONG_CRT_CARID = "Dong_Crt_CarID";
        public const string TBL_NAME_KEY_INFO_HAEIN = "Key_Info_Haein";
        public const string VIEW_NAME_KEY_INFO_HAEIN = "Key_Info_Haein_View";
        public const string TBL_NAME_LOG_KMSUKR = "Log_KmsUKR";
        public const string TBL_NAME_BACKUP_CONFIG = "Backup_Config";
        public const string TBL_NAME_LOG_ENTRYRECORD = "Log_EntryRecord";

        public const string TBL_CREATE_CARNUM = "CREATE TABLE  `kms`.`CarNum` (  `Dong` int(11) NOT NULL,  `Ho` int(11) NOT NULL,  `CarNum` varchar(64) NOT NULL,  `EnterTime` varchar(64) DEFAULT NULL,  `NearHomeInfo` varchar(64) DEFAULT NULL,  `SyncFlag` int(11) DEFAULT NULL,  `RegDate` varchar(32) DEFAULT NULL,  PRIMARY KEY (`CarNum`),  UNIQUE KEY `CarNum_UNIQUE` (`CarNum`)) ENGINE=InnoDB DEFAULT CHARSET=utf8;";
        public const string TBL_CREATE_DONG_LOBBY = "CREATE TABLE kms.Dong_Lobby (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL,  `Lobby_Name` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_KEY_INFO_MASTER = "CREATE TABLE kms.Key_Info_Master (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL,  `Key_Sn` varchar(32) DEFAULT NULL,  `Key_Id` varchar(32) DEFAULT NULL,  `RegDate` varchar(24) DEFAULT NULL,  `Key_type` int(11) DEFAULT '0',  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_CRT_INFO = "CREATE TABLE kms.Lobby_Crt_Info (  `Index` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Lobby_Name` varchar(16) CHARACTER SET latin1 DEFAULT NULL,  `UKR_GID` varchar(4) DEFAULT NULL,  `UKR_DID` varchar(4) DEFAULT '0',  `UKR_EL_GID` varchar(16) DEFAULT NULL,  `UKR_EL_DID` varchar(4) DEFAULT NULL,  `EL_ID` varchar(16) DEFAULT NULL,  `Delay_Time` float DEFAULT NULL,  `Take_On` varchar(4) DEFAULT NULL,  `EL_USE` int(10) unsigned DEFAULT NULL,  `Car_Config` varchar(512) DEFAULT NULL,  PRIMARY KEY (`Index`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_ID = "CREATE TABLE kms.Lobby_ID (  `Idx` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Lobby_GID` varchar(4) DEFAULT NULL,  `Lobby_DID` varchar(4) DEFAULT NULL,  `Lobby_Name` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Idx`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_UKR_STATE_INFO = "CREATE TABLE kms.UKR_State_Info (  `Idx` int(10) unsigned NOT NULL AUTO_INCREMENT,  `GID` varchar(4) DEFAULT NULL,  `DID` varchar(4) DEFAULT NULL,  `IPAddr` varchar(32) DEFAULT NULL,  `State` int(11) DEFAULT NULL,  `StateSendTime` varchar(32) DEFAULT NULL,  `StateRecvTime` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Idx`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_UKR_CONNECT_INFO = "CREATE TABLE kms.UKR_Connect_Info (  `Idx` int(10) unsigned NOT NULL AUTO_INCREMENT,  `GID` varchar(4) DEFAULT NULL,  `DID` varchar(4) DEFAULT NULL,  `IPAddr` varchar(32) DEFAULT NULL,  `ConnectTime` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Idx`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_INFO = "CREATE TABLE kms.Lobby_Info (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LobbyName` varchar(32) NOT NULL, `Reg_Tbl` varchar(32) DEFAULT NULL,  `Del_Tbl` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_RESET = "CREATE TABLE kms.Lobby_Reset (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LobbyName` varchar(64) DEFAULT NULL,  `Flag` int(10) unsigned DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_RESET_TIMER = "CREATE TABLE kms.Lobby_Reset_Timer (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Reset_Use` int(10) DEFAULT NULL,  `Reset_Hour` int(10) DEFAULT NULL,  `Reset_Minute` int(10) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOBBY_SYNC = "CREATE TABLE kms.Lobby_Sync (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LobbyName` varchar(64) DEFAULT NULL,  `Flag` int(11) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_KMS_DEV = "CREATE TABLE kms.Log_KmsDev (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LogDate` varchar(24) DEFAULT NULL,`LobbyName` varchar(24) DEFAULT NULL, `Event` varchar(16) DEFAULT NULL, `Comment` varchar(512) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_DEV_STATE = "CREATE TABLE kms.Log_DevState (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LogDate` varchar(24) DEFAULT NULL,`LobbyName` varchar(24) DEFAULT NULL,  `Comment` varchar(512) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_KEY_REG_DEL = "CREATE TABLE kms.Log_KmsKeyRegDel (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LogDate` varchar(24) DEFAULT NULL,  `Type` varchar(16) DEFAULT NULL,  `LobbyName` varchar(16) DEFAULT NULL, `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL,  `Comment` varchar(512) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_HOMENET = "CREATE TABLE kms.Log_KmsHomenet (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `LogDate` varchar(24) DEFAULT NULL,  `Comment` varchar(512) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_CRT = "CREATE TABLE kms.Log_KmsCrt (`Num` int(10) unsigned NOT NULL AUTO_INCREMENT, `LogDate` varchar(24) DEFAULT NULL, `LobbyName` varchar(24) DEFAULT NULL, `Dong` varchar(8) DEFAULT NULL, `Ho` varchar(8) DEFAULT NULL,`Comment` varchar(64) DEFAULT NULL, `Packet` varchar(256) DEFAULT NULL, PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_LOG_WEB = "CREATE TABLE kms.Log_KmsWeb (`Num` int(10) unsigned NOT NULL AUTO_INCREMENT, `LogDate` varchar(24) DEFAULT NULL, `UKR_ID` varchar(8) DEFAULT NULL, `Comment` varchar(64) DEFAULT NULL, `Packet` varchar(256) DEFAULT NULL, PRIMARY KEY (`Num`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_UKR_SENSITIVITY = "CREATE TABLE kms.UKR_Sensitivity (  `Idx` int(10) unsigned NOT NULL AUTO_INCREMENT, `LobbyName` varchar(32) DEFAULT NULL, `Distance` varchar(4) DEFAULT NULL, `Sensitivity` varchar(4) DEFAULT NULL, `Speed` varchar(4) DEFAULT NULL, `SetFlag` int(10) DEFAULT NULL,  PRIMARY KEY (`Idx`)) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_DONG_CRT_CARID = "CREATE TABLE kms.Dong_Crt_CarID (`Num` int(10) unsigned NOT NULL AUTO_INCREMENT, `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL, `EV_CarID` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=0 DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_KEY_INFO_HAEIN = "CREATE TABLE kms.Key_Info_Haein (  `Num` int(10) unsigned NOT NULL AUTO_INCREMENT,  `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL,  `Key_Sn` varchar(32) DEFAULT NULL, `Key_Cham` varchar(32) DEFAULT NULL, `Key_Haein` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Num`)) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=euckr;";
        public const string VIEW_CREATE_KEY_INFO_HAEIN = "CREATE VIEW kms.Key_Info_Haein_View AS SELECT * FROM kms.Key_Info_Haein;";
        public const string TBL_CREATE_LOG_KMSUKR = "CREATE TABLE kms.Log_KmsUKR (  `Idx` int(11) NOT NULL AUTO_INCREMENT,  `Log_Date` varchar(24) DEFAULT NULL,  `LobbyName` varchar(16) DEFAULT NULL,  `Dong` int(11) DEFAULT NULL,  `Ho` int(11) DEFAULT NULL,  `Key_ID` varchar(32) DEFAULT NULL,  PRIMARY KEY (`Idx`)) ENGINE=InnoDB DEFAULT CHARSET=euckr;";
        public const string TBL_CREATE_BACKUP_CONFIG = "CREATE TABLE kms.Backup_Config (`Num` INT NOT NULL AUTO_INCREMENT,  `Comment` VARCHAR(128) NULL,  PRIMARY KEY (`Num`)) ENGINE = InnoDB DEFAULT CHARACTER SET = euckr COLLATE = euckr_bin;";
        public const string TBL_CREATE_LOG_ENTRYRECORD = "CREATE TABLE kms.Log_EntryRecord ( `Idx` INT NOT NULL AUTO_INCREMENT,  `EntryTime` VARCHAR(32) NULL, `Dong` INT NULL,  `Ho` INT NULL,   `LobbyName` VARCHAR(32) NULL,  `KeySn` VARCHAR(16) NULL,  `KeyId` VARCHAR(16) NULL,  PRIMARY KEY(`Idx`)) ENGINE = InnoDB DEFAULT CHARACTER SET = euckr;";

        //////////////////////////////////////////////////////////////////////////////////////////////////
        // 로그 저장
        public static Log m_logCrt = new Log("LogCrt");
        public static Log m_logMainList = new Log("Log");

        public delegate void KeyConfirm(OPDevPtrl a_OpDevData);

        public static KeyConfirm g_fnKeyConfirm;

        public static MySqlDB m_mysql = null;

        public static Boolean g_bpassword = false;
        //건설사 타입 load 변수
        public static int g_appType;

        // SPK 등록기 사용여부
        public static int g_nSPKTypeUse = 0;

        // 2021-04-20추가 Key_Info_Master에 Key_Type 사용여부 확인, 특정키에서만 E/V 목적층을 호출할 때, 사용함.
        public static int g_nKeyTypeUse = 0;
        // 2021-04-20추가
        public static int g_nRegistingKeyType = 0;  // Key 등록시에 Key_Type 컬럼이 있으면, 등록시 Type을 저장하기 위한 변수

        // 2021-11-03 스마트폰 및 키 등록시 듀얼아이와 공동현관 등록기를 따로 사용을 하고 있어, 통합 요구가 있었음. H/W팀에서 통합 등록기를 개발했음.
        // 그래서 기존 방식과 통합등록기 사용방식에 대한 구분
        public static int g_nRegisterDeviceType = 0;

        // 2022-04-21 스마트폰에서 NFC 기능 사용여부 셋팅 ==> NFC 사용을 함으로써, Key_Info_Master에서 테이블이 변경이 되고, Main Form에서 UI 변경이 됨.
        public static int g_nPhoneNfcUse = 0;

        // RF키, BLE 키 = 1, 스마트폰 = 2, 스마트폰에서 원격으로 등록=3(향후 Web 서버에서 사용)

        // 2020-05-06 한정태 추가 Data Recv 시 Thread에서 처리하기 위한 변수 추가
        public static Boolean g_bDataRecv = true;

        public static string g_CrtType = "";

        // 2020-05-15 한정태 추가 ==> Sub E/V 호출시 Car 호기의 번호를 Main과 상관없이 1,2,3,4, .....으로 할지
        //                            아니면 Main Car 호기 다음 번호 21,22,23,... 으로 결정할 Config 데이터
        //                        g_strSubCrtCarFlow = 0 이면 Car ID 가 1,2,3,4,....
        //                        g_strSubCrtCarFlow = 1 이면 Car ID 가 21,22,23,24,....
        public static string g_strSubCrtCarFlow = "0";

        public static int g_nCrtCallDongHo = 0; // 2020-08-12 추가 E/V 호출시 동/호를 이용하여 호출할 수 있게 수정(공동현관은 1대인데, E/V가 2대일 경우에 활용)


        public static int g_nRemoteUKROpen = 0; // 2023-04-04 웹서버를 사용하여, 원격에서 문열림이 가능하게 기능 추가함.(Ver 1.3 부터 가능)
        public static int g_nSetOpenID = 0;     // 2023-04-04 원격 문열림 데이터 전송을 할때, 패킷에 ID 정보를 넣을지를 선택하는 Config(E/V 목적층 호출할 때 필요함)
        public static int g_nCrtCallDongHoGroup = 0;   // 2023-07-20 엘리베이터를 동호로 호출할때, 그룹인지 단독이지 판단
        public static int g_nHanInParking = 0;          // 2023-07-24 금호건설 혜인 시스템과 연동을 하기 위한 Flag ==> View 테이블 생성만 가능하고, 권한은 워크벤치를 이용하여 할당해야 한다.

        public static int g_nEv_CarConfig_Use = 0;  // 2023-08-17 DL 남양주 뉴타운 e편한세상, 공동현관리더기 1대에서 E/V가 따로 있는 상태에서 엘리베이터 구성이 틀릴때 사용함.

        public static int g_nChargerUse = 0;        // 2023-10-10 개포 퍼스티어 현장에서 전기차 충전에 Key를 등록하기 위해 사용

        public static int g_nMainThreadTime = 5;    // 2023-12-07 개포 퍼스티어 현장 공동현관리더기가 많아, Thread Timer을 조절 할 수 있게 함.
        public static int g_nStateThreadTime = 60;  // 2023-12-19 개포 퍼스티어 현장 공동현관리더기가 많아, 상태 체크 Thread Timer을 조절 할 수 있게 함.

        // Lobby 이름 관리
        public static List<stLobbyId> m_listLobbyId = new List<stLobbyId>();

        // 상수 선언
        public const string CHARGER_PORTABLE_CARD = "이동형 충전 카드";
        public const string CHARGER_NORMAL_CARD = "과금형 충전 카드";

        // 2023-12-22 추가 Form에서 MainForm으로 데이터 전달 Callback
        public static CallBack mMainCb = new CallBack();  
        
        public static string GetLobbyName(string a_strUkrGid, string a_strUkrDid)
        {
            string strRet = "";

            foreach (stLobbyId _st in m_listLobbyId)
            {
                if ((a_strUkrGid == _st.strUkrGid) &&
                    (a_strUkrDid == _st.strUkrDid))
                {
                    return _st.strLobbyName.ToUpper();
                }
            }

            return strRet;
        }

        public static void printNowTime()
        {
            Console.WriteLine("{0}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff"));
        }

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process _process = Process.GetCurrentProcess();

            if (ProcessChecker.IsOnlyProcess(_process.ProcessName) == true)
            {
                Application.EnableVisualStyles();

                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new FormMain());
            }
            else
            {
                MessageBox.Show(string.Format("이미 실행중인 프로그램입니다."));
            }
        }

        // 2021-08-10 추가=> 최준혁 책임 코드와 맞춤.
        //클라이언트 모드 시 소켓 접속 유무 검사
        public static bool IsConnected(Socket a_Socket)
        {
            try
            {
                if (a_Socket != null)
                {
                    return !((a_Socket.Poll(1000, SelectMode.SelectRead) && (a_Socket.Available == 0)) || !a_Socket.Connected);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            /*
            try
            {
                if (a_Socket != null && a_Socket.Connected)
                {
                    if (a_Socket.Poll(0, SelectMode.SelectRead))
                    {
                        byte[] buff = new byte[1];
                        if (a_Socket.Receive(buff, SocketFlags.Peek) == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            */
        }


        // UI 윈도에서 사용
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);
        [DllImport("user32.dll")]
        private static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);



        public static void CreateRoundRectRgn(Control a_control)
        {
            IntPtr ip = CreateRoundRectRgn(0, 0, a_control.Width, a_control.Height, 15, 15);
            int i = SetWindowRgn(a_control.Handle, ip, true);
        }

        // 데이터 존재 유무 확인
        public static Boolean isDBData(string a_strQry, int a_nCoulmnCnt)
        {
            List<string[]> qryListRet = Program.m_mysql.MySqlSelect(a_strQry, a_nCoulmnCnt);

            if (qryListRet.Count == 0)  // 결과가 없으면 false 리턴
                return false;

            return true;
        }


        // 공동현관 리더기에서 받은 데이터를 각각의 상항에 맞게 로그에 남김
        public static void doRecvDataDBLog(OPDevPtrl a_opDevPrtl, byte[] a_byte)
        {
            string strLobbyName = Program.GetLobbyName(a_opDevPrtl.byGid.ToString("X2"), a_opDevPrtl.byDid.ToString("X2"));

            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            switch (a_opDevPrtl.byType)
            {
                case 0xC0:  // 응답
                    switch (a_opDevPrtl.byCmd)
                    {
                        case 0x01:  // 상태 응답
                            Program.doDBLogKmsDevState(strLobbyName, "Recv Data = ", a_byte);
                            break;
                        case 0x02: // 리셋 응답
                        case 0x03: // 키 ID 응답
                        case 0x04: // 키 삭제 응답
                        case 0x05: // Ukr Key List 응답
                        case 0x06: // 문열림 응답
                        case 0x07: // 서버에서 UKR에 주차위치 전송 응답
                        case 0x08: // 서버에서 UKR에 BLE ID 등록 이벤트 요청 응답(BLE 등록기)
                        case 0x09: // FMCW 센서 제어 응답
                            Program.doDBLogKmsDev(strLobbyName, "Recv Data = ", a_byte);
                            break;
                    }
                    break;
                case 0xE0:  // 이벤트
                    switch (a_opDevPrtl.byCmd)
                    {
                        case 0x01:  // 기기 동작
                        case 0x02:  // 출입 인증
                            Program.doDBLogKmsDev(strLobbyName, "Recv Data = ", a_byte);
                            break;
                    }
                    break;
            }
        }

        
        public static void doDBLogKmsDevState(string a_strLobbyName, string a_strPreToken, byte[] a_by)
        {
            /*
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strPacket = a_strPreToken;
            for (int i = 0; i < a_by.Length; i++)
            {
                strPacket = strPacket + " " + a_by[i].ToString("X2");
            }

            string strQry = string.Format("Insert kms.Log_DevState(LogDate, LobbyName, Comment) values('{0}', '{1}', '{2}');", strTime, a_strLobbyName, strPacket);
            m_mysql.MySqlExec(strQry, "");
            */
        }

        public static void doDBLogKmsDev(string a_strLobbyName, string a_strPreToken, byte[] a_by)
        {
            /*
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strEvent = "";
            
            if (a_by != null)
            {
                strEvent = string.Format("{0} {1} {2}", a_by[3].ToString("X2"), a_by[4].ToString("X2"), a_by[5].ToString("X2"));
            }

            string strPacket = a_strPreToken;

            if (a_by != null)
            {
                for (int i = 0; i < a_by.Length; i++)
                {
                    strPacket = strPacket + " " + a_by[i].ToString("X2");
                }
            }

            // 키 리스트 요청시에는 패킷이 너무 길어 Key ID로 뛰어쓰기를 함
            if (strPacket.Length > 512)
            {
                strPacket = a_strPreToken;

                for (int i = 0; i < a_by.Length; i++)
                {
                    if (i < 9)
                    {
                        strPacket = strPacket + " " + a_by[i].ToString("X2");
                    }
                    else
                    {
                        if ((i%8) == 1 )
                        {
                            strPacket = strPacket + " " + a_by[i].ToString("X2");
                        }
                        else
                        {
                            strPacket = strPacket + a_by[i].ToString("X2");
                        }
                    }

                }
            }

            string strQry = string.Format("Insert kms.Log_KmsDev(LogDate, LobbyName, Event, Comment) values('{0}', '{1}', '{2}', '{3}');", strTime, a_strLobbyName, strEvent, strPacket);
            m_mysql.MySqlExec(strQry, "");
            */
        }

        public static void doDBLogKmsKeyRegDel(string a_strType, string a_strLobbyName, string a_strDong, string a_strHo, string a_strComment )
        {
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strQry = string.Format("Insert kms.Log_KmsKeyRegDel(LogDate, Type, LobbyName, Dong, Ho, Comment) values('{0}', '{1}', '{2}', {3}, {4}, '{5}');", strTime, a_strType, a_strLobbyName, a_strDong, a_strHo, a_strComment);
            m_mysql.MySqlExec(strQry, "");
        }

        public static void doDBLogKmsCrt(string a_strLobbyName, string a_strDong, string a_strHo, string a_strComment, string a_strPacket)
        {
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strQry = string.Format("Insert kms.Log_KmsCrt(LogDate, LobbyName, Dong, Ho, Comment, Packet) values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');", strTime, a_strLobbyName, a_strDong, a_strHo, a_strComment, a_strPacket);
            m_mysql.MySqlExec(strQry, "");
        }

        public static void doDBLogKmsWeb(string a_strUkrID, string a_strComment, string a_strPacket)
        {
            string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string strQry = string.Format("Insert kms.Log_KmsWeb(LogDate, UKR_ID, Comment, Packet) values('{0}', '{1}', '{2}', '{3}');", strTime, a_strUkrID, a_strComment, a_strPacket);
            m_mysql.MySqlExec(strQry, "");
        }


        public static void doDBLogKmsUkr(string a_strLobbyName, string a_strKeyId)
        {
            int nDong = 0, nHo = 0;
            GetDongHo(a_strKeyId, ref nDong, ref nHo);

            string strQry = string.Format("Insert kms.Log_KmsUKR(Log_Date, LobbyName, Dong, Ho, Key_ID) values('{0}', '{1}', {2}, {3}, '{4}');",
                System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), a_strLobbyName, nDong, nHo, a_strKeyId);
            m_mysql.MySqlExec(strQry, "");

            // 백업 테이블을 만들어야함.
            // 매년 1월 1일 4월 1일, 7월 1일, 10월 1일에 Log_KmsUkr테이블을 xxxx_xx_backup_Log_KmsUKR(ex: 2023_10_backup_Log_UKR) 테이블로 만듬
            string strMonth = System.DateTime.Now.ToString("MM-dd");
            if (strMonth == "01-01" ||
                strMonth == "04-01" ||
                strMonth == "07-01" ||
                strMonth == "10-01")
            {
                // 백업 테이블 존재 확인
                string strBackupName = string.Format("Backup_Log_KmsUKR_{0}", System.DateTime.Now.ToString("yyyy_MM"));
                if (!IsTable(strBackupName))
                {
                    // Log_KmsUkr테이블을 백업 테이블로 만듬
                    strQry = string.Format("ALTER TABLE kms.Log_KmsUKR RENAME TO {0} ;", strBackupName);
                    m_mysql.MySqlExec(strQry, "");

                    // Log_KmsUkr 테이블 새로 생성
                    CreateTable(TBL_CREATE_LOG_KMSUKR);
                }
            }
        }

        // 테이블 존재 판단
        public static bool IsTable(string a_strTblName)
        {
            string _strQry = string.Format("show tables where Tables_in_kms = '{0}';", a_strTblName);
            string _strName = "";

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 1);

            foreach (string[] _str in _qryList)
            {
                _strName = _str[0];
            }

            if (_strName != "")
                return true;
            else
                return false;
        }

        // 테이블 생성
        public static void CreateTable(string a_strQry)
        {
            try
            {
                m_mysql.MySqlExec(a_strQry);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CreateTable() 메소드 예외: " + ex.Message.ToString(), System.Drawing.Color.Red);
                Console.WriteLine("strQry = " + a_strQry, System.Drawing.Color.Red);

                m_logCrt.SetLogFile("Crt", "CreateTable() 메소드 예외: " + ex.Message.ToString());
                m_logMainList.SetLogFile("", "strQry = " + a_strQry);
            }
        }

        public static byte[] StrToByteArray(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++) 
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }

        ///////////////////////////////////////////////////
        // 동호 정보 찾기
        public static void GetDongHo(string a_strKeyID, ref int a_nDong, ref int a_nHo)
        {
            string _strQry = "";
            _strQry = string.Format("SELECT Dong,Ho from Key_Info_Master where Key_Id = '{0}';", a_strKeyID);

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                a_nDong = int.Parse(_str[0]);
                a_nHo = int.Parse(_str[1]);
            }
        }

        public static void GetDongHo(string a_strKeyID, ref int a_nDong, ref int a_nHo, int nType)
        {
            string _strQry = "";
            if (nType == 10)
            {
                _strQry = string.Format("SELECT Dong,Ho from kms.Key_Portable_Charger where Key_Id = '{0}';", a_strKeyID);
            }
            else
            {
                _strQry = string.Format("SELECT Dong,Ho from Key_Info_Master where Key_Id = '{0}';", a_strKeyID);
            }

            List<string[]> _qryList = m_mysql.MySqlSelect(_strQry, 2);

            foreach (string[] _str in _qryList)
            {
                a_nDong = int.Parse(_str[0]);
                a_nHo = int.Parse(_str[1]);
            }
        }

        public static string GetKeySn(string a_strKeyId)
        {
            string strQry = "", strRet = "";
            strQry = string.Format("SELECT Key_Sn from Key_Info_Master where Key_Id = '{0}';", a_strKeyId);
            
            List<string[]> qryList = m_mysql.MySqlSelect(strQry, 1);

            foreach (string[] str in qryList)
            {
                strRet = str[0];
            }

            return strRet;
        }
    }
}