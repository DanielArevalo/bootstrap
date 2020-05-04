using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Package.Data;
using Xpinn.Package.Entities;
using Xpinn.Util;

namespace Xpinn.Package.Business
{
    public class funciones
    {
        private packageData DApackage;
        Usuario usuario = new Usuario();

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public funciones(Usuario pUsuario)
        {
            DApackage = new packageData();
            usuario = pUsuario;
        }

        public string AjustarTexto(string lTexto2, string lCaracter, int? lLongitud, bool lOrientacion)
        {
            int? lLargo;
            string lTexto;
            if (lTexto2 == null)
                lTexto = " ";
            else
                lTexto = lTexto2;
            lLargo = lTexto.Length;
            if (lLargo > lLongitud)
            {
                if (lOrientacion)
                    return lTexto.Substring(Convert.ToInt32(lLargo - lLongitud + 1), Convert.ToInt32(lLongitud));
                else
                    return lTexto.Substring(0, Convert.ToInt32(lLongitud));
            }
            else
            {
                if (lOrientacion == true)
                    return LPad(lTexto, lLongitud, lCaracter);
                else
                    return RPad(lTexto, lLongitud, lCaracter);
            }
        }

        public int? AtrAporte()
        {
            try
            {
                return To_Number(BuscarGeneral(250, 1)); ;
            }
            catch
            {
                return null;
            }
        }

        public int? AtrComision()
        {
            try
            {
                return To_Number(BuscarGeneral(201, 1)); ;
            }
            catch
            {
                return null;
            }
        }

        public int? AtrCorriente()
        {
            string SATRCTE;
            int? NATRCTE;
            try
            {
                SATRCTE = BuscarGeneral(200, 1);
                NATRCTE = To_Number(SATRCTE);
                return NATRCTE;
            }
            catch
            {
                return 2;
            }
        }

        public int? AtrGarCom()
        {
            clscredito clsCredito = new clscredito();
            return clsCredito.n_atr_GARCOM;
        }

        public int? AtrIvaLeyMiPyme()
        {
            clscredito clsCredito = new clscredito();
            return clsCredito.n_atr_IVALeyMiPyme;
        }

        public int? AtrLeyMiPyme()
        {
            clscredito clsCredito = new clscredito();
            return clsCredito.n_atr_LeyMiPyme;
        }

        public int? AtrMora()
        {
            string SATRMORA;
            int? NATRMORA;
            try
            {
                NATRMORA = null;
                SATRMORA = BuscarGeneral(230, 1);
                if (SATRMORA == "")
                    SATRMORA = "3";
                NATRMORA = To_Number(SATRMORA);
                return NATRMORA;
            }
            catch
            {
                return 3;
            }
        }

        public int? AtrSeguro()
        {
            try
            {
                return To_Number(BuscarGeneral(202, 1));
            }
            catch
            {
                return null;
            }
        }

        public int BooleanToNumber(bool pDato)
        {
            if (pDato)
                return 1;
            else
                return 0;
        }

        public string BuscarCuenta(int? PCODIGO)
        {
            string lcod_cuenta = DApackage.BuscarCuenta(PCODIGO, usuario);
            return lcod_cuenta;
        }

        public string BuscarGeneral(int? pn_cod_parametro, int? pn_tipo_dato)
        {
            Configuracion conf = new Configuracion();
            string sValor = "";
            try
            {
                sValor = DApackage.ConGeneral(pn_cod_parametro, usuario);
                if (pn_tipo_dato == 1)
                    return sValor;
                else if (pn_tipo_dato == 2)
                    if (StrIsValidNumber(sValor))
                        return To_Number(sValor).ToString();
                    else
                        return null;
                else if (pn_tipo_dato == 3)
                    return Convert.ToDateTime(To_Date(sValor)).ToString(conf.ObtenerFormatoFecha());
                else
                    return sValor;
            }
            catch
            {
                return null;
            }

        }

