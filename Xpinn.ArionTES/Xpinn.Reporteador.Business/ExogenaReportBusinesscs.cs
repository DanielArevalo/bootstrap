using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;
using System.Data;
using System.Transactions;

namespace Xpinn.Reporteador.Business
{
    public class ExogenaReportBusinesscs:GlobalBusiness
    {
        private ExogenaReportData DAExogena;

        public ExogenaReportBusinesscs()
        {
            DAExogena = new ExogenaReportData();
        }

        public List<ExogenaReport> FormatoAhorros(ExogenaReport exo, int año,Usuario vUsuario, ref string pError,int Cuantia)
        {
            try
            {
                return DAExogena.FormatoAhorros(exo,año,vUsuario,ref pError,Cuantia);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ExogenaReportBusinesscs", "FormatoAhorros", ex);
                return null;
            }
        }
        public List<ExogenaReport> FormatoAhorros1019(ExogenaReport exo, string FechaInicial, string FechaFinal, Usuario vUsuario, ref string pError,int Cuantia)
        {
            try
            {
                return DAExogena.FormatoAhorros1019(exo, FechaInicial,FechaFinal, vUsuario, ref pError,Cuantia);
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                //BOExcepcion.Throw("ExogenaReportBusinesscs", "FormatoAhorros", ex);
                return null;
            }
        }
        public int Validar_Saldo_Mensual(string numero_cuenta,int año, Usuario vUsuario)
        {
            try
            {
                return DAExogena.Validar_Saldo_Mensual(numero_cuenta,año, vUsuario);
            }
            catch (Exception ex)
            {
              
                return 0;
            }
        }
        public Int64 Saldo_Total(string numero_cuenta, int año, Usuario vUsuario)
        {
            try
            {
                return DAExogena.Saldo_Total(numero_cuenta, año, vUsuario);
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public DataTable FormatoAporte(int año, Usuario vUsuario)
        {
            try
            {
                return DAExogena.FormatoAporte(año, vUsuario);
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
                return DAExogena.FormatoCDAT(Inicio,final, vUsuario);
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
                return DAExogena.Formato1008(año, vUsuario);
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
                return DAExogena.Formato1001(año, cuantia, vUsuario);
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
                return DAExogena.Formato1026(año, vUsuario);
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
                return DAExogena.Formato1007(año, vUsuario);
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
                return DAExogena.Formato1009(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        public List<ExogenaReport> TiposConceptos(Usuario vUsuario)
        {
            try
            {
                return DAExogena.TiposConcepto(vUsuario);
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("ExogenaReportBusinesscs", "TiposConceptos", ex);
                return null;
            }
        }
        public ExogenaReport CrearTiposConceptos(ExogenaReport pTiposConcepto, Usuario pUsuario,int pOpcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTiposConcepto = DAExogena.CrearTiposConceptos(pTiposConcepto, pUsuario,pOpcion);
                    ts.Complete();
                }

                return pTiposConcepto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportBusinesscs", "CrearTiposConceptos", ex);
                return null;
            }
        }
        public ExogenaReport ConsultarConceptosDian(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAExogena.ConsultarConceptosDian(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportBusinesscs", "ConsultarConceptosDian", ex);
                return null;
            }
        }
        public void EliminarCodConcepto(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAExogena.EliminarCodConcepto(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportBusinesscs", "EliminarCodConcepto", ex);
            }
        }
        public List<ExogenaReport> lstHomologaDian(string codcuenta,Usuario vUsuario)
        {
            try
            {
                return DAExogena.lstHomologacionDian(codcuenta,vUsuario);
            }
            catch (Exception ex)
            {

                BOExcepcion.Throw("ExogenaReportBusinesscs", "lstHomologaDian", ex);
                return null;
            }
        }
        public ExogenaReport CrearPlanCtasHomologacionDIAN(ExogenaReport pExogena, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pExogena = DAExogena.CrearPlanCtasHomologacionDIAN(pExogena, pUsuario);
                    ts.Complete();
                }

                return pExogena;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExogenaReportBusinesscs", "CrearPlanCtasHomologacionDIAN", ex);
                return null;
            }

        }
        public DataTable ReporteCausacion(int año, Usuario vUsuario)
        {
            try
            {
                return DAExogena.ReporteCausacion(año, vUsuario);
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
                return DAExogena.ReporteInteresCDAT(año, vUsuario);
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
