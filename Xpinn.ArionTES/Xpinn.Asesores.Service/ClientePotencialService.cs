using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClientePotencialService
    {
        private ClientePotencialBusiness busCliente;
        private ExcepcionBusiness excepBusinnes;

        public ClientePotencialService()
        {
            busCliente = new ClientePotencialBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110101"; } }

        public ClientePotencial CrearCliente(ClientePotencial pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.CrearClientePotencial(pAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "CrearCliente", ex);
                return null;
            }
        }

        public bool CrearClientes(List<ClientePotencial> pClientes, Usuario pUsuario,int limpiar , List<ErroresCarga> pErrores)
        {
            try
            {
                return busCliente.CrearClientesPotenciales(pClientes, pUsuario ,limpiar, pErrores);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "CrearCliente", ex);
                return false;
            }
        }

        public void EliminarCliente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                busCliente.EliminarClientePotencial(pId, pUsuario);

            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("AsesoresServices", "EliminarCliente", ex);
            }
        }

        public ClientePotencial ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarClientePotencial(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarCliente", ex);
                return null;
            }
        }

        public List<ClientePotencial> ListarCliente(ClientePotencial pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarClientePotencial(pAseEntiCliente, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarCliente", ex);
                return null;
            }
        }

        public ClientePotencial ActualizarCliente(ClientePotencial pAseEntiCliente, Usuario pUsuario)
        {
            //try
            //{
                return busCliente.ActualizarClientePotencial(pAseEntiCliente, pUsuario);
            //}
            //catch (Exception ex)
            //{
            //    excepBusinnes.Throw("ServiceCliente", "ActualizarCliente", ex);
            //    return null;
            //}
        }
        public ClientePotencial ConsultarClienteyaExistente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarClienteyaExistente(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarCliente", ex);
                return null;
            }
        }

        public ClientePotencial Consultarusuario(Int64 pIdUsuario, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarUsuario(pIdUsuario, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarUsuario", ex);
                return null;
            }
        }
    }
}