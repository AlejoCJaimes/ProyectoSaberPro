using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Enunciado")]
    public class Enunciado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Título")]
        [StringLength(300, ErrorMessage = "El título no puede exceder los 300 caracteres")]

        public string TItulo { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(7000, ErrorMessage = "La descripción no puede exceder los 7000 caracteres")]

        public string Descripcion { get; set; }
    }
}