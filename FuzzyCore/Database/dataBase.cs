using System;

namespace FuzzyCore.Database
{
    public class dataBase
    {
        public databases Database = databases.NULL;
        public string Host = null;
        public string UserName = null;
        public string Password = null;
        public string DatabaseName = null;

        Server.ConsoleMessage Message = new Server.ConsoleMessage();

        public mongodb Mongodb { get { return Mongodb_Private; } }
        public mysql Mysql { get { return Mysql_Private; } }
        public mssql Mssql { get { return Mssql_Private; } }

        private mongodb Mongodb_Private;
        private mysql Mysql_Private;
        private mssql Mssql_Private;

        public enum databases
        {
            MONGODB,
            MYSQL,
            MSSQL,
            NULL
        }

        public void init()
        {
            int stat = variableControl();

            if (stat == 0)
            {
                Message.Write("Selected database : " + Database, Server.ConsoleMessage.MessageType.BACKPROCESS);
                Message.Write("Initializing", Server.ConsoleMessage.MessageType.BACKPROCESS);
                switch (Database)
                {
                    case databases.MONGODB:
                        Mongodb_Private = new mongodb(DatabaseName,Host,UserName,Password);
                        break;
                    case databases.MSSQL:
                        break;
                    case databases.MYSQL:
                        break;
                    case databases.NULL:
                        break;
                }
            }
            else if (stat == 1)
            {
                Message.Write("Selected database : " + Database, Server.ConsoleMessage.MessageType.BACKPROCESS);
                Message.Write("Initializing", Server.ConsoleMessage.MessageType.BACKPROCESS);
                switch (Database)
                {
                    case databases.MONGODB:
                        Mongodb_Private = new mongodb(DatabaseName , Host);
                        break;
                    case databases.MSSQL:
                        break;
                    case databases.MYSQL:
                        break;
                    case databases.NULL:
                        break;
                }
            }
            else if (stat == 2)
            {
                Message.Write("Selected database : " + Database, Server.ConsoleMessage.MessageType.BACKPROCESS);
                Message.Write("Initializing", Server.ConsoleMessage.MessageType.BACKPROCESS);
                switch (Database)
                {
                    case databases.MONGODB:
                        Mongodb_Private = new mongodb(DatabaseName);
                        break;
                    case databases.MSSQL:
                        break;
                    case databases.MYSQL:
                        break;
                    case databases.NULL:
                        break;
                }
            }
            else
            {
                Console.WriteLine("Database or database name not selected!");
            }
        }

        public int variableControl()
        {
            if (!string.IsNullOrEmpty(DatabaseName) && Database != databases.NULL && Host != null && UserName != null && Password != null)
            {
                return 0;
            }
            else if (!string.IsNullOrEmpty(DatabaseName) && Database != databases.NULL && Host != null)
            {
                return 1;
            }
            else if (!string.IsNullOrEmpty(DatabaseName) && Database != databases.NULL)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }

        //Full data constructor = 0
        public dataBase(databases Database , string DatabaseName , string UserName , string Password , string Host)
        {
            this.Database = Database;
            this.DatabaseName = DatabaseName;
            this.Host = Host;
            this.UserName = UserName;
            this.Password = Password;
        }
        //Database Name contructor = 1
        public dataBase(databases Database , string DatabaseName)
        {
            this.Database = Database;
            this.DatabaseName = DatabaseName;
        }
        //Database Name and host contructor = 2
        public dataBase(databases Database, string DatabaseName , string Host)
        {
            this.Database = Database;
            this.DatabaseName = DatabaseName;
            this.Host = Host;
        }
    }
}
