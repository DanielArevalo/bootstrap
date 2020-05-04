using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteMovimiento
    {

        [DataMember]
        public String numero_cdat { get; set; }
        [DataMember]
        public String numero_fisico { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime fecha_oper { get; set; }
        [DataMember]
        public string tipo_ope { get; set; }
        [DataMember]
        public int cod_ope { get; set; }
        [DataMember]
        public string nomtipo_ope { get; set; }
        [DataMember]
        public int num_comp { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int cod_linea_cdat { get; set; }
        [DataMember]
        public int cod_cdat { get; set; }
        [DataMember]
        public int cod_destinacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64 cod_persona{ get; set; }
        [DataMember]
        public string soporte { get; set; }     
    }

}