using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
using Xpinn.Comun.Business;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para concepto
    /// </summary>
    public class CierreAnualBusiness : GlobalBusiness
    {
        private CierreAnualData DACierreAnual;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public CierreAnualBusiness()
        {
            DACierreAnual = new CierreAnualData();
            BOFechas = new FechasBusiness();
        }

        public List<CierreAnual> ListarFechaCierre(Usuario pUsuario)
        {
            List<CierreAnual> LstCierre = new List<CierreAnual>();
            DateTime FecIni = DACierreAnual.FechaUltimoCierre(pUsuario);
            if (FecIni == DateTime.MinValue)
                return null;
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACierreAnual.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            DateTime FecCieIni = DateTime.MinValue;
            DateTime FecCie = DateTime.MinValue;
            if (dias_cierre == 360)
            {
                FecCieIni = BOFechas.FecUltDia(FecIni).AddDays(1);
                FecIni = FecCieIni;
                FecCieIni = BOFechas.FecSumDia(FecCieIni, dias_cierre, 1);
                FecCie = FecCieIni.AddDays(-1);
            }
            else
            {
                FecCieIni = BOFechas.FecSumDia(FecIni, dias_cierre, tipo_calendario);
            }
            while (FecCieIni <= DateTime.Now.AddDays(15))
            {
                if (dias_cierre == 360)
                {
                    CierreAnual CieMen = new CierreAnual();
                    FecCieIni = FecCieIni.AddDays(-1);
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.FecSumDia(FecCieIni.AddDays(1), dias_cierre, 1);
                }
                else
                {
                    CierreAnual CieMen = new CierreAnual();
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.FecSumDia(FecCieIni, dias_cierre, tipo_calendario);
                }
            }
            return LstCierre;
        }

        /// <summary>
        /// Crea un Cierre Mensual
        /// </summary>
        /// <param name="pEntity">Entidad CierreAnual</param>
        /// <returns>Entidad creada</returns>
        public CierreAnual CrearCierreAnual(CierreAnual pCierreAnual, ref string pError, bool IsNiif, Usuario pUsuario)
        {
            try
            {
                TransactionOptions trancierreanual = new TransactionOptions();
                trancierreanual.Timeout = TimeSpan.MaxValue;

              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, trancierreanual))
                //{
                    pCierreAnual = DACierreAnual.CrearCierreAnual(pCierreAnual, ref pError, IsNiif, pUsuario);
                 //   ts.Complete();
               // }
                return pCierreAnual;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCierreAnual", "CrearCierreAnual", ex);
                return null;
            }
        }


    }

}