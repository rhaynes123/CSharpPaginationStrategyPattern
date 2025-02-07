﻿using FluentAssertions;
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
        _context = TestDbContext.Create();
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
    
    
    [Test]
    public async Task TestStrategyThrowsWithNegativePageSize()
    {
        // Arrange
        var options = new StrategyOptions
        {
            PageNumber = -1,
            PageSize = -1,
        };
        // Act
        var act  = () =>  _sut.GetPhysicists(options);
        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
    
    
    [Test]
    public async Task TestStrategyThrowsWithoutOptions()
    {
        // Arrange
        StrategyOptions options = null!;
        // Act
        var act  = () =>  _sut.GetPhysicists(options);
        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}