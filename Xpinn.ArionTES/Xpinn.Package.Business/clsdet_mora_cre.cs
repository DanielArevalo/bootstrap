using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xpinn.Package.Business
{
    public class clsdet_mora_cre
    {

        public DateTime? f_fecha_cuota;
        public int? n_cod_atr;
        public decimal? n_valor;
        public decimal? n_saldo;
        public decimal? n_tasa_int;
        public decimal? n_valor_base;
        public DateTime? f_fecha_ini;
        public DateTime? f_fecha_fin;
        public int? n_dias_mora;
        public string s_estado;
        public string s_estado_pago;

        public clsdet_mora_cre()
        {
            f_fecha_cuota = null;
            n_cod_atr = null;
            n_valor = null;
            n_saldo = null;
            n_tasa_int = null;
            n_valor_base = null;
            f_fecha_ini = null;
            f_fecha_fin = null;
            n_dias_mora = null;
            s_estado = null;
            s_estado_pago = null;
        }


    }
}
