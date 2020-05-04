using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
  public class LineaProgramado_Requisito
    {
        [DataMember]
        public Int64 idrequisito { get; set; }
        [DataMember]
        public int idrango { get; set; }
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public int tipo_tope { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public string maximo { get; set; }
    }
}
