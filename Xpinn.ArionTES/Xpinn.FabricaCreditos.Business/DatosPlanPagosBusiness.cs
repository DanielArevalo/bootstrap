using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class DatosPlanPagosBusiness : GlobalData
    {
        private DatosPlanPagosData DADatosPlanPagos;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public DatosPlanPagosBusiness()
        {
            DADatosPlanPagos = new DatosPlanPagosData();
        }

        /// <summary>
        /// Obtiene la lista del plan de pagos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de plan de pagos obtenidos</returns>
        public List<DatosPlanPagos> ListarDatosPlanPagos(Credito pDatos, Usuario pUsuario)
        {
            try
            {
                return DADatosPlanPagos.ListarDatosPlanPagos(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "ListarDatosPlanPagos", ex);
                return null;
            }
        }

        public List<DatosPlanPagos> ListarDatosPlanPagosNue(Credito pDatos, Usuario pUsuario)
        {
            try
            {
                return DADatosPlanPagos.ListarDatosPlanPagosNue(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "ListarDatosPlanPagosNue", ex);
                return null;
            }
        }

        public List<Atributos> GenerarAtributosPlan(Usuario pUsuario)
        {
            try
            {
                return DADatosPlanPagos.GenerarAtributosPlan(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "GenerarAtributosPlan", ex);
                return null;
            }
        }

        public List<DatosPlanPagos> ListarDatosPlanPagosOriginal(Credito pDatos, Usuario usuario)
        {
            try
            {
                return DADatosPlanPagos.ListarDatosPlanPagosOriginal(pDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosPlanPagosBusiness", "ListarDatosPlanPagosOriginal", ex);
                return null;
            }
        }
    }
}
