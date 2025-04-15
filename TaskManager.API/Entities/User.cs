namespace TaskManager.API.Entities
{
    public class User
    {
        public Guid id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
