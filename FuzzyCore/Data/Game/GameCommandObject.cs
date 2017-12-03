using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Data.Game
{
    class Axis
    {
        public string X { get; set; }
        public string Y { get; set; }
        public string Z { get; set; }
    }
    class GameCommandObject
    {
        public string CommandType { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public Axis Location { get; set; }
        public Axis Rotation { get; set; }
    }
}
