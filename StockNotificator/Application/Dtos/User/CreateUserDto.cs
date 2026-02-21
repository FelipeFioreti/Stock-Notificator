using System.ComponentModel.DataAnnotations;

namespace StockNotificator.Application.Dtos.User
{
    public record CreateUserDto
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
