using StockNotificator.Application.Interfaces.Repositories;
using StockNotificator.Domain.Entities;
using StockNotificator.Infraestructure.Context;

namespace StockNotificator.Infraestructure.Repositories
{
    public class UserRepository(ApplicationDbContext context) : BaseRepository<User>(context), IUserRepository
    {

    }
}
