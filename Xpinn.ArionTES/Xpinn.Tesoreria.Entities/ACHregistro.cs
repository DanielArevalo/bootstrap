using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class ACHregistro
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public string separador { get; set; }
        [DataMember]
        public List<ACHcampo> LstCampos { get; set; }
        [DataMember]
        public Int64 plantilla { get; set; }
    }
}