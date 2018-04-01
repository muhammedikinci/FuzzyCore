using FuzzyCore.Commands;
using FuzzyCore.Data;
using FuzzyCore.CommandClasses;

namespace FuzzyCore.ConcreteCommands
{
    class GetLogs_Command : Command
    {
        public GetLogs_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            GetLogs logs = new GetLogs();
            string data = logs.GetAll_WithCommand();
            data.SendDataString(Comm.Client_Socket);
        }
    }
}
