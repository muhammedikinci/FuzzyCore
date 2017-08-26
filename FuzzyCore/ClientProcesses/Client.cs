using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    class Client
    {
        public int ID { get; set; }
        public Socket SOCKET { get; set; }
        public string LASTCONNECTIONTIME { get; set; }
        public ClosedStates CLOSEDSTATE { get; set; }
        public int PROCESS { get; set; }

        public enum ClosedStates
        {
            FORCIBLY,
            NORMAL,
            NULL
        }
    }

}
