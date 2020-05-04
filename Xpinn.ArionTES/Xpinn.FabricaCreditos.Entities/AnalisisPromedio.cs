using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class AnalisisPromedio
    {
        [DataMember]
        public long cod_persona { get; set; }
        [DataMember]
        public string producto { get; set; }
        [DataMember]
        public long afiliacion { get; set; }
        [DataMember]
        public DateTime? fecha_afiliacion { get; set; }
        [DataMember]
        public DateTime? fecha_apertura { get; set; }
        [DataMember]
        public long saldo { get; set; }
        [DataMember]
        public long reciprocidad { get; set; }
        [DataMember]
        public long cupo_disponible { get; set; }

        [DataMember]
        public string Año { get; set; }

        [DataMember]
        public string Ene { get; set; }
        [DataMember]
        public string Feb { get; set; }
        [DataMember]
        public string Mar { get; set; }
        [DataMember]
        public string Abr { get; set; }
        [DataMember]
        public string May { get; set; }
        [DataMember]
        public string Jun { get; set; }
        [DataMember]
        public string Jul { get; set; }
        [DataMember]
        public string Ago { get; set; }
        [DataMember]
        public string Sep { get; set; }
        [DataMember]
        public string Oct { get; set; }
        [DataMember]
        public string Nov { get; set; }
        [DataMember]
        public string Dic { get; set; }





    }
}