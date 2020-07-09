using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCdeneme2.Models.EntityFramework;

namespace MVCdeneme2.Controllers
{
    public class OgrenciController : Controller
    {
        private OkulDbEntities db = new OkulDbEntities();

        // GET: Ogrenci
        public ActionResult Index()
        {
            var ogrenci = db.Ogrenci.Include(o => o.Sinif);
            return View(ogrenci.ToList());
        }

        // GET: Ogrenci/Create
        public ActionResult Create()
        {
            ViewBag.SınıfId = new SelectList(db.Sinif, "Id", "Ad");
            return View();
        }

        // POST: Ogrenci/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad,Soyad,DogumTarihi,Cinsiyet,GenelOrt,SınıfId")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Ogrenci.Add(ogrenci);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SınıfId = new SelectList(db.Sinif, "Id", "Ad", ogrenci.SınıfId);
            return View(ogrenci);
        }

        // GET: Ogrenci/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            if (ogrenci == null)
            {
                return HttpNotFound();
            }
            ViewBag.SınıfId = new SelectList(db.Sinif, "Id", "Ad", ogrenci.SınıfId);
            return View(ogrenci);
        }

        // POST: Ogrenci/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad,Soyad,DogumTarihi,Cinsiyet,GenelOrt,SınıfId")] Ogrenci ogrenci)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenci).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SınıfId = new SelectList(db.Sinif, "Id", "Ad", ogrenci.SınıfId);
            return View(ogrenci);
        }
        public ActionResult Delete(int? id)
        {
            Ogrenci ogrenci = db.Ogrenci.Find(id);
            db.Ogrenci.Remove(ogrenci);
            List<Ders> list = db.Ders.ToList();
            OgrenciDers ogrenciDers;
            for (int i = 0; i < list.Count(); i++)
            {
                ogrenciDers = db.OgrenciDers.Find(id,list[i].Id);
                if (ogrenciDers != null)
                {
                    db.OgrenciDers.Remove(ogrenciDers);
                }
            }
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
