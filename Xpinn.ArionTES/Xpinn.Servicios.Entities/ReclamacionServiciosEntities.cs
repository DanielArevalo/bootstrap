using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Servicios.Entities
{
    [DataContract]
    [Serializable]
    public class ReclamacionServicios
    {
        [DataMember]
        public int ideclamacion { get; set; }
        [DataMember]
        public string identificacion_fallecido { get; set; }
        [DataMember]
        public string nombre_fallecido { get; set; }
        [DataMember]
        public int? codparentesco { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public DateTime fechacrea { get; set; }
        [DataMember]
        public int? codusuario { get; set; }

        [DataMember]
        public int requierebeneficiarios { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
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
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public string estado { get; set; }
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

        ///agregado
        [DataMember]
        public string descripcion { get; set; }

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
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public DateTime fecha_reclamacion { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public DateTime fecha_creacion { get; set; }
    }
}