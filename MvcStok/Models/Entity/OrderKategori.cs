using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcStok.Models.Entity
{
    public class OrderKategori
    {
        public Nullable<int> UrunId { get; set; }
        public string UrunAd { get; set; }
        public int KategoriId { get; set; }
        public string  KategoriAd { get; set; }

        public int total { get; set; }
        public int SatisId { get; set; }
        public Nullable<byte> Adet { get; set; }
        
        public Nullable<decimal> Fiyat { get; set; }
    }
}