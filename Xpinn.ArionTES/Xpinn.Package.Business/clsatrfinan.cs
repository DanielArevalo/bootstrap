using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Package.Data;
using Xpinn.Util;

namespace Xpinn.Package.Business
{
    public class clatrfinan
    {
        private packageData DApackage;
        private funciones BOFunciones;

        public int? n_cod_atr;
        public string s_nom_atr;
        public string s_tip_cal;
        public decimal? n_tasa;
        public int? n_tip_tas;
        public int? n_tip_his;
        public int? n_tip_his_pon;
        public decimal? n_desv;
        public int? n_per_pro;
        public string s_tip_gra;
        public decimal? n_gradiente;
        public int? n_cob_mor;
        public decimal? n_tasa_calculo;
        public decimal? n_valor;
        public decimal? n_tasa_dtf;

        Usuario usuario = new Usuario();

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public clatrfinan(Usuario pUsuario)
        {
            DApackage = new packageData();
            BOFunciones = new funciones(pUsuario);
            usuario = pUsuario;

            n_cod_atr = null;
            s_nom_atr = null;
            s_tip_cal = null;
            n_tasa = null;
            n_tip_tas = null;
            n_tip_his = null;
            n_tip_his_pon = null;
            n_desv = null;
            n_per_pro = null;
            s_tip_gra = null;
            n_gradiente = null;
            n_cob_mor = null;
            n_tasa_calculo = null;
            n_valor = null;
            n_tasa_dtf = null;
        }

        public void SetAtrfinan(int? pn_cod_atr, string ps_tip_cal, decimal? pn_tasa, int? pn_tip_tas, int? pn_tip_his, decimal? pn_desv, int? pn_per_pro, string ps_tip_gra, decimal? pn_gradiente, int? pn_cob_mor, decimal? pn_tasa_calculo, decimal? pn_valor)
        {
            n_cod_atr = pn_cod_atr;
            s_tip_cal = ps_tip_cal;
            n_tasa = pn_tasa;
            n_tip_tas = pn_tip_tas;
            n_tip_his = pn_tip_his;
            n_desv = pn_desv;
            n_per_pro = pn_per_pro;
            s_tip_gra = ps_tip_gra;
            n_gradiente = pn_gradiente;
            n_cob_mor = pn_cob_mor;
            n_tasa_calculo = pn_tasa_calculo;
            n_valor = pn_valor;
            try
            {
                s_nom_atr = DApackage.NombreAtributo(n_cod_atr, usuario);
            }
            catch
            { };
        }

        public void GetAtrfinan(ref int? rn_cod_atr, ref string rs_tip_cal, ref decimal? rn_tasa, ref int? rn_tip_tas, ref int? rn_tip_his, ref decimal? rn_desv, ref int? rn_per_pro, ref string rs_tip_gra, ref decimal? rn_gradiente, ref int? rn_cob_mor, ref decimal? rn_tasa_calculo, ref decimal? rn_valor)
        {
            rn_cod_atr = n_cod_atr;
            rs_tip_cal = s_tip_cal;
            rn_tasa = n_tasa;
            rn_tip_tas = n_tip_tas;
            rn_tip_his = n_tip_his;
            rn_desv = n_desv;
            rn_per_pro = n_per_pro;
            rs_tip_gra = s_tip_gra;
            rn_gradiente = n_gradiente;
            rn_cob_mor = n_cob_mor;
            rn_tasa_calculo = n_tasa_calculo;
            rn_valor = n_valor;
        }

        public void GetAtrfinan_Tasa(ref int? rn_cod_atr, ref string rs_nom_atr, ref decimal? rn_tasa_cal)
        {
            rn_cod_atr = n_cod_atr;
            rs_nom_atr = s_nom_atr;
            rn_tasa_cal = n_tasa_calculo;
        }

        public void Conv_Tasa(int? pn_per, int? pn_mod, DateTime? pf_fec_apro, string ps_tipo, decimal? pn_monto, Int64? pn_cod_cliente, string ps_cod_credi, ref decimal? rn_val_tasa)
        {
            try
            { 
                string s_efe_nom = "";
                int? n_per = null;
                string s_mod = "";
                int? n_mod_per = null;
                decimal? n_dias_per = null;
                decimal? n_dias_pnper = null;
                if (s_tip_cal == "2")
                { 
                   if (!Ponderado(ref n_tasa, ref n_tip_tas, ref n_desv, ref n_tip_his, pn_monto, pn_cod_cliente, ps_cod_credi, pf_fec_apro) )
                      n_tasa = null;
                }
                else
                {
                    if (!BOFunciones.CalIntAtr(s_tip_cal, ref n_tasa, ref n_tip_tas, n_tip_his, n_desv, n_per_pro, pf_fec_apro))
                        n_tasa = null;
                }
                DApackage.ConTipoTasa(n_tip_tas, ref s_efe_nom, ref n_per, ref s_mod, ref n_mod_per, usuario);
                if (n_tasa != null)
                {
                    if (ps_tipo == "1")
                    {
                        n_dias_per = DApackage.ConPeriodicidadNumDia(n_per, usuario);
                        n_dias_pnper = DApackage.ConPeriodicidadNumDia(pn_per, usuario);
                        if (n_dias_per != 0)
                            n_tasa_calculo = n_tasa / n_dias_per * n_dias_pnper;
                        else
                            n_tasa_calculo = 0;
                    }
                    else
                    { 
                        n_tasa_dtf = n_tasa - BOFunciones.NVL(n_desv, 0);
                        n_tasa_calculo = BOFunciones.CalTasMod(n_tasa, To_Number(s_efe_nom), n_per, To_Number(s_mod), n_mod_per, 1, pn_per, pn_mod, pn_per, 0);
                    }
                }
                rn_val_tasa = n_tasa_calculo;
            }
            catch
            {
                rn_val_tasa = null;
            }
        }

        public bool Ponderado(ref decimal? rn_val_tasa, ref int? rn_tip_tas, ref decimal? n_desviacion, ref int? n_tipo_hist, decimal? pn_monto, Int64? pn_cod_cliente, string ps_cod_credi, DateTime? pf_fec_apro)
        {
            decimal? n_deuda;
            decimal? n_tasa1 = null;
            decimal? n_tasa2 = null;
            decimal? n_tasa3 = null;
            decimal? n_monto;
            decimal? n_tot_aportes = null;

            n_tasa1 = 0;
            n_tasa2 = 0;
            n_tasa3 = 0;
            // Suma la deuda pendiente al monto solicitado
            n_deuda = 0;            
            n_deuda = DApackage.SumaDeudas(pn_cod_cliente, usuario);
            if (n_deuda > 0 && n_deuda != null)
                n_monto = pn_monto + n_deuda;            
            if (s_tip_cal == "2")
                n_tot_aportes = 0;            
            return true;
        }


        public int? To_Number(string pvalor)
        {
            return BOFunciones.To_Number(pvalor);
        }


    }

}
