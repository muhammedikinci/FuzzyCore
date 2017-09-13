using System;
using FuzzyCore.Server;
using System.Net;
using System.ServiceModel;
using System.IO;

namespace FuzzyCore.Initialize
{
    public class Init
    {
        public FuzzyServer InitServer
        {
            get
            {
                return Server;
            }
        }
        public ServiceHost InitHost
        {
            get
            {
                return Host;
            }
        }
        public static String ProgramJsonPath;
        ConsoleMessage Message = new ConsoleMessage();
        FuzzyServer Server;
        ServiceHost Host;
        public Init(string ProgramJsonPathArg,Action<Client> AcceptTask, Action<string, Client> ReceiveTask, string Ip = "127.0.0.1" , string Port = "111" , bool StartServer = true ,bool StartWCFService = true,bool StartReceive = true,bool StartAccept = true)
        {
            Message.Write("Advanced Initializing Started", ConsoleMessage.MessageType.BACKPROCESS);
            //Program.Json Exits
            ProgramJsonPath = ProgramJsonPathArg;
            if (File.Exists(ProgramJsonPath))
            {
                Message.Write("Program.Json - Ready!",ConsoleMessage.MessageType.SUCCESS);
            }
            else
            {
                Message.Write("Program.Json - NotFound!", ConsoleMessage.MessageType.ERROR);
            }

            //Wcf Service Start
            if (StartWCFService)
            {
                Host = new ServiceHost(typeof(Services.FuzzyService));
                Host.Open();
            }

            //Socket Server Start
            Server = new FuzzyServer(new IPEndPoint(IPAddress.Parse(Ip),int.Parse(Port)));
            Server.AcceptTask = AcceptTask;
            Server.ReceiverTask = ReceiveTask;
            if (StartServer)
            {
                Server.startListen();
            }
        }

        //Basic Start Initializing
        public Init()
        {
            Message.Write("Basic Initializing",ConsoleMessage.MessageType.BACKPROCESS);
            Message.Write("WCF Service is Running", ConsoleMessage.MessageType.BACKPROCESS);
            ProgramJsonPath = "Program.json";
            Host = new ServiceHost(typeof(Services.FuzzyService));
            Host.Open();
            Server = new FuzzyServer(new IPEndPoint(IPAddress.Any,111));
            Server.startListen();
        }
    }
}
