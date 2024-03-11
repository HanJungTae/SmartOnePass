using System;
using System.Text;

namespace SmartOnePass.CRT
{
    class Crt_ThyssenKrupp : CRTBase
    {
        public override bool CrtCall(string a_strLobbyName, int a_nDong, int a_nHo, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff, float delyTime)
        {
            // 리스트에 담는다.
            CRT_CALL call = new CRT_CALL();

            call.TakeOn = a_byTakeOn;
            call.TakeOff = a_byTakeOff;
            call.Dong = a_nDong;
            call.Ho = a_nHo;

            m_CallList[a_byCrtId].Add(call);
            

            // System.Console.WriteLine("호출 정보 등록 : CAR {0} / TakeOn {1} / TakeOff {2}", a_byCrtId, a_byTakeOn, a_byTakeOff);

            return Send(a_strLobbyName, 0x02, a_byCrtId, a_byTakeOn, a_byTakeOff, a_nDong, a_nHo);
        }

        public bool Send(string a_strLobbyName, byte a_byType, byte a_byCrtId, byte a_byTakeOn, byte a_byTakeOff, int a_nDong, int a_nHo)
        {
            try
            {
                byte[] _byPacket = new byte[18];

                for (int i = 0; i < _byPacket.Length; i++)
                    _byPacket[i] = 0x00;

                _byPacket[0] = 0x53;        // S
                _byPacket[1] = 0x54;        // T
                _byPacket[2] = 0x58;        // X
                _byPacket[3] = a_byType;    // Hall Call제어 요청
                _byPacket[4] = a_byCrtId;   // 호출 호기
                _byPacket[9] = (byte)(a_byTakeOn);  // 탑승충
                _byPacket[10] = 0x01;       // 상향호출
                _byPacket[15] = 0x45;       // E
                _byPacket[16] = 0x54;       // T
                _byPacket[17] = 0x58;       // X

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

                        string log = string.Format("[SEND] Type {0:X2} / Car 0x{1:X2} / Floor {2:X2}, {3}, Dong = {4}, Ho = {5}", a_byType, a_byCrtId, a_byTakeOn, strMsg, a_nDong, a_nHo);

                        m_fnLogPrint(log);
                    }
                    // DB에 Log 저장(엘리베이터 호출 정상)
                    string strCommnet = string.Format("Call Type:{0}, Car ID:{1}, Take On:{2}, Off:{3}", a_byType, a_byCrtId, a_byTakeOn, a_byTakeOff);
                    Program.doDBLogKmsCrt(a_strLobbyName, a_nDong.ToString(), a_nHo.ToString(), strCommnet, strMsg);
                }
                else
                    return false;

                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("CrtCall() 예외 발생:" + ex.Message.ToString());

                return false;
            }
        }

        public override void MessageResolver(byte[] message, int length)
        {
            byte[] msg = new byte[1024];

            Array.Copy(message, msg, length);
            // System.Console.WriteLine(" 1: CAR {0} / FLOOR {1} / DOOR {2} / STATE {3} / DIR {4}", msg[4], msg[9], msg[12], msg[11], msg[10]);

            // 패킷의 크기는 18byte 이므로 18byte 크기로 잘라서 파싱 한다.
            for (int i = 0; i < msg.Length; i+=18)
            {
                // 상태정보
                if (msg[i + 3] != 0x01)
                    break;

                // 엘리베이터 호기
                byte car = msg[i + 4];

                // 엘리베이터 방향
                // 상향버튼이 눌러진 상태에서만 호출 한다.
                if (msg[i + 10] != 0x01)
                    continue;

                // 문이 열릴 때 호출 해 준다.
                if (msg[i + 12] != 0x02)
                    continue;

                System.Console.WriteLine(" 3: CAR {0} / FLOOR {1} / DOOR {2} / STATE {3}", car, msg[i + 9], msg[i + 12], msg[i + 11]);

                // 해당 호기의 호출정보 확인
                if (m_CallList.ContainsKey(car) == false)
                    continue;

                System.Console.WriteLine(" 4: CAR {0} / FLOOR {1} / DOOR {2} / STATE {3}", car, msg[i + 9], msg[i + 12], msg[i + 11]);

                for (int j =  m_CallList[car].Count - 1; j >= 0; j--)
                {
                    System.Console.WriteLine(" 5: {0} 호출 리스트 / TakeOn {1} / TakeOff {2}", car,  m_CallList[car][j].TakeOn, m_CallList[car][j].TakeOff);
                    
                    if (m_CallList[car][j].TakeOn != msg[i + 9])
                        continue;

                    // 목적층 호출(목적층 호출시에는 로비 정보를 알 수 없음)
                    Send("", 0x03, car, m_CallList[car][j].TakeOff, m_CallList[car][j].TakeOn, m_CallList[car][j].Dong, m_CallList[car][j].Ho);

                    System.Console.WriteLine(" 6: {0} 호출 / TakeOn {1} / TakeOff{2}", car, m_CallList[car][j].TakeOn, m_CallList[car][j].TakeOff);

                    m_CallList[car].Remove(m_CallList[car][j]);
                }
            }
        }
    }
}
