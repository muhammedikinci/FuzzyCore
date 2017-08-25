using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fuzzyControl.Server.Data;
using fuzzyControl.Server.Commands;

namespace fuzzyControl.Server
{
    public class GetFile_Command : Command
    {
        public GetFile_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            GetFile GetFileComm = new GetFile();
            GetFileComm.GetFileBytes(Comm);
        }
    }
}
