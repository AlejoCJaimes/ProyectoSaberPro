using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Pregunta")]
    public class Pregunta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Descripcion")]
        [StringLength(800, ErrorMessage = "La descripción no puede exceder los 800 caracteres")]
        [ValidacionPreguntaDiferente]

        public string Descripcion { get; set; }

        [ForeignKey("Competencia")]
        public int IdCompetencia { get; set; }
        public virtual Competencia Competencia { get; set; }

        [ForeignKey("Enunciado")]
        public int IdEnunciado { get; set; }
        public virtual Enunciado Enunciado { get; set; }

        

        


    }
}