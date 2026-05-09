using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using AWS.SNS.Samples.Model.Dtos;
using System.Text.Json;

namespace AWS.SNS.Samples.Service;

public class SNSService: ISNSService
{
    private readonly string topicArn = "arn:aws:sns:us-east-1:552242636878:New-User";
    private readonly IAmazonSimpleNotificationService _client;

    public SNSService(IAmazonSimpleNotificationService client)
    {
        _client = client;
    }

    public async Task PublishUserAsync(UserDto user)
    {
        var request = new PublishRequest
        {   
            Subject = "Successfully published user",
            TopicArn = topicArn,
            Message = JsonSerializer.Serialize(user),
        };

        var response = await _client.PublishAsync(request);

        Console.WriteLine($"Successfully published user ID: {response.MessageId}");
    }
}
