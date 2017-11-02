using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyCore.Permissions
{
    interface IPermission
    {
        string FilePath { get; set; }
        string FileContent { get; set; }
        bool PermissionControl();
        bool FileControl();
        void Serialize(); 
    }
}
