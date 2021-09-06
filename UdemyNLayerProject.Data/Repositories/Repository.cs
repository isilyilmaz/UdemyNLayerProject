using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Data.Repositories
{
    //Belirttiğim TEntity generic ifade class olmak zorunda.
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        //miras aldığım yerde kullanıyorumi bu yüzden access modifier protected belirleyebilirim.
        protected readonly DbContext _context;
        //Sadece burada kullanıldığı için access modifier private belirleyebilirim.
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        /*asenkron programlamadaki önemli keywordler async ve await. 
         * await keywordunun yaptığı şey şu, bundan sonra yazacağım method bitene kadar bu satırda bekle.
        */
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        //Senkton programlamada void keywordu, asenkron programlamada task keywordü kullanılır.
        public async Task AddRangesync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> getAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> getByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public async Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.SingleOrDefaultAsync(predicate);
        }

        public TEntity Update(TEntity entity)
        {
            //Dezavantajı şu; tek bir kolon update olsa bile tüm alanlar değişmiş gibi veri tabanına sorgu gönderir.
            //Avantajı: tek tek name alıp eşitleyip kod yaznak yerine tek bir satırda yazabiliyoruz
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
