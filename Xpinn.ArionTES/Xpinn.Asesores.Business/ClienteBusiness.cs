using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Comun.Entities;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class ClienteBusiness : GlobalData
    {
        private ClienteData dataCliente;
        protected AsUdMotivoAfiliacionData dataMotAfiliacion;

        public ClienteBusiness()
        {
            dataCliente = new ClienteData();
            dataMotAfiliacion = new AsUdMotivoAfiliacionData();
        }

        public Cliente CrearCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAseEntiCliente = dataCliente.CrearCliente(pAseEntiCliente, pUsuario);
                    //dataMotAfiliacion.CrearMotivoAfiliacion(pAseEntiCliente,pUsuario);
                    ts.Complete();
                }
                return pAseEntiCliente;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessCliente", "CrearCliente", ex);
                return null;
            }
        }

        public void EliminarCliente(Int64 pIdCliente, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    dataCliente.EliminarCliente(pIdCliente, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "EliminarCliente", ex);
            }
        }

        public Cliente ConsultarCliente(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ConsultarCliente(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ConsultarCliente", ex);
                return null;
            }

        }

        public List<Cliente> ListarCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ListarCliente(pAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarCliente", ex);
                return null;
            }
        }

        public Cliente ConsultarClienteEjecutivo(Int64 pIdAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ConsultarClienteEjecutivo(pIdAseEntiCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ConsultarCliente", ex);
                return null;
            }

        }

        public Cliente ConsultarClienteEjecutivorepomora(Int64 radicado, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ConsultarClienteEjecutivorepomora(radicado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ConsultarClienteEjecutivorepomora", ex);
                return null;
            }

        }
      


        public List<Cliente> ListarClientesEjecutivo(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            try
            {
                return dataCliente.ListarClientesEjecutivo(pCliente, pUsuario, opcion, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }
        public List<Cliente> ListarClientesEjecutivopotencial(Cliente pCliente, Usuario pUsuario, int opcion, String filtro)
        {
            try
            {
                return dataCliente.ListarClientesEjecutivopotencial(pCliente, pUsuario, opcion, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }
        public Cliente guardarobservacion(Usuario pUsuario, DateTime fecha, string observacion, string cod_persona, string numero_radicacion, string usuario,int tipo)
        {
            try
            {
                return dataCliente.guardarobservacion(pUsuario, fecha, observacion, cod_persona, numero_radicacion, usuario, tipo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarClientesEjecutivo", ex);
                return null;
            }
        }

        public List<Cliente> ListarCodeudores(Int64 numero_radicacion, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ListarCodeudores(numero_radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarCodeudores", ex);
                return null;
            }
        }

        public List<Cliente> ListarUbicacionClientes(AgendaActividad agActividad, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ListarUbicacionClientes(agActividad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarUbicacionClientes", ex);
                return null;
            }
        }

        public List<Cliente> ListarClientesPersona(Cliente iCliente, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ListarClientesPersona(iCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarClientesPersona", ex);
                return null;
            }
        }
        public List<Cliente> ListarClientesPersonageo(Cliente iCliente, Usuario pUsuario)
        {
            try
            {
                return dataCliente.ListarClientesPersonageo(iCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ListarClientesPersona", ex);
                return null;
            }
        } 

        public Cliente ActualizarCliente(Cliente pAseEntiCliente, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAseEntiCliente = dataCliente.ActualizarCliente(pAseEntiCliente, pUsuario);
                    ts.Complete();
                }
                return pAseEntiCliente;
            }
            catch (TransactionException ex)
            {
                BOExcepcion.Throw("BusinessCliente", "ActualizarCliente", ex);
                return null;
            }
        }

    }
}