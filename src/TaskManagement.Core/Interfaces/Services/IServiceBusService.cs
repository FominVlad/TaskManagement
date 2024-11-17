namespace TaskManagement.Core.Interfaces.Services;

public interface IServiceBusService
{
    Task SendMessageAsync(string queueName, string messageBody, bool retryAllowed);
}