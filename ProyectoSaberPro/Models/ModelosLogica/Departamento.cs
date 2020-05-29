using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [Display(Name ="Código")]

        public string Codigo { get; set; }

        [Required]
        [Display(Name="Nombre")]
        [StringLength(50,ErrorMessage ="El nombre del departamento no puede excede 50 caracteres.")]

        public string Nombre { get; set; }
    }
}