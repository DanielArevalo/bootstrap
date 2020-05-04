using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;
using Xpinn.Comun.Entities;

namespace Xpinn.Cartera.Business
{
    public class CortoyLargoPlazoBusiness : GlobalData
    {

        private CortoyLargoPlazoData DACortoyLargoPlazo;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public CortoyLargoPlazoBusiness()
        {
            DACortoyLargoPlazo = new CortoyLargoPlazoData();
        }

        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            try
            {
                return DACortoyLargoPlazo.ListarFechaCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CortoyLargoPlazoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Método para listar los créditos a refinanciar
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<CortoyLargoPlazo> ListarCredito(DateTime pFecha, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACortoyLargoPlazo.ListarCredito(pFecha, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CortoyLargoPlazoBusiness", "ListarCredito", ex);
                return null;
            }
        }

    }
}
