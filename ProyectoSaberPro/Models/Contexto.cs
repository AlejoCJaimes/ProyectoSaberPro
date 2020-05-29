using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    public class Contexto: DbContext
    {
        public Contexto() : base("DefaultConnection") { }

        //Dbset Manejo de Roles, uso de Identity, configuracion de mensajes SMTP ROL a cargo: Usuarios
        /* Camilo Valencia
         * John Villamizar*/
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet <Departamento> Departamentos { get; set; }
        public DbSet<Municipio> Municipios { get; set; }

        //Dbset Creacion de Preguntas, Asignacion de Enunciados, Competencias, Opciones e interfaces Complementarias de Docente ROL a cargo: Docente
        /* Alejandro Jaimes*/
        public DbSet<Prueba> Prueba { get; set; }
        public DbSet<Pregunta> Pregunta { get; set; }
        public DbSet<Competencia> Competencias { get; set; }
        public DbSet<Opcion> Opciones { get; set; }
        public DbSet<Enunciado> Enunciados { get; set; }
        public DbSet<PruebaPregunta> PruebasPreguntas { get; set; }

        //Dbset Creacion de Modulo de presentar pruebas, exposición de preguntas, y registro de resultados para el estudiante. ROL a cargo: Estudiante
        /* Johan Rangel*/
        public DbSet<PreguntaUsuario> PreguntaUsuarios { get; set; }
        public DbSet<Resultado> Resultados { get; set; }
        public DbSet<ResultadoEstudiante> ResultadoEstudiantes { get; set; }
        
   
       

    }
}