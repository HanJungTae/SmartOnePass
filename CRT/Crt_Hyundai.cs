using System.Text;
using SmartOnePass.CRT;

namespace SmartOnePass
{
    /*
     현대 엘리베이터 연동 Class
     * CRT 업체 특징
     */
    class Crt_Hyundai : CRTBase
    {
        public override bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff)
        {
            try
            {
                byte[] _byPacket = new byte[39 + 6];

                for (int i = 0; i < _byPacket.Length; i++ )
                    _byPacket[i] = 0x30;

                _byPacket[0] = 0x53;    // S
                _byPacket[1] = 0x54;    // T
                _byPacket[2] = 0x58;    // X
                _byPacket[3] = (byte)(0x30 + a_byCrtId);        // 엘리베이터 호기
                _byPacket[40] = (byte)(0x30 + a_byTakeOn);      // 탑승층
                _byPacket[41] = (byte)(0x30 + a_byTakeOff);     // 목적층
                _byPacket[42] = 0x45;    // E
                _byPacket[43] = 0x54;    // T
                _byPacket[44] = 0x58;    // X

                if (this.IsCrtConnected())
                {
                    /////////////////////////////////
                    // Crt Send
                    int nRet = m_hSocket.Send(_byPacket);
                    string strMsg = "Send: ";
                    for (int i = 0; i < nRet; i++)
                    {
                        strMsg = strMsg + " " + _byPacket[i].ToString("X2");
                    }

                    if (m_fnLogPrint != null)
                    {
                        string _strData = Encoding.Default.GetString(_byPacket, 0, nRet);
                        _strData = _strData + " Dong = " + a_nDong + "  Ho = " + a_nHo;
                        m_fnLogPrint("Send Packet = " + _strData);
                    }

                    // DB에 Log 저장
                    string strCommnet = string.Format("Car ID:{0}, Take On:{1}, Off:{2}", a_byCrtId,  a_byTakeOn, a_byTakeOff);
                    Program.doDBLogKmsCrt(a_strLobbyName, a_nDong.ToString(), a_nHo.ToString(), strCommnet, strMsg);
                }
                else
                {
                    m_fnLogPrint("접속 오류");
                    return false;
                }
                return true;
            }
            catch (System.Exception ex)
            {
                m_fnLogPrint("CrtCall() Hyundai 예외 발생:" + ex.Message.ToString());
                return false;
            }
        }
    }
}
