using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    [DataContract]
    [Serializable]
    public class Provision
    {
        [DataMember]
        public int idprovision { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public int cod_atr { get; set; }
        [DataMember]
        public decimal base_provision { get; set; }
        [DataMember]
        public decimal valor_provision { get; set; }
        [DataMember]
        public decimal? porc_provision { get; set; }
        [DataMember]
        public int? tipo_provision { get; set; }
        [DataMember]
        public int? diferencia_provision { get; set; }
        [DataMember]
        public int? diferencia_actual { get; set; }
        [DataMember]
        public int? diferencia_anterior { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public decimal? aporte_resta { get; set; }

        //ADICIONADO
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string cod_categoria_cli { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public decimal vr_provision_anterior { get; set; }

        // Para modificar la clasificación
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public int dias_mora { get; set; }
    }

}