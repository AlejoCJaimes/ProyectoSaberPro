using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSaberPro.Models
{
    /*Este viewModel se utiliza para incluir el enunciado
     * y la prueba*/
    public class PreguntaViewModel
    {
        public string _Enunciado { get; set; }
        public string _Prueba { get; set; }
        
        public Pregunta objeto = new Pregunta();

        public IEnumerable<Pregunta> ListaPreguntas { get; set; }
        public IEnumerable<SelectListItem> ListaCompetencia { get; set; }
        public PreguntaViewModel ()
        {

        }

        public PreguntaViewModel (Pregunta pregunta/*, string Enunciado, string Prueba*/, Contexto db)
        {
            ListaCompetencia = new SelectList(db.Competencias, "Id", "Nombre", pregunta.IdCompetencia);
            //var usuarios = db.Usuarios.Include(u => u.Departamento).Include(u => u.Municipio);
            //_Enunciado = Enunciado;
            // _Prueba = Prueba;
            //return View(usuarios.ToList());
            objeto = pregunta;
            ListaPreguntas = db.Pregunta.ToList();
        }
    }
}