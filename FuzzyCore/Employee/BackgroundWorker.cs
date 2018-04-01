using FuzzyCore.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FuzzyCore.Employee
{
    public class BackgroundWorker
    {
        ConsoleMessage Message = new ConsoleMessage();
        static bool Stopper = false;
        static int Step = 0;
        static bool Logged = false;

        public static string FilePath { get; set; }
        public static int WaitingSeconds { get; set; }

        Thread Worker = new Thread(new ThreadStart(() =>
        {
            while (!Stopper)
            {
                DetectState();
            }
        }));

        public void StartWorker(ref bool IsWorking)
        {
            try
            {
                Worker.Start();
                Worker.IsBackground = true;
                IsWorking = true;
            }
            catch (Exception e)
            {
                Message.Write(e.Message.ToString(),ConsoleMessage.MessageType.ERROR);
                IsWorking = false;
            }
        }
        public void StopWorking()
        {
            Stopper = true;
            Worker.IsBackground = false;
            Worker.Abort();
        }
        static void DetectState()
        {
            string LocX = Cursor.Position.X.ToString();
            string LocY = Cursor.Position.Y.ToString();

            Thread.Sleep(1000);

            if (LocX == Cursor.Position.X.ToString() &&
                LocY == Cursor.Position.Y.ToString())
            {
                Step++;
            }
            else
            {
                if (Logged == true)
                {
                    PerformanceCounter Counter = new PerformanceCounter();
                    Counter.CategoryName = "Processor";
                    Counter.CounterName = "% Processor Time";
                    Counter.InstanceName = "_Total";
                    Counter.NextValue();
                    Thread.Sleep(1000);

                    MachineState MS = new MachineState();
                    MS.IsActive = true;
                    MS.Timing = new ActivePassive();
                    MS.Timing.ToActive = DateTime.Now.ToString();
                    MS.CPU_Percentage = Counter.NextValue().ToString() + "%";

                    Logger l = new Logger();
                    l.WriteLog(MS,FilePath);

                    Logged = false;

                    Step = 0;
                }
                else
                {
                    Step = 0;
                }
            }

            if (Step > WaitingSeconds && Logged == false)
            {
                PerformanceCounter Counter = new PerformanceCounter();
                Counter.CategoryName = "Processor";
                Counter.CounterName = "% Processor Time";
                Counter.InstanceName = "_Total";
                Counter.NextValue();
                Thread.Sleep(1000);

                MachineState MS = new MachineState();
                MS.IsActive = false;
                MS.Timing = new ActivePassive();
                MS.Timing.ToPassive = DateTime.Now.ToString();
                MS.CPU_Percentage = Counter.NextValue().ToString()+"%";

                Logger l = new Logger();
                l.WriteLog(MS,FilePath);

                Logged = true;

                Step = 0;
            }
        }
    }
}
