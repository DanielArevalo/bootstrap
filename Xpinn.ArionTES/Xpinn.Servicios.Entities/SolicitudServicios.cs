using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class Servicio
    {

        [DataMember]

        public int cod_oficina { get; set; }
        [DataMember]

        public int requierebeneficiarios { get; set; }
        [DataMember]
     
        public int numero_servicio { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string cod_linea_servicio { get; set; }
        [DataMember]
        public string cod_plan_servicio { get; set; }
        [DataMember]
        public DateTime? fecha_inicio_vigencia { get; set; }
        [DataMember]
        public DateTime? fecha_final_vigencia { get; set; }
        [DataMember]
        public string num_poliza { get; set; }
        [DataMember]
        public decimal? valor_total { get; set; }
        [DataMember]
        public DateTime? fecha_primera_cuota { get; set; }
        [DataMember]
        public int? numero_cuotas { get; set; }
        [DataMember]
        public decimal? valor_cuota { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string identificacion_titular { get; set; }
        [DataMember]
        public string nombre_titular { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public decimal? saldo { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string descripcion_estado { get; set; }
        [DataMember]
        public string beneficiarios { get; set; }
        [DataMember]
        public DateTime? fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecha_activacion { get; set; }
        [DataMember]
        public string observacionaproba { get; set; }
        [DataMember]
        public int cuotas_pendientes { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public int tipo_comp { get; set; }
        [DataMember]
        public string tipo_mov { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public int numero_tran { get; set; }
        
        //agregado
        [DataMember]
        public string identificacion { get; set; }       
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public string nom_plan { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public List<DetalleServicio> lstDetalle { get; set; }
        [DataMember]
        public DateTime Fec_ini { get; set; }
        [DataMember]
        public DateTime Fec_fin { get; set; }
        [DataMember]
        public DateTime Fec_1Pago { get; set; }
        [DataMember]
        public Int64? codigo_proveedor { get; set; }
        [DataMember]
        public string identificacion_proveedor { get; set; }
        [DataMember]
        public string nombre_proveedor { get; set; }
        [DataMember]
        public string nombre_fallecido { get; set; }
        [DataMember]
        public string identificacion_fallecido { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public DateTime fecha_reclamacion { get; set; }
        [DataMember]
        public int tipo_identificacion { get; set; }
        [DataMember]
        public string nom_forma_pago { get; set; }
        [DataMember]
        public string nom_ciudad { get; set; }
        [DataMember]
        public Int64? numero_preimpreso { get; set; }
        [DataMember]
        public Int64? cod_ope { get; set; }
        [DataMember]
        public Int64? cod_cliente { get; set; }
        [DataMember]
        public decimal tasa { get; set; }
        [DataMember]
        public int dias_periodicidad { get; set; }
        [DataMember]
        public int tipo_calendario { get; set; }
        [DataMember]
        public int tipo_cuota { get; set; }
        [DataMember]
        public string tipo_registro { get; set; }
        [DataMember]
        public decimal interes_corriente { get; set; }
        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public decimal? total_calculado { get; set; }
        [DataMember]
        public Int64? cod_destino { get; set; }
        [DataMember]
        public decimal total_interes_calculado { get; set; }

        [DataMember]
        public long? servicio_telefonia { get; set; }
    }
    

    [DataContract]
    [Serializable]
    public class CONTROLSERVICIOS
    {
        [DataMember]
        public int idcontrol_servicios { get; set; }
        [DataMember]
        public int numero_servicio { get; set; }
        [DataMember]
        public int codtipo_proceso { get; set; }
        [DataMember]
        public DateTime fechaproceso { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
    }


}
