using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Business
{
    public class ReporteArqueoCajBusiness : GlobalBusiness
    {
        private ReporteArqueoCajData DAReporteArqueoCaj;

        public ReporteArqueoCajBusiness()
        {
            DAReporteArqueoCaj = new ReporteArqueoCajData();
        }
        
        public ArqueoCajaMenor ReporteArqueoCaj(int pid_arqueo, Usuario pUsuario)
        {
            try
            {
                ArqueoCajaMenor arqueo = new ArqueoCajaMenor();
                arqueo.lista_extracto_efectivo = DAReporteArqueoCaj.lstDetalleEfectivo(pid_arqueo, pUsuario);
                arqueo.lista_extracto_legalizados = DAReporteArqueoCaj.DocLegalizados(pid_arqueo, pUsuario);
                arqueo.lista_extracto_no_legalizados = DAReporteArqueoCaj.DocNoLegalizados(pid_arqueo, pUsuario);
                arqueo.resumenArqueo = DAReporteArqueoCaj.ResumenArqueo(pid_arqueo, pUsuario);
                return arqueo;
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("ReporteArqueoCajBusiness", "ReporteArqueoCaj", ex);
                return null;
            }
        }       
    }
}
