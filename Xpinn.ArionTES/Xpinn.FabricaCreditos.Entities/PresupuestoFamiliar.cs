using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad PresupuestoFamiliar
    /// </summary>
    [DataContract]
    [Serializable]
    public class PresupuestoFamiliar
    {
        [DataMember] 
        public Int64 cod_presupuesto { get; set; }
        [DataMember] 
        public Int64 cod_persona { get; set; }
        [DataMember] 
        public Int64 actividadprincipal { get; set; }
        [DataMember] 
        public Int64 conyuge { get; set; }
        [DataMember] 
        public Int64 otrosingresos { get; set; }
        [DataMember] 
        public Int64 consumofamiliar { get; set; }
        [DataMember] 
        public Int64 obligaciones { get; set; }
        [DataMember] 
        public Int64 excedente { get; set; }

    }
}