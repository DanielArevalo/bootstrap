using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
    public class Etran_Programado
    {
        [DataMember]
        public Int64 COD_OPE { get; set; }
        [DataMember]
        public String NUMERO_PROGRAMADO { get; set; }
        [DataMember]
        public Int64 COD_CLIENTE { get; set; }
        [DataMember]
        public Int64 TIPO_TRAN { get; set; }
        [DataMember]
        public Int64 COD_DET_LIS { get; set; }
        [DataMember]
        public String DOCUMENTO_SOPORTE { get; set; }
        [DataMember]
        public Decimal VALOR { get; set; }
        [DataMember]
        public Int64 ESTADO { get; set; }
        [DataMember]
        public Int64 NUM_TRAN_ANULA { get; set; }
        [DataMember]
        public DateTime Fecha_Interes { get; set; }
    }
}
