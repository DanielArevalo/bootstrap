using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class IndicadoresAhorros
    {
        [DataMember]
        public string fecha_corte { get; set; }

        [DataMember]
        public string fecha_historico { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal numero_cuentas { get; set; }
        [DataMember]
        public decimal variacion_valor { get; set; }
        [DataMember]
        public decimal variacion_numero { get; set; }
        [DataMember]
        public decimal total_cartera_vivienda { get; set; }

        // Atributos para el manejo de tipo de producto
        [DataMember]
        public Int64 tipo_producto { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }

        /// <summary>
        /// parametrizacion de cod_linea
        /// </summary>
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}


