using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Dto
{
    public class UserProfileDto
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Role { get; set; }
        public int Convertions { get; set; }
        public string Subscription { get; set; }
    }
}
