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
    /// Objeto de negocio para CreditoRecoger
    /// </summary>
    public class CreditoRecogerBusiness : GlobalBusiness
    {
        private CreditoRecogerData DACreditoRecoger;

        /// <summary>
        /// Constructor del objeto de negocio para CreditoRecoger
        /// </summary>
        public CreditoRecogerBusiness()
        {
            DACreditoRecoger = new CreditoRecogerData();
        }

        /// <summary>
        /// Crea un CreditoRecoger
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger creada</returns>
        public CreditoRecoger CrearCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCreditoRecoger = DACreditoRecoger.CrearCreditoRecoger(pCreditoRecoger, pUsuario);

                    ts.Complete();
                }

                return pCreditoRecoger;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "CrearCreditoRecoger", ex);
                return null;
            }
        }
        public int ConsultaPermisoModificarTasa(string cod, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.ConsultaPermisoModificarTasa(cod, pUsuario);
            }
            catch 
            {
                return 0;
                //BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
            }
        }

        /// <summary>
        /// Modifica un CreditoRecoger
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger modificada</returns>
        public CreditoRecoger ModificarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCreditoRecoger = DACreditoRecoger.ModificarCreditoRecoger(pCreditoRecoger, pUsuario);

                    ts.Complete();
                }

                return pCreditoRecoger;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "ModificarCreditoRecoger", ex);
                return null;
            }
        }


        public List<CreditoRecoger> ListarCreditoARecoger(string cod, DateTime pfechacalculo, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.ListarCreditoARecoger(cod, pfechacalculo, pUsuario);
            }
            catch 
            {

                return null;
                //BOExcepcion.Throw("$Programa$Service", "EliminarCreditoRecoger", ex);
            }
        }


        /// <summary>
        /// Elimina un CreditoRecoger
        /// </summary>
        /// <param name="pId">Identificador de CreditoRecoger</param>
        public void EliminarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACreditoRecoger.EliminarCreditoRecoger(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "EliminarCreditoRecoger", ex);
            }
        }

        /// <summary>
        /// Obtiene un CreditoRecoger
        /// </summary>
        /// <param name="pId">Identificador de CreditoRecoger</param>
        /// <returns>Entidad CreditoRecoger</returns>
        public List<CreditoRecoger> ConsultarCreditoRecoger(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.ConsultarCreditoRecoger(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "ConsultarCreditoRecoger", ex);
                return null;
            }
        }

        public List<CreditoRecoger> Consultarterminosfijos(string radicacion, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.Consultarterminosfijos(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerService", "ConsultarCreditoRecoger", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecoger(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.ListarCreditoRecoger(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "ListarCreditoRecoger", ex);
                return null;
            }
        }
        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCreditoRecoger">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CreditoRecoger obtenidos</returns>
        public List<CreditoRecoger> ListarCreditoRecogerSolicitud(CreditoRecoger pCreditoRecoger, Usuario pUsuario)
        {
            try
            {
                return DACreditoRecoger.ListarCreditoRecogerSolicitud(pCreditoRecoger, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "ListarCreditoRecogerSolicitud", ex);
                return null;
            }
        }

        public decimal ConsultarValorNoCapitalizado(string numeroRadicacion, Usuario usuario)
        {
            try
            {
                return DACreditoRecoger.ConsultarValorNoCapitalizado(numeroRadicacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoRecogerBusiness", "ConsultarValorNoCapitalizado", ex);
                return 0;
            }
        }
    }
}