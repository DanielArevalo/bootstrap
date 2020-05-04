using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class TipoOperacion
    {
        // Atributos para el manejo del tipo de operaciòn
        [DataMember]
        public Int64 IdTipoOpe { get; set; }
        [DataMember]
        public string cod_operacion { get; set; }
        [DataMember]
        public string nom_tipo_operacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 conteo { get; set; }
        [DataMember]
        public Int64 tipo_movimiento { get; set; }
        [DataMember]
        public decimal? porc_imp { get; set; }

        // Atributos para el manejo de tipo de producto
        [DataMember]
        public Int64 tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }

        // Atributos para el manejo de tipo de transaccion
        [DataMember]
        public Int64 tipo_tran { get; set; }
        [DataMember]
        public string nom_tipo_tran { get; set; }

        // Atributos para el manejo de los conceptos para la factura
        [DataMember]
        public string concepto { get; set; }
        [DataMember]
        public string nom_moneda { get; set; }
        [DataMember]
        public string nro_producto { get; set; }
        [DataMember]
        public Int64 valor { get; set; }
        [DataMember]
        public Int64 valor_iva { get; set; }
        [DataMember]
        public Int64 valor_base { get; set; }
        [DataMember]
        public string num_factura { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal saldo { get; set; }

        // atributo para referencia de pagos ahorros a la vista
        [DataMember]
        public string referencia { get; set; }

        // Atributos usados en TipoOperacionData.cs en ConsultarTranCreditosboucher();
        [DataMember]
        public DateTime? fecha_operacion { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public string nombre_caja { get; set; }
        [DataMember]
        public string nombre_cajero { get; set; }
        [DataMember]
        public string cod_persona { get; set; }
        [DataMember]
        public string observaciones { get; set; }

        [DataMember]
        public string num_comp { get; set; }

        [DataMember]
        public string tipo_comp { get; set; }

        [DataMember]
        public string tipo_comp_desc { get; set; }
    }
}
