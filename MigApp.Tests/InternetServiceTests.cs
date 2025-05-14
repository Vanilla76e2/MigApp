using MigApp.Core.Services.DnsResolver;
using MigApp.Services;
using Moq;
using System.Net;

namespace MigApp.Tests
{
    [TestClass]
    public class InternetServiceTests
    {
        [TestMethod]
        [Description("Проверяет, что метод InternetCheckerAsync возвращает true при наличии интернет-соединения.")]
        public async Task InternetCheckerAsync_HasInternet_ReturnsTrue()
        {
            // Организуем
            var mockDnsResolver = new Mock<IDnsResolver>();
            mockDnsResolver.Setup(x => x.GetHostEntryAsync(It.IsAny<string>())).ReturnsAsync(new IPHostEntry()); // Симуляция успешного запроса к DNS

            var internetService = new InternetService(mockDnsResolver.Object);

            // Выполняем
            bool hasInternet = await internetService.InternetCheckerAsync();

            // Проверяем
            Assert.IsTrue(hasInternet);
        }

        [TestMethod]
        [Description("Проверяет, что метод InternetCheckerAsync возвращает false при отсутствии интернет-соединения.")]
        public async Task InternetCheckerAsync_NoInternet_ReturnsFalse()
        {
            // Организуем
            var mockDnsResolver = new Mock<IDnsResolver>();
            mockDnsResolver.Setup(x => x.GetHostEntryAsync(It.IsAny<string>()))
                .ThrowsAsync(new WebException()); // Simulate DNS resolution failure

            var internetService = new InternetService(mockDnsResolver.Object);

            // Выполняем
            bool hasInternet = await internetService.InternetCheckerAsync();

            // Проверяем
            Assert.IsFalse(hasInternet);
        }
    }
}
