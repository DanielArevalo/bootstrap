using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class ClienteExtractoBusiness : GlobalData
    {
        private ClienteExtractoData DAClienteExtracto;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public ClienteExtractoBusiness()
        {
            DAClienteExtracto = new ClienteExtractoData();
        }

        /// <summary>
        /// Obtiene la lista de ClienteExtractos
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de ClienteExtractos obtenidas</returns>        
        public List<ClienteExtracto> ListarClienteExtractos(ClienteExtracto pClienteExtracto, string Filtro, Usuario pUsuario)
        {
            try
            {
                return DAClienteExtracto.ListarClienteExtractos(pClienteExtracto,Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoBusiness", "ListarClienteExtractos", ex);
                return null;
            }
        }


        public List<ClienteExtracto> ListarExtractoCliente(string filtro, Usuario vUsuario)
        {
            try
            {
                return DAClienteExtracto.ListarExtractoCliente(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoBusiness", "ListarExtractoCliente", ex);
                return null;
            }
        }



        public void GenerarExtractoClientes(string Filtro, Usuario vUsuario)
        {
            try
            {
                DAClienteExtracto.GenerarExtractoClientes(Filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ClienteExtractoBusiness", "GenerarExtractoClientes", ex);
            }
        }



    }
}