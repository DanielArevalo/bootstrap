using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class LibroDiarioColumnario
    {
        [DataMember]      
        public DateTime fecha { get; set; }
        [DataMember]
        public string tipocomp { get; set; } 
        [DataMember]
        public Int64 debito { get; set; }
        [DataMember]
        public Int64 credito { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }   
        [DataMember]
        public Int64 nivel { get; set; }
        [DataMember]
        public Int64 moneda { get; set; }
        [DataMember]
        public Int16 cuentascero { get; set; }
        [DataMember]
        public Int16 cuentasorden { get; set; }
        [DataMember]
        public Int16 comparativo { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public Double valor { get; set; }
        [DataMember]
        public Int64 centro_costo_fin { get; set; }   
    }          
       
}
