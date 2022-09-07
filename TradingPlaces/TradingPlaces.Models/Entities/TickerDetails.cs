using System.ComponentModel.DataAnnotations;

namespace TradingPlaces.Models.Entities
{
    public class TickerDetails
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
