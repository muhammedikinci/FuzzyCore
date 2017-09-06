using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using FuzzyCore.Data;
using System.Threading;

namespace FuzzyCore.Commands
{
    public class OpenProgram
    {
        List<Programs> Progs;
        JsonCommand Comm;
        int CurrentProgramIndex;
        private void OpenJson()
        {
            using (StreamReader Reader = new StreamReader("Programs.json"))
            {
                string ProgramsJson = Reader.ReadToEnd();
                Progs = JsonConvert.DeserializeObject<List<Programs>>(ProgramsJson);
            }
        }

        public OpenProgram(JsonCommand Comm)
        {
            this.Comm = Comm;
            DataProcess();
        }

        void DataProcess()
        {
            if (!string.IsNullOrEmpty(Comm.Text))
            {
                if (Comm.OverTime > 500 && Comm.AfterTime > 500)
                {
                    OpenOverAndAfter();
                }
                else if (Comm.OverTime > 500)
                {
                    OpenOver();
                }
                else if (Comm.AfterTime > 500)
                {
                    OpenAfter();
                }
                else
                {
                    Open();
                }
            }
        }

        void Open()
        {
            OpenJson();
            for (int i = 0; i < Progs.Count; i++)
            {
                if (Progs[i].ProgramName == Comm.Text)
                {
                    System.Diagnostics.Process.Start(Progs[i].Path);
                }
            }
        }
        void OpenAfter()
        {
            OpenJson();
            for (int i = 0; i < Progs.Count; i++)
            {
                if (Progs[i].ProgramName == Comm.Text)
                {
                    CurrentProgramIndex = i;
                    Thread OAThread = new Thread(new ThreadStart(OAVoid));
                    OAThread.Start();
                }
            }
        }
        void OAVoid()
        {
            Thread.Sleep((int)Comm.AfterTime);
            System.Diagnostics.Process.Start(Progs[CurrentProgramIndex].Path);
        }
        void OpenOver()
        {
            OpenJson();
            for (int i = 0; i < Progs.Count; i++)
            {
                if (Progs[i].ProgramName == Comm.Text)
                {
                    CurrentProgramIndex = i;
                    System.Diagnostics.Process.Start(Progs[i].Path);
                    Thread OOThread = new Thread(new ThreadStart(OOVoid));
                    OOThread.Start();
                }
            }
        }
        void OOVoid()
        {
            Thread.Sleep((int)Comm.OverTime);
            foreach (var Process in System.Diagnostics.Process.GetProcessesByName(Progs[CurrentProgramIndex].KillName))
            {
                Process.Kill();
            }
        }
        public void OpenOverAndAfter()
        {
            OpenJson();
            for (int i = 0; i < Progs.Count; i++)
            {
                if (Progs[i].ProgramName == Comm.Text)
                {
                    System.Diagnostics.Process.Start(Progs[i].Path);
                }
            }
        }

        public string[] getProgramList()
        {
            OpenJson();
            string[] Arr = new string[Progs.Count];
            for (int i = 0; i < Progs.Count; i++)
            {
                Arr[i] = Progs[i].ProgramName;
            }
            return Arr;
        }

        class Programs
        {
            public string ProgramName { get; set; }
            public string Path { get; set; }
            public string KillName { get; set; }
        }
    }
}
