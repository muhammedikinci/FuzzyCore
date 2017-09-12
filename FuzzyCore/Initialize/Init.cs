using System;
using FuzzyCore.Server;
using System.Net;
using System.ServiceModel;
using System.IO;

namespace FuzzyCore.Initialize
{
    public class Init
    {
        public static String ProgramJsonPath;
        ConsoleMessage Message = new ConsoleMessage();
        FuzzyServer Server;
        public Init(string ProgramJsonPathArg,Action<Client> AcceptTask, Action<string, Client> ReceiveTask, string Ip = "127.0.0.1" , string Port = "111" , bool StartServer = true ,bool StartWCFService = true,bool StartReceive = true,bool StartAccept = true)
        {
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
                var host = new ServiceHost(typeof(Services.FuzzyService));
                host.Open();
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
    }
}
