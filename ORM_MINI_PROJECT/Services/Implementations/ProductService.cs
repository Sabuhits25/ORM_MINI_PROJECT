using ORM_MINI_PROJECT.DTO_s;
using ORM_MINI_PROJECT.Exceptions;
using ORM_MINI_PROJECT.IRepositories.Implementations;
using ORM_MINI_PROJECT.IRepositories.Interfaces;
using ORM_MINI_PROJECT.Models;
using ORM_MINI_PROJECT.Services.Interfaces;

namespace ORM_MINI_PROJECT.Services.Implementation
{
    public class ProductService 
    {

        private readonly IProductRepository _repository;

        public ProductService()
        {
            _repository = new ProductRepository();
        }

        public void AddProduct(Product product)
        {
            
            if (string.IsNullOrWhiteSpace(product.Name) || product.Price < 0)
                throw new InvalidProductException("Məhsulun adı boş və ya qiyməti mənfi ola bilməz.");

            _repository.CreateAsync(product);
        }

        public async void UpdateProduct(Product product)
        {
            var existingProduct =await _repository.GetSingleAsync(p => p.Id == product.Id);

            if (existingProduct == null)
                throw new NotFoundException("Yeniləmək istədiyiniz məhsul tapılmadı.");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _repository.GetSingleAsync(p => p.Id == productId);
            if (product == null)
                throw new NotFoundException("Silinmək istənilən məhsul tapılmadı.");

            _repository.Delete(product);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<Product>> SearchProducts(string name)
        {
            return (await _repository.GetAllAsync()).Where(p => p.Name.Contains(name)).ToList();
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product =await _repository.GetSingleAsync(p => p.Id == productId);
            if (product == null)
                throw new NotFoundException("Məhsul tapılmadı.");

            return product;
        }

        public async Task AddProduct(ProductDTO productDTO)
        {
            if (string.IsNullOrWhiteSpace(productDTO.Name) || productDTO.Price < 0)
                throw new InvalidProductException("Məhsulun adı boş və ya qiyməti mənfi ola bilməz.");

            var product = new Product
            {
               
                Name = productDTO.Name,
                Price = productDTO.Price,
                Stock = productDTO.Stock,
                Description = productDTO.Description,
            };

            await _repository.CreateAsync(product);
            await _repository.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductDTO productDTO)
        {
            var existingProduct =await _repository.GetSingleAsync(p => p.Id == productDTO.Id);
            if (existingProduct == null)
                throw new NotFoundException("Yeniləmək istədiyiniz məhsul tapılmadı.");

            existingProduct.Name = productDTO.Name;
            existingProduct.Price = productDTO.Price;
        }
    }
}
