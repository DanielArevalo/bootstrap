using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    
    [DataContract]
    [Serializable]
    public class Avance
    {
        [DataMember]
        public int idavance { get; set; }
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public decimal valor_solicitado { get; set; }
        [DataMember]
        public decimal valor_aprobado { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public decimal valor_desembolsado { get; set; }
        [DataMember]
        public decimal saldo_avance { get; set; }
        [DataMember]

        public decimal saldo_capital { get; set; }
        [DataMember]
        public Int64 cuotas_pagadas { get; set; }
        [DataMember]

        public Int64 aprobar_avances { get; set; }
        [DataMember]

        public String aprobar_avance { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string observacion { get; set; }
        //AGREGADO
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public string nomlinea { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public DateTime fecha_ult_avance { get; set; }
        [DataMember]
        public decimal cupototal { get; set; }
        [DataMember]
        public decimal cupodisponible { get; set; }
        [DataMember]
        public string Nomforma_pago { get; set; }
        [DataMember]
        public string descforma_pago { get; set; }
        [DataMember]
        public Int64 cod_deudor { get; set; }


        [DataMember]
        public Int64 plazo_diferir { get; set; }
        [DataMember]
        public Int64 plazo_maximo { get; set; }


        [DataMember]
        public int diferir { get; set; }
        [DataMember]
        public int? forma_tasa { get; set; }
        [DataMember]
        public int? tipo_historico { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public int? tipo_tasa { get; set; }
        [DataMember]
        public string nom_tipo_tasa { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }

        [DataMember]
        public List<DescuentosCredito> lstDescuentosCredito { get; set; }

        // atributos credito
        [DataMember]
        public int? cod_atr { get; set; }
        [DataMember]
        public int? calculo_atr { get; set; }

        [DataMember]
        public Boolean pagoavance { get; set; }

        [DataMember]
        public long? numero_cuenta_ahorro_vista { get; set; }

    }
}

