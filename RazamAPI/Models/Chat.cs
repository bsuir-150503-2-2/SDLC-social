using System.ComponentModel.DataAnnotations;

namespace razam.Models
{
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }

        // Навигационное свойство для связи с сообщениями
        public ICollection<Message> Messages { get; set; }
    }
}