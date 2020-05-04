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
    public class ParentescoPolizas
    {
        [DataMember]
        public Int64 codparentesco { get; set; }
        [DataMember]
        public string parentesco { get; set; }
    
       
    }
}