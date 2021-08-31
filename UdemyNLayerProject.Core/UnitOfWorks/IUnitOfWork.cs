using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Repositories;

namespace UdemyNLayerProject.Core.UnitOfWorks
{
    /*Access Modifiers: Internal, Protected, Public, Protected Internal, Private Protected, Private
  * Diğer katmanlardan erişileceği için public olmak durumunda.*/
    public interface IUnitOfWork
    {
        //UnitOfWorks Pattern neden uygulanır?
        //Entity tarafında yapılan insert delete, update işleminin veritabanına yansımasını size bırakması.
        //Sen değişikliklerini yap, entity framework hafızada tutar, sen commit çağırıp veritabanına yansıt.
        //Hata aldığında Rollback zorunluluğu kalmaz.

        //DB işlemleri olacağından repositoryleri de burada alalım
        IProductRepository Products { get; }

        ICategoryRepository Categories { get; }

        Task CommitAsync();

        void Commit();
    }
}
