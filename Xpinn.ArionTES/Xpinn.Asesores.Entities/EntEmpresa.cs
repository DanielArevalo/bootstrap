using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class EntEmpresa
    {
        [DataMember]
        public int cod_empresa { get; set; }
        [DataMember]
        public string nit { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string sigla { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public DateTime? fecha_constitucion { get; set; }
        [DataMember]
        public int? ciudad { get; set; }
        [DataMember]
        public string e_mail { get; set; }
        [DataMember]
        public string nom_Ciudad { get; set; }
        //agregado
        [DataMember]
        public int tipo_de_empresa { get; set; }
    }
}
