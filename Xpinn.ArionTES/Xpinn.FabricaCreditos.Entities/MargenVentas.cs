using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad MargenVentas
    /// </summary>
    [DataContract]
    [Serializable]
    public class MargenVentas
    {
        [DataMember] 
        public Int64 cod_margen { get; set; }
        [DataMember] 
        public Int64 cod_ventas { get; set; }
        [DataMember] 
        public string tipoproduco { get; set; }
        [DataMember] 
        public string nombreproducto { get; set; }
        [DataMember] 
        public Int64 univendida { get; set; }
        [DataMember] 
        public Int64 costounidven { get; set; }
        [DataMember] 
        public Int64 preciounidven { get; set; }
        [DataMember]
        public Int64 costoventa { get; set; }
        [DataMember] 
        public Int64 ventatotal { get; set; }
        [DataMember]
        public Double margen { get; set; }
        [DataMember]
        public Int64 utilidad { get; set; }
        [DataMember]
        public Int64 cod_persona { get; set; }


        //Resultado de los calculos totales
        [DataMember]
        public Int64 totalCostoVenta { get; set; }
        [DataMember]
        public Int64 totalVentaTotal { get; set; }
        [DataMember]
        public Double porcentajeCostoVenta { get; set; }
        [DataMember]
        public Double porcentajeMargen { get; set; }
        [DataMember]
        public Int64 diasLaborados { get; set; }


    }
}