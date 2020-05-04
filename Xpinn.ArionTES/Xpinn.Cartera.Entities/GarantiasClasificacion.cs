using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad Efecto Garantias sobre la Provisión
    /// </summary>
    [DataContract]
    [Serializable]
    public class GarantiasClasificacion
    {
        [DataMember]
        public int idgarantia { get; set; }
        [DataMember]
        public int cod_clasifica { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int tipo_garantia { get; set; }
        [DataMember]
        public string nom_tipo_garantia { get; set; }        
        [DataMember]
        public int? dias_inicial { get; set; }
        [DataMember]
        public int? dias_final { get; set; }
        [DataMember]
        public decimal porcentaje { get; set; }
    }
}
