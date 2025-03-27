using Moq;
using MigApp.Helpers;
using MigApp.Interfaces;

namespace MigApp.Tests
{
    [TestClass]
    public sealed class ConnectionStringAssemblerTests
    {
        [TestMethod]
        public void BuildConnectionString_ValidSettings_ReturnConnectionString()
        {
            // Arrange
            var mockSettings = new Mock<ISettings>();
            mockSettings.Setup(s => s.pgServer).Returns("localhost");
            mockSettings.Setup(s => s.pgPort).Returns("1234");
            mockSettings.Setup(s => s.pgDatabase).Returns("TestDB");
            mockSettings.Setup(s => s.pgUser).Returns("testuser");
            mockSettings.Setup(s => s.pgPassword).Returns("password");

            var assembler = new ConnectionStringAssembler();

            // Act
            string connectionString = assembler.BuildConnectionString(mockSettings.Object);

            // Assert
            var expected = "Host=localhost;Port=1234;Database=TestDB;Username=testuser;Password=password";
            Assert.AreEqual(expected, connectionString);
        }
    }
}
