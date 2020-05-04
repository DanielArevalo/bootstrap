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
    public class EmpresaNovedad
    {
        #region Encabezado
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public Int64? numero_novedad { get; set; }
        [DataMember]
        public Int64? tipo_recaudo { get; set; }
        [DataMember]
        public string nom_tipo_recaudo { get; set; }
        [DataMember]
        public DateTime? periodo_corte { get; set; }
        [DataMember]
        public DateTime? fecha_generacion { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public string nom_estado { get; set; }
        [DataMember]
        public string usuario { get; set; }
        [DataMember]
        public int? tipo_lista { get; set; }
        [DataMember]
        public string nom_tipo_lista { get; set; }
        [DataMember]
        public string concepto { get; set; }
        //AGREGADO
        [DataMember]
        public List<EmpresaNovedad> lstTemp { get; set; }
        [DataMember]
        public List<EmpresaNovedad> lstTempNuevos { get; set; }

        [DataMember]
        public DateTime? fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime? fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }

        [DataMember]
        public DateTime? fecha_novedad { get; set; }
        [DataMember]
        public DateTime? fecha_inicial { get; set; }
        [DataMember]
        public DateTime? fecha_final { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public string numero_planilla { get; set; }
        [DataMember]
        public decimal valor_total { get; set; }
        [DataMember]
        public DateTime fecha_radicacion { get; set; }
        [DataMember]
        public Int64 codciudad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string periodo_dscto { get; set; }
        [DataMember]
        public string vr_campo_fijo { get; set; }
        [DataMember]
        public string periodicidad { get; set; }
        [DataMember]
        public decimal saldo { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal total_saldo { get; set; }
        #endregion

        #region detalle
        [DataMember]
        public Int64 iddetalle { get; set; }
        [DataMember]
        public Int64 cod_cliente { get; set; }
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
        public String tipo_novedad { get; set; }
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
        [DataMember]
        public int cod_estructura_carga { get; set; }
        [DataMember]
        public decimal total_fijos { get; set; }
        [DataMember]
        public decimal total_prestamos { get; set; }
        
        #endregion

        #region Temp_Recaudo
        [DataMember]
        public int? tipo_productotemp { get; set; }
        [DataMember]
        public string nom_tipo_producto { get; set; }
        [DataMember]
        public string linea { get; set; }
        [DataMember]
        public string nom_linea { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string cod_nomina_empleado { get; set; }
        [DataMember]
        public string nombres { get; set; }
        [DataMember]
        public string apellidos { get; set; }
        [DataMember]
        public string nombres1 { get; set; }
        [DataMember]
        public string apellidos1 { get; set; }
        [DataMember]
        public string nombres2 { get; set; }
        [DataMember]
        public string apellidos2 { get; set; }
        [DataMember]
        public DateTime? fecha_proximo_pago { get; set; }
        [DataMember]
        public int? tipo_identificacion { get; set; }
        [DataMember]
        public string desc_tipo_identificacion { get; set; }
        [DataMember]
        public long? idconsecutivo { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipo_calculo { get; set; }
        [DataMember]
        public long codigo_pagaduria { get; set; }

        [DataMember]
        public long vacaciones { get; set; }

        [DataMember]
        public DateTime? fecha_inicio_producto { get; set; }
        [DataMember]
        public DateTime? fecha_vencimiento_producto { get; set; }

        #endregion

    }

}