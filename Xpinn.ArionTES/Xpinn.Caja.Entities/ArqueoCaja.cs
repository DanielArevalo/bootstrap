using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Caja.Entities
{
    /// <summary>
    /// Entidad ArqueoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class ArqueoCaja
    {
        [DataMember] 
        public Int64 cod_caja { get; set; }
        [DataMember]
        public string nom_caja { get; set; }
        [DataMember] 
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public string nom_cajero { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public Int64 tipo_horario { get; set; }
        [DataMember]
        public string nom_horario { get; set; }
        [DataMember] 
        public DateTime fecha_arqueo { get; set; }
        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember] 
        public Int64 cod_moneda { get; set; }
        [DataMember] 
        public Int64 valor_efectivo { get; set; }
        [DataMember] 
        public Int64 valor_cheque { get; set; }
        [DataMember]
        public Int64 cod_caja_ppal { get; set; }
        [DataMember]
        public Int64 cod_cajero_ppal { get; set; }

        [DataMember]
        public string tipo_movimiento { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
         [DataMember]
        public Int64 codigo_parametro { get; set; }
         [DataMember]
         public Int64 codigo_usuario { get; set; }
         [DataMember]
        public String fechacierre { get; set; }
       
             
        /// <summary>
        /// agregado para poder modificar el arqueo
        /// </summary>
        [DataMember]
        public long id_arqueo { get; set; }
        
}
    }
    
