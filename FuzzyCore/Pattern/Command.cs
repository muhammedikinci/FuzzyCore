using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyControl.Server.Data;

namespace fuzzyControl.Server
{
    public abstract class Command
    {
        protected JsonCommand Comm { get; set; }
        public Command(JsonCommand comm)
        {
            this.Comm = comm;
        }
        public abstract void Execute();
    }
}
