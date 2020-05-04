using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
    public class CierreMensualBusiness : GlobalBusiness
    {
        private CierremensualData DACierremensual;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para concepto
        /// </summary>
        public CierreMensualBusiness()
        {
            DACierremensual = new CierremensualData();
            BOFechas = new FechasBusiness();
        }

        public List<Cierremensual> ListarFechaCierre(Usuario pUsuario, string pTipo = "C")
        {
            List<Cierremensual> LstCierre = new List<Cierremensual>();            
            DateTime FecIni = DACierremensual.FechaUltimoCierre(pUsuario, pTipo);
            if (FecIni == DateTime.MinValue)
                return null;
            int dias_cierre = 0;
            int tipo_calendario = 0;
            
            DACierremensual.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            DateTime FecCieIni = DateTime.MinValue;
            DateTime FecCie = DateTime.MinValue;
            if (dias_cierre == 30)
            {
                FecCieIni = BOFechas.FecUltDia(FecIni).AddDays(1);
                FecIni = FecCieIni;
                FecCieIni = BOFechas.SumarMeses(FecCieIni, 1);
                FecCie = FecCieIni.AddDays(-1);
            }
            else
            {
                FecCieIni = BOFechas.FecSumDia(FecIni, dias_cierre, tipo_calendario);
            }
            while (FecCieIni <= DateTime.Now.AddDays(28) || (FecCieIni.AddDays(-1).Year == DateTime.Now.Year && FecCieIni.AddDays(-1).Month == DateTime.Now.Month))
            {
                if (dias_cierre == 30)
                {
                    Cierremensual CieMen = new Cierremensual();
                    FecCieIni = FecCieIni.AddDays(-1);
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.SumarMeses(FecCieIni.AddDays(1), 1);                    
                }
                else
                {
                    Cierremensual CieMen = new Cierremensual();
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
        /// <param name="pEntity">Entidad CierreMensual</param>
        /// <returns>Entidad creada</returns>
        public Cierremensual CrearCierreMensual(Cierremensual pCierreMensual, Usuario pUsuario)
        {
            try
            {
                return DACierremensual.CrearCierremensual(pCierreMensual,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BusinessCierreMensual", "CrearCierreMensual", ex);
                return null;
            }
        }


    }
       
}