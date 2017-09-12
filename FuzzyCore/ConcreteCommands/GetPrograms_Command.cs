using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyCore.Data;
using FuzzyCore.Commands;
using Newtonsoft.Json;

namespace FuzzyCore.Server
{
    public class GetPrograms_Command : Command
    {
        public GetPrograms_Command(JsonCommand comm) : base(comm)
        {
        }

        public override void Execute()
        {
            OpenProgram Program = new OpenProgram(Comm);
            string[] list = Program.getProgramList();
            foreach (string item in list)
            {
                JsonCommand CurrentCommand = new JsonCommand();
                CurrentCommand.CommandType = "program_name";
                CurrentCommand.Text = item;
                Comm.Client_Socket.Send(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(CurrentCommand)));
            }
        }
    }
}
