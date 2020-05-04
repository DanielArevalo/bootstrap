using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class SaldosTercerosBusiness : GlobalBusiness
    {
        private SaldosTercerosData DASaldosTerceros;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public SaldosTercerosBusiness()
        {
            DASaldosTerceros = new SaldosTercerosData();
        }

        public List<SaldosTerceros> ListarSaldoConsolidado(SaldosTerceros pDatos, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario pUsuario)
        {
            return DASaldosTerceros.ListarSaldoConsolidado(pDatos, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, pUsuario);
        }

        public List<SaldosTerceros> ListarSaldosTerceros(SaldosTerceros pDatos, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario pUsuario)
        {
            return DASaldosTerceros.ListarSaldosTerceros(pDatos, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, pUsuario);
        }

        //METODOS NIIF
        public List<SaldosTerceros> ListarSaldoConsolidadoNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            return DASaldosTerceros.ListarSaldoConsolidadoNIIF(pEntidad, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, vUsuario);
        }

        public List<SaldosTerceros> ListarSaldosTercerosNIIF(SaldosTerceros pEntidad, ref Double SalIni, ref Double TotDeb, ref Double TotCre, ref Double SalFin, Usuario vUsuario)
        {
            return DASaldosTerceros.ListarSaldosTercerosNIIF(pEntidad, ref SalIni, ref TotDeb, ref TotCre, ref SalFin, vUsuario);
        }

        public SaldosTerceros CrearTrasladoSaldoTer(SaldosTerceros pEntidad, List<SaldosTerceros> lstTerceros, Usuario vUsuario)
        {

            try
            {
                SaldosTerceros pSaldos = new SaldosTerceros();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSaldos = DASaldosTerceros.CrearTrasladoSaldoTer(pEntidad, vUsuario);

                    foreach (SaldosTerceros pSaldoTercero in lstTerceros)
                    {
                        pSaldoTercero.cod_traslado = pSaldos.cod_traslado;
                        DASaldosTerceros.CrearTrasladoSaldoTerDet(pSaldoTercero, vUsuario);
                    }
                    ts.Complete();
                }
                return pSaldos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosBusiness", "CrearTrasladoSaldoTer", ex);
                return null;
            }   
        }

        public List<SaldosTerceros> ListarTercerosTraslado(DateTime pFecha, Int64 cod_cuenta, Int64 centro_costo, Usuario vUsuario)
        {
            try
            {
                return DASaldosTerceros.ListarTercerosTraslado(pFecha, cod_cuenta, centro_costo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SaldosTercerosBusiness", "ListarTercerosTraslado", ex);
                return null;
            }

        }
    }
}

