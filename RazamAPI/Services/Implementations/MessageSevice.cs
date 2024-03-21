using Microsoft.EntityFrameworkCore;
using razam.Models;

public class MessageService : IMessageService
{
    private readonly ApplicationDbContext _context;

    public MessageService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Message> SendMessageAsync(string senderId, string receiverId, string content)
    {
        var chat = await GetOrCreateChatAsync(senderId, receiverId);

        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            Content = content,
            SentAt = DateTime.UtcNow,
            IsRead = false,
            ChatId = chat.Id
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync();

        return message;
    }

    private async Task<Chat> GetOrCreateChatAsync(string userId1, string userId2)
    {
        var existingChat = await _context.Chats.FirstOrDefaultAsync(c =>
            (c.User1Id == userId1 && c.User2Id == userId2) ||
            (c.User1Id == userId2 && c.User2Id == userId1));

        if (existingChat != null)
            return existingChat;

        var newChat = new Chat
        {
            User1Id = userId1,
            User2Id = userId2
        };

        _context.Chats.Add(newChat);
        await _context.SaveChangesAsync();

        return newChat;
    }

    public async Task<List<Message>> GetMessagesAsync(string userId)
    {
        var messages = await _context.Messages
            .Where(m => m.ReceiverId == userId)
            .OrderByDescending(m => m.SentAt)
            .ToListAsync();

        return messages;
    }

    public async Task<List<Message>> GetAllMessagesAsync()
    {
        var messages = await _context.Messages.ToListAsync();
        return messages;
    }

    public async Task<bool> MarkAsReadAsync(int messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null)
            return false;

        message.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }
}