using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlCash.EF;
using ControlCash.Entities;

namespace ControlCash.Web.Controllers
{
    public class LimitsController : Controller
    {
        private ControlCashDbContext db = new ControlCashDbContext();

        // GET: Limits
        public ActionResult Index()
        {
            
            return View(db.LimitRepository.GetAll());
        }

        // GET: Limits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limit limit = db.LimitRepository.Find(a => a.LimitId == id);
            if (limit == null)
            {
                return HttpNotFound();
            }
            return View(limit);
        }

        // GET: Limits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Limits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LimitId,TagDescription,LimitValue")] Limit limit)
        {
            if (ModelState.IsValid)
            {
                db.LimitRepository.Add(limit);
                db.SaveChanges().Wait();
                return RedirectToAction("Index");
            }

            return View(limit);
        }

        // GET: Limits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limit limit = db.LimitRepository.Find(a => a.LimitId == id);
            if (limit == null)
            {
                return HttpNotFound();
            }
            return View(limit);
        }

        // POST: Limits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LimitId,TagDescription,LimitValue")] Limit limit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(limit).State = EntityState.Modified;
                db.SaveChanges().Wait();
                return RedirectToAction("Index");
            }
            return View(limit);
        }

        // GET: Limits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Limit limit = db.LimitRepository.Find(a => a.LimitId == id);
            if (limit == null)
            {
                return HttpNotFound();
            }
            return View(limit);
        }

        // POST: Limits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Limit limit = db.LimitRepository.Find(a => a.LimitId == id);
            db.LimitRepository.Delete(limit);
            db.SaveChanges().Wait();
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
