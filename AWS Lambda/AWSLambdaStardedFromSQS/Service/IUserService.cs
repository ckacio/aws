using AWSLambdaStardedFromSQS.Model.Dtos;

namespace AWSLambdaStardedFromSQS.Service
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserDto user);
    }
}
