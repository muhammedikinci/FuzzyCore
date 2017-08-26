using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Server.Data;

namespace FuzzyCore.Server
{
    public class GetFolderList_Command : Command
    {
        public GetFolderList_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            Commands.GetFolderList GetFolderList_Command = new Commands.GetFolderList();
            GetFolderList_Command.GetFoldersName(Comm);
        }
    }
}
