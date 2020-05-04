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
    public class CierreTercerosBusiness : GlobalBusiness
    {
        private CierreTercerosData DACierreTerceros;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public CierreTercerosBusiness()
        {
            DACierreTerceros = new CierreTercerosData();
            BOFechas = new FechasBusiness();
        }

        public List<CierreTerceros> ListarFechaCierre(Usuario pUsuario)
        {
            List<CierreTerceros> LstCierre = new List<CierreTerceros>();
            DateTime FecIni = DACierreTerceros.FechaUltimoCierre(pUsuario);
            if (FecIni == DateTime.MinValue)
                return null;
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACierreTerceros.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
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
                    CierreTerceros CieMen = new CierreTerceros();
                    FecCieIni = FecCieIni.AddDays(-1);
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.FecSumDia(FecCieIni.AddDays(1), dias_cierre, 1);
                }
                else
                {
                    CierreTerceros CieMen = new CierreTerceros();
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
        /// <param name="pEntity">Entidad CierreTerceros</param>
        /// <returns>Entidad creada</returns>
        public CierreTerceros CrearCierreTerceros(CierreTerceros pCierreTerceros, ref string pError, bool IsNiif, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    pCierreTerceros = DACierreTerceros.CrearCierreTerceros(pCierreTerceros, ref pError, IsNiif, pUsuario);
                    ts.Complete();
                }
                return pCierreTerceros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCierreTerceros", "CrearCierreTerceros", ex);
                return null;
            }
        }


    }

}