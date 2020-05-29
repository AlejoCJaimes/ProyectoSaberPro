using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace ProyectoSaberPro.Models
{
    public class UsuariosViewModel
    {
        public string ErrorMessage { get; set; }
        public string Confirmacion { get; set; }
        public string _opcionCorrecta { get; set; }
        public Usuario objetoCreacion { get; set; }
        public IEnumerable<SelectListItem> Departamento { get; set; }
        public IEnumerable<SelectListItem> Municipio { get; set; }

        public string CodigoDepartamento { get; set; }
        public string CodigoMunicipio { get; set; }
        public IEnumerable<Usuario> ListaUsuarios { get; set; }

        public UsuariosViewModel()
        {

        }
        public UsuariosViewModel(Contexto db)
        {
            //var usuarios = db.Usuarios.Include(u => u.Departamento).Include(u => u.Municipio);
            Departamento = new SelectList(db.Departamentos, "Codigo", "Nombre");
            Municipio = new SelectList(db.Municipios, "Codigo", "Nombre");
            //return View(usuarios.ToList());

            ErrorMessage = null;
            Confirmacion = null;
            objetoCreacion = new Usuario();
            ListaUsuarios = db.Usuarios.ToList();
        }
    }
}