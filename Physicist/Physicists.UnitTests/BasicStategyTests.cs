using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Physicists.Data;
using Physicists.Domain.Strategies;
using Physicists.Models;

namespace Physicists.UnitTests;

public class BasicStategyTests
{
    private BasicPaginationStrategy _sut;
    private ApplicationDbContext _context;

    public BasicStategyTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        builder.UseInMemoryDatabase("TestDb");
        var options = builder.Options;
        _context = new ApplicationDbContext(options);
        _context.Database.EnsureCreated();
        _context.Physicists.AddRange(new List<Physicist>()
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
        _context.SaveChanges();
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        _sut = new BasicPaginationStrategy(_context);
    }

    [Test]
    [TestCase(1, 3, "Albert Einstein")]
    [TestCase(2, 2, "Marie Curie")]
    public async Task TestFirstNameIsCorrect(int page, int pageSize, string name)
    {
        // Arrange
        var options = new StrategyOptions
        {
            PageNumber = page,
            PageSize = pageSize,
        };
        // Act
        var results = await _sut.GetPhysicists(options);
        // Assert
        results.First().Name.Should().Be(name);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}