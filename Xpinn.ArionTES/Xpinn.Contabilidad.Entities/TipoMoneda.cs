using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Contabilidad.Entities
{
    [DataContract]
    [Serializable]
    public class TipoMoneda
    {
        [DataMember]
        public Int64? tipo_moneda{ get; set; }
        [DataMember]
        public string descripcion { get; set; }
    }          
       
}
