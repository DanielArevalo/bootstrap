using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Business;
using Xpinn.Util;

namespace Xpinn.TarjetaDebito.Services
{

    public class CuentaService
    {
        public string CodigoPrograma { get { return "220503"; } }
        public string CodigoProgramaMovimiento { get { return "220504"; } }
        public string CodigoProgramaTarjeta { get { return "220505"; } }
        public string CodigoProgramaBloqueo { get { return "220506"; } }
        public string CodigoProgramaConciliacion { get { return "220507"; } }
        public string CodigoProgramaAsignacion { get { return "220508"; } }
        public string CodigoProgramaMovimientoFinancial { get { return "220509"; } }
        public string CodigoProgramaClientesCoopcentral { get { return "220510"; } }
        public string CodigoProgramaMovimientoCoopcentral { get { return "220511"; } }

        CuentaBusiness BOCuentas;
        ExcepcionBusiness BOExcepcion;

        public CuentaService()
        {
            BOCuentas = new CuentaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public List<Cuenta> ListarCuenta(Cuenta pCuenta, string pgeneral, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarCuenta(pCuenta, pgeneral, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "ListarCuentas", ex);
                return null;
            }
        }

        public Boolean AplicarMovimientos(string pConvenio, DateTime fecha, List<Movimiento> lstMovimiento, Usuario pUsuario, ref string Error, ref Int64 pCodOpe, int pTipoAplicacion)
        {
            try
            {                
                return BOCuentas.AplicarMovimientos(pConvenio, fecha, lstMovimiento, pUsuario, ref Error, ref pCodOpe, pTipoAplicacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "AplicarMovimientos", ex);
                return false;
            }
        }

        public Movimiento CrearMovimiento(Movimiento movimiento, long cod_ope, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.CrearMovimiento(movimiento, cod_ope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "CrearMovimiento", ex);
                return null;
            }
        }

        public Boolean ValidarCuenta(string pConvenio, string pTarjeta, string pCuenta, string pTipoTran, Decimal pValor, string pFecha, bool bValidarSaldo, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ValidarCuenta(pConvenio, pTarjeta, pCuenta, pTipoTran, pValor, pFecha, bValidarSaldo, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return false;
            }
        }

        public int? TipoTranComision(string ptipo_cuenta, int? ptipo_tran)
        {
            return BOCuentas.TipoTranComision(ptipo_cuenta, ptipo_tran);
        }

        public int? HomologaTipoTran(string ptipocuenta, string tipotransaccion, int? _tipo_convenio = 0)
        {
            return BOCuentas.HomologaTipoTran(ptipocuenta, tipotransaccion, _tipo_convenio);
        }

