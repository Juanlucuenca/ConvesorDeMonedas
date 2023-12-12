namespace ConvesorDeMonedas.Models
{
    public enum Role
    {
        User,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; }
        public int ConvertionsCount { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
        public ICollection<Conversion>? Conversions { get; set; }
    }
}
