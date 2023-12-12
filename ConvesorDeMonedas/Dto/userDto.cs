using ConvesorDeMonedas.Models;

namespace ConvesorDeMonedas.Dto
{
    public class userDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Mail { get; set; }
        public Role Role { get; set; }
        public int ConvertionsCount { get; set; }
        public int SubscriptionId { get; set; }
    }
}
