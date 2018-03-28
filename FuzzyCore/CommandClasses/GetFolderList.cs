using System;
using System.IO;
using Newtonsoft.Json;
using FuzzyCore.Server;
using FuzzyCore.CommandClasses;

namespace FuzzyCore.Commands
{
    public class GetFolderList
    {
        public static bool Test_StackBoolean = false;
        ConsoleMessage message = new ConsoleMessage();
        Data.JsonCommand Command;
        public GetFolderList(Data.JsonCommand Comm)
        {
            Command = Comm;
        }
        public void SendFoldersName()
        {
            try
            {
                var Client = Command.Client_Socket;

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
                    JsonConvert.SerializeObject(comm).SendDataString(Client);
                }
                foreach (string item in folderPath)
                {
                    DirectoryInfo dr = new DirectoryInfo(item);
                    message.Write(dr.Name, ConsoleMessage.MessageType.SUCCESS);
                    Data.JsonCommand comm = new Data.JsonCommand();
                    comm.CommandType = "folder_name";
                    comm.Text = dr.Name;
                    comm.PrevDirectory = dr.Parent.Name;
                    JsonConvert.SerializeObject(comm).SendDataString(Client);
                }

                Test_StackBoolean = true;
            }
            catch (Exception ex)
            {
                message.Write(ex.Message.ToString(), ConsoleMessage.MessageType.ERROR);
                Console.WriteLine("GetFolderList->GetFoldersName");

                Test_StackBoolean = true;
            }
        }
    }
}
