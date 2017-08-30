using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Server
{
    public class ConsoleMessage
    {
        public enum MessageType
        {
            ERROR,
            SUCCESS,
            CONNECT,
            DISCONNECT,
            PROCESS,
            BACKPROCESS,
            NORMAL
        }
        public void Write(String Message , MessageType mType)
        {
            if (mType == MessageType.ERROR)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR : " + Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.SUCCESS)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("SUCCESS : " + Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.CONNECT)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("CONNECT : " + Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.DISCONNECT)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("DISCONNECT : " + Message);
                Console.BackgroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.NORMAL)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("MESSAGE : " + Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.PROCESS)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(Message);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (mType == MessageType.BACKPROCESS)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(Message);
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}

