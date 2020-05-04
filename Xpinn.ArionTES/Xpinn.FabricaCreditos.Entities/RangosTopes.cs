using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad rangos de topes de Lineas de Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class RangosTopes
    {
        [DataMember]
        public Int64 idtope { get; set; }
        [DataMember]
        public Int64 cod_rango_atr { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public Int64 tipo_tope { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public String descripcion { get; set; }
        [DataMember]
        public List<RangosTopes> lstRangosTopes { get; set; }

        //Agregado para parametrizacion de rangos
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public Int64 tipo_valor { get; set; }
        [DataMember]
        public String nom_tipo_valor { get; set; }
        [DataMember]
        public Decimal valor { get; set; }
    }
}
