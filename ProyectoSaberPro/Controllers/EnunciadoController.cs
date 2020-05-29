using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoSaberPro.Models;

namespace ProyectoSaberPro.Controllers
{
    public class EnunciadoController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Enunciado
        public ActionResult Index()
        {
            return View(db.Enunciados.ToList());
        }

        // GET: Enunciado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enunciado enunciado = db.Enunciados.Find(id);
            if (enunciado == null)
            {
                return HttpNotFound();
            }
            return View(enunciado);
        }

        // GET: Enunciado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enunciado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TItulo,Descripcion")] Enunciado enunciado)
        {
            if (ModelState.IsValid)
            {
                db.Enunciados.Add(enunciado);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enunciado);
        }

        // GET: Enunciado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enunciado enunciado = db.Enunciados.Find(id);
            if (enunciado == null)
            {
                return HttpNotFound();
            }
            return View(enunciado);
        }

        // POST: Enunciado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TItulo,Descripcion")] Enunciado enunciado)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enunciado).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enunciado);
        }

        // GET: Enunciado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enunciado enunciado = db.Enunciados.Find(id);
            if (enunciado == null)
            {
                return HttpNotFound();
            }
            return View(enunciado);
        }

        // POST: Enunciado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enunciado enunciado = db.Enunciados.Find(id);
            db.Enunciados.Remove(enunciado);
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
