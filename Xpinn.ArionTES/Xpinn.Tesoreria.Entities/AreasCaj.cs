using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class AreasCaj
    {
        [DataMember]
        public int idarea { get; set; }
        //Agregado
        [DataMember]
        public DateTime fecha_constitucion { get; set; }
        [DataMember]
        public decimal? base_valor { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public string nom_usuario { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public Int64? valor_minimo { get; set; }
        //Agregado para manejo de saldo de caja menor
        [DataMember]
        public Int64 saldo_caja { get; set; }
        [DataMember]
        public Int64 centro_costo { get; set; }

        [DataMember]
        public Int64? cod_persona { get; set; }



        [DataMember]
        public string identificacion { get; set; }
    }
    //Agregado para arqueo de caja menor
    public class Efectivo
    {
        [DataMember]
        public Int64 denominacion { get; set; }
        [DataMember]
        public Int64 cantidad { get; set; }
        [DataMember]
        public Int64 total { get; set; }
        [DataMember]
        public string tipo { get; set; }
    }
    public class ArqueoCaj
    {
        [DataMember]
        public Int64? id_arqueo { get; set; }
        [DataMember]
        public DateTime fecha_arqueo { get; set; }
        [DataMember]
        public decimal total_arqueo { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public int idarea { get; set; }
    }
    public class ArqueoDetalle
    {
        [DataMember]
        public Int64? id_det_arqueo { get; set; }
        [DataMember]
        public Int64? id_arqueo { get; set; }
        [DataMember]
        public string tipo_efectivo { get; set; }
        [DataMember]
        public Int64 denominacion { get; set; }
        [DataMember]
        public Int64 cantidad { get; set; }
        [DataMember]
        public Int64 total { get; set; }
    }
}
