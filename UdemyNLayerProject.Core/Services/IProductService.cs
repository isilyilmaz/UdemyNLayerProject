using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UdemyNLayerProject.Core.Models;

namespace UdemyNLayerProject.Core.Services
{
  
    /*Access Modifiers: Internal, Protected, Public, Protected Internal, Private Protected, Private
     * Diğer katmanlardan erişileceği için public olmak durumunda.*/
    public interface IProductService:IService<Product>
    {
        //ürünün id si ile ürün aldığımda o ürünün category side gelsin istiyorum.
        Task<Product> GetWithCategoryByIdAsync(int productId);

        //içinde sadece product a özel servisler olacak.
        //bool ControlInnerBarcode(Product product);
    }
}
