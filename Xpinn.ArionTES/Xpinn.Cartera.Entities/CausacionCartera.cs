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
    public class CausacionCartera
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
        public string COD_ATR { get; set; }
        [DataMember]
        public string NOMBRE_ATRIBUTO { get; set; }
        [DataMember]
        public long VALOR_CAUSADO { get; set; }
        [DataMember]
        public long VALOR_ORDEN { get; set; }
        [DataMember]
        public long SALDO_CAUSADO { get; set; }
        [DataMember]
        public long SALDO_ORDEN { get; set; }
        [DataMember]
        public string DIAS_MORA { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
    }
}
