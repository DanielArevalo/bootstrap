using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para ProcesoOficina
    /// </summary>
    public class ProcesoOficinaBusiness : GlobalData
    {

        private ProcesoOficinaData DAProcesoOficina;

        /// <summary>
        /// Constructor del objeto de negocio para ProcesoOficina
        /// </summary>
        public ProcesoOficinaBusiness()
        {
            DAProcesoOficina = new ProcesoOficinaData();
        }

        /// <summary>
        /// Crea un procesoOficina
        /// </summary>
        /// <param name="pEntity">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public ProcesoOficina CrearProcesoOficina(ProcesoOficina pProcesoOficina, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesoOficina = DAProcesoOficina.InsertarProcesoOficina(pProcesoOficina, ref pError, pUsuario);

                    ts.Complete();
                }
                return pProcesoOficina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaBusiness", "CrearProcesoOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene consulta por una Oficina
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public ProcesoOficina ConsultarXProcesoOficina(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            try
            {
                return DAProcesoOficina.ConsultarXProcesoOficina(pProcesoOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaBusiness", "ConsultarXProcesoOficina", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene consulta por una Oficina
        /// </summary>
        /// <param name="pId">identificador de la Oficina</param>
        /// <returns>Caja consultada</returns>
        public ProcesoOficina ConsultarUsuarioAperturo(ProcesoOficina pProcesoOficina, Usuario pUsuario)
        {
            try
            {
                return DAProcesoOficina.ConsultarUsuarioAperturo(pProcesoOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaBusiness", "ConsultarUsuarioAperturo", ex);
                return null;
            }
        }

        public int CrearCajasAbrir(List<Xpinn.Caja.Entities.Caja> pListCaja,string pcod_cajero, Usuario pUsuario)
        {
            try
            {
                int result = 1;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (Xpinn.Caja.Entities.Caja i in pListCaja)
                    {
                        SaldoCaja saldocaja = new SaldoCaja();
                        SaldoCajaData DASaldoCaja = new SaldoCajaData();
                        saldocaja.cod_caja = Convert.ToInt64(i.cod_caja);
                        saldocaja.cod_cajero = Convert.ToInt64(pcod_cajero);
                        saldocaja.fecha = i.fecha_creacion;
                        saldocaja.tipo_moneda = 1;
                        saldocaja.valor = 0;
                        saldocaja = DASaldoCaja.CrearSaldoCaja(saldocaja, pUsuario);
                    }
                    ts.Complete();
                }
                return result;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoOficinaBusiness", "CrearProcesoOficina", ex);
                return 0;
            }
        }
    }
}
