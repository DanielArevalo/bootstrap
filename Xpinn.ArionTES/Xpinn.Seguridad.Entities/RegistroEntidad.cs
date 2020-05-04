using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    public class RegistroEntidad
    {
        String registro = "";
        String mensaje = "";

        public String Mensaje
        {
            get { return mensaje; }
            set { mensaje = value; }
        }

        public String Registro
        {
            get { return registro; }
            set { registro = value; }
        }
    }
}
