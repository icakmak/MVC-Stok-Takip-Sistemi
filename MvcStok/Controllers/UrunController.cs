using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Urun
        public ActionResult Index(int page=1)
        {
            var urunler = db.TBLUrunlers.ToList().ToPagedList(page,10);
            return View(urunler);
        }
        public ActionResult Create()
        {
            ViewBag.kat = db.TBLKategorilers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(string ad, string marka, short kategoriId, decimal fiyat, int stok)
        {
            TBLUrunler urun = new TBLUrunler();
            urun.UrunAd = ad;
            urun.Marka = marka;
            urun.KategoriId = kategoriId;
            urun.Fiyat = fiyat;
            urun.Stok = stok;
            db.TBLUrunlers.Add(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int ProductId)
        {
            var urun = db.TBLUrunlers.Find(ProductId);
            ViewBag.kat = db.TBLKategorilers.ToList();
            return View(urun);
        }
        [HttpPost]
        public ActionResult Edit(int ProductId, string ad, string marka, short kategoriId, decimal fiyat, int stok)
        {
            var urun = db.TBLUrunlers.Find(ProductId);
            urun.UrunAd = ad;
            urun.Marka = marka;
            urun.KategoriId = kategoriId;
            urun.Fiyat = fiyat;
            urun.Stok = stok;
            //db.TBLUrunlers.
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderList(int ProductId)
        {
            ViewBag.urun = db.TBLUrunlers.Find(ProductId);
            var satis = db.TBLSatislars.Where(i => i.UrunId == ProductId).ToList();
            var satismiktari = 0;
            decimal toplam = 0;
            foreach (var item in satis)
            {
                satismiktari += Convert.ToInt32(item.Adet);
                toplam += Convert.ToDecimal(item.Fiyat) * Convert.ToDecimal(item.Adet);
            }
            ViewBag.SMiktari = satismiktari;
            ViewBag.SToplam = toplam;
            return View(satis);
        }

        public ActionResult Delete(int ProductId)
        {
            var satis = db.TBLSatislars.Where(i => i.UrunId == ProductId).Count();
            var urun = db.TBLUrunlers.Find(ProductId);

            if (satis == 0)
            {
                db.TBLUrunlers.Remove(urun);
                db.SaveChanges();
            }

            return RedirectToAction("Index");

        }
    }
}