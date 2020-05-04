using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.Asesores.Services
{
    /// <summary>
    /// Listado de clientes generado para la generación de extracto.
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClienteExtractoService
    {
        private ClienteExtractoBusiness BOClienteExtracto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public ClienteExtractoService()
        {
            BOClienteExtracto = new ClienteExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110112"; } }

        public List<ClienteExtracto> ListarClienteExtractos(ClienteExtracto ClienteExtracto,string Filtro, Usuario pUsuario)
        {
            try
            {
                return BOClienteExtracto.ListarClienteExtractos(ClienteExtracto, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoService", "ListarClienteExtractos", ex);
                return null;
            }
        }


        public List<ClienteExtracto> ListarExtractoCliente(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOClienteExtracto.ListarExtractoCliente(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoService", "ListarExtractoCliente", ex);
                return null;
            }
        }



        public void GenerarExtractoClientes(string Filtro, Usuario vUsuario)
        {
            try
            {
                BOClienteExtracto.GenerarExtractoClientes(Filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoService", "GenerarExtractoClientes", ex);
            }
        }



    }
}