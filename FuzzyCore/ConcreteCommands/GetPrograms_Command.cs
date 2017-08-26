using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Server.Data;

namespace FuzzyCore.Server
{
    public class GetPrograms_Command : Command
    {
        public GetPrograms_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            Console.WriteLine(Comm.CommandType);
        }
    }
}
