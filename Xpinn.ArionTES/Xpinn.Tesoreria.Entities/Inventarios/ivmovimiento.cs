using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ivmovimiento
    {
        [DataMember]
        public Int64 id_movimiento { get; set; }
        [DataMember]
        public DateTime fecha_movimiento { get; set; }
        [DataMember]
        public int tipo_movimiento { get; set; }
        [DataMember]
        public string nom_tipo_movimiento { get; set; }
        [DataMember]
        public int id_almacen { get; set; }
        [DataMember]
        public string almacenname { get; set; }
        [DataMember]
        public decimal total_costo { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal total_impuesto { get; set; }
        [DataMember]
        public bool seleccionar { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string id_persona { get; set; }
        [DataMember]
        public string nombre_persona { get; set; }
        [DataMember]
        public Int64? cod_proceso { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public Int64? tipo_comp { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public List<ivordenconcepto> LstRetencion { get; set; }
        [DataMember]
        public List<ivdatospago> LstDatosPago { get; set; }
        [DataMember]
        public string numero_factura { get; set; }
    }
}
