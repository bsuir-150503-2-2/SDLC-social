namespace razam.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Bio { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}