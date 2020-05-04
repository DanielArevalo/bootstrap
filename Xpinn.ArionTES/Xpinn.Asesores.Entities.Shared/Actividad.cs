﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities.Common
{
    [DataContract]
    [Serializable]
    public class Actividad
    {
        [DataMember]
        public Int64 IdActividad { get; set; } // Corresponde al campo de CODACTIVIDAD
        [DataMember]
        public string NombreActividad { get; set; } // Corresponde al campo de DESCRIPCION
    }
}
