using FuzzyCore.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FuzzyCore.CommandClasses
{
    static class ExtensionMethods
    {
        static ConsoleMessage Message = new ConsoleMessage();
        public static void SendDataArray(this byte[] Data, Socket Client)
        {
            try
            {
                Client.Send(Data);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
        public static void SendDataString(this String Data, Socket Client)
        {
            try
            {
                byte[] arr = Encoding.UTF8.GetBytes(Data);
                Client.Send(arr);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
        public static void SendDataStringUDP(this String Data, Client C)
        {
            try
            {
                UdpClient Client = new UdpClient();
                FuzzyCore.Data.Game.GameCommander.SendTo(Encoding.UTF8.GetBytes(Data), C.UDP_Socket);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
        //{"CommandType":"get_folder_list" , "FilePath":"c:\\"}
    }
}
