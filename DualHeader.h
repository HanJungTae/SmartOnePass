

/****************************************************************************/
/*                                                                          */
/*         Copyright 2001 - ? Duali		                                    */
/*                                                                          */
/*         This software is supplied under the terms of a license           */
/*         agreement or nondisclosure agreement and may not be copied       */
/*         or disclosed except in accordance with the terms of that         */
/*         agreement.                                                       */
/*                                                                          */
/****************************************************************************/
/*    Name: DUALHEADER.H                                                    */
/*                                                                          */
/*      Description: Include file for Windows 32bit DLL                     */
/*                                                                          */
/*--------------------------------------------------------------------------*/
/* History   :                                                              */
/*   Revision      Date            Author         Description               */
/*   -----------------------------------------------------------------------*/
/*   Version: 1.00: 2001-06-13   Kwon young Eak  Initial design	            */
/****************************************************************************/

/****************************************************************************
*
*				 USB Define....	2003/10/06  yekwon
*
****************************************************************************/


//-------------------------------------------------- definition




/****************************************************************************
*
*				 공통 Define....
*
****************************************************************************/

#define DUAL_COMMTYPE_232		1		// No Loopback(send byte) and RTS Control
#define DUAL_COMMTYPE_485		2		// Loopback and RTS Control


#define DUALEYE_2000				0
#define DUALEYE_100_INTELLI			1
#define DUALEYE_100_DUMMY			2
#define DUALEYE_200					3
#define DUALEYE_700					7
#define DUAL_MODEM					10
#define DUALEYE_ABM					11

#define POLLING_ERROR_START			1024//249	// Error define Index....
#define INVALID_PORT				1250
#define STX_ERROR					1027
#define INVALID_LENGTH_ERROR		1252
#define TIMEOUT_ERROR				1030
#define LRC_ERROR					1028
#define RW_ERROR					1255
#define ETX_ERROR					1256
// Dual Modem Define

#define MODEM_ERROR_START			1024
#define HEADER_ERROR				1025
#define TERMINAL_ERROR				1026
#define MODEM_STX_ERROR				1027
#define MODEM_CRC_ERROR				1028
#define MODEM_ETX_ERROR				1029
#define MODEM_TIMEOUT_ERROR			1030
#define USB_WRITE_ERROR				1031
#define USB_READ_ERROR				1032

#define POLLING_ERROR_START_USB10000		11024//249	// Error define Index....
#define INVALID_PORT_USB10000				11250
#define STX_ERROR_USB10000					11027
#define INVALID_LENGTH_ERROR_USB10000		11252
#define TIMEOUT_ERROR_USB10000				11030
#define LRC_ERROR_USB10000					11028
#define RW_ERROR_USB10000					11255
#define ETX_ERROR_USB10000					11256


// Dual Modem Define

#define MODEM_ERROR_START_USB1000			11024
#define HEADER_ERROR_USB1000				11025
#define TERMINAL_ERROR_USB1000				11026
#define MODEM_STX_ERROR_USB1000				11027
#define MODEM_CRC_ERROR_USB1000				11028
#define MODEM_ETX_ERROR_USB1000				11029
#define MODEM_TIMEOUT_ERROR_USB1000			11030
#define USB_WRITE_ERROR_USB1000				11031
#define USB_READ_ERROR_USB1000				11032
/*
#define STX							0xEE            
#define ETX							0xCC            
*/
#define STX							0x02            
#define ETX							0x03     


#define MAX_BUFF_SIZE				2048       
/****************************************************************************
*
*				 각 DEVICE CLASS 별 Define..........
*
****************************************************************************/
// Dual 200 lib class 

#define DUAL200LIB_STX				"60"
#define DUAL200LIB_ETX				0x03
#define WAIT_INTERVAL_DUAL2000			500




#define DUAL200LIB_LRC_ERROR					5000
#define DUAL200LIB_DATA_LEN_ERROR				5001
#define DUAL200LIB_ACK_ERROR					5002
#define DUAL200LIB_ETX_ERROR					5003
#define DUAL200LIB_NOTFOUND_COMMAND_ERROR		5004
#define DUAL200LIB_CARD_READER_ERROR			5005
#define DUAL200LIB_SUPPORT_ERROR				5006
#define	DUAL200LIB_FORMAT_ERROR					5007
#define	DUAL200LIB_WRITE_ERROR					5008
#define	DUAL200LIB_ADDRESS_ERROR				5009
#define	DUAL200LIB_LOCKBIT_ERROR				5010
#define DUAL200LIB_COMMUNICATION_ERROR			5011
#define DUAL200LIB_TIMEOUT_ERROR				5012
//#define DUAL200LIB_ETX_ERROR					5013
// Dual 2000 Class

#define DUAL_WAIT_INTERVAL			150



// Dual 100 Intelli Class





// Dual 100 Dummy Class





// Export function 정의 
#define API_DLL __declspec(dllexport)


