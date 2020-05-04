using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Cartera.Entities
{
    /// <summary>
    /// Entidad Clasificación de Cartera
    /// </summary>
    [DataContract]
    [Serializable]
    public class ClasificacionCartera
    {
        [DataMember]
        public string FECHA_HISTORICO { get; set; }
        [DataMember]
        public string NUMERO_RADICACION { get; set; }
        [DataMember]
        public string COD_LINEA_CREDITO { get; set; }
        [DataMember]
        public string NOMBRE_LINEA { get; set; }
        [DataMember]
        public string IDENTIFICACION { get; set; }
        [DataMember]
        public string NOMBRE { get; set; }
        [DataMember]
        public string CLASIFICACION { get; set; }
        [DataMember]
        public string FORMA_PAGO { get; set; }
        [DataMember]
        public string TIPO_GARANTIA { get; set; }
        [DataMember]
        public string COD_CATEGORIA_CLI { get; set; }
        [DataMember]
        public string COD_ATR { get; set; }
        [DataMember]
        public string NOMBRE_ATRIBUTO { get; set; }
        [DataMember]
        public long SALDO { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nombre_oficina { get; set; }
        [DataMember]
        public string fecha { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string campo1 { get; set; }
        [DataMember]
        public string campo2 { get; set; }

        /// <summary>
        /// parametrizacion de clasificacion
        /// </summary>
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo_historico { get; set; }

        /// <summary>
        /// parametrizacion dias_Categorias 
        /// </summary>
        [DataMember]
        public Int64 rango { get; set; }
        [DataMember]
        public Int64 clasifica { get; set; }
        [DataMember]
        public string categoria { get; set; }
        [DataMember]
        public Int64 diasminimo { get; set; }
        [DataMember]
        public Int64 diasmaximo { get; set; }
        [DataMember]
        public long tipo_provision { get; set; }
        [DataMember]
        public Int64 por_provision { get; set; }
        [DataMember]
        public Int64 causa { get; set; }

        //Clasificaciones
        [DataMember]
        public string CC { get; set; }
        [DataMember]
        public string CO { get; set; }
        [DataMember]
        public string VI { get; set; }
        [DataMember]
        public string MI { get; set; }

    }
}
