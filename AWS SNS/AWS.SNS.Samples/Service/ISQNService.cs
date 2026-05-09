using AWS.SNS.Samples.Model.Dtos;

namespace AWS.SNS.Samples.Service
{
    public interface ISNSService
    {
        Task PublishUserAsync(UserDto user);
    }
}
