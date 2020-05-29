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
    public class PreguntaUsuarioController : Controller
    {
        private Contexto db = new Contexto();

        // GET: PreguntaUsuario
        public ActionResult Index()
        {
            var preguntaUsuarios = db.PreguntaUsuarios.Include(p => p.Pregunta).Include(p => p.Usuario);
            return View(preguntaUsuarios.ToList());

            /*Hacer consulta para devolver las pruebas y el numero de pruebas.*/
        }

        public ActionResult IndexPresentarPrueba ()
        {
            var pruebas = db.PruebasPreguntas.Include(p => p.Prueba);
            ViewBag.NumeroPreguntas = db.PruebasPreguntas.Where(pr => pr.IdPregunta == pr.Pregunta.Id).Count();
            return View(pruebas.ToList());
        }

        public ActionResult PresentarPrueba (int? id)
        {
            PruebaPOViewModel Prueba = new PruebaPOViewModel();
            var datos = db.PruebasPreguntas.Include(x => x.Prueba).Include(x => x.Pregunta).Where(x => x.Pregunta.Id.Equals(id))
            var _opciones = db.Opciones.Where(op => op.Id == op.IdPreguntaOpcion).ToList();

            var _competencias = db.Competencias.ToList(); ;
            var competencias = db.Competencias.ToList();
            var enunciados = db.Enunciados.ToList();
            var preguntas = db.Pregunta.ToList();
            var opciones = db.Opciones.ToList();

            Prueba.Competencias = competencias;
            Prueba.Enunciados = enunciados;
            Prueba.Preguntas = preguntas;
            Prueba.Opciones = opciones;

            return View(Prueba);

        }
        //metodo presentar prueba
        public ActionResult PresentarPrueba()
        {

            PruebaPOViewModel Prueba = new PruebaPOViewModel();

            var competencias = db.Competencias.ToList();
            var enunciados = db.Enunciados.ToList();
            var preguntas = db.Pregunta.ToList();
            var opciones = db.Opciones.ToList();

            Prueba.Competencias = competencias;
            Prueba.Enunciados = enunciados;
            Prueba.Preguntas = preguntas;
            Prueba.Opciones = opciones;

            return View(Prueba);
        }
        [HttpPost]
        public ActionResult AttachValuesProof (IEnumerable<string> results)
        {
            return View("PresentarPrueba");
        }
        
        // GET: PreguntaUsuario/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaUsuario preguntaUsuario = db.PreguntaUsuarios.Find(id);
            if (preguntaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(preguntaUsuario);
        }

        // GET: PreguntaUsuario/Create
        public ActionResult Create()
        {
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "ID", "Nombre");
            return View();
        }

        // POST: PreguntaUsuario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdPregunta,IdUsuario")] PreguntaUsuario preguntaUsuario)
        {
            if (ModelState.IsValid)
            {
                db.PreguntaUsuarios.Add(preguntaUsuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", preguntaUsuario.IdPregunta);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "ID", "Nombre", preguntaUsuario.IdUsuario);
            return View(preguntaUsuario);
        }

        // GET: PreguntaUsuario/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaUsuario preguntaUsuario = db.PreguntaUsuarios.Find(id);
            if (preguntaUsuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", preguntaUsuario.IdPregunta);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "ID", "Nombre", preguntaUsuario.IdUsuario);
            return View(preguntaUsuario);
        }

        // POST: PreguntaUsuario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdPregunta,IdUsuario")] PreguntaUsuario preguntaUsuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntaUsuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPregunta = new SelectList(db.Pregunta, "Id", "Descripcion", preguntaUsuario.IdPregunta);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "ID", "Nombre", preguntaUsuario.IdUsuario);
            return View(preguntaUsuario);
        }

        // GET: PreguntaUsuario/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PreguntaUsuario preguntaUsuario = db.PreguntaUsuarios.Find(id);
            if (preguntaUsuario == null)
            {
                return HttpNotFound();
            }
            return View(preguntaUsuario);
        }

        // POST: PreguntaUsuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PreguntaUsuario preguntaUsuario = db.PreguntaUsuarios.Find(id);
            db.PreguntaUsuarios.Remove(preguntaUsuario);
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
