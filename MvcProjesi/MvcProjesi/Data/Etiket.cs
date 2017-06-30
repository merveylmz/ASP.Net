using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcProjesi.Data
{
    public class Etiket
    {
        public int EtiketId { get; set; }

        [Required(ErrorMessage = "Lütfen etiketin içeriğini giriniz.")]
        [StringLength(50, ErrorMessage = "Etiketin içeriği 50 karakterden uzun olamaz.")]
        public string Icerik { get; set; }

        //Aynı etiket, birden çok makale de kullanılıyor olabilir.
        public virtual List<Makale> Makales { get; set; }
    }
}