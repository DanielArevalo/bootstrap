using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    public class ColumnaEntidadExt
    {

        String nombreDestino = "";

        public String NombreDestino
        {
            get { return nombreDestino; }
            set { nombreDestino = value; }
        }
        String tipoDestino = "";

        public String TipoDestino
        {
            get { return tipoDestino; }
            set { tipoDestino = value; }
        }
        int longitudDestino = 0;

        public int LongitudDestino
        {
            get { return longitudDestino; }
            set { longitudDestino = value; }
        }

        String nombreOrigen = "";

        public String NombreOrigen
        {
            get { return nombreOrigen; }
            set { nombreOrigen = value; }
        }
        String tipoOrigen = "";

        public String TipoOrigen
        {
            get { return tipoOrigen; }
            set { tipoOrigen = value; }
        }
        int longitudOrigen = 0;

        public int LongitudOrigen
        {
            get { return longitudOrigen; }
            set { longitudOrigen = value; }
        }

    }
}
