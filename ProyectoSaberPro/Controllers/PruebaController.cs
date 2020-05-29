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
    public class PruebaController : Controller
    {
        private Contexto db = new Contexto();
       
        // GET: Prueba
        public ActionResult Index()
        {
            var prueba = db.Prueba.Include(p => p.Usuario);
            return View(prueba.ToList());
        }

        // GET: Prueba/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prueba prueba = db.Prueba.Find(id);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            return View(prueba);
        }

        // GET: Prueba/Create
        public ActionResult Create()
        {
            ViewBag.IdDocente = new SelectList(db.Usuarios, "ID", "Nombre");
            return View();
        }

        // POST: Prueba/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,FechaCreacion")] Prueba prueba, [Bind(Include ="Docente")] string Docente)
        {
            int id_docente = ObtenerIdDocente(Docente);
            if (ModelState.IsValid)
            {
                prueba.IdDocente = id_docente;
                db.Prueba.Add(prueba);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDocente = new SelectList(db.Usuarios, "ID", "Nombre", prueba.IdDocente);
            return View(prueba);
        }

        // GET: Prueba/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prueba prueba = db.Prueba.Find(id);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDocente = new SelectList(db.Usuarios, "ID", "Nombre", prueba.IdDocente);
            return View(prueba);
        }

        // POST: Prueba/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,FechaCreacion,IdDocente")] Prueba prueba)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prueba).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDocente = new SelectList(db.Usuarios, "ID", "Nombre", prueba.IdDocente);
            return View(prueba);
        }

        // GET: Prueba/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prueba prueba = db.Prueba.Find(id);
            if (prueba == null)
            {
                return HttpNotFound();
            }
            return View(prueba);
        }

        // POST: Prueba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prueba prueba = db.Prueba.Find(id);
            db.Prueba.Remove(prueba);
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

        protected int ObtenerIdDocente (string NombreDocente)
        {
            int id = db.Usuarios.Where(x => x.Correo.Equals(NombreDocente)).Select(x => x.ID).FirstOrDefault();
            return id;
        }
    }
}
