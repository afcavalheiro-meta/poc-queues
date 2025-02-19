using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace WebApi.Services;

public class ServiceBusSender
{
    private readonly ServiceBusClient _client;
    private readonly Azure.Messaging.ServiceBus.ServiceBusSender _clientSender;
    private const string QUEUE_NAME = "afc-poc";

    public ServiceBusSender(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
        _client = new ServiceBusClient(connectionString);
        _clientSender = _client.CreateSender(QUEUE_NAME);
    }

    public async Task<long> SendMessage(object payload)
    {
        string messagePayload = JsonSerializer.Serialize(payload);
        var message = new ServiceBusMessage(messagePayload);

        var dateToEnqueue = DateTime.UtcNow.AddSeconds(20);
        var data = await _clientSender.ScheduleMessageAsync(message, dateToEnqueue);

        Console.WriteLine($"Message  return {data} enqueue at {dateToEnqueue}");
        return data;
    }
}