using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Xpinn.Seguridad.Entities
{
    public class RegistrosEntidad
    {
        List<RegistroEntidad> registrosExitosos = new List<RegistroEntidad>();

        public List<RegistroEntidad> RegistrosExitosos
        {
            get { return registrosExitosos; }
            set { registrosExitosos = value; }
        }
        List<RegistroEntidad> registrosFallidos = new List<RegistroEntidad>();

        public List<RegistroEntidad> RegistrosFallidos
        {
            get { return registrosFallidos; }
            set { registrosFallidos = value; }
        }
    }
}
