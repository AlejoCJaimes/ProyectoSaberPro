using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoSaberPro.Models;


namespace ProyectoSaberPro.Controllers
{
    public class HomeController : Controller
    {
        CascadingViewModelMunicipioDepartamento model = new CascadingViewModelMunicipioDepartamento();
        private Contexto db = new Contexto();

        //método que devuelve los departamentos.
        public void getDepartamentos()
        {
            var departamentos = db.Departamentos.ToList();
            model.Departamento = new List<SelectListItem>();
            for (int i = 0; i < departamentos.Count; i++)
            {
                SelectListItem obj = new SelectListItem();
                obj.Value = departamentos[i].Codigo;
                obj.Text = departamentos[i].Codigo;
                model.Departamento.Add(obj);
            }
        }

        [HttpPost]
        public JsonResult AjaxMethod(string value)
        {


            var municipios = db.Municipios.Where(x => x.CodigoDepartamento == value).ToList(); //esperanding...
            model.Municipio = new List<SelectListItem>();
            for (int i = 0; i < municipios.Count; i++)
            {
                SelectListItem obj = new SelectListItem();
                obj.Value = municipios[i].Codigo;
                obj.Text = municipios[i].Nombre;
                model.Municipio.Add(obj);

            }


            return Json(model);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*Test Jquery*/
       public ActionResult ObtenerOpcion (string pregunta, string opcion)
        {
            return Json(new { message = "OK" });
        }
       
        /*AutoComplete*/
        public JsonResult BuscarEnunciado(string term)
        {
            //consulta
            var listaEnunciados = db.Enunciados.Where(x => x.TItulo.Contains(term)).Select(x => x.TItulo).ToList();
            if (listaEnunciados.Count() > 0)
            {
                return Json(listaEnunciados, JsonRequestBehavior.AllowGet);
            }
            else
            {
                listaEnunciados.Clear();
                string sms = "No se encontraron resultados :c";
                listaEnunciados.Add(sms);
            }
            return Json(listaEnunciados, JsonRequestBehavior.AllowGet);
        }
        /*public JsonResult BuscarEnunciado(string term, string idcombo)
        {
            //consulta
            var listaEnunciados = db.Pregunta.Where(x => x.Enunciado.TItulo.Contains(term) && x.Competencia.Descripcion.Contains(idcombo)).Select(x => x.Enunciado.TItulo).ToList();
            //var listaEnunciados = db.Enunciados.Where(x => x.TItulo.Contains(term)).Select(x => x.TItulo).ToList();
            if (listaEnunciados.Count() > 0)
            {
                return Json(listaEnunciados, JsonRequestBehavior.AllowGet);
            }
            else
            {
                listaEnunciados.Clear();
                string sms = "No se encontraron resultados :c";
                listaEnunciados.Add(sms);
            }
            return Json(listaEnunciados, JsonRequestBehavior.AllowGet);
        }*/
        public JsonResult BuscarPrueba(string term)
        {
            //consulta
            var listaPruebas = db.Prueba.Where(x => x.Nombre.StartsWith(term)).Select(x => x.Nombre).Take(5).ToList();
            if (listaPruebas.Count() > 0)
            {
                return Json(listaPruebas, JsonRequestBehavior.AllowGet);
            }
            else
            {
                listaPruebas.Clear();
                string sms = "No se encontraron resultados :c";
                listaPruebas.Add(sms);
            }
            return Json(listaPruebas, JsonRequestBehavior.AllowGet);
        }


        public JsonResult BuscarPregunta(string term)
        {
            var _listaPreguntas = db.Pregunta.Where(x => x.Descripcion.Contains(term)).Select(x => x.Descripcion).ToList();
            if (_listaPreguntas.Count() > 0)
            {
                return Json(_listaPreguntas, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _listaPreguntas.Clear();
                string sms = "No se encontraron resultados :c";
                _listaPreguntas.Add(sms);
            }
            return Json(_listaPreguntas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarPreguntaOpcion(string term)
        {
            var preguntaOpcion = db.Opciones.Where(x => x.Pregunta.Descripcion.Contains(term) && x.OpcionCorrecta == 1).Select(x => x.Pregunta.Descripcion).ToList();
            if (preguntaOpcion.Count()>0)
            {
                return Json(preguntaOpcion, JsonRequestBehavior.AllowGet);
            } else
            {
                preguntaOpcion.Clear();
                string sms = "No se encontraron resultados :c";
                preguntaOpcion.Add(sms); 
            }
            
            return Json(preguntaOpcion, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarPreguntasDisponibles(string term)
        {
            /*Creamos una lista y dos vectores:
             * La lista nos devuelve la cantidad de preguntas en forma de string que se
             * encuentran disponibles en la entidad Pregunta
             * Respecto a los vectores, vector_id_opcion retornará el ID de las preguntas que ya
             * están enlazadas con una opción, con el fin de reconocer cuales están ocupadas.
             * El vector_preguntas, traerá el ID de todas las preguntas que están en la entidad
             * preguntas. Esto con el fin de si hay nuevas, conocer su ID. */

            var preguntas = db.Pregunta.Select(p => p.Descripcion).ToList();
            int[] vector_id_opcion = ObtenerIdOpcionesPregunta();
            int[] vector_preguntas = ObtenerIdPreguntas();


            /*Revisamos si la longitud del vector de las preguntas es mayor que el vector
             * de las IdPreguntaOpciones, de esta manera encontraremos cuales
             * opciones son diferentes y por lo tanto serian las disponibles
             */

            if (vector_preguntas.Length > vector_id_opcion.Length)
            {
                /*El método ObtenerIdPreguntasDisponibles me devuelve una diferencia
                 * entre ambos vectores, esto con el fin de obtner los ID que están en 
                 * la entidad pregunta, pero no en la entidad Opciones, se retorna
                 a través de un dato tipo IEnumeralble<int>*/
                var diferencia = ObtenerIdPreguntasDisponibles(vector_preguntas, vector_id_opcion);
                preguntas.Clear();

                foreach (var IdEncontrado in diferencia)
                {

                    /*Realizamos una consulta auxiliar que me retorna 
                     la descripción de la pregunta que está disponible*/
                    var aux_preguntas_disponibles = db.Pregunta.Where(x => x.Id.Equals(IdEncontrado) && x.Descripcion.Contains(term)).Select(x => x.Descripcion).ToList();

                    preguntas.AddRange(aux_preguntas_disponibles);
                }
                preguntas.Contains(term);
                preguntas.Count();

                /*Por ultimo hacemos el filtrado por el campo que está entrando sobre la lista
                 * que contiene ya las preguntas disponibles*/
                return Json(preguntas, JsonRequestBehavior.AllowGet);


            }
            else if (vector_preguntas.Length == vector_id_opcion.Length)
            {
                var sms_notFound = "No hay preguntas disponibles";
                return Json(sms_notFound, JsonRequestBehavior.AllowGet);

            }
            else if (vector_id_opcion.Length == 0)
            {
                var preguntas_disponibles_ = db.Pregunta.Where(x => x.Descripcion.Contains(term)).Select(x => x.Descripcion).ToList();

                return Json(preguntas_disponibles_, JsonRequestBehavior.AllowGet);
            }


            return Json("No hay preguntas creadas", JsonRequestBehavior.AllowGet);
        }


        protected IEnumerable<int> ObtenerIdPreguntasDisponibles(int[] vector_preguntas, int[] vector_id_opcion)
        {
            /*Encontramos el Id diferente entre ambos para poder realizar la 
             * selección en el método principal*/
            var diferencia = vector_preguntas.Except(vector_id_opcion);
            return diferencia;
        }

        protected int[] ObtenerIdPreguntas()
        {

            int i = 0;
            //Preguntas disponibles
            var _listaPreguntas = db.Pregunta.Where(x => x.Id > 1);
            //Vector para llenar el Id de las preguntas.
            int[] vector_preguntas = new int[_listaPreguntas.Count()];
            //string[] vector_des_preguntas = new string[_listaPreguntas.Count()];

            var preguntas_completas = db.Pregunta.Select(x => x.Descripcion).ToList();
            foreach (var Pregunta in _listaPreguntas)
            {
                vector_preguntas[i] = Pregunta.Id;

                i++;
            }
            return vector_preguntas;
        }

        protected int[] ObtenerIdOpcionesPregunta()
        {

            int i = 0;

            //Encontrar y llenar las preguntas que ya tienen opcion

            var lista_opcionesPregunta = db.Opciones.Where(o => o.OpcionCorrecta == 1);

            //Crear vector dinámico para almacenar el idPreguntaOpcion de la entidad Opcion.
            int[] vector_id_opcion = new int[lista_opcionesPregunta.Count()];
            foreach (var Opciones in lista_opcionesPregunta)
            {
                vector_id_opcion[i] = Opciones.IdPreguntaOpcion;
                //vector_descripcion_opcion[i] = Opciones.Descripcion;
                i++;

            }

            return vector_id_opcion;
        }

        /*private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public static long CurrentTimeMillis()
        {

            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }*/

    }
}