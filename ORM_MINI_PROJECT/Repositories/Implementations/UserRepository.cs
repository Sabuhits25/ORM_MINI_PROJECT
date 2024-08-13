using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repositories.Interfaces;
using ORM_MINI_PROJECT.Repository.Generic;

namespace ORM_PROJECT.Repositories.Implementations
{
    public class UserRepository : Repository<User>,IUserRepository
    {
    }
}
