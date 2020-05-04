
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Icetex.Entities
{
    [DataContract]
    [Serializable]
    public class ListadosIcetex
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string columna { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public byte[] archivo { get; set; }
        [DataMember]
        public string observacion { get; set; }
    }
}
