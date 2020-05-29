using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Opcion")]
    public class Opcion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        [StringLength(5, ErrorMessage = "la opcion no puede exceder los 5 caracteres.")]
        public string Titulo { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        [StringLength(200, ErrorMessage = "la opcion puede exceder los 200 caracteres.")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Opcion Correcta")]
        public int OpcionCorrecta { get; set; }

        [ForeignKey("Pregunta")]
        public int IdPreguntaOpcion { get; set; }
        public virtual Pregunta Pregunta { get; set; }
        
    }
}