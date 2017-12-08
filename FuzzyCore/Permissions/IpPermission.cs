using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace FuzzyCore.Permissions
{
    public class IpPermission : IPermission
    {
        List<PermissionIP> Objects { get; set; }

        public string FileContent { get; set; }

        public string FilePath { get; set; }

        public bool FileControl()
        {
            return System.IO.File.Exists(FilePath);
        }
        public string TargetIP { get; set; }
        public bool PermissionControl()
        {
            Serialize();
            try
            {
                if (Objects.Count > 0)
                {
                    for (int i = 0; i < Objects.Count; i++)
                    {
                        if (Objects[i].IPAddress == TargetIP)
                        {
                            switch (Objects[i].Permission)
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
                Server.ConsoleMessage.WriteException(ex.Message,"IpPermission","PermissionControl");
                return true;
            }
        }

        public void Serialize()
        {
            if (FileControl())
            {
                using (System.IO.StreamReader Reader = new System.IO.StreamReader(FilePath))
                {
                    FileContent = Reader.ReadToEnd();
                    Objects = JsonConvert.DeserializeObject<List<PermissionIP>>(Reader.ReadToEnd());
                }
            }
        }

        public class PermissionIP
        {
            public string IPAddress { get; set; }
            public string Permission { get; set; }
        }

    }
}
