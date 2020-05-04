using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities

{
    public class TablaEntidad
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

        List<ColumnaEntidad> columnasEntidad = new List<ColumnaEntidad>();

        public List<ColumnaEntidad> ColumnasEntidad
        {
            get { return columnasEntidad; }
            set { columnasEntidad = value; }
        }
    }
}
