using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    public class Invoker
    {
        Command Comm;
        public Invoker(Command Comm) { this.Comm = Comm; }
        public void Execute() { Comm.Execute(); }
    }
}
