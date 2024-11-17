using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TaskManagement.Core.DTO;
using TaskManagement.Core.Interfaces;
using TaskManagement.Core.Interfaces.Services;

namespace TaskManagement.AzureFunctions.TaskProcessor;

public class TaskProcessor
{

    #region Private Fields

    private readonly ILogger<TaskProcessor> logger;
    private readonly IUnitOfWork unitOfWork;
    private readonly ITaskService taskService;
    private readonly IValidator validator;

    #endregion

    #region Public Constructors

    public TaskProcessor(
        ILogger<TaskProcessor> logger,
        IUnitOfWork unitOfWork,
        ITaskService taskService,
        IValidator validator)
    {
        this.logger = logger;
        this.unitOfWork = unitOfWork;
        this.taskService = taskService;
        this.validator = validator;
    }

    #endregion

    #region Public Methods

    [Function(nameof(TaskProcessor))]
    public async Task Run(
        [ServiceBusTrigger(queueName: "taskmanagement", Connection = "ServiceBusConnectionString")]
        ServiceBusReceivedMessage message,
        ServiceBusMessageActions messageActions)
    {
        logger.LogDebug($"Function {nameof(TaskProcessor)} started processing at {DateTime.UtcNow}.");
        logger.LogDebug($"MessageId: {message.MessageId}. Message: {message.Body}");

        TaskProcessingDto taskProcessingDto = JsonConvert.DeserializeObject<TaskProcessingDto>(message.Body.ToString());

        try
        {
            await this.taskService.ProcessTask(taskProcessingDto);

            await messageActions.CompleteMessageAsync(message);

            logger.LogDebug($"Processed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogDebug($"Exception in time of processing. Exception details: {ex.Message}");

            await messageActions.AbandonMessageAsync(message);
        }

        logger.LogDebug($"Function {nameof(TaskProcessor)} finished processing at {DateTime.UtcNow}.");

        return;
    }

    #endregion

    #region Private Methods


    #endregion

}