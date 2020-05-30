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
    public class OpcionController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Opcion
        public ActionResult Index()
        {
            var opciones = db.Opciones.Include(o => o.Pregunta).Where(o => o.OpcionCorrecta == 1);
            //List<OpcionViewModel> lista = new List<OpcionViewModel>();


            // new OpcionViewModel(db)
            return View(opciones.ToList());
        }

        [HttpPost]
        public ActionResult Index(string Pregunta)
        {
            var opciones = db.Opciones.Include(o => o.Pregunta).Where(o => o.OpcionCorrecta == 1);
            if (Pregunta != "")
            {
                opciones = db.Opciones.Include(o => o.Pregunta).Where(o => o.Pregunta.Descripcion.Equals(Pregunta) && o.OpcionCorrecta == 1);
                return View(opciones.ToList());
            }
            opciones = db.Opciones.Include(o => o.Pregunta).Where(o => o.OpcionCorrecta == 1);
            return View(opciones.ToList());
        }

        // GET: Opcion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opciones.Find(id);

            int _id = Convert.ToInt32(id);

            if (opcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPreguntaOpcion = new SelectList(db.Pregunta, "Id", "Descripcion", opcion.IdPreguntaOpcion);
            return View(new OpcionViewModel(db, _id));
        }

        // GET: Opcion/Create
        public ActionResult Create()
        {
            ViewBag.IdPreguntaOpcion = new SelectList(db.Pregunta, "Id", "Descripcion");
            return View(new OpcionViewModel(db));
        }

        // POST: Opcion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OpcionCorrecta,Pregunta,descA,descB,descC,descD")] OpcionViewModel opcionViewModel)

        {
            Opcion objeto_arreglo = new Opcion();
            if (ModelState.IsValid)
            {
                /*Objeto instanciado del modelo Opcion*/

                int id_pregunta = ObtenerIdPregunta(opcionViewModel.Pregunta);

                /*Arreglos auxiliares*/
                string[] array_opciones = { opcionViewModel.descA, opcionViewModel.descB, opcionViewModel.descC, opcionViewModel.descD };
                string[] array_titulo = { "A", "B", "C", "D" };
                int[] opcion_a = { 1, 0, 0, 0 };
                int[] opcion_b = { 0, 1, 0, 0 };
                int[] opcion_c = { 0, 0, 1, 0 };
                int[] opcion_d = { 0, 0, 0, 1 };
                objeto_arreglo.IdPreguntaOpcion = id_pregunta;
                /*Llenado del vector con las cuatro opciones*/
                for (int i = 0; i < array_opciones.Length; i++)
                {

                    objeto_arreglo.Titulo = array_titulo[i];
                    objeto_arreglo.Descripcion = array_opciones[i];
                    /*Validacion para la opc_correcta*/
                    if (opcionViewModel.OpcionCorrecta == "A")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_a[i];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "B")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_b[i];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "C")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_c[i];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "D")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_d[i];
                    }
                    db.Opciones.Add(objeto_arreglo);
                    db.SaveChanges();

                }

                /*Inserción de los datos*/


                return RedirectToAction("Index");
            }

            ViewBag.IdPreguntaOpcion = new SelectList(db.Pregunta, "Id", "Descripcion", objeto_arreglo.IdPreguntaOpcion);
            return View(objeto_arreglo);
        }

        // GET: Opcion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opciones.Find(id);
            //Pregunta _pregunta = db.Pregunta.Find(id);
            //id.GetValueOrDefault
            int _id = Convert.ToInt32(id);

            if (opcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPreguntaOpcion = new SelectList(db.Pregunta, "Id", "Descripcion", opcion.IdPreguntaOpcion);
            return View(new OpcionViewModel(db, _id));
        }

        // POST: Opcion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "opciones.Id,OpcionCorrecta,Pregunta,descA,descB,descC,descD,idAuxiliarPregunta")] OpcionViewModel opcionViewModel)
        {
            int id_pregunta = opcionViewModel.idAuxiliarPregunta;
            if (ModelState.IsValid)
            {
                /*Consulta en LINQ para eliminar las opciones*/
                int[] vector_id_opcion = new int[4];
                int i = 0;
                var id_opcion = db.Opciones.Where(o => o.IdPreguntaOpcion.Equals(id_pregunta));
                foreach (var Opciones in id_opcion)
                {
                    vector_id_opcion[i] = Opciones.Id;
                    i++;

                }
                for (int j = 0; j < vector_id_opcion.Length; j++)
                {
                    Opcion opcion = db.Opciones.Find(vector_id_opcion[j]);
                    db.Opciones.Remove(opcion);
                    db.SaveChanges();
                }
                /*Consulta en LINQ para insertar las nuevas opciones*/
                Opcion objeto_arreglo = new Opcion();
                /*Objeto instanciado del modelo Opcion*/

                /*Arreglos auxiliares*/
                string[] array_opciones = { opcionViewModel.descA, opcionViewModel.descB, opcionViewModel.descC, opcionViewModel.descD };
                string[] array_titulo = { "A", "B", "C", "D" };
                int[] opcion_a = { 1, 0, 0, 0 };
                int[] opcion_b = { 0, 1, 0, 0 };
                int[] opcion_c = { 0, 0, 1, 0 };
                int[] opcion_d = { 0, 0, 0, 1 };
                objeto_arreglo.IdPreguntaOpcion = id_pregunta;
                /*Llenado del vector con las cuatro opciones*/
                for (int h = 0; h < array_opciones.Length; h++)
                {

                    objeto_arreglo.Titulo = array_titulo[h];
                    objeto_arreglo.Descripcion = array_opciones[h];
                    /*Validacion para la opc_correcta*/
                    if (opcionViewModel.OpcionCorrecta == "A")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_a[h];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "B")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_b[h];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "C")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_c[h];
                    }
                    else if (opcionViewModel.OpcionCorrecta == "D")
                    {
                        objeto_arreglo.OpcionCorrecta = opcion_d[h];
                    }
                    db.Opciones.Add(objeto_arreglo);
                    db.SaveChanges();

                }
                return RedirectToAction("Index");
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Opcion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Opcion opcion = db.Opciones.Find(id);
            //Pregunta _pregunta = db.Pregunta.Find(id);
            //id.GetValueOrDefault
            int _id = Convert.ToInt32(id);

            if (opcion == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPreguntaOpcion = new SelectList(db.Pregunta, "Id", "Descripcion", opcion.IdPreguntaOpcion);
            return View(new OpcionViewModel(db, _id));
        }

        // POST: Opcion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string Pregunta = db.Opciones.Where(op => op.Id == id).Select(op => op.Pregunta.Descripcion).FirstOrDefault();
            int id_pregunta = ObtenerIdPregunta(Pregunta);
            int[] vector_id_opcion = new int[4];
            int i = 0;
            var id_opcion = db.Opciones.Where(o => o.IdPreguntaOpcion.Equals(id_pregunta));
            foreach (var Opciones in id_opcion)
            {
                vector_id_opcion[i] = Opciones.Id;
                i++;

            }
            for (int j = 0; j < vector_id_opcion.Length; j++)
            {
                Opcion opcion = db.Opciones.Find(vector_id_opcion[j]);
                db.Opciones.Remove(opcion);
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

        protected int ObtenerIdPregunta(string pregunta)
        {
            int id = db.Pregunta.Where(x => x.Descripcion.Contains(pregunta)).Select(x => x.Id).FirstOrDefault();
            return id;
        }


    }
}
