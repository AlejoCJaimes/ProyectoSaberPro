using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models.ViewModelsLogica
{
    public class CompetenciaViewModel
    {
        public string ErrorMessage { get; set; }
        public string Confirmacion { get; set; }
        public Competencia objetoCreacion { get; set; }

        public IEnumerable<Competencia> ListaCompetencia { get; set; }

        public CompetenciaViewModel()
        {

        }
        public CompetenciaViewModel(Contexto db)
        {
          
            ErrorMessage = null;
            Confirmacion = null;
            objetoCreacion = new Competencia();
            ListaCompetencia = db.Competencias.ToList();
        }
    }
}