namespace Physicists.Domain.Strategies;
using Physicists.Models;
public interface IPaginationStrategy
{
    Task<IEnumerable<Physicist>> GetPhysicists(StrategyOptions options);
}