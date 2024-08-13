using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;

namespace ORM_MINI_PROJECT.Services.Implementation
{
    public class ProductService : IProductService
    {
        private List<Product> _products = new List<Product>();

        public void AddProduct(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0)
                throw new InvalidProductException("Məhsulun adı boş və ya qiyməti mənfi ola bilməz.");

            _products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct == null)
                throw new NotFoundException("Yeniləmək istədiyiniz məhsul tapılmadı.");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }

        public void DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                throw new NotFoundException("Silinmək istənilən məhsul tapılmadı.");

            _products.Remove(product);
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public List<Product> SearchProducts(string name)
        {
            return _products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public Product GetProductById(int productId)
        {
            var product = _products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                throw new NotFoundException("Məhsul tapılmadı.");

            return product;
        }
    }
}
