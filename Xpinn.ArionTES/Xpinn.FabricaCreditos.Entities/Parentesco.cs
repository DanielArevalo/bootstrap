using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad Parentesco
    /// </summary>
    [DataContract]
    [Serializable]
    public class Parentesco
    {
        [DataMember]
        public Int64 codparentesco { get; set; }
        [DataMember]
        public string descripcion { get; set; }

    }
}