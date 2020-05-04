using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para Caja
    /// </summary>
    public class CajaBusiness : GlobalData
    {
        private CajaData DACaja;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public CajaBusiness()
        {
            DACaja = new CajaData();
        }

        /// <summary>
        /// Crea una Caja
        /// </summary>
        /// <param name="pEntity">Entidad Caja</param>
        /// <returns>Entidad creada</returns>
        public Xpinn.Caja.Entities.Caja CrearCaja(Xpinn.Caja.Entities.Caja pCaja, GridView gvTopes, GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCaja = DACaja.InsertarCaja(pCaja, gvTopes, gvOperaciones, pUsuario);

                    ts.Complete();
                }

                return pCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "CrearCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica una Caja
        /// </summary>
        /// <param name="pEntity">Entidad Caja</param>
        /// <returns>Entidad modificada</returns>
        public Caja.Entities.Caja ModificarCaja(Caja.Entities.Caja pCaja, GridView gvTopes,GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCaja = DACaja.ModificarCaja(pCaja, gvTopes, gvOperaciones, pUsuario);

                    ts.Complete();
                }

                return pCaja;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ModificarCaja", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina una Caja
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        public void EliminarCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DACaja.EliminarCaja(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "EliminarCaja", ex);
            }
        }

        /// <summary>
        /// Obtiene una Caja
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCaja(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACaja.ConsultarCaja(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ConsultarCaja", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene una Caja principal existente
        /// </summary>
        /// <param name="pId">identificador de la Caja</param>
        /// <returns>Caja consultada</returns>
        public Caja.Entities.Caja ConsultarCajaPrincipal(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACaja.ConsultarCajaPrincipal(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ConsultarCajaPrincipal", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarCaja(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarComboCaja(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarComboCaja", ex);
                return null;
            }
        }


        public List<Caja.Entities.Caja> ListarComboCajaXOficinaycaja(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarComboCajaXOficinaycaja(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarComboCajaXOficinaycaja", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXOficina(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarComboCajaXOficina(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarComboCajaXOficina", ex);
                return null;
            }
        }

        public List<Caja.Entities.Caja> ListarComboCajaXOficinaActiva(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarComboCajaXOficinaActiva(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarComboCajaXOficinaActiva", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Cajas obtenidas por todas las oficinas</returns>
        public List<Caja.Entities.Caja> ListarCajaAllOficinas(Caja.Entities.Caja pCaja, Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarCajaAllOficinas(pCaja, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarCajaAllOficinas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Caja que tenga datafono
        /// </summary>
        /// <returns>Conjunto de Cajas obtenidos</returns>
        public List<Caja.Entities.Caja> ListarComboCajaXDatafono(Usuario pUsuario)
        {
            try
            {
                return DACaja.ListarComboCajaXDatafono(pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CajaBusiness", "ListarComboCajaXDatafono", ex);
                return null;
            }
        }
        
    }
}
