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
    public class ReportesService
    {
        private ReporteBusiness BOAReporte;
        private ExcepcionBusiness BOExcepcion;

        public ReportesService()
        {
            BOAReporte = new ReporteBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110115"; } }


        /// <summary>
        /// Método que permite consultar los crèditos de un asesor
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public List<Reporte> ListarReporteInteresPagados(Usuario pUsuario,DateTime fechaini,DateTime fechafin)
        {
            try
            {
                return BOAReporte.ListarReporteInteresPagados(pUsuario, fechaini, fechafin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteService", "ListarReporteInteresPagados", ex);
                return null;
            }
        }
    }


       


}

