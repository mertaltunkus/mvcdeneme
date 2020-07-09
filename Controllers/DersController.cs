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
    public class DersController : Controller
    {
        private OkulDbEntities db = new OkulDbEntities();

        // GET: Ders
        public ActionResult Index()
        {
            return View(db.Ders.ToList());
        }
        public ActionResult Tekders(string id)
        {
            if (id == null)
                return RedirectToAction("Index");
            int _id = Int32.Parse(id);
            if (db.Ders.Find(_id) == null)
            {
                TempData["Message"] = "Boyle bir ders bulunamadi...";
                return RedirectToAction("Index");
            }

            ViewBag.DersAdi = (db.Ders.SingleOrDefault(m => m.Id == _id).Ad).ToString();
            return View(db.OgrenciDers.Where(i => i.DersId == _id).ToList());
        }




        // GET: Ders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Ad")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                db.Ders.Add(ders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ders);
        }

        // GET: Ders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ders ders = db.Ders.Find(id);
            if (ders == null)
            {
                return HttpNotFound();
            }
            return View(ders);
        }

        // POST: Ders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Ad")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ders);
        }

        // GET: Ders/Delete/5
        public ActionResult Delete(int? id)
        {
            Ders ders = db.Ders.Find(id);
            db.Ders.Remove(ders);
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