        public Movimiento ConsultarMovimiento(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ConsultarMovimiento(ptipo_convenio, pTarjeta, pOperacion, ptipoTransaccion, pDocumento, pFecha, pValor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "ConsultarMovimiento", ex);
                return null;
            }
        }

        public Movimiento DatosDeAplicacion(Int32? pnum_tran, string pnumero_cuenta, Int64? pcod_ope, Int64? pcod_persona, DateTime pFecha, decimal pValor, int? pTipoTran, string pOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.DatosDeAplicacion(pnum_tran, pnumero_cuenta, pcod_ope, pcod_persona, pFecha, pValor, pTipoTran, pOperacion, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "DatosDeAplicacion", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetas(Tarjeta pTarjeta, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarTarjetas(pTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarTarjetas", ex);
                return null;
            }
        }


        public string ConsultarTipoCuenta(string pConvenio, string pCuenta, ref int? pCodOfi, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ConsultarTipoCuenta(pConvenio, pCuenta, ref pCodOfi, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                Error += ex.Message;
                return "";
            }
        }

        public bool VerificarSiTarjetaExiste(Tarjeta tarjeta, Usuario usuario)
        {
            try
            {
                return BOCuentas.VerificarSiTarjetaExiste(tarjeta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "VerificarSiTarjetaExiste", ex);
                return false;
            }
        }

        public Tarjeta CrearTarjeta(Tarjeta tarjeta, Usuario usuario)
        {
            try
            {
                return BOCuentas.CrearTarjeta(tarjeta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "CrearTarjeta", ex);
                return null;
            }
        }

        public decimal ConsultarSaldoCuenta(string pConvenio, string pCuenta, ref string pError, Usuario pUsuario)
        {
            return BOCuentas.ConsultarSaldoCuenta(pConvenio, pCuenta, ref pError, pUsuario);
        }

        public bool ComprobanteValorBanco(Int64 pNumComp, Int64 pTipoComp, ref decimal pValor, ref DateTime? pFecha, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ComprobanteValorBanco(pNumComp, pTipoComp, ref pValor, ref pFecha, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public List<Cuenta> ListarCuentaAsignacion(Cuenta pCuenta, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarCuentaAsignacion(pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarCuentaAsignacion", ex);
                return null;
            }
        }

        public Int64 AsignarCuenta(Cuenta pCuenta, Usuario pusuario)
        {
            try
            {
                return BOCuentas.AsignarCuenta(pCuenta, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaServices", "AsignarCuenta", ex);
                return 0;
            }
        }

        public bool ActualizarMovimiento(string pConvenio, Movimiento pMovimiento, Usuario pUsuario, ref string Error)
        {
            try
            {
                BOCuentas.ActualizarMovimiento(pConvenio, pMovimiento, pUsuario, ref Error);
                if (Error.Trim() != "")
                    return false;
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public List<TransaccionFinancial> ListarTransaccionesPendientesAplicarEnpacto(int ptipo_convenio, string pConvenio, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarTransaccionesPendientesAplicarEnpacto(ptipo_convenio, pConvenio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaServices", "ListarTransaccionesPendientesAplicarEnpacto", ex);
                return null;
            }
        }

        public bool CrearControlOperacion(string pConvenio, TransaccionFinancial pMovimiento, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.CrearControlOperacion(pConvenio, pMovimiento, ref pError, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public int TiposAplicacionEnpacto(int ptipo_convenio, string pConvenio, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.TiposAplicacionEnpacto(ptipo_convenio, pConvenio, ref pError, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public bool ActualizarMovimientoConciliacion(Int64 pnumTran, string pfechaCorte, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ActualizarMovimientoConciliacion(pnumTran, pfechaCorte, pUsuario);
            }
            catch
            {
                return false;
            }
        }

        public List<Movimiento> ListaTransaccionesSinConciliar(string pConvenio, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListaTransaccionesSinConciliar(pConvenio, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public string HomologarCuentas(string pTarjeta, string pCuenta, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.HomologarCuentas(pTarjeta, pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaServices", "HomologarCuentas", ex);
                return null;
            }
        }

        public List<Movimiento> ListarTipoTran(Int64 pNumTranTarjeta, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarTipoTran(pNumTranTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarTipoTran", ex);
                return null;
            }
        }

        public decimal ConsultarValor(Int64 pNumTranTarjeta, Int32 pTipoTran, Usuario pUsuario)
        {
            return BOCuentas.ConsultarValor(pNumTranTarjeta, pTipoTran, pUsuario);
        }

        public void AjustarMovimiento(string pConvenio, string pTipocuenta, Int64 pNumTranTarjeta, Int32 pTipoTran, decimal pValor, Usuario pUsuario, ref string Error)
        {
            try
            {
                BOCuentas.AjustarMovimiento(pConvenio, pTipocuenta, pNumTranTarjeta, pTipoTran, pValor, pUsuario, ref Error);
                return;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "AjustarMovimiento", ex);
                return;
            }
        }

        public List<TransaccionFinancial> ListarTransaccionesFinancial(string pConvenio, DateTime pFecha, Usuario pUsuario)
        {
            return BOCuentas.ListarTransaccionesFinancial(pConvenio, pFecha, pUsuario);
        }

        public Boolean AplicarMovimientosCoopcentral(string pConvenio, DateTime fecha, List<MovimientoCoopcentral> lstMovimiento, Usuario pUsuario, ref string Error, ref Int64 pCodOpe, int pTipoAplicacion)
        {
            try
            {
                return BOCuentas.AplicarMovimientosCoopcentral(pConvenio, fecha, lstMovimiento, pUsuario, ref Error, ref pCodOpe, pTipoAplicacion);
            }
            catch //(Exception ex)
            {
                //BOExcepcion.Throw("CuentaService", "AplicarMovimientosCoopcentral", ex);
                return false;
            }
        }

        public List<CuentaCoopcentral> ListarCuentaCoopcentral(Cuenta pCuenta, Usuario pUsuario)
        {
            try
            {
                return BOCuentas.ListarCuentaCoopcentral(pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarCuentaCoopcentral", ex);
                return null;
            }
        }

        public StreamWriter GenerarArchivoClientesCoopcentral(List<CuentaCoopcentral> lstConsulta, string separador, StreamWriter newfile, Usuario _usuario)
        {
            return BOCuentas.GenerarArchivoClientesCoopcentral(lstConsulta, separador, newfile, _usuario);
        }

        public string VerificarCampo(string pcampo)
        {
            return BOCuentas.VerificarCampo(pcampo);
        }

        public string VerificarTexto(string pcampo, string pseparador)
        {
            return BOCuentas.VerificarTexto(pcampo, pseparador);
        }

        public List<MovimientoCoopcentral> CargarArchivoMovCoopcentral(string[] data, string pseparador_miles, string pseparador_decimal = "")
        {
            return BOCuentas.CargarArchivoMovCoopcentral(data, pseparador_miles, pseparador_decimal);
        }

        public string ValidarArchivoCargaCoopcentral(int? _tipo_convenio, string convenio, List<MovimientoCoopcentral> lstConsulta, bool bValidar, Usuario pusuario)
        {
            return BOCuentas.ValidarArchivoCargaCoopcentral(_tipo_convenio, convenio, lstConsulta, bValidar, pusuario);
        }

        public Movimiento ConsultarMovimientoCoopcentral(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario vUsuario)
        {
            return BOCuentas.ConsultarMovimientoCoopcentral(ptipo_convenio, pTarjeta, pOperacion, ptipoTransaccion, pDocumento, pFecha, pValor, vUsuario);
        }

        public Boolean ValidarCuentaCoopcentral(string pConvenio, string pCuenta, ref string tipoCuenta, ref string Error, Usuario pUsuario)        
        {
            return BOCuentas.ValidarCuentaCoopcentral(pConvenio, pCuenta, ref tipoCuenta, ref Error, pUsuario);
        }

        public List<Datafono> ListarDatafono(Usuario pUsuario)
        {
            return BOCuentas.ListarDatafono(pUsuario);
        }



    }

}
