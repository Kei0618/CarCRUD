using System.ComponentModel.DataAnnotations;

namespace Car.Models
{
    public class Cars
    {
        public int Id { get; set; }
        public string Brand { get; set; } = "";

        public string Model { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        

        public int Price { get; set; }

    }
}
