namespace Physicists.Domain.Strategies;
using Physicists.Models;
public interface IPaginationStrategy
{
    /// <summary>
    
    /// Uses a pagination strategy to get a collection of physicists
    /// </summary>
    /// <param name="options">Different data that each strategy will rely on</param>
    /// <exception cref="InvalidOperationException">If data the strategy in question needs is not provided</exception>
    /// <exception cref="ArgumentNullException">If options are not provided</exception>
    /// <returns> A task that represents a collection of Physicists</returns>
    Task<IEnumerable<Physicist>> GetPhysicists(StrategyOptions options);
}