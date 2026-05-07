using AWSLambdaStardedFromSQS.Model.Dtos;
using Microsoft.Extensions.Logging;

namespace AWSLambdaStardedFromSQS.Service
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public async Task RegisterUserAsync(UserDto user) 
        {
            await Task.Run(() =>
            {
                _logger.LogInformation($"User successfully registered Name:{user.Name} Email:{user.Email}");
            });
        }
    }
}
