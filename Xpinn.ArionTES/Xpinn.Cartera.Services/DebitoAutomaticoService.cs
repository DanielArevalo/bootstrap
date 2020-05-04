using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;

namespace Xpinn.Cartera.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DebitoAutomaticoService
    {

        private DebitoAutomaticoBusiness BODebitoAutomatico;
        private ExcepcionBusiness BOExcepcion;

        public DebitoAutomaticoService()
        {
            BODebitoAutomatico = new DebitoAutomaticoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60117"; } }

        public DebitoAutomatico CrearDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                pDebitoAutomatico = BODebitoAutomatico.CrearDebitoAutomatico(pDebitoAutomatico, pusuario);
                return pDebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "CrearDebitoAutomatico", ex);
                return null;
            }
        }


        public DebitoAutomatico ModificarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                pDebitoAutomatico = BODebitoAutomatico.ModificarDebitoAutomatico(pDebitoAutomatico, pusuario);
                return pDebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ModificarDebitoAutomatico", ex);
                return null;
            }
        }


        public void EliminarDebitoAutomatico(Int64 pId, Usuario pusuario)
        {
            try
            {
                BODebitoAutomatico.EliminarDebitoAutomatico(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "EliminarDebitoAutomatico", ex);
            }
        }


        public DebitoAutomatico ConsultarDebitoAutomatico(Int64 pId, Usuario pusuario)
        {
            try
            {
                DebitoAutomatico DebitoAutomatico = new DebitoAutomatico();
                DebitoAutomatico = BODebitoAutomatico.ConsultarDebitoAutomatico(pId, pusuario);
                return DebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ConsultarDebitoAutomatico", ex);
                return null;
            }
        }


        public List<DebitoAutomatico> ListarDebitoAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                return BODebitoAutomatico.ListarDebitoAutomatico(pDebitoAutomatico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ListarDebitoAutomatico", ex);
                return null;
            }
        }
        public List<DebitoAutomatico> ListarProductosClientes(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                return BODebitoAutomatico.ListarProductosClientes(pDebitoAutomatico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ListarProductosClientes", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarProductosAhorrosClientes(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                return BODebitoAutomatico.ListarProductosAhorrosClientes(pDebitoAutomatico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ListarProductosAhorrosClientes", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarProductosClientesDebAutomatico(DebitoAutomatico pDebitoAutomatico, Usuario pusuario)
        {
            try
            {
                return BODebitoAutomatico.ListarProductosClientesDebAutomatico(pDebitoAutomatico, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ListarProductosClientesDebAutomatico", ex);
                return null;
            }
        }

        public List<DebitoAutomatico> ListarDatosClientes(DebitoAutomatico pDebitoAutomatico, Int64 cuenta, Usuario pusuario)
        {
            try
            {
                return BODebitoAutomatico.ListarDatosClientes(pDebitoAutomatico,cuenta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ListarDatosClientes", ex);
                return null;
            }
        }


        public DebitoAutomatico ConsultarDatosCliente(string pId, Usuario pusuario)
        {
            try
            {
                DebitoAutomatico DebitoAutomatico = new DebitoAutomatico();
                DebitoAutomatico = BODebitoAutomatico.ConsultarDatosCliente(pId, pusuario);
                return DebitoAutomatico;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DebitoAutomaticoService", "ConsultarDatosCliente", ex);
                return null;
            }
        }
    }
}
