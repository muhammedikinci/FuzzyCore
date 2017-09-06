using FuzzyCore.Data;

namespace FuzzyCore.Commands
{
    public abstract class Command
    {
        protected JsonCommand Comm { get; set; }
        public Command(JsonCommand comm)
        {
            this.Comm = comm;
        }
        public abstract void Execute();
    }
}
