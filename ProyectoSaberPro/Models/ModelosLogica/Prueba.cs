using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [Table("Prueba")]
    public class Prueba
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre de la prueba no puede exceder 25 caracteres.")]

        public string Nombre { get; set; }

        
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [ForeignKey("Usuario")]

        public int IdDocente { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}