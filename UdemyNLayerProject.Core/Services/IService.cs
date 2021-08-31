using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNLayerProject.Core.Services
{
    //IRepository ile aynı içerik, neden peki direk repository kullanılmıyor ayrıca service katmanı var?
    // DB değişirse o zaman farklı repository oluşturmak gerekiyor.
    // Mesela, Oracle Repository oluşturup Service katmanında değişiklik yapmak gerekmeyecek.

    /*Access Modifiers: Internal, Protected, Public, Protected Internal, Private Protected, Private
   * Diğer katmanlardan erişileceği için public olmak durumunda.*/
    public interface IService<TEntity> where TEntity : class
    {
        // Asenkron içerik olması için Task oluşturuyoruz.

        //id ye göre nesne getir
        Task<TEntity> getByIdAsync(int Id);

        //tüm nesneleri getir
        Task<IEnumerable<TEntity>> getAllAsync();

        // daha sonra bu şekilde kullanılabilecek; find(x=> x.id = 2)
        //Expression<Func<TEntity, bool>> bu ifade delegate olarak geçiyor.
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> Predicate);

        // daha sonra bu şekilde kullanılabilecek; Tek bir kayıt getirecek
        // category.SingleOrDefaultAsync(x=> x.name = "kalem")
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> Predicate);

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
