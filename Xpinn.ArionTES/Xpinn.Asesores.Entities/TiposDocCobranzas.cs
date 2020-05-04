using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Comun.Entities;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class TiposDocCobranzas
    {

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo { get; set; }
        [DataMember]
        public byte[] Textos { get; set; }
        [DataMember]
        public string texto { get; set; }
        [DataMember]
        public Int64 tipo_documento { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public Int64 id { get; set; }
        [DataMember]
        public string campo { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public Empresa empresa { get; set; }
        [DataMember]
        public List<TiposDocCobranzas> lstVariables { get; set; }
    }
}
