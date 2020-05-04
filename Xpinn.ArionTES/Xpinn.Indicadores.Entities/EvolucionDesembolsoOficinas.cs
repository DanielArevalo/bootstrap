using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Xpinn.Indicadores.Entities
{
    [DataContract]
    [Serializable]
    public class EvolucionDesembolsoOficinas
    {
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal total_desembolsos { get; set; }
        [DataMember]
        public decimal numero_desembolsos { get; set; }
        [DataMember]
        public decimal participacion_desembolsos { get; set; }
        [DataMember]
        public decimal participacion_numero { get; set; }
        [DataMember]
        public decimal total_desembolsos_c { get; set; }
        [DataMember]
        public decimal numero_desembolsos_c { get; set; }
        [DataMember]
        public decimal participacion_desembolsos_c { get; set; }
        [DataMember]
        public decimal participacion_numero_c { get; set; }


        /// <summary>
        /// agregado para la colocacionporoficinas
        /// </summary>
        /// 
        [DataMember]
        public int numero { get; set; }
        [DataMember]
        public decimal valor { get; set; }
    }
}


