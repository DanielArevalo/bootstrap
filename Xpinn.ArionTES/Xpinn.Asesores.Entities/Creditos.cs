using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Asesores.Entities
{
    /// <summary>
    /// Entidad Credito
    /// </summary>
    [DataContract]
    [Serializable]
    public class Creditos
    {
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public String numero_obligacion { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string linea_credito { get; set; }
        [DataMember]
        public Int64 monto { get; set; }
        [DataMember]
        public Int64 plazo { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public Int64 valor_cuota { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64 codigo_oficina { get; set; }
        [DataMember]
        public Int64 codigo_cliente { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public string fecha_solicitud_string { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public decimal monto_aprobado { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public decimal otros_saldos { get; set; }
        [DataMember]
        public Int64 calificacion_promedio { get; set; }
        [DataMember]
        public Int64 calificacion_cliente { get; set; }
        [DataMember]
        public Int64 numero_cuotas { get; set; }
        [DataMember]
        public Int64 cuotas_pagadas { get; set; }
        [DataMember]
        public DateTime fecha_prox_pago { get; set; }
        [DataMember]
        public string fecha_prox_pago_string { get; set; }
        [DataMember]
        public Int64 porc_renovacion_cuotas { get; set; }
        [DataMember]
        public Int64 porc_renovacion_montos { get; set; }
        [DataMember]
        public decimal ult_valor_pagado { get; set; }
        [DataMember]
        public decimal valor_a_pagar { get; set; }
        [DataMember]
        public decimal total_a_pagar { get; set; }
        [DataMember]
        public Int64 idinforme { get; set; }
        [DataMember]
        public Int64 dias_mora { get; set; }
        [DataMember]
        public decimal saldo_mora { get; set; }
        [DataMember]
        public decimal saldo_atributos_mora { get; set; }
        [DataMember]
        public string estado_juridico { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public string fecha_corte_string { get; set; }
        [DataMember]
        public Int64 zona { get; set; }
        [DataMember]
        public Int64 dias_mora_param { get; set; }

        #region AsesorComercial
        [DataMember]
        public Int64 CodigoAsesor { get; set; }
        [DataMember]
        public string NombreAsesor { get; set; }
        #endregion AsesorComercial
    }
}