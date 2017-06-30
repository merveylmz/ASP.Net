using System;
using System.Collections.Generic;

//Bu isim uzayını eklememizin sebebi, [DataType],[StringLength] gibi Attribute'leri tanımlıyor olmamız.
using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Uye
    {
        //Bu alan tablonun id'si olacak. Tablo adının sonuna Id takısı eklendiğinde, framework bu alanı otomatik olarak, 
        //tablonun anahtar sütunu yani primary key'si yapacaktır. Bu varsayılanı değiştirmek mümkündür, ancak bu konuya 
        //girmeyeceğiz. Detaylı bilgi için, Code First Fluent olarak araştırma yapabilirsiniz.
        public int UyeId { get; set; }

        //Bu alanı zorunlu hale getiriyoruz. Böylelikle boş geçilemeyecek.
        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        //Girilen metnin uzunluğunu belirtiyoruz. İlk değişken minimum uzunluk olurken, sonrakiler ise, opsiyonel 
        //girdilerdir.
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Adınızı 3-50 karakter arasında girebilirsiniz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Soyadınızı 3-50 karakter arasında girebilirsiniz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Lütfen e-posta adresinizi giriniz.")]
        //Girilen metnin, geçerli bir e-posta adresi formatında girilmesini sağlıyoruz. 
        //DataType tipleri, Microsoft tarafından Framework'e eklenen hazır tiplerdir.
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen e-posta adresinizi geçerli bir formatta giriniz.")]
        public string EPosta { get; set; }

        //Required Attribute'si eklemediğimiz için, bu alan zorunlu olmayacak ve boş geçilebiliyor olacak.
        //Girilen metnin, geçerli bir web sitesi adresi formatında girilmesini sağlıyoruz. 
        [DataType(DataType.Url, ErrorMessage = "Lütfen web site adresinizi, geçerli bir formatta giriniz.")]
        public string WebSite { get; set; }

        //Girilen metnin, geçerli bir resim yolu formatında girilmesini sağlıyoruz. 
        [DataType(DataType.ImageUrl, ErrorMessage = "Lütfen resim yolunuzu doğru şekilde giriniz.")]
        public string ResimYol { get; set; }


        [Required(ErrorMessage = "Lütfen üye olma tarihini giriniz.")]
        //Girilen tarihin, geçerli bir tarih ve saat formatında girilmesini sağlıyoruz.
        [DataType(DataType.DateTime, ErrorMessage = "Lütfen üye olma tarihini, doğru bir şekilde giriniz.")]
        public DateTime UyeOlmaTarih { get; set; }

        //Bir üyenin, birden çok yorumu olabileceği için Yorumları bir liste içerisine alıyoruz. 
        //Property adının sonuna s takısı koymamızın sebebi, veri tipinin çoğul olduğunun daha kolay anlaşılabilmesi 
        //içindir. Entity varsayılan olarak, liste verilerin sonuna s takısı koymaktadır.
        //Biz de bu standarda uyum gösterdik. Eğer veri tipinin adı zaten s ile bitiyorsa o zaman da s yi siliyoruz.
        //Örneğin hayvanlarla ilgili bir tabloda, Kus adlı bir sınıfımız çoğul olarak temsil edilecekse Ku olarak 
        //kullanabiliriz.
        public virtual List<Yorum> Yorums { get; set; }

        //Bir üyenin eklediği, birden çok makale olabilir.
        public virtual List<Makale> Makales { get; set; }

        //Baştan eklemeyi unuttuğumuz şifre kısmını ekledik.
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz.")]
        public string Sifre { get; set; }
    }
}