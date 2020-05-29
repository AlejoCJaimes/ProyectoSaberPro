using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoSaberPro.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    internal sealed class ValidacionFechaUsuario : ValidationAttribute
    {
        public DateTime Mininum = DateTime.Now.AddYears(-110);
        public DateTime Maxinum = DateTime.Now.AddYears(-18);


        public ValidacionFechaUsuario() { }


        public override bool IsValid(object value)
        {
            if (value == null)

                return true;

            var s = value as string;
            if (s != null && string.IsNullOrEmpty(s))

                return true;

            var min = (IComparable)Mininum;
            var max = (IComparable)Maxinum;

            return min.CompareTo(value) <= 0 && max.CompareTo(value) >= 0;
        }

        public override string FormatErrorMessage(string name)
        {
            var msg = string.Format("La fecha de nacimiento debe estar entre {0:dd/MM/yyyy} y  {1:dd/MM/yyyy}", Mininum, Maxinum);
            return msg; //:c 
        }
    }
}