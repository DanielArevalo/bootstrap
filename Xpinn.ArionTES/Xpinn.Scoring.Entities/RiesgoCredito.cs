using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Scoring.Entities
{
    public class RiesgoCredito
    {
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public string oficina { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public decimal total_ingresos { get; set; }
        [DataMember]
        public decimal total_activos { get; set; }
        [DataMember]
        public decimal total_pasivos { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public decimal monto { get; set; }
        [DataMember]
        public decimal saldo_capital { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public int cuotas_pagadas { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public string cod_categoria { get; set; }
        [DataMember]
        public int dias_mora { get; set; }
        [DataMember]
        public decimal aportes { get; set; }
        [DataMember]
        public string cod_tipo_garantia { get; set; }
        [DataMember]
        public string tipo_garantia { get; set; }
        [DataMember]
        public decimal valor_avaluo { get; set; }
        [DataMember]
        public decimal valor_garantia { get; set; }
        [DataMember]
        public int reestructurado { get; set; }
        [DataMember]
        public decimal provision { get; set; }
        [DataMember]
        public decimal capacidad_pago { get; set; }
        [DataMember] 
        public decimal solvencia { get; set; }
        [DataMember]
        public decimal garantias { get; set; }
        [DataMember]
        public decimal servicio { get; set; }
        [DataMember]
        public decimal antiguedad { get; set; }
        [DataMember]
        public string centrales_riesgo { get; set; }
        [DataMember]
        public string calificacion { get; set; }
        [DataMember]
        public string segmento { get; set; }
        [DataMember]
        public Int64 score { get; set; }
        [DataMember]
        public double probabilidad_incumplimiento { get; set; }
        [DataMember]
        public string cod_categoria_pro { get; set; }
        [DataMember]
        public decimal provision_riesgo { get; set; }
        [DataMember]
        public double porc_provision_riesgo { get; set; }
        [DataMember]
        public decimal aumento_provision { get; set; }
        [DataMember]
        public double porc_provision { get; set; }
    }
}
