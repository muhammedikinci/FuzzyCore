using FuzzyCore.Commands;
using FuzzyCore.Data;
using FuzzyCore.Employee;
using Newtonsoft.Json;

namespace FuzzyCore.CommandClasses
{
    public class GetLogs
    {
        public string GetAll_WithCommand()
        {
            Logger L = new Logger();
            string logs = L.GetLogs();
            JsonCommand Com = new JsonCommand();
            Com.CommandType = "get_logs";
            Com.Text = logs;
            return JsonConvert.SerializeObject(Com);
        }
    }
}
