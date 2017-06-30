//Veritabanı context sınıfımızı referans veriyoruz
using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcProjesi
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //Uygulama ilk başlatıldığında, buradaki metod çalışacak.
        protected void Application_Start()
        {
            //Burada veritabanı sınıfımızdan, bir nesne oluşturuyoruz. using kullanmamızın sebebi,
            //db nesnesinin işi bittiğinde, silinmesini ve hafızada yer tutmamasını sağlamak.
            using (MvcProjesiContext db = new MvcProjesiContext())
            {
                //Bu metod, eğer veritabanımız oluşturulmamış ise, oluşturulmasını sağlıyor.
                db.Database.CreateIfNotExists();

                //Veritabanındaki makalelerin, yorumların, üyelerin ve etiketlerin adetini alıyoruz.
                int makaleAdet = (from i in db.Makales select i).Count();
                int yorumAdet = (from i in db.Yorums select i).Count();
                int uyeAdet = (from i in db.Uyes select i).Count();
                int etiketAdet = (from i in db.Etikets select i).Count();

                //Veritabanına, sürekli aynı makalelerin ve yorumların eklenmemesi için
                //en az 5 adet makale ve yorum var mı diye kontrol ediyoruz.
                //Ayrıca sistemde en az 1 üye olduğunu da onaylıyoruz.
                //Bununla birlikte en az 10 adet etiket olduğunu da onaylıyoruz.
                if (makaleAdet < 5 || yorumAdet < 5 || uyeAdet < 1 || etiketAdet < 10)
                {
                    //Bir tane örnek üye oluşturuyoruz.
                    Uye uye = new Uye() { Ad = "Merve", Soyad = "Yilmaz", EPosta = "merveyilmaz@deneme.com", ResimYol = "", UyeOlmaTarih = DateTime.Now, WebSite = "http://www.merveyilmaz.com", Sifre = "123456" };
                    
                    db.Uyes.Add(uye);
                            
                    //Makalelerimizi oluşturuyoruz. Ayrıca makalelerin, yukarıda oluşturduğumuz kullanıcı 
                    //tarafından oluşturulduğunu gösteriyoruz.
                    Makale makale1 = new Makale() { Baslik = "Makale Başlığı 1", Icerik = "Makale İçeriği 1", Tarih = DateTime.Now, Uye = uye };
                    Makale makale2 = new Makale() { Baslik = "Makale Başlığı 2", Icerik = "Makale İçeriği 2", Tarih = DateTime.Now, Uye = uye };
                    Makale makale3 = new Makale() { Baslik = "Makale Başlığı 3", Icerik = "Makale İçeriği 3", Tarih = DateTime.Now, Uye = uye };
                    Makale makale4 = new Makale() { Baslik = "Makale Başlığı 4", Icerik = "Makale İçeriği 4", Tarih = DateTime.Now, Uye = uye };
                    Makale makale5 = new Makale() { Baslik = "Makale Başlığı 5", Icerik = "Makale İçeriği 5", Tarih = DateTime.Now, Uye = uye };
                    Makale makale6 = new Makale() { Baslik = "Makale Başlığı 6", Icerik = "Makale İçeriği 6", Tarih = DateTime.Now, Uye = uye };

                    //Makaleleri eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Makales.Add(makale1);
                    db.Makales.Add(makale2);
                    db.Makales.Add(makale3);
                    db.Makales.Add(makale4);
                    db.Makales.Add(makale5);
                    db.Makales.Add(makale6);

                    //Yorumlarımızı oluşturuyoruz. Ayrıca yorumların, yukarıda oluşturduğumuz kullanıcı 
                    //tarafından oluşturulduğunu gösteriyor, ayrıca makalelerimize de bağlıyoruz.
                    Yorum yorum1 = new Yorum() { Icerik = "Makale 1 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };
                    Yorum yorum2 = new Yorum() { Icerik = "Makale 2 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };
                    Yorum yorum3 = new Yorum() { Icerik = "Makale 3 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };
                    Yorum yorum4 = new Yorum() { Icerik = "Makale 4 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };
                    Yorum yorum5 = new Yorum() { Icerik = "Makale 5 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };
                    Yorum yorum6 = new Yorum() { Icerik = "Makale 6 için yazılan yorum", Tarih = DateTime.Now, Makale = makale1, Uye = uye };

                    //Yorumları eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Yorums.Add(yorum1);
                    db.Yorums.Add(yorum2);
                    db.Yorums.Add(yorum3);
                    db.Yorums.Add(yorum4);
                    db.Yorums.Add(yorum5);
                    db.Yorums.Add(yorum6);

                    //Etiketlerimizi oluşturuyoruz. Ayrıca etiketleri, kullanıldığı makalelerimize de bağlıyoruz.
                    Etiket etiket1 = new Etiket() { Icerik = "Asp.Net", Makales = new List<Makale>() { makale1, makale2, makale3, makale4, makale6 } };
                    Etiket etiket2 = new Etiket() { Icerik = "PHP", Makales = new List<Makale>() { makale5, makale3, makale2, makale1 } };
                    Etiket etiket3 = new Etiket() { Icerik = "Java", Makales = new List<Makale>() { makale2, makale4, makale5 } };
                    Etiket etiket4 = new Etiket() { Icerik = "C#", Makales = new List<Makale>() { makale5, makale4 } };
                    Etiket etiket5 = new Etiket() { Icerik = "Ruby", Makales = new List<Makale>() { makale5, makale6 } };
                    Etiket etiket6 = new Etiket() { Icerik = "C++", Makales = new List<Makale>() { makale5, makale2 } };
                    Etiket etiket7 = new Etiket() { Icerik = "D", Makales = new List<Makale>() { makale5, makale1 } };
                    Etiket etiket8 = new Etiket() { Icerik = "Phyton", Makales = new List<Makale>() { makale1, makale4 } };

                    Etiket etiket9 = new Etiket() { Icerik = "JSF", Makales = new List<Makale>() { makale5, makale4 } };
                    Etiket etiket10 = new Etiket() { Icerik = "JSP", Makales = new List<Makale>() { makale5, makale3, makale6 } };
                    Etiket etiket11 = new Etiket() { Icerik = "XCode", Makales = new List<Makale>() { makale5, makale4, makale1 } };
                    Etiket etiket12 = new Etiket() { Icerik = "ColdFusion", Makales = new List<Makale>() { makale5, makale2 } };
                    Etiket etiket13 = new Etiket() { Icerik = "Pascal", Makales = new List<Makale>() { makale5, makale1, makale3 } };
                    Etiket etiket14 = new Etiket() { Icerik = "Cobol", Makales = new List<Makale>() { makale5, makale4, makale3, makale1, makale2 } };

                    //Etiketleri eklemek için komutumuzu veriyoruz.
                    //SaveChanges() komutu gelene kadar veritabanına kayıt yapılmayacak.
                    db.Etikets.Add(etiket1);
                    db.Etikets.Add(etiket2);
                    db.Etikets.Add(etiket3);
                    db.Etikets.Add(etiket4);
                    db.Etikets.Add(etiket5);
                    db.Etikets.Add(etiket6);
                    db.Etikets.Add(etiket7);
                    db.Etikets.Add(etiket8);
                    db.Etikets.Add(etiket9);
                    db.Etikets.Add(etiket10);
                    db.Etikets.Add(etiket11);
                    db.Etikets.Add(etiket12);
                    db.Etikets.Add(etiket13);
                    db.Etikets.Add(etiket14);

                    //Son olarak da yaptığımız eklemelerin, veritabanına yansıtılmasını
                    //sağlamak için kaydet komutu veriyoruz.
                    db.SaveChanges();

                }
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
