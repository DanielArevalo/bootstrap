using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
 
namespace Xpinn.Cartera.Business
{
 
    public class DebitoAutomaticoBusiness : GlobalBusiness
    {
 
        private DebitoAutomaticoData DADebitoAutomatico;
 
        public DebitoAutomaticoBusiness()
        {
            DADebitoAutomatico = new DebitoAutomaticoData();
        }
 
        public DebitoAutomatico CrearDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDebitoAutomatico = DADebitoAutomatico.CrearDebitoAutomatico(pDebitoAutomatico, pusuario);
 
                    ts.Complete();
 
                }
 
                return pDebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "CrearClasificacion", ex);
                return null;
            }
        }
 
 
        public DebitoAutomatico ModificarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDebitoAutomatico = DADebitoAutomatico.ModificarDebitoAutomatico(pDebitoAutomatico, pusuario);
 
                    ts.Complete();
 
                }
 
                return pDebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ModificarClasificacion", ex);
                return null;
            }
        }
 
 
        public void EliminarDebitoAutomatico(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DADebitoAutomatico.EliminarDebitoAutomatico(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "EliminarClasificacion", ex);
            }
        }
 
 
        public DebitoAutomatico ConsultarDebitoAutomatico(Int64 pId, Usuario pusuario)
        {
            try
            {
                DebitoAutomatico DebitoAutomatico = new DebitoAutomatico();
                DebitoAutomatico = DADebitoAutomatico.ConsultarDebitoAutomatico(pId, pusuario);
                return DebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ConsultarDebitoAutomatico", ex);
                return null;
            }
        }

        public DebitoAutomatico ConsultarDatosCliente(string pId, Usuario pusuario)
        {
            try
            {
                DebitoAutomatico DebitoAutomatico = new DebitoAutomatico();
                DebitoAutomatico = DADebitoAutomatico.ConsultarDatosCliente(pId, pusuario);
                return DebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ConsultarDatosCliente", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                return DADebitoAutomatico.ListarDebitoAutomatico(pDebitoAutomatico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ListarDebitoAutomatico", ex);
                return null;
            }
        }
        public List<DebitoAutomatico> ListarProductosClientes(DebitoAutomatico pDebitoAutomatico,Usuario pUsuario)
        {
            try
            {
                return DADebitoAutomatico.ListarProductosClientes(pDebitoAutomatico,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ListarProductosClientes", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarProductosAhorrosClientes(DebitoAutomatico pDebitoAutomatico, Usuario pUsuario)
        {
            try
            {
                return DADebitoAutomatico.ListarProductosAhorrosClientes(pDebitoAutomatico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ListarProductosAhorrosClientes", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarProductosClientesDebAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pUsuario)
        {
            try
            {
                return DADebitoAutomatico.ListarProductosClientesDebAutomatico(pDebitoAutomatico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ListarProductosClientesDebAutomatico", ex);
                return null;
            }
        }
        public List<DebitoAutomatico> ListarDatosClientes(DebitoAutomatico pDebitoAutomatico,Int64 cuenta, Usuario pUsuario)
        {
            try
            {
                return DADebitoAutomatico.ListarDatosClientes(pDebitoAutomatico, cuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoBusiness", "ListarDatosClientes", ex);
                return null;
            }
        }

    }
}
