using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
   public class InformacionFinanciera
    {
        [DataMember]
           public Int32 cod_persona    { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
           public Int32 totingresos    { get; set; }
        [DataMember]
           public Int32 totegresos     { get; set; }
        [DataMember]
           public Int32 totactivos     { get; set; }
        [DataMember]
           public Int32 totpasivo     { get; set; } 
        [DataMember]
           public Int32 totdisponible  { get; set; }
        [DataMember]
        public Int32 totpatrimonio     { get; set; }
    }
}
