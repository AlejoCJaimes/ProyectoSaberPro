using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Resultado")]
    public class Resultado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Puntaje Obtenido")]
        public int PuntajeTotal { get; set; }

        [ForeignKey("Usuario")]
        public int IdEstudiante { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}