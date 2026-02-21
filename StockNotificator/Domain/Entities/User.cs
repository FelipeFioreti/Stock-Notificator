using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockNotificator.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email invalid.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(255, ErrorMessage = "Name cannot exceed 255 characteres.")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; } = string.Empty;

        public ICollection<UserStock> UserStocks { get; set; } = [];
    }
}
