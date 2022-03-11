using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var musteriler = db.TBLMusterilers.ToList();
            return View(musteriler);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string adi,string soyadi)
        {
            TBLMusteriler musteri = new TBLMusteriler();
            musteri.MusteriAd = adi;
            musteri.MusteriSoyad = soyadi;
            db.TBLMusterilers.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var musteri = db.TBLMusterilers.Find(id);
            return View(musteri);
        }
        [HttpPost]
        public ActionResult Edit(int id,string adi, string soyadi)
        {
            TBLMusteriler musteri = db.TBLMusterilers.Find(id);
            musteri.MusteriAd = adi;
            musteri.MusteriSoyad = soyadi;
            //db.TBLMusterilers.Add(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderList(int MusteriId)
        {
            ViewBag.musteri = db.TBLMusterilers.Find(MusteriId);
            var satislar= db.TBLSatislars.Where(i => i.MusteriId == MusteriId).ToList();
            decimal toplam = 0;
            foreach (var item in satislar)
            {
                toplam += Convert.ToDecimal(item.Fiyat) *Convert.ToDecimal(item.Adet);
            }
            ViewBag.Toplam = toplam;
            return View(satislar);
        }

        public ActionResult Order(int MusteriId)
        {
            var musteri = db.TBLMusterilers.Find(MusteriId);
            ViewBag.Urun = db.TBLUrunlers.ToList();
            return View(musteri);

        }
        [HttpPost]
        public ActionResult Order(int MusteriId,int urunid,byte adet)
        {
            var urun = db.TBLUrunlers.Find(urunid);
            TBLSatislar satis = new TBLSatislar();
            satis.MusteriId = MusteriId;
            satis.UrunId = urunid;
            satis.Fiyat = urun.Fiyat;
            satis.Adet = adet;
            db.TBLSatislars.Add(satis);
            db.SaveChanges();
            return RedirectToAction("OrderList", new { MusteriId = MusteriId });
            

        }
    }
}