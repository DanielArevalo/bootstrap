using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Xpinn.ActivosFijos.Entities
{
    [DataContract]
    [Serializable]
    public class ActivoFijo
    {
        [DataMember]
        public Int64 consecutivo { get; set; }
        [DataMember]
        public Int64? cod_act { get; set; }
        [DataMember]
        public long? cod_persona { get; set; }
        [DataMember]
        public long? cod_tipo_activo_per { get; set; }
        [DataMember]
        public string str_clase { get; set; }
        [DataMember]
        public string rango_vivienda { get; set; }
        [DataMember]
        public string documentos_importacion { get; set; }
        [DataMember]
        public string localizacion { get; set; }
        [DataMember]
        public string entidad_redescuento { get; set; }
        [DataMember]
        public string margen_redescuento { get; set; }
        [DataMember]
        public string desembolso_directo { get; set; }
        [DataMember]
        public string desembolso { get; set; }
        [DataMember]
        public string tipo_vivienda { get; set; }
        [DataMember]
        public string referencia { get; set; }
        [DataMember]
        public string num_chasis { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]
        public string capacidad { get; set; }
        [DataMember]
        public int? cod_uso { get; set; }
        [DataMember]
        public int? SENALVIS { get; set; }
        [DataMember]
        public string descripcion { get; set; }
        [DataMember]
        public decimal? valor_comprometido { get; set; }
        [DataMember]
        public int clase { get; set; }
        [DataMember]
        public string nomclase { get; set; }
        [DataMember]
        public int? tipo { get; set; }
        [DataMember]
        public string nomtipo { get; set; }
        [DataMember]
        public int cod_ubica { get; set; }
        [DataMember]
        public string nomubica { get; set; }
        [DataMember]
        public int cod_costo { get; set; }
        [DataMember]
        public string nomcosto { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public double anos_util { get; set; }
        [DataMember]
        public int? estado { get; set; }
        [DataMember]
        public string nomestado { get; set; }
        [DataMember]
        public string serial { get; set; }
        [DataMember]
        public Int64 cod_encargado { get; set; }
        [DataMember]
        public string nom_encargado { get; set; }
        [DataMember]
        public string nomencargado { get; set; }
        [DataMember]
        public DateTime? fecha_compra { get; set; }
        [DataMember]
        public DateTime? fecha_importacion { get; set; }
        [DataMember]
        public decimal valor_compra { get; set; }
        [DataMember]
        public decimal valor_avaluo { get; set; }
        [DataMember]
        public decimal valor_salvamen { get; set; }
        [DataMember]
        public decimal num_factura { get; set; }
        [DataMember]
        public Int64 cod_proveedor { get; set; }
        [DataMember]
        public string nom_proveedor { get; set; }
        [DataMember]
        public string observaciones { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public string nomoficina { get; set; }
        [DataMember]
        public DateTime fecha_ult_depre { get; set; }
        [DataMember]
        public DateTime? fecha_ult_deterioro { get; set; }
        [DataMember]
        public decimal acumulado_depreciacion { get; set; }
        [DataMember]
        public decimal saldo_por_depreciar { get; set; }
        [DataMember]
        public DateTime fechacreacion { get; set; }
        [DataMember]
        public string usuariocreacion { get; set; }
        [DataMember]
        public DateTime fecultmod { get; set; }
        [DataMember]
        public string usuultmod { get; set; }
        [DataMember]
        public string imagen { get; set; }
        [DataMember]
        public string cod_cuenta_depreciacion { get; set; }
        [DataMember]
        public string nomcuenta_depreciacion { get; set; }
        [DataMember]
        public string cod_cuenta_depreciacion_gasto { get; set; }
        [DataMember]
        public string nomcuenta_depreciacion_gasto { get; set; }
        [DataMember]
        public DateTime? fecha_depreciacion { get; set; }
        [DataMember]
        public decimal? valor_a_depreciar { get; set; }
        [DataMember]
        public Int64 dias_a_depreciar { get; set; }
        [DataMember]
        public DateTime? fecha_baja { get; set; }
        [DataMember]
        public string motivo { get; set; }
        [DataMember]
        public DateTime? fecha_venta { get; set; }
        [DataMember]
        public Int64? cod_comprador { get; set; }
        [DataMember]
        public decimal? valor_venta { get; set; }
        [DataMember]
        public Boolean bDatosAdicionales { get; set; }
        [DataMember]
        public Int64? tipos { get; set; }
        [DataMember]
        public string marca { get; set; }
        [DataMember]
        public string modelo { get; set; }
        [DataMember]
        public string matricula { get; set; }
        [DataMember]
        public string notaria { get; set; }
        [DataMember]
        public string escritura { get; set; }
        [DataMember]
        public string placa { get; set; }
        [DataMember]
        public string num_motor { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public Boolean bPoliza { get; set; }
        [DataMember]
        public Int64? num_poliza { get; set; }
        [DataMember]
        public string asegurador { get; set; }
        [DataMember]
        public decimal? valor { get; set; }
        [DataMember]
        public DateTime? fecha_poliza { get; set; }
        [DataMember]
        public DateTime? fecha_vigencia { get; set; }
        [DataMember]
        public DateTime? fecha_garantia { get; set; }

        [DataMember]
        public DateTime? fecha_historico { get; set; }
        [DataMember]
        public Int64? estadop { get; set; }
        /// <summary>
        /// Agregado para el comprobante de activos fijos
        /// </summary>
        [DataMember]
        public Int64 cod_ope { get; set; }
        [DataMember]
        public int? tipo_tran { get; set; }

        //Agregado para determinar si el bien está comprometico
        [DataMember]
        public int? porcentaje_pignorado { get; set; }
        [DataMember]
        public int? hipoteca { get; set; }


        [DataMember]
        public DateTime fecha_cierre { get; set; }

        [DataMember]
        public string estadocierre { get; set; }

        //Agregado
        #region VALIABLES NIIF 


        [DataMember]
        public int codclasificacion_nif { get; set; }
        [DataMember]
        public int tipo_activo_nif { get; set; }
        [DataMember]
        public int metodo_costeo_nif { get; set; }
        [DataMember]
        public decimal valor_activo_nif { get; set; }
        [DataMember]
        public decimal vida_util_nif { get; set; }
        [DataMember]
        public decimal valor_residual_nif { get; set; }
        [DataMember]
        public decimal porcentaje_residual_nif { get; set; }
        [DataMember]
        public int unigeneradora_nif { get; set; }
        [DataMember]
        public decimal adiciones_nif { get; set; }
        [DataMember]
        public decimal vrdeterioro_nif { get; set; }
        [DataMember]
        public decimal vrrecdeterioro_nif { get; set; }
        [DataMember]
        public decimal revaluacion_nif { get; set; }
        [DataMember]
        public decimal revrevaluacion_nif { get; set; }
        [DataMember]
        public int cod_moneda { get; set; }
        [DataMember]
        public DateTime? fecha_ult_adicion { get; set; }
        [DataMember]
        public int? uso_del_bien { get; set; }

        //TABLA LEASING

        [DataMember]
        public Boolean bLeasing { get; set; }
        [DataMember]
        public int codigo_act { get; set; }
        [DataMember]
        public int numero_cuotas { get; set; }
        [DataMember]
        public decimal valor_cuota { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public decimal opcion_compra { get; set; }


        [DataMember]
        public string nomTipoNif { get; set; }
        [DataMember]
        public string nomMetodo { get; set; }
        [DataMember]
        public string nomMoneda { get; set; }
        [DataMember]
        public string nomClasificacion { get; set; }
        [DataMember]
        public decimal valoractual { get; set; }



        #endregion


        #region DeterioroNIF


        [DataMember]
        public int codigo_ope { get; set; }
        [DataMember]
        public DateTime fecha { get; set; }
        [DataMember]
        public decimal valor_deterioro { get; set; }


        #endregion

        [DataMember]
        public string nom_centro { get; set; }
        [DataMember]
        public int idActivo { get; set; }
    }
}

