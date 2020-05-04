using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Comun.Entities
{
    [DataContract]
    [Serializable]
    public class Empresa
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
        public DateTime fecha_constitución { get; set; }
        [DataMember]
        public int ciudad { get; set; }
        [DataMember]
        public string e_mail { get; set; }
        [DataMember]
        public string clave_e_mail { get; set; }
    }
}