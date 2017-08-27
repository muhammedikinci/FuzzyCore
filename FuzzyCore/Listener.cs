using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FuzzyCore.Server
{
    public class FuzzyServer
    {
        public bool ReceiveData_Permission { get; set; } = true;
        public bool AcceptClient_Permission { get; set; } = true;
        public bool socketState { get { return SocketStatePrivate; } }
        private bool SocketStatePrivate = false;
        private EndPoint localEP;
        private byte[] _buff = new byte[1024];
        private byte[] copyBuff;
        private ConsoleMessage Message = new ConsoleMessage();
        private Socket ServerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        private static Dictionary<int, Client> SocketList = new Dictionary<int, Client>();
        private Thread DestroyThread = new Thread(new ThreadStart(() => {
            while (true)
            {
                ClientDestroyer Destroyer = new ClientDestroyer(ref SocketList);
            }
        }));
        //For Receive Catch Block
        Socket CurrentSocket;
        public FuzzyServer(EndPoint glEP)
        {
            this.localEP = glEP;
        }

        public void startListen()
        {
            try
            {
                if (!AcceptClient_Permission)
                {
                    throw new Exception("Accept Client Permission Is False");
                }
                ServerSocket.Bind(localEP);
                ServerSocket.Listen(2);
                ServerSocket.BeginAccept(new AsyncCallback(AcceptSocket), ServerSocket);
            }
            catch (Exception Ex)
            {
                Message.Write(Ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }

        public void AcceptSocket(IAsyncResult State)
        {
            try
            {
                if (!AcceptClient_Permission)
                {
                    throw new Exception("Accept Client Permission Is False");
                }
                SocketStatePrivate = true;
                if (!DestroyThread.IsAlive)
                {
                    DestroyThread.IsBackground = true;
                    DestroyThread.Start();
                }
                //Create current accepted socket
                Socket AccSocket = ServerSocket.EndAccept(State);

                //Detect forcibly closed client
                int ReconnectVal = 0;
                int[] SocketIDS = new int[500];
                foreach (KeyValuePair<int,Client> item in SocketList)
                {
                    if (item.Value.CLOSEDSTATE == Client.ClosedStates.FORCIBLY)
                    {
                        if (item.Value.SOCKET.AddressFamily == AccSocket.AddressFamily)
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
                        SocketList.Remove(SocketIDS[i]);
                    }
                }
                else if (ReconnectVal == 1)
                {
                    SocketList.Remove(SocketIDS[1]);
                }

                //Create Client Datas
                int CurrentClientId = getID();
                Client CurrentClient = new Client();
                CurrentClient.ID = CurrentClientId;
                CurrentClient.SOCKET = AccSocket;
                CurrentClient.CLOSEDSTATE = Client.ClosedStates.NULL;
                CurrentClient.LASTCONNECTIONTIME = DateTime.Now.ToString();
                CurrentClient.PROCESS = 0;

                //Add created client to socket list
                SocketList.Add(CurrentClientId, CurrentClient);
                //Write Message
                Message.Write(AccSocket.RemoteEndPoint.ToString(), ConsoleMessage.MessageType.CONNECT);
                //We accepting new connection again
                ServerSocket.BeginAccept(new AsyncCallback(AcceptSocket), ServerSocket);

                if (!AcceptClient_Permission)
                {
                    throw new Exception("Accept Client Permission Is False");
                }
                //Begin Receive in Current Client Stream
                AccSocket.BeginReceive(_buff, 0, _buff.Length, SocketFlags.None, new AsyncCallback(ReceiveData), AccSocket);
            }
            catch (Exception Ex)
            {
                Message.Write(Ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }

        public void ReceiveData(IAsyncResult State)
        {
            try
            {
                if (!ReceiveData_Permission)
                {
                    throw new Exception("Receive Data Permission Is False");
                }
                CurrentSocket = (Socket)State.AsyncState;
                int ReceivedInt = CurrentSocket.EndReceive(State);
                copyBuff = new byte[ReceivedInt];
                Array.Copy(_buff,copyBuff,ReceivedInt);
                String Data = Encoding.UTF8.GetString(copyBuff);
                _buff = new byte[1024];
                if (!string.IsNullOrEmpty(Data))
                {
                    DataParser parser = new DataParser(Data, CurrentSocket);
                    CurrentSocket.BeginReceive(_buff, 0, _buff.Length, SocketFlags.None, new AsyncCallback(ReceiveData), CurrentSocket);
                }
                else
                {
                    Message.Write(CurrentSocket.RemoteEndPoint.ToString(), ConsoleMessage.MessageType.DISCONNECT);
                }
            }
            catch (Exception Ex)
            {
                Message.Write(Ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
                SocketList[FoundSocketID(CurrentSocket)].CLOSEDSTATE = Client.ClosedStates.FORCIBLY;
            }
        }
        private int getID()
        {
            int currentID = 0;
            bool While = true;
            bool inFor = false;
            while (While)
            {
                inFor = false;
                foreach (KeyValuePair<int,Client> item in SocketList)
                {
                    if (item.Key == currentID)
                    {
                        inFor = true;
                    }
                }
                if (!inFor)
                {
                    While = false;
                    break;
                }
                currentID++;
            }
            return currentID;
        }
        private int FoundSocketID(Socket sck)
        {
            foreach (KeyValuePair<int,Client> item in SocketList)
            {
                if (item.Value.SOCKET == sck)
                {
                    return item.Key;
                }
            }
            return 0;
        }
        private Client GetClientBySocket(Socket sck)
        {
            foreach (KeyValuePair<int, Client> item in SocketList)
            {
                if (item.Value.SOCKET == sck)
                {
                    return item.Value;
                }
            }
            return new Client();
        }

        public void SendDataAllClient(string Data)
        {
            foreach (KeyValuePair<int, Client> item in SocketList)
            {
                item.Value.SOCKET.Send(Encoding.UTF8.GetBytes(Data));
            }
        }

    }
}
