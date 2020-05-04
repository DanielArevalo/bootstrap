using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Atributo
    /// </summary>
    [DataContract]
    [Serializable]
    public class Atributo
    {
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal saldo_atributo { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }

    }
}