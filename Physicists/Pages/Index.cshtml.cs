using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Physicists.Data;
using Physicists.Domain.Strategies;
using Physicists.Models;

namespace Physicists.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ApplicationDbContext _db;
    
    public List<Physicist> Physicists { get; set; } = [];
    
    [BindProperty(SupportsGet = true)]
    public int PageSize { get; set; } = 10;
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;
    
    [BindProperty(SupportsGet = true)]
    public string? SearchString { get; set; }
    public int Total { get; set; }
    [BindProperty(SupportsGet = true)] public int LastId { get; set; } = 0;

    public IndexModel(ILogger<IndexModel> logger
    , ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    private IPaginationStrategy GetStrategy()
    {
        return LastId == 0 ? new BasicPaginationStrategy(_db) : new KeysetPaginationStrategy(_db);
    }

    public async Task<IActionResult> OnGet()
    {
        Total = string.IsNullOrWhiteSpace(SearchString) 
            ? await _db.Physicists.CountAsync() 
            : await _db.Physicists
                .Where(phy => phy.Name.Contains(SearchString))
                .CountAsync();
        var strategy = GetStrategy();
        var results = await strategy.GetPhysicists(new StrategyOptions
        {
            PageSize = PageSize,
            PageNumber = PageNumber,
            KeyWord = SearchString

        });
        Physicists = results.ToList();
        if (Physicists.Count > 0)
        {
            LastId = Physicists.Last().Id;
        }
        
        return Page();
    }
}