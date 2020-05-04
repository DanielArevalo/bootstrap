using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Ahorros.Entities
{
    [DataContract]
    [Serializable]
    public class CuentasExenta
    {
        [DataMember]
        public Int64 idexenta { get; set; }
        [DataMember]
        public string numero_cuenta { get; set; }
        [DataMember]
        public int? tipo_cuenta { get; set; }
        [DataMember]
        public int cod_tipo_cuenta { get; set; } // Se colocó porque no deja cargar datos en tipo_cuenta
        [DataMember]
        public DateTime? fecha_exenta { get; set; }
        [DataMember]
        public decimal? monto { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }

        //AGREGADO
        [DataMember]
        public List<CuentasExenta> lstCuentas { get; set; }
        [DataMember]
        public string cod_linea { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }

        [DataMember]
        public string nom_tipocuenta { get; set; }
    }

}