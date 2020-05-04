using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class Reintegro
    {
        [DataMember]
        public Int64 IdReintegro { get; set; }
        [DataMember]
        public Int64 codreintegro { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public DateTime fechareintegro { get; set; }
        [DataMember]
        public DateTime fechaarqueo { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public string nomcaja { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nomcajero { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public Int64 cod_banco { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public Decimal valor_reintegro { get; set; }
        [DataMember]
        public Int64 esprincipal { get; set; }
        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public string nomciudad { get; set; }
        [DataMember]
        public string ip { get; set; }
    }
}
