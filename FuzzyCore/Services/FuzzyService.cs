using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FuzzyCore.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerStatusService" in both code and config file together.
    public class FuzzyService : IFuzzyService
    {
        public int GetClientCount()
        {
            return Server.FuzzyServer.SocketList.Count();
        }

        public string GetLastCommand()
        {
            return Data.DataParser.LastCommand;
        }

        public string GetListeningIPAndPort()
        {
            return Server.FuzzyServer.IPAndPort;
        }

        public string GetSelectedDatabase()
        {
            return Database.dataBase.SelectedDatabase;
        }

        public bool GetServerStatus()
        {
            return Server.FuzzyServer.socketState;
        }

        public bool WorkingAcceptTask()
        {
            return Server.FuzzyServer.AcceptClient_Permission;
        }

        public bool WorkingReceiveTask()
        {
            return Server.FuzzyServer.ReceiveData_Permission;
        }
    }
}
