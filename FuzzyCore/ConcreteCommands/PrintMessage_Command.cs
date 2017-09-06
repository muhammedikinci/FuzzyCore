using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Data;
using FuzzyCore.Commands;

namespace FuzzyCore.Server
{
    public class PrintMessage_Command : Command
    {
        public PrintMessage_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            PrintMessage PM = new PrintMessage(this.Comm);
        }
    }
}
