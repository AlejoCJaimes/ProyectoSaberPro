using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("ResultadoEstudiante")]
    public class ResultadoEstudiante
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Opcion")]
        public int IdOpcion { get; set; }
        public virtual Opcion Opcion { get; set; }

        [ForeignKey("Resultado")]
        public int IdResultado { get; set; }
        public virtual Resultado Resultado { get; set; }
    }
}