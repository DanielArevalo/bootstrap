using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad Cargos
    /// </summary>
    [DataContract]
    [Serializable]
    public class Cargos
    {
        [DataMember]
        public Int64 cod_cargo { get; set; }
        [DataMember]
        public string nom_cargo { get; set; }
        [DataMember]
        public decimal comision_colocacion_ant { get; set; }
        [DataMember]
        public decimal comision_cartera_ant { get; set; }
        [DataMember]
        public decimal aux_gas_ant { get; set; }
        [DataMember]
        public decimal aux_tel_ant { get; set; }
        [DataMember]
        public decimal comision_colocacion { get; set; }
        [DataMember]
        public decimal comision_cartera { get; set; }
        [DataMember]
        public decimal aux_gas { get; set; }
        [DataMember]
        public decimal aux_tel { get; set; }
        [DataMember]
        public decimal incremento_colocacion { get; set; }
        [DataMember]
        public decimal incremento_cartera { get; set; }
        [DataMember]
        public decimal incremento_aux_gas { get; set; }
        [DataMember]
        public decimal incremento_aux_tel { get; set; }
    }
}
