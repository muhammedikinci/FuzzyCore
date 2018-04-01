using System;
using Newtonsoft.Json;
using FuzzyCore.Server;
using FuzzyCore.Commands;
using System.Net.Sockets;
using FuzzyCore.ConcreteCommands;

namespace FuzzyCore.Data
{
    public class DataParser
    {
        public static string LastCommand = "None";

        // {4} Cancel or Use embedded system commands
        public static bool SystemCommandIsActive = true;

        JsonCommand jsonComm;
        public static bool OutParserPermission = false;
        public static Action<JsonCommand> OutParser;
        Invoker INV;
        String Data;
        Socket Client;
        Initialize.InitType Type;
        public DataParser(String Data, Socket Client, Initialize.InitType Type)
        {
            this.Data = Data;
            this.Client = Client;
            this.Type = Type;
        }
        public void Parse()
        {
            if (Type.ServerProp.TYPE == Initialize.Props.ServerType.REMOTING)
            {
                Remoting();
            }
            else if (Type.ServerProp.TYPE == Initialize.Props.ServerType.DATATRANFSER)
            {
                DataTransfer();
            }
        }
        void Remoting()
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
                if (!SystemCommandIsActive)
                {
                    goto UserCommands;
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
                    case "get_logs":
                        {
                            GetLogs_Command getlog = new GetLogs_Command(jsonComm);
                            Command comm = getlog;
                            INV = new Invoker(comm);
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
                UserCommands:
                    if (OutParserPermission)
                    {
                        OutParser(jsonComm);
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.Source.ToString());
            }
        }
        void DataTransfer()
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
                if (!SystemCommandIsActive)
                {
                    goto UserCommands;
                }
                switch (jsonComm.CommandType)
                {
                    case "get_folder_list":
                        {
                            GetFolderList.Test_StackBoolean = true;
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
                    default:
                        {
                            if (OutParserPermission)
                            {
                                OutParser(jsonComm);
                            }
                            break;
                        }
                }
            UserCommands:
                if (OutParserPermission)
                {
                    OutParser(jsonComm);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ex.Source.ToString());
            }
        }
    }
}
