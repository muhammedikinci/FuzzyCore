using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FuzzyCore.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServerStatusService" in both code and config file together.
    public class ServerStatusService : IServerStatusService
    {
        public bool GetServerStatus()
        {
            return FuzzyCore.Server.FuzzyServer.socketState;
        }
    }
}
