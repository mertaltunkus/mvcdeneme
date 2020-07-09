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
    public class SinifController : Controller
    {
        private OkulDbEntities db = new OkulDbEntities();
        // GET: Sinif
        public ActionResult Index()
        {
            return View(db.Sinif.ToList());
        }
        public ActionResult Delete(int? id)
        {
            List<Ogrenci> listOgrenci = db.Ogrenci.ToList();
            OgrenciDers ogrenciDers = new OgrenciDers();
            List<Ders> listDers = db.Ders.ToList();
            foreach (var item in listOgrenci)
            {
                if (item.SınıfId == id)
                {
                    for (int i = 0; i < listDers.Count(); i++)
                    {
                        ogrenciDers = db.OgrenciDers.Find(item.Id, listDers[i].Id);
                        if (ogrenciDers != null)
                            db.OgrenciDers.Remove(ogrenciDers);
                    }
                    db.Ogrenci.Remove(item);
                }
            }
            db.Sinif.Remove(db.Sinif.Find(id));
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinif sinif = db.Sinif.Find(id);
            if (sinif == null)
            {
                return HttpNotFound();
            }
            return View(sinif);
        }

        // POST: Ders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad")] Sinif sinif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sinif);
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad")] Sinif sinif)
        {
            if (ModelState.IsValid)
            {
                db.Sinif.Add(sinif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sinif);
        }

        public ActionResult TekSinif(string id)
        {
            if (id == null)
                return RedirectToAction("Index");
            int _id = Int32.Parse(id);
            if (db.Sinif.Find(_id) == null)
            {
                TempData["Message"] = "Boyle bir sinif bulunamadi...";
                return RedirectToAction("Index");
            }
            List<Ogrenci> ogrenciler = db.Ogrenci.Where(i => i.Sinif.Id == _id).ToList();
            return View(ogrenciler);

        }
    }
}
