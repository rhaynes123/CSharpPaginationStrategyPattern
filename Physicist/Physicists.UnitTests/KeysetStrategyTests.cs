using Physicists.Domain.Strategies;
using Physicists.Data;
using FluentAssertions;
namespace Physicists.UnitTests;

[TestFixture]
public class KeysetStrategyTests
{
    private KeysetPaginationStrategy _sut;
    private ApplicationDbContext _context;

    public KeysetStrategyTests()
    {
        _context = TestDbContext.Create();
    }
    
    [OneTimeSetUp]
    public void Setup()
    {
        _sut = new KeysetPaginationStrategy(_context);
    }

    [Test]
    [TestCase(2, 2, "Marie Curie")]
    [TestCase(3, 2, "Nikola Tesla")]
    public async Task TestFirstNameIsCorrect(int lastId, int pageSize, string name)
    {
        // Arrange
        var options = new StrategyOptions
        {
            LastId = lastId,
            PageSize = pageSize,
        };
        // Act
        var results = await _sut.GetPhysicists(options);
        // Assert
        results.First().Name.Should().Be(name);
    }

    [Test]
    public async Task TestStrategyThrowsWithoutLastId()
    {
        // Arrange
        var options = new StrategyOptions
        {
        };
        // Act
        var act  = () =>  _sut.GetPhysicists(options);
        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}