using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.Asesores.Entities.Common;

namespace Xpinn.Asesores.Entities
{
    public class TipoContrato
    {
        #region TablaTipoContrato
        [DataMember]
        public Int64 IdTipoContrato { get; set; }
        [DataMember]
        public string NombreTipoContrato { get; set; }
        #endregion
    }
}