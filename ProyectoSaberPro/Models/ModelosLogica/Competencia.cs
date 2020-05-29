using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Competencia")]
    public class Competencia
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 25 caracteres")]

        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres")]

        public string Descripcion { get; set; }
    }
}