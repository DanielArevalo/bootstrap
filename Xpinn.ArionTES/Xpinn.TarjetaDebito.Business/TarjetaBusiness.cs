using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Data;
using Xpinn.Util;
using System.Transactions;

namespace Xpinn.TarjetaDebito.Business
{
    public class AsignacionTarjetasBusiness :GlobalBusiness
    {
        private TarjetaData DAAsignacionTarjeta;

        public AsignacionTarjetasBusiness()
        {
            DAAsignacionTarjeta = new TarjetaData();    }


        public List<Tarjeta> ListarAsignacionTarjetas(string  filtro, Usuario vUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarAsignacion(filtro, vUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("TarjetasBusiness", "ListarAsignacionTarjetas", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarOficina(Tarjeta pEntOficina, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarOficina(pEntOficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarOficina", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTipoCuenta(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarTipoCuenta(pEntCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarTipoCuenta", ex);
                return null;
            }
        }
        public List<Tarjeta> ListarConvenio(Tarjeta pEntCuenta, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarConvenio(pEntCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarConvenio", ex);
                return null;
            }
        }

        public List<Tarjeta> Listartarjeta(Int64 filtro, Tarjeta pEntTarjeta, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.Listartarjeta(filtro, pEntTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "Listartarjeta", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarAhorros(Int64 filtro, Tarjeta pEntAhorros, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarAhorros(filtro, pEntAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "Listartarjeta", ex);
                return null;
            }
        }
        public List<Tarjeta> ListarCredito(Int64 filtro, Tarjeta pEntAhorros, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarCredito(filtro, pEntAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public void EliminarAsignacion(Int64 pIdTarjeta, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAsignacionTarjeta.Eliminar(pIdTarjeta, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "EliminarAsignacion", ex);
            }
        }

        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadas(int numeroDeDiasParaBloquearTarjetas, string ProductosenCuentaparaBloqueo, int tipo_bloqueo, Usuario usuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarTarjetasEnMoraYNoBloqueadas(numeroDeDiasParaBloquearTarjetas, ProductosenCuentaparaBloqueo, tipo_bloqueo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarTarjetasEnMoraYNoBloqueadas", ex);
                return null;
            }
        }

        public Tarjeta CrearAsignacion(Tarjeta pTarjeta, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAsignacionTarjeta.CrearAsignacionTarjeta(pTarjeta, vUsuario);
                    ts.Complete();
                }

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "CrearAsignacion", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetasBloqueadasYAlDia(int ptipo_bloqueo, Usuario usuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarTarjetasBloqueadasYAlDia(ptipo_bloqueo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ListarTarjetasBloqueadasYAlDia", ex);
                return null;
            }
        }

        public Tarjeta ConsultarValoresCredito(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Tarjeta pTarjeta = new Tarjeta();

                pTarjeta = DAAsignacionTarjeta.ConsultarValoresCredito(pId, vUsuario);

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarValoresCredito", ex);
                return null;
            }
        }
        public Tarjeta ConsultarValoresAhorro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Tarjeta pTarjeta = new Tarjeta();

                pTarjeta = DAAsignacionTarjeta.ConsultarValoresAhorros(pId, vUsuario);

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarValoresAhorro", ex);
                return null;
            }
        }

        public Tarjeta ConsultarAsignacion(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Tarjeta pTarjeta = new Tarjeta();

                pTarjeta = DAAsignacionTarjeta.ConsultarAsignacion(pId, vUsuario);

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarAsignacion", ex);
                return null;
            }
        }


        public Tarjeta ConsultarNumTarjeta(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Tarjeta pTarjeta = new Tarjeta();

                pTarjeta = DAAsignacionTarjeta.ConsultarNumTarjeta(pId, vUsuario);

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarNumTarjeta", ex);
                return null;
            }
        }

        public Tarjeta ConsultarTarjetaDeUnaCuenta(string numeroCuenta, Usuario pUsuario)
        {
            try
            {
                return DAAsignacionTarjeta.ConsultarTarjetaDeUnaCuenta(numeroCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaBusiness", "ConsultarTarjetaDeUnaCuenta", ex);
                return null;
            }
        }

        public void CambiarEstadoTarjeta(Tarjeta tarjeta, EstadoTarjetaEnpacto estado, Usuario usuario)
        {
            try
            {
                DAAsignacionTarjeta.CambiarEstadoTarjeta(tarjeta, estado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaBusiness", "CambiarEstadoTarjeta", ex);
            }
        }

        public Tarjeta ConsultarCuentas(Int64 pId, Usuario vUsuario)
        {
            try
            {
                Tarjeta pTarjeta = new Tarjeta();

                pTarjeta = DAAsignacionTarjeta.ConsultarCuentas(pId, vUsuario);

                return pTarjeta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ConsultarCuentas", ex);
                return null;
            }
        }

        public Tarjeta ActualizarSaldoTarjeta(Tarjeta tarjeta, ref string error,  Usuario usuario)
        {
            try
            {
                return DAAsignacionTarjeta.ActualizarSaldoTarjeta(tarjeta, ref error, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TarjetaBusiness", "ActualizarSaldoTarjeta", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetasEnMoraYNoBloqueadasXSaldo(int numeroDeDiasParaBloquearTarjetas, Usuario usuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarTarjetasEnMoraYNoBloqueadasXSaldo(numeroDeDiasParaBloquearTarjetas, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionTarjetasBusiness", "ListarTarjetasEnMoraYNoBloqueadasXSaldo", ex);
                return null;
            }
        }


        public List<Tarjeta> ListarTarjetasBloqueadasYAlDiaXSaldo(Usuario usuario)
        {
            try
            {
                return DAAsignacionTarjeta.ListarTarjetasBloqueadasYAlDiaXSaldo(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AsignacionTarjetaBusinness", "ListarTarjetasBloqueadasYAlDiaXSaldo", ex);
                return null;
            }
        }



    }
}
