using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Nomina.Entities
{
    [DataContract]
    [Serializable]
    public class IngresoPersonal
    {
        [DataMember]
        public long consecutivo { get; set; }
        [DataMember]
        public long codigoempleado { get; set; }
        [DataMember]
        public long codigopersona { get; set; }
        [DataMember]
        public long? codigonomina { get; set; }
        [DataMember]
        public long? codigotipocontrato { get; set; }
        [DataMember]
        public long? codigocargo { get; set; }
        [DataMember]
        public long? codigocentrocosto { get; set; }
        [DataMember]
        public DateTime? fechaingreso { get; set; }
        [DataMember]
        public DateTime? fechainicioperiodoprueba { get; set; }
        [DataMember]
        public DateTime? fechaterminacionperiodoprueba { get; set; }
        [DataMember]
        public int? tieneley50 { get; set; }
        [DataMember]
        public int? esextranjero { get; set; }
        [DataMember]
        public string nombre_empleado { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string desc_centro_costo { get; set; }
        [DataMember]
        public string desc_nomina { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public decimal? salario { get; set; }
        [DataMember]
        public long? essueldovariable { get; set; }
        [DataMember]
        public long? essalariointegral { get; set; }
        [DataMember]
        public string dia_habil { get; set; }
        [DataMember]

        public long? auxiliotransporte { get; set; }
        [DataMember]
        public long? formapago { get; set; }

        [DataMember]
        public long? diasvacaciones { get; set; }

        [DataMember]

        public long? pagavacacionesant { get; set; }
        [DataMember]
        public long? tipocuenta { get; set; }
        [DataMember]
        public long? codigobanco { get; set; }
        [DataMember]
        public string numerocuentabancaria { get; set; }
        [DataMember]
        public long? codigofondosalud { get; set; }
        [DataMember]
        public long? codigofondopension { get; set; }
        [DataMember]
        public long? codigofondocesantias { get; set; }
        [DataMember]
        public long? codigocajacompensacion { get; set; }
        [DataMember]
        public long? codigoarl { get; set; }
        [DataMember]
        public long? codigopensionvoluntaria { get; set; }
        [DataMember]
        public DateTime? fechaafiliacionsalud { get; set; }
        [DataMember]
        public DateTime? fechaafiliacionpension { get; set; }
        [DataMember]
        public DateTime? fechaafiliacioncesantias { get; set; }
        [DataMember]
        public DateTime? fechaafiliacajacompensacion { get; set; }
        [DataMember]
        public DateTime? fecharetirosalud { get; set; }
        [DataMember]
        public DateTime? fecharetiropension { get; set; }
        [DataMember]
        public DateTime? fecharetirocesantias { get; set; }
        [DataMember]
        public DateTime? fecharetirocajacompensacion { get; set; }
        [DataMember]
        public long? tipocotizante { get; set; }
        [DataMember]
        public long? espensionadoporvejez { get; set; }
        [DataMember]
        public long? espensionadoporinvalidez { get; set; }
        [DataMember]
        public long? tipo_riesgo { get; set; }
        [DataMember]
        public decimal? porcentajearl { get; set; }
        [DataMember]
        public DateTime? fechaultimopagoperiodica { get; set; }
        [DataMember]
        public DateTime? fechacausacionprima { get; set; }
        [DataMember]
        public DateTime? fechacausacioncesantias { get; set; }
        [DataMember]
        public DateTime? fechacausainterescesa { get; set; }
        [DataMember]
        public DateTime? fechacausacionvacaciones { get; set; }
        [DataMember]
        public string cuentaprovision { get; set; }
        [DataMember]
        public string cuentacontable { get; set; }
        [DataMember]
        public long? escontratoprestacional { get; set; }
        [DataMember]
        public string desc_contrato { get; set; }
        [DataMember]
        public string desc_cargo { get; set; }

        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string desc_estaactivocontrato { get; set; }
        [DataMember]
        public Int64 area { get; set; }

        [DataMember]
        public DateTime? fechafinvacaciones { get; set; }

        [DataMember]
        public DateTime? fecharegresovacaciones { get; set; }

        [DataMember]
        public long?  procesoretencion { get; set; }

        [DataMember]
        public long estaactivocontrato { get; set; }

        [DataMember]
        public long? inactivacion { get; set; }
        [DataMember]
        public long? cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }


    }
}