using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.TarjetaDebito.Entities
{
    [DataContract]
    [Serializable]
    public class CuentaCoopcentral
    {
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }       
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string direccion_casa { get; set; }
        [DataMember]
        public string direccion_trabajo { get; set; }
        [DataMember]
        public string telefono_casa { get; set; }
        [DataMember]
        public string telefono_trabajo { get; set; }
        [DataMember]
        public string telefono_movil { get; set; }
        [DataMember]
        public DateTime? fecha_nacimiento { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string correo_electronico { get; set; }
        [DataMember]
        public string pais_residencia { get; set; }
        [DataMember]
        public string dpto_residencia { get; set; }
        [DataMember]
        public string ciudad_residencia { get; set; }
        [DataMember]
        public string pais_nacimiento { get; set; }
        [DataMember]
        public string dpto_nacimiento { get; set; }
        [DataMember]
        public string ciudad_nacimiento { get; set; }
        [DataMember]
        public string cuenta { get; set; }
        [DataMember]
        public string tipo_cuenta { get; set; }
        [DataMember]
        public decimal saldodisponible { get; set; }
        [DataMember]
        public decimal saldototal { get; set; }
        [DataMember]
        public DateTime fecha_actualizacion { get; set; }
        [DataMember]
        public DateTime fecha_expedicion { get; set; }
        [DataMember]
        public decimal? pagominimo { get; set; }
        [DataMember]
        public decimal? pagototal { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento { get; set; }

    }

}
