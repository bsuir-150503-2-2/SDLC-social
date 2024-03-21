using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using razam.Models;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ApplicationDbContext _context;

    public MessageController(IMessageService messageService, ApplicationDbContext context)
    {
        _messageService = messageService;
        _context = context;
    }

    [HttpPost("send")]
    public async Task<ActionResult<Message>> SendMessageAsync([FromBody] MessageSendModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var message = await _messageService.SendMessageAsync(senderId, model.ReceiverId, model.Content);
        return Ok(message);
    }


    [HttpGet("chats")]
    public async Task<ActionResult<List<Chat>>> GetUserChatsAsync()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var chats = await _context.Chats
            .Where(c => c.User1Id == userId || c.User2Id == userId)
            .Include(c => c.Messages)
            .ToListAsync();

        return Ok(chats);
    }

    [HttpGet("chat/{chatId}")]
    public async Task<ActionResult<List<Message>>> GetChatMessagesAsync(int chatId)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var chat = await _context.Chats.FindAsync(chatId);

        if (chat == null || (chat.User1Id != userId && chat.User2Id != userId))
            return NotFound();

        var messages = await _context.Messages
            .Where(m => m.ChatId == chatId)
            .ToListAsync();

        return Ok(messages);
    }

    [HttpPost("read/{messageId}")]
    public async Task<ActionResult> MarkAsReadAsync(int messageId)
    {
        var message = await _context.Messages.FindAsync(messageId);
        if (message == null)
            return NotFound();

        message.IsRead = true;
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Message marked as read." });
    }
}
