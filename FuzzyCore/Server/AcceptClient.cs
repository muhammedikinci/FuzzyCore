using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    class AcceptClient : CommanderObject
    {
        public AcceptClient(IAsyncResult Result) : base(Result)
        {
        }
        public override Client Invoke()
        {
            // Permission Control
            Accept_PermissionControl();
            // Wait clients reconnecting.
            // And destroy.
            mClientDestroy();
            //Get Accepted Client
            Socket mSocket = GetClientWithState();
            #region Ip Permission Control
            Console.WriteLine(mSocket.RemoteEndPoint.ToString());
            string REMOTEIP = mSocket.RemoteEndPoint.ToString();
            string[] exp = REMOTEIP.Split(':');
            Permissions.IpPermission Permission = new Permissions.IpPermission();
            Permission.FilePath = FuzzyServer.getInstance().m_initializeType.Paths.IpPermissions;
            Permission.TargetIP = exp[0].ToString();
            if (!Permission.PermissionControl())
            {
                mSocket.Send(Encoding.UTF8.GetBytes("You have banned This Server!"));
                mSocket.Close();
                throw new Exception(exp[0].ToString() + " Banned This Server!");
            }
            #endregion
            //Detect forcibly closed client
            DetectReconnection(mSocket);
            //Create Client Datas
            Client mClient = CreateAndGet_Client(mSocket);
            //Send accepting data for user method.
            Thread AcceptTaskTh = new Thread(new ThreadStart(() => {
                FuzzyServer.getInstance().AcceptTask(mClient);
            }));
            if (FuzzyServer.getInstance().AcceptTask != null)
            {
                AcceptTaskTh.Start();
            }
            AddSocketList_Client(mClient);

            return mClient;
        }
        private ConsoleMessage Message = new ConsoleMessage();
        private Socket GetClientWithState()
        {
            return FuzzyServer.getInstance().ServerSocket.EndAccept(Result);
        }
        private void Accept_PermissionControl()
        {
            if (!FuzzyServer.AcceptClient_Permission)
            {
                throw new Exception("Accept Client Permission Is False");
            }
        }
        private void mClientDestroy()
        {
            if (!FuzzyServer.getInstance().DestroyThread.IsAlive)
            {
                FuzzyServer.getInstance().DestroyThread.IsBackground = true;
                FuzzyServer.getInstance().DestroyThread.Start();
            }
        }
        private void DetectReconnection(Socket Sck)
        {
            int ReconnectVal = 0;
            int[] SocketIDS = new int[500];
            foreach (KeyValuePair<int, Client> item in FuzzyServer.SocketList)
            {
                if (item.Value.CLOSEDSTATE == Client.ClosedStates.FORCIBLY)
                {
                    if (item.Value.SOCKET.AddressFamily == Sck.AddressFamily)
                    {
                        Message.Write("Reconnected!", ConsoleMessage.MessageType.CONNECT);
                        ReconnectVal++;
                        SocketIDS[ReconnectVal] = item.Key;
                    }
                }
            }

            if (ReconnectVal > 1)
            {
                for (int i = 1; i < SocketIDS.Length; i++)
                {
                    FuzzyServer.SocketList.Remove(SocketIDS[i]);
                }
            }
            else if (ReconnectVal == 1)
            {
                FuzzyServer.SocketList.Remove(SocketIDS[1]);
            }
        }
        private Client CreateAndGet_Client(Socket Sck)
        {
            int CurrentClientId = FuzzyServer.getInstance().getID();
            Client CurrentClient = new Client();
            CurrentClient.ID = CurrentClientId;
            CurrentClient.SOCKET = Sck;
            CurrentClient.CLOSEDSTATE = Client.ClosedStates.NULL;
            CurrentClient.LASTCONNECTIONTIME = DateTime.Now.ToString();
            CurrentClient.PROCESS = 0;
            return CurrentClient;
        }
        private void AddSocketList_Client(Client cl)
        {
            //Add created client to socket list
            FuzzyServer.SocketList.Add(cl.ID, cl);
            //Write Message
            Message.Write(cl.SOCKET.RemoteEndPoint.ToString(), ConsoleMessage.MessageType.CONNECT);
        }
    }
}
