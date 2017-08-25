using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace fuzzyControl.Server.Commands
{
    class GetFile
    {
        ConsoleMessage Message = new ConsoleMessage();
        public void GetFileBytes(Data.JsonCommand Command)
        {
            try
            {
                byte[] file = File.ReadAllBytes(Command.FilePath + "\\" + Command.Text);
                if (file.Length > 0)
                {
                    SendDataArray(file, Command.Client_Socket);
                    Message.Write(Command.CommandType,ConsoleMessage.MessageType.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
        public void SendDataArray(byte[] Data, Socket Client)
        {
            try
            {
                Thread.Sleep(100);
                Client.Send(Data);
            }
            catch (Exception ex)
            {
                Message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
    }
}
