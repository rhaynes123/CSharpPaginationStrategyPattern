using Microsoft.EntityFrameworkCore;
using Physicists.Data;
using Physicists.Models;

namespace Physicists.Domain.Strategies;

public class KeysetPaginationStrategy: IPaginationStrategy
{
    private readonly ApplicationDbContext _dbContext;

    public KeysetPaginationStrategy(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Physicist>> GetPhysicists(StrategyOptions options)
    {
        var physicists = options.LastId == 0
            ? _dbContext.Physicists 
            : _dbContext.Physicists.Where(p => p.Id > options.LastId);
        
        return await physicists
            .AsNoTracking()
            .OrderBy(phy => phy.Id)
            .Take(options.PageSize)
            .ToListAsync();
    }
}