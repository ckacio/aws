using AWS.SNS.Samples.Model.Dtos;

namespace AWS.SNS.Samples.Service;

public interface IUserService
{
    Task RegisterUserAsync(UserDto user);
}

