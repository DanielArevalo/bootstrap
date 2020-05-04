using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.FabricaCreditos.Entities
{
    /// <summary>
    /// Entidad LineasCredito
    /// </summary>
    [DataContract]
    [Serializable]
    public class LineasCredito
    {
        // Estos atributos los puso Martha Noriega toca después normalizarlos
        #region LineasMarNor
        [DataMember]
        public string Codigo { get; set; }
        //[DataMember]
        //public string Nombre { get; set; }        
        [DataMember]
        public String Maneja_Pergracia { get; set; }
        [DataMember]
        public Int16? Periodo_Gracia { get; set; }
        [DataMember]
        public Int16? Tipo_Periodic_Gracia { get; set; }
        [DataMember]
        public String Modifica_Datos { get; set; }
        [DataMember]
        public String Modifica_Fecha_Pago { get; set; }
        [DataMember]
        public String Garantia_Requerida { get; set; }
        [DataMember]
        public Int16? Tipo_Capitalizacion { get; set; }
        [DataMember]
        public Double? Cuotas_Extras { get; set; }
        [DataMember]
        public Int16? Cod_Clasifica { get; set; }
        [DataMember]
        public Int16? Numero_Codeudores { get; set; }
        [DataMember]
        public Int16? Cod_Moneda { get; set; }
        [DataMember]
        public Double? Porc_Corto { get; set; }
        [DataMember]
        public Int16? Tipo_Amortiza { get; set; }
        [DataMember]
        public Int16? Estado { get; set; }
        [DataMember]
        public Double? Monto_Maximo { get; set; }
        [DataMember]
        public Double? Plazo_Maximo { get; set; }
        #endregion

        #region Lineas
        [DataMember]
        public Int64 cod_lineacredito { get; set; }
        [DataMember]
        public string cod_linea_credito { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string nom_linea_credito { get; set; }
        [DataMember]
        public Int64? tipo_linea { get; set; }
        [DataMember]
        public Int64? tipo_liquidacion { get; set; }
        [DataMember]
        public Int64? tipo_cupo { get; set; }
        [DataMember]
        public Int64? recoge_saldos { get; set; }
        [DataMember]
        public Int64? cobra_mora { get; set; }
        [DataMember]
        public Int64? modifica { get; set; }
        [DataMember]
        public Int64? tipo_refinancia { get; set; }
        [DataMember]
        public decimal? minimo_refinancia { get; set; }
        [DataMember]
        public decimal? maximo_refinancia { get; set; }
        [DataMember]
        public string maneja_pergracia { get; set; }
        [DataMember]
        public Int64 periodo_gracia { get; set; }
        [DataMember]
        public string tipo_periodic_gracia { get; set; }
        [DataMember]
        public string modifica_datos { get; set; }
        [DataMember]
        public string modifica_fecha_pago { get; set; }
        [DataMember]
        public string garantia_requerida { get; set; }
        [DataMember]
        public Int64 tipo_capitalizacion { get; set; }
        [DataMember]
        public Int64 cuotas_extras { get; set; }
        [DataMember]
        public Int64 cod_clasifica { get; set; }
        [DataMember]
        public Int64 numero_codeudores { get; set; }
        [DataMember]
        public Int64 cod_moneda { get; set; }
        [DataMember]
        public Int64 porc_corto { get; set; }
        [DataMember]
        public Int64 tipo_amortiza { get; set; }
        [DataMember]
        public Int64 estado { get; set; }
        [DataMember]
        public Int64 cod_rango_atr { get; set; }
        [DataMember]
        public Int64 tipo_tope { get; set; }
        [DataMember]
        public string maneja_excepcion { get; set; }
        [DataMember]
        public int cuota_intajuste { get; set; }
        [DataMember]
        public Int64 cod_atr { get; set; }
        [DataMember]
        public string calculo_atr { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public string tipoliquidacion { get; set; }
        [DataMember]
        public string tiposdescuento { get; set; }
        [DataMember]
        public Int64 tipo_descuento { get; set; }
        [DataMember]
        public string Formadescuento { get; set; }
        [DataMember]
        public Int64 Forma_descuento { get; set; }
        [DataMember]
        public string valor { get; set; }
        [DataMember]
        public decimal valor1 { get; set; }
        [DataMember]
        public string numero_cuotas { get; set; }
        [DataMember]
        public int numero_cuotas1 { get; set; }
        [DataMember]
        public string tipoimpuesto { get; set; }
        [DataMember]
        public string descripcion_tipo_tasa { get; set; }
        [DataMember]
        public Int64? tipotasa { get; set; }
        [DataMember]
        public string tipotasaNom { get; set; }
        [DataMember]
        public decimal? tasa { get; set; }
        [DataMember]
        public string formacalculo { get; set; }
        [DataMember]
        public decimal? desviacion { get; set; }
        [DataMember]
        public Int64? tipo_historico { get; set; }
        [DataMember]
        public Int64 idtope { get; set; }
        [DataMember]
        public string maximo { get; set; }
        [DataMember]
        public string minimo { get; set; }
        [DataMember]
        public Int64 cod_tipo_impuesto { get; set; }
        [DataMember]
        public List<LineasCredito> lstAtributos { get; set; }
        [DataMember]
        public List<RangosTopes> lstTopes { get; set; }
        [DataMember]
        public Int64 plazo_diferir { get; set; }
        [DataMember]
        public Int64 avances_aprob { get; set; }
        [DataMember]
        public Int64 desem_ahorros { get; set; }
        [DataMember]
        public Int64 aplica_tercero { get; set; }
        [DataMember]
        public Int64 aplica_asociado { get; set; }
        [DataMember]
        public Int64 aplica_empleado{ get; set; }
        [DataMember]
        public int? numero { get; set; }
        [DataMember]
        public int credito_gerencial { get; set; }
        [DataMember]
        public int credito_educativo { get; set; }
        [DataMember]
        public int educativo { get; set; }
        [DataMember]
        public int credito_x_linea { get; set; }
        [DataMember]
        public int orden_servicio { get; set; }
        [DataMember]
        public int prioridad { get; set; }
        [DataMember]
        public int maneja_auxilio { get; set; }
        [DataMember]
        public int diferir { get; set; }
        [DataMember]
        public DateTime fecha_corte { get; set; }
        [DataMember]
        public Int64 cantidad_comision { get; set; }
        [DataMember]
        public Int64 valor_comision { get; set; }
        [DataMember]
        public Int64 signo_comision { get; set; }
        public int causa { get; set; }
        [DataMember]
        public int aporte_garantia { get; set; }
        [DataMember]
        public int afiancol { get; set; }
        [DataMember]
        public string meses_gracia { get; set; }
        #endregion

        #region Entidades de Documentos 

        //tabla docrequeridoslinea
        [DataMember]
        public Int64? tipo_documento { get; set; }       
        [DataMember]
        public int checkbox { get; set; }
        [DataMember]
        public string aplica_codeudor { get; set; }
        [DataMember]
        public List<LineasCredito> lstDocumentos { get; set; }
        [DataMember]
        public List<LineasCredito> lstGarantiaDoc { get; set; }
        [DataMember]
        public List<ProcesoLineaCredito> lstProcesoLinea { get; set; }
        [DataMember]
        public List<ProcesoLineaCredito> LstParametrosLinea { get; set; }
        [DataMember]
        public List<LineasCredito> lstPrioridad { get; set; }
        [DataMember]
        public int? requerido { get; set; }
        [DataMember]
        public string plantilla { get; set; }
        [DataMember]
        public int consecutivo { get; set; }
        [DataMember]
        public object cod_empresa { get; set; }
        [DataMember]
        public List<LineasCredito> lstempresa { get; set; }
        [DataMember]
        public List<LineasCredito> lstdestinacion { get; set; }
        [DataMember]
        public int es_orden { get; set; }
        //Se agrega para parametrizar la linea de credito con la destinacion
        [DataMember]
        public int cod_destino { get; set; }
        [DataMember]
        public Int64 cod_lineacred { get; set; }
        [DataMember]
        public int seleccionar { get; set; }

        #endregion

    }

}
