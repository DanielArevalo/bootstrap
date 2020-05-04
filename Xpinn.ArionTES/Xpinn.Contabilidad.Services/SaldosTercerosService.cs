using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class SaldosTercerosService
    {
        private SaldosTercerosBusiness BOSaldosTerceros;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public SaldosTercerosService()
        {
            BOSaldosTerceros = new SaldosTercerosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30206"; } }
        public string CodigoProgramaNIIF { get { return "210112"; } }

        public string CodigoProgramaTraslado { get { return "30809"; } }

        /// <summary>
        /// Servicio para obtener lista de SaldoConsolidado a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de SaldoConsolidado obtenidos</returns>
        public List<SaldosTerceros> ListarSaldoConsolidado(SaldosTerceros pDatos, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario pUsuario)
        {
            try
            {
                return BOSaldosTerceros.ListarSaldoConsolidado(pDatos, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "ListarSaldoConsolidado", ex);
                return null;
            }
        }


        /// <summary>
        /// Servicio para obtener lista de Saldos Terceros a partir de unos filtros
        /// </summary>
        /// <param name="pMovimientoCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Saldos Terceros obtenidos</returns>
        public List<SaldosTerceros> ListarSaldosTerceros(SaldosTerceros pDatos, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario pUsuario)
        {
            try
            {
                return BOSaldosTerceros.ListarSaldosTerceros(pDatos, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "ListarSaldosTerceros", ex);
                return null;
            }
        }


        //METODOS NIIF
        public List<SaldosTerceros> ListarSaldoConsolidadoNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            try
            {
                return BOSaldosTerceros.ListarSaldoConsolidadoNIIF(pEntidad, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "ListarSaldoConsolidadoNIIF", ex);
                return null;
            }
        }

        public List<SaldosTerceros> ListarSaldosTercerosNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            try
            {
                return BOSaldosTerceros.ListarSaldosTercerosNIIF(pEntidad, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "ListarSaldoConsolidadoNIIF", ex);
                return null;
            }
        }

        public SaldosTerceros CrearTrasladoSaldoTer(SaldosTerceros pEntidad, List<SaldosTerceros> lstTerceros, Usuario vUsuario)
        {
            try
            {
                return BOSaldosTerceros.CrearTrasladoSaldoTer(pEntidad, lstTerceros, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "CrearTrasladoSaldoTer", ex);
                return null;
            }

        }

        public List<SaldosTerceros> ListarTercerosTraslado(DateTime pFecha, Int64 cod_cuenta, Int64 centro_costo, Usuario vUsuario)
        {
            try
            {
                return BOSaldosTerceros.ListarTercerosTraslado(pFecha, cod_cuenta, centro_costo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosServices", "ListarTercerosTraslado", ex);
                return null;
            }

        }
    }
}