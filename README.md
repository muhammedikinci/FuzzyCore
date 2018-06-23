## **Fuzzy Remote**
You can use this library to access other computers from the computer where you can set permissions. Some commands you can use during access are available in the library. 

The library can use TCP and UDP protocols, but there is only TCP usage for commands. Transports data in JSON format on TCP stream.

**Commands:**

 - [Print Message](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples#example-print-message*)
 - [Open Program](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples#example-open-program)
 - [List&View -> Files And Folders](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples#example-listview---files-and-folders)
 - [Get File](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples#example-get-file)
 - Get Log (Last Added)
	>Logging tool added to Initializer Class. Can be started as Basic Initializing. When no operation is done in Basic for 10 seconds, the passivity log is recorded. When the motion is detected, the activity is recorded this time. These records are transferred into "Your Project Directory / Debug / Logs / Log.json" in Basic.
The GetLogs command has been added to the system to retrieve logs. Acceptable logs will be retrieved and examined by the Clients.

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

## Other Ways
### Config Start
```c#
using System;
using FuzzyCore.Server;
using FuzzyCore.Initialize;
class Program
{
    static void Main(string[] args)
    {
        ConsoleMessage message = new ConsoleMessage();
        ConfigReader RD = new ConfigReader();
        InitType Type = RD.Read();
        Init FuzzyInit = new Init(Type);
        Console.ReadLine();
    }
}
```

### Quick Start - Alternavite 1
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

### Quick Start - Alternavite 2
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
            Init FuzzyInit = new Init();
            if (FuzzyServer.socketState)
            {
                message.Write("Listening! -> "+FuzzyServer.IPAndPort,ConsoleMessage.MessageType.BACKPROCESS);
            }
            Console.ReadLine();
        }
    }
}
```

## Private Files
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
<h3>Permissions Example : MacPermission</h3>

<b>Mac.json</b> <span> - add to main project (deployment)bin/debug/Permissions</span>
```json
[
  {
    "MacAddress": "heheh",
    "Permission": "YES"
  },
  {
    "MacAddress": "nope",
    "Permission": "NO"
  }
]
```
<h3>IpPermission</h3>

<b>Ip.json</b> <span> - add to main project (deployment)bin/debug/Permissions</span>
```json
[
  {
    "IpAddress": "0.0.0.0",
    "Permission": "YES"
  },
  {
    "IpAddress": "127.0.0.1",
    "Permission": "NO"
  }
]
```

## Contributing
You can;

 - set crypt algorithms to process all data in the network stream.
 - add new commands.
 - review tcp listener and sender function for all crashes.
 - write TESTS  :D

## Browse Diagrams

<img src="https://github.com/muhammedikinci/FuzzyCore/blob/master/UML/Employee-Manager.svg">
<img src="https://github.com/muhammedikinci/FuzzyCore/blob/master/UML/initializing-uml-fuzzy.svg">

## **Browse Wiki**

[Simple Main Project](https://github.com/muhammedikinci/FuzzyCore/wiki/Main-Project-Simple)

[Advanced Main Project](https://github.com/muhammedikinci/FuzzyCore/wiki/Main-Project-Advanced)

[Command Examples](https://github.com/muhammedikinci/FuzzyCore/wiki/Command-Examples)

[Database Example](https://github.com/muhammedikinci/FuzzyCore/wiki/Database-Example)

[Service Example](https://github.com/muhammedikinci/FuzzyCore/wiki/Service-Examples)

![Example Result](https://image.prntscr.com/image/Vu4EWxinQSSSHRDndG46mA.png)
