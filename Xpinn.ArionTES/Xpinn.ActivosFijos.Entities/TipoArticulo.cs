using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    

    public class TipoArticulo
    {
        [DataMember]
        public Int64 IdTipo_Articulo { get; set; }
        [DataMember]
        public String Descripcion { get; set; }
        [DataMember]
        public Int64? Dias_Periodicidad { get; set; }


    }
}

