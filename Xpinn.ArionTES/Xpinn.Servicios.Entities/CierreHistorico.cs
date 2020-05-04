using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
       /// <summary>
    /// Entidad ArqueoCaja
    /// </summary>
    [DataContract]
    [Serializable]
    public class CierreHistorico
    {

        [DataMember]
        public string estadocierre { get; set; }

        [DataMember]
        public DateTime fecha_cierre { get; set; }
    }
}
