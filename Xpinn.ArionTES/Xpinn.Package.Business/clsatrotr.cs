using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Package.Data;
using Xpinn.Util;
using Xpinn.Package.Entities;
using System.Data.Common;

namespace Xpinn.Package.Business
{
    public class clsatrotr
    {
        private packageData DApackage;
        private funciones BOFunciones;

        public int? n_cod_atr;
        public string s_nom_atr;
        public int? n_tip_des;
        public decimal? n_valor;
        public int? n_tip_liq;
        public int? n_signo;
        public int? n_cob_mor;
        public int? n_num_cuotas;
        public decimal? n_valor_calculo;
        public decimal?[] n_atributo_depende;
        public int n_num_atr;
        public decimal?[] n_valor_presentes;
        public decimal?[] n_valor_pagos;
        public int? n_num_pag;

        Usuario usuario = new Usuario();

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public clsatrotr()
        {
            DApackage = new packageData();
        }

        public clsatrotr(Usuario pUsuario)
        {
            DApackage = new packageData();
            BOFunciones = new funciones(pUsuario);
            usuario = pUsuario;

            n_cod_atr = null;
            s_nom_atr = null;
            n_tip_des = null;
            n_valor = null;
            n_tip_liq = null;
            n_signo = null;
            n_cob_mor = null;
            n_num_cuotas = null;
            n_valor_calculo = null;
            n_atributo_depende = new decimal?[99];
            n_num_atr = 0;
            n_valor_presentes = new decimal?[99];
            n_valor_pagos = new decimal?[99];
            n_num_pag = null;
        }

        public void SetAtrOtr(int? pn_cod_atr, int? pn_tip_des, decimal? pn_valor, int? pn_tip_liq, int? pn_signo, int? pn_cob_mor, int? pn_num_cuotas)
        {
            n_cod_atr = pn_cod_atr;
            n_tip_des = pn_tip_des;
            n_valor = pn_valor;
            n_tip_liq = pn_tip_liq;
            n_signo = pn_signo;
            n_cob_mor = pn_cob_mor;
            n_num_cuotas = pn_num_cuotas;
            if (pn_cod_atr != null)
                s_nom_atr = DApackage.NombreAtributo(pn_cod_atr, usuario);
            n_atributo_depende = new decimal?[99];
            n_valor_presentes = new decimal?[99];
            n_valor_pagos = new decimal?[99];
        }

        public void GetAtrOtr(ref int? rn_cod_atr, ref int? rn_tip_des, ref decimal? rn_valor, ref int? rn_tip_liq, ref int? rn_signo, ref int? rn_cob_mor, ref int? rn_num_cuotas)
        {
            rn_cod_atr = n_cod_atr;
            rn_tip_des = n_tip_des;
            rn_valor = n_valor;
            rn_tip_liq = n_tip_liq;
            rn_signo = n_signo;
            rn_cob_mor = n_cob_mor;
            rn_num_cuotas = n_num_cuotas;
        }

        public void Ins_Dat_Basico(int? pn_cod_atr, string ps_nom_atr, decimal? pn_valor_cal, int? pn_signo)
        {
            n_cod_atr = pn_cod_atr;
            s_nom_atr = ps_nom_atr;
            n_valor_calculo = pn_valor_cal;
            n_signo = pn_signo;
        }

