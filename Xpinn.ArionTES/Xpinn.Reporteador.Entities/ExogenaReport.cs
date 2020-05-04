using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Reporteador.Entities
{
    [DataContract]
    [Serializable]
    public class ExogenaReport
    {
   
        [DataMember]
        public string separador { get; set; }
        [DataMember]
        public int idlinea { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public Int64 codconcepto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string Formato { get; set; }
        [DataMember]
        public Int64 idhomologa { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
    }
   
}
