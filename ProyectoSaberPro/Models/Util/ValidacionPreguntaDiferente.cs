using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidacionPreguntaDiferente : ValidationAttribute
    {
        private Contexto db = new Contexto();
        public ValidacionPreguntaDiferente() { }


        public override bool IsValid(object cadena)
        {
            if (cadena == null)

                return true;

            var pregunta = cadena as string;
            if (pregunta != null && string.IsNullOrEmpty(pregunta))

                return true;
            //convertir a Camelcase y minuscula
            pregunta = pregunta.Replace(" ", "").ToLower();

            //validacion de consulta//

            string res = ObtenerPregunta(pregunta);
            
            if (pregunta != res)
            {
                return true;
            }
            return false;
        }
        public string ObtenerPregunta(string pregunta)
        {
            string consulta = db.Pregunta.Where(x => x.Descripcion.Replace(" ", "").Equals(pregunta)).Select(x => x.Descripcion.Replace(" ", "").ToLower()).FirstOrDefault();
            return consulta;
        }
        public override string FormatErrorMessage(string name)
        {
            var msg = string.Format("La pregunta ya existe en nuestra base de datos, intenta con otra :)");
            return msg; //:c 
        }
    }
}