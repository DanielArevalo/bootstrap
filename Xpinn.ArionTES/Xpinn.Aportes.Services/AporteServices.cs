using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Aportes.Business;
using Xpinn.Aportes.Entities;
using System.IO;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class AporteServices
    {
        private AporteBusiness BOAporte;
        private ExcepcionBusiness BOExcepcion;
        public int Codigoaporte;
        /// <summary>
        /// Constructor del servicio para Aporte
        /// </summary>
        public AporteServices()
        {
            BOAporte = new AporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaMov { get { return "170302"; } }
        public string CodigoPrograma { get { return "170202"; } }
        public string ProgramaAperturaAporte { get { return "170101"; } }
        public string ProgramaModificacion { get { return "170304"; } }
        public string ProgramaRetiro { get { return "170106"; } }
        public string ProgramaCruce { get { return "170107"; } }
        public string CodigoProgramaCruce { get { return "170108"; } }
        public string codigoProgramaCambio { get { return "170111"; } }
        public string CodigoProgramaCreditoE { get { return "100160"; } }
        public string CodigoProgramaLiqAportes { get { return "170102"; } }
        public string CodigoProgramaRepCierreAportes { get { return "170305"; } }
        public string CodigoProgramaConfirmaModificacion { get { return "170122"; } }

        public string CodigoProgramaPagoIntAPermanente { get { return "170128"; } }

        public string CodigoProgramaConfirmarRetiroAsociado { get { return "170130"; } }
        public string CodigoProgramaConfirmarRetiroaprobado { get { return "170136"; } }
        /// <summary>

        /// Servicio para crear Aporte
        /// </summary>
        /// <param name="pEntity">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearAporte(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.CrearAporte(vAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearAporte", ex);
                return null;
            }
        }

        public Aporte CrearNovedadCambio(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.CrearNovedadCambio(vAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearNovedadCambio", ex);
                return null;
            }
        }
        public Aporte ClasificarPorDiasMora(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ClasificarPorDiasMora(vAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ClasificarPorDiasMora", ex);
                return null;
            }
        }


        public Aporte CrearCruceCuentas(Aporte pAporte, Xpinn.Tesoreria.Entities.Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, List<DetalleCruce> pDetallesCruce , Usuario vUsuario)
        {
            try
            {
                pAporte = BOAporte.CrearCruceCuentas(pAporte, pOperacion, pGiro, pDetallesCruce, vUsuario);
                return pAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearCruceCuentas", ex);
                return null;
            }
        }

        public List<Aporte> ListarAportesNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return BOAporte.ListarAportesNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarAportesNovedadesCambio", ex);
                return null;
            }
        }

        public List<Aporte> ListarTipoProducto(Usuario usuario)
        {
            try
            {
                return BOAporte.ListarTiposConNovedad(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarTiposConNovedad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Aporte
        /// </summary>
        /// <param name="pAporte">Entidad Aporte</param>
        /// <returns>Entidad Aporte modificada</returns>
        public Aporte ModificarAporte(Aporte vAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ModificarAporte(vAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ModificarAporte", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Aporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        public void EliminarAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOAporte.EliminarAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarAporte", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener GrupoAporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarGrupoAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarGrupoAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarGrupoAporte", ex);
                return null;
            }
        }
        public string ClasificacionAporte(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ClasificacionAporte(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ClasificacionAporte", ex);
                return "";
            }
        }
        public List<Aporte> ConsultarCuentasPorGrupoAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarCuentasPorGrupoAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarCuentasPorGrupoAporte", ex);
                return null;
            }
        }

        public void ModificarNovedadCuotaAporte(Aporte aporte, Usuario usuario)
        {
            try
            {
                BOAporte.ModificarNovedadCuotaAporte(aporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ModificarNovedadCuotaAporte", ex);
            }
        }


        /// <summary>
        /// Servicio para obtener Aporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarAporte", ex);
                return null;
            }
        }


        public List<Aporte> ListarCruceCuentas(Aporte entidad, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarCruceCuentas(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarCruceCuentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener los detalles de un cruce atravez del codigo de la persona
        /// </summary>
        /// <param name="pCodPersona">Codigo de la persona</param>
        /// <returns>Entidad Cliente</returns>
        public List<DetalleCruce> ListarDetalleCruceCuenta(long pCodPersona, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarDetalleCruce(pCodPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarCruceCuentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener Aporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Cliente</returns>
        public Aporte ConsultarCliente(String pId, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarCliente(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarCliente", ex);
                return null;
            }
        }

        public decimal ConsultarClienteSalario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ConsultarClienteSalario(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarClienteSalario", ex);
                return 0;
            }
        }


        public decimal Calcular_Cuota(decimal Salario,decimal Porcentaje,decimal Periodicidad, Usuario vUsuario)
        {
            try
            {
                return BOAporte.Calcular_Cuota(Salario,Porcentaje,Periodicidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarClienteSalario", ex);
                return 0;
            }
        }

        public decimal ConsultarSMLMV(Usuario vUsuario)
        {
            try
            {
                return BOAporte.ConsultarSMLMV(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarSMLMV", ex);
                return 0;
            }
        }

        // <summary>
        /// Servicio para obtener Aporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Cliente</returns>
        public Aporte ConsultarClienteAporte(String pId, Int32 cod_Linea, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarClienteAporte(pId, cod_Linea, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarClienteAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarAperturaAporte(Aporte pAporte, Usuario pUsuario, String Orden)
        {
            try
            {
                return BOAporte.ListarAperturaAporte(pAporte, pUsuario, Orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarAperturaAporte", ex);
                return null;
            }
        }
        public List<Aporte> ListarDiasCategoria(int cod_clasifica, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarDiasCategoria(cod_clasifica, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarAperturaAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarRetiros(string pFiltro, DateTime pFechaReti, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarRetiros(pFiltro, pFechaReti, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarRetiros", ex);
                return null;
            }
        }

        public List<Aporte> ListarSolicitudRetiro(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarSolicitudRetiro(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarSolicitudRetiro", ex);
                return null;
            }
        }        

        public bool ModificarEstadoSolicitud(Aporte pAporte, Usuario pUsuario)
        {//
            try
            {
                return BOAporte.ModificarEstadoSolicitud(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ModificarEstadoSolicitud", ex);
                return false;
            }
        }

        public List<Aporte> ListarEstadoCuentaAporte(Int64 pCliente, int? pEstadoAporte, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarEstadoCuentaAporte(pCliente, pEstadoAporte, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarEstadoCuentaAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarTiposConNovedad(Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarTiposConNovedad(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarTiposConNovedad", ex);
                return null;
            }
        }

        public List<Aporte> ListarEstadoCuentaAportestodos(Int64 pCliente, string pEstadoAporte, DateTime pFecha, Usuario pUsuario, int EstadoCuenta = 0)
        {
            try
            {
                return BOAporte.ListarEstadoCuentaAportestodos(pCliente, pEstadoAporte, pFecha, pUsuario, EstadoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarEstadoCuentaAportestodos", ex);
                return null;
            }
        }
        public List<Aporte> ListarEstadoCuentaAportePermitePago(Int64 pCliente, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarEstadoCuentaAportePermitePago(pCliente, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarEstadoCuentaAportePermitePago", ex);
                return null;
            }
        }

        public List<Aporte> ListarDistribucionAporte(Usuario pUsuario, Int64 cliente, string pCod_linea_aporte = null)
        {
            try
            {
                return BOAporte.ListarDistribucionAporte(pUsuario, cliente, pCod_linea_aporte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarDistribucionAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarDistrAporCambiarCuota(Usuario pUsuario, Int64 cliente)
        {
            try
            {
                return BOAporte.ListarDistrAporCambiarCuota(pUsuario, cliente);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarDistrAporCambiarCuota", ex);
                return null;
            }
        }
        public List<Aporte> ListarDistribucionAporteNuevo(Usuario pUsuario, Int64 pGrupo)
        {
            try
            {
                return BOAporte.ListarDistribucionAporteNuevo(pUsuario, pGrupo);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarDistribucionAporteNuevo", ex);
                return null;
            }
        }

        public List<Aporte> ListarAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarAporte(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarAporte", ex);
                return null;
            }
        }

        public String[] getRegistroServices(Usuario pUsuario, String pIdentificacion)
        {
            try
            {
                return BOAporte.getRegistroBusiness(pUsuario, pIdentificacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "getRegistroBusiness", ex);
                return null;
            }
        }

        public void updateInsertServices(Int64 idconsecutivo, Int64 idAfilia, Int64 CodPerson, String estadoAn, DateTime fechaDeCambio, String estadoN, Int64 CodMoNue, String Observa, Usuario pUsuario)
        {
            try
            {
                BOAporte.UpdateInsertEstadoBussines(idconsecutivo, idAfilia, CodPerson, estadoAn, fechaDeCambio, estadoN, CodMoNue, Observa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "getRegistroBusiness", ex);
            }
        }

        public List<MovimientoAporte> ListarMovAporte(Int64 pNumeroAporte, DateTime? pfechaInicial, DateTime? pfechaFinal, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarMovAporte(pNumeroAporte, pfechaInicial, pfechaFinal, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarMovAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarLineaAporte(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarLineaAporte(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarLineaAporte", ex);
                return null;
            }
        }

        public List<Aporte> ListarTipoIdentificacion(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarTipoIdentificacion(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarTipoIdentificacion", ex);
                return null;
            }
        }

        public List<Aporte> ListarTipoRetiro(Aporte pAporte, Usuario pUsuario)
        {
            try
            {
                return BOAporte.ListarTipoRetiro(pAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarTipoRetiro", ex);
                return null;
            }
        }

        public List<Aporte> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarCuentasPersona", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener MaxAporte
        /// </summary>
        /// <param name="pId">identificador de Aporte</param>
        /// <returns>Entidad Aporte</returns>
        public Aporte ConsultarMaxAporte(Usuario pUsuario)
        {
            try
            {
                return BOAporte.ConsultarMaxAporte(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarMaxAporte", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para crear Aporte
        /// </summary>
        /// <param name="pEntity">Entidad Aporte</param>
        /// <returns>Entidad Aporte creada</returns>
        public Aporte CrearRetiroAporte(Aporte pAporte, Xpinn.Tesoreria.Entities.Operacion pOperacion, long formadesembolso,
            int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                BOAporte.CrearRetiroAporte(pAporte, pOperacion, formadesembolso, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, ref Error, pUsuario);
                return pAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearRetiroAporte", ex);
                return null;
            }
        }


        public List<Aporte> ListarAportesControl(string filtro, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarAportesControl(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarAportesControl", ex);
                return null;
            }
        }
        public List<Aporte> ListarSaldos(Aporte persona, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarSaldos(persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarSaldos", ex);
                return null;
            }
        }

        public Aporte ConsultarPersonaRetiro(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ConsultarPersonaRetiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarPersonaRetiro", ex);
                return null;
            }
        }


        public Aporte ConsultarTotalAportes(Int64 pId, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ConsultarTotalAportes(pId,pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarTotalAportes", ex);
                return null;
            }
        }

        public void CargaAportes(ref string pError, string sformato_fecha, Stream pstream, ref List<Aporte> lstAportes, ref List<ErroresCargaAportes> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOAporte.CargaAportes(ref pError, sformato_fecha, pstream, ref lstAportes, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CargaAportes", ex);                
            }
        }


        public void CrearAporteImportacion(DateTime pFechaCarga, ref string pError, List<Aporte> lstAporte, Usuario pUsuario)
        {
            try
            {
                BOAporte.CrearAporteImportacion(pFechaCarga, ref pError, lstAporte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearAporteImportacion", ex);
            }
        }

        public List<LiquidacionInteres> getAportesLiquidarServices(DateTime pfechaLiquidacion, String codLinea, Usuario pusuario)
        {
            try
            {
                return BOAporte.getAportesLiquidarBusinnes(pfechaLiquidacion, codLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "getCuentasLiquidarServices", ex);
                return null;
            }
        }

        public void guardarDatosLiquidacionServices(List<Tran_Aportes> datosIntere, List<Tran_Aportes> datosRetafuentes, Operacion pOperacion, Usuario pUsuario, ref Int64 codigo, DateTime fechaoperacion)
        {
            try
            {
                BOAporte.guardarDatosLiquidacion(datosIntere, datosRetafuentes, pOperacion, pUsuario, ref codigo, fechaoperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "getCuentasLiquidarServices", ex);
            }
        }

        public LiquidacionInteres CrearLiquidacionAportes(LiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                return BOAporte.CrearLiquidacionAportes(pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearLiquidacionAportes", ex);
                return null;
            }
        }

        public void GuardarLiquidacionAportes(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, LiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                BOAporte.GuardarLiquidacionAportes(ref COD_OPE, pOperacion, pLiqui, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "GuardarLiquidacionAportes", ex);
            }
        }


        public List<Aporte> ListarAporteReporteCierre(DateTime pFechaCierre, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarAporteReporteCierre(pFechaCierre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarAporteReporteCierre", ex);
                return null;
            }
        }

        public bool? ValidarFechaSolicitudCambio(Aporte pAporte, Usuario usuario)
        {
            try
            {
                return BOAporte.ValidarFechaSolicitudCambio(pAporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ValidarFechaSolicitudCambio", ex);
                return null;
            }
        }


        public List<Aporte> ListarAportesClubAhorradores(Int64 pcliente, Boolean pResult, string pFiltroAdd, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarAportesClubAhorradores(pcliente, pResult, pFiltroAdd, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarAportesClubAhorradores", ex);
                return null;
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            return BOAporte.ListarFechaCierreCausacion(pUsuario);
        }

        public List<provision_aportes> ListarProvision(DateTime pFechaIni, provision_aportes pAportes, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarProvision(pFechaIni, pAportes, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ListarProvision", ex);
                return null;
            }
        }

        public void InsertarDatos(provision_aportes Insertar_cuenta, List<provision_aportes> lstInsertar, Xpinn.Tesoreria.Entities.Operacion poperacion, Usuario vUsuario)
        {
            try
            {
                BOAporte.InsertarDatos(Insertar_cuenta, lstInsertar, poperacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "InsertarDatos", ex);
            }
        }

        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                BOAporte.Crearcierea(pcierea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "aplicartranslado", ex);
            }
        }

        public List<Aporte> RptLibroSocios(string fechaHist, bool incluye_retirados, Usuario pUsuario)
        {
            try
            {
                return BOAporte.RptLibroSocios(fechaHist, incluye_retirados, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ListaService", "RptLibroSocios", ex);
                return null;
            }
        }


        public Aporte ConsultarCierreAportes(Usuario vUsuario)
        {
            try
            {
                return BOAporte.ConsultarCierreAportes(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ConsultarCierreAportes", ex);
                return null;
            }
        }

        public void CrearPagoIntereses(List<Pago_IntPermanente> lstIntereses, DateTime pFecha, ref Operacion pOperacion, Usuario pUsuario)
        {
            try
            {
                BOAporte.CrearPagoIntereses(lstIntereses, pFecha, ref pOperacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "CrearPagoIntereses", ex);
            }
        }

        public List<Pago_IntPermanente> ListarIntPermanenteRec(Int64 pNumeroRecaudo, Usuario vUsuario)
        {
            try
            {
                return BOAporte.ListarIntPermanenteRec(pNumeroRecaudo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteService", "ListarIntPermanenteRec", ex);
                return null;
            }
        }

        public string ValidarAporte(Aporte pAporte, Usuario usuario)
        {
            try
            {
                return BOAporte.ValidarAporte(pAporte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteServices", "ValidarAporte", ex);
                return null;
            }
        }

        public bool EsAfiancol(Int64 pCodPersona, Usuario pUsuario)
        {
            return BOAporte.EsAfiancol(pCodPersona, pUsuario);
        }



    }
}