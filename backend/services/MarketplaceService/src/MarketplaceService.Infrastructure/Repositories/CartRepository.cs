﻿using MarketplaceService.Domain.Entities;
using MarketplaceService.Domain.Repositories;
using MarketplaceService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketplaceService.Infrastructure.Repositories
{
    
    public class CartRepository : ICartRepository
    {
        private readonly ApiDbContext apiDbContext;

        public CartRepository(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }

        public async Task AddCartAsync(Cart cart)
        {
            await apiDbContext.carts.AddAsync(cart);
            await apiDbContext.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(int id)
        {
            var cart = await apiDbContext.carts.FindAsync(id);
            if (cart != null)
            {
                apiDbContext.carts.Remove(cart);
                await apiDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Cart>> GetAllCartsAsync()
        {
            return await apiDbContext.carts.ToListAsync();
        }

        public Task<Cart> GetCartByCustomerAsync(string CustomerId)
        {
            return apiDbContext.carts.Include(x=>x.CartProducts).ThenInclude(x=>x.Product).FirstOrDefaultAsync(x => x.CustomerId == CustomerId);
        }

        public async Task<Cart> GetCartByIdAsync(int id)
        {
            return await apiDbContext.carts.FindAsync(id);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            var existingCart = await apiDbContext.carts.FindAsync(cart.CartId);
            if (existingCart != null)
            {
                apiDbContext.Entry(existingCart).CurrentValues.SetValues(cart);
                await apiDbContext.SaveChangesAsync();
            }
        }
    }
}
