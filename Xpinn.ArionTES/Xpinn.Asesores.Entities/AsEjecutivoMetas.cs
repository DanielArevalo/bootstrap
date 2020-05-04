using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    public class AsEjecutivoMetas
    {
        [DataMember]
        public Int64 IdMeta { get; set; } //Corresponde a la columna ICODMETA
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Corresponde a la columna ICODIGO
        [DataMember]
        public string VlrMeta { get; set; }//Corresponde a la columna IEXPR
        [DataMember]
        public DateTime Fecha { get; set; }//Corresponde a la columna FECHA
    }
}
