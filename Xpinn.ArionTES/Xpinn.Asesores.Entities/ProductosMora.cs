using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Atributo
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProductosMora
    {
        [DataMember]
        public string periodo { get; set; }
        [DataMember]
        public string numero_producto { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime? fecha_vencimento { get; set; }
        [DataMember]
        public Int32? dias { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public decimal? capital { get; set; }
        [DataMember]
        public decimal? extras { get; set; }
        [DataMember]
        public decimal? interes { get; set; }
        [DataMember]
        public decimal? mora { get; set; }
        [DataMember]
        public decimal? otros { get; set; }
        [DataMember]
        public decimal? saldo_total { get; set; }
        [DataMember]
        public Int32? tipo_producto { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }

    }
}