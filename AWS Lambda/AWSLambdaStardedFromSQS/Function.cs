using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using AWSLambdaStardedFromSQS.Config;
using AWSLambdaStardedFromSQS.Model.Dtos;
using AWSLambdaStardedFromSQS.Service;
using AWSLambdaStardedFromSQS.Utility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace AWSLambdaStardedFromSQS;

public class Function
{
    private readonly IServiceProvider _services;
    private List<SQSBatchResponse.BatchItemFailure>? _failures;
    private readonly ILogger<Func<Function>> _logger;
    private readonly IUserService _userService;

    public Function()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings{EnviromentLambda.GetByEnviromentVariable()}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        _services = LambdaConfig.ConfigureServices(configuration);
        _logger = _services.GetRequiredService<ILogger<Func<Function>>>();
        _userService = _services.GetRequiredService<IUserService>();
    }


    public async Task<SQSBatchResponse> FunctionHandler(SQSEvent @event, ILambdaContext context)
    {
        _logger.LogInformation("initial processing of messages coming from SQS");
        _failures = new List<SQSBatchResponse.BatchItemFailure>();
        var queueName = @event.Records.First().EventSourceArn.Split(":").Last();

        foreach(var message in @event.Records)
        {
            try
            {
                UserDto? user = JsonSerializer.Deserialize<UserDto>(message.Body, new JsonSerializerOptions
                {
                     PropertyNameCaseInsensitive = true
                });

                if (user is null)
                {
                    var msg = $"Empty object when deserialized: {message} - {DateTime.UtcNow}";
                    _logger.LogError(msg);
                    throw new Exception(msg);
                }


                await _userService.RegisterUserAsync(user);


            }
            catch (Exception ex)
            {
                _failures.Add(new SQSBatchResponse.BatchItemFailure
                {
                    ItemIdentifier = message.MessageId
                });

                _logger.LogError($"Error processing FunctionHandler Queue:{queueName} MessageId: {message.MessageId} - {DateTime.UtcNow}",ex);
            }
        }

        return new SQSBatchResponse(_failures);
    }
}
