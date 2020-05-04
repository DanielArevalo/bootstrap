using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class ProductoPorPagar
    {
        [DataMember]
        public long cod_persona { get; set; }

        [DataMember]
        public int tipo_producto { get; set; }

        [DataMember]
        public string descr_tipo_producto { get; set; }

        [DataMember]
        public int id_linea { get; set; }

        [DataMember]
        public string descr_linea { get; set; }

        [DataMember]
        public Int64 id_producto { get; set; }

        [DataMember]
        public DateTime fecha_prox_pago { get; set; }

        [DataMember]
        public decimal valor_a_pagar { get; set; }        
    }

    [DataContract]
    [Serializable]
    public class ProductoOrigenPago
    {
        [DataMember]
        public long cod_persona { get; set; }

        [DataMember]
        public int tipo_producto { get; set; }

        [DataMember]
        public string descr_tipo_producto { get; set; }

        [DataMember]
        public int id_linea { get; set; }

        [DataMember]
        public string descr_linea { get; set; }

        [DataMember]
        public Int64 id_producto { get; set; }
        
        [DataMember]
        public decimal disponible { get; set; }
    }

    [DataContract]
    [Serializable]
    public class PagoInterno
    {
        [DataMember]
        public long cod_persona { get; set; }

        [DataMember]
        public int origen_tipo_producto { get; set; }

        [DataMember]
        public Int64 origen_id_producto { get; set; }

        [DataMember]
        public int destino_tipo_producto { get; set; }

        [DataMember]
        public Int64 destino_id_producto { get; set; }

        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public int id { get; set; }

        //Datos consulta
        [DataMember]
        public string nombres { get; set; }

        [DataMember]
        public string identificacion { get; set; }

        [DataMember]
        public string asesor { get; set; }

        [DataMember]
        public DateTime fecha { get; set; }

        [DataMember]
        public string nom_tipo_origen { get; set; }

        [DataMember]
        public string nom_tipo_destino { get; set; }

        [DataMember]
        public string nom_linea_origen { get; set; }

        [DataMember]
        public string nom_linea_destino { get; set; }

        [DataMember]
        public int cod_ope { get; set; }
    }
}