using System; 
using System.Collections.Generic; 
using System.Text; 
using Xpinn.Util; 
using System.ServiceModel; 
using Xpinn.Nomina.Entities; 
using Xpinn.Nomina.Business; 
 
namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoContratoService
    {

        private ContratacionBusiness BOContratacion;
        private ExcepcionBusiness BOExcepcion;

        public TipoContratoService()
        {
            BOContratacion = new ContratacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250611"; } }
        public string CodigoProgramaTiposRetiroContrato { get { return "250616"; } }
        


        public TipoContrato CrearContratacion(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                pContratacion = BOContratacion.CrearContratacion(pContratacion, pusuario);
                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "CrearContratacion", ex);
                return null;
            }
        }


        public TipoContrato CrearTipoRetirocontrato(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                pContratacion = BOContratacion.CrearTipoRetirocontrato(pContratacion, pusuario);
                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "CrearTipoRetirocontrato", ex);
                return null;
            }
        }

        public TipoContrato ModificarTipoRetirocontrato(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                pContratacion = BOContratacion.ModificarTipoRetirocontrato(pContratacion, pusuario);
                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "MofdificarTipoRetirocontrato", ex);
                return null;
            }
        }


        public TipoContrato ModificarContratacion(TipoContrato pContratacion, Usuario pusuario)
        {
            try
            {
                pContratacion = BOContratacion.ModificarContratacion(pContratacion, pusuario);
                return pContratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ModificarContratacion", ex);
                return null;
            }
        }


        public void EliminarContratacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOContratacion.EliminarContratacion(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "EliminarContratacion", ex);
            }
        }


        public TipoContrato ConsultarContratacion(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoContrato Contratacion = new TipoContrato();
                Contratacion = BOContratacion.ConsultarContratacion(pId, pusuario);
                return Contratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ConsultarContratacion", ex);
                return null;
            }
        }

        public TipoContrato ConsultarTipoRetiroContrato(Int64 pId, Usuario pusuario)
        {
            try
            {
                TipoContrato Contratacion = new TipoContrato();
                Contratacion = BOContratacion.ConsultarTipoRetiroContrato(pId, pusuario);
                return Contratacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ConsultarTipoRetiroContrato", ex);
                return null;
            }
        }

        public List<TipoContrato> ListarTipoContratos(Usuario usuario)
        {
            try
            {
                return BOContratacion.ListarTipoContratos(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ListarTipoContratos", ex);
                return null;
            }
        }

        public List<TipoContrato> ListarContratacion(string pid, Usuario pusuario)
        {
            try
            {
                return BOContratacion.ListarContratacion(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ListarContratacion", ex);
                return null;
            }
        }


        public List<TipoContrato> ListarTipoRetiroContrato(string pid, Usuario pusuario)
        {
            try
            {
                return BOContratacion.ListarTipoRetiroContrato(pid, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ContratacionService", "ListarTipoRetiroContrato", ex);
                return null;
            }
        }



    }
}