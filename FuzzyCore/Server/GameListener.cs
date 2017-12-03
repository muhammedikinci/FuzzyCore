using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FuzzyCore.Server
{
    class GameListener
    {
        public EndPoint EP;
        UdpClient UDPWorker = new UdpClient();
        public void Listen()
        {
            Thread T = new Thread(new ThreadStart(() => {
                using (var udpClient = new UdpClient((IPEndPoint)EP))
                {
                    string Value = "";
                    while (true)
                    {
                        //IPEndPoint object will allow us to read datagrams sent from any source.
                        var ep = (IPEndPoint)EP;
                        var receivedResults = udpClient.Receive(ref ep);
                        Value = Encoding.ASCII.GetString(receivedResults);
                        Data.Game.GameCommander.Start(Value,ep);
                        Console.WriteLine(Value + " : " + ep.Port.ToString());
                    }
                }
            }));
            T.Start();
        }
    }
}
