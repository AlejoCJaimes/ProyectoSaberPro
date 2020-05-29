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
    public class PruebaPreguntaController : Controller
    {
        private Contexto db = new Contexto();

        // GET: PruebaPregunta
        public ActionResult Index()
        {
            var pruebasPreguntas = db.PruebasPreguntas.Include(p => p.Pregunta).Include(p => p.Prueba);
            return View(pruebasPreguntas.ToList());
        }

        // GET: PruebaPregunta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaPregunta pruebaPregunta = db.PruebasPreguntas.Find(id);
            if (pruebaPregunta == null)
            {
                return HttpNotFound();
            }
            return View(pruebaPregunta);
        }

        // GET: PruebaPregunta/Create
        public ActionResult Create()
        {
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion");
            ViewBag.IdPrueba = new SelectList(db.Prueba, "Id", "Nombre");
            return View();
        }

        // POST: PruebaPregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPrueba,IdPregunta")] PruebaPregunta pruebaPregunta)
        {
            if (ModelState.IsValid)
            {
                db.PruebasPreguntas.Add(pruebaPregunta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", pruebaPregunta.IdPregunta);
            ViewBag.IdPrueba = new SelectList(db.Prueba, "Id", "Nombre", pruebaPregunta.IdPrueba);
            return View(pruebaPregunta);
        }

        // GET: PruebaPregunta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaPregunta pruebaPregunta = db.PruebasPreguntas.Find(id);
            if (pruebaPregunta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", pruebaPregunta.IdPregunta);
            ViewBag.IdPrueba = new SelectList(db.Prueba, "Id", "Nombre", pruebaPregunta.IdPrueba);
            return View(pruebaPregunta);
        }

        // POST: PruebaPregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPrueba,IdPregunta")] PruebaPregunta pruebaPregunta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pruebaPregunta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", pruebaPregunta.IdPregunta);
            ViewBag.IdPrueba = new SelectList(db.Prueba, "Id", "Nombre", pruebaPregunta.IdPrueba);
            return View(pruebaPregunta);
        }

        // GET: PruebaPregunta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PruebaPregunta pruebaPregunta = db.PruebasPreguntas.Find(id);
            if (pruebaPregunta == null)
            {
                return HttpNotFound();
            }
            return View(pruebaPregunta);
        }

        // POST: PruebaPregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PruebaPregunta pruebaPregunta = db.PruebasPreguntas.Find(id);
            db.PruebasPreguntas.Remove(pruebaPregunta);
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
