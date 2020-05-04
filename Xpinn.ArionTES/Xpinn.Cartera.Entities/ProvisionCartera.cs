using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad ArqueoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProvisionCartera
    {
        [DataMember]
        public string FECHA_HISTORICO { get; set; }
        [DataMember]
        public string NUMERO_RADICACION { get; set; }
        [DataMember]
        public string COD_LINEA_CREDITO { get; set; }
        [DataMember]
        public string NOMBRE_LINEA { get; set; }
        [DataMember]
        public string IDENTIFICACION { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public string CLASIFICACION { get; set; }
        [DataMember]
        public string FORMA_PAGO { get; set; }
        [DataMember]
        public string TIPO_GARANTIA { get; set; }
        [DataMember]
        public string COD_CATEGORIA { get; set; }
        [DataMember]
        public string COD_ATR { get; set; }
        [DataMember]
        public string NOMBRE_ATRIBUTO { get; set; }
        [DataMember]
        public string PORC_PROVISION { get; set; }
        [DataMember]
        public long VALOR_PROVISION { get; set; }
        [DataMember]
        public long DIFERENCIA_PROVISION { get; set; }
        [DataMember]
        public long DIFERENCIA_ACTUAL { get; set; }
        [DataMember]
        public long DIFERENCIA_ANTERIOR { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public decimal valor_sinlibranza { get; set; }
        [DataMember]
        public decimal valor_provision_sinlibranza { get; set; }
        [DataMember]
        public decimal provision_sinlibranza { get; set; }
        [DataMember]
        public decimal valor_conlibranza { get; set; }
        [DataMember]
        public decimal valor_provision_conlibranza { get; set; }
        [DataMember]
        public decimal provision_conlibranza { get; set; }

    }
}
