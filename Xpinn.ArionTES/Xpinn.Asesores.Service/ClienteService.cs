using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    //Actualizado
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ClienteService
    {
        private ClienteBusiness busCliente;
        private ExcepcionBusiness excepBusinnes;

        public ClienteService()
        {
            busCliente = new ClienteBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110102"; } }
        public string CodigoProgramaReal { get { return "110105"; } }
        public string CodigoProgramaRealRecuperaciones { get { return "110106"; } }
        public string CodigoProgramaRealRecuperacionesDetalle { get { return "110107"; } }        
        public string CodigoProgramaReportesAsesores { get { return "110302"; } }
        public string CodigoProgRepoMora { get { return "60205"; } }
        public Cliente CrearCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.CrearCliente(pAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "CrearCliente", ex);
                return null;
            }
        }

        public void EliminarCliente(Int64 pId, Usuario pUsuario)
        {
            try
            {
                busCliente.EliminarCliente(pId, pUsuario);

            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("AsesoresServices", "EliminarCliente", ex);
            }
        }

        public Cliente ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarCliente(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarCliente", ex);
                return null;
            }
        }

        public List<Cliente> ListarCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarCliente(pAseEntiCliente, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarCliente", ex);
                return null;
            }
        }

        public Cliente ConsultarClienteEjecutivo(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarClienteEjecutivo(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarCliente", ex);
                return null;
            }
        }

        public Cliente ConsultarClienteEjecutivorepomora(Int64 radicado, Usuario pUsuario)
        {
            try
            {
                return busCliente.ConsultarClienteEjecutivorepomora(radicado, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ConsultarClienteEjecutivorepomora", ex);
                return null;
            }
        }
        

        public List<Cliente> ListarClientesEjecutivo(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            try
            {
                return busCliente.ListarClientesEjecutivo(pCliente, pUsuario, opcion, filtro);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }
        public List<Cliente> ListarClientesEjecutivopotencial(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            try
            {
                return busCliente.ListarClientesEjecutivopotencial(pCliente, pUsuario, opcion, filtro);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }
        public Cliente guardarobservacion(Usuario pUsuario, DateTime fecha, string observacion, string cod_persona, string numero_radicacion, string usuario, int tipo)
        {
            try
            {
                return busCliente.guardarobservacion(pUsuario, fecha, observacion, cod_persona, numero_radicacion, usuario, tipo);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }

        public List<Cliente> ListarCodeudores(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarCodeudores(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarCodeudores", ex);
                return null;
            }
        }

        public List<Cliente> ListarUbicacionClientes(AgendaActividad agActividad, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarUbicacionClientes(agActividad, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarUbicacionClientes", ex);
                return null;
            }
        }

        public List<Cliente> ListarClientesPersona(Cliente iCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarClientesPersona(iCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarClientesPersona", ex);
                return null;
            }
        }
        public List<Cliente> ListarClientesPersonageo(Cliente iCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ListarClientesPersonageo(iCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ListarClientesPersona", ex);
                return null;
            }
        }

        public Cliente ActualizarCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return busCliente.ActualizarCliente(pAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceCliente", "ActualizarCliente", ex);
                return null;
            }
        }
    }
}