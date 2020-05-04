using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class DetalleComprobante
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public string nombre_cuenta { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public string nombre_cuenta_nif { get; set; }
        [DataMember]
        public Int64? maneja_ter { get; set; }
        [DataMember]
        public Int64 moneda { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public Int64? centro_gestion { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public decimal? valord { get; set; }
        [DataMember]
        public decimal? valorc { get; set; }
        [DataMember]
        public Int64? tercero { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nom_tercero { get; set; }
        [DataMember]
        public string operacion { get; set; }
        [DataMember]
        public int cod_tipo_producto { get; set; }
        [DataMember]
        public string numero_transaccion { get; set; }

        //AGREGADO
        [DataMember]
        public Int64? impuesto { get; set; }
        [DataMember]
        public decimal? base_comp { get; set; }
        [DataMember]
        public decimal? porcentaje { get; set; }
        [DataMember]
        public bool isVisible_base_comp { get; set; }
        [DataMember]
        public bool isVisible_porcentaje { get; set; }
    }
}
