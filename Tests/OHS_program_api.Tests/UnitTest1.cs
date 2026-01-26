using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using OHS_program_api.API.Services;

namespace OHS_program_api.Tests;

public class CacheServiceTests
{
    [Fact]
    public void CacheService_SetAndGet_WorksCorrectly()
    {
        // Arrange
        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = new Mock<ILogger<MemoryCacheService>>();
        var service = new MemoryCacheService(cache, logger.Object);
        
        var testKey = "test-key";
        var testValue = "test-value";

        // Act
        service.Set(testKey, testValue);
        var result = service.Get<string>(testKey);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(testValue);
    }

    [Fact]
    public void CacheService_Remove_DeletesData()
    {
        // Arrange
        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = new Mock<ILogger<MemoryCacheService>>();
        var service = new MemoryCacheService(cache, logger.Object);
        
        var testKey = "test-key";
        var testValue = "test-value";
        service.Set(testKey, testValue);

        // Act
        service.Remove(testKey);
        var result = service.Get<string>(testKey);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void CacheService_Expiration_RemovesExpiredData()
    {
        // Arrange
        var cache = new MemoryCache(new MemoryCacheOptions());
        var logger = new Mock<ILogger<MemoryCacheService>>();
        var service = new MemoryCacheService(cache, logger.Object);
        
        var testKey = "test-key";
        var testValue = "test-value";
        var expiration = TimeSpan.FromMilliseconds(100);

        // Act
        service.Set(testKey, testValue, expiration);
        var resultBefore = service.Get<string>(testKey);
        
        System.Threading.Thread.Sleep(150); // Wait for expiration
        var resultAfter = service.Get<string>(testKey);

        // Assert
        resultBefore.Should().NotBeNull();
        resultAfter.Should().BeNull();
    }
}

