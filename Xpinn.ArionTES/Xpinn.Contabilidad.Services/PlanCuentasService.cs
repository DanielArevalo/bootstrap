using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;
using Xpinn.NIIF.Entities;
using System.IO;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class PlanCuentasService
    {
        private PlanCuentasBusiness BOPlanCuentas;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public PlanCuentasService()
        {
            BOPlanCuentas = new PlanCuentasBusiness();
            BOExcepcion = new ExcepcionBusiness();
            CodigoPrograma = "30502";
            CodigoProgramaAdic = "30501";
            CodigoProgramaModif = "30503";
        }

        public string cod_cuenta;

        public string CodigoPrograma;
        public string CodigoProgramaModif;
        public string CodigoProgramaAdic;

        public PlanCuentas ConsultarPlanCuentas(String pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.ConsultarPlanCuentas(pcod_cuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasService", "ConsultarPlanCuentas", ex);
                return null;
            }
        }

        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(String pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                PlanCuentasNIIF pPlanCuentasNif = new PlanCuentasNIIF();

                pPlanCuentasNif = BOPlanCuentas.ConsultarPlanCuentasNIIF(pcod_cuenta, pUsuario);

                return pPlanCuentasNif;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasService", "ConsultarComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PlanCuentass a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanCuentass obtenidos</returns>
        public List<PlanCuentas> ListarPlanCuentasLocal(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentasLocal(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarPlanCuentasLocal", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentasNif(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentasNif(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarPlanCuentasNif", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentasAmbos(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentasAmbos(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarPlanCuentasAmbos", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentas(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarPlanCuentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de PlanCuentass a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de PlanCuentass obtenidos</returns>
        public List<PlanCuentas> ListarPlanCuentasxterceros(Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.ListarPlanCuentasTerceros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarPlanCuentasxterceros", ex);
                return null;
            }
        }


        public PlanCuentas CrearPlanCuentas(PlanCuentas pPlanCuentas, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario, ExogenaReport ExogenaReport)
        {
            try
            {
                pPlanCuentas = BOPlanCuentas.CrearPlanCuentas(pPlanCuentas, lstData, vUsuario, ExogenaReport);

                return pPlanCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "CrearPlanCuentas", ex);
                return null;
            }
        }


        public PlanCuentas ModificarPlanCuentas(PlanCuentas pPlanCuentas, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario, ExogenaReport ExogenaReport)
        {
            try
            {
                pPlanCuentas = BOPlanCuentas.ModificarPlanCuentas(pPlanCuentas, lstData, vUsuario, ExogenaReport);

                return pPlanCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ModificarPlanCuentas", ex);
                return null;
            }
        }


        public void EliminarPlanCuentas(string pId, Usuario vUsuario)
        {
            try
            {
                BOPlanCuentas.EliminarPlanCuentas(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "EliminarPlanCuentas", ex);
                return;
            }
        }
        public bool VerficarAuxiliar(string pId, Usuario vUsuario)
        {
            try
            {
                return BOPlanCuentas.VerficarAuxiliar(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "VerficarAuxiliar", ex);
                return false;
            }
        }

        public List<PlanCuentas> ListarTipoImpuesto(PlanCuentas pTipo, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.ListarTipoImpuesto(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarTipoImpuesto", ex);
                return null;
            }
        }


        public void CargaPlanCuentasYbalance(ref string pError, int pTipoCarga, string sformato_fecha, Stream pstream, ref List<PlanCuentas> lstPlanBalance, ref List<BalanceGeneral> lstBalance, ref List<Xpinn.Contabilidad.Entities.ErroresCargaContabil> plstErrores, Usuario pUsuario)
        {
            try
            {
                BOPlanCuentas.CargaPlanCuentasYbalance(ref pError, pTipoCarga, sformato_fecha, pstream, ref lstPlanBalance, ref lstBalance, ref plstErrores, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "CargaPlanCuentasYbalance", ex);
            }
        }

        public void CrearPlanBalanceImportacion(DateTime pFechaCarga, ref string pError, int pTipoCarga, List<PlanCuentas> lstPlanCta, List<BalanceGeneral> lstBalance, Usuario pUsuario)
        {
            try
            {
                BOPlanCuentas.CrearPlanBalanceImportacion(pFechaCarga, ref pError, pTipoCarga, lstPlanCta, lstBalance, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "CrearPlanBalanceImportacion", ex);
            }
        }

        public List<PlanCuentas> ListarCuentasTraslado(string filtro, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.ListarCuentasTraslado(filtro, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarCuentasTraslado", ex);
                return null;
            }
        }
        public bool EsPlanCuentasNIIF(Usuario pUsuario)
        {
            try
            {
                return BOPlanCuentas.EsPlanCuentasNIIF(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasServices", "ListarCuentasTraslado", ex);
                return false;
            }
        }
    }
}