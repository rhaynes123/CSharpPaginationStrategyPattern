namespace Physicists.Domain.Strategies;

public record StrategyOptions
{
    public int PageSize { get; init; }
    public int PageNumber { get; init; }
    public string? KeyWord { get; init; }
    public int LastId { get; init; }
}