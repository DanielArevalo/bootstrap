using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    [DataContract]
    [Serializable]
    public class Analisis_Capacidad_Pago
    {
        [DataMember]
        public int idcapacidadpago { get; set; }
        [DataMember]
        public int? idanalisis { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public decimal? ingresos { get; set; }
        [DataMember]
        public decimal? otros_ingresos { get; set; }
        [DataMember]
        public decimal? deduccion_nom { get; set; }
        [DataMember]
        public decimal? cuotas_oblig { get; set; }
        [DataMember]
        public decimal? cuotas_cod { get; set; }
        [DataMember]
        public decimal? gastos_fam { get; set; }
        [DataMember]
        public decimal? arrendamientos { get; set; }
        [DataMember]
        public decimal? honorarios { get; set; }
        [DataMember]
        public decimal? aportes { get; set; }
        [DataMember]
        public decimal? creditos { get; set; }
        [DataMember]
        public decimal? servicios { get; set; }
        [DataMember]
        public decimal? proteccion_salarial { get; set; }
        [DataMember]
        public decimal? otro_descuento { get; set; }
        [DataMember]
        public decimal? deuda_tercero { get; set; }
        [DataMember]
        public decimal? capacidad_descuento { get; set; }
        [DataMember]
        public decimal? capacidad_pago { get; set; }
    }
}