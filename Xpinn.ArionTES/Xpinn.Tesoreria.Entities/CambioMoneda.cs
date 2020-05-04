using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class CambioMoneda
    {
        [DataMember]
        public Int64 idcambiomoneda { get; set; }
        [DataMember]
        public int cod_moneda_ini { get; set; }
        [DataMember]
        public int cod_moneda_fin { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal valor_compra { get; set; }
        [DataMember]
        public decimal valor_venta { get; set; }
        [DataMember]
        public string nom_monedaOrigen { get; set; }
        [DataMember]
        public string nom_monedaDestino { get; set; }
    }
}
