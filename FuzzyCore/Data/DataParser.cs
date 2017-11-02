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
using System.IO;
using System.Reflection;

namespace FuzzyCore.Data
{
    public class DataParser
    {
        public static string LastCommand = "None";
        JsonCommand jsonComm;
        public static bool OutParserPermission = false;
        public static Action<JsonCommand> OutParser;
        Invoker INV;
        public DataParser(String Data, Socket Client)
        {
            try
            {
                DataSerializer ds = new DataSerializer();
                string a = ds.Serialize(Data);
                if (a == "WAIT_NEXT_DATA")
                {
                    jsonComm.CommandType = "WAIT_NEXT_DATA";
                    jsonComm.Client_Socket = Client;
                    LastCommand = jsonComm.CommandType.ToString();
                }
                else
                {
                    jsonComm = JsonConvert.DeserializeObject<JsonCommand>(a);
                    jsonComm.Client_Socket = Client;
                    LastCommand = jsonComm.CommandType.ToString();
                }
                switch (jsonComm.CommandType)
                {
                    case "print_message":
                        {
                            PrintMessage_Command PrintMessageComm = new PrintMessage_Command(jsonComm);
                            Command Comm = PrintMessageComm;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "open_program":
                        {
                            ProgramOpen_Command ProgramOpenCommand = new ProgramOpen_Command(jsonComm);
                            Command Comm = ProgramOpenCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "get_folder_list":
                        {
                            GetFolderList_Command GetFolderListCommand = new GetFolderList_Command(jsonComm);
                            Command Comm = GetFolderListCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "get_file":
                        {
                            GetFile_Command GetFileCommand = new GetFile_Command(jsonComm);
                            Command Comm = GetFileCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "get_programs":
                        {
                            GetPrograms_Command GetProgramsCommand = new GetPrograms_Command(jsonComm);
                            Command Comm = GetProgramsCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "get_processes":
                        {
                            GetProcesses_Command GetProcessesCommand = new GetProcesses_Command(jsonComm);
                            Command Comm = GetProcessesCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    case "process_kill":
                        {
                            ProcessKill_Command ProcessKillCommand = new ProcessKill_Command(jsonComm);
                            Command Comm = ProcessKillCommand;
                            INV = new Invoker(Comm);
                            INV.Execute();
                            break;
                        }
                    default:
                        {
                            if (OutParserPermission)
                            {
                                OutParser(jsonComm);
                            }
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.Source.ToString());
            }
        }
    }
}
