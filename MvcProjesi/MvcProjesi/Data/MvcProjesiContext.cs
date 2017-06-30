using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    //Öncelikle sınıfımızı, DbContext sınıfından implemente ediyoruz. Böylelikle, DbContext sınıfının özelliklerini
    //kullanabiliyor olacağız.
    public class MvcProjesiContext : DbContext
    {
        //Daha sonra veritabanımızda, tablo olarak temsil edilecek tüm sınıflarımızı DbSet<..> içerisinde tek tek
        //çağırıyoruz. Sonuna s takısı koyduğumuza dikkat edin. Böylelikle bunun tablo olduğunu anlıyor olacağız.
        //Önceki yazımızda bahsettiğimiz gibi, sonunda zaten s olan bir sınıf ismimiz varsa, bu sefer de s takısını
        //kaldırabiliriz.
        public DbSet<Etiket> Etikets { get; set; }
        public DbSet<Makale> Makales { get; set; }
        public DbSet<Uye> Uyes { get; set; }
        public DbSet<Yorum> Yorums { get; set; }
    }
}