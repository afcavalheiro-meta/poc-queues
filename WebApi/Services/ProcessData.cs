using Azure.Messaging.ServiceBus;

namespace WebApi.Services;

public interface IProcessData
{
    Task Process(ServiceBusReceivedMessage myPayload);
}

public class ProcessData : IProcessData
{
    private readonly IConfiguration _configuration;

    public ProcessData(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task Process(ServiceBusReceivedMessage myPayload)
    {
        
        Console.WriteLine($"Process message with body {myPayload.Body} at {DateTime.UtcNow}");
        return Task.CompletedTask;
    }
}