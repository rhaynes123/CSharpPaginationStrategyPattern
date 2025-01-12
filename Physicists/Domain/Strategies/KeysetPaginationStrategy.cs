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
    /// <inheritdoc cref="IPaginationStrategy"/>
    public async Task<IEnumerable<Physicist>> GetPhysicists(StrategyOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.PageSize < 0)
        {
            throw new InvalidOperationException("Page size cannot be less than zero.");
        }
        if (options.LastId < 1)
        {
            throw new InvalidOperationException("Last Id has to be greater than zero.");
        }
        
        return await _dbContext.Physicists
            .Where(p => p.Id > options.LastId)
            .AsNoTracking()
            .OrderBy(phy => phy.Id)
            .Take(options.PageSize)
            .ToListAsync();
    }
}