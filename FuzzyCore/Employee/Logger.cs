using FuzzyCore.Server;
using Newtonsoft.Json;
using System;
using System.IO;

namespace FuzzyCore.Employee
{
    public class Logger
    {
        ConsoleMessage Message = new ConsoleMessage();
        public void WriteLog(MachineState MS,string FilePath)
        {
            string data = JsonConvert.SerializeObject(MS);
            string[] contents = null;
            if (File.Exists(FilePath))
            {
                contents = File.ReadAllLines(FilePath);
                Array.Resize(ref contents, contents.Length + 1);
                contents[contents.Length - 1] = data;
            }
            else
            {
                contents = new string[1];
                contents[0] = data;
            }
            File.WriteAllLines(Environment.CurrentDirectory + "\\" + FilePath, contents);
            Console.WriteLine("Log Writed at " + DateTime.Now);
        }
        public string GetLogs()
        {
            MachineState[] states = new MachineState[0];
            string[] contents = null;
            string data = "";
            if (File.Exists(BackgroundWorker.FilePath))
            {
                contents = File.ReadAllLines(BackgroundWorker.FilePath);
                states = new MachineState[contents.Length];

                for (int i = 0; i < contents.Length; i++)
                {
                    states[i] = JsonConvert.DeserializeObject<MachineState>(contents[i]);
                }
                data = JsonConvert.SerializeObject(states);
            }
            else
            {
                Message.Write("Log file not found!", ConsoleMessage.MessageType.ERROR);
            }

            return data;
        }
        /*
        public MachineState[] GetLogs()
        {
            MachineState[] states = new MachineState[0];
            string[] contents = null;
            if (File.Exists(BackgroundWorker.FilePath))
            {
                contents = File.ReadAllLines(BackgroundWorker.FilePath);
                states = new MachineState[contents.Length];

                for (int i = 0; i < contents.Length; i++)
                {
                    states[i] = JsonConvert.DeserializeObject<MachineState>(contents[i]);
                } 
            }
            else
            {
                Message.Write("Log file not found!",ConsoleMessage.MessageType.ERROR);
            }

            return states;
        }
        */
    }
}
