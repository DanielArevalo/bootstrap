using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.CDATS.Entities
{
    [DataContract]
    [Serializable]
    public class FormaCaptacion
    {
        [DataMember]
        public int? codforma_captacion { get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }
}