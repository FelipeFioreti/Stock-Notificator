using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockNotificator.Domain.Entities
{
    public class UserStock : BaseEntity
    {
        [Required(ErrorMessage = "UserId is required.")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "StockId is required.")]
        [ForeignKey("Stock")]
        public Guid StockId { get; set; }
        [Required(ErrorMessage = "Value is required.")]
        public decimal ReferencePrice { get; set; }

        public User User { get; set; } = null!;
        public Stock Stock { get; set; } = null!;
        public ICollection<AlertCondition> AlertConditions { get; set; } = [];
    }
}
