using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class LiquidacionVacacionesEmpleado
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long usuariocreacion { get; set; }

        [DataMember]
        public String usuario_crea { get; set; }
        [DataMember]
        
        public long codigoempleado { get; set; }
        [DataMember]

        public long codigopersona { get; set; }

        [DataMember]
        public long cantidaddias { get; set; }
        [DataMember]
        public long codigonomina { get; set; }
        [DataMember]
        public DateTime fechainicio { get; set; }
        [DataMember]
        public DateTime fechaterminacion { get; set; }
        [DataMember]
        public long diasliquidados { get; set; }
        [DataMember]
        public decimal valortotalpagar { get; set; }
        [DataMember]
        public long codigocentrocosto { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string desc_centro_costo { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public decimal salario { get; set; }

        [DataMember]
        public decimal salario_ad { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }
        [DataMember]
        public DateTime fechaPago { get; set; }

        [DataMember]
        public DateTime fechafinvacaciones { get; set; }
        [DataMember]
        public DateTime fechainicioperiodo { get; set; }
        [DataMember]
        public DateTime fechainicioproxperiodo { get; set; }
        [DataMember]
        public DateTime fechaterminacionproxperiodo { get; set; }
        [DataMember]
        public DateTime fechaterminacionperiodo { get; set; }

        [DataMember]
        public long diasdisfrutar { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public long diaspagados { get; set; }

        [DataMember]
        public Int64 cod_concepto { get; set; }

        [DataMember]
        public decimal valor { get; set; }

        [DataMember]
        public long codorigen { get; set; }

        [DataMember]
        public long vacacionesanticipadas { get; set; }


        [DataMember]
        public long codigoconceptonomina { get; set; }

        [DataMember]
        public long num_comp { get; set; }


        [DataMember]
        public long tipo_comp { get; set; }


        [DataMember]
        public long diaspendientes { get; set; }
    }
}