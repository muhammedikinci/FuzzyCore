using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using fuzzyControl.Server.Data;
using System.Threading;

namespace fuzzyControl.Server.Commands
{
    class OpenProgram
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

        public void Open(JsonCommand Comm)
        {
            this.Comm = Comm;
            OpenJson();
            for (int i = 0; i < Progs.Count; i++)
            {
                if (Progs[i].ProgramName == Comm.Text)
                {
                    System.Diagnostics.Process.Start(Progs[i].Path);
                }
            }
        }
        public void OpenAfter(JsonCommand Comm)
        {
            this.Comm = Comm;
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
        public void OpenOver(JsonCommand Comm)
        {
            this.Comm = Comm;
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
        public void OpenOverAndAfter(JsonCommand Comm)
        {
            this.Comm = Comm;
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
