using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebPowerShell.Models;
using WebPowerShell.Services;

namespace WebPowerShell.Controllers
{
    public class CommandsController : Controller
    {
        private readonly CommandService _service;

        public CommandsController(CommandService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Process([FromBody] string command)
        {
            try
            {
                var result = await _service.Process(command);
                return Ok(Json(result));
            }
            catch (Exception ex)
            {
                return Ok(Json(ex.Message));
            }
        }

        public async Task<IEnumerable<CommandModel>> GetAllCommands()
        {
            return await _service.GetAll();
        }
    }
}