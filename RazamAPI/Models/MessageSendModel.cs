using System.ComponentModel.DataAnnotations;

public class MessageSendModel
{
    [Required(ErrorMessage = "The content field is required.")]
    public string Content { get; set; }

    [Required(ErrorMessage = "The receiverId field is required.")]
    public string ReceiverId { get; set; }
}