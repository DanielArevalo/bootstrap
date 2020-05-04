using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Asesores.Entities
{
    [DataContract]
    [Serializable]
    public class Meta
    {
        [DataMember]
        public Int64 IdMeta { get; set; } //Corresponde al campo (icodmeta) consecutivo que Corresponde al código de la meta   
        [DataMember]
        public string Nombre { get; set; }//Corresponde al campo (snombremeta) Corresponde al nombre de la meta  
        [DataMember]
        public string Formato { get; set; }//Corresponde al campo (sformatometa) Indica en qué formato esta expresada la meta: Es un número entero, un porcentaje 
    }
}