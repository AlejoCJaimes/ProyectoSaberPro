using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSaberPro.Models
{
    public class CascadingViewModelMunicipioDepartamento
    {
        //Departamento Departamento = new Departamento();
        //Municipio Municipio = new Municipio();
        public CascadingViewModelMunicipioDepartamento()
        {

            this.Departamento = new List<SelectListItem>();
            this.Municipio = new List<SelectListItem>();
        }//vale

        public List<SelectListItem> Departamento { get; set; }
        public List<SelectListItem> Municipio { get; set; }

        public string CodigoDepartamento { get; set; }
        public string CodigoMunicipio { get; set; }
    }
}