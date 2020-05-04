using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    public class TablaEntidadExt
    {
        TipoProveedorBaseDatos tipoprovedorbd;

        public TipoProveedorBaseDatos Tipoprovedorbd
        {
            get { return tipoprovedorbd; }
            set { tipoprovedorbd = value; }
        }

        String nombreTablaDestino = "";
        public String NombreTablaDestino
        {
            get { return nombreTablaDestino; }
            set { nombreTablaDestino = value; }
        }

        String tipoTablaDestino = "";
        public String TipoTablaDestino
        {
            get { return tipoTablaDestino; }
            set { tipoTablaDestino = value; }
        }

        String nombreTablaOrigen = "";

        public String NombreTablaOrigen
        {
            get { return nombreTablaOrigen; }
            set { nombreTablaOrigen = value; }
        }
        String tipoTablaOrigen = "";

        public String TipoTablaOrigen
        {
            get { return tipoTablaOrigen; }
            set { tipoTablaOrigen = value; }
        }

        List<ColumnaEntidadExt> columnasEntidadExt = new List<ColumnaEntidadExt>();

        public List<ColumnaEntidadExt> ColumnasEntidadExt
        {
            get { return columnasEntidadExt; }
            set { columnasEntidadExt = value; }
        }



    }
}
