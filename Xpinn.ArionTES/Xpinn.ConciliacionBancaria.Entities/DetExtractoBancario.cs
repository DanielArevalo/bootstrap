using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ConciliacionBancaria.Entities
{
    [DataContract]
    [Serializable]
    public class DetExtractoBancario
    {
        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public Int64 idextracto { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public string cod_concepto { get; set; }
        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public string num_documento { get; set; }
        [DataMember]
        public string referencia1 { get; set; }
        [DataMember]
        public string referencia2 { get; set; }
        [DataMember]
        public decimal? valor { get; set; }


        //AGREGADO
        [DataMember]
        public string descripcion { get; set; }
    }
}