using System;
using System.Collections.Generic;
using System.IO;
using FuzzyCore.Initialize;
using Newtonsoft.Json;

namespace FuzzyCore.Permissions
{
    public class MacPermission : IPermission
    {
        public string FileContent { get; set; }

        public string FilePath { get; set; }

        public List<PermissionMac> JsonObject { get; set; }

        public PermissionMac MacObject;

        public bool FileControl()
        {
            return File.Exists(FilePath);
        }

        public bool PermissionControl()
        {
            Serialize();
            try
            {
                if (JsonObject.Count > 0)
                {
                    for (int i = 0; i < JsonObject.Count; i++)
                    {
                        if (JsonObject[i].MacAddress == MacObject.MacAddress)
                        {
                            switch (JsonObject[i].Permission)
                            {
                                case "YES":
                                    return true;
                                case "NO":
                                    return false;
                                default:
                                    return false;
                            }
                        }
                    }
                }
                else
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Server.ConsoleMessage.WriteException(ex.Message, "MacPermission", "PermissionControl");
                return true;
            }
        }

        public void Serialize()
        {
            if (FileControl())
            {
                using (StreamReader Reader = new StreamReader(FilePath))
                {
                    FileContent = Reader.ReadToEnd();
                    JsonObject = JsonConvert.DeserializeObject<List<PermissionMac>>(FileContent);
                }
            }
        }
        public class PermissionMac
        {
            public string MacAddress { get; set; }
            public string Permission { get; set; }
        }
    }
}
