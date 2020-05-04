using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

namespace Xpinn.Tesoreria.Entities
{
    [DataContract]
    [Serializable]
    public class RecaudosMasivos
    {
        #region Encabezado
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public Int64 numero_recaudo { get; set; }
        [DataMember]
        public Int64 tipo_recaudo { get; set; }
        [DataMember]
        public string nom_tipo_recaudo { get; set; }
        [DataMember]
        public DateTime? periodo_corte { get; set; }
        [DataMember]
        public DateTime? fecha_aplicacion { get; set; }
        [DataMember]
        public DateTime? fecha_recaudo { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public int tipo_lista { get; set; }
        [DataMember]
        public Int64? numero_novedad { get; set; }
        [DataMember]
        public List<RecaudosMasivos> lstTemp { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public string errores { get; set; }
        [DataMember]
        public Int64? cod_oficina { get; set; }
        #endregion

        #region detalle
        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public Int64? cod_cliente { get; set; }
        [DataMember]
        public String identificacion { get; set; }
        [DataMember]
        public String nombre { get; set; }
        [DataMember]
        public String tipo_producto { get; set; }
        [DataMember]
        public String numero_producto { get; set; }
        [DataMember]
        public decimal valor { get; set; }
        [DataMember]
        public Int64 num_cuotas { get; set; }
        [DataMember]
        public String tipo_aplicacion { get; set; }
        [DataMember]
        public int estadod { get; set; }
        [DataMember]
        public decimal sobrante { get; set; }
        [DataMember]
        public String error { get; set; }
        [DataMember]
        public int cambioestado { get; set; }
        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public decimal intcte { get; set; }
        [DataMember]
        public decimal intmora { get; set; }
        [DataMember]
        public decimal seguro { get; set; }
        [DataMember]
        public decimal leymipyme { get; set; }
        [DataMember]
        public decimal ivaleymipyme { get; set; }
        [DataMember]
        public decimal otros { get; set; }
        [DataMember]
        public decimal devolucion { get; set; }
        #endregion

        #region Temp_Recaudo        
        [DataMember]
        public int tipo_productotemp { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string nom_linea { get; set; }        
        [DataMember]
        public Int64? cod_persona { get; set; }        
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }       
        #endregion


        //REPORTE CONSULTA RECAUDO
        [DataMember]
        public decimal num_devolucion { get; set; }
        [DataMember]
        public decimal valor_aplicado { get; set; }
        [DataMember]
        public decimal valor_novedad { get; set; }

        /// <summary>
        /// Agregado para el dato de la oficina en reporteproductos
        /// </summary>
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public string cod_atr { get; set; }
        [DataMember]
        public string cod_nomina_empleado { get; set; }
        [DataMember]
        public string nom_atr { get; set; }
        [DataMember]
        public int tipo_tran { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public string tipo_novedad { get; set; }
        [DataMember]
        public int cod_linea_producto { get; set; }
        [DataMember]
        public decimal saldo_producto { get; set; }
        public long codigo_usuario { get; set; }
        public string usuario_ip { get; set; }
        public bool aporte_pendiente { get; set; }
    }

}