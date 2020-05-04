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
    public class Dias_no_habiles
    {
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public int mes { get; set; }
        [DataMember]
        public int ano { get; set; }
        [DataMember]
        public int dia_festivo { get; set; }
        [DataMember]
        public int dia_semana { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }

        public List<Dias_no_habiles> lstDias { get; set; }

    }
}
