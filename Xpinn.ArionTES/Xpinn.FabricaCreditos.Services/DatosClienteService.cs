using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Cliente
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DatosClienteService
    {
        private DatosClienteBusiness BOdatosCliente;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DatosCliente
        /// </summary>
        public DatosClienteService()
        {
            BOdatosCliente = new DatosClienteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "SEG010"; } }
        public string CodigoProgramamodifica { get { return "100150"; } }
        
        /// <summary>
        /// Crea un Cliente
        /// </summary>
        /// <param name="pEntity">Entidad Cliente</param>
        /// <returns>Entidad creada</returns>
        public DatosCliente CrearCliente(DatosCliente pCliente, Usuario pUsuario)
        {
            try
            {
                return BOdatosCliente.CrearCliente(pCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteService", "CrearCliente", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Cliente
        /// </summary>
        /// <param name="pEntity">Entidad Programa</param>
        /// <returns>Entidad modificada</returns>
        public DatosCliente ModificarCliente(DatosCliente pCliente, Usuario pUsuario)
        {
            try
            {
                return BOdatosCliente.ModificarCliente(pCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteService", "ModificarCliente", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un Cliente
        /// </summary>
        /// <param name="pId">identificador del Programa</param>
        public void EliminarCliente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOdatosCliente.EliminarCliente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteService", "EliminarCliente", ex);
            }
        }




        /// <summary>
        /// Obtiene un Programa
        /// </summary>
        /// <param name="pId">identificador del Programa</param>
        /// <returns>Programa consultado</returns>
        public DatosCliente ConsultarCliente(Int64 cId, Usuario pUsuario)
        {
            try
            {
                return BOdatosCliente.ConsultarCliente(cId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProgramaService", "ConsultarPrograma", ex);
                return null;
            }
        }





        /// <summary>
        /// Obtiene la lista de Clientes dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Clientes obtenidos</returns>
        public List<DatosCliente> ListarClientes(DatosCliente pClientes, Usuario pUsuario, String pId)
        {
            try
            {
                return BOdatosCliente.ListarClientes(pClientes, pUsuario, pId);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteClientesService", "ListarClientes", ex);
                return null;
            }
        }
        


        /// <summary>
        /// Obtiene  menu Actividades
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Actividades obtenidos</returns>
        public List<DatosCliente> ListasActividades(DatosCliente pDatosCliente, Usuario pUsuario)
        {
            try
            {
                return BOdatosCliente.ListarActividades(pDatosCliente, pUsuario); 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene  menu Tipos Documento
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos Documento obtenidos</returns>
        public List<DatosCliente> ListasTiposdoc(DatosCliente pDatosCliente, Usuario pUsuario)
        {
            try
            {
                return BOdatosCliente.ListarTiposDoc(pDatosCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene  listas desplegables
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de elementos obtenidos</returns>
        public List<DatosCliente> ListasDesplegables(DatosCliente pDatosCliente, Usuario pUsuario, String ListaSolicitada)
        {
            try
            {
                return BOdatosCliente.ListasDesplegables(pDatosCliente, pUsuario, ListaSolicitada);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DatosClienteService", "ListarListasMenu", ex);
                return null;
            }
        }



    }
}
