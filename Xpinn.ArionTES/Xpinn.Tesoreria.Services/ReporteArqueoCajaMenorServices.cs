using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ReporteArqueoCajaMenorServices
    {
        private ReporteArqueoCajBusiness BOReporteCajM;
        private ExcepcionBusiness BOExcepcion;

        //Constructor
        public ReporteArqueoCajaMenorServices()
        {
            BOReporteCajM = new ReporteArqueoCajBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40605"; } }

            /// <summary>
            /// Servicio para crear Reporte Arqueo de Caja Menor
            /// </summary>
            /// <param name="pArqueoCajaMenor">Entidad ArqueoCajaMenor</param>
            /// <returns>Entidad ArqueoCajaMenor creada</returns>
            /// 
        public ArqueoCajaMenor ReporteArqueoCaj(int vid_arqueo, Usuario vUsuario)
        {
            try
            {
                return BOReporteCajM.ReporteArqueoCaj(vid_arqueo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteArqueoCajaMenorServices", "ReporteArqueoCaj", ex);
                return null;
            }
        }
    }
}
