using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.ServiceBus.Services;

public class ServiceBusService : IServiceBusService
{
    #region Private Fields

    private readonly IValidator validator;
    private readonly IConfiguration configuration;

    #endregion

    #region Public Constructors

    public ServiceBusService(
        IValidator validator,
        IConfiguration configuration)
    {
        this.validator = validator;
        this.configuration = configuration;
    }

    #endregion

    #region Public Methods

    public async Task SendMessageAsync(
        string queueName,
        string messageBody,
        bool retryAllowed)
    {
        string connectionString = this.configuration.GetValue<string>("ServiceBus:ConnectionString");

        this.validator
            .CheckStringIsNotNullOrWhiteSpace(queueName, nameof(queueName))
            .CheckStringIsNotNullOrWhiteSpace(messageBody, nameof(messageBody))
            .CheckStringIsNotNullOrWhiteSpace(connectionString, nameof(connectionString));

        RetryPolicy retryPolicy = null;

        if (retryAllowed)
        {
            int maxRetryCount = this.configuration.GetValue<int>("ServiceBus:MaxRetryCount");
            int minBackoffSeconds = this.configuration.GetValue<int>("ServiceBus:MinimumBackoffSeconds");
            int maxBackoffSeconds = this.configuration.GetValue<int>("ServiceBus:MaximumBackoffSeconds");
            int deltaBackoffSeconds = this.configuration.GetValue<int>("ServiceBus:DeltaBackoffSeconds");

            retryPolicy = new RetryExponential(
                maxRetryCount: maxRetryCount,
                minimumBackoff: TimeSpan.FromSeconds(minBackoffSeconds),
                maximumBackoff: TimeSpan.FromSeconds(maxBackoffSeconds),
                deltaBackoff: TimeSpan.FromSeconds(deltaBackoffSeconds)
            );
        }

        IQueueClient client = new QueueClient(connectionString, queueName, retryPolicy: retryPolicy);

        Message message = new Message(Encoding.UTF8.GetBytes(messageBody))
        {
            MessageId = Guid.NewGuid().ToString(),
            ContentType = "application/json"
        };

        await client.SendAsync(message);
    }

    #endregion

}