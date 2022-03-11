using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;

namespace MvcStok.Controllers
{

    public class SatisController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Satis
        public ActionResult Index()
        {
            var satislar = db.TBLSatislars.ToList();
            return View(satislar);
        }

        public ActionResult Create()
        {
            ViewBag.Urun = db.TBLUrunlers.ToList();
            ViewBag.Musteri = db.TBLMusterilers.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(int urunid, int musteriid, int adet)
        {
            var urun = db.TBLUrunlers.Find(urunid);
            TBLSatislar satis = new TBLSatislar();
            satis.UrunId = urunid;
            satis.MusteriId = musteriid;
            satis.Adet = Convert.ToByte(adet);
            satis.Fiyat = urun.Fiyat;
            db.TBLSatislars.Add(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Update(int OrderId)
        {
            ViewBag.Urun = db.TBLUrunlers.ToList();
            ViewBag.Musteri = db.TBLMusterilers.ToList();
            var satis = db.TBLSatislars.Find(OrderId);
            return View(satis);
        }
        [HttpPost]
        public ActionResult Update(int OrderId, int urunid, int musteriid, byte adet)
        {
            var urun = db.TBLUrunlers.Find(urunid);
            TBLSatislar satis = db.TBLSatislars.Find(OrderId);
            satis.Adet = adet;
            satis.UrunId = urunid;
            satis.MusteriId = musteriid;
            satis.Fiyat = urun.Fiyat;
            db.SaveChanges();

            return RedirectToAction("Index");

        }
        public ActionResult Delete(int OrderId)
        {
            var satis= db.TBLSatislars.Find(OrderId);
            db.TBLSatislars.Remove(satis);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}