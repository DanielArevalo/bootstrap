using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;
using Xpinn.Util;

namespace Xpinn.Programado.Business
{
    public class CierreHisProgramadoBusiness : GlobalData
    {

        private CierreHisProgramdoData DACierreHistorico;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public CierreHisProgramadoBusiness()
        {
            DACierreHistorico = new CierreHisProgramdoData();
            BOFechas = new FechasBusiness();
        }

        /// <summary>
        /// Método para realizar el cierre histórico de cartera
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="fecha"></param>
        /// <param name="cod_usuario"></param>
        public CierreHistorico CierreHistorico(CierreHistorico pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            try
            {

            return  DACierreHistorico.CierreHistorico(pentidad,estado, fecha, cod_usuario, ref serror, pUsuario);              
               
            }
            catch (Exception ex)
            {
               BOExcepcion.Throw("CierreHisProgramadoBusiness", "CierreHistorico", ex);
                return null;    
            }            
        }


        public List<Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACierreHistorico.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "L";
            pCierre.estado = "D";
            pCierre = DACierreHistorico.FechaUltimoCierre(pCierre, "", pUsuario);
            if (pCierre == null)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre = new Cierea();
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            DateTime FecIni = pCierre.fecha;
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;            
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);
            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);                        
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Cierea cieRea = new Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);
                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }
    }
}
