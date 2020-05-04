using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class PyGComp
    {
        [DataMember]
        public DateTime fechaprimerper { get; set; }
        [DataMember]
        public DateTime fechasegunper { get; set; }
        [DataMember]
        public DateTime? fechatercerper { get; set; }
        [DataMember]
        public Int64 centro_costoini { get; set; }
        [DataMember]
        public Int64 centro_costofin { get; set; }
        [DataMember]
        public Int64 nivel { get; set; }
        [DataMember]
        public decimal balance1 { get; set; }
        [DataMember]
        public decimal porcpart1 { get; set; }
        [DataMember]
        public decimal balance2 { get; set; }
        [DataMember]
        public decimal porcpart2 { get; set; }
        [DataMember]
        public decimal diferencia { get; set; }
        [DataMember]
        public decimal porcdif { get; set; }
        [DataMember]
        public decimal balance3 { get; set; }
        [DataMember]
        public decimal porcpart3 { get; set; }
        [DataMember]
        public decimal diferencia2 { get; set; }
        [DataMember]
        public decimal porcdif2 { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombrecuenta { get; set; }
        [DataMember]
        public Int64 valor1 { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }


    }

}
