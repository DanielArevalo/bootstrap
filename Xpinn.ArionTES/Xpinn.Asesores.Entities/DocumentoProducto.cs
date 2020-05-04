using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    public class DocumentoProducto
    {
        public DocumentoProducto()
        {

        }

        [DataMember]
        public Int64 NumeroRadicacion { get; set; }
        [DataMember]
        public string Referencia { get; set; }
        [DataMember]
        public string Descripcion { get; set; }

    }
}