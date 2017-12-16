using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    public abstract class CommanderObject
    {
        protected IAsyncResult Result { get; set; }
        public CommanderObject(IAsyncResult Result)
        {
            this.Result = Result;
        }
        public abstract Client Invoke();
    }
}
