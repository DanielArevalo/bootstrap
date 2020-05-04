using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad IngresosFamilia
    /// </summary>
    [DataContract]
    [Serializable]
    public class IngresosFamilia
    {
        [DataMember] 
        public Int64 cod_ingreso { get; set; }
        [DataMember] 
        public Int64 ingresos { get; set; }
        [DataMember] 
        public Int64 negocio { get; set; }
        [DataMember] 
        public Int64 conyuge { get; set; }
        [DataMember] 
        public Int64 hijos { get; set; }
        [DataMember] 
        public Int64 arriendos { get; set; }
        [DataMember] 
        public Int64 pension { get; set; }
        [DataMember] 
        public Int64 otros { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }

    }
}