using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class SaldosTerceros
    {
        [DataMember]      
        public DateTime fechaini { get; set; }
        [DataMember]
        public DateTime fechafin { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public Int64? centro_gestion{ get; set; }  
        [DataMember]
        public string codtercero { get; set; }
        [DataMember]
        public string identercero { get; set; }
        [DataMember]
        public string nombretercero { get; set; }
        [DataMember]
        public Double saldo { get; set; }
        [DataMember]
        public Double saldoinicial { get; set; }
        [DataMember]    
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public Double debito { get; set; }
        [DataMember]
        public Double credito { get; set; }
        [DataMember]
        public Double saldofinal { get; set; }
        [DataMember]
        public Int64 centro_costo_fin { get; set; }
        [DataMember]
        public Int64 consolidado { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }

        //Agregado para traslado de saldos de terceros
        [DataMember]
        public Int64 cod_traslado { get; set; }
        [DataMember]
        public Int64 cod_traslado_det { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
    }          
       
}
