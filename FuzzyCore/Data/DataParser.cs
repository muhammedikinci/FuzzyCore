using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FuzzyCore.Server;
using FuzzyCore.Commands;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;

namespace FuzzyCore.Data
{
    public class DataParser
    {
        public static string LastCommand = "None";
        public DataParser(String Data, Socket Client)
        {
            try
            {
                JsonCommand jsonComm = JsonConvert.DeserializeObject<JsonCommand>(Data);
                jsonComm.Client_Socket = Client;
                LastCommand = jsonComm.CommandType.ToString();
                switch (jsonComm.CommandType)
                {
                    case "print_message":
                        {
                            PrintMessage_Command PrintMessageComm = new PrintMessage_Command(jsonComm);
                            Command Comm = PrintMessageComm;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "open_program":
                        {
                            ProgramOpen_Command ProgramOpenCommand = new ProgramOpen_Command(jsonComm);
                            Command Comm = ProgramOpenCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "get_folder_list":
                        {
                            GetFolderList_Command GetFolderListCommand = new GetFolderList_Command(jsonComm);
                            Command Comm = GetFolderListCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "get_file":
                        {
                            GetFile_Command GetFileCommand = new GetFile_Command(jsonComm);
                            Command Comm = GetFileCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "get_programs":
                        {
                            GetPrograms_Command GetProgramsCommand = new GetPrograms_Command(jsonComm);
                            Command Comm = GetProgramsCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "get_processes":
                        {
                            GetProcesses_Command GetProcessesCommand = new GetProcesses_Command(jsonComm);
                            Command Comm = GetProcessesCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    case "process_kill":
                        {
                            ProcessKill_Command ProcessKillCommand = new ProcessKill_Command(jsonComm);
                            Command Comm = ProcessKillCommand;
                            Invoker Inv = new Invoker(Comm);
                            Inv.Execute();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Null Command");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
