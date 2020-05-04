using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class Empleados
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64 estaactivocontrato { get; set; }



        
        [DataMember]
        public Int64 consecutivo_empleado { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cod_nomina_emp { get; set; }
        [DataMember]
        public string tipo_sueldo { get; set; }
        [DataMember]
        public DateTime? fecha_expedicion { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string cod_estado_civil { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public DateTime? fecha_nacimiento { get; set; }
        [DataMember]
        public string cod_cargo { get; set; }
        [DataMember]
        public string cod_ciudad_nac { get; set; }
        [DataMember]
        public string cod_oficina { get; set; }
        [DataMember]
        public string cod_tipo_contrato { get; set; }
        [DataMember]
        public DateTime? fecha_ingreso { get; set; }
        [DataMember]
        public string salario { get; set; }
        [DataMember]
        public string jornada_laboral { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nom_contrato { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string cod_identificacion { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string profesion { get; set; }

        [DataMember]
        public DateTime fechainicioperiodo { get; set; }

        [DataMember]
        public DateTime fechaterminacionperiodo { get; set; }

        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public long codigotipocontrato { get; set; }

        [DataMember]
        public long codigocentrocosto{ get; set; }


        [DataMember]
        public decimal? porcentajearl { get; set; }

        [DataMember]
        public long tipo_riesgo { get; set; }


        [DataMember]
        public String tipo_sangre { get; set; }

        [DataMember]
        public String visa { get; set; }

        [DataMember]
        public DateTime? fecha_ven_visa { get; set; }
        [DataMember]
        public String pasaporte { get; set; }
        [DataMember]
        public  DateTime? fecha_ven_pasaporte { get; set; }
    }

}
