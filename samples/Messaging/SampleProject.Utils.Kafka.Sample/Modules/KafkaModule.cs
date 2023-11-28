using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.Utils.Messaging.Kafka.Extensions;
using SampleProject.Utils.DependencyInjection.Abstractions;
using SampleProject.Utils.Kafka.Sample.Models;
using SampleProject.Utils.Messaging.Kafka.Serializers.Extensions;

namespace SampleProject.Utils.Kafka.Sample.Modules;

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