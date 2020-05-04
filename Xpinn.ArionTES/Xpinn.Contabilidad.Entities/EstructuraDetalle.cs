using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class EstructuraDetalle
    {
        [DataMember]
        public int cod_est_det { get; set; }
        [DataMember]
        public string detalle { get; set; }
    }
}
