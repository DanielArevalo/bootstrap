using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Cajero
    /// </summary>
    public class CajeroBusiness : GlobalData
    {
        private CajeroData DACajero;

        /// <summary>
        /// Constructor del objeto de negocio para Cajero
        /// </summary>
        public CajeroBusiness()
        {
            DACajero = new CajeroData();
        }

        /// <summary>
        /// Crea un Cajero
        /// </summary>
        /// <param name="pEntity">Entidad Cajero</param>
        /// <returns>Entidad creada</returns>
        public Cajero CrearCajero(Cajero pCajero, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCajero = DACajero.InsertarCajero(pCajero,pUsuario);

                    ts.Complete();
                }

                return pCajero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "CrearCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea cajeros de forma masiva - la asigna a una caja
        /// </summary>
        /// <param name="pEntity">Entidad Caja</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.Cajero CrearCajeroMass(Xpinn.Caja.Entities.Cajero pCajero, GridView gvCajeros,Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCajero = DACajero.InsertarCajeroMass(pCajero, gvCajeros, pUsuario);

                    ts.Complete();
                }

                return pCajero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "CrearCajeroMass", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Cajero
        /// </summary>
        /// <param name="pEntity">Entidad Cajero</param>
        /// <returns>Entidad modificada</returns>
        public Cajero ModificarCajero(Cajero pCajero, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCajero = DACajero.ModificarCajero(pCajero,pUsuario);

                    ts.Complete();
                }

                return pCajero;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ModificarCajero", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Cajero
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        public void EliminarCajero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DACajero.EliminarCajero(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "EliminarCajero", ex);
            }
        }


        /// <summary>
        /// Obtiene un Cajero
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        /// <returns>Caja consultada</returns>
        public Cajero ConsultarCajero(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarCajero(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Caja principal existente
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarCajeroRelCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarCajeroRelCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarCajeroRelCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Cajero principal existente
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarCajeroPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarCajeroPrincipal", ex);
                return null;
            }
        }


        public Caja.Entities.Cajero ConsultarCajeroPrincipalAsignadoAlCajero(Int64 cod_oficina, Int64 cod_cajero, Usuario pUsuario)
        {
            try
            {
                var cajeroPrincipal = DACajero.ConsultarCajeroPrincipalAsignadoAlCajero(cod_cajero, pUsuario);

                if (cajeroPrincipal.conteo == 0)
                {
                    cajeroPrincipal = ConsultarCajeroPrincipal(cod_oficina, pUsuario);
                    return cajeroPrincipal;
                }
                else
                {
                    return cajeroPrincipal;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarCajeroPrincipalAsignadoAlCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene una Caja principal existente
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Cajero ConsultarIfUserIsntCajeroPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarIfUserIsntCajeroPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarIfUserIsntCajeroPrincipal", ex);
                return null;
            }
        }


        public Caja.Entities.Reintegro ConsultarFecha(Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarFecha", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarTCajero(Cajero pCajero, Usuario pUsuario)
        {
            try
            {
                return DACajero.ListarTCajero(pCajero, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ListarTCajero", ex);
                return null;
            }
        }
         /// <summary>
        /// Obtiene la lista de Cajeros dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarCajero(Cajero pCajero,Usuario pUsuario)
        {
            try
            {
                return DACajero.ListarCajero(pCajero,pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ListarCajero", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de TipoOperacion-Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CajeroXCaja - Caja obtenidos</returns>
        public Cajero ConsultarCajeroXCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            try
            {
                return DACajero.ConsultarCajeroXCaja(pId,pCaja,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ConsultarCajeroXCaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Cajeros de una Caja especificada
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajeros obtenidos</returns>
        public List<Cajero> ListarCajeroXCaja(Cajero pCajero, Usuario pUsuario)
        {
            try
            {
                return DACajero.ListarCajeroXCaja(pCajero, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ListarCajeroXCaja", ex);
                return null;
            }
        }


        public List<Cajero> ListarCajeroPorOficina(Cajero pCajero, Usuario pUsuario)
        {
            try
            {
                return DACajero.ListarCajeroPorOficina(pCajero, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajeroBusiness", "ListarCajeroPorOficina", ex);
                return null;
            }
        }

    }
}
