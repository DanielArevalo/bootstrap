using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class documentosrequeridos
    {
        [DataMember]
        public string cod_linea_credio { get; set; }
        [DataMember]
        public int tipo_documento { get; set; }
        [DataMember]
        public int iddocumento { get; set; }
        [DataMember]
        public string aplica_codeudor { get; set; }
        [DataMember]
        public Boolean entregado { get; set; }
        [DataMember]
        public String observaciones { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public DateTime? fechaanexo{ get; set; }
        // Para documentos anexos
        [DataMember]
        public int numerosolicitud { get; set; }
        [DataMember]
        public int numeroradicacion{ get; set; }
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int asesor { get; set; }
        [DataMember]
        public object imagen { get; set; }
        [DataMember]
        public DateTime? fechaentrega{ get; set; }


    }
}
