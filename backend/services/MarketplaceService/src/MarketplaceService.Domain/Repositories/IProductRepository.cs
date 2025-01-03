﻿using MarketplaceService.Domain.Entities;
using MarketplaceService.Domain.Queries;

namespace MarketplaceService.Domain.Repositories
{
    public interface IProductRepository
    {
        // Get all products
        Task<List<Product>> GetAllProductsAsync(ProductQuery productQuery);

        // Get a product by ID
        Task<Product> GetProductByIdAsync(string id);

        // Add a new product
        Task AddProductAsync(Product product);

        // Update an existing product
        Task UpdateProductAsync(Product product);

        // Delete a product by ID
        Task DeleteProductAsync(string id);
    }
}
