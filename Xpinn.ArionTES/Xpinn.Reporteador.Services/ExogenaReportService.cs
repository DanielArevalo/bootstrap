using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;
using System.Data;

namespace Xpinn.Reporteador.Services
{
    public class ExogenaReportService
    {
        private ExogenaReportBusinesscs BOReporte;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get; set; }

        public string CodigoProgramaReportelista1019 { get { return "270204"; } }

        public string CodigoProgramaReportelista1010 { get { return "270205"; } }

        public string CodigoProgramaInformesDian { get { return "31105"; } }

        public string CodigoTiposConcepto { get { return "31106"; } }

        public string CodigoReporteCausacion { get { return "31108"; } }

        public ExogenaReportService()
        {
            CodigoPrograma = "200101";
            BOReporte = new ExogenaReportBusinesscs();
            BOExcepcion = new ExcepcionBusiness();
        }

        public void AsignarCodigoPrograma(string valor)
        {
            CodigoPrograma = valor;
        }

        public List<ExogenaReport> FormatoAhorros (ExogenaReport exo, int año,Usuario vUsuario, ref string pError,int Cuantia)
        {
            try
            {
                return BOReporte.FormatoAhorros(exo,año,vUsuario,ref pError,Cuantia);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ExogenaReportService", "FormatoAhorros", ex);
                return null;
            }
        }
        public List<ExogenaReport> FormatoAhorros1019(ExogenaReport exo, string FechaInicial, string FechaFinal, Usuario vUsuario, ref string pError,int Cuantia)
        {
            try
            {
                return BOReporte.FormatoAhorros1019(exo, FechaInicial,FechaFinal, vUsuario, ref pError,Cuantia);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ExogenaReportService", "FormatoAhorros", ex);
                return null;
            }
        }
        public int Validar_Saldo_Mensual(string numero_cuenta,int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Validar_Saldo_Mensual(numero_cuenta,año, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "FormatoAhorros", ex);
                return 0;
            }
        }
        public Int64 Saldo_Total(string numero_cuenta, int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Saldo_Total(numero_cuenta, año, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "FormatoAhorros", ex);
                return 0;
            }
        }
        public DataTable FormatoAporte(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.FormatoAporte(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable FormatoCDAT(string Inicio, string final, Usuario vUsuario)
        {
            try
            {
                return BOReporte.FormatoCDAT(Inicio,final, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable Formato1008(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Formato1008(año, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportData", "Formato1008", ex);
                return null;
            }
        }
        public DataTable Formato1001(int año, int cuantia, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Formato1001(año, cuantia, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable Formato1026(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Formato1026(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable Formato1007(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Formato1007(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable Formato1009(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.Formato1009(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public List<ExogenaReport> TiposConceptos( Usuario vUsuario)
        {
            try
            {
                return BOReporte.TiposConceptos(vUsuario);
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("ExogenaReportService", "TiposConceptos", ex);
                return null;
            }
        }
        public ExogenaReport CrearTiposConceptos(ExogenaReport pTiposConcepto, Usuario pUsuario,int pOpcion)
        {
            try
            {
                return BOReporte.CrearTiposConceptos(pTiposConcepto, pUsuario,pOpcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "CrearTiposConceptos", ex);
                return null;
            }
        }
        public ExogenaReport ConsultarConceptosDian(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ConsultarConceptosDian(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "ConsultarConceptosDian", ex);
                return null;
            }
        }
        public void EliminarCodConcepto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOReporte.EliminarCodConcepto(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "EliminarCodConcepto", ex);
            }
        }
        public List<ExogenaReport> lstHomologaDian(string codcuenta,Usuario vUsuario)
        {
            try
            {
                return BOReporte.lstHomologaDian(codcuenta,vUsuario);
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("ExogenaReportService", "lstHomologaDian", ex);
                return null;
            }
        }
        public ExogenaReport CrearPlanCtasHomologacionDIAN(ExogenaReport pExogena, Usuario pUsuario)
        {
            try
            {
                return BOReporte.CrearPlanCtasHomologacionDIAN(pExogena, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportService", "CrearPlanCtasHomologacionDIAN", ex);
                return null;
            }
        }
        public DataTable ReporteCausacion(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ReporteCausacion(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public DataTable ReporteInteresCDAT(int año, Usuario vUsuario)
        {
            try
            {
                return BOReporte.ReporteInteresCDAT(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
