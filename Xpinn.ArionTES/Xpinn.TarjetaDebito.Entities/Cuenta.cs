using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class Cuenta
    {
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string tipocuenta { get; set; }
        [DataMember]
        public string nrocuenta { get; set; }
        [DataMember]
        public decimal saldodisponible { get; set; }
        [DataMember]
        public decimal saldototal { get; set; }
        [DataMember]
        public DateTime fechasaldo { get; set; }
        [DataMember]
        public Int32? cod_oficina { get; set; }
        [DataMember]
        public DateTime? fechaapertura { get; set; }
        //Agregado para solucionar problema de filtro
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public string estado { get; set; }

    }

}
