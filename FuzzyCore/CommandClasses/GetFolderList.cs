using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Threading;
using fuzzyControl.Server.Data;

namespace fuzzyControl.Server.Commands
{
    public class GetFolderList
    {
        ConsoleMessage message = new ConsoleMessage();
        public void GetFoldersName(Data.JsonCommand Command)
        {
            try
            {
                Socket Client = Command.Client_Socket;

                string[] filePaths = Directory.GetFiles(@"" + Command.FilePath, "*",
                                                 SearchOption.TopDirectoryOnly);
                string[] folderPath = Directory.GetDirectories(@"" + Command.FilePath, "*", SearchOption.TopDirectoryOnly);
                foreach (string item in filePaths)
                {
                    DirectoryInfo dr = new DirectoryInfo(item);
                    message.Write(dr.Name, ConsoleMessage.MessageType.SUCCESS);
                    Data.JsonCommand comm = new Data.JsonCommand();
                    comm.CommandType = "file_name";
                    comm.Text = dr.Name;
                    comm.PrevDirectory = dr.Parent.Name;
                    SendDataString(JsonConvert.SerializeObject(comm), Client);
                }
                foreach (string item in folderPath)
                {
                    DirectoryInfo dr = new DirectoryInfo(item);
                    message.Write(dr.Name, ConsoleMessage.MessageType.SUCCESS);
                    Data.JsonCommand comm = new Data.JsonCommand();
                    comm.CommandType = "folder_name";
                    comm.Text = dr.Name;
                    comm.PrevDirectory = dr.Parent.Name;
                    SendDataString(JsonConvert.SerializeObject(comm), Client);
                }
            }
            catch (Exception ex)
            {
                message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
            }
        }

        // { \"CommandType\" : \"get_folder_list" , "FilePath" : "c:\\muham" }
        public void SendDataString(String Data, Socket Client)
        {
            try
            {
                Thread.Sleep(100);
                byte[] arr = Encoding.UTF8.GetBytes(Data);
                Client.Send(arr);
            }
            catch (Exception ex)
            {
                message.Write(ex.Message,ConsoleMessage.MessageType.ERROR);
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
                message.Write(ex.Message, ConsoleMessage.MessageType.ERROR);
            }
        }
    }
}