        public bool CalIntAtr(string ptip_cal, ref decimal? rtasa, ref int? rtip_tas, int? ptip_his, decimal? pdesv, int? pper_pro, DateTime? pf_fec_apro)
        {
            decimal? num_dias;
            DateTime? f_fec_ini;
            try
            {
                if (ptip_cal == "1")                            // Tasa Fija
                {
                    return true;
                }
                else if (ptip_cal == "2")                       // Tasa Ponderada
                {
                    return true;
                }
                else if (ptip_cal == "3" || ptip_cal == "5")   // Tasa Histórica
                {
                    if (!DApackage.ConHistoricoTasa(pf_fec_apro, ref rtip_tas, ref rtasa, usuario))
                    {
                        rtip_tas = DApackage.ConTasaHist(ptip_his, usuario);
                    }
                    rtasa = NVL(rtasa, 0) + NVL(pdesv, 0);
                }
                else if (ptip_cal == "4")                       // Promedio histórico
                {
                    num_dias = DApackage.ConPeriodicidadNumDia(pper_pro, usuario);
                    f_fec_ini = Convert.ToDateTime(pf_fec_apro).AddDays(-Convert.ToDouble(num_dias));
                    DApackage.ConTipoTasaDelHist(f_fec_ini, pf_fec_apro, ref rtip_tas, ref rtasa, usuario);
                    rtasa = rtasa + pdesv;
                }
                else
                {
                    return false;
                }
                if (rtasa == null || rtasa < 0 || rtip_tas == null || rtip_tas < 0)
                    return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public decimal? CalTasMod(
          decimal? ptasa,
          // tipo:   1:Efectiva,  2:Nominal
          int? tipo,
          decimal? per_tip,
          // mod:   1:Anticipada,  2:Vencida
          int? nmod,
          decimal? per_mod,
          // nue_tip:   1:Efectiva,  2:Nominal
          int? nue_tipo,
          decimal? nue_per_tip,
          // nue_mod:   1:Anticipada,  2:Vencida
          int? nue_mod,
          decimal? nue_per_mod,
          // Determina el tipo de producto
          int? tipo_prod)
        {
            decimal? dias;
            decimal? nue_dias;
            bool b_ex_mod;
            string mod_cal;
            decimal? tasa;

            tasa = ptasa;
            //  verificar si se cobra interes simple o compuesto
            mod_cal = "";
            b_ex_mod = false;
            mod_cal = DApackage.ConGeneral(450, usuario);
            if (mod_cal.Trim() == "")
                b_ex_mod = true;
            if (!b_ex_mod)
                mod_cal = "2";

            // Si la tasa es nominal la convierte a efectiva
            if (tipo == 2)
                tasa = CalTasNomEfe(tasa, Convert.ToInt32(per_tip), Convert.ToInt32(per_mod), 0);

            // Si la tasa esta anticipada la pasa a vencida
            if (nmod == 1)
                tasa = CalTasVen(tasa);
            tasa = tasa / 100;

            dias = DApackage.ConPeriodicidadNumDia(Convert.ToInt32(per_mod), usuario);
            if (dias == 0 || dias == null)
            {
                dias = 0;
                return 0;
            }
            nue_dias = DApackage.ConPeriodicidadNumDia(Convert.ToInt32(nue_per_mod), usuario);
            if (nue_dias == 0 || nue_dias == null)
                nue_dias = 0;
            //  verificar si se cobra interes simple o compuesto
            if (tipo_prod == 0)
            {
                if (mod_cal == "2")
                {
                    tasa = tasa + 1;
                    if (tasa >= 0)
                        tasa = Power(tasa, nue_dias / dias);
                    else
                        tasa = 0;
                    tasa = tasa - 1;
                }
                else
                {
                    tasa = (tasa / dias) * nue_dias;
                }
            }
            else
            {
                tasa = tasa + 1;
                tasa = Power(tasa, nue_dias / dias);
                tasa = tasa - 1;
            }
            tasa = tasa * 100;
            if (nue_mod == 1)
                tasa = CalTasAnt(tasa);
            if (nue_tipo == 2)
                tasa = CalTasEfeNom(tasa, Convert.ToInt32(nue_per_tip), Convert.ToInt32(nue_per_mod), 0);
            return tasa;
        }

        public decimal? CalTasNomEfe(decimal? ptasa, int? per_cap, int? per_pago, decimal? plazo)
        {
            decimal? tasa;
            decimal? per_ano_cap;
            decimal? per_ano_pago;
            tasa = ptasa;
            per_ano_cap = 0;
            per_ano_pago = 0;
            per_ano_cap = DApackage.ConPeriodicidadPerAnu(per_cap, usuario);
            per_ano_pago = DApackage.ConPeriodicidadPerAnu(per_pago, usuario);
            if (per_ano_cap != 0 && per_ano_cap != null && per_ano_pago != 0 && per_ano_pago != null)
                tasa = tasa * per_ano_cap / per_ano_pago;
            else
                tasa = 0;
            return tasa;
        }

        public decimal? CalTasEfeNom(decimal? ptasa, int? per_cap, int? per_pago, decimal? plazo)
        {
            decimal? tasa;
            decimal? per_ano_cap;
            decimal? per_ano_pago;
            tasa = ptasa;
            per_ano_cap = 0;
            per_ano_pago = 0;
            per_ano_cap = DApackage.ConPeriodicidadPerAnu(per_cap, usuario);
            per_ano_pago = DApackage.ConPeriodicidadPerAnu(per_pago, usuario);
            if (per_ano_cap != 0 && per_ano_cap != null && per_ano_pago != 0 && per_ano_pago != null)
                tasa = tasa * per_ano_pago / per_ano_cap;
            else
                tasa = 0;
            return tasa;
        }

        public decimal? CalTasVen(decimal? ptasa)
        {
            decimal? tasa;
            tasa = ptasa;
            tasa = tasa / 100;
            if (tasa != 1)
                tasa = tasa / (1 - tasa);
            else
                tasa = 0;
            tasa = tasa * 100;
            return tasa;
        }

        public decimal? CalTasAnt(decimal? ptasa)
        {
            decimal? tasa;
            tasa = ptasa;
            tasa = tasa / 100;
            if (tasa != -1)
                tasa = tasa / (1 + tasa);
            else
                tasa = 0;
            tasa = tasa * 100;
            return tasa;
        }

        public bool CalValDes(int? ptip_des, decimal? pvalor, int? ptip_liq, decimal? pmonto, decimal? pplazo, int? pnum_cuo, ref decimal? rval_cal, decimal? psaldo, decimal? pinteres, decimal? pvalorCatg, decimal? pvalfactor, decimal? pvrComer, decimal? pcuota, int? pnumCodeu)
        {
            string val_aux;
            string sval;
            decimal? val;
            decimal? auxpvalfactor;
            decimal? lfactor;

            if (ptip_des == 0)
                rval_cal = pvalor;
            else if (ptip_des == 1)
                rval_cal = pvalor;
            else if (ptip_des == 2 || ptip_des == 3 || ptip_des == 4)
            {
                // Si es porcentaje entonces convertir a valor
                lfactor = pvalor;
                if (ptip_des == 3)
                    lfactor = pvalor / 100;
                // Factor aplicado de acuerdo al tipo de liquidación
                if (ptip_liq == 0)
                    rval_cal = lfactor;
                else if (ptip_liq == 1)
                    rval_cal = lfactor * (pplazo + 1) * pmonto;
                else if (ptip_liq == 2)
                    rval_cal = lfactor * pmonto;
                else if (ptip_liq == 3)
                    rval_cal = lfactor * pplazo * pmonto;
                else if (ptip_liq == 4)
                    rval_cal = lfactor * Round(lfactor + 1, Convert.ToInt32(pnum_cuo));
                else if (ptip_liq == 5)
                {
                    val_aux = "";
                    val_aux = DApackage.ConGeneral(440, usuario);
                    if (StrIsValidNumber(val_aux))
                    {
                        rval_cal = lfactor * (pmonto - To_Number(val_aux));
                        if (rval_cal < 0)
                            rval_cal = 0;
                    }
                }
                else if (ptip_liq == 6)
                    rval_cal = lfactor * psaldo;
                else if (ptip_liq == 7)
                    rval_cal = lfactor * (psaldo * (pinteres / 100));
                else if (ptip_liq == 8)
                    rval_cal = pvalorCatg * pmonto;
                else if (ptip_liq == 9)
                {
                    sval = "";
                    sval = DApackage.ConGeneral(15, usuario);
                    val = To_Number(sval);
                    auxpvalfactor = pvalfactor / 100;
                    rval_cal = ((auxpvalfactor * pvrComer) + (auxpvalfactor * pvrComer) * val / 100) / 12;
                }
                else if (ptip_liq == 10)
                    rval_cal = ((pcuota * pplazo) + rval_cal) * lfactor;
                else if (ptip_liq == 11)
                    rval_cal = lfactor * pvrComer;
                else if (ptip_liq == 12)
                {
                    sval = "";
                    sval = DApackage.ConGeneral(15, usuario);
                    val = To_Number(sval);
                    rval_cal = (val * pvrComer) / 100;
                }
                else if (ptip_liq == 13)
                    rval_cal = lfactor * pplazo * psaldo;
                else if (ptip_liq == 14)
                    rval_cal = lfactor * pcuota;
                else if (ptip_liq == 15)
                {
                    sval = "";
                    sval = DApackage.ConGeneral(15, usuario);
                    val = To_Number(sval) / 100;
                    rval_cal = (pcuota - (pvrComer / lfactor)) * val;
                }
                else if (ptip_liq == 16)
                    rval_cal = lfactor * pvrComer;
                else if (ptip_liq == 17)
                    rval_cal = lfactor * psaldo * (pnumCodeu + 1);
                else if (ptip_liq == 18)
                    rval_cal = lfactor * (pnumCodeu + 1) * pmonto;
                else if (ptip_liq == 19)
                    rval_cal = lfactor * pplazo;
                else if (ptip_liq == 20)
                    rval_cal = lfactor / 1000000 * pmonto * pplazo;
                else if (ptip_liq == 21)
                    rval_cal = lfactor / 12 * pmonto * pplazo;
                else if (ptip_liq == 22)
                    rval_cal = 0;
                else
                    return false;
            }
            else
                return false;
            rval_cal = Round(rval_cal);
            if (rval_cal == null)
                return false;
            return true;
        }

        public decimal? CalNumCuotas(decimal? pplazo, int? pfor_pla, int? pperiodic)
        {
            decimal? diapla;
            decimal? diaper;
            decimal? num_cuo;
            diapla = 0;
            diaper = 0;
            diapla = DApackage.ConPeriodicidadNumDia(pfor_pla, usuario);
            diaper = DApackage.ConPeriodicidadNumDia(pperiodic, usuario);
            if (diapla == null || diapla <= 0 || diaper == null || diaper <= 0)
                // 'Existe un error en la periodicidad del crédito o en el formato del plazo, el número de cuotas no pudo ser calculado';
                num_cuo = 0;
            else
                num_cuo = Round(diapla * pplazo / diaper);
            return num_cuo;
        }

        public decimal? Calculo_TIR(DateTime? pFechaCalculo, DateTime?[] pAFechas, decimal?[] pAValores, decimal? p_guess = 0, int pNumReg = 1)
        {
            int z = 0;
            int step_limit = 0;
            decimal? valor_presente = null;
            decimal? vpn = null;
            decimal? step = 1/1000;
            decimal? tir = 1/1000;
            DateTime? l_maxfecha = null;
            DateTime? l_minfecha = null;
            decimal? srok = null;
            decimal? dias = null;
            decimal? total_dias = null;
            int i = 0;
            
            // DBMS_OUTPUT.ENABLE(buffer_size => NULL);
            // --Determinar fechas mínima y máxima del flujo para efectos de control
            l_maxfecha = pAFechas[1];
            l_minfecha = pAFechas[1];
            i = 1;
            while (i <= pNumReg) {
                if (pAFechas[i] > l_maxfecha) {
                    l_maxfecha = pAFechas[i];
                };
                if (pAFechas[i] < l_minfecha) {
                    l_minfecha = pAFechas[i];
                };
            i = i + 1;
            };
            //SELECT MONTHS_BETWEEN(l_maxfecha, l_minfecha)
            // INTO srok
            // FROM DUAL;
            srok = Math.Abs((Convert.ToDateTime(l_maxfecha).Month - Convert.ToDateTime(l_minfecha).Month) + 12 * (Convert.ToDateTime(l_maxfecha).Year - Convert.ToDateTime(l_minfecha).Year));
            //--Calculando la TIR por varias iteraciones
            while (true) {
                //--Calculando la suma de los valores presentes
                total_dias = 0;
                valor_presente = 0;
                i = 1;
                while (i <= pNumReg) {
                    dias = Convert.ToDecimal(Convert.ToDateTime(pAFechas[i]) - pFechaCalculo);
                    if (dias > 0) {
                        vpn = Round((pAValores[i] / Power((1 + tir), (dias))));
                    } else {
                        dias = 0;
                        vpn = pAValores[i];
                    };
                total_dias = total_dias + dias;
                valor_presente = valor_presente + vpn;
                    //--DBMS_OUTPUT.PUT_LINE('Dias:' || dias || ' Valor Presente:' || vpn);
                    i = i + 1;
                };
                if (total_dias <= 0) {
                    //-- DBMS_OUTPUT.PUT_LINE('Valor Presente:' || valor_presente);
                    return 0;
                };
                // --DBMS_OUTPUT.PUT_LINE('Valor Presente:' || valor_presente || ' TIR:' || tir);
                // --Si los valores presentes netos no suman cero va a la siguiente iteración
                if ((valor_presente > 0) && (z == 0)) {
                    step = step / 2;
                    z = 1;
                };
                // --Si los valores presentens netos son negativos
                if ((valor_presente < 0) && (z == 1)) {
                    step = step / 2;
                    z = 0;
                };
                // --Pasa a la siguiente iteración aumento el valor de la tasa en el siguiente paso
                if (z == 0) {
                    tir = tir - step;
                } else {
                    tir = tir + step;
                };
                step_limit = step_limit + 1;
                // EXIT WHEN((ROUND(valor_presente * 100000) = 0) OR(step_limit = 10000));
            };
            // --DBMS_OUTPUT.PUT_LINE(' TIR:' || Round(tir, 6));
            return Round(tir, 6);            
        }

        public int? CodPeriodicidadDiaria(int? pTipoCal)
        {
            int? lper_dia;
            lper_dia = DApackage.ConPeriodicidadDiaria(pTipoCal, usuario);
            return lper_dia;
        }

        public Int32? FecDifDia(DateTime? fecha_ini, DateTime? fecha_fin, int tipo_cal)
        {
            // Inicializando las variables
            Int32 dias = 0;
            Int32 ano_ini = 0;
            Int32 ano_fin = 0;
            Int32 mes_ini = 0;
            Int32 mes_fin = 0;
            Int32 dia_ini = 0;
            Int32 dia_fin = 0;
            // Verificando datos nulos
            if (fecha_ini == null || fecha_fin == null)
                return null;
            // Verificando que la fecha inicial no sea superior a la fecha final
            if (fecha_ini > fecha_fin)
                return null;
            // Calcular la diferencia según el tipo de calendario
            if (tipo_cal == 1)              // Días comerciales
            {
                ano_ini = Convert.ToDateTime(fecha_ini).Year;
                ano_fin = Convert.ToDateTime(fecha_fin).Year;
                mes_ini = Convert.ToDateTime(fecha_ini).Month;
                mes_fin = Convert.ToDateTime(fecha_fin).Month;
                dia_ini = Convert.ToDateTime(fecha_ini).Day;
                if (dia_ini > 30 || (dia_ini >= 28 && mes_ini == 2))
                    dia_ini = 30;
                dia_fin = Convert.ToDateTime(fecha_fin).Day;
                if (dia_fin > 30 || (dia_fin >= 28 && mes_fin == 2))
                {
                    dia_fin = 30;
                    if (dia_ini >= 28 && (mes_ini == 2 || mes_fin == 2))
                        dia_ini = 30;
                }
                dias = (ano_fin - ano_ini) * 360;
                dias = dias + (mes_fin - mes_ini) * 30;
                dias = dias + (dia_fin - dia_ini);
            }
            else if (tipo_cal == 2)         // Días calendario
                dias = Convert.ToInt32(fecha_fin - fecha_ini);
            else
                return null;

            return dias;
        }

        public DateTime? FecSumDia(DateTime? fecha, int dias, int tipo_cal)
        {
            int dato = 0;
            if (fecha == null)
                return fecha;
            if (tipo_cal == 1)
            {
                int año_fec = Convert.ToDateTime(fecha).Year;
                int mes_fec = Convert.ToDateTime(fecha).Month;
                int dia_fec = Convert.ToDateTime(fecha).Day;
                int dias_febrero = 28;
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                if (dia_fec > 30 || (mes_fec == 2 && dia_fec >= dias_febrero))
                    dia_fec = 30;
                dato = (dias / 360);
                if (dato > 1)
                {
                    año_fec = año_fec + dato;
                    dias = dias - (360 * dato);
                }
                dato = (dias / 30);
                if (dato >= 1)
                {
                    mes_fec = mes_fec + dato;
                    if (mes_fec > 12)
                    {
                        año_fec = año_fec + 1;
                        mes_fec = mes_fec - 12;
                    }
                    dias = dias - (30 * dato);
                }
                if (dias > 0)
                {
                    if (30 - dia_fec < dias)
                    {
                        mes_fec = mes_fec + 1;
                        if (mes_fec > 12)
                        {
                            año_fec = año_fec + 1;
                            mes_fec = 1;
                        }
                        dia_fec = dias - (30 - dia_fec);
                    }
                    else
                    {
                        dia_fec = dia_fec + dias;
                    }
                }
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                if (mes_fec == 2 && (dia_fec > 30 && dia_fec <= 59))
                    dia_fec = dia_fec - (30 - dias_febrero);
                if (mes_fec == 4 || mes_fec == 6 || mes_fec == 9 || mes_fec == 11)
                    if (dia_fec == 31)
                        dia_fec = 30;
                // Validar mes de febrero
                if (año_fec % 4 == 0)
                    dias_febrero = 29;
                else
                    dias_febrero = 28;
                if (mes_fec == 2 && (dia_fec > dias_febrero && dia_fec <= 30))
                    dia_fec = dias_febrero;
                // Crear la fecha
                fecha = new DateTime(año_fec, mes_fec, dia_fec, 0, 0, 0);
            }
            else
            {
                fecha = Convert.ToDateTime(fecha).AddDays(dias);
            }
            return fecha;
        }

        public DateTime? FecResDia(DateTime? pf_fecha,  int? pn_dias, int n_tipo_cal)
        { 
            DateTime? f_fecha;
            int? n_dias;
            int? n_ano_fec;
            int? n_mes_fec;
            int? n_dia_fec;
            int? n_dato;

            n_dias = pn_dias;
            f_fecha = pf_fecha;
            if (f_fecha == null)
            {
                //DBMS_OUTPUT.PUT_LINE('La fecha se encuentra errada, verifique información (FecResDia)');
                return f_fecha;
            }
            if (n_tipo_cal == 1)
            {
                n_ano_fec = DateYear(f_fecha);
                n_mes_fec = DateMonth(f_fecha);
                n_dia_fec = DateDay(f_fecha);
                if (n_dia_fec > 30 || (n_mes_fec == 2 && n_dia_fec >= 28))
                    n_dia_fec = 30;
                n_dato = Trunc(n_dias / 360);
                if (n_dato > 1)
                {
                    n_ano_fec = n_ano_fec - n_dato;
                    n_dias = n_dias - (360 * n_dato);
                }
                n_dato = Trunc(n_dias / 30);
                if (n_dato > 1)
                {
                    n_mes_fec = n_mes_fec - n_dato;
                    if (n_mes_fec < 1)
                    {
                        n_ano_fec = n_ano_fec - 1;
                        n_mes_fec = 12 + n_mes_fec;
                    }
                    n_dias= n_dias - (30 * n_dato);
                }
                if (n_dias > 0)
                {
                    if (n_dia_fec > n_dias)
                    {
                        n_dia_fec = n_dia_fec - n_dias;
                    }
                    else
                    {
                        n_mes_fec = n_mes_fec - 1;
                        if (n_mes_fec < 1)
                        {
                            n_ano_fec = n_ano_fec - 1;
                            n_mes_fec = 12;
                        }
                        n_dia_fec = 30 - (n_dias - n_dia_fec);
                    }
                }
                if (n_mes_fec == 2 && n_dia_fec > 28)
                    n_dia_fec= 28;
                f_fecha= DateConstruct(n_ano_fec, n_mes_fec, n_dia_fec, 0, 0, 0);
            }
            else if (n_tipo_cal == 2)
            {
                f_fecha= Convert.ToDateTime(f_fecha).AddDays(-Convert.ToInt32(n_dias));
            }
            else
            { 
                //DBMS_OUTPUT.PUT_LINE('El tipo de calendario esta errado, debe ser 1:Comercial,  2:Calendario');
            }
            return f_fecha;
        }

        public bool FecIniCre(DateTime? pfecha_desembolso, int? lcod_periodicidad_cre, int? pforma_pago, int? ptipo_pago_int, int? ptipo_pago_intant, Int64? pcod_empresa, string pcod_linea_credito, ref DateTime? pfecha_inicio, ref decimal? n_dias_aju, ref string pError)
        {
            decimal? n_dias_per = null;
            int? n_tip_cal = null;
            // -- Datos de la empresa recaudadora
            Int64? lcod_empresa = null;
            decimal? ldias_novedad = 0;
            int? lcod_periodicidad = null;
            decimal? n_dia_per = null;
            string s_tipo_ini = null;
            decimal? n_dias_per_lis = null;
            // -- Variables para calculos
            DateTime? lfecha_inicio_cre;
            DateTime? lfecha_inicio_ant;
            string s_pag;
            bool exis_pag;
            int? n_tipo_lista = null;
            DateTime? f_fecha_lista;
            DateTime? f_fecha_pago_cre;

            pError = "";
            // -- Determinar si existe la empresa recaudadora para programar pagos por ventanilla
            exis_pag = false;
            s_pag = DApackage.ConGeneral(980, usuario);
            if (s_pag.Trim() != "")
                exis_pag = true;
            if (exis_pag && StrIsValidNumber(s_pag))
                lcod_empresa = To_Number(s_pag);
            // -- Determinar el número de dias y tipo de calendario según la periodicidad del crédito
            DApackage.ConsultaPeriodicidad(lcod_periodicidad_cre, ref n_dias_per, ref n_tip_cal, usuario);
            if (n_dias_per == null || n_dias_per <= 0)
            {
                pError = "La periodicidad del crédito se encuentra errada";
                return false;
            }
            // -- Calcular fecha de inicio y días de ajuste según programación de la empresa de recaudo
            if (pforma_pago == 2 || (pforma_pago == 1 && exis_pag))
            {
                //-- Determinar datos de la pagaduria para inicio de créditos
                if (!DApackage.ConsultaEmpresa(pcod_empresa, pcod_linea_credito, ref ldias_novedad, ref lcod_periodicidad, ref n_dia_per, ref pfecha_inicio, ref s_tipo_ini, ref lcod_empresa, ref n_tipo_lista, usuario))
                {
                    pError = "El crédito no posee empresa o pagaduría definida o no existe un tipo de lista sobre el cual se defina el concepto de descuento";
                    return false;
                }
                // -- Determinar los días de la periodicidad de descuento de la empresa de recaudo
                n_dias_per_lis = DApackage.ConPeriodicidadNumDia(lcod_periodicidad, usuario);
                // -- Determinar la próxima fecha según la fecha de inicio de la empresa y los dias de novedad
                if (DateYear(Trunc(pfecha_desembolso)) - 1 >= DateYear(Trunc(pfecha_inicio)))
                    pfecha_inicio = DateConstruct(DateYear(Trunc(pfecha_desembolso)) - 1, DateMonth(pfecha_inicio), DateDay(pfecha_inicio), 1, 1, 1);
                else
                    pfecha_inicio = DateConstruct(DateYear(Trunc(pfecha_inicio)) - 1, DateMonth(pfecha_inicio), DateDay(pfecha_inicio), 1, 1, 1);
                lfecha_inicio_ant = pfecha_inicio;
                while (pfecha_inicio < FecSumDia(Trunc(pfecha_desembolso), Convert.ToInt32(ldias_novedad), Convert.ToInt32(n_tip_cal)))
                {
                    lfecha_inicio_ant = pfecha_inicio;
                    pfecha_inicio = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per_lis), Convert.ToInt32(n_tip_cal));
                }
                // -- Verificar si para la fecha ya estan generadas las novedades
                f_fecha_lista = null;
                f_fecha_pago_cre = null;
                f_fecha_lista = DApackage.ConsultaPeriodo(n_tipo_lista, usuario);
                if (f_fecha_lista != null)
                {
                    f_fecha_pago_cre = FecSumDia(lfecha_inicio_ant, Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tip_cal));
                    if (f_fecha_pago_cre <= f_fecha_lista)
                    {
                        pfecha_inicio = lfecha_inicio_ant;
                        while (f_fecha_pago_cre <= f_fecha_lista)
                        {
                            pfecha_inicio = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per_lis), Convert.ToInt32(n_tip_cal));
                            f_fecha_pago_cre = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tip_cal));
                        }
                        lfecha_inicio_ant = pfecha_inicio;
                    }
                }
                // -- Según el tipo de pago calcular los días de ajuste
                if (ptipo_pago_int == 1)
                {
                    n_dias_aju = FecDifDia(Trunc(pfecha_desembolso), pfecha_inicio, Convert.ToInt32(n_tip_cal));
                }
                else if (ptipo_pago_int == 2)
                {
                    lfecha_inicio_cre = FecSumDia(Trunc(pfecha_desembolso), Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tip_cal));
                    while (pfecha_inicio < lfecha_inicio_cre)
                    {
                        lfecha_inicio_ant = pfecha_inicio;
                        pfecha_inicio = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per_lis), Convert.ToInt32(n_tip_cal));
                    }
                    n_dias_aju = Convert.ToInt32(lfecha_inicio_ant - Trunc(pfecha_desembolso));
                }
                else
                {
                    pError = "El tipo de pago de interes se encuentra errado, debe ser Anticipado o Vencido";
                    return false;
                }
                pfecha_inicio = lfecha_inicio_ant;
            }
            else if (pforma_pago == 1 && exis_pag == true)
            {
                // -- Determinar datos de la pagaduria para inicio de créditos
                if (!DApackage.ConsultaEmpresa(pcod_empresa, pcod_linea_credito, ref ldias_novedad, ref lcod_periodicidad, ref n_dia_per, ref pfecha_inicio, ref s_tipo_ini, ref lcod_empresa, ref n_tipo_lista, usuario))
                {
                    pError = "El crédito no posee empresa o pagaduría definida o no existe un tipo de lista sobre el cual se defina el concepto de descuento";
                    return false;
                }
                // -- Determinar los días de la periodicidad de descuento de la empresa de recaudo
                n_dias_per_lis = DApackage.ConPeriodicidadNumDia(lcod_periodicidad, usuario);
                // -- Calcular la fecha de inicio
                pfecha_inicio = DateConstruct(DateYear(Trunc(pfecha_desembolso)), DateMonth(pfecha_inicio), DateDay(pfecha_inicio), 1, 1, 1);
                lfecha_inicio_ant = pfecha_inicio;
                while (pfecha_inicio < Trunc(pfecha_desembolso).AddDays(Convert.ToInt32(ldias_novedad)))
                {
                    lfecha_inicio_ant = pfecha_inicio;
                    pfecha_inicio = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per_lis), Convert.ToInt32(n_tip_cal));
                }
                // -- Según el tipo de pago calcular los días de ajuste
                if (ptipo_pago_int == 1)
                {
                    n_dias_aju = FecDifDia(Trunc(pfecha_desembolso), pfecha_inicio, Convert.ToInt32(n_tip_cal));
                }
                else if (ptipo_pago_int == 2)
                {
                    lfecha_inicio_cre = FecSumDia(Trunc(pfecha_desembolso), Convert.ToInt32(n_dias_per), Convert.ToInt32(n_tip_cal));
                    while (pfecha_inicio < lfecha_inicio_cre)
                    {
                        lfecha_inicio_ant = pfecha_inicio;
                        pfecha_inicio = FecSumDia(pfecha_inicio, Convert.ToInt32(n_dias_per_lis), Convert.ToInt32(n_tip_cal));
                    }
                    n_dias_aju = Convert.ToInt32(lfecha_inicio_ant - Trunc(pfecha_desembolso));
                }
                else
                {
                    pError = "El tipo de pago de interes se encuentra errado, debe ser Anticipado o Vencido";
                    return false;
                }
                pfecha_inicio = lfecha_inicio_ant;
            }
            else
            {
                if (pfecha_inicio == null)
                    pfecha_inicio = Trunc(pfecha_desembolso);
                n_dias_aju = FecDifDia(Trunc(pfecha_desembolso), pfecha_inicio, Convert.ToInt32(n_tip_cal));
            }
            return true;
        }

        public decimal? Redondeo(decimal? pvalor)
        {
            return Convert.ToDecimal(Math.Round(Convert.ToDouble(pvalor)));
        }

        public DateTime? DateConstruct(int? año, int? mes, int? dia, int? horas, int? minutos, int? segundos)
        {
            int? dato = null;
            int? año_cal = null;
            int? mes_cal = null;
            int? dia_cal = null;
            año_cal = año;
            mes_cal = mes;
            dia_cal = dia;
            if (mes_cal > 12)
            {
                dato = Trunc(mes_cal / 12);
                año_cal = año_cal + dato;
                mes_cal = mes_cal - (dato * 12);
            }
            if (mes_cal < 1 || mes_cal > 12)
                return null;
            if (dia_cal < 0 || dia_cal > 31)
                return null;
            if (mes_cal == 2)
            {
                if (Mod(año_cal, 4) == 0)
                {
                    if (dia_cal > 29)
                        dia_cal = 29;
                }
                else
                {
                    if (dia_cal > 28)
                        dia_cal = 28;
                }
            }
            if (mes_cal == 4 || mes_cal == 6 || mes_cal == 7 || mes_cal == 9 || mes_cal == 11)
            {
                if (dia_cal > 30)
                    dia_cal = 30;
            }
            return To_Date(mes_cal.ToString() + '/' + dia_cal.ToString() + '/' + año_cal.ToString(), "MM/DD/YYYY");
        }

        public int? DateYear(DateTime? pFecha)
        {
            return Convert.ToDateTime(pFecha).Year;
        }

        public int? DateMonth(DateTime? pFecha)
        {
            return Convert.ToDateTime(pFecha).Month;
        }

        public int? DateDay(DateTime? pFecha)
        {
            return Convert.ToDateTime(pFecha).Day;
        }

        public int? StrTokenize(string p_list, string p_del, ref string[] l_retlist)
        {
            //int l_idx;
            //string l_list = "";
            //int l_cont;

            //l_retlist = new string[100];
            //l_cont = 0;
            //while (true)
            //{ 
            //    l_idx = Convert.ToInt32(Instr(l_list, p_del));
            //    Array.Resize(ref l_retlist, l_retlist.Length + 1);
            //    l_cont = l_cont + 1;
            //    if (l_idx > 0)
            //    { 
            //        l_retlist[l_cont] = Trim(Substr(l_list, 1, l_idx-1));
            //        l_list = Substr(l_list, l_idx + p_del.Length, l_list.Length);
            //    }
            //    else
            //    { 
            //        l_retlist[l_cont] = Trim( l_list);
            //        break;
            //    }
            //}
            //return l_cont;
            l_retlist = p_list.Split(Convert.ToChar(p_del));

            return l_retlist.Length;
        }

        public bool LlineaCreditoEsReestructuracion(string pcod_linea_credito)
        { 
            string llineasreestructuracion;
            string[] llineas= new string[99];
            int lpos;
            bool besreestructurada;
            int? lnumero;
            // lDatos SYS_REFCURSOR;
            string lvalor = "";
            
            besreestructurada = false;
            //-- Determinar por parámetros generales
            llineasreestructuracion = BuscarGeneral(430, 1);
            if (Trim(llineasreestructuracion) != null)
            {
                lnumero = StrTokenize(llineasreestructuracion, ",", ref llineas);
                if (lnumero >= 0 || llineas.Length >= 1)
                {
                    lpos = 1;
                    while (lpos <= lnumero) {
                        if (Trim(pcod_linea_credito) == Trim(llineas[lpos])) {
                            besreestructurada = true;
                            break;
                        };
                        lpos = lpos + 1;
                    };
                };
            };
            //-- Determinar por parámetros de la línea
            //Open lDatos For Select valor From PARAMETROS_LINEA Where cod_parametro = 230 And cod_linea_credito = pcod_linea_credito;
            //Fetch lDatos Into lvalor;
            if (lvalor == null || lvalor == "")
            {
                if (lvalor == "1")
                {
                    besreestructurada = true;
                };
            };
            // Close lDatos;
            return besreestructurada;
        }

        public int? VerifLlineaEsReestructuracion(string pcod_linea_credito)
        { 
            if (LlineaCreditoEsReestructuracion(pcod_linea_credito))
                return 1;
            else
                return 0;
        }



        #region Funciones Generales

        public string NVL(string pvalor, string pdefecto)
        {
            if (pvalor.ToString().Trim() == "")
                return pdefecto;
            else
                return pvalor;
        }

        public decimal? NVL(decimal? pvalor, decimal? pdefecto)
        {
            if (pvalor.ToString().Trim() == "")
                return pdefecto;
            else
                return pvalor;
        }

        public int? NVL(int? pvalor, int? pdefecto)
        {
            if (pvalor.ToString().Trim() == "")
                return pdefecto;
            else
                return pvalor;
        }



        public decimal? Power(decimal? pexponente, decimal? pexponencial)
        {
            return Convert.ToDecimal(Math.Pow(Convert.ToDouble(pexponente), Convert.ToDouble(pexponencial)));
        }

        public decimal? Mod(decimal? pexponente, decimal? pexponencial)
        {
            return pexponente % pexponencial;
        }

        public decimal? Round(decimal? pexponente)
        {
            return Convert.ToDecimal(Math.Round(Convert.ToDouble(pexponente)));
        }

        public decimal? Round(decimal? pexponente, int? pnumdecimales)
        {
            return Convert.ToDecimal(Math.Round(Convert.ToDouble(pexponente), Convert.ToInt32(pnumdecimales)));
        }

        public int? To_Number(string pvalor)
        {
            return Convert.ToInt32(pvalor);
        }

        public DateTime? To_Date(string pvalor)
        {
            try
            {
                return Convert.ToDateTime(pvalor);
            }
            catch
            {
                return null;
            }
        }

        public DateTime? To_Date(string pvalor, string pFormato)
        {
            try
            {
                return DateTime.ParseExact(pvalor, pFormato, null);
            }
            catch
            {
                return null;
            }
        }

        public string Trim(string pvalor)
        {
            return pvalor.TrimEnd().TrimStart();
        }

        public int Length(string pvalor)
        {
            return pvalor.Length;
        }

        public string Substr(string ptexto, int pinicial, int plargo)
        {
            return ptexto.Substring(pinicial, plargo);
        }

        public bool StrIsValidNumber(string stexto)
        {
            string scaracter;
            int x;

            x = 0;
            while (x <= Length(stexto))
            {
                scaracter = Substr(stexto, x, 1);
                if (!(scaracter == "0" || scaracter == "1" || scaracter == "2" || scaracter == "3" || scaracter == "4" || scaracter == "5" || scaracter == "6" || scaracter == "7" || scaracter == "8" || scaracter == "9"))
                    return false;
                x = x + 1;
            }
            return true;
        }

        public DateTime Trunc(DateTime pFecha)
        {
            return new DateTime(pFecha.Year, pFecha.Month, pFecha.Day);
        }

        public DateTime Trunc(DateTime? pFecha)
        {
            return new DateTime(Convert.ToDateTime(pFecha).Year, Convert.ToDateTime(pFecha).Month, Convert.ToDateTime(pFecha).Day);
        }

        public int? Trunc(int? pValor)
        {
            return pValor;
        }

        public decimal? Trunc(decimal? pNumero)
        {
            return Convert.ToInt64(pNumero);
        }

        public string LPad(string lTexto, int? lLongitud, string lCaracter)
        {
            string ldato = lTexto;
            for (int i = 1; i <= lLongitud; i++)
            {
                ldato = lCaracter + ldato;
            }
            return ldato;
        }

        public string RPad(string lTexto, int? lLongitud, string lCaracter)
        {
            string ldato = lTexto;
            for (int i = 1; i <= lLongitud; i++)
            {
                ldato = ldato + lCaracter;
            }
            return ldato;
        }

        #endregion

        #region consultas a la base de datos

        public bool ConsultarCredito(Int64 pId, ref Credito pCredito)
        {
            return DApackage.ConsultarCredito(pId, ref pCredito, usuario);
        }

        public int? Instr(string pTexto, string pCadena)
        {
            return pTexto.IndexOf(pCadena);
        }

        #endregion

    }
}
