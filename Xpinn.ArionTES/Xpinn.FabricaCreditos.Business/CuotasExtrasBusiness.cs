using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para CuotasExtras
    /// </summary>
    public class CuotasExtrasBusiness : GlobalData
    {
        private CuotasExtrasData DACuotasExtras;

        /// <summary>
        /// Constructor del objeto de negocio para CuotasExtras
        /// </summary>
        public CuotasExtrasBusiness()
        {
            DACuotasExtras = new CuotasExtrasData();
        }

        /// <summary>
        /// Crea un CuotasExtras
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras creada</returns>
        public CuotasExtras CrearCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuotasExtras = DACuotasExtras.CrearCuotasExtras(pCuotasExtras, pUsuario);

                    ts.Complete();
                }

                return pCuotasExtras;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "CrearCuotasExtras", ex);
                return null;
            }
        }




        public void CrearSolicitudCuotasExtras(List<CuotasExtras> lstCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstCuotasExtras != null && lstCuotasExtras.Count > 0)
                        foreach (CuotasExtras pCuotasExtras in lstCuotasExtras)
                        {
                            CuotasExtras entidad = new CuotasExtras();
                            entidad = DACuotasExtras.CrearSolicitudCuotasExtras(pCuotasExtras, pUsuario);
                        }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "CrearSolicitudCuotasExtras", ex);
            }
        }

        public void CrearSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {                    
                    CuotasExtras entidad = new CuotasExtras();
                    entidad = DACuotasExtras.CrearSolicitudCuotasExtras(pCuotasExtras, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "CrearSolicitudCuotasExtras", ex);
            }
        }


        /// <summary>
        /// Modifica un CuotasExtras
        /// </summary>
        /// <param name="pCuotasExtras">Entidad CuotasExtras</param>
        /// <returns>Entidad CuotasExtras modificada</returns>
        public CuotasExtras ModificarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuotasExtras = DACuotasExtras.ModificarCuotasExtras(pCuotasExtras, pUsuario);

                    ts.Complete();
                }

                return pCuotasExtras;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "ModificarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un CuotasExtras
        /// </summary>
        /// <param name="pId">Identificador de CuotasExtras</param>
        public void EliminarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuotasExtras.EliminarCuotasExtras(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "EliminarCuotasExtras", ex);
            }
        }

        /// <summary>
        /// Obtiene un CuotasExtras
        /// </summary>
        /// <param name="pId">Identificador de CuotasExtras</param>
        /// <returns>Entidad CuotasExtras</returns>
        public CuotasExtras ConsultarCuotasExtras(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ConsultarCuotasExtras(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "ConsultarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ListarCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "ListarCuotasExtras", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCuotasExtras">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CuotasExtras obtenidos</returns>
        public List<CuotasExtras> ListarSolicitudCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ListarSolicitudCuotasExtras(pCuotasExtras, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "ListarSolicitudCuotasExtras", ex);
                return null;
            }
        }


        public List<CuotasExtras> ListarCuotasExtrasId(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACuotasExtras.ListarCuotasExtrasId(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "DACuotasExtras", ex);
                return null;
            }
        }


        public void CrearSimulacionCuotasExtras(List<CuotasExtras> lstCuotasExtras, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstCuotasExtras != null && lstCuotasExtras.Count > 0)
                        foreach (CuotasExtras pCuotasExtras in lstCuotasExtras)
                        {
                            CuotasExtras entidad = new CuotasExtras();
                            entidad = DACuotasExtras.CrearSimulacionCuotasExtras(pCuotasExtras, pUsuario);
                        }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "CrearSolicitudCuotasExtras", ex);
            }
        }


        public void CrearSimulacionCuotasExtras(CuotasExtras pCuotasExtras, Usuario pUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    CuotasExtras entidad = new CuotasExtras();
                    entidad = DACuotasExtras.CrearSimulacionCuotasExtras(pCuotasExtras, pUsuario);
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "CrearSolicitudCuotasExtras", ex);
            }
        }

        public void EliminarCuotasExtrasTemporales(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuotasExtras.EliminarCuotasExtrasTemporales(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuotasExtrasBusiness", "EliminarCuotasExtrasTemporales", ex);
            }
        }

    }
}