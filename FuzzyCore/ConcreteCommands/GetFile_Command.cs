using System;
using FuzzyCore.Data;
using FuzzyCore.Commands;
using FuzzyCore.CommandClasses;

namespace FuzzyCore.Server
{
    public class GetFile_Command : Command
    {
        public GetFile_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            GetFile getFile = new GetFile(Comm);
            var fileBytes = getFile.GetFileBytes();
            fileBytes.SendDataArray(Comm.Client_Socket);
        }
    }
}
