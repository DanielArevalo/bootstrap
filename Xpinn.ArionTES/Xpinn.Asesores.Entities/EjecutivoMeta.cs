using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Asesores.Entities
{
    public class EjecutivoMeta
    {
        [DataMember]
        public Int64 IdEjecutivoMeta { get; set; } //Corresponde al PK
        [DataMember]
        public Int64 IdEjecutivo { get; set; } //Corresponde al PK
        [DataMember]
        public string PrimerNombre { get; set; }//Corresponde a la columna IEXPR
        [DataMember]
        public string SegundoNombre { get; set; }//Corresponde a la columna IEXPR
        [DataMember]
        public string PrimerApellido { get; set; }//Corresponde a la columna IEXPR
        [DataMember]
        public Int64 IdMeta { get; set; } //Corresponde al PK
        [DataMember]
        public string NombreMeta { get; set; }//Corresponde a la columna IEXPR

        [DataMember]
        public string NombreOficina { get; set; }//Corresponde a la columna IEXPR

        [DataMember]
        public Int64 Codficina { get; set; }//Corresponde a la columna IEX

        [DataMember]
        public Int64 VlrMeta { get; set; }//Corresponde a la columna IEXPR
        [DataMember]
        public string Vigencia { get; set; }//Corresponde a la columna Vigencia
        [DataMember]
        public Stream stream { get; set; }
        [DataMember]
        public string Mes { get; set; }
        [DataMember]
        public string Year { get; set; }
        [DataMember]
        public string Nombres { get; set; }
        [DataMember]
        public string formatoMeta { get; set; }
        [DataMember]
        public DateTime Fecha { get; set; }//Corresponde a la columna fecha

        [DataMember]
        public string DescripcionPeriodicidad { get; set; }

        [DataMember]
        public Int64 IdPeriodicidad { get; set; }

    }
}