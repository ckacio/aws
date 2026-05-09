using AWS.SNS.Samples.Service;
using Amazon.SimpleNotificationService; // Ensure this namespace is included  

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.  
builder.Services.AddScoped<IAmazonSimpleNotificationService, AmazonSimpleNotificationServiceClient>(); 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISNSService, SNSService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle  
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
