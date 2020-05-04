using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Package.Data;
using Xpinn.Util;
using Xpinn.Package.Entities;
using System.Data.Common;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Package.Business
{
    public class clscredito : GlobalPackage
    {
        private packageData DApackage;
        private funciones BOFunciones;

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Descripción: Esta paquete contiene los procedimientos necesarios para el manejo de créditos                    -- 
        //-- Autor: LFOM
        //-- Fecha Creación:                                                                                                -- 
        //-- Fecha Ultima Modificación:                                                                                     -- 
        //-------------------------------------------------------------------------------------------------------------------- 

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Atributos del crédito 
        //-------------------------------------------------------------------------------------------------------------------- 
        Int64? n_radic;                 // Número de radicación del crédito.X
        decimal? n_monto_sol;           // Monto solicitado.X
        decimal? n_monto;               // Monto aprobado del crédito.X
        DateTime? f_sol;                // fecha de solicitud.X
        DateTime? f_fec_apro;           // fecha de aprobación.X
        DateTime? f_fec_desembolso;     // fecha de desembolso.X
        decimal? n_plazo;               // plazo o número de cuotas del crédito.X
        int? n_for_pla;                 // formato en que esta expresado el plazo es igual a la periodicidad.X
        decimal? n_cuota;               // valor de la cuota del crédito.X
        DateTime? f_fec_term;           // fecha de terminación del crédito.X
        DateTime? f_fec_inicio;         // fecha de inicio del crédito para pago de intereses.X
        int? n_periodic;                // periodicidad de amortización de la cuota.X
        string s_for_pag;               // forma de pago C = Caja, N = Nomina.X
        string s_cod_credi;             // Código de línea de credito.X
        string s_tipo_liq;              // Código de tipo de liquidación.X
        decimal? n_saldo;               // Saldo de capital del crédito.X
        int? n_cuo_pag;                 // Número de cuotas pagadas.X
        DateTime? f_prox_pag;           // fecha de próximo pago de la cuota.X
        int? n_dia_habil;               // Indica si el crédito maneja días habiles.X
        int? n_tip_gracia;              // Indica el tipo de gracia que maneja el crédito.X
        string s_atr_gracia;            // Si maneja gracia por atributos indica el atributo sobre el cual maneja gracia.X
        decimal? n_duracion_gracia;     // Número de cuotas de gracia.X
        int? n_periodic_gracia;         // tipo de periodicidad en que esta expresado el periodo de gracia.X
        string s_estado;                // Estado del desembolso del crédito.X
        Int64? n_cod_cliente;           // codigo del deudor.X
        decimal? n_cuo_credito;         // número de cuotas del crédito.X
        DateTime? f_prim_pago;          // fecha de primer pago del crédito.X
        DateTime? f_ult_pago;           // fecha de último pago del crédito.X
        string s_estado_cre;            // estado del crédito.X
        Int64? n_cod_empresa;           // código de la empresa por la que se descuenta el crédito.X
        Int64? n_cod_pagaduria;         // código de la pagaduria por la que se descuenta el crédito
        string s_tipo_grad;             // tipo de gradiente.X
        decimal? n_val_grad;            // valor del gradiente.X
        int? n_for_pag;                 // código de forma de pago.X
        int? n_tipo_prod;               // tipo de producto 1=Crédito, 2=Servicios.X
        decimal? n_dias_aju;            // Dias de ajuste del crédito, es entre fecha aprobación y fecha de inicio.X
        int? n_tipo_cal;                // tipo de calendario del crédito(comercial o calendario). 
        decimal? n_dias_per_cre;        // dias de amortización según la periodicidad del crédito
        int? n_cuotas_gracia;           // Cuotas de gracia a manejar
        int? n_num_cuotas;              // número de cuotas del crédito
        decimal? n_tasa_interes = null; // tasa de interés total períodica del crédito
        decimal? n_tasa_intcte;         // tasa de interés corriente
        decimal? n_tasa_intmor;         // tasa de interés de mora
        int? n_plazo_mes;               // plazo en meses del crédito
        int? n_cod_clasifica;           // Código de clasificación del crédito
        decimal? n_tir;                 // TIR del crédito
        decimal? n_monto_desembolsado;  // Valor desembolsado del crédito
        int? n_tipo_linea;              // Tipo de línea de crédito

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Atributos Financiados del crédito 
        //-------------------------------------------------------------------------------------------------------------------- 
        clatrfinan[] atr_finan;
        int n_atr_fin;
        clsatrotr[] atr_otro;
        int n_atr_otr;

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Cuotas extras del crédito 
        //-------------------------------------------------------------------------------------------------------------------- 
        clscuotasextras[] cuotasextras;
        int n_num_cuoext;
        bool bterminos;

        // -------------------------------------------------------------------------------------------------------------------- 
        //-- Determina los parametros de la línea de crédito 
        //-------------------------------------------------------------------------------------------------------------------- 
        string s_cob_mor;               // indica si se cobra mora al crédito
        decimal? n_porc_corto;          // porcentaje de amortización del crédito en corto plazo(crédito educativo). 
        string s_tipo_amortiza;         // tipo de amortización(crédito educativo). 
        int? n_clasificacion;           // Calificación del crédito(CONSUMO, COMERCIAL, VIVIENDA, MICROCREDITO). 

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Determina los parametros según el tipo de liquidación del crédito.
        //-------------------------------------------------------------------------------------------------------------------- 
        int? n_tip_cuota = null;        // tipo de cuota del crédito(1=Pago Unico, 2=Serie Uniforme, 3=Gradiente). 
        int? n_tip_tf;                  // tipo de terminos fijos
        int? n_tip_inttf;               // tipo de interés de terminos fijos(1=Simple, 2=Compuesto)
        int? n_tip_amo;                 // tipo de amortización de la cuota
        int? n_tip_pago;                // tipo de pago del crédito(1=Anticipado, 2=Vencido)
        int? n_tip_gra;                 // tipo de gradiente
        int? n_tip_int;                 // tipo de interés del crédito
        int? n_tip_paginttf;            // tipo de pago de interés de terminos fijos
        int? n_tip_intant;              // tipo de interés de ajuste
        int? n_per_dia;                 // código de la periodicidad díaria.
        int? n_gracia;                  // dias de gracia
        bool b_exist_gra;               // determina si el crédito maneja período de gracia
        int? n_tip_mor;                 // Es el tipo de interés de mora 1=Sobre Atributos, 2=Sobre saldos insolutos
        string s_error_men;             // Mensajes de error 

        //--------------------------------------------------------------------------------------------------------------------   
        //-- Variables para manejo de los pendientes 
        //-------------------------------------------------------------------------------------------------------------------- 
        DateTime?[] f_fechas_pago;
        clsamortiza_cre[] cl_amortiza_cre;
        int n_cont_amortiza;
        int n_cont_rep;
        clsrep_tran_cred[] cl_detalle_pago;
        DateTime?[] f_fechas_mora;
        int n_cont_mora;
        int n_cont_mora_ini;
        clsdet_mora_cre[] cl_det_mora_cre;

        //--------------------------------------------------------------------------------------------------------------------   
        //-- Variables para el manejo del desembolso 
        //-------------------------------------------------------------------------------------------------------------------- 
        decimal? n_adi_monto;
        decimal? n_des_cheque;
        int?[] n_cod_atr_des = new int?[99];
        string[] s_nom_atr_des = new string[99];
        int?[] n_tip_atr_des = new int?[99];
        decimal?[] n_val_atr_des = new decimal?[99];
        int n_num_atr_des;
        decimal? n_adi_cuota;

        //--------------------------------------------------------------------------------------------------------------------   
        //-- Variables para el manejo de pagos por atributos 
        //--------------------------------------------------------------------------------------------------------------------     
        int?[] n_cod_atr_pago = new int?[99];
        decimal?[] n_val_atr_pago = new decimal?[99];
        int n_num_atr_pago;
        int n_man_pago_atr;

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Otros atributos del crédito 
        //-------------------------------------------------------------------------------------------------------------------- 
        bool g_b_yaprimero;                 // Indica si es el primer ciclo esto cuando se maneja Ley MiPyme
        decimal? g_n_monto_credito;
        bool g_b_cruce;                     // Indica si el proceso corresponde a un cruce de cuentas.
        DateTime? g_f_usura;                // Es la fecha a la cual se determina la tasa de usura
        int? gn_tipo_pago;
        bool g_b_historico_amortiza;
        bool b_LeyMiPyme;                   // Determina si el crédito maneja Ley MiPyme
        public int? n_atr_LeyMiPyme = 40;   // Es el código del atributo que corresponde a Ley MiPyme
        public int? n_atr_IVALeyMiPyme = 41;// Es el código del atributo que corresponde a IVA Ley MiPyme
        int? n_atr_CTACOB = 43;             // Es el código del atributo para cuentas por cobrar de Garantias Comunitarias.
        int? n_atr_PREJUR = 11;             // Es el código del atributo para cobro de Pre-Juridico.
        int? n_atr_JURID = 12;              // Es el código del atributo para cobro de Pre-Juridico.
        int? n_atr_CTAATR = 44;             // Es el código del atributo para cobro de Cuenta por Cobrar.
        public int? n_atr_GARCOM = 14;      // Es el código del atributo que corresponde Garantias Comunitars
        int? n_atr_IVAGARCOM = 15;          // Es el código del atributo que corresponde a IVA Garantias Comunitarias
        int? n_atr_APORTE = 8;              // Es el código del atributo que corresponde a aportes sociales
        string s_calc_int;                  // Determina el tipo de calculo del interes 1=Simple, 2=Compuesto
        string s_calc_int_mora;             // Determina el tipo de calculo de la mora 1=Simple, 2=Compuesto
        int? n_atr_mora;                    // Código del atributo de interés de mora
        int? n_atr_corr;                    // Código del atributo de interés corriente
        int? n_atr_seguro;                  // Código del atributo de seguro
        int? n_atrib_gra;                   // Código del atributo para cobro de los intereses del período de gracia
        int? n_dias_gracia_mora;            // Días de gracia a partir de los cuales se cobra mora
        string s_mor_sig;                   // Indica si se cobra o no la mora en el siguiente pago.
        bool b_cambio_mora;
        bool b_tasa_variable;
        int? n_num_codeu;                   // Número de codeudores del crédito.
        decimal? n_val_interes = 0;
        decimal? n_valorCatg;
        decimal? n_vrComer;
        decimal? n_valfactor;
        decimal? n_dias_fijo;               // Indica el plazo en días para créditos de plazo fijo
        bool b_calculo;                     // Cuando se hace la liquidación indica si se vuelve a calcular fechas
        decimal? n_cuota_adi;
        decimal? n_val_indiv;
        decimal? n_acumulado_int;
        int? p_n_num_dias_per;
        int? n_tipo_tasa_usura;             // Indica el tipo de tasa de usura a manejar
        bool b_calcular_ini;                // En la liquidación se indica si se vuelve o no a calcular la fecha de inicio
        decimal? n_int_aju = null;          // Valor calculado de intereses de ajuste
        decimal? n_tot_interes;             // Valor total que se cobra al crédito de intereses
        int? n_dias_ajuste;
        decimal? n_val_grad_per;
        int? n_per_gradiente;
        int?[] n_cod_atr_par;               // Codigo del atributo para valores pendientes de la cuota
        DateTime?[] d_fec_pago_par;         // fecha de cuota para valores pendientes de la cuota
        decimal?[] n_valor_par;             // valor pendiente de la cuota
        decimal?[] n_valor_dis;             // valor distribuido de la cuota
        int n_num_par;                     // numero de pendientes de cuota 

        //-------------------------------------------------------------------------------------------------------------------- 
        //-- Variables para la generación del plan de pagos 
        //-------------------------------------------------------------------------------------------------------------------- 
        int? sn_cuota_desc;
        decimal? sn_monto;
        int? sn_atr_adm;
        decimal? sn_monto_cuo;
        decimal? sn_saldo;
        decimal? sn_saldo_cuo;
        decimal? sn_cuota;
        int? sn_dias;
        DateTime? sf_fecha;
        DateTime? sf_fec_tf;
        int? sn_pos_tf;
        int sn_num_atr_fin;
        int? sn_num_atr;
        decimal?[] sn_tasa_atr_fin;
        decimal? sn_cuota_grad;
        decimal? sn_cuota_ant;
        decimal? sn_cuota_gracia;
        bool sb_existe_reestr;
        decimal? sn_val_atr_rees;
        decimal? n_interes_gracia;
        bool b_detalle;

        Usuario usuario = new Usuario();

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Inicializar variables de los créditos  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public void Inicializar()
        {
            BOFunciones = new funciones(usuario);
            DApackage = new packageData();
            string s_atr_aporte;
            b_calculo = false;
            // Tipo de cálculo de interés corriente
            s_calc_int = BOFunciones.BuscarGeneral(450, 1);
            // Tipo de cálculo de interés de mora
            s_calc_int_mora = BOFunciones.BuscarGeneral(451, 1);
            // Código de atributo de interés de mora
            n_atr_mora = BOFunciones.AtrMora();
            // Código de atributo de interés corriente
            n_atr_corr = BOFunciones.AtrCorriente();
            // Atributo para Aportes
            s_atr_aporte = BOFunciones.BuscarGeneral(250, 1);
            if (s_atr_aporte != "")
                n_atr_APORTE = Convert.ToInt32(s_atr_aporte);
            // Días de gracia para cobro de mora
            n_dias_gracia_mora = Convert.ToInt32(BOFunciones.BuscarGeneral(420, 1));
            if (n_dias_gracia_mora < 0)
                n_dias_gracia_mora = 0;
            // Determina si se cobra mora en el siguiente pago
            s_mor_sig = BOFunciones.BuscarGeneral(300, 1);
            // Código del atributo de gradiente
            n_atrib_gra = Convert.ToInt32(BOFunciones.BuscarGeneral(970, 2));
            // Determina cambio de mora
            if (BOFunciones.BuscarGeneral(1160, 1) == "1")
                b_cambio_mora = true;
            else
                b_cambio_mora = false;
            // Inicialización de variables
            n_atr_fin = 0;
            atr_finan = new clatrfinan[20];
            // Inicializar arreglo para cargar las tasas
            sn_tasa_atr_fin = new decimal?[20];
            // Inicializando arreglo de atributos descontados
            n_atr_otr = 0;
            atr_otro = new clsatrotr[20];
            // Inicializando variable de control de cuotas extras
            bterminos = false;
            // Inicializando arreglo para pendientes de cuotas de créditos            
            n_cont_amortiza = 0;
            cl_amortiza_cre = new clsamortiza_cre[3000];
            // Inicializando arreglo de valores a pagar por cuota.
            n_cont_rep = 0;
            cl_detalle_pago = new clsrep_tran_cred[3000];
            // Inicializando valores de interés de mora
            n_cont_mora = 0;
            cl_det_mora_cre = new clsdet_mora_cre[3000];
            // Inicializar cuotas extras
            n_num_cuoext = 0;
            cuotasextras = new clscuotasextras[100];
            // Datos de la línea de crédito
            s_cob_mor = "";
            n_porc_corto = null;
            s_tipo_amortiza = "";
            n_clasificacion = null;
            n_adi_monto = 0;
            // Inicializando arreglo de atributos descontados
            n_num_atr_des = 0;
            if (n_cod_atr_des.Length > 0)
                n_cod_atr_des.Initialize();
            if (s_nom_atr_des.Length > 0)
                s_nom_atr_des.Initialize();
            if (n_val_atr_des.Length > 0)
                n_val_atr_des.Initialize();
            if (f_fechas_pago.Length > 0)
                f_fechas_pago.Initialize();
            f_fechas_pago = new DateTime?[999];
            f_fechas_mora = new DateTime?[999];
            // Inicializando datos del crédito
            n_radic = null;
            n_monto_sol = null;
            n_monto = null;
            f_sol = null;
            f_fec_apro = null;
            f_fec_desembolso = null;
            n_plazo = null;
            n_for_pla = null;
            n_cuota = null;
            f_fec_term = null;
            f_fec_inicio = null;
            f_prim_pago = null;
            n_periodic = null;
            s_for_pag = null;
            s_cod_credi = null;
            s_tipo_liq = null;
            n_saldo = null;
            n_cuo_pag = null;
            f_prox_pag = null;
            n_dia_habil = null;
            n_tip_gracia = null;
            s_atr_gracia = null;
            n_duracion_gracia = null;
            n_periodic_gracia = null;
            s_estado = null;
            n_cod_cliente = null;
            n_cuo_credito = null;
            f_ult_pago = null;
            s_estado_cre = null;
            n_cod_empresa = null;
            n_cod_pagaduria = null;
            n_cod_clasifica = null;
            n_cod_atr_par = new int?[100];
            d_fec_pago_par = new DateTime?[100];
            n_valor_par = new decimal?[100];
            n_valor_dis = new decimal?[100];
            b_detalle = false;
            g_b_historico_amortiza = false;
            s_error_men = "";
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Cargar datos del crédito a liquidar o amortizar  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Cargar_Credito(Int64 pn_radic)
        {
            //-- Cursor para cargar atributos financiados
            //pBasedato SYS_REFCURSOR;  
            //pConexion SYS_REFCURSOR;
            string sentencia = "";
            bool bRetorna = false;
            decimal? n_val_aux = 0;
            int n_cont_1;
            decimal? n_tasa_dia_tf = null;
            int n_num = 0;
            //-- Variables para cargar los atributos financiados
            int? n_cod_atr = null;
            string s_tip_cal = "";
            decimal? n_tasa = null;
            int? n_tip_tas = null;
            int? n_tip_his = null;
            decimal? n_desv_tas = null;
            int? n_per_pro = null;
            string s_tip_gra = "";
            decimal? n_gradiente = null;
            int? n_cob_mor = null;
            int? n_tip_des = null;
            decimal? n_valor = null;
            int? n_tip_liq = null;
            int? lforma_descuento = null;
            int? n_num_cuotas_desc = null;
            //-- Variables para las cuotas extras
            DateTime? f_ter;
            decimal? n_ter;
            decimal? n_valcap_ter;
            decimal? n_saldo_ter;
            decimal? n_valint_ter;
            decimal? n_salint_ter;
            int? n_forpag_ter;
            string c_est_ter;
            int? n_num_ter;
            // -- Otras variables
            int n_num_cuota_desc;
            decimal? n_valor_presente_desc;
            decimal? n_valor_desc;
            int? atr_depende;
            int? cont = 0;
            int? lValida;

            Inicializar();
            // -- Carga datos del crédito
            Package.Entities.Credito datosCred = new Package.Entities.Credito();
            if (!DApackage.ConsultarCredito(pn_radic, ref datosCred, usuario))
            {
                n_monto_sol = datosCred.monto_solicitado;
                n_monto = datosCred.monto_aprobado;
                f_sol = datosCred.fecha_solicitud;
                f_fec_apro = datosCred.fecha_aprobacion;
                n_plazo = datosCred.numero_cuotas;
                n_for_pla = Convert.ToInt32(datosCred.cod_periodicidad);
                n_cuota = datosCred.valor_cuota;
                f_fec_term = datosCred.fecha_vencimiento;
                f_fec_inicio = datosCred.fecha_inicio;
                n_periodic = Convert.ToInt32(datosCred.cod_periodicidad);
                s_for_pag = datosCred.forma_pago;
                s_cod_credi = datosCred.cod_linea_credito;
                s_tipo_liq = Convert.ToString(datosCred.tipo_liquidacion);
                n_cod_clasifica = datosCred.cod_clasifica;
                n_saldo = datosCred.saldo_capital;
                n_cuo_pag = Convert.ToInt32(datosCred.cuotas_pagadas);
                f_prox_pag = datosCred.fecha_proximo_pago;
                n_dia_habil = 1;
                n_tip_gracia = datosCred.tipo_gracia;
                s_atr_gracia = "2";
                n_duracion_gracia = datosCred.periodo_gracia;
                n_periodic_gracia = Convert.ToInt32(datosCred.cod_periodicidad);
                s_estado = datosCred.estado;
                n_cod_cliente = datosCred.cod_deudor;
                n_cuo_credito = datosCred.cuotas_pagadas;
                f_ult_pago = datosCred.fecha_ultimo_pago;
                s_estado_cre = datosCred.estado;
                n_cod_empresa = datosCred.cod_empresa;
                n_cod_pagaduria = datosCred.cod_pagaduria;
                s_tipo_grad = "1";
                n_val_grad = datosCred.gradiente;
                n_radic = datosCred.numero_radicacion;
                n_dias_aju = datosCred.dias_ajuste;
                s_tip_gra = "1";
                n_gradiente = datosCred.gradiente;
                f_prim_pago = datosCred.fecha_primerpago;
                f_fec_desembolso = datosCred.fecha_desembolso;
                n_tir = datosCred.tir;
                n_monto_desembolsado = datosCred.monto_desembolsado;

                DApackage.ControlError(pn_radic, null, "No existe un crédito con la radicación " + pn_radic, 0, usuario);
                return false;
            }
            if (n_monto == 0 && s_estado != "C" && s_estado != "T" && ((s_estado != "A" && s_estado != "G") || n_monto == null || n_monto == 0))
                n_monto = n_monto_sol;
            if (n_monto_sol == 0)
            {
                DApackage.ControlError(pn_radic, null, "El monto del crèdito " + n_radic + " no puede ser cero", 0, usuario);
                return false;
            }
            if (n_plazo == 0)
            {
                DApackage.ControlError(pn_radic, null, "El plazo del crèdito " + n_radic + " no puede ser cero", 0, usuario);
                return false;
            }
            if (s_estado_cre == "C")
                if (f_prox_pag == null)
                {
                    DApackage.ControlError(pn_radic, null, "La fecha de pròximo pago del crèdito " + n_radic + " no puede ser nula", 0, usuario);
                    return false;
                }

            //-- Determinando la forma de pago
            if (s_for_pag == "C")
                n_for_pag = 1;
            else if (s_for_pag == "N")
                n_for_pag = 2;
            else
                n_for_pag = 1;

            // -- Validando la fecha de inicio
            if (f_fec_inicio == null)
                f_fec_inicio = f_fec_apro;

            //-- Carga datos de la línea o programa de crédito
            n_tip_mor = 1;
            lineascredito eLineas = new lineascredito();
            eLineas = DApackage.Consultarlineascredito(s_cod_credi, usuario);
            s_cob_mor = Convert.ToString(eLineas.cobra_mora);
            n_porc_corto = eLineas.porc_corto;
            s_tipo_amortiza = Convert.ToString(eLineas.tipo_amortiza);
            n_clasificacion = eLineas.cod_clasifica;
            n_tipo_linea = eLineas.tipo_linea;
            if (n_cod_clasifica == null)
                n_cod_clasifica = n_clasificacion;

            //-- Actualiza el tipo de calendario dependiendo de la periodicidad del crédito
            DApackage.ConsultaPeriodicidad(n_periodic, ref n_dias_per_cre, ref n_tipo_cal, usuario);
            if (n_tipo_cal == null || n_tipo_cal < 1 || n_tipo_cal > 2)
                n_tipo_cal = 1;

            //-- Buscado parámetros de liquidación
            tipoliquidacion etipoliq = new tipoliquidacion();
            etipoliq = DApackage.Consultartipoliquidacion(Convert.ToInt32(s_tipo_liq), usuario);

            //-- Calcula la periodicidad diaria en el caso que sea un plazo fijo
            n_per_dia = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);

            //-- Si es un plazo fijo se toma la periodicidad diaria
            if (n_tip_cuota == 1)
                n_periodic = n_per_dia;

            //-- Consulta los datos de la gracia
            if ((n_tip_gracia == 2 || n_tip_gracia == 3 || n_tip_gracia == 4) && n_duracion_gracia != 0)
            {
                n_gracia = 1;
                b_exist_gra = true;
            }

            //-- Si hay gracia transforma el tiempo de esta al número de cuotas
            if (n_gracia == 1 && n_duracion_gracia != 0)
                n_cuotas_gracia = Convert.ToInt32(BOFunciones.CalNumCuotas(n_duracion_gracia, n_periodic_gracia, n_periodic));
            else
                n_cuotas_gracia = 0;

            //-- Calculando número de cuotas
            if (n_tip_cuota == 1)
                n_num_cuotas = 1;
            else
                n_num_cuotas = Convert.ToInt32(BOFunciones.CalNumCuotas(n_plazo, n_for_pla, n_periodic));

            //-- Validar las cuotas contra el período de gracia
            if (n_gracia == 1 && n_cuotas_gracia > 0)
                if (n_num_cuotas <= n_cuotas_gracia)
                    n_cuotas_gracia = n_num_cuotas - 1;

            //-- Iniciar las variables de tasa de interés
            n_tasa_interes = 0;
            n_tasa_intcte = 0;
            n_tasa_intmor = 0;
            b_tasa_variable = false;

            //-- Cargando los atributos que hacen parte de la cuota del crédito
            n_atr_fin = 0;
            List<atributoscredito> lstatributos = new List<atributoscredito>();
            lstatributos = DApackage.Listaratributoscredito(Convert.ToInt64(n_radic), usuario);
            foreach (atributoscredito item in lstatributos)
            {
                n_cod_atr = item.cod_atr;
                s_tip_cal = item.calculo_atr;
                n_tasa = item.tasa;
                n_tip_tas = item.tipo_tasa;
                n_tip_his = item.tipo_historico;
                n_desv_tas = item.desviacion;
                n_per_pro = 1;
                n_cob_mor = item.cobra_mora;
                //-- Cargando valores a la clase
                n_atr_fin = n_atr_fin + 1;
                atr_finan[n_atr_fin].SetAtrfinan(n_cod_atr, s_tip_cal, n_tasa, n_tip_tas, n_tip_his, n_desv_tas, n_per_pro, s_tip_gra, n_gradiente, n_cob_mor, 0, 0);
                //-- Determinar la tasa de interés del período
                atr_finan[n_atr_fin].Conv_Tasa(n_periodic, n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                //-- Determinar la tasa total sin incluir la tasa de interés de mora
                if (n_cod_atr != n_atr_mora)
                    n_tasa_interes = n_tasa_interes + atr_finan[n_atr_fin].n_tasa_calculo;
                // -- Calculando el valor de la tasa de interés corriente
                if (n_cod_atr == n_atr_corr)
                    n_tasa_intcte = atr_finan[n_atr_fin].n_tasa_calculo;
                // -- Calculando el valor de la tasa de interés de mora
                if (n_cod_atr == n_atr_mora)
                    n_tasa_intmor = atr_finan[n_atr_fin].n_tasa_calculo;
                // -- Determinando si se maneja tasa variable
                if (s_tip_cal == "5")
                    b_tasa_variable = true;
            }

            // -- Convierte el plazo a meses
            n_plazo_mes = Convert.ToInt32(DApackage.ConPeriodicidadNumeroMeses(n_for_pla, usuario));
            n_plazo_mes = Convert.ToInt32(n_plazo) * n_plazo_mes;

            // -- Busca numero de codeudores.
            n_num_codeu = DApackage.ConsultarNumeroCodeudores(Convert.ToInt64(n_radic), usuario);

            //-- Carga otro tipo de atributos
            b_LeyMiPyme = false;
            n_adi_monto = 0;
            n_des_cheque = 0;
            n_atr_otr = 1;
            sentencia = "";
            List<descuentoscredito> lstdescuentos = new List<descuentoscredito>();
            lstdescuentos = DApackage.Listardescuentoscredito(Convert.ToInt64(n_radic), usuario);
            foreach (descuentoscredito item in lstdescuentos)
            {
                n_cod_atr = item.cod_atr;
                n_tip_des = item.tipo_descuento;
                n_valor = item.valor;
                n_tip_liq = item.tipo_liquidacion;
                lforma_descuento = item.forma_descuento;
                n_cob_mor = item.cobra_mora;
                n_num_cuotas_desc = Convert.ToInt32(item.numero_cuotas);
                // -- Cargando valores a la clase
                n_atr_otr = n_atr_otr + 1;
                atr_otro[n_atr_otr].SetAtrOtr(n_cod_atr, n_tip_des, n_valor, n_tip_liq, lforma_descuento, n_cob_mor, n_num_cuotas_desc);
                if (atr_otro[n_atr_otr].n_num_cuotas != null && (atr_otro[n_atr_otr].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_atr_otr].n_cod_atr == n_atr_IVALeyMiPyme))
                {
                    if (atr_otro[n_atr_otr].n_tip_des == 4)
                        lValida = 0;
                    lValida = DApackage.ConsultarNumeroCodeudores(Convert.ToInt32(atr_otro[n_atr_otr].n_cod_atr), Convert.ToDecimal(n_monto), usuario);
                    if (lValida <= 0)
                    {
                        atr_otro[n_atr_otr].n_num_cuotas = null;
                        atr_otro[n_atr_otr].n_tip_des = 1;
                        atr_otro[n_atr_otr].n_valor = 0;
                    }
                }
                if (atr_otro[n_atr_otr].n_signo == 3 && atr_otro[n_atr_otr].n_num_cuotas != null && atr_otro[n_atr_otr].n_num_cuotas > 0 && (atr_otro[n_atr_otr].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_atr_otr].n_cod_atr == n_atr_IVALeyMiPyme))
                {
                    //-- Si el plazo para LeyMiPyme excede el año entonces se recalcula
                    if (n_num_cuotas_desc * n_dias_per_cre > 360 && n_dias_per_cre != null && n_dias_per_cre != 0)
                    {
                        n_num_cuotas_desc = Convert.ToInt32(360 / n_dias_per_cre);
                        atr_otro[n_atr_otr].n_num_cuotas = n_num_cuotas_desc;
                    }
                    b_LeyMiPyme = true;
                }
                // -- Calculando el valor del atributo
                atr_otro[n_atr_otr].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref lforma_descuento, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bRetorna);
                if (bRetorna == false)
                    break;
                //-- Si el atributo es financiado y se suma al monto del crédito entonces sumar a valores a financiar de otros atributos
                if (lforma_descuento == 1)
                    n_des_cheque = n_des_cheque + n_val_aux;
                else if (lforma_descuento != 5)
                    n_adi_monto = n_adi_monto + n_val_aux;
                // -- Establecer si el atributo depende de otros atributos.
                atr_otro[n_atr_otr].n_num_atr = 0;
                List<atributo_depende> lsatrdep = new List<atributo_depende>();
                lsatrdep = DApackage.Listaratributo_depende(Convert.ToInt32(atr_otro[n_atr_otr].n_cod_atr), usuario);
                foreach (atributo_depende itemdep in lsatrdep)
                {
                    atr_depende = itemdep.depende;
                    atr_otro[n_atr_otr].n_num_atr = atr_otro[n_atr_otr].n_num_atr + 1;
                    atr_otro[n_atr_otr].n_atributo_depende[atr_otro[n_atr_otr].n_num_atr] = atr_depende;
                    if (atr_otro[n_atr_otr].n_num_atr == 0)
                        atr_otro[n_atr_otr].n_valor_calculo = 0;
                    n_cont_1 = 1;
                    while (n_cont_1 <= n_atr_otr)
                    {
                        if (atr_otro[n_cont_1].n_cod_atr == atr_otro[n_atr_otr].n_atributo_depende[atr_otro[n_atr_otr].n_num_atr])
                        {
                            if (atr_otro[n_atr_otr].n_tip_des == 3)
                            {
                                atr_otro[n_atr_otr].n_valor_calculo = atr_otro[n_atr_otr].n_valor_calculo + (atr_otro[n_cont_1].n_valor_calculo * atr_otro[n_atr_otr].n_valor / 100);
                                atr_otro[n_atr_otr].n_valor_calculo = BOFunciones.Round(atr_otro[n_atr_otr].n_valor_calculo);
                            }
                            else
                            {
                                atr_otro[n_atr_otr].n_valor = atr_otro[n_atr_otr].n_valor + atr_otro[n_cont_1].n_valor_calculo;
                                atr_otro[n_atr_otr].n_valor_calculo = atr_otro[n_atr_otr].n_valor_calculo + atr_otro[n_cont_1].n_valor_calculo;
                            }
                        }
                        n_cont_1 = n_cont_1 + 1;
                    }
                }
                // -- Cargar valores de atributos que se cobran si ya los calculó
                atr_otro[n_atr_otr].n_valor_presentes = new decimal?[200];
                atr_otro[n_atr_otr].n_valor_pagos = new decimal?[200];
                n_num_cuota_desc = 0;
                n_valor_presente_desc = 0;
                n_valor_desc = 0;
                atr_otro[n_atr_otr].n_num_pag = 0;
                List<descuentosvalores> lstdescuentosval = new List<descuentosvalores>();
                lstdescuentosval = DApackage.Listardescuentosvalores(Convert.ToInt64(n_radic), Convert.ToInt32(atr_otro[n_atr_otr].n_cod_atr), usuario);
                foreach (descuentosvalores itemval in lstdescuentosval)
                {
                    n_num_cuota_desc = Convert.ToInt32(itemval.num_cuota);
                    n_valor_presente_desc = itemval.valor_presente;
                    n_valor_desc = itemval.valor;
                    atr_otro[n_atr_otr].n_num_pag = atr_otro[n_atr_otr].n_num_pag + 1;
                    if (n_num_cuota_desc > atr_otro[n_atr_otr].n_valor_presentes.Length)
                        atr_otro[n_atr_otr].n_valor_presentes = new decimal?[n_num_cuota_desc];
                    if (n_num_cuota_desc > atr_otro[n_atr_otr].n_valor_pagos.Length)
                        atr_otro[n_atr_otr].n_valor_pagos = new decimal?[n_num_cuota_desc];
                    atr_otro[n_atr_otr].n_valor_presentes[n_num_cuota_desc] = n_valor_presente_desc;
                    atr_otro[n_atr_otr].n_valor_pagos[n_num_cuota_desc] = n_valor_desc;
                }
            }
            // -- Cargar atributo para garantías comunitarias
            n_atr_otr = n_atr_otr + 1;
            atr_otro[n_atr_otr].SetAtrOtr(n_atr_CTACOB, 1, 0, 1, 3, 0, null);
            //-- Calculo de la fecha inicial y dias de ajuste para creditos que no esten desembolsados
            if (s_estado_cre == "S" || f_fec_inicio == null)
                CalFechas(null);
            // -- Carga de terminos fijos
            List<cuotasextras> lstcuotasextras = new List<Entities.cuotasextras>();
            lstcuotasextras = DApackage.Listarcuotasextras(Convert.ToInt64(n_radic), usuario);
            foreach (cuotasextras item in lstcuotasextras)
            {
                f_ter = item.fecha_pago;
                n_ter = item.valor;
                n_valcap_ter = item.saldo_capital;
                n_saldo_ter = item.valor_interes;
                n_valint_ter = item.valor_interes;
                n_salint_ter = item.saldo_interes;
                n_forpag_ter = Convert.ToInt32(item.forma_pago);
                n_num_ter = item.cod_cuota;
                bterminos = true;
                c_est_ter = "1";
                SetCuotaExtra(f_ter, n_ter, n_valcap_ter, n_saldo_ter, n_valint_ter, n_salint_ter, n_forpag_ter, c_est_ter, n_num_ter);
            }
            // -- Calculo de capital e interés de terminos fijos o primas para destinaciones
            if (n_tip_tf == 1)
            {
                if (bterminos)
                {
                    DApackage.ControlError(pn_radic, null, "No se pueden tener terminos fijos de acuedo al tipo de liquidación", 0, usuario);
                    return false;
                }
            }
            else
            {
                // -- Calcula tasa diaria para los terminos fijos
                n_per_dia = DApackage.ConPeriodicidadDiaria(n_tipo_cal, usuario);
                if (n_per_dia == null || n_per_dia < 0)
                    //ControlError(pn_radic, Null, "No se encontro periodicidad diaria para el tipo de calendario: " || n_tipo_cal || ". Verifique periodicidades. " || n_per_dia, 0);
                    return false;
                n_tasa_dia_tf = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_paginttf, n_per_dia, 0);
                n_tasa_dia_tf = n_tasa_dia_tf / 100;
                //--Calcula valor de capital e interés de términos fijos si el crédito todavía esta en proceso
                if (s_estado_cre != "C" && s_estado_cre != "T")
                    if (n_tip_amo != 5 && !(n_tip_tf == 4 && n_tip_inttf == 1))
                        if (bterminos)
                            CalcularInteresCuotasExtras(n_tip_tf, n_tip_inttf, n_tasa_dia_tf, n_tipo_cal, f_fec_inicio);
            }
            // -- Insertando como atributo descontado interés de terminos fijos anticipado
            if (bterminos)
            {
                if (n_tip_tf != 1 && n_tip_amo != 5 && n_tip_tf != 4)
                {
                    if (n_tip_paginttf == 1)
                    {
                        if (ExistenCuotasExtras())
                        {
                            if (cuotasextras[1].n_interes != 0)
                            {
                                n_atr_otr = n_atr_otr + 1;
                                atr_otro[n_atr_otr].Ins_Dat_Basico(0, "Interes Anticipado TF", cuotasextras[1].n_interes, 1);
                                n_des_cheque = n_des_cheque + cuotasextras[1].n_interes;
                            }
                        }
                    }
                }
            }
            //-- Calcular el valor por cada atributo financiado
            n_num = 1;
            while (n_num < n_atr_fin)
            {
                if (!Calcular_Atr_Fin(n_num, n_tasa_dia_tf))
                    return false;
                n_num = n_num + 1;
            }
            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Cargar datos para cuando son créditos nuevos.Carga los datos de la línea de crédito  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Cargar_Datos(decimal? pmonto, decimal? pplazo, int? pfor_pla, int? pperiodic, DateTime? pfecha, int? pfor_pag, string pcod_credi, int? ptipo_liquidacion, int? pn_cod_deudor, Int64? pempresarecuado)
        {
            int? n_cod_atr;
            string s_tip_cal;
            decimal? n_tasa;
            int? n_tip_tas;
            int? n_tip_his;
            decimal? n_desv_tas;
            decimal? n_per_pro;
            string s_tip_gra;
            decimal? n_gradiente;
            int? n_cob_mor;
            int? n_tip_des;
            decimal? n_valor;
            int? n_tip_liq;
            int? n_signo;
            int? n_cuotas_desc;
            decimal? n_val_aux;
            bool bRetorna;
            int? cod_rango;
            bool bencontro;
            int? ntipo_tope;
            string sminimo;
            string smaximo;
            decimal? smlmv;
            int? antiguedad;
            int? reciprocidad;
            decimal? lsaldo_aporte;
            DateTime? lfecha_afiliacion;
            string lcontrol = string.Empty;
            // --Variables para las cuotas extras
            DateTime? f_ter = null;
            decimal? n_ter = null;
            decimal? n_valcap_ter = null;
            decimal? n_saldo_ter = null;
            decimal? n_valint_ter = null;
            decimal? n_salint_ter = null;
            int? n_forpag_ter = null;
            string c_est_ter = null;
            int? n_num_ter = null;
            decimal? n_suma_ter = null;

            // Inicializar las variables.
            Inicializar();
            // Cargar las variables del crédito
            n_monto_sol = pmonto;
            n_monto = pmonto;
            n_plazo = pplazo;
            n_for_pla = pfor_pla;
            n_periodic = pperiodic;
            f_sol = pfecha;
            f_fec_apro = pfecha;
            if (f_fec_inicio == null)
                f_fec_inicio = pfecha;
            n_for_pag = pfor_pag;
            if (pfor_pag == 1)
                s_for_pag = "C";
            else if (pfor_pag == 2)
                s_for_pag = "N";
            else
                s_for_pag = "C";
            s_cod_credi = pcod_credi;
            n_cod_empresa = pempresarecuado;
            // Cargar datos de la línea de crédito
            s_tipo_liq = "";
            s_cob_mor = "";
            n_per_gradiente = 3;
            lineascredito lineas = new lineascredito();
            lineas = DApackage.Consultarlineascredito(s_cod_credi, usuario);
            if (lineas == null)
            {
                Mensaje_Error("No existe el tipo de liquidación definido");
            }
            s_tipo_liq = Convert.ToString(lineas.tipo_liquidacion);
            s_cob_mor = Convert.ToString(lineas.cobra_mora);
            n_clasificacion = lineas.cod_clasifica;
            if (ptipo_liquidacion != null)
                s_tipo_liq = Convert.ToString(ptipo_liquidacion);
            // Buscado parámetros de liquidación
            tipoliquidacion etipoliq = new tipoliquidacion();
            n_tip_cuota = etipoliq.tipo_cuota;
            n_tip_tf = 4;
            n_tip_inttf = 1;
            n_tip_amo = etipoliq.tip_amo;
            n_tip_pago = etipoliq.tipo_pago;
            n_tip_gra = 0;
            n_tip_int = etipoliq.tipo_interes;
            n_tip_paginttf = 2;
            n_tip_intant = etipoliq.tipo_intant;
            // Actualiza el tipo de calendario dependiendo de la periodicidad del crédito
            n_tipo_cal = DApackage.ConPeriodicidadTipoCal(n_periodic, usuario);
            if (n_tipo_cal == null || n_tipo_cal < 1 || n_tipo_cal > 2)
            {
                Mensaje_Error("El tipo de calendario se encuentra errado");
            }
            // Actualiza el tipo de calendario dependiendo de la periodicidad del crédito
            DApackage.ConsultaPeriodicidad(n_periodic, ref n_dias_per_cre, ref n_tipo_cal, usuario);
            if (n_tipo_cal == null || n_tipo_cal < 1 || n_tipo_cal > 2)
                n_tipo_cal = 1;

            // Convierte el plazo a meses
            n_plazo_mes = null;
            n_plazo_mes = Convert.ToInt32(DApackage.ConPeriodicidadNumeroMeses(Convert.ToInt32(n_for_pla), usuario));
            n_plazo_mes = Convert.ToInt32(n_plazo * n_plazo_mes);

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Atributos financiados  
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            cod_rango = null;
            List<rangosatributos> lstrangos = new List<rangosatributos>();
            lstrangos = DApackage.Listarrangosatributos(s_cod_credi, usuario);
            bencontro = false;
            foreach (rangosatributos item in lstrangos)
            {
                cod_rango = item.cod_rango_atr;
                lcontrol = " ";
                bencontro = true;
                List<rangostopes> lsttopes = new List<rangostopes>();
                lsttopes = DApackage.Listarrangostopes(s_cod_credi, Convert.ToInt32(cod_rango), usuario);
                foreach (rangostopes itemtop in lsttopes)
                {
                    ntipo_tope = itemtop.tipo_tope;
                    sminimo = itemtop.minimo;
                    smaximo = itemtop.maximo;
                    {
                        if (ntipo_tope == 0)        //-- Ningún tope
                        {
                            antiguedad = 0;
                        }
                        else if (ntipo_tope == 1)   //-- Fecha de aprobación
                        {
                            if (sminimo != null && smaximo != null)
                            {
                                if (pfecha < BOFunciones.To_Date(sminimo) || pfecha > BOFunciones.To_Date(smaximo))
                                    bencontro = false;
                            }
                        }
                        else if (ntipo_tope == 2)    //-- Plazos
                        {
                            if (sminimo != null && smaximo != null)
                            {
                                if (sminimo == null)
                                    sminimo = smaximo;
                                if (smaximo == null)
                                    smaximo = sminimo;
                                if (n_plazo_mes < BOFunciones.To_Number(sminimo) && n_plazo_mes > BOFunciones.To_Number(smaximo))
                                    bencontro = false;
                            }
                        }
                        else if (ntipo_tope == 3)   //-- Montos
                        {
                            if (sminimo != null && smaximo != null)
                            {
                                if (sminimo == null)
                                    sminimo = smaximo;
                                if (smaximo == null)
                                    smaximo = sminimo;
                                if (pmonto < BOFunciones.To_Number(sminimo) && pmonto > BOFunciones.To_Number(smaximo))
                                    bencontro = false;
                            }
                        }
                        else if (ntipo_tope == 6)   //-- Antiguedad
                        {
                            if (sminimo != null && smaximo != null)
                            {
                                antiguedad = 0;
                                lfecha_afiliacion = DApackage.ConsultarFechaAfiliacion(Convert.ToInt64(pn_cod_deudor), usuario);
                                if (lfecha_afiliacion != null)
                                {
                                    if (lfecha_afiliacion <= pfecha)
                                    {
                                        antiguedad = BOFunciones.FecDifDia(lfecha_afiliacion, pfecha, 1);
                                        antiguedad = Convert.ToInt32(BOFunciones.Round(antiguedad / 30));
                                    }
                                }
                                if ((antiguedad < BOFunciones.To_Number(sminimo) && sminimo != null) && (antiguedad > BOFunciones.To_Number(smaximo) && smaximo != null))
                                {
                                    lcontrol = lcontrol + " ANTIGUEDAD: Actual=" + antiguedad.ToString() + " Requerida: " + BOFunciones.NVL(sminimo, "") + " a " + BOFunciones.NVL(smaximo, "");
                                    bencontro = false;
                                }
                            }
                        }
                        else if (ntipo_tope == 8)   //-- Reciprocidad aportes
                        {
                            if (sminimo != null || smaximo != null)
                            {
                                reciprocidad = 0;
                                lsaldo_aporte = DApackage.ConsultarSaldoAportes(Convert.ToInt64(pn_cod_deudor), usuario);
                                if (lsaldo_aporte != 0 && lsaldo_aporte != null)
                                    reciprocidad = Convert.ToInt32(BOFunciones.Round(pmonto / lsaldo_aporte));
                                if ((reciprocidad < BOFunciones.To_Number(sminimo) && sminimo != null) || (reciprocidad > BOFunciones.To_Number(smaximo) && smaximo != null))
                                {
                                    lcontrol = lcontrol + " RECIPROCIDAD APORTES: Actual=" + reciprocidad + " Requerida: " + BOFunciones.NVL(sminimo, "") + " a " + BOFunciones.NVL(smaximo, "") + " Saldo aportes:" + BOFunciones.NVL(lsaldo_aporte, 0);
                                    bencontro = false;
                                }
                            }
                        }
                        else if (ntipo_tope == 9)   //-- Salarios mínimos legales vigentes
                        {
                            if (sminimo != null || smaximo != null)
                            {
                                if (sminimo == null)
                                    sminimo = smaximo;
                                if (smaximo == null)
                                    smaximo = sminimo;
                                smlmv = Convert.ToDecimal(DApackage.ConGeneral(10, usuario));
                                if (pmonto < BOFunciones.To_Number(sminimo) * smlmv || pmonto > BOFunciones.To_Number(smaximo) * smlmv)
                                    bencontro = false;
                            }
                        }
                        else
                        {
                            bencontro = false;
                        }
                    }
                    if (!bencontro)
                        break;
                }
                if (bencontro)
                    break;
            }
            if (cod_rango != null && bencontro == false)
            {
                Mensaje_Error("No se encontraron condiciones en la línea que correspondan con los montos y plazos estipulados. Línea " + s_cod_credi + " " + lcontrol);
            }

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //--CUOTAS EXTRAS SIMULACION
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            n_suma_ter = 0;
            List<cuotasextras> lstTFijos = DApackage.ListarTEMPTerminosFijos(usuario);
            foreach (cuotasextras item in lstTFijos)
            {
                f_ter = item.fecha_pago; n_ter = item.valor; n_valcap_ter = item.valor_capital; n_saldo_ter = item.saldo_capital; n_valint_ter = item.valor_interes; n_salint_ter = item.saldo_interes; n_forpag_ter = Convert.ToInt32(item.forma_pago); n_num_ter = item.cod_cuota;
                bterminos = true;
                c_est_ter = "1";
                n_suma_ter = n_suma_ter + n_ter;
                SetCuotaExtra(f_ter, n_ter, n_valcap_ter, n_saldo_ter, n_valint_ter, n_salint_ter, n_forpag_ter, c_est_ter, n_num_ter);
            };

            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Atributos financiados  
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            n_atr_fin = 0;
            List<atributoslinea> lstAtrLin = new List<atributoslinea>();
            lstAtrLin = DApackage.Listaratributoslinea(s_cod_credi, Convert.ToInt32(cod_rango), usuario);
            foreach (atributoslinea item in lstAtrLin)
            {
                n_cod_atr = item.cod_atr;
                s_tip_cal = item.calculo_atr;
                n_tasa = item.tasa;
                n_tip_tas = item.tipo_tasa;
                n_tip_his = item.tipo_historico;
                n_desv_tas = item.desviacion;
                n_per_pro = 1;
                s_tip_gra = "1";
                n_gradiente = 0;
                n_cob_mor = item.cobra_mora;
                n_atr_fin = n_atr_fin + 1;
                atr_finan[n_atr_fin].SetAtrfinan(n_cod_atr, s_tip_cal, n_tasa, n_tip_tas, n_tip_his, n_desv_tas, Convert.ToInt32(n_per_pro), s_tip_gra, n_gradiente, n_cob_mor, 0, 0);
            }
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Atributos sumados al monto o descontados del cheque  
            //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            n_atr_otr = 0;
            List<descuentoslinea> lstDescLin = new List<descuentoslinea>();
            lstDescLin = DApackage.Listardescuentoslinea(s_cod_credi, usuario);
            foreach (descuentoslinea item in lstDescLin)
            {
                n_cod_atr = item.cod_atr;
                n_tip_des = item.tipo_descuento;
                n_valor = item.valor;
                n_tip_liq = item.tipo_liquidacion;
                n_signo = Convert.ToInt32(item.forma_descuento);
                n_cob_mor = item.cobra_mora;
                n_cuotas_desc = item.numero_cuotas;
                n_atr_otr = n_atr_otr + 1;
                n_val_aux = 0;
                bRetorna = false;
                atr_otro[n_atr_otr].SetAtrOtr(n_cod_atr, n_tip_des, n_valor, n_tip_liq, n_signo, n_cob_mor, n_cuotas_desc);
                //-- Establecer si tiene Ley MiPyme
                if (atr_otro[n_atr_otr].n_signo == 3 && atr_otro[n_atr_otr].n_num_cuotas != null && atr_otro[n_atr_otr].n_num_cuotas > 0 && (atr_otro[n_atr_otr].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_atr_otr].n_cod_atr == n_atr_IVALeyMiPyme))
                {
                    //--Si el plazo para LeyMiPyme excede el año entonces se recalcula
                    if (n_cuotas_desc * n_dias_per_cre > 360 && n_dias_per_cre != null && n_dias_per_cre != 0)
                    {
                        n_cuotas_desc = Convert.ToInt32(360 / n_dias_per_cre);
                        atr_otro[n_atr_otr].n_num_cuotas = n_cuotas_desc;
                    }
                    b_LeyMiPyme = true;
                }
                //-- Calculando el valor del atributo
                atr_otro[n_atr_otr].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signo, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bRetorna);
                if (bRetorna == false)
                    break;
                //-- Establecer si el atributo depende de otros atributos.
                atr_otro[n_atr_otr].n_num_atr = 0;
                List<atributo_depende> lstatrdep = new List<atributo_depende>();
                lstatrdep = DApackage.Listaratributo_depende(Convert.ToInt32(atr_otro[n_atr_otr].n_cod_atr), usuario);
                foreach (atributo_depende itemdep in lstatrdep)
                {
                    atr_otro[n_atr_otr].n_atributo_depende[atr_otro[n_atr_otr].n_num_atr] = itemdep.depende;
                    atr_otro[n_atr_otr].n_num_atr = atr_otro[n_atr_otr].n_num_atr + 1;
                }
            }
            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Calcular valores de los atributos financiados  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Calcular_Atr_Fin(int pn_num_atr, decimal? n_tasa_dia_tf)
        {
            decimal? n_monto_cal;
            decimal? n_factor;
            decimal? n_val_tfpro = null;
            decimal? n_aux;
            if (pn_num_atr <= 0)
                return false;
            // --Adicionando valores sumados al monto
            n_monto_cal = n_monto + BOFunciones.NVL(n_adi_monto, 0);
            // --Descontado del monto valor de capital de tèrminos fijos
            if (n_tip_tf == 1)
            {
                DApackage.ControlError(n_radic, null, "El tipo de terminos fijos es incorrecto. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                return false;
            }
            else
            {
                if (n_tip_amo != 5 && n_tip_tf != 4)
                    n_monto_cal = n_monto_cal - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0);
            }

            //--Inicializando variables de atributos
            atr_finan[pn_num_atr].n_valor = 0;

            //--Verificando que el número de cuotas sea correcto
            if (n_num_cuotas <= 0)
            {
                DApackage.ControlError(n_radic, null, "El número de cuotas del atributo es menor o igual a cero. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                return false;
            }

            //--Calculando valor de cada atributo segùn parámetros de liquidación
            //--Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente
            if (n_tip_cuota == 1)
            {
                if (n_num_cuotas > 1)
                {
                    DApackage.ControlError(n_radic, null, "El número de cuotas es mayor de uno y el crédito es un plazo fijo. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                    return false;
                }

                //--Tipo de interés:  1:Simple,  2:Compuesto
                if (n_tip_int == 1)
                    atr_finan[pn_num_atr].n_valor = (atr_finan[pn_num_atr].n_tasa_calculo / 100) * n_num_cuotas * n_monto_cal;
                else if (n_tip_int == 2)
                    atr_finan[pn_num_atr].n_valor = (BOFunciones.Power((atr_finan[pn_num_atr].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1) * n_monto_cal;
                else
                {
                    DApackage.ControlError(n_radic, null, "El tipo de interes debe ser 1 o 2. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                    return false;
                }

                //--Tipo de interés:  1:Anticipado,  2:Vencido
                if (n_tip_pago != 1 && n_tip_pago != 2)
                {
                    DApackage.ControlError(n_radic, null, "El tipo pago debe ser 1 o 2. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                    return false;
                }
            }
            else if (n_tip_cuota == 2)
            {
                //-- Tipo de interés: 1:Simple,  2:Compuesto
                if (n_tip_int == 1)
                {
                    if (n_tip_amo == 1)
                        atr_finan[pn_num_atr].n_valor = n_monto_cal * n_num_cuotas * (atr_finan[pn_num_atr].n_tasa_calculo / 100);
                    else
                    {
                        DApackage.ControlError(n_radic, null, "El tipo de amortización debe ser 1 porque el crédito tiene interes simple. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                        return false;
                    }
                }
                else if (n_tip_int == 2)
                {
                    //-- Tipo amortización:  2: KF, IV; 3: KF, IF; 4: CV, IV   5: T.F.Prorrateados
                    if (n_tip_amo == 2)
                        n_aux = 0;
                    else if (n_tip_amo == 3)
                        n_aux = 0;
                    else if (n_tip_amo == 4)
                    {
                        if (n_tasa_interes != 0)
                        {
                            n_factor = (BOFunciones.Power((n_tasa_interes / 100) + 1, n_num_cuotas) - 1);
                            if (n_factor != 0)
                            {
                                atr_finan[pn_num_atr].n_valor = BOFunciones.Power((atr_finan[pn_num_atr].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1;
                                if (atr_finan[pn_num_atr].n_valor == 0)
                                {
                                    atr_finan[pn_num_atr].n_valor = 0;
                                }
                                else
                                {
                                    if (bterminos && (n_tip_tf == 4 && n_tip_inttf == 1))
                                        CalcularProrrateoCuotasExtras(n_tasa_dia_tf, n_tipo_cal, f_fec_inicio, 0, ref n_val_tfpro);
                                    else
                                        n_val_tfpro = 0;
                                    atr_finan[pn_num_atr].n_valor = (((atr_finan[pn_num_atr].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[pn_num_atr].n_tasa_calculo / 100) + 1, n_num_cuotas)) / atr_finan[pn_num_atr].n_valor) * (n_monto_cal - n_val_tfpro);
                                    atr_finan[pn_num_atr].n_valor = (atr_finan[pn_num_atr].n_valor * n_num_cuotas) - n_monto_cal;
                                }
                            }
                        }
                    }
                    else if (n_tip_amo == 5)
                    {
                        n_factor = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas) - 1);
                        if (n_factor != 0)
                        {
                            CalcularProrrateoCuotasExtras(n_tasa_dia_tf, n_tipo_cal, f_fec_inicio, 0, ref n_val_tfpro);
                            atr_finan[pn_num_atr].n_valor = BOFunciones.Power((atr_finan[pn_num_atr].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1;
                            if (atr_finan[pn_num_atr].n_valor == 0)
                                atr_finan[pn_num_atr].n_valor = 0;
                            else
                            {
                                atr_finan[pn_num_atr].n_valor = (((atr_finan[pn_num_atr].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[pn_num_atr].n_tasa_calculo / 100) + 1, n_num_cuotas)) / atr_finan[pn_num_atr].n_valor);
                                atr_finan[pn_num_atr].n_valor = atr_finan[pn_num_atr].n_valor * (n_monto_cal - n_val_tfpro);
                                atr_finan[pn_num_atr].n_valor = (atr_finan[pn_num_atr].n_valor * n_num_cuotas) - n_monto_cal;
                            }
                        }
                    }
                    else
                    {
                        DApackage.ControlError(n_radic, null, "El tipo de amortización no corresponde. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                        return false;
                    }
                }
                else
                {
                    DApackage.ControlError(n_radic, null, "El tipo de interes no corresponde debe ser 1 o 2. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                    return false;
                }
            }
            else if (n_tip_cuota == 3)
                n_aux = 0;
            else
            {
                DApackage.ControlError(n_radic, null, "El tipo de cuota no corresponde debe ser 1, 2 o 3. Crédito:" + n_radic + " Atributo:" + atr_finan[pn_num_atr].n_cod_atr, 0, usuario);
                return false;
            }
            n_aux = atr_finan[pn_num_atr].n_valor;
            n_aux = BOFunciones.Round(n_aux);
            atr_finan[pn_num_atr].n_valor = n_aux;
            b_calculo = true;
            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Calcular fechas del crédito  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public void CalFechas(DateTime? pf_fec_inicio)
        {
            bool bResultado = true;
            decimal? n_dias = 0;
            decimal? n_per_dia = 0;

            try
            {
                if (pf_fec_inicio == null)
                {
                    if (n_tip_cuota == 1)
                    {
                        n_per_dia = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
                        if (n_per_dia == null || n_per_dia <= 0)
                            bResultado = false;
                        if (bResultado)
                        {
                            if (!BOFunciones.FecIniCre(f_fec_apro, Convert.ToInt32(n_per_dia), n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                                bResultado = false;
                        }
                    }
                    else
                    {
                        if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                            bResultado = false;
                    }
                }
                //-- Calculando la fecha del primer pago
                n_dias = DApackage.ConPeriodicidadNumDia(Convert.ToInt32(n_periodic), usuario);
                if (n_tip_cuota == 1)
                {
                    //-- Calculando el número de días total del plazo fijo
                    n_dias_fijo = DApackage.ConPeriodicidadNumDia(Convert.ToInt32(n_for_pla), usuario);
                    n_dias_fijo = n_dias_fijo * Convert.ToInt32(n_plazo);
                    n_dias_fijo = BOFunciones.Round(n_dias_fijo);
                    //-- Calcula fecha de pago
                    f_prox_pag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_fijo), Convert.ToInt32(n_tipo_cal));
                    f_fec_term = f_prox_pag;
                }
                else
                {
                    //-- Calcula fecha de primer pago
                    if (f_fec_inicio != null)
                    {
                        f_prox_pag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias), Convert.ToInt32(n_tipo_cal));
                        // -- Calculando la fecha de terminación del crédito
                        f_fec_term = f_fec_inicio;
                        f_fec_term = BOFunciones.FecSumDia(f_fec_term, Convert.ToInt32(n_dias * n_num_cuotas), Convert.ToInt32(n_tipo_cal));
                    }
                }
            }
            catch
            {
                return;
            }

        }

        public bool Tasa_Interes(Int64 N_Radic, ref decimal? n_tasa_IntCte_per, ref decimal? N_Tasa_IntCte_Efe, ref decimal? N_Tasa_IntCte_Nom)
        {
            decimal? Lperiodos_Anuales;
            if (Cargar_Credito(N_Radic))
            {
                Lperiodos_Anuales = DApackage.ConPeriodicidadPerAnu(n_periodic, usuario);
                n_tasa_IntCte_per = BOFunciones.Round(n_tasa_intcte, 4);
                N_Tasa_IntCte_Nom = BOFunciones.Round(n_tasa_intcte * Lperiodos_Anuales, 4);
                N_Tasa_IntCte_Efe = BOFunciones.Round((BOFunciones.Power(1 + (n_tasa_intcte / 100), Lperiodos_Anuales) - 1) * 100, 4);
                return true;
            }
            else
                return false;
        }

        public void CargarAtributoDescontado(int pn_cod_atr, decimal pn_val_atr, int pn_tipo, string ps_titulo)
        {
            if (pn_val_atr != 0)
            {
                n_num_atr_des = n_num_atr_des + 1;
                n_cod_atr_des[n_num_atr_des] = pn_cod_atr;
                s_nom_atr_des[n_num_atr_des] = DApackage.NombreAtributo(pn_cod_atr, usuario);
                s_nom_atr_des[n_num_atr_des] = s_nom_atr_des[n_num_atr_des] + BOFunciones.NVL(ps_titulo, "");
                n_tip_atr_des[n_num_atr_des] = pn_tipo;
                n_val_atr_des[n_num_atr_des] = pn_val_atr;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Liquidación del crédito.
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Liquidar_Credito()
        {
            int n_num;
            int n_cont;
            decimal? n_val_aux = null;
            int? n_signoatr = null;
            bool b_existe;
            decimal? n_monto_cal = null;
            decimal? n_tasa_dia_tf = null;
            decimal? n_per_dia = null;
            decimal? n_val_tfpro = null;
            int? n_cod_atr_aux = null;
            string s_nom_atr_aux = null;
            decimal? n_aux_int_aju = null;
            int? n1;
            int? n2;
            decimal? n_int_aju_tf;
            decimal? n_int_aju_cal;
            // -- Variables para controlar el número de iteraciones cuando es todo financiado
            string s_num_ite;
            int? n_num_ite;
            string g_sentencia;
            string s_usura;
            bool b_existe_usura;
            decimal? n_tasa_calculo;
            decimal? n_tasa_usura;
            decimal? n_numero;
            int? n_tipo_tasa = null;
            string s_efe_nom = null;
            string s_mod = null;
            int? n_per = null;
            int? n_mod_per = null;
            string s_efe_nom_nue;
            string s_mod_nue;
            decimal? n_per_nue;
            decimal? n_mod_per_nue;
            string g_sentencia_nue;
            decimal? n_monto_aux;
            DateTime? f_fec_cal;
            // -- Variables gradiente
            decimal? n_tasa_interes_grad;
            decimal? n_fac_grad;
            decimal? n_per_ano = null;
            // -- Numero de cuotas real, para manejo de periodo de gracia
            int? n_num_cuotas_temp;
            // -- Variable de redondeo
            string s_red_cuo;
            int? n_red_cuo;
            decimal? n_val_interes = 0;
            int? n_dias_mes;
            DateTime? n_fec;
            decimal? n_val;
            decimal? n_dias;
            decimal? n_factor = null;
            decimal? n_cuota_nue;
            decimal? n_dia_des;
            decimal? n_elevado;
            decimal? n_tasa_dtf;
            DateTime? f_fec_pripag;
            string s_val_timb;
            int? n_depende_atr;
            DateTime? f_fec_aux;
            decimal? n_vpn_otros_tot;
            decimal? n_vpn_otros;
            decimal? n_vpn_otros_1;
            int n_cont_1;
            decimal? n_valfactor = null;
            decimal? n_suma_int_simple_tf;
            int? n_cod_atr_dep = null;
            // -- Variables para generar plan de pagos
            decimal? n_saldo_ini = null;
            decimal?[] n_interes = null;
            int?[] n_cod_atributos = null;
            decimal? n_capital = null;
            decimal? n_cap_tf = null;
            decimal? n_int_tf = null;
            DateTime? f_fecha = null;
            int? n_contcuotas;
            decimal? n_saldo_base;
            int? n_meses_prop = null;
            int? n_num_cuotas_pend = null;
            int? n_pos;
            decimal? n_vpn_aux;
            decimal? n_valor_aux = null;
            bool bResultado = false;
            decimal? n_meses_periodic;
            decimal? n_tasa_int_aju = null;

            n_num_cuotas_temp = 0;
            n_adi_cuota = 0;
            n_cuota_adi = 0;
            n_acumulado_int = 0;
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Buscado parámetros de liquidación si no se han cargado  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_tip_cuota == null)
            {
                tipoliquidacion tipoliq = new tipoliquidacion();
                tipoliq = DApackage.Consultartipoliquidacion(Convert.ToInt32(s_tipo_liq), usuario);
                if (tipoliq == null)
                {
                    Mensaje_Error("No se encontro el tipo de liquidación correspondiente");
                }
                n_tip_cuota = tipoliq.tipo_cuota;
                n_tip_tf = 4;
                n_tip_inttf = 1;
                n_tip_amo = tipoliq.tipo_cuota;
                n_tip_pago = tipoliq.tipo_pago;
                n_tip_gra = tipoliq.tip_gra;
                n_tip_int = tipoliq.tipo_interes;
                n_tip_paginttf = 2;
                n_tip_intant = tipoliq.tipo_intant;
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //--  Calculo de los días de la periodicidad  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_dias_per_cre == null)
            {
                // Mensaje("Calculo de los días de la periodicidad");
                n_dias_per_cre = DApackage.ConPeriodicidadNumDia(n_periodic, usuario);
            }
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Cargar la variable de dias del periodo para efectos de calculo del seguro en los atributos descontados  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            p_n_num_dias_per = Convert.ToInt32(n_dias_per_cre);
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculo de la periodicidad diaria para el tipo de calendario  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region periodicidad diaria
            if (n_per_dia == null)
            {
                Mensaje_Error("Calculo de la periodicidad diaria para el tipo de calendario");
                n_per_dia = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
                if (n_per_dia == null || n_per_dia < 0)
                {
                    // Mensaje_Error("No se encontro periodicidad diaria para el tipo de calendario: " || n_tipo_cal || ". Verifique periodicidades.. " || n_per_dia);
                    return false;
                }
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculando número de cuotas  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region número de cuotas
            if (n_tip_cuota == 1)
            {
                n_num_cuotas = 1;
            }
            else
            {
                // Mensaje("Calculo del número de cuotas");
                n_num_cuotas = Convert.ToInt32(BOFunciones.CalNumCuotas(n_plazo, n_for_pla, Convert.ToInt32(n_periodic)));
                n_num_cuotas_temp = n_num_cuotas;
                n_num_cuotas = Convert.ToInt32(n_num_cuotas - BOFunciones.NVL(n_cuotas_gracia, 0));
            }
            //-- Se define el tipo de tasa de usura para la clasificación de la linea de credito
            // Mensaje("Determinando el tipo de histórico de usura según la clasificación");
            n_tipo_tasa_usura = DApackage.ConsultarHistoricoClasifica(n_clasificacion, usuario);
            if (n_tipo_tasa_usura == null)
            {
                // Mensaje_Error("No se ha definido el tipo de tasa de usura para el tipo de clasificación de la linea de credito. Verifique Parametrización de la Clasificación");
                return false;
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculando tasa de interés y la compara con la de la usura  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region "Validando que la tasa del crédito no exceda la usura"
            n_tasa_calculo = 0;
            s_usura = BOFunciones.BuscarGeneral(680, 2);
            if (s_usura != null)
                b_existe_usura = true;
            else
                b_existe_usura = false;
            if (b_existe_usura && n_tipo_tasa_usura != null)
            {
                n_tasa_usura = 0;
                MensajeConsola("Determinar el valor de la tasa de usura a la fecha de aprobación del crédito");
                DApackage.ConsultarTasaUsura(f_fec_apro, n_tipo_tasa_usura, ref n_tasa_usura, ref n_tipo_tasa, usuario);
                if (n_tasa_usura != 0)
                {
                    DApackage.ConTipoTasa(n_tipo_tasa, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);
                    //-- Se calcula la tasa de usuara de acuerdo al formato del crédito
                    n_tasa_calculo = BOFunciones.CalTasMod(n_tasa_usura, BOFunciones.To_Number(s_efe_nom), n_per, BOFunciones.To_Number(s_mod), n_mod_per, 1, n_periodic, n_tip_pago, n_periodic, 0);
                }
                else
                {
                    b_existe_usura = false;
                }
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Determina la tasa de interés del crédito por atributo y total.
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //Mensaje("Determinar las tasas de interés del crédito -->" || n_atr_fin);
            #region atributos financiados
            n_num = 1;
            n_tasa_interes = 0;
            n_tasa_intmor = 0;
            n_tasa_intcte = 0;
            while (n_num <= n_atr_fin)
            {
                //-- Si es plazo fijo, se calcula interés diario
                if (n_tip_cuota == 1)
                {
                    if (n_tip_int == 1)
                        atr_finan[n_num].Conv_Tasa(Convert.ToInt32(n_per_dia), n_tip_pago, f_fec_apro, "1", n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                    else
                        atr_finan[n_num].Conv_Tasa(Convert.ToInt32(n_per_dia), n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                }
                else
                {
                    atr_finan[n_num].Conv_Tasa(n_periodic, n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                }
                if (n_val_aux == null)
                {
                    Mensaje_Error("No pudo calcular valor del atributo " + atr_finan[n_num].n_cod_atr);
                    return false;
                }

                MensajeConsola("Compara con la tasa de usura. Primero valida si es interes de mora. Usura:" + n_tasa_calculo);
                if (b_existe_usura && n_tasa_calculo > 0 && n_tasa_calculo < atr_finan[n_num].n_tasa_calculo)
                {
                    atr_finan[n_num].n_tasa_calculo = n_tasa_calculo;
                    n_val_aux = n_tasa_calculo;
                }
                //-- No se tiene en cuenta el interes de mora
                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                    n_tasa_interes = n_tasa_interes + atr_finan[n_num].n_tasa_calculo;
                else
                    n_tasa_intmor = n_tasa_intmor + atr_finan[n_num].n_tasa_calculo;
                //-- Determinando la tasa de interés corriente
                if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                    n_tasa_intcte = atr_finan[n_num].n_tasa_calculo;
                n_num = n_num + 1;
            }
            n_tasa_interes = n_tasa_interes / 100;
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calcula el plazo en meses  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region plazo en meses
            if (n_plazo_mes == null)
            {
                // Mensaje("Determinando el plazo en meses");
                n_plazo_mes = Convert.ToInt32(DApackage.ConPeriodicidadNumeroMeses(n_for_pla, usuario));
                n_plazo_mes = Convert.ToInt32(n_plazo * n_plazo_mes);
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculando valores sumados al monto o descontados del cheque  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

            MensajeConsola("Calculando valores de atributos descontados");

            #region calculo atributos descontados
            n_num = 1;
            n_vpn_otros_tot = 0;
            n_adi_monto = 0;
            n_des_cheque = 0;
            n_val_indiv = 0;
            while (n_num <= n_atr_otr)
            {
                #region
                n_val_aux = 0;
                n_signoatr = 0;
                if (atr_otro[n_num].n_signo != 99)
                {
                    //-- Si el tipo de liquidación es sobre SALDO del crédito entonces lo carga
                    if (atr_otro[n_num].n_tip_liq == 17 || atr_otro[n_num].n_tip_liq == 13 || atr_otro[n_num].n_tip_liq == 7 || atr_otro[n_num].n_tip_liq == 6)
                        n_saldo = n_monto;
                    //-- Calculando el valor del atributo.Cuando es tipo de liquidación 17 mira los atributos del cual depende.
                    #region atributo depende
                    n_cod_atr_dep = DApackage.ConsultarAtributoDepende(atr_otro[n_num].n_cod_atr, usuario);
                    if (n_cod_atr_dep == null)
                    {
                        #region atributo depende
                        if (n_cod_atr_dep != 0 && n_cod_atr_dep != null)
                        {
                            //-- Si el atributo depende de una atributo financiado
                            n_cont = 1;
                            while (n_cont <= n_atr_fin)
                            {
                                if (atr_finan[n_cont].n_cod_atr == n_cod_atr_dep)
                                {
                                    atr_otro[n_num].n_valor = atr_finan[n_cont].n_tasa_calculo / 100;
                                    break;
                                }
                                n_cont = n_cont + 1;
                            }
                            //-- Actualiza el valor del atributo
                            atr_otro[n_num].Cal_Valor(0, n_plazo_mes, n_num_cuotas + n_cuotas_gracia, ref n_val_aux, ref n_signoatr, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                            if (!bResultado)
                            {
                                Mensaje_Error("Error al calcular valor del atributo " + atr_otro[n_num].n_cod_atr);
                                return false;
                            }
                        }
                        else
                        {
                            atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas + n_cuotas_gracia, ref n_val_aux, ref n_signoatr, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                            if (!bResultado)
                            {
                                Mensaje_Error("Error al calcular el valor del atributo " + atr_otro[n_num].n_cod_atr);
                                return false;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        //Close pBasedato;
                        #region Determina el valor de los atributos que dependen de otros.
                        if (atr_otro[n_num].n_num_atr > 0)
                        {
                            n_signoatr = atr_otro[n_num].n_signo;
                            if (atr_otro[n_num].n_tip_des != 3)
                                atr_otro[n_num].n_valor = 0;
                            atr_otro[n_num].n_valor_calculo = 0;
                            n_cont = 1;
                            while (n_cont < atr_otro[n_num].n_num_atr)
                            {
                                n_cont_1 = 1;
                                while (n_cont_1 <= n_atr_otr)
                                {
                                    if (atr_otro[n_cont_1].n_cod_atr == atr_otro[n_num].n_atributo_depende[n_cont])
                                    {
                                        if (atr_otro[n_num].n_tip_des == 3)
                                        {
                                            atr_otro[n_num].n_valor_calculo = atr_otro[n_num].n_valor_calculo + (atr_otro[n_cont_1].n_valor_calculo * atr_otro[n_num].n_valor / 100);
                                            atr_otro[n_num].n_valor_calculo = BOFunciones.Round(atr_otro[n_num].n_valor_calculo);
                                        }
                                        else
                                        {
                                            atr_otro[n_num].n_valor = atr_otro[n_num].n_valor + atr_otro[n_cont_1].n_valor_calculo;
                                            atr_otro[n_num].n_valor_calculo = atr_otro[n_num].n_valor_calculo + atr_otro[n_cont_1].n_valor_calculo;
                                        }
                                    }
                                    n_cont_1 = n_cont_1 + 1;
                                }
                                n_cont = n_cont + 1;
                            }
                            if (n_signoatr != 3 && n_signoatr != 4 && n_signoatr != 5 && n_signoatr != 6)
                                n_val_aux = atr_otro[n_num].n_valor_calculo;
                        }
                        else
                        {
                            atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas + n_cuotas_gracia, ref n_val_aux, ref n_signoatr, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                            if (!bResultado)
                            {
                                Mensaje_Error("Error al calcular valor del atributo " + atr_otro[n_num].n_cod_atr);
                                return false;
                            }
                        }
                        #endregion
                    }
                    #endregion
                    #region Descuento del Desembolso
                    if (n_signoatr == 1)
                    {
                        if (BOFunciones.StrIsValidNumber(BOFunciones.Trim(BOFunciones.BuscarGeneral(690, 2))))
                        {
                            //-- Se ajustò para que no sume el valor de la capitalizaciòn de aportes esta se calcula en la impresion o solicitud.
                            if (atr_otro[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.Trim(BOFunciones.BuscarGeneral(690, 2))))
                                n_val_aux = 0;
                        }
                        n_des_cheque = n_des_cheque + n_val_aux;
                    }
                    #endregion
                    #region  Si el atributo es 2=Financiado Conjunto o 3=Financiado Individual
                    if (n_signoatr == 2 || n_signoatr == 3)
                    {
                        #region Suma los valores financiados individual
                        if (n_val_aux > 0)
                        {
                            n_adi_monto = n_adi_monto + n_val_aux;
                            n_des_cheque = n_des_cheque + n_val_aux;
                            CargarAtributoDescontado(Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToDecimal(n_val_aux), 1, "");
                        }
                        #endregion
                        #region Financiado Individual
                        if (n_signoatr == 3)
                        {
                            if (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0)
                            {
                                //-- Calcular VPN de los valores individuales para calculo de cuota fija
                                if (atr_otro[n_num].n_valor_calculo != 0)
                                {
                                    if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 1)) == "1" && b_LeyMiPyme && (atr_otro[n_num].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_num].n_cod_atr == n_atr_IVALeyMiPyme))
                                    {
                                        if (g_b_yaprimero)
                                        {
                                            n_cont = 1;
                                            while (n_cont <= atr_otro[n_num].n_num_pag)
                                            {
                                                n_vpn_otros_tot = n_vpn_otros_tot + BOFunciones.NVL(atr_otro[n_num].n_valor_presentes[n_cont], 0);
                                                if (n_cont == 1 && n_tip_pago == 1)
                                                {
                                                    n_des_cheque = n_des_cheque + n_val_aux;
                                                    CargarAtributoDescontado(Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToDecimal(BOFunciones.Round(atr_otro[n_num].n_valor_calculo / (atr_otro[n_num].n_num_cuotas - 1))), 1, "");
                                                }
                                                n_cont = n_cont + 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (n_tasa_interes != 0)
                                        {
                                            //-- Lleva el total a pagar del atributo a VPN
                                            if (n_tip_pago == 1)
                                                n_vpn_otros_1 = (1 - BOFunciones.Power(1 - n_tasa_interes, atr_otro[n_num].n_num_cuotas));
                                            else
                                                n_vpn_otros_1 = (BOFunciones.Power(n_tasa_interes + 1, atr_otro[n_num].n_num_cuotas) - 1);
                                            if (n_vpn_otros_1 != 0)
                                            {
                                                //-- Tipo de pago  1:Anticipado, 2:Vencido
                                                if (n_tip_pago == 1)
                                                {
                                                    n_vpn_otros_1 = (n_tasa_interes / n_vpn_otros_1);
                                                    n_vpn_otros_1 = (n_vpn_otros_1) / atr_otro[n_num].n_num_cuotas;
                                                }
                                                else
                                                {
                                                    n_vpn_otros_1 = n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, atr_otro[n_num].n_num_cuotas) / n_vpn_otros_1;
                                                }
                                                if (n_vpn_otros_1 != null && n_vpn_otros_1 != 0)
                                                    n_vpn_otros_1 = (atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas) / n_vpn_otros_1;
                                            }
                                            //-- Calcula el valor del atributo que se suma a la cuota
                                            if (n_tip_pago == 1)
                                                n_vpn_otros = (1 - BOFunciones.Power(1 - n_tasa_interes, n_num_cuotas));
                                            else
                                                n_vpn_otros = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas) - 1);
                                            if (n_vpn_otros != 0)
                                            {
                                                //-- Tipo de pago  1:Anticipado, 2:Vencido
                                                if (n_tip_pago == 1)
                                                {
                                                    n_vpn_otros = (n_tasa_interes / n_vpn_otros) * (n_vpn_otros_1);
                                                    n_vpn_otros = (n_vpn_otros * n_vpn_otros_1) / n_num_cuotas;
                                                }
                                                else
                                                {
                                                    if (n_cuotas_gracia > 0 && n_tip_gracia == 3)
                                                    {
                                                        if (n_tipo_prod == 3)
                                                            n_vpn_otros = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_vpn_otros) * (n_vpn_otros_1 + (n_cuotas_gracia * n_tasa_interes * n_vpn_otros_1));
                                                        else
                                                            n_vpn_otros = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_vpn_otros) * (n_vpn_otros_1 + (n_cuotas_gracia * n_tasa_interes * n_vpn_otros_1));
                                                    }
                                                    else
                                                    {
                                                        n_vpn_otros = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_vpn_otros) * n_vpn_otros_1;
                                                    }
                                                }
                                            }
                                            n_vpn_otros_tot = n_vpn_otros_tot + BOFunciones.NVL(n_vpn_otros, 0);
                                        }
                                    }
                                }
                                else
                                {
                                    n_val_indiv = atr_otro[n_num].n_valor_calculo / (n_num_cuotas + n_cuotas_gracia);
                                }
                                n_val_indiv = BOFunciones.Round(n_val_indiv);
                                //-- Determinar valores adicionales a la cuota.Liquidación
                                n_adi_cuota = n_adi_cuota + BOFunciones.NVL(n_val_indiv, 0);
                            }
                        }
                        #endregion
                        #region Financiado Individual Excluido
                        if (n_signoatr == 4)
                            n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo;
                        #endregion
                        #region  Adicional a la cuota
                        if (n_signoatr == 5)
                            n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo;
                        #endregion
                        #region Pago en la Primera Cuota.
                        if (n_signoatr == 6)
                            n_val_indiv = atr_otro[n_num].n_valor_calculo;
                        #endregion
                        n_num = n_num + 1;
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            n_monto_cal = n_monto + BOFunciones.NVL(n_adi_monto, 0);
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculo de la fecha inicial y dias de ajuste.Si es plazo fijo, se calcula con periodicidad diaria  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

            MensajeConsola("Calculando fecha de inicio y días de ajuste del crédito");
            #region calculo de la fecha de inicio
            if (n_tip_cuota == 1)
            {
                if (!BOFunciones.FecIniCre(f_fec_apro, Convert.ToInt32(n_per_dia), n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                {
                    Mensaje_Error("Error al calcular fecha inicial del crédito");
                    return false;
                }
            }
            else
            {
                if (b_calcular_ini)
                {
                    if (n_tip_pago == 1)
                    {
                        if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                        {
                            Mensaje_Error("Error al calcular fecha inicial del crédito");
                            return false;
                        }
                        n_dias_aju = n_dias_aju + n_dias_per_cre;
                    }
                }
                else
                {
                    if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                    {
                        Mensaje_Error("Error al calcular fecha inicial del crédito. " + s_error_men);
                        return false;
                    }
                }
                //-- Calcular la fecha de terminación del crédito
                CalFechas(f_fec_inicio);
                //-- Calculo especial de los días de ajuste
                if (BOFunciones.BuscarGeneral(1500, 1) == "1")
                {
                    f_fec_aux = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                    if (BOFunciones.DateMonth(f_fec_aux) == 1 || BOFunciones.DateMonth(f_fec_aux) == 3 || BOFunciones.DateMonth(f_fec_aux) == 5 || BOFunciones.DateMonth(f_fec_aux) == 7 || BOFunciones.DateMonth(f_fec_aux) == 8 || BOFunciones.DateMonth(f_fec_aux) == 10 || BOFunciones.DateMonth(f_fec_aux) == 12 || (BOFunciones.DateMonth(f_fec_aux) == 2 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_aux), 4) == 0))
                        n_dias_aju = BOFunciones.DateDay(f_fec_aux) - BOFunciones.DateDay(f_fec_apro) + 1;
                    else if (BOFunciones.DateMonth(f_fec_aux) == 2)
                        n_dias_aju = BOFunciones.DateDay(f_fec_aux) - BOFunciones.DateDay(f_fec_apro);
                    else
                    {
                        if (n_tip_pago == 1)
                        {
                            f_fec_cal = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                            n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_cal, Convert.ToInt32(n_tipo_cal));
                        }
                        else
                        {
                            if (f_fec_apro > f_fec_inicio)
                                n_dias_aju = -BOFunciones.FecDifDia(f_fec_inicio, f_fec_apro, Convert.ToInt32(n_tipo_cal));
                            else
                                n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_inicio, Convert.ToInt32(n_tipo_cal));
                        }
                    }
                }
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculo de interes de ajuste o fraccionado  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region calculo días de ajuste
            n_int_aju = 0;
            n_dias_aju = BOFunciones.Round(n_dias_aju);
            if (n_dias_aju > 0)
            {
                if (n_tip_intant == 2 || n_tip_intant == 3 || n_tip_intant == 4 || n_tip_intant == 5)
                {
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    //-- Calcula el valor de intereses anticipados de términos fijos a devolver si se pagan antes de la primera cuota  
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    if (BOFunciones.BuscarGeneral(1360, 1) == "1")
                        b_existe = true;
                    else
                        b_existe = false;
                    if (n_tip_tf != 1)
                    {
                        //-- Calcula tasa diaria para los terminos fijos
                        n_tasa_dia_tf = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_paginttf, n_per_dia, 0);
                        n_tasa_dia_tf = n_tasa_dia_tf / 100;
                        //-- Si no se manejan terminos fijos prorrateados.n_tip_amo != 5  
                        if (n_tip_amo != 5)
                            CalcularInteresCuotasExtras(n_tip_tf, n_tip_inttf, n_tasa_dia_tf, n_tipo_cal, f_fec_inicio);
                    }
                    n_int_aju_tf = 0;
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    //-- Calcula el valor de los intereses anticipados de ajuste  
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    if ((BOFunciones.BuscarGeneral(1500, 1) == "1" || BOFunciones.BuscarGeneral(1500, 1) == "2") && n_tip_intant == 3)
                    {
                        n_tasa_int_aju = BOFunciones.CalTasMod(n_tasa_interes, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, 1, n_per_dia, 0);
                        n_int_aju = n_tasa_int_aju * n_monto_cal * n_dias_aju;
                    }
                    else
                    {
                        if (s_calc_int == "1")
                        {
                            if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                            {
                                n_int_aju = n_tasa_interes / n_dias_per_cre * n_dias_aju * n_monto_cal;
                                n_int_aju = BOFunciones.Redondeo(n_int_aju);
                            }
                            //-- Se verifica cantidad de iteraciones para el redondeo, por defecto una(1)
                            if (n_tip_intant == 4)
                            {
                                s_num_ite = "";
                                s_num_ite = BOFunciones.BuscarGeneral(600, 1);
                                if (s_num_ite == "")
                                    n_num_ite = 1;
                                else
                                    n_num_ite = BOFunciones.To_Number(s_num_ite);
                                while (n_num_ite > 0)
                                {
                                    n_aux_int_aju = n_int_aju;
                                    n_int_aju = n_tasa_interes / n_dias_per_cre * n_dias_aju * (n_monto_cal + n_aux_int_aju);
                                    n_num_ite = n_num_ite - 1;
                                }
                            }
                        }
                        else
                        {
                            n_int_aju = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, 2, n_per_dia, 0);
                            n_int_aju = (BOFunciones.Power(n_int_aju / 100 + 1, n_dias_aju) - 1);
                            n_int_aju = (BOFunciones.CalTasAnt(n_int_aju * 100) / 100) * n_monto_cal;
                            n_int_aju = BOFunciones.Redondeo(n_int_aju);
                            //-- Se verifica cantidad de iteraciones para el redondeo, por defecto una(1)
                            if (n_tip_intant == 4)
                            {
                                s_num_ite = "";
                                s_num_ite = BOFunciones.BuscarGeneral(600, 1);
                                if (s_num_ite == "")
                                    n_num_ite = 1;
                                else
                                    n_num_ite = BOFunciones.To_Number(s_num_ite);
                                while (n_num_ite > 0)
                                {
                                    n_aux_int_aju = n_int_aju;
                                    n_int_aju = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, 2, n_per_dia, 0);
                                    n_int_aju = (BOFunciones.Power(n_int_aju / 100 + 1, n_dias_aju) - 1);
                                    n_int_aju = (BOFunciones.CalTasAnt(n_int_aju * 100) / 100) * (n_monto_cal + n_aux_int_aju);
                                    n_num_ite = n_num_ite - 1;
                                }
                            }
                        }
                    }
                    n_int_aju = BOFunciones.Redondeo(n_int_aju);
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    //-- Inserta atributo de descuento por cada atributo financiado  
                    //----------------------------------------------------------------------------------------------------------------------------------  
                    n_monto_aux = n_monto_cal;
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        atr_finan[n_num].GetAtrfinan_Tasa(ref n_cod_atr_aux, ref s_nom_atr_aux, ref n_tasa_int_aju);
                        //-- No se incluye interes de mora
                        if (n_cod_atr_aux != n_atr_mora)
                        {
                            if (n_num == n_atr_fin - 1)
                            {
                                if (n_int_aju <= 0)
                                    n_val_aux = 0;
                                else
                                    n_val_aux = n_int_aju;
                            }
                            else
                            {
                                //-- Determina si se manejan interes 1:Simple o 2:Compuesto
                                if (s_calc_int == "1")
                                {
                                    n_aux_int_aju = (n_tasa_int_aju / 100) / n_dias_per_cre * n_dias_aju * n_monto_aux;
                                    n_val_aux = (n_tasa_int_aju / 100) / n_dias_per_cre * n_dias_aju * (n_monto_aux + n_aux_int_aju);
                                }
                                else
                                {
                                    n_tasa_int_aju = BOFunciones.CalTasMod(n_tasa_int_aju, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, 2, n_per_dia, 0);
                                    n_tasa_int_aju = (BOFunciones.Power(n_tasa_int_aju / 100 + 1, n_dias_aju) - 1);
                                    n_aux_int_aju = (BOFunciones.CalTasAnt(n_tasa_int_aju * 100) / 100) * n_monto_aux;
                                    n_val_aux = (BOFunciones.CalTasAnt(n_tasa_int_aju * 100) / 100) * (n_monto_aux + n_aux_int_aju);
                                }
                                n_val_aux = BOFunciones.Redondeo(n_val_aux);
                            }
                            n_int_aju = n_int_aju - n_val_aux;
                            if (n_val_aux > 0)
                            {
                                //-- Se inserta los valores a ser cobrados por anticipado - Se señala con el signo = 99 para diferenciarlos de los demas tipos
                                if (n_tip_intant == 2)    //-- Cargar para descontar del cheque
                                {
                                    if (n_tip_pago == 1 && n_dias_per_cre < n_dias_aju)
                                    {
                                        atr_otro[n_atr_otr].Ins_Dat_Basico(n_cod_atr_aux, s_nom_atr_aux, BOFunciones.Round(n_val_aux * n_dias_per_cre / n_dias_aju), 99);
                                        n_atr_otr = n_atr_otr + 1;
                                        atr_otro[n_atr_otr].Ins_Dat_Basico(0, "Dias de ajuste", n_val_aux - BOFunciones.Round(n_val_aux * n_dias_per_cre / n_dias_aju), 99);
                                    }
                                    else
                                    {
                                        if (n_atr_otr <= 0)
                                            n_atr_otr = 1;
                                        if (n_atr_otr > atr_otro.Length)
                                            Array.Resize(ref atr_otro, n_atr_otr + 1);
                                        atr_otro[n_atr_otr].Ins_Dat_Basico(n_cod_atr_aux, s_nom_atr_aux, n_val_aux, 99);
                                    }
                                    n_atr_otr = n_atr_otr + 1;
                                    CargarAtributoDescontado(Convert.ToInt32(n_cod_atr_aux), Convert.ToDecimal(n_val_aux), 1, " Ajuste");
                                    n_des_cheque = n_des_cheque + n_val_aux;
                                }
                                else if (n_tip_intant == 4) //-- Cargar para sumar al monto
                                {
                                    atr_otro[n_atr_otr].Ins_Dat_Basico(n_cod_atr_aux, s_nom_atr_aux, n_val_aux, 2);
                                    n_atr_otr = n_atr_otr + 1;
                                    CargarAtributoDescontado(Convert.ToInt32(n_cod_atr_aux), Convert.ToDecimal(n_val_aux), 2, " Ajuste");
                                    n_adi_monto = n_adi_monto + n_val_aux;
                                }
                                else if (n_tip_intant == 5) //-- Cargar para financiar en las cuotas
                                {
                                    atr_otro[n_atr_otr].Ins_Dat_Basico(n_cod_atr_aux, s_nom_atr_aux, n_val_aux, 3);
                                    n_atr_otr = n_atr_otr + 1;
                                    n_val_indiv = BOFunciones.Redondeo(n_val_aux / n_num_cuotas);
                                    n_adi_cuota = n_adi_cuota + BOFunciones.NVL(n_val_indiv, 0);
                                }
                            }
                        }
                        n_num = n_num + 1;
                    }
                    n_monto_cal = n_monto + BOFunciones.NVL(n_adi_monto, 0);
                }
            }
            else if (n_dias_aju < 0)
            {
                if (n_tip_intant == 3)
                {
                    //-- Calculando los intereses anticipados de ajuste    
                    if (BOFunciones.BuscarGeneral(1500, 1) == "1" || BOFunciones.BuscarGeneral(1500, 1) == "2")
                    {
                        n_tasa_int_aju = BOFunciones.CalTasMod(n_tasa_interes, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, 1, n_per_dia, 0);
                        n_int_aju = n_tasa_int_aju * n_monto_cal * n_dias_aju;
                        n_int_aju = BOFunciones.Redondeo(n_int_aju);
                    }
                    else
                    {
                        n_int_aju = (BOFunciones.Power(n_tasa_interes + 1, n_dias_aju / n_dias_per_cre) - 1) * n_monto_cal;
                        n_int_aju = BOFunciones.Redondeo(n_int_aju);
                    }
                }
                else
                {
                    n_dias_aju = 0;
                }
            }
            //-- Verificar días de ajuste nulos
            n_dias_aju = BOFunciones.NVL(n_dias_aju, 0);
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculo de terminos fijos o primas.Tipo de terminos  1:No utiliza  
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

            MensajeConsola("Si tiene cuotas extras determinar el valor de interés anticipados a pagar");
            #region calculo terminos fijos
            n_suma_int_simple_tf = 0;
            if (bterminos)
            {
                if (n_tip_tf == 1)
                {
                    Mensaje_Error("No se pueden tener terminos fijos de acuedo al tipo de liquidación");
                    return false;
                }
                else
                {
                    //-- Calcula tasa diaria para los terminos fijos
                    n_tasa_dia_tf = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_paginttf, n_per_dia, 0);
                    n_tasa_dia_tf = n_tasa_dia_tf / 100;
                    //-- Si no se manejan terminos fijos prorrateados.n_tip_amo != 5  
                    if (n_tip_amo != 5 && !(n_tip_amo == 4 && n_tip_tf == 4 && n_tip_inttf == 1))
                    {
                        CalcularInteresCuotasExtras(n_tip_tf, n_tip_inttf, n_tasa_dia_tf, n_tipo_cal, f_fec_inicio);
                        n_monto_cal = n_monto_cal - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0);
                        //-- Calcula interes de los terminos fijos simple en el caso de cuota fija, terminos fijos a capital,
                        //--    este valor se suma al monto calculado para calcular la cuota periodica
                        if (n_tip_amo == 4 && n_tip_inttf == 2 && n_tip_tf != 3)
                        {
                            n_valor_aux = n_dias_per_cre;
                            n_suma_int_simple_tf = BOFunciones.NVL(SumarInteresCuotasExtras(n_tasa_interes, n_tipo_cal, f_fec_inicio, Convert.ToInt32(n_valor_aux)), 0);
                            if (n_tip_pago != 1)
                                n_monto_cal = n_monto_cal + n_suma_int_simple_tf;
                        }
                        //-- Interes de terminos fijos anticipado
                        if (n_tip_paginttf == 1)
                        {
                            if (ExistenCuotasExtras())
                            {
                                if (cuotasextras[1].n_interes != 0 && cuotasextras[1].n_interes != null)
                                {
                                    atr_otro[n_atr_otr].Ins_Dat_Basico(0, "Interes Anticipado TF", cuotasextras[1].n_interes, 1);
                                    n_des_cheque = n_des_cheque + cuotasextras[1].n_interes;
                                    n_atr_otr = n_atr_otr + 1;
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Calculando el valor de la cuota según tipo de liquidación.Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region Calculando el valor de la cuota según tipo de liquidación");
            if (n_tip_cuota == 1)
            {
                #region Plazos Fijos
                if (ExistenCuotasExtras())
                {
                    Mensaje_Error("No se pueden tener terminos fijos de acuedo al tipo de liquidación (Plazo Fijo)");
                    return false;
                }
                if (n_num_cuotas > 1)
                {
                    Mensaje_Error("Un crédito plazo fijo solo puede tener una cuota. Verifique la información");
                    return false;
                }
                //-- Calculando el número de días total del plazo fijo
                n_dias_fijo = DApackage.ConPeriodicidadNumDia(n_for_pla, usuario);
                n_dias_fijo = n_dias_fijo * n_plazo;
                n_dias_fijo = BOFunciones.Round(n_dias_fijo);
                #region Tipo de interés:  1:Simple,  2:Compuesto
                if (n_tip_int == 1)
                {
                    #region interés simple
                    n_tot_interes = n_tasa_interes * n_dias_fijo * n_monto_cal;
                    n_tot_interes = BOFunciones.Redondeo(n_tot_interes);
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                        {
                            n_val_aux = (atr_finan[n_num].n_tasa_calculo / 100) * n_dias_fijo * n_monto_cal;
                            n_val_aux = BOFunciones.Redondeo(n_val_aux);
                            atr_finan[n_num].n_valor = n_val_aux;
                        }
                        n_num = n_num + 1;
                    }
                    n_num = 1;
                    while (n_num <= n_atr_otr)
                    {
                        if (atr_otro[n_num].n_tip_liq == 7 && atr_otro[n_num].n_signo == 1)
                        {
                            atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, n_tot_interes, 100, 0, 0, 0, 0, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                            if (!bResultado)
                            {
                                Mensaje_Error("Error al calcular el valor del atributo");
                                return false;
                            }
                            if (n_val_aux != 0)
                                n_des_cheque = n_des_cheque + n_val_aux;
                        }
                        n_num = n_num + 1;
                    }
                    #endregion
                }
                else if (n_tip_int == 2)
                {
                    #region interés compuesto
                    if (n_tip_pago == 1)
                    {
                        n_tot_interes = n_tasa_interes / (1 - n_tasa_interes);
                        n_tot_interes = BOFunciones.Power(n_tot_interes + 1, n_dias_fijo) - 1;
                        n_tot_interes = n_tot_interes / (n_tot_interes + 1) * n_monto_cal;
                    }
                    else
                    {
                        n_tot_interes = (BOFunciones.Power(n_tasa_interes + 1, n_dias_fijo) - 1) * n_monto_cal;
                    }
                    n_tot_interes = BOFunciones.Redondeo(n_tot_interes);
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                        {
                            if (n_tip_pago == 1)
                            {
                                n_val_aux = (atr_finan[n_num].n_tasa_calculo / 100) / (1 - atr_finan[n_num].n_tasa_calculo / 100);
                                n_val_aux = BOFunciones.Power(n_val_aux + 1, n_dias_fijo) - 1;
                                n_val_aux = n_val_aux / (n_val_aux + 1) * n_monto_cal;
                            }
                            else
                            {
                                n_val_aux = (BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_dias_fijo) - 1) * n_monto_cal;
                            }
                            n_val_aux = BOFunciones.Redondeo(n_val_aux);
                            atr_finan[n_num].n_valor = n_val_aux;
                        }
                        n_num = n_num + 1;
                    }
                    #endregion
                }
                else
                {
                    Mensaje_Error("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                    return false;
                }
                #endregion
                #region Calcula valores adicionales como retencion e Ica
                n_num = 1;
                while (n_num <= n_atr_otr)
                {
                    if (atr_otro[n_num].n_tip_liq == 7 && atr_otro[n_num].n_signo == 1)
                    {
                        atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, n_tot_interes, 100, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                        if (!bResultado)
                        {
                            Mensaje_Error("Error al calcular el valor del atributo");
                            return false;
                        }
                        if (n_val_aux != 0)
                            n_des_cheque = n_des_cheque + n_val_aux;
                    }
                    n_num = n_num + 1;
                }
                #endregion
                #region Tipo de interés:  1:Anticipado,  2:Vencido
                if (n_tip_pago == 1)
                {
                    n_cuota = n_monto_cal;
                    Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, n_monto_cal);
                }
                else if (n_tip_pago == 2)
                {
                    n_cuota = n_monto_cal + n_tot_interes;
                }
                else
                {
                    Mensaje_Error("El tipo de pago o modalidad se encuentra errado, debe ser Vencido o Anticipado");
                    return false;
                }
                #endregion
                #endregion
            }
            else if (n_tip_cuota == 2)
            {
                #region -- Serie Unifome. Tipo de interés:  1:Simple,  2:Compuesto
                if (n_tip_int == 1)
                {
                    #region simple
                    if (n_tip_amo == 1)
                    {
                        n_tot_interes = n_monto_cal * n_tasa_interes;
                        n_num = 1;
                        while (n_num <= n_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                n_val_aux = n_monto_cal * n_num_cuotas * (atr_finan[n_num].n_tasa_calculo / 100);
                                n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                atr_finan[n_num].n_valor = n_val_aux;
                            }
                            n_num = n_num + 1;
                        }
                        if (n_num_cuotas <= 0)
                        {
                            Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                            return false;
                        }
                        n_cuota = (n_monto_cal + n_tot_interes) / n_num_cuotas;

                        //-- Modalidad de pago:   1:Anticipado,  2:Vencido
                        if (n_tip_pago == 1)
                            Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, n_monto_cal);
                    }
                    else
                    {
                        Mensaje_Error("El tipo de amortización definido no es posible");
                        return false;
                    }
                    #endregion
                }
                else if (n_tip_int == 2)
                {
                    #region compuesto
                    // Tipo amortización:  2: KF, IV;   3: KF, IF;   4: KV, IV   5: T.F.Prorrateados
                    if (n_tip_amo == 1 || n_tip_amo == 2)
                    {
                        #region Capital Fijo Interés Variable
                        if (n_num_cuotas <= 0)
                        {
                            Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                            return false;
                        }
                        else
                        {
                            n_tot_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    atr_finan[n_num].n_valor = 0;
                                n_num = n_num + 1;
                            }
                            if (n_tip_gracia == 4 && n_duracion_gracia == n_plazo - 1) //-- Cuando se paga el capital en la última cuota
                                n_cuota = (n_monto_cal * n_tasa_interes);
                            else
                                n_cuota = (n_monto_cal / n_num_cuotas);
                            //-- Modalidad de pago:   1:Anticipado,  2:Vencido
                            if (n_tip_pago == 1 && n_dias_aju == 0)
                                Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, n_monto_cal);
                        }
                        #endregion
                    }
                    else if (n_tip_amo == 3)
                    {
                        #region Capital Fijo Interés Fijo
                        n_cuota = 0;
                        n_tot_interes = 0;
                        n_num = 1;
                        while (n_num <= n_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                atr_finan[n_num].n_valor = 0;
                            n_num = n_num + 1;
                        }
                        if (n_tip_gracia == 4 && n_duracion_gracia == n_plazo - 1) //-- Cuando se paga el capital en la última cuota
                        {
                            n_cuota = (n_monto_cal * n_tasa_interes);
                        }
                        else
                        {
                            n_cuota = (n_monto_cal / n_num_cuotas) + (n_monto_cal * n_tasa_interes);
                        }
                        #endregion
                    }
                    else if (n_tip_amo == 4)
                    {
                        #region Capital Variable Interés Variable
                        if (n_tasa_interes == 0)
                        {
                            #region 
                            n_cuota = (n_monto_cal - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0)) / n_num_cuotas;
                            n_tot_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    atr_finan[n_num].n_valor = 0;
                                n_num = n_num + 1;
                            }
                            #endregion
                        }
                        else
                        {
                            #region Tipo de pago  1:Anticipado, 2:Vencido
                            if (n_tip_pago == 1)
                                n_cuota = (1 - BOFunciones.Power(1 - n_tasa_interes, n_num_cuotas));
                            else
                                n_cuota = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas) - 1);
                            if (n_cuota == 0)
                            {
                                #region
                                n_tot_interes = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                        atr_finan[n_num].n_valor = 0;
                                    n_num = n_num + 1;
                                }
                                if (n_num_cuotas == 0 || n_num_cuotas == null)
                                {
                                    Mensaje_Error("Los parámetros para el cálculo de la cuota se encuentran inconsistentes, Verificar.");
                                    return false;
                                }
                                else
                                {
                                    n_cuota = (n_monto_cal - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0)) / n_num_cuotas;
                                }
                                #endregion
                            }
                            else
                            {
                                #region
                                if (BOFunciones.BuscarGeneral(1196, 1) == "1" && b_LeyMiPyme && g_b_yaprimero)
                                {
                                    n_monto_cal = n_monto_cal + BOFunciones.NVL(n_vpn_otros_tot, 0);
                                }
                                //-- Determinar cuotas extras en valor presente
                                if (n_tip_tf == 4 && n_tip_inttf == 1)
                                    CalcularProrrateoCuotasExtras(n_tasa_dia_tf, n_tipo_cal, f_fec_inicio, n_dias_aju, ref n_val_tfpro);
                                else
                                    n_val_tfpro = 0;
                                //-- Tipo de pago  1:Anticipado, 2:Vencido
                                if (n_tip_pago == 1)
                                {
                                    n_cuota = (n_tasa_interes / n_cuota) * ((n_monto_cal - n_val_tfpro) + BOFunciones.NVL(SumarCapitalCuotasExtras(), 0));
                                    n_cuota = ((n_cuota * n_num_cuotas) - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0)) / n_num_cuotas;
                                }
                                else
                                {
                                    if (n_monto_cal == 0)
                                    {
                                        n_cuota = n_tasa_interes * (n_monto_cal - n_val_tfpro);
                                    }
                                    else
                                    {
                                        if (n_cuotas_gracia > 0 && n_tip_gracia == 3)
                                        {
                                            if (n_tipo_prod == 3)
                                                n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_cuota) * ((n_monto_cal - n_val_tfpro) + (n_cuotas_gracia * n_tasa_interes * ((n_monto_cal - n_val_tfpro))));
                                            else
                                                n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_cuota) * ((n_monto_cal - n_val_tfpro) + (n_cuotas_gracia * n_tasa_interes * (n_monto_cal - n_val_tfpro)));
                                        }
                                        else
                                        {
                                            n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_cuota) * (n_monto_cal - n_val_tfpro);
                                        }
                                    }
                                }
                                #endregion
                                n_tot_interes = (n_cuota * n_num_cuotas) - n_monto_cal;
                                #region
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        //-- Tipo de pago  1:Anticipado, 2:Vencido
                                        if (n_tip_pago == 1)
                                            n_val_aux = 1 - BOFunciones.Power(1 - (atr_finan[n_num].n_tasa_calculo / 100), n_num_cuotas);
                                        else
                                            n_val_aux = BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1;
                                        if (n_val_aux == 0)
                                        {
                                            n_val_aux = 0;
                                        }
                                        else
                                        {
                                            //-- Tipo de pago  1:Anticipado, 2:Vencido
                                            if (n_tip_pago == 1)
                                                n_val_aux = ((atr_finan[n_num].n_tasa_calculo / 100) / n_val_aux) * (n_monto_cal - n_val_tfpro);
                                            else
                                                n_val_aux = (((atr_finan[n_num].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas)) / n_val_aux) * (n_monto_cal - n_val_tfpro);
                                            n_val_aux = (n_val_aux * n_num_cuotas) - (n_monto_cal - n_val_tfpro);
                                        }
                                        n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                        atr_finan[n_num].n_valor = n_val_aux;
                                    }
                                    n_num = n_num + 1;
                                }
                                #endregion
                                if (BOFunciones.BuscarGeneral(1196, 1) != "1" || !b_LeyMiPyme)
                                    n_cuota = n_cuota + BOFunciones.NVL(n_vpn_otros_tot, 0);
                                #region Modalidad de pago:   1:Anticipado,  2:Vencido
                                if (n_tip_pago == 1)
                                    Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, n_monto);
                                //-- Ajustar la cuota para los días de ajuste cuando estos de pagan en la primera cuota.
                                if (BOFunciones.BuscarGeneral(1500, 1) == "2" && n_tip_intant == 3)
                                {
                                    if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                    {
                                        n_factor = BOFunciones.Power(1 + (n_tasa_interes / n_dias_per_cre), n_dias_aju);
                                        n_cuota = n_cuota * BOFunciones.NVL(n_factor, 1);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else if (n_tip_amo == 5)
                    {
                        #region Prorrateados
                        //Mensaje("Tasa de Interés para Cálculo Cuota TipAmo:5 ==>" || n_tasa_interes);
                        if (BOFunciones.BuscarGeneral(1196, 1) == "1" && b_LeyMiPyme && g_b_yaprimero)
                            n_monto_cal = n_monto_cal + BOFunciones.NVL(n_vpn_otros_tot, 0);
                        n_cuota = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas) - 1);
                        if (n_cuota == 0)
                        {
                            #region calculo de la cuota con tasa cero
                            n_tot_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    atr_finan[n_num].n_valor = 0;
                                n_num = n_num + 1;
                            }
                            if (n_num_cuotas == 0 || n_num_cuotas == null)
                            {
                                Mensaje_Error("Los parámetros para el cálculo de la cuota se encuentran inconsistentes, Verificar.");
                                return false;
                            }
                            else
                            {
                                n_cuota = (n_monto_cal - BOFunciones.NVL(SumarCapitalCuotasExtras(), 0)) / n_num_cuotas;
                            }
                            #endregion
                        }
                        else
                        {
                            #region calculo de fecha primer pago
                            if (BOFunciones.BuscarGeneral(1500, 1) == "1" && n_tip_intant == 3)
                            {
                                #region calculo fecha primer pago
                                //-- Ajustar la cuota cuando los intereses de ajuste se cobran en la primera cuota y se manejan dias calendario.COOACEDED.
                                //-- Determinando fecha del primer pago
                                if (n_dias_per_cre == 0 || n_dias_per_cre == null)
                                {
                                    n_dias_per_cre = 0;
                                    n_dias_per_cre = DApackage.ConPeriodicidadNumDia(n_periodic, usuario);
                                }
                                f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                //-- Calculando el factor de dias para calculo de la tasa
                                n_dias_mes = BOFunciones.DateDay(f_fec_pripag);
                                n_dia_des = BOFunciones.DateDay(f_fec_apro);
                                if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                    n_factor = (31 - n_dia_des) / 31;
                                else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                    n_factor = (28 - n_dia_des) / 28;
                                else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                    n_factor = (29 - n_dia_des) / 29;
                                else
                                    n_factor = (n_dias_mes - n_dia_des) / n_dias_mes;
                                n_factor = n_factor * -1;
                                //-- Calculando dias para calculo de intereses de la primera cuota
                                n_dias_aju = Convert.ToDecimal(f_fec_pripag - f_fec_inicio) + n_dias_aju;
                                if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                    n_dias_ajuste = Convert.ToInt32((31 - n_dia_des) + 31);
                                else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                    n_dias_ajuste = Convert.ToInt32((28 - n_dia_des) + 28);
                                else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                    n_dias_ajuste = Convert.ToInt32((29 - n_dia_des) + 29);
                                else
                                    n_dias_ajuste = Convert.ToInt32((n_dias_mes - n_dia_des) + n_dias_mes);
                                //-- Calculando el valor de la cuota
                                n_cuota_nue = 1 - BOFunciones.Power((1 + n_tasa_interes), (n_num_cuotas * -1));
                                n_cuota_nue = n_cuota_nue * BOFunciones.Power((1 + n_tasa_interes), n_factor);
                                n_cuota_nue = (n_monto_cal * n_tasa_interes) / n_cuota_nue;
                                n_cuota = n_cuota_nue;
                                n_cuota = BOFunciones.Redondeo(n_cuota);
                                n_elevado = n_dias_aju / 30;
                                n_tot_interes = BOFunciones.Power((1 + n_tasa_interes), n_elevado) - 1;
                                n_tot_interes = n_tot_interes * n_monto_cal;
                                #endregion
                            }
                            else
                            {
                                #region calcular prorrateo
                                CalcularProrrateoCuotasExtras(n_tasa_dia_tf, n_tipo_cal, f_fec_inicio, n_dias_aju, ref n_val_tfpro);
                                n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuotas)) / n_cuota);
                                n_cuota = n_cuota * (n_monto_cal - n_val_tfpro);
                                n_tot_interes = (n_cuota * n_num_cuotas) - n_monto_cal;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        n_val_aux = BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1;
                                        if (n_val_aux == 0)
                                        {
                                            n_val_aux = 0;
                                        }
                                        else
                                        {
                                            n_val_aux = (((atr_finan[n_num].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas)) / n_val_aux);
                                            n_val_aux = n_val_aux * (n_monto_cal - n_val_tfpro);
                                            n_val_aux = (n_val_aux * n_num_cuotas) - n_monto_cal;
                                        }
                                        n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                        atr_finan[n_num].n_valor = n_val_aux;
                                    }
                                    n_num = n_num + 1;
                                }
                                //-- Modalidad de pago:   1:Anticipado,  2:Vencido
                                if (n_tip_pago == 1)
                                    Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, n_monto_cal);
                                //-- Ajustar la cuota para los días de ajuste cuando estos de pagan en la primea cuota.FerOrt. 8-Dic-2008  
                                if (BOFunciones.BuscarGeneral(1196, 1) != "1" && b_LeyMiPyme)
                                    n_cuota = n_cuota + BOFunciones.NVL(n_vpn_otros_tot, 0);
                                if (BOFunciones.BuscarGeneral(1500, 1) == "2" && n_tip_intant == 3)
                                {
                                    if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                    {
                                        n_factor = BOFunciones.Power(1 + (n_tasa_interes / n_dias_per_cre), n_dias_aju);
                                        n_cuota = n_cuota * BOFunciones.NVL(n_factor, 1);
                                    }
                                }
                                #endregion
                            }
                            #endregion
                        }
                        #endregion
                    }
                    else
                    {
                        #region Errado
                        Mensaje_Error("El tipo de amortización definido no es posible. ->" + n_tip_amo + "<-");
                        n_cuota = 0;
                        return false;
                        #endregion
                    }
                    #endregion
                }
                else
                {
                    Mensaje_Error("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                    return false;
                }
                #endregion
            }
            else if (n_tip_cuota == 3)
            {
                #region Tipo amortizacion:  7: Aumento periodico y Gradiente
                n_num = 1;
                #region determinar valor del gradiente
                while (n_num <= n_atr_fin)
                {
                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                    {
                        n_val_grad = atr_finan[n_num].n_gradiente;
                        s_tipo_grad = atr_finan[n_num].s_tip_gra;
                        break;
                    }
                    n_num = n_num + 1;
                }
                #endregion
                #region
                if (n_tip_amo == 7)
                {
                    #region Gradiente 1
                    if (n_num_cuotas <= 0)
                    {
                        Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                        return false;
                    }
                    else
                    {
                        n_val_grad_per = BOFunciones.CalTasMod(n_val_grad, 1, n_per_gradiente, 2, n_per_gradiente, 2, n_periodic, 2, n_periodic, 0) / 100;
                        if (n_tasa_interes == n_val_grad / 100)
                            n_cuota = n_monto_cal * (1 + n_tasa_interes) / n_num_cuotas;
                        else
                            n_cuota = (n_monto_cal * (n_val_grad_per - n_tasa_interes)) / ((BOFunciones.Power(((1 + n_val_grad_per) / (1 + n_tasa_interes)), n_num_cuotas)) - 1);
                    }
                    #endregion
                }
                else if (n_tip_amo == 8)
                {
                    #region Grandiente 2
                    if (n_num_cuotas <= 0)
                    {
                        Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                        return false;
                    }
                    else
                    {
                        n_cuota = n_monto_cal * (n_tasa_interes + 1) * n_val_grad / n_num_cuotas;
                    }
                    #endregion
                }
                else if (n_tip_amo == 9 || n_tip_amo == 11)
                {
                    #region Gradiente 3
                    if (n_num_cuotas <= 0)
                    {
                        Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                        return false;
                    }
                    else
                    {
                        n_val_grad = n_val_grad / 100;
                        n_tasa_interes_grad = BOFunciones.CalTasMod(n_tasa_interes * 100, 2, n_periodic, 2, n_periodic, 2, n_per_gradiente, 2, n_per_gradiente, 0) / 100;
                        n_per_ano = DApackage.ConPeriodicidadPerAnu(n_periodic, usuario);
                        n_fac_grad = (n_monto_cal * (n_val_grad - n_tasa_interes_grad)) / (BOFunciones.Power(1 + n_val_grad, n_num_cuotas / n_per_ano) * BOFunciones.Power(1 + n_tasa_interes_grad, -n_num_cuotas / n_per_ano) - 1);
                        n_cuota = n_fac_grad / (n_tasa_interes_grad / n_tasa_interes);
                    }
                    #endregion
                }
                else if (n_tip_amo == 10)
                {
                    #region Gradiente 4
                    if (n_num_cuotas <= 0)
                    {
                        Mensaje_Error("El número de cuotas no puede ser cero o negativo");
                        return false;
                    }
                    else
                    {
                        n_cuota = n_monto_cal * (1 + n_tasa_interes) / n_num_cuotas;
                    }
                    #endregion
                }
                #endregion
                #endregion
            }
            else
            {
                //Mensaje_Error("El tipo de liquidación posee errado el tipo de cuota a ser calculado");
                return false;
            }
            #endregion

            //-- Calculando valor de impuesto de timbres
            //Mensaje("Calculando valor de impuesto de timbres");
            #region timbres
            n_num = 1;
            while (n_num <= n_atr_otr)
            {
                if (atr_otro[n_num].n_tip_liq == 10)
                {
                    atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                    if (!bResultado)
                    {
                        //Mensaje_Error("Error al calcular el valor del atributo");
                        return false;
                    }
                    if (n_valor_aux == 1)
                        n_des_cheque = n_des_cheque + n_val_aux;
                }
                n_num = n_num + 1;
            }
            #endregion

            //-- Redondea los valores
            n_tot_interes = BOFunciones.Redondeo(n_tot_interes);

            //-- Calcula la cuota total
            n_cuota = n_cuota + BOFunciones.NVL(n_adi_cuota, 0);

            //-- Redondea los valores de la cuota
            #region
            s_red_cuo = BOFunciones.BuscarGeneral(900, 1);
            if (s_red_cuo != "")
            {
                n_red_cuo = BOFunciones.To_Number(s_red_cuo);
                if (n_red_cuo > 10000 || BOFunciones.Mod(10000, n_red_cuo) != 0)
                {
                    Mensaje_Error("El valor de redondeo no es valido");
                    return false;
                }
                //-- Para redondear al valor superior
                if (BOFunciones.BuscarGeneral(903, 1) == "1")
                {
                    if (n_cuota <= BOFunciones.Round(n_cuota / n_red_cuo) * n_red_cuo)
                    {
                        n_cuota = BOFunciones.Round(n_cuota / n_red_cuo) * n_red_cuo;
                    }
                    else
                    {
                        n_cuota = BOFunciones.Round((n_cuota + n_red_cuo) / n_red_cuo) * n_red_cuo;
                    }
                }
                else
                {
                    n_cuota = BOFunciones.Round(n_cuota / n_red_cuo) * n_red_cuo;
                }
            }
            else
            {
                n_cuota = BOFunciones.Redondeo(n_cuota);
            }
            #endregion

            //-- Ajusta el numero real de cuotas
            if (n_num_cuotas_temp > 0)
            {
                n_num_cuotas = n_num_cuotas_temp;
            }
            b_calculo = true;
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Si la LEYMIPYME se genera con base en el saldo entonces generar el plan de pagos.No se re-calculan si el crédito ya esta activo.
            //---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region 
            if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 1)) == "1" && b_LeyMiPyme && !g_b_yaprimero)
            {
                // Mensaje("Cargando valores de saldos de créditos para calculo de ley mi pyme");  
                #region Inicializa el arreglo
                n_num = 1;
                while (n_num <= n_atr_otr)
                {
                    if (atr_otro[n_num].n_signo == 3 && atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0 && (atr_otro[n_num].n_cod_atr == 40 || atr_otro[n_num].n_cod_atr == 41))
                    {
                        atr_otro[n_num].n_num_pag = -1;
                        atr_otro[n_num].n_valor_calculo = 0;
                        if (atr_otro[n_num].n_valor_presentes.Length > 0)
                        {
                            atr_otro[n_num].n_valor_presentes.Initialize();
                        }
                        atr_otro[n_num].n_valor_presentes = new decimal?[200];
                        if (!(s_estado_cre == "C" || s_estado_cre == "T") || s_estado_cre != null)
                        {
                            if (atr_otro[n_num].n_valor_pagos.Length > 0)
                            {
                                atr_otro[n_num].n_valor_pagos.Initialize();
                            }
                            atr_otro[n_num].n_valor_pagos = new decimal?[200];
                        }
                    }
                    n_num = n_num + 1;
                }
                #endregion
                //-- Inicializa variables de monto y número de cuotas
                n_saldo_base = n_monto;
                g_n_monto_credito = n_monto;
                n_contcuotas = 0;
                //-- Determinar el número de meses de la periodicidad
                n_meses_periodic = 0;
                n_meses_periodic = DApackage.ConPeriodicidadNumeroMeses(n_periodic, usuario);
                #region Genera el plan de pagos para saber el saldo inicial al inicio de cada año del crédito
                n_num_cuotas_pend = n_num_cuotas;
                n_pos = 0;
                while (Plan_Pagos(ref n_pos, ref n_saldo_ini, ref n_cod_atributos, ref n_interes, ref n_capital, ref f_fecha, ref n_int_tf, ref n_cap_tf))
                {
                    //-- Si inicia un nuevo año entonces generar las cuotas de LEY MiPyme
                    if (BOFunciones.Mod((n_pos - 1) * n_meses_periodic, 12) == 0 || n_contcuotas > 0)
                    {
                        #region
                        n_num = 1;
                        while (n_num <= n_atr_otr)
                        {
                            n_val_aux = 0;
                            n_valor_aux = 0;
                            #region  Calcular los valores por cada atributo
                            if (atr_otro[n_num].n_signo == 3 && atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0 && (atr_otro[n_num].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_num].n_cod_atr == n_atr_IVALeyMiPyme))
                            {
                                //-- Establecer el número de cuotas
                                if (n_contcuotas == 0 || n_contcuotas == null)
                                {
                                    n_contcuotas = atr_otro[n_num].n_num_cuotas;
                                }
                                //-- Mirar la cuota en que se esta generando
                                atr_otro[n_num].n_num_pag = n_pos;
                                //-- Tomar el saldo base del inicio del año
                                if (BOFunciones.Mod((n_pos - 1) * n_meses_periodic, 12) == 0)
                                {
                                    n_saldo_base = n_saldo_ini;
                                    if (n_num_cuotas_pend * n_meses_periodic >= 12)
                                    {
                                        n_meses_prop = 12;
                                    }
                                    else
                                    {
                                        if (n_num_cuotas_pend < atr_otro[n_num].n_num_cuotas)
                                            n_meses_prop = Convert.ToInt32(atr_otro[n_num].n_num_cuotas * n_meses_periodic);
                                        else
                                            n_meses_prop = Convert.ToInt32(n_num_cuotas_pend * n_meses_periodic);
                                    }
                                }
                            }
                            #endregion
                            #region Calcular el valor con base en el saldo inicial
                            atr_otro[n_num].Cal_Valor(n_saldo_base, n_meses_prop, n_meses_prop, ref n_val_aux, ref n_signoatr, n_saldo, n_val_interes, n_valorCatg, n_valfactor, n_monto, n_cuota, 0, 4, n_num_codeu, ref bResultado);
                            if (!bResultado)
                            {
                                // Mensaje_Error("Error al calcular el valor del atributo");
                                return false;
                            }
                            #endregion
                            #region Calcular el valor y el valor presente neto
                            n_vpn_aux = BOFunciones.Power(n_tasa_interes + 1, n_pos);
                            if (n_vpn_aux != 0 && n_vpn_aux != null)
                            {
                                if (atr_otro[n_num].n_tip_liq == 19 || atr_otro[n_num].n_tip_liq == 20 || atr_otro[n_num].n_tip_liq == 21)
                                {
                                    if (atr_otro[n_num].n_valor_presentes.Length < atr_otro[n_num].n_num_pag)
                                    {
                                        Array.Resize(ref atr_otro[n_num].n_valor_presentes, Convert.ToInt32(atr_otro[n_num].n_num_pag));
                                    }
                                    if (!(s_estado_cre == "C" || s_estado_cre == "T") || s_estado_cre == null)
                                    {
                                        atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + ((atr_otro[n_num].n_valor_calculo) / n_vpn_aux);
                                        atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                        atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + (atr_otro[n_num].n_valor_calculo);
                                        atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                    }
                                    else
                                        atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + ((atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)]) / n_vpn_aux);
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                }
                                if (atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] == 0 || atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] == null)
                                {
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = 0;
                                }
                            }
                            else
                            {
                                if (!(s_estado_cre == "C" || s_estado_cre == "T") || s_estado_cre == null)
                                {
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + ((atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas) / n_vpn_aux);
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                    atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + (atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas);
                                    atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                }
                                else
                                {
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.NVL(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)], 0) + ((atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] / atr_otro[n_num].n_num_cuotas) / n_vpn_aux);
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = BOFunciones.Redondeo(atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)]);
                                }
                                if (atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] == 0 || atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] == null)
                                {
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = 0;
                                }
                            }
                            #endregion
                            n_num = n_num + 1;
                        }
                        #endregion                    
                        //-- Se resta una cuota porque se hace el primer descuento en el desembolso cuando es anticipado
                        if (n_tip_pago == 1 && n_pos == 1)
                        {
                            n_contcuotas = n_contcuotas - 1;
                        }
                        n_contcuotas = n_contcuotas - 1;
                    }
                    else
                    {
                        #region calcular 
                        n_num = 1;
                        while (n_num <= n_atr_otr)
                        {
                            if (atr_otro[n_num].n_signo == 3 && atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0 && (atr_otro[n_num].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_num].n_cod_atr == n_atr_IVALeyMiPyme))
                            {
                                atr_otro[n_num].n_num_pag = n_pos;
                                if (atr_otro[n_num].n_num_pag != null && n_num <= atr_otro.Length && atr_otro[n_num].n_num_pag <= atr_otro[n_num].n_valor_pagos.Length)
                                {
                                    atr_otro[n_num].n_valor_presentes[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = 0;
                                    atr_otro[n_num].n_valor_pagos[Convert.ToInt32(atr_otro[n_num].n_num_pag)] = 0;
                                }
                            }
                            n_num = n_num + 1;
                        }
                        #endregion
                    }
                    n_num_cuotas_pend = n_num_cuotas_pend - 1;
                }
                g_b_yaprimero = true;
                #endregion
                #region Mensaje("Llamando segundo ciclo de liquidación de crédito para calculo de Ley MiPyme");
                n_num_atr_des = 0;
                if (n_cod_atr_des.Length > 0)
                {
                    n_cod_atr_des.Initialize();
                }
                if (s_nom_atr_des.Length > 0)
                {
                    s_nom_atr_des.Initialize();
                }
                if (n_val_atr_des.Length > 0)
                {
                    n_val_atr_des.Initialize();
                }
                if (f_fechas_pago.Length > 0)
                {
                    f_fechas_pago.Initialize();
                }
                bResultado = Liquidar_Credito();
                #endregion
            }
            #endregion

            return true;

        }

        public bool Liquidar()
        {
            int n_num;
            decimal? n_vpn_aux = null;
            g_n_monto_credito = n_monto;
            g_b_yaprimero = false;
            n_num = 1;
            while (n_num <= n_atr_otr)
            {
                if (atr_otro[n_num].n_signo == 3 && atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0 && (atr_otro[n_num].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_num].n_cod_atr == n_atr_IVALeyMiPyme))
                {
                    atr_otro[n_num].n_num_pag = -1;
                    atr_otro[n_num].n_valor_presentes.Initialize();
                    if (!(s_estado_cre == "C" || s_estado_cre == "T") || s_estado_cre == null)
                        atr_otro[n_num].n_valor_pagos.Initialize();
                }
                n_num = n_num + 1;
            }
            return Liquidar_Credito();
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Calcular interes anticipado  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------     
        public void Int_Anticipado(int? n_tip_cuota, int? n_tip_int, int? n_tip_amo, decimal? n_monto_cal)
        {
            int? n_cod_atr = null;
            string s_nom_atr = null;
            decimal? n_val_atr = null;
            int n_num;
            string g_sentencia;
            // --  Inserta registro en los valores a ser descontados del cheque
            n_num = 1;
            while (n_num <= n_atr_fin)
            {
                atr_finan[n_num].GetAtrfinan_Tasa(ref n_cod_atr, ref s_nom_atr, ref n_val_atr);
                n_val_atr = n_val_atr / 100;
                if (n_val_atr > 0 && n_cod_atr != n_atr_mora)
                {
                    //-- Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente
                    if (n_tip_cuota == 1)
                    {
                        //-- Tipo de interés:  1:Simple,  2:Compuesto
                        if (n_tip_int == 1)
                        {
                            n_val_atr = n_val_atr * n_dias_fijo * n_monto_cal;
                            n_val_atr = BOFunciones.Redondeo(n_val_atr);
                        }
                        else if (n_tip_int == 2)
                        {
                            n_val_atr = BOFunciones.CalTasVen(n_val_atr * 100) / 100;
                            n_val_atr = (BOFunciones.Power((n_val_atr) + 1, n_dias_fijo) - 1);
                            n_val_atr = (BOFunciones.CalTasAnt(n_val_atr * 100) / 100) * n_monto_cal;
                            n_val_atr = BOFunciones.Redondeo(n_val_atr);
                        }
                    }
                    else if (n_tip_cuota == 2)
                    {
                        //-- Tipo de interés:  1:Simple,  2:Compuesto
                        if (n_tip_int == 1)
                        {
                            n_val_atr = n_monto_cal * n_num_cuotas * n_val_atr;
                            n_val_atr = n_val_atr / n_num_cuotas;
                        }
                        else if (n_tip_int == 2)
                        {
                            if (n_tip_amo == 2)
                                n_val_atr = (n_monto_cal * n_val_atr);
                            else if (n_tip_amo == 3)
                                n_val_atr = 0;
                            else if (n_tip_amo == 4)
                                n_val_atr = n_monto_cal * n_val_atr;
                            else if (n_tip_amo == 5)
                                n_val_atr = n_monto_cal * n_val_atr;
                        }
                    }
                    else if (n_tip_cuota == 3)
                    {
                        n_val_atr = 0;
                    }
                    n_val_atr = BOFunciones.Redondeo(n_val_atr);
                    CargarAtributoDescontado(Convert.ToInt32(n_cod_atr), Convert.ToInt32(n_val_atr), 1, " Anticipado");
                    n_des_cheque = n_des_cheque + n_val_atr;
                }
                n_num = n_num + 1;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Generar plan de pagos  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Plan_Pagos(ref int? rn_num_cuota, ref decimal? rn_sal_ini, ref int?[] rn_cod_atributos, ref decimal?[] rn_interes, ref decimal? rn_capital, ref DateTime? rf_fecha, ref decimal? rn_int_tf, ref decimal? rn_cap_tf)
        {
            decimal? n_tot_tf = null;
            decimal? n_cap_tf = null;
            decimal? n_int_tf = null;
            Int64? n_num_tf = null;
            Int64 n_num = 0;
            Int64? n_num_ = 0;
            int n_cont;
            decimal? n_sum_int;
            decimal? n_cap_ant = null;
            decimal? n_periodos_anuales;
            int n_pos;
            //--Variable utilizada para calcular el siguiente pago y determinar si un termino fijo cae en el rango
            //--cuando es cobro anticipado, para cobrar proporcionalmente el interes
            DateTime? f_sig_cuota = null;
            int? n_dias_1;
            int? n_dias_2;
            decimal? n_aux_cuota;
            //--Para cambios de cuotas en gradientes
            decimal? n_nue_cuota;
            //--Para reestructurados manejo del atributo adicional
            string s_lin_rees;
            //--Variables para manejo de devolucion de interes sobre terminos fijos pagado por anticipado en cuotas
            decimal? n_saldo_actual;
            decimal? n_per_diaria = null;
            DateTime? f_fec_prox;
            decimal? n_tasa_interes_diaria;
            decimal? n_val_aux = null;
            decimal? n_dias_cte;
            //--Manejo de gracia
            bool sb_existe;
            string s_nom_atr = "";
            decimal? n_temp_capital;
            int? n_signoatr = null;
            decimal? n_valor_aux;
            string s_nom_atr_aux;
            decimal? n_val_interes = 0;
            //--Otras variables
            decimal? n_interes = null;
            decimal? n_num_cuot;
            decimal? n_tasa_interes_grad;
            decimal? n_fac_grad;
            decimal? n_per_ano = null;
            decimal? n_val_tfpro = null;
            decimal? n_tasa_dia_tf;
            decimal? n_amortiza_tf;
            decimal? n_val_indiv_rest;
            decimal? n_aux_indiv_rest;
            decimal? n_cuotas_desc;
            decimal? n_valfactor = null;
            int? n_cod_atr_dep;
            decimal? n_int_tf2;
            string sAux = "";
            bool bResultado = false;
            string g_sentencia;
            string s_atr = "";
            decimal? n_val_temp;
            decimal? n_cuota_original;
            DateTime? f_fec_act1;
            decimal? n_dias_apro;

            rn_interes = new decimal?[10];
            rn_cod_atributos = new int?[10];

            if (b_calculo)
            {
                #region -- Inicializaciónn
                MensajeConsola("Inicialización de cálculos en la cuota " + rn_num_cuota);
                n_int_tf2 = 0;
                if ((rn_num_cuota == 0 || rn_num_cuota == null) && (sn_pos_tf <= 1 || sn_pos_tf == null))
                {
                    #region calculo de la primera cuota
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ -
                    //--Cuando es período de gracia entonces calcula el valor de interes acumulado lo carga al atributo de gracia
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ -
                    #region calculo interes acumulado
                    if (n_acumulado_int > 0)
                    {
                        n_cuota_adi = n_acumulado_int;
                        n_num = 1;
                        while (true)
                        {
                            MensajeConsola("Cargando interes durante el período de gracia " + n_num);
                            if (n_num >= n_atr_otr)
                            {
                                s_nom_atr = DApackage.NombreAtributo(n_atrib_gra, usuario);
                                DApackage.AtributoGracia(n_atrib_gra, ref s_nom_atr, usuario);

                                n_atr_otr = n_atr_otr + 1;
                                atr_otro[n_num].Ins_Dat_Basico(n_atrib_gra, s_nom_atr, n_cuota_adi, 3);
                                break;
                            }
                            else
                            {
                                if (atr_otro[n_num].n_cod_atr == n_atrib_gra)
                                {
                                    atr_otro[n_num].n_valor_calculo = n_cuota_adi;
                                    break;
                                }
                                n_num = n_num + 1;
                            }
                        }
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //--Calculo de los días de la periodicidad
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    #region calculo días
                    if (p_n_num_dias_per == 0 || p_n_num_dias_per == null)
                    {
                        n_dias_per_cre = 0;
                        n_dias_per_cre = DApackage.ConPeriodicidadNumDia(n_periodic, usuario);
                        p_n_num_dias_per = Convert.ToInt32(n_dias_per_cre);
                    }
                    else
                    {
                        n_dias_per_cre = p_n_num_dias_per;
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //--Parametro para manejo de atributo adicional de reestructurado
                    //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ -
                    MensajeConsola("Determinar si el crédito es una reestructuración");
                    #region reestructurados
                    DApackage.CreditoRestructurado(s_cod_credi, ref sAux, usuario);

                    if (string.IsNullOrWhiteSpace(sAux)) // (pBasedato % NOTFOUND)
                        sb_existe_reestr = false;
                    else
                        sb_existe_reestr = true;

                    //--Línea de crédito reestructurado
                    s_lin_rees = BOFunciones.BuscarGeneral(430, 1);
                    if (BOFunciones.Instr(s_lin_rees, s_cod_credi) != -1 && sb_existe_reestr)
                    {
                        sb_existe_reestr = true;
                        //-- >>>=== if wh_recoger = hWndNULL )
                        if (false)
                        {
                            sn_val_atr_rees = 0;
                            DApackage.CreditoRestructuradosuma(n_radic, ref sn_val_atr_rees, usuario);
                        }
                        else
                        {
                            sn_val_atr_rees = 0;
                            //--sn_val_atr_rees = SalSendMsg(wh_recoger, PM_Click_1, 0, 0);
                        }
                        n_monto = n_monto - sn_val_atr_rees;
                    }
                    else
                    {
                        sb_existe_reestr = false;
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    //--Carga los valores de los terminos fijos
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    #region terminos fijos
                    if (bterminos)
                        n_tot_tf = SumarCapitalCuotasExtras();
                    else
                        n_tot_tf = 0;
                    sn_monto = n_monto + n_adi_monto;
                    sn_saldo = sn_monto;
                    if (n_tip_tf != 4 && !(n_tip_amo == 4 && n_tip_tf == 4 && n_tip_inttf == 1))
                    {
                        sn_monto_cuo = n_monto + n_adi_monto - n_tot_tf;
                        sn_saldo_cuo = sn_monto - n_tot_tf;
                    }
                    else
                    {
                        sn_monto_cuo = n_monto + n_adi_monto;
                        sn_saldo_cuo = sn_monto;
                    }
                    #endregion
                    //--Inicializando variables
                    #region inicializar
                    MensajeConsola("Inicializando las variables para poder generar el plan de pagos");
                    sn_cuota = 0;
                    sn_cuota_desc = 0;
                    sn_cuota_gracia = 0;
                    sn_dias = Convert.ToInt32(n_dias_per_cre);
                    sf_fecha = f_fec_inicio;
                    //--Trae primer termino fijo si posee
                    sn_pos_tf = 1;
                    if (bterminos)
                    {
                        n_cap_tf = 0;
                        bResultado = GetCuotaExtra(ref sn_pos_tf, ref n_cap_tf, ref n_int_tf, ref sf_fec_tf, ref n_num_);
                    }
                    #endregion
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    MensajeConsola("Trae interes de atributos financiados " + n_atr_fin);
                    //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                    #region atributos financiados
                    n_num = 1;
                    n_val_interes = 0;
                    sn_num_atr_fin = 0;
                    while (n_num <= n_atr_fin)
                    {
                        MensajeConsola("Cargando tasa de interés del atributo " + atr_finan[n_num].n_cod_atr + " -->" + atr_finan[n_num].n_tasa_calculo);
                        sn_num_atr_fin = sn_num_atr_fin + 1;
                        Array.Resize(ref sn_tasa_atr_fin, 1);
                        sn_tasa_atr_fin[sn_num_atr_fin] = atr_finan[n_num].n_tasa_calculo / 100;
                        if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                            n_val_interes = atr_finan[n_num].n_tasa_calculo;
                        n_num = n_num + 1;
                    }
                    #endregion
                    //--Calculando valores sumados al monto o descontados del cheque
                    #region valores sumados al monto
                    n_num = 1;
                    n_val_indiv = 0;
                    while (n_num <= n_atr_otr)
                    {
                        if (atr_otro[n_num].n_signo != 99)
                        {
                            if ((atr_otro[n_num].n_tip_des == 2 || atr_otro[n_num].n_tip_des == 3) && (atr_otro[n_num].n_tip_liq == 6 || atr_otro[n_num].n_tip_liq == 7 || atr_otro[n_num].n_tip_liq == 13 || atr_otro[n_num].n_tip_liq == 17) && !(BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 1)) == atr_otro[n_num].n_cod_atr))
                            {
                                atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, sn_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                if (!bResultado)
                                    return false;
                                //--Si el valor del atributo es mayor de cero cargarlo a los datos
                                if (n_val_aux > 0)
                                {
                                    //-- Suma los valores financiados individual
                                    CargarAtributoDescontado(Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToDecimal(n_val_aux), 1, " Anticipado");
                                    n_adi_monto = n_adi_monto + n_val_aux;
                                    n_des_cheque = n_des_cheque + n_val_aux;
                                }
                                //--Si el valor del atributo es financiado individual
                                if (n_signoatr == 3)
                                {
                                    n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                    break;
                                }
                            }
                            //--Determinar el signo del atributo
                            if (n_signoatr == 1)       //-- Descontado del cheque
                            {
                                n_des_cheque = n_des_cheque + n_val_aux;
                            }
                            else if (n_signoatr == 2) //-- Financiado conjunto
                            {
                                n_adi_monto = n_adi_monto + n_val_aux;
                            }
                            else if (n_signoatr == 3) //-- Financiado Individual
                            {
                                n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                n_adi_cuota = n_adi_cuota + n_val_indiv;
                            }
                            else if (n_signoatr == 4) //-- Financiado Individual Excluido
                            {
                                n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo;
                            }
                            else if (n_signoatr == 5)  //-- Adicional a la cuota
                            {
                                n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo;
                            }
                            else if (n_signoatr == 6)  //-- Pago en la primera cuota
                            {
                                n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                n_adi_cuota = n_adi_cuota + n_val_indiv;
                            }
                        }
                        n_num = n_num + 1;
                    }
                    #endregion
                    #endregion
                }
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Calculando valores sumados al monto o descontados del cheque
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                MensajeConsola("Calculando los valores de los atributos descontados que hacen parte de la cuota");
                #region calculando atributos descontados
                n_num = 1;
                n_val_indiv = 0;
                n_val_indiv_rest = 0;
                while (n_num <= n_atr_otr)
                {
                    if (atr_otro[n_num].n_signo != 99)
                    {
                        #region buscando atributos
                        if (((atr_otro[n_num].n_tip_des == 2 || atr_otro[n_num].n_tip_des == 3) && (atr_otro[n_num].n_tip_liq == 6 || atr_otro[n_num].n_tip_liq == 7 || atr_otro[n_num].n_tip_liq == 9)) || (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0 && atr_otro[n_num].n_num_cuotas <= n_num_cuotas))
                        {
                            #region //-- Calcular el valor del atributo
                            if (n_tip_cuota == 1 && n_tip_pago == 2)
                            {
                                atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, n_cuota - sn_saldo, 100, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                if (!bResultado)
                                    return false;
                            }
                            else
                            {
                                atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, sn_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                if (!bResultado)
                                    return false;
                            }
                            #endregion
                            #region //--Determinar los valores individuales a pagar en cada cuota                            
                            if ((n_signoatr == 3 || n_signoatr == 4 || n_signoatr == 5) && n_num <= atr_otro.Length && rn_num_cuota + 1 <= atr_otro[n_num].n_valor_pagos.Length)
                            {
                                #region calcular el valor
                                n_val_indiv = n_val_indiv + atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                if (sn_cuota + 1 <= atr_otro[n_num].n_num_cuotas || (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme && atr_otro[n_num].n_valor_pagos[Convert.ToInt32(rn_num_cuota + 1)] != 0))
                                {
                                    if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme && !g_b_yaprimero)
                                    {
                                        n_aux_indiv_rest = 0;
                                    }
                                    else
                                    {
                                        //-- Determinar el valor a pagar
                                        if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 1)) == "1" && b_LeyMiPyme && g_b_yaprimero && rn_num_cuota != null)
                                        {
                                            n_aux_indiv_rest = atr_otro[n_num].n_valor_pagos[Convert.ToInt32(rn_num_cuota + 1)];
                                            n_aux_indiv_rest = BOFunciones.Redondeo(n_aux_indiv_rest);
                                        }
                                        else
                                        {
                                            n_aux_indiv_rest = (atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas);
                                            n_aux_indiv_rest = BOFunciones.Redondeo(n_aux_indiv_rest);
                                        }
                                        n_val_indiv_rest = n_val_indiv_rest + n_aux_indiv_rest;
                                    }
                                }
                                #endregion
                            }
                            if (n_signoatr == 4)
                                n_val_indiv = 0;
                            if (n_signoatr == 6 && rn_num_cuota != 0)
                                n_val_indiv = 0;
                            #endregion
                        }
                        #endregion
                    }
                    n_num = n_num + 1;
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Se inicializa los valores de las variables que contiene los valores a pagar por cada atributo
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region inicializar valores del plan de pagos
                MensajeConsola("Inicializar los valores del plan de pagos");
                if (rn_interes.Length > 0)
                    rn_interes.Initialize();
                if (rn_cod_atributos.Length > 0)
                    rn_cod_atributos.Initialize();
                n_num = 1;
                while (n_num <= n_atr_fin + n_atr_otr)
                {
                    Array.Resize(ref rn_interes, rn_interes.Length + 1);
                    Array.Resize(ref rn_cod_atributos, rn_cod_atributos.Length + 1);
                    rn_interes[n_num] = 0;
                    if (n_num <= n_atr_fin)
                        rn_cod_atributos[n_num] = atr_finan[n_num].n_cod_atr;
                    else
                        rn_cod_atributos[n_num] = atr_otro[n_num - n_atr_fin].n_cod_atr;
                    n_num = n_num + 1;
                }
                rn_capital = 0;
                rn_int_tf = 0;
                rn_cap_tf = 0;
                n_sum_int = 0;
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Calculo del número de cuota
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region calular número cuota
                MensajeConsola("Calcular el número de cuota");
                sn_cuota = sn_cuota + 1;
                if (n_gracia == 1)
                    sn_cuota_gracia = sn_cuota_gracia + 1;
                if (sn_cuota > n_num_cuotas && sn_saldo <= 0)
                    return false;
                rn_num_cuota = Convert.ToInt32(sn_cuota);
                rn_sal_ini = sn_saldo;
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Calculo de la fecha de pago
                //--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region fecha de pago
                rf_fecha = sf_fecha;
                if (n_tip_cuota == 1)
                {
                    sf_fecha = BOFunciones.FecSumDia(sf_fecha, Convert.ToInt32(n_dias_fijo), Convert.ToInt32(n_tipo_cal));
                }
                else
                {
                    sf_fecha = BOFunciones.FecSumDia(sf_fecha, Convert.ToInt32(sn_dias), Convert.ToInt32(n_tipo_cal));
                    f_sig_cuota = BOFunciones.FecSumDia(sf_fecha, Convert.ToInt32(sn_dias), Convert.ToInt32(n_tipo_cal));
                }
                #endregion
                #region determinar terminos fijos
                if (sn_pos_tf > 0)
                {
                    //-- Si el pago es anticipado se coloca el termino fijo antes de la cuota, de lo contrario se coloca despues
                    if ((sf_fec_tf <= sf_fecha && n_tip_pago == 1) || (sf_fec_tf < sf_fecha && n_tip_pago == 2))
                    {
                        n_saldo_actual = sn_saldo;
                        f_fec_prox = sf_fecha;
                        //--Ajusta fecha y numero de cuota para controlar las cuotas periodicas
                        sf_fecha = rf_fecha;
                        sn_cuota = sn_cuota - 1;
                        //--Inicializa valores para el termino fijo
                        rn_num_cuota = null;
                        if (bterminos)
                        {
                            rn_cap_tf = 0;
                            bResultado = GetCuotaExtra(ref sn_pos_tf, ref rn_cap_tf, ref rn_int_tf, ref rf_fecha, ref n_num_tf);
                        }
                        if (sn_saldo < rn_cap_tf)
                            rn_cap_tf = 0;
                        sn_pos_tf = sn_pos_tf + 1;
                        sn_saldo = sn_saldo - rn_cap_tf;
                        //--Determina si se disminuye el saldo de cuotas cuando incluye los tf para calculo de interes
                        if (n_tip_tf == 4)
                            sn_saldo_cuo = sn_saldo_cuo - rn_cap_tf;
                        //--Calcula siguiente termino fijo
                        bResultado = GetCuotaExtra(ref sn_pos_tf, ref n_cap_tf, ref n_int_tf, ref sf_fec_tf, ref n_num_tf);
                        #region --Interes de terminos fijos anticipado                        
                        if (n_tip_paginttf == 1)
                        {
                            if (sn_pos_tf > 0)
                                rn_int_tf = n_int_tf;
                            else
                                rn_int_tf = 0;
                            //--Determina valor adicional por devolucion de interes de termino fijo anticipado
                            if (BOFunciones.BuscarGeneral(930, 1) == "1" || BOFunciones.BuscarGeneral(930, 1) == "2")
                            {
                                n_dias_cte = BOFunciones.FecDifDia(rf_fecha, f_fec_prox, 1);
                                if (n_dias_cte > 0)
                                {
                                    n_per_dia = BOFunciones.CodPeriodicidadDiaria(1);
                                    n_tasa_interes_diaria = 0;
                                    n_num = 1;
                                    while (n_num <= n_atr_fin)
                                    {
                                        atr_finan[n_num].Conv_Tasa(Convert.ToInt32(n_per_diaria), n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                                        //--No se tiene en cuenta el interes de mora
                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                            n_tasa_interes_diaria = n_tasa_interes_diaria + n_val_aux;
                                        n_num = n_num + 1;
                                    }
                                    n_tasa_interes_diaria = n_tasa_interes_diaria / 100;
                                    if (BOFunciones.BuscarGeneral(930, 2) == "1")
                                    {
                                        n_tasa_interes_diaria = n_tasa_interes_diaria * n_dias_cte;
                                    }
                                    else
                                    {
                                        n_tasa_interes_diaria = n_tasa_interes_diaria + 1;
                                        n_tasa_interes_diaria = BOFunciones.Power(n_tasa_interes_diaria, n_dias_cte);
                                        n_tasa_interes_diaria = (n_tasa_interes_diaria - 1);
                                    }
                                    n_int_tf2 = rn_cap_tf * n_tasa_interes_diaria;
                                    n_int_tf2 = BOFunciones.Redondeo(n_int_tf2);
                                    sn_saldo = sn_saldo - n_int_tf2;
                                    sn_saldo_cuo = sn_saldo_cuo - n_int_tf2;
                                }
                            }
                        }
                        n_num = 1;
                        while (n_num <= n_atr_otr)
                        {
                            if (atr_otro[n_num].n_cod_atr == sn_atr_adm)
                                rn_interes[sn_num_atr_fin + n_num] = 0;
                            n_num = n_num + 1;
                        }
                        #endregion
                    }
                    return true;
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Verifica  el dia de pago
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region verificar el dia de pago
                MensajeConsola("Verificar el día de pago");
                if (BOFunciones.DateMonth(rf_fecha) == 2 && BOFunciones.DateDay(rf_fecha) >= 28 && BOFunciones.DateMonth(f_fec_inicio) != 2 && (BOFunciones.DateDay(f_fec_inicio) == 28 || BOFunciones.DateDay(f_fec_inicio) == 29))
                    if (sn_dias == 30 || sn_dias == 90 || sn_dias == 180 || sn_dias == 360)
                        sf_fecha = BOFunciones.DateConstruct(BOFunciones.DateYear(sf_fecha), BOFunciones.DateMonth(sf_fecha), BOFunciones.DateDay(f_fec_inicio), 0, 0, 0);
                rf_fecha = sf_fecha;
                //--Quita de la cuota el valor de los atributos financiados individual
                n_aux_cuota = n_cuota - n_adi_cuota;
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                MensajeConsola("Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente " + n_tip_cuota);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region tipo de cuota
                if (n_tip_cuota == 1)
                {
                    #region plazo fijo
                    //-- Tipo de interés: 1:Anticipado,  2:Vencido
                    if (n_tip_pago == 1)
                    {
                        rn_capital = sn_saldo;
                    }
                    else if (n_tip_pago == 2)
                    {
                        n_num = 1;
                        n_sum_int = 0;
                        n_tot_interes = n_aux_cuota - sn_saldo;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                {
                                    rn_interes[n_num] = n_tot_interes - n_sum_int;
                                }
                                else
                                {
                                    //-- Se ajustó para que calcule correctamente los intereses según corresponda el tipo de interés.
                                    if (n_tip_int == 1)
                                    {
                                        rn_interes[n_num] = sn_tasa_atr_fin[n_num] * n_dias_fijo * sn_saldo;
                                    }
                                    else
                                    {
                                        rn_interes[n_num] = (BOFunciones.Power(sn_tasa_atr_fin[n_num] + 1, n_dias_fijo) - 1) * sn_saldo;
                                    }
                                }
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        rn_capital = sn_saldo;
                    }
                    else
                    {
                        MensajeConsola("El tipo de pago o modalidad se encuentra errado, debe ser Vencido o Anticipado");
                        return false;
                    }
                    #endregion
                }
                else if (n_tip_cuota == 2)
                {
                    #region serie uniforme
                    //--Tipo de interés: 1:Simple,  2:Compuesto
                    if (n_tip_int == 1)
                    {
                        #region simple
                        if (n_tip_amo == 1)
                        {
                            if (n_tip_tf != 4)
                            {
                                rn_capital = sn_monto_cuo / (n_num_cuotas - n_cuotas_gracia);
                            }
                            else
                            {
                                n_tot_tf = SumarCapitalCuotasExtras();
                                rn_capital = (sn_monto_cuo - n_tot_tf) / (n_num_cuotas - n_cuotas_gracia);
                            }
                            rn_capital = BOFunciones.Redondeo(rn_capital);
                            n_sum_int = 0;
                            //--Determina si es interés anticipado y es la última cuota que no aparezca interés
                            if (!(n_tip_pago == 1 && rn_num_cuota == n_num_cuotas))
                            {
                                n_num = 1;
                                while (n_num <= sn_num_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        if (n_num == sn_num_atr_fin)
                                        {
                                            rn_interes[n_num] = n_aux_cuota - rn_capital - n_sum_int;
                                        }
                                        else
                                        {
                                            rn_interes[n_num] = (sn_tasa_atr_fin[n_num] * sn_monto_cuo) / n_num_cuotas;
                                            rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                        }
                                    }
                                    else
                                    {
                                        rn_interes[n_num] = 0;
                                    }
                                    if (rn_interes[n_num] == null)
                                        rn_interes[n_num] = 0;
                                    n_sum_int = n_sum_int + rn_interes[n_num];
                                    n_num = n_num + 1;
                                }
                            }
                        }
                        else
                        {
                            MensajeConsola("El tipo de amortización definido no es posible");
                            return false;
                        }
                        #endregion
                    }
                    else if (n_tip_int == 2)
                    {
                        MensajeConsola("Tipo amortización:  2: KF, IV;   3: KF, IF;   4: CV, IV   5: T.F. Prorrateados " + sn_num_atr_fin);
                        if (n_tip_amo == 2)
                        {
                            #region capital fijo interés variable
                            if (n_tip_tf != 4)
                            {
                                rn_capital = sn_monto_cuo / (n_num_cuotas - n_cuotas_gracia);
                            }
                            else
                            {
                                if (bterminos)
                                    n_tot_tf = SumarCapitalCuotasExtras();
                                else
                                    n_tot_tf = 0;
                                rn_capital = (sn_monto_cuo - n_tot_tf) / (n_num_cuotas - n_cuotas_gracia);
                            }
                            rn_capital = BOFunciones.Redondeo(rn_capital);
                            n_sum_int = 0;
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    MensajeConsola("Tipo de pago:   1: Anticipado,  2: Vencido " + n_tip_pago);
                                    if (n_tip_pago == 1)
                                    {
                                        if (n_num == sn_num_atr_fin)
                                        {
                                            //--Si los terminos fijos van a solo capital, es interes anticipado y cae una prima en el rango se cobra interes proporcional
                                            if (n_tip_tf == 4 && sf_fec_tf > sf_fecha && sf_fec_tf < f_sig_cuota)
                                            {
                                                n_dias_1 = Convert.ToInt32(sf_fec_tf - sf_fecha);
                                                if (n_dias_1 == null || n_dias_1 < 0)
                                                    n_dias_1 = 0;
                                                if (n_dias_1 > sn_dias)
                                                    n_dias_1 = sn_dias;
                                                n_dias_2 = sn_dias - n_dias_1;
                                                if (bterminos)
                                                {
                                                    n_cap_tf = 0;
                                                    bResultado = GetCuotaExtra(ref sn_pos_tf, ref n_cap_tf, ref n_int_tf, ref sf_fec_tf, ref n_num_tf);
                                                }
                                                rn_interes[n_num] = ((n_tasa_interes * n_dias_1 / sn_dias) * (sn_saldo_cuo - rn_capital)) + ((n_tasa_interes * n_dias_2 / sn_dias) * (sn_saldo_cuo - rn_capital - n_cap_tf)) - n_sum_int;
                                            }
                                            else
                                            {
                                                rn_interes[n_num] = ((sn_saldo_cuo - rn_capital) * n_tasa_interes) - n_sum_int;
                                            }
                                        }
                                        else
                                        {
                                            //-- Si los terminos fijos van a solo capital, es interes anticipado y cae una prima en el rango se cobra interes proporcional
                                            if (n_tip_tf == 4 && sf_fec_tf > sf_fecha && sf_fec_tf < f_sig_cuota)
                                            {
                                                n_dias_1 = Convert.ToInt32(sf_fec_tf - sf_fecha);
                                                if (n_dias_1 == null || n_dias_1 < 0)
                                                    n_dias_1 = 0;
                                                if (n_dias_1 > sn_dias)
                                                    n_dias_1 = sn_dias;
                                                n_dias_2 = sn_dias - n_dias_1;
                                                if (bterminos)
                                                {
                                                    n_cap_tf = 0;
                                                    bResultado = GetCuotaExtra(ref sn_pos_tf, ref n_cap_tf, ref n_int_tf, ref sf_fec_tf, ref n_num_tf);
                                                }
                                                rn_interes[n_num] = ((sn_tasa_atr_fin[n_num] * n_dias_1 / sn_dias) * (sn_saldo_cuo - rn_capital)) + ((sn_tasa_atr_fin[n_num] * n_dias_2 / sn_dias) * (sn_saldo_cuo - rn_capital - n_cap_tf));
                                            }
                                            else
                                            {
                                                rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - rn_capital);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (n_num == sn_num_atr_fin)
                                            rn_interes[n_num] = (sn_saldo_cuo * n_tasa_interes) - n_sum_int;
                                        else
                                            rn_interes[n_num] = sn_tasa_atr_fin[n_num] * sn_saldo_cuo;
                                    }
                                    if (rn_num_cuota == 1 && n_tip_intant == 3 && n_num == 0 && n_int_aju != null)
                                    {
                                        rn_interes[n_num] = rn_interes[n_num] + n_int_aju;
                                        rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                    }
                                    else
                                    {
                                        rn_interes[n_num] = 0;
                                    }
                                    if (rn_interes[n_num] == null)
                                        rn_interes[n_num] = 0;
                                    n_sum_int = n_sum_int + rn_interes[n_num];
                                    n_num = n_num + 1;
                                }
                            }
                            #endregion
                        }
                        else if (n_tip_amo == 3)
                        {
                            #region capital fijo interes fijo
                            if (n_tip_tf != 4)
                            {
                                rn_capital = sn_monto_cuo / (n_num_cuotas - n_cuotas_gracia);
                            }
                            else
                            {
                                if (bterminos)
                                {
                                    n_tot_tf = SumarCapitalCuotasExtras();
                                }
                                else
                                {
                                    n_tot_tf = 0;
                                }
                                rn_capital = (sn_monto_cuo - n_tot_tf) / (n_num_cuotas - n_cuotas_gracia);
                            }
                            rn_capital = BOFunciones.Redondeo(rn_capital);
                            n_sum_int = 0;
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                //--Tipo de pago: 1: Anticipado,  2: Vencido
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * sn_monto_cuo;
                                    rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                }
                                else
                                {
                                    rn_interes[n_num] = 0;
                                }
                                if (rn_interes[n_num] == null)

                                    rn_interes[n_num] = 0;
                                n_sum_int = n_sum_int + rn_interes[n_num];
                                n_num = n_num + 1;
                            }
                            #endregion
                        }
                        else if (n_tip_amo == 4)
                        {
                            #region capital variable interés variable
                            //--Tipo de pago: 1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_periodos_anuales = DApackage.ConPeriodicidadPerAnu(n_periodic, usuario);
                            }
                            n_sum_int = 0;
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                //--Tipo de pago: 1: Anticipado,  2: Vencido
                                if (n_tip_pago == 1)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        rn_interes[n_num] = (sn_saldo_cuo - n_aux_cuota) * ((1 / (1 - sn_tasa_atr_fin[n_num])) - 1);
                                        //--Calcular los intereses por los días de ajuste y sumarlos al valor de los intereses del período.  
                                        if (BOFunciones.BuscarGeneral(1500, 2) == "2" && n_tip_intant == 3 && sn_cuota == 1)
                                            if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                                rn_interes[n_num] = rn_interes[n_num] + (sn_tasa_atr_fin[n_num] / n_dias_per_cre * n_dias_aju * (sn_saldo - BOFunciones.NVL(n_cap_ant, 0)));
                                        rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                    }
                                    else
                                    {
                                        rn_interes[n_num] = 0;
                                    }
                                }
                                else
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        if (n_num == sn_num_atr_fin && !(n_tip_intant == 3 && sn_cuota == 1))
                                            rn_interes[n_num] = (sn_saldo_cuo * n_tasa_interes) - n_sum_int;
                                        else
                                            rn_interes[n_num] = sn_tasa_atr_fin[n_num] * sn_saldo_cuo;
                                        //--Calcular los intereses por los días de ajuste y sumarlos al valor de los intereses del período.  
                                        if (BOFunciones.BuscarGeneral(1500, 1) == "2" && n_tip_intant == 3 && sn_cuota == 1)
                                            if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                                if (n_cap_ant == null)
                                                    n_cap_ant = 0;
                                        rn_interes[n_num] = rn_interes[n_num] + BOFunciones.NVL((sn_tasa_atr_fin[n_num] / n_dias_per_cre * n_dias_aju * (sn_saldo - n_cap_ant)), 0);
                                        rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                    }
                                    else
                                    {
                                        rn_interes[n_num] = 0;
                                    }
                                }
                                if (rn_interes[n_num] == null)
                                    rn_interes[n_num] = 0;
                                n_sum_int = n_sum_int + rn_interes[n_num];
                                n_num = n_num + 1;
                            }
                            rn_capital = n_aux_cuota - n_sum_int - BOFunciones.NVL(n_val_indiv_rest, 0);
                            #endregion
                        }
                        else if (n_tip_amo == 5)
                        {
                            #region prorrateados
                            //--Tipo de pago: 1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_sum_int = sn_saldo * n_tasa_interes;
                                n_sum_int = BOFunciones.Redondeo(n_sum_int);
                                if (n_sum_int == null)
                                    n_sum_int = 0;
                                rn_capital = n_aux_cuota - n_sum_int;
                                n_cap_ant = rn_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            }
                            n_sum_int = 0;
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == sn_num_atr_fin && !(n_tip_intant == 3 && sn_cuota == 1))
                                    {
                                        rn_interes[n_num] = ((sn_saldo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                    }
                                    else
                                    {
                                        if (BOFunciones.BuscarGeneral(1500, 2) == "1" && n_tip_intant == 3 && sn_cuota == 1)
                                        {
                                            //--Para la primera cuota calcular los atributos de la cuota incluyendo días de ajuste, tomar dias calendario.COOACEDED.
                                            if (n_dias_ajuste != 0)
                                            {
                                                if (BOFunciones.DateMonth(rf_fecha) == 1 || BOFunciones.DateMonth(rf_fecha) == 3 || BOFunciones.DateMonth(rf_fecha) == 5 || BOFunciones.DateMonth(rf_fecha) == 7 || BOFunciones.DateMonth(rf_fecha) == 8 || BOFunciones.DateMonth(rf_fecha) == 10 || BOFunciones.DateMonth(rf_fecha) == 12 || (BOFunciones.DateMonth(rf_fecha) == 2 && BOFunciones.Mod(BOFunciones.DateYear(rf_fecha), 4) == 0))
                                                    rn_interes[n_num] = (BOFunciones.Power(1 + sn_tasa_atr_fin[n_num], n_dias_ajuste / BOFunciones.DateDay(Convert.ToDateTime(rf_fecha).AddDays(1))) - 1) * sn_saldo;
                                                else
                                                    rn_interes[n_num] = (BOFunciones.Power(1 + sn_tasa_atr_fin[n_num], n_dias_ajuste / BOFunciones.DateDay(rf_fecha)) - 1) * sn_saldo;
                                            }
                                            else
                                            {
                                                rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo - n_cap_ant);
                                            }
                                        }
                                        else if (BOFunciones.BuscarGeneral(1500, 2) == "2" && n_tip_intant == 3 && sn_cuota == 1)
                                        {
                                            //--Calcular los intereses por los días de ajuste y sumarlos al valor de los intereses del período.
                                            rn_interes[n_num] = (sn_tasa_atr_fin[n_num] * (sn_saldo - n_cap_ant));
                                            if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                                rn_interes[n_num] = rn_interes[n_num] + (sn_tasa_atr_fin[n_num] / n_dias_per_cre * n_dias_aju * (sn_saldo - n_cap_ant));
                                        }
                                        else if (BOFunciones.BuscarGeneral(1500, 2) == "3" && n_tip_intant == 3 && sn_cuota == 1 && atr_finan[n_num].n_cod_atr == n_atr_corr)
                                        {
                                            //--Calcular los intereses por los días de ajuste y sumarlos al valor de los intereses del período.
                                            rn_interes[n_num] = (sn_tasa_atr_fin[n_num] * (sn_saldo - n_cap_ant));
                                            if (n_dias_per_cre != 0 && n_dias_per_cre != null)
                                                rn_interes[n_num] = rn_interes[n_num] + (sn_tasa_atr_fin[n_num] / n_dias_per_cre * n_dias_aju * (sn_saldo - n_cap_ant));
                                        }
                                        else
                                            rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo - n_cap_ant);
                                    }
                                    rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                                }
                                else
                                {
                                    rn_interes[n_num] = 0;
                                }
                                if (rn_interes[n_num] == null)
                                    rn_interes[n_num] = 0;
                                n_sum_int = n_sum_int + BOFunciones.NVL(rn_interes[n_num], 0);
                                n_num = n_num + 1;
                            }
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                                rn_capital = n_aux_cuota - n_sum_int - n_val_indiv_rest;
                            #endregion
                        }
                        else
                        {
                            MensajeConsola("El tipo de amortización definido no es posible");
                            n_cuota = 0;
                            return false;
                        }
                    }
                    else
                    {
                        MensajeConsola("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                        return false;
                    }
                    #endregion
                }
                else if (n_tip_cuota == 3)
                {
                    if (n_tip_amo == 7)
                    {
                        #region gradiente aritmético
                        //--Recalculo de la cuota
                        if (sn_cuota > 1)
                        {
                            if (BOFunciones.Mod(sn_cuota - 1, 12) == 0)
                            {
                                n_nue_cuota = sn_cuota_grad * (1 + n_val_grad / 100);
                                sn_cuota_grad = n_nue_cuota;
                            }
                            else
                            {
                                n_nue_cuota = sn_cuota_grad * n_val_grad / 1200 + sn_cuota_ant;
                            }
                        }
                        else
                        {
                            sn_cuota_grad = n_aux_cuota;
                            n_nue_cuota = n_aux_cuota;
                        }
                        sn_cuota_ant = n_nue_cuota;
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 1)
                        {
                            n_sum_int = BOFunciones.NVL(sn_saldo_cuo * n_tasa_interes, 0);
                            n_sum_int = BOFunciones.Redondeo(n_sum_int);
                            rn_capital = n_nue_cuota - n_sum_int;
                            n_cap_ant = rn_capital;
                        }
                        else
                        {
                            n_cap_ant = 0;
                        }
                        n_sum_int = 0;
                        n_num = 0;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                    rn_interes[n_num] = ((sn_saldo_cuo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                else
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - n_cap_ant);
                                rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 2)
                        {
                            rn_capital = n_nue_cuota - n_sum_int;
                        }
                        #endregion
                    }
                    else if (n_tip_amo == 8)
                    {
                        #region gradiente geometrico
                        //--Recalculo de la cuota
                        n_nue_cuota = sn_saldo_cuo * (n_tasa_interes + 1) * n_val_grad / n_num_cuotas;
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 1)
                        {
                            n_sum_int = BOFunciones.NVL(sn_saldo_cuo * n_tasa_interes, 0);
                            n_sum_int = BOFunciones.Redondeo(n_sum_int);
                            rn_capital = n_nue_cuota - n_sum_int;
                            n_cap_ant = rn_capital;
                        }
                        else
                        {
                            n_cap_ant = 0;
                        }
                        n_sum_int = 0;
                        n_num = 0;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                    rn_interes[n_num] = ((sn_saldo_cuo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                else
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - n_cap_ant);
                                rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 2)
                            rn_capital = n_nue_cuota - n_sum_int;
                        #endregion
                    }
                    else if (n_tip_amo == 9)
                    {
                        #region gradiente escalonado
                        //--Recalculo de la cuota
                        if (sn_cuota > 1)
                        {
                            if (BOFunciones.Mod(sn_cuota - 1, Convert.ToDecimal(12)) == 0)
                                n_nue_cuota = sn_cuota_ant * (1 + n_val_grad);
                            else
                                n_nue_cuota = sn_cuota_ant;
                        }
                        else
                        {
                            n_nue_cuota = n_aux_cuota;
                        }
                        sn_cuota_ant = n_nue_cuota;
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 1)
                        {
                            n_sum_int = BOFunciones.NVL(sn_saldo_cuo * n_tasa_interes, 0);
                            n_sum_int = BOFunciones.Redondeo(n_sum_int);
                            rn_capital = n_nue_cuota - n_sum_int;
                            n_cap_ant = rn_capital;
                        }
                        else
                        {
                            n_cap_ant = 0;
                        }
                        n_sum_int = 0;
                        n_num = 0;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                {
                                    rn_interes[n_num] = ((sn_saldo_cuo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                }
                                else
                                {
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - n_cap_ant);
                                }
                                rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 2)
                            rn_capital = n_nue_cuota - n_sum_int;
                        #endregion
                    }
                    else if (n_tip_amo == 10)
                    {
                        #region otro gradiente           
                        //--Recalculo de la cuota
                        if (sn_cuota > 1)
                            n_nue_cuota = sn_cuota_ant * (1 + n_tasa_interes);
                        else
                            n_nue_cuota = n_aux_cuota;
                        sn_cuota_ant = n_nue_cuota;
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 1)
                        {
                            n_sum_int = BOFunciones.NVL(sn_saldo_cuo * n_tasa_interes, 0);
                            n_sum_int = BOFunciones.Redondeo(n_sum_int);
                            rn_capital = n_nue_cuota - n_sum_int;
                            n_cap_ant = rn_capital;
                        }
                        else
                        {
                            n_cap_ant = 0;
                        }
                        n_sum_int = 0;
                        n_num = 0;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                    rn_interes[n_num] = ((sn_saldo_cuo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                else
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - n_cap_ant);
                                rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 2)
                            rn_capital = n_nue_cuota - n_sum_int;
                        #endregion
                    }
                    else if (n_tip_amo == 11)
                    {
                        #region mas gradientes
                        //--Recalculo de la cuota
                        if (sn_cuota > 1)
                        {
                            if (BOFunciones.Mod(sn_cuota - 1, 12) == 0)
                            {
                                n_nue_cuota = sn_cuota_ant * (1 + n_val_grad);
                                n_nue_cuota = BOFunciones.Redondeo(n_nue_cuota);
                                n_num = 0;
                                while (true)
                                {
                                    if (n_num > n_atr_otr) { break; }
                                    if (n_num <= n_atr_otr)
                                    {
                                        if (atr_otro[n_num].n_cod_atr == n_atrib_gra)
                                        {
                                            atr_otro[n_num].n_valor_calculo = atr_otro[n_num].n_valor_calculo * (1 + n_val_grad);
                                            n_val_temp = atr_otro[n_num].n_valor_calculo;
                                            n_val_temp = BOFunciones.Redondeo(n_val_temp);
                                            atr_otro[n_num].n_valor_calculo = n_val_temp;
                                            n_num = n_num + 1;
                                        }
                                        else
                                        {
                                            n_num = n_num + 1;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                n_nue_cuota = sn_cuota_ant;
                            }
                        }
                        else
                        {
                            n_nue_cuota = n_aux_cuota;
                        }
                        sn_cuota_ant = n_nue_cuota;
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 1)
                        {
                            n_sum_int = BOFunciones.NVL(sn_saldo_cuo * n_tasa_interes, 0);
                            n_sum_int = BOFunciones.Redondeo(n_sum_int);
                            rn_capital = n_nue_cuota - n_sum_int;
                            n_cap_ant = rn_capital;
                        }
                        else
                        {
                            n_cap_ant = 0;
                        }
                        n_sum_int = 0;
                        n_num = 1;
                        while (n_num <= sn_num_atr_fin)
                        {
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                            {
                                if (n_num == sn_num_atr_fin)
                                    rn_interes[n_num] = ((sn_saldo_cuo - n_cap_ant) * n_tasa_interes) - n_sum_int;
                                else
                                    rn_interes[n_num] = sn_tasa_atr_fin[n_num] * (sn_saldo_cuo - n_cap_ant);
                                rn_interes[n_num] = BOFunciones.Redondeo(rn_interes[n_num]);
                            }
                            else
                            {
                                rn_interes[n_num] = 0;
                            }
                            if (rn_interes[n_num] == null)
                                rn_interes[n_num] = 0;
                            n_sum_int = n_sum_int + rn_interes[n_num];
                            n_num = n_num + 1;
                        }
                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                        if (n_tip_pago == 2)
                        {
                            rn_capital = n_nue_cuota - n_sum_int;
                            if (rn_capital < 0)
                            {
                                n_num = 1;
                                while (n_num <= sn_num_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                    {
                                        rn_interes[n_num] = rn_interes[n_num] + rn_capital;
                                        n_sum_int = n_sum_int + BOFunciones.NVL(rn_capital, 0);
                                    }
                                    n_num = n_num + 1;
                                }
                                n_acumulado_int = n_acumulado_int + rn_capital * (-1);
                                n_temp_capital = rn_capital;
                                rn_capital = 0;
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    MensajeConsola("El tipo de liquidación posee errado el tipo de cuota a ser calculado");
                    return false;
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                MensajeConsola("Si maneja gracia " + n_gracia);
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                if (n_gracia == 1 && n_gracia != null)
                {
                    #region plazo de gracia
                    if (sn_cuota_gracia <= n_cuotas_gracia)
                    {
                        if (n_tip_gracia == 4)       //-- Gracia por atributos
                        {
                            #region gracia por atributos
                            if (s_atr_gracia == "2")
                            {
                                rn_capital = 0;
                            }
                            else if (s_atr_gracia == "3")
                            {
                                n_num = 1;
                                while (n_num <= sn_num_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                        rn_interes[n_num] = 0;
                                    n_num = n_num + 1;
                                }
                            }
                            else if (s_atr_gracia == "1")
                            {
                                n_num = 1;
                                while (n_num <= sn_num_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                        rn_interes[n_num] = 0;
                                    rn_capital = 0;
                                    n_num = n_num + 1;
                                }
                            }
                            #endregion
                        }
                        if (n_tip_gracia == 2)         //-- Gracia Muerta
                        {
                            #region gracia muerta
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                rn_interes[n_num] = 0;
                                rn_capital = 0;
                                n_num = n_num + 1;
                            }
                            #endregion
                        }
                        if (n_tip_gracia == 3)          //-- Gracia Capitalizable
                        {
                            #region gracia capitalizable                        
                            n_num = 1;
                            while (n_num <= sn_num_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                {
                                    n_interes_gracia = n_interes_gracia + rn_interes[n_num];
                                    rn_interes[n_num] = 0;
                                    rn_capital = 0;
                                    n_num = n_num + 1;
                                }
                            }
                            #endregion
                        }
                    }
                    if (n_tip_gracia == 3 && sn_cuota_gracia == n_cuotas_gracia)   //-- Gracia capitalizable
                    {
                        sn_saldo = sn_saldo + n_interes_gracia;
                        sn_saldo_cuo = sn_saldo_cuo + n_interes_gracia;
                        n_interes_gracia = 0;
                        n_num_cuot = n_num_cuotas - n_cuotas_gracia;
                        n_amortiza_tf = 0;
                        //--Mirar si maneja terminos fijos para recalcular la cuota
                        if (n_tip_tf != 1)
                            //-- Si no se manejan terminos fijos prorrateados.n_tip_amo != 5
                            if (n_tip_amo != 5)
                                n_amortiza_tf = -SumarCapitalCuotasExtras();
                        //--Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente
                        if (n_tip_cuota == 1)
                        {
                            #region plazo fijo
                            if (ExistenCuotasExtras())
                            {
                                MensajeConsola("No se pueden tener terminos fijos de acuedo al tipo de liquidación (Plazo Fijo)");
                                return false;
                            }
                            if (n_num_cuotas > 1)
                            {
                                MensajeConsola("Un crédito plazo fijo solo puede tener una cuota. Verifique la información");
                                return false;
                            }
                            //--Calculando el número de días total del plazo fijo
                            n_dias_fijo = DApackage.ConPeriodicidadNumDia(n_for_pla, usuario);

                            n_dias_fijo = n_dias_fijo * n_plazo;
                            n_dias_fijo = BOFunciones.Round(n_dias_fijo);
                            //--Tipo de interés:  1:Simple,  2:Compuesto
                            if (n_tip_int == 1)
                            {
                                n_tot_interes = n_tasa_interes * n_dias_fijo * sn_saldo;
                                n_tot_interes = BOFunciones.Redondeo(n_tot_interes);
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        n_val_aux = (atr_finan[n_num].n_tasa_calculo / 100) * n_dias_fijo * sn_saldo;
                                        n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                        atr_finan[n_num].n_valor = n_val_aux;
                                    }
                                    n_num = n_num + 1;
                                }
                            }
                            else if (n_tip_int == 2)
                            {
                                if (n_tip_pago == 1)
                                {
                                    n_tot_interes = n_tasa_interes / (1 - n_tasa_interes);
                                    n_tot_interes = BOFunciones.Power(n_tot_interes + 1, n_dias_fijo) - 1;
                                    n_tot_interes = n_tot_interes / (n_tot_interes + 1) * sn_saldo;
                                }
                                else
                                {
                                    n_tot_interes = (BOFunciones.Power(n_tasa_interes + 1, n_dias_fijo) - 1) * sn_saldo;
                                }
                                n_tot_interes = BOFunciones.Redondeo(n_tot_interes);
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        if (n_tip_pago == 1)
                                        {
                                            n_val_aux = (atr_finan[n_num].n_tasa_calculo / 100) / (1 - atr_finan[n_num].n_tasa_calculo / 100);
                                            n_val_aux = BOFunciones.Power(n_val_aux + 1, n_dias_fijo) - 1;
                                            n_val_aux = n_val_aux / (n_val_aux + 1) * sn_saldo;
                                        }
                                        else
                                        {
                                            n_val_aux = (BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_dias_fijo) - 1) * sn_saldo;
                                        }
                                        n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                        atr_finan[n_num].n_valor = n_val_aux;
                                    }
                                    n_num = n_num + 1;
                                }
                            }
                            else
                            {
                                MensajeConsola("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                                return false;
                            }
                            //--Tipo de interés:  1:Anticipado,  2:Vencido
                            if (n_tip_pago == 1)
                            {
                                n_cuota = sn_saldo;
                                Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, sn_saldo);
                            }
                            else if (n_tip_pago == 2)
                            {
                                n_cuota = sn_saldo + n_tot_interes;
                            }
                            else
                            {
                                MensajeConsola("El tipo de pago o modalidad se encuentra errado, debe ser Vencido o Anticipado");
                                return false;
                            }
                            #endregion
                        }
                        if (n_tip_cuota == 2)
                        {
                            #region serie uniforme
                            //-- Tipo de interés: 1:Simple,  2:Compuesto
                            if (n_tip_int == 1)
                            {
                                #region simple
                                if (n_tip_amo == 1)
                                {
                                    n_tot_interes = sn_saldo * n_tasa_interes;
                                    if (n_num_cuotas <= 0)
                                    {
                                        MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                        return false;
                                    }
                                    n_cuota = (sn_saldo + n_tot_interes) / n_num_cuot;
                                    //--Modalidad de pago:   1:Anticipado,  2:Vencido
                                    if (n_tip_pago == 1)
                                        Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, sn_saldo);
                                }
                                else
                                {
                                    MensajeConsola("El tipo de amortización definido no es posible");
                                    return false;
                                }
                                #endregion
                            }
                            else if (n_tip_int == 2)
                            {
                                //-- Tipo amortización:  2: KF, IV; 3: KV, IF; 4: CV, IV   5: T.F.Prorrateados
                                if (n_tip_amo == 2)
                                {
                                    #region capital fijo interés variable
                                    if (n_num_cuotas <= 0)
                                    {
                                        MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                        return false;
                                    }
                                    else
                                    {
                                        n_tot_interes = 0;
                                        n_num = 1;
                                        while (n_num <= n_atr_fin)
                                        {
                                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                atr_finan[n_num].n_valor = 0;
                                            n_num = n_num + 1;
                                        }
                                        n_cuota = ((sn_saldo + n_amortiza_tf) / n_num_cuot);
                                        //--Modalidad de pago:   1:Anticipado,  2:Vencido
                                        if (n_tip_pago == 1 && n_dias_aju == 0)
                                            Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, sn_saldo + n_amortiza_tf);
                                    }
                                    #endregion
                                }
                                else if (n_tip_amo == 3)
                                {
                                    #region capital variable interés fijo

                                    MensajeConsola("Formula desconocida ?");
                                    n_cuota = 0;
                                    n_tot_interes = 0;
                                    n_num = 1;
                                    while (n_num <= n_atr_fin)
                                    {
                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                            atr_finan[n_num].n_valor = 0;
                                        n_num = n_num + 1;
                                    }
                                    return false;
                                    #endregion
                                }
                                else if (n_tip_amo == 4)
                                {
                                    #region capital variable interés variable
                                    if (n_tasa_interes == 0)
                                    {
                                        n_cuota = (sn_saldo + n_amortiza_tf) / n_num_cuot;
                                        n_tot_interes = 0;
                                        n_num = 1;
                                        while (n_num <= n_atr_fin)
                                        {
                                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                            {
                                                atr_finan[n_num].n_valor = 0;
                                            }
                                            n_num = n_num + 1;
                                        }
                                    }
                                    else
                                    {
                                        //-- Tipo de pago  1:Anticipado, 2:Vencido
                                        if (n_tip_pago == 1)
                                            n_cuota = (1 - BOFunciones.Power(1 - n_tasa_interes, n_num_cuot));
                                        else
                                            n_cuota = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuot) - 1);
                                        if (n_cuota == 0)
                                        {
                                            n_tot_interes = 0;
                                            n_num = 1;
                                            while (n_num <= n_atr_fin)
                                            {
                                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                    atr_finan[n_num].n_valor = 0;
                                                n_num = n_num + 1;
                                            }
                                            MensajeConsola("Los parámetros para el cálculo de la cuota se encuentran inconsistentes, Verificar.");
                                            return false;
                                        }
                                        else
                                        {
                                            //-- Tipo de pago  1:Anticipado, 2:Vencido
                                            if (n_tip_pago == 1)
                                                n_cuota = (n_tasa_interes / n_cuota) * (sn_saldo + n_amortiza_tf);
                                            else
                                                n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuot)) / n_cuota) * (sn_saldo + n_amortiza_tf);
                                            n_tot_interes = (n_cuota * n_num_cuot) - (sn_saldo + n_amortiza_tf);
                                            n_num = 1;
                                            while (n_num <= n_atr_fin)
                                            {
                                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                {
                                                    //-- Tipo de pago  1:Anticipado, 2:Vencido
                                                    if (n_tip_pago == 1)
                                                        n_val_aux = 1 - BOFunciones.Power(1 - (atr_finan[n_num].n_tasa_calculo / 100), n_num_cuot);
                                                    else
                                                        n_val_aux = BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuot) - 1;
                                                    if (n_val_aux == 0)
                                                        n_val_aux = 0;
                                                    else
                                                        //-- Tipo de pago  1:Anticipado, 2:Vencido
                                                        if (n_tip_pago == 1)
                                                        n_val_aux = ((atr_finan[n_num].n_tasa_calculo / 100) / n_val_aux) * (sn_saldo + n_amortiza_tf);
                                                    else
                                                        n_val_aux = (((atr_finan[n_num].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuot)) / n_val_aux) * (sn_saldo + n_amortiza_tf);
                                                    n_val_aux = (n_val_aux * n_num_cuot) - (sn_saldo + n_amortiza_tf);
                                                }
                                                n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                                atr_finan[n_num].n_valor = n_val_aux;
                                            }
                                            n_num = n_num + 1;
                                        }
                                    }
                                    #endregion
                                }
                                else if (n_tip_amo == 5)
                                {
                                    #region prorrateados
                                    n_cuota = (BOFunciones.Power(n_tasa_interes + 1, n_num_cuot) - 1);
                                    if (n_cuota == 0)
                                    {
                                        n_tot_interes = 0;
                                        n_num = 1;
                                        while (n_num <= n_atr_fin)
                                        {
                                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                atr_finan[n_num].n_valor = 0;
                                            n_num = n_num + 1;
                                        }
                                        MensajeConsola("Los parámetros para el cálculo de la cuota se encuentran inconsistentes, Verificar.");
                                        return false;
                                    }
                                    else
                                    {
                                        //-- - ===>>> n_val_tfpro = terminos.Cal_Prorrateados(n_tasa_dia_tf, n_tipo_cal, f_fec_inicio);
                                        n_cuota = ((n_tasa_interes * BOFunciones.Power(n_tasa_interes + 1, n_num_cuot)) / n_cuota);
                                        n_cuota = n_cuota * (sn_saldo - n_val_tfpro);
                                        n_tot_interes = (n_cuota * n_num_cuot) - sn_saldo;
                                        n_num = 1;
                                        while (n_num <= n_atr_fin)
                                        {
                                            if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                            {
                                                n_val_aux = BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas) - 1;
                                                if (n_val_aux == 0)
                                                {
                                                    n_val_aux = 0;
                                                }
                                                else
                                                {
                                                    n_val_aux = (((atr_finan[n_num].n_tasa_calculo / 100) * BOFunciones.Power((atr_finan[n_num].n_tasa_calculo / 100) + 1, n_num_cuotas)) / n_val_aux);
                                                    n_val_aux = n_val_aux * (sn_saldo - n_val_tfpro);
                                                    n_val_aux = (n_val_aux * n_num_cuotas) - sn_saldo;
                                                }
                                                n_val_aux = BOFunciones.Redondeo(n_val_aux);
                                                atr_finan[n_num].n_valor = n_val_aux;
                                            }
                                            n_num = n_num + 1;
                                        }
                                        //--Modalidad de pago:   1:Anticipado,  2:Vencido
                                        if (n_tip_pago == 1)
                                            Int_Anticipado(n_tip_cuota, n_tip_int, n_tip_amo, sn_saldo);
                                    }
                                    #endregion
                                }
                                else
                                {
                                    MensajeConsola("El tipo de amortiazación definido no es posible");
                                    n_cuota = 0;
                                    return false;
                                }
                            }
                            else
                            {
                                MensajeConsola("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                                return false;
                            }
                            #endregion
                        }
                        else if (n_tip_cuota == 3)
                        {
                            #region gradientes
                            //-- Tipo amortizacion:  7: Aumento periodico y Gradiente
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                {
                                    n_val_grad = atr_finan[n_num].n_gradiente;
                                    s_tipo_grad = atr_finan[n_num].s_tip_gra;
                                    break;
                                }
                                n_num = n_num + 1;
                            }
                            if (n_tip_amo == 7)
                            {
                                #region gradiente aritmetico
                                if (n_num_cuotas <= 0)
                                {
                                    MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                    return false;
                                }
                                else
                                {
                                    n_val_grad_per = BOFunciones.CalTasMod(n_val_grad, 1, n_per_gradiente, 2, n_per_gradiente, 2, n_periodic, 2, n_periodic, 0) / 100;
                                    if (n_tasa_interes == n_val_grad / 100)
                                        n_cuota = sn_saldo * (1 + n_tasa_interes) / n_num_cuot;
                                    else
                                        n_cuota = (sn_saldo * (n_val_grad_per - n_tasa_interes)) / ((BOFunciones.Power(((1 + n_val_grad_per) / (1 + n_tasa_interes)), n_num_cuot)) - 1);
                                }
                                #endregion
                            }
                            else if (n_tip_amo == 8)
                            {
                                #region gradiente geometrico
                                if (n_num_cuotas <= 0)
                                {
                                    MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                    return false;
                                }
                                else
                                {
                                    n_cuota = sn_saldo * (n_tasa_interes + 1) * n_val_grad / n_num_cuot;
                                }
                                #endregion
                            }
                            else if (n_tip_amo == 9 || n_tip_amo == 11)
                            {
                                #region otros gradientes    
                                if (n_num_cuotas <= 0)
                                {
                                    MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                    return false;
                                }
                                else
                                {
                                    n_val_grad = n_val_grad / 100;
                                    n_tasa_interes_grad = BOFunciones.CalTasMod(n_tasa_interes * 100, 2, n_periodic, 2, n_periodic, 2, n_per_gradiente, 2, n_per_gradiente, 0) / 100;
                                    n_per_ano = DApackage.ConPeriodicidadPerAnu(n_periodic, usuario);

                                    n_fac_grad = (sn_saldo * (n_val_grad - n_tasa_interes_grad)) / (BOFunciones.Power(1 + n_val_grad, n_num_cuot / n_per_ano) * BOFunciones.Power(1 + n_tasa_interes_grad, -n_num_cuot / n_per_ano) - 1);
                                    n_cuota = n_fac_grad / (n_tasa_interes_grad / n_tasa_interes);
                                }
                                #endregion
                            }
                            else if (n_tip_amo == 10)
                            {
                                #region gradientes
                                if (n_num_cuotas <= 0)
                                {
                                    MensajeConsola("El número de cuotas no puede ser cero o negativo");
                                    return false;
                                }
                                else
                                {
                                    n_cuota = sn_saldo * (1 + n_tasa_interes) / n_num_cuot;
                                }
                                #endregion
                            }
                            #endregion
                        }
                        else
                        {
                            MensajeConsola("El tipo de liquidación posee errado el tipo de cuota a ser calculado");
                            return false;
                        }
                        n_interes = BOFunciones.Redondeo(n_interes);
                        n_cuota = BOFunciones.Redondeo(n_cuota);
                    }
                    #endregion
                }
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Calculando valores financiados individuales del crèdito
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                n_num = 1;
                n_pos = 1;
                while (n_num <= n_atr_otr)
                {
                    #region valores financiados
                    if (atr_otro[n_num].n_signo == 3 || atr_otro[n_num].n_signo == 4 || atr_otro[n_num].n_signo == 5 || atr_otro[n_num].n_signo == 6)
                    {
                        if (!(atr_otro[n_num].n_signo == 6 && rn_num_cuota != 1))
                        {
                            #region atributos financiados
                            if (n_num_cuotas != 0 || n_num_cuotas == null)
                            {
                                #region reestructurados
                                if (sb_existe_reestr && atr_otro[n_num].n_cod_atr == BOFunciones.To_Number(s_atr))
                                {
                                    if (sn_val_atr_rees > 0)
                                    {
                                        if (sn_val_atr_rees >= rn_capital)
                                        {
                                            rn_interes[sn_num_atr_fin + n_pos] = rn_capital;
                                            sn_val_atr_rees = sn_val_atr_rees - rn_capital;
                                            rn_capital = 0;
                                        }
                                        else
                                        {
                                            rn_interes[sn_num_atr_fin + n_pos] = sn_val_atr_rees;
                                            rn_capital = rn_capital - sn_val_atr_rees;
                                            sn_val_atr_rees = 0;
                                        }
                                    }
                                    else
                                    {
                                        rn_interes[sn_num_atr_fin + n_pos] = 0;
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                #region --Calcular el valor para tipos de liquidación 13, 6 o 17)  
                                if ((atr_otro[n_num].n_tip_des == 2 || atr_otro[n_num].n_tip_des == 3) && (atr_otro[n_num].n_tip_liq == 13 || atr_otro[n_num].n_tip_liq == 6 || atr_otro[n_num].n_tip_liq == 17))
                                {
                                    n_cont = 1;
                                    while (n_cont <= n_atr_fin)
                                    {
                                        if (atr_finan[n_cont].n_cod_atr == n_atr_corr)
                                        {
                                            n_val_interes = atr_finan[n_cont].n_tasa_calculo;
                                            break;
                                        }
                                        n_cont = n_cont + 1;
                                    }
                                    atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_signoatr, sn_saldo, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                    if (!bResultado)
                                        return false;
                                    if (n_tip_cuota == 1)
                                        atr_otro[n_num].n_valor_calculo = atr_otro[n_num].n_valor_calculo / sn_dias * n_dias_fijo;
                                }
                                #endregion
                                #region --Cargar el valor del atributo
                                if (atr_otro[n_num].n_signo == 3)
                                {
                                    if (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0)
                                    {
                                        if (sn_cuota <= atr_otro[n_num].n_num_cuotas)
                                            rn_interes[sn_num_atr_fin + n_pos] = atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas;
                                        else
                                            rn_interes[sn_num_atr_fin + n_pos] = 0;
                                    }
                                    else
                                    {
                                        rn_interes[sn_num_atr_fin + n_pos] = atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                    }
                                    //--Determinar el valor a pagar
                                    if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme && g_b_yaprimero)
                                    {
                                        if (rn_num_cuota <= atr_otro[n_num].n_valor_pagos.Length && n_num <= atr_otro.Length && sn_num_atr_fin + n_pos <= rn_interes.Length)
                                        {
                                            rn_interes[sn_num_atr_fin + n_pos] = atr_otro[n_num].n_valor_pagos[Convert.ToInt32(rn_num_cuota)];
                                        }
                                        else
                                        {
                                            rn_interes[sn_num_atr_fin + n_pos] = atr_otro[n_num].n_valor_calculo;
                                        }
                                    }
                                }
                                else
                                {
                                    rn_interes[sn_num_atr_fin + n_pos] = atr_otro[n_num].n_valor_calculo;
                                    rn_interes[sn_num_atr_fin + n_pos] = BOFunciones.Redondeo(rn_interes[sn_num_atr_fin + n_pos]);
                                }
                                #endregion
                            }
                            n_pos = n_pos + 1;
                            n_num = n_num + 1;
                            #endregion
                        }
                    }
                    #endregion
                }
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Validar ultima cuota
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region validar última cuota
                if (sn_cuota == n_num_cuotas)
                {
                    if (sn_pos_tf <= 0)
                    {
                        rn_capital = sn_saldo;
                    }
                    else
                    {
                        rn_capital = sn_saldo_cuo;
                    }
                }
                sn_saldo = sn_saldo - rn_capital;
                if (sn_saldo_cuo > rn_capital)
                {
                    sn_saldo_cuo = sn_saldo_cuo - rn_capital;
                }
                else
                {
                    rn_capital = sn_saldo_cuo;
                    sn_saldo_cuo = 0;
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Validar saldo negativo
                //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region validar saldo negativo
                if (sn_saldo <= 0)
                {
                    rn_capital = rn_capital + sn_saldo;
                    sn_saldo = 0;
                }
                #endregion
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //--Validar información negativa
                //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #region validar información negativa
                n_num = 1;
                while (n_num <= sn_num_atr_fin)
                {
                    if (rn_interes[n_num] < 0)
                        rn_interes[n_num] = 0;
                    n_num = n_num + 1;
                }
                #endregion
                return true;
                #endregion
            }
            else
            {
                MensajeConsola("Debe primero calcular la información de la liquidación");
                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- FUNCION PARA LEER RESULTADOS DE LA LIQUIDACION  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public bool Leer_Datos(ref decimal? rn_cuota, ref decimal? rn_tasa_interes, ref decimal? rn_num_cuotas, ref decimal? rn_adi_monto, ref decimal? rn_des_cheque, ref DateTime? rf_fecha_ini, ref decimal? rn_dias_ajuste)
        {
            if (b_calculo)
            {
                rn_cuota = n_cuota;
                if (n_tip_cuota == 1)
                    rn_tasa_interes = BOFunciones.Power(n_tasa_interes + 1, n_dias_fijo) - 1;
                else
                    rn_tasa_interes = n_tasa_interes;
                rn_num_cuotas = n_num_cuotas;
                rn_adi_monto = n_adi_monto;
                rn_des_cheque = n_des_cheque;
                rf_fecha_ini = f_fec_inicio;
                rn_dias_ajuste = n_dias_aju;
                return true;
            }
            else
            {
                rn_cuota = 0;
                rn_tasa_interes = 0;
                rn_num_cuotas = 0;
                rn_adi_monto = 0;
                rn_des_cheque = 0;
                rf_fecha_ini = null;
                rn_dias_ajuste = n_dias_aju;
                MensajeConsola("Debe primero calcular la información de la liquidación");
                return false;
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- AMORTIZACION DE CREDITOS.El tipo de pago es 1 = Por Valor, 2 = A Fecha, 3 = Número de Cuotas, 4 = Total, 5 = Causacion(Por cuotas)
        //-- 6 = Por valor - capital, 7=Reporte de Riesgo de Liquidez  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public int? Amortizar(int n_tipo_pago, decimal? n_valor_pago, DateTime? pf_fecha_pago, int? n_cuotas_a_pag, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr)
        {
            string sLinea = "";
            int? n_pos_corr;
            int? n_pos_mora;
            int? n_pos_reg_amortiza;
            decimal? n_valor_retorno;
            bool b_interes_dias;
            int? n_pos_reg_mora;
            bool b_exis_cal;
            int n_num;
            int? n_calendario;
            int n_dias_ven;
            decimal? n_otr = null;
            decimal? n_val_capitaliza = null;
            int n_error = 0;
            int n_tipo_pago_temp;
            decimal? n_mor_sig = 0;
            bool b_plazo_fijo;
            int n_dias_gracia = 0;
            DateTime? f_prox_pag_temp = null;
            decimal? n_capitaliza_ctrr;
            bool b_reestruc_atr;
            DateTime? f_ultcie = null;
            bool b_maneja_habil;
            DateTime? f_fec_habil;
            int? n_ano_pago = null;
            int? n_mes_pago = null;
            int? n_dia_pago = null;
            string s_no_cruza;
            int n_num_cruza = 0;
            decimal? n_tot_tf = null;
            decimal? n_sal_tf = null;
            decimal? n_monto_cuo = null;
            decimal? n_saldo_cuo = null;
            int?[] n_dias_periodos;
            int?[] n_no_cruza = new int?[100];
            int? n_dias = null;
            string g_sentencia;
            string s_tipo_prio;
            string s_dias_mora_saldo;
            string[] s_aux = new string[100];
            decimal?[] n_tasa_original = new decimal?[100];
            bool b_ajusto_cuota;
            bool b_control_atr;
            decimal? n_aux_pago;
            bool b_categ = false;
            int sn_cuotas_adm = 0;
            bool b_control_seg;
            int n_num_cuo_act;
            decimal? n_saldo_act;
            decimal? n_excedente;
            int n_max_iteraciones;
            DateTime? f_fecha_proximo = null;
            int n_num_cuo_pag_aux;
            int n_num_cuo_pag;
            int n_cuo_pag_rep;
            decimal? n_valcueord;
            bool b_int_prop;
            bool b_exis_mora;
            decimal? n_tasa_mora = null;
            int n_verifica;
            int n_mes_causa;
            int n_ano_causa;
            bool b_causa;
            int n_cont;
            DateTime? f_fec_ini_aux = null;
            int n_dias_mora_cuotas;
            bool b_pendientes;
            bool b_ya_causo;
            DateTime? f_fec_ant = null;
            DateTime? f_fec_cuota_rep = null;
            DateTime? f_fec_ant1 = null;
            int? n_mes_ant1 = null;
            int? n_ano_ant1 = null;
            int n_dias_prop;
            DateTime? f_fec_act = null;
            DateTime? f_fec_act1 = null;
            decimal? n_capital = null;
            int n_dias_apro = 0;
            decimal? n_interes = null;
            decimal? n_otros = null;
            decimal? n_cuota_original = null;
            decimal? n_val_aux = null;
            string s_usura;
            bool b_existe;
            decimal? n_usura;
            decimal? n_tasa_usura;
            int? n_tipo_tasa = null;
            string s_efe_nom = null;
            int? n_per = null;
            decimal? n_tasa_calculo;
            string s_mod = null;
            decimal? n_int_prop;
            int? n_mod_per = null;
            decimal? n_numero = null;
            decimal? n_sum_tf;
            decimal?[] n_atributos = new decimal?[100];
            decimal? n_intcuototaux;
            bool b_ya_cob_mora;
            int n_for_pag_real;
            DateTime? f_fecha_pago_real;
            int n_pos;
            DateTime? f_prox_pag_real;
            DateTime? f_fecha_cambio = null;
            int n_num_mora;
            string s_val_ant;
            int n_dias_tf_nomina;
            DateTime? rf_f_prox_pago_real;
            string s_val_nue = string.Empty;
            DateTime? f_mora;
            int n_dias_mora;
            int n_dias_mora_lleva;
            DateTime? f_fec_prox;
            int n_dias_mora_temp;
            int n_contador_;
            DateTime? f_fec_ini = null;
            DateTime? f_fec_fin = null;
            int? n_dias_dif;
            decimal? n_val_ant;
            decimal? n_valor_atr = null;
            int? n_atr_depende;
            DateTime? f_fecha_pago;
            int? n_cod_atr_dep = null;
            int? n_val_aux1 = null;
            bool bResultado = false;
            bool b_existe_cat = false;
            bool b_exis_acelerado;
            bool b_existe1;
            decimal? n_val_mora;
            int? n_dias_insoluto;
            decimal? n_tasa_interes_mora;
            decimal? n_val_mor_aux;
            decimal? n_cap_ant;
            decimal? n_tot_interes_aux;
            DateTime? f_fec_pripag;
            int? n_dias_mes;
            decimal? n_factor;
            int? n_dia_des;
            decimal? n_val_temp;
            int? n_veces;
            int? n_num_temp;
            decimal? n_num_gra;
            bool b_exis_cap;
            decimal? n_saldo_actual;
            int? n_dias_cte;
            decimal? n_valor_seg;
            int n_cant_cod;
            int? n_per_dia;
            int n_per_diaria;
            decimal? n_tasa_interes_diaria;
            decimal? n_val_int_tmp;
            int n_pos_cod;
            int n_nume;
            string[] s_vector_atr = new string[100];
            DateTime? f_fecha_pago1 = null;
            decimal? n_capital_aux = null;
            decimal? n_interes_aux;
            decimal?[] n_atributos_aux = new decimal?[100];
            bool b_calcula_pend;
            int? n_cod_atr = null;
            int? n_atributo;
            decimal? n_restante;
            decimal?[] n_tasa_int = new decimal?[100];
            bool b_reporte_edades = false;
            int? n_dias_atraso;
            bool b_exis_suspendido = false;
            decimal? n_valor_seg_aux = null;
            int? n_per_pago_seg = null;
            int? n_dias_seg;
            decimal? n_dif_red;
            decimal? n_cuotas_atraso;
            decimal? n_ind_seg;
            int? n_dias_tot_atraso;
            DateTime? f_fecha_pago_aux;
            string s_atr_no_dev;
            int? n_num_no_dev;
            int?[] n_atr_no_dev = new int?[100];
            int n_periodos_anuales;
            bool b_prejuridico;
            bool b_yagenero_prejuridico;
            DateTime? f_fecha_ult_cierre;
            int? n_dias_mora_cierre;
            decimal? n_valor_prejuridico;
            decimal? n_valor_prejuridico_pagado;
            DateTime? fecha_pendiente;
            decimal? n_formaCatg = null;

            MensajeConsola("Inicializando variables");
            b_prejuridico = false;
            b_exis_acelerado = false;
            f_fecha_pago = pf_fecha_pago;
            n_pos_corr = null;
            n_pos_mora = null;
            n_pos_reg_amortiza = null;
            n_pos_reg_mora = null;
            n_valor_retorno = null;
            sn_atr_adm = BOFunciones.To_Number(BOFunciones.BuscarGeneral(1515, 2));
            //--Validando el estado del crèdito
            if (s_estado_cre != "C")
            {
                if (!(n_saldo == 0 && (n_tipo_pago == 5 || n_tipo_pago == 7)))
                {
                    MensajeConsola("El crèdito no se encuentra activo o tiene saldo cero " + n_radic);
                    return -1;
                };
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            //--Determina si se calcula interes corriente por días
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region interes por dias
            MensajeConsola("Determinando tipo de calculo de interés corriente por días");
            if (BOFunciones.BuscarGeneral(1370, 2) == "1")
            {
                b_interes_dias = true;
            }
            else
            {
                b_interes_dias = false;
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Valida si existe el parametro 1670 y si esta en 1 para manejar dias calendario  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region dias calendario
            MensajeConsola("Determina el tipo de calendario para el cálculo de la mora");
            n_calendario = BOFunciones.To_Number(BOFunciones.BuscarGeneral(1670, 2));
            if (BOFunciones.BuscarGeneral(1670, 2) != "")
            {
                b_exis_cal = true;
            }
            else
            {
                b_exis_cal = false;
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Determina cobro prejuridico
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region determina cobro prejuridico
            MensajeConsola("Valida si se calcula prejuridico por días de mora");
            n_dias_ven = Convert.ToInt32(BOFunciones.Trunc(f_fecha_pago)) - Convert.ToInt32(BOFunciones.Trunc(f_prox_pag));

            if (DApackage.ConsultaCobroPrejuridico(n_dias_ven, ref n_formaCatg, ref n_valorCatg, usuario)) // pBasedato % Found)
            {
                if (n_formaCatg == 0)
                {
                    n_otr = n_valorCatg / 100;
                    n_atr_otr = n_atr_otr + 1;
                    atr_otro[n_atr_otr].SetAtrOtr(n_atr_JURID, 3, n_otr, 8, 4, 0, null);
                    atr_otro[n_atr_otr].n_valor_calculo = n_otr;
                }
                else
                {
                    n_otr = n_valor_pago * n_valorCatg / 100;
                    n_otr = BOFunciones.Redondeo(n_otr);
                    n_atr_otr = n_atr_otr + 1;
                    atr_otro[n_atr_otr].SetAtrOtr(n_atr_JURID, 1, n_otr, 8, 4, 0, null);
                    atr_otro[n_atr_otr].n_valor_calculo = n_otr;
                };
            };
            //Close pBasedato;
            //--Inicializa
            n_val_capitaliza = 0;
            n_error = 0;
            n_tipo_pago_temp = 0;
            n_mor_sig = 0;
            b_plazo_fijo = false;
            if (n_tipo_pago < 1 || n_tipo_pago > 7)
            {
                n_error = -2;
                return n_error;
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Si tiene período de gracia toma la fecha de próximo pago  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region periodo de gracia
            MensajeConsola("Determinando periodo de gracia");
            if (n_gracia == 1)
            {
                if (n_cuo_pag == 0)
                {
                    n_dias_gracia = Convert.ToInt32(DApackage.ConPeriodicidadNumDia(n_periodic_gracia, usuario));
                    n_dias_gracia = n_dias_gracia * Convert.ToInt32(n_duracion_gracia);
                    f_prox_pag_temp = BOFunciones.FecSumDia(f_prox_pag, n_dias_gracia, 1);
                    if (n_tip_gracia == 3)
                    {
                        f_prox_pag = f_prox_pag_temp;
                    }
                    else if (n_tip_gracia == 2)
                    {
                        f_prox_pag = f_prox_pag_temp;
                    };
                };
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Manejo de otros tipos de pago  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region otros tipos pago
            if (n_tipo_pago == 6)
            {
                n_tipo_pago_temp = n_tipo_pago;
                n_tipo_pago = 1;
                n_capitaliza_ctrr = 1;
            };
            if (n_tipo_pago == 7)
            {
                n_tipo_pago_temp = n_tipo_pago;
                n_tipo_pago = 3;
            };
            if (b_interes_dias)
            {
                if (n_tipo_pago == 2)
                {
                    n_tipo_pago_temp = n_tipo_pago;
                    n_tipo_pago = 4;
                };
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Determina si se manejan parametro adicional para reestructuracion  
            // ----------------------------------------------------------------------------------,--------------------------------------------------------------------------------------------------------------------------------------------------  
            #region parametro reestructuracion
            MensajeConsola("Determinando si es línea re-estructurada");

            if (!DApackage.LineaRestructurada(s_cod_credi, ref sLinea, usuario)) //pBasedato % NOTFOUND)
            {
                b_reestruc_atr = false;
            }
            else
            {
                b_reestruc_atr = true;
            };
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Ajusta el saldo del credito de acuerdo al parametro de capital para el tipo de pago 5 (Causacion)  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_tipo_pago == 5 || n_tipo_pago_temp == 7)
            {
                MensajeConsola("Determinar datos a fecha de cierre");

                f_ultcie = DApackage.DatosFechaCierre(n_radic, f_fecha_pago, usuario);
                if (f_ultcie == null)
                {
                    return -1;
                }
                else
                {
                    DApackage.Proximo_Pago(n_radic, f_ultcie, ref f_prox_pag, ref n_saldo, ref f_ult_pago, usuario);
                };
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Determina si se manejan festivos y calcula el proximo dia habil  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region dia habil
            MensajeConsola("Determinar si se maneja día hábil");

            if (BOFunciones.BuscarGeneral(902, 2) == "1")
            {
                b_maneja_habil = true;
            }
            else
            {
                b_maneja_habil = false;
            };
            f_fec_habil = f_prox_pag;
            if (b_maneja_habil)
            {
                while (true)
                {
                    n_ano_pago = BOFunciones.DateYear(f_fec_habil);
                    n_mes_pago = BOFunciones.DateMonth(f_fec_habil);
                    n_dia_pago = BOFunciones.DateDay(f_fec_habil);

                    bool encontradoDiasHabiles = DApackage.Dias_Habiles(n_mes_pago, n_ano_pago, n_dia_pago, ref sLinea, usuario);

                    if (encontradoDiasHabiles)
                    {
                        b_maneja_habil = true;
                    }
                    else
                    {
                        b_maneja_habil = false;
                    };

                    if (b_maneja_habil == true)
                    {
                        f_fec_habil = BOFunciones.FecSumDia(f_fec_habil, 1, 2);
                    }
                    else
                    {
                        break;
                    };
                };
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Determina los atributos a no cruzar en el cruce de cuentas  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region atributos a cruzar
            MensajeConsola("Determinando parametros que se cruzan");

            if (g_b_cruce)
            {
                s_no_cruza = BOFunciones.BuscarGeneral(360, 1);
                n_num_cruza = Convert.ToInt32(BOFunciones.StrTokenize(s_no_cruza, ",", ref s_aux));
                n_num = 1;
                while (n_num <= n_num_cruza)
                {
                    n_no_cruza[n_num] = BOFunciones.To_Number(s_aux[n_num]);
                    n_num = n_num + 1;
                };
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // ---- Determina el valor de terminos fijos a descontar del monto del crédito  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region terminos fijos
            MensajeConsola("Determinando el valor de los terminos fijos");

            if (bterminos)
            {
                n_tot_tf = SumarCapitalCuotasExtras();
                n_sal_tf = SumarSaldoCapitalCuotasExtras(true, Convert.ToDateTime(f_fecha_pago));
                if (n_tip_tf != 4)
                {
                    n_monto_cuo = n_monto - n_tot_tf;
                    n_saldo_cuo = n_saldo - n_sal_tf;
                }
                else
                {
                    n_monto_cuo = n_monto - n_tot_tf;
                    n_saldo_cuo = n_saldo;
                };
            }
            else
            {
                n_monto_cuo = n_monto - n_tot_tf;
                n_saldo_cuo = n_saldo;
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // ---- Determina el número de días según la periodicidad  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region dias periodicidad
            MensajeConsola("Determinando número de días de la periodicidad");

            if (n_tip_cuota == 1)
            {
                n_dias = Convert.ToInt32(DApackage.ConPeriodicidadNumDia(n_for_pla, usuario));
                n_dias = n_dias * Convert.ToInt32(n_plazo);
            }
            else
            {
                n_dias = Convert.ToInt32(DApackage.ConPeriodicidadNumDia(n_periodic, usuario));
            };
            // -- Cargar la variable de dias del periodo para efectos de calculo del seguro en los atributos descontados
            p_n_num_dias_per = n_dias;

            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Calculo de la periodicidad diaria para el tipo de calendario  
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            MensajeConsola("Determinando la periodicidad diaria");
            n_per_dia = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
            if (n_per_dia == null || n_per_dia < 0)
            {
                MensajeConsola("No se encontro periodicidad diaria para el tipo de calendario: " + n_tipo_cal + ". Verifique periodicidades..." + n_per_dia);
                return -1;
            };
            //----Determina el tipo de prioridad
            s_tipo_prio = BOFunciones.BuscarGeneral(40, 2);
            s_tipo_prio = "2";
            //----Determina si el interés de mora es sobre atributos(cuota)o sobre saldo insoluto
            s_dias_mora_saldo = BOFunciones.BuscarGeneral(410, 2);
            #endregion

            // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // ----Trae interes de atributos financiados y determina la posición del atributo de mora  
            // --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region atributos financiados
            MensajeConsola("Trae los atributos de interés de mora y corriente");
            n_pos_mora = -1;
            n_num = 1;
            while (n_num <= n_atr_fin && n_num <= atr_finan.Length)
            {
                rn_tot_atributos[n_num] = 0;
                rn_cod_atributos[n_num] = atr_finan[n_num].n_cod_atr;
                if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                {
                    n_pos_mora = n_num;
                };
                if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                {
                    n_pos_corr = n_num;
                };
                n_tasa_original[n_num] = atr_finan[n_num].n_tasa_calculo;
                n_num = n_num + 1;
            };
            rn_num_atr = n_atr_fin;
            #endregion

            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // ---- Incluyendo descuentos financiados de forma individual  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region descuentos financiados
            MensajeConsola("Determinando valor de atributos descontados");
            b_ajusto_cuota = false;
            b_control_seg = false;
            b_control_atr = false;
            n_aux_pago = 0;
            n_num = 1;
            while (n_num <= n_atr_otr && n_num <= atr_otro.Length)
            {
                //--Financiado individual
                if (atr_otro[n_num].n_signo == 3)
                {
                    rn_num_atr = rn_num_atr + 1;
                    if (b_categ && atr_otro[n_num].n_tip_liq == 8)
                    {
                        rn_tot_atributos[rn_num_atr] = n_otr;
                        rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                    }
                    else
                    {
                        rn_tot_atributos[rn_num_atr] = 0;
                        rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                    };
                    //--Adicionado para atributos descontados
                    if (atr_otro[n_num].n_cod_atr == sn_atr_adm)
                    {
                        n_cuota = n_cuota - ((atr_otro[n_num].n_valor_calculo / n_num_cuotas) / sn_cuotas_adm);
                        n_cuota = BOFunciones.Redondeo(n_cuota);
                    }
                    else
                    {
                        if (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas != 0)
                        {
                            if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme)
                            {
                                n_aux_pago = 0;
                            }
                            else
                            {
                                n_aux_pago = n_aux_pago + BOFunciones.NVL(atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas, 0);
                                n_aux_pago = BOFunciones.Redondeo(n_aux_pago);
                            };
                        }
                        else
                        {
                            if (n_num_cuotas != 0)
                            {
                                n_aux_pago = n_aux_pago + BOFunciones.NVL(atr_otro[n_num].n_valor_calculo / n_num_cuotas, 0);
                            }
                            else
                            {
                                n_aux_pago = 0;
                            };
                            n_aux_pago = BOFunciones.Redondeo(n_aux_pago);
                        };
                    };
                };
                //--Financiado individual excluido
                if (atr_otro[n_num].n_signo == 4)
                {
                    rn_num_atr = rn_num_atr + 1;
                    rn_tot_atributos[rn_num_atr] = 0;
                    rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                };
                //--Adicional a la cuota
                if (atr_otro[n_num].n_signo == 5)
                {
                    rn_num_atr = rn_num_atr + 1;
                    rn_tot_atributos[rn_num_atr] = 0;
                    rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                };
                //--Pago en la primera cuota
                if (atr_otro[n_num].n_signo == 6)
                {
                    rn_num_atr = rn_num_atr + 1;
                    rn_tot_atributos[rn_num_atr] = 0;
                    rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                };
                n_num = n_num + 1;
            };
            n_aux_pago = BOFunciones.Redondeo(n_aux_pago);
            n_cuota = n_cuota - BOFunciones.NVL(n_aux_pago, 0);
            n_cuota_original = n_cuota;
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // Mensaje("Inicializando variables de calculos de pagos");  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region inicializando variables
            n_num = 1;
            while (n_num <= rn_num_atr)
            {
                rn_tot_atributos[n_num] = 0;
                n_num = n_num + 1;
            };
            rn_tot_capital = 0;
            n_num_cuo_act = Convert.ToInt32(n_cuo_pag);
            n_saldo_act = n_saldo_cuo;
            n_excedente = n_valor_pago;
            n_max_iteraciones = 0;
            rf_f_prox_pago = f_prox_pag;
            f_fecha_proximo = f_prox_pag;
            n_num_cuo_pag = 0;
            n_cuo_pag_rep = Convert.ToInt32(n_cuo_pag);
            n_num_cuo_pag_aux = 0;
            n_num_par = 1;
            n_valcueord = 0;
            b_int_prop = false;
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // Mensaje("Se determina si se cobra mora por nómina");  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region cobra mora por nomina
            if (n_pos_mora != -1)
            {
                if (n_tip_cuota == 1)
                {
                    if (BOFunciones.BuscarGeneral(1050, 2) == "1" || BOFunciones.BuscarGeneral(1050, 2) == "2")
                    {
                        b_exis_mora = true;
                    }
                    else
                    {
                        b_exis_mora = false;
                    };
                }
                else
                {
                    if (BOFunciones.BuscarGeneral(1040, 2) == "1")
                    {
                        b_exis_mora = true;
                    }
                    else
                    {
                        b_exis_mora = false;
                    };
                };
                if (n_for_pag == 2 && b_exis_mora)
                {
                    n_tasa_mora = 0;
                }
                else
                {
                    atr_finan[Convert.ToInt32(n_pos_mora)].Conv_Tasa(n_per_dia, 2, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                };
            }
            else
            {
                n_tasa_mora = 0;
                b_exis_mora = false;
            };
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // ---- Si el pago es por fecha verifica si esta al día  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region verifica si esta al día
            if (n_tipo_pago == 2 && n_tipo_pago_temp != 6)
            {
                if (rf_f_prox_pago > f_fecha_pago)
                {
                    return 0;
                };
            };
            n_verifica = 0;
            #endregion
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Consulta el tipo de clasificación de la línea si es 1 ó no existe = Consumo, 2=Comercial, 3=Vivienda, 4=Microcredito  
            // ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            n_clasificacion = BOFunciones.To_Number(Convert.ToString(n_cod_clasifica));
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Determina si esta en cobro pre-juridico  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region cobro pre-juridico
            n_dias_mora_cierre = 0;
            f_fecha_ult_cierre = null;

            DApackage.DeterminaCobroPrejuridico(ref f_fecha_ult_cierre, usuario);

            if (f_fecha_ult_cierre != null)
            {
                n_dias_mora_cierre = DApackage.DiasMora(f_fecha_ult_cierre, n_radic, usuario);
                if (n_dias_mora_cierre != null)
                {
                    if ((n_dias_mora_cierre >= Convert.ToInt32(BOFunciones.BuscarGeneral(1915, 2)) && n_dias_mora_cierre <= Convert.ToInt32(BOFunciones.BuscarGeneral(1916, 2))))
                    {
                        b_prejuridico = true;
                    };
                };
                n_dias_mora_cierre = Convert.ToInt32(BOFunciones.NVL(Convert.ToInt32(n_dias_mora_cierre), 0));
            };
            #endregion

            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            // -- Se define el tipo de tasa de usura para la clasificación de la linea de crédito  
            // ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            #region tipo tasa usura
            bool encontrado = DApackage.TipoHistorico(n_clasificacion, ref n_tipo_tasa_usura, usuario);

            if (!encontrado || n_tipo_tasa_usura == null)
            {
                MensajeConsola("No se ha definido el tipo de tasa de usura para el tipo de clasificación de la linea de credito. Verifique Parametrización de la Clasificación");
                return -1;
            };

            b_int_prop = false;
            n_mes_causa = Convert.ToInt32(BOFunciones.DateMonth(f_fecha_pago));
            n_ano_causa = Convert.ToInt32(BOFunciones.DateYear(f_fecha_pago));
            #endregion
            // ----------------------------------------------------------------------------------------------------------------------  
            // -- Ciclo que limpia el arreglo de detalle de los pagos
            #region limpiar datos
            b_causa = false;
            n_cont = 0;
            f_fec_ini_aux = f_fec_inicio;
            // ----------------------------------------------------------------------------------------------------------------------  
            // -- Carga la Información de las Cuotas ya Calculadas
            n_cont_rep = 1;
            n_cont_mora = 1;
            n_cont_amortiza = 1;
            n_dias_mora_cuotas = 0;
            MensajeConsola("Cargando valores pendientes y moras del crédito");
            //--Verificar la fecha a partir de la cual hay saldos pendientes
            if (n_tipo_pago != 5)
            {
                fecha_pendiente = null;
                bool encontradoSaldosPendientes = DApackage.FechaSaldosPend(n_radic, rf_f_prox_pago, ref fecha_pendiente, usuario);

                if (encontradoSaldosPendientes)
                {
                    if (fecha_pendiente != null)
                    {
                        if (fecha_pendiente > rf_f_prox_pago)
                        {
                            rf_f_prox_pago = fecha_pendiente;
                        };
                    };
                };
            };
            #endregion
            Cargar_Amortiza(rf_f_prox_pago, f_fecha_pago, n_tipo_pago);
            Cargar_Mora(rf_f_prox_pago, f_fecha_pago, n_tipo_pago);
            // ----------------------------------------------------------------------------------------------------------------------  
            // ----------------------------------------------------------------------------------------------------------------------  
            // -- Ciclo de calculo de los valores de las cuotas  
            // ----------------------------------------------------------------------------------------------------------------------  
            // ----------------------------------------------------------------------------------------------------------------------  
            #region ciclo de calculo
            while (true)
            {
                MensajeConsola("Inicializa la variable para que en caso de que existan pendientes no realice distribución " + Convert.ToDateTime(rf_f_prox_pago).ToString());
                b_pendientes = false;
                b_ya_causo = false;
                #region  validacion del saldo de capital del crédito
                if (n_saldo_act <= 0)
                {
                    if (n_tipo_pago == 4 || n_tipo_pago == 5)
                    {
                        if (n_tipo_pago != 5)
                        {
                            if (n_tip_tf != 4)
                            {
                                rn_tot_capital = rn_tot_capital + n_saldo_act + n_sal_tf;
                            }
                            else
                            {
                                rn_tot_capital = rn_tot_capital + n_saldo_act;
                            };
                        };
                    };
                    if (n_num_cuo_pag_aux != 0)
                    {
                        break;
                    };
                };
                n_num = 1;
                while (n_num <= rn_num_atr)
                {
                    n_atributos[n_num] = 0;
                    n_num = n_num + 1;
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Determinar la fecha de inicio del período que se esta calculando " || to_char(rf_f_prox_pago, "MM/DD/YYYY"));  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region determina fecha inicio
                f_fec_ant = BOFunciones.FecResDia(rf_f_prox_pago, n_dias, Convert.ToInt32(n_tipo_cal));
                if ((((n_tipo_pago == 2 && n_tipo_pago_temp == 6) || n_tipo_pago == 4) && (BOFunciones.BuscarGeneral(1500, 1) == "1" || BOFunciones.BuscarGeneral(1500, 1) == "2") && n_cuo_pag == 0) && n_tip_intant == 3)
                {
                    f_fec_ant = f_fec_apro;
                };
                if (n_tipo_pago == 5 && n_tip_intant == 3 && n_cuo_pag == 0 && n_num_cuo_pag == 0)
                {
                    f_fec_ant = f_fec_apro;
                };
                if (BOFunciones.DateMonth(rf_f_prox_pago) == 2 && BOFunciones.DateDay(rf_f_prox_pago) >= 28 && BOFunciones.DateMonth(f_fec_inicio) != 2 && (BOFunciones.DateDay(f_fec_inicio) == 28 || BOFunciones.DateDay(f_fec_inicio) == 29))
                {
                    if (n_dias == 30 || n_dias == 90 || n_dias == 180 || n_dias == 360)
                    {
                        f_fec_ant = BOFunciones.DateConstruct(BOFunciones.DateYear(f_fec_ant), BOFunciones.DateMonth(f_fec_ant), BOFunciones.DateDay(rf_f_prox_pago), 0, 0, 0);
                    };
                };
                f_fec_cuota_rep = rf_f_prox_pago;
                f_fec_ant1 = BOFunciones.FecSumDia(f_fec_ant, 1, Convert.ToInt32(n_tipo_cal));
                n_mes_ant1 = BOFunciones.DateMonth(f_fec_ant1);
                n_ano_ant1 = BOFunciones.DateYear(f_fec_ant1);
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Determina si se encuentra al día el crédito y no se debe cobrar ningun atributo " || to_char(rf_f_prox_pago, "MM/DD/YYYY"));  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------        
                #region credito al dia
                if (n_tipo_pago == 4 || n_tipo_pago == 5)
                {
                    //--Vencido o Anticipado
                    if ((rf_f_prox_pago >= f_fecha_pago && n_tip_pago == 1) || (f_fecha_pago < f_fec_ant && n_tip_pago == 2))
                    {
                        if (n_tipo_pago != 5)
                        {
                            if (n_tip_tf != 4)
                            {
                                rn_tot_capital = rn_tot_capital + n_saldo_act + n_sal_tf;
                            }
                            else
                            {
                                rn_tot_capital = rn_tot_capital + n_saldo_act;
                            };
                        };
                        break;
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   -- Determina si se cobran los atributos proporcionales o no  
                //   --      teniendo en cuenta que sea anticipado: 1 o vencido: 2  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region atributos proporcionales
                n_dias_prop = 0;
                if (n_tip_pago == 2 && ((n_tipo_pago == 4 || n_tipo_pago == 5) || n_tip_cuota == 1))
                {
                    if (f_fecha_pago < rf_f_prox_pago)
                    {
                        if (f_fecha_pago > f_fec_ant || (f_fecha_pago == f_fec_ant && BOFunciones.DateMonth(f_fecha_pago) == 2 && BOFunciones.DateDay(f_fecha_pago) >= 28 &&
                            BOFunciones.DateMonth(f_fec_inicio) != 2 && (BOFunciones.DateDay(f_fec_inicio) == 28 || BOFunciones.DateDay(f_fec_inicio) == 29)))
                        {
                            n_dias_prop = Convert.ToInt32(BOFunciones.FecDifDia(f_fec_ant, f_fecha_pago, Convert.ToInt32(n_tipo_cal)));
                            //--Ajustar los días por efecto del manejo de días calendario vs días comerciales.
                            if (f_fecha_pago == f_fec_ant && BOFunciones.DateMonth(f_fecha_pago) == 2 && BOFunciones.DateDay(f_fecha_pago) >= 28 && BOFunciones.DateMonth(f_fec_inicio) != 2 && (BOFunciones.DateDay(f_fec_inicio) == 28 || BOFunciones.DateDay(f_fec_inicio) == 29))
                            {
                                if (n_dias == 30 || n_dias == 90 || n_dias == 180 || n_dias == 360)
                                {
                                    if (BOFunciones.DateDay(f_fec_apro) == 28)
                                    {
                                        n_dias_prop = 2;
                                    }
                                    else
                                    {
                                        n_dias_prop = 1;
                                    };
                                };
                            };
                            b_int_prop = true;
                            //--Verificar que los días proporcionales no excedan los días de la periodicidad
                            if (n_dias_prop > n_dias && !(n_dias == 1 && n_tip_cuota == 1) && !(n_tipo_pago == 5 && n_tip_intant == 3 && n_cuo_pag == 0 && n_num_cuo_pag == 0))
                            {
                                n_dias_prop = Convert.ToInt32(n_dias);
                            };
                        }
                        else
                        {
                            n_dias_prop = 0;
                            b_int_prop = true;
                        };
                        if (b_int_prop && n_dias_prop == 0)
                        {
                            break;
                        }
                        else if (b_int_prop && n_dias_prop > 0 && n_tipo_pago == 5)
                        {
                            b_int_prop = false;
                            n_dias_prop = 0;
                        };
                    };
                }
                else if (n_tip_pago == 1 && ((n_tipo_pago == 4 || n_tipo_pago == 5) || n_tip_cuota == 1))
                {
                    if ((f_fecha_pago >= rf_f_prox_pago && n_tip_cuota != 1) || (f_fecha_pago > f_fec_ant && n_tip_cuota != 1))
                    {
                        //--Determina interés proporcional a cobrar
                        f_fec_act = BOFunciones.FecSumDia(rf_f_prox_pago, Convert.ToInt32(n_dias), Convert.ToInt32(n_tipo_cal));
                        if (f_fecha_pago < f_fec_act)
                        {
                            n_dias_prop = Convert.ToInt32(BOFunciones.FecDifDia(rf_f_prox_pago, f_fecha_pago, Convert.ToInt32(n_tipo_cal)));
                            b_int_prop = true;
                            if (n_dias_prop > n_dias)
                            {
                                n_dias_prop = Convert.ToInt32(n_dias);
                            };
                            f_fec_act1 = BOFunciones.FecSumDia(f_prox_pag, Convert.ToInt32(n_dias), Convert.ToInt32(n_tipo_cal));
                            n_dias_apro = Convert.ToInt32(BOFunciones.FecDifDia(f_fecha_pago, f_fec_act1, Convert.ToInt32(n_tipo_cal)));
                        }
                        else
                        {
                            n_dias_prop = 0;
                        };
                    }
                    else
                    {
                        //--Determina el valor del interés devuelto, en el caso de interés anticipado y pago total
                        if (f_fecha_pago < f_prox_pag)
                        {
                            n_dias_prop = Convert.ToInt32(BOFunciones.FecDifDia(f_fecha_pago, f_prox_pag, Convert.ToInt32(n_tipo_cal)));
                            b_int_prop = true;
                            if (n_dias_prop > n_dias && n_tip_cuota != 1)
                            {
                                n_dias_prop = Convert.ToInt32(n_dias);
                            };
                        }
                        else
                        {
                            n_dias_prop = 0;
                            b_int_prop = true;
                        };
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Inicializa variables para calculos de atributos para la cuota " || to_char(rf_f_prox_pago, "MM/DD/YYYY"));  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                n_capital = 0;
                n_interes = 0;
                n_otros = 0;
                n_int_aju = 0;
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Ajusta la cuota para los atributos descontados financiados");  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region atributos descontados
                if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme)
                {
                    n_aux_pago = 0;
                };
                n_num = 1;
                while (n_num <= n_atr_otr && n_num <= atr_otro.Length && !b_ajusto_cuota)
                {
                    if (atr_otro[n_num].n_signo == 3)
                    {
                        if (atr_otro[n_num].n_cod_atr != sn_atr_adm || sn_atr_adm == null)
                        {
                            if (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas != 0)
                            {
                                if (BOFunciones.Trim(BOFunciones.BuscarGeneral(1196, 2)) == "1" && b_LeyMiPyme)
                                {
                                    //--Ajusta la cuota a como estaba antes carga el nuevo valor y lo vuelve a restar a la cuota
                                    n_aux_pago = n_aux_pago + atr_otro[n_num].n_valor_pagos[Convert.ToInt32(n_cuo_pag + n_num_cuo_pag + 1)];
                                    n_cuota = n_cuota_original - BOFunciones.NVL(n_aux_pago, 0);
                                }
                                else
                                {
                                    if (n_cuo_pag + n_num_cuo_pag >= atr_otro[n_num].n_num_cuotas)
                                    {
                                        n_cuota = n_cuota + BOFunciones.NVL(n_aux_pago, 0);
                                        b_ajusto_cuota = true;
                                    };
                                };
                            };
                        };
                    };
                    n_num = n_num + 1;
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Determina el valor de la tasa de interes para créditos con tasa variable");  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region control tasa usura
                n_num = 1;
                n_tasa_interes = 0;
                while (n_num <= n_atr_fin)
                {
                    if (atr_finan[n_num].s_tip_cal == "5")
                    {
                        atr_finan[n_num].Conv_Tasa(n_periodic, n_tip_pago, f_fec_ant1, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                    }
                    else
                    {
                        n_val_aux = n_tasa_original[n_num];
                        atr_finan[n_num].n_tasa_calculo = n_tasa_original[n_num];
                    };
                    //--Compara la tasa de interes con la de usura y deja la menor
                    if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                    {
                        s_usura = BOFunciones.BuscarGeneral(681, 1);
                        if (BOFunciones.BuscarGeneral(681, 1) != null)
                        {
                            b_existe = true;
                        }
                        else
                        {
                            b_existe = false;
                        };
                    }
                    else
                    {
                        s_usura = BOFunciones.BuscarGeneral(680, 1);
                        if (BOFunciones.BuscarGeneral(680, 1) != null)
                        {
                            b_existe = true;
                        }
                        else
                        {
                            b_existe = false;
                        };
                    };
                    if (b_existe)
                    {
                        n_usura = n_tipo_tasa_usura;
                        n_tasa_usura = 0;
                        g_f_usura = f_fec_ant1;
                        g_sentencia = "";

                        DApackage.tipo_Hist_Tasa(g_f_usura, n_usura, ref n_tasa_usura, ref n_tipo_tasa, usuario);

                        if (n_tasa_usura != 0)
                        {
                            DApackage.ConTipoTasa(n_tipo_tasa, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);

                            if (s_calc_int == "1")
                            {
                                if (n_per == n_periodic)
                                {
                                    n_tasa_calculo = n_tasa_usura;
                                }
                                else
                                {
                                    n_int_prop = n_dias_per_cre;
                                    n_tasa_calculo = n_tasa_usura / n_int_prop;
                                    n_int_prop = n_dias;
                                    n_tasa_calculo = n_tasa_calculo * n_int_prop;
                                };
                            }
                            else
                            {
                                n_tasa_calculo = BOFunciones.CalTasMod(n_tasa_usura, BOFunciones.To_Number(s_efe_nom), n_per, BOFunciones.To_Number(s_mod), n_mod_per, 1, n_periodic, n_tip_pago, n_periodic, 0);
                            };
                            if (n_tasa_calculo < n_val_aux && n_tasa_calculo > 0)
                            {
                                n_val_aux = n_tasa_calculo;
                                atr_finan[n_num].n_tasa_calculo = n_tasa_calculo;
                            };
                            n_numero = n_numero + 1;
                        };
                    };
                    //--Determina la tasa global del crédito, sumando tasas de los atributos
                    if (atr_finan[n_num].n_cod_atr != n_atr_mora && n_val_aux != null)
                    {
                        n_tasa_interes = n_tasa_interes + n_val_aux;
                    };
                    n_num = n_num + 1;
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Se determina la tasa de interés diaria del interés de mora");  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region tasa interes diaria mora
                if (n_pos_mora != -1)
                {
                    if (n_for_pag == 2 && b_exis_mora)
                    {
                        n_tasa_mora = 0;
                    }
                    else
                    {
                        if (atr_finan[Convert.ToInt32(n_pos_mora)].s_tip_cal == "5")
                        {
                            atr_finan[Convert.ToInt32(n_pos_mora)].Conv_Tasa(n_per_dia, 2, rf_f_prox_pago, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                        };
                    };
                }
                else
                {
                    n_tasa_mora = 0;
                };
                n_tasa_interes = n_tasa_interes / 100;
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Resta valores de terminos fijos si van ha capital");  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region terminos fijos
                if (bterminos)
                {
                    if (ExistenCuotasExtras() && n_tip_tf == 4 && n_tip_paginttf != 1)
                    {
                        n_sum_tf = 0;

                        if (n_num_cuo_pag == 0)
                        {
                            DApackage.SaldoCapital(n_radic, rf_f_prox_pago, ref n_sum_tf, usuario);
                        }
                        else
                        {
                            DApackage.SaldoCapitalRangoFechaProximoPago(n_radic, f_fec_ant, rf_f_prox_pago, ref n_sum_tf, usuario);
                        };

                        n_sum_tf = BOFunciones.NVL(n_sum_tf, 0);
                        n_saldo_act = n_saldo_act - n_sum_tf;

                        if (n_saldo_act < 0)
                        {
                            n_saldo_act = 0;
                        };
                        if (n_tipo_pago == 5 && n_num_cuo_pag == 0)
                        {
                            n_sum_tf = 0;
                            g_sentencia = "Select sum (valor) From tran_cred t, operacion o Where t.cod_ope = o.cod_ope And t.numero_radicacion = :1 And t.tipo_tran = 12 And t.cod_atr = 1 And o.fecha_oper > :2";

                            DApackage.ConsultarValorTransaccionCredito(n_radic, f_fecha_pago, ref n_sum_tf, usuario);

                            n_sum_tf = BOFunciones.NVL(n_sum_tf, 0);
                            n_saldo_act = n_saldo_act - n_sum_tf;
                        };
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Calculo de valores de atributos según tipo de cuota y amortización");  
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------        
                #region Si en select anteriores se encontraron registros en pendientes
                if (VisArrayFindDateTimeFechas(1, rf_f_prox_pago) >= 0 && !(b_int_prop && n_tipo_pago == 4 && f_fecha_pago > f_fec_ant && f_fecha_pago < rf_f_prox_pago))
                {
                    #region pendientes
                    b_existe = true;
                    b_pendientes = true;
                    MensajeConsola("Determina el valor de los valores  adeudados por atributo");
                    n_capital = 0;
                    bResultado = Buscar_Amortiza(rf_f_prox_pago, 1, ref n_capital, ref n_pos_reg_amortiza);
                    if (n_capital == null)
                    {
                        n_capital = 0;
                    };
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        n_atributos[n_num] = 0;
                        bResultado = Buscar_Amortiza(rf_f_prox_pago, Convert.ToInt32(rn_cod_atributos[n_num]), ref n_atributos[n_num], ref n_pos_reg_amortiza);
                        if (n_atributos[n_num] == null)
                        {
                            n_atributos[n_num] = 0;
                        };
                        if (rn_cod_atributos[n_num] == n_atr_mora)
                        {
                            n_atributos[n_num] = 0;
                        };
                        n_interes = n_interes + n_atributos[n_num];
                        n_num = n_num + 1;
                    };
                    #endregion
                }
                else
                {
                    b_existe = false;
                    MensajeConsola("Determina el valor total de la cuota a pagar por atributo");
                    //     -- Tipo de cuota: 1:Plazo fijo, 2:Serie Uniforme, 3:Gradiente
                    if (n_tip_cuota == 1)
                    {
                        #region  -- Tipo de interés:  1:Anticipado,  2:Vencido
                        if (n_tip_pago == 1)
                        {
                            if (n_tipo_pago == 1)
                            {
                                if (s_tipo_prio == "2" && n_man_pago_atr == 2)
                                {
                                    b_plazo_fijo = true;
                                    n_num = 1;
                                    while (n_num <= n_num_atr_pago)
                                    {
                                        if (n_cod_atr_pago[n_num] == 1)
                                        {
                                            n_capital = n_val_atr_pago[n_num];
                                            break;
                                        };
                                        n_num = n_num + 1;
                                    };
                                }
                                else
                                {
                                    n_capital = n_saldo_act;
                                };
                            }
                            else
                            {
                                n_capital = n_saldo_act;
                            };
                            //         -- Devuelve interés
                            if (b_int_prop)
                            {
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        n_interes = n_interes + n_atributos[n_num];
                                        //--Determina si es interes simple o compuesto
                                        if (s_calc_int == "1")
                                        {
                                            n_int_prop = n_dias;
                                            n_atributos[n_num] = atr_finan[n_num].n_tasa_calculo / 100 / n_int_prop * n_dias_prop * n_capital;
                                        }
                                        else
                                        {
                                            n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, 1, n_per_dia, 0);
                                            n_atributos[n_num] = -((n_capital) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                        };
                                        n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                    }
                                    else
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_num = n_num + 1;
                                };
                            };
                        }
                        else if (n_tip_pago == 2)
                        {
                            n_num = 1;
                            n_interes = 0;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                    {
                                        if (n_tip_int == 1)
                                        {
                                            //--Se quito porque para los plazos fijos la tasa es diaria.
                                            n_int_prop = n_tasa_interes;
                                            if (n_tip_tf != 4)
                                            {
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act + n_sal_tf);
                                            }
                                            else
                                            {
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * n_saldo_act;
                                            };
                                        }
                                        else
                                        {
                                            //--Se cambio porque no estaba calculando bien el valor de cada atributo del crédito.
                                            n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                            if (n_tip_tf != 4)
                                            {
                                                n_atributos[n_num] = (n_int_prop / 100) * n_dias_prop * (n_saldo_act + n_sal_tf);
                                            }
                                            else
                                            {
                                                n_atributos[n_num] = (n_int_prop / 100) * n_dias_prop * (n_saldo_act);
                                            };
                                        };
                                    }
                                    else
                                    {
                                        if (n_tip_int == 1)
                                        {
                                            n_atributos[n_num] = atr_finan[n_num].n_tasa_calculo / 100 * n_num_cuotas * n_saldo_act;
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_num_cuotas) - 1) * n_saldo_act;
                                        };
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            n_capital = n_saldo_act;
                        }
                        else
                        {
                            MensajeConsola("El tipo de pago o modalidad se encuentra errado, debe ser Vencido o Anticipado");
                            return -1;
                        };
                        #endregion
                    }
                    else if (n_tip_cuota == 2)
                    {
                        #region       -- Tipo de interés:  1:Simple,  2:Compuesto
                        if (n_tip_int == 1)
                        {
                            if (n_tip_amo == 1)
                            {
                                if (n_num_cuotas == 0)
                                {
                                    n_capital = n_monto_cuo;
                                }
                                else
                                {
                                    n_capital = n_monto_cuo / n_num_cuotas;
                                    n_capital = BOFunciones.Redondeo(n_capital);
                                };
                                n_num = 1;
                                //--Determina si es interés anticipado y es la última cuota que no aparezca interés
                                if (!(n_tip_pago == 1 && n_num_cuo_act == n_num_cuotas))
                                {
                                    while (n_num <= n_atr_fin)
                                    {
                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                        {
                                            if (n_num == n_atr_fin)
                                            {
                                                n_atributos[n_num] = n_cuota - n_capital - n_interes;
                                            }
                                            else
                                            {
                                                if (n_num_cuotas == 0)
                                                {
                                                    n_atributos[n_num] = ((atr_finan[n_num].n_tasa_calculo / 100) * n_monto_cuo);
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = ((atr_finan[n_num].n_tasa_calculo / 100) * n_monto_cuo) / n_num_cuotas;
                                                };
                                            };
                                            if (n_atributos[n_num] < 0)
                                            {
                                                n_atributos[n_num] = 0;
                                            };
                                            n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = 0;
                                        };
                                        n_interes = n_interes + n_atributos[n_num];
                                        n_num = n_num + 1;
                                    };
                                };
                            }
                            else
                            {
                                MensajeConsola("El tipo de amortización definido no es posible");
                                return -1;
                            };
                        }
                        else if (n_tip_int == 2)
                        {
                            //--Tipo amortización: 2: KF, IV; 3: KV, if; 4: CV, IV   5: T.F.Prorrateados
                            if (n_tip_amo == 2)
                            {
                                if (n_num_cuotas == 0)
                                {
                                    n_capital = n_monto;
                                }
                                else
                                {
                                    n_capital = n_cuota;
                                    if (n_capital > n_saldo_act)
                                    {
                                        n_capital = n_saldo_act;
                                    };
                                    n_capital = BOFunciones.Redondeo(n_capital);
                                };
                                n_interes = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                                        if (n_tip_pago == 1)
                                        {
                                            if (n_num == n_atr_fin && !(n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3))
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    //--Determina si es interes simple o compuesto
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = n_tasa_interes / n_int_prop;
                                                    }
                                                    else
                                                    {
                                                        n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                    };
                                                    if (f_fecha_pago < f_prox_pag)
                                                    {
                                                        //--Devuelve interés                   
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = -n_int_prop * n_dias_prop * n_saldo_act - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = -((n_saldo_act) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        //--Cobra interes
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_capital) - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = ((n_saldo_act - n_capital) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    };
                                                }
                                                else
                                                {
                                                    //--Para el atributo de interes corriente y si existe terminos fijos devuelve interès de T.F.
                                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                                    {
                                                        n_atributos[n_num] = ((n_saldo_act - n_capital) * n_tasa_interes) - n_interes;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = ((n_saldo_act - n_capital) * n_tasa_interes) - n_interes;
                                                    };
                                                };
                                            }
                                            else
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = n_tasa_interes / n_int_prop;
                                                    }
                                                    else
                                                    {
                                                        n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, 1, n_per_dia, 0);
                                                    };
                                                    if (f_fecha_pago < f_prox_pag)
                                                    {
                                                        //--Devuelve interés                   
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = -n_int_prop * n_dias_prop * n_saldo_act - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = -((n_saldo_act) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        //--Cobra interes
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_capital) - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_capital) - n_interes;
                                                        };
                                                    };
                                                }
                                                else
                                                {
                                                    //--Para el atributo de interes corriente y si existe terminos fijos devuelve interès de T.F.
                                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_capital);
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_capital);
                                                    };
                                                };
                                            };
                                        }
                                        else
                                        {
                                            if (n_num == n_atr_fin && !(n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3))
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                    n_atributos[n_num] = (n_saldo_act * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (n_saldo_act * n_tasa_interes) - n_interes;
                                                };
                                            }
                                            else
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, 1, n_per_dia, 0);
                                                    n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * n_saldo_act;
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo_act;
                                                };
                                            };
                                        };
                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3)
                                        {
                                            if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                            {
                                                n_dias_aju = 0;
                                            }
                                            else
                                            {
                                                if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                                                {
                                                    return -1;
                                                };
                                            };
                                            if (n_dias_aju == 0)
                                            {
                                                if (f_fec_apro > f_fec_ini_aux)
                                                {
                                                    n_dias_aju = -BOFunciones.FecDifDia(f_fec_ini_aux, f_fec_apro, Convert.ToInt32(n_tipo_cal));
                                                }
                                                else
                                                {
                                                    n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_ini_aux, Convert.ToInt32(n_tipo_cal));
                                                };
                                            };
                                            if (BOFunciones.BuscarGeneral(1500, 2) == "1")
                                            {
                                                n_int_aju = 0;
                                            }
                                            else if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0)
                                                {
                                                    n_int_aju = (atr_finan[n_num].n_tasa_calculo / 100) / n_dias * n_dias_aju * n_saldo_act;
                                                };
                                            }
                                            else
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0 && !(BOFunciones.BuscarGeneral(1500, 2) == "3" && atr_finan[n_num].n_cod_atr != n_atr_corr))
                                                {
                                                    n_int_aju = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_dias_aju / n_dias) - 1) * n_saldo_act;
                                                };
                                            };
                                            n_atributos[n_num] = n_atributos[n_num] + n_int_aju;
                                        };
                                        n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                    }
                                    else
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_interes = n_interes + n_atributos[n_num];
                                    n_num = n_num + 1;
                                };
                            }
                            else if (n_tip_amo == 3)
                            {
                                if (n_num_cuotas == 0)
                                {
                                    n_capital = n_monto;
                                }
                                else
                                {
                                    n_capital = n_monto / n_num_cuotas;
                                    if (n_capital > n_saldo_act)
                                    {
                                        n_capital = n_saldo_act;
                                    };
                                    n_capital = BOFunciones.Redondeo(n_capital);
                                };
                                n_interes = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        //--Tipo de pago:   1: Anticipado,  2: Vencido
                                        if (n_tip_pago == 1)
                                        {
                                            if (n_num == n_atr_fin)
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    //--Determina si es interes simple o compuesto
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = n_tasa_interes / n_int_prop;
                                                    }
                                                    else
                                                    {
                                                        n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                    };
                                                    if (f_fecha_pago < f_prox_pag)
                                                    {
                                                        //--Devuelve interés                   
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = -n_int_prop * n_dias_prop * n_monto - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = -((n_monto) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        //--Cobra interes
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_monto - n_capital) - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = ((n_monto - n_capital) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    };
                                                }
                                                else
                                                {
                                                    //--Para el atributo de interes corriente y si existe terminos fijos devuelve interès de T.F.
                                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                                    {
                                                        n_atributos[n_num] = ((n_monto - n_capital) * n_tasa_interes) - n_interes;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = ((n_monto - n_capital) * n_tasa_interes) - n_interes;
                                                    };
                                                };
                                            }
                                            else
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = n_tasa_interes / n_int_prop;
                                                    }
                                                    else
                                                    {
                                                        n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, 1, n_per_dia, 0);
                                                    };
                                                    if (f_fecha_pago < f_prox_pag)
                                                    {
                                                        //--Devuelve interés                   
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = -n_int_prop * n_dias_prop * n_monto - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = -((n_monto) * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        //--Cobra interes
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_monto - n_capital) - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_monto - n_capital) - n_interes;
                                                        };
                                                    };
                                                }
                                                else
                                                {
                                                    //--Para el atributo de interes corriente y si existe terminos fijos devuelve interès de T.F.
                                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_monto - n_capital);
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_monto - n_capital);
                                                    };
                                                };
                                            };
                                        }
                                        else
                                        {
                                            if (n_num == n_atr_fin && !(n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3))
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                    n_atributos[n_num] = (n_monto * (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1)) - n_interes;
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (n_monto * n_tasa_interes) - n_interes;
                                                };
                                            }
                                            else
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, 1, n_per_dia, 0);
                                                    n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * n_monto;
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_monto;
                                                };
                                            };
                                        };
                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3)
                                        {
                                            if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                            {
                                                n_dias_aju = 0;
                                            }
                                            else
                                            {
                                                if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                                                {
                                                    return -1;
                                                };
                                            };
                                            if (n_dias_aju == 0)
                                            {
                                                if (f_fec_apro > f_fec_ini_aux)
                                                {
                                                    n_dias_aju = -BOFunciones.FecDifDia(f_fec_ini_aux, f_fec_apro, Convert.ToInt32(n_tipo_cal));
                                                }
                                                else
                                                {
                                                    n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_ini_aux, Convert.ToInt32(n_tipo_cal));
                                                };
                                            };
                                            if (BOFunciones.BuscarGeneral(1500, 2) == "1")
                                            {
                                                n_int_aju = 0;
                                            }
                                            else if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0)
                                                {
                                                    n_int_aju = (atr_finan[n_num].n_tasa_calculo / 100) / n_dias * n_dias_aju * n_monto;
                                                };
                                            }
                                            else
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0 && !(BOFunciones.BuscarGeneral(1500, 2) == "3" && atr_finan[n_num].n_cod_atr != n_atr_corr))
                                                {
                                                    n_int_aju = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_dias_aju / n_dias) - 1) * n_monto;
                                                };
                                            };
                                            n_atributos[n_num] = n_atributos[n_num] + n_int_aju;
                                        };
                                        n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                    }
                                    else
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_interes = n_interes + n_atributos[n_num];
                                    n_num = n_num + 1;
                                };
                            }
                            else if (n_tip_amo == 4)
                            {
                                //--Tipo de pago:   1: Anticipado,  2: Vencido
                                if (n_tip_pago == 1)
                                {
                                    n_periodos_anuales = Convert.ToInt32(DApackage.ConPeriodicidadPerAnu(n_periodic, usuario));
                                };
                                n_interes = 0;
                                n_tot_interes_aux = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    //--Tipo de pago:   1: Anticipado,  2: Vencido
                                    if (n_tip_pago == 1)
                                    {
                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                        {
                                            if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                            {
                                                if (s_calc_int == "1")
                                                {
                                                    n_int_prop = n_dias;
                                                    n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                    n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act);
                                                    if (n_dias_apro > 0)
                                                    {
                                                        n_atributos[n_num + 50] = n_int_prop / 100 * n_dias_apro * (n_saldo_act);
                                                    };
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (n_saldo_act - n_cuota) * ((1 / (1 - atr_finan[n_num].n_tasa_calculo / 100)) - 1);
                                                    n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                };
                                            }
                                            else
                                            {
                                                n_atributos[n_num] = (n_saldo_act - n_cuota) * ((1 / (1 - atr_finan[n_num].n_tasa_calculo / 100)) - 1);
                                            };
                                            if (n_atributos[n_num] < 0)
                                            {
                                                n_atributos[n_num] = 0;
                                            };
                                            //--Si es la primera cuota y los interes de ajuste se cobran lo antes posible.
                                            if (n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3)
                                            {
                                                if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                                {
                                                    n_dias_aju = 0;
                                                };
                                                if (n_dias_aju == 0)
                                                {
                                                    if (f_fec_apro > f_fec_ini_aux)
                                                    {
                                                        n_dias_aju = -BOFunciones.FecDifDia(f_fec_ini_aux, f_fec_apro, Convert.ToInt32(n_tipo_cal));
                                                    }
                                                    else
                                                    {
                                                        n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_ini_aux, Convert.ToInt32(n_tipo_cal));
                                                    };
                                                };
                                                if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                                {
                                                    if (BOFunciones.NVL(n_dias, 0) != 0)
                                                    {
                                                        n_int_aju = (atr_finan[n_num].n_tasa_calculo / 100) / n_dias * n_dias_aju * n_saldo_act;
                                                        n_int_aju = BOFunciones.Redondeo(n_int_aju);
                                                    };
                                                }
                                                else
                                                {
                                                    if (BOFunciones.NVL(n_dias, 0) != 0)
                                                    {
                                                        n_int_aju = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_dias_aju / n_dias) - 1) * n_saldo_act;
                                                        n_int_aju = BOFunciones.Redondeo(n_int_aju);
                                                    };
                                                };
                                                n_atributos[n_num] = n_atributos[n_num] + n_int_aju;
                                            };
                                            n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = 0;
                                        };
                                    }
                                    else
                                    {
                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                        {
                                            if (n_num == n_atr_fin && !(n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3))
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    //--Se ajusta para que el seguro en pago total cobre el valor completo del mes y no la proporcion.
                                                    if (atr_finan[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 2)))
                                                    {
                                                        n_int_prop = atr_finan[n_num].n_tasa_calculo / 100;
                                                        if (n_dias <= 30)
                                                        {
                                                            n_atributos[n_num] = (n_int_prop * n_saldo_act) * 30 / n_dias;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (n_int_prop * n_saldo_act);
                                                        };
                                                    }
                                                    else
                                                    {
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_int_prop = n_dias;
                                                            n_int_prop = n_tasa_interes / n_int_prop;
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * n_saldo_act - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (n_saldo_act * n_tasa_interes) - n_tot_interes_aux;
                                                            n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                        };
                                                    };
                                                }
                                                else
                                                {
                                                    if (n_tip_intant == 3 && BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                    {
                                                        //--Calculando los intereses para los días de ajuste
                                                        n_int_prop = n_dias;
                                                        if ((n_cuo_pag == 0 && n_num_cuo_pag == 0) && n_dias <= 31)
                                                        {
                                                            //--Determinando fecha del primer pago
                                                            f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                                            //--Calculando el factor de dias para calculo de la tasa
                                                            n_dias_mes = BOFunciones.DateDay(f_fec_pripag);
                                                            n_dia_des = BOFunciones.DateDay(f_fec_apro);
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_factor = (31 - n_dia_des) / 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_factor = (28 - n_dia_des) / 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_factor = (29 - n_dia_des) / 29;
                                                            }
                                                            else
                                                            {
                                                                n_factor = (n_dias_mes - n_dia_des) / n_dias_mes;
                                                            };
                                                            n_factor = n_factor * -1;
                                                            //--Calculando dias para calculo de intereses de la primera cuota
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_dias_ajuste = (31 - n_dia_des) + 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_dias_ajuste = (28 - n_dia_des) + 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_dias_ajuste = (29 - n_dia_des) + 29;
                                                            }
                                                            else
                                                            {
                                                                n_dias_ajuste = (n_dias_mes - n_dia_des) + n_dias_mes;
                                                            };
                                                            if (n_dias_ajuste != 0)
                                                            {
                                                                if (((BOFunciones.DateMonth(rf_f_prox_pago) == 1 || BOFunciones.DateMonth(rf_f_prox_pago) == 3 || BOFunciones.DateMonth(rf_f_prox_pago) == 5 || BOFunciones.DateMonth(rf_f_prox_pago) == 7 || BOFunciones.DateMonth(rf_f_prox_pago) == 8 || BOFunciones.DateMonth(rf_f_prox_pago) == 10 || BOFunciones.DateMonth(rf_f_prox_pago) == 12) && BOFunciones.DateDay(rf_f_prox_pago) < 31) || (BOFunciones.DateMonth(rf_f_prox_pago) == 2 || BOFunciones.Mod(BOFunciones.DateYear(rf_f_prox_pago), 4) == 0))
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), n_dias_ajuste / BOFunciones.DateDay(Convert.ToDateTime(rf_f_prox_pago).AddDays(1))) - 1) * n_saldo_act;
                                                                }
                                                                else
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), n_dias_ajuste / BOFunciones.DateDay(rf_f_prox_pago)) - 1) * n_saldo_act;
                                                                };
                                                            }
                                                            else
                                                            {
                                                                n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo_act;
                                                            };
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), 30 / 30) - 1) * n_saldo_act;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        //--Se ajustó porque estaba dejando de cobrar el último atributo
                                                        n_atributos[n_num] = (n_saldo_act * n_tasa_interes) - n_interes;
                                                    };
                                                };
                                            }
                                            else
                                            {
                                                if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                                {
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                        n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * n_saldo_act;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo_act;
                                                        if (n_atributos[n_num] < 0)
                                                        {
                                                            n_atributos[n_num] = 0;
                                                        };
                                                        n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                                        n_tot_interes_aux = n_tot_interes_aux + n_atributos[n_num];
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo_act;
                                                        n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                    };
                                                }
                                                else
                                                {
                                                    if (n_tip_intant == 3 && BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                    {
                                                        //--Calculando los intereses para los días de ajuste
                                                        n_int_prop = n_dias;
                                                        if ((n_cuo_pag == 0 && n_num_cuo_pag == 0) && n_dias <= 31)
                                                        {
                                                            //--Determinando fecha del primer pago
                                                            f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                                            //--Calculando el factor de dias para calculo de la tasa
                                                            n_dias_mes = BOFunciones.DateDay(f_fec_pripag);
                                                            n_dia_des = BOFunciones.DateDay(f_fec_apro);
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_factor = (31 - n_dia_des) / 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_factor = (28 - n_dia_des) / 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_factor = (29 - n_dia_des) / 29;
                                                            }
                                                            else
                                                            {
                                                                n_factor = (n_dias_mes - n_dia_des) / n_dias_mes;
                                                            };
                                                            n_factor = n_factor * -1;
                                                            //--Calculando dias para calculo de intereses de la primera cuota
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_dias_ajuste = (31 - n_dia_des) + 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_dias_ajuste = (28 - n_dia_des) + 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_dias_ajuste = (29 - n_dia_des) + 29;
                                                            }
                                                            else
                                                            {
                                                                n_dias_ajuste = (n_dias_mes - n_dia_des) + n_dias_mes;
                                                            };
                                                            if (n_dias_ajuste != 0)
                                                            {
                                                                if (((BOFunciones.DateMonth(rf_f_prox_pago) == 1 || BOFunciones.DateMonth(rf_f_prox_pago) == 3 || BOFunciones.DateMonth(rf_f_prox_pago) == 5 || BOFunciones.DateMonth(rf_f_prox_pago) == 7 || BOFunciones.DateMonth(rf_f_prox_pago) == 8 || BOFunciones.DateMonth(rf_f_prox_pago) == 10 || BOFunciones.DateMonth(rf_f_prox_pago) == 12) && BOFunciones.DateDay(rf_f_prox_pago) < 31) || (BOFunciones.DateMonth(rf_f_prox_pago) == 2 && BOFunciones.Mod(BOFunciones.DateYear(rf_f_prox_pago), 4) == 0))
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), n_dias_ajuste / BOFunciones.DateDay(Convert.ToDateTime(rf_f_prox_pago).AddDays(1))) - 1) * n_saldo_act;
                                                                }
                                                                else
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), n_dias_ajuste / BOFunciones.DateDay(rf_f_prox_pago)) - 1) * n_saldo_act;
                                                                };
                                                            }
                                                            else
                                                            {
                                                                n_atributos[n_num] = n_tasa_interes * n_saldo_act;
                                                            };
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + (atr_finan[n_num].n_tasa_calculo / 100), 30 / 30) - 1) * n_saldo_act;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo_act;
                                                    };
                                                };
                                            };
                                            if (n_atributos[n_num] < 0)
                                            {
                                                n_atributos[n_num] = 0;
                                            };
                                            // ------------------------------------------------------------------------------------------
                                            // --Si es la primera cuota y los intereses de ajuste se cobran entonces sumar el valor
                                            //------------------------------------------------------------------------------------------
                                            if (n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3)
                                            {
                                                n_int_aju = 0;
                                                if (BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                {
                                                    n_int_aju = 0;
                                                }
                                                else if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                                {
                                                    if (BOFunciones.NVL(n_dias, 0) != 0)
                                                    {
                                                        n_int_aju = (atr_finan[n_num].n_tasa_calculo / 100) / n_dias * n_dias_aju * n_saldo_act;
                                                    };
                                                }
                                                else
                                                {
                                                    if (BOFunciones.NVL(n_dias, 0) != 0 && !(BOFunciones.BuscarGeneral(1500, 2) == "3" && atr_finan[n_num].n_cod_atr != n_atr_corr))
                                                    {
                                                        n_int_aju = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_dias_aju / n_dias) - 1) * n_saldo_act;
                                                    };
                                                };
                                                n_atributos[n_num] = n_atributos[n_num] + n_int_aju;
                                            };
                                            n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = 0;
                                        };
                                    };
                                    if (n_atributos[n_num] == null)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_interes = n_interes + n_atributos[n_num];
                                    n_num = n_num + 1;
                                };
                                if (b_int_prop && b_interes_dias)
                                {
                                    n_capital = 0;
                                }
                                else
                                {
                                    if (s_mor_sig == "1")
                                    {
                                        n_capital = n_cuota - (n_interes - n_atributos[Convert.ToInt32(n_pos_mora)]) + n_int_aju;
                                    }
                                    else
                                    {
                                        if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                        {
                                            n_int_aju = 0;
                                        };
                                        n_int_aju = BOFunciones.Redondeo(n_int_aju);
                                        n_capital = n_cuota - n_interes + n_int_aju;
                                        n_capital = BOFunciones.Redondeo(n_capital);
                                    };
                                };
                            }
                            else if (n_tip_amo == 5)
                            {
                                //--Tipo de pago:   1: Anticipado,  2: Vencido
                                if (n_tip_pago == 1)
                                {
                                    n_interes = (n_saldo_act + n_sal_tf) * n_tasa_interes;
                                    n_interes = BOFunciones.Redondeo(n_interes);
                                    n_capital = n_cuota - n_interes;
                                    n_cap_ant = n_capital;
                                }
                                else
                                {
                                    n_cap_ant = 0;
                                };
                                n_interes = 0;
                                n_tot_interes_aux = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                    {
                                        if (n_num == n_atr_fin && !(n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3))
                                        {
                                            if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                            {
                                                //--Se ajusta para que el seguro en pago total cobre el valor completo del mes y no la proporcion.
                                                if (atr_finan[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 2)))
                                                {
                                                    n_int_prop = atr_finan[n_num].n_tasa_calculo / 100;
                                                    if (n_dias <= 30)
                                                    {
                                                        n_atributos[n_num] = (n_int_prop * n_saldo_act) * 30 / n_dias;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (n_int_prop * n_saldo_act);
                                                    };
                                                }
                                                else
                                                {
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = n_tasa_interes / n_int_prop;
                                                        if (n_tip_tf != 4)
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act + n_sal_tf - n_cap_ant) - n_interes;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        if (n_tip_tf != 4)
                                                        {
                                                            n_atributos[n_num] = (((n_saldo_act + n_sal_tf) - n_cap_ant) * n_tasa_interes) - n_tot_interes_aux;
                                                            n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (((n_saldo_act) - n_cap_ant) * n_tasa_interes) - n_tot_interes_aux;
                                                            n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                        };
                                                    };
                                                };
                                            }
                                            else
                                            {
                                                if (n_tip_tf != 4)
                                                {
                                                    if (n_tip_intant == 3 && BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                    {
                                                        //--Calculando los intereses para los días de ajuste
                                                        n_int_prop = n_dias;
                                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0)
                                                        {
                                                            //--Determinando fecha del primer pago
                                                            f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                                            //--Calculando el factor de dias para calculo de la tasa
                                                            n_dias_mes = BOFunciones.DateDay(f_fec_pripag);
                                                            n_dia_des = BOFunciones.DateDay(f_fec_apro);
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_factor = (31 - n_dia_des) / 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_factor = (28 - n_dia_des) / 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_factor = (29 - n_dia_des) / 29;
                                                            }
                                                            else
                                                            {
                                                                n_factor = (n_dias_mes - n_dia_des) / n_dias_mes;
                                                            };
                                                            n_factor = n_factor * -1;
                                                            //--Calculando dias para calculo de intereses de la primera cuota
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_dias_ajuste = (31 - n_dia_des) + 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_dias_ajuste = (28 - n_dia_des) + 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_dias_ajuste = (29 - n_dia_des) + 29;
                                                            }
                                                            else
                                                            {
                                                                n_dias_ajuste = (n_dias_mes - n_dia_des) + n_dias_mes;
                                                            };
                                                            if (n_dias_ajuste != 0)
                                                            {
                                                                if (((BOFunciones.DateMonth(rf_f_prox_pago) == 1 || BOFunciones.DateMonth(rf_f_prox_pago) == 3 || BOFunciones.DateMonth(rf_f_prox_pago) == 5 || BOFunciones.DateMonth(rf_f_prox_pago) == 7 || BOFunciones.DateMonth(rf_f_prox_pago) == 8 || BOFunciones.DateMonth(rf_f_prox_pago) == 10 || BOFunciones.DateMonth(rf_f_prox_pago) == 12) && BOFunciones.DateDay(rf_f_prox_pago) < 31) || (BOFunciones.DateMonth(rf_f_prox_pago) == 2 && BOFunciones.Mod(BOFunciones.DateYear(rf_f_prox_pago), 4) == 0))
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, n_dias_ajuste / BOFunciones.DateDay(Convert.ToDateTime(rf_f_prox_pago).AddDays(1))) - 1) * n_saldo_act;
                                                                }
                                                                else
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, n_dias_ajuste / BOFunciones.DateDay(rf_f_prox_pago)) - 1) * n_saldo_act;
                                                                };
                                                            }
                                                            else
                                                            {
                                                                n_atributos[n_num] = n_tasa_interes * n_saldo_act;
                                                            };
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, 30 / 30) - 1) * n_saldo_act;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (((n_saldo_act + n_sal_tf) - n_cap_ant) * n_tasa_interes) - n_interes;
                                                    };
                                                }
                                                else
                                                {
                                                    n_atributos[n_num] = (((n_saldo_act) - n_cap_ant) * n_tasa_interes) - n_interes;
                                                };
                                            };
                                        }
                                        else
                                        {
                                            if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                            {
                                                //--Se ajusta para que el seguro en pago total cobre el valor completo del mes y no la proporcion.FerOrt.Ene - 2 - 2006
                                                if (atr_finan[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 2)))
                                                {
                                                    n_int_prop = atr_finan[n_num].n_tasa_calculo / 100;
                                                    if (n_dias <= 30)
                                                    {
                                                        n_atributos[n_num] = (n_int_prop * n_saldo_act) * 30 / n_dias;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (n_int_prop * n_saldo_act);
                                                    };
                                                }
                                                else
                                                {
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_int_prop = n_dias;
                                                        n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                        if (n_tip_tf != 4)
                                                        {
                                                            n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act + n_sal_tf - n_cap_ant);
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                                        };
                                                    }
                                                    else
                                                    {
                                                        if (n_tip_tf != 4)
                                                        {
                                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act + n_sal_tf - n_cap_ant);
                                                            if (n_atributos[n_num] < 0)
                                                            {
                                                                n_atributos[n_num] = 0;
                                                            };
                                                            n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                                            n_tot_interes_aux = n_tot_interes_aux + n_atributos[n_num];
                                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act + n_sal_tf - n_cap_ant);
                                                            n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                                            if (n_atributos[n_num] < 0)
                                                            {
                                                                n_atributos[n_num] = 0;
                                                            };
                                                            n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                                            n_tot_interes_aux = n_tot_interes_aux + n_atributos[n_num];
                                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                                            n_atributos[n_num] = n_atributos[n_num] * n_dias_prop / n_dias;
                                                        };
                                                    };
                                                };
                                            }
                                            else
                                            {
                                                if (n_tip_tf != 4)
                                                {
                                                    if (n_tip_intant == 3 && BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                    {
                                                        //--Calculando los intereses para los días de ajuste
                                                        n_int_prop = n_dias;
                                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0)
                                                        {
                                                            //--Determinando fecha del primer pago
                                                            f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                                            //--Calculando el factor de dias para calculo de la tasa
                                                            n_dias_mes = BOFunciones.DateDay(f_fec_pripag);
                                                            n_dia_des = BOFunciones.DateDay(f_fec_apro);
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_factor = (31 - n_dia_des) / 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_factor = (28 - n_dia_des) / 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_factor = (29 - n_dia_des) / 29;
                                                            }
                                                            else
                                                            {
                                                                n_factor = (n_dias_mes - n_dia_des) / n_dias_mes;
                                                            };
                                                            n_factor = n_factor * -1;
                                                            //--Calculando dias para calculo de intereses de la primera cuota
                                                            if ((BOFunciones.DateMonth(f_fec_pripag) == 1 || BOFunciones.DateMonth(f_fec_pripag) == 3 || BOFunciones.DateMonth(f_fec_pripag) == 5 || BOFunciones.DateMonth(f_fec_pripag) == 7 || BOFunciones.DateMonth(f_fec_pripag) == 8 || BOFunciones.DateMonth(f_fec_pripag) == 10 || BOFunciones.DateMonth(f_fec_pripag) == 12) && BOFunciones.DateDay(f_fec_pripag) == 30)
                                                            {
                                                                n_dias_ajuste = (31 - n_dia_des) + 31;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) != 0)
                                                            {
                                                                n_dias_ajuste = (28 - n_dia_des) + 28;
                                                            }
                                                            else if (BOFunciones.DateMonth(f_fec_pripag) == 2 && BOFunciones.DateDay(f_fec_pripag) == 28 && BOFunciones.Mod(BOFunciones.DateYear(f_fec_pripag), 4) == 0)
                                                            {
                                                                n_dias_ajuste = (29 - n_dia_des) + 29;
                                                            }
                                                            else
                                                            {
                                                                n_dias_ajuste = (n_dias_mes - n_dia_des) + n_dias_mes;
                                                            };
                                                            if (n_dias_ajuste != 0)
                                                            {
                                                                if (((BOFunciones.DateMonth(rf_f_prox_pago) == 1 || BOFunciones.DateMonth(rf_f_prox_pago) == 3 || BOFunciones.DateMonth(rf_f_prox_pago) == 5 || BOFunciones.DateMonth(rf_f_prox_pago) == 7 || BOFunciones.DateMonth(rf_f_prox_pago) == 8 || BOFunciones.DateMonth(rf_f_prox_pago) == 10 || BOFunciones.DateMonth(rf_f_prox_pago) == 12) && BOFunciones.DateDay(rf_f_prox_pago) < 31) || (BOFunciones.DateMonth(rf_f_prox_pago) == 2 && BOFunciones.Mod(BOFunciones.DateYear(rf_f_prox_pago), 4) == 0))
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, n_dias_ajuste / BOFunciones.DateDay(Convert.ToDateTime(rf_f_prox_pago).AddDays(1))) - 1) * n_saldo_act;
                                                                }
                                                                else
                                                                {
                                                                    n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, n_dias_ajuste / BOFunciones.DateDay(rf_f_prox_pago)) - 1) * n_saldo_act;
                                                                };
                                                            }
                                                            else
                                                            {
                                                                n_atributos[n_num] = n_tasa_interes * n_saldo_act;
                                                            };
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, 30 / 30) - 1) * n_saldo_act;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act + n_sal_tf - n_cap_ant);
                                                    };
                                                }
                                                else
                                                {
                                                    if (n_tip_intant == 3 && BOFunciones.BuscarGeneral(1500, 2) == "1")
                                                    {
                                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0)
                                                        {
                                                            bResultado = BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men);
                                                            f_fec_pripag = BOFunciones.FecSumDia(f_fec_inicio, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                                                            n_dias_aju = Convert.ToInt32((Convert.ToDateTime(f_fec_pripag) - Convert.ToDateTime(f_fec_inicio))) + Convert.ToInt32(n_dias_aju);
                                                            n_dias_aju = BOFunciones.Redondeo(n_dias_aju);
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, n_dias_aju / 30) - 1) * n_saldo_act;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[n_num] = (BOFunciones.Power(1 + n_tasa_interes, 30 / 30) - 1) * n_saldo_act;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                                    };
                                                };
                                            };
                                        };
                                        if (n_cuo_pag == 0 && n_num_cuo_pag == 0 && n_tip_intant == 3 && n_tipo_pago != 4)
                                        {
                                            if (BOFunciones.BuscarGeneral(1500, 1) == "2")
                                            {
                                                n_dias_aju = 0;
                                            }
                                            else
                                            {
                                                if (!BOFunciones.FecIniCre(f_fec_apro, n_periodic, n_for_pag, n_tip_pago, n_tip_intant, n_cod_empresa, s_cod_credi, ref f_fec_inicio, ref n_dias_aju, ref s_error_men))
                                                {
                                                    return -1;
                                                };
                                            };
                                            if (n_dias_aju == 0)
                                            {
                                                if (f_fec_apro > f_fec_ini_aux)
                                                {
                                                    n_dias_aju = -BOFunciones.FecDifDia(f_fec_ini_aux, f_fec_apro, Convert.ToInt32(n_tipo_cal));
                                                }
                                                else
                                                {
                                                    n_dias_aju = BOFunciones.FecDifDia(f_fec_apro, f_fec_ini_aux, Convert.ToInt32(n_tipo_cal));
                                                };
                                            };
                                            if (BOFunciones.BuscarGeneral(1500, 2) == "1")
                                            {
                                                n_int_aju = 0;
                                            }
                                            else if (BOFunciones.BuscarGeneral(1500, 2) == "2")
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0)
                                                {
                                                    n_int_aju = (atr_finan[n_num].n_tasa_calculo / 100) / n_dias * n_dias_aju * n_saldo_act;
                                                };
                                            }
                                            else
                                            {
                                                if (BOFunciones.NVL(n_dias, 0) != 0 && !(BOFunciones.BuscarGeneral(1500, 2) == "3" && atr_finan[n_num].n_cod_atr != n_atr_corr))
                                                {
                                                    n_int_aju = (BOFunciones.Power(atr_finan[n_num].n_tasa_calculo / 100 + 1, n_dias_aju / n_dias) - 1) * n_saldo_act;
                                                };
                                            };
                                            n_atributos[n_num] = n_atributos[n_num] + n_int_aju;
                                        };
                                        n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                        if (n_atributos[n_num] < 0)
                                        {
                                            n_atributos[n_num] = 0;
                                        };
                                    }
                                    else
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_interes = n_interes + n_atributos[n_num];
                                    n_num = n_num + 1;
                                };
                                //--Tipo de pago:   1: Anticipado,  2: Vencido
                                if (n_tip_pago == 2)
                                {
                                    n_capital = n_cuota - n_interes;
                                };
                                n_sal_tf = SumarSaldoCapitalCuotasExtras(true, Convert.ToDateTime(f_fecha_pago));
                            }
                            else
                            {
                                MensajeConsola("El tipo de amortiazación definido no es posible");
                                n_cuota = 0;
                                return -1;
                            };
                        }
                        else
                        {
                            MensajeConsola("El tipo de interes se encuentra errado, debe ser simple o compuesto");
                            return -1;
                        };
                        #endregion
                    }
                    else if (n_tip_cuota == 3)
                    {
                        #region gradientes
                        if (n_tip_amo == 7)
                        {
                            if (BOFunciones.Mod(n_num_cuo_act + n_num_cuo_pag, 12 * 30 / n_dias) == 0 && n_num_cuo_act + n_num_cuo_pag > 0)
                            {
                                n_cuota = n_cuota * (1 + n_val_grad / 100);
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_interes = n_saldo_act * n_tasa_interes;
                                n_interes = BOFunciones.Redondeo(n_interes);
                                n_capital = n_cuota - n_interes;
                                n_cap_ant = n_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            };
                            n_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == n_atr_fin - 1)
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = n_tasa_interes / n_int_prop;
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = ((BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant)) - n_interes;
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = ((n_saldo_act - n_cap_ant) * n_tasa_interes) - n_interes;
                                        };
                                    }
                                    else
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant);
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                        };
                                    };
                                    if (n_atributos[n_num] < 0)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                            {
                                n_capital = n_cuota - n_interes;
                            };
                        }
                        else if (n_tip_amo == 8)
                        {
                            //--Recalculo de la cuota
                            n_cuota = n_saldo_act * (n_tasa_interes + 1) * n_val_grad / n_num_cuotas;
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_interes = n_saldo_act * n_tasa_interes;
                                n_interes = BOFunciones.Redondeo(n_interes);
                                n_capital = n_cuota - n_interes;
                                n_cap_ant = n_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            };
                            n_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == n_atr_fin - 1)
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = n_tasa_interes / n_int_prop;
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = ((BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant)) - n_interes;
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = ((n_saldo_act - n_cap_ant) * n_tasa_interes) - n_interes;
                                        };
                                    }
                                    else
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant);
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                        };
                                    };
                                    if (n_atributos[n_num] < 0)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                            {
                                n_capital = n_cuota - n_interes;
                            };
                        }
                        else if (n_tip_amo == 9)
                        {
                            //--Recalculo de la cuota
                            if (BOFunciones.Mod(n_num_cuo_act + n_num_cuo_pag, 12 * 30 / n_dias) == 0 && n_num_cuo_act + n_num_cuo_pag > 0)
                            {
                                n_cuota = n_cuota * (1 + n_val_grad / 100);
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_interes = n_saldo_act * n_tasa_interes;
                                n_interes = BOFunciones.Redondeo(n_interes);
                                n_capital = n_cuota - n_interes;
                                n_cap_ant = n_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            };
                            n_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == n_atr_fin - 1)
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = n_tasa_interes / n_int_prop;
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = ((BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant)) - n_interes;
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = ((n_saldo_act - n_cap_ant) * n_tasa_interes) - n_interes;
                                        };
                                    }
                                    else
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant);
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                        };
                                    };
                                    if (n_atributos[n_num] < 0)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                            {
                                n_capital = n_cuota - n_interes;
                            };
                        }
                        else if (n_tip_amo == 10)
                        {
                            //--Recalculo de la cuota
                            if (n_num_cuo_act + n_num_cuo_pag > 0)
                            {
                                n_cuota = n_cuota * (1 + n_tasa_interes);
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_interes = n_saldo_act * n_tasa_interes;
                                n_interes = BOFunciones.Redondeo(n_interes);
                                n_capital = n_cuota - n_interes;
                                n_cap_ant = n_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            };
                            n_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == n_atr_fin - 1)
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = n_tasa_interes / n_int_prop;
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = ((BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant)) - n_interes;
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = ((n_saldo_act - n_cap_ant) * n_tasa_interes) - n_interes;
                                        };
                                    }
                                    else
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant);
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                        };
                                    };
                                    if (n_atributos[n_num] < 0)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                            {
                                n_capital = n_cuota - n_interes;
                            };
                        }
                        else if (n_tip_amo == 11)
                        {
                            //--Recalculo de la cuota
                            n_val_temp = 0;
                            if (BOFunciones.Mod(n_num_cuo_act + n_num_cuo_pag, 12) == 0 && n_num_cuo_act + n_num_cuo_pag > 0)
                            {
                                n_veces = (n_num_cuo_act + n_num_cuo_pag) / 12;
                                n_num_temp = 0;
                                while (n_num_temp < n_veces)
                                {
                                    n_cuota = n_cuota * (1 + n_val_grad / 100);
                                    n_cuota = BOFunciones.Redondeo(n_cuota);
                                    n_num_gra = 1;
                                    while (true)
                                    {
                                        if (n_num_gra <= n_atr_otr)
                                        {
                                            if (atr_otro[Convert.ToInt32(n_num_gra)].n_cod_atr == n_atrib_gra)
                                            {
                                                atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo = atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo * (1 + n_val_grad / 100);
                                                n_val_temp = atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo;
                                                n_num_gra = n_num_gra + 1;
                                                n_val_temp = BOFunciones.Redondeo(n_val_temp);
                                                atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo = n_val_temp;
                                            }
                                            else
                                            {
                                                n_num_gra = n_num_gra + 1;
                                            };
                                        }
                                        else
                                        {
                                            break;
                                        };
                                    };
                                    n_num_temp = n_num_temp + 1;
                                };
                                n_verifica = 1;
                            }
                            else if (((n_num_cuo_act + n_num_cuo_pag) / 12) >= 1 && n_verifica == 0)
                            {
                                n_veces = (n_num_cuo_act + n_num_cuo_pag) / 12;
                                n_num_temp = 1;
                                while (n_num_temp <= n_veces)
                                {
                                    n_cuota = n_cuota * (1 + n_val_grad / 100);
                                    n_cuota = BOFunciones.Redondeo(n_cuota);
                                    n_num_gra = 1;
                                    while (true)
                                    {
                                        if (n_num_gra <= n_atr_otr)
                                        {
                                            if (atr_otro[Convert.ToInt32(n_num_gra)].n_cod_atr == n_atrib_gra)
                                            {
                                                atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo = atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo * (1 + n_val_grad / 100);
                                                n_val_temp = atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo;
                                                n_num_gra = n_num_gra + 1;
                                                n_val_temp = BOFunciones.Redondeo(n_val_temp);
                                                atr_otro[Convert.ToInt32(n_num_gra)].n_valor_calculo = n_val_temp;
                                            }
                                            else
                                            {
                                                n_num_gra = n_num_gra + 1;
                                            };
                                        }
                                        else
                                        {
                                            break;
                                        };
                                    };
                                    n_num_temp = n_num_temp + 1;
                                };
                                n_verifica = 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 1)
                            {
                                n_interes = n_saldo_act * n_tasa_interes;
                                n_interes = BOFunciones.Redondeo(n_interes);
                                n_capital = n_cuota - n_interes;
                                n_cap_ant = n_capital;
                            }
                            else
                            {
                                n_cap_ant = 0;
                            };
                            n_interes = 0;
                            n_num = 1;
                            while (n_num <= n_atr_fin)
                            {
                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                {
                                    if (n_num == n_atr_fin - 1)
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = n_tasa_interes / n_int_prop;
                                                n_atributos[n_num] = n_int_prop * n_dias_prop * (n_saldo_act - n_cap_ant) - n_interes;
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(n_tasa_interes * 100, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = ((BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant)) - n_interes;
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = ((n_saldo_act - n_cap_ant) * n_tasa_interes) - n_interes;
                                        };
                                    }
                                    else
                                    {
                                        if (b_int_prop && (n_tipo_pago == 4 || n_tipo_pago == 5))
                                        {
                                            if (s_calc_int == "1")
                                            {
                                                n_int_prop = n_dias;
                                                n_int_prop = atr_finan[n_num].n_tasa_calculo / n_int_prop;
                                                n_atributos[n_num] = n_int_prop / 100 * n_dias_prop * (n_saldo_act - n_cap_ant);
                                            }
                                            else
                                            {
                                                n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, n_tip_pago, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                n_atributos[n_num] = (BOFunciones.Power(n_int_prop / 100 + 1, n_dias_prop) - 1) * (n_saldo_act - n_cap_ant);
                                            };
                                        }
                                        else
                                        {
                                            n_atributos[n_num] = (atr_finan[n_num].n_tasa_calculo / 100) * (n_saldo_act - n_cap_ant);
                                        };
                                    };
                                    if (n_atributos[n_num] < 0)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_atributos[n_num] = BOFunciones.Redondeo(n_atributos[n_num]);
                                }
                                else
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_interes = n_interes + n_atributos[n_num];
                                n_num = n_num + 1;
                            };
                            //--Tipo de pago:   1: Anticipado,  2: Vencido
                            if (n_tip_pago == 2)
                            {
                                n_capital = n_cuota - n_interes;
                                if (n_capital < 0)
                                {
                                    n_num = 1;
                                    while (n_num <= n_atr_fin)
                                    {
                                        if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                        {
                                            n_atributos[n_num] = n_atributos[n_num] + n_capital;
                                        };
                                        n_num = n_num + 1;
                                    };
                                    n_interes = n_interes + n_capital;
                                    n_capital = 0;
                                };
                            };
                        };
                    }
                    else
                    {
                        MensajeConsola("El tipo de liquidación posee errado el tipo de cuota a ser calculado");
                        return -1;
                    };
                    //--Calcula el interes devuelto de interes corriente y lo abona al capital
                    if (BOFunciones.BuscarGeneral(930, 2) == "1" || BOFunciones.BuscarGeneral(930, 2) == "2")
                    {
                        b_exis_cap = true;
                    }
                    else
                    {
                        b_exis_cap = false;
                    };
                    if (b_exis_cap)
                    {
                        if (n_tip_pago == 1)
                        {
                            n_saldo_actual = n_saldo;
                            n_dias_cte = BOFunciones.FecDifDia(f_fecha_pago, rf_f_prox_pago, 1);
                            if (n_dias_cte > 0)
                            {
                                n_per_diaria = Convert.ToInt32(BOFunciones.CodPeriodicidadDiaria(1));
                                n_tasa_interes_diaria = 0;
                                n_num = 1;
                                while (n_num <= n_atr_fin)
                                {
                                    //--No se tiene en cuenta el interes de mora
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                    {
                                        n_tasa_interes_diaria = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 2, n_periodic, n_tip_amo, n_periodic, 2, n_per_diaria, n_tip_amo, n_per_diaria, 0);
                                    };
                                    n_num = n_num + 1;
                                };
                                n_tasa_interes_diaria = n_tasa_interes_diaria / 100;
                                n_tasa_interes_diaria = n_tasa_interes_diaria + 1;
                                n_tasa_interes_diaria = BOFunciones.Power(n_tasa_interes_diaria, n_dias_cte);
                                n_tasa_interes_diaria = (n_tasa_interes_diaria - 1);
                                n_val_int_tmp = ((n_saldo_act - n_capital) * n_tasa_interes_diaria);
                                n_val_int_tmp = BOFunciones.Redondeo(n_val_int_tmp);
                                n_capital = n_capital + n_val_int_tmp;
                            };
                        };
                        #endregion
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Si maneja gracia ajustar el valor de los atributos");
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region maneja gracia
                if (n_gracia == 1)
                {
                    n_cuo_credito = n_cuo_credito + 1;
                    if (n_cuo_credito <= n_cuotas_gracia)
                    {
                        if (n_tip_gracia == 4)
                        {
                            if (s_atr_gracia == "2")
                            {
                                n_capital = 0;
                            }
                            else if (s_atr_gracia == "3")
                            {
                                n_num = 1;
                                while (n_num <= rn_num_atr)
                                {
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_num = n_num + 1;
                                };
                            }
                            else if (s_atr_gracia == "1")
                            {
                                n_num = 1;
                                while (n_num <= rn_num_atr)
                                {
                                    if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                                    {
                                        n_atributos[n_num] = 0;
                                    };
                                    n_capital = 0;
                                    n_num = n_num + 1;
                                };
                            };
                        };
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Elimina atributos que no se cruzan");
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region atributos cruce
                if (g_b_cruce)
                {
                    n_num = 1;
                    while (n_num < rn_num_atr)
                    {
                        if (n_atributos[n_num] != 0)
                        {
                            n_pos = 1;
                            while (n_pos <= n_num_cruza)
                            {
                                if (n_no_cruza[n_pos] == rn_cod_atributos[n_num])
                                {
                                    n_atributos[n_num] = 0;
                                };
                                n_pos = n_pos + 1;
                            };
                        };
                        n_num = n_num + 1;
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Verificar que el capital no exceda el saldo");
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                if (n_saldo_act < n_capital)
                {
                    n_capital = n_saldo_act;
                };

                string fechaString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Inserta los Valores calculado de Interes y de Capital " + fechaString + " " + n_atr_fin);
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region valor calculados en amortiza
                if (!b_existe)
                {
                    Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), 1, Convert.ToDecimal(n_capital), 0, n_saldo_act, "0");
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                        {
                            Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), Convert.ToInt32(atr_finan[n_num].n_cod_atr), Convert.ToDecimal(n_atributos[n_num]), Convert.ToDecimal(atr_finan[n_num].n_tasa_calculo), n_saldo_act, "0");
                        };
                        n_num = n_num + 1;
                    };
                };
                #endregion
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Calcula el interes de mora, teniendo en cuenta los cambios de forma de pago");
                //   ------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
                #region calculo interés de mora
                n_intcuototaux = 0;
                b_ya_cob_mora = false;
                n_for_pag_real = Convert.ToInt32(n_for_pag);
                f_fecha_pago_real = f_fecha_pago;
                rf_f_prox_pago_real = rf_f_prox_pago;
                f_prox_pag_real = f_prox_pag;

                var listaCambiosHistoricos = DApackage.ConsultarHistoricoCambiosCredito(n_radic, rf_f_prox_pago, usuario);

                if (listaCambiosHistoricos == null)
                {
                    Mensaje_Error("Ocurrio un error al consultar los cambios historicos de este credito!.");
                    return null;
                }

                //   Open pConsulta For Select fechacambio, valor_ant, valor_nue From histcambios_cre Where tipo_cambio = 313 and numero_radicacion = n_radic and fechacambio > rf_f_prox_pago Order By 1;  
                foreach (var cambio in listaCambiosHistoricos)
                {
                    f_fecha_cambio = cambio.Item1;
                    s_val_ant = cambio.Item2;
                    s_val_nue = cambio.Item3;

                    #region calculo de la mora
                    if (rf_f_prox_pago == null)
                    {
                        break;
                    };
                    if (!(f_fecha_cambio != null || !b_ya_cob_mora))
                    {
                        break;
                    };
                    b_ya_cob_mora = true;
                    //-- Calcula el interés de mora, si se cobra.Valida que cobre mora, que exista atributo de mora,
                    //--que la fecha de pago sea menor a la de la cuota y que no haya superado el periodo para cobro sobre saldo insoluto
                    if (s_cob_mor == "1" && n_pos_mora != -1 && f_fecha_pago > rf_f_prox_pago && (n_for_pag == 1 || (n_for_pag == 2 && !b_exis_mora)) && n_num_cuo_pag_aux <= 0)
                    {
                        if (f_fecha_pago > f_fec_habil)
                        {
                            //--Determina si se cobra sobre  1:saldos insolutos o sobre 2:atributos
                            n_dias_tf_nomina = 0;
                            if (n_tip_cuota == 1 && n_for_pag == 2)
                            {
                                n_dias_tf_nomina = Convert.ToInt32(BOFunciones.To_Number(BOFunciones.BuscarGeneral(1060, 2)));
                            };
                            if (n_tip_mor == 1)
                            {
                                #region --Calcula en número de días de mora.Se ajusta para que lo tome desde la fecha de proximo pago.
                                f_mora = null;
                                f_mora = rf_f_prox_pago;
                                if (b_exis_cal && n_calendario == 1)
                                {
                                    n_dias_mora = Convert.ToInt32(BOFunciones.FecDifDia(f_mora, f_fecha_pago, 2));
                                    n_dias_mora_lleva = Convert.ToInt32(BOFunciones.FecDifDia(f_prox_pag, rf_f_prox_pago, 2));
                                }
                                else
                                {
                                    n_dias_mora = Convert.ToInt32(BOFunciones.FecDifDia(f_mora, f_fecha_pago, Convert.ToInt32(n_tipo_cal)));
                                    n_dias_mora_lleva = Convert.ToInt32(BOFunciones.FecDifDia(f_prox_pag, rf_f_prox_pago, Convert.ToInt32(n_tipo_cal)));
                                };
                                if (n_tip_cuota == 1)
                                {
                                    n_dias_mora = n_dias_mora - n_dias_tf_nomina;
                                    n_dias_mora_lleva = n_dias_mora_lleva - n_dias_tf_nomina;
                                };
                                if (n_dias_mora <= n_dias_gracia_mora)
                                {
                                    n_dias_mora = 0;
                                };
                                if (n_dias_mora > 0)
                                {
                                    #region calculo de la mora
                                    if (f_mora > rf_f_prox_pago)
                                    {
                                        f_fec_prox = f_mora;
                                    }
                                    else
                                    {
                                        f_fec_prox = rf_f_prox_pago;
                                    };
                                    n_dias_mora_temp = n_dias_mora;
                                    if (atr_finan[Convert.ToInt32(n_pos_mora)].n_cod_atr == n_atr_mora)
                                    {
                                        n_contador_ = 0;
                                        while (n_dias_mora_temp >= 1 && n_contador_ <= 9999)
                                        {
                                            if (atr_finan[Convert.ToInt32(n_pos_mora)].s_tip_cal == "5")
                                            {
                                                //--Determinar la tasa histórico vigente a la fecha de pago de la cuota
                                                bool encontroFecha = DApackage.DeterminarFechaInicioYFinTasaHistorica(atr_finan[n_pos_mora.Value].n_tip_his, f_fec_prox, ref f_fec_ini, ref f_fec_fin, usuario);

                                                if (!encontroFecha)
                                                {
                                                    f_fec_ini = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                    f_fec_fin = f_fecha_pago;
                                                    n_dias_dif = n_dias_mora_temp;
                                                };
                                                if (f_fec_ini <= f_fec_prox && (f_fec_prox == rf_f_prox_pago || f_fec_prox == f_mora))
                                                {
                                                    f_fec_ini = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                    f_fec_ini = BOFunciones.FecSumDia(f_fec_prox, 1, Convert.ToInt32(n_tipo_cal));
                                                }
                                                else if (f_fec_ini <= f_fec_prox)
                                                {
                                                    f_fec_ini = f_fec_prox;
                                                };
                                                if (f_fec_fin > f_fecha_pago)
                                                {
                                                    f_fec_fin = f_fecha_pago;
                                                    if (f_fec_ini > f_fec_fin)
                                                    {
                                                        f_fec_ini = f_fec_fin;
                                                    };
                                                };
                                                //--Calcular los días del histórico de mora
                                                if (b_exis_cal && n_calendario == 1)
                                                {
                                                    n_dias_dif = BOFunciones.FecDifDia(f_fec_ini, Convert.ToDateTime(f_fec_fin).AddDays(1), 2);
                                                }
                                                else
                                                {
                                                    n_dias_dif = BOFunciones.FecDifDia(f_fec_ini, BOFunciones.FecSumDia(f_fec_fin, 1, Convert.ToInt32(n_tipo_cal)), Convert.ToInt32(n_tipo_cal));
                                                };
                                                if (f_fec_prox > f_fecha_pago)
                                                {
                                                    n_dias_dif = n_dias_mora_temp;
                                                };
                                            }
                                            else
                                            {
                                                f_fec_fin = f_fecha_pago;
                                                f_fec_ini = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                n_dias_dif = n_dias_mora_temp;
                                            };
                                            //--Determinar los días de mora a cobrar
                                            if (n_dias_dif < n_dias_mora_temp)
                                            {
                                                n_dias_mora = Convert.ToInt32(n_dias_dif);
                                            }
                                            else
                                            {
                                                n_dias_mora = n_dias_mora_temp;
                                            };
                                            //--Mirar si ya se cobraron moras para esa fecha
                                            if (Buscar_Mora(rf_f_prox_pago, n_atr_mora, f_fec_ini, f_fec_fin, ref n_valor_retorno, ref n_pos_reg_mora))
                                            {
                                                n_val_mora = n_valor_retorno;
                                                n_val_ant = n_atributos[Convert.ToInt32(n_pos_mora)];
                                                n_atributos[Convert.ToInt32(n_pos_mora)] = n_atributos[Convert.ToInt32(n_pos_mora)] + n_val_mora;
                                            }
                                            else
                                            {
                                                atr_finan[Convert.ToInt32(n_pos_mora)].Conv_Tasa(n_per_dia, 2, f_fec_prox, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                                                s_usura = BOFunciones.BuscarGeneral(681, 2);
                                                if (BOFunciones.BuscarGeneral(681, 1) != null)
                                                {
                                                    b_existe1 = true;
                                                }
                                                else
                                                {
                                                    b_existe1 = false;
                                                };
                                                if (b_existe1)
                                                {
                                                    n_usura = n_tipo_tasa_usura;
                                                    n_tasa_usura = 0;
                                                    g_f_usura = f_fec_prox;

                                                    DApackage.DeterminarValorYTipoTasaHistorica(n_usura, g_f_usura, ref n_tasa_usura, ref n_tipo_tasa, usuario);

                                                    if (n_tasa_usura != 0)
                                                    {
                                                        DApackage.ConTipoTasa(n_tipo_tasa, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);

                                                        if (s_calc_int == "1")
                                                        {
                                                            n_int_prop = n_dias_per_cre;
                                                            n_tasa_calculo = n_tasa_usura / n_int_prop;
                                                        }
                                                        else
                                                        {
                                                            n_tasa_calculo = BOFunciones.CalTasMod(n_tasa_usura, BOFunciones.To_Number(s_efe_nom), n_per, BOFunciones.To_Number(s_mod), n_mod_per, 1, n_per_dia, n_tip_pago, n_per_dia, 0);
                                                        };
                                                        if (n_tasa_calculo < n_tasa_mora && n_tasa_calculo > 0)
                                                        {
                                                            n_tasa_mora = n_tasa_calculo;
                                                            atr_finan[Convert.ToInt32(n_pos_mora)].n_tasa_calculo = n_tasa_calculo;
                                                        };
                                                    };
                                                };
                                                n_val_ant = n_atributos[Convert.ToInt32(n_pos_mora)];
                                                //--Determina si es sobre saldo insoluto o no
                                                if (n_dias_mora_lleva + n_dias_mora > BOFunciones.To_Number(s_dias_mora_saldo))
                                                {
                                                    n_dias_insoluto = (n_dias_mora_lleva + n_dias_mora) - BOFunciones.To_Number(s_dias_mora_saldo);
                                                    if (n_dias_insoluto > n_dias_mora)
                                                    {
                                                        n_dias_insoluto = n_dias_mora;
                                                    };
                                                }
                                                else
                                                {
                                                    n_dias_insoluto = 0;
                                                };
                                                //--Calcula sobre los intereses financiados
                                                n_tasa_interes_mora = 0;
                                                if (s_calc_int_mora == "1")
                                                {
                                                    n_tasa_interes_mora = n_tasa_mora / 100 * (n_dias_mora - n_dias_insoluto);
                                                }
                                                else
                                                {
                                                    n_tasa_interes_mora = BOFunciones.Power(n_tasa_mora / 100 + 1, (n_dias_mora - n_dias_insoluto)) - 1;
                                                };
                                                if (n_dias_mora > n_dias_insoluto)
                                                {
                                                    n_num = 1;
                                                    while (n_num <= n_atr_fin)
                                                    {
                                                        if (atr_finan[n_num].n_cod_atr != n_atr_mora && atr_finan[n_num].n_cob_mor == 1)
                                                        {
                                                            //--Determina si es interes 1:simple o 2:Compuesto
                                                            if (s_calc_int_mora == "1")
                                                            {
                                                                n_val_mora = n_tasa_mora / 100 * (n_dias_mora - n_dias_insoluto) * n_atributos[n_num];
                                                            }
                                                            else
                                                            {
                                                                n_val_mora = (BOFunciones.Power(n_tasa_mora / 100 + 1, (n_dias_mora - n_dias_insoluto)) - 1) * n_atributos[n_num];
                                                            };
                                                            if (n_val_mora < 0)
                                                            {
                                                                n_val_mora = 0;
                                                            };
                                                            //--Inserta los valores de mora calculado
                                                            n_val_mora = BOFunciones.Redondeo(n_val_mora);
                                                            Insertar_Mora(Convert.ToDateTime(rf_f_prox_pago), atr_finan[n_num].n_cod_atr, n_val_mora, n_tasa_interes_mora, n_atributos[n_num], f_fec_ini, f_fec_fin, n_dias_mora, "0");
                                                        }
                                                        else
                                                        {
                                                            n_val_mora = 0;
                                                        };
                                                        //--Actualiza total de interes causado
                                                        //--Determina si se cobra la mora o se crea como un pendiente para posteriormente cobrarla
                                                        if (s_mor_sig == "1")
                                                        {
                                                            n_mor_sig = n_mor_sig + n_val_mora;
                                                        }
                                                        else
                                                        {
                                                            n_atributos[Convert.ToInt32(n_pos_mora)] = n_atributos[Convert.ToInt32(n_pos_mora)] + n_val_mora;
                                                        };
                                                        n_num = n_num + 1;
                                                    };
                                                };
                                                //--Calcula la mora sobre el capital
                                                //-- Determina si es interes 1:simple o 2:Compuesto
                                                if (s_calc_int_mora == "1")
                                                {
                                                    n_val_mora = n_tasa_mora / 100 * (n_dias_mora - n_dias_insoluto) * n_capital;
                                                }
                                                else
                                                {
                                                    n_val_mora = (BOFunciones.Power(n_tasa_mora / 100 + 1, (n_dias_mora - n_dias_insoluto)) - 1) * n_capital;
                                                };
                                                //--Calculo mora sobre saldos insolutos
                                                if (n_dias_insoluto > 0 && n_num_cuo_pag_aux <= 0)
                                                {
                                                    n_num_cuo_pag_aux = -1;
                                                    if (n_dias_mora_cuotas + n_dias >= BOFunciones.To_Number(s_dias_mora_saldo))
                                                    {
                                                        n_num_cuo_pag_aux = -1;
                                                    };
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_val_mor_aux = (n_tasa_mora / 100 * n_dias_insoluto * n_saldo);
                                                        n_val_mor_aux = BOFunciones.Redondeo(n_val_mor_aux);
                                                    }
                                                    else
                                                    {
                                                        n_val_mor_aux = (BOFunciones.Power(n_tasa_mora / 100 + 1, n_dias_insoluto) - 1) * n_saldo;
                                                        n_val_mor_aux = BOFunciones.Redondeo(n_val_mor_aux);
                                                    };
                                                    n_val_mora = n_val_mora + n_val_mor_aux;
                                                };
                                                n_val_mora = BOFunciones.Redondeo(n_val_mora);
                                                MensajeConsola("Insertando valores en mora");
                                                Insertar_Mora(Convert.ToDateTime(rf_f_prox_pago), 1, n_val_mora, n_tasa_interes_mora, n_capital, f_fec_ini, f_fec_fin, n_dias_mora, "0");
                                                //--Mora en el siguiente pago
                                                if (s_mor_sig == "1")
                                                {
                                                    n_mor_sig = n_mor_sig + n_val_mora;
                                                    n_mor_sig = BOFunciones.Redondeo(n_mor_sig);
                                                }
                                                else
                                                {
                                                    n_atributos[Convert.ToInt32(n_pos_mora)] = n_atributos[Convert.ToInt32(n_pos_mora)] + n_val_mora;
                                                    n_atributos[Convert.ToInt32(n_pos_mora)] = BOFunciones.Redondeo(n_atributos[Convert.ToInt32(n_pos_mora)]);
                                                };
                                            };
                                            if (n_val_ant <= n_atributos[Convert.ToInt32(n_pos_mora)])
                                            {
                                                n_interes = n_interes + (n_atributos[Convert.ToInt32(n_pos_mora)] - n_val_ant);
                                            }
                                            else
                                            {
                                                n_interes = n_interes + n_atributos[Convert.ToInt32(n_pos_mora)];
                                            };
                                            f_fec_prox = Convert.ToDateTime(f_fec_fin).AddDays(1);
                                            n_dias_mora_temp = n_dias_mora_temp - n_dias_mora;
                                            n_dias_mora_lleva = n_dias_mora_lleva + n_dias_mora;
                                        };
                                        n_dias_mora_cuotas = Convert.ToInt32(n_dias_mora_cuotas + n_dias);
                                        if (Buscar_Amortiza(rf_f_prox_pago, n_atr_mora, ref n_valor_retorno, ref n_pos_reg_amortiza))
                                        {
                                            Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), n_atributos[Convert.ToInt32(n_pos_mora)], n_atributos[Convert.ToInt32(n_pos_mora)], 0, 0, "1");
                                        }
                                        else
                                        {
                                            Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), Convert.ToInt32(n_atr_mora), Convert.ToDecimal(n_atributos[Convert.ToInt32(n_pos_mora)]), Convert.ToDecimal(n_tasa_mora), 0, "0");
                                        };
                                    };
                                    #endregion
                                };
                                #endregion
                            }
                            else
                            {
                                MensajeConsola("El manejo de mora seleccionado no se encuentra definido, verifique parámetros");
                            };
                        };
                        if (true) //pConsulta%FOUND)
                        {
                            rf_f_prox_pago = f_fecha_cambio;
                            f_prox_pag = f_fecha_cambio;
                            n_for_pag = BOFunciones.To_Number(s_val_nue);
                        };
                    };
                    #endregion
                };

                if (n_num_cuo_pag_aux == -1)
                {
                    n_num_cuo_pag_aux = 1;
                };
                n_for_pag = n_for_pag_real;
                f_fecha_pago = f_fecha_pago_real;
                rf_f_prox_pago = rf_f_prox_pago_real;
                f_prox_pag = f_prox_pag_real;
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Calculando valores financiados individuales del crèdito");
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                #region valores financiados
                n_num = 1;
                n_pos = n_atr_fin + 1;
                n_otros = 0;
                if (b_existe)
                {
                    #region--Determina los valores parciales adeudados
                    while (n_num <= n_atr_otr && n_pos <= rn_num_atr)
                    {
                        if ((atr_otro[n_num].n_signo == 3 || atr_otro[n_num].n_signo == 5 || atr_otro[n_num].n_signo == 6))
                        {
                            if (!(rn_cod_atributos[n_pos] == n_atr_PREJUR))
                            {
                                n_atributos[n_pos] = 0;
                                bResultado = Buscar_Amortiza(rf_f_prox_pago, Convert.ToInt32(rn_cod_atributos[n_pos]), ref n_atributos[n_pos], ref n_pos_reg_amortiza);
                                if (n_atributos[n_pos] == null)
                                {
                                    n_atributos[n_pos] = 0;
                                };
                                n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                            };
                            n_pos = n_pos + 1;
                        };
                        n_num = n_num + 1;
                    };
                    #endregion
                }
                else
                {
                    #region--Determina el valor total de cuota a cancelar
                    while (n_num <= n_atr_otr && n_pos <= rn_num_atr)
                    {
                        if (atr_otro[n_num].n_signo == 3 || atr_otro[n_num].n_signo == 4 || atr_otro[n_num].n_signo == 5 || atr_otro[n_num].n_signo == 6)
                        {
                            #region calculando atributos decontados
                            if (atr_otro[n_num].n_signo == 6)
                            {
                                #region atributos sumado a la cuota
                                n_atr_depende = null;

                                DApackage.ConsultarDependeDeAtributos(atr_otro[n_num].n_cod_atr, ref n_atr_depende, usuario);

                                if (n_num_cuo_act + n_num_cuo_pag != 0)
                                {
                                    n_atributos[n_pos] = 0;
                                }
                                else
                                {
                                    n_atributos[n_pos] = atr_otro[n_num].n_valor_calculo;
                                };
                                if (atr_otro[n_num].n_tip_liq == 16 && n_atr_depende == n_atr_corr && b_int_prop && n_tipo_pago == 4)
                                {
                                    n_atributos[n_pos] = n_atributos[n_pos] / n_dias * n_dias_prop;
                                };
                                n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToDecimal(n_atributos[n_pos]), Convert.ToDecimal(atr_otro[n_num].n_valor), n_saldo_act, "0");
                                n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                                n_pos = n_pos + 1;
                                #endregion
                            }
                            else
                            {
                                #region--Determinar código de atributo
                                n_valor_atr = BOFunciones.To_Number(BOFunciones.BuscarGeneral(952, 2));
                                if (n_valor_atr != null && n_valor_atr != 0)
                                {
                                    b_existe_cat = true;
                                }
                                else
                                {
                                    b_existe_cat = false;
                                };
                                #endregion
                                #region --Determina si depende de otros atributos
                                n_atr_depende = null;

                                DApackage.ConsultarDependeDeAtributos(atr_otro[n_num].n_cod_atr, ref n_atr_depende, usuario);

                                #endregion
                                #region --Se ajusta para que se calcule el valor cuando el atributo depende del saldo.
                                if ((atr_otro[n_num].n_tip_des == 2 || atr_otro[n_num].n_tip_des == 3) && (atr_otro[n_num].n_tip_liq == 6 || atr_otro[n_num].n_tip_liq == 13 || atr_otro[n_num].n_tip_liq == 17))
                                {
                                    #region tipo de liquidacion
                                    if (atr_otro[n_num].n_tip_liq == 17)
                                    {
                                        //--Se ajustò para que cuando el atributo es dependiente entonces calcule sobre el saldo acumulado.
                                        DApackage.ConsultarDependeDeAtributos(atr_otro[n_num].n_cod_atr, ref n_cod_atr_dep, usuario);

                                        if (n_cod_atr_dep != 0 && n_cod_atr_dep != null)
                                        {
                                            atr_otro[n_num].Cal_Valor(0, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_val_aux1, 0, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                            if (!bResultado)
                                                return -1;
                                        };
                                    }
                                    else
                                    {
                                        atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_val_aux1, n_saldo_act, n_val_interes, n_valorCatg, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                        if (!bResultado)
                                        {
                                            return -1;
                                        };
                                    };
                                    #endregion
                                }
                                else
                                {
                                    #region calculando
                                    n_cont = 1;
                                    while (n_cont < n_atr_fin)
                                    {
                                        if (atr_finan[n_cont].n_cod_atr == n_atr_corr)
                                        {
                                            n_val_interes = atr_finan[n_cont].n_tasa_calculo;
                                            break;
                                        };
                                        n_cont = n_cont + 1;
                                    };
                                    atr_otro[n_num].Cal_Valor(n_monto, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_val_aux1, n_saldo_act, n_val_interes, n_capital + n_interes + n_otros, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                    if (!bResultado)
                                    {
                                        return -1;
                                    };
                                    #endregion
                                };
                                #endregion
                            };
                            if (n_otr > 0 && b_existe_cat)
                            {
                                if (atr_otro[n_num].n_cod_atr == n_valor_atr)
                                {
                                    n_atributos[n_pos] = n_otr;
                                    n_otr = 0;
                                };
                            }
                            else
                            {
                                if (n_num_cuotas != 0)
                                {
                                    #region calculando
                                    if (atr_otro[n_num].n_signo != 6 && n_num_cuotas != 0)
                                    {
                                        if (atr_otro[n_num].n_num_cuotas != null && atr_otro[n_num].n_num_cuotas > 0)
                                        {
                                            if (BOFunciones.BuscarGeneral(1196, 1) == "1" && b_LeyMiPyme)
                                            {
                                                if (atr_otro[n_num].n_valor_pagos[Convert.ToInt32(n_cuo_pag + n_num_cuo_pag + 1)] != null)
                                                {
                                                    n_atributos[n_pos] = BOFunciones.NVL(atr_otro[n_num].n_valor_pagos[Convert.ToInt32(n_cuo_pag + n_num_cuo_pag + 1)], 0);
                                                }
                                                else
                                                {
                                                    n_atributos[n_pos] = 0;
                                                };
                                            }
                                            else
                                            {
                                                if (atr_otro[n_num].n_num_cuotas > n_num_cuo_act + n_num_cuo_pag)
                                                {
                                                    n_atributos[n_pos] = BOFunciones.NVL(atr_otro[n_num].n_valor_calculo / atr_otro[n_num].n_num_cuotas, 0);
                                                    n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                                }
                                                else
                                                {
                                                    n_atributos[n_pos] = 0;
                                                };
                                            };
                                        }
                                        else
                                        {
                                            if (atr_otro[n_num].n_cod_atr == n_atr_JURID)
                                            {
                                                atr_otro[n_num].Cal_Valor(n_capital + n_interes + n_otros, n_plazo_mes, n_num_cuotas, ref n_val_aux, ref n_val_aux1, n_saldo_act, n_val_interes, atr_otro[n_num].n_valor, n_valfactor, n_vrComer, n_cuota, 0, n_tipo_prod, n_num_codeu, ref bResultado);
                                            };
                                            if (atr_otro[n_num].n_signo != 4 && atr_otro[n_num].n_signo != 5)
                                            {
                                                n_atributos[n_pos] = atr_otro[n_num].n_valor_calculo / n_num_cuotas;
                                            }
                                            else
                                            {
                                                n_atributos[n_pos] = atr_otro[n_num].n_valor_calculo;
                                            };
                                        };
                                    }
                                    else
                                    {
                                        n_atributos[n_pos] = atr_otro[n_num].n_valor_calculo;
                                    };
                                    #endregion
                                };
                            };
                            if (atr_otro[n_num].n_tip_liq == 16 && n_atr_depende == n_atr_corr && b_int_prop && n_tipo_pago == 4)
                            {
                                n_atributos[n_pos] = n_atributos[n_pos] / n_dias * n_dias_prop;
                            };
                            n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                            Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToInt32(n_atributos[n_pos]), Convert.ToDecimal(atr_otro[n_num].n_valor), n_saldo_act, "0");
                            n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                            n_pos = n_pos + 1;
                            #endregion
                        };
                        n_num = n_num + 1;
                    };
                    #endregion
                }
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Calculando valor de cobro prejuridico");
                //   ----------------------------------------------------------------------------------------------------------------------------------------       
                #region prejuridico
                if ((b_prejuridico == true && (rf_f_prox_pago <= f_fecha_ult_cierre && f_fecha_ult_cierre != null)))
                {
                    b_yagenero_prejuridico = false;
                    if (b_existe)
                    {
                        n_num = 1;
                        n_pos = n_atr_fin + 1;
                        //--Determina los valores parciales adeudados
                        while (n_num <= n_atr_otr && n_pos <= rn_num_atr)
                        {
                            if (atr_otro[n_num].n_signo == 3 || atr_otro[n_num].n_signo == 4 || atr_otro[n_num].n_signo == 6)
                            {
                                if (atr_otro[n_num].n_cod_atr == n_atr_PREJUR)
                                {
                                    n_atributos[n_pos] = 0;
                                    bResultado = Buscar_Amortiza(rf_f_prox_pago, Convert.ToInt32(rn_cod_atributos[n_pos]), ref n_atributos[n_pos], ref n_pos_reg_amortiza);
                                    if (n_atributos[n_pos] == null)
                                    {
                                        n_atributos[n_pos] = 0;
                                    }
                                    else
                                    {
                                        if (bResultado == true)
                                        {
                                            b_yagenero_prejuridico = true;
                                            n_valor_prejuridico = BOFunciones.NVL(BOFunciones.Redondeo(((n_capital + n_interes + n_otros) * atr_otro[n_num].n_valor) / 100), 0);
                                            n_valor_prejuridico_pagado = 0;

                                            DApackage.ConsultarValorPrejuridicoPagado(n_radic, rf_f_prox_pago, n_atr_PREJUR, ref n_valor_prejuridico_pagado, usuario);
                                            n_valor_prejuridico_pagado = BOFunciones.NVL(n_valor_prejuridico_pagado, 0);
                                            if ((n_valor_prejuridico > n_valor_prejuridico_pagado && n_valor_prejuridico > 0 && (n_atributos[n_pos] == 0 || rf_f_prox_pago > f_prox_pag)))
                                            {
                                                if (n_valor_prejuridico_pagado < 0)
                                                {

                                                    n_valor_prejuridico_pagado = 0;
                                                };
                                                n_atributos[n_pos] = n_valor_prejuridico - n_valor_prejuridico_pagado;
                                                Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), n_atributos[n_pos], n_atributos[n_pos], 0, 0, "0");
                                            };
                                        };
                                    };
                                    n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                                };
                                n_pos = n_pos + 1;
                            };
                            n_num = n_num + 1;
                        };
                    };
                    if (!b_existe || !b_yagenero_prejuridico)
                    {
                        n_num = 1;
                        n_pos = n_atr_fin + 1;
                        while (n_num <= n_atr_otr && n_pos <= rn_num_atr)
                        {
                            if (atr_otro[n_num].n_signo == 3 || atr_otro[n_num].n_signo == 4 || atr_otro[n_num].n_signo == 6)
                            {
                                if (atr_otro[n_num].n_cod_atr == n_atr_PREJUR)
                                {
                                    n_atributos[n_pos] = BOFunciones.Redondeo(((n_capital + n_interes + n_otros) * atr_otro[n_num].n_valor) / 100);
                                    Insertar_Amortiza(Convert.ToDateTime(rf_f_prox_pago), Convert.ToInt32(atr_otro[n_num].n_cod_atr), Convert.ToDecimal(n_atributos[n_pos]), Convert.ToDecimal(atr_otro[n_num].n_valor), n_saldo_act, "0");
                                    n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                                };
                                n_pos = n_pos + 1;
                            };
                            n_num = n_num + 1;
                        };
                    };
                };
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Calculando valores de garantías comunitarias");
                //   ----------------------------------------------------------------------------------------------------------------------------------------              
                #region garantias comunitarias
                if (n_num_cuo_pag == 0)
                {
                    n_pos = 1;
                    while (n_pos <= rn_num_atr)
                    {
                        if (rn_cod_atributos[n_pos] == n_atr_CTACOB)
                        {
                            n_atributos[n_pos] = 0;

                            DApackage.ConsultarCuentaPorCobrarAtributosCredito(n_radic, ref n_atributos[n_pos], usuario);
                            if (n_atributos[n_pos] == null)
                            {
                                n_atributos[n_pos] = 0;
                            };
                            n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                        };
                        n_pos = n_pos + 1;
                    };
                };
                n_otros = BOFunciones.NVL(n_otros, 0);
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Actualiza el saldo del credito " + n_saldo_act + " " + n_capital);
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                #region actualiza saldo
                n_saldo_act = n_saldo_act - n_capital;
                if (n_saldo_act < 0)
                {
                    n_saldo_act = 0;
                    n_capital = n_capital + n_saldo_act;
                };
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Determina si el valor de interes corriente debe ser cero por > 90 dias");  
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                #region creditos acelerados
                if (!(n_valcueord <= BOFunciones.To_Number(s_dias_mora_saldo) && (!(b_exis_acelerado && BOFunciones.To_Number(s_dias_mora_saldo) == 0) || BOFunciones.To_Number(s_dias_mora_saldo) != 0)) && n_tip_mor == 1)
                {
                    n_num = 1;
                    while (n_num <= rn_num_atr)
                    {
                        if (rn_cod_atributos[n_num] == n_atr_corr)
                        {
                            n_interes = n_interes - n_atributos[n_num];
                            n_atributos[n_num] = 0;
                            if (Buscar_Amortiza(rf_f_prox_pago, n_atr_corr, ref n_valor_retorno, ref n_pos_reg_amortiza))
                            {
                                Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), 0, 0, 0, 0, "1");
                            };
                        };
                        n_num = n_num + 1;
                    };
                };
                #endregion
                //   ----------------------------------------------------------------------------------------------------------------------------------------  
                //   -- Determina el valor total de cuota a cancelar  
                //   ----------------------------------------------------------------------------------------------------------------------------------------      
                #region total cuota a cancelar
                MensajeConsola("Si es el seguro de cartera cobra todo el periodo completo cuando es PAGO TOTAL");
                if (n_tipo_pago == 4)
                {
                    n_num = 1;
                    n_pos = n_atr_fin + 1;
                    while (n_num <= n_atr_otr && n_pos <= rn_num_atr)
                    {
                        if (atr_otro[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 2)) && atr_otro[n_num].n_signo == 4 && (f_fec_ant <= f_fecha_pago && rf_f_prox_pago >= f_fecha_pago))
                        {
                            //--Quitar lo ya calculado para colocar el valor total
                            if (b_int_prop)
                            {
                                if (n_atributos[n_pos] != 0 && n_otros >= n_atributos[n_pos])
                                {
                                    n_otros = n_otros - n_atributos[n_pos];
                                };
                                n_int_prop = atr_otro[n_num].n_valor;
                                n_atributos[n_pos] = (n_int_prop * (n_saldo_act + n_capital));
                                if (n_tip_cuota == 1 && atr_otro[n_num].n_cod_atr == BOFunciones.To_Number(BOFunciones.BuscarGeneral(1679, 2)))
                                {
                                    n_atributos[n_pos] = n_atributos[n_pos] * n_dias_prop / 30;
                                };
                                n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                n_otros = n_otros + n_atributos[n_pos];
                            };
                            n_pos = n_pos + 1;
                        };
                        n_num = n_num + 1;
                    };
                };
                #endregion
                //   ---------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("Si no pudo distribuir retorna error");  
                //   ---------------------------------------------------------------------------------------------------------------------------------------  
                if ((n_capital == 0 || n_capital == null) && (n_interes == 0 || n_interes == null) && (n_otros == 0 || n_otros == null))
                {
                    if (!((b_exis_acelerado && f_fecha_pago != f_fecha_pago1) || n_tipo_pago == 4))
                    {
                        break;
                    };
                };
                //   ---------------------------------------------------------------------------------------------------------------------------------------  
                //   Mensaje("el tipo de pago es por valor verifica atributos, También para pagos a FECHA de CREDITOS");  
                //   ---------------------------------------------------------------------------------------------------------------------------------------  
                if ((n_tipo_pago == 1 || n_tipo_pago == 4 || n_tipo_pago == 5 || (n_tipo_pago == 1 && n_tipo_pago_temp == 6)) || ((n_tipo_pago == 2) && (n_tipo_prod == 2 || n_tipo_prod == 1) && n_tipo_pago_temp != 6) && n_tipo_pago_temp != 7)
                {
                    //---Si no alcanza a aplicar todo distribuye lo que alcance según prioridades
                    //---- 2: Distribuye por cada atributo, de acuerdo a la inicialización
                    //---- != 2: Distribuye de acuerdo a las prioridades de los atributos y al valor pagado
                    if (s_tipo_prio == "2" && n_man_pago_atr == 2)
                    {
                        #region determinar prioridades
                        //-- Inicializa las variables para determinar el valor a cancelar y el faltante
                        n_capital_aux = n_capital;
                        n_capital = 0;
                        n_interes_aux = n_interes;
                        n_interes = 0;
                        n_otros = 0;
                        n_num = 1;
                        while (n_num <= rn_num_atr)
                        {
                            n_atributos_aux[n_num] = n_atributos[n_num];
                            n_atributos[n_num] = 0;
                            n_num = n_num + 1;
                        };
                        n_num = 1;
                        while (n_num <= n_num_atr_pago)
                        {
                            if (n_cod_atr_pago[n_num] == 1)
                            {
                                if (n_val_atr_pago[n_num] >= n_capital_aux && n_capital_aux != 0)
                                {
                                    n_val_atr_pago[n_num] = n_val_atr_pago[n_num] - n_capital_aux;
                                    n_capital = n_capital_aux;
                                }
                                else
                                {
                                    if (n_capital_aux == 0)
                                    {
                                        n_capital = n_capital_aux;
                                    }
                                    else
                                    {
                                        n_capital = n_val_atr_pago[n_num];
                                        n_val_atr_pago[n_num] = 0;
                                    };
                                };
                            }
                            else
                            {
                                n_pos = 1;
                                while (n_pos <= rn_num_atr)
                                {
                                    if (rn_cod_atributos[n_pos] == n_cod_atr_pago[n_num])
                                    {
                                        if (n_val_atr_pago[n_num] >= n_atributos_aux[n_pos] && n_atributos_aux[n_pos] != 0)
                                        {
                                            n_val_atr_pago[n_num] = n_val_atr_pago[n_num] - n_atributos_aux[n_pos];
                                            n_atributos[n_pos] = n_atributos_aux[n_pos];
                                        }
                                        else
                                        {
                                            if (n_atributos_aux[n_pos] == 0)
                                            {
                                                n_atributos[n_pos] = n_atributos_aux[n_pos];
                                            }
                                            else
                                            {
                                                n_atributos[n_pos] = n_val_atr_pago[n_num];
                                                n_val_atr_pago[n_num] = 0;
                                            };
                                        };
                                        n_interes = n_interes + n_atributos[n_pos];
                                    };
                                    n_pos = n_pos + 1;
                                };
                            };
                            n_num = n_num + 1;
                        };
                        if (n_capital == null)
                        {
                            n_capital = 0;
                        };
                        if (n_interes == null)
                        {
                            n_interes = 0;
                        };
                        if (n_otros == null)
                        {
                            n_otros = 0;
                        };
                        n_excedente = n_excedente - (n_capital + n_interes + n_otros);
                        //       -- Ajusta los valores si se digito mas del valor total calculado
                        if (n_saldo_act == 0 && n_excedente > 0 && n_tipo_pago == 1)
                        {
                            n_num = 1;
                            while (n_num <= n_num_atr_pago)
                            {
                                if (n_cod_atr_pago[n_num] != 1 && n_val_atr_pago[n_num] > 0)
                                {
                                    n_pos = 1;
                                    while (n_pos <= rn_num_atr)
                                    {
                                        if (rn_cod_atributos[n_pos] == n_cod_atr_pago[n_num])
                                        {
                                            n_atributos[n_pos] = n_atributos[n_pos] + n_val_atr_pago[n_num];
                                            n_val_atr_pago[n_num] = 0;
                                            n_interes = n_interes + n_atributos[n_pos];
                                        };
                                        n_pos = n_pos + 1;
                                    };
                                };
                                n_num = n_num + 1;
                            };
                            n_excedente = n_excedente - (n_capital + n_interes + n_otros);
                        };
                        //       -- Crea los valores parciales pendientes
                        if (n_capital_aux > n_capital && !b_reestruc_atr)
                        {
                            n_cod_atr_par[n_num_par] = 1;
                            d_fec_pago_par[n_num_par] = rf_f_prox_pago;
                            n_valor_par[n_num_par] = n_capital_aux - n_capital;
                            n_valor_dis[n_num_par] = n_capital_aux - n_capital;
                            n_valor_dis[n_num_par] = BOFunciones.Redondeo(n_valor_dis[n_num_par]);
                            if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                            {
                                Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, n_valor_dis[n_num_par], null, null, "");
                            };
                            n_num_par = n_num_par + 1;
                        }
                        else if (n_capital_aux == n_capital)
                        {
                            if (Buscar_Amortiza(rf_f_prox_pago, 1, ref n_valor_retorno, ref n_pos_reg_amortiza))
                            {
                                Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                            };
                        };
                        n_num = 1;
                        while (n_num <= rn_num_atr)
                        {
                            if (n_atributos_aux[n_num] >= n_atributos[n_num] && !b_reestruc_atr)
                            {
                                n_cod_atr_par[n_num_par] = Convert.ToInt32(rn_cod_atributos[n_num]);
                                d_fec_pago_par[n_num_par] = rf_f_prox_pago;
                                n_valor_par[n_num_par] = n_atributos_aux[n_num] - n_atributos[n_num];
                                n_valor_dis[n_num_par] = n_atributos_aux[n_num] - n_atributos[n_num];
                                n_valor_dis[n_num_par] = BOFunciones.Redondeo(n_valor_dis[n_num_par]);
                                if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, n_valor_dis[n_num_par], null, null, "");
                                };
                                n_num_par = n_num_par + 1;
                            }
                            else if (n_atributos_aux[n_num] == n_atributos[n_num])
                            {
                                if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                                };
                            };
                            n_num = n_num + 1;
                        };
                        #endregion
                    }
                    else
                    {
                        #region determinar pendientes
                        b_ya_causo = true;
                        b_calcula_pend = false;
                        if (n_capital + n_interes + n_otros > n_excedente)
                        {
                            if (n_tipo_pago == 1 || (n_tipo_pago == 1 && n_tipo_pago_temp == 6))
                            {
                                b_calcula_pend = true;
                                n_capital_aux = n_capital;
                                n_capital = 0;
                                n_interes_aux = n_interes;
                                n_interes = 0;
                                n_num = 1;
                                while (n_num <= rn_num_atr)
                                {
                                    if (n_num <= n_atributos.Length)
                                    {
                                        n_atributos_aux[n_num] = BOFunciones.NVL(n_atributos[n_num], 0);
                                    }
                                    else
                                    {
                                        n_atributos_aux[n_num] = 0;
                                    };
                                    n_atributos[n_num] = 0;
                                    n_num = n_num + 1;
                                };

                                //--Distribuye según las prioridades para el pago
                                bool encontradaPrioridad = DApackage.ConsultarPrioridadLinea(s_cod_credi, ref n_cod_atr, ref n_num, usuario);
                                n_pos = 0;

                                while (true)
                                {
                                    if (!encontradaPrioridad)
                                    {
                                        break;
                                    };
                                    if (n_cod_atr == 1)
                                    {
                                        if (n_capital_aux > n_excedente)
                                        {
                                            n_capital = n_excedente;
                                        }
                                        else
                                        {
                                            n_capital = n_capital_aux;
                                        };
                                        n_excedente = n_excedente - n_capital;
                                    }
                                    else
                                    {
                                        n_num = 1;
                                        while (n_num <= rn_num_atr && n_excedente > 0)
                                        {
                                            if (rn_cod_atributos[n_num] == n_cod_atr)
                                            {
                                                if ((n_cod_atr != n_atr_corr) || !(n_cod_atr == n_atr_corr && (n_tipo_pago == 1 && n_tipo_pago_temp == 6)))
                                                {
                                                    if (n_atributos_aux[n_num] > n_excedente)
                                                    {
                                                        n_atributos[n_num] = n_excedente;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = n_atributos_aux[n_num];
                                                    };
                                                    n_excedente = n_excedente - n_atributos[n_num];
                                                    n_interes = n_interes + n_atributos[n_num];
                                                }
                                                else if (n_cod_atr == n_atr_corr && (n_tipo_pago == 1 && n_tipo_pago_temp == 6))
                                                {
                                                    if (n_atributos_aux[n_num] > n_excedente)
                                                    {
                                                        n_val_capitaliza = n_atributos_aux[n_num] - n_excedente;
                                                        n_atributos[n_num] = n_excedente;
                                                    }
                                                    else
                                                    {
                                                        n_atributos[n_num] = n_atributos_aux[n_num];
                                                    };
                                                    n_excedente = n_excedente - n_atributos[n_num];
                                                    n_interes = n_interes + n_atributos[n_num];
                                                };
                                            };
                                            n_num = n_num + 1;
                                        };
                                    };
                                    n_pos = n_pos + 1;
                                    if (n_excedente <= 0)
                                    {
                                        break;
                                    };
                                };

                                //----Verifica si hay prioridades definidas
                                if (n_pos == 0)
                                {
                                    n_error = -5;
                                    n_excedente = 0;
                                };
                            };
                            //         -- Condición para que en caso de pendientes no realice distribución  
                            //         -- Crea los valores parciales pendientes
                            if (n_capital_aux > n_capital && !b_reestruc_atr)
                            {
                                n_cod_atr_par[n_num_par] = 1;
                                d_fec_pago_par[n_num_par] = rf_f_prox_pago;
                                n_valor_dis[n_num_par] = n_capital_aux - n_capital;
                                n_valor_dis[n_num_par] = BOFunciones.Redondeo(n_valor_dis[n_num_par]);
                                if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, n_valor_dis[n_num_par], null, null, "");
                                };
                                n_num_par = n_num_par + 1;
                            }
                            else if (n_capital_aux == n_capital)
                            {
                                if (Buscar_Amortiza(rf_f_prox_pago, 1, ref n_valor_retorno, ref n_pos_reg_amortiza))
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                                };
                            };
                            n_num = 1;
                            while (n_num <= rn_num_atr && n_num <= n_atributos.Length && n_num <= n_atributos_aux.Length && n_num <= rn_cod_atributos.Length)
                            {
                                n_valor_dis[n_num_par] = 0;
                                n_valor_dis[n_num_par + 100] = 0;
                                n_valor_dis[n_num_par + 200] = 0;
                                n_atributo = 0;
                                if (n_atributos_aux[n_num] >= n_atributos[n_num] && !b_reestruc_atr)
                                {
                                    n_cod_atr_par[n_num_par] = Convert.ToInt32(rn_cod_atributos[n_num]);
                                    d_fec_pago_par[n_num_par] = rf_f_prox_pago;
                                    n_restante = n_atributos[n_num];
                                    n_valor_dis[n_num_par] = n_atributos_aux[n_num] - n_atributos[n_num];
                                    n_valor_dis[n_num_par] = BOFunciones.Redondeo(n_valor_dis[n_num_par]);
                                    if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                                    {
                                        Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, n_valor_dis[n_num_par], null, null, "");
                                    };
                                    n_num_par = n_num_par + 1;
                                }
                                else if (n_atributos_aux[n_num] == n_atributos[n_num])
                                {
                                    if (Buscar_Amortiza(rf_f_prox_pago, n_cod_atr_par[n_num_par], ref n_valor_retorno, ref n_pos_reg_amortiza))
                                    {
                                        Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                                    };
                                };
                                n_num = n_num + 1;
                            };
                        }
                        else
                        {
                            n_excedente = n_excedente - (n_capital + n_interes + n_otros);
                        };
                        #endregion
                    };
                };

                string fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                //   ----------------------------------------------------------------------------------------------------------------------------------  
                //   ---- Determina los valores causados y del mes
                MensajeConsola("Corre la fecha del proximo pago " + fechaProxPagoString + " " + n_dias + " " + n_tipo_cal);
                //   ----------------------------------------------------------------------------------------------------------------------------------  

                #region determina valores causados
                f_fec_act = rf_f_prox_pago;
                rf_f_prox_pago = BOFunciones.FecSumDia(f_fec_act, Convert.ToInt32(n_dias), Convert.ToInt32(n_tipo_cal));
                if (BOFunciones.DateMonth(f_fec_act) == 2 && BOFunciones.DateDay(f_fec_act) >= 28 && BOFunciones.DateMonth(f_fec_inicio) != 2 && (BOFunciones.DateDay(f_fec_inicio) == 28 || BOFunciones.DateDay(f_fec_inicio) == 29))
                {
                    if (n_dias == 30 || n_dias == 90 || n_dias == 180 || n_dias == 360)
                    {
                        rf_f_prox_pago = BOFunciones.DateConstruct(BOFunciones.DateYear(rf_f_prox_pago), BOFunciones.DateMonth(rf_f_prox_pago), BOFunciones.DateDay(f_fec_act), 0, 0, 0);
                    };
                };
                if (n_tipo_pago == 5 && rf_f_prox_pago >= f_fecha_pago)
                {
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                        {
                            n_tasa_int[n_num] = atr_finan[n_num].n_tasa_calculo;
                        };
                        n_num = n_num + 1;
                    };
                };
                if (n_tipo_pago == 5 && n_mes_causa == n_mes_ant1 && n_ano_causa == n_ano_ant1)
                {
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        if (atr_finan[n_num].n_cod_atr == n_atr_corr)
                        {
                            n_tasa_int[n_num] = atr_finan[n_num].n_tasa_calculo;
                        };
                        n_num = n_num + 1;
                    };
                };
                if ((n_tipo_pago != 1 || (n_tipo_pago == 1 && n_num_par == 1)) && n_tip_cuota != 1)
                {
                    f_fecha_proximo = rf_f_prox_pago;
                    n_num_cuo_pag = n_num_cuo_pag + 1;
                };
                #endregion
                //--Prepara g_sentencia para grabar datos de riesgo de liquidez
                if (g_b_historico_amortiza)
                {
                    if (n_capital > 0)
                    {
                        DApackage.InsertarHistoricoAmortizaCre(f_fecha_pago, 1, n_radic, f_fec_act, 1, n_capital, usuario);
                    };
                };
                //----Ajuste para que tome valores a fecha de cierre -CifIN / DATECREDITO
                if (n_tipo_pago == 5 && f_fec_act > f_fecha_pago)
                {
                    n_capital = 0;
                };
                //-----Actualiza variables de totales por atributo
                #region actualiza variables
                rn_tot_capital = rn_tot_capital + n_capital;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= n_atributos.Length && n_num <= rn_tot_atributos.Length)
                {
                    rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + BOFunciones.NVL(n_atributos[n_num], 0);
                    if (n_tipo_pago_temp == 7)
                    {
                        if (n_atributos[n_num] > 0)
                        {
                            DApackage.InsertarHistoricoAmortizaCre(f_fecha_pago, 1, n_radic, f_fec_act, rn_cod_atributos[n_num], n_atributos[n_num], usuario);
                        };
                    };
                    n_num = n_num + 1;
                };
                #endregion

                fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                //   ----------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Detalle de los pagos " + fechaProxPagoString);
                //   ----------------------------------------------------------------------------------------------------------------------  

                #region pendientes
                if (n_tipo_pago != 5 || b_reporte_edades)
                {
                    //--Inicializa las variables
                    n_cuo_pag_rep = n_cuo_pag_rep + 1;
                    //--Saldo actual
                    if (n_capital > 0)
                    {
                        //--Pago de capital
                        cl_detalle_pago[n_cont_rep].f_fecha_cuota = f_fec_cuota_rep;
                        cl_detalle_pago[n_cont_rep].n_cod_atr = 1;
                        cl_detalle_pago[n_cont_rep].n_valor = n_capital;
                        if (Buscar_Amortiza(f_fec_cuota_rep, cl_detalle_pago[n_cont_rep].n_cod_atr, ref n_valor_retorno, ref n_pos_reg_amortiza))
                        {
                            cl_detalle_pago[n_cont_rep].n_saldo = n_valor_retorno;
                            if (n_num_par == 1)
                            {
                                Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                            };
                        };
                        cl_detalle_pago[n_cont_rep].n_tasa_base = 0;
                        cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                        cl_detalle_pago[n_cont_rep].s_estado = "1";
                        n_cont_rep = n_cont_rep + 1;
                    };
                    n_num = 1;
                    while (n_num <= n_atr_fin)
                    {
                        //--Inicializa las variables
                        if (n_atributos[n_num] > 0)
                        {
                            cl_detalle_pago[n_cont_rep].f_fecha_cuota = f_fec_cuota_rep;
                            cl_detalle_pago[n_cont_rep].n_cod_atr = atr_finan[n_num].n_cod_atr;
                            cl_detalle_pago[n_cont_rep].n_valor = n_atributos[n_num];
                            cl_detalle_pago[n_cont_rep].n_tasa_base = atr_finan[n_num].n_tasa_calculo;
                            if (Buscar_Amortiza(f_fec_cuota_rep, cl_detalle_pago[n_cont_rep].n_cod_atr, ref n_valor_retorno, ref n_pos_reg_amortiza))
                            {
                                cl_detalle_pago[n_cont_rep].n_saldo = cl_amortiza_cre[Convert.ToInt32(n_pos_reg_amortiza)].n_saldo;
                                if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                                {
                                    Actualizar_Mora(f_fec_cuota_rep, ref n_atributos[n_num], cl_amortiza_cre[Convert.ToInt32(n_pos_reg_amortiza)].n_saldo);
                                };
                                if (n_num_par == 1)
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                                };
                            };
                            cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                            cl_detalle_pago[n_cont_rep].s_estado = "1";
                            n_cont_rep = n_cont_rep + 1;
                        };
                        n_num = n_num + 1;
                    };
                    n_num = n_atr_fin + 1;
                    while (n_num <= rn_num_atr && n_num <= n_atributos.Length)
                    {
                        //--Inicializa las variables
                        if (n_atributos[n_num] > 0)
                        {
                            cl_detalle_pago[n_cont_rep].f_fecha_cuota = f_fec_cuota_rep;
                            cl_detalle_pago[n_cont_rep].n_cod_atr = Convert.ToInt32(rn_cod_atributos[n_num]);
                            cl_detalle_pago[n_cont_rep].n_valor = n_atributos[n_num];
                            if (Buscar_Amortiza(f_fec_cuota_rep, cl_detalle_pago[n_cont_rep].n_cod_atr, ref n_valor_retorno, ref n_pos_reg_amortiza))
                            {
                                cl_detalle_pago[n_cont_rep].n_saldo = n_valor_retorno;
                                if (n_num_par == 1)
                                {
                                    Actualizar_Amortiza(Convert.ToInt32(n_pos_reg_amortiza), null, 0, null, null, "");
                                };
                            };
                            cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                            cl_detalle_pago[n_cont_rep].s_estado = "1";
                            n_cont_rep = n_cont_rep + 1;
                        };
                        n_num = n_num + 1;
                    };
                };
                #endregion

                fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                //   ----------------------------------------------------------------------------------------------------------------------  
                MensajeConsola("Si el tipo de pago es por un valor dado " + fechaProxPagoString);
                //   -------------------------------------------------------------------------------------------------------------------------------------  
                #region si el pago es por valor
                if (n_tipo_pago == 1)
                {
                    if (n_num_par <= 1 && n_excedente <= 0)
                    {
                        rf_f_prox_pago = f_fec_act;
                    };
                    if (n_excedente <= 0)
                    {
                        if (!(s_cob_mor == "1" && n_pos_mora != -1 && f_fecha_pago > rf_f_prox_pago))
                        {
                            break;
                        };
                    };
                    if (n_tipo_pago_temp == 6)
                    {
                        if (rf_f_prox_pago > f_fecha_pago || n_excedente <= 0)
                        {
                            break;
                        };
                    };
                };
                #endregion

                fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                MensajeConsola("Si el tipo de pago es por fecha verificar fecha de próximo pago " + fechaProxPagoString);

                #region pago por fecha
                if (n_tipo_pago == 2)
                {
                    if (rf_f_prox_pago > f_fecha_pago)
                    {
                        if (!(b_exis_acelerado && f_fecha_pago != f_fecha_pago1))
                        {
                            break;
                        };
                    };
                };
                #endregion

                #region validando segun forma de pago
                if (n_tipo_pago == 4 || n_tipo_pago == 5 || (n_tipo_pago == 3 && (n_cuotas_a_pag == 99999 && n_num_cuo_pag + n_cuo_pag >= n_num_cuotas)))
                {
                    //--1:Anticipado, 2:Vencido
                    if ((f_fec_act >= f_fecha_pago && n_tip_pago == 1) || (f_fecha_pago < f_fec_act && n_tip_pago == 2) || (f_fec_act == f_fecha_pago && n_tipo_pago == 4 && n_tipo_pago_temp == 2))
                    {
                        if (!b_interes_dias || n_tipo_pago_temp == 0)
                        {
                            if (n_tipo_pago != 5)
                            {
                                if (n_tip_tf != 4)
                                {
                                    rn_tot_capital = rn_tot_capital + n_saldo_act + n_sal_tf;
                                }
                                else
                                {
                                    rn_tot_capital = rn_tot_capital + n_saldo_act;
                                };
                                //--Pago de capital
                                cl_detalle_pago[n_cont_rep].f_fecha_cuota = f_fec_cuota_rep;
                                cl_detalle_pago[n_cont_rep].n_cod_atr = 1;
                                cl_detalle_pago[n_cont_rep].n_valor = n_saldo_act;
                                cl_detalle_pago[n_cont_rep].n_saldo = 0;
                                cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                                cl_detalle_pago[n_cont_rep].s_estado = "1";
                                n_cont_rep = n_cont_rep + 1;
                            };
                        };
                        break;
                    };
                };
                #endregion

                fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                MensajeConsola("Si el tipo de pago es por nùmero de cuotas " + fechaProxPagoString);
                #region pago por cuotas
                if (n_tipo_pago == 3)
                {
                    if (n_num_cuo_pag >= n_cuotas_a_pag)
                    {
                        break;
                    };
                };
                #endregion

                fechaProxPagoString = rf_f_prox_pago.HasValue ? rf_f_prox_pago.Value.ToShortDateString() : " sin fecha ";

                MensajeConsola("Verifica variables de control " + fechaProxPagoString);
                #region verificando saldos
                if (n_saldo_act == 0)
                {
                    if (n_tipo_pago == 4 || n_tipo_pago == 5)
                    {
                        if (n_tipo_pago != 5)
                        {
                            rn_tot_capital = rn_tot_capital + n_saldo_act + n_sal_tf;
                        };
                    };
                    if (!(b_exis_acelerado && f_fecha_pago != f_fecha_pago1))
                    {
                        break;
                    };
                };
                #endregion
                n_max_iteraciones = n_max_iteraciones + 1;
                #region controlar iteraciones
                if (n_max_iteraciones > 5000)
                {
                    n_num = 1;
                    while (n_num <= rn_num_atr)
                    {
                        rn_tot_atributos[n_num] = 0;
                        rn_cod_atributos[n_num] = atr_finan[n_num].n_cod_atr;
                        n_num = n_num + 1;
                    };
                    rn_tot_capital = 0;
                    break;
                };
                #endregion
            };
            #endregion
            // -------------------------------------------------------------------------------------------------------------------------------------  
            // Mensaje("Cobrar el valor de la garantia comunitaria si el crédito esta al día");  
            // -------------------------------------------------------------------------------------------------------------------------------------  
            #region garantias comunitarias
            if (n_num_cuo_pag == 0 && n_tipo_pago == 2)
            {
                n_pos = 1;
                while (n_pos <= rn_num_atr)
                {
                    if (rn_cod_atributos[n_pos] == n_atr_CTACOB)
                    {
                        n_atributos[n_pos] = 0;

                        DApackage.ConsultarCuentaPorCobrarAtributosCredito(n_radic, ref n_atributos[n_pos], usuario);

                        if (n_atributos[n_pos] == null)
                        {
                            n_atributos[n_pos] = 0;
                        };
                        n_otros = n_otros + BOFunciones.NVL(n_atributos[n_pos], 0);
                    };
                    n_pos = n_pos + 1;
                };
            };
            #endregion
            // -------------------------------------------------------------------------------------------------------------------------------------  
            // Mensaje("Completa el valor de capital en creditos acelerados y suspendidos " || to_char(rf_f_prox_pago, "MM/DD/YYYY"));  
            // -------------------------------------------------------------------------------------------------------------------------------------  
            if (b_exis_suspendido)
            {
                rn_tot_capital = n_saldo;
            };
            #region -- Completa el valor de capital en creditos que poseen terminos fijos pendientes
            if (n_tipo_pago == 4)
            {
                n_capital = n_saldo - BOFunciones.NVL(rn_tot_capital, 0);
                rn_tot_capital = n_saldo;
                if (n_capital > 0)
                {
                    //--Determinar valor pagado
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        if (cl_detalle_pago[n_cont_rep].n_cod_atr == 1)
                        {
                            n_capital = n_capital - cl_detalle_pago[n_cont_rep].n_valor;
                        };
                        n_cont_rep = n_cont_rep + 1;
                    };
                    //--Pago de capital
                    if (n_capital > 0)
                    {
                        cl_detalle_pago[n_cont_rep].f_fecha_cuota = rf_f_prox_pago;
                        cl_detalle_pago[n_cont_rep].n_cod_atr = 1;
                        cl_detalle_pago[n_cont_rep].n_valor = n_capital;
                        cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                        cl_detalle_pago[n_cont_rep].s_estado = "1";
                        n_cont_rep = n_cont_rep + 1;
                    };
                };
            };
            #endregion
            // -- Actualiza la fecha de proximo pago de acuerdo al pago de cuotas totales
            rf_f_prox_pago = f_fecha_proximo;
            // -------------------------------------------------------------------------------------------------------------------------------------  
            // -- Verifica si la fecha de proximo pago es menor a la fecha de pago para calcular el valor de los seguros restantes  
            // -------------------------------------------------------------------------------------------------------------------------------------  
            #region calculo seguros restantes
            if (n_saldo_act == 0 && f_fecha_proximo < f_fecha_pago)
            {
                n_dias_atraso = BOFunciones.FecDifDia(f_fecha_proximo, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                //--Se verifica cada uno de los atributos descontados para verificar si pertenecen a algun tipo de seguro
                n_num = 1;
                n_pos = n_atr_fin;
                while (n_num < n_atr_otr && n_pos < rn_num_atr)
                {
                    #region calculo atributos descontados
                    if (atr_otro[n_num].n_signo == 4 || atr_otro[n_num].n_signo == 5)
                    {
                        n_valor_seg = 0;
                        b_control_atr = false;
                        if (!b_control_atr)
                        {
                            #region
                            try
                            {
                                n_cant_cod = Convert.ToInt32(BOFunciones.StrTokenize(BOFunciones.BuscarGeneral(1510, 1), ",", ref s_vector_atr));
                            }
                            catch
                            {
                                n_cant_cod = 0;
                                break;
                            }
                            n_pos_cod = 1;
                            while (n_pos_cod <= n_cant_cod)
                            {
                                if (Convert.ToString(atr_otro[n_num].n_cod_atr) == s_vector_atr[n_pos_cod])
                                {
                                    n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                    n_pos = n_pos + 1;
                                    b_control_atr = true;
                                    g_sentencia = "Select i_vlr_prima, i_per_pago From seg_veh where cod_atr = :1 and i_num_radic = :n_radic";
                                    //Open pBasedato For g_sentencia Using atr_otro[n_num].n_cod_atr, n_radic;
                                    //Fetch pBasedato Into n_valor_seg_aux, n_per_pago_seg;
                                    //Close pBasedato;
                                    if (n_valor_seg == 0 || n_valor_seg == null)
                                    {
                                        n_valor_seg = n_valor_seg_aux;
                                    };
                                    break;
                                };
                                n_pos_cod = n_pos_cod + 1;
                            };
                            #endregion
                        }
                        if (!b_control_atr)
                        {
                            #region 
                            n_cant_cod = Convert.ToInt32(BOFunciones.StrTokenize(BOFunciones.BuscarGeneral(1512, 1), ",", ref s_vector_atr));
                            n_pos_cod = 1;
                            while (n_pos_cod <= n_cant_cod)
                            {
                                if (atr_otro[n_num].n_cod_atr.ToString() == s_vector_atr[n_pos_cod])
                                {
                                    n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                    n_pos = n_pos + 1;
                                    b_control_atr = true;
                                    g_sentencia = "Select i_vlr_prima,i_per_pago from seg_vida where cod_atr=:1 and i_num_radic=:2";
                                    //Open pBasedato For g_sentencia Using atr_otro[n_num].n_cod_atr, n_radic;
                                    //Fetch pBasedato Into n_valor_seg_aux, n_per_pago_seg;
                                    //Close pBasedato;
                                    if (n_valor_seg == 0 || n_valor_seg == null)
                                    {
                                        n_valor_seg = n_valor_seg_aux;
                                    };
                                    break;
                                };
                                n_pos_cod = n_pos_cod + 1;
                            };
                            #endregion
                        };
                        if (!b_control_atr)
                        {
                            #region
                            n_cant_cod = Convert.ToInt32(BOFunciones.StrTokenize(BOFunciones.BuscarGeneral(1513, 1), ",", ref s_vector_atr));
                            n_pos_cod = 1;
                            while (n_pos_cod <= n_cant_cod)
                            {
                                if (atr_otro[n_num].n_cod_atr.ToString() == s_vector_atr[n_pos_cod])
                                {
                                    n_atributos[n_pos] = BOFunciones.Redondeo(n_atributos[n_pos]);
                                    n_pos = n_pos + 1;
                                    b_control_atr = true;
                                    g_sentencia = "Select i_vlr_prima,i_per_pago from seg_otros where cod_atr = :1 and i_num_radic = :2";
                                    //Open pBasedato For g_sentencia Using atr_otro[n_num].n_cod_atr, n_radic;
                                    //Fetch pBasedato Into n_valor_seg_aux, n_per_pago_seg;
                                    //Close pBasedato;
                                    if (n_valor_seg == 0 || n_valor_seg == null)
                                    {
                                        n_valor_seg = n_valor_seg_aux;
                                    };
                                    break;
                                };
                                n_pos_cod = n_pos_cod + 1;
                            };
                            #endregion
                        };
                        if (b_control_atr)
                        {
                            #region
                            //--Se verifica la periodicidad para cada seguro y se calcula el numero de cuotas por pagar por concepto del seguro
                            n_dias_seg = Convert.ToInt32(DApackage.ConPeriodicidadNumDia(n_per_pago_seg, usuario));

                            if (n_dias_seg > 0)
                            {
                                n_dias_atraso = n_dias_atraso / n_dias_seg;
                                n_dif_red = BOFunciones.Round(n_dias_atraso);
                                if (n_dias_atraso != n_dif_red)
                                {
                                    n_dias_atraso = n_dias_atraso - Convert.ToInt32(n_dif_red);
                                };
                                if (n_dias_atraso < 0)
                                {
                                    n_cuotas_atraso = n_dif_red;
                                }
                                else
                                {
                                    n_cuotas_atraso = n_dif_red;
                                };
                                if (n_cuotas_atraso > 0)
                                {
                                    n_ind_seg = 0;
                                    n_valor_seg = n_valor_seg * n_cuotas_atraso;
                                    while (n_ind_seg < rn_num_atr)
                                    {
                                        if (rn_cod_atributos[Convert.ToInt32(n_ind_seg)] == atr_otro[n_num].n_cod_atr)
                                        {
                                            rn_tot_atributos[Convert.ToInt32(n_ind_seg)] = rn_tot_atributos[Convert.ToInt32(n_ind_seg)] + n_valor_seg;
                                            break;
                                        };
                                        n_ind_seg = n_ind_seg + 1;
                                    };
                                };
                            };
                            #endregion
                        };
                    };
                    #endregion
                    n_num = n_num + 1;
                };
            };
            #endregion
            // -------------------------------------------------------------------------------------------------------------------------------------  
            // -- Verifica los atributos de seguros para seguir causando  
            // -------------------------------------------------------------------------------------------------------------------------------------  
            #region atributo seguro
            if (n_saldo_act == 0 && f_fecha_proximo <= f_fecha_pago && n_tipo_pago == 5)
            {
                n_dias_tot_atraso = BOFunciones.FecDifDia(f_fecha_proximo, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                f_fecha_pago_aux = f_fecha_proximo;
                //--Se verifica cada uno de los atributos descontados para verificar si pertenecen a algun tipo de seguro
                n_num = 1;
                while (n_num <= n_atr_fin)
                {
                    n_valor_seg = 0;
                    b_control_atr = false;
                    if (!b_control_atr)
                    {
                        n_cant_cod = Convert.ToInt32(BOFunciones.StrTokenize(BOFunciones.BuscarGeneral(1512, 1), ",", ref s_vector_atr));
                        n_pos_cod = 1;
                        while (n_pos_cod <= n_cant_cod)
                        {
                            if (Convert.ToString(atr_finan[n_num].n_cod_atr) == s_vector_atr[n_pos_cod])
                            {
                                b_control_atr = true;
                                n_valor_seg = (atr_finan[n_num].n_tasa_calculo / 100) * n_saldo;
                                n_valor_seg = BOFunciones.Redondeo(n_valor_seg);
                                if (n_valor_seg == 0 || n_valor_seg == null)
                                {
                                    n_valor_seg = n_valor_seg_aux;
                                };
                                break;
                            };
                            n_pos_cod = n_pos_cod + 1;
                        };
                    };
                    if (b_control_atr)
                    {
                        //--Se verifica la periodicidad para cada seguro y se calcula el numero de cuotas por pagar por concepto del seguro
                        while (f_fecha_pago_aux <= f_fecha_pago)
                        {
                            Insertar_Amortiza(Convert.ToDateTime(f_fecha_pago_aux), Convert.ToInt32(atr_finan[n_num].n_cod_atr), Convert.ToDecimal(n_valor_seg), Convert.ToDecimal(atr_finan[n_num].n_tasa_calculo), n_saldo, "2");
                            f_fecha_pago_aux = BOFunciones.FecSumDia(f_fecha_pago_aux, Convert.ToInt32(n_dias), Convert.ToInt32(n_tipo_cal));
                        };
                        n_dias_seg = n_dias;
                        if (n_dias_seg > 0)
                        {
                            n_dias_atraso = n_dias_tot_atraso / n_dias_seg;
                            n_dif_red = BOFunciones.Round(n_dias_atraso);
                            if (n_dias_atraso != n_dif_red)
                            {
                                n_dias_atraso = n_dias_atraso - Convert.ToInt32(n_dif_red);
                            };
                            if (n_dias_atraso < 0)
                            {
                                n_cuotas_atraso = n_dif_red;
                            }
                            else
                            {
                                n_cuotas_atraso = n_dif_red + 1;
                            };
                            if (n_cuotas_atraso > 0)
                            {
                                n_ind_seg = 0;
                                n_valor_seg = n_valor_seg * n_cuotas_atraso;
                                while (n_ind_seg < rn_num_atr)
                                {
                                    if (rn_cod_atributos[Convert.ToInt32(n_ind_seg)] == atr_finan[n_num].n_cod_atr)
                                    {
                                        rn_tot_atributos[Convert.ToInt32(n_ind_seg)] = rn_tot_atributos[Convert.ToInt32(n_ind_seg)] + n_valor_seg;
                                        break;
                                    };
                                    n_ind_seg = n_ind_seg + 1;
                                };
                            };
                        };
                    };
                    n_num = n_num + 1;
                };
            };
            #endregion
            // -------------------------------------------------------------------------------------------------------------------------------------  
            // -- Verifica los atributos a cobrar proporcional a los dias  
            // -------------------------------------------------------------------------------------------------------------------------------------  
            #region 
            n_num = 1;
            if (((n_tipo_pago == 4 || n_tipo_pago == 5) && n_tip_pago == 1) && (rf_f_prox_pago > f_fecha_pago) && (n_tip_amo == 5 || n_tip_amo == 2 || n_tip_amo == 6 || n_tip_amo == 4) && n_max_iteraciones <= 0)
            {
                f_fec_ant = BOFunciones.FecResDia(rf_f_prox_pago, n_dias, Convert.ToInt32(n_tipo_cal));
                n_dias_prop = Convert.ToInt32(BOFunciones.FecDifDia(f_fecha_pago, rf_f_prox_pago, Convert.ToInt32(n_tipo_cal)));
                if (n_dias_prop > 0)
                {
                    n_capital = 0;
                    n_interes = 0;
                    n_otros = 0;
                    //---Determina el valor de la tasa de interes para créditos con tasa variable
                    if (b_tasa_variable)
                    {
                        n_num = 1;
                        n_tasa_interes = 0;
                        while (n_num < n_atr_fin)
                        {
                            if (atr_finan[n_num].s_tip_cal == "5")
                            {
                                atr_finan[n_num].Conv_Tasa(n_periodic, n_tip_pago, f_fec_ant1, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                            }
                            else
                            {
                                n_val_aux = n_tasa_original[n_num];
                                atr_finan[n_num].n_tasa_calculo = n_tasa_original[n_num];
                            };
                            //--Compara la tasa de interes con la de usura y deja la menor
                            if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                            {
                                s_usura = BOFunciones.BuscarGeneral(681, 1);
                            }
                            else
                            {
                                s_usura = BOFunciones.BuscarGeneral(680, 1);
                            };
                            if (s_usura != null)
                            {
                                b_existe = true;
                            }
                            else
                            {
                                b_existe = false;
                            };
                            if (b_existe)
                            {
                                n_usura = n_tipo_tasa_usura;
                                n_tasa_usura = 0;
                                g_f_usura = f_fec_ant1;

                                // Para evitar que explote por usar un convert en un null
                                int? n_usura_int = n_usura.HasValue ? Convert.ToInt32(n_usura.Value) : default(int?);

                                DApackage.ConsultarTasaUsura(g_f_usura, n_usura_int, ref n_tasa_usura, ref n_tipo_tasa, usuario);

                                if (n_tasa_usura != 0)
                                {
                                    DApackage.ConTipoTasa(n_tipo_tasa, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);

                                    n_tasa_calculo = BOFunciones.CalTasMod(n_tasa_usura, BOFunciones.To_Number(s_efe_nom), n_per, BOFunciones.To_Number(s_mod), n_mod_per, 1, n_periodic, n_tip_pago, n_periodic, 0);
                                    if (n_tasa_calculo < n_val_aux && n_tasa_calculo > 0)
                                    {
                                        n_val_aux = n_tasa_calculo;
                                        atr_finan[n_num].n_tasa_calculo = n_tasa_calculo;
                                    };
                                    n_numero = n_numero + 1;
                                };
                            };
                            //--Determina la tasa global del crédito, sumando tasas de los atributos
                            if (atr_finan[n_num].n_cod_atr != n_atr_mora && n_val_aux != null)
                            {
                                n_tasa_interes = n_tasa_interes + n_val_aux;
                            };
                            n_num = n_num + 1;
                        };
                        //--Se determina la tasa de interés diaria del interés de mora
                        if (n_pos_mora != -1)
                        {
                            if (atr_finan[Convert.ToInt32(n_pos_mora)].s_tip_cal == "5")
                            {
                                atr_finan[Convert.ToInt32(n_pos_mora)].Conv_Tasa(n_per_dia, 2, rf_f_prox_pago, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                            };
                        }
                        else
                        {
                            n_tasa_mora = 0;
                        };
                        n_tasa_interes = n_tasa_interes / 100;
                    };
                    n_num = 1;
                    while (n_num < rn_num_atr)
                    {
                        if (rn_cod_atributos[n_num] != n_atr_mora)
                        {
                            if (n_num < n_atr_fin)
                            {
                                if (s_calc_int == "1")
                                {
                                    n_int_prop = n_dias;
                                    n_int_prop = n_tasa_interes / n_int_prop;
                                    if (n_tip_tf != 4)
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + (n_int_prop * n_dias_prop * (n_saldo_act + n_sal_tf));
                                    }
                                    else
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + (n_int_prop * n_dias_prop * n_saldo_act);
                                    };
                                }
                                else
                                {
                                    n_int_prop = BOFunciones.CalTasMod(atr_finan[n_num].n_tasa_calculo, 1, n_periodic, n_tip_pago, n_periodic, 1, n_per_dia, n_tip_pago, n_per_dia, 2);
                                    if (n_tip_tf != 4)
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + ((n_int_prop / 100) * n_dias_prop * (n_saldo_act + n_sal_tf));
                                    }
                                    else
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + ((n_int_prop / 100) * n_dias_prop * n_saldo_act);
                                    };
                                };
                                if (rn_tot_atributos[n_num] < 0)
                                {
                                    rn_tot_atributos[n_num] = 0;
                                };
                            }
                            else
                            {
                                if (atr_otro[n_num - n_atr_fin + 1].n_signo == 3 || atr_otro[n_num - n_atr_fin + 1].n_signo == 4)
                                {
                                    if (n_num_cuotas != 0)
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + (atr_otro[n_num - n_atr_fin].n_valor_calculo / n_num_cuotas) * (n_dias_prop / 30);
                                    }
                                    else
                                    {
                                        rn_tot_atributos[n_num] = rn_tot_atributos[n_num] + atr_otro[n_num - n_atr_fin].n_valor_calculo * (n_dias_prop / 30);
                                    };
                                };
                            };
                            //--Elimina atributos que no se cruzan
                            n_pos = 1;
                            while (n_pos < n_num_cruza)
                            {
                                if (n_no_cruza[n_pos] == rn_cod_atributos[n_num])
                                {
                                    rn_tot_atributos[n_num] = 0;
                                };
                                n_pos = n_pos + 1;
                            };
                            rn_tot_atributos[n_num] = BOFunciones.Redondeo(rn_tot_atributos[n_num]);
                            rn_tot_atributos[n_num] = -rn_tot_atributos[n_num];
                        };
                        n_num = n_num + 1;
                    };
                };
            };
            #endregion
            // -- Verifica los atributos a devolver si no se financiaron y se cancela totalmente
            #region 
            n_num = 1;
            if (n_tipo_pago == 4 || n_tipo_pago == 5)
            {
                while (n_num < n_atr_otr)
                {

                    if (atr_otro[n_num].n_signo == 1 && (atr_otro[n_num].n_tip_des == 2 || atr_otro[n_num].n_tip_des == 3))
                    {
                        if (n_tipo_pago == 5 || (n_tipo_pago == 4 && f_prox_pag >= f_fecha_pago))
                        {
                            bResultado = BOFunciones.CalValDes(atr_otro[n_num].n_tip_des, atr_otro[n_num].n_valor, atr_otro[n_num].n_tip_liq, rn_tot_capital, n_plazo, n_num_cuotas, ref rn_tot_atributos[rn_num_atr], n_saldo, 0, n_valorCatg, n_valfactor, n_vrComer, 0, n_num_codeu);
                            rn_tot_atributos[rn_num_atr] = -rn_tot_atributos[rn_num_atr];
                            rn_cod_atributos[rn_num_atr] = atr_otro[n_num].n_cod_atr;
                            rn_num_atr = rn_num_atr + 1;
                        };
                    };
                    n_num = n_num + 1;
                };
            };
            #endregion
            // -- Verifica atributos que no se deben devolver
            #region 
            if (n_tip_pago == 1 || n_tip_pago == 2)
            {
                //--Atributos que no se devuelven
                s_atr_no_dev = BOFunciones.BuscarGeneral(350, 1);
                if (s_atr_no_dev != null)
                {
                    b_existe = true;
                }
                else
                {
                    b_existe = false;
                };
                n_num_no_dev = BOFunciones.StrTokenize(s_atr_no_dev, ",", ref s_aux);
                n_num = 1;
                while (n_num <= n_num_no_dev)
                {
                    n_atr_no_dev[n_num] = BOFunciones.To_Number(s_aux[n_num]);
                    n_num = n_num + 1;
                };
                //--Elimina valores de los atributos no devueltos
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_cod_atributos.Length)
                {
                    n_pos = 1;
                    while (n_pos <= n_num_no_dev)
                    {
                        if (rn_cod_atributos[n_num] == n_atr_no_dev[n_pos] && rn_tot_atributos[n_num] < 0)
                        {
                            rn_tot_atributos[n_num] = 0;
                        };
                        n_pos = n_pos + 1;
                    };
                    n_num = n_num + 1;
                };
            };
            #endregion
            // -- Determina si es un pago por valor, el capital
            #region 
            if (n_tipo_pago == 1 && n_tipo_pago_temp == 6)
            {
                if (n_excedente > 0)
                {
                    //--Pago de capital
                    if (f_fec_cuota_rep == null)
                    {
                        f_fec_cuota_rep = rf_f_prox_pago;
                    };
                    cl_detalle_pago[n_cont_rep].f_fecha_cuota = f_fec_cuota_rep;
                    cl_detalle_pago[n_cont_rep].n_cod_atr = 1;
                    if (n_saldo_act >= n_excedente)
                    {
                        cl_detalle_pago[n_cont_rep].n_valor = n_excedente;
                        cl_detalle_pago[n_cont_rep].n_saldo = n_saldo_act - n_excedente;
                        rn_tot_capital = rn_tot_capital + n_excedente;
                    }
                    else
                    {
                        cl_detalle_pago[n_cont_rep].n_valor = n_saldo_act;
                        cl_detalle_pago[n_cont_rep].n_saldo = n_saldo_act;
                        rn_tot_capital = rn_tot_capital + n_saldo_act;
                    };
                    cl_detalle_pago[n_cont_rep].n_num_cuota = n_cuo_pag_rep;
                    cl_detalle_pago[n_cont_rep].s_estado = "1";
                    n_cont_rep = n_cont_rep + 1;
                };
            };
            #endregion
            // -- Elimina atributos que no se cruzan
            #region 
            if (g_b_cruce)
            {
                n_num = 1;
                while (n_num < rn_num_atr)
                {
                    if (rn_tot_atributos[n_num] != 0)
                    {
                        n_pos = 0;
                        while (n_pos < n_num_cruza)
                        {
                            if (n_no_cruza[n_pos] == rn_cod_atributos[n_num])
                            {
                                rn_tot_atributos[n_num] = 0;
                            };
                            n_pos = n_pos + 1;
                        };
                    };
                    n_num = n_num + 1;
                };
            };
            n_man_pago_atr = 0;
            #endregion
            return n_error;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Función para el calculo de la amortización de terminos fijos  
        //-- El tipo de pago corresponde a 1=Por Valor, 2=A Fecha de Pago, 3=Por nùmero de cuotas.
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        private int? Amortizar_CuoExt(int pn_tipo_pago, decimal? n_valor_pago, DateTime? pf_fecha_pago, int? n_cuotas_a_pag, ref decimal? rn_tot_capital, ref decimal? rn_tot_interes, ref decimal? rn_tot_int_mora, ref decimal? rn_sobrante)
        {
            int? n_error = null;
            bool b_existe = false;
            decimal? n_tot_tf = null;
            decimal? n_sal_tf = null;
            int? n_pos = null;
            int? n_num_cuo = null;
            decimal? n_tot_cap = null;
            decimal? n_tot_int = null;
            // -- Datos de cada termino fijo
            decimal? n_cap_tf = null;
            decimal? n_int_tf = null;
            DateTime? f_fec_tf = null;
            //-- Calculo de la mora
            decimal? n_dias_mora = null;
            decimal? n_val_mora = null;
            decimal? n_aux_mora = null;
            int n_pos_mora = 0;
            int n_num = 0;
            decimal? n_tasa_mora = null;
            int? n_per_dia = null;
            //-- Variables para el calculo de la mora de historico } variable
            DateTime? f_fec_prox = null;
            decimal? n_dias_mora_temp = null;
            DateTime? f_fec_ini = null;
            DateTime? f_fec_fin = null;
            decimal? n_dias_dif = null;
            //-- Variables utiliadas para la comparación de la tasa de cada atributo con la de la usura
            string s_usura = null;
            int n_usura = 0;
            decimal? n_tasa_usura = null;
            bool b_existe_usura = false;
            decimal? n_numero = null;
            string sentencia = null;
            string sentencia_nue = null;
            decimal? n_tasa_calculo = null;
            decimal? n_tas_cal_mora = null;
            string s_efe_nom = null;
            string s_mod = null;
            int? n_per = null;
            int? n_mod_per = null;
            string s_efe_nom_nue = null;
            string s_mod_nue = null;
            decimal? n_per_nue = null;
            decimal? n_mod_per_nue = null;
            int? n_tipo_tasa = null;
            decimal? pn_per = null;
            decimal? pn_mod = null;
            //-- Variables Para la Causacion
            decimal? n_dias = null;
            decimal? n_mora = null;
            decimal? n_dias_temp = null;
            decimal? n_mora_cau = null;
            decimal? n_tot_dias = null;
            decimal? n_termino = null;
            decimal? n_val = null;
            //-- Variables para calculo de interes devuelto por pago anticipado
            bool b_exis_cap = false;
            decimal? n_saldo_actual = null;
            decimal? n_per_diaria = null;
            DateTime? f_fec_prox_cap = null;
            decimal? n_val_tasa_diaria = null;
            decimal? n_val_aux = null;
            decimal? n_dias_cte = null;
            bool b_exis_abono = false;
            decimal? n_tot_abono = null;
            Int64? n_num_ter = 0;
            //-- Variable para calculo de mora de acuerdo a la forma de pago
            bool b_exis_mora_tf = false;
            decimal? n_dias_tf_nomina = null;
            string s_dias_tf_nomina = null;
            string s_sentencia = null;
            int n_num_mora = 0;
            string s_val_ant = null;
            string s_val_nue = null;
            DateTime? f_fecha_cambio = null;
            int? n_for_pag_real = null;
            DateTime? f_fecha_pago_real = null;
            DateTime? f_fec_tf_real = null;
            bool b_ya_cob_mora = false;
            int n_tipo_pago_temp = 0;
            // pCursor SYS_REFCURSOR;
            // PCursorMora SYS_REFCURSOR;
            int n_causa = 0;
            decimal? n_orden = null;
            decimal? n_mes = null;
            DateTime? f_fec_ven_tf = null;
            bool bRetorna = false;
            int n_tipo_pago = 0;
            DateTime f_fecha_pago;

            n_tipo_pago = pn_tipo_pago;
            f_fecha_pago = Convert.ToDateTime(pf_fecha_pago);

            n_causa = 0;
            n_orden = 0;
            n_mes = 0;
            //----------------------------------------------------------------------------------------------------------------------------  
            // Trae interes de atributos financiados y determina la posición del atributo de mora  
            //----------------------------------------------------------------------------------------------------------------------------  
            n_pos_mora = -1;
            n_num = 1;
            while (n_num <= n_atr_fin)
            {
                if (atr_finan[n_num].n_cod_atr == n_atr_mora)
                {
                    n_pos_mora = n_num;
                    break;
                };
                n_num = n_num + 1;
            };
            //----------------------------------------------------------------------------------------------------------------------------  
            // Se determina la tasa de interés diaria del interés de mora.Si General 1050 es 2 no cobra mora sobre T.F.
            //----------------------------------------------------------------------------------------------------------------------------  
            if (n_pos_mora != -1)
            {
                if (BOFunciones.BuscarGeneral(1050, 1) == "1" || BOFunciones.BuscarGeneral(1050, 1) == "2")
                {
                    b_exis_mora_tf = true;
                }
                else
                {
                    b_exis_mora_tf = false;
                };
                if ((n_for_pag == 2 && b_exis_mora_tf) || (BOFunciones.BuscarGeneral(1050, 1) == "2"))
                {
                    n_tasa_mora = 0;
                }
                else
                {
                    n_per_dia = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
                    atr_finan[n_pos_mora].Conv_Tasa(n_per_dia, 2, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                };
            }
            else
            {
                n_tasa_mora = 0;
                b_exis_mora_tf = false;
            };
            //------------------------------------------------------------------------------------------------------------------------------  
            //-- Calcula valores de mora
            //------------------------------------------------------------------------------------------------------------------------------  
            n_error = 0;
            rn_sobrante = 0;
            if ((n_tipo_pago < 1 || n_tipo_pago > 4) && n_tipo_pago != 7)
            {
                n_error = -2;
                return n_error;
            };
            n_tot_cap = 0;
            n_tot_int = 0;
            //------------------------------------------------------------------------------------------------------------------------------  
            //-- Ajusta para tipo_de_pago 7, para que se iguale por numero de terminos fijos  
            //------------------------------------------------------------------------------------------------------------------------------  
            if (n_tipo_pago == 7)
            {
                n_tipo_pago_temp = 7;
                n_tipo_pago = 3;
            };
            if (bterminos)
            {
                // Determina el valor de terminos fijos, monto inicial y el saldo
                n_tot_tf = SumarCapitalCuotasExtras();
                n_sal_tf = SumarSaldoCapitalCuotasExtras(true, f_fecha_pago);
                n_val = n_saldo;
                n_pos = 1;
                n_num_cuo = 1;
                while (n_num_cuoext >= n_pos)
                {
                    n_aux_mora = 0;
                    bRetorna = GetCuotaExtra(ref n_pos, ref n_cap_tf, ref n_int_tf, ref f_fec_tf, ref n_num_ter);
                    if (n_cap_tf > 0 || n_int_tf > 0)
                    {
                        if (f_fec_ven_tf == null || f_fec_tf < f_fec_ven_tf)
                        {
                            f_fec_ven_tf = f_fec_tf;
                        };
                        n_tot_abono = 0;

                        DApackage.ConsultarValorCuotaExtra(n_radic, n_num_ter, ref n_tot_abono, usuario);

                        if (n_tot_abono == 0 || n_tot_abono == null)
                        {
                            n_tot_abono = 0;
                            b_exis_abono = false;
                        }
                        else
                        {
                            b_exis_abono = true;
                        };
                        if (n_cap_tf > 0 || n_int_tf > 0)
                        {
                            // Calcula el interes devuelto de terminos fijos y lo abona al capital
                            if (BOFunciones.BuscarGeneral(930, 1) == "1" || BOFunciones.BuscarGeneral(930, 1) == "2")
                            {
                                b_exis_cap = true;
                            }
                            else
                            {
                                b_exis_cap = false;
                            };
                            if (b_exis_cap)
                            {
                                if (n_tip_paginttf == 1)
                                {
                                    if (BOFunciones.BuscarGeneral(930, 1) == "1")
                                    {
                                        n_saldo_actual = n_saldo;
                                        if (f_fecha_pago >= f_prox_pag)
                                        {
                                            n_dias_cte = 0;
                                        }
                                        else
                                        {
                                            if (f_fecha_pago >= f_fec_tf || BOFunciones.BuscarGeneral(1050, 1) == "2")
                                            {
                                                n_dias_cte = BOFunciones.FecDifDia(f_fecha_pago, f_prox_pag, 1);
                                            }
                                            else
                                            {
                                                n_dias_cte = BOFunciones.FecDifDia(f_fec_tf, f_prox_pag, 1);
                                            };
                                        };
                                        if (n_dias_cte > 0)
                                        {
                                            n_per_diaria = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
                                            n_val_tasa_diaria = 0;
                                            n_num = 1;
                                            while (n_num < n_atr_fin)
                                            {
                                                atr_finan[n_num].Conv_Tasa(Convert.ToInt32(n_per_diaria), n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                                                // No se tiene en cuenta el interes de mora
                                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                {
                                                    n_val_tasa_diaria = n_val_tasa_diaria + n_val_aux;
                                                };
                                                n_num = n_num + 1;
                                            };
                                            n_val_tasa_diaria = n_val_tasa_diaria / 100;
                                            if (s_calc_int == "1")
                                            {
                                                n_val_tasa_diaria = n_val_tasa_diaria * n_dias_cte;
                                            }
                                            else
                                            {
                                                n_val_tasa_diaria = n_val_tasa_diaria + 1;
                                                n_val_tasa_diaria = BOFunciones.Power(n_val_tasa_diaria, n_dias_cte);
                                                n_val_tasa_diaria = (n_val_tasa_diaria - 1);
                                            };
                                            // Si el pago es por valor devuelve intereses solamente sobre la parte del pago.FerOrt. 26-Nov-2009  
                                            if (n_tipo_pago == 1 && n_valor_pago < n_cap_tf)
                                            {
                                                n_int_tf = (n_int_tf + (n_valor_pago * n_val_tasa_diaria)) * -1;
                                                n_cap_tf = n_cap_tf + (n_valor_pago * n_val_tasa_diaria);
                                            }
                                            else
                                            {
                                                n_int_tf = (n_int_tf + (n_cap_tf * n_val_tasa_diaria)) * -1;
                                                n_cap_tf = n_cap_tf + (n_cap_tf * n_val_tasa_diaria);
                                            };
                                            n_int_tf = BOFunciones.Redondeo(n_int_tf);
                                            n_cap_tf = BOFunciones.Redondeo(n_cap_tf);
                                        };
                                    }
                                    else
                                    {
                                        n_saldo_actual = n_saldo;
                                        n_dias_cte = BOFunciones.FecDifDia(f_fec_tf, f_prox_pag, 1);
                                        if (n_dias_cte > 0)
                                        {
                                            n_per_diaria = BOFunciones.CodPeriodicidadDiaria(n_tipo_cal);
                                            n_val_tasa_diaria = 0;
                                            n_num = 1;
                                            while (n_num < n_atr_fin)
                                            {
                                                atr_finan[n_num].Conv_Tasa(Convert.ToInt32(n_per_diaria), n_tip_pago, f_fec_apro, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_val_aux);
                                                // No se tiene en cuenta el interes de mora
                                                if (atr_finan[n_num].n_cod_atr != n_atr_mora)
                                                {
                                                    n_val_tasa_diaria = n_val_tasa_diaria + n_val_aux;
                                                };
                                                n_num = n_num + 1;
                                            };
                                            n_val_tasa_diaria = n_val_tasa_diaria / 100;
                                            n_val_tasa_diaria = n_val_tasa_diaria + 1;
                                            n_val_tasa_diaria = BOFunciones.Power(n_val_tasa_diaria, n_dias_cte);
                                            n_val_tasa_diaria = (n_val_tasa_diaria - 1);
                                            // Si el pago es por valor devuelve intereses solamente sobre la parte del pago.FerOrt. 26-Nov-2009  
                                            if (n_tipo_pago == 1 && n_valor_pago < n_cap_tf)
                                            {
                                                n_int_tf = (n_int_tf + (n_valor_pago * n_val_tasa_diaria)) * -1;
                                                n_cap_tf = n_cap_tf + (n_valor_pago * n_val_tasa_diaria);
                                            }
                                            else
                                            {
                                                n_int_tf = (n_int_tf + (n_cap_tf * n_val_tasa_diaria)) * -1;
                                                n_cap_tf = n_cap_tf + (n_cap_tf * n_val_tasa_diaria);
                                            };
                                            n_int_tf = BOFunciones.Redondeo(n_int_tf);
                                            n_cap_tf = BOFunciones.Redondeo(n_cap_tf);
                                        };
                                    };
                                };
                            };
                            // Calcula el interés de mora, si se cobra
                            if (n_val <= n_cap_tf && n_val > 0)
                            {
                                n_cap_tf = n_val;
                                n_val = 0;
                            }
                            else if (n_val > n_cap_tf)
                            {
                                n_val = n_val - n_cap_tf;
                            }
                            else
                            {
                                n_cap_tf = 0;
                            };
                            // Verifica cuantas veces se ha cambiado la forma de pago y calcula la mora de acuerdo
                            // al tiempo de cada forma de pago
                            n_for_pag_real = n_for_pag;
                            f_fecha_pago_real = f_fecha_pago;
                            f_fec_tf_real = f_fec_tf;
                            b_ya_cob_mora = false;

                            #region calculo mora         

                            var listaCambiosHistoricos = DApackage.ConsultarHistoricoCambiosCredito(n_radic, f_fec_tf, usuario);

                            if (!b_ya_cob_mora)
                            {
                                foreach (var historico in listaCambiosHistoricos)
                                {
                                    f_fecha_cambio = historico.Item1;
                                    s_val_ant = historico.Item2;
                                    s_val_nue = historico.Item3;

                                    b_ya_cob_mora = true;
                                    if (f_fecha_cambio != null)
                                    {
                                        f_fecha_cambio = BOFunciones.DateConstruct(BOFunciones.DateYear(f_fecha_cambio), BOFunciones.DateMonth(f_fecha_cambio), BOFunciones.DateDay(f_fecha_cambio), 0, 0, 0);
                                        n_for_pag = BOFunciones.To_Number(s_val_ant);
                                        f_fecha_pago = Convert.ToDateTime(f_fecha_cambio);
                                    }
                                    else
                                    {
                                        f_fecha_pago = Convert.ToDateTime(f_fecha_pago_real);
                                        n_for_pag = n_for_pag_real;
                                    };
                                    // Calcula el interés de mora, si se cobra.Valida que cobre mora, que exista atributo de mora,
                                    // que la fecha de pago sea menor a la de la cuota y que no haya superado el periodo para cobro sobre saldo insoluto
                                    if (s_cob_mor == "1" && f_fecha_pago > f_fec_tf && n_pos_mora != -1 && (n_for_pag == 1 || (n_for_pag == 2 && !b_exis_mora_tf)))
                                    {
                                        // Determina cuantos dias de gracia para terminos fijos de creditos
                                        // por nomina existen.
                                        n_dias_tf_nomina = BOFunciones.To_Number(BOFunciones.BuscarGeneral(1060, 1));
                                        n_dias_tf_nomina = BOFunciones.To_Number(s_dias_tf_nomina);
                                        if (n_dias_tf_nomina > 0)
                                        {
                                            n_dias_gracia_mora = Convert.ToInt32(n_dias_tf_nomina);
                                        };
                                        // Determina si se cobra sobre  1:saldos insolutos o sobre 2:atributos
                                        if (n_tip_mor == 1)
                                        {
                                            // Calcula en número de días de mora
                                            n_dias_mora = BOFunciones.FecDifDia(f_fec_tf, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                                            if (n_dias_mora <= n_dias_gracia_mora)
                                            {
                                                n_dias_mora = 0;
                                            };
                                            if (n_dias_mora > 0)
                                            {
                                                // Calcula la mora sobre el capital
                                                // Determina si es un calculo de interes 1:Simple, 2:Compuesto
                                                f_fec_prox = f_fec_tf;
                                                n_dias = 0;
                                                n_dias_temp = 0;
                                                n_dias_mora_temp = n_dias_mora;
                                                if (atr_finan[n_pos_mora].n_cod_atr == n_atr_mora)
                                                {
                                                    while (n_dias_mora_temp >= 1)
                                                    {
                                                        if (atr_finan[n_pos_mora].s_tip_cal == "5")
                                                        {
                                                            //Open pCursor For 'Select fecha_final, fecha_inicial From historicotasa Where tipo_historico = :1 and :2 between fecha_inicial and fecha_final'   
                                                            //Using atr_finan(n_pos_mora).n_tip_his, atr_finan(n_pos_mora).n_tip_his;  
                                                            //Fetch pCursor Into f_fec_fin, f_fec_ini;
                                                            //Close pCursor;

                                                            // Esta mal la fecha de rango
                                                            DApackage.ConsultarRangoFechasHistoricoTasa(atr_finan[n_pos_mora].n_tip_his, null, ref f_fec_fin, ref f_fec_ini, usuario);

                                                            if (f_fec_ini < f_fec_prox)
                                                            {
                                                                f_fec_ini = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                            };
                                                            n_dias_dif = BOFunciones.FecDifDia(f_fec_ini, f_fec_fin, 1) + 1;
                                                        }
                                                        else
                                                        {
                                                            f_fec_ini = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                            f_fec_fin = Convert.ToDateTime(f_fec_prox).AddDays(1);
                                                            n_dias_dif = n_dias_mora_temp;
                                                        };
                                                        if (n_dias_dif < n_dias_mora_temp)
                                                        {
                                                            n_dias_mora = n_dias_dif;
                                                        }
                                                        else
                                                        {
                                                            n_dias_mora = n_dias_mora_temp;
                                                        };
                                                        atr_finan[n_pos_mora].Conv_Tasa(n_per_dia, 2, f_fec_prox, s_calc_int, n_monto, n_cod_cliente, s_cod_credi, ref n_tasa_mora);
                                                        // Compara la tasa de interes con la de usura y deja la menor
                                                        s_usura = BOFunciones.BuscarGeneral(681, 1);
                                                        if (s_usura != null)
                                                        {
                                                            b_existe = true;
                                                        }
                                                        else
                                                        {
                                                            b_existe = false;
                                                        };
                                                        if (b_existe)
                                                        {
                                                            n_usura = Convert.ToInt32(BOFunciones.To_Number(s_usura));
                                                            n_tasa_usura = 0;
                                                            g_f_usura = f_fec_prox;

                                                            DApackage.ConsultarTasaUsura(g_f_usura, n_usura, ref n_tasa_usura, ref n_tipo_tasa, usuario);

                                                            if (n_tasa_usura != 0)
                                                            {
                                                                DApackage.ConTipoTasa(n_tipo_tasa, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);

                                                                n_tasa_calculo = BOFunciones.CalTasMod(n_tasa_usura, BOFunciones.To_Number(s_efe_nom), n_per, BOFunciones.To_Number(s_mod), n_mod_per, 1, n_periodic, n_tip_pago, n_periodic, 0);
                                                                if (n_tasa_calculo < n_tasa_mora && n_tasa_calculo > 0)
                                                                {
                                                                    n_tasa_mora = n_tasa_calculo;
                                                                    atr_finan[n_num].n_tasa_calculo = n_tasa_calculo;
                                                                };
                                                            };
                                                        };
                                                        if (s_calc_int == "1")
                                                        {
                                                            n_aux_mora = n_aux_mora + (n_tasa_mora / 100 * n_dias_mora * n_cap_tf);
                                                            n_mora = (n_tasa_mora / 100 * n_dias_mora * n_cap_tf);
                                                        }
                                                        else
                                                        {
                                                            n_aux_mora = n_aux_mora + ((BOFunciones.Power(n_tasa_mora / 100 + 1, n_dias_mora) - 1) * n_cap_tf);
                                                            n_mora = ((BOFunciones.Power(n_tasa_mora / 100 + 1, n_dias_mora) - 1) * n_cap_tf);
                                                        };
                                                        n_mora = BOFunciones.Redondeo(n_mora);
                                                        // Determina los valores causados y del mes, si se esta realizando el proceso de causacion
                                                        if (gn_tipo_pago == 5)
                                                        {
                                                            // CAUSACION  
                                                            // Causar intereses vencidos
                                                            n_dias = n_dias + n_dias_mora;
                                                            if (n_dias <= 90)
                                                            {
                                                                n_causa = n_causa + Convert.ToInt32(n_mora);
                                                            }
                                                            else
                                                            {
                                                                if (90 - n_dias_temp > 0)
                                                                {
                                                                    if (s_calc_int == "1")
                                                                    {
                                                                        n_mora_cau = (n_tasa_mora / 100 * (90 - n_dias_temp) * n_cap_tf);
                                                                    }
                                                                    else
                                                                    {
                                                                        n_mora_cau = ((BOFunciones.Power(n_tasa_mora / 100 + 1, (90 - n_dias_temp)) - 1) * n_cap_tf);
                                                                    };
                                                                    n_mora_cau = BOFunciones.Redondeo(n_mora_cau);
                                                                    n_causa = n_causa + Convert.ToInt32(n_mora_cau);
                                                                    n_orden = n_orden + n_mora - n_mora_cau;
                                                                    n_mora = n_mora - n_mora_cau;
                                                                }
                                                                else
                                                                {
                                                                    n_orden = n_orden + n_mora;
                                                                };
                                                            };
                                                            n_dias_temp = n_dias;
                                                        };
                                                        f_fec_prox = Convert.ToDateTime(f_fec_fin).AddDays(1);
                                                        n_dias_mora_temp = n_dias_mora_temp - n_dias_mora;
                                                    };
                                                    if (s_calc_int == "1")
                                                    {
                                                        n_mes = (n_tasa_mora / 100 * 30 * n_cap_tf);
                                                    }
                                                    else
                                                    {
                                                        n_mes = ((BOFunciones.Power(n_tasa_mora / 100 + 1, 30) - 1) * n_cap_tf);
                                                    };
                                                    n_mes = BOFunciones.Redondeo(n_mes);
                                                };
                                                n_aux_mora = BOFunciones.Redondeo(n_aux_mora);
                                            };
                                        }
                                        else
                                        {
                                            MensajeConsola("El manejo de mora seleccionado no se encuentra definido, verifique parámetros");
                                        };
                                    };
                                    if (true)
                                    { // pCursorMora%NOTFOUND ) {
                                        n_for_pag = BOFunciones.To_Number(s_val_nue);
                                    };
                                };
                            }
                            // Close pCursorMora;
                            #endregion
                            n_for_pag = n_for_pag_real;
                            f_fecha_pago = Convert.ToDateTime(f_fecha_pago_real);
                            //------------------------------------------------------------------------------------------------------------------------------------------  
                            // Determina si se ha realizado algún abono a mora  
                            //------------------------------------------------------------------------------------------------------------------------------------------  
                            n_val_mora = BOFunciones.NVL(n_val_mora, 0);
                            n_cap_tf = BOFunciones.NVL(n_cap_tf, 0);
                            n_int_tf = BOFunciones.NVL(n_int_tf, 0);
                            n_tot_cap = BOFunciones.NVL(n_tot_cap, 0);
                            n_tot_int = BOFunciones.NVL(n_tot_int, 0);
                            n_aux_mora = BOFunciones.NVL(n_aux_mora, 0);
                            // Determina acumulado de acuerdo al tipo de pago
                            if (n_tipo_pago == 1)
                            {
                                if (n_valor_pago >= n_tot_cap + n_tot_int + n_val_mora + n_cap_tf + n_int_tf + n_aux_mora)
                                {
                                    n_tot_cap = n_tot_cap + n_cap_tf;
                                    n_tot_int = n_tot_int + n_int_tf;
                                    n_val_mora = n_val_mora + n_aux_mora;
                                    if (n_valor_pago == n_tot_cap + n_tot_int + n_val_mora)
                                    {
                                        break;
                                    };
                                }
                                else
                                {
                                    if (n_valor_pago >= n_tot_cap + n_tot_int + n_val_mora + n_int_tf + n_aux_mora)
                                    {
                                        n_tot_int = n_tot_int + n_int_tf;
                                        n_val_mora = n_val_mora + n_aux_mora;
                                        n_tot_cap = n_tot_cap + n_valor_pago - (n_tot_int + n_val_mora + n_tot_cap);
                                    }
                                    else if (n_valor_pago >= n_tot_cap + n_tot_int + n_val_mora + n_aux_mora)
                                    {
                                        n_val_mora = n_val_mora + n_aux_mora;
                                        n_tot_int = n_tot_int + n_valor_pago - (n_tot_int + n_val_mora + n_tot_cap);
                                    }
                                    else
                                    {
                                        n_val_mora = n_val_mora + n_valor_pago - (n_tot_int + n_val_mora + n_tot_cap);
                                    };
                                    break;
                                };
                            };
                            if (n_tipo_pago == 2)
                            {
                                if (f_fec_tf <= f_fecha_pago)
                                {
                                    n_tot_cap = n_tot_cap + n_cap_tf;
                                    n_tot_int = n_tot_int + n_int_tf;
                                    n_val_mora = n_val_mora + n_aux_mora;
                                }
                                else
                                {
                                    break;
                                };
                            };
                            if (n_tipo_pago == 3 && n_num_cuo <= n_cuotas_a_pag)
                            {
                                n_tot_cap = n_tot_cap + n_cap_tf;
                                n_tot_int = n_tot_int + n_int_tf;
                                n_val_mora = n_val_mora + n_aux_mora;
                                // Almacena valores en la tabla ries_liq_cre
                                if (n_tipo_pago_temp == 7)
                                {
                                    if (n_cap_tf > 0)
                                    {
                                        DApackage.InsertarHistoricoAmortizaCre(f_fecha_pago, 2, n_radic, f_fec_tf_real, 1, n_cap_tf, usuario);
                                    };
                                    if (n_int_tf > 0)
                                    {
                                        DApackage.InsertarHistoricoAmortizaCre(f_fecha_pago, 2, n_radic, f_fec_tf_real, n_atr_corr, n_int_tf, usuario);
                                    };
                                    if (n_aux_mora > 0)
                                    {
                                        DApackage.InsertarHistoricoAmortizaCre(f_fecha_pago, 2, n_radic, f_fec_tf_real, n_atr_mora, n_aux_mora, usuario);
                                    };
                                };
                                if (n_num_cuo == n_cuotas_a_pag)
                                {
                                    break;
                                };
                            };
                            if (n_tipo_pago == 4)
                            {
                                n_tot_cap = n_tot_cap + n_cap_tf;
                                if (f_fec_tf <= f_fecha_pago)
                                {
                                    n_tot_int = n_tot_int + n_int_tf;
                                };
                                n_val_mora = n_val_mora + n_aux_mora;
                            };
                            n_num_cuo = n_num_cuo + 1;
                        };
                    };
                    n_pos = n_pos + 1;
                };
            }
            else
            {
                n_error = -10;
            };
            rn_tot_capital = n_tot_cap;
            rn_tot_interes = n_tot_int;
            rn_tot_int_mora = n_val_mora;
            return n_error;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Procedimientos y Funciones para el manejo de los valores pendientes del crédito  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        public void Insertar_Amortiza(DateTime pf_fecha_cuota, int pn_cod_atr, decimal pn_valor, decimal pn_tasa, decimal? pn_saldo_base, string ps_estado)
        {
            if (n_cont_amortiza > cl_amortiza_cre.Length)
                Array.Resize(ref cl_amortiza_cre, 1000);
            cl_amortiza_cre[n_cont_amortiza].f_fecha_cuota = pf_fecha_cuota;
            cl_amortiza_cre[n_cont_amortiza].n_cod_atr = pn_cod_atr;
            cl_amortiza_cre[n_cont_amortiza].n_valor = pn_valor;
            if (gn_tipo_pago == 5)
                cl_amortiza_cre[n_cont_amortiza].n_saldo = pn_valor;
            else
                cl_amortiza_cre[n_cont_amortiza].n_saldo = 0;
            cl_amortiza_cre[n_cont_amortiza].n_tasa_int = pn_tasa;
            cl_amortiza_cre[n_cont_amortiza].n_saldo_base = pn_saldo_base;
            cl_amortiza_cre[n_cont_amortiza].s_estado = ps_estado;
            n_cont_amortiza = n_cont_amortiza + 1;
        }

        public void Actualizar_Amortiza(int pn_pos, decimal? pn_valor, decimal? pn_saldo, decimal? pn_tasa, decimal? pn_saldo_base, string ps_estado)
        {
            if (pn_valor != null)
                cl_amortiza_cre[pn_pos].n_valor = pn_valor;
            if (pn_saldo != null)
                cl_amortiza_cre[pn_pos].n_saldo = pn_saldo;
            if (pn_tasa != null)
                cl_amortiza_cre[pn_pos].n_tasa_int = pn_tasa;
            if (pn_saldo_base != null)
                cl_amortiza_cre[pn_pos].n_saldo_base = pn_saldo_base;
            if (s_estado != null)
                cl_amortiza_cre[pn_pos].s_estado = ps_estado;
        }

        public void Cargar_Amortiza(DateTime? pf_fecha_cuota, DateTime? pf_fecha_pago, int? pn_tipo_pago)
        {
            int n_cont_amo;
            int n_cont_fec;
            decimal? n_valor_pago;
            DateTime? f_fecha_aux;
            string g_sentencia;
            DateTime? f_fecha_cuota = null;
            int? n_cod_atr = null;
            decimal? n_valor = null;
            decimal? n_saldo = null;
            string s_estado = null;

            n_cont_amo = 1;
            n_cont_fec = 1;
            f_fecha_aux = null;

            // Open pBasedato For Select fecha_cuota, cod_atr, valor, saldo, estado From amortiza_cre Where numero_radicacion = n_radic And fecha_cuota >= pf_fecha_cuota Order by fecha_cuota, cod_atr;
            var listaAmortiza = DApackage.InfCargaAmortiza(n_radic, pf_fecha_cuota, ref f_fecha_cuota, ref n_cod_atr, ref n_valor, ref n_saldo, ref s_estado, usuario);

            foreach (var amortiza in listaAmortiza)
            {
                f_fecha_cuota = amortiza.Item1;
                n_cod_atr = amortiza.Item2;
                n_valor = amortiza.Item3;
                n_saldo = amortiza.Item4;
                s_estado = amortiza.Item5;

                Array.Resize(ref cl_amortiza_cre, 1);
                cl_amortiza_cre[n_cont_amo].f_fecha_cuota = f_fecha_cuota;
                cl_amortiza_cre[n_cont_amo].n_cod_atr = n_cod_atr;
                cl_amortiza_cre[n_cont_amo].n_valor = n_valor;
                cl_amortiza_cre[n_cont_amo].n_saldo = n_saldo;
                cl_amortiza_cre[n_cont_amo].s_estado = s_estado;
                if (f_fecha_aux != cl_amortiza_cre[n_cont_amo].f_fecha_cuota || f_fecha_aux == null)
                {
                    if (n_cont_fec > f_fechas_pago.Length)
                        Array.Resize(ref f_fechas_pago, 10);
                    f_fechas_pago[n_cont_fec] = cl_amortiza_cre[n_cont_amo].f_fecha_cuota;
                    n_cont_fec = n_cont_fec + 1;
                    f_fecha_aux = cl_amortiza_cre[n_cont_amo].f_fecha_cuota;
                }

                if ((pn_tipo_pago == 5 || gn_tipo_pago == 7) && cl_amortiza_cre[n_cont_amo].n_saldo < cl_amortiza_cre[n_cont_amo].n_valor)
                {
                    n_valor_pago = 0;

                    n_valor_pago = DApackage.ValorAmortiza(n_radic, f_fecha_cuota, n_cod_atr, pf_fecha_pago, usuario);

                    if (n_valor_pago > 0)
                    {
                        cl_amortiza_cre[n_cont_amo].n_saldo = cl_amortiza_cre[n_cont_amo].n_saldo + Convert.ToDecimal(BOFunciones.NVL(n_valor_pago, Convert.ToDecimal(0)));
                        if ((pn_tipo_pago == 5 || gn_tipo_pago == 7) && cl_amortiza_cre[n_cont_amo].s_estado == null)
                        {
                            //-- Esto es para validar el saldo de los pendientes para la causación
                            if (cl_amortiza_cre[n_cont_amo].n_saldo > cl_amortiza_cre[n_cont_amo].n_valor)
                                cl_amortiza_cre[n_cont_amo].n_saldo = cl_amortiza_cre[n_cont_amo].n_valor;
                        }
                    }
                }
                n_cont_amo = n_cont_amo + 1;
                Array.Resize(ref cl_amortiza_cre, 1);
            }
            n_cont_amortiza = n_cont_amo;
        }

        public bool Buscar_Amortiza(DateTime? pf_fecha_cuota, int? pn_cod_atr, ref decimal? rn_valor, ref int? rn_pos)
        {
            int n_cont = 0;
            bool b_encontro;
            DateTime? f_fecha_cuota;

            f_fecha_cuota = pf_fecha_cuota;
            //n_cont = VisArrayFindDateTimeFechas(1, f_fecha_cuota);
            if (n_cont == -1)
                n_cont = 1;
            rn_valor = 0;
            b_encontro = false;
            rn_pos = null;
            while (n_cont > 0 && n_cont < n_cont_amortiza)
            {
                if (cl_amortiza_cre[n_cont].f_fecha_cuota == f_fecha_cuota && cl_amortiza_cre[n_cont].n_cod_atr == pn_cod_atr)
                {
                    if (cl_amortiza_cre[n_cont].s_estado == "0")
                        rn_valor = cl_amortiza_cre[n_cont].n_valor;
                    else
                        rn_valor = cl_amortiza_cre[n_cont].n_saldo;
                    rn_pos = n_cont;
                    b_encontro = true;
                    break;
                }
                n_cont = n_cont + 1;
            }
            return b_encontro;
        }

        public bool Grabar_Amortiza()
        {
            int n_cont;
            long? n_consecutivo = 0;
            int n_aux_pos;

            n_cont = 1;
            while (n_cont < n_cont_amortiza)
            {
                if (cl_amortiza_cre[n_cont].n_saldo_base == null)
                    cl_amortiza_cre[n_cont].n_saldo_base = 0;
                if (cl_amortiza_cre[n_cont].s_estado == "0")
                {
                    cl_amortiza_cre[n_cont].s_estado = "1";
                    cl_amortiza_cre[n_cont].n_tasa_int = BOFunciones.Trunc(cl_amortiza_cre[n_cont].n_tasa_int);

                    n_consecutivo = Consecutivos("1");
                    n_aux_pos = 0;
                    while (true)
                    {
                        try
                        {
                            DApackage.BorrarAmortizaCre(n_radic, cl_amortiza_cre[n_cont].f_fecha_cuota, cl_amortiza_cre[n_cont].n_cod_atr, usuario);

                            if (!(n_tip_gracia == 4 && s_atr_gracia == "1" && n_duracion_gracia == n_num_cuotas - 1)) //-- No grabar datos de pendientes para créditos cuyo pago de capital es al final
                            {
                                DApackage.InsertarAmortizaCredito(n_consecutivo, n_radic, cl_amortiza_cre[n_cont].n_cod_atr, cl_amortiza_cre[n_cont].f_fecha_cuota, cl_amortiza_cre[n_cont].n_valor, cl_amortiza_cre[n_cont].n_saldo, cl_amortiza_cre[n_cont].n_saldo_base, cl_amortiza_cre[n_cont].n_tasa_int, 0, cl_amortiza_cre[n_cont].s_estado, usuario);
                            }

                            n_consecutivo = n_consecutivo + 1;
                            break;
                        }
                        catch
                        {
                            n_consecutivo = Consecutivos("1");
                        }

                        n_aux_pos = n_aux_pos + 1;
                        if (n_aux_pos > 10000)
                            break;
                    }
                }
                else if (cl_amortiza_cre[n_cont].s_estado == "1" && (gn_tipo_pago != 5 || gn_tipo_pago == null))
                {
                    DApackage.ActualizarAmortizaCre(n_radic, cl_amortiza_cre[n_cont].f_fecha_cuota, cl_amortiza_cre[n_cont].n_cod_atr, cl_amortiza_cre[n_cont].n_saldo, usuario); ;
                }
                n_cont = n_cont + 1;
            }
            return true;
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Procedimientos y Funciones para el manejo de los valores en mora  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  

        public void Insertar_Mora(DateTime pf_fecha_cuota, int? pn_cod_atr, decimal? pn_valor, decimal? pn_tasa, decimal? pn_saldo_base, DateTime? pf_fecha_ini, DateTime? pf_fecha_fin, decimal? pn_dias_mora, string ps_estado)
        {
            try
            {
                //if (n_cont_mora == null)
                //    n_cont_mora = 1;
                if (n_cont_mora < 1)
                    n_cont_mora = 1;
                cl_det_mora_cre[n_cont_mora].f_fecha_cuota = pf_fecha_cuota;
                cl_det_mora_cre[n_cont_mora].n_cod_atr = pn_cod_atr;
                cl_det_mora_cre[n_cont_mora].n_valor = pn_valor;
                if (gn_tipo_pago == 5)
                {
                    cl_det_mora_cre[n_cont_mora].n_saldo = pn_valor;
                }
                else
                {
                    cl_det_mora_cre[n_cont_mora].n_saldo = pn_valor;
                    cl_det_mora_cre[n_cont_mora].s_estado_pago = "1";
                }
                cl_det_mora_cre[n_cont_mora].n_tasa_int = pn_tasa;
                cl_det_mora_cre[n_cont_mora].n_valor_base = pn_saldo_base;
                cl_det_mora_cre[n_cont_mora].f_fecha_ini = pf_fecha_ini;
                cl_det_mora_cre[n_cont_mora].f_fecha_fin = pf_fecha_fin;
                cl_det_mora_cre[n_cont_mora].n_dias_mora = Convert.ToInt32(pn_dias_mora);
                cl_det_mora_cre[n_cont_mora].s_estado = ps_estado;
                n_cont_mora = n_cont_mora + 1;
            }
            catch { return; }
        }

        public void Cargar_Mora(DateTime? pf_fecha_cuota, DateTime? pf_fecha_pago, int? pn_tipo_pago)
        {
            int n_cont_amo;
            int n_cont_fec;
            decimal? n_valor_pago = null;
            DateTime? f_fecha_aux;
            DateTime? f_fecha_aux_ini;
            string g_sentencia;

            n_cont_amo = 1;
            n_cont_fec = 1;
            f_fecha_aux = null;
            f_fecha_aux_ini = null;

            List<DetalleMoraCredito> listaDetalleMora = DApackage.ListarDetalleMoraCredito(n_radic, pf_fecha_cuota, usuario);

            if (pn_tipo_pago == 5 || gn_tipo_pago == 7)
            {
                n_valor_pago = 0;
                n_valor_pago = DApackage.ValorAmortiza(n_radic, pf_fecha_cuota, n_atr_mora, pf_fecha_pago, usuario);
            }

            foreach (DetalleMoraCredito detalle in listaDetalleMora)
            {
                cl_det_mora_cre[n_cont_amo].f_fecha_cuota = detalle.f_fecha_cuota;
                cl_det_mora_cre[n_cont_amo].n_cod_atr = detalle.n_cod_atr;
                cl_det_mora_cre[n_cont_amo].n_valor = detalle.n_valor;
                cl_det_mora_cre[n_cont_amo].n_saldo = detalle.n_saldo;
                cl_det_mora_cre[n_cont_amo].f_fecha_ini = detalle.f_fecha_ini;
                cl_det_mora_cre[n_cont_amo].f_fecha_fin = detalle.f_fecha_fin;
                cl_det_mora_cre[n_cont_amo].s_estado = detalle.s_estado;
                cl_det_mora_cre[n_cont_amo].n_dias_mora = detalle.dias_mora;

                if (f_fecha_aux != cl_det_mora_cre[n_cont_amo].f_fecha_cuota && f_fecha_aux_ini != cl_det_mora_cre[n_cont_amo].f_fecha_ini)
                {
                    f_fechas_mora[n_cont_amo] = cl_det_mora_cre[n_cont_amo].f_fecha_cuota;
                    f_fecha_aux = cl_det_mora_cre[n_cont_amo].f_fecha_cuota;
                    f_fecha_aux_ini = cl_det_mora_cre[n_cont_amo].f_fecha_ini;
                }
                if (pn_tipo_pago == 5 || gn_tipo_pago == 7)
                {
                    if (n_valor_pago > 0 && (cl_det_mora_cre[n_cont_amo].n_saldo == 0 || cl_det_mora_cre[n_cont_amo].n_saldo < cl_det_mora_cre[n_cont_amo].n_valor))
                    {
                        if (n_valor_pago > cl_det_mora_cre[n_cont_amo].n_valor)
                        {
                            cl_det_mora_cre[n_cont_amo].n_saldo = cl_det_mora_cre[n_cont_amo].n_valor;
                            n_valor_pago = n_valor_pago - cl_det_mora_cre[n_cont_amo].n_valor;
                        }
                        else
                        {
                            cl_det_mora_cre[n_cont_amo].n_saldo = BOFunciones.NVL(n_valor_pago, 0);
                            n_valor_pago = 0;
                        }
                    }
                }
                n_cont_amo = n_cont_amo + 1;
            }
            n_cont_mora = n_cont_amo;
            n_cont_mora_ini = n_cont_mora;
        }

        public bool Buscar_Mora(DateTime? f_fecha_cuota, int? pn_cod_atr, DateTime? pf_fecha_ini, DateTime? pf_fecha_fin, ref decimal? rn_valor, ref int? rn_pos)
        {
            int n_cont;
            bool b_encontro;
            DateTime? pf_fecha_cuota;

            pf_fecha_cuota = f_fecha_cuota;
            n_cont = VisArrayFindDateTimeFechas(2, pf_fecha_cuota);
            if (n_cont == -1)
                n_cont = 1;
            rn_valor = 0;
            b_encontro = false;
            rn_pos = null;
            while (n_cont < n_cont_mora && n_cont <= cl_det_mora_cre.Length)
            {
                if (pn_cod_atr == 3)
                {
                    if (BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_cuota) == BOFunciones.Trunc(pf_fecha_cuota) && BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_ini) == BOFunciones.Trunc(pf_fecha_ini) && BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_fin) == BOFunciones.Trunc(pf_fecha_fin))
                    {
                        rn_valor = rn_valor + cl_det_mora_cre[n_cont].n_saldo;
                        cl_det_mora_cre[n_cont].s_estado_pago = "1";
                        rn_pos = n_cont;
                        b_encontro = true;
                    }
                    else if (BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_cuota) > BOFunciones.Trunc(pf_fecha_cuota))
                        break;
                }
                else
                {
                    if (BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_cuota) == BOFunciones.Trunc(pf_fecha_cuota) && cl_det_mora_cre[n_cont].n_cod_atr == pn_cod_atr && BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_ini) == BOFunciones.Trunc(pf_fecha_ini) && BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_fin) == BOFunciones.Trunc(pf_fecha_fin))
                    {
                        rn_valor = cl_det_mora_cre[n_cont].n_saldo;
                        cl_det_mora_cre[n_cont].s_estado_pago = "1";
                        rn_pos = n_cont;
                        b_encontro = true;
                        break;
                    }
                }
                n_cont = n_cont + 1;
            }
            return b_encontro;
        }

        public void Actualizar_Mora(DateTime? f_fecha_cuota, ref decimal? pn_valor, decimal? pn_saldo)
        {
            int n_cont;
            DateTime? pf_fecha_cuota;

            pf_fecha_cuota = f_fecha_cuota;
            n_cont = VisArrayFindDateTimeFechas(2, pf_fecha_cuota);
            if (n_cont == -1)
                n_cont = 1;
            while (n_cont < n_cont_mora && pn_valor > 0)
            {
                if (BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_cuota) == BOFunciones.Trunc(pf_fecha_cuota))
                {
                    if (cl_det_mora_cre[n_cont].s_estado_pago == "1")
                    {
                        if (cl_det_mora_cre[n_cont].n_valor <= pn_valor)
                        {
                            pn_valor = pn_valor - cl_det_mora_cre[n_cont].n_valor;
                            cl_det_mora_cre[n_cont].n_saldo = 0;
                        }
                        else
                        {
                            cl_det_mora_cre[n_cont].n_saldo = cl_det_mora_cre[n_cont].n_valor - pn_valor;
                            pn_valor = 0;
                        }
                    }
                    else
                    {
                        cl_det_mora_cre[n_cont].n_saldo = 0;
                        cl_det_mora_cre[n_cont].s_estado = "2";
                    }
                }
                else if (cl_det_mora_cre[n_cont].f_fecha_cuota > pf_fecha_cuota)
                {
                    break;
                }
                n_cont = n_cont + 1;
            }
            if (pn_valor > 0 && n_cont_mora != n_cont_mora_ini)
            {
                n_cont = n_cont_mora_ini;
                while (n_cont < n_cont_mora && pn_valor > 0)
                {
                    if (BOFunciones.Trunc(cl_det_mora_cre[n_cont].f_fecha_cuota) == BOFunciones.Trunc(pf_fecha_cuota))
                    {
                        if (cl_det_mora_cre[n_cont].s_estado_pago == "1")
                        {
                            if (cl_det_mora_cre[n_cont].n_valor <= pn_valor)
                            {
                                pn_valor = pn_valor - cl_det_mora_cre[n_cont].n_valor;
                                cl_det_mora_cre[n_cont].n_saldo = 0;
                            }
                            else
                            {
                                cl_det_mora_cre[n_cont].n_saldo = cl_det_mora_cre[n_cont].n_valor - pn_valor;
                                pn_valor = 0;
                            }
                        }
                        else
                        {
                            cl_det_mora_cre[n_cont].n_saldo = 0;
                            cl_det_mora_cre[n_cont].s_estado = "2";
                        }
                    }
                    else if (cl_det_mora_cre[n_cont].f_fecha_cuota > pf_fecha_cuota)
                    {
                        break;
                    }
                    n_cont = n_cont + 1;
                }
            }
        }

        public bool Grabar_Mora()
        {
            int n_cont;
            int n_consecutivo = 0;
            int n_aux_pos = 0;
            bool bOk;

            n_cont = 1;

            n_consecutivo = DApackage.ConsultarSiguienteValorDetalleMoraSecuencia(usuario);

            while (n_cont < n_cont_mora)
            {
                cl_det_mora_cre[n_cont].n_tasa_int = BOFunciones.Round(cl_det_mora_cre[n_cont].n_tasa_int * 10000) / 10000;
                if (cl_det_mora_cre[n_cont].s_estado == "0")
                {
                    cl_det_mora_cre[n_cont].s_estado = "1";

                    n_consecutivo = DApackage.ConsultarSiguienteValorDetalleMoraSecuencia(usuario);

                    DApackage.InsertarDetalleMoraCredito(n_consecutivo, n_radic, cl_det_mora_cre[n_cont].f_fecha_cuota, cl_det_mora_cre[n_cont].n_cod_atr, cl_det_mora_cre[n_cont].n_valor, cl_det_mora_cre[n_cont].n_saldo, cl_det_mora_cre[n_cont].n_tasa_int, cl_det_mora_cre[n_cont].n_valor_base, cl_det_mora_cre[n_cont].f_fecha_ini, cl_det_mora_cre[n_cont].f_fecha_fin, cl_det_mora_cre[n_cont].n_dias_mora, cl_det_mora_cre[n_cont].s_estado, usuario);

                    n_consecutivo = n_consecutivo + 1;
                }
                else if (gn_tipo_pago != 5)
                {
                    DApackage.ActualizarDetalleMoraCredito(n_radic, cl_det_mora_cre[n_cont].f_fecha_cuota, cl_det_mora_cre[n_cont].n_cod_atr, cl_det_mora_cre[n_cont].n_saldo, cl_det_mora_cre[n_cont].f_fecha_ini, cl_det_mora_cre[n_cont].f_fecha_fin, cl_det_mora_cre[n_cont].s_estado, usuario);
                }
                n_cont = n_cont + 1;
            }
            return true;
        }

        public int VisArrayFindDateTimeFechas(int? ntipo, DateTime? f_prox_pago)
        {
            int numero;
            int pos;

            if (ntipo == 1)
            {
                numero = f_fechas_pago.Length;
                if (numero <= 0)
                    return -1;
                pos = 1;
                while (pos <= numero)
                {
                    if (f_fechas_pago[pos] == f_prox_pago)
                        return pos;
                    pos = pos + 1;
                }
            }
            else if (ntipo == 2)
            {
                numero = f_fechas_mora.Length;
                if (numero <= 0)
                    return -1;
                pos = 1;
                while (pos <= numero)
                {
                    if (f_fechas_mora[pos] == f_prox_pago)
                        return pos;
                    pos = pos + 1;
                }
            }
            return -1;
        }

        private bool Pagar(Int64 pn_cod_ope, DateTime pf_fecha_pago, ref Int64? pn_cod_det_lis, int? pn_tipo_tran, ref DateTime? f_proximo_pago, ref decimal? n_tot_capital, ref decimal?[] n_cod_atributos, ref decimal?[] n_tot_atributos, ref int n_num_atr, Int64 pn_cod_usu)
        {
            int n_cont_rep = 0;
            bool b_grabar = false;
            int n_cont = 0;
            string g_sentencia = null;
            // pBasedato SYS_REFCURSOR;
            int n_num_cuo = 0;
            DateTime? f_fec_aux = null;
            int n_dias = 0;
            Int64 n_num_tran = 0;
            decimal? n_val_capitaliza = null;
            Int64 n_consecutivo = 0;
            int? n_cod_atr = null;
            decimal? n_capitaliza_ctrr = null;
            int? n_num_pago = 0;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_saldo = null;
            decimal? n_valor_pago = null;
            string s_estado_pago = null;
            decimal? n_tasa = null;
            decimal? dias_mora = null;
            DateTime? f_prox_pago = null;
            Int64 n_nov_cre = 0;
            DateTime? f_fec_act = null;
            DateTime? f_hor_act = null;
            Int64 lidcuenta = 0;
            decimal? lsaldo = null;
            decimal? lvalor_aplica = null;
            // pCuentas SYS_REFCURSOR;
            DateTime? fecultcie = null;
            string lcod_linea_aporte = null;
            Int64 lnumero_aporte = 0;
            // -- Variables para reliquidación
            decimal? l_cuota = null;
            decimal? l_tasa_interes = null;
            decimal? l_num_cuotas = 0;
            decimal? l_adi_monto = null;
            decimal? l_des_cheque = null;
            DateTime? l_fecha_ini = null;
            decimal? l_dias_aju = null;
            string s_atr_aporte = null;

            b_grabar = true;
            //-- Atributo para Aportes
            s_atr_aporte = BOFunciones.BuscarGeneral(250, 1);
            if (s_atr_aporte != "" || s_atr_aporte != null)
            {
                n_atr_APORTE = Convert.ToInt32(s_atr_aporte);
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Validar la fecha  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            fecultcie = DApackage.ConsultarFechaUltimoCierre(usuario);

            if (!fecultcie.HasValue)
            {
                Mensaje_Error(@"No se pudo hallar la fecha de ultimo cierre");
            }
            else if (pf_fecha_pago <= fecultcie)
            {
                Mensaje_Error(@"La fecha de pago " + pf_fecha_pago + " no puede ser anterior o igual a la fecha del último cierre. (" + fecultcie);
            };
            if (f_fec_desembolso != null)
            {
                if (BOFunciones.Trunc(pf_fecha_pago) < BOFunciones.Trunc(f_fec_desembolso))
                {
                    Mensaje_Error("La fecha de pago " + pf_fecha_pago + " no puede ser anterior a la fecha de desembolso. (" + f_fec_desembolso);
                };
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //---- Calculando el valor total pagado por los atributos financiados  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            n_tot_interes = 0;
            n_cont = 1;
            while (n_cont <= n_num_atr && n_cont <= n_tot_atributos.Length)
            {
                if (n_tot_atributos[n_cont] != null)
                {
                    n_tot_interes = n_tot_interes + n_tot_atributos[n_cont];
                };
                n_cont = n_cont + 1;
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //----- Calcula nuevos valores del desembolso  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_tot_capital > n_saldo)
            {
                return false;
            };
            if (n_tot_capital == null)
            {
                n_tot_capital = 0;
            };
            n_saldo = n_saldo - n_tot_capital;
            if (n_saldo <= 0)
            {
                s_estado = "2";
            };
            //---- Determina el nùmero de días según la periodicidad
            n_dias = DApackage.ConsultarDiasDeUnaPeriodicidad(n_periodic, usuario);

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //----- Calculando el numero de cuotas pagadas  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (pn_tipo_tran == 6)
            {
                f_proximo_pago = f_prox_pag;
                n_num_cuo = 0;
            }
            else
            {
                if (f_proximo_pago == null)
                {
                    f_proximo_pago = f_prox_pag;
                };
                n_num_cuo = 0;
                f_fec_aux = f_prox_pag;
                while (f_fec_aux < f_proximo_pago)
                {
                    f_fec_aux = BOFunciones.FecSumDia(f_fec_aux, n_dias, Convert.ToInt32(n_tipo_cal));
                    n_num_cuo = n_num_cuo + 1;
                };
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //----- Valida si no se generan cuotas pendientes que avance por lo menos una vez la fecha  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_num_cuo == 0 && n_num_par <= 1 && pn_tipo_tran != 6)
            {
                f_proximo_pago = BOFunciones.FecSumDia(f_prox_pag, n_dias, Convert.ToInt32(n_tipo_cal));
                if (BOFunciones.DateMonth(f_prox_pag) == 2 && BOFunciones.DateDay(f_prox_pag) >= 28 && BOFunciones.DateMonth(f_fec_apro) != 2 && (BOFunciones.DateDay(f_fec_apro) == 28 || BOFunciones.DateDay(f_fec_apro) == 29))
                {
                    if (n_dias == 30 || n_dias == 90 || n_dias == 180 || n_dias == 360)
                    {
                        f_proximo_pago = BOFunciones.DateConstruct(BOFunciones.DateYear(f_proximo_pago), BOFunciones.DateMonth(f_proximo_pago), BOFunciones.DateDay(f_prox_pag), 0, 0, 0);
                    };
                };
                n_num_cuo = n_num_cuo + 1;
            };

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //------ Guarda Copia de la Informacion de la Información de desembolso antes de ser actualizada - Desembolso  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  

            //-- Para pago total guardar auditoria de saldos pendientes
            DApackage.InsertarCreditoAuditoria(n_radic, pn_cod_ope, usuario);
            DApackage.InsertarAmortizaCreditoAuditoria(n_radic, pn_cod_ope, usuario);

            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            //------- Actualiza la fecha y el número de cuotas pagas si no se cancela ningun valor de capital excepto para abono a capital  
            //------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (f_prox_pag != f_proximo_pago && n_tot_capital <= 0 && pn_tipo_tran != 6)
            {
                DApackage.ActualizarCredito(n_num_cuo, f_proximo_pago, pf_fecha_pago, n_radic, usuario);
            }
            else
            {
                DApackage.ActualizarFechaUltimoPagoCredito(pf_fecha_pago, n_radic, usuario);
            };
            //-- Coloca el capital como un atributo más
            if (n_tot_capital != 0)
            {
                n_num_atr = n_num_atr + 1;
                n_cod_atributos[n_num_atr] = 1;
                n_tot_atributos[n_num_atr] = n_tot_capital;
            };
            //-- Calcula en numero de transaccion consecutivo
            n_num_tran = 0;
            //----- Actualizando datos del desembolso y saldos por atributos

            //-- Determina la terminación de credito, actualiza el estado y inserta la novedad de terminación
            #region termina crédito
            if (n_saldo <= 0)
            {
                //-- Actualiza estado del crédito depende si es servicio o credito
                DApackage.ActualizarEstadoCredito("T", n_radic, usuario);

                //-- Crea novedad de terminacion
                n_nov_cre = Convert.ToInt64(Consecutivos("4"));
                f_fec_act = DateTime.Now;
                f_hor_act = DateTime.Now;

                DApackage.InsertarNovedadCredito(n_nov_cre, 302, pn_cod_usu, n_radic, f_fec_act, f_hor_act, string.Empty, "1", usuario);
            };
            #endregion
            //-- Graba la Informacion Detallada dela Amortizacion
            #region
            if (!Grabar_Amortiza())
            {
                b_grabar = false;
            };
            if (!Grabar_Mora())
            {
                b_grabar = false;
            };
            #endregion
            //-- Actualizar la cuota para créditos cuyo capital se paga al final
            #region actualizar la cuota
            if ((n_tip_gracia == 4 && s_atr_gracia == "1" && n_duracion_gracia == n_num_cuotas - 1) && n_saldo > 0)
            {
                b_calcular_ini = false;
                n_monto = n_saldo;
                {
                    if (Liquidar())
                    {
                        if (Leer_Datos(ref l_cuota, ref l_tasa_interes, ref l_num_cuotas, ref l_adi_monto, ref l_des_cheque, ref l_fecha_ini, ref l_dias_aju))
                        {
                            DApackage.ActualizarValorCuotaCredito(l_cuota, n_radic, usuario);

                            n_nov_cre = Convert.ToInt64(Consecutivos("4"));
                            f_fec_act = DateTime.Now;
                            f_hor_act = DateTime.Now;

                            string nCuotaDescripcion = n_cuota.HasValue ? n_cuota.Value.ToString() : string.Empty;
                            DApackage.InsertarNovedadCredito(n_nov_cre, 308, pn_cod_usu, n_radic, f_fec_act, f_hor_act, nCuotaDescripcion, "1", usuario);
                        };
                    };
                };
            };
            #endregion
            return b_grabar;
        }

        private bool Pagar_CuoExt(Int64? pn_cod_ope, ref DateTime pf_fecha_pago, ref Int64? pn_cod_det_lis, int? pn_tipo_tran, ref decimal? n_tot_capital, ref decimal? n_tot_interes, ref decimal? n_tot_int_mora, Int64 pn_cod_usu)
        {
            bool b_grabar = false;
            string s_sentencia = null;
            Int64? n_num_tran = null;
            Int64? n_nov_cre = null;
            bool b_existe = false;
            DateTime? f_fec_act = null;
            DateTime? f_hor_act = null;
            //-- Variables para actualizar cada termino fijo
            int? n_err = null;
            Int64? n_num_ter = null;
            decimal? n_val_ter = null;
            decimal? n_pagado = null;
            DateTime? f_pago = null;
            decimal? n_cap_tf = null;
            decimal? n_mora_tf = null;
            decimal? n_int_tf = null;
            // pCursor SYS_REFCURSOR;
            string s_num_ter = null;
            decimal? n_sobrante = null;
            Int64? nRetorna = null;
            decimal? f_fecha_pago = null;

            b_grabar = true;
            if (n_tot_capital > n_saldo)
            {
                return false;
            };
            if (n_tot_capital == null)
            {
                n_tot_capital = 0;
            };
            n_saldo = n_saldo - n_tot_capital;
            if (n_saldo <= 0)
            {
                s_estado = "2";
            };

            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Guarda Copia de la Informacion de la Información de desembolso antes de ser actualizada - Desembolso  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            DApackage.InsertarCreditoAuditoria(n_radic, pn_cod_ope, usuario);

            //------------------------------------------------------------------------------------------------------------------------------------------------------      
            //-- Actualiza el saldo del credito y el estado si es necesario  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            DApackage.ActualizarSaldoEstadoYFechaCredito(pf_fecha_pago, n_saldo, s_estado, n_radic, usuario);

            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Crea la transaccion de capital  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_tot_capital != 0)
            {
                TRAN_CRED transaccionParaCrear = new TRAN_CRED
                {
                    num_tran = n_num_tran.HasValue ? Convert.ToInt32(n_num_tran) : 0,
                    cod_ope = pn_cod_ope.HasValue ? Convert.ToInt32(pn_cod_ope) : 0,
                    numero_radicacion = n_radic.HasValue ? Convert.ToInt32(n_radic) : 0,
                    cod_cliente = n_cod_cliente.HasValue ? Convert.ToInt32(n_cod_cliente) : 0,
                    cod_linea_credito = s_cod_credi,
                    tipo_tran = pn_tipo_tran.HasValue ? pn_tipo_tran.Value : 0,
                    cod_det_lis = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis.Value) : 0,
                    cod_atr = 1,
                    valor = n_tot_capital.HasValue ? Convert.ToDecimal(n_tot_capital) : 0,
                    valor_mes = 0,
                    valor_causa = 0,
                    valor_orden = 0,
                    estado = 1,
                    num_tran_anula = 0
                };

                DApackage.InsertarTransaccionCredito(transaccionParaCrear, usuario);
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------            
            //-- Crea transaccion de interes  
            //------------------------------------------------------------------------------------------------------------------------------------------------------    
            if (n_tot_interes != 0)
            {
                TRAN_CRED transaccionParaCrear = new TRAN_CRED
                {
                    num_tran = n_num_tran.HasValue ? Convert.ToInt32(n_num_tran) : 0,
                    cod_ope = pn_cod_ope.HasValue ? Convert.ToInt32(pn_cod_ope) : 0,
                    numero_radicacion = n_radic.HasValue ? Convert.ToInt32(n_radic) : 0,
                    cod_cliente = n_cod_cliente.HasValue ? Convert.ToInt32(n_cod_cliente) : 0,
                    cod_linea_credito = s_cod_credi,
                    tipo_tran = pn_tipo_tran.HasValue ? pn_tipo_tran.Value : 0,
                    cod_det_lis = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis.Value) : 0,
                    cod_atr = n_atr_corr.HasValue ? n_atr_corr.Value : 0,
                    valor = n_tot_interes.HasValue ? Convert.ToDecimal(n_tot_interes) : 0,
                    valor_mes = 0,
                    valor_causa = 0,
                    valor_orden = 0,
                    estado = 1,
                    num_tran_anula = 0
                };

                DApackage.InsertarTransaccionCredito(transaccionParaCrear, usuario);
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Crea transaccion de interes de mora  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (n_tot_int_mora != 0)
            {
                TRAN_CRED transaccionParaCrear = new TRAN_CRED
                {
                    num_tran = n_num_tran.HasValue ? Convert.ToInt32(n_num_tran) : 0,
                    cod_ope = pn_cod_ope.HasValue ? Convert.ToInt32(pn_cod_ope) : 0,
                    numero_radicacion = n_radic.HasValue ? Convert.ToInt32(n_radic) : 0,
                    cod_cliente = n_cod_cliente.HasValue ? Convert.ToInt32(n_cod_cliente) : 0,
                    cod_linea_credito = s_cod_credi,
                    tipo_tran = pn_tipo_tran.HasValue ? pn_tipo_tran.Value : 0,
                    cod_det_lis = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis.Value) : 0,
                    cod_atr = n_atr_mora.HasValue ? n_atr_mora.Value : 0,
                    valor = n_tot_interes.HasValue ? Convert.ToDecimal(n_tot_interes) : 0,
                    valor_mes = 0,
                    valor_causa = 0,
                    valor_orden = 0,
                    estado = 1,
                    num_tran_anula = 0
                };

                DApackage.InsertarTransaccionCredito(transaccionParaCrear, usuario);
            };
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Guarda Copia de la Informacion de la Información de desembolso antes de ser actualizada - Desembolso  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            DApackage.InsertarCuotasExtrasAuditoria(pn_cod_ope, pf_fecha_pago, n_radic, usuario);

            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Actualiza cada una de las primas o terminos fijos  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            n_pagado = n_tot_capital;
            var listaCuotasExtras = DApackage.ListarCuotasExtras(n_radic, usuario);

            foreach (var cuotaExtra in listaCuotasExtras)
            {
                n_num_ter = cuotaExtra.Item1;
                n_val_ter = cuotaExtra.Item2;
                f_pago = cuotaExtra.Item3;

                nRetorna = Amortizar_CuoExt(3, 0, pf_fecha_pago, 1, ref n_cap_tf, ref n_int_tf, ref n_mora_tf, ref n_sobrante);
                if (n_tot_int_mora > n_mora_tf)
                {
                    DApackage.InsertarAmortizaCuotasExtras(n_radic, pf_fecha_pago, n_num_ter, n_mora_tf, usuario);
                    n_tot_int_mora = n_tot_int_mora - n_mora_tf;
                }
                else
                {
                    DApackage.InsertarAmortizaCuotasExtras(n_radic, pf_fecha_pago, n_num_ter, n_tot_int_mora, usuario);
                    n_tot_int_mora = 0;
                };
                n_pagado = n_pagado - n_val_ter;
                if (n_pagado >= 0)
                {
                    DApackage.ActualizarSaldoDeUnaCuotaExtra(0, n_num_ter, usuario);
                }
                else
                {
                    DApackage.ActualizarSaldoDeUnaCuotaExtra(-n_pagado, n_num_ter, usuario);
                };
                if (s_num_ter != null)
                {
                    s_num_ter = s_num_ter + "," + n_num_ter;
                }
                else
                {
                    s_num_ter = Convert.ToString(n_num_ter);
                };
                if (n_pagado <= 0)
                {
                    break;
                };
            };

            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            //-- Determina la terminación de credito, actualiza el estado y inserta la novedad de terminación  
            //------------------------------------------------------------------------------------------------------------------------------------------------------  
            if (b_grabar && n_saldo <= 0)
            {
                //-- Actualiza estado del crédito
                DApackage.ActualizarEstadoCredito("T", n_radic, usuario);

                n_nov_cre = Consecutivos("4");
                DApackage.InsertarNovedadCredito(n_nov_cre, 302, pn_cod_usu, n_radic, DateTime.Now, DateTime.Now, "Terminación Crédito-Cuotas Extras", "1", usuario);
            };

            return b_grabar;

        }

        private bool Anular(Int64? pn_cod_ope, Int64? pn_numero_radicacion, Int64? pn_cod_ope_reversa, bool bReversion)
        {
            string lestadoope = null;
            int? tipo_ope = null;
            string g_sentencia = null;
            // pBasedato SYS_REFCURSOR;
            // pConexion SYS_REFCURSOR;
            // pConsulta SYS_REFCURSOR;
            Int64? lnum_tran = null;
            Int64? lnumero_radicacion = null;
            Int64? lcod_cliente = null;
            string lcod_linea_credito = null;
            int? ltipo_tran = null;
            int? lcod_atr = null;
            decimal? lvalor = null;
            string lestado = null;
            Int64? lnum_tran_anula = null;
            decimal? lsaldo_capital = null;
            decimal? lotros_saldos = null;
            int? lcuotas_pagadas = null;
            DateTime? lfecha_proximo_pago = null;
            DateTime? lfecha_ultimo_pago = null;
            string lestadoc = null;
            int? lcuotas_pendientes = null;
            long? ltipo_mov = null;
            bool bauditoria = false;
            int? tipo_tran = null;
            int? lcod_atr_cuota = null;
            DateTime? lfecha_cuota = null;
            decimal? lvalor_cuota = null;
            Int64? num_tran = null;
            Int64? lidtran = null;
            decimal? lsaldo_ac = null;
            decimal? ltotal_ac = null;
            Int64? lnumreg_ac = null;
            string lestado_ac = null;
            decimal? lvalor_aplica = null;
            Int64? lidcuenta = null;
            decimal? ltotal_cuenta = null;

            // -- Verificar que la operación existe
            lestadoope = "1";

            DApackage.ConsultarCaracteristicaOperacion(pn_cod_ope, ref lestadoope, ref tipo_ope, usuario);

            if (tipo_ope == null)
            {
                Mensaje_Error("La operación " + pn_cod_ope + " no existe");
                return false;
            };
            if (lestadoope == "2")
            {
                Mensaje_Error("La operacion " + pn_cod_ope + " ya fue anulada. Estado:" + lestadoope);
                return false;
            };

            // -- Determinando las transacciones del crédito a anular.
            List<TransaccionCredito> listaTransacciones = DApackage.ListarTransaccionesCredito(pn_cod_ope, usuario);

            foreach (var transaccion in listaTransacciones)
            {
                lnum_tran = transaccion.num_tran;
                lnumero_radicacion = transaccion.numero_radicacion;
                lcod_cliente = transaccion.cod_cliente;
                lcod_linea_credito = transaccion.cod_linea_credito;
                ltipo_tran = transaccion.tipo_tran;
                lcod_atr = transaccion.cod_atr;
                lvalor = transaccion.valor;
                lestado = transaccion.estado.HasValue ? Convert.ToString(transaccion.estado) : string.Empty;
                lnum_tran_anula = transaccion.num_tran_anula;
                ltipo_mov = transaccion.tipo_mov;

                // -- Anular si la transacción no esta anulada
                if (lestado == "0" || lestado == "1")
                {
                    // -- Determinar registro de auditoria del crédito para la operaciòn
                    DApackage.ConsultarRegistroAuditoriaCredito(pn_cod_ope, lnumero_radicacion, ref lsaldo_capital, ref lotros_saldos, ref lcuotas_pagadas, ref lfecha_proximo_pago, ref lfecha_ultimo_pago, ref lestadoc, ref lcuotas_pendientes, usuario);

                    if (lsaldo_capital == null)
                    {
                        bauditoria = false;
                        MensajeConsola("No existe registro de auditoria de aplicación de la operación");
                    }
                    else
                    {
                        bauditoria = true;
                    };

                    // -- Si es cruce de cuentas activar a la persona
                    if (tipo_ope == 4)
                    {
                        DApackage.ActualizarEstadoPersona("A", lcod_cliente, usuario);
                    };
                    // -- Determinar el tipo de transacción de anulación según el tipo de movimiento de la transacción a anular
                    if (ltipo_mov == 1)
                    {
                        tipo_tran = 10;
                    }
                    else
                    {
                        tipo_tran = 9;
                    };
                    // -- Según el atributo actualizar tabla
                    if (lcod_atr == 1)
                    {
                        if (bauditoria)
                        {
                            DApackage.ActualizarCredito(lsaldo_capital, lotros_saldos, lcuotas_pagadas, lcuotas_pendientes, lfecha_proximo_pago, lfecha_ultimo_pago, lestadoc, lnumero_radicacion, usuario);
                        }
                        else
                        {
                            if (ltipo_mov == 1)
                            {
                                DApackage.ActualizarSaldoCapitalCredito(lvalor, lnumero_radicacion, true, usuario);
                            }
                            else
                            {
                                DApackage.ActualizarSaldoCapitalCredito(lvalor, lnumero_radicacion, false, usuario);
                            };
                        };

                        // -- Se determinar si la transacciòn es del desembolso para cambiar el estado del crédito
                        if (ltipo_tran == 1)
                        {
                            DApackage.ActualizarEstadoCredito("B", lnumero_radicacion, usuario);
                        };
                    }
                    else
                    {
                        if (ltipo_mov == 1)
                        {
                            DApackage.ActualizarSaldoAtributoCredito(lvalor, lnumero_radicacion, true, usuario);
                        }
                        else
                        {
                            DApackage.ActualizarSaldoAtributoCredito(lvalor, lnumero_radicacion, true, usuario);
                        };

                        if (lcod_atr == n_atr_CTACOB)
                        {
                            lvalor_aplica = lvalor;

                            var listaCuentasPorCobrar = DApackage.ListarCuentaPorCobrarCredito(n_radic, usuario);

                            foreach (var cuentaPorCobrar in listaCuentasPorCobrar)
                            {
                                lidcuenta = cuentaPorCobrar.Item1;
                                ltotal_cuenta = cuentaPorCobrar.Item2;

                                if (lvalor_aplica > 0)
                                {
                                    if (ltotal_cuenta <= lvalor_aplica)
                                    {
                                        DApackage.ActualizarSaldoCuentasPorCobrar(ltotal_cuenta, lidcuenta, false, usuario);

                                        lvalor_aplica = lvalor_aplica - ltotal_cuenta;
                                    }
                                    else
                                    {
                                        DApackage.ActualizarSaldoCuentasPorCobrar(ltotal_cuenta, lidcuenta, false, usuario);

                                        lvalor_aplica = 0;
                                    };
                                };
                            };
                        };
                    };

                    // -- Actualizando lo registros de amortización del crédito
                    if (ltipo_tran == 5)
                    {
                        DApackage.BorrarTodaAmortizacionDeUnCredito(lnumero_radicacion, usuario);

                        var listaAmortizaCreAuditoria = DApackage.ListarAmortizaCreAuditoriaDeUnaOperacionYCredito(pn_cod_ope, lnumero_radicacion, usuario);

                        foreach (var amortiza in listaAmortizaCreAuditoria)
                        {
                            lcod_atr_cuota = amortiza.Item1;
                            lfecha_cuota = amortiza.Item2;
                            lvalor_cuota = amortiza.Item3;
                            lsaldo_ac = amortiza.Item4;
                            lestado_ac = amortiza.Item5.HasValue ? amortiza.Item5.ToString() : string.Empty;

                            DApackage.InsertarAmortizaCredito(null, lnumero_radicacion, lcod_atr_cuota, lfecha_cuota, lvalor_cuota, lsaldo_ac, 0, 0, 0, lestado_ac, usuario);
                        };
                    }
                    else
                    {
                        var listaDetalleTransaccion = DApackage.ListarDetalleTransaccionCreditoTotalizado(pn_cod_ope, lnumero_radicacion, lnum_tran, usuario);

                        foreach (var detalle in listaDetalleTransaccion)
                        {
                            lcod_atr_cuota = detalle.Item1;
                            lfecha_cuota = detalle.Item2;
                            lvalor_cuota = detalle.Item3;

                            DApackage.ConsultarAmortizaCreditoDeUnaCuotaYAtributo(lnumero_radicacion, lcod_atr_cuota, lfecha_cuota, ref lsaldo_ac, ref ltotal_ac, ref lnumreg_ac, usuario);

                            if ((ltotal_ac - lsaldo_ac) < lvalor_cuota)
                            {
                                DApackage.ActualizarAmortizaCre(lnumero_radicacion, lfecha_cuota, 1, ltotal_ac, usuario);
                            }
                            else
                            {
                                DApackage.ActualizarAmortizaCre(lnumero_radicacion, lfecha_cuota, lcod_atr_cuota, lvalor_cuota, usuario);
                            };

                            // -- if (lcod_atr_cuota = n_atr_mora ) {  
                            // -- Update det_mora_cre Set saldo = saldo + lvalor_cuota
                            // -- Where numero_radicacion = lnumero_radicacion And cod_atr = lcod_atr_cuota And fecha_cuota = lfecha_cuota;
                            //Delete From det_mora_cre
                            // Where numero_radicacion = lnumero_radicacion;
                            // -- };
                        };
                    };

                    // -- Según sea reversión o anulación entonces actualizar el estado de la transacción
                    if (bReversion)
                    {
                        DApackage.InsertarTransaccionAnulacion(null, pn_cod_ope, lnum_tran, lnumero_radicacion, lcod_cliente, lcod_linea_credito, ltipo_tran, lcod_atr, lvalor, usuario);
                        DApackage.BorrarDetalleTransaccionCreditoPorNumeroTransaccion(lnum_tran, usuario);
                        DApackage.BorrarTransaccionCredito(lnum_tran, usuario);
                    }
                    else
                    {
                        TRAN_CRED transaccionParaCrear = new TRAN_CRED
                        {
                            num_tran = num_tran.HasValue ? Convert.ToInt32(num_tran) : 0,
                            cod_ope = pn_cod_ope_reversa.HasValue ? Convert.ToInt32(pn_cod_ope_reversa) : 0,
                            numero_radicacion = lnumero_radicacion.HasValue ? Convert.ToInt32(lnumero_radicacion) : 0,
                            cod_cliente = lcod_cliente.HasValue ? Convert.ToInt32(lcod_cliente) : 0,
                            cod_linea_credito = lcod_linea_credito,
                            tipo_tran = tipo_tran.HasValue ? tipo_tran.Value : 0,
                            cod_det_lis = 0,
                            cod_atr = lcod_atr.HasValue ? lcod_atr.Value : 0,
                            valor = lvalor.HasValue ? Convert.ToDecimal(lvalor) : 0,
                            valor_mes = 0,
                            valor_causa = 0,
                            valor_orden = 0,
                            estado = 1,
                            num_tran_anula = 0
                        };

                        transaccionParaCrear = DApackage.InsertarTransaccionCredito(transaccionParaCrear, usuario);

                        DApackage.ActualizarEstadoTransaccionAnulada(numeroTransaccionParaActualizar: lnum_tran, numeroTransaccionAnulacion: num_tran, pusuario: usuario);
                    };
                };
            };

            return true;

        }

        private void Amortizar_PorValor(Int64? n_radic, decimal? n_valor_pago, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr, ref decimal? rn_sobra, ref int? n_error)
        {
            int n_num;
            decimal? n_interes = 0;
            int tipo_pago;

            rn_sobra = 0;
            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    tipo_pago = 1;
                    n_error = Amortizar(tipo_pago, n_valor_pago, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        n_interes = n_interes + rn_tot_atributos[n_num];
                        n_num = n_num + 1;
                    };
                    if (n_error == 0)
                    {
                        rn_sobra = n_valor_pago - (rn_tot_capital + n_interes);
                    }
                    else
                    {
                        rn_sobra = n_valor_pago;
                    };
                }
                else
                {
                    n_error = -1;
                };
            }
            else
            {
                n_error = -2;
            };
        }

        private void Amortizar_PorFecha(Int64? n_radic, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num;
            decimal? n_interes = 0;
            int n_tipo;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 2;
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Amortizar_PorCuotas(Int64? n_radic, int? n_cuotas_a_pag, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr, ref int? n_error)
        {
            int n_num;
            decimal? n_interes = 0;
            int tipo_pago;

            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    tipo_pago = 3;
                    n_error = Amortizar(tipo_pago, 0, f_fecha_pago, n_cuotas_a_pag, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        n_interes = n_interes + rn_tot_atributos[n_num];
                        n_num = n_num + 1;
                    };
                }
                else
                {
                    n_error = -1;
                };
            }
            else
            {
                n_error = -2;
            };
        }

        private void Amortizar_Total(Int64? n_radic, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num;
            decimal? n_interes = 0;
            int tipo_pago;

            rn_valor_cobrado = 0;
            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    tipo_pago = 4;
                    n_error = Amortizar(tipo_pago, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        if (rn_tot_atributos[n_num] != null)
                        {
                            n_interes = n_interes + rn_tot_atributos[n_num];
                        };
                        n_num = n_num + 1;
                    };
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    n_error = -1;
                };
            }
            else
            {
                n_error = -2;
            };
        }

        private void Amortizar_Recoger(Int64? n_radic, DateTime? f_fecha_pago, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = 0;
            int tipo_pago;
            DateTime? rf_f_prox_pago = null;
            Decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            decimal? rn_valor_cobrado = null;

            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    tipo_pago = 4;
                    n_error = Amortizar(tipo_pago, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        if (rn_tot_atributos[n_num] != null)
                        {
                            if (rn_cod_atributos[n_num] == n_atr_corr)
                            {
                                DApackage.ActualizarTemporalRecogerAtributoCorriente(rn_tot_atributos[n_num], n_radic, usuario);
                            }
                            else if (rn_cod_atributos[n_num] == n_atr_mora)
                            {
                                DApackage.ActualizarTemporalRecogerAtributoMora(rn_tot_atributos[n_num], n_radic, usuario);
                            }
                            else if (rn_cod_atributos[n_num] == n_atr_seguro)
                            {
                                DApackage.ActualizarTemporalRecogerAtributoSeguro(rn_tot_atributos[n_num], n_radic, usuario);
                            }
                            else if (rn_cod_atributos[n_num] == n_atr_LeyMiPyme)
                            {
                                DApackage.ActualizarTemporalRecogerAtributoLeyPime(rn_tot_atributos[n_num], n_radic, usuario);
                            }
                            else if (rn_cod_atributos[n_num] == n_atr_IVALeyMiPyme)
                            {
                                DApackage.ActualizarTemporalRecogerAtributoIvaLeyPime(rn_tot_atributos[n_num], n_radic, usuario);
                            }
                            else
                            {
                                DApackage.ActualizarTemporalRecogerAtributoOtros(rn_tot_atributos[n_num], n_radic, usuario);
                            };

                            n_interes = n_interes + rn_tot_atributos[n_num];
                        };
                        n_num = n_num + 1;
                    };
                }
                else
                {
                    n_error = -1;
                };
            }
            else
            {
                n_error = -2;
            };
        }

        private void Amortizar_Capital(Int64? n_radic, decimal? n_valor_pago, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal? rn_tot_intcte, ref decimal? rn_sobra, ref int? rn_error)
        {
            // ldatos SYS_REFCURSOR;
            string lcalculo_atr = null;
            int? ltipo_tasa = null;
            decimal? ltasa = null;
            int? ltipo_historico = null;
            decimal? ldesviacion = null;
            int? lefectiva_nomina = null;
            int? lcod_periodicidad = null;
            int? lmodalidad = null;
            int? lcod_periodicidad_cap = null;
            int? lcod_periodicidad_dia = null;
            decimal? ltasa_calculo = null;
            decimal? ltasa_total = null;
            decimal? ldias_trans = null;
            DateTime? lfec_ant_pag = null;

            rn_tot_intcte = 0;
            rn_error = 0;
            rn_sobra = 0;
            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    // -- Validar que este al día
                    if (f_prox_pag < f_fecha_pago)
                    {
                        rn_error = -9;
                        return;
                    };

                    // -- Calcular los días
                    lfec_ant_pag = BOFunciones.FecResDia(f_prox_pag, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                    if (n_tip_intant == 3 && n_cuo_pag == 0)
                    {
                        lfec_ant_pag = f_fec_apro;
                    };
                    ldias_trans = BOFunciones.NVL(BOFunciones.FecDifDia(lfec_ant_pag, f_fecha_pago, Convert.ToInt32(n_tipo_cal)), 0);

                    // -- Calcular el valor de los intereses corrientes
                    if (ldias_trans > 0)
                    {
                        DApackage.ConsultarAtributoCreditoCorrienteDeUnCredito(n_radic, ref lcalculo_atr, ref ltipo_tasa, ref ltasa, ref ltipo_historico, ref ldesviacion, usuario);

                        if (lcalculo_atr == null)
                        {
                            rn_tot_intcte = 0;
                        };

                        // -- Si se maneja tasa histórica
                        if (lcalculo_atr == "3" || lcalculo_atr == "5")
                        {
                            DApackage.DeterminarValorYTipoTasaHistorica(ltipo_historico, f_fec_apro, ref ltasa, ref ltipo_tasa, usuario);

                            ltasa = ltasa + BOFunciones.NVL(ldesviacion, 0);
                        }

                        // -- Determinar parametros del tipo de tasa
                        string efectivaNomina = string.Empty;
                        string modalidad = string.Empty;

                        DApackage.ConTipoTasa(ltipo_tasa, ref efectivaNomina, ref lcod_periodicidad, ref modalidad, ref lcod_periodicidad_cap, usuario);

                        if (!string.IsNullOrWhiteSpace(efectivaNomina))
                        {
                            lefectiva_nomina = Convert.ToInt32(efectivaNomina);
                        }

                        if (!string.IsNullOrWhiteSpace(modalidad))
                        {
                            lmodalidad = Convert.ToInt32(modalidad);
                        }

                        // -- Determinar la periodicidad diaria
                        lcod_periodicidad_dia = DApackage.ConPeriodicidadDiaria(n_tipo_cal, usuario);

                        if (n_tip_pago == 1)
                        {
                            ltasa_calculo = BOFunciones.CalTasMod(ltasa, lefectiva_nomina, lcod_periodicidad, lmodalidad, lcod_periodicidad_cap, 2, n_periodic, 1, n_periodic, 2);
                            ltasa_calculo = ((ltasa_calculo / 100) / n_dias_per_cre) * ldias_trans;
                            ltasa_total = ltasa_calculo;
                        }
                        else
                        {
                            ltasa_calculo = BOFunciones.CalTasMod(ltasa, lefectiva_nomina, lcod_periodicidad, lmodalidad, lcod_periodicidad_cap, 2, lcod_periodicidad_dia, 2, lcod_periodicidad_dia, 2);
                            ltasa_calculo = ltasa_calculo / 100;
                            ltasa_calculo = ltasa_calculo + 1;
                            ltasa_total = BOFunciones.Power(ltasa_calculo, (ldias_trans));
                            ltasa_total = (ltasa_total - 1);
                        };
                        rn_tot_intcte = BOFunciones.Redondeo(n_valor_pago * ltasa_total);
                    };
                    if (rn_tot_intcte < 0)
                    {
                        rn_tot_intcte = 0;
                    };

                    // -- Calcular el valor de capital
                    rf_f_prox_pago = f_prox_pag;
                    if (n_saldo >= (n_valor_pago - rn_tot_intcte))
                    {
                        rn_tot_capital = (n_valor_pago - rn_tot_intcte);
                    }
                    else
                    {
                        rn_tot_capital = n_saldo;
                    };

                    // -- Verificar el valor
                    if (rn_error == 0)
                    {
                        rn_sobra = n_valor_pago - rn_tot_capital;
                    }
                    else
                    {
                        rn_sobra = n_valor_pago;
                    };
                }
                else
                {
                    rn_error = -1;
                };
            }
            else
            {
                rn_error = -2;
            };
        }

        private void Amortizar_CuotasExtras(Int64? n_radic, int pn_tipo_pago, decimal? n_valor_pago, DateTime pf_fecha_pago, int? n_cuotas_a_pag, ref decimal? rn_tot_capital, ref decimal? rn_tot_interes, ref decimal? rn_tot_int_mora, ref int? rn_error)
        {
            decimal? n_interes = 0;
            decimal? n_sobrante = 0;

            rn_error = 0;
            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                rn_error = Amortizar_CuoExt(pn_tipo_pago, n_valor_pago, pf_fecha_pago, n_cuotas_a_pag, ref rn_tot_capital, ref rn_tot_interes, ref rn_tot_int_mora, ref n_sobrante);
            }
            else
            {
                rn_error = -1;
            };
        }

        private void Amortizar_TotalPrior(Int64? n_radic, decimal? n_valor_pago, DateTime? f_fecha_pago, ref DateTime? rf_f_prox_pago, ref decimal? rn_tot_capital, ref decimal?[] rn_cod_atributos, ref decimal?[] rn_tot_atributos, ref int rn_num_atr, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int tipo_pago = 0;
            decimal? n_capital_aux = null;
            decimal?[] n_atributos = new decimal?[99];
            // pBasedato SYS_REFCURSOR;
            int n_pos = 0;
            int n_cod_atr = 0;
            decimal? n_excedente = null;

            rn_valor_cobrado = 0;
            if (n_radic != null)
            {
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    n_excedente = n_valor_pago;
                    tipo_pago = 4;

                    // -- Determinar valores totales a pagar
                    n_error = Amortizar(tipo_pago, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref n_capital_aux, ref rn_cod_atributos, ref n_atributos, ref rn_num_atr);

                    // -- Distribuye según las prioridades para el pago
                    var listaPrioridadLineaCredito = DApackage.ListarPrioridadAtributoLineaCredito(s_cod_credi, usuario);

                    #region calcular valores
                    n_pos = 0;
                    foreach (var prioridad in listaPrioridadLineaCredito)
                    {
                        n_cod_atr = prioridad.Item1.HasValue ? prioridad.Item1.Value : 0;
                        n_num = prioridad.Item2.HasValue ? prioridad.Item2.Value : 0;

                        if (n_cod_atr == 1)
                        {
                            if (n_capital_aux > n_excedente)
                            {
                                rn_tot_capital = n_excedente;
                            }
                            else
                            {
                                rn_tot_capital = n_capital_aux;
                            };
                            n_excedente = n_excedente - rn_tot_capital;
                        }
                        else
                        {
                            n_num = 1;
                            while (n_num <= rn_num_atr && n_excedente > 0)
                            {
                                if (rn_cod_atributos[n_num] == n_cod_atr)
                                {
                                    if (n_atributos[n_num] > n_excedente)
                                    {
                                        rn_tot_atributos[n_num] = n_excedente;
                                    }
                                    else
                                    {
                                        rn_tot_atributos[n_num] = n_atributos[n_num];
                                    };
                                    n_excedente = n_excedente - rn_tot_atributos[n_num];
                                };
                                n_num = n_num + 1;
                            };
                        };
                        n_pos = n_pos + 1;
                        if (n_excedente <= 0)
                        {
                            break;
                        };
                    };
                    #endregion

                    // -- Calcular total de atributos y valor total cobrado
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        if (rn_tot_atributos[n_num] != null)
                        {
                            n_interes = n_interes + rn_tot_atributos[n_num];
                        };
                        n_num = n_num + 1;
                    };
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    n_error = -1;
                };
            }
            else
            {
                n_error = -2;
            };
        }

        private void Pagar_Credito(Int64? pn_radic, Int64 pn_cod_ope, int? pn_tipo_tran, DateTime pf_fecha_pago, Int64? pn_cod_det_lis, decimal? pn_valor_pago, Int64 pn_cod_usu, ref decimal? rn_sobrante, ref int? n_error)
        {
            DateTime? f_f_prox_pago = null;
            decimal? n_capital = null;
            decimal?[] n_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int n_num_atr = 0;
            decimal? n_interes = null;
            int n_num = 0;
            decimal? n_sobra = null;
            Int64? n_cod_det = null;
            int? tipo_pago = null;
            decimal? n_valor_cobrado = null;

            n_cod_det = pn_cod_det_lis;
            n_error = 0;
            Inicializar();
            if (pn_tipo_tran == 13)
            {
                tipo_pago = 4;
                Amortizar_TotalPrior(pn_radic, pn_valor_pago, pf_fecha_pago, ref f_f_prox_pago, ref n_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr, ref n_valor_cobrado, ref n_error);
            }
            else
            {
                if (Cargar_Credito(Convert.ToInt64(pn_radic)))
                {
                    #region
                    if (pn_tipo_tran == 3 || pn_tipo_tran == 4)
                    {
                        // -- Pago normal(Pagos por Caja, Recaudos Masivos)
                        tipo_pago = 1;
                    }
                    else if (pn_tipo_tran == 2 || pn_tipo_tran == 5)
                    {
                        // -- Pago total
                        tipo_pago = 4;
                    }
                    else if (pn_tipo_tran == 32)
                    {
                        // -- Pago hasta poner al día y luego a capital(Recaudos Masivos)
                        tipo_pago = 1;
                    }
                    else if (pn_tipo_tran == 33)
                    {
                        // -- Pago hasta poner al dìa y luego a capital(Caja)
                        tipo_pago = 6;
                    }
                    else if (pn_tipo_tran == 6)
                    {
                        // -- Abono a capital
                        tipo_pago = 8;
                    }
                    else
                    {
                        // -- Cualquier otro tipo de pago
                        tipo_pago = 1;
                    };
                    #endregion
                    // -- Validar el estado del crédito
                    #region
                    if (s_estado != "C")
                    {
                        n_error = -10;
                        int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                        ControlError(pn_radic, pn_cod_ope, "El crédito " + pn_radic + " no esta activo", codigoDetalleLista);
                        return;
                    };
                    // -- Realizar la amortización del crédito
                    if (pn_tipo_tran == 6)
                    {
                        n_num_atr = 0;
                        Amortizar_Capital(pn_radic, pn_valor_pago, BOFunciones.Trunc(pf_fecha_pago), ref f_f_prox_pago, ref n_capital, ref n_interes, ref n_sobra, ref n_error);
                        if (n_error < 0)
                        {
                            if (n_error == -9)
                            {
                                int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                                ControlError(pn_radic, pn_cod_ope, "Para el crédito " + pn_radic + " no se pueden hacer abonos a capital porque el crédito esta atrasado", codigoDetalleLista);
                            }
                            else
                            {
                                int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                                ControlError(pn_radic, pn_cod_ope, "Error " + n_error + " en el crédito " + pn_radic, codigoDetalleLista);
                            };
                        };
                        n_num_atr = 1;
                        n_cod_atributos[1] = BOFunciones.AtrCorriente();
                        n_tot_atributos[1] = n_interes;
                    }
                    else
                    {
                        n_error = Amortizar(Convert.ToInt32(tipo_pago), pn_valor_pago, BOFunciones.Trunc(pf_fecha_pago), 0, ref f_f_prox_pago, ref n_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr);
                    };
                    #endregion
                }
                else
                {
                    n_error = -1;
                    int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                    ControlError(pn_radic, pn_cod_ope, "Error al cargar datos del crèdito", codigoDetalleLista);
                    return;
                };
            };
            if (n_error == 0)
            {
                #region
                n_interes = 0;
                n_num = 1;
                while (n_num <= n_num_atr && n_num <= n_tot_atributos.Length)
                {
                    n_interes = n_interes + n_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_num_atr > n_tot_atributos.Length)
                {
                    n_num_atr = n_tot_atributos.Length;
                };
                n_sobra = pn_valor_pago - (n_capital + n_interes);
                if (n_sobra > 0)
                {
                    rn_sobrante = n_sobra;
                }
                else
                {
                    rn_sobrante = 0;
                };
                if (n_interes == 0 && n_capital == 0)
                {
                    int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                    ControlError(pn_radic, pn_cod_ope, "No se pudo determinar el valor a pagar del crédito", codigoDetalleLista);
                    n_error = -9;
                };
                if (!Pagar(pn_cod_ope, pf_fecha_pago, ref n_cod_det, pn_tipo_tran, ref f_f_prox_pago, ref n_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr, pn_cod_usu))
                {
                    int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                    ControlError(pn_radic, pn_cod_ope, "Error al pagar el crèdito", codigoDetalleLista);
                    n_error = -8;
                };
                #endregion
            }
            else
            {
                int? codigoDetalleLista = pn_cod_det_lis.HasValue ? Convert.ToInt32(pn_cod_det_lis) : default(int?);

                ControlError(pn_radic, pn_cod_ope, "Error al amortizar el crèdito " + pn_radic, codigoDetalleLista);
                n_sobra = pn_valor_pago;
            };
        }

        private void ControlError(Int64? pn_radic, Int64? pn_cod_ope, string pError, int? pTipo)
        {
            if (pTipo == null || pTipo == 0)
            {
                throw new InvalidOperationException(pError + " Radic: " + pn_radic);
            }
            else
            {
                DApackage.InsertarTemporalErrorRecaudo(pn_cod_ope, "1", pn_radic, pError, pTipo, usuario);
            };
        }

        private void Pagar_CuotasExtras(Int64? pn_radic, Int64? pn_cod_ope, int? pn_tipo_tran, DateTime pf_fecha_pago, Int64? pn_cod_det_lis, decimal? pn_valor_pago, Int64 pn_cod_usu, ref decimal? rn_sobrante, ref int? n_error)
        {
            DateTime? f_f_prox_pago = null;
            decimal? n_tot_capital = null;
            decimal? n_tot_interes = null;
            decimal? n_tot_int_mora = null;
            int tipo_pago = 0;
            Int64? n_cod_det = null;
            DateTime f_fecha_pago;

            n_cod_det = pn_cod_det_lis;
            n_error = 0;
            Inicializar();
            if (Cargar_Credito(Convert.ToInt64(pn_radic)))
            {
                n_error = Amortizar_CuoExt(1, pn_valor_pago, pf_fecha_pago, 0, ref n_tot_capital, ref n_tot_interes, ref n_tot_int_mora, ref rn_sobrante);
                if (n_error == 0 || (n_tot_capital == 0 && n_tot_interes == 0 && n_tot_int_mora == 0))
                {
                    f_fecha_pago = pf_fecha_pago;
                    if (!Pagar_CuoExt(pn_cod_ope, ref f_fecha_pago, ref n_cod_det, pn_tipo_tran, ref n_tot_capital, ref n_tot_interes, ref n_tot_int_mora, pn_cod_usu))
                    {
                        n_error = -8;
                        Mensaje_Error("Error al aplicar el pago de la cuota extra del crédito " + pn_radic);
                        return;
                    };
                }
                else
                {
                    rn_sobrante = pn_valor_pago;
                    Mensaje_Error("Error al Amortizar la cuota extra del crédito " + pn_radic);
                    return;
                };
            }
            else
            {
                rn_sobrante = pn_valor_pago;
                n_error = -1;
                Mensaje_Error("Error al cargar datos del crèdito");
                return;
            };
        }

        private void Calcular_PorValor(Int64? n_radic, DateTime? f_fecha_pago, decimal? rn_valor_pago, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            decimal? n_valor_cobrado = null;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 1;
                n_error = Amortizar(n_tipo, rn_valor_pago, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    n_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    n_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_PorCuotas(Int64? n_radic, DateTime? f_fecha_pago, int? n_num_cuotas, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 3;
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, n_num_cuotas, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_PorFecha(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 2;
                b_detalle = true;

                // -- Calculando el valor a pagar
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);

                // -- Determinando el valor total
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_Total(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 4;
                n_error = Amortizar(n_tipo, 0, BOFunciones.Trunc(f_fecha_pago), 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + BOFunciones.NVL(rn_tot_atributos[n_num], 0);
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_ACierre(Int64? n_radic, DateTime? f_fecha_cierre, ref decimal? rn_valor_apagar, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;

            rn_valor_apagar = 0;
            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 5;
                // -- Calculando el valor a pagar
                n_error = Amortizar(n_tipo, 0, f_fecha_cierre, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                // -- Determinando el valor total
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_apagar = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_apagar = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_CuoExt(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal? rn_tot_interes = null;
            decimal? rn_tot_int_mora = null;
            decimal? rn_sobrante = null;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                // -- Calculando el valor a pagar
                n_tipo = 2;
                n_error = Amortizar_CuoExt(n_tipo, 0, f_fecha_pago, 0, ref rn_tot_capital, ref rn_tot_interes, ref rn_tot_int_mora, ref rn_sobrante);
                if (n_error == 0)
                {
                    rn_valor_cobrado = BOFunciones.NVL(rn_tot_capital, 0) + BOFunciones.NVL(rn_tot_interes, 0) + BOFunciones.NVL(rn_tot_int_mora, 0);
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        // -- Mètodos para realizaciòn de calculos con valores detallado por cuota  
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void Calcular_PorFechaDet(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            int? n_cod_atr = null;
            int? n_num_pago = null;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_pago = null;
            decimal? n_valor_saldo = null;
            decimal? dias_mora = null;
            int n_cont = 0;
            decimal? n_saldo_act = null;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 2;
                b_detalle = true;

                // -- Calculando el valor a pagar
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);

                // -- Determinando el valor total
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };

                // -- Insertando valores detallados por cuota
                if (b_detalle)
                {
                    DApackage.BorrarTodaTemporalPagar(usuario);

                    n_saldo_act = n_saldo;
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                        n_num_pago = cl_detalle_pago[n_cont_rep].n_num_cuota;
                        f_fecha_tran = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                        n_valor_pago = cl_detalle_pago[n_cont_rep].n_valor;
                        if (n_cod_atr == 1)
                        {
                            n_saldo_act = n_saldo_act - cl_detalle_pago[n_cont_rep].n_valor;
                            n_valor_saldo = n_saldo_act;
                        }
                        else
                        {
                            n_valor_saldo = 0;
                        };
                        if (f_fecha_pago > f_fecha_tran)
                        {
                            dias_mora = BOFunciones.FecDifDia(f_fecha_tran, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                        }
                        else
                        {
                            dias_mora = 0;
                        };

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);
                        n_cont_rep = n_cont_rep + 1;
                    };
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_PorValorDet(Int64? n_radic, DateTime? f_fecha_pago, decimal? rn_valor_pago, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            int? n_cod_atr = null;
            int? n_num_pago = null;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_pago = null;
            decimal? n_valor_saldo = null;
            int? dias_mora = null;
            int n_cont = 0;
            decimal? n_total_pendiente = null;
            decimal? n_total_detalle = null;
            decimal? n_saldo_act = null;
            decimal? n_valor_cobrado = null;
            bool b_seguir = false;

            DApackage.BorrarTodaTemporalPagar(usuario);

            b_seguir = true;
            if (n_error == 2)
            {
                n_error = 0;
                n_tipo = 8;

                // -- Calculando el valor a pagar
                Amortizar_TotalPrior(n_radic, rn_valor_pago, f_fecha_pago, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr, ref n_valor_cobrado, ref n_error);

                // -- Determinando el valor a pagar por atributos
                DApackage.BorrarTodaTemporalPagar(usuario);

                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];

                    int? codAtributo = rn_cod_atributos[n_num].HasValue ? Convert.ToInt32(rn_cod_atributos[n_num].Value) : default(int?);
                    DApackage.InsertarTemporalPagar(n_radic, codAtributo, 0, rf_f_prox_pago, rn_tot_atributos[n_num], 0, 0, usuario);

                    n_num = n_num + 1;
                };

                DApackage.InsertarTemporalPagar(n_radic, 1, 0, rf_f_prox_pago, rn_tot_capital, 0, 0, usuario);
            }
            else
            {
                n_error = 0;
                if (n_error == 1)
                {
                    n_tipo = 1;
                }
                else
                {
                    n_tipo = 6;
                };

                // -- Calculando el valor a pagar
                if (Cargar_Credito(Convert.ToInt64(n_radic)))
                {
                    n_error = Amortizar(n_tipo, rn_valor_pago, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                }
                else
                {
                    b_seguir = false;
                };
                if (b_seguir)
                {
                    b_detalle = true;

                    // -- Determinando el valor a pagar por atributos
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                    {
                        n_interes = n_interes + rn_tot_atributos[n_num];
                        n_num = n_num + 1;
                    };

                    // -- Insertando valores detallados por cuota
                    if (b_detalle)
                    {
                        n_total_detalle = 0;
                        DApackage.BorrarTodaTemporalPagar(usuario);
                        n_saldo_act = n_saldo;
                        n_cont_rep = 1;
                        while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                        {
                            n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                            n_num_pago = cl_detalle_pago[n_cont_rep].n_num_cuota;
                            f_fecha_tran = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                            n_valor_pago = cl_detalle_pago[n_cont_rep].n_valor;
                            n_valor_saldo = cl_detalle_pago[n_cont_rep].n_saldo;
                            if (n_cod_atr == 1)
                            {
                                n_saldo_act = n_saldo_act - cl_detalle_pago[n_cont_rep].n_valor;
                                n_valor_saldo = n_saldo_act;
                            }
                            else
                            {
                                n_valor_saldo = 0;
                            };
                            if (f_fecha_pago > f_fecha_tran)
                            {
                                dias_mora = BOFunciones.FecDifDia(f_fecha_tran, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                            }
                            else
                            {
                                dias_mora = 0;
                            };

                            DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                            n_total_detalle = n_total_detalle + n_valor_pago;
                            n_cont_rep = n_cont_rep + 1;
                        };
                        n_total_pendiente = 0;
                        n_cont = 1;
                        while (n_cont < n_cont_amortiza)
                        {
                            n_total_pendiente = n_total_pendiente + BOFunciones.NVL(cl_amortiza_cre[n_cont].n_saldo, 0);
                            n_cont = n_cont + 1;
                        };
                    };
                }
                else
                {
                    n_error = -1;
                };
            };
        }

        private void Calcular_TotalDet(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            int? n_cod_atr = null;
            int? n_num_pago = null;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_pago = null;
            decimal? n_valor_saldo = null;
            int? dias_mora = null;
            int n_cont = 0;
            decimal? n_total_pendiente = null;
            decimal? n_total_detalle = null;

            DApackage.BorrarTodaTemporalPagar(usuario);
            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 4;
                b_detalle = true;
                n_error = Amortizar(n_tipo, 0, BOFunciones.Trunc(f_fecha_pago), 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);

                // -- Determinando los valores por atributo
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };

                // -- Insertando valores detallados por cuota
                if (b_detalle)
                {
                    n_total_detalle = 0;
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                        n_num_pago = cl_detalle_pago[n_cont_rep].n_num_cuota;
                        f_fecha_tran = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                        n_valor_pago = cl_detalle_pago[n_cont_rep].n_valor;
                        n_valor_saldo = cl_detalle_pago[n_cont_rep].n_saldo;
                        if (f_fecha_pago > f_fecha_tran)
                        {
                            dias_mora = BOFunciones.FecDifDia(f_fecha_tran, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                        }
                        else
                        {
                            dias_mora = 0;
                        };

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                        n_total_detalle = n_total_detalle + n_valor_pago;
                        n_cont_rep = n_cont_rep + 1;
                    };

                    // -- Si no inserto ningún dato.
                    if ((n_cont_rep == 1))
                    {
                        DApackage.InsertarTemporalPagar(n_radic, 1, 1, f_prox_pag, n_saldo, n_saldo, 0, usuario);

                        n_total_detalle = n_total_detalle + n_saldo;
                    };
                    n_total_pendiente = 0;
                    n_cont = 1;
                    while (n_cont < n_cont_amortiza)
                    {
                        n_total_pendiente = n_total_pendiente + BOFunciones.NVL(cl_amortiza_cre[n_cont].n_saldo, 0);
                        n_cont = n_cont + 1;
                    };
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_PorCuotasDet(Int64? n_radic, DateTime? f_fecha_pago, int? n_num_cuotas, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            int? n_cod_atr = null;
            int? n_num_pago = null;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_pago = null;
            decimal? n_valor_saldo = null;
            decimal? dias_mora = null;
            int n_cont = 0;
            decimal? n_total_pendiente = null;
            decimal? n_total_detalle = null;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 3;
                b_detalle = true;
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, n_num_cuotas, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);

                // -- Determinando los valores por atributo
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = rn_tot_capital + n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };

                // -- Insertando valores detallados por cuota
                if (b_detalle)
                {
                    n_total_detalle = 0;
                    DApackage.BorrarTodaTemporalPagar(usuario);
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                        n_num_pago = cl_detalle_pago[n_cont_rep].n_num_cuota;
                        f_fecha_tran = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                        n_valor_pago = cl_detalle_pago[n_cont_rep].n_valor;
                        n_valor_saldo = cl_detalle_pago[n_cont_rep].n_saldo;
                        if (f_fecha_pago > f_fecha_tran)
                        {
                            dias_mora = BOFunciones.FecDifDia(f_fecha_tran, f_fecha_pago, Convert.ToInt32(n_tipo_cal));
                        }
                        else
                        {
                            dias_mora = 0;
                        };

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                        n_total_detalle = n_total_detalle + n_valor_pago;
                        n_cont_rep = n_cont_rep + 1;
                    };

                    // -- Si no inserto ningún dato.
                    if (n_cont_rep == 1)
                    {
                        DApackage.InsertarTemporalPagar(n_radic, 1, 1, f_prox_pag, n_saldo, n_saldo, 0, usuario);

                        n_total_detalle = n_total_detalle + n_saldo;
                    };
                    n_total_pendiente = 0;
                    n_cont = 1;
                    while (n_cont < n_cont_amortiza)
                    {
                        n_total_pendiente = n_total_pendiente + BOFunciones.NVL(cl_amortiza_cre[n_cont].n_saldo, 0);
                        n_cont = n_cont + 1;
                    };
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_CuoExtDet(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal? rn_tot_interes = null;
            decimal? rn_tot_int_mora = null;
            decimal? rn_sobrante = null;
            int? n_cod_atr = null;
            int? n_num_pago = null;
            DateTime? f_fecha_tran = null;
            decimal? n_valor_pago = null;
            decimal? n_valor_saldo = null;
            decimal? dias_mora = null;
            int n_cont = 0;
            decimal? n_total_pendiente = null;
            decimal? n_total_detalle = null;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                b_detalle = true;

                // -- Calculando el valor a pagar
                n_tipo = 1;
                n_error = Amortizar_CuoExt(n_tipo, rn_valor_cobrado, f_fecha_pago, 0, ref rn_tot_capital, ref rn_tot_interes, ref rn_tot_int_mora, ref rn_sobrante);
                if (n_error == 0)
                {
                    rn_valor_cobrado = BOFunciones.NVL(rn_tot_capital, 0) + BOFunciones.NVL(rn_tot_interes, 0) + BOFunciones.NVL(rn_tot_int_mora, 0);
                }
                else
                {
                    rn_valor_cobrado = 0;
                };

                // -- Insertando valores detallados por cuota
                if (b_detalle)
                {
                    n_total_detalle = 0;
                    DApackage.BorrarTodaTemporalPagar(usuario);
                    if (rn_tot_capital != 0 && rn_tot_capital != null)
                    {
                        n_cod_atr = 1;
                        n_num_pago = 0;
                        f_fecha_tran = f_fecha_pago;
                        n_valor_pago = rn_tot_capital;
                        n_valor_saldo = n_saldo;
                        dias_mora = 0;

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                        n_total_detalle = n_total_detalle + n_valor_pago;
                        n_cont_rep = n_cont_rep + 1;
                    };
                    if (rn_tot_interes != 0 && rn_tot_interes != null)
                    {
                        n_cod_atr = n_atr_corr;
                        n_num_pago = 0;
                        f_fecha_tran = f_fecha_pago;
                        n_valor_pago = rn_tot_interes;
                        n_valor_saldo = n_saldo;
                        dias_mora = 0;

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                        n_total_detalle = n_total_detalle + n_valor_pago;
                        n_cont_rep = n_cont_rep + 1;
                    };
                    if (rn_tot_int_mora != 0 && rn_tot_int_mora != null)
                    {
                        n_cod_atr = n_atr_mora;
                        n_num_pago = 0;
                        f_fecha_tran = f_fecha_pago;
                        n_valor_pago = rn_tot_int_mora;
                        n_valor_saldo = n_saldo;
                        dias_mora = 0;

                        DApackage.InsertarTemporalPagar(n_radic, n_cod_atr, n_num_pago, f_fecha_tran, n_valor_pago, n_valor_saldo, dias_mora, usuario);

                        n_total_detalle = n_total_detalle + n_valor_pago;
                        n_cont_rep = n_cont_rep + 1;
                    };
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Calcular_AtributosPorFecha(Int64? n_radic, DateTime? f_fecha_pago, ref decimal? rn_valor_cobrado, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;

            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 2;
                b_detalle = true;

                // -- Calculando el valor a pagar
                n_error = Amortizar(n_tipo, 0, f_fecha_pago, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);

                // -- Determinando el valor total
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    rn_valor_cobrado = n_interes;
                }
                else
                {
                    rn_valor_cobrado = 0;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        // -- Mètodos para la realizaciòn del desembolso del crèdito  
        // ----------------------------------------------------------------------------------------------------------------------------------------------------------------  

        private void Desembolsar(DateTime? f_fecha_operacion, Int64? n_cod_usu, Int64? n_cod_ofi, string s_ip, ref Int64? n_cod_ope, ref int? n_error)
        {
            Int64? n_num_tran = null;
            Int64? consecutivo = null;
            int? n_cod_atr = null;
            decimal? n_valor = null;
            int n_pos = 0;
            // Pcursor Sys_Refcursor;
            decimal? lvalor_prima_total = null;

            n_error = 0;
            // -- Insertando datos de la operación
            try
            {
                OperacionData DAOperacion = new OperacionData();
                Operacion operacion = new Operacion
                {
                    cod_ope = n_cod_ope.HasValue ? n_cod_ope.Value : 0,
                    tipo_ope = 1,
                    cod_usu = n_cod_usu.HasValue ? n_cod_usu.Value : 0,
                    cod_ofi = n_cod_ofi.HasValue ? n_cod_ofi.Value : 0,
                    cod_caja = 0,
                    cod_cajero = 0,
                    num_recibo = 0,
                    fecha_real = DateTime.Now,
                    hora = DateTime.Now,
                    fecha_oper = f_fecha_operacion.HasValue ? f_fecha_operacion.Value : DateTime.MinValue,
                    fecha_calc = f_fecha_operacion.HasValue ? f_fecha_operacion.Value : DateTime.MinValue,
                    num_comp = -2,
                    tipo_comp = 0,
                    estado = 0,
                    num_lista = 0,
                    ip = s_ip
                };

                operacion = DAOperacion.GrabarOperacion(operacion, usuario);
                n_cod_ope = operacion.cod_ope;
            }
            catch
            {
                n_cod_ope = Consecutivos("5");
                n_error = 1;
                Mensaje_Error("No se pudo crear la operaciòn de desembolso del crèdito " + n_cod_ope);
                return;
            }

            // -- Insertando datos de la transacción
            try
            {
                decimal? totalCapital = n_monto + (n_adi_monto.HasValue ? n_adi_monto.Value : 0);

                TRAN_CRED transaccionParaCrear = new TRAN_CRED
                {
                    num_tran = n_num_tran.HasValue ? Convert.ToInt32(n_num_tran) : 0,
                    cod_ope = n_cod_ope.HasValue ? Convert.ToInt32(n_cod_ope) : 0,
                    numero_radicacion = n_radic.HasValue ? Convert.ToInt32(n_radic) : 0,
                    cod_cliente = n_cod_cliente.HasValue ? Convert.ToInt32(n_cod_cliente) : 0,
                    cod_linea_credito = s_cod_credi,
                    tipo_tran = 1,
                    cod_det_lis = 0,
                    cod_atr = 1,
                    valor = totalCapital.HasValue ? Convert.ToDecimal(totalCapital) : 0,
                    valor_mes = 0,
                    valor_causa = 0,
                    valor_orden = 0,
                    estado = 1,
                    num_tran_anula = 0
                };

                DApackage.InsertarTransaccionCredito(transaccionParaCrear, usuario);
            }
            catch (Exception ex)
            {
                n_num_tran = Consecutivos("2");
                n_error = 2;
                Mensaje_Error("No se pudo crear la transacciòn de desembolso del crèdito " + n_radic + ". Error: " + ex.Message);
                return;
            }

            // -- Insertando atributos descontados
            n_pos = 1;
            while (n_pos <= n_atr_otr)
            {
                #region -- atributos descontados
                if (atr_otro[n_pos].n_signo == 1)
                {
                    n_cod_atr = atr_otro[n_pos].n_cod_atr;
                    n_valor = BOFunciones.NVL(atr_otro[n_pos].n_valor_calculo, 0);
                    if (n_valor != 0)
                    {
                        while (true)
                        {
                            try
                            {
                                //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 18, Null, n_cod_atr, n_valor, 0, 0, 0, 1, Null)
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                                n_error = 3;
                                // raise_application_error(-20101, 'Atributos Descontados. No se pudo crear la transacciòn de desembolso del crèdito ' || n_radic || '. Error: ' || SQLERRM);
                                return;
                            };
                        };
                    };
                };
                #endregion
                #region -- Contabilización Cxp atributos
                if (atr_otro[n_pos].n_signo == 3 && atr_otro[n_pos].n_cod_atr == n_atr_CTAATR)
                {
                    n_cod_atr = atr_otro[n_pos].n_cod_atr;
                    n_valor = BOFunciones.NVL(atr_otro[n_pos].n_valor_calculo, 0);
                    if (n_valor != 0)
                    {
                        while (true)
                        {
                            try
                            {
                                //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 1, Null, n_cod_atr, n_valor, 0, 0, 0, 1, Null)
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                                n_error = 4;
                                //raise_application_error(-20101, "CxP. No se pudo crear la transacciòn de desembolso del crèdito." || n_radic || ". Error: " || SQLERRM);
                                return;
                            };
                        };
                    };
                };
                #endregion
                #region -- Contabilización El valor de garantias comunitarias
                if (atr_otro[n_pos].n_signo == 3 && (atr_otro[n_pos].n_cod_atr == n_atr_GARCOM || atr_otro[n_pos].n_cod_atr == n_atr_IVAGARCOM))
                {
                    n_cod_atr = atr_otro[n_pos].n_cod_atr;
                    n_valor = BOFunciones.NVL(atr_otro[n_pos].n_valor_calculo, 0);
                    if (n_valor != 0)
                    {
                        while (true)
                        {
                            try
                            {
                                //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 1, Null, n_cod_atr, n_valor, 0, 0, 0, 1, Null)
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                                n_error = 5;
                                // raise_application_error(-20101, "Garantias Comunitarias. No se pudo crear la transacciòn de desembolso del crèdito." || n_radic || ". Error: " || SQLERRM);
                                return;
                            };
                        };
                    };
                };
                #endregion
                #region-- Actualizar atributos de LeyMiPyme e IVA LeyMiPyme para que guarde los valores
                if (atr_otro[n_pos].n_cod_atr == n_atr_LeyMiPyme || atr_otro[n_pos].n_cod_atr == n_atr_IVALeyMiPyme)
                {
                    if (atr_otro[n_pos].n_tip_des == 4)
                    {
                        //Update descuentoscredito Set valor = Atr_Otro(n_pos).n_valor
                        //where numero_radicacion = n_radic and cod_atr = Atr_Otro(n_pos).n_cod_atr;
                    };
                };
                #endregion
                n_pos = n_pos + 1;
            };
            #region -- Insertando atributos descontados por anticipado
            if (n_num_atr_des >= 1)
            {
                for (int Lcntr = 1; Lcntr <= n_num_atr_des; Lcntr++)
                {
                    if ((n_val_atr_des[Lcntr] != 0 && n_val_atr_des[Lcntr] != null) && n_tip_atr_des[Lcntr] == 1)
                    {
                        while (true)
                        {
                            try
                            {
                                //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 28, Null, n_cod_atr_des(Lcntr), n_val_atr_des(Lcntr), 0, 0, 0, 1, Null)
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                                n_error = 2;
                                //raise_application_error(-20101, "Atributos Descontados por Anticipado. No se pudo crear la transacciòn de desembolso del crèdito." || n_radic || ". Error: " || SQLERRM);
                                return;
                            };
                        };
                    };
                };
            };
            #endregion
            #region-- Si el crèdito tiene poliza descontar el valor del desembolso
            lvalor_prima_total = 0;
            //Open Pcursor For Select Sum(Valor_Prima_Total) From PolizasSeguros Where Numero_Radicacion = N_Radic;
            //Fetch Pcursor Into Lvalor_Prima_Total;
            if (lvalor_prima_total != null && lvalor_prima_total != 0)
            {
                while (true)
                {
                    try
                    {
                        //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 18, Null, 100, lValor_Prima_Total, 0, 0, 0, 1, Null)
                        //returning num_tran Into n_num_tran;
                        break;
                    }
                    catch
                    {
                        n_num_tran = Consecutivos("2");
                        n_error = 2;
                        //raise_application_error(-20101, "Poliza Seguros. No se pudo crear la transacciòn de desembolso del crèdito " || n_radic || ". Error: " || SQLERRM);
                        return;
                    };
                };
            };
            #endregion
            #region-- Insertando la novedad del credito
            consecutivo = Consecutivos("4");
            while (true)
            {
                try
                {
                    //Insert Into Novedad_cre Values(Consecutivo, 301, 1, n_radic, F_Fecha_Operacion, SYSDATE, "DESEMBOLSO CREDITO", 1);
                    break;
                }
                catch
                {
                    consecutivo = Consecutivos("4");
                    n_error = 3;
                    //raise_application_error(-20101, "No se pudo crear la novedad de desembolso del crèdito " || n_radic);
                    return;
                };
            };
            #endregion
        }

        private void Desembolsar_Avance(Int64? p_idavance, decimal? p_valor_desembolsado, DateTime? f_fecha_operacion, Int64? n_cod_usu, Int64? n_cod_ofi, string s_ip, ref Int64? n_cod_ope, ref int? n_error)
        {
            Int64? n_num_tran = null;
            Int64? consecutivo = null;
            int n_cod_atr = 0;
            decimal? n_valor = null;
            int n_pos = 0;
            // Pcursor Sys_Refcursor;
            decimal? lvalor_prima_total = null;

            n_error = 0;
            // -- Insertando datos de la operación
            while (true)
            {
                try
                {
                    //Insert Into operacion(cod_ope, tipo_ope, cod_usu, cod_ofi, cod_caja, cod_cajero, num_recibo, fecha_real, hora, fecha_oper, fecha_calc, num_comp, tipo_comp, estado, num_lista, ip)                                                                          
                    //Values(n_cod_ope, 114, n_cod_usu, n_cod_ofi, null, null, null, SYSDATE, SYSDATE, F_Fecha_Operacion, F_Fecha_Operacion, 0, 0, 1, null, s_ip)                                                                          
                    //returning COD_OPE into n_cod_ope;
                    break;
                }
                catch
                {
                    n_cod_ope = Consecutivos("5");
                    n_error = 1;
                    // raise_application_error(-20101, "No se pudo crear la operaciòn de desembolso del crèdito " || n_cod_ope || ". Error: " || SQLERRM);
                    return;
                };
            };
            // -- Insertando datos de la transacción
            while (true)
            {
                try
                {
                    //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 1, Null, 1, p_valor_desembolsado + Nvl(n_adi_monto, 0), 0, 0, 0, 1, Null)
                    //Returning num_tran Into n_num_tran;
                    break;
                }
                catch
                {
                    n_num_tran = Consecutivos("2");
                    n_error = 2;
                    // raise_application_error(-20101, "No se pudo crear la transacciòn de desembolso del crèdito " || n_radic || ". Error: " || SQLERRM);
                    return;
                };
            };
            // -- Insertando la novedad del credito
            consecutivo = Consecutivos("4");
            while (true)
            {
                try
                {
                    //Insert Into Novedad_cre Values(Consecutivo, 301, 1, n_radic, F_Fecha_Operacion, SYSDATE, "DESEMBOLSO CREDITO", 1);
                    break;
                }
                catch
                {
                    consecutivo = Consecutivos("4");
                    n_error = 3;
                    //raise_application_error(-20101, "No se pudo crear la novedad de desembolso del crèdito " || n_radic);
                    return;
                };
            };
        }

        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  
        //-- Mètodos para el manejo de las cuotas extras  
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------  

        //-- Mètodo para adicionar una cuota extra al arreglo
        public void SetCuotaExtra(DateTime? pf_fecha, decimal? pn_valor, decimal? pn_capital, decimal? pn_saldo_cap, decimal? pn_interes, decimal? pn_saldo_int, int? pn_forma_pag, string ps_estado, int? pn_num_ter)
        {
            if (n_num_cuoext > cuotasextras.Length)
                Array.Resize(ref cuotasextras, 1);
            n_num_cuoext = n_num_cuoext + 1;
            cuotasextras[n_num_cuoext].f_fecha = pf_fecha;
            cuotasextras[n_num_cuoext].n_valor = pn_valor;
            cuotasextras[n_num_cuoext].n_capital = pn_capital;
            cuotasextras[n_num_cuoext].n_saldo_cap = pn_saldo_cap;
            cuotasextras[n_num_cuoext].n_interes = pn_interes;
            cuotasextras[n_num_cuoext].n_saldo_int = pn_saldo_int;
            cuotasextras[n_num_cuoext].n_forma_pag = pn_forma_pag;
            cuotasextras[n_num_cuoext].c_estado = ps_estado;
            cuotasextras[n_num_cuoext].n_num_ter = pn_num_ter;
        }

        //-- Mètodo para calcular los intereses de los terminos fijos
        public void CalcularInteresCuotasExtras(int? n_tip_tf, int? n_tip_inttf, decimal? n_tasa_dia, int? n_tipo_cal, DateTime? f_fec_ini)
        {
            int n_num;
            int? n_dias;
            decimal n_aux;
            decimal? n_tot_tf;
            decimal? n_redondeo;

            if (n_num_cuoext > 0)
            {
                //-- Recorriendo cada termino fijo
                n_num = 1;
                n_tot_tf = 0;
                while (n_num <= n_num_cuoext)
                {
                    n_tot_tf = n_tot_tf + cuotasextras[n_num].n_valor;
                    n_num = n_num + 1;
                }
                //-- Recorriendo cada termino fijo
                n_num = 1;
                while (n_num <= n_num_cuoext)
                {
                    //-- Tipo de terminos  1:No utiliza,   2:Valor presente,  3:Valor Futuro,  4: Solo Capital,       ????? 5:Sobre Saldos  -- Antes era 4 ????  
                    if (n_tip_tf == 2)
                    {
                        // -- Tipo interes termino fijo   1: Sin interes,  2:Compuesto,  3:Simple
                        if (n_tip_inttf == 1)
                        {
                            cuotasextras[n_num].n_interes = 0;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else if (n_tip_inttf == 2)
                        {
                            n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            cuotasextras[n_num].n_capital = BOFunciones.Power(1 + n_tasa_dia, n_dias) * cuotasextras[n_num].n_valor;
                            cuotasextras[n_num].n_interes = cuotasextras[n_num].n_capital - cuotasextras[n_num].n_valor;
                            n_redondeo = cuotasextras[n_num].n_interes;
                            n_redondeo = BOFunciones.Round(n_redondeo);
                            cuotasextras[n_num].n_interes = n_redondeo;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else if (n_tip_inttf == 3)
                        {
                            n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            cuotasextras[n_num].n_interes = cuotasextras[n_num].n_valor * n_tasa_dia * n_dias;
                            n_redondeo = cuotasextras[n_num].n_interes;
                            n_redondeo = BOFunciones.Round(n_redondeo);
                            cuotasextras[n_num].n_interes = n_redondeo;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else
                        {
                            //DBMS_OUTPUT.PUT_LINE("El tipo de interés de los terminos fijos se encuentra errado");                    
                            return;
                        }
                    }
                    else if (n_tip_tf == 3)
                    {
                        //-- Tipo interes termino fijo   1: Sin interes,  2:Compuesto,  3:Simple
                        if (n_tip_inttf == 1)
                        {
                            cuotasextras[n_num].n_interes = 0;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else if (n_tip_inttf == 2)
                        {
                            n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            n_aux = Convert.ToDecimal(BOFunciones.Power(1 + n_tasa_dia, n_dias));
                            if (n_aux <= 0)
                                cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                            else
                                cuotasextras[n_num].n_capital = 1 / n_aux * cuotasextras[n_num].n_valor;
                            n_redondeo = cuotasextras[n_num].n_capital;
                            n_redondeo = BOFunciones.Round(n_redondeo);
                            cuotasextras[n_num].n_capital = n_redondeo;
                            cuotasextras[n_num].n_interes = cuotasextras[n_num].n_valor - cuotasextras[n_num].n_capital;
                        }
                        else if (n_tip_inttf == 3)
                        {
                            n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            cuotasextras[n_num].n_interes = cuotasextras[n_num].n_valor * n_tasa_dia * n_dias;
                            n_redondeo = cuotasextras[n_num].n_interes;
                            n_redondeo = BOFunciones.Round(n_redondeo);
                            cuotasextras[n_num].n_interes = n_redondeo;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else
                        {
                            //DBMS_OUTPUT.PUT_LINE("El tipo de interés de los terminos fijos se encuentra errado");
                            return;
                        }
                    }
                    else if (n_tip_tf == 4)
                    {
                        //-- Tipo interes termino fijo   1: Sin interes,  2:Compuesto,  3:Simple
                        if (n_tip_inttf == 1 || n_tip_inttf == 2)
                        {
                            cuotasextras[n_num].n_interes = 0;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else
                        {
                            //DBMS_OUTPUT.PUT_LINE("El tipo de interés de los terminos fijos se encuentra errado");
                            return;
                        }
                    }
                    else if (n_tip_tf == 5)
                    {
                        //-- Tipo interes termino fijo   1: Sin interes,  2:Compuesto,  3:Simple
                        if (n_tip_inttf == 1)
                        {
                            cuotasextras[n_num].n_interes = 0;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else if (n_tip_inttf == 2)
                        {
                            if (n_num == 0)
                                n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            else
                                n_dias = BOFunciones.FecDifDia(cuotasextras[n_num - 1].f_fecha, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            cuotasextras[n_num].n_interes = n_tot_tf * n_tasa_dia * n_dias;
                            n_redondeo = Convert.ToDecimal(cuotasextras[n_num].n_interes);
                            n_redondeo = BOFunciones.Round(n_redondeo);
                            cuotasextras[n_num].n_interes = n_redondeo;
                            n_tot_tf = n_tot_tf - cuotasextras[n_num].n_valor;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else if (n_tip_inttf == 3)
                        {
                            n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                            cuotasextras[n_num].n_interes = cuotasextras[n_num].n_valor * n_tasa_dia * n_dias;
                            n_redondeo = cuotasextras[n_num].n_interes;
                            n_redondeo = Convert.ToDecimal(BOFunciones.Round(n_redondeo));
                            cuotasextras[n_num].n_interes = n_redondeo;
                            cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                        }
                        else
                        {
                            //DBMS_OUTPUT.PUT_LINE("El tipo de interés de los terminos fijos se encuentra errado");
                            return;
                        }
                    }
                    else
                    {
                        // DBMS_OUTPUT.PUT_LINE("El tipo de interés de los terminos fijos se encuentra errado");
                        return;
                    }
                    cuotasextras[n_num].n_saldo_cap = cuotasextras[n_num].n_capital;
                    cuotasextras[n_num].n_saldo_int = cuotasextras[n_num].n_interes;
                    n_num = n_num + 1;
                }
            }
        }

        public bool ExistenCuotasExtras()
        {
            if (n_num_cuoext > 0)
                return true;
            else
                return false;

        }

        public decimal? SumarCapitalCuotasExtras()
        {
            int n_num;
            decimal? n_valor;

            n_num = 1;
            n_valor = 0;
            while (n_num <= n_num_cuoext)
            {
                n_valor = n_valor + cuotasextras[n_num].n_capital;
                n_num = n_num + 1;
            }
            return n_valor;
        }

        public decimal? SumarSaldoCapitalCuotasExtras(bool b_actual, DateTime f_fecha)
        {
            int n_num;
            decimal? n_valor;

            n_num = 1;
            n_valor = 0;
            while (n_num <= n_num_cuoext)
            {
                if (b_actual || cuotasextras[n_num].f_fecha >= f_fecha)
                    n_valor = n_valor + BOFunciones.NVL(cuotasextras[n_num].n_saldo_cap, 0);
                n_num = n_num + 1;
            }
            return n_valor;
        }

        public bool GetCuotaExtra(ref int? rn_pos, ref decimal? rn_capital, ref decimal? rn_interes, ref DateTime? rf_fecha, ref Int64? rn_num_ter)
        {
            if (rn_pos <= n_num_cuoext && rn_pos >= 1)
            {
                rn_capital = cuotasextras[Convert.ToInt32(rn_pos)].n_saldo_cap;
                rn_interes = cuotasextras[Convert.ToInt32(rn_pos)].n_saldo_int;
                rf_fecha = cuotasextras[Convert.ToInt32(rn_pos)].f_fecha;
                rn_num_ter = cuotasextras[Convert.ToInt32(rn_pos)].n_num_ter;
                return true;
            }
            else
            {
                rn_pos = 0;
                return false;
            }
        }

        public decimal SumarInteresCuotasExtras(decimal? n_tasa_per, int? n_tipo_cal, DateTime? f_fec_ini, int? pn_dias_per)
        {
            int n_num;
            decimal? n_dias;
            decimal? n_aux;
            decimal? n_total;

            n_num = 1;
            n_total = 0;
            while (n_num <= n_num_cuoext)
            {
                n_dias = BOFunciones.FecDifDia(f_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(n_tipo_cal));
                if (pn_dias_per != 0 && pn_dias_per != null)
                    n_dias = BOFunciones.Round(n_dias / pn_dias_per);
                n_aux = (BOFunciones.Power(1 + n_tasa_per, n_dias) - 1) / BOFunciones.Power(1 + n_tasa_per, n_dias);
                n_total = n_total + n_aux * cuotasextras[n_num].n_valor;
                n_num = n_num + 1;
            }
            return Convert.ToDecimal(n_total);
        }

        public void CalcularProrrateoCuotasExtras(decimal? pn_tasa_dia, int? pn_tipo_cal, DateTime? pf_fec_ini, decimal? pn_dias_aju, ref decimal? pn_total)
        {
            int n_num;
            decimal? n_dias;
            decimal? n_aux = 0;
            decimal? n_val_pre;

            n_num = 1;
            pn_total = 0;
            while (n_num <= n_num_cuoext)
            {
                cuotasextras[n_num].n_capital = cuotasextras[n_num].n_valor;
                n_dias = BOFunciones.FecDifDia(pf_fec_ini, cuotasextras[n_num].f_fecha, Convert.ToInt32(pn_tipo_cal)) + pn_dias_aju;
                n_aux = BOFunciones.Power(1 + pn_tasa_dia, n_dias);
                if (n_aux <= 0)
                    n_val_pre = cuotasextras[n_num].n_valor;
                else
                    n_val_pre = 1 / n_aux * cuotasextras[n_num].n_valor;
                pn_total = pn_total + n_val_pre;
                n_num = n_num + 1;
            }
        }


        //--------------------------------------------------------------------------------------------------------------------  
        //-- Procedimientos para el cierre històrico  
        //--------------------------------------------------------------------------------------------------------------------  
        private void CierreHistorico(Int64 pnumero_radicacion, DateTime pfecha, int ntipo, bool bcerraratributos, string bestado)
        {
            // -- Variables para datos del crèdito
            string s_estado = null;
            DateTime? d_fec_ven_des = null;
            int? n_cuo_pag = null;
            decimal? n_sal_des = null;
            decimal? n_cuo_des = null;
            decimal? n_per_des = null;
            string n_for_pago = null;
            string s_cod_credi = null;
            Int64? n_cod_cliente = null;
            decimal? n_monto = null;
            DateTime? f_fecha_apro = null;
            decimal? n_plazo = null;
            DateTime? f_ult_pago = null;
            decimal? n_valor_reestruc = null;
            DateTime? f_reestruc = null;
            int? n_marca_reestruc = null;
            // -- Variables para las consultas
            // pCursor SYS_REFCURSOR;
            // pConectar SYS_REFCURSOR;  
            // -- Variables para movimientos del crèdito
            decimal? n_debito = null;
            decimal? n_credito = null;
            decimal? n_nue_saldo = null;
            DateTime? d_fec_ven = null;
            int? n_atr_mor = null;
            int? n_cod_clasifica = null;
            string ncodcategoria = null;
            int? ndiasmora = null;
            Int64? n_cierre = null;
            int? n_cod_atr = null;
            decimal? saldo_atr = null;
            int? n_cod_oficina = null;
            Int64? n_cod_asesor = null;
            DateTime? f_inicial = null;
            decimal? n_movimientos = null;
            bool b_cerrar = false;
            int? nerror = null;
            DateTime? f_fec_des = null;
            DateTime? fecha_cta = null;
            decimal? dias_mora_sinreclamacion = null;
            Int64? cod_ope = null;
            DateTime? fecha_proximo_pago = null;
            decimal? tasa_interes = null;
            decimal? total = null;
            decimal? saldo = null;
            DateTime? d_fec_ini = null;
            int? n_mes_par_hist = null;
            int? ltipo_garantia = 2;

            n_atr_mor = BOFunciones.AtrMora(); ;
            // -- Determinar datos del crèdito
            //Open pConectar For
            //  Select D.Estado, D.Fecha_Proximo_Pago, D.Cuotas_Pagadas, D.Saldo_Capital, D.Valor_Cuota, D.Cod_Periodicidad, D.Forma_Pago,   
            //  d.cod_linea_credito, d.cod_deudor, d.monto_aprobado, d.fecha_aprobacion, d.numero_cuotas, d.fecha_ultimo_pago, d.cod_oficina, d.cod_asesor_com,  
            //  d.fecha_vencimiento, d.fecha_desembolso, d.VRREESTRUCTURADO, d.FECREESTRUCTURADO, d.REESTRUCTURADO
            //  From credito d Where d.numero_radicacion = pnumero_radicacion;
            //Fetch Pconectar Into S_Estado, D_Fec_Ven_Des, N_Cuo_Pag, N_Sal_Des, N_Cuo_Des, N_Per_Des, N_For_Pago, S_Cod_Credi, N_Cod_Cliente, N_Monto,
            //  f_fecha_apro, n_plazo, f_ult_pago, n_cod_oficina, n_cod_asesor, f_fec_term, f_fec_des, n_valor_reestruc, f_reestruc, n_marca_reestruc;
            if (true)
            { // pConectar%FOUND) {
                #region cerrar crédito
                //-- Determina el valor de las transacciones realizadas
                #region
                n_debito = 0;
                n_credito = 0;
                if (ntipo == 1)
                {
                    //Select sum(a.valor) Into n_debito  From temp_trancred a
                    //    Where a.numero_radicacion = pnumero_radicacion And a.tipo_mov = "1";   
                    //Select sum(a.valor) Into n_credito From temp_trancred a
                    //Where a.numero_radicacion = pnumero_radicacion And a.tipo_mov = "2";  
                }
                else
                {
                    //Select sum(a.valor) Into n_debito From tipo_tran t, tran_cred a, operacion o
                    //    Where a.cod_ope = o.cod_ope And a.tipo_tran = t.tipo_tran And a.numero_radicacion = pnumero_radicacion And a.cod_atr = 1 And t.tipo_mov = '1'   
                    //    And trunc(o.fecha_oper) > pfecha;   
                    //Select sum(a.valor) Into n_credito From tipo_tran t, tran_cred a, operacion o
                    //    Where a.cod_ope = o.cod_ope And a.tipo_tran = t.tipo_tran And a.numero_radicacion = pnumero_radicacion And a.cod_atr = 1 And t.tipo_mov = '2'   
                    //    And trunc(o.fecha_oper) > pfecha;  
                };
                #endregion
                // -- Calcula los valores del cierre
                n_nue_saldo = n_sal_des + BOFunciones.NVL(n_credito, 0) - BOFunciones.NVL(n_debito, 0);
                // -- Se agrega la siguiente verificacion de que si el saldo de desembolso es igual al saldo calculado deje como fecha de vencimiento la de desembolso
                d_fec_ven = d_fec_ven_des;
                if (d_fec_ven == null || n_nue_saldo == n_sal_des)
                {
                    d_fec_ven = d_fec_ven_des;
                };
                // -- No se encontraron pendientes, la fecha de proximo pago igual a la de desembolso.Pero los saldos son diferentes, por lo cual no pueden ser iguales las fechas
                #region
                if (d_fec_ven == d_fec_ven_des && n_nue_saldo != n_sal_des)
                {
                    d_fec_ven = null;
                    //Select Min(d.fecha_cuota) Into d_fec_ven From tipo_tran t, tran_cred a, operacion o, det_tran_cred d
                    //    Where a.cod_ope = o.cod_ope And a.tipo_tran = t.tipo_tran And a.numero_radicacion = pnumero_radicacion
                    //    And a.numero_radicacion = d.numero_radicacion And d.num_tran = a.num_tran
                    //    And trunc(o.fecha_oper) > pfecha And t.tipo_tran Not In(5, 6, 7, 8, 11, 12, 14, 25)
                    //    And a.cod_atr  != n_atr_mor And o.estado = 1;
                    if (d_fec_ven == null)
                    {
                        d_fec_ven = d_fec_ven_des;
                    };
                };
                if (d_fec_ven == null)
                {
                    if (d_fec_ven_des == null)
                    {
                        d_fec_ven = pfecha;
                    }
                    else
                    {
                        d_fec_ven = d_fec_ven_des;
                    };
                };
                #endregion
                // -- Determinando la clasificación del credito y los dìas de mora
                n_cod_clasifica = null;
                //Open pCursor For Select cod_clasifica From lineascredito Where cod_linea_credito = s_cod_credi;  
                //Fetch pCursor Into n_cod_clasifica;
                if (n_cod_clasifica == null)
                {
                    //Close Pcursor;
                    return;
                };
                //Close pCursor;
                if (d_fec_ven < pfecha)
                {
                    ndiasmora = BOFunciones.FecDifDia(d_fec_ven, pfecha, 1);
                }
                else
                {
                    ndiasmora = 0;
                };
                ncodcategoria = "";
                #region determinar categoria
                if (n_cod_clasifica != null)
                {
                    #region determinar clasificacion
                    //Open Pcursor For
                    //    Select Cod_Categoria From Dias_Categorias Where Cod_Clasifica = N_Cod_Clasifica And Ndiasmora Between Dias_Minimo And Dias_Maximo;  
                    //Fetch pCursor Into nCodCategoria;
                    if (ncodcategoria == null)
                    {
                        ncodcategoria = null;
                    };
                    //Close pCursor;

                    // -- Determinamos si el credito es reestructurado y la fecha de cierre inicial si es asi
                    #region reestructurados
                    if (BOFunciones.VerifLlineaEsReestructuracion(s_cod_credi) == 1 || n_marca_reestruc == 1)
                    {
                        //Open Pcursor For SELECT MAX(FECHA) FROM CIEREA WHERE TIPO = 'R' AND ESTADO = 'D';
                        //Fetch pCursor Into D_FEC_INI;
                        //Close pCursor;

                        // -- Determinamos que haya sido reestructurado dentro del periodo de cierre, si fue se baja una categoria
                        // -- Si no se evaluan otras condiciones
                        if (f_reestruc >= d_fec_ini && f_reestruc <= pfecha)
                        {
                            //--Determinar la última categoria del crédito
                            //Open Pcursor For
                            //    SELECT cod_categoria FROM HISTORICO_CRE WHERE FECHA_HISTORICO = d_fec_ini and numero_radicacion = pnumero_radicacion;
                            //Fetch pCursor Into nCodCategoria;
                            //Close pCursor;
                            //--Se sube una categoria
                            #region 
                            if (ncodcategoria == "A")
                            {
                                ncodcategoria = "B";
                            }
                            else if (ncodcategoria == "B")
                            {
                                ncodcategoria = "C";
                            }
                            else if (ncodcategoria == "C")
                            {
                                ncodcategoria = "D";
                            }
                            else if (ncodcategoria == "D")
                            {
                                ncodcategoria = "E";
                            }
                            else if (ncodcategoria == "E")
                            {
                                ncodcategoria = "E1";
                            }
                            else if (ncodcategoria == "E1")
                            {
                                ncodcategoria = "E2";
                            };
                            #endregion
                        }
                        else
                        {
                            // -- Si el credito tiene mora de mas de 30 dias se devuelve a su categoria inicial
                            #region 
                            if (ndiasmora > 30)
                            {
                                //Open Pcursor For
                                //SELECT cod_categoria FROM HISTORICO_CRE WHERE FECHA_HISTORICO = LAST_DAY(f_reestruc) and numero_radicacion = pnumero_radicacion;
                                //Fetch pCursor Into nCodCategoria;
                                //Close pCursor;
                            }
                            else
                            {
                                //--Determinar la última categoria del crédito
                                //Open Pcursor For
                                //    SELECT cod_categoria FROM HISTORICO_CRE WHERE FECHA_HISTORICO = d_fec_ini and numero_radicacion = pnumero_radicacion;
                                //Fetch pCursor Into nCodCategoria;
                                //Close pCursor;
                                // -- Si el credito esta al dia(menos de 30 dias de mora), y es el segundo mes asi de buena paga se sube una categoria
                                //Open Pcursor For
                                //SELECT MOD(COUNT(*), 2) FROM Historico_cre where numero_radicacion = pnumero_radicacion;
                                //Fetch pCursor Into n_mes_par_hist;
                                //Close pCursor;
                                #region
                                if (n_mes_par_hist == 1)
                                {
                                    if (ncodcategoria == "E2")
                                    {
                                        ncodcategoria = "E1";
                                    }
                                    else if (ncodcategoria == "E1")
                                    {
                                        ncodcategoria = "E";
                                    }
                                    else if (ncodcategoria == "E")
                                    {
                                        ncodcategoria = "D";
                                    }
                                    else if (ncodcategoria == "D")
                                    {
                                        ncodcategoria = "C";
                                    }
                                    else if (ncodcategoria == "C")
                                    {
                                        ncodcategoria = "B";
                                    }
                                    else if (ncodcategoria == "B")
                                    {
                                        ncodcategoria = "A";
                                    };
                                    #endregion
                                };
                                #endregion
                            };
                        };
                        #endregion reestructurados

                        #endregion determinar clasificacion
                    };
                    #endregion
                    // -- Determinar los dìas de mora sin reclamaciòn de garantias
                    #region garantias
                    dias_mora_sinreclamacion = ndiasmora;
                    fecha_cta = null;
                    //Open pCursor For Select Min(fecha_cta) From cuenta_porcobrar_cre Where numero_radicacion = pnumero_radicacion And saldo > 0;
                    //Fetch pCursor Into fecha_cta;
                    if (fecha_cta != null)
                    { // pCursor%FOUND ) { 
                      //Open pCursor For Select g.cod_ope, c.fecha_proximo_pago, g.total, g.saldo From cuenta_porcobrar_cre g Left Join credito_aud c On g.cod_ope = c.cod_ope And g.numero_radicacion = c.numero_radicacion
                      //    Where g.numero_radicacion = pnumero_radicacion And g.fecha_cta = fecha_cta;
                      //Fetch pCursor Into cod_ope, fecha_proximo_pago, total, saldo;
                        if (cod_ope != null)
                        { // pCursor%FOUND ) { 
                            dias_mora_sinreclamacion = dias_mora_sinreclamacion + BOFunciones.Round(Convert.ToDecimal(BOFunciones.FecDifDia(fecha_proximo_pago, pfecha, 1)) * saldo / total);
                        };
                    };
                    #endregion
                    // -- Si el crédito esta terminado mirar si tuvo movimientos en el período
                    #region movimientos            
                    b_cerrar = true;
                    if (n_nue_saldo == 0)
                    {
                        //Select Max(Fecha) Into f_inicial From Cierea Where Tipo = 'R' And Estado = 'D'And Fecha<Pfecha;          
                        if (f_inicial != null)
                        {
                            f_inicial = Convert.ToDateTime(BOFunciones.Trunc(f_inicial)).AddDays(1);
                            n_movimientos = 0;
                            //Select Sum(A.Valor) Into n_movimientos From Tran_Cred A Inner Join Operacion O On a.cod_ope = o.cod_ope
                            //Where a.numero_radicacion = pnumero_radicacion And trunc(o.fecha_oper) >= F_Inicial;  
                            if (n_movimientos == 0 || n_movimientos == null)
                            {
                                b_cerrar = false;
                            };
                        }
                        else
                        {
                            b_cerrar = false;
                        };
                    };
                    #endregion
                    // -- Inserta el registro en el cierre si es necesario
                    #region insertar cierre
                    if (b_cerrar)
                    {
                        // -- Validar la forma de pago
                        if (n_for_pago == "C")
                        {
                            n_for_pago = "1";
                        }
                        else if (n_for_pago == "N")
                        {
                            n_for_pago = "2";
                        }
                        else if (n_for_pago == null)
                        {
                            n_for_pago = "1";
                        };
                        // -- Calcular el número de cuotas pagadas y fecha de último pago
                        //Select Max(o.fecha_oper), Max(t.num_cuota) Into f_ult_pago, n_cuo_pag From operacion o Inner Join det_tran_cred t On o.cod_ope = t.cod_ope
                        //    Where t.numero_radicacion = Pnumero_Radicacion And trunc(o.fecha_oper) <= trunc(Pfecha);
                        if (f_ult_pago == null)
                        {
                            f_ult_pago = pfecha;
                        };
                        if (n_cuo_pag == null)
                        {
                            n_cuo_pag = 0;
                        };
                        // -- tasa_interes = TasaCredito(Pnumero_Radicacion);
                        tasa_interes = BOFunciones.Round(n_tasa_intcte, 4);
                        n_cierre = Consecutivos("6");
                        //Insert Into Historico_Cre Values(N_Cierre, Pfecha, Pnumero_Radicacion, N_Cod_Cliente, S_Cod_Credi, N_Monto, F_Fecha_Apro, N_Plazo, N_Per_Des, N_Cuo_Des,
                        //    n_for_pago, d_fec_ven, n_nue_saldo, 0, n_cod_asesor, f_ult_pago, nCodCategoria, s_estado, n_cod_clasifica, n_cuo_pag, nDiasMora, n_cod_oficina,
                        //    Null, ltipo_garantia, f_fec_term, f_fec_des, dias_mora_sinreclamacion, tasa_interes, n_valor_reestruc, f_reestruc, n_marca_reestruc);  
                        // -- Guardando històrico para proyección de pagos  
                        // -- If bestado = "D" And n_nue_saldo != 0) {
                        // --   Proyeccion(Pnumero_Radicacion, Pfecha);  
                        // -- };  
                        // -- Inserta los saldos por atributos
                        if (bcerraratributos)
                        {
                            //Open pCursor For Select ac.cod_atr, ac.saldo_atributo From atributos atr, atributoscredito ac Where ac.cod_atr = atr.cod_atr And ac.numero_radicacion = pnumero_radicacion And atr.causa = '1';  
                            while (true)
                            {
                                //Fetch pCursor Into n_cod_atr, saldo_atr;
                                //Exit When pCursor % NOTFOUND;
                                n_debito = 0;
                                //Select sum(a.valor)Into n_debito From tipo_tran t, tran_cred a, operacion o        
                                //    Where a.cod_ope = o.cod_ope And a.tipo_tran = t.tipo_tran And a.numero_radicacion = pnumero_radicacion And a.cod_atr = n_cod_atr And t.tipo_mov = '1'        
                                //    And trunc(o.fecha_oper) > pfecha;
                                n_credito = 0;
                                //Select sum(a.valor)Into n_credito From tipo_tran t, tran_cred a, operacion o        
                                //    Where a.cod_ope = o.cod_ope And a.tipo_tran = t.tipo_tran And a.numero_radicacion = pnumero_radicacion And a.cod_atr = n_cod_atr And t.tipo_mov = '2'        
                                //    And trunc(o.fecha_oper) > pfecha;
                                saldo_atr = saldo_atr + BOFunciones.NVL(n_credito, 0) - BOFunciones.NVL(n_debito, 0);
                                n_cierre = Consecutivos("6");
                                //Insert Into historico_cre_atr Values(n_cierre, pfecha, pnumero_radicacion, n_cod_atr, saldo_atr, 0, 0);
                            };
                            //Close pCursor;
                        };
                    };
                    #endregion
                    #endregion cerrar crédito
                }
                else
                {
                    // raise_application_error(-20101, "No se encontro crèdito con los datos dados, radicaciòn: " || pnumero_radicacion);
                    return;
                };
                //if (pCursor % ISOPEN) {
                //    Close pCursor;
                //};
                //if (pConectar % ISOPEN) {
                //    Close Pconectar;
                //};
            };
        }

        //--------------------------------------------------------------------------------------------------------------------  
        //-- Procedimientos para la causación  
        //--------------------------------------------------------------------------------------------------------------------  
        private void Causacion(Int64 pnumero_radicacion, DateTime pfecha_corte)
        {
            int? nerror = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            int n_num = 0;
            int n_causa = 0;
            int?[] n_atr_causa = new int?[99];
            decimal? n_dias_per = null;
            DateTime? f_fecha_inicio = null;
            decimal? n_dias_per_aux = null;
            decimal? n_dias_total = null;
            decimal? n_dias_acu_mor_pf = null;
            decimal? n_dias_causa = null;
            decimal? n_dias_cau = null;
            decimal? n_dias_ord = null;
            decimal? n_dias_mes = null;
            decimal? n_dias_mes_cau = null;
            decimal? n_dias_mes_ord = null;
            bool b_cuota_acelerada = false;
            decimal[] rn_cau_atributos = new decimal[99];
            int n_num1 = 0;
            decimal? n_dias_plazo = null;
            decimal? n_dias_tot_mora = null;
            DateTime? f_fec_ant = null;
            decimal? n_dias_acu_mor = null;
            decimal? n_dias_periodo = null;
            int n_num_mor = 0;
            DateTime? f_fec_cau_ant = null;
            DateTime? f_fecha_mora_ant = null;
            decimal? n_dias_cau_mor = null;
            decimal? n_dias_ord_mor = null;
            decimal? n_dias_mes_mor = null;
            decimal? n_dias_mes_cau_mor = null;
            decimal? n_dias_mes_ord_mor = null;
            DateTime? f_prox_pago_aux = null;
            decimal? n_dias_venc = null;
            decimal? n_val_tot = null;
            decimal? n_total_cobrado = null;
            // lconsulta SYS_REFCURSOR;
            decimal? n_tasa = null;

            gn_tipo_pago = 5;
            if (Cargar_Credito(pnumero_radicacion))
            {
                // -- Determinar la fecha de la causacio anterior              
                f_fec_cau_ant = Convert.ToDateTime(BOFunciones.DateConstruct(BOFunciones.DateYear(pfecha_corte), BOFunciones.DateMonth(pfecha_corte), 1, 0, 0, 0)).AddDays(-1);
                // -- Inicializar variables
                n_num1 = 1;
                while (n_num1 <= 20)
                {
                    rn_cau_atributos[n_num1 + 20] = 0;
                    rn_cau_atributos[n_num1 + 30] = 0;
                    rn_cau_atributos[n_num1 + 40] = 0;
                    rn_cau_atributos[n_num1 + 50] = 0;
                    rn_cau_atributos[n_num1 + 60] = 0;
                    n_num1 = n_num1 + 1;
                };
                // -- Calcula los valores causados
                nerror = Amortizar(Convert.ToInt32(gn_tipo_pago), 0, pfecha_corte, 0, ref f_prox_pago_aux, ref rn_tot_capital, ref rn_cod_atributos, ref n_tot_atributos, ref rn_num_atr);
                // -- Determina que atributos causan
                n_total_cobrado = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_cod_atributos.Length)
                {
                    //Open lconsulta For Select a.causa From atributos a Where a.cod_atr = rn_cod_atributos(n_num);
                    //Fetch lConsulta Into n_causa;
                    //Close lConsulta;
                    n_atr_causa[Convert.ToInt32(rn_cod_atributos[n_num])] = BOFunciones.NVL(n_causa, 0) + 1;
                    n_total_cobrado = n_total_cobrado + BOFunciones.NVL(n_tot_atributos[n_num], 0);
                    n_num = n_num + 1;
                };
                if (n_total_cobrado == 0)
                {
                    return;
                };
                // -- Determina los dias del periodo, teniendo e cuenta si es un plazo fijo
                if (n_tip_cuota == 1)
                {
                    //Open lconsulta For Select numero_dias From periodicidad Where cod_periodicidad = n_for_pla;
                    //Fetch lConsulta Into n_dias_per;
                    //Close lConsulta;
                    n_dias_per = n_dias_per * n_plazo;
                    n_dias_plazo = n_dias_per;
                    f_fecha_inicio = f_fec_inicio;
                }
                else
                {
                    //Open lconsulta For Select numero_dias From periodicidad Where cod_periodicidad = n_periodic;
                    //Fetch lConsulta Into n_dias_per;
                    //Close lConsulta;
                };
                // -- Inicializa dias trascurridos para pasar a cuentas de orden
                n_dias_per_aux = n_dias_per;
                n_dias_total = 0;
                if (BOFunciones.BuscarGeneral(25, 2) == "1")
                {
                    // -- Determinar si no se tienen en cuenta los días de la cuota vencida
                    if (n_tip_pago == 1)
                    {
                        n_dias_total = 0;
                    }
                    else
                    {
                        n_dias_total = -n_dias_per;
                    };
                };
                rn_tot_capital = 0;
                n_dias_acu_mor_pf = 0;
                // -- Calcular el número total de días de mora.
                if (f_prox_pag < pfecha_corte)
                {
                    n_dias_tot_mora = BOFunciones.FecDifDia(f_prox_pag, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                }
                else
                {
                    n_dias_tot_mora = 0;
                };
                // -- Determina los dias de suspencion de la causacion
                if (n_clasificacion == null)
                {
                    n_clasificacion = 1;
                };
                //Select BOFunciones.NVL(Max(dias_maximo), 0) Into n_dias_causa From dias_categorias Where cod_clasifica = n_clasificacion And cod_categoria = (Select max(cod_categoria) From dias_categorias Where cod_clasifica = n_clasificacion And causa = '1');  
                // -- Ciclo por cada fecha
                while (true)
                {
                    n_dias_cau = 0;
                    n_dias_ord = 0;
                    n_dias_mes = 0;
                    n_dias_mes_cau = 0;
                    n_dias_mes_ord = 0;
                    n_dias_ajuste = 0;
                    b_cuota_acelerada = false;
                    // -- Determina los dias dentro del periodo si es anticipado o vencido
                    if (n_tip_pago == 1)
                    {
                        f_fec_ant = BOFunciones.FecResDia(f_prox_pag, Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tipo_cal));
                        if (f_fec_ant > pfecha_corte)
                        {
                            if (f_prox_pag >= pfecha_corte)
                            {
                                n_dias_periodo = 0;
                            }
                            else
                            {
                                n_dias_periodo = BOFunciones.FecDifDia(f_prox_pag, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                            };
                        }
                        else
                        {
                            n_dias_periodo = n_dias_per;
                        };
                        if (f_prox_pag >= pfecha_corte)
                        {
                            break;
                        };
                    }
                    else
                    {
                        f_fec_ant = BOFunciones.FecResDia(f_prox_pag, Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tipo_cal));
                        if (n_tip_intant == 3 && n_cuo_pag == 0)
                        {
                            f_fec_ant = f_fec_apro;
                            if (n_dias_aju > 0 && n_dias_aju != null)
                            {
                                n_dias_per = n_dias_per + BOFunciones.NVL(n_dias_aju, 0);
                            };
                        };
                        if (f_prox_pag > pfecha_corte)
                        {
                            if (f_fec_ant >= pfecha_corte)
                            {
                                n_dias_periodo = 0;
                            }
                            else
                            {
                                n_dias_periodo = BOFunciones.FecDifDia(f_fec_ant, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                            };
                        }
                        else
                        {
                            n_dias_periodo = n_dias_per;
                        };
                        if (f_fec_ant >= pfecha_corte)
                        {
                            break;
                        };
                    };
                    // -- Determina cuantos dias son causados y de orden
                    if (n_dias_total + n_dias_periodo > n_dias_causa && !(n_tip_intant == 3 && n_cuo_pag == 0 && n_dias_total + n_dias_periodo - n_dias_ajuste <= n_dias_causa))
                    {
                        if (n_dias_total >= n_dias_causa)
                        {
                            n_dias_ord = n_dias_periodo;
                        }
                        else
                        {
                            n_dias_cau = (n_dias_causa - BOFunciones.NVL(n_dias_total, 0));
                            n_dias_ord = n_dias_periodo - BOFunciones.NVL(n_dias_cau, 0);
                        };
                    }
                    else
                    {
                        n_dias_cau = n_dias_periodo;
                    };
                    n_dias_total = n_dias_total + n_dias_per;
                    n_dias_acu_mor = n_dias_total;
                    // -- Determina cuantos dias son del mes causados y cuantos del mes de orden
                    if (n_tip_pago == 1)
                    {
                        if (f_prox_pag > f_fec_cau_ant)
                        {
                            if (f_fec_ant > pfecha_corte)
                            {
                                n_dias_mes = BOFunciones.FecDifDia(f_prox_pag, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                            }
                            else
                            {
                                n_dias_mes = BOFunciones.FecDifDia(f_prox_pag, f_fec_ant, Convert.ToInt32(n_tipo_cal));
                            };
                        }
                        else
                        {
                            if (f_fec_ant > f_fec_cau_ant)
                            {
                                if (f_fec_ant > pfecha_corte)
                                {
                                    n_dias_mes = BOFunciones.FecDifDia(f_fec_cau_ant, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                                }
                                else
                                {
                                    n_dias_mes = BOFunciones.FecDifDia(f_fec_cau_ant, f_fec_ant, Convert.ToInt32(n_tipo_cal));
                                };
                            }
                            else
                            {
                                n_dias_mes = 0;
                            };
                        };
                    }
                    else
                    {
                        if (f_fec_ant > f_fec_cau_ant)
                        {
                            if (f_prox_pag > pfecha_corte)
                            {
                                n_dias_mes = BOFunciones.FecDifDia(f_fec_ant, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                            }
                            else
                            {
                                n_dias_mes = BOFunciones.FecDifDia(f_fec_ant, f_prox_pag, Convert.ToInt32(n_tipo_cal));
                            };
                        }
                        else
                        {
                            if (f_prox_pag > f_fec_cau_ant)
                            {
                                if (f_prox_pag > pfecha_corte)
                                {
                                    n_dias_mes = BOFunciones.FecDifDia(f_fec_cau_ant, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                                }
                                else
                                {
                                    n_dias_mes = BOFunciones.FecDifDia(f_fec_cau_ant, f_prox_pag, Convert.ToInt32(n_tipo_cal));
                                };
                            }
                            else
                            {
                                n_dias_mes = 0;
                            };
                        };
                    };
                    if (n_dias_ord > n_dias_mes)
                    {
                        n_dias_mes_ord = n_dias_mes;
                    }
                    else
                    {
                        n_dias_mes_ord = n_dias_ord;
                        n_dias_mes_cau = n_dias_mes - n_dias_ord;
                    };
                    // -- Recorre arreglo de valores por cada cuota
                    n_num = 1;
                    while (n_num < n_cont_amortiza)
                    {
                        if (cl_amortiza_cre[n_num].f_fecha_cuota == f_prox_pag)
                        {
                            // -- Recorre cada atributo
                            n_num1 = 1;
                            while (n_num1 <= rn_num_atr && n_num1 <= rn_cod_atributos.Length)
                            {
                                if (rn_cod_atributos[n_num1] == cl_amortiza_cre[n_num].n_cod_atr && n_atr_causa[Convert.ToInt32(rn_cod_atributos[n_num1])] == 2)
                                {
                                    if (n_atr_mora == rn_cod_atributos[n_num1])
                                    {
                                        n_num_mor = VisArrayFindDateTimeFechas(2, f_prox_pag);
                                        // -- Esta variable se utiliza para controlar que no repita cuotas
                                        f_fecha_mora_ant = null;
                                        if (n_num_mor == -1)
                                        {
                                            n_num_mor = 1;
                                        };
                                        while (n_num_mor <= n_cont_mora)
                                        {
                                            if (cl_det_mora_cre[n_num_mor].f_fecha_cuota == f_prox_pag && cl_det_mora_cre[n_num_mor].f_fecha_fin == pfecha_corte)
                                            {
                                                // -- Esto se colocó porque en COOACEDED los intereses de ajuste se pagan en la primera cuota.
                                                if (n_tip_intant == 3)
                                                {
                                                    n_dias_periodo = BOFunciones.FecDifDia(cl_det_mora_cre[n_num_mor].f_fecha_ini, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                                                }
                                                else
                                                {
                                                    n_dias_periodo = cl_det_mora_cre[n_num_mor].n_dias_mora;
                                                };
                                                n_dias_cau_mor = 0;
                                                n_dias_ord_mor = 0;
                                                // -- Determina cuantos dias son causados y de orden
                                                if (n_tip_pago == 1)
                                                {
                                                    if (n_dias_acu_mor_pf + n_dias_periodo > n_dias_causa)
                                                    {
                                                        if ((n_dias_acu_mor - n_dias_per) >= n_dias_causa)
                                                        {
                                                            n_dias_ord_mor = n_dias_periodo;
                                                        }
                                                        else
                                                        {
                                                            n_dias_cau_mor = (n_dias_causa - (n_dias_acu_mor_pf));
                                                            n_dias_ord_mor = n_dias_periodo - n_dias_cau_mor;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_dias_cau_mor = n_dias_periodo;
                                                    };
                                                }
                                                else
                                                {
                                                    if ((n_dias_acu_mor + n_dias_periodo) > n_dias_causa && !(n_dias_tot_mora < n_dias_causa && n_tip_intant == 3))
                                                    {
                                                        if (n_dias_acu_mor >= n_dias_causa)
                                                        {
                                                            n_dias_ord_mor = n_dias_periodo;
                                                        }
                                                        else
                                                        {
                                                            n_dias_cau_mor = (n_dias_causa - n_dias_acu_mor);
                                                            n_dias_ord_mor = n_dias_periodo - n_dias_cau_mor;
                                                        };
                                                    }
                                                    else
                                                    {
                                                        n_dias_cau_mor = n_dias_periodo;
                                                    };
                                                };
                                                // -- Determina cuantos dias son del mes causados y cuantos del mes de orden
                                                if (n_dias_cau_mor > 0 && b_cuota_acelerada)
                                                {
                                                    n_dias_ord_mor = n_dias_ord_mor + n_dias_cau_mor;
                                                    n_dias_cau_mor = 0;
                                                };
                                                if (cl_det_mora_cre[n_num_mor].f_fecha_ini > f_fec_cau_ant)
                                                {
                                                    if (cl_det_mora_cre[n_num_mor].f_fecha_fin > pfecha_corte)
                                                    {
                                                        n_dias_mes_mor = BOFunciones.FecDifDia(cl_det_mora_cre[n_num_mor].f_fecha_ini, pfecha_corte, Convert.ToInt32(n_tipo_cal));
                                                    }
                                                    else
                                                    {
                                                        n_dias_mes_mor = BOFunciones.FecDifDia(cl_det_mora_cre[n_num_mor].f_fecha_ini, cl_det_mora_cre[n_num_mor].f_fecha_fin, Convert.ToInt32(n_tipo_cal));
                                                    };
                                                    if (n_dias_mes_mor >= 0)
                                                    {
                                                        n_dias_mes_mor = n_dias_mes_mor + 1;
                                                    };
                                                }
                                                else
                                                {
                                                    if (cl_det_mora_cre[n_num_mor].f_fecha_fin > f_fec_cau_ant)
                                                    {
                                                        n_dias_mes_mor = BOFunciones.FecDifDia(f_fec_cau_ant, cl_det_mora_cre[n_num_mor].f_fecha_fin, Convert.ToInt32(n_tipo_cal));
                                                    }
                                                    else
                                                    {
                                                        n_dias_mes_mor = 0;
                                                    };
                                                };
                                                if (n_dias_ord_mor > n_dias_mes_mor)
                                                {
                                                    n_dias_mes_ord_mor = n_dias_mes_mor;
                                                }
                                                else
                                                {
                                                    n_dias_mes_ord_mor = n_dias_ord_mor;
                                                    n_dias_mes_cau_mor = n_dias_mes_mor - n_dias_ord_mor;
                                                };
                                                // -- Distribuir el valor de acuerdo a los dias
                                                if (n_dias_periodo == 0 || n_dias_periodo == null)
                                                {
                                                    n_dias_periodo = 1;
                                                };
                                                if (n_dias_cau_mor > 0)
                                                {
                                                    rn_cau_atributos[n_num1 + 20] = rn_cau_atributos[n_num1 + 20] + Convert.ToDecimal(BOFunciones.NVL(cl_det_mora_cre[n_num_mor].n_saldo / n_dias_periodo * n_dias_cau_mor, 0));
                                                };
                                                if (n_dias_ord_mor > 0)
                                                {
                                                    rn_cau_atributos[n_num1 + 30] = rn_cau_atributos[n_num1 + 30] + Convert.ToDecimal(BOFunciones.NVL(cl_det_mora_cre[n_num_mor].n_saldo / n_dias_periodo * n_dias_ord_mor, 0));
                                                };
                                                if (n_dias_mes_mor > 0)
                                                {
                                                    rn_cau_atributos[n_num1 + 40] = rn_cau_atributos[n_num1 + 40] + Convert.ToDecimal(BOFunciones.NVL(cl_det_mora_cre[n_num_mor].n_saldo / n_dias_periodo * n_dias_mes_mor, 0));
                                                };
                                                if (n_dias_mes_cau_mor > 0)
                                                {
                                                    rn_cau_atributos[n_num1 + 50] = rn_cau_atributos[n_num1 + 50] + Convert.ToDecimal(BOFunciones.NVL(cl_det_mora_cre[n_num_mor].n_saldo / n_dias_periodo * n_dias_mes_cau_mor, 0));
                                                };
                                                if (n_dias_mes_ord_mor > 0)
                                                {
                                                    rn_cau_atributos[n_num1 + 60] = rn_cau_atributos[n_num1 + 60] + Convert.ToDecimal(BOFunciones.NVL(cl_det_mora_cre[n_num_mor].n_saldo / n_dias_periodo * n_dias_mes_ord_mor, 0));
                                                };
                                                n_dias_acu_mor = n_dias_acu_mor + n_dias_periodo;
                                                n_dias_acu_mor_pf = n_dias_acu_mor_pf + n_dias_periodo;
                                            };
                                            f_fecha_mora_ant = cl_det_mora_cre[n_num_mor].f_fecha_cuota;
                                            n_num_mor = n_num_mor + 1;
                                        };
                                    }
                                    else
                                    {
                                        if (n_dias_per != 0 && n_dias_per != null)
                                        {
                                            //DBMS_OUTPUT.PUT_LINE("n_saldo" || cl_amortiza_cre(n_num).n_saldo);
                                            // -- Calculando el valor causado total
                                            if (n_dias_cau > 0)
                                            {
                                                rn_cau_atributos[n_num1 + 20] = rn_cau_atributos[n_num1 + 20] + Convert.ToDecimal(BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo / n_dias_per * n_dias_cau, 0));
                                            };
                                            // -- Calculando el valor de orden total
                                            if (n_dias_ord > 0)
                                            {
                                                rn_cau_atributos[n_num1 + 30] = rn_cau_atributos[n_num1 + 30] + Convert.ToDecimal(BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo / n_dias_per * n_dias_ord, 0));
                                            };
                                            // -- Calculando el valor del mes para ingreso
                                            if (n_dias_mes > 0)
                                            {
                                                rn_cau_atributos[n_num1 + 40] = rn_cau_atributos[n_num1 + 40] + Convert.ToDecimal(BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo / n_dias_per * n_dias_mes, 0));
                                            };
                                            // -- Calculando el valor causado del mes
                                            if (n_dias_mes_cau > 0)
                                            {
                                                rn_cau_atributos[n_num1 + 50] = rn_cau_atributos[n_num1 + 50] + Convert.ToDecimal(BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo / n_dias_per * n_dias_mes_cau, 0));
                                            };
                                            // -- calculando el valor de orden del mes
                                            if (n_dias_mes_ord > 0)
                                            {
                                                rn_cau_atributos[n_num1 + 60] = rn_cau_atributos[n_num1 + 60] + Convert.ToDecimal(BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo / n_dias_per * n_dias_mes_ord, 0));
                                            };
                                            //Insert Into det_causacion Values(sq_det_causacion.nextval, pfecha_corte, Pnumero_Radicacion, cl_amortiza_cre(n_num).f_fecha_cuota, cl_amortiza_cre(n_num).n_cod_atr, n_dias_cau, n_dias_ord, n_dias_mes_cau, n_dias_mes_ord, cl_amortiza_cre(n_num).n_saldo);  
                                        };
                                    };
                                    break;
                                };
                                n_num1 = n_num1 + 1;
                            };
                        };
                        // -- Determina el valor vencido de capital
                        if (cl_amortiza_cre[n_num].f_fecha_cuota == f_prox_pag && cl_amortiza_cre[n_num].n_cod_atr == 1 && f_prox_pag <= pfecha_corte)
                        {
                            rn_tot_capital = BOFunciones.NVL(rn_tot_capital, 0) + BOFunciones.NVL(cl_amortiza_cre[n_num].n_saldo, 0);
                        };
                        n_num = n_num + 1;
                    };
                    // -- Avanza la fecha al siguiente periodo
                    if (f_prox_pag == null)
                    {
                        break;
                    };
                    f_prox_pag = BOFunciones.FecSumDia(f_prox_pag, Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tipo_cal));
                    if (n_tip_intant == 3 && n_cuo_pag == 0)
                    {
                        n_cuo_pag = n_cuo_pag + 1;
                        n_dias_per = n_dias_per_aux;
                    };
                };
                // -- Insertar los valores en la tabla de causaciòn
                n_num1 = 1;
                while (n_num1 <= rn_num_atr && n_num1 <= rn_cod_atributos.Length)
                {
                    // -- Redondear los valores
                    if (rn_cau_atributos[n_num1 + 20] != 0)
                    {
                        rn_cau_atributos[n_num1 + 20] = Convert.ToDecimal(BOFunciones.Redondeo(rn_cau_atributos[n_num1 + 20]));
                    };
                    if (rn_cau_atributos[n_num1 + 30] != 0)
                    {
                        rn_cau_atributos[n_num1 + 30] = Convert.ToDecimal(BOFunciones.Redondeo(rn_cau_atributos[n_num1 + 30]));
                    };
                    if (rn_cau_atributos[n_num1 + 40] != 0)
                    {
                        rn_cau_atributos[n_num1 + 40] = Convert.ToDecimal(BOFunciones.Redondeo(rn_cau_atributos[n_num1 + 40]));
                    };
                    if (rn_cau_atributos[n_num1 + 50] != 0)
                    {
                        rn_cau_atributos[n_num1 + 50] = Convert.ToDecimal(BOFunciones.Redondeo(rn_cau_atributos[n_num1 + 50]));
                    };
                    if (rn_cau_atributos[n_num1 + 60] != 0)
                    {
                        rn_cau_atributos[n_num1 + 60] = Convert.ToDecimal(BOFunciones.Redondeo(rn_cau_atributos[n_num1 + 60]));
                    };
                    // -- Insertar datos en la tabla si se causa
                    if (n_atr_causa[Convert.ToInt32(rn_cod_atributos[n_num1])] == 2)
                    {
                        if (rn_cau_atributos[n_num1 + 20] > 0 || rn_cau_atributos[n_num1 + 30] > 0 || rn_cau_atributos[n_num1 + 40] > 0)
                        {
                            if (f_prox_pag < pfecha_corte)
                            {
                                n_dias_venc = BOFunciones.FecDifDia(f_prox_pag, pfecha_corte, 1);
                            }
                            else
                            {
                                n_dias_venc = 0;
                            };
                            n_val_tot = BOFunciones.NVL(rn_cau_atributos[n_num1 + 20], 0) + BOFunciones.NVL(rn_cau_atributos[n_num1 + 30], 0);
                            if (rn_cod_atributos[n_num1] == n_atr_corr)
                            {
                                n_tasa = n_tasa_intcte;
                            };
                            if (rn_cod_atributos[n_num1] == n_atr_mora)
                            {
                                n_tasa = n_tasa_intmor;
                            };
                            //Insert Into causacion Values(sq_causacion.nextval, pfecha_corte, Pnumero_Radicacion, rn_cod_atributos(n_num1), rn_cau_atributos(n_num1 + 40), rn_cau_atributos(n_num1 + 50), rn_cau_atributos(n_num1 + 60), rn_cau_atributos(n_num1 + 20), rn_cau_atributos(n_num1 + 30), rn_tot_capital, n_dias_tot_mora, n_dias_causa, n_dias_per, n_tasa);
                        };
                    };
                    n_num1 = n_num1 + 1;
                };
            };

        }

        private void Proyeccion(Int64 pnumero_radicacion, DateTime pfecha)
        {
            // -- Variables para movimientos del crèdito
            int? nerror;
            decimal? rn_tot_capital = 0;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            DateTime? f_prox_pago_aux = null;

            if (Cargar_Credito(pnumero_radicacion))
            {
                g_b_historico_amortiza = true;
                gn_tipo_pago = 7;
                nerror = Amortizar(Convert.ToInt32(gn_tipo_pago), 0, pfecha, n_num_cuotas, ref f_prox_pago_aux, ref rn_tot_capital, ref rn_cod_atributos, ref n_tot_atributos, ref rn_num_atr);
            }
            else
            {
                // raise_application_error(-20101, "No se encontro crèdito con los datos dados, radicaciòn: " || pnumero_radicacion);
                return;
            };
            // Commit;
        }

        private void Condonacion(Int64 pnumero_radicacion, DateTime pfecha_condonacion, Int64 pcod_ope, Int64 pcod_usu, Int64 pcod_ofi, decimal pintcorriente, decimal pintmora)
        {
            DateTime? f_f_prox_pago = null;
            decimal? n_tot_capital = null;
            decimal?[] n_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int n_num_atr = 0;
            decimal? n_valor_cobrado;
            int? n_error;
            decimal? n_cap_tf = null;
            decimal? n_int_tf = null;
            decimal? n_mora_tf = null;
            int n_cont = 0;
            int n_tipo = 0;
            decimal? n_interes = null;
            decimal? n_valor_pago = null;
            Int64 n_cod_det_lis = 0;
            // pCursor SYS_REFCURSOR;
            DateTime? fecultcie = null;

            // -- Verificando datos nulos
            if (pnumero_radicacion == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el número del crédito");
                return;
            };
            if (pfecha_condonacion == null)
            {
                // Raise_Application_Error(-20101, "Debe especificar la fecha de condonación");
                return;
            };
            if (pcod_usu == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el usuario que realiza la condonación");
                return;
            };
            if (pcod_ofi == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el código de la oficina");
                return;
            };
            // -- Validar la fecha
            //Open pCursor For Select Max(fecha) From cierea Where tipo = 'R' and estado = 'D';
            //Fetch pCursor Into fecultcie;
            //Close pCursor;
            if (fecultcie != null)
            {
                if (pfecha_condonacion <= fecultcie)
                {
                    //Raise_Application_Error(-20101, "La fecha de condonacion " || pfecha_condonacion || " no puede ser anterior o igual a la fecha del último cierre. (" || fecultcie);
                    return;
                };
            };
            // -- Verificando la amortización del crédito
            if (Cargar_Credito(pnumero_radicacion))
            {
                n_tipo = 2;
                n_error = Amortizar(n_tipo, 0, pfecha_condonacion, 0, ref f_f_prox_pago, ref n_tot_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr);
                if (n_error != 0)
                {
                    //Raise_Application_Error(-20101, "No se pudo hacer la amortización del crédito " || pnumero_radicacion || " a la fecha " || pfecha_condonacion || " error:" || n_error);
                    return;
                };
            }
            else
            {
                //Raise_Application_Error(-20101, "No se pudo cargar datos del crédito " || pnumero_radicacion);
                return;
            };
            // -- Aplicar el valor a condonar los demás atributos van en cero
            if (pintcorriente != 0)
            {
                Nota_Credito(pnumero_radicacion, pcod_ope, 15, pfecha_condonacion, BOFunciones.AtrCorriente(), pintcorriente, ref n_error);
            };
            if (pintmora != 0)
            {
                Nota_Credito(pnumero_radicacion, pcod_ope, 15, pfecha_condonacion, BOFunciones.AtrMora(), pintmora, ref n_error);
            };
            // --n_tot_capital = 0;
            // --n_valor_pago = 0;  
            // --n_num_atr_pago = 0;  
            // --n_cont = 1;  
            // --While n_cont <= n_num_atr ) {  
            // --  If n_cod_atributos(n_cont) = n_atr_corr) {
            // --    If n_tot_atributos(n_cont) < pintcorriente And(pintcorriente != null And pintcorriente != 0)) {
            // --      Raise_Application_Error(-20101, "El valor a condonar de interés corriente " || pintcorriente || " es mayor que el valor adeudado " || n_tot_atributos(n_cont) || " para el crédito " || pnumero_radicacion || " a la fecha " || pfecha_condonacion);  
            // --    } else {   
            // --    n_num_atr_pago = n_num_atr_pago + 1;  
            // --    n_val_atr_pago(n_num_atr) =  pintcorriente;  
            // --    n_cod_atr_pago(n_num_atr) =  n_atr_corr;     
            // --    n_valor_pago = n_valor_pago + pintcorriente;  
            // --    };  
            // --  };  
            // --  n_cont = n_cont + 1;  
            // --};;  
            // --If n_num_atr_pago >= 1) {      
            // --n_tipo = 1;  
            // --n_error = Amortizar(n_tipo, n_valor_pago, pfecha_condonacion, 0, f_f_prox_pago, n_tot_capital, n_cod_atributos, n_tot_atributos, n_num_atr);  
            // --If n_error != 0) {
            // --  Raise_Application_Error(-20101, "No se pudo hacer la amortización del crédito " || pnumero_radicacion || " a la fecha " || pfecha_condonacion);  
            // --};  
            // --n_cod_det_lis = null;        
            // --If NOT Pagar(pcod_ope, pfecha_condonacion, n_cod_det_lis, 15, f_f_prox_pago, n_tot_capital, n_cod_atributos, n_tot_atributos, n_num_atr, pcod_usu)) {  
            // --  Raise_Application_Error(-20101, "No se pudo hacer la condonación del crédito " || pnumero_radicacion || " a la fecha " || pfecha_condonacion);  
            // --};  
            // --};
        }

        private void Castigo(Int64? pnumero_radicacion, DateTime? pfecha_castigo, string pcod_linea_castigo, Int64 pcod_ope, Int64? pcod_usu, Int64? pcod_ofi)
        {
            DateTime? f_f_prox_pago = null;
            decimal? n_tot_capital = null;
            decimal?[] n_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int n_num_atr = 0;
            int? n_error = null;
            int? n_tipo = null;
            decimal? n_interes = null;
            int n_num = 0;
            Int64? n_num_tran = 0;
            // pCursor SYS_REFCURSOR;
            DateTime? fecultcie = null;
            int TomarCausado = 0;
            DateTime? lfecha_corte = null;

            // -- Verificando datos nulos
            if (pnumero_radicacion == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el número del crédito");
                return;
            };
            if (pfecha_castigo == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de castigo");
                return;
            };
            if (pcod_usu == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el usuario que realiza el castigo");
                return;
            };
            if (pcod_ofi == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el código de la oficina");
                return;
            };
            // -- Validar la fecha
            //Open pCursor For Select Max(fecha) From cierea Where tipo = "R" and estado = "D";
            //Fetch pCursor Into fecultcie;
            //Close pCursor;
            if (fecultcie != null)
            {
                if (pfecha_castigo <= fecultcie)
                {
                    //Raise_Application_Error(-20101, "La fecha de castigo " || pfecha_castigo || " no puede ser anterior o igual a la fecha del último cierre. (" || fecultcie);
                    return;
                };
            };
            // -- Determinar fecha de la ultima causación
            //Open pCursor For Select Max(fecha) From cierea Where tipo = "U" and estado = "D";
            //Fetch pCursor Into lfecha_corte;
            if (lfecha_corte != null)
            {
                TomarCausado = 1;
            }
            else
            {
                TomarCausado = 0;
            };
            //Close pCursor;
            // -- Verificando la amortización del crédito
            if (Cargar_Credito(Convert.ToInt64(pnumero_radicacion)))
            {
                n_tipo = 4;
                n_error = Amortizar(Convert.ToInt32(n_tipo), 0, pfecha_castigo, 0, ref f_f_prox_pago, ref n_tot_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr);
                if (n_error != 0)
                {
                    //Raise_Application_Error(-20101, "No se pudo hacer la amortización del crédito " || pnumero_radicacion || " a la fecha " || pfecha_castigo || " error:" || n_error);
                    return;
                };
                // -- Insertando los valores de capital del castigo
                if (n_saldo != null)
                {
                    while (true)
                    {
                        try
                        {
                            n_num_tran = 0;
                            //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, s_cod_credi, 24, Null, 1, n_saldo, 0, 0, 0, 1, Null)
                            //  Returning num_tran Into n_num_tran;
                            break;
                        }
                        catch
                        {
                            n_num_tran = Consecutivos("2");
                        }
                        break;
                    };
                    while (true)
                    {
                        try
                        {
                            n_num_tran = 0;
                            //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, pcod_linea_castigo, 1, Null, 1, n_saldo, 0, 0, 0, 1, Null)
                            //Returning num_tran Into n_num_tran;
                            break;
                        }
                        catch
                        {
                            n_num_tran = Consecutivos("2");
                        }
                        break;
                    };
                };
                // -- Insertando los valores por atributos del castigo
                if (TomarCausado == 1)
                {
                    //For datos In(Select cod_atr, saldo_causado From causacion Where fecha_corte = lfecha_corte And numero_radicacion = pnumero_radicacion) ) {
                    while (true)
                    {
                        try
                        {
                            n_num_tran = 0;
                            //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, s_cod_credi, 24, Null, datos.cod_atr, datos.saldo_causado, 0, 0, 0, 1, Null)
                            //Returning num_tran Into n_num_tran;
                            break;
                        }
                        catch
                        {
                            n_num_tran = Consecutivos("1");
                        }
                        break;
                    };
                    while (true)
                    {
                        try
                        {
                            n_num_tran = 0;
                            //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, pcod_linea_castigo, 1, Null, datos.cod_atr, datos.saldo_causado, 0, 0, 0, 1, Null)
                            //Returning num_tran Into n_num_tran;
                            break;
                        }
                        catch
                        {
                            n_num_tran = Consecutivos("1");
                        }
                        break;
                    };
                }
                else
                {
                    n_interes = 0;
                    n_num = 1;
                    while (n_num <= n_num_atr && n_num <= n_tot_atributos.Length)
                    {
                        while (true)
                        {
                            try
                            {
                                n_num_tran = 0;
                                //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, s_cod_credi, 24, Null, n_cod_atributos(n_num), n_tot_atributos(n_num), 0, 0, 0, 1, Null)  
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("1");
                            }
                            break;
                        };
                        while (true)
                        {
                            try
                            {
                                n_num_tran = 0;
                                //Insert Into tran_cred Values(n_num_tran, pcod_ope, pnumero_radicacion, n_cod_cliente, pcod_linea_castigo, 1, Null, n_cod_atributos(n_num), n_tot_atributos(n_num), 0, 0, 0, 1, Null)  
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("1");
                            }
                            break;
                        };
                        n_interes = n_interes + n_tot_atributos[n_num];
                        n_num = n_num + 1;
                    };
                };
                // -- Actualizando datos del crédito
                //Insert Into castigo_cre Values(pnumero_radicacion, pfecha_castigo, pcod_usu, SYSDATE, Null, Null, n_saldo);
                //Insert Into novedad_cre Values(sq_novedad_cre.nextval, 310, pcod_usu, pnumero_radicacion, pfecha_castigo, SYSDATE, s_cod_credi, "1");
                //Update credito set cod_linea_credito = pcod_linea_castigo where numero_radicacion = pnumero_radicacion;
            }
            else
            {
                //Raise_Application_Error(-20101, "No se pudo cargar datos del crédito " || pnumero_radicacion);
                return;
            };
        }

        private void Refinanciacion(Int64? pnumero_radicacion, DateTime? pfecha_refinancia, decimal? pplazo, DateTime? pfecha_proximo_pago, decimal? pcuota, DateTime? pfecha_vencimiento, decimal? pvalor_pago, decimal? pvalor_refinancia, Int64? pCodOpe, Int64? pCodUsu)
        {
            DateTime? fecultcie = null;
            // pCursor SYS_REFCURSOR;
            Int64? consecutivo = null;
            int? n_error = null;
            DateTime? f_f_prox_pago = null;
            decimal? n_tot_capital = null;
            decimal?[] n_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int n_num_atr = 0;
            int lcod_atr = 0;
            //string lnom_atr = "";
            decimal? lvalor = null;
            int lrefinanciar = 0;
            int n_tipo = 0;
            Int64? ln_cod_det_lis = null;
            decimal? ltotal_aplica = null;
            Int64? n_num_tran = null;
            int contador = 0;

            // -- Verificando datos nulos
            if (pnumero_radicacion == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el número del crédito");
                return;
            };
            if (pfecha_refinancia == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de refinanciación");
                return;
            };
            if (pplazo == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el plazo");
                return;
            };
            if (pfecha_proximo_pago == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de próximo pago");
                return;
            };
            if (pfecha_vencimiento == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de vencimiento");
                return;
            };
            if (pvalor_refinancia == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el valor a refinanciar");
                return;
            };
            // -- Validar la fecha
            //Open pCursor For Select Max(fecha) From cierea Where tipo = "R" and estado = "D";
            //Fetch pCursor Into fecultcie;
            //Close pCursor;
            if (fecultcie != null)
            {
                if (pfecha_refinancia <= fecultcie)
                {
                    //Raise_Application_Error(-20101, "La fecha de refinanciación " || pfecha_refinancia || " no puede ser anterior o igual a la fecha del último cierre. (" || fecultcie);
                    return;
                };
            };
            // -- Verificando la amortización del crédito
            if (Cargar_Credito(Convert.ToInt64(pnumero_radicacion)))
            {
                contador = 0;
                //Select Count(*) Into contador From planpagos_original Where numero_radicacion = pnumero_radicacion;
                if (contador == 0)
                {
                    //Insert Into planpagos_original
                    //Select numero_radicacion, numerocuota, fechacuota, cod_atr, valor, valor_presente, sal_ini, sal_fin
                    //From planpagos Where numero_radicacion = pnumero_radicacion;
                };
                f_fec_inicio = BOFunciones.FecResDia(pfecha_proximo_pago, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                f_fec_term = f_fec_inicio;
                f_fec_term = BOFunciones.FecSumDia(f_fec_term, Convert.ToInt32(n_dias_per_cre * n_num_cuotas), Convert.ToInt32(n_tipo_cal));
                #region aplicar
                if (pCodOpe != null)
                {
                    ltotal_aplica = 0;
                    ln_cod_det_lis = 0;
                    n_tipo = 1;
                    n_num_atr_pago = 0;
                    n_man_pago_atr = 2;
                    //OPEN pCursor FOR Select cod_atr, nom_atr, valor, refinanciar From TEMP_ATRIBUTOS Where numero_radicacion = pnumero_radicacion;  
                    while (true)
                    {
                        //FETCH pCursor INTO lcod_atr, lnom_atr, lvalor, lrefinanciar;  
                        //EXIT WHEN pCursor%NOTFOUND;  
                        if (lrefinanciar == 1)
                        {
                            #region refinanciar
                            while (true)
                            {
                                try
                                {
                                    n_num_tran = 0;
                                    //Insert Into tran_cred Values(n_num_tran, pCodOpe, pnumero_radicacion, n_cod_cliente, s_cod_credi, 25, Null, lcod_atr, lvalor, 0, 0, 0, 1, Null)
                                    //Returning num_tran Into n_num_tran;
                                    break;
                                }
                                catch
                                {
                                    n_num_tran = Consecutivos("1");
                                }
                                break;
                            };
                            #endregion
                        }
                        else
                        {
                            n_num_atr_pago = n_num_atr_pago + 1;
                            n_cod_atr_pago[n_num_atr_pago] = lcod_atr;
                            n_val_atr_pago[n_num_atr_pago] = lvalor;
                            ltotal_aplica = ltotal_aplica + lvalor;
                        };
                    };
                    #region valor pago 
                    if (pvalor_pago > 0)
                    {
                        n_num_atr_pago = n_num_atr_pago + 1;
                        n_cod_atr_pago[n_num_atr_pago] = 1;
                        n_val_atr_pago[n_num_atr_pago] = pvalor_pago;
                        ltotal_aplica = ltotal_aplica + pvalor_pago;
                    };
                    #endregion
                    #region aplicar
                    if (ltotal_aplica > 0)
                    {
                        n_error = Amortizar(n_tipo, 0, pfecha_refinancia, 0, ref f_f_prox_pago, ref n_tot_capital, ref n_cod_atributos, ref n_tot_atributos, ref n_num_atr);
                        if (n_error == 0)
                        {
                            //if (!Pagar(pCodOpe, pfecha_refinancia, ln_cod_det_lis, 3, f_f_prox_pago, n_tot_capital, n_cod_atributos, n_tot_atributos, n_num_atr, pCodUsu) ) { 
                            //    return;
                            //};
                        };
                    };
                    #endregion                    
                };
                #endregion
                //Insert Into Refinanciacion Values(0, pnumero_radicacion, pfecha_refinancia, pCodOpe, n_plazo, f_prox_pag, n_cuota, f_prim_pago, f_fec_term, n_monto, n_saldo, n_cuo_pag,
                //  pplazo, pfecha_proximo_pago, pcuota, pfecha_vencimiento, pvalor_refinancia, pCodUsu, SYSDATE);
                //Update credito Set numero_cuotas = pplazo, fecha_proximo_pago = pfecha_proximo_pago, valor_cuota = pcuota, fecha_primerpago = pfecha_refinancia,
                //fecha_vencimiento = f_fec_term, saldo_capital = pvalor_refinancia, monto_aprobado = pvalor_refinancia, cuotas_pagadas = 0, CUOTAS_PENDIENTES = pplazo,
                //fecha_inicio = f_fec_inicio
                //where numero_radicacion = pnumero_radicacion;
                //Delete From amortiza_cre where numero_radicacion = pnumero_radicacion;  
                consecutivo = Consecutivos("4");
                //Insert Into novedad_cre Values(consecutivo, 305, pCodUsu, pnumero_radicacion, pfecha_refinancia, pfecha_refinancia, "Refinanciación",  "1");
            }
            else
            {
                //Raise_Application_Error(-20101, "No se pudo cargar datos del crédito " || pnumero_radicacion);
                return;
            };
        }

        private void Reliquidacion(Int64? pnumero_radicacion, DateTime? pfecha_reliquida, decimal? pplazo, DateTime? pfecha_proximo_pago, decimal? pcuota, decimal? pcod_periodicidad, Int64? pCodUsu)
        {
            DateTime? fecultcie = null;
            // pCursor SYS_REFCURSOR;
            Int64? consecutivo = 0;
            int? n_error = null;
            DateTime? f_f_prox_pago = null;
            decimal? n_tot_capital = null;
            decimal?[] n_cod_atributos = new decimal?[99];
            decimal?[] n_tot_atributos = new decimal?[99];
            int n_num_atr = 0;
            int lcod_atr = 0;
            string lnom_atr = null;
            decimal? lvalor = null;
            int lrefinanciar = 0;
            int n_tipo = 0;
            Int64? ln_cod_det_lis = null;
            decimal? ltotal_aplica = null;
            Int64? n_num_tran = null;
            int contador = 0;

            // -- Verificando datos nulos
            if (pnumero_radicacion == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el número del crédito");
                return;
            };
            if (pfecha_reliquida == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de reliquidación");
                return;
            };
            if (pplazo == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar el plazo");
                return;
            };
            if (pfecha_proximo_pago == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la fecha de próximo pago");
                return;
            };
            if (pcod_periodicidad == null)
            {
                //Raise_Application_Error(-20101, "Debe especificar la periodicidad");
                return;
            };
            // -- Validar la fecha
            //Open pCursor For Select Max(fecha) From cierea Where tipo = "R" and estado = "D";
            //Fetch pCursor Into fecultcie;
            //Close pCursor;
            if (fecultcie != null)
            {
                if (pfecha_reliquida <= fecultcie)
                {
                    //Raise_Application_Error(-20101, "La fecha de reliquidación " || pfecha_reliquida || " no puede ser anterior o igual a la fecha del último cierre. (" || fecultcie);
                    return;
                };
            };
            // -- Verificando la amortización del crédito
            if (Cargar_Credito(Convert.ToInt64(pnumero_radicacion)))
            {
                contador = 0;
                //Select Count(*) Into contador From planpagos_original Where numero_radicacion = pnumero_radicacion;
                if (contador == 0)
                {
                    //Insert Into planpagos_original
                    //Select numero_radicacion, numerocuota, fechacuota, cod_atr, valor, valor_presente, sal_ini, sal_fin
                    //From planpagos Where numero_radicacion = pnumero_radicacion;
                };
                //Insert Into Reliquidacion Values(0, pnumero_radicacion, pfecha_reliquida, n_monto, n_cuota, n_periodic, n_plazo, f_fec_apro, f_fec_inicio, f_prim_pago, f_fec_term, f_prox_pag, f_ult_pago,
                //n_cuo_pag, n_duracion_gracia, pCodUsu, SYSDATE);
                f_prox_pag = pfecha_proximo_pago;
                f_fec_term = BOFunciones.FecSumDia(f_prox_pag, Convert.ToInt32(n_dias_per_cre * n_num_cuotas), Convert.ToInt32(n_tipo_cal));
                f_fec_inicio = BOFunciones.FecResDia(f_prox_pag, Convert.ToInt32(n_dias_per_cre), Convert.ToInt32(n_tipo_cal));
                //Update credito Set numero_cuotas = pplazo, fecha_proximo_pago = pfecha_proximo_pago, valor_cuota = pcuota, fecha_primerpago = pfecha_reliquida,
                //fecha_vencimiento = f_fec_term, monto_aprobado = saldo_capital, cuotas_pagadas = 0, periodo_gracia = 0, cod_periodicidad = pcod_periodicidad,
                //cuotas_pendientes = 0, fecha_inicio = f_fec_inicio
                //where numero_radicacion = pnumero_radicacion;
                //Delete From amortiza_cre where numero_radicacion = pnumero_radicacion;  
                consecutivo = Consecutivos("4");
                // Insert Into novedad_cre Values(consecutivo, 303, pCodUsu, pnumero_radicacion, pfecha_reliquida, pfecha_reliquida, "Reliquidación",  "1");
            }
            else
            {
                //Raise_Application_Error(-20101, "No se pudo cargar datos del crédito " || pnumero_radicacion);
                return;
            };
        }

        private decimal? TIRCredito(Int64 pnumero_radicacion)
        {
            // lcursor SYS_REFCURSOR;
            DateTime?[] lAFechas = new DateTime?[99];
            decimal?[] lAValores = new decimal?[99];
            int? lguess;
            int lnumreg;
            decimal? ltir = null;
            DateTime? lfecha = null;
            decimal? lvalor = null;
            int? lverificar = null;
            DateTime? lfecha_calculo = null;
            // pBasedato SYS_REFCURSOR;

            Inicializar();
            //OPEN pBasedato For Select monto_solicitado, monto_aprobado, fecha_solicitud, fecha_aprobacion, numero_cuotas, cod_periodicidad, valor_cuota, fecha_vencimiento, fecha_inicio, cod_periodicidad, forma_pago, cod_linea_credito, tipo_liquidacion, cod_clasifica,  
            //            saldo_capital, cuotas_pagadas, fecha_proximo_pago, 1, tipo_gracia, "2", periodo_gracia, cod_periodicidad, estado, cod_deudor, cuotas_pagadas, fecha_ultimo_pago, estado, cod_empresa, cod_pagaduria, 1, gradiente, numero_radicacion, dias_ajuste,   
            //            fecha_primerpago, fecha_desembolso
            //            From credito Where numero_radicacion = pnumero_radicacion;
            //Fetch pBasedato Into n_monto_sol, n_monto, f_sol, f_fec_apro, n_plazo, n_for_pla, n_cuota, f_fec_term, f_fec_inicio, n_periodic, s_for_pag, s_cod_credi, s_tipo_liq, n_cod_clasifica,
            //  n_saldo, n_cuo_pag, f_prox_pag, n_dia_habil, n_tip_gracia, s_atr_gracia, n_duracion_gracia, n_periodic_gracia, s_estado, n_cod_cliente, n_cuo_credito, f_ult_pago, s_estado_cre, n_cod_empresa, n_cod_pagaduria, s_tipo_grad, n_val_grad, n_radic, n_dias_aju,
            //  f_prim_pago, f_fec_desembolso;
            if (true)
            { // pBasedato% FOUND) {
                // -- Determinando la periodicidad
                //Select tipo_calendario, numero_dias into n_tipo_cal, n_dias_per_cre from periodicidad where cod_periodicidad = n_periodic;
                // -- Determinar la fecha de cálculo de la TIR
                if (f_fec_desembolso == null)
                {
                    if (f_fec_apro == null)
                    {
                        lfecha_calculo = f_sol;
                    }
                    else
                    {
                        lfecha_calculo = f_fec_apro;
                    };
                }
                else
                {
                    lfecha_calculo = f_fec_desembolso;
                };
                // -- Verificar que el crédito tenga el plan de pagos
                lverificar = 0;
                // SELECT COUNT(*) INTO lverificar FROM planpagos p WHERE p.numero_radicacion = pnumero_radicacion;
                if (lverificar <= 0 || lverificar == null)
                {
                    // USP_XPINN_CRE_GENERAPLANPAGOS(pnumero_radicacion);
                };
                // -- Generando la TIR
                lAFechas = new DateTime?[99];
                lAValores = new decimal?[99];
                ltir = 0;
                lnumreg = 0;
                lnumreg = lnumreg + 1;
                Array.Resize(ref lAFechas, 1);
                lAFechas[lnumreg] = lfecha_calculo;
                Array.Resize(ref lAValores, 1);
                if (n_monto == null || n_monto == 0)
                {
                    lAValores[lnumreg] = -n_monto_sol;
                }
                else
                {
                    lAValores[lnumreg] = -n_monto;
                };
                //OPEN lCursor FOR
                //  SELECT p.fechacuota, SUM(p.valor) As valor FROM planpagos p
                //    WHERE p.numero_radicacion = pnumero_radicacion GROUP BY p.fechacuota ORDER BY 1;
                while (true)
                {
                    // FETCH lCursor INTO lfecha, lvalor;
                    // EXIT WHEN lCursor % NOTFOUND;
                    lnumreg = lnumreg + 1;
                    Array.Resize(ref lAFechas, lAFechas.Length + 1);
                    lAFechas[lnumreg] = lfecha;
                    Array.Resize(ref lAValores, lAValores.Length + 1);
                    lAValores[lnumreg] = lvalor;
                    // -- DBMS_OUTPUT.PUT_LINE("Fecha: " || lfecha || " Valor:" || lvalor);  
                };
                lguess = null;
                if (lnumreg > 0)
                {
                    ltir = BOFunciones.Calculo_TIR(lfecha_calculo, lAFechas, lAValores, lguess, lnumreg) * n_dias_per_cre * 100;
                    // -- DBMS_OUTPUT.PUT_LINE("Tir: " || ltir);  
                };
            }
            else
            {
                ltir = null;
            };
        }

        private void CostoAmortizado(Int64? pnumero_radicacion, DateTime? pfecha, ref decimal? pTir, ref decimal? pValor_Presente, ref decimal? pSaldoTotal)
        {
            // lhistorico SYS_REFCURSOR;
            bool lexisteHistorico = false;
            // -- Variables para movimientos del crèdito
            int? lerror = null;
            decimal? ltot_capital = null;
            decimal?[] lcod_atributos = new decimal?[99];
            decimal?[] ltot_atributos = new decimal?[99];
            int lnum_atr = 0;
            DateTime? lprox_pago_aux = null;
            int lnum = 0;
            // -- Variables para el cálculo de la TIR
            // lcursor SYS_REFCURSOR;
            DateTime?[] lAFechas = new DateTime?[99];
            decimal?[] lAValores = new decimal?[99];
            decimal? lguess = null;
            int lnumreg = 0;
            DateTime? lfecha = null;
            decimal? lvalor = null;
            decimal? lvpn = null;
            int lverificar = 0;

            pTir = null;
            pValor_Presente = 0;
            pSaldoTotal = 0;
            lAFechas = new DateTime?[99];
            lAValores = new decimal?[99];
            lnumreg = 0;
            if (Cargar_Credito(Convert.ToInt64(pnumero_radicacion)))
            {
                // -- Cargando saldo vigente del crédito
                lnumreg = lnumreg + 1;
                Array.Resize(ref lAFechas, lAFechas.Length + 1);
                lAFechas[lnumreg] = pfecha;
                Array.Resize(ref lAValores, lAValores.Length + 1);
                lAValores[lnumreg] = -n_saldo;
                // -- Cargando los pagos del crédito
                lexisteHistorico = false;
                //OPEN lHistorico FOR SELECT p.fecha_cuota, SUM(p.valor) As valor FROM historico_amortiza p
                //  WHERE p.fecha_historico = pfecha AND p.numero_radicacion = pnumero_radicacion GROUP BY p.fecha_cuota ORDER BY 1;
                while (true)
                {
                    //FETCH LHistorico INTO lfecha, lvalor;
                    // EXIT WHEN lCursor % NOTFOUND;
                    lexisteHistorico = true;
                    lnumreg = lnumreg + 1;
                    Array.Resize(ref lAFechas, lAFechas.Length + 1);
                    lAFechas[lnumreg] = lfecha;
                    Array.Resize(ref lAValores, lAValores.Length + 1);
                    lAValores[lnumreg] = lvalor;
                };
                if (!lexisteHistorico)
                {
                    // -- Si no existe histórico de pagos entonces generarlo
                    b_detalle = true;
                    g_b_historico_amortiza = false;
                    gn_tipo_pago = 7;
                    lerror = Amortizar(Convert.ToInt32(gn_tipo_pago), 0, pfecha, n_num_cuotas, ref lprox_pago_aux, ref ltot_capital, ref lcod_atributos, ref ltot_atributos, ref lnum_atr);
                    if (lerror == 0)
                    {
                        if (b_detalle)
                        {
                            // DELETE FROM temp_pagar;
                            // -- Consolidando los valores
                            n_cont_rep = 1;
                            while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                            {
                                //INSERT INTO temp_pagar Values(pnumero_radicacion, cl_detalle_pago[n_cont_rep].n_cod_atr, cl_detalle_pago[n_cont_rep].n_num_cuota,
                                //      cl_detalle_pago[n_cont_rep].f_fecha_cuota, cl_detalle_pago[n_cont_rep].n_valor, cl_detalle_pago[n_cont_rep].n_valor, 0);
                                pSaldoTotal = pSaldoTotal + cl_detalle_pago[n_cont_rep].n_valor;
                                n_cont_rep = n_cont_rep + 1;
                            };
                            if (!(pSaldoTotal == null || pSaldoTotal == 0))
                            {
                                lAValores[lnumreg] = -pSaldoTotal;
                            };
                            // -- Generando la TIR
                            //OPEN lCursor FOR
                            //  SELECT p.fecha_cuota, SUM(p.valor) As valor FROM temp_pagar p
                            //    WHERE p.numero_radicacion = pnumero_radicacion GROUP BY p.fecha_cuota ORDER BY 1;
                            while (true)
                            {
                                //FETCH lCursor INTO lfecha, lvalor;
                                //EXIT WHEN lCursor % NOTFOUND;
                                lnumreg = lnumreg + 1;
                                Array.Resize(ref lAFechas, lAFechas.Length + 1);
                                lAFechas[lnumreg] = lfecha;
                                Array.Resize(ref lAFechas, lAValores.Length + 1);
                                lAValores[lnumreg] = lvalor;
                                // -- DBMS_OUTPUT.PUT_LINE("Fecha: " || lfecha || " Valor: " || lvalor);  
                            };
                        };
                    };
                };
                lguess = null;
                if (lnumreg > 0)
                {
                    // -- Cálculo de la TIR varias iteraciones
                    if (n_tir != null)
                    {
                        pTir = n_tir;
                    }
                    else
                    {
                        pTir = BOFunciones.Calculo_TIR(pfecha, lAFechas, lAValores, lguess, lnumreg) * n_dias_per_cre * 100;
                    };
                    // -- Calculo del valor presente neto
                    for (int i = 2; i <= lnumreg; i++)
                    {
                        if (lAFechas[i] > pfecha)
                        {
                            lvpn = BOFunciones.Round((lAValores[i] / BOFunciones.Power((1 + pTir / (n_dias_per_cre * 100)), Convert.ToDecimal((Convert.ToDateTime(lAFechas[i]) - Convert.ToDateTime(pfecha))))));
                        }
                        else
                        {
                            lvpn = lAValores[i];
                        };
                        pValor_Presente = pValor_Presente + lvpn;
                        // -- DBMS_OUTPUT.PUT_LINE("Fecha: "|| lAFechas(i) || " Valor: " || lAValores(i) || " Valor Presente: " || lvpn || " Tir: " ||pTir);  
                    };
                };
            }
            else
            {
                //raise_application_error(-20101, "No se encontro crèdito con los datos dados, radicaciòn: " || pnumero_radicacion);
                return;
            };
        }

        private void Nota_Debito(Int64 n_radic, Int64 n_cod_ope, DateTime? f_fecha_pago, decimal? rn_cod_atributo, decimal? rn_val_atributo, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            // -- Variables para determinar los valores a pagar
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atributos = new decimal?[99];
            decimal?[] rn_tot_atributos = new decimal?[99];
            int rn_num_atr = 0;
            // basedato SYS_REFCURSOR;
            int? n_cod_atr = 0;
            DateTime? f_fecha_cuota = null;
            decimal? n_valor = null;
            Int64 n_idamortiza = 0;
            DateTime? f_fecha_calculo = null;
            bool bCargado = false;
            Int64? n_num_tran = 0;

            bCargado = false;
            if (Cargar_Credito(Convert.ToInt64(n_radic)))
            {
                n_tipo = 2;
                if (f_prox_pag <= f_fecha_pago)
                {
                    f_fecha_calculo = f_fecha_pago;
                }
                else
                {
                    f_fecha_calculo = f_prox_pag;
                };
                n_error = Amortizar(n_tipo, 0, f_fecha_calculo, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atributos, ref rn_tot_atributos, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atributos.Length)
                {
                    n_interes = n_interes + rn_tot_atributos[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    // -- Registrando datos de auditoria
                    //Insert Into credito_aud
                    //Select sq_credito_aud.nextval, n_cod_ope, c.numero_radicacion, c.saldo_capital, c.otros_saldos, c.cuotas_pagadas, c.fecha_proximo_pago, c.fecha_ultimo_pago, c.estado, c.cuotas_pendientes
                    //From credito c Where c.numero_radicacion = n_radic;
                    // -- Para pago total guardar auditoria de saldos pendientes
                    //Insert Into Amortiza_cre_aud
                    //   Select sq_amortiza_cre_aud.nextval, n_cod_ope, a.numero_radicacion, a.cod_atr, a.fecha_cuota, a.valor, a.saldo, a.estado
                    //   From amortiza_cre a Where a.numero_radicacion = n_radic;
                    if (rn_cod_atributo == 1)
                    {
                        // Update credito Set saldo_capital = saldo_capital + rn_val_atributo Where numero_radicacion = n_radic;
                    }
                    else
                    {
                        // -- Si es un atributo diferente a capital
                        //Update atributoscredito Set atributoscredito.saldo_atributo = atributoscredito.saldo_atributo + rn_val_atributo Where atributoscredito.numero_radicacion = n_radic And atributoscredito.cod_atr = rn_cod_atributo;          
                    };
                    // -- Insertando la transacción
                    if (rn_cod_atributo != n_atr_APORTE)
                    {
                        while (true)
                        {
                            try
                            {
                                n_num_tran = 0;
                                // Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 8, Null, rn_cod_atributo, rn_val_atributo, 0, 0, 0, 1, Null)
                                // Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                            };
                        };
                    };
                    // -- Actualizar pendientes
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                        f_fecha_cuota = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                        n_valor = cl_detalle_pago[n_cont_rep].n_valor;
                        if (f_prox_pag == f_fecha_cuota)
                        {
                            // -- Actualizar amortiza_cre
                            //Open basedato For Select idamortiza From amortiza_cre where numero_radicacion = n_radic And cod_atr = n_cod_atr And fecha_cuota = f_fecha_cuota;
                            //Fetch basedato Into n_idamortiza;
                            if (n_idamortiza != null)
                            {
                                if (rn_cod_atributo == n_cod_atr)
                                {
                                    //Update amortiza_cre Set valor = valor + rn_val_atributo, saldo = saldo + rn_val_atributo
                                    //where idamortiza = n_idamortiza;
                                    bCargado = true;
                                };
                            }
                            else
                            {
                                if (rn_cod_atributo == n_cod_atr)
                                {
                                    n_valor = n_valor + rn_val_atributo;
                                    bCargado = true;
                                };
                                //Insert Into amortiza_cre(IDAMORTIZA, NUMERO_RADICACION, COD_ATR, FECHA_CUOTA, VALOR, SALDO, SALDO_BASE, TASA_BASE, DIAS_BASE, ESTADO)
                                //Values(0, n_radic, n_cod_atr, f_fecha_cuota, n_valor, n_valor, 0, 0, 0, 1);
                            };
                        };
                        n_cont_rep = n_cont_rep + 1;
                    };
                    if (!bCargado)
                    {
                        //Insert Into amortiza_cre(IDAMORTIZA, NUMERO_RADICACION, COD_ATR, FECHA_CUOTA, VALOR, SALDO, SALDO_BASE, TASA_BASE, DIAS_BASE, ESTADO)
                        //Values(0, n_radic, n_cod_atr, f_fecha_cuota, rn_val_atributo, rn_val_atributo, 0, 0, 0, 1);
                    };
                }
                else
                {
                    n_error = -2;
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private void Nota_Credito(Int64 n_radic, Int64 n_cod_ope, int? n_tipo_tran, DateTime? f_fecha_pago, decimal? rn_cod_atributo, decimal? rn_val_atributo, ref int? n_error)
        {
            int n_num = 0;
            decimal? n_interes = null;
            int n_tipo = 0;
            // -- Variables para determinar los valores a pagar
            DateTime? rf_f_prox_pago = null;
            decimal? rn_tot_capital = null;
            decimal?[] rn_cod_atr = new decimal?[99];
            decimal?[] rn_tot_atr = new decimal?[99];
            int rn_num_atr = 0;
            // -- Variables para aplicar nota
            // basedato SYS_REFCURSOR;
            decimal? n_cod_atr = null;
            DateTime? f_fecha_cuota = null;
            Int64? n_idamortiza = null;
            DateTime? f_fecha_calculo = null;
            Int64? n_num_tran = null;
            decimal? n_valor = null;
            decimal? n_saldo = null;
            bool bCargado = false;
            // -- Variables para la novedad
            Int64? n_nov_cre = null;
            DateTime? f_fec_act = null;
            DateTime? f_hor_act = null;
            decimal? n_pendiente_aplicar = null;
            int n_cont = 0;
            decimal? n_valor_aplicar = null;
            decimal? total_mora = null;

            bCargado = false;
            if (Cargar_Credito(n_radic))
            {
                n_tipo = 2;
                if (f_prox_pag <= f_fecha_pago || f_prox_pag == null)
                {
                    f_fecha_calculo = f_fecha_pago;
                }
                else
                {
                    f_fecha_calculo = f_prox_pag;
                };
                n_error = Amortizar(n_tipo, 0, f_fecha_calculo, 0, ref rf_f_prox_pago, ref rn_tot_capital, ref rn_cod_atr, ref rn_tot_atr, ref rn_num_atr);
                n_interes = 0;
                n_num = 1;
                while (n_num <= rn_num_atr && n_num <= rn_tot_atr.Length)
                {
                    n_interes = n_interes + rn_tot_atr[n_num];
                    n_num = n_num + 1;
                };
                if (n_error == 0)
                {
                    // -- Registrando datos de auditoria
                    //Insert Into credito_aud
                    //Select sq_credito_aud.nextval, n_cod_ope, c.numero_radicacion, c.saldo_capital, c.otros_saldos, c.cuotas_pagadas, c.fecha_proximo_pago, c.fecha_ultimo_pago, c.estado, c.cuotas_pendientes
                    //From credito c Where c.numero_radicacion = n_radic;
                    // -- Para pago total guardar auditoria de saldos pendientes
                    //Insert Into Amortiza_cre_aud
                    //Select sq_amortiza_cre_aud.nextval, n_cod_ope, a.numero_radicacion, a.cod_atr, a.fecha_cuota, a.valor, a.saldo, a.estado
                    //From amortiza_cre a Where a.numero_radicacion = n_radic;
                    // -- Actualizando el crédito
                    if (rn_cod_atributo == 1)
                    {
                        //Update Credito Set Credito.saldo_capital = Credito.saldo_capital - rn_val_atributo, Credito.fecha_ultimo_pago = f_fecha_pago
                        //Where Credito.numero_radicacion = n_radic;         
                    }
                    else
                    {
                        // -- Si es un atributo diferente a capital
                        //Update atributoscredito Set atributoscredito.saldo_atributo = atributoscredito.saldo_atributo - rn_val_atributo Where atributoscredito.numero_radicacion = n_radic And atributoscredito.cod_atr = rn_cod_atributo;  
                    };
                    // -- Insertando la transacción
                    if (rn_cod_atributo != n_atr_APORTE)
                    {
                        while (true)
                        {
                            try
                            {
                                n_num_tran = 0;
                                //Insert Into tran_cred Values(n_num_tran, n_cod_ope, n_radic, n_cod_cliente, s_cod_credi, 7, Null, rn_cod_atributo, rn_val_atributo, 0, 0, 0, 1, Null)
                                //Returning num_tran Into n_num_tran;
                                break;
                            }
                            catch
                            {
                                n_num_tran = Consecutivos("2");
                            };
                        };
                    };
                    // -- Actualizar saldos de moras
                    n_cont = 1;
                    while (n_cont < n_cont_mora)
                    {
                        cl_det_mora_cre[n_cont].n_saldo = cl_det_mora_cre[n_cont].n_valor;
                        n_cont = n_cont + 1;
                    };
                    // -- Actualizar pendientes
                    n_pendiente_aplicar = rn_val_atributo;
                    n_cont_rep = 1;
                    while (cl_detalle_pago[n_cont_rep].f_fecha_cuota != null)
                    {
                        n_cod_atr = cl_detalle_pago[n_cont_rep].n_cod_atr;
                        f_fecha_cuota = cl_detalle_pago[n_cont_rep].f_fecha_cuota;
                        n_valor = cl_detalle_pago[n_cont_rep].n_valor;
                        n_saldo = cl_detalle_pago[n_cont_rep].n_valor;
                        // -- Determinando valor del atributo luego de aplicar la nota crédito
                        n_valor_aplicar = 0;
                        if (rn_cod_atributo == n_cod_atr && n_pendiente_aplicar > 0)
                        {
                            if (n_valor >= n_pendiente_aplicar)
                            {
                                n_saldo = n_valor - n_pendiente_aplicar;
                                n_valor_aplicar = n_pendiente_aplicar;
                                n_pendiente_aplicar = 0;
                            }
                            else
                            {
                                n_saldo = 0;
                                n_valor_aplicar = n_valor;
                                n_pendiente_aplicar = n_pendiente_aplicar - n_valor;
                            };
                            bCargado = true;
                        };
                        // -- Actualizar amortiza_cre
                        //Open basedato For Select idamortiza From amortiza_cre where numero_radicacion = n_radic And cod_atr = n_cod_atr And fecha_cuota = f_fecha_cuota;
                        //Fetch basedato Into n_idamortiza;
                        if (n_idamortiza != null)
                        {
                            //Update amortiza_cre Set valor = n_valor, saldo = n_saldo where idamortiza = n_idamortiza and numero_radicacion = n_radic;
                        }
                        else
                        {
                            if (n_cod_atr == null)
                            {
                                n_cod_atr = rn_cod_atributo;
                            };
                            //Insert Into amortiza_cre(IDAMORTIZA, NUMERO_RADICACION, COD_ATR, FECHA_CUOTA, VALOR, SALDO, SALDO_BASE, TASA_BASE, DIAS_BASE, ESTADO)
                            //Values(0, n_radic, n_cod_atr, f_fecha_cuota, n_valor, n_saldo, 0, 0, 0, 1);
                        };
                        if (n_cod_atr == n_atr_mora)
                        {
                            Actualizar_Mora(BOFunciones.Trunc(f_fecha_cuota), ref n_valor_aplicar, n_saldo);
                        };
                        n_cont_rep = n_cont_rep + 1;
                    };
                    if (!bCargado)
                    {
                        if (f_fecha_cuota == null)
                        {
                            f_fecha_cuota = f_prox_pag;
                        };
                        //Insert Into amortiza_cre(IDAMORTIZA, NUMERO_RADICACION, COD_ATR, FECHA_CUOTA, VALOR, SALDO, SALDO_BASE, TASA_BASE, DIAS_BASE, ESTADO)
                        //Values(0, n_radic, rn_cod_atributo, f_fecha_cuota, rn_val_atributo, rn_val_atributo, 0, 0, 0, 1);
                    };
                    // -- Actualizar det_mora_cre   
                    // --n_pendiente_aplicar = rn_val_atributo;
                    // --If(rn_cod_atributo = n_atr_mora) ) { 
                    // --Delete From det_mora_cre where numero_radicacion = n_radic;
                    // --n_cont = 1;  
                    // --While n_cont<n_cont_mora ) {
                    // --  If (n_pendiente_aplicar != 0) ) { 
                    // --    If(cl_det_mora_cre(n_cont).n_saldo >= n_pendiente_aplicar) ) { 
                    // --      cl_det_mora_cre(n_cont).n_saldo = cl_det_mora_cre(n_cont).n_saldo - n_pendiente_aplicar;
                    // --      n_pendiente_aplicar = 0;
                    // --    } else { 
                    // --      n_pendiente_aplicar = n_pendiente_aplicar - cl_det_mora_cre(n_cont).n_saldo;
                    // --      cl_det_mora_cre(n_cont).n_saldo = 0;                  
                    // --    };
                    // --  };
                    // --  n_cont = n_cont + 1;  
                    // --};;  
                    // --n_cont = 1;  
                    // --While n_cont<n_cont_mora ) {
                    // --  cl_det_mora_cre(n_cont).n_tasa_int = Round(cl_det_mora_cre(n_cont).n_tasa_int*10000 )/10000; 
                    // --  Insert Into det_mora_cre values(0, n_radic, cl_det_mora_cre(n_cont).f_fecha_cuota, cl_det_mora_cre(n_cont).n_cod_atr,  
                    // --    cl_det_mora_cre(n_cont).n_valor, cl_det_mora_cre(n_cont).n_saldo, cl_det_mora_cre(n_cont).n_tasa_int, cl_det_mora_cre(n_cont).n_valor_base,  
                    // --    cl_det_mora_cre(n_cont).f_fecha_ini, cl_det_mora_cre(n_cont).f_fecha_fin, cl_det_mora_cre(n_cont).n_dias_mora, cl_det_mora_cre(n_cont).s_estado);  
                    // --  n_cont = n_cont + 1;  
                    // --};;  
                    // --};
                    bCargado = Grabar_Mora();
                    // -- Verificar la mora
                    if (rn_cod_atributo == n_atr_mora)
                    {
                        total_mora = 0;
                        //Select Sum(Saldo) Into total_mora From amortiza_cre Where numero_radicacion = n_radic And cod_atr = n_atr_mora;
                        if (total_mora == 0)
                        {
                            //Update det_mora_cre Set saldo = 0 Where numero_radicacion = n_radic;
                        };
                    };
                    // -- Determina la terminación de credito, actualiza el estado y inserta la novedad de terminación
                    if (rn_cod_atributo == 1)
                    {
                        if (n_saldo == rn_val_atributo)
                        {
                            // -- Actualiza estado del crédito depende si es servicio o credito
                            // Update credito Set credito.estado = "T" Where credito.numero_radicacion = n_radic;
                            // -- Crea novedad de terminacion
                            n_nov_cre = Consecutivos("4");
                            f_fec_act = DateTime.Now;
                            f_hor_act = DateTime.Now;
                            // Insert Into novedad_cre Values(n_nov_cre, 302, 1, n_radic, f_fec_act, f_hor_act, " ", "1");
                        };
                    };
                };
            }
            else
            {
                n_error = -1;
            };
        }

        private Int64? Consecutivos(string pTabla)
        {
            Int64? lconsecutivo;

            lconsecutivo = 0;
            if (pTabla == "1")
            {
                //SELECT sq_amortiza_cre.nextval INTO lconsecutivo FROM dual;
            };
            if (pTabla == "2")
            {
                //SELECT sq_tran_cred.nextval INTO lconsecutivo FROM dual;
            };
            if (pTabla == "3")
            {
                //SELECT sq_det_tran_cred.nextval INTO lconsecutivo FROM dual;
            };
            if (pTabla == "4")
            {
                //SELECT sq_novedad_cre.nextval INTO lconsecutivo FROM dual;
            };
            if (pTabla == "5")
            {
                //SELECT sq_operacion.nextval INTO lconsecutivo FROM dual;
            };
            if (pTabla == "6")
            {
                //SELECT sq_historico_cre.nextval INTO lconsecutivo FROM dual;
            };
            return lconsecutivo;
        }

        private void CruceGenerar(Int64? pnumero_radicacion, DateTime? pfecha, ref decimal? ptotal_a_pagar, ref decimal? pcapital, ref decimal? pintcte, ref decimal? pintmora, ref decimal? potros, ref decimal? ptotal, int? ptipo)
        {
            int? lerror = 0;

            pcapital = 0;
            pintcte = 0;
            pintmora = 0;
            potros = 0;
            ptotal = 0;
            if (ptipo == 1)
            {
                //-- Calcular valor de los créditos
                Calcular_TotalDet(pnumero_radicacion, pfecha, ref ptotal_a_pagar, ref lerror);

                DApackage.ConsultarValorPagarCredito(pnumero_radicacion, ref pcapital, ref pintcte, ref pintmora, ref potros, ref ptotal, usuario);

                //CALCULAR_TOTALDET(pnumero_radicacion, pFecha, ptotal, lerror);
                //Select Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) capital,
                //        Sum(Case ac.cod_atr When 2 Then ac.valor Else 0 End) intcte,
                //        Sum(Case ac.cod_atr When 3 Then ac.valor Else 0 End) intmora,
                //        Sum(Case ac.cod_atr When 1  Then 0  When 2  Then 0  When 3  Then 0 Else ac.valor End) otros,
                //        Sum(ac.valor) total
                //        Into pcapital, pintcte, pintmora, potros, ptotal
                //        from temp_pagar ac
                //        where numero_radicacion = pnumero_radicacion;
            }
            else
            {
                //lerror = 2;

                Calcular_PorValorDet(pnumero_radicacion, pfecha, ptotal_a_pagar, ref lerror);
                //CLSCREDITO.CALCULAR_PORVALORDET(pnumero_radicacion, pFecha, ptotal_a_pagar, lerror);

                DApackage.ConsultarValorPagarCredito(pnumero_radicacion, ref pcapital, ref pintcte, ref pintmora, ref potros, ref ptotal, usuario);

                //Select Sum(Case ac.cod_atr When 1 Then ac.valor Else 0 End) capital,
                //        Sum(Case ac.cod_atr When 2 Then ac.valor Else 0 End) intcte,
                //        Sum(Case ac.cod_atr When 3 Then ac.valor Else 0 End) intmora,
                //        Sum(Case ac.cod_atr When 1  Then 0  When 2  Then 0  When 3  Then 0 Else ac.valor End) otros,
                //        Sum(ac.valor) total
                //        Into pcapital, pintcte, pintmora, potros, ptotal
                //        from temp_pagar ac
                //        where numero_radicacion = pnumero_radicacion;
            };
        }



    }

}
