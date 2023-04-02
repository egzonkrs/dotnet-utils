using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using SampleProject.Tools.Kafka.Sample.Models;
using SampleProject.Tools.Messaging.Abstractions;
using SampleProject.Tools.Messaging.Kafka.Abstractions;

namespace SampleProject.Tools.Kafka.Sample.Handlers;

public class CreateUserHandler : IMessageHandler<CreateUser>, IMessageErrorHandler<CreateUser>
{
    private readonly IKafkaProducer<CreateUser> _kafkaProducer;
    private readonly ILogger<CreateUserHandler> _logger;

    public CreateUserHandler(IKafkaProducer<CreateUser> kafkaProducer, ILogger<CreateUserHandler> logger)
    {
        _kafkaProducer = kafkaProducer;
        _logger = logger;
    }

    public async Task HandleAsync(IMessageContext<CreateUser> messageContext, CancellationToken cancellationToken)
    {
        try
        {
            // Validation
            ValidateMessage(messageContext.Message.Value);

            // Process 
            _logger.LogInformation("Consumed command in service [value: '{@serializedValue}']",
                messageContext.Message.Value);

            foreach (var (key, value) in messageContext.Message.Headers)
            {
                _logger.LogInformation("Header: {key}\tValue: {value}", key, value);
            }

            var personCreated = new CreateUser
            {
                UserId = messageContext.Message.Value.UserId,
                Name = messageContext.Message.Value.Name,
                Age = messageContext.Message.Value.Age,
                JoinedAt = messageContext.Message.Value.JoinedAt,
            };

            var personCreatedMessage = new KafkaMessage<CreateUser>(nameof(CreateUser),
                messageContext.Message.Headers,
                personCreated.UserId.ToString(), 
                personCreated);

            // Publish Event
            await _kafkaProducer.ProduceAsync(personCreatedMessage, cancellationToken);
        }
        catch (ValidationException ex)
        {
            // Add Handle for Poison Message
            _logger.LogError(ex, ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }

    private static void ValidateMessage(CreateUser message)
    {
        var isUserIdValid = message.UserId != Guid.Empty;
        
        if (isUserIdValid) throw new ValidationException($"Incorrect {nameof(message.UserId)}: {message.UserId}.");
    }

    public Task HandleAsync(IErrorMessageContext<CreateUser> errorMessageContext, CancellationToken cancellationToken)
    {
        _logger.LogError(errorMessageContext.InnerException, "Message {@message} cannot be processed", 
            errorMessageContext.Context.Message);

        return Task.CompletedTask;
    }
}