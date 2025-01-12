using Physicists.Data;
using Microsoft.EntityFrameworkCore;
using Physicists.Models;
namespace Physicists.UnitTests;

public class TestDbContext
{
    public static ApplicationDbContext Create()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var options = builder.Options;
        var context = new ApplicationDbContext(options);
        context.Database.EnsureCreated();
        context.Physicists.AddRange(new List<Physicist>()
        {
            new Physicist
            {
                Id = 1,
                Name = "Albert Einstein",
                DateOfBirth = new DateTime(1873, 3, 14),
            },
            new Physicist
            {
                Id = 2,
                Name = "Isaac Newton",
                DateOfBirth = new DateTime(1643, 1, 4),
            },
            new Physicist
            {
                Id = 3,
                Name = "Marie Curie",
                DateOfBirth = new DateTime(1867, 11, 7),
            },
            new Physicist
            {
                Id = 4,
                Name = "Nikola Tesla",
                DateOfBirth = new DateTime(1856, 7, 10),
            },
        });
        context.SaveChanges();
        return context;
    }
}