using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyControl.Server.Data;

namespace fuzzyControl.Server
{
    public class ProcessKill_Command : Command
    {
        public ProcessKill_Command(JsonCommand Comm) : base(Comm)
        {

        }

        public override void Execute()
        {
            Console.WriteLine(Comm.CommandType);
        }
    }
}
