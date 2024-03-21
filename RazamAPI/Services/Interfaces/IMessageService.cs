using razam.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMessageService
{
    Task<Message> SendMessageAsync(string senderId, string receiverId, string content);
    Task<List<Message>> GetMessagesAsync(string userId);
    Task<bool> MarkAsReadAsync(int messageId);
}