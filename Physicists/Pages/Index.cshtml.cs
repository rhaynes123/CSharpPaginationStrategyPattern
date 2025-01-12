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
    [BindProperty(SupportsGet = true)] public string LastId { get; set; } = string.Empty;

    public IndexModel(ILogger<IndexModel> logger
    , ApplicationDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    private IPaginationStrategy GetStrategy()
    {
        // For this example a method like this is sufficient but if more strategies get added consider a switch or Factory method solution.
        return  string.IsNullOrWhiteSpace(LastId) ? new BasicPaginationStrategy(_db) : new KeysetPaginationStrategy(_db);
    }

    /*
     * NOTE: Encoded and decoded while better than a raw id is also a subpar solution and is merely for demonstration purposes.
     */
    private string EncodeId(int id)
    {
        var bytes = BitConverter.GetBytes(id);
        return Convert.ToBase64String(bytes);
    }

    private int DecodeId(string id)
    {
        if(string.IsNullOrWhiteSpace(LastId)) return 0;
        var bytes = Convert.FromBase64String(id);
        return BitConverter.ToInt32(bytes, 0);
    }

    public async Task<IActionResult> OnGet()
    {
        int totalCount = string.IsNullOrWhiteSpace(SearchString) 
            ? await _db.Physicists.CountAsync() 
            : await _db.Physicists
                .Where(phy => phy.Name.Contains(SearchString))
                .CountAsync();
        // Makes sure the number of pages are limited
        Total = (int)Math.Ceiling(totalCount / (double)PageSize);
        var strategy = GetStrategy();
        var results = await strategy.GetPhysicists(new StrategyOptions
        {
            PageSize = PageSize,
            PageNumber = PageNumber,
            KeyWord = SearchString,
            LastId = DecodeId(LastId)

        });
        Physicists = results.ToList();
        if (Physicists.Count > 0)
        {
            LastId = EncodeId(Physicists.Last().Id);
        }
        
        return Page();
    }
}