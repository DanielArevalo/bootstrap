using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Xpinn.Package.Business
{
    public class clscuotasextras
    {
        public DateTime? f_fecha;
        public int? n_cuota;
        public decimal? n_valor;
        public decimal? n_capital;
        public decimal? n_saldo_cap;
        public decimal? n_interes;
        public decimal? n_saldo_int;
        public int? n_forma_pag;
        public string c_estado;
        public Int64? n_num_ter;

        public clscuotasextras()
        {
            f_fecha = null;
            n_cuota = null;
            n_valor = null;
            n_capital = null;
            n_saldo_cap = null;
            n_interes = null;
            n_saldo_int = null;
            n_forma_pag = null;
            c_estado = null;
            n_num_ter = null;
        }

    }
}
