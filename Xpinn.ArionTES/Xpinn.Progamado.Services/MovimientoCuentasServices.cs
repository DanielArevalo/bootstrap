using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Business;

namespace Xpinn.Programado.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MovimientoCuentasServices
    {
        private MovimientoCuentasBusiness BOMovimiento;
        private ExcepcionBusiness BOExcepcion;

        public MovimientoCuentasServices()
        {
            BOMovimiento = new MovimientoCuentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220402"; } }
        public string CodigoProgramaMotivoAp { get {return "220110"; } }
        public string CodigoProgramaVenci { get { return "220411"; } }

        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario, int estadoCuenta = 0)
        {
            try
            {
                return BOMovimiento.ListarAhorrosProgramado(pFiltro, pFechaApe, vUsuario, estadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ListarAhorrosProgramado", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarCuentasPersona(Int64 cod_persona, Usuario vUsuario)
        {
            try
            {
                return BOMovimiento.ListarCuentasPersona(cod_persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ListarCuentasPersona", ex);
                return null;
            }

        }

        public CuentasProgramado ConsultarAhorrosProgramado(String pNumeroProgramado, Usuario vUsuario)
        {
            try
            {
                return BOMovimiento.ConsultarAhorrosProgramado(pNumeroProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ConsultarAhorrosProgramado", ex);
                return null;
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleMovimiento(String pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOMovimiento.ListarDetalleMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ListarDetalleMovimiento", ex);
                return null;
            }
        }

        public void creaMotivoProgramadoServices(String pdescripcion, Usuario pUsuario) 
        {
            try
            {
                BOMovimiento.creaMotivoProgramadoBusiness(pdescripcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "creaMotivoProgramadoServices", ex);
            }
        }

        public void deleteMotivoProgramadoServices(Int64 idCodigo, Usuario pUsuario) 
        {
            try
            {
                BOMovimiento.deleteMotivoProgramadoBusiness(idCodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "deleteMotivoProgramadoServices", ex);
            }
        }

        public void updateMotivoProgramadoServices(Int64 pIdicodigo, Usuario pUsuario, String pDescripcion) 
        {
            try
            {
                BOMovimiento.updateMotivoProgramadoBusiness(pIdicodigo, pUsuario, pDescripcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "updateMotivoProgramadoServices", ex);
            }
        }

        public List<MotivoProgramadoE> getListaMotivoProgramadoServices(Usuario pUsuario, String pFiltro) 
        {
            try
            {
                return BOMovimiento.getListaMotivoProgramadoBusiness(pUsuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "getListaMotivoProgramadoServices", ex);
                return null;
            }
        }

        public MotivoProgramadoE getMotivoPByIdServices(Usuario pUsuario, Int64 pCodigo) 
        {
            try
            {
                return BOMovimiento.getMotivoPByIdBusiness(pUsuario, pCodigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "getMotivoPByIdServices", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarProgramadoAvencer(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOMovimiento.ListarProgramadoAvencer(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ListarProgramadoAvencer", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarAprobaciones(Usuario vUsuario)
        {
            try
            {
                return BOMovimiento.ListarAprobaciones(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasServices", "ListarAprobaciones", ex);
                return null;
            }
        }

    }
}
