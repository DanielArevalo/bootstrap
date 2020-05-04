using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.NIIF.Entities
{
    [DataContract]
    [Serializable]
    public class BalanceNIIF
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string cod_cuenta_niif { get; set; }
        [DataMember]
        public int centro_costo { get; set; }
        [DataMember]
        public int? tipo_moneda { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public decimal saldo { get; set; }

        //Agregado
        [DataMember]
        public string cod_cuentaOrigen_niif { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_tercero { get; set; }
        [DataMember]
        public decimal saldo_colgaap { get; set; }
        [DataMember]
        public int? ajuste { get; set; }
        [DataMember]
        public int? reclasificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }

        /// <summary>
        /// Consultar balance niif para  variable para filtrar
        /// </summary>
        [DataMember]
        public string filtro { get; set; }
        [DataMember]
        public int nivel { get; set; }
        [DataMember]
        public int mostrar_ceros { get; set; }
        [DataMember]
        public int centro_costo_fin { get; set; }
        [DataMember]
        public int cuentascero { get; set; }
        [DataMember]
        public int comparativo { get; set; }
        [DataMember]
        public int mostrarmovper13 { get; set; }
        [DataMember]
        public String estado { get; set; }
    }
}