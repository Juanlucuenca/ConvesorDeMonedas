namespace ConvesorDeMonedas.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public int AmountOfConvertions { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
