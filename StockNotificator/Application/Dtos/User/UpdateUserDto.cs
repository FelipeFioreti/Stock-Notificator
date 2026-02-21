using StockNotificator.Application.Dtos.Base;

namespace StockNotificator.Application.Dtos.User
{
    public record UpdateUserDto : BasicBaseDto
    {
        public string Email = string.Empty;
        public string Name = string.Empty;
        public string PasswordHash = string.Empty;
    }
}
