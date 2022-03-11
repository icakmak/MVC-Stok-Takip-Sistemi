using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        OrderKategori ok = new OrderKategori();
        // GET: Kategori Sayfalama Yapılmak istenirse bu kullanılabilir
        /*public ActionResult Index(int page=1)
        {
            //var kategoriler = db.TBLKategorilers.ToList();
            var sorgu = (from k in db.TBLKategorilers
                         select new OrderKategori
                         {
                             KategoriId = k.KategoriId,
                             KategoriAd = k.KategoriAd,
                             total = db.TBLUrunlers.Where(i => i.KategoriId == k.KategoriId).Count()
                         }).ToList().ToPagedList(page,10);
            return View(sorgu);
        }*/

        public ActionResult Index()
        {
            //var kategoriler = db.TBLKategorilers.ToList();
            var sorgu = (from k in db.TBLKategorilers
                         select new OrderKategori
                         {
                             KategoriId = k.KategoriId,
                             KategoriAd = k.KategoriAd,
                             total = db.TBLUrunlers.Where(i => i.KategoriId == k.KategoriId).Count()
                         }).ToList();
            return View(sorgu);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string kategoriAdi)
        {
            TBLKategoriler kategori = new TBLKategoriler();
            kategori.KategoriAd = kategoriAdi;
            db.TBLKategorilers.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var Ktgr = db.TBLKategorilers.Find(id);

            return View(Ktgr);
        }
        [HttpPost]
        public ActionResult Edit(int id, string kategoriAdi)
        {
            TBLKategoriler kategori = db.TBLKategorilers.Find(id);
            kategori.KategoriAd = kategoriAdi;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult OrderList(int KategoriId)
        {
            ViewBag.kategori = db.TBLKategorilers.Find(KategoriId);
            var sorgu = (from s in db.TBLSatislars
                         join u in db.TBLUrunlers on s.UrunId equals u.UrunId
                         join k in db.TBLKategorilers on u.KategoriId equals k.KategoriId
                         where k.KategoriId == KategoriId
                         select new OrderKategori
                         {
                             Adet = s.Adet,
                             Fiyat = s.Fiyat,
                             UrunId = s.UrunId,
                             KategoriId = k.KategoriId,
                             KategoriAd = k.KategoriAd,
                             UrunAd = u.UrunAd,
                         }).ToList();
            //var kate = db.KategoriSatis.Where(i=>i.KategoriId==KategoriId).ToList();
            return View(sorgu);
        }
        public ActionResult Delete(int KategoriId)
        {
            var urun = db.TBLUrunlers.Where(i => i.KategoriId == KategoriId).ToList();
            if (urun.Count == 0)
            {
                var ktg = db.TBLKategorilers.Find(KategoriId);
                db.TBLKategorilers.Remove(ktg);
                db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        
    }

}

