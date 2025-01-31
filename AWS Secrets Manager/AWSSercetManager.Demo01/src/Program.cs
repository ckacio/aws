using Amazon.Extensions.NETCore.Setup;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Config AWS
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSecretsManager>(new AWSOptions
{
    Region = Amazon.RegionEndpoint.USEast1,
    Credentials = new Amazon.Runtime.BasicAWSCredentials("ID da chave de acesso", "Secret access key")
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.MapGet("/secret", async (IAmazonSecretsManager secrets) =>
{
    var request = new GetSecretValueRequest()
    {
        SecretId = "dev/AWSSercetManager.Demo01/ConnectionStringDBContabil"
    };

    var data = await secrets.GetSecretValueAsync(request);

    return Results.Ok(data.SecretString);
});

app.Run();

