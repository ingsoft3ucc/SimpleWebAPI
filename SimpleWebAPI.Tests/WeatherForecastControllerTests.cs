using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using SimpleWebAPI.Controllers;
using Xunit;

namespace SimpleWebAPI.Tests
{
    public class WeatherForecastControllerTests
    {
        [Fact]
        public void Get_ReturnsFiveWeatherForecasts()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public void Get_ReturnsWeatherForecastsWithDifferentDates()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get();
            var dates = result.Select(w => w.Date).ToList();

            // Assert
            Assert.Equal(dates.Distinct().Count(), dates.Count);
        }

        [Fact]
        public void Get_ReturnsWeatherForecastsWithNonEmptySummary()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.All(result, forecast => Assert.False(string.IsNullOrEmpty(forecast.Summary)));
        }

        [Fact]
        public void Get_ReturnsWeatherForecastsWithValidTemperatureRange()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.Get();

            // Assert
            Assert.All(result, forecast => Assert.InRange(forecast.TemperatureC, -20, 55));
        }
    }
}
