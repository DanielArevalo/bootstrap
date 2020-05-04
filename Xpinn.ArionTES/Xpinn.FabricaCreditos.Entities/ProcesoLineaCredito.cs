using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class ProcesoLineaCredito
    {
        [DataMember]
        public int codtipoproceso { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int cod_procesolinea { get; set; }
        [DataMember]
        public string cod_lineacredito { get; set; }
        [DataMember]
        public int checkbox { get; set; }
        [DataMember]
        public string Valor { get; set; }
        // agregado para descuentsolinea
        [DataMember]
        public List<DescuentosCredito> lstDescuentosCredito { get; set; }
    }
}
