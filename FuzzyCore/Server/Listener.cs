using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FuzzyCore.Data;

namespace FuzzyCore.Server
{
    public class FuzzyServer
    {
        static FuzzyServer Instance { get; set; }
        public static FuzzyServer getInstance()
        {
            return Instance;
        }
        public Initialize.InitType m_initializeType;

        #region Variables
        //bool
        public static bool ReceiveData_Permission { get; set; } = true; //Data receiving control
        public static bool AcceptClient_Permission { get; set; } = true; //Client accepting control
        public static bool socketState { get { return SocketStatePrivate; } } //Socket Open/Close control get property
        private static bool SocketStatePrivate = false; //Socket Open/Close Control
        //byte
        private byte[] _buff = new byte[1024]; //Receiving Buffer
        private byte[] copyBuff; //Copy array by buffer
        //listener wars
        public Socket ServerSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
        public static Dictionary<int, Client> SocketList = new Dictionary<int, Client>();
        private ConsoleMessage Message = new ConsoleMessage();
        public static string IPAndPort;
        private EndPoint localEP;
        Socket CurrentSocket;
        #endregion
        //
        //THREADS AND METHODS
        //
        public Thread DestroyThread = new Thread(new ThreadStart(() => {
            while (true)
            {
                ClientDestroyer Destroyer = new ClientDestroyer(ref SocketList);
            }
        }));
        public Action<string,Client> ReceiverTask;
        public Action<Client> AcceptTask;
        //
        //CONSTRUCTOR
        //
        public FuzzyServer(EndPoint glEP)
        {
            this.localEP = glEP;
            IPAndPort = localEP.ToString();
            Instance = this;
        }
        //
        //Initializer Method
        //
        public void Init(Initialize.InitType Type)
        {
            m_initializeType = Type;
            switch (Type.ServerProp.TYPE)
            {
                case Initialize.Props.ServerType.REMOTING:
                    StartListenTCP();
                    break;
                case Initialize.Props.ServerType.GAMESERVER:
                    StartListenTCP();
                    StartListenUDP();
                    break;
                case Initialize.Props.ServerType.DATATRANFSER:
                    StartListenTCP();
                    StartListenUDP();
                    break;
                default:
                    break;
            }
        }
        //UDP STARTER METHOD
        void StartListenUDP()
        {
            GameListener UDP = new GameListener();
            UDP.EP = localEP;
            UDP.Listen();
        }
        //
        //TCP STARTER METHOD
        //
        void StartListenTCP()
        {
            try
            {
                //
                //PERMISSION TRUE/FALSE CONTROL
                //
                if (!AcceptClient_Permission)
                {
                    throw new Exception("Accept Client Permission Is False");
                }
                ServerSocket.Bind(localEP);
                ServerSocket.Listen(2);
                SocketStatePrivate = true;
                ServerSocket.BeginAccept(new AsyncCallback(AcceptSocket),ServerSocket);
            }
            catch (Exception Ex)
            {
                ConsoleMessage.WriteException(Ex.Message,"Listener.cs","StartListenTCP");
            }
        }
        // Accept
        private void AcceptSocket(IAsyncResult State)
        {
            try
            {
                CommanderObject Accept = new AcceptClient(State);
                Handler Handle = new Handler(Accept);
                Client mClient = Handle.Invoke();
                //We accepting new connection again
                ServerSocket.BeginAccept(new AsyncCallback(AcceptSocket), ServerSocket);
                //Begin Receive in Current Client Stream
                mClient.SOCKET.BeginReceive(_buff, 0, _buff.Length, SocketFlags.None, new AsyncCallback(ReceiveData), mClient.SOCKET);
            }
            catch (Exception Ex)
            {
                ConsoleMessage.WriteException(Ex.Message, "Listener.cs", "AcceptSocket");
                ServerSocket.BeginAccept(new AsyncCallback(AcceptSocket), ServerSocket);
            }
        }
        // Receive
        private void ReceiveData(IAsyncResult State)
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
                    DataParser parser = new DataParser(Data, CurrentSocket,m_initializeType);
                    parser.Parse();
                    Thread reciveTask = new Thread(new ThreadStart(() => { ReceiverTask(Data, GetClientBySocket(CurrentSocket)); }));
                    if (ReceiverTask != null)
                    {
                        reciveTask.Start();
                    }
                    CurrentSocket.BeginReceive(_buff, 0, _buff.Length, SocketFlags.None, new AsyncCallback(ReceiveData), CurrentSocket);
                }
                else
                {
                    Message.Write(CurrentSocket.RemoteEndPoint.ToString(), ConsoleMessage.MessageType.DISCONNECT);
                }
            }
            catch (Exception Ex)
            {
                ConsoleMessage.WriteException(Ex.Message, "Listener.cs", "ReceiveData");
                SocketList[FoundSocketID(CurrentSocket)].CLOSEDSTATE = Client.ClosedStates.FORCIBLY;
            }
        }
        //Get Available ID
        public int getID()
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
        //Found ID by Socket
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
        //Data sender
        public void SendDataAllClient(string Data)
        {
            foreach (KeyValuePair<int, Client> item in SocketList)
            {
                item.Value.SOCKET.Send(Encoding.UTF8.GetBytes(Data));
            }
        }

    }
}
