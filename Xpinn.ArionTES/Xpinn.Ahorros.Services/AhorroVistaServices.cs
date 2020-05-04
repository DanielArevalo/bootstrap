using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;
using Xpinn.Comun.Entities;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;

namespace Xpinn.Ahorros.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall, TransactionTimeout = "99:59:59")]
    public class AhorroVistaServices
    {
        private AhorroVistaBusiness BOAhorroVista;
        private ExcepcionBusiness BOExcepcion;


        /// <summary>
        /// Constructor del servicio para AhorroVista
        /// </summary>
        /// 
        public void CambioEstadoServices()
        {
            BOAhorroVista = new AhorroVistaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public AhorroVistaServices()
        {
            BOAhorroVista = new AhorroVistaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220201"; } }
        public string CodigoProgramaLiq { get { return "220204"; } }
        public string CodigoProgramaCie { get { return "220205"; } }
        public string CodigoProgramaHis { get { return "220210"; } }
        public string CodigoProgramaRet { get { return "220206"; } }
        public string CodigoProgramaCambioEstado { get { return "220212"; } }

        public string CodigoProgramaTrasladocuenta { get { return "220213"; } }
        public string CodigoProgramaProvision { get { return "220214"; } }
        public string CodigoProgramaReportePeriodico { get { return "220209"; } }
        public string CodigoProgramaReporteGMF { get { return "220218"; } }
        public string CodigoProgramaFondoLiquidez { get { return "220217"; } }
        public string CodigoProgramaMovimientos { get { return "220215"; } }
        public string CodigoProgramaExtractosAhorro { get { return "220203"; } }
        public string CodigoProgramaLibreta { get { return "220208"; } }
        public string CodigoProgramacrucedepproductos { get { return "220211"; } }
        public string CodigoProgramaDebCreditos { get { return "220216"; } }
        public string CodigoProgramaReporteCierre { get { return "220220"; } }
        public string CodigoProgramaCuentaBeneficiarios { get { return "220221"; } }
        public string CodigoProgramaConfirmarRetiroAhorros { get { return "170129"; } }
        public string CodigoProgramaConfirmarRetiroAprobado { get { return "170134"; } }
        public string CodigoProgramaConfirmarSolicitudProducto { get { return "170131"; } }
        public string CodigoProgramaConfirmarProductoAprobado { get { return "170133"; } }
        public string CodigoProgramaConfirmarCruceAhorrosProductos { get { return "170131"; } }
        /// <summary>
        /// Servicio para crear AhorroVista
        /// </summary>
        /// <param name="pEntity">Entidad AhorroVista</param>
        /// <returns>Entidad AhorroVista creada</returns>
        public AhorroVista CrearAhorroVista(AhorroVista vAhorroVista, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearAhorroVista(vAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearAhorroVista", ex);
                return null;
            }
        }

        public void EliminarBeneficiarioAhorro(long idbeneficiario, Usuario usuario)
        {
            try
            {
                BOAhorroVista.EliminarBeneficiarioAhorro(idbeneficiario, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "EliminarBeneficiarioAhorro", ex);
            }
        }

        /// <summary>
        /// Servicio para modificar AhorroVista
        /// </summary>
        /// <param name="pAhorroVista">Entidad AhorroVista</param>
        /// <returns>Entidad AhorroVista modificada</returns>
        public AhorroVista ModificarAhorroVista(AhorroVista vAhorroVista, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ModificarAhorroVista(vAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarAhorroVista", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar estado de solicitud de retiro
        /// </summary>
        /// <param name="pAhorroVista">Entidad AhorroVista</param>
        /// <returns>Entidad AhorroVista modificada</returns>
        public bool ModificarEstadoSolicitud(AhorroVista vAhorroVista, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ModificarEstadoSolicitud(vAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarEstadoSolicitud", ex);
                return false;
            }
        }

        public AhorroVista ModificarCambioEstados(AhorroVista vAhorroVista, Usuario pUsuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            try
            {
                return BOAhorroVista.ModificarCambioEstados(vAhorroVista, pUsuario, pOperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarCambioEstados", ex);
                return null;
            }
        }



        public AhorroVista AplicarTraslado(ref string pError,AhorroVista traslado_cuenta, List<AhorroVista> lstIngreso, Xpinn.Tesoreria.Entities.Operacion operacion, Usuario vUsuario)
        {

            pError = "";
            try
            {
                 BOAhorroVista.AplicarTraslado(ref pError,traslado_cuenta, lstIngreso, operacion, vUsuario);
                  return traslado_cuenta;
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return null;
            }
        }

        public void AplicarRetiroDeposito(AhorroVista traslado_cuenta, List<AhorroVista> lstIngreso, Xpinn.Tesoreria.Entities.Operacion operacion, Usuario vUsuario)
        {
            try
            {
                BOAhorroVista.AplicarRetiroDeposito(traslado_cuenta, lstIngreso, operacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "AplicarRetiroDeposito", ex);
            }
        }


        public List<ReporteMovimiento> ListarDetalleExtracto(String cod_cuenta, DateTime pFechaPago, Usuario pUsuario, DateTime? fechaInicio = null, DateTime? fechaFinal = null, decimal saldoInicial = 0)
        {
            try
            {
                return BOAhorroVista.ListarDetalleExtracto(cod_cuenta, pFechaPago, pUsuario, fechaInicio, fechaFinal, saldoInicial);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarTipoProductoConSolicitud(Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarTipoProductoConSolicitud(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaService", "ListarTipoProductoConSolicitud", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarTipoProductoConSolicitudRetiro(Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarTipoProductoConSolicitudRetiro(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaService", "ListarTipoProductoConSolicitud", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorrosBeneficiaros(string filtro, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorrosBeneficiaros(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorrosBeneficiaros", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAprobaciones(Usuario usuario,DateTime Fecha)
        {
            try
            {
                return BOAhorroVista.ListarAprobaciones(usuario,Fecha);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAprobaciones", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAprobacionesCuota(Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarAprobacionesCuota(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAprobacionesCuota", ex);
                return null;
            }
        }

        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                BOAhorroVista.Crearcierea(pcierea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "aplicartranslado", ex);
            }
        }

        /// <summary>
        /// Cambia el estado de una solicitud de producto
        /// </summary>
        /// <param name="solicitud"></param>
        /// <param name="usuario"></param>
        public bool ModificarEstadoSolicitudProducto(AhorroVista solicitud, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ModificarEstadoSolicitudProducto(solicitud, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarEstadoSolicitudProducto", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para Eliminar AhorroVista
        /// </summary>
        /// <param name="pId">identificador de AhorroVista</param>
        public void EliminarAhorroVista(String pId, Usuario pUsuario)
        {
            try
            {
                BOAhorroVista.EliminarAhorroVista(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAhorroVista", ex);
            }
        }


        /// <summary>
        /// Servicio para obtener AhorroVista
        /// </summary>
        /// <param name="pId">identificador de AhorroVista</param>
        /// <returns>Entidad AhorroVista</returns>
        public AhorroVista ConsultarAhorroVista(String pId, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarAhorroVista(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarAhorroVista", ex);
                return null;
            }
        }

        public byte[] ImagenTarjeta(Int64 NumCuenta, ref Int64 IdImagen, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ImagenTarjeta(NumCuenta.ToString(), ref IdImagen, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ImagenTarjeta", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener AhorroVista
        /// </summary>
        /// <param name="pId">identificador de AhorroVista</param>
        /// <returns>Entidad AhorroVista</returns>
        public AhorroVista ConsultarAhorroVistaTraslado(String pId, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarAhorroVistatraslado(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarAhorroVistaTraslado", ex);
                return null;
            }
        }


        public List<AhorroVista> ConsultarMovimientosMasivos(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarMovimientosMasivos(pAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVista", ex);
                return null;
            }
        }


        public List<AhorroVista> ReportePeriodico(AhorroVista pAhorroVista, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ReportePeriodico(pAhorroVista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ReportePeriodico", ex);
                return null;
            }
        }

        public List<AhorroVista> ReporteGMF(AhorroVista pAhorroVista, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ReporteGMF(pAhorroVista, pFechaIni, pFechaFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ReporteGmf", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroVista(string pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroVista(pFiltro, pFechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVista", ex);
                return null;
            }
        }

        public List<AhorroVista> ListaAhorroExtractos(AhorroVista pAhorroVista, Usuario pUsuario, String filtro)
        {
            try
            {
                return BOAhorroVista.ListaAhorroExtractos(pAhorroVista, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListaAhorroExtractos", ex);
                return null;
            }
        }


        public List<AhorroVista> FondoLiquidez(AhorroVista pAhorroVista, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.FondoLiquidez(pAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "FondoLiquidez", ex);
                return null;
            }
        }


        public void enviarfondoliquidez(List<AhorroVista> pAhorroVista, Usuario pUsuario)
        {
            try
            {
                BOAhorroVista.enviarfondoliquidez(pAhorroVista, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVista", ex);

            }
        }

        public bool LiquidacionAhorroVista(DateTime pFechaLiquidacion, List<ELiquidacionInteres> pAhorroVista, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string Error, ref Int64 CodOpe, Usuario pUsuario)
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVista", ex);
                return false;
            }
        }

        public void InsertarDatos(provision_ahorro Insertar_cuenta, List<provision_ahorro> lstInsertar, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                BOAhorroVista.InsertarDatos(Insertar_cuenta, lstInsertar, poperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "IngresoCuenta", ex);
            }
        }

        public List<provision_ahorro> ListarProvision(DateTime pFechaIni, provision_ahorro pAhorroVista, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarProvision(pFechaIni, pAhorroVista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "ListarProvision", ex);
                return null;
            }
        }

        public Boolean GeneraNumeroCuenta(Usuario pUsuario)
        {
            return BOAhorroVista.GeneraNumeroCuenta(pUsuario);
        }


        public void CambioEstadoServices(AhorroVista vAhorroVista, Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public List<ELibretas> getAllLibreta(String pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.getAllLibretas(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "getAllLibreta", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAportesClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroClubAhorradores(pcliente, pResult, pFiltroAdd, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarAportesClubAhorradores", ex);
                return null;
            }
        }

        public List<ELibretas> llenarListaNuevoService(DateTime pFechaap, String pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.llenarListaNuevoBussines(pFechaap, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "llenarListaNuevoService", ex);
                return null;
            }
        }

        public void eliminarLibretaServices(Int64 pIdLibreta, Usuario pUsuario)
        {
            try
            {
                BOAhorroVista.eliminarLibretaBusines(pIdLibreta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "eliminarLibretaServices", ex);
            }
        }

        public String validarServices(Usuario pUsuario, Int64 id)
        {
            try
            {
                return BOAhorroVista.validarBusiness(pUsuario, id);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "eliminarLibretaServices", ex);
                return null;
            }
        }

        public ELibretas getLibretaByNumeroCuentaService(String codigo, Usuario pusuario)
        {
            try
            {
                return BOAhorroVista.getLibretaByNumeroCuentaBuss(codigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "eliminarLibretaServices", ex);
                return null;
            }
        }

        public void InsertarLibretaServices(Usuario pUsuario, ELibretas pElibreta, Int64 pidMotivo)
        {
            try
            {
                BOAhorroVista.InsertarLibretaBusness(pUsuario, pElibreta, pidMotivo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "InsertarLibretaServices", ex);
            }
        }

        public ELibretas getLibretaByIdLibretaServices(Int64 idCodigo, Usuario pusuario)
        {
            try
            {
                return BOAhorroVista.getLibretaByIdLibretaBusiness(idCodigo, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "getLibretaByIdLibretaServices", ex);
                return null;
            }
        }

        public void updateLibretaServices(Usuario pUsuario, ELibretas pElibreta, Int64 idMotivo)
        {
            try
            {
                BOAhorroVista.updateLibretaBusiness(pUsuario, pElibreta, idMotivo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "getLibretaByIdLibretaServices", ex);
            }
        }

        public decimal consultarServices(Int64 codigo, String codCuenta, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.consultarBusiness(codigo, codCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "consultarServices", ex);
                return -1;
            }
        }

        public Int64 getNumeroDesprendibleBusines(Usuario pUsuario, String numeroCuenta)
        {
            try
            {
                return BOAhorroVista.getNumeroDesprendibleBusines(pUsuario, numeroCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getNumeroDesprendibleBusines", ex);
                return -1;
            }
        }

        [OperationBehavior(TransactionAutoComplete = true, TransactionScopeRequired = true)]

        [TransactionFlow(TransactionFlowOption.Mandatory)]
        public AhorroVista ciCierreHistorico(AhorroVista pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
              try
            {
                return  BOAhorroVista.ciCierreHistorico(pentidad,estado, fecha, cod_usuario, ref serror, pUsuario);
            }
           catch (Exception ex)
           {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearCierreMensual", ex);
               return null;
           }

        }

        //public string ValidarCierre(string estado, DateTime fecha, int cod_usuario, Usuario pUsuario)
        //{
        //  return  BOAhorroVista.ValidarCierre(estado, fecha, cod_usuario, pUsuario);
        //}

        public List<Cierea> ListarErrorCierre(string tipo,Usuario pUsuario)
        {
            return BOAhorroVista.ListarErrorCierre(tipo, pUsuario);
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOAhorroVista.ListarFechaCierre(pUsuario);
        }
        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            return BOAhorroVista.ListarFechaCierreCausacion(pUsuario);
        }
        public List<Imagenes> Handler(Imagenes pImagenes, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.Handler(pImagenes, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public DateTime getFechaPosCierreConServices(Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.getFechaPosCierreCon(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getFechaPosCierreConServices", ex);
                return DateTime.MinValue;
            }
        }

        public DateTime getfechaUltimaCierreAhorros(Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.getfechaUltimaCierreAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getfechaUltimaCierreAhorros", ex);
                return DateTime.MinValue;
            }
        }

        public List<ELiquidacionInteres> getCuentasLiquidarServices(DateTime pfechaLiquidacion, String codLinea, Usuario pusuario, String cuenta)
        {
            try
            {
                return BOAhorroVista.getCuentasLiquidarBusinnes(pfechaLiquidacion, codLinea, pusuario, cuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "getCuentasLiquidarServices", ex);
                return null;
            }
        }


        //CONSULTAR DETALLE TITULAR
        public List<CuentaHabientes> ListarDetalleTitulares(Int64 pCod, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarDetalleTitulares(pCod, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarTarjetas(String pId, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarTarjetas(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarDetalleTitulares", ex);
                return null;
            }
        }

        public void EliminarCtaHabiente(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOAhorroVista.EliminarCtaHabiente(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATService", "EliminarCtaHabiente", ex);
            }
        }

        public AhorroVista ConsultarAfiliacion(String pId, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarAfiliacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarAfiliacion", ex);
                return null;
            }

        }

        public ELiquidacionInteres CalculoLiquidacionaHORRO(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.CalculoLiquidacionaHORRO(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CalculoLiquidacionaHORRO", ex);
                return null;
            }
        }


        public  ELiquidacionInteres CierreLiquidacionAhorro(ref string pError, ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            pError = "";

            try

            {
                //return pLiqui;
                pLiqui = BOAhorroVista.CierreLiquidacionAhorro(ref pError,ref COD_OPE, pOperacion, pGiro, pLiqui, vUsuario);
                return pLiqui;

            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return null;
                //BOExcepcion.Throw("AhorroVistaservice", "CierreLiquidacionCDAT", ex);
            }
        }

        public ELiquidacionInteres GuardarLiquidacionAhorro(ref string pError, ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            pError = "";

            try
            {
                BOAhorroVista.GuardarLiquidacionAhorro(ref pError,ref COD_OPE, pOperacion, pLiqui, vUsuario);
                return pLiqui;

            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return null;
               // BOExcepcion.Throw("AhorroVistaservice", "GuardarLiquidacionAhorro", ex);
            }
        }


        public ELiquidacionInteres CrearLiquidacionAhorro(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.CrearLiquidacionAhorro(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearLiquidacionAhorro", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarAhorroVistApPagos(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroVistApPagos(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVistApPagos", ex);
                return null;
            }
        }


        public List<AhorroVista> ListarAhorroAptPagos(string pFiltro, int p_producto, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroAptPagos(pFiltro, p_producto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVistApPagos", ex);
                return null;
            }
        }

        public TransaccionCaja Aplicar(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOAhorroVista.Aplicar(pTransaccionCaja, gvTransacciones, lstAhorroVista, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }


        }

        public TransaccionCaja Aplicar(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOAhorroVista.Aplicar(pTransaccionCaja, perTran, gvTransacciones, lstAhorroVista, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }


        }


        public List<CreditoDebAhorros> ListarCreditoDebAhorros(Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarCreditoDebAhorros(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVista", ex);
                return null;
            }
        }

        public Decimal? Calcular_VrAPagar(Int64 pNumRadicacion, String pFecha, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.Calcular_VrAPagar(pNumRadicacion, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "Calcular_VrAPagar", ex);
                return null;
            }
        }

        public Boolean AplicarCréditoDebAhorros(List<CreditoDebAhorros> LCredito, DateTime pfecha_actual, ref Int64 pcod_ope, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.AplicarCréditoDebAhorros(LCredito, pfecha_actual, ref pcod_ope, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "AplicarCréditoDebAhorros", ex);
                return false;
            }
        }

        public bool RetiroDeposito(ref string pError,AhorroVista pCuenta, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref Int64 pCodOpe, ref Int64 pIdGiro, Usuario pUsuario)
        {
            pError = "";
            try
            {
                return BOAhorroVista.RetiroDeposito(ref pError, pCuenta, pOperacion, pGiro, ref pCodOpe, ref pIdGiro, pUsuario);
              
            }
            catch (Exception ex)
            {
                pError +=  ex.Message;
                //BOExcepcion.Throw("AhorroVistaservice", "RetiroDeposito", ex);
                return false;
            }
        }

        public List<AhorroVista> ListarAhorroVistaReporteCierre(DateTime pFechaCierre, Usuario vUsuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroVistaReporteCierre(pFechaCierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroVistaReporteCierre", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarCuentaAhorroVista(long cod, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarCuentaAhorroVista(cod, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarCuentaAhorroVista", ex);
                return null;
            }
        }

        public ReporteMovimiento ConsultarExtractoAhorroVista(string numeroCuenta, DateTime fechaCorte, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ConsultarExtractoAhorroVista(numeroCuenta, fechaCorte, fechaInicio, fechaFinal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarExtractoAhorroVista", ex);
                return null;
            }
        }

        public string ConsultarNombreLineaDeAhorroPorCodigo(string linea, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ConsultarNombreLineaDeAhorroPorCodigo(linea, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarNombreLineaDeAhorroPorCodigo", ex);
                return null;
            }
        }

        public AhorroVista ConsultarAhorroVistaDatosOficina(string numeroCuenta, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ConsultarAhorroVistaDatosOficina(numeroCuenta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarAhorroVistaDatosOficina", ex);
                return null;
            }
        }


        public AhorroVista ConsultarCuentaAhorroVista(String pId, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarCuentaAhorroVista(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarCuentaAhorroVista", ex);
                return null;
            }
        }


        public TransaccionCaja AplicarCruce_Ahs_Pro(TransaccionCaja pTransaccionCaja, PersonaTransaccion  perTran, GridView gvTransacciones, List<AhorroVista> lstAhorroVista, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                return BOAhorroVista.AplicarCruce_Ahs_Pro(pTransaccionCaja, perTran, gvTransacciones, lstAhorroVista, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error +=  ex.Message;
                return null;
            }


        }


        public List<AhorroVista> ListarCuentaAhorroVistaGiros(long cod, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarCuentaAhorroVistaGiro(cod, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarCuentaAhorroVistaGiros", ex);
                return null;
            }
        }


        public AhorroVista ConsultarCierreAhorroVista(Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarCierreAhorroVista( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ConsultarCierreAhorroVista", ex);
                return null;
            }
        }
        public AhorroVista CrearNovedadCambio(AhorroVista vAporte, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearNovedadCambio(vAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearNovedadCambio", ex);
                return null;
            }
        }
        public List<AhorroVista> ListarAhorroNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ListarAhorroNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ListarAhorroNovedadesCambio", ex);
                return null;
            }
        }

        public string CrearSolicitudAhorros(AhorroVista pAhorros, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearSolicitudAhorros(pAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearSolicitudAhorros", ex);
                return null;
            }
        }

        public string CrearSolicitudAhorrosVista(AhorroVista pAhorros, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearSolicitudAhorrosVista(pAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearSolicitudAhorrosVista", ex);
                return null;
            }
        }        

        public void ModificarNovedadCuotaAhorro(AhorroVista ahorro, Usuario usuario)
        {
            try
            {
                BOAhorroVista.ModificarNovedadCuotaAhorro(ahorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "ModificarNovedadCuotaAhorro", ex);
            }
        }
        public bool? ValidarFechaSolicitudCambio(AhorroVista pAhorro, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.ValidarFechaSolicitudCambio(pAhorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }
        public List<SolicitudProductosWeb> ListarSolicitudCreditoAAC(string pFiltro, Usuario pUsuario)
        {
            try
            { 
                return BOAhorroVista.ListarSolicitudCreditoAAC(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarSolicitudCreditoAAC", ex);
                return null;
            }
        }
        public SolicitudProductosWeb CrearAprobacionProducto(SolicitudProductosWeb pProducto, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearAprobacionProducto(pProducto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearAprobacionProducto", ex);
                return null;
            }
        }        
            
        public string MaxRegistro(string pAhorro, Usuario usuario)
        {
            try
            {
                return BOAhorroVista.MaxRegistro(pAhorro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }

        public int CrearSolicitudRetiroAhorros(AhorroVista pAhorros, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.CrearSolicitudRetiroAhorros(pAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaservice", "CrearSolicitudRetiroAhorros", ex);
                return 0;
            }
        }

        public List<AhorroVista> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ListarSolicitudRetiro(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "ListarSolicitudRetiro", ex);
                return null;
            }
        }

        public List<AhorroVista> ListarSolicitudProducto(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ListarSolicitudProducto(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "ListarSolicitudProducto", ex);
                return null;
            }
        }

        public AhorroVista ConsultarCuentaBancaria(string pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOAhorroVista.ConsultarCuentaBancaria(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaServices", "ConsultarCuentaBancaria", ex);
                return null;
            }
        }
    }
}
