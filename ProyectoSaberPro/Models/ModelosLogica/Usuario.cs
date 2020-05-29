using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]

        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido")]
        [StringLength(50, ErrorMessage = "El apellido no puede exceder los 50 caracteres")]

        public string Apellido { get; set; }

        [Required]
        [Display(Name = "Dirección")]
        [StringLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres")]

        public string Direccion { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Numero Celular")]
        [StringLength(15, ErrorMessage = "El celular no puede exceder 15 caracteres")]

        public string Celular { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int Estado { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [ValidacionFechaUsuario]

        public DateTime FechaNacimiento { get; set; }

        [ForeignKey("Departamento")]
        public string CodigoDepartamento { get; set; }
        public virtual Departamento Departamento { get; set; }

        [ForeignKey("Municipio")]
        public string CodigoMunicipio { get; set; }
        public virtual Municipio Municipio { get; set; }

       


    }
}