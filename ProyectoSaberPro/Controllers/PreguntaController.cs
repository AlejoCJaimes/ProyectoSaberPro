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
    public class PreguntaController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Pregunta
        public ActionResult Index()
        {
            
            var pregunta = db.Pregunta.Include(p => p.Competencia);
            //var pruebaviewmodel = db.PruebasPreguntas.Include(x => x.IdPrueba).ToList();
            return View(pregunta.ToList());
        }

        // GET: Pregunta/Details/5
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

        // GET: Pregunta/Create
        public ActionResult Create()
        {
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre");
            return View();
        }

        // POST: Pregunta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,IdCompetencia")] Pregunta pregunta, [Bind(Include = "Enunciado,Prueba")] string Enunciado, string Prueba)
        {
            /*Instancia de modelo pruebapregunta*/
            PruebaPregunta pruebaPregunta = new PruebaPregunta();
            Contexto dba = new Contexto();

            /*Consulta ID de la prueba*/
            int id_prueba = ObtenerIdPrueba(Prueba);

            /*Consulta ID Enunciado*/
            int id_enunciado = ObtenerIdEnunciado(Enunciado);

            if (ModelState.IsValid)
            {
                pregunta.IdEnunciado = id_enunciado;
                db.Pregunta.Add(pregunta);
                db.SaveChanges();

                /*start: Insercion tabla transaccional PruebaPregunta*/
                pruebaPregunta.IdPrueba = id_prueba;
                pruebaPregunta.IdPregunta = pregunta.Id;
                dba.PruebasPreguntas.Add(pruebaPregunta);
                dba.SaveChanges();
                /*end: Insercion tabla transaccional PruebaPregunta*/
                return RedirectToAction("Index");

            }
           

            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            return View(pregunta);
        }

        // GET: Pregunta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pregunta pregunta = db.Pregunta.Find(id);
            Enunciado enunciado = new Enunciado();
            Prueba prueba = new Prueba();
            if (pregunta == null)
            {
                return HttpNotFound();
            }
            //var usuarios = db.Usuarios.Include(u => u.Departamento).Include(u => u.Municipio);
            ////string Enunciado = ObtenerTituloEnunciado(id);
            //string Prueba = ObtenerNombrePrueba(id);
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            ViewBag.Enunciado = db.Enunciados.Where(e => e.Id.Equals(pregunta.IdEnunciado)).Select(e => e.TItulo).FirstOrDefault();
            //ViewBag.Prueba = new SelectList(db.Prueba, "Id", "Nombre", prueba.Nombre);
            
            /*ViewBag.IdEnunciado = new SelectList(db.Enunciados, "Id", "TItulo", pregunta.IdEnunciado);*/
            // PreguntaViewModel preguntaViewModel = new PreguntaViewModel();
            
            return View(pregunta);
        }

        // POST: Pregunta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,IdCompetencia")] Pregunta pregunta, [Bind(Include ="enunciado")] string enunciado)
        {
            /*Instancia de modelo pruebapregunta*/
            PruebaPregunta pruebaPregunta = new PruebaPregunta();
            Contexto dba = new Contexto();
            Enunciado _enunciado = new Enunciado();
            /*Consulta ID de la prueba*/
            int id_prueba = ObtenerIdPruebaPregunta(pregunta.Id);

            /*Consulta ID Enunciado*/
            int id_enunciado = ObtenerIdEnunciado(enunciado);
            if (ModelState.IsValid)
            {
                pregunta.IdEnunciado = id_enunciado;
                
                db.Entry(pregunta).State = EntityState.Modified;
                db.SaveChanges();

                /*start: Insercion tabla transaccional PruebaPregunta
                pruebaPregunta.IdPrueba = id_prueba;
                pruebaPregunta.IdPregunta = pregunta.Id;
                dba.Entry(pruebaPregunta).State = EntityState.Modified;
                dba.SaveChanges();
                /*end: Insercion tabla transaccional PruebaPregunta*/
                return RedirectToAction("Index");
            }
            ViewBag.IdCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            ViewBag.Enunciado = db.Enunciados.Where(e => e.Id.Equals(id_enunciado)).Select(e => e.TItulo).FirstOrDefault();
            return View(pregunta);
        }

        // GET: Pregunta/Delete/5
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

        // POST: Pregunta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
           
            Pregunta pregunta = db.Pregunta.Find(id);
           
            db.Pregunta.Remove(pregunta);
            
            db.SaveChanges();
            /*Consulta en LINQ para eliminar las opciones*/
            var id_opcion = db.Opciones.Where(d => d.IdPreguntaOpcion == id);
            foreach (var Opciones in id_opcion)
            {
                db.Opciones.Remove(Opciones);
                db.SaveChanges();
            }
            /*Consulta en LINQ para eliminar el enlace entre PruebasPreguntas*/
            var id_pruebapreguntas = db.PruebasPreguntas.Where(x => x.IdPregunta == id);
            foreach (var _pruebapreguntas in id_pruebapreguntas)
            {
                db.PruebasPreguntas.Remove(_pruebapreguntas);
                db.SaveChanges();
            }
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

        protected int ObtenerIdEnunciado (string Enunciado)
        {
            // int id_enunciado = db.Enunciados.Where(e => e.TItulo.Equals(IdEnunciado)).Select(e => e.Id).Take(1);
            int id = db.Enunciados.Where(e => e.TItulo.Equals(Enunciado)).Select(e => e.Id).FirstOrDefault();
            return id;
        }

        protected int ObtenerIdPrueba(string Prueba)
        {
            // int id_enunciado = db.Enunciados.Where(e => e.TItulo.Equals(IdEnunciado)).Select(e => e.Id).Take(1);
            int id = db.Prueba.Where(e => e.Nombre.Equals(Prueba)).Select(e => e.Id).FirstOrDefault();
            return id;
        }

        
             protected int ObtenerIdPruebaPregunta(int idPregunta)
        {
            // int id_enunciado = db.Enunciados.Where(e => e.TItulo.Equals(IdEnunciado)).Select(e => e.Id).Take(1);
            int id = db.PruebasPreguntas.Where(e => e.IdPregunta.Equals(idPregunta)).Select(e => e.IdPrueba).FirstOrDefault();
            return id;
        }
    }
}
