using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class SoporteCaj
    {
        [DataMember]
        public Int64 idsoporte { get; set; }
        [DataMember]
        public Int64? cod_per { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public int idtiposop { get; set; }
        [DataMember]
        public string nomtiposop { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public Int64? num_comp { get; set; }
        [DataMember]
        public int? tipo_comp { get; set; }
        [DataMember]
        public string nomtipo_comp { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public Int64? cod_usuario { get; set; }
        [DataMember]
        public string nomusuario { get; set; }
        [DataMember]
        public int? idarea { get; set; }
        [DataMember]
        public string nomarea { get; set; }
        //Para verificar si es vale provisional
        [DataMember]
        public string vale_prov { get; set; }
        [DataMember]
        public string nomvale { get; set; }
        [DataMember]
        public Int64? id_arqueo { get; set; }


        // Registros para generar el comprobante
        [DataMember]
        public string cod_cuenta { get; set; }
        [DataMember]
        public Int64? centro_costo { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public Int64? centro_gestion { get; set; }

        //Agregado para reintegro de caja menor
        List<SoporteCaj> lstSoporte { get; set; }
    }
}