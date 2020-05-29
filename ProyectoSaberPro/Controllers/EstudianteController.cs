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
    public class EstudianteController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Estudiante
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Departamento).Include(u => u.Municipio);
            return View(usuarios.ToList());
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "Codigo", "Nombre");
            ViewBag.CodigoMunicipio = new SelectList(db.Municipios, "Codigo", "Nombre");
            return View();
        }

        // POST: Estudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellido,Direccion,Correo,Celular,Estado,FechaNacimiento,CodigoDepartamento,CodigoMunicipio")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "Codigo", "Nombre", usuario.CodigoDepartamento);
            ViewBag.CodigoMunicipio = new SelectList(db.Municipios, "Codigo", "Nombre", usuario.CodigoMunicipio);
            return View(usuario);
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "Codigo", "Nombre", usuario.CodigoDepartamento);
            ViewBag.CodigoMunicipio = new SelectList(db.Municipios, "Codigo", "Nombre", usuario.CodigoMunicipio);
            return View(usuario);
        }

        // POST: Estudiante/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,Apellido,Direccion,Correo,Celular,Estado,FechaNacimiento,CodigoDepartamento,CodigoMunicipio")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CodigoDepartamento = new SelectList(db.Departamentos, "Codigo", "Nombre", usuario.CodigoDepartamento);
            ViewBag.CodigoMunicipio = new SelectList(db.Municipios, "Codigo", "Nombre", usuario.CodigoMunicipio);
            return View(usuario);
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Estudiante/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
