using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using TaskManagement.Core.Interfaces;
using TaskManagement.ServiceBus.Services;

namespace TaskManagement.Tests;

[TestFixture]
public class ServiceBusTests
{
    private Mock<IValidator> _mockValidator;
    private Mock<IConfiguration> _mockConfiguration;
    private ServiceBusService _serviceBusService;

    [SetUp]
    public void SetUp()
    {
        _mockValidator = new Mock<IValidator>();
        _mockConfiguration = new Mock<IConfiguration>();
        _serviceBusService = new ServiceBusService(_mockValidator.Object, _mockConfiguration.Object);
    }

    [Test]
    public async Task SendMessageAsync_ShouldCallValidatorWithCorrectParameters()
    {
        // Arrange
        string queueName = "testQueue";
        string messageBody = "testMessage";
        string connectionString = "Endpoint=sb://test.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=testKey";

        _mockConfiguration
            .Setup(config => config.GetValue<string>("ServiceBus:ConnectionString"))
            .Returns(connectionString);

        // Act
        await _serviceBusService.SendMessageAsync(queueName, messageBody);

        // Assert
        _mockValidator.Verify(v => v.CheckStringIsNotNullOrWhiteSpace(queueName, nameof(queueName)), Times.Once);
        _mockValidator.Verify(v => v.CheckStringIsNotNullOrWhiteSpace(messageBody, nameof(messageBody)), Times.Once);
        _mockValidator.Verify(v => v.CheckStringIsNotNullOrWhiteSpace(connectionString, nameof(connectionString)), Times.Once);
    }

    [Test]
    public void SendMessageAsync_ShouldThrowException_WhenQueueNameIsNullOrEmpty()
    {
        // Arrange
        string queueName = null;
        string messageBody = "testMessage";
        string connectionString = "validConnectionString";

        _mockConfiguration
            .Setup(config => config.GetValue<string>("ServiceBus:ConnectionString"))
            .Returns(connectionString);

        _mockValidator
            .Setup(v => v.CheckStringIsNotNullOrWhiteSpace(queueName, nameof(queueName)))
            .Throws(new ArgumentException("Queue name cannot be null or empty"));

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _serviceBusService.SendMessageAsync(queueName, messageBody));
    }

    [Test]
    public void SendMessageAsync_ShouldThrowException_WhenMessageBodyIsNullOrEmpty()
    {
        // Arrange
        string queueName = "testQueue";
        string messageBody = null;
        string connectionString = "validConnectionString";

        _mockConfiguration
            .Setup(config => config.GetValue<string>("ServiceBus:ConnectionString"))
            .Returns(connectionString);

        _mockValidator
            .Setup(v => v.CheckStringIsNotNullOrWhiteSpace(messageBody, nameof(messageBody)))
            .Throws(new ArgumentException("Message body cannot be null or empty"));

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _serviceBusService.SendMessageAsync(queueName, messageBody));
    }

    [Test]
    public void SendMessageAsync_ShouldThrowException_WhenConnectionStringIsNullOrEmpty()
    {
        // Arrange
        string queueName = "testQueue";
        string messageBody = "testMessage";
        string connectionString = null;

        _mockConfiguration
            .Setup(config => config.GetValue<string>("ServiceBus:ConnectionString"))
            .Returns(connectionString);

        _mockValidator
            .Setup(v => v.CheckStringIsNotNullOrWhiteSpace(connectionString, nameof(connectionString)))
            .Throws(new ArgumentException("Connection string cannot be null or empty"));

        // Act & Assert
        Assert.ThrowsAsync<ArgumentException>(() => _serviceBusService.SendMessageAsync(queueName, messageBody));
    }
}
