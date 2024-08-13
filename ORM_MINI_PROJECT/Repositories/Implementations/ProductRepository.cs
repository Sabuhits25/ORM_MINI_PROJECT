using ORM_MINI_PROJECT.IRepositories.Interfaces;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Repository.Generic;

namespace ORM_MINI_PROJECT.IRepositories.Implementations
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
       
    }

}
