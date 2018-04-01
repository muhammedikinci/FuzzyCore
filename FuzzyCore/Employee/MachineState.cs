using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Employee
{
    public class ActivePassive
    {
        public string ToActive { get; set; }
        public string ToPassive { get; set; }
    }
    public class MachineState
    {
        public bool IsActive { get; set; }
        public ActivePassive Timing { get; set; }
        public string CPU_Percentage { get; set; }
    }
}
