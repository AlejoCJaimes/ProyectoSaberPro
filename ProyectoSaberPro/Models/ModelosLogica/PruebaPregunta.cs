using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("PruebaPregunta")]
    public class PruebaPregunta
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Prueba")]
        public int IdPrueba { get; set; }
        public virtual Prueba Prueba { get; set; }

        [ForeignKey("Pregunta")]
        public int IdPregunta { get; set; }
        public virtual Pregunta Pregunta { get; set; }

    }
}