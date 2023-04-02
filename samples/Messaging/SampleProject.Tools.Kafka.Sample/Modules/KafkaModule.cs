using Microsoft.Extensions.Configuration;
using SampleProject.Tools.Kafka.Sample.Models;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Tools.Messaging.Kafka.Extensions;
using SampleProject.Tools.DependencyInjection.Abstractions;
using SampleProject.Tools.Messaging.Kafka.Serializers.Extensions;

namespace SampleProject.Tools.Kafka.Sample.Modules;

public class KafkaModule : IModule
{
    private readonly IConfiguration _configuration;

    public KafkaModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void Load(IServiceCollection services)
    {
        // Producer
        services
            .AddKafkaProducer<CreateUser>(options => options
            .FromConfiguration(_configuration)
            .ConfigureTopicCreation(_configuration)
            .WithTopic("users.profile.commands.v1")
            .WithJson());
    }
}