using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class provision_programado
    {
        [DataMember]
        public int idprovision { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public String numero_programado { get; set; }

        [DataMember]
        public decimal tasa_interes { get; set; }
        [DataMember]
        public decimal? saldo_base { get; set; }
        [DataMember]
        public decimal intereses { get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public int? dias { get; set; }

        //AGREGADO
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public int? cod_oficina { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public decimal? valor_acumulado { get; set; }


        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public Int64 codusuario { get; set; }
    }
}
