using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Core.Repositories
{
    //Repository sadece veri tabanı işlemlerini gerçekleştirir.
    public interface IRepository<TEntity> where TEntity : class
    {
        // Asenkron içerik olması için Task oluşturuyoruz.

        //id ye göre nesne getir
        Task<TEntity> getByIdAsync(int id);

        //tüm nesneleri getir
        Task<IEnumerable<TEntity>> getAllAsync();

        // daha sonra bu şekilde kullanılabilecek; Where(x=> x.id = 2)
        //Expression<Func<TEntity, bool>> bu ifade delegate olarak geçiyor.
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        // daha sonra bu şekilde kullanılabilecek; Tek bir kayıt getirecek
        // category.SingleOrDefaultAsync(x=> x.name = "kalem")
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        //1 kayıt eklenebilir
        Task AddAsync(TEntity entity);

        //1 den fazla kayıt eklenebilir
        Task AddRangesync(IEnumerable<TEntity> entities);

        //silme
        void Remove(TEntity entity);

        //toplu silme
        void RemoveRange(IEnumerable<TEntity> entities);

        //Güncelleme
        TEntity Update(TEntity entity);
    }
}
