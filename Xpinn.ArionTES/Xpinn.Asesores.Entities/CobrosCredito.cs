using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad CobrosCredito
    /// </summary>
    [DataContract]
    [Serializable]
    public class CobrosCredito
    {
        [DataMember]
        public Int64 codigo { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public Int64 estado_proceso { get; set; }
        [DataMember]
        public Int64 encargado { get; set; }
        [DataMember]
        public Int64 abogadoencargado { get; set; }
        [DataMember]
        public Int64 cod_motivo_cambio { get; set; }
        [DataMember]
        public Int64 ciudad { get; set; }
        [DataMember]
        public Int64 ciudad_juzgado { get; set; }
        [DataMember]
        public string numero_juzgado { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public Int64 usucreacion { get; set; }
        [DataMember]
        public DateTime fechamodif { get; set; }
        [DataMember]
        public Int64 usumodif { get; set; }
    }
    }

