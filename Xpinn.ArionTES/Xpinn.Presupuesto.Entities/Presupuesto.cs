using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Data;

namespace Xpinn.Presupuesto.Entities
{
    /// <summary>
    /// Entidad presupuesto
    /// </summary>
    [DataContract]
    [Serializable]
    public class Presupuesto
    {
        #region Encabezado
            [DataMember]
            public Int64 idpresupuesto { get; set; }
            [DataMember]
            public string descripcion { get; set; }
            [DataMember]
            public DateTime fecha_elaboracion { get; set; }
            [DataMember]
            public DateTime fecha_aprobacion { get; set; }
            [DataMember]
            public Int64 cod_elaboro { get; set; }
            [DataMember]
            public Int64 cod_aprobo { get; set; }
            [DataMember]
            public Int32 tipo_presupuesto { get; set; }
            [DataMember]
            public Int32 num_periodos { get; set; }
            [DataMember]
            public Int32 cod_periodicidad { get; set; }
            [DataMember]
            public DateTime periodo_inicial { get; set; }
            [DataMember]
            public Int32 centro_costo { get; set; }
            [DataMember]
            public double valorPromedioCredito { get; set; }
            [DataMember]
            public double porPolizasVencidas { get; set; }
            [DataMember]
            public double valorUnitPoliza { get; set; }
            [DataMember]
            public double comisionPoliza { get; set; }
            [DataMember]
            public double porLeyMiPyme { get; set; }
            [DataMember]
            public double porProvision { get; set; }
            [DataMember]
            public double porProvisionGen { get; set; }
            [DataMember]
            public double flujoinicial { get; set; }
            [DataMember]
            public DateTime fechacorte { get; set; }   
        #endregion

        #region RegistroDetalle
            [DataMember]
            public Int64 iddetalle { get; set; }
            [DataMember]
            public Int64 numero_periodo { get; set; }
            [DataMember]
            public DateTime fecha_inicial { get; set; }
            [DataMember]
            public DateTime fecha_final { get; set; }
            [DataMember]
            public string cod_cuenta { get; set; }
            [DataMember]
            public Int64 dcentro_costo { get; set; }
            [DataMember]
            public double valor { get; set; }
            [DataMember]
            public Int64 iddetallecolocacion { get; set; }
            [DataMember]
            public string item { get; set; }
            [DataMember]
            public double colocacion { get; set; }
            [DataMember]
            public Int64 codigo_obl { get; set; }
            [DataMember]
            public string descripcion_obl { get; set; }
            [DataMember]
            public double cupo_obl { get; set; }
            [DataMember]
            public double tasa_obl { get; set; }
            [DataMember]
            public double plazo_obl { get; set; }
            [DataMember]
            public double gracia_obl { get; set; }
            [DataMember]
            public double cod_periodicidad_obl { get; set; }
            [DataMember]
            public Int64 oficina_obl { get; set; }
            [DataMember]
            public double incremento { get; set; }
            [DataMember]
            public string fecha_periodo { get; set; }
        #endregion RegistroDetalle

        #region RegistroEjecutivos
            [DataMember]
            public Int64 oficina { get; set; }
            [DataMember]
            public Int64 numero_ejecutivos { get; set; }
        #endregion RegistroEjecutivos

        #region Detalle
            [DataMember]
            public DataTable dtPresupuesto { get; set; }
            [DataMember]
            public DataTable dtFlujo { get; set; }
            [DataMember]
            public DataTable dtFechas { get; set; }
            [DataMember]
            public DataTable dtColocacion { get; set; }
            [DataMember]
            public DataTable dtObligacionesNuevas { get; set; }
        #endregion
    }
}
