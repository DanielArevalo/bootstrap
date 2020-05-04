using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class Articulo
    {
        [DataMember]
        public Int64 idarticulo { get; set; }
        [DataMember]
        public String serial { get; set; }
        [DataMember]
        public String descripcion { get; set; }

        [DataMember]
        public String referencia { get; set; }

        [DataMember]
        public String marca { get; set; }

        [DataMember]
        public Int64 idtipo_articulo { get; set; }

        [DataMember]
        public Int64 Cantidad { get; set; }

        


    }
}

