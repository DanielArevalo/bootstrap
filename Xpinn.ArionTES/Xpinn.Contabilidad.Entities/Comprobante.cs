using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class Comprobante
    {
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public string num_consig { get; set; }
        [DataMember]
        public string n_documento { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public DateTime hora { get; set; }
        [DataMember]
        public Int64 ciudad { get; set; }
        [DataMember]
        public Int64 concepto { get; set; }
        [DataMember]
        public Int64? tipo_pago { get; set; }
        [DataMember]
        public Int64? entidad { get; set; }      
        [DataMember]
        public string descripcion_concepto { get; set; }
        [DataMember]
        public decimal totalcom { get; set; }
        [DataMember]
        public string tipo_benef { get; set; }
        [DataMember]
        public Int64 cod_benef { get; set; }
        [DataMember]
        public string iden_benef { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nom_tipo_iden { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public string soporte { get; set; }
        [DataMember]
        public Int64 cheque_id { get; set; }
        [DataMember]
        public string cheque_iden_benef { get; set; }
        [DataMember]
        public string cheque_tipo_identificacion { get; set; }
        [DataMember]
        public string cheque_nombre { get; set; }
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public Int64? cod_elaboro { get; set; }
        [DataMember]
        public string elaboro { get; set; }
        [DataMember]
        public Int64? cod_aprobo { get; set; }
        [DataMember]
        public string aprobo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public decimal desembolso { get; set; }

        [DataMember]
        public Stream stream { get; set; }
        [DataMember]
        public Boolean rptaLista { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }


        //AGREGADO 
        [DataMember]
        public int tipo_motivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int idanulaicon { get; set; }
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public string usuario { get; set; }


        [DataMember]
        public Int64 num_comp_anula { get; set; }
        [DataMember]
        public Int64 tipo_comp_anula { get; set; }


        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
    }
}