        public void Cal_Valor(decimal? pn_monto, decimal? pn_plazo, int? pn_num_cuo, ref decimal? rn_valor, ref int? rn_signo, decimal? pn_saldo, decimal? pn_interes, decimal? pn_valorCatg, decimal? pn_valfactor, decimal? pn_vrComer, decimal? pn_cuota, decimal? n_cuo_ext_ini, int? n_tipo_prod, int? n_num_codeu, ref bool b_resultado) 
        {
            //string sentencia;
            string general_10;
            //string s_valor;
            bool b_ok;
            //decimal? n_val_par;
            //decimal? n_val_timbre;
            int? n_depende_atr;
            int? n_tipo_tope = null;
            decimal? n_desde = null;
            decimal? n_hasta = null;
            decimal? n_valor_tope = null;
            int? n_pos;
            //int? n_error;
            decimal? n_smlmv;

            b_resultado = true;              
            if (n_tip_des == 4)
            { 
                n_valor = 0;
                general_10 = DApackage.ConGeneral(10, usuario);
                List<RanValAtributo> lstRanVal = new List<RanValAtributo>();
                lstRanVal = DApackage.ConRanValAtributo(n_cod_atr, ref n_tipo_tope, ref n_desde, ref n_hasta, ref n_valor_tope, usuario);                
                n_pos = 0;
                foreach (RanValAtributo item in lstRanVal)
                {
                    n_tipo_tope = item.n_tipo_tope;
                    n_desde = item.n_desde;
                    n_hasta = item.n_hasta;
                    n_valor_tope = item.n_valor_tope;
                    if (n_tipo_tope == 0)
                    { 
                        if (n_tipo_prod == 4)
                        { 
                            if (n_desde <= pn_vrComer && pn_vrComer <= n_hasta)
                            { 
                                n_valor = n_valor_tope;
                                break;
                            }
                        }
                        else
                        { 
                            if (n_desde <= pn_monto && pn_monto <= n_hasta)
                            { 
                                n_valor = n_valor_tope;
                                break;
                            }
                        }
                    }
                    if (n_tipo_tope == 1 && StrIsValidNumber(Trim(general_10)) )
                    { 
                        n_smlmv = To_Number(Trim(general_10));
                        n_smlmv = Math.Round(Convert.ToDecimal((pn_monto/n_smlmv) * 10))  / 10;
                        if (n_desde <= n_smlmv && n_smlmv <= n_hasta)
                        { 
                            n_valor = n_valor_tope;
                            break;
                        }
                    }
                    n_pos = n_pos + 1;
                }
            }

            // Se coloco para que cobre atributos sobre el monto del credito
            n_valor_calculo = n_cuo_ext_ini;
            if (CalValDes(n_tip_des, n_valor, n_tip_liq, pn_monto, pn_plazo, pn_num_cuo, ref n_valor_calculo, pn_saldo, pn_interes, pn_valorCatg, pn_valfactor, pn_vrComer, pn_cuota, n_num_codeu))
            {
                // Redondear el valor calculado.
                n_valor_calculo = Round(n_valor_calculo);
                // Calculo cuando el atributo depende de otro.
                n_depende_atr = null;
                DbDataReader pAtrOtr = DApackage.EjecutarSQL("Select depende from atributos where cod_atr = " + n_cod_atr, usuario);
                n_pos = 0;
                while (pAtrOtr.Read())
                { 
                    if (pAtrOtr["depende"] != DBNull.Value) n_depende_atr = Convert.ToInt32(pAtrOtr["depende"]);
                }
                // Calculo del valor total del atributo
                rn_valor = n_valor_calculo;
                rn_signo = n_signo;
                if (n_signo == 3)
                    rn_valor = 0;                
            }
            else
            {
                b_resultado = false;
            }

        }

        public void GetAtrOtrVal(int? pn_signo, ref int? rn_cod_atr, ref string rs_nom_atr, ref decimal? rn_valor) 
        { 
            if (n_signo == pn_signo)
            {
                rn_cod_atr = n_cod_atr;
                rs_nom_atr = s_nom_atr;
                rn_valor = n_valor_calculo;
                return;
            }
            else
            {
                rn_cod_atr = 0;
                rs_nom_atr = "";
                rn_valor = 0;
                return;
            }
        }

        public int? To_Number(string pvalor)
        {
            return BOFunciones.To_Number(pvalor);
        }

        public string Trim(string pvalor)
        {
            return BOFunciones.Trim(pvalor);
        }

        public decimal? Round(decimal? pexponente)
        {
            return BOFunciones.Round(pexponente);
        }

        public bool StrIsValidNumber(string stexto)
        {
            return BOFunciones.StrIsValidNumber(stexto);
        }

        public bool CalValDes(int? ptip_des, decimal? pvalor, int? ptip_liq, decimal? pmonto, decimal? pplazo, int? pnum_cuo, ref decimal? rval_cal, decimal? psaldo, decimal? pinteres, decimal? pvalorCatg, decimal? pvalfactor, decimal? pvrComer, decimal? pcuota, int? pnumCodeu)
        {
            return BOFunciones.CalValDes(ptip_des, pvalor, ptip_liq, pmonto, pplazo, pnum_cuo, ref rval_cal, psaldo, pinteres, pvalorCatg, pvalfactor, pvrComer, pcuota, pnumCodeu);
        }

    }
}
