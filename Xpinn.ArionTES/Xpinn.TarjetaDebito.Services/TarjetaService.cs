using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Business;
using Xpinn.Util;

namespace Xpinn.TarjetaDebito.Services
{
   
    public class TarjetaService
    {
        public string CodigoPrograma { get { return "220501"; } }
        public string CodigoProgramaBloqueoDesbloqueo { get { return "220506"; } }


        AsignacionTarjetasBusiness BOAsignacionTarjetas;
        ExcepcionBusiness BOExcepcion;

        public TarjetaService()
        {
            BOAsignacionTarjetas = new AsignacionTarjetasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public List<Tarjeta> ListarAsignacionTarjetas(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarAsignacionTarjetas(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetasService", "ListarAsignacionTarjetas", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarOficina(Tarjeta pEntOficina, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarOficina(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarOficina", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTipoCuenta(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarTipoCuenta(pEntCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarTipoCuenta", ex);
                return null;
            }
        }
        public List<Tarjeta> ListarConvenio(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarConvenio(pEntCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarConvenio", ex);
                return null;
            }
        }

        public List<Tarjeta> Listartarjeta(Int64 filtro, Tarjeta pEntTarjeta, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.Listartarjeta(filtro, pEntTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "Listartarjeta", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarAhorros(Int64 filtro, Tarjeta pEntAhorros, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarAhorros(filtro, pEntAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarAhorros", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarCredito(Int64 filtro, Tarjeta pEntAhorros, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarCredito(filtro, pEntAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarCredito", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadas(int numeroDeDiasParaBloquearTarjetas, string ProductosenCuentaparaBloqueo, int tipo_bloqueo, Usuario usuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarTarjetasEnMoraYNoBloqueadas(numeroDeDiasParaBloquearTarjetas, ProductosenCuentaparaBloqueo, tipo_bloqueo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarTarjetasEnMoraYNoBloqueadas", ex);
                return null;
            }
        }

        public void EliminarAsignacion(Int64 pIdTarjeta, Usuario pUsuario)
        {
            try
            {
                BOAsignacionTarjetas.EliminarAsignacion(pIdTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "EliminarAsignacion", ex);
            }
        }

        public Tarjeta CrearAsignacion(Tarjeta pTarjeta, Usuario vUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.CrearAsignacion(pTarjeta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "CrearLineaServicio", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetasBloqueadasYAlDia(int ptipo_bloqueo,Usuario usuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarTarjetasBloqueadasYAlDia(ptipo_bloqueo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarTarjetasBloqueadasYAlDia", ex);
                return null;
            }
        }

        public Tarjeta ConsultarValoresCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarValoresCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarValoresCredito", ex);
                return null;
            }
        }
        public Tarjeta ConsultarValoresAhorros(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarValoresAhorro(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarValoresAhorros", ex);
                return null;
            }
        }


        public Tarjeta ConsultarAsignacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarAsignacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarAsignacion", ex);
                return null;
            }
        }
        public Tarjeta ConsultarNumTarjeta(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarNumTarjeta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarNumTarjeta", ex);
                return null;
            }
        }
        public Tarjeta ConsultarCuentas(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarCuentas(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarCuentas", ex);
                return null;
            }
        }

        public Tarjeta ConsultarTarjetaDeUnaCuenta(string numeroCuenta, Usuario pUsuario)
        {
            try
            {
                return BOAsignacionTarjetas.ConsultarTarjetaDeUnaCuenta(numeroCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaService", "ConsultarTarjetaDeUnaCuenta", ex);
                return null;
            }
        }

        public void CambiarEstadoTarjeta(Tarjeta tarjeta, EstadoTarjetaEnpacto estado, Usuario usuario)
        {
            try
            {
                BOAsignacionTarjetas.CambiarEstadoTarjeta(tarjeta, estado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaService", "CambiarEstadoTarjeta", ex);
            }
        }

        public Tarjeta ActualizarSaldoTarjeta(Tarjeta tarjeta, ref string error, Usuario usuario)
        {
            try
            {
               return BOAsignacionTarjetas.ActualizarSaldoTarjeta(tarjeta, ref error, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaService", "ActualizarSaldoTarjeta", ex);
                return null;
            }
        }


        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadasXSaldo(int numeroDeDiasParaBloquearTarjetas, Usuario usuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarTarjetasEnMoraYNoBloqueadasXSaldo(numeroDeDiasParaBloquearTarjetas, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarTarjetasEnMoraYNoBloqueadasXSaldo", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetasBloqueadasYAlDiaXSaldo(Usuario usuario)
        {
            try
            {
                return BOAsignacionTarjetas.ListarTarjetasBloqueadasYAlDiaXSaldo(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetasServices", "ListarTarjetasBloqueadasYAlDiaXSaldo", ex);
                return null;
            }
        }

    }
}
