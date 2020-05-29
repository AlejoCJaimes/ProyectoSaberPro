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
    public class PPPController : Controller
    {
        private Contexto db = new Contexto();

        // GET: PPP
        public ActionResult Index()
        {
            var pregunta = db.Pregunta.Include(p => p.Competencia).Include(p => p.Enunciado);
            return View(pregunta.ToList());
        }

        // GET: PPP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // GET: PPP/Create
        public ActionResult Create()
        {
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre");
            ViewBag.IdEnunciado = new SelectList(db.Enunciados, "Id", "TItulo");
            return View();
        }

        // POST: PPP/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,IdCompetencia,IdEnunciado")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                db.Pregunta.Add(pregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            ViewBag.IdEnunciado = new SelectList(db.Enunciados, "Id", "TItulo", pregunta.IdEnunciado);
            return View(pregunta);
        }

        // GET: PPP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            ViewBag.IdEnunciado = new SelectList(db.Enunciados, "Id", "TItulo", pregunta.IdEnunciado);
            return View(pregunta);
        }

        // POST: PPP/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IdCompetencia,Enunciado.TItulo")] Pregunta pregunta)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            ViewBag.IdEnunciado = new SelectList(db.Enunciados, "Id", "TItulo", pregunta.IdEnunciado);
            ViewBag.Enunciado = new SelectList(db.Enunciados, "Id", "Titulo", pregunta.Enunciado);
            return View(pregunta);
        }

        // GET: PPP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            return View(pregunta);
        }

        // POST: PPP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pregunta pregunta = db.Pregunta.Find(id);
            db.Pregunta.Remove(pregunta);
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
