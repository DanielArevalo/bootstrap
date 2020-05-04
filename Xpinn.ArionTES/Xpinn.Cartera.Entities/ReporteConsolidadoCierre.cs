using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Xpinn.Cartera.Entities
{
    public class ReporteConsolidadoCierre
    {
        [DataMember]
        public DateTime fecha_historico { get; set; }
        [DataMember]
        public string Clasificacion { get; set; }
        [DataMember]
        public string FormaPago { get; set; }
        [DataMember]
        public string TipoGarantia { get; set; }
        [DataMember]
        public string Categoria { get; set; }
        [DataMember]
        public string Oficina { get; set; }
        [DataMember]
        public decimal SaldoCapital { get; set; }
        [DataMember]
        public long NumeroCreditos { get; set; }
        [DataMember]
        public decimal Porcentaje { get; set; }
        [DataMember]
        public string CodigoCategoria { get; set; }
        [DataMember]
        public string Atributo { get; set; }
        [DataMember]
        public long CodigoAtributo { get; set; }
        [DataMember]
        public decimal SaldoCausado { get; set; }
        [DataMember]
        public decimal SaldoOrden { get; set; }
        [DataMember]
        public decimal ValorProvision { get; set; }

        [DataMember]
        public string CodigoLinea { get; set; }

        [DataMember]
        public string LineaCredito { get; set; }
    }
}