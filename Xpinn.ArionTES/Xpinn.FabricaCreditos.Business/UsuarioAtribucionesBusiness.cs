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
    /// Objeto de negocio para UsuarioAtribuciones
    /// </summary>
    public class UsuarioAtribucionesBusiness : GlobalData
    {
        private UsuarioAtribucionesData DAUsuarioAtribuciones;

        /// <summary>
        /// Constructor del objeto de negocio para UsuarioAtribuciones
        /// </summary>
        public UsuarioAtribucionesBusiness()
        {
            DAUsuarioAtribuciones = new UsuarioAtribucionesData();
        }

        /// <summary>
        /// Crea un UsuarioAtribuciones
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones creada</returns>
        public UsuarioAtribuciones CrearUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarioAtribuciones = DAUsuarioAtribuciones.CrearUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);

                    ts.Complete();
                }

                return pUsuarioAtribuciones;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "CrearUsuarioAtribuciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un UsuarioAtribuciones
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones modificada</returns>
        public UsuarioAtribuciones ModificarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUsuarioAtribuciones = DAUsuarioAtribuciones.ModificarUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);

                    ts.Complete();
                }

                return pUsuarioAtribuciones;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "ModificarUsuarioAtribuciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un UsuarioAtribuciones
        /// </summary>
        /// <param name="pId">Identificador de UsuarioAtribuciones</param>
        public void EliminarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUsuarioAtribuciones.EliminarUsuarioAtribuciones(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "EliminarUsuarioAtribuciones", ex);
            }
        }

        /// <summary>
        /// Obtiene un UsuarioAtribuciones
        /// </summary>
        /// <param name="pId">Identificador de UsuarioAtribuciones</param>
        /// <returns>Entidad UsuarioAtribuciones</returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DAUsuarioAtribuciones.ConsultarUsuarioAtribuciones(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "ConsultarUsuarioAtribuciones", ex);
                return null;
            }
        }
                
        /// <summary>
        /// Permite saber si un usuario tiene una atribuciòn especifica
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="?"></param>
        /// <param name="?"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public UsuarioAtribuciones ConsultarUsuarioAtribuciones(Int64 pId, Int64 pTip, Usuario pUsuario)
        {
            try
            {
                return DAUsuarioAtribuciones.ConsultarUsuarioAtribuciones(pId, pTip, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "ConsultarUsuarioAtribuciones", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarioAtribuciones">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de UsuarioAtribuciones obtenidos</returns>
        public List<UsuarioAtribuciones> ListarUsuarioAtribuciones(UsuarioAtribuciones pUsuarioAtribuciones, Usuario pUsuario)
        {
            try
            {
                return DAUsuarioAtribuciones.ListarUsuarioAtribuciones(pUsuarioAtribuciones, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UsuarioAtribucionesBusiness", "ListarUsuarioAtribuciones", ex);
                return null;
            }
        }





        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<UsuarioAtribuciones> ListasDesplegables(String ListaSolicitada, Usuario pUsuario)
        {
            try
            {
                return DAUsuarioAtribuciones.ListasDesplegables(ListaSolicitada, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }

    }
}