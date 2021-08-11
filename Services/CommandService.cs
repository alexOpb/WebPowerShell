using System.Collections.Generic;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading.Tasks;
using WebPowerShell.Models;
using WebPowerShell.Repositories;

namespace WebPowerShell.Services
{
    public class CommandService
    {
        private readonly CommandRepository _repository;
        
        public CommandService(CommandRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<string> Process(string command)
        {
            if (!await _repository.Exist(command))
            {
                await _repository.Save(new CommandModel
                {
                    CommandText = command
                });
            }
            
            var runSpace = RunspaceFactory.CreateRunspace();
            runSpace.Open();
            var pipeline = runSpace.CreatePipeline();
            pipeline.Commands.AddScript(command);
            pipeline.Commands.Add("Out-String");
            var results = pipeline.Invoke();
            runSpace.Close();

            var stringBuilder = new StringBuilder();

            foreach (var line in results)
                stringBuilder.AppendLine(line.ToString());

            return stringBuilder.ToString();
        }
        
        public async Task<IEnumerable<CommandModel>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}