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
    /// <summary>
    /// Objeto de negocio para FabricaCreditos
    /// </summary>
    public class DatosClienteBusiness : GlobalData
    {
        private DatosClienteData DAcliente;

        /// <summary>
        /// Constructor del objeto de negocio para cliente
        /// </summary>
        public DatosClienteBusiness()
        {
            DAcliente = new DatosClienteData();
        }

        /// <summary>
        /// Crea un Cliente
        /// </summary>
        /// <param name="pEntity">Entidad Cliente</param>
        /// <returns>Entidad creada</returns>
        public DatosCliente CrearCliente(DatosCliente pCliente, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCliente = DAcliente.CrearCliente(pCliente, pUsuario);

                    ts.Complete();
                }

                return pCliente;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "CrearCliente", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Programa
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad modificada</returns>
        public DatosCliente ModificarCliente(DatosCliente pCliente, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCliente = DAcliente.ModificarCliente(pCliente, pUsuario);

                    ts.Complete();
                }

                return pCliente;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ModificarPrograma", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un Cliente
        /// </summary>
        /// <param name="pId">identificador del Cliente</param>
        public void EliminarCliente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAcliente.EliminarCliente(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "EliminarCliente", ex);
            }
        }

        /// <summary>
        /// Obtiene la lista de Clientes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Programas obtenidos</returns>
        public List<DatosCliente> ListarClientes(DatosCliente pCliente, Usuario pUsuario, String pId)
        {
            try
            {
                return DAcliente.ListarCliente(pCliente, pUsuario, pId);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarClientes", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene un Cliente
        /// </summary>
        /// <param name="pId">identificador del Programa</param>
        /// <returns>Programa consultado</returns>
        public DatosCliente ConsultarCliente(Int64 cId, Usuario pUsuario)
        {
            try
            {
                return DAcliente.ConsultarCliente(cId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteBusiness", "ConsultarCliente", ex);
                return null;
            }
        }




        /// <summary>
        /// Obtiene la lista de Actividades dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Programas obtenidos</returns>
        public List<DatosCliente> ListarActividades(DatosCliente pMenus, Usuario pUsuario)
        {
            try
            {
                return DAcliente.ListarActividades(pMenus, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarPrograma", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<DatosCliente> ListarTiposDoc(DatosCliente pMenus, Usuario pUsuario)
        {
            try
            {
                return DAcliente.ListarTiposDoc(pMenus, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de tipos documento dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de tipos documento obtenidos</returns>
        public List<DatosCliente> ListasDesplegables(DatosCliente pMenus, Usuario pUsuario, String ListaSolicitada)
        {
            try
            {
                return DAcliente.ListasDesplegables(pMenus, pUsuario, ListaSolicitada);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteBusiness", "ListarTiposDoc", ex);
                return null;
            }
        }


    }
}
