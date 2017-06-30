using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Yorum
    {
        public int YorumId { get; set; }

        [Required(ErrorMessage = "Lütfen yorumunuzu giriniz.")]
        public string Icerik { get; set; }

        [Required(ErrorMessage = "Lütfen yorumun yapılma tarihini giriniz.")]
        [DataType(DataType.DateTime, ErrorMessage = "Lütfen yorum yapılma tarihini, doğru bir şekilde giriniz.")]
        public DateTime Tarih { get; set; }

        //Her yorum, yalnızca bir makaleye ait olabilir. Bu yüzden, tek bir makaleye bağlıyoruz. 
        //Dikkat edileceği üzere veri türü (burada aynı zamanda sınıf) olarak Makale yazılıyor.
        public virtual Makale Makale { get; set; }

        //Her yorumu, yalnızca bir kişi yazmış olabilir.
        public virtual Uye Uye { get; set; }
    }
}