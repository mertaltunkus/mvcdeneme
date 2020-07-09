using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Web;
using System.Web.Mvc;
using MVCdeneme2.Models.EntityFramework;

namespace MVCdeneme2.Controllers
{
    [Authorize]
    public class OgrenciDersController : Controller
    {
        private OkulDbEntities db = new OkulDbEntities();

        // GET: OgrenciDers
        public ActionResult Index()
        {
            ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad");
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad");
            return View();
        }
        [HttpPost]
        public ActionResult Index(OgrenciDers ogrenciDers)
        {

            return RedirectToAction("Edit", ogrenciDers);
        }

        // GET: OgrenciDers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            if (ogrenciDers == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciDers);
        }

        // GET: OgrenciDers/Create
        public ActionResult Create()
        {
            ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad");
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad");
            return View();
        }

        // POST: OgrenciDers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OgrenciId,DersId,Vize,Final,SonNot")] OgrenciDers ogrenciDers)
        {
            if (ModelState.IsValid)
            {
                var res = db.OgrenciDers.Find(ogrenciDers.OgrenciId, ogrenciDers.DersId);
                if (res==null)
                {
                    ogrenciDers.FindSonNot();
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    string ogrenciAd = db.Ogrenci.Find(ogrenciDers.OgrenciId).Ad + " " + db.Ogrenci.Find(ogrenciDers.OgrenciId).Soyad;
                    string dersAd = db.Ders.Find(ogrenciDers.DersId).Ad;
                    ViewBag.Message = ogrenciAd + " isimli ogrencinin " + dersAd + " dersi icin girilmiş notu zaten bulunmaktadır.";
                    ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad");
                    ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad");
                    return View();

                }



            }

            ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad", ogrenciDers.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad", ogrenciDers.OgrenciId);
            return View(ogrenciDers);
        }



        // GET: OgrenciDers/Edit/5
        public ActionResult Edit(int? ogrenciId, int? dersId)
        {
            if (ogrenciId == null && dersId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(ogrenciId, dersId);
            if (ogrenciDers == null)
            {
                Ogrenci ogrenci = db.Ogrenci.Find(ogrenciId);
                Ders ders = db.Ders.Find(dersId);
                string isim = ogrenci.Ad + " " + ogrenci.Soyad;
                string str = isim + " isimli ogrencinin " + ders.Ad + " dersi icin onceden girilmis bir notu bulunmamaktadir.";
                TempData["Message"] = str;
                //ogrenciDers = new OgrenciDers { OgrenciId = ogrenciId.Value, DersId = dersId.Value };
                return RedirectToAction("Index");
            }
            ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad", ogrenciDers.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad", ogrenciDers.OgrenciId);
            return View(ogrenciDers);
        }

        // POST: OgrenciDers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OgrenciId,DersId,Vize,Final,SonNot")] OgrenciDers ogrenciDers)
        {
            if (ModelState.IsValid)
            {
                ogrenciDers.FindSonNot();
                return RedirectToAction("Index");
            }
            ViewBag.DersId = new SelectList(db.Ders, "Id", "Ad", ogrenciDers.DersId);
            ViewBag.OgrenciId = new SelectList(db.Ogrenci, "Id", "Ad", ogrenciDers.OgrenciId);
            return View(ogrenciDers);
        }


        public ActionResult YeniDonem()
        {
            foreach (var item in db.OgrenciDers.ToList())
            {
                db.OgrenciDers.Remove(item);

            }
            foreach (var item in db.Ogrenci.ToList())
            {
                item.GenelOrt = null;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Ogrenci");
        }

        // GET: OgrenciDers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            if (ogrenciDers == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciDers);
        }

        // POST: OgrenciDers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgrenciDers ogrenciDers = db.OgrenciDers.Find(id);
            db.OgrenciDers.Remove(ogrenciDers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
