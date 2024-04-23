namespace SolisDensCuraBETA.services
{
    public interface IChatService
    {

        Task SendMessageAsync(string senderId, string receiverId, string message);

    }
}
