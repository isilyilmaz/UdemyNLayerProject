using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        //repository içindeki context i appdbcontext e dönüştürmem lazım. çünkü ne olduğunu biliyorum.
        private AppDbContext appDbContext { get => _context as AppDbContext; }
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        //ürünün id si ile ürün aldığımda o ürünün category side gelsin istiyorum.
        public async Task<Product> GetWithCategoryByIdAsync(int productId)
        {
            return await appDbContext.Products.Include(x => x.Category)
                                              .SingleOrDefaultAsync(x => x.Id == productId);
        }
    }
}
