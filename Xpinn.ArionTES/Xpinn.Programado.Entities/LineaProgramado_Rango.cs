using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Programado.Entities
{
    public class LineaProgramado_Rango
    {
        [DataMember]
        public int idrango { get; set; }
        [DataMember]
        public string cod_linea_programado { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public LineaProgramado_Tasa LineaTasa { get; set; }
        [DataMember]
        public List<LineaProgramado_Requisito> ListaRequisitos { get; set; }
    }
}
