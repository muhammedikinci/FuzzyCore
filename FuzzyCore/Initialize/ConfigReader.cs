using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using FuzzyCore.Server;

namespace FuzzyCore.Initialize
{
    public class ConfigReader
    {
        public string ConfigFile_Path = "./config.json";
        public string ConfigFile_Content = "";
        bool ConfigFile_Control()
        {
            return File.Exists(ConfigFile_Path);
        }
        public InitType Read(string path = "./config.json")
        {
            try
            {
                ConfigFile_Path = path;
                using (StreamReader Rd = new StreamReader(ConfigFile_Path))
                {
                    ConfigFile_Content = Rd.ReadToEnd();
                    return JsonConvert.DeserializeObject<InitType>(ConfigFile_Content);
                }
            }
            catch (Exception Ex)
            {
                ConsoleMessage.WriteException(Ex.Message,"ConfigReader.cs","Read");
                return new InitType();
            }
        }
    }
}
