using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Services
{
    public class TransaccionEfectivoService
    {
        private TransaccionEfectivoBusiness BOTransaccion;
        private UIAF_ReporteBusiness BOReporte;
        private UIAF_ExoneradosBusiness BOExonerados;
        private ExcepcionBusiness BOExcepcion;
        public TransaccionEfectivoService()
        {
            CodigoPrograma = "200402";
            CodigoPrograma2 = "200403";
            BOTransaccion = new TransaccionEfectivoBusiness();
            BOReporte = new UIAF_ReporteBusiness();
            BOExonerados = new UIAF_ExoneradosBusiness();
            BOExcepcion = new ExcepcionBusiness();

        }

        public string CodigoPrograma { get; set; }

        public string CodigoPrograma2 { get; set; }

        public void AsignarCodigoPrograma(string valor)
        {
            CodigoPrograma = valor;
        }
        public void AsignarCodigoPrograma2(string valor)
        {
            CodigoPrograma2 = valor;

        }

        public List<TransaccionEfectivo> ListaTransaccionEfectivo(DateTime pFecIni, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOTransaccion.ListaTransaccionEfectivo(pFecIni, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoService", "ListaTransaccionEfectivo", ex);
                return null;
            }
        }

        public List<TransaccionEfectivo> ListaClientesExonerados(DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return BOTransaccion.ListaClientesExonerados(pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoService", "ListaClientesExoneradas", ex);
                return null;
            }
        }
        public Boolean ClientesExonerados(List<UIAF_Exonerados> listaExonerados,DateTime pFecIni, DateTime pfecha_corte,Int64 pid_reporte , Usuario vUsuario)
        {
            try
            {
                                   
              return  BOTransaccion.ClientesExonerados(listaExonerados, pFecIni, pfecha_corte, pid_reporte , vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoBusiness", "ClientesExonerados", ex);
                return false;
            }
        }

        public UIAF_Reporte Uiaf_Reporte(DateTime pFecha, Usuario vUsuario)
        {
            {
                try
                {
                    return BOReporte.ConsultarUIAF_Reporte(pFecha, vUsuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("TransaccionEfectivoService", "Uiaf_Reporte", ex);
                    return null;
                }
            }
        }

        public List<UIAF_Exonerados> ListaUIAFExonerados(Int64 pIdreporte, Usuario vUsuario)
        {
            try
            {
                return BOExonerados.ListarUIAF_Exonerados(pIdreporte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoService", "ListaClientesExoneradas", ex);
                return null;
            }
        }
        public List<UIAF_Exonerados> ListaUIAFExoneradosDate(DateTime pFechaCorte, Usuario vUsuario)
        {
            try
            {
                return BOExonerados.ListaUIAFExoneradosDate(pFechaCorte, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoService", "ListaClientesExoneradas", ex);
                return null;
            }
        }
    }
}
