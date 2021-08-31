using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UdemyNLayerProject.Core.Models
{
    //Her kategori birden fazla ürüne sahip olabilir.
    /*Access Modifiers: Internal, Protected, Public, Protected Internal, Private Protected, Private
     * Internal Class'lar sadece tanımlandıkları katmanlardan erişim sağlanması özelliğine sahiptirler. 
     * Tanımlı oldukları katmanların dışından hiçbir şekilde erişilemezler.*/
    internal class Category
    {
        public Category()
        {
            Products = new Collection<Product>();
        }
        public int Id { get; set; }

        public String Name { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
