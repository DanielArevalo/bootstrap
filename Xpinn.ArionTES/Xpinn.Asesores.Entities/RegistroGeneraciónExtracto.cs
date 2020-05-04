using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad AgendaHora
    /// </summary>
    [DataContract]
    [Serializable]
    public class RegistroGeneraciónExtracto
    {
        [DataMember]
        public DateTime pfechacorte { get; set; }
        [DataMember]
        public Int64 pcod_persona { get; set; }
        [DataMember]
        public Int64 pnumero_radicacion { get; set; }
        [DataMember]
        public string pusuario { get; set; }
        [DataMember]
        public Int32 pcodmotivo { get; set; }
        [DataMember]
        public string pobservaciones { get; set; }
        

        [DataMember]
        public Int64 pidextracto { get; set; }
        [DataMember]
        public DateTime pfecha_generacion { get; set; }
    }
}