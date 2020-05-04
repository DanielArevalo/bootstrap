using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class BalanceGeneralBusiness : GlobalBusiness
    {
        private BalanceGeneralData DABalanceGeneral;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public BalanceGeneralBusiness()
        {
            DABalanceGeneral = new BalanceGeneralData();
        }

        public List<BalanceGeneral> ListarBalance(BalanceGeneral pEntidad, Usuario vUsuario, int pOpcion)
        {
            return DABalanceGeneral.ListarBalance(pEntidad, vUsuario, pOpcion);
        }

        public List<BalanceGeneral> ListarFechaCierre(Usuario pUsuario)
        {
            return DABalanceGeneral.ListarFechaCierre(pUsuario);
        }

        public List<BalanceGeneral> ListarFechaCierre(string pTipo, string pEstado, Usuario pUsuario)
        {
            return DABalanceGeneral.ListarFechaCierre(pTipo, pEstado, pUsuario);
        }

        public string VerificarComprobantesYCuentas(DateTime fechaCorte, Usuario pUsuario)
        {
            try
            {
                return DABalanceGeneral.VerificarComprobantesYCuentas(fechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceGeneralServices", "VerificarComprobantesYCuentas", ex);
                return null;
            }
        }
    }
}