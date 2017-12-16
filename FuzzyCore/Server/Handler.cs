using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    class Handler
    {
        private CommanderObject ListenerObject { get; set; }
        public Client Invoke()
        {
            return ListenerObject.Invoke();
        }
        public Handler(CommanderObject ListenerObject)
        {
            this.ListenerObject = ListenerObject;
        }
    }
}
