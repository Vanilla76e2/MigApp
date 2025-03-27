using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using MigApp.Services;
using MigApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MigApp.Tests.Services
{
    public class AuthenticationServiceTests
    {
        [Fact]
        public async Task TestConnectionAsync_ShouldReturnTrue_WhenConnectionIsSuccessful()
        {
            // Arrange
            var mockContext = new Mock<MigDataBaseContext>();
            var mockDatabase = new Mock<DatabaseFacade>(mockContext.Object);
            mockDatabase.Setup(db => db.CanConnectAsync(default)).ReturnsAsync(true);
            mockContext.Setup(db => db.Database).Returns(mockDatabase.Object);
            var service = new DatabaseService(mockContext.Object);

            // Act
            var result = await service.TestConnectionAsync();

            // Assert
            Xunit.Assert.True(result);
        }

        [Fact]
        public async Task TestConnectionAsync_ShouldReturnFalse_WhenConnectionTimesOut()
        {
            // Arrange
            var mockContext = new Mock<MigDataBaseContext>();
            var mockDatabase = new Mock<DatabaseFacade>(mockContext.Object);
            mockDatabase.Setup(db => db.CanConnectAsync(default)).Returns(Task.Delay(11000).ContinueWith(_ => true));
            mockContext.Setup(db => db.Database).Returns(mockDatabase.Object);
            var service = new DatabaseService(mockContext.Object);

            // Act
            var result = await service.TestConnectionAsync();

            // Assert
            Xunit.Assert.False(result);
        }

        [Fact]
        public async Task TestConnectionAsync_ShouldReturnFalse_WhenExceptionIsThrown()
        {
            // Arrange
            var mockContext = new Mock<MigDataBaseContext>();
            var mockDatabase = new Mock<DatabaseFacade>(mockContext.Object);
            mockDatabase.Setup(db => db.CanConnectAsync(default)).ThrowsAsync(new Exception("Test exception"));
            mockContext.Setup(db => db.Database).Returns(mockDatabase.Object);
            var service = new DatabaseService(mockContext.Object);

            // Act
            var result = await service.TestConnectionAsync();

            // Assert
            Xunit.Assert.False(result);
        }
    }
}