using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace Xpinn.Seguridad.Entities
{
    [DataContract]
    [Serializable]
    public class ReporteAuditoria
    {
        #region Reporte_persona
        [DataMember]
        public Int64 cod_persona { get; set; }
        [DataMember]
        public string tipo_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public Int64 digito_verificacion { get; set; }
        [DataMember]
        public Int64 tipo_identificacion { get; set; }
        [DataMember]
        public DateTime fechaexpedicion { get; set; }
        [DataMember]
        public Int64 codciudadexpedicion { get; set; }
        [DataMember]
        public string sexo { get; set; }
        [DataMember]
        public string primer_nombre { get; set; }
        [DataMember]
        public string segundo_nombre { get; set; }
        [DataMember]
        public string primer_apellido { get; set; }
        [DataMember]
        public string segundo_apellido { get; set; }
        [DataMember]
        public string razon_social { get; set; }
        [DataMember]
        public DateTime fechanacimiento { get; set; }
        [DataMember]
        public Int64 codciudadnacimiento { get; set; }
        [DataMember]
        public Int64 codestadocivil { get; set; }
        [DataMember]
        public Int64 codescolaridad { get; set; }
        [DataMember]
        public Int64 codactividad { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public Int64 codciudadresidencia { get; set; }
        [DataMember]
        public Int64 antiguedadlugar { get; set; }
        [DataMember]
        public string tipovivienda { get; set; }
        [DataMember]
        public string arrendador { get; set; }
        [DataMember]
        public string telefonoarrendador { get; set; }
        [DataMember]
        public string celular { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string empresa { get; set; }
        [DataMember]
        public string telefonoempresa { get; set; }
        [DataMember]
        public Int64 codcargo { get; set; }
        [DataMember]
        public Int64 codtipocontrato { get; set; }
        [DataMember]
        public Int64 cod_oficina { get; set; }
        [DataMember]
        public Int64 cod_asesor { get; set; }
        [DataMember]
        public string residente { get; set; }
        [DataMember]
        public DateTime fecha_residencia { get; set; }
        [DataMember]
        public string tratamiento { get; set; }
        [DataMember]
        public string estado { get; set; }
        [DataMember]
        public Int64 cod_zona { get; set; }
        [DataMember]
        public Int64 valorarriendo { get; set; }
        [DataMember]
        public string direccionempresa { get; set; }
        [DataMember]
        public Int64 antiguedadlugarempresa { get; set; }
        [DataMember]
        public Int64 barresidencia { get; set; }
        [DataMember]
        public string dircorrespondencia { get; set; }
        [DataMember]
        public string telcorrespondencia { get; set; }
        [DataMember]
        public Int64 ciucorrespondencia { get; set; }
        [DataMember]
        public Int64 barcorrespondencia { get; set; }
        [DataMember]
        public Int64 numhijos { get; set; }
        [DataMember]
        public Int64 numpersonasacargo { get; set; }
        [DataMember]
        public string ocupacion { get; set; }
        [DataMember]
        public Int64 salario { get; set; }
        [DataMember]
        public Int64 antiguedadlaboral { get; set; }
        [DataMember]
        public Int64 estrato { get; set; }
        [DataMember]
        public string celularempresa { get; set; }
        [DataMember]
        public Int64 ciudadempresa { get; set; }
        [DataMember]
        public Int64 posicionempresa { get; set; }
        [DataMember]
        public Int64 actividadempresa { get; set; }
        [DataMember]
        public Int64 parentescoempleado { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public string tipo_cambio { get; set; }
        [DataMember]
        public DateTime fecha_cambio { get; set; }
        [DataMember]
        public string usuario_cambio { get; set; }
#endregion
        #region Reporte
        public Int64 idreporte { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        #endregion
        #region ReporteCreditos
        [DataMember]
        public Int64 numero_radicacion { get; set; }
        [DataMember]
        public string numero_obligacion { get; set; }
       
        [DataMember]
        public Int64 cod_deudor { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public Int64 monto_solicitado { get; set; }
        [DataMember]
        public Int64 monto_aprobado { get; set; }
        [DataMember]
        public Int64 monto_desembolsado { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public DateTime fecha_solicitud { get; set; }
        [DataMember]
        public DateTime fecha_aprobacion { get; set; }
        [DataMember]
        public DateTime fecha_desembolso { get; set; }
        [DataMember]
        public DateTime fecha_primerpago { get; set; }
        [DataMember]
        public Int64 numero_cuotas { get; set; }
        [DataMember]
        public Int64 cuotas_pagadas { get; set; }
        [DataMember]
        public Int64 cuotas_pendientes { get; set; }
        [DataMember]
        public Int64 cod_periodicidad { get; set; }
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public Int64 valor_cuota { get; set; }
        [DataMember]
        public string forma_pago { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public DateTime fecha_vencimiento { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public Int64 tipo_gracia { get; set; }
        [DataMember]
        public Int64 cod_atr_gra { get; set; }
        [DataMember]
        public Int64 periodo_gracia { get; set; }
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        [DataMember]
        public Int64 saldo_capital { get; set; }
        [DataMember]
        public Int64 otros_saldos { get; set; }
        [DataMember]
        public Int64 cod_asesor_com { get; set; }
        [DataMember]
        public string tipo_credito { get; set; }
        [DataMember]
        public Int64 num_radic_origen { get; set; }
        [DataMember]
        public Int64 vrreestructurado { get; set; }
        [DataMember]
        public Int64 cod_empresa { get; set; }
        [DataMember]
        public Int64 cod_pagaduria { get; set; }
        [DataMember]
        public Int64 gradiente { get; set; }
        [DataMember]
        public DateTime fecha_inicio { get; set; }
        [DataMember]
        public Int64 dias_ajuste { get; set; }

        #endregion
        #region ReporteOperacion

        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public Int64 tipo_ope { get; set; }
        [DataMember]
        public Int64 cod_usu { get; set; }
        [DataMember]
        public Int64 cod_ofi { get; set; }
        [DataMember]
        public Int64 cod_caja { get; set; }
        [DataMember]
        public Int64 cod_cajero { get; set; }
        [DataMember]
        public Int64 num_recibo { get; set; }
        [DataMember]
        public DateTime? fecha_real { get; set; }
        [DataMember]
        public DateTime? hora { get; set; }
        [DataMember]
        public DateTime? fecha_oper { get; set; }
        [DataMember]
        public DateTime? fecha_calc { get; set; }
        [DataMember]
        public Int64 num_comp { get; set; }
        [DataMember]
        public Int64 tipo_comp { get; set; }
        [DataMember]
        public Int64 estado_ope { get; set; }
        [DataMember]
        public Int64 num_lista { get; set; }
        [DataMember]
        public string tipo_cambio_ope { get; set; }
        [DataMember]
        public DateTime? fecha_cambio_ope { get; set; }
        [DataMember]
        public string usuario_cambio_ope { get; set; }
        #endregion
        #region ReporteComprobante

        [DataMember]
        public Int64 num_compr { get; set; }
        [DataMember]
        public Int64 tipo_compr { get; set; }
        [DataMember]
        public DateTime? fecha_compr { get; set; }
        [DataMember]
        public DateTime? hora_compr { get; set; }
        [DataMember]
        public string ciudad { get; set; }
        [DataMember]
        public Int64 concepto { get; set; }
        [DataMember]
        public Int64 totalcom { get; set; }
        [DataMember]
        public String tipo_benef { get; set; }
        [DataMember]
        public Int64 cod_benef { get; set; }
        [DataMember]
        public Int64 cod_elaboro { get; set; }
        [DataMember]
        public Int64 cod_aprobo { get; set; }
        [DataMember]
        public string estado_compr { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public string tipo_cambio_compr { get; set; }
        [DataMember]
        public DateTime? fecha_cambio_compr { get; set; }
        [DataMember]
        public string usuario_cambio_compr { get; set; }
        [DataMember]
        public Int64 tipo_pago_compr { get; set; }
        [DataMember]
        public Int64 numero_pago_compr { get; set; }
        [DataMember]
        public Int64 numero_documento_compr { get; set; }
        [DataMember]
        public Int64 entidad { get; set; }

        #endregion
        #region ReportePerfil
        [DataMember]
        public Int64 cod_opcion { get; set; }
        [DataMember]
        public string nombreopcion { get; set; }
        [DataMember]
        public string Insertar { get; set; }
        [DataMember]
        public string Modificar { get; set; }
        [DataMember]
        public string Borrar { get; set; }
        [DataMember]
        public string Consultar { get; set; }
        #endregion

    }
}
