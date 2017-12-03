using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using FuzzyCore.Server;

namespace FuzzyCore.Data.Game
{
    static class GameCommander
    {
        static UdpClient UDPWorker = new UdpClient();
        public static void Start(string Receiving , IPEndPoint EP)
        {
            try
            {
                GameCommandObject obj = JsonConvert.DeserializeObject<GameCommandObject>(Receiving);
                EP.AddToSocketList();
            }
            catch (Exception ex)
            {
                ConsoleMessage.WriteException(ex.Message,"GameCommander.cs","Start");
            }
        }
        static void AddToSocketList(this IPEndPoint Ep)
        {
            foreach (var ClientItem in FuzzyServer.SocketList)
            {
                string Client_IP = ClientItem.Value.SOCKET.RemoteEndPoint.GetIp();
                if (Client_IP == Ep.Address.ToString() && ClientItem.Value.UDP_Socket == null)
                {
                    ClientItem.Value.UDP_Socket = Ep;
                }
            }
        }
        static string GetIp(this EndPoint AF)
        {
            var Parse = AF.ToString().Split(':');
            return Parse[0];
        }
        static void SendTo(this GameCommandObject Value, IPEndPoint eps)
        {
            try
            {
                foreach (var item in FuzzyServer.SocketList)
                {
                    UDPWorker.Client.Connect(item.Value.UDP_Socket);
                    var Str = JsonConvert.SerializeObject(Value);
                    UDPWorker.Client.SendTo(Encoding.UTF8.GetBytes(Str), item.Value.UDP_Socket);
                }
            }
            catch (Exception ex)
            {
                ConsoleMessage.WriteException(ex.Message,"GameCommander.cs","SendTo");
            }
        }
    }
}
