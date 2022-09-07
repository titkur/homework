using System.ComponentModel.DataAnnotations;
using TradingPlaces.Resources;

namespace TradingPlaces.Models.Dtos
{
    public class StrategyDetailsDto
    {
        [Required]
        [MinLength(3)]
        [MaxLength(5)]
        [RegularExpression(@"^[A-Z]+$", ErrorMessage = "Only capital letters are allowed.")]
        public string Ticker { get; set; }
        [Required]
        public BuySell Instruction { get; set; }
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal PriceMovement { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}