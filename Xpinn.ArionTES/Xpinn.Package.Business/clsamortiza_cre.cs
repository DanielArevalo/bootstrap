using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xpinn.Package.Data;
using Xpinn.Util;


namespace Xpinn.Package.Business
{
    public class clsamortiza_cre
    {
        private packageData DApackage;
        private funciones BOFunciones;

        public DateTime? f_fecha_cuota;
        public int? n_cod_atr;
        public decimal? n_valor;
        public decimal? n_saldo;
        public decimal? n_tasa_int;
        public decimal? n_saldo_base;
        public string s_estado;

        Usuario usuario = new Usuario();

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public clsamortiza_cre(Usuario pUsuario)
        {
            DApackage = new packageData();
            BOFunciones = new funciones(pUsuario);
            usuario = pUsuario;

            f_fecha_cuota = null;
            n_cod_atr = null;
            n_valor = null;
            n_saldo = null;
            n_tasa_int = null;
            n_saldo_base = null;
            s_estado = null;
        }
    }
}
