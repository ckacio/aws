namespace AWS.SNS.Samples.Service;

using AWS.SNS.Samples.Model.Dtos;
using Microsoft.Extensions.Logging;

public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly ISNSService _snsService;

    public UserService(ILogger<UserService> logger, ISNSService snsService)
    {
        _logger = logger;
        _snsService = snsService;
    }

    public async Task RegisterUserAsync(UserDto user)
    {
       await _snsService.PublishUserAsync(user);
           
    }
}

