using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Data
{
    class DataSerializer
    {
        public static String LastData = null;
        public static bool CreateCommand = false;
        public String Serialize(String Data)
        {
            if (CreateCommand)
            {
                LastData = null;
                CreateCommand = false;
            }
            if (Data.IndexOf("{") != -1)
            {
                if (Data.IndexOf("}") != -1)
                {
                    return Data;
                }
                else
                {
                    LastData = Data;
                    return "WAIT_NEXT_DATA";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(LastData))
                {
                    CreateCommand = true;
                    return LastData + Data;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}
