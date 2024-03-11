using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SmartOnePass
{
    public struct OPDevPtrl
    {
        public Socket socket;
        public byte bySeq;
        public byte byGid;
        public byte byDid;
        public byte byType;
        public byte byCmd;
        public byte byOpt;
        public byte byLen;
        public byte[] byData;
    }

    class OPProtocal
    {
        // 2020-05-06 수정 삭제
//        public delegate void MyEventHanderParsingComplete(object sender, EventArgs e);

//        public event MyEventHanderParsingComplete EvtParsingComplete;

        // Log 기록 추가
        private Log m_logException = new Log("LogCrt");

        private Queue<OPDevPtrl> m_prtcQue = null;

        public OPProtocal()
        {
            m_prtcQue = new Queue<OPDevPtrl>();
        }

        public Queue<OPDevPtrl> HLDevPrtlParsingQueData
        {
            get { return m_prtcQue; }
        }

        ////////////////////////////////////////////////////////
        // 현재 UPIS 프로토콜로 Data를 진행하는데, 
        // 향 후, 발열선로 Update 되어야 됨.
        // Gid, Did의 ID값은 PrtcParsing()에서 할당 하기 어려워, 
        // byData[0] = Gid, byData[1] = Did에 Data 값이 있음.
        public OPDevPtrl PrtcParsing(Socket _socket, byte[] a_byte, int nSize)
        {
            OPDevPtrl _opDevPrtl = new OPDevPtrl();

            try
            {
                int _nState = 0, _nLoop = 0;
                byte _byData = 0x00;
                byte _byCrc = 0x00;
                

                byte _byEmuCode = 0x00;
                //Console.Write("[Prtc]");
                for (int i = 0; i < nSize; i++)
                {
                    _byData = a_byte[i];
                    //Console.Write(_byData.ToString("X2") + " ");

                    switch (_nState)
                    {
                        case 0: // Stx
                            if (_byData == 0x02)
                            {
                                _nState = 1;
                                _nLoop = 0;
                                _byCrc = 0x00;
                            }
                            break;

                        case 1: // Mac
                            _byEmuCode = _byData;
                            if ((_byData == 0x33) || (_byData == 0x55))
                                _nState = 2;
                            else
                                _nState = 0;
                            break;

                        case 2: // Seq
                            _opDevPrtl.bySeq = _byData;
                            _nState = 3;
                            break;

                        case 3: // Type
                            _opDevPrtl.byType = _byData;
                            _nState = 4;
                            break;

                        case 4: // Cmd
                            _opDevPrtl.byCmd = _byData;
                            _nState = 5;
                            break;

                        case 5: // Opt
                            _opDevPrtl.byOpt = _byData;
                            _nState = 6;
                            break;

                        case 6: // Len
                            _opDevPrtl.byLen = _byData;
                            if (_opDevPrtl.byLen != 0x00)
                            {
                                _opDevPrtl.byData = new byte[_opDevPrtl.byLen];
                                _nState = 7;
                            }
                            else
                                _nState = 8;
                            break;

                        case 7:
                            // 예외 처리
                            if (_nLoop > _opDevPrtl.byLen)
                                _nState = 0;

                            _opDevPrtl.byData[_nLoop] = _byData;
                            _nLoop++;
                            if (_nLoop == _opDevPrtl.byLen)
                                _nState = 8;


                            break;

                        case 8:
                            if (_byCrc == _byData)  // Crc 체크(현재는 Crc체크 안함)
                            {
                            }
                            else
                            {
                            }
                            _nState = 9;
                            break;

                        case 9:
                            if (_byData == 0x03)
                            {
                                _opDevPrtl.socket = _socket;

                                //Console.WriteLine();
                                // Gid, Did 할당
                                // Data에 첫번째와 두번째가 Gid와 Did인데, 만약 
                                // 프로토콜에서 Gid, Did 부분이바뀌면 여기 로직을 
                                // 수정 해야 함.
                                // Ack 데이터가 아닐경우는 Ack 데이터 전송
                                if (_opDevPrtl.byType == 0x06)
                                {
                                    //Console.WriteLine("Ack데이타 수신");
                                }
                                else
                                {
                                    SendAck(_socket, _opDevPrtl);
                                    if (_byEmuCode == 0x55)
                                    // 에뮬레이터 처리를 위한 코드로 실 제품을 사용할 때는 필요가 없음
                                    {
                                        _opDevPrtl.byGid = _opDevPrtl.byData[1];
                                        _opDevPrtl.byDid = _opDevPrtl.byData[0];
                                    }
                                    else
                                    {
                                        _opDevPrtl.byGid = _opDevPrtl.byData[0];
                                        _opDevPrtl.byDid = _opDevPrtl.byData[1];
                                    }

                                    // DB에 Recv Data 로그 기록(doDBLog에서 Table에 맞게 로그 기록을 남김);
                                    Program.doRecvDataDBLog(_opDevPrtl, a_byte);
                                    
                                    return _opDevPrtl;
                                }
                                _nState = 1;
                                _nLoop = 0;
                                _byCrc = 0x00;
                            }
                            break;

                        default:
                            _nState = 1;
                            _nLoop = 0;
                            _byCrc = 0x00;
                            break;
                    }
                }
            }
            catch (System.Exception ex)
            {
                // 2020-05-06 예외 처리(파싱이 정확히 되면 파싱이 된 후에 Return
                Console.WriteLine("PrtcParsing()에서 예외 발생:" + ex.Message.ToString());
                _opDevPrtl.byCmd = 0x00;
                _opDevPrtl.byGid = 0x00;
                _opDevPrtl.byDid = 0x00;

                m_logException.SetLogFile("예외 발생", "리셋 요청 중 오류가 발생 했습니다: " + ex.Message.ToString());
                
                return _opDevPrtl;
            }
            // 2020-05-06 예외 처리(파싱이 정확히 되면 파싱이 된 후에 Return
            _opDevPrtl.byCmd = 0x00;
            _opDevPrtl.byGid = 0x00;
            _opDevPrtl.byDid = 0x00;
            return _opDevPrtl;
        }

        public void SendAck(Socket a_socket, OPDevPtrl a_opDevPrtl)
        {
            try
            {
                //if (a_opDevPrtl.byType != 0xE0)
                //    return;

                byte[]	_byPacket = new byte[9];;
                byte    _byCrc = 0x00;
                
                _byPacket[0] = 0x02;		// STX
                _byPacket[1] = 0x33;
                _byPacket[2] = a_opDevPrtl.bySeq;
                _byPacket[3] = 0x06;		// ACK
                _byPacket[4] = a_opDevPrtl.byCmd;
                _byPacket[5] = a_opDevPrtl.byOpt;
                _byPacket[6] = 0x00;		// LEN
            	
                for( int i = 1 ; i < 7 ; i++ )
                {
                    _byCrc = (byte)(_byCrc ^ _byPacket[i]);
                	
	                _byPacket[7] = _byCrc;
                }
                _byPacket[8] = 0x03;

                //Console.Write("[Send]");
                //for(int i = 0; i < 9; i++)
                //{
                //    Console.Write("{0} ",_byPacket[i].ToString("X2"));
                //}
                //Console.WriteLine("");

                //a_socket.Send(_byPacket);
                a_opDevPrtl.socket.Send(_byPacket);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("SendAck 예외 :: " + ex.Message.ToString());

                m_logException.SetLogFile("예외 발생", "SendAck 예외 :: " + ex.Message.ToString());
            }
            
        }
    }

    public class PtrlEventArgs : EventArgs
    {
        public OPDevPtrl ArgsData { get; private set; }

        public PtrlEventArgs(OPDevPtrl hlDevPrtl)
        {
            this.ArgsData = hlDevPrtl;
        }
    }
}
