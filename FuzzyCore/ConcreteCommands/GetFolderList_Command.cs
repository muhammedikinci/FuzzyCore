using FuzzyCore.Data;
using FuzzyCore.Commands;

namespace FuzzyCore.Server
{
    public class GetFolderList_Command : Command
    {
        public GetFolderList_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            GetFolderList folderList = new GetFolderList(Comm);
            folderList.SendFoldersName();
        }
    }
}
