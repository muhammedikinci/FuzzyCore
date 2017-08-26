using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace FuzzyCore.Server
{
    class ClientDestroyer
    {
        public static Dictionary<int, Client> SocketList;

        static Thread ForciblyThread;
        static int CurrentForciblyClient;

        public ClientDestroyer(ref Dictionary<int,Client> SockList)
        {
            SocketList = SockList;
            ClientForcibly();
            ClientNormal();
        }

        void ClientForcibly()
        {
            try
            {
                foreach (KeyValuePair<int, Client> item in SocketList)
                {
                    if (item.Value.CLOSEDSTATE == Client.ClosedStates.FORCIBLY && item.Value.PROCESS == 0)
                    {
                        CurrentForciblyClient = item.Key;
                        item.Value.PROCESS = 1;
                        ForciblyThread = new Thread(new ThreadStart(ForciblyDestroy));
                        ForciblyThread.IsBackground = true;
                        ForciblyThread.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        static void ForciblyDestroy()
        {
            try
            {
                int a = 0;
                while (a <= 3)
                {
                    Thread.Sleep(1000);
                    if (SocketList[CurrentForciblyClient].CLOSEDSTATE == Client.ClosedStates.FORCIBLY)
                    {
                        a++;
                    }
                }

                SocketList.Remove(CurrentForciblyClient);
                Console.WriteLine("Client bağlantıyı tamamlayamadığı için kapatıldı.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        void ClientNormal()
        {
            try
            {
                int id = -1;
                foreach (KeyValuePair<int, Client> item in SocketList)
                {
                    if (item.Value.CLOSEDSTATE == Client.ClosedStates.NORMAL)
                    {
                        id = item.Key;
                    }
                }

                if (id >= 0)
                {
                    SocketList.Remove(id);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
