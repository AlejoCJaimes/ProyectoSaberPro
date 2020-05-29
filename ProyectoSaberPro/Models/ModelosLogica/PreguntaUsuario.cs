using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("PreguntaUsuario")]
    public class PreguntaUsuario
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Pregunta")]
        public int IdPregunta { get; set; }
        public virtual Pregunta Pregunta { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

    }
}