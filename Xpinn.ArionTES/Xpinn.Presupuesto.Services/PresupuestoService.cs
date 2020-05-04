using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Presupuesto.Business;
using Xpinn.Presupuesto.Entities;

namespace Xpinn.Presupuesto.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PresupuestoService
    {
        private PresupuestoBusiness BOPresupuesto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Presupuesto
        /// </summary>
        public PresupuestoService()
        {
            BOPresupuesto = new PresupuestoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "140206"; } }

        public int GetNumeroColumnas()
        {
            return BOPresupuesto.GetNumeroColumnas();
        }

        /// <summary>
        /// Servicio para crear Presupuesto
        /// </summary>
        /// <param name="pEntity">Entidad Presupuesto</param>
        /// <returns>Entidad Presupuesto creada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto CrearPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.CrearPresupuesto(vPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar Presupuesto
        /// </summary>
        /// <param name="pPresupuesto">Entidad Presupuesto</param>
        /// <returns>Entidad Presupuesto modificada</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ModificarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ModificarPresupuesto(vPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Presupuesto
        /// </summary>
        /// <param name="pId">identificador de Presupuesto</param>
        public void EliminarPresupuesto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOPresupuesto.EliminarPresupuesto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarPresupuesto", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener Presupuesto
        /// </summary>
        /// <param name="pId">identificador de Presupuesto</param>
        /// <returns>Entidad Presupuesto</returns>
        public Xpinn.Presupuesto.Entities.Presupuesto ConsultarPresupuesto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarPresupuesto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarPresupuesto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Presupuestos a partir de unos filtros
        /// </summary>
        /// <param name="pPresupuesto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Presupuesto obtenidos</returns>
        public List<Xpinn.Presupuesto.Entities.Presupuesto> ListarPresupuesto(Xpinn.Presupuesto.Entities.Presupuesto vPresupuesto, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarPresupuesto(vPresupuesto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarPresupuesto", ex);
                return null;
            }
        }

        public DataTable ListarCuentas(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarCuentas(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarCuentas", ex);
                return null;
            }
        }

        public decimal SaldoPromedioCuenta(string pcod_cuenta, DateTime pfecha_inicial, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.SaldoPromedioCuenta(pcod_cuenta, pfecha_inicial, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "SaldoPromedioCuenta", ex);
                return 0;
            }
        }

        public decimal SaldoFinalCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.SaldoFinalCuenta(pcod_cuenta, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "SaldoFinalCuenta", ex);
                return 0;
            }
        }

        public decimal SaldoPeriodoCuenta(string pcod_cuenta, DateTime pfecha_final, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.SaldoPeriodoCuenta(pcod_cuenta, pfecha_final, pfiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "SaldoPeriodoCuenta", ex);
                return 0;
            }
        }

        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }

        public DataTable GenerarFlujoCaja(DataTable dtPresupuesto, DataTable dtFechas, DataTable dtColocacion, DataTable dtObligaciones, DataTable dtObligacionesPagos, DataTable dtObligacionesNuevas, DataTable dtTecnologia, double pvalorFlujoInicial, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                return BOPresupuesto.GenerarFlujoCaja(dtPresupuesto, dtFechas, dtColocacion, dtObligaciones, dtObligacionesPagos, dtObligacionesNuevas, dtTecnologia, pvalorFlujoInicial, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "GenerarFlujoCaja", ex);
                return dtPresupuesto;
            }
        }

        public Decimal ConsultarValorPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorPresupuesto(idPresupuesto, pnumeroPeriodo, pcod_cuenta_pre, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorPresupuesto", ex);
                return 0;
            }
        }

        public Decimal ConsultarIncrementoPresupuesto(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pcod_cuenta_pre, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarIncrementoPresupuesto(idPresupuesto, pnumeroPeriodo, pcod_cuenta_pre, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarIncrementoPresupuesto", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorPresupuestoColocacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorPresupuestoColocacion(idPresupuesto, pnumeroPeriodo, pitem, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorPresupuestoColocacion", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorPresupuestoObligacion(Int64 idPresupuesto, Int64 pnumeroPeriodo, string pitem, Int64 pcentro_costo, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorPresupuestoObligacion(idPresupuesto, pnumeroPeriodo, pitem, pcentro_costo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorPresupuestoObligacion", ex);
                return 0;
            }
        }

        public Int64 ConsultarNumeroEjecutivos(Int64 idPresupuesto, Int64 pcod_oficina, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarNumeroEjecutivos(idPresupuesto, pcod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarNumeroEjecutivos", ex);
                return 0;
            }
        }

        public void MayorizarPresupuesto(ref DataTable dtPresupuesto, DataTable dtFechas, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.MayorizarPresupuesto(ref dtPresupuesto, dtFechas, pUsuario,nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "MayorizarPresupuesto", ex);
                return;
            }
        }

        public void TotalizarPresupuesto(ref DataTable dtPresupuesto, DataTable dtFechas)
        {
            try
            {
                BOPresupuesto.TotalizarPresupuesto(ref dtPresupuesto, dtFechas);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "TotalizarPresupuesto", ex);
                return;
            }
        }

        public string ConsultarParametroCuenta(Int64 pcodigo, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarParametroCuenta(pcodigo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarParametroCuenta", ex);
                return "";
            }
        }

        public Boolean EsParametroCuenta(string pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.EsParametroCuenta(pcod_cuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EsParametroCuenta", ex);
                return false;
            }
        }

        #region Cartera

        public DataTable ListarClasificacion(DateTime pfechahistorico, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarClasificacion(pfechahistorico, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarClasificacion", ex);
                return null;
            }
        }

        public DataTable ListarClasificacionOficinas(DateTime pfechahistorico, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarClasificacionOficinas(pfechahistorico, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarClasificacionOficinas", ex);
                return null;
            }
        }

        public Decimal ConsultarValorCartera(DateTime fechahistorico, int cod_clasifica, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorCartera(fechahistorico, cod_clasifica, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorCartera", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorRecuperacion(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorRecuperacion(fechahistorico, cod_clasifica, cod_atr, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorRecuperacion", ex);
                return 0;
            }
        }

        public Decimal ConsultarValorRecuperacionCausado(DateTime fechahistorico, int cod_clasifica, int cod_atr, DateTime fecha_inicial, DateTime fecha_final, string filtro, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorRecuperacionCausado(fechahistorico, cod_clasifica, cod_atr, fecha_inicial, fecha_final, filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorRecuperacion", ex);
                return 0;
            }
        }

        public double TasaPromedioColocacion(Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.TasaPromedioColocacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "TasaPromedioColocacion", ex);
                return 0;
            }
        }

        public double PlazoPromedioColocacion(Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.PlazoPromedioColocacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "PlazoPromedioColocacion", ex);
                return 0;
            }
        }

        public int NumeroEjecutivosOficina(int cod_oficina, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.NumeroEjecutivosOficina(cod_oficina, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "PlazoPromedioColocacion", ex);
                return 0;
            }
        }       

        public DateTime FechaUltimoHistorico(DateTime pFechaInicial, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.FechaUltimoHistorico(pFechaInicial, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "FechaUltimoHistorico", ex);
                return DateTime.MinValue;
            }
        }

        public void CalcularPresupuestoCartera(ref DataTable dtColocacion, DataTable dtFechas, double VrPorCredito, double saldoActualCartera, double porPolizasVencidas, double valorUnitPoliza, double comisionPoliza, double porLeyMiPyme, double porProvision, double porProvisionGen)
        {
            try
            {
                BOPresupuesto.CalcularPresupuestoCartera(ref dtColocacion, dtFechas, VrPorCredito, saldoActualCartera, porPolizasVencidas, valorUnitPoliza, comisionPoliza, porLeyMiPyme, porProvision, porProvisionGen);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CalcularColocacionXOficina", ex);
                return;
            }
        }

        public void CargarPresupuestoCartera(ref DataTable dtPresupuesto, DataTable dtColocacion, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoCartera(ref dtPresupuesto, dtColocacion, dtFechas, bMayorizar, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoCartera", ex);
                return;
            }
        }

        #endregion Cartera

        #region Nomina

        public DataTable ListarNomina(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarNomina(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarNomina", ex);
                return null;
            }
        }

        public DataTable ListarTotalesNomina(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarTotalesNomina(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarTotalesNomina", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina CrearEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearEmpleado(pNomina, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearEmpleado", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ModificarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarEmpleado(pNomina, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarEmpleado", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Nomina ActualizarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ActualizarEmpleado(pNomina, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ActualizarEmpleado", ex);
                return null;
            }
        }

        public void EliminarEmpleado(Xpinn.Presupuesto.Entities.Nomina pNomina, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarEmpleado(pNomina, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarEmpleado", ex);
                return;
            }
        }

        public void CargarPresupuestoNomina(ref DataTable dtPresupuesto, DataTable dtNomina, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoNomina(ref dtPresupuesto, dtNomina, dtFechas, bMayorizar, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoNomina", ex);
                return;
            }
        }

        public DataTable ListarCargos(Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarCargos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarCargos", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos CrearCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearCargo(pCargos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearCargo", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ModificarCargo(Xpinn.Presupuesto.Entities.Cargos pCargos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarCargo(pCargos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarCargo", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Cargos ConsultarCargo(Int64 pId, Usuario vusuario)
        {
            try
            {
                Xpinn.Presupuesto.Entities.Cargos lCargos = new Xpinn.Presupuesto.Entities.Cargos();

                lCargos = BOPresupuesto.ConsultarCargo(pId, vusuario);

                return lCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarCargo", ex);
                return null;
            }
        }

        #endregion Nomina

        #region ActivosFijos

        public DataTable ListarActivosFijos(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarActivosFijos(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarActivosFijos", ex);
                return null;
            }
        }

        public void CargarPresupuestoActivosFijos(ref DataTable dtPresupuesto, DataTable dtActivosFij, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoActivosFijos(ref dtPresupuesto, dtActivosFij, dtFechas, bMayorizar, pUsuario,nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoActivosFijos", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos CrearActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearActivoFijo(pActivosFijos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearActivoFijo", ex);
                return null;
            }
        }

        public void EliminarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarActivoFijo(pActivosFijos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarActivoFijo", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.ActivosFijos ModificarActivoFijo(Xpinn.Presupuesto.Entities.ActivosFijos pActivosFijos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarActivoFijo(pActivosFijos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarActivoFijo", ex);
                return null;
            }
        }

        #endregion ActivosFijos

        #region Diferidos

        public Xpinn.Presupuesto.Entities.Diferidos CrearDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearDiferido(pDiferidos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearDiferido", ex);
                return null;
            }
        }

        public void EliminarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarDiferido(pDiferidos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarDiferido", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Diferidos ModificarDiferido(Xpinn.Presupuesto.Entities.Diferidos pDiferidos, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarDiferido(pDiferidos, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarDiferido", ex);
                return null;
            }
        }

        public void CargarPresupuestoDiferidos(ref DataTable dtPresupuesto, DataTable dtDiferidos, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario, Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoDiferidos(ref dtPresupuesto, dtDiferidos, dtFechas, bMayorizar,  pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoDiferidos", ex);
                return;
            }
        }

        public DataTable ListarDiferidos(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarDiferidos(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarDiferidos", ex);
                return null;
            }
        }

        #endregion Diferidos

        #region Otros

        public Xpinn.Presupuesto.Entities.Otros CrearOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearOtro(pOtros, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearOtro", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Otros ModificarOtro(Xpinn.Presupuesto.Entities.Otros pOtros, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarOtro(pOtros, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarOtro", ex);
                return null;
            }
        }

        public void CargarPresupuestoOtros(ref DataTable dtPresupuesto, DataTable dtOtros, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoOtros(ref dtPresupuesto, dtOtros, dtFechas, bMayorizar, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoOtros", ex);
                return;
            }
        }

        public DataTable ListarOtros(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarOtros(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarOtros", ex);
                return null;
            }
        }

        #endregion Otros

        #region Honorarios

        public Xpinn.Presupuesto.Entities.Honorarios CrearHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearHonorario(pHonorarios, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearHonorario", ex);
                return null;
            }
        }

        public void EliminarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarHonorario(pHonorarios, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarHonorario", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Honorarios ModificarHonorario(Xpinn.Presupuesto.Entities.Honorarios pHonorarios, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarHonorario(pHonorarios, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarHonorario", ex);
                return null;
            }
        }

        public void CargarPresupuestoHonorarios(ref DataTable dtPresupuesto, DataTable dtHonorarios, DataTable dtFechas, Boolean bMayorizar, Usuario pUsuario, Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoHonorarios(ref dtPresupuesto, dtHonorarios, dtFechas, bMayorizar,  pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoHonorarios", ex);
                return;
            }
        }

        public DataTable ListarHonorarios(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarHonorarios(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarHonorarios", ex);
                return null;
            }
        }

        #endregion Honorarios

        #region Obligaciones

        public DataTable ListarObligaciones(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarObligaciones(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarObligaciones", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesTotal(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarObligacionesTotal(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarObligacionesTotal", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesTotalPagos(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarObligacionesTotalPagos(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarObligacionesTotalPagos", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesNuevas(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarObligacionesNuevas(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarObligacionesNuevas", ex);
                return null;
            }
        }

        public DataTable ListarObligacionesPagos(DateTime pfechahistorico, ref string Error, Usuario pUsuario, string filtro)
        {
            try
            {
                return BOPresupuesto.ListarObligacionesPagos(pfechahistorico, ref Error, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarObligacionesPagos", ex);
                return null;
            }
        }

        public Xpinn.Presupuesto.Entities.Obligacion CrearObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearObligacion(pObligacion, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearObligacion", ex);
                return null;
            }
        }

        public void EliminarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarObligacion(pObligacion, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarObligacion", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Obligacion ModificarObligacion(Xpinn.Presupuesto.Entities.Obligacion pObligacion, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarObligacion(pObligacion, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarObligacion", ex);
                return null;
            }
        }

        public void CargarPresupuestoObligacion(ref DataTable dtPresupuesto, DataTable dtObligaciones, DataTable dtObligacionesNuevas, DataTable dtObligacionPagos, DataTable dtFechas, Boolean pCausado, Boolean bMayorizar, Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoObligacion(ref dtPresupuesto, dtObligaciones, dtObligacionesNuevas, dtObligacionPagos, dtFechas, pCausado, bMayorizar, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoObligacion", ex);
                return;
            }
        }

        public void ConsultarTotalObligaciones(ref DataTable dtTotales, ref DataTable dtTotalesPagos, DataTable dtObligacion, DataTable dtObligacionNuevas, DataTable dtObligacionPagos, DataTable dtFechas, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.TotalizarObligaciones(ref dtTotales, ref dtTotalesPagos, dtObligacion, dtObligacionNuevas, dtObligacionPagos, dtFechas, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "TotalizarObligaciones", ex);
                return;
            }
        }

        public decimal ConsultarValorAPagarObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, int codcomponente, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorAPagarObligacion(fechahistorico, cod_obligacion, fecha_inicial, fecha_final, codcomponente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorAPagarObligacion", ex);
                return 0;
            }
        }

        public decimal ConsultarValorProvisionObligacion(DateTime fechahistorico, int cod_obligacion, DateTime fecha_inicial, DateTime fecha_final, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ConsultarValorProvisionObligacion(fechahistorico, cod_obligacion, fecha_inicial, fecha_final, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ConsultarValorProvisionObligacion", ex);
                return 0;
            }
        }

        #endregion Obligaciones

        #region Tecnologia

        public DataTable ListarTecnologia(DateTime pfechahistorico, ref string Error, Usuario pUsuario)
        {
            try
            {
                return BOPresupuesto.ListarTecnologia(pfechahistorico, ref Error, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ListarTecnologia", ex);
                return null;
            }
        }

        public void CargarPresupuestoTecnologia(ref DataTable dtPresupuesto, DataTable dtTecnologia, DataTable dtFechas, Boolean bMayorizar,Usuario pUsuario,Int16 nivel)
        {
            try
            {
                BOPresupuesto.CargarPresupuestoTecnologia(ref dtPresupuesto, dtTecnologia, dtFechas, bMayorizar, pUsuario, nivel);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CargarPresupuestoTecnologia", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia CrearTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.CrearTecnologia(pTecnologia, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "CrearTecnologia", ex);
                return null;
            }
        }

        public void EliminarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                BOPresupuesto.EliminarTecnologia(pTecnologia, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "EliminarTecnologia", ex);
                return;
            }
        }

        public Xpinn.Presupuesto.Entities.Tecnologia ModificarTecnologia(Xpinn.Presupuesto.Entities.Tecnologia pTecnologia, Usuario vusuario)
        {
            try
            {
                return BOPresupuesto.ModificarTecnologia(pTecnologia, vusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PresupuestoService", "ModificarTecnologia", ex);
                return null;
            }
        }

        #endregion Tecnologia
    }
}