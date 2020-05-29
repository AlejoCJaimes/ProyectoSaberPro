using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Municipio")]
    public class Municipio
    {
        [Key]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre del municipio no puede exceder 50 caracteres.")]
        public string Nombre { get; set; }

        [ForeignKey("Departamento")]
        public string CodigoDepartamento { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}