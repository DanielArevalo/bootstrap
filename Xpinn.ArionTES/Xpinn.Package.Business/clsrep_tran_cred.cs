using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xpinn.Package.Business
{
    public class clsrep_tran_cred
    {
        public DateTime? f_fecha_cuota;
        public int? n_num_cuota;
        public int? n_cod_atr;
        public decimal? n_valor;
        public decimal? n_saldo;
        public decimal? n_causado;
        public decimal? n_orden;
        public decimal? n_mes;
        public string s_estado;
        public decimal? n_saldo_base;
        public decimal? n_tasa_base;

       public clsrep_tran_cred()
        {
            f_fecha_cuota = null;
            n_num_cuota = null;
            n_cod_atr = null;
            n_valor = null;
            n_saldo = null;
            n_causado = null;
            n_orden = null;
            n_mes = null;
            s_estado = null;
            n_saldo_base = null;
            n_tasa_base = null;
        }
    }
}
