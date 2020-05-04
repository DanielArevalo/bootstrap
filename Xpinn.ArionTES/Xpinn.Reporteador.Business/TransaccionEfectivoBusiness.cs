using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Reporteador.Business
{
 public class TransaccionEfectivoBusiness : GlobalBusiness
    {

        private TransaccionEfectivoData DATransaccion;
        private UIAF_ExoneradosData DAUIAF_Exonerados;
        private UIAF_ReporteData DAUIAF_Reporte;

        public TransaccionEfectivoBusiness()
        {
            DATransaccion = new TransaccionEfectivoData();
            DAUIAF_Exonerados = new UIAF_ExoneradosData();
            DAUIAF_Reporte = new UIAF_ReporteData();
        }

        public List<TransaccionEfectivo> ListaTransaccionEfectivo(DateTime pFecIni, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DATransaccion.ListaTransaccionEfectivo(pFecIni, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoBusiness", "ListaTransaccionEfectivo", ex);
                return null;
            }
        }

        public List<TransaccionEfectivo> ListaClientesExonerados(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DATransaccion.ListaClientesExonerados(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoBusiness", "ListaClientesExoneradas", ex);
                return null;
            }
        }
        public Boolean ClientesExonerados(List<UIAF_Exonerados> listaExonerados,DateTime pFecIni, DateTime pfecha_corte, Int64 pid_reporte,  Usuario vUsuario)
        {
            try
            {
                if (pid_reporte == 0)
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        UIAF_Reporte reporte = new UIAF_Reporte();
                        reporte.numero_registros = listaExonerados.Count;
                        reporte.fecha_generacion = pfecha_corte;
                        reporte.fecha_final = pfecha_corte;
                        reporte.fecha_inicial = pFecIni;
                        reporte.codusuario = vUsuario.codusuario;
                        reporte = DAUIAF_Reporte.CrearUIAF_Reporte(reporte, vUsuario);
                        pid_reporte = reporte.idreporte;
                        ts.Complete();
                    }
                }
                else
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        UIAF_Reporte reporte = new UIAF_Reporte();
                        reporte.idreporte = pid_reporte;
                        reporte.numero_registros = listaExonerados.Count;
                        reporte.fecha_generacion = pfecha_corte;
                        reporte.fecha_final = pfecha_corte;
                        reporte.fecha_inicial = pFecIni;
                        reporte.codusuario = vUsuario.codusuario;
                        reporte = DAUIAF_Reporte.ModificarUIAF_Reporte(reporte, vUsuario);
                        ts.Complete();
                    }
                }
                Int64[] exonerados = new Int64[listaExonerados.Count];
                int secuencia = 0;
                foreach (UIAF_Exonerados x in listaExonerados)
                {
                    if (x.idexonerado == 0)
                    {
                        x.idreporte = pid_reporte;
                        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                        {
                            UIAF_Exonerados exonerado = new UIAF_Exonerados();
                            exonerado= DAUIAF_Exonerados.CrearUIAF_Exonerados(x, vUsuario);
                            exonerados[secuencia] = exonerado.idexonerado;
                            secuencia += 1;
                            ts.Complete();
                        }
                    }

                    else
                    {
                        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                        {
                            DAUIAF_Exonerados.ModificarUIAF_Exonerados(x, vUsuario);
                            exonerados[secuencia] = x.idexonerado;
                            secuencia += 1;
                            ts.Complete();
                        }
                    }
                }
                string filtro = "";
                if (exonerados.Count() == 1)
                   filtro = "" + exonerados[0];
                else if (exonerados.Count() > 1)
                {
                    filtro = "" + exonerados[0] + "";
                    for (int i=1; i<=exonerados.Count()-1;i++)
                    {
                        filtro = filtro + "," + exonerados[i];
                    }
                }
                if (filtro=="")
                {
                    filtro = "0";
                }
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAUIAF_Exonerados.EliminarUIAF_Exonerados(filtro, pid_reporte, vUsuario);
                    ts.Complete();
                }
                return true;

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransaccionEfectivoBusiness", "ListaClientesExoneradas", ex);
                return false;
            }
        }
    }
}
