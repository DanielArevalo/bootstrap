using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.Programado.Entities
{
    [DataContract]
    [Serializable]
    public class cierreCuentaDetalle
    {
        [DataMember]
        public string NumeroProgramado { get; set; }
        [DataMember]
        public String Cuenta { get; set; }
        [DataMember]
        public String Nombre { get; set; }
        [DataMember]
        public String Identificacion { get; set; }
        [DataMember]
        public Int32 TipoIdentificacion { get; set; }
        [DataMember]
        public String Descripcion_Id { get; set; }
        [DataMember]
        public DateTime Fecha_Apertura { get; set; }

        [DataMember]
        public DateTime Fecha_Vencimiento { get; set; }
        [DataMember]
        public String Oficina { get; set; }
        [DataMember]
        public String Linea { get; set; }
        [DataMember]
        public Int32 Plazo { get; set; }
        [DataMember]
        public DateTime fecha_Liquida { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public String Periodicidad { get; set; }
        [DataMember]
        public DateTime Fecha_Proximo_Pago { get; set; }
        [DataMember]
        public Int32 Codigo_Perioricidad { get; set; }

        [DataMember]
        public Int32 Codigo_Perioricidad_int { get; set; }
        [DataMember]
        public decimal Interes_ { get; set; }
        [DataMember]
        public decimal Interes_causado { get; set; }
        [DataMember]
        public decimal Retencion_causada { get; set; }
        [DataMember]
        public decimal Valor { get; set; }
        [DataMember]
        public decimal Menos_Retencion { get; set; }
        [DataMember]
        public decimal Tasa_Diferencial { get; set; }
        [DataMember]
        public decimal Menos_GMF { get; set; }
        [DataMember]
        public decimal Menos_Descuento { get; set; }
        [DataMember]
        public decimal Total_pagar { get; set; }
        [DataMember]
        public int Dias_Liquidados { get; set; }
        [DataMember]
        public Int64 saldo { get; set; }
        [DataMember]
        public Int64 TipoTasa { get; set; }
        [DataMember]
        public Int64 calculo_tasa { get; set; }
        [DataMember]
        public Int64 forma_pago { get; set; }
        [DataMember]
        public DateTime fecha_interes { get; set; }
        [DataMember]
        public decimal total_interes { get; set; }
        [DataMember]
        public decimal total_retencion { get; set; }
        [DataMember]
        public decimal tipo_historico { get; set; }
        [DataMember]
        public Int64 Cod_persona { get; set; }
        [DataMember]
        public Int64 cod_Oficina { get; set; }
        [DataMember]
        public String nombre_linea { get; set; }
        [DataMember]
        public Int32 porcentaje { get; set; }

        [DataMember]
        public decimal Tasa { get; set; }

        [DataMember]
        public decimal desviacion { get; set; }

        [DataMember]
        public Int32 Codigo_prorroga { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }

        [DataMember]
        public Int64 tipo_tran { get; set; }

        [DataMember]
        public Int64 num_tran { get; set; }
        [DataMember]
        public string concepto { get; set; }


        [DataMember]
        public Int64 origen { get; set; }

        [DataMember]
        public string NumeroProgramado_Renovado { get; set; }

        [DataMember]
        public String cod_linea { get; set; }

        [DataMember]
        public String Observacion { get; set; }
    }
}
