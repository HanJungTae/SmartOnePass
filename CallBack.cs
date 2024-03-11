using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartOnePass
{
    class CallBack
    {
        public delegate void DoCallBackUkrKeyIdClear(List<string> listLobbyName);
        public delegate void DoCallBackUkrReset(List<string> listLobbyName);
        public delegate void DoCallBackTimerUkrReset(TimerUkrReset a_tur);

        private DoCallBackUkrKeyIdClear m_fnDoCallbackUkrKeyIdClear = null;
        private DoCallBackUkrReset m_fnDoCallReset = null;
        private DoCallBackTimerUkrReset m_fnDoCallTimerUkrReset = null;

        public void doCallUkrIdDelete(List<string> listLobbyName)
        {
            m_fnDoCallbackUkrKeyIdClear(listLobbyName);
        }

        public void doCallUkrReset(List<string> listLobbyName)
        {
            m_fnDoCallReset(listLobbyName);
        }

        public void doCallTimerUkrReset(TimerUkrReset a_tur)
        {
            m_fnDoCallTimerUkrReset(a_tur);
        }

        public void SetCallBackUkrKeyIdClear(DoCallBackUkrKeyIdClear doCallBackUkrKeyIdClear)
        {
            m_fnDoCallbackUkrKeyIdClear = doCallBackUkrKeyIdClear;

        }

        public void SetCallBackUkrReset(DoCallBackUkrReset doCallBackUkrReset)
        {
            m_fnDoCallReset = doCallBackUkrReset;

        }

        public void SetCallBackTimerUkrReset(DoCallBackTimerUkrReset doCallBackUkrReset)
        {
            m_fnDoCallTimerUkrReset = doCallBackUkrReset;
        }

    }
}
