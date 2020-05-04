using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Integracion.Entities
{
    [DataContract]
    [Serializable]
    public class Integracion
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public string entidad { get; set; }

        [DataMember]
        public string descripcion { get; set; }

        [DataMember]
        public string usuario { get; set; }

        [DataMember]
        public string password { get; set; }

        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string datos { get; set; }
    }
}