namespace razam.Models
{
    public class Like
    {
        public int Id { get; set; }

        public string LikerId { get; set; } // Id пользователя, который поставил лайк

        public string LikedId { get; set; } // Id профиля, который получил лайк
        public DateTime LikedAt { get; set; } // Добавлено поле для отслеживания времени лайка
    }
}