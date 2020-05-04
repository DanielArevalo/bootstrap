using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad EgresosFamilia
    /// </summary>
    [DataContract]
    [Serializable]
    public class EgresosFamilia
    {
        [DataMember] 
        public Int64 cod_egreso { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 egresos { get; set; }
        [DataMember] 
        public Int64 alimentacion { get; set; }
        [DataMember] 
        public Int64 vivienda { get; set; }
        [DataMember] 
        public Int64 educacion { get; set; }
        [DataMember] 
        public Int64 serviciospublicos { get; set; }
        [DataMember] 
        public Int64 transporte { get; set; }
        [DataMember] 
        public Int64 pagodeudas { get; set; }
        [DataMember] 
        public Int64 otros { get; set; }

    }
}