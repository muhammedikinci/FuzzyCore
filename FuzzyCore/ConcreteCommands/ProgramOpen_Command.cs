using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Server.Data;
using FuzzyCore.Server.Commands;

namespace FuzzyCore.Server
{
    public class ProgramOpen_Command : Command
    {
        public ProgramOpen_Command(JsonCommand Comm) : base(Comm)
        {
        }

        public override void Execute()
        {
            OpenProgram Op = new OpenProgram(Comm);
        }
    }
}
