using System;
using System.Collections.Generic;
using System.Linq;
using FuzzyCore.Server;

namespace FuzzyCore.Services
{
    public class FuzzyService : IFuzzyService
    {
        public int GetClientCount()
        {
            return FuzzyServer.SocketList.Count();
        }

        public string GetLastCommand()
        {
            return Data.DataParser.LastCommand;
        }

        public string GetListeningIPAndPort()
        {
            return FuzzyServer.IPAndPort;
        }

        public string GetSelectedDatabase()
        {
            return Database.dataBase.SelectedDatabase;
        }

        public bool GetServerStatus()
        {
            return FuzzyServer.socketState;
        }

        public bool AcceptTaskStatus()
        {
            return FuzzyServer.AcceptClient_Permission;
        }

        public bool ReceiveTaskStatus()
        {
            return FuzzyServer.ReceiveData_Permission;
        }

        public bool SetAcceptTask(bool TaskStatus)
        {
            FuzzyServer.AcceptClient_Permission = TaskStatus;
            return FuzzyServer.AcceptClient_Permission;
        }

        public bool SetReceiveTask(bool TaskStatus)
        {
            FuzzyServer.ReceiveData_Permission = TaskStatus;
            return FuzzyServer.ReceiveData_Permission;
        }
    }
}
