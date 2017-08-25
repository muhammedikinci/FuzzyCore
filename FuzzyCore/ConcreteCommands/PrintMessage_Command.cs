using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyControl.Server.Data;

namespace fuzzyControl.Server
{
    public class PrintMessage_Command : Command
    {
        public PrintMessage_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            Console.WriteLine(Comm.CommandType);
        }
    }
}
