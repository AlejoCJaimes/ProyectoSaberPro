using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    public class OpcionViewModel
    {
        Contexto db = new Contexto();
        public string OpcionCorrecta {get; set;}
        public string descA { get; set; }
        public string descB { get; set; }
        public string descC { get; set; }
        public string descD { get; set; }
        public string Pregunta { get; set; }
        public int idAuxiliarPregunta { get; set; }

        public Opcion opciones = new Opcion();

        public IEnumerable<Opcion> ListaOpciones { get; set; }
        public List<Pregunta> ListaPreguntas { get; set; }
        
        public OpcionViewModel () { }

        public OpcionViewModel(Contexto db)
        {
            Pregunta pregunta = new Pregunta();

            ListaPreguntas = db.Pregunta.ToList();
            
            opciones = new Opcion();
            ListaOpciones = db.Opciones.ToList();
        }

        public OpcionViewModel(Contexto db, int _id)
        {
            /*Consulta en LINQ para eliminar las opciones*/
            int[] vector_id_opcion = new int[4];
            string[] vector_descripcion_opcion = new string[4];
            int i = 0;
            
            int id_preguntaOpcion = db.Opciones.Where(d => d.Id == _id).Select(d => d.IdPreguntaOpcion).FirstOrDefault();
            var id_opcion = db.Opciones.Where(o => o.IdPreguntaOpcion.Equals(id_preguntaOpcion));
            foreach (var Opciones in id_opcion)
            {
                vector_id_opcion[i] = Opciones.Id;
                vector_descripcion_opcion[i] = Opciones.Descripcion;
                
                i++;

            }
            /*Llenado de opciones*/
            descA = vector_descripcion_opcion[0];
            descB = vector_descripcion_opcion[1];
            descC = vector_descripcion_opcion[2];
            descD = vector_descripcion_opcion[3];

            /*Llenado de Pregunta y Opcion Correcta*/
            Pregunta = db.Opciones.Where(op => op.Id == _id).Select(op => op.Pregunta.Descripcion).FirstOrDefault();
             idAuxiliarPregunta = db.Opciones.Where(op => op.Id == _id).Select(op => op.Pregunta.Id).FirstOrDefault();
            //Pregunta =Convert.ToString(_idAuxiliarPregunta);
            OpcionCorrecta = db.Opciones.Where(op => op.Id == _id).Select(op => op.Titulo).FirstOrDefault();

            opciones = new Opcion();
            //Opcion _opcion = db.Opciones.Find(_id);
            ListaOpciones = db.Opciones.ToList();
            
        }

        public string OpcionA (int id)
        {
            string opcionA = db.Opciones.Where(x => x.Pregunta.Id == id).Select(x => x.Descripcion).FirstOrDefault();
            
            return opcionA;
        }

    }
}