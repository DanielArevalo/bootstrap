using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class Vacaciones
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public Int64 numero_novedad { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public DateTime? fecha_grabacion { get; set; }
        [DataMember]
        public long codigo_pagaduria { get; set; }
        [DataMember]
        public DateTime? fecha_novedad { get; set; }
        [DataMember]
        public int tipo_calculo { get; set; }
        [DataMember]
        public DateTime? fecha_inicial { get; set; }
        [DataMember]
        public DateTime? fecha_final { get; set; }
    }
}