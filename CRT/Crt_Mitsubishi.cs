using System;
using System.Text;
using SmartOnePass.CRT;

namespace SmartOnePass
{
    class Crt_Mitsubishi : CRTBase
    {
        public override bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff)
        {
            try
            {
                string dong = string.Format("{0:X4}", a_nDong);
                string ho = string.Format("{0:X4}", a_nHo);

                System.Console.WriteLine("동 : {0} 호 : {1}", a_nDong, a_nHo);

                byte[] byDong = ToByteArray(dong);
                byte[] byHo = ToByteArray(ho);

                byte[] _byPacket = new byte[18];

                for (int i = 0; i < _byPacket.Length; i++ )
                    _byPacket[i] = 0x00;

                _byPacket[0] = 0x53;            // S
                _byPacket[1] = 0x54;            // T
                _byPacket[2] = 0x58;            // X
                _byPacket[3] = 0x00;            // 제어응답Flag  0x00: 제어, 0x01: 응답
                _byPacket[4] = 0x02;            // 요청 콜
                _byPacket[5] = a_byCrtId;       // 호기
                _byPacket[6] = byDong[0];       // 동  ex) 101 => 0x00
                _byPacket[7] = byDong[1];       // 동  ex) 101 => 0x65
                _byPacket[8] = byHo[0];         // 호  ex) 101 => 0x00
                _byPacket[9] = byHo[1];         // 호  ex) 101 => 0x65
                _byPacket[10] = a_byTakeOn;     // 탑승층
                _byPacket[11] = 0x01;           // 호출 방향
                _byPacket[15] = 0x45;           // E
                _byPacket[16] = 0x54;           // T
                _byPacket[17] = 0x58;           // X


                if (this.IsCrtConnected())
                {
                    /////////////////////////////////
                    // Crt Send
                    int nRet = m_hSocket.Send(_byPacket);
                    string strMsg = "Send: ";

                    for (int i = 0; i < nRet; i++)
                        strMsg = strMsg + " " + _byPacket[i].ToString("X2");

                    if (m_fnLogPrint != null)
                    {
                        string _strData = strMsg + " Dong = " + a_nDong + "  Ho = " + a_nHo;
                        
                        m_fnLogPrint("Send Packet = " + _strData);
                    }

                    // DB에 Log 저장(엘리베이터 호출 정상)
                    string strCommnet = string.Format("Car ID:{0}, Take On:{1}, Off:{2}", a_byCrtId, a_byTakeOn, a_byTakeOff);
                    Program.doDBLogKmsCrt(a_strLobbyName, a_nDong.ToString(), a_nHo.ToString(), strCommnet, strMsg);

                    return true;
                }
                else
                {
                    if (m_fnLogPrint != null)
                        m_fnLogPrint("엘리베이터 서버와 연결이 끊어 졌습니다.");

                    return false;
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtCall() 예외 발생:" + ex.Message.ToString());
                return false;
            }
        }

        public byte[] ToByteArray(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length / 2; i++) 
                bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);

            return bytes;
        }
    }
}
