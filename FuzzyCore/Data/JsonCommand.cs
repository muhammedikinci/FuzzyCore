using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FuzzyCore.Data
{
    public class JsonCommand
    {
        public String CommandType { get; set; }
        public String Premission { get; set; }

        public float AfterTime { get; set; }
        public float OverTime { get; set; }
        public bool Repeat { get; set; }
        public int RepeatStep { get; set; }

        public String FormCaption { get; set; }
        public float FontSize { get; set; }
        public String Text { get; set; }
        public String FilePath { get; set; }
        public String PrevDirectory { get; set; }

        public Socket Client_Socket { get; set; }
    }
}
