using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Caja.Entities
{
    [DataContract]
    [Serializable]
    public class DiaSemana
    {
        public Int64 IdDiaSemana { get; set; }
        public Int64 cod_diasemana { get; set; }
        public string nom_diasemana { get; set; }
    }
}
