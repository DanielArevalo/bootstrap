﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Cartera.Business
{
    public class CierreHistoricoBusiness : GlobalData
    {

        private CierreHistoricoData DACierreHistorico;
        private FechasBusiness BOFechas;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public CierreHistoricoBusiness()
        {
            DACierreHistorico = new CierreHistoricoData();
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
            
              return   DACierreHistorico.CierreHistorico(pentidad,estado, fecha, cod_usuario, ref serror, pUsuario);
            
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierremensualServices", "CrearCierreMensual", ex);
                return null;
            }            
        }

        /// <summary>
        /// Método para ejecutar el proceso de causación
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="fecha"></param>
        /// <param name="cod_usuario"></param>
        /// <param name="serror"></param>
        public void Causacion(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            //try
            //{
            //    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            //    {
                    DACierreHistorico.Causacion(estado, fecha, cod_usuario, ref serror, pUsuario);
            //        ts.Complete();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    BOExcepcion.Throw("CierreHistoricoBusiness", "Causacion", ex);
            //}              
        }

        /// <summary>
        /// Méotod para ejecutar el proceso de Provisión
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="fecha"></param>
        /// <param name="cod_usuario"></param>
        /// <param name="serror"></param>
        public void Provision(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    DACierreHistorico.Provision(estado, fecha, cod_usuario, ref serror, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreHistoricoBusiness", "Causacion", ex);
            }
        }

        public List<Cierea> ListarFechaCierre(string pTipo = "R", Usuario pUsuario = null)
        {
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACierreHistorico.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = pTipo;
            pCierre.estado = "D";
            pCierre = DACierreHistorico.FechaUltimoCierre(pCierre, "", pUsuario);
            DateTime FecIni;
            if (pCierre == null)
                FecIni = new DateTime(System.DateTime.Now.Year, System.DateTime.Now.Month, 1).AddDays(-1);
            else
                FecIni = pCierre.fecha;
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
            while (FecFin <= DateTime.Now.AddDays(30))
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
