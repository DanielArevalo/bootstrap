using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    public class ColumnaEntidad
    {
        String nombre = "";

        public String Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        String tipo = "";

        public String Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }
        int longitud = 0;

        public int Longitud
        {
            get { return longitud; }
            set { longitud = value; }
        }

    }
}
