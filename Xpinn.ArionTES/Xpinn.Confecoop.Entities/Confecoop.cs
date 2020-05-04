using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Confecoop.Entities
{
    [DataContract]
    [Serializable]
    public class PUC
    {
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public string separador { get; set; }

        [DataMember]
        public int idlinea { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public int maneja_niif { get; set; }
        [DataMember]
        public DateTime fecha_ini { get; set; }
        [DataMember]
        public string nombre_archivo { get; set; }
        [DataMember]
        public int tipo_norma { get; set; }
        [DataMember]
        public int a_fecha_corte { get; set; }
    }

}