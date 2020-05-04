using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Business
{

    public class UIAF_ReporteBusiness : GlobalBusiness
    {

        private UIAF_ReporteData DAUIAF_Reporte;

        public UIAF_ReporteBusiness()
        {
            DAUIAF_Reporte = new UIAF_ReporteData();
        }

        public UIAF_Reporte CrearUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF_Reporte = DAUIAF_Reporte.CrearUIAF_Reporte(pUIAF_Reporte, pusuario);

                    ts.Complete();

                }

                return pUIAF_Reporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ReporteBusiness", "CrearUIAF_Reporte", ex);
                return null;
            }
        }


        public UIAF_Reporte ModificarUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pUIAF_Reporte = DAUIAF_Reporte.ModificarUIAF_Reporte(pUIAF_Reporte, pusuario);

                    ts.Complete();

                }

                return pUIAF_Reporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ReporteBusiness", "ModificarUIAF_Reporte", ex);
                return null;
            }
        }


        public void EliminarUIAF_Reporte(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUIAF_Reporte.EliminarUIAF_Reporte(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ReporteBusiness", "EliminarUIAF_Reporte", ex);
            }
        }


        public UIAF_Reporte ConsultarUIAF_Reporte(DateTime pFecha, Usuario pusuario)
        {
            try
            {
                UIAF_Reporte UIAF_Reporte = new UIAF_Reporte();
                UIAF_Reporte = DAUIAF_Reporte.ConsultarUIAF_Reporte(pFecha, pusuario);
                return UIAF_Reporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ReporteBusiness", "ConsultarUIAF_Reporte", ex);
                return null;
            }
        }


        public List<UIAF_Reporte> ListarUIAF_Reporte(UIAF_Reporte pUIAF_Reporte, Usuario pusuario)
        {
            try
            {
                return DAUIAF_Reporte.ListarUIAF_Reporte(pUIAF_Reporte, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("UIAF_ReporteBusiness", "ListarUIAF_Reporte", ex);
                return null;
            }
        }


    }
}
