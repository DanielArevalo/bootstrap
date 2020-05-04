using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Auxilios.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteAuxilio
    {
        [DataMember]
        public int numero_auxilio { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cod_linea_auxilio { get; set; }
        [DataMember]
        public decimal valor_solicitado { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public decimal valor_aprobado { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public string detalle { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public decimal valor_matricula { get; set; }

        [DataMember]
        public object identificacion { get; set; }
        [DataMember]
        public object nombres { get; set; }
        [DataMember]
        public object cod_nomina { get; set; }
        [DataMember]
        public object nom_empresa { get; set; }
        [DataMember]
        public object nom_linea { get; set; }
        [DataMember]
        public object numero_Auxilio { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public long cod_parentesco { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string identificacionPersona { get; set; }
        [DataMember]
        public string descripciones { get; set; }
        [DataMember]
        public DateTime fecha_proxima_solicitud { get; set; }


    }
}
