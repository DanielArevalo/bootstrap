using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Util;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad CuadreCartera
    /// </summary>
    [DataContract]
    [Serializable]
    public class CuadreHistorico
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecini { get; set; }
        [DataMember]
        public decimal? saldo_inicial { get; set; }
        [DataMember]
        public decimal? debitos { get; set; }
        [DataMember]
        public decimal? creditos { get; set; }
        [DataMember]
        public decimal? saldo_final { get; set; }
        [DataMember]
        public decimal? diferencia { get; set; }
        [DataMember]
        public decimal? saldo_operativo { get; set; }
        [DataMember]
        public decimal? saldo_contable { get; set; }
    }
}
