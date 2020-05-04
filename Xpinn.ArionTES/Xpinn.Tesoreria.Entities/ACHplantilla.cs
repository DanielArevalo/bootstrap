using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ACHplantilla
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public DateTime? fecha { get; set; }
        [DataMember]
        public int? activo { get; set; }
        [DataMember]
        public Int64? cod_banco { get; set; }
        [DataMember]
        public List<ACHregistro> LstRegistros { get; set; }
    }
}