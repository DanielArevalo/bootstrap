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
    public class Cargo
    {
        #region TablaCargo
        [DataMember]
        public Int64 IdCargo { get; set; }
        [DataMember]
        public string NombreCargo { get; set; }
        #endregion
    }
}
