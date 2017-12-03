using System;
using FuzzyCore.Server;
using System.Net;
using System.ServiceModel;
using System.IO;
using System.Collections.Generic;

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
        ConsoleMessage Message = new ConsoleMessage();
        FuzzyServer Server;
        ServiceHost Host;
        public Init(string ProgramJsonPathArg,Action<Client> AcceptTask, Action<string, Client> ReceiveTask,  InitType.Type initilizeType = InitType.Type.BASIC, string Ip = "127.0.0.1" , string Port = "111" , bool StartServer = true ,bool StartWCFService = true,bool StartReceive = true,bool StartAccept = true)
        {
            InitType Type = new InitType();
            Type.Paths = new FilePaths();
            Type.ServerProp = new Props();

            Type.initType = initilizeType;
            Message.Write("Advanced Initializing Started", ConsoleMessage.MessageType.BACKPROCESS);
            //Program.Json Exits
            Type.Paths.ProgramJson = ProgramJsonPathArg;
            if (File.Exists(Type.Paths.ProgramJson))
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
                Server.Init(Type);
            }
        }

        //Basic Start Initializing
        public Init()
        {
            InitType Type = new InitType();
            Type.Paths = new FilePaths();
            Type.ServerProp = new Props();
            //Information Message
            Message.Write("Basic Initializing", ConsoleMessage.MessageType.BACKPROCESS);

            //
            //File Paths Init
            //
            Type.Paths.ProgramJson = "Program.json";
            Type.Paths.MacPermissions = "Permissions/Mac.json";
            Type.Paths.IpPermissions = "Permissions/Ip.json";

            //
            //WCF Service Init
            //
            Host = new ServiceHost(typeof(Services.FuzzyService));
            Host.Open();
            if (Host.State == CommunicationState.Opened)
            {
                Message.Write("WCF Service is Running", ConsoleMessage.MessageType.BACKPROCESS);
            }

            //
            //Fuzzy Server Init
            //
            Server = new FuzzyServer(new IPEndPoint(IPAddress.Any,5959));
            Server.Init(Type);
        }

        public Init(InitType Type)
        {
            staticInitData.Current_Init_Type = Type;
            if (!string.IsNullOrEmpty(Type.FirstConsole_Message))
            {
                Console.WriteLine(Type.FirstConsole_Message);
            }
            if (Type.Wcf_Running)
            {
                Host = new ServiceHost(typeof(Services.FuzzyService));
                Host.Open();
                if (Host.State == CommunicationState.Opened)
                {
                    Message.Write("WCF Service is Running", ConsoleMessage.MessageType.BACKPROCESS);
                }
            }
            if (Type.Server_Running)
            {
                Server = new FuzzyServer(new IPEndPoint(IPAddress.Parse(Type.ServerProp.IP), int.Parse(Type.ServerProp.Port)));
                Server.Init(Type);
                Console.WriteLine("Listening : " + Type.ServerProp.IP + ":" + Type.ServerProp.Port);
            }
        }
    }
}
