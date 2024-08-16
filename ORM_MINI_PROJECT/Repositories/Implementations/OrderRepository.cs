using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.IRepositories.Interfaces;
using ORM_MINI_PROJECT.Repository.Generic;

namespace ORM_MINI_PROJECT.IRepositories.Implementation;


public class OrderRepository : Repository<Order>, IOrderRepository
{

}

