using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Models;

namespace ORM_MINI_PROJECT.Services.Interfaces
{
    public interface IProductService
    {
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        List<Product> GetProducts();
        List<Product> SearchProducts(string name);
        Product GetProductById(int productId);
        void AddProduct(ProductDTO productDTO);
        void UpdateProduct(ProductDTO productDTO);
    }
}
