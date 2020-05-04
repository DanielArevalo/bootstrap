using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad ProcesosCobro
    /// </summary>
    [DataContract]
    [Serializable]
    public class ProcesosCobro
    {
        [DataMember]
        public Int64 codprocesocobro { get; set; }
        [DataMember]
        public Int64 codprocesoprecede { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public Int64 rangoinicial { get; set; }
        [DataMember]
        public Int64 rangofinal { get; set; }


        [DataMember]
        public Int64 codusuario { get; set; }
        [DataMember]
        public string nombreusuario { get; set; }
        [DataMember]
        public Int64 codabogado { get; set; }
        [DataMember]
        public string nombreagogado { get; set; }
        [DataMember]
        public Int64 codmotivo { get; set; }
        [DataMember]
        public string nombremotivo { get; set; }
        [DataMember]    
        public Int64 codciudadjuzgado { get; set; }
        [DataMember]
        public Int64 codciudad { get; set; }
        [DataMember]
        public string numero_juzgado { get; set; }
        [DataMember]
        public string observaciones { get; set; }
    }
}