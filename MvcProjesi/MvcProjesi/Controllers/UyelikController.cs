using MvcProjesi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjesi.Controllers
{
    public class UyelikController : Controller
    {
        public ActionResult YeniUyelik()
        {
            return View();
        }

        //Formumuzun geliş metodu Post
        //Dikkat ederseniz aynı isimli, iki adet Action var.
        //Üsttekinin metodu boş olduğu için Get oluyor.
        //Alttakinin üzerinde [HttpPost] olduğu için metodu Post oluyor.
        //Burada eğer sayfa içinden bir form gönderimi yapılmışsa, Post olan Action çağrılır.
        //Normal adres üzerinden sayfaya talepte bulunulursa, Get metodlu olan çağrılır.
        [HttpPost]
        public ActionResult YeniUyelik(Uye model, string textBoxDogum, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (String.IsNullOrEmpty(textBoxDogum))
            {
                //Burada Uye modelimizde olmayan bir elemanla çalıştığımız için, kendimiz elle hata
                //mesajını, sayfadaki hata listesine (@Html.ValidationSummary()) ekliyoruz.
                ModelState.AddModelError("textBoxDogum", "Doğum tarihi boş geçilemez");

                //Hata oluşması halinde sayfayı tekrar yüklüyoruz.
                //Böylelikle otomatik olarak hatalar sayfada gösteriliyor.
                return View();
            }
            int yil = int.Parse(textBoxDogum.Substring(6));
            if (yil > 2002)
            {
                ModelState.AddModelError("textBoxDogum", "Yaşınız 12 den küçük olamaz");
                return View();
            }
            Uye uye = new Uye();
            if (file != null)
            {
                //Sunucuya dosya kaydedilirken, sunucunun dosya sistemini, yolunu bilemeyeceğimiz için
                //Server.MapPath() ile sitemizin ana dizinine gelmiş oluruz. Devamında da sitemizdeki
                //yolu tanımlarız.
                file.SaveAs(Server.MapPath("~/Images/") + file.FileName);
                uye.ResimYol = "/Images/" + file.FileName;
            }
            uye.Ad = model.Ad;
            uye.EPosta = model.EPosta;
            uye.Soyad = model.Soyad;
            uye.UyeOlmaTarih = DateTime.Now;
            uye.WebSite = model.WebSite;
            uye.Sifre = model.Sifre;
            using (MvcProjesiContext db = new MvcProjesiContext())
            {
                db.Uyes.Add(uye);
                db.SaveChanges();

                //İşlemimiz başarıyla biterse, başarılı olduğuna dair bir sayfaya yönlendiriyoruz.
                return RedirectToAction("UyelikBasarili");
            }
        }

        public ActionResult UyelikBasarili()
        {
            return View();
        }

        public ActionResult UyeGiris()
        {
            return View();
        }

        [HttpPost]
        public string UyeGirisi()
        {
            //Request.Form["html elementinin name özelliği"] ile Post edilen formdaki elemanların
            //değerlerini alabiliyoruz. Bu metod yalnızca Post ile çalışır.
            string posta = Request.Form["txtPosta"];
            string sifre = Request.Form["pswSifre"];
            if (String.IsNullOrEmpty(posta) && String.IsNullOrEmpty(sifre))
            {
                return "E-Posta adresinizi ve şifrenizi girmediniz.";
            }
            else if (String.IsNullOrEmpty(posta))
            {
                return "E-posta adresinizi girmediniz.";
            }
            else if (string.IsNullOrEmpty(sifre))
            {
                return "Şifrenizi girmediniz.";
            }
            else
            {
                using (MvcProjesiContext db = new MvcProjesiContext())
                {
                    //Normalde şifreyi hashleyerek yazdırmamız ve kontrol etmemiz gerekir.
                    var uye = (from i in db.Uyes where i.Sifre == sifre && i.EPosta == posta select i).SingleOrDefault();

                    if (uye == null) return "Kullanıcı adınızı ya da şifreyi hatalı girdiniz.";

                    //Session'da müşteri ile ilgili bilgileri saklamaktayız.
                    //Güvenlik açısından bilgileri şifreleyerek saklamamız daha doğru bir yöntemdir.
                    //Asp.Net Membership yapısı, bu güvenliği sunmaktadır.
                    Session["uyeId"] = uye.UyeId;

                    //Burada eğer, kullanıcı bilgileri, sistemde eşleşirse, geriye girişin başarılı
                    //olduğuna dair bir mesaj ve 3 saniye sonra, ana sayfaya yönlendirecek bir
                    //javascript kodu ekliyoruz.
                    return "Sisteme, başarıyla giriş yaptınız.<script type='text/javascript'>setTimeout(function(){window.location='/'},3000);</script>";
                }
            }
        }
    }
}