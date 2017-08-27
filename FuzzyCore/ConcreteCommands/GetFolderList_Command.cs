using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Server.Data;
using FuzzyCore.Server.Commands;

namespace FuzzyCore.Server
{
    public class GetFolderList_Command : Command
    {
        public GetFolderList_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            GetFolderList GetFolderList_Command = new GetFolderList();
            GetFolderList_Command.GetFoldersName(Comm);
        }
    }
}
