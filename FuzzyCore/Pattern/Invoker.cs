namespace FuzzyCore.Commands
{
    public class Invoker
    {
        Command Comm;
        public Invoker(Command Comm) { this.Comm = Comm; }
        public void Execute() { Comm.Execute(); }
    }
}
