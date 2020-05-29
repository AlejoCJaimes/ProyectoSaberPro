using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    public class PruebaPOViewModel
    {
        public PruebaPOViewModel()
        {

        }

        public List<Enunciado> Enunciados { get; set; }
        public List<Competencia> Competencias { get; set; }
        public List<Pregunta> Preguntas { get; set; }
        public List<Opcion> Opciones { get; set; }

        public List<string> OpcionesEstudiante { get; set; }

    }
}
