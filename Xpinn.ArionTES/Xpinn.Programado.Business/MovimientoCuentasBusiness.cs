using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Data;

namespace Xpinn.Programado.Business
{
    public class MovimientoCuentasBusiness : GlobalBusiness
    {
        private MovimientoCuentasData DAMovimiento;
        private MotivoProgramadoData DAProgramado;

        public MovimientoCuentasBusiness()
        {
            DAMovimiento = new MovimientoCuentasData();
            DAProgramado = new MotivoProgramadoData();
        }


        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario, int estadoCuenta = 0)
        {
            try
            {
                return DAMovimiento.ListarAhorrosProgramado(pFiltro, pFechaApe, vUsuario, estadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ListarAhorrosProgramado", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarCuentasPersona(Int64 cod_persona, Usuario vUsuario)
        {
            try
            {
                return DAMovimiento.ListarCuentasPersona(cod_persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ListarCuentasPersona", ex);
                return null;
            }

        }

        public CuentasProgramado ConsultarAhorrosProgramado(String pNumeroProgramado, Usuario vUsuario)
        {
            try
            {
                return DAMovimiento.ConsultarAhorrosProgramado(pNumeroProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ConsultarAhorrosProgramado", ex);
                return null;
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleMovimiento(String pNumeroCuenta, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return DAMovimiento.ListarDetalleMovimiento(pNumeroCuenta, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ListarDetalleMovimiento", ex);
                return null;
            }
        }


        public void creaMotivoProgramadoBusiness(String pdescripcion, Usuario pUsuario) 
        {
            try
            {
                DAProgramado.creaMotivoProgramado(pdescripcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "creaMotivoProgramadoBusiness", ex);
            }
        }

        public void deleteMotivoProgramadoBusiness(Int64 idCodigo, Usuario pUsuario) 
        {
            try
            {
                DAProgramado.deleteMotivoProgramado(idCodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "deleteMotivoProgramadoBusiness", ex);
            }
        }

        public void updateMotivoProgramadoBusiness(Int64 pIdicodigo, Usuario pUsuario, String pDescripcion) 
        {
            try
            {
                DAProgramado.updateMotivoProgramado(pIdicodigo, pUsuario, pDescripcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "updateMotivoProgramadoBusiness", ex);
            }
        }

        public List<MotivoProgramadoE> getListaMotivoProgramadoBusiness(Usuario pUsuario, String pFiltro) 
        {
            try
            {
                return DAProgramado.getListaMotivoProgramado(pUsuario, pFiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "getListaMotivoProgramadoBusiness", ex);
                return null;
            }
        }

        public MotivoProgramadoE getMotivoPByIdBusiness(Usuario pUsuario, Int64 pCodigo) 
        {
            try
            {
                return DAProgramado.getMotivoPById(pUsuario, pCodigo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "getMotivoPByIdBusiness", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarProgramadoAvencer(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAMovimiento.ListarProgramadoAvencer(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ListarProgramadoAvencer", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarAprobaciones( Usuario vUsuario)
        {
            try
            {
                return DAMovimiento.ListarAprobaciones( vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovimientoCuentasBusiness", "ListarAprobaciones", ex);
                return null;
            }
        }
    }
}
