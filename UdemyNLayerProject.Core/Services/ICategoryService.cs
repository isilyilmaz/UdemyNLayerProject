using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Services
{
    /*Access Modifiers: Internal, Protected, Public, Protected Internal, Private Protected, Private
   * Diğer katmanlardan erişileceği için public olmak durumunda.*/
    public interface ICategoryService : IService<Category>
    {
        //category alırken o category altındaki ürünlerde gelsin. Bunun için ilgili modelin içine method eklendi.
        //Category altına eklenen "public ICollection<Product> Products { get; set; }" satır ile sağlanıyor.
        Task<Category> GetWithProductsByIdAsync(int categoryId);
        
        //Category özelinde method oluşturulacaksa burada oluşturulmalı
    }
        
}
