using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad PRESUPUESTOEMPRESARIAL
    /// </summary>
    [DataContract]
    [Serializable]
    public class PresupuestoEmpresarial
    {
        [DataMember] 
        public Int64 cod_presupuesto { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 totalactivo { get; set; }
        [DataMember] 
        public Int64 totalpasivo { get; set; }
        [DataMember] 
        public Int64 totalpatrimonio { get; set; }
        [DataMember] 
        public Int64 ventamensual { get; set; }
        [DataMember] 
        public Int64 costototal { get; set; }
        [DataMember] 
        public Int64 utilidad { get; set; }

    }
}