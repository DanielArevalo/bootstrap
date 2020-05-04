using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Entities
{
    [DataContract]
    [Serializable]
    public class Aporte
    {
        #region Listas
        // LISTAS DESPLEGABLES
        [DataMember]
        public Int64 ListaId { get; set; }
        [DataMember]
        public String ListaIdStr { get; set; }
        [DataMember]
        public String ListaDescripcion { get; set; }
      
        #endregion Listas
        
        #region Aporte
        [DataMember]

        public String celular { get; set; }
        [DataMember]
        public String sexo { get; set; }
        [DataMember]

        public Int64 numero_aporte { get; set; }
        [DataMember]
        public int cod_linea_aporte { get; set; }
        [DataMember]
        public String nom_linea_aporte { get; set; }
        [DataMember]
        public int cod_oficina { get; set; }
        [DataMember]
        public string nom_oficina { get; set; }
        [DataMember]
        public Int64? cod_persona { get; set; }
        [DataMember]
        public string identificacion { get; set; }
        [DataMember]
        public string tipo_identificacion { get; set; }
        [DataMember]
        public string nombre { get; set; }
        [DataMember]
        public string cod_nomina { get; set; }
        [DataMember]
        public DateTime fecha_apertura { get; set; }
        [DataMember]
        public decimal cuota { get; set; }
        [DataMember]
        public decimal valor_base { get; set; }
        [DataMember]
        public int cod_periodicidad { get; set; }
        [DataMember]
        public string nom_periodicidad { get; set; }
        [DataMember]
        public int forma_pago { get; set; }
        [DataMember]
        public string nom_forma_pago { get; set; }
        [DataMember]
        public DateTime fecha_proximo_pago { get; set; }
        [DataMember]
        public DateTime fecha_ultimo_pago { get; set; }
        [DataMember]
        public DateTime fecha_cierre { get; set; }
        [DataMember]
        public decimal Saldo { get; set; }
        [DataMember]
        public DateTime fecha_interes { get; set; }
        [DataMember]
        public decimal total_intereses { get; set; }
        [DataMember]
        public decimal total_retencion { get; set; }
        [DataMember]
        public decimal saldo_intereses { get; set; }
        [DataMember]
        public decimal porcentaje_distribucion { get; set; }
        [DataMember]
        public decimal base_cuota { get; set; }
        [DataMember]
        public string estado_Linea { get; set; }       
        [DataMember]
        public int estado { get; set; }
        [DataMember]
        public int cod_usuario { get; set; }
        [DataMember]
        public int nom_usuario { get; set; }
        [DataMember]
        public DateTime fecha_crea { get; set; }
        [DataMember]
        public string direccion { get; set; }
        [DataMember]
        public string telefono { get; set; }
        [DataMember]
        public long principal { get; set; }
        [DataMember]
        public Int64? cod_empresa { get; set; }
        [DataMember]
        public string nom_empresa { get; set; }
        [DataMember]
        public decimal? valor_a_pagar { get; set; }
        [DataMember]
        public decimal? valor_acumulado { get; set; }
        [DataMember]
        public decimal? valor_total_acumu { get; set; }
        [DataMember]
        public decimal? porcentaje_apo { get; set; }
        [DataMember]
        public decimal porcentaje_aporte { get; set; }
        [DataMember]
        public int cod_tipo_producto { get; set; }
        [DataMember]
        public string descripcion_tipo_prod { get; set; }
        [DataMember]
        public string estadocierre { get; set; }
        //agregado para Solicitud retiro
        [DataMember]
        public string tipo_persona { get; set; }
        #endregion Aporte

        #region RetiroAportes
        [DataMember]
        public DateTime fecha_retiro { get; set; }
        [DataMember]
        public Int64 valor_retiro { get; set; }
        [DataMember]
        public String autorizacion { get; set; }
        [DataMember]
        public String observaciones { get; set; }
        [DataMember]
        public Int64 cod_ope { get; set; }
        #endregion 
        
        #region LineaAporte
        [DataMember]
        public Int64 porcentaje { get; set; }
        [DataMember]
        public Int64 grupo { get; set; }
        [DataMember]
        public Int64 tipo_aporte { get; set; }       
        [DataMember]
        public Int64 tipo_cuota { get; set; }
        [DataMember]
        public Int64 valor_cuota_minima { get; set; }
        [DataMember]
        public Int64 valor_cuota_maximo { get; set; }        
        [DataMember]
        public Int64 tipo_liquidacion { get; set; }
        [DataMember]
        public Int64 min_valor_pago { get; set; }
        [DataMember]
        public Int64 min_valor_retiro { get; set; }
        [DataMember]
        public Int64 max_valor_retiro { get; set; }
        [DataMember]
        public Int64 saldo_minimo { get; set; }
        [DataMember]
        public Int64 valor_cierre { get; set; }
        [DataMember]
        public Int64 dias_cierre { get; set; }
        [DataMember]
        public int cruzar { get; set; }
        [DataMember]
        public decimal porcentaje_cruce { get; set; }
        [DataMember]
        public int cobra_mora { get; set; }
        [DataMember]
        public int provisionar { get; set; }
        [DataMember]
        public int permite_retiros { get; set; }
        [DataMember]
        public int permite_traslados { get; set; }
        [DataMember]
        public int permite_pagoprod { get; set; }
        [DataMember]
        public decimal porcentaje_minimo { get; set; }
        [DataMember]
        public decimal porcentaje_maximo { get; set; }
        [DataMember]        
        public int distribuye { get; set; }
        [DataMember]
        public decimal porcentaje_distrib { get; set; }
        [DataMember]
        public Int64 saldo_minimo_Liqui { get; set; }
        [DataMember]
        public Int64 idgrupo { get; set; }
        //Agregado
        [DataMember]
        public int? pago_intereses { get; set; }
        [DataMember]
        public string tipo_registro { get; set; }

        #endregion LineaAporte

        #region ListadoMovimientos
        [DataMember]
        public List<MovimientoAporte> lstMovimientosProducto { get; set; }
        #endregion ListadoMovimientos
        
        #region RetiroAportes     
  
        [DataMember]
        public Int64 Identificacion { get; set; }
        [DataMember]
        public Int64 idretiro { get; set; }
        [DataMember]
        public String Nombres { get; set; }
        [DataMember]
        public decimal Saldos { get; set; }
        [DataMember]
        public String descripcion { get; set; }
        [DataMember]
        public Int64 tipo_iden{ get; set; }
        [DataMember]
        public String estadocruce { get; set; }
        [DataMember]
        public String motivoretiro { get; set; }
        [DataMember]
        public Int64 cod_motivo { get; set; }
        [DataMember]
        public int tipo_cruce { get; set; }
        [DataMember]

        public int tipo_pago_cruce { get; set; }
        [DataMember]
        public String acta { get; set; }
        [DataMember]
        public String concepto { get; set; }
        [DataMember]
        public decimal capital { get; set; }
        [DataMember]
        public decimal interes { get; set; }
        [DataMember]

        //añadido para interes causado
        public decimal interes_causado { get; set; }
        [DataMember]
        public decimal rentecioncausada { get; set; }
        [DataMember]
        public decimal intmora { get; set; }
        [DataMember]
        public decimal otros { get; set; }

        [DataMember]
        public decimal cartera { get; set; }
        [DataMember]
        public decimal retencion { get; set; }
        [DataMember]
        public String signo { get; set; }
        [DataMember]
        public decimal total { get; set; }
        [DataMember]
        public decimal saldo_cruce { get; set; }
        [DataMember]
        public Int64 numproducto { get; set; }
        [DataMember]
        public String linea_producto { get; set; }
        [DataMember]
        public String Apellidos { get; set; }

        #endregion RetiroAportes

        [DataMember]
        public List<Aporte> lstAporte { get; set; }

        [DataMember]
        public bool pendiente_crear { get; set; }
        [DataMember]
        public DateTime? fecha_ultima_mod { get; set; }
        [DataMember]
        public string estado_modificacion { get; set; }
        [DataMember]
        public DateTime? fecha_empieza_cambio { get; set; }
        [DataMember]
        public decimal nuevo_valor_cuota { get; set; }
        [DataMember]
        public long id_novedad_cambio { get; set; }
        [DataMember]
        public DateTime? fecha_novedad_cambio { get; set; }
        [DataMember]
        public List<Beneficiario> lstBeneficiarios { get; set; }
        [DataMember]
        public int? Dias_Minimo { get; set; }
        [DataMember]
        public int? Dias_Maximo { get; set; }
        [DataMember]
        public int Cod_Clasificacion { get; set; }
        [DataMember]
        public string Cod_Categoria { get; set; }
        [DataMember]
        public Int64 DiasMora { get; set; }

        //Saldos Devoluciones y CDAT
        [DataMember]
        public string numero { get; set; }
        [DataMember]
        public Int64 valor_en { get; set; }
        [DataMember]
        public string tipo { get; set; }
    }
     
          
}