# FuzzyCore
>Remote Control for Buisness

### Quick Start
```c#
using System;
using FuzzyCore.Server;
using FuzzyCore.Initialize;

namespace TT
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMessage message = new ConsoleMessage();
            Init FuzzyInit = new Init("Program.json",Accept,Receive,"127.0.0.1","111");
            if (FuzzyServer.socketState)
            {
                message.Write("Listening! -> "+FuzzyServer.IPAndPort,ConsoleMessage.MessageType.BACKPROCESS);
            }
            Console.ReadLine();
        }
        static void Accept(Client cl)
        {

        }
        static void Receive(string str, Client cl)
        {

        }
    }
}
```

[Simple Main Project](https://github.com/muhammedikinci/FuzzyCore/wiki/Main-Project-Simple)

[Advanced Main Project](https://github.com/muhammedikinci/FuzzyCore/wiki/Main-Project-Advanced)

[Command Examples](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples)

[Database Example](https://github.com/muhammedikinci/FuzzyCore/wiki/Database-Example)

[Service Example](https://github.com/muhammedikinci/FuzzyCore/wiki/Service-Examples)


<h3>For Open Program Command Json File</h3>

<b>Program.json</b> <span> - add to main project (deployment)bin/debug</span>
```json
﻿[
  {
    "ProgramName": "Spotify",
    "Path": "C:\\Users\\exampleUser\\AppData\\Roaming\\Spotify\\Spotify.exe",
    "KillName": "Spotify"
  },
  {
    "ProgramName": "SublimeText",
    "Path": "C:\\Program Files\\Sublime Text 3\\sublime_text.exe",
    "KillName": "sublime_text"
  },
  {
    "ProgramName": "Robomongo",
    "Path": "C:\\Program Files\\Robomongo 1.0.0\\Robomongo.exe",
    "KillName": "Robomongo"
  },
  {
    "ProgramName": "Xampp",
    "Path": "D:\\Xampp\\xampp-control.exe",
    "KillName": "xampp-control"
  }
]
```
