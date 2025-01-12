using Microsoft.EntityFrameworkCore;
using Physicists.Models;
using Physicists.Data;
namespace Physicists.Domain.Strategies;

public class BasicPaginationStrategy: IPaginationStrategy
{
    private readonly ApplicationDbContext _dbContext;

    public BasicPaginationStrategy(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    /// <inheritdoc cref="IPaginationStrategy"/>
   
    public async Task<IEnumerable<Physicist>> GetPhysicists(StrategyOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.PageSize < 0 || options.PageNumber < 0)
        {
            throw new InvalidOperationException("Page size and/or Page Number cannot be less than zero.");
        }
        var physicists = string.IsNullOrWhiteSpace(options.KeyWord) 
            ? _dbContext.Physicists 
            : _dbContext.Physicists.Where(p => p.Name.Contains(options.KeyWord));
        
        return await physicists
            .AsNoTracking()
            .OrderBy(phy => phy.Id)
            .Skip((options.PageNumber - 1) * options.PageSize)
            .Take(options.PageSize)
            .ToListAsync();
    }
}