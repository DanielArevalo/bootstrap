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
    public class EstFlujoEfectivoBusiness : GlobalBusiness
    {
        private EstFlujoEfectivoData DAEstFlujoEfectivo;

         /// </summary>
        public EstFlujoEfectivoBusiness()
        {
            DAEstFlujoEfectivo = new EstFlujoEfectivoData();
        }

        public List<EstFlujoEfectivo> ListarDdllBusiness(Usuario pUsuario, int opcion) 
        {
            try
            {
                return DAEstFlujoEfectivo.ListarDdll(pUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Crearconcepto", ex);
                return null;
            }
        }

        public List<EstFlujoEfectivo> getListaReporGridv(Usuario pUsuario, DateTime fechaActual, DateTime fechaAnterior, int costoid, int pOpcion)
        {
            try
            {
                return DAEstFlujoEfectivo.getListaReporGridv(pUsuario, fechaActual, fechaAnterior, costoid, pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("conceptoBusiness", "Crearconcepto", ex);
                return null;
            }
        }
    }
}
