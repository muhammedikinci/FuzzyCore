using FuzzyCore.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace FuzzyCore.Services
{
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
        bool AcceptTaskStatus();
        [OperationContract]
        bool ReceiveTaskStatus();
        [OperationContract]
        bool SetAcceptTask(bool TaskStatus);
        [OperationContract]
        bool SetReceiveTask(bool TaskStatus);
    }
}
