using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FuzzyCore.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServerStatusService" in both code and config file together.
    [ServiceContract]
    public interface IFuzzyService
    {
        [OperationContract]
        bool GetServerStatus();
        [OperationContract]
        int GetClientCount();
        [OperationContract]
        string GetSelectedDatabase();
        [OperationContract]
        string GetListeningIPAndPort();
        [OperationContract]
        string GetLastCommand();
        [OperationContract]
        bool WorkingAcceptTask();
        [OperationContract]
        bool WorkingReceiveTask();
    }
}