extern "C" API_DLL int WINAPI DUAL_InitPort(int nPort,int nBaud);
extern "C" API_DLL void WINAPI DUAL_ClosePort(int nPort);
extern "C" API_DLL int WINAPI DUAL_Polling(int nPort, int nAddress, LPBYTE data, int datalen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_Polling_TOUT(int nPort, int nAddress, LPBYTE data, int datalen, LPBYTE lpRes, int nTOUT);
// Dual 200 lib class 

extern "C" API_DLL int WINAPI DUAL200LIB_STD_Write(BYTE nPort, BYTE nAddress, CString strOffset,CString strLock, BYTE* data, BYTE datalen,BYTE* lpRes);
extern "C" API_DLL int WINAPI DUAL200LIB_Polling(int nPort, int nAddress, LPBYTE data, int datalen, LPBYTE lpRes);

//Dual Modem for Acash pstn server
extern "C" API_DLL int WINAPI DUAL_ModemCommand(int nPort, LPBYTE data, int datalen, LPBYTE lpRes, int nWaitMilliSeconds);
extern "C" API_DLL int WINAPI DUAL_SendData2Modem(int nPort, LPBYTE data, int datalen, int nWaitMilliSeconds, LPBYTE lpRes);
extern "C" API_DLL BOOL WINAPI DUAL_ListenModemENQ(int nPort, BYTE enq, int nWaitMilliSeconds);

//Dual ABM Test by fuzzo
extern "C" API_DLL int WINAPI DUAL_ABM_RF_On(int nPort);
extern "C" API_DLL int WINAPI DUAL_ABM_RF_Off(int nPort);
extern "C" API_DLL int WINAPI DUAL_ABM_RF_Reset(int nPort);
extern "C" API_DLL int WINAPI DUAL_ABM_RF_Version(int nPort, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Reset(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Idle_Req(int nPort, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Wakeup_Req(int nPort, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Anticoll(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Select(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Loadkey(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Auth(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Halt(int nPort);
extern "C" API_DLL int WINAPI DUAL_AM_Read(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Write(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Increment(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Decrement(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Transfer(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Restore(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Inc_Transfer(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Dec_Transfer(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Inc_Transfer2(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Dec_Transfer2(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Authkey(int nPort, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_Req_Auth(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Req_Authkey(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Device_Info(int nPort, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_WriteE2(int nPort, int datalen, LPBYTE data);
extern "C" API_DLL int WINAPI DUAL_AM_ReadE2(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Req_Read(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Req_Readkey(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Req_Writekey(int nPort, LPBYTE data, LPBYTE lpRes);

// Type A 용 Command
extern "C" API_DLL int WINAPI DUAL_AM_Transparent(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_TransparentCRC(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);

// Type B 용 Command
extern "C" API_DLL int WINAPI DUAL_BM_Transparent_Set(int nPort, unsigned char sCard, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_BM_Transparent(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_BM_TransparentCRC(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_BM_Transparent_Test(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes,unsigned char lower_time,unsigned char upper_time);


// DE-X00 범용 command
extern "C" API_DLL int WINAPI DUAL_ABM_Transparent(int nPort, unsigned char cmd, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);

// IC Card 용 Command
extern "C" API_DLL int WINAPI DUAL_IC_PowerOn(int nPort, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_IC_Case1(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_IC_Case2(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_IC_Case3(int nPort, int datalen, LPBYTE data, LPINT outlen, LPBYTE lpRes);

extern "C" API_DLL int WINAPI DUAL_ABM_BuzzerOFF(int nPort);
extern "C" API_DLL int WINAPI DUAL_ABM_BuzzerON(int nPort);

// Test 용
extern "C" API_DLL BYTE WINAPI InitPort(BYTE nPort, DWORD nBaud, BYTE nParity, BYTE nData, BYTE nStop);
extern "C" API_DLL int WINAPI PollingDual(BYTE nPort, BYTE nRWaddress, BYTE* data, BYTE datalen, BYTE* lpRes);
extern "C" API_DLL BYTE WINAPI ClosePort(BYTE nPort);
extern "C" API_DLL int WINAPI DUAL_AM_Read_Royalty(int nPort, LPBYTE data, LPBYTE lpRes);
extern "C" API_DLL int WINAPI DUAL_AM_Read_Credit(int nPort, LPBYTE data, LPBYTE lpRes);

extern "C" API_DLL int WINAPI ListenPollDual(int nPort, DWORD nBaud, short nParity, short nData, short nStop, short nComm, short nDev);
extern "C" API_DLL int WINAPI listPollingDual(BYTE nPort, BYTE nRWaddress, BYTE* data, BYTE datalen, BYTE* lpRes);

//VERSION
extern "C" API_DLL int WINAPI DUAL_SetVersion(BYTE nVer,int nPort);
extern "C" API_DLL int WINAPI DUAL_Version(LPBYTE lpRes);

/*
	CDual700Lib define
*/
#define ON			1
#define OFF			0
#define OK			0
#define WAIT_INTERVAL_700	100
#define WAIT_INTERVAL_DUAL700			2000

#define WAIT_INTERVAL_DUALABM			10000//3500//2006.05.10.bykim
