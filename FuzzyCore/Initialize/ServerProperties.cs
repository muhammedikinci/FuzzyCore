namespace FuzzyCore.Initialize
{
    public class InitType
    {
        public enum Type
        {
            BASIC,
            CONFIG
        }
        public Type initType = Type.BASIC;
        public Props ServerProp { get; set; }
        public FilePaths Paths { get; set; }
        public bool Wcf_Running { get; set; }
        public bool Server_Running { get; set; }
        public string FirstConsole_Message { get; set; }
        public bool Logging { get; set; }
        public int LoggingTime { get; set; }
    }
    public class Props
    {
        public enum ServerType
        {
            REMOTING,
            GAMESERVER,
            DATATRANFSER
        }
        public ServerType TYPE = ServerType.REMOTING;
        public string IP { get; set; }
        public string Port { get; set; }
    }
    public class FilePaths
    {
        public string ProgramJson;
        public string MacPermissions;
        public string IpPermissions;
        public string LogFile;
    }
}
