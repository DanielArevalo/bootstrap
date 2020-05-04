using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Business;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;


namespace Xpinn.Programado.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CuentasProgramadoServices
    {
        private CuentasProgramadoBusiness BOLineasPro;
        private ExcepcionBusiness BOExcepcion;

        public CuentasProgramadoServices()
        {
            BOLineasPro = new CuentasProgramadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220401"; } }
        public string CodigoProgramaReporte { get { return "220403"; } }
        public string CodigoProgramaCierreCuenta { get { return "220404"; } }
        public string CodigoProgramaExtractos { get { return "220405"; } }
        public string CodigoProgramaRetiro { get { return "220407"; } }
        public string CodigoProgramaTraslado { get { return "220408"; } }
        public string CodigoProgramaLiqInteres { get { return "220409"; } }

        public string CodigoProgramaProvision { get { return "220410"; } }


        public string CodigoProgramaProrrogaCuenta { get { return "220412"; } }
        public string CodigoProgramaRenovacion{ get { return "220413"; } }

        public CuentasProgramado Crear_ModAhorroProgramado(CuentasProgramado pCuentas, Usuario vUsuario, Int32 opcion)
        {
            try
            {
                return BOLineasPro.Crear_ModAhorroProgramado(pCuentas, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "Crear_ModAhorroProgramado", ex);
                return null;
            }
        }

        public void EliminarAhorroProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOLineasPro.EliminarAhorroProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "EliminarAhorroProgramado", ex);
            }
        }

        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ListarAhorrosProgramado(pFiltro, pFechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarAhorrosProgramado", ex);
                return null;
            }
        }

        public CuentasProgramado ConsultarAhorroProgramado(String pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarAhorroProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ConsultarAhorroProgramado", ex);
                return null;
            }
        }
        public CuentasProgramado ConsultarNumeracionProgramado(CuentasProgramado pAhorroProgramado, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarNumeracionProgramado(pAhorroProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ConsultarNumeracionProgramado", ex);
                return null;
            }
        }
        public CuentasProgramado Consultartasayplazos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.Consultartasayplazos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "Consultartasayplazos", ex);
                return null;
            }
        }
    
        // retorna reportes periodicos de service
        public List<ReporteCuentasPeriodico> ReportePeriodico(string pCodLinea, DateTime pFechaInicial, DateTime pFechaFinal, Int64 pCodOficina, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ReportePeriodico(pCodLinea, pFechaInicial, pFechaFinal, pCodOficina, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ReportePeriodico", ex);
                return null;
            }
        }

        //cargar la griv en la vista
        public List<CierreCuentaAhorroProgramado> cerrarCuentaProgramadoService(DateTime PfechaApertura, String Filtro, Usuario pUsuario) 
        {
            try
            {
                return BOLineasPro.cerrarCuentaProgramadobussine(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ReportePeriodico", ex);
                return null;
            }
        }

        public List<CierreCuentaAhorroProgramado> ListarProrrogas(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.ListarProrrogas(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarProrrogas", ex);
                return null;
            }
        }

        public List<CierreCuentaAhorroProgramado> ListarRenovaciones(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.ListarRenovaciones(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarRenovaciones", ex);
                return null;
            }
        }

        //carga el detalle del cliente 
        public cierreCuentaDetalle cierreCuentaDService(String pcodigo, Usuario pusuario) 
        {
            try
            {
                return BOLineasPro.cerrarCuentaDetalle(pcodigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "cierreCuentaDService", ex);
                return null;
            }
        }


        public cierreCuentaDetalle ProrrogarCuentaDService(String pcodigo, Usuario pusuario)
        {
            try
            {
                return BOLineasPro.ProrrogarCuenta(pcodigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ProrrogarCuenta", ex);
                return null;
            }
        }

        public cierreCuentaDetalle cerrarCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario) 
        {
            try
            {
                return BOLineasPro.cerrarCuentasBusiness(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "cerrarCuentasServices", ex);
                return null;
            }
        }

        public cierreCuentaDetalle AperturaCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            try
            {
                return BOLineasPro.AperturaCuentasServices(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "AperturaCuentasServices", ex);
                return null;
            }
        }

        public cierreCuentaDetalle  CrearRenovacionCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            try
            {
                return BOLineasPro.CrearRenovacionCuentasServices(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "CrearRenovacionCuentasServices", ex);
                return null;
            }
        }

        public void cambiarEstadoCuentasServices(cierreCuentaDetalle entidad, ref Int64 Cod_Operacion, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario pusuario, int tipo, Xpinn.CDATS.Entities.Cdat cdat, Xpinn.Tesoreria.Entities.Giro giro) 
        {
            try
            {
                BOLineasPro.cerrarCuentaCambiarEstadoBusiness(entidad,ref Cod_Operacion, poperacion, pusuario,tipo,cdat, giro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "cambiarEstadoCuentasServices", ex);
            }
        }
        public void CerrarCuentaProgramadoServices(cierreCuentaDetalle entidad, ref Int64 Cod_Operacion, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario pusuario, int tipo, Xpinn.Tesoreria.Entities.Giro giro)
        {
            try
            {
                BOLineasPro.CerrarCuentaProgramadoServices(entidad, ref Cod_Operacion, poperacion, pusuario, tipo, giro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "cambiarEstadoCuentasServices", ex);
            }
        }



        public void ProrrogaProgramado(cierreCuentaDetalle entidad, ref Int64 Cod_Operacion, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario pusuario)
        {
            try
            {
                BOLineasPro.Prorroga_programadoBusiness(entidad, ref Cod_Operacion, poperacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "prorrogaProgramadoBusiness", ex);
            }
        }

        public List<ELiquidacionInteres> getCuentasLiquidarServices(DateTime pfechaLiquidacion, String codLinea, Usuario pusuario) 
        {
            try
            {
                return BOLineasPro.getCuentasLiquidarBusinnes(pfechaLiquidacion, codLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "getCuentasLiquidarServices", ex);
                return null;
            }
        }

        public void guardarDatosLiquidacionServices(List<Etran_Programado> datosIntere, List<Etran_Programado> datosRetafuentes, Operacion pOperacion, Usuario pUsuario, ref Int64 codigo, DateTime fechaoperacion) 
        {
            try
            {
                BOLineasPro.guardarDatosLiquidacion(datosIntere, datosRetafuentes, pOperacion, pUsuario, ref codigo, fechaoperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "getCuentasLiquidarServices", ex);
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleExtracto(String cod_pesona, DateTime pFechaPago, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.ListarDetalleExtracto(cod_pesona, pFechaPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }


        public List<CuentasProgramado> ListarAhorroExtractos(CuentasProgramado cuentasProgramado, Usuario usuario, String filtro)
        {
            try
            {
                return BOLineasPro.ListarAhorroExtractos(cuentasProgramado, usuario,filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public void cierreHistoricoServices(Usuario pUsuario, DateTime fechaCierre, String pEstado, ref string sError)
        {
            try
            {
                BOLineasPro.cierreHistoricoBusines(pUsuario, fechaCierre, pEstado, ref sError);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "cierreHistoricoServices", ex);
            }
        }

        public DateTime verificaFechaServices(Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.verificaFechaBusines(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "verificaFechaServices", ex);
                return DateTime.MinValue;
            }
        }

        public DateTime getFechaPosCierreConServices(Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.getFechaPosCierreCon(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "verificaFechaBusines", ex);
                return DateTime.MinValue;
            }
        }

        public DateTime getFechaposProgra(Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.getFechaposProgra(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "verificaFechaBusines", ex);
                return DateTime.MinValue;
            }
        }

        public void grabarDatos(Usuario pUsuario, cierreCuentaDetalle entidacuneta, ref Int64 codi_Opera, Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, decimal pValor, DateTime fechaoperacion)
        {
            try
            {
                BOLineasPro.trasaction(pUsuario, entidacuneta, ref codi_Opera, pOperacion, pGiro, pValor, fechaoperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "getCuentasLiquidarServices", ex);
            }
        }

        public Operacion deposiTrasaccionServices(ref string pError,Usuario pUsuario, List<CuentasProgramado> list, ref Int64 codigo_op, Operacion pOperacion, String pnuemroP, Int64 pcodUsuario, DateTime pfecha_pago, decimal valorpag, DateTime fechaoperacion) 
        {
            pError = "";
            try
            {
                BOLineasPro.deposiTrasaccion(ref pError,pUsuario, list, ref codigo_op, pOperacion, pnuemroP, pcodUsuario, pfecha_pago, valorpag, fechaoperacion);
                return pOperacion;
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return null;
            }
        }


        public List<CuentasProgramado> ListarParametrizacionCuentas(CuentasProgramado pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.ListarParametrizacionCuentas(pNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarParametrizacionCuentas", ex);
                return null;
            }
        }

        public CuentasProgramado ConsultarNumeracion(CuentasProgramado pahorro, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarNumeracion(pahorro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ConsultarNumeracion", ex);
                return null;
            }
        }


        public ELiquidacionInteres CrearLiquidacionProgramado(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.CrearLiquidacionProgramado(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "CrearLiquidacionProgramado", ex);
                return null;
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            return BOLineasPro.ListarFechaCierreCausacion(pUsuario);
        }

        public List<provision_programado> ListarProvision(DateTime pFechaIni, provision_programado pAhorroVProgramado, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ListarProvision(pFechaIni, pAhorroVProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarProvision", ex);
                return null;
            }
        }


        public void InsertarDatos(provision_programado Insertar_cuenta, List<provision_programado> lstInsertar, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                BOLineasPro.InsertarDatos(Insertar_cuenta, lstInsertar, poperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "InsertarDatos", ex);
            }
        }


        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                BOLineasPro.Crearcierea(pcierea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "Crearcierea", ex);
            }
        }

        public CuentasProgramado ConsultarPeriodicidadProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarPeriodicidadProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ConsultarPeriodicidadProgramado", ex);
                return null;
            }
        }

        public CuentasProgramado ConsultarCierreAhorroProgramado(Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarCierreAhorroProgramado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarCierreAhorroProgramado", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarPrograClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ListarPrograClubAhorradores(pcliente, pFiltroAdd, pResult, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarPrograClubAhorradores", ex);
                return null;
            }
        }
        public CuentasProgramado CrearNovedadCambio(CuentasProgramado vProgramado, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.CrearNovedadCambio(vProgramado, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "CrearNovedadCambio", ex);
                return null;
            }
        }


        public DateTime? FechaPrimerPago(CuentasProgramado vProgramado, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.FechaPrimerPago(vProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "FechaPrimerPago", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarPrograNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return BOLineasPro.ListarPrograNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ListarPrograNovedadesCambio", ex);
                return null;
            }
        }
        public void ModificarNovedadCuotaProgra(CuentasProgramado ahorro, Usuario usuario)
        {
            try
            {
                BOLineasPro.ModificarNovedadCuotaProgra(ahorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "ModificarNovedadCuotaProgra", ex);
            }
        }

        public string CrearSolicitudAhorroProgramado(CuentasProgramado pAhoProgra, Usuario pUsuario)
        {
            try
            {
                return BOLineasPro.CrearSolicitudAhorroProgramado(pAhoProgra, pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoServices", "CrearSolicitudAhorroProgramado", ex);
                return null;
            }
        }
    }
}
