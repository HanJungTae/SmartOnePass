using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SmartOnePass
{
    class DualiControl
    {
        //DLL IMPORT
        [DllImport("DualComm.dll")]
        private static extern int DUAL_InitPort(int nPort, int nBaud);

        [DllImport("DualComm.dll")]
        private static extern void DUAL_ClosePort(int nPort);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_ABM_RF_On(int nPort);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_AM_Idle_Req(int nPort, byte[] lpRes);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_AM_Anticoll(int nPort, byte[] data, byte[] lpRes);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_AM_Select(int nPort, byte[] data, byte[] lpRes);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_ABM_BuzzerOFF(int nPort);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_ABM_BuzzerON(int nPort);

        [DllImport("DualComm.dll")]
        private static extern int DUAL_AM_Transparent(int nPort, int datalen, byte[] data, ref int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_InitPort(int port, int baudrate);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_ClosePort(int port);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_FindCard(int port, byte baudrate, byte cid, byte nad, byte option, ref int outlen, byte[] result);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_APDU(int port, int datalen, byte[] data, ref int outlen, byte[] result);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_BuzzerOn(int port);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_BuzzerOff(int port);

        [DllImport("DualCardDll.dll")]
        private static extern int DEA_Idle_Req(int port, ref int outlen, byte[] result);

        [DllImport("DualCardDll.dll")]
        private static extern int DEA_Anticoll(int port, ref int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        private static extern int DEA_Select(int port, byte[] uid, ref int outlen, byte[] lpRes);
        //int DEA_Select(int nPort, LPBYTE uid, LPINT outlen, LPBYTE lpRes)

        //[DllImport("DualCardDll.dll")]
        //private static extern int DEC_Transparent(int port, int datalen, byte[] data, ref int outlen, byte[] lpRes, byte timeout);

        [DllImport("DualCardDll.dll")]
        private static extern int DE_NFC_Init_INITIATOR(int nPort, byte mode, byte speed, ref int outlen, byte[] lpRes);

        /*
         int DE_NFC_Init_INITIATOR(int nPort, BYTE mode, BYTE speed, LPINT outlen, LPBYTE lpRes)
         int DE_NFC_Init_TARGET(int nPort, BYTE timeout, LPINT outlen, LPBYTE lpRes)
         * int DE_NFC_SendData(int nPort,BYTE mode, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes)
         * int DE_NFC_Stop(int nPort, BYTE code)
         * int DE_NFC_GetTargetData(int nPort, LPINT outlen, LPBYTE lpRes)
         int DE_NFC_SetTargetData(int nPort, int datalen, LPBYTE data)
         * int DE_NFC_GetTargetState(int nPort, LPINT outlen, LPBYTE lpRes)
         * int DE_NFC_TAG_CMD(int nPort, BYTE TagType,BYTE TagCMD, int optdatalen, LPBYTE optdata, LPINT outlen, LPBYTE lpRes)
         * 
         */
        /* ISO 15693 API */


        //
        public int[] m_sSignCode = new int[256] {    41,   35, 190, 132, 225, 108, 214, 174,  82, 144,  73, 241, 187, 233, 235, 179,    // 15
			                                        166,  219,  60, 135,  12,  62, 153,  36,  94,  13,  28,   6, 183,  71, 222,  18,	// 31
			                                         77,  200,  67, 139,  31,   3,  90, 125,   9,  56,  37,  93, 212, 203, 252, 150,	// 47
			                                        245,   69,  59,  19, 137,  10,  50,  32, 154,  80, 238,  64, 120,  54, 253, 246,	// 63
			                                        158,  220, 173,  79,  20, 242,  68, 102, 208, 107, 196,  48, 161,  34, 145, 157,	// 79
			                                        218,  176, 202,   2, 185, 114,  44, 128, 126, 197, 213, 178, 234, 201, 204,  83,	// 95
			                                        191,  103,  45, 142, 131, 239,  87,  97, 255, 105, 143, 205, 209,  30, 156,  22,	// 111
			                                        230,   29, 240,  74, 119, 215, 232,  57,  51, 116, 244, 159, 164,  89,  53, 207,	// 127
			                                        211,   72, 117, 217,  42, 229, 192, 247,  43, 129,  14,  95,   0, 141, 123,   5,	// 143
			                                         21,    7, 130,  24, 112, 146, 100,  84, 206, 177, 133, 248,  70, 106,   4, 115,	// 159
			                                         47,  104, 118, 250,  17, 136, 121, 254, 216,  40,  11,  96,  61, 151,  39, 138,	// 175
			                                        194,    8, 165, 193, 140, 169, 149, 155, 168, 167, 134, 181, 231,  85,  78, 113,	// 191
			                                        226,  180, 101, 122,  99,  38, 223, 109,  98, 224,  52,  63, 227,  65,  15,  27,	// 207
			                                        243,  160, 127, 170,  91, 184,  58,  16,  76, 236,  49,  66, 124, 228,  33, 147,	// 223
			                                        175,  111,   1,  23,  86, 198, 249,  55, 189, 110,  92, 195, 163, 152, 199, 182,	// 239
			                                         81,   25,  46, 188, 148,  75,  88, 210, 172,  26, 162, 237, 251, 221, 186, 171};   //255

        private string m_strCardID = "";
        private string m_strCardSn = "";
        public string m_strCardDecHex = "";
        public string m_strCardDecAscii = "";

        public string StrCardId
        {
            get { return m_strCardID; }
            set { m_strCardID = value; }
        }

        public string StrCardSn
        {
            get { return m_strCardSn; }
            set { m_strCardSn = value; }
        }

        public int GetSignInt(int a_nInput)
        {
            int nValue = m_sSignCode[a_nInput];
            return nValue;
        }

        public string GetSignString(int a_nInput)
        {
            string strRet = "";
            int nValue = GetSignInt(a_nInput);

            strRet = nValue.ToString("x2");
            return strRet;
        }

        public int HexToString(string a_strInput, byte[] a_byOutput)
        {
            int nSize = a_strInput.Length;
            a_strInput = a_strInput.ToLower();

            for (int i = 0; i < nSize; i++)
            {
                string strTemp = a_strInput.Substring(i, 1);
                if (strTemp.CompareTo("a") == 0)
                {
                    a_byOutput[i] = 0x41;
                }
                else if (strTemp.CompareTo("b") == 0)
                {
                    a_byOutput[i] = 0x42;
                }
                else if (strTemp.CompareTo("c") == 0)
                {
                    a_byOutput[i] = 0x43;
                }
                else if (strTemp.CompareTo("d") == 0)
                {
                    a_byOutput[i] = 0x44;
                }
                else if (strTemp.CompareTo("e") == 0)
                {
                    a_byOutput[i] = 0x45;
                }
                else if (strTemp.CompareTo("f") == 0)
                {
                    a_byOutput[i] = 0x46;
                }
                else
                {
                    a_byOutput[i] = 0x30;
                    a_byOutput[i] += byte.Parse(strTemp);
                }
            }

            return 1;
        }

        // 구형(Tomney, 마이페어)
        public int ReadCard()
        {
            int nRet = 0;
            int nPort = 0;
            int nBaud = 19200;

            byte[] byRes = new byte[128];
            byte[] byCmd = new byte[128];
            byte[] Snr = new byte[128];

            nPort = DUAL_InitPort(100, nBaud);
            if (nPort != 100)
            {
                return -1;
            }

            // * 1 RF On
            nRet = DUAL_ABM_RF_On(nPort);
            if (nRet != 0)
            {
                DUAL_ClosePort(nPort);
                return 0;
            }

            // * 2. Idle Request
            nRet = DUAL_AM_Idle_Req(nPort, byRes);
            if (nRet != 0 || byRes[0] != 0x04)
            {
                DUAL_ClosePort(nPort);
                return 0;
            }

            // * 3 Anticall
            byCmd[0] = 0x00;
            byCmd[1] = 0x02;
            byCmd[2] = 0x23;
            byCmd[3] = 0x00;

            nRet = DUAL_AM_Anticoll(nPort, byCmd, byRes);
            if (nRet != 0)
            {
                DUAL_ClosePort(nPort);
                return 0;
            }

            // * 4. Select
            Snr[0] = byRes[0];
            Snr[1] = byRes[1];
            Snr[2] = byRes[2];
            Snr[3] = byRes[3];

            Array.Clear(byCmd, 0, 128);

            Array.Clear(byRes, 0, 128);

            byCmd[0] = Snr[0];
            byCmd[1] = Snr[1];
            byCmd[2] = Snr[2];
            byCmd[3] = Snr[3];
            nRet = DUAL_AM_Select(nPort, Snr, byRes);
            if (nRet != 0)
            {
                DUAL_ClosePort(nPort);
                return 0;
            }

            // * 5. MiFare이면 Dlg에 ID를 보여 준다.
            if (byRes[0] == 0x08/* || byRes[0] == 0x20*/)
            {
                // * 5-1. Beep음
                DUAL_ABM_BuzzerON(nPort);
                Thread.Sleep(300);
                DUAL_ABM_BuzzerOFF(nPort);
                m_strCardID = Snr[3].ToString("x2") + Snr[2].ToString("x2") + Snr[1].ToString("x2") + Snr[0].ToString("x2");
                //DUAL_ClosePort(nPort);

                // ID를 암호화 시킴.
                HexToString(m_strCardID, byRes);

                m_strCardSn = "";
                m_strCardDecAscii = m_strCardID.ToUpper();
                m_strCardDecHex = byRes[0].ToString("X2")
                                + byRes[1].ToString("X2")
                                + byRes[2].ToString("X2")
                                + byRes[3].ToString("X2")
                                + byRes[4].ToString("X2")
                                + byRes[5].ToString("X2")
                                + byRes[6].ToString("X2")
                                + byRes[7].ToString("X2");
                m_strCardID = GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2])
                            + GetSignString(byRes[1])
                            + GetSignString(byRes[0]);
                //return 1;
            }
            else
            {
                Array.Clear(byCmd, 0, 128);
                byCmd[0] = 0xe0;
                byCmd[1] = 0x50;
                byCmd[2] = 0x64;

                int nLen = 0;
                nRet = DUAL_AM_Transparent(nPort, 3, byCmd, ref nLen, byRes);
                if (nRet != 0)
                {
                    DUAL_ClosePort(nPort);
                    return 0;
                }

                Array.Clear(byCmd, 0, 128);
                byCmd[0] = 0x0A;
                byCmd[1] = 0x00;
                byCmd[2] = 0x00;
                byCmd[3] = 0xa4;
                byCmd[4] = 0x04;
                byCmd[5] = 0x00;
                byCmd[6] = 0x07;
                byCmd[7] = 0xd4;
                byCmd[8] = 0x10;
                byCmd[9] = 0x00;
                byCmd[10] = 0x00;
                byCmd[11] = 0x03;
                byCmd[12] = 0x00;
                byCmd[13] = 0x01;
                byCmd[14] = 0x64;

                nRet = DUAL_AM_Transparent(nPort, 15, byCmd, ref nLen, byRes);
                if (nRet != 0)
                {
                    DUAL_ClosePort(nPort);
                    return 0;
                }

                Array.Clear(byCmd, 0, 128);
                byCmd[0] = 0x0A;
                byCmd[1] = 0x00;
                byCmd[2] = 0x00;
                byCmd[3] = 0xca;
                byCmd[4] = 0x01;
                byCmd[5] = 0x01;
                byCmd[6] = 0x08;
                byCmd[7] = 0x64;

                nRet = DUAL_AM_Transparent(nPort, 8, byCmd, ref nLen, byRes);
                
                if (nRet != 0)
                {
                    DUAL_ClosePort(nPort);
                    return 0;
                }
                

                if (m_strCardID.CompareTo("0000000000000000") == 0)
                {
                    DUAL_ClosePort(nPort);
                    return 0;
                }

                // * 7. Beep음
                //DUAL_ABM_BuzzerON(nPort);
                Thread.Sleep(300);
                //DUAL_ABM_BuzzerOFF(nPort);

                m_strCardID = GetSignString(byRes[9])
                            + GetSignString(byRes[8])
                            + GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2]);
                m_strCardID = m_strCardID.ToUpper();

                m_strCardDecHex = byRes[2].ToString("X2")
                            + byRes[3].ToString("X2")
                            + byRes[4].ToString("X2")
                            + byRes[5].ToString("X2")
                            + byRes[6].ToString("X2")
                            + byRes[7].ToString("X2")
                            + byRes[8].ToString("X2")
                            + byRes[9].ToString("X2");
                m_strCardDecAscii = "";

                // * 9. Sn Data
                Int64 nSnData = 0;
                int nSn1 = GetSignInt(byRes[9]);
                int nSn2 = GetSignInt(byRes[8]);
                int nSn3 = GetSignInt(byRes[7]);

                nSnData = (nSn1 * 0x010000) + (nSn2 * 0x0100) + nSn3;

                m_strCardSn = nSnData.ToString("d8");
            }

            DUAL_ClosePort(nPort);
            return 1;
        }

        // 신형(Tmoney, Mifare, 캐시비)
        public int ReadCard2()
        {
            int nPort = 0;

            int nLen = 0;

            byte[] byRes = new byte[128];
            byte[] byCmd = new byte[128];
            byte[] Snr = new byte[128];

            nPort = DE_InitPort(100, 0x00);

            if (nPort != 100)
                return -1;

            if (DE_FindCard(100, 0x00, 0x00, 0x01, 0x00, ref nLen, byRes) == 2)
            {
                DE_ClosePort(nPort);

                return 0;
            }

            // * 5. MiFare이면 Dlg에 ID를 보여 준다.
            if (byRes[1] == 0x4D)
            {
                // * 5-1. Beep음
                DE_BuzzerOn(nPort);

                Thread.Sleep(300);

                DE_BuzzerOff(nPort);

                m_strCardID = byRes[6].ToString("x2") + byRes[5].ToString("x2") + byRes[4].ToString("x2") + byRes[3].ToString("x2");

                DE_ClosePort(nPort);

                // ID를 암호화 시킴.
                HexToString(m_strCardID, byRes);

                m_strCardSn = "";
                m_strCardDecAscii = m_strCardID.ToUpper();
                m_strCardDecHex = byRes[0].ToString("X2")
                                + byRes[1].ToString("X2")
                                + byRes[2].ToString("X2")
                                + byRes[3].ToString("X2")
                                + byRes[4].ToString("X2")
                                + byRes[5].ToString("X2")
                                + byRes[6].ToString("X2")
                                + byRes[7].ToString("X2");
                m_strCardID = GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2])
                            + GetSignString(byRes[1])
                            + GetSignString(byRes[0]);

                // 마이페어는 SN생성 하지 않음
                // S 000 00000
                // 앞3자리는 현장 뒤 5자리는 일련번호

                DE_ClosePort(100);

                return 1;
            }
            else
            {
                /*
                byCmd[0] = 0x00;
                byCmd[1] = 0xCA;
                byCmd[2] = 0x01;
                byCmd[3] = 0x01;
                byCmd[4] = 0x08;
                 */
                
                byCmd[0] = 0xC0;
                byCmd[1] = 0x20;
                byCmd[2] = 0x00;
                byCmd[3] = 0x01;
                byCmd[4] = 0x08;
                

                Array.Clear(byRes, 0, 128);

                int nRet = DE_APDU(nPort, 5, byCmd, ref nLen, byRes);

                // * 7. Beep음
                DE_BuzzerOn(nPort);

                Thread.Sleep(300);

                DE_BuzzerOff(nPort);

                m_strCardID = GetSignString(byRes[8])
                            + GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2])
                            + GetSignString(byRes[1]);
                m_strCardID = m_strCardID.ToUpper();

                m_strCardDecHex = byRes[1].ToString("X2")
                            + byRes[2].ToString("X2")
                            + byRes[3].ToString("X2")
                            + byRes[4].ToString("X2")
                            + byRes[5].ToString("X2")
                            + byRes[6].ToString("X2")
                            + byRes[7].ToString("X2")
                            + byRes[8].ToString("X2");
                m_strCardDecAscii = "";

                // * 9. Sn Data
                Int64 nSnData = 0;
                int nSn1 = GetSignInt(byRes[8]);
                int nSn2 = GetSignInt(byRes[7]);
                int nSn3 = GetSignInt(byRes[6]);

                nSnData = (nSn1 * 0x010000) + (nSn2 * 0x0100) + nSn3;

                m_strCardSn = nSnData.ToString("d8");

                DE_ClosePort(100);

                return 1;
            }
        }

        public int ReadCard3()
        {
            int nPort = 0;

            int nLen = 0;

            byte[] byRes = new byte[128];
            byte[] byCmd = new byte[128];
            byte[] Snr = new byte[128];

            nPort = DE_InitPort(100, 0x00);

            if (DE_FindCard(100, 0x00, 0x00, 0x01, 0x00, ref nLen, byRes) == 2)
            {
                DE_ClosePort(nPort);

                return 0;
            }

            if (nPort != 100)
                return -1;

            int nRet = DE_NFC_Init_INITIATOR(nPort, 0x50, 0x00, ref nLen, byRes);

            return 1;
        }

        // MapViewer에 있는 코드를 가지고 옮
        public int ReadCard4()
        {
            int nPort = 0;

            int nLen = 0;

            byte[] byRes = new byte[128];
            byte[] byCmd = new byte[128];
            byte[] Snr = new byte[128];

            nPort = DE_InitPort(100, 0x00);

            if (nPort != 100)
                return -1;

            if (DE_FindCard(100, 0x00, 0x00, 0x01, 0x00, ref nLen, byRes) == 2)
            {
                DE_ClosePort(nPort);

                return 0;
            }

            // * 5. MiFare이면 Dlg에 ID를 보여 준다.
            if (byRes[1] == 0x4D)
            {
                // * 5-1. Beep음
                DE_BuzzerOn(nPort);

                Thread.Sleep(300);

                DE_BuzzerOff(nPort);

                m_strCardID = byRes[6].ToString("x2") + byRes[5].ToString("x2") + byRes[4].ToString("x2") + byRes[3].ToString("x2");

                DE_ClosePort(nPort);

                // ID를 암호화 시킴.
                HexToString(m_strCardID, byRes);

                m_strCardSn = "";
                m_strCardDecAscii = m_strCardID.ToUpper();
                m_strCardDecHex = byRes[0].ToString("X2")
                                + byRes[1].ToString("X2")
                                + byRes[2].ToString("X2")
                                + byRes[3].ToString("X2")
                                + byRes[4].ToString("X2")
                                + byRes[5].ToString("X2")
                                + byRes[6].ToString("X2")
                                + byRes[7].ToString("X2");
                m_strCardID = GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2])
                            + GetSignString(byRes[1])
                            + GetSignString(byRes[0]);

                // 마이페어는 SN생성 하지 않음
                // S 000 00000
                // 앞3자리는 현장 뒤 5자리는 일련번호

                DE_ClosePort(100);

                return 1;
            }
            else
            {
                byCmd[0] = 0x00;
                byCmd[1] = 0xCA;
                byCmd[2] = 0x01;
                byCmd[3] = 0x01;
                byCmd[4] = 0x08;

                Array.Clear(byRes, 0, 128);

                DE_APDU(nPort, 5, byCmd, ref nLen, byRes);

                // * 7. Beep음
                DE_BuzzerOn(nPort);

                Thread.Sleep(300);

                DE_BuzzerOff(nPort);

                m_strCardID = GetSignString(byRes[8])
                            + GetSignString(byRes[7])
                            + GetSignString(byRes[6])
                            + GetSignString(byRes[5])
                            + GetSignString(byRes[4])
                            + GetSignString(byRes[3])
                            + GetSignString(byRes[2])
                            + GetSignString(byRes[1]);
                m_strCardID = m_strCardID.ToUpper();

                m_strCardDecHex = byRes[1].ToString("X2")
                            + byRes[2].ToString("X2")
                            + byRes[3].ToString("X2")
                            + byRes[4].ToString("X2")
                            + byRes[5].ToString("X2")
                            + byRes[6].ToString("X2")
                            + byRes[7].ToString("X2")
                            + byRes[8].ToString("X2");
                m_strCardDecAscii = "";

                // * 9. Sn Data
                Int64 nSnData = 0;
                int nSn1 = GetSignInt(byRes[8]);
                int nSn2 = GetSignInt(byRes[7]);
                int nSn3 = GetSignInt(byRes[6]);

                nSnData = (nSn1 * 0x010000) + (nSn2 * 0x0100) + nSn3;

                m_strCardSn = nSnData.ToString("d8");

                DE_ClosePort(100);

                return 1;
            }
        }

        public string Demodulation(string a_strInput)
        {
            string strRet = "";

            if (a_strInput.Length != 16)
            {
                // 길이가 맞지 않으면 값을 NULL을 반환한다.

                return "";
            }
            else
            {
                string str1 = "";
                string str2 = "";
                string str3 = "";
                string str4 = "";
                string str5 = "";
                string str6 = "";
                string str7 = "";
                string str8 = "";

                // 리버스로 데이터를 받는다.
                str1 = a_strInput.Substring(0, 2);
                str2 = a_strInput.Substring(2, 2);
                str3 = a_strInput.Substring(4, 2);
                str4 = a_strInput.Substring(6, 2);
                str5 = a_strInput.Substring(8, 2);
                str6 = a_strInput.Substring(10, 2);
                str7 = a_strInput.Substring(12, 2);
                str8 = a_strInput.Substring(14, 2);

                int nIdx1 = 0;
                int nIdx2 = 0;
                int nIdx3 = 0;
                int nIdx4 = 0;
                int nIdx5 = 0;
                int nIdx6 = 0;
                int nIdx7 = 0;
                int nIdx8 = 0;

                nIdx1 = DecCalc(str8);
                nIdx2 = DecCalc(str7);
                nIdx3 = DecCalc(str6);
                nIdx4 = DecCalc(str5);
                nIdx5 = DecCalc(str4);
                nIdx6 = DecCalc(str3);
                nIdx7 = DecCalc(str2);
                nIdx8 = DecCalc(str1);

                if (nIdx1 >= 0 && nIdx2 >= 0 && nIdx3 >= 0 && nIdx4 >= 0 && nIdx5 >= 0 && nIdx6 >= 0 && nIdx7 >= 0 && nIdx8 >= 0)
                {
                    string strSignValue1 = "";
                    string strSignValue2 = "";
                    string strSignValue3 = "";
                    string strSignValue4 = "";
                    string strSignValue5 = "";
                    string strSignValue6 = "";
                    string strSignValue7 = "";
                    string strSignValue8 = "";

                    strSignValue1 = GetSignIdx(nIdx1);
                    strSignValue2 = GetSignIdx(nIdx2);
                    strSignValue3 = GetSignIdx(nIdx3);
                    strSignValue4 = GetSignIdx(nIdx4);
                    strSignValue5 = GetSignIdx(nIdx5);
                    strSignValue6 = GetSignIdx(nIdx6);
                    strSignValue7 = GetSignIdx(nIdx7);
                    strSignValue8 = GetSignIdx(nIdx8);

                    strRet = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", strSignValue1, strSignValue2, strSignValue3,
                        strSignValue4, strSignValue5, strSignValue6, strSignValue7, strSignValue8);
                    strRet.ToUpper();

                }
                else
                {
                    return "";
                }
            }
            return strRet;
        }

        int DecCalc(string a_strInput)
        {
            int nRet = 0;

            nRet = Convert.ToByte(a_strInput, 16);

            return nRet;
        }

        string GetSignIdx(int a_nInput)
        {
            string strOutPut = "";

            for (int i = 0; i < 256; i++)
            {
                if (a_nInput == m_sSignCode[i])
                {
                    strOutPut = string.Format("{0:X2}", i);
                    return strOutPut;
                }
            }

            return strOutPut;
        }
    }
}
