using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebPowerShell.Data;
using WebPowerShell.Models;

namespace WebPowerShell.Repositories
{
    public class CommandRepository
    {
        private readonly ApplicationDbContext _context;
        
        public CommandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Save(CommandModel model)
        {
            await _context.Command.AddAsync(model);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommandModel>> GetAll()
        {
            return await _context.Command.ToListAsync();
        }

        public async Task<bool> Exist(string command)
        {
            return await _context.Command.AnyAsync(c => c.CommandText == command);
        }
    }
}