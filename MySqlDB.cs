using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Diagnostics;

namespace SmartOnePass
{
    public class MySqlDB
    {
        /////////////////////////////////////////////////////////////
        // MySql 연동 멤버 변수
        private MySqlConnection m_myCon;

        private Object thisLock = new Object();

        // Insert와 Update시에 사용하는 Queue
        private List<string> m_MySqlQue = new List<string>();
        
        public void MySqlCon(string strDbIp, string strDbName, string strDbId, string strDbPw)
        {
            try
            {
                m_myCon = new MySqlConnection();
                m_myCon.ConnectionString = "Data Source=" + strDbIp + ";Database=" + strDbName + ";User Id=" + strDbId + ";Password=" + strDbPw + ";CHARSET=euckr";
                MySqlQueThreadStart();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("MySqlCon() 예외 발생: " + ex.Message.ToString());
            }
        }

        public void MySqlExec(string strQry)
        {
            lock (thisLock)
            {
                try
                {
                    if(m_myCon.State != System.Data.ConnectionState.Open)
                        m_myCon.Open();

                    MySqlCommand Com = new MySqlCommand(strQry, m_myCon);
                    Com.ExecuteNonQuery();
                    m_myCon.Close();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("MySqlExec 예외 발생: " + ex.Message.ToString());
                    Console.WriteLine("strQry = {0}", strQry);
                    m_myCon.Close();
                }
            }
        }

        // 큐를 사용하기 위한 메소드추가함. 전기차 코드와는 반대로 되어 있음. 
        public void MySqlExec(string strQry, string strQryUse)
        {
            try
            {
                Console.WriteLine("En Que strQry = {0}", strQry);
                m_MySqlQue.Add(strQry);
                return;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("MySqlExec1 예외 발생: " + ex.Message.ToString());
                Console.WriteLine("strQry = {0}", strQry);
                m_myCon.Close();
                return;
            }
        }

        private void MySqlExecQueProc()
        {
            while (true)
            {
                while (m_MySqlQue.Count > 0) // Queue가 비어 있지 않음
                {
                    try
                    {

                        string strQry = m_MySqlQue[0];
                        m_MySqlQue.RemoveAt(0);

                        if (strQry == null)
                        {
                            continue;
                        }

                        if (strQry == "")
                        {
                            continue;
                        }

                        //Console.WriteLine("m_MySqlQue.Count = {0}, ==> {1}", m_MySqlQue.Count, strQry);

                        MySqlExec(strQry);

                    }
                    catch (Exception e)
                    {
                        e.ToString();
                    }
                    Thread.Sleep(2);

                }

                Thread.Sleep(100); // Queu가 비어 있으면.
            }
        }

        public void MySqlQueThreadStart()
        {
            Thread mysqlQueThread = new Thread(MySqlExecQueProc);
            mysqlQueThread.Start();
        }
        

        public MySqlDataReader MySqlSelect(string strQry)
        {
            lock (thisLock)
            {
                try
                {
                    if (m_myCon.State != System.Data.ConnectionState.Open)
                        m_myCon.Open();

                    MySqlCommand Com = new MySqlCommand(strQry, m_myCon);
                    MySqlDataReader R = Com.ExecuteReader();
                    m_myCon.Close();
                    return R;
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("MySqlSelect 예외 발생: " + ex.Message.ToString());
                    Console.WriteLine("strQry = {0}", strQry);
                }
                return null;
            }
        }

        public List<string[]> MySqlSelect(string strQry, int nColumnCnt)
        {
            lock (thisLock)
            {
                //Stopwatch stopwatch = new Stopwatch();
                //stopwatch.Start();

                List<string[]> _list = new List<string[]>();

                MySqlDataReader _R = null;

                try
                {
                    if (m_myCon.State != System.Data.ConnectionState.Open)
                        m_myCon.Open();

                    MySqlCommand _Com = new MySqlCommand(strQry, m_myCon);

                    _R = _Com.ExecuteReader();

                    while (_R.Read())
                    {
                        string[] _strTmp = new string[nColumnCnt];

                        for (int i = 0; i < nColumnCnt; i++)
                        {
                            _strTmp[i] = string.Format("{0}", _R[i]);
                        }

                        _list.Add(_strTmp);
                    }
                    m_myCon.Close();
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine("MySqlSelect() 예외 발생 : " + ex.ToString() + "\r\n strQry = " + strQry);
                    m_myCon.Close();
                }
                finally
                {

                    if (_R != null)
                        _R.Close();
                }

                //stopwatch.Stop();
                //Console.WriteLine("Sql Query Elapsed Time: {0} milliseconds ==> {1}", stopwatch.ElapsedMilliseconds, strQry);
                return _list;
            }
        }

        // 데이터 존재 여부(파라미터의 값만큼 존재하는지를 확인함.
        public bool IsData(string strQry)
        {
            List<string[]> strList = MySqlSelect(strQry, 1);

            if (strList.Count > 0)  // 데이터가 있을 때만
            {
                string[] strRet = strList[0];
                return true;
            }

            return false;
        }
    }
}
