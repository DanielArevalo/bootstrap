using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Business;
using Xpinn.Comun.Entities;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
    public class HistoricoSegmentacionBusiness : GlobalBusiness
    {
        HistoricoSegmentacionData DAHistorico;
        Comun.Business.FechasBusiness BOFechas;

        public HistoricoSegmentacionBusiness()
        {
            DAHistorico = new HistoricoSegmentacionData();
            BOFechas = new Comun.Business.FechasBusiness();
        }

        public void CierreSegmentacion(DateTime fechaCierre, string estado, Usuario usuario)
        {
            try
            {
                if (fechaCierre == DateTime.MinValue) throw new ArgumentException("Fecha de cierre invalida!.");
                if (usuario == null) throw new ArgumentException("Usuario invalido!.");
                if (estado == null || estado.Trim() == "") estado = "D";

                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromHours(1)))
                //{
                    DAHistorico.CierreSegmentacion(fechaCierre, estado, usuario);

                    //ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "CierreSegmentacion", ex);
            }
        }

        public List<HistoricoSegmentacion> ListarFechaCierreYaHechas(string pEstado = "D", Usuario usuario = null)
        {
            try
            {
                return DAHistorico.ListarFechaCierreYaHechas(pEstado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarFechaCierreYaHechas", ex);
                return null;
            }
        }

        public List<HistoricoSegmentacion> ListarHistoricosSegmentacion(string filtro, Usuario usuario)
        {
            try
            {
                return DAHistorico.ListarHistoricosSegmentacion(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarHistoricosSegmentacion", ex);
                return null;
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoActual(string consecutivo, Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(consecutivo)) throw new ArgumentException("El consecutivo es necesario para continuar!.");

                return DAHistorico.ConsultarHistoricoSegmentoActual(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ConsultarHistoricoSegmentoActual", ex);
                return null;
            }
        }

        public Segmentos ConsultarSegmento(Segmentos vDatos, Usuario usuario)
        {
            try
            {
                return DAHistorico.ConsultarSegmento(vDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ConsultarSegmento", ex);
                return null;
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoAnterior(string consecutivo, Usuario usuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(consecutivo)) throw new ArgumentException("El consecutivo es necesario para continuar!.");

                return DAHistorico.ConsultarHistoricoSegmentoAnterior(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ConsultarHistoricoSegmentoAnterior", ex);
                return null;
            }
        }

        public List<Segmento_Detalles> ListarDetalleSegmentos(int consecutivo, Usuario usuario)
        {
            try
            {
                return DAHistorico.ListarDetalleSegmentos(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarDetalleSegmentos", ex);
                return null;
            }
        }
        public List<tipoVariable> ListarTiposVariable(tipoVariable variables, Usuario usuario)
        {
            try
            {
                return DAHistorico.ListarTiposVariable(variables, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarSegmentos", ex);
                return null;
            }
        }

        public List<Segmentos> ListarSegmentos(Segmentos segmentos, Usuario usuario)
        {
            try
            {
                return DAHistorico.ListarSegmentos(segmentos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarSegmentos", ex);
                return null;
            }
        }

        public List<listaMultiple> ListarActividadesMultiple(Usuario usuario)
        {
            try
            {
                return DAHistorico.ListarActividadesMultiple(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ListarSegmentos", ex);
                return null;
            }
        }
        
        public void GuardarAnalisisOficialDeHistoricoSegmentacion(HistoricoSegmentacion historico, Usuario usuario)
        {
            try
            {
                if (historico == null) throw new ArgumentException("historico del analisis invalido!.");
                if (string.IsNullOrWhiteSpace(historico.analisisoficialcumplimiento)) throw new ArgumentException("Analisis realizado invalido!.");
                if (usuario == null) throw new ArgumentException("Usuario invalido!.");

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAHistorico.GuardarAnalisisOficialDeHistoricoSegmentacion(historico, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "GuardarAnalisisOficialDeHistoricoSegmentacion", ex);
            }
        }

        public Segmentos CrearSegmento(Segmentos vSegment, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vSegment = DAHistorico.CrearSegmento(vSegment, usuario);
                    int cod = vSegment.codsegmento;
                    if (vSegment.lstDetalle != null && vSegment.lstDetalle.Count > 0)
                    {
                        foreach (Segmento_Detalles pSeg in vSegment.lstDetalle)
                        {
                            Segmento_Detalles nSegmento = new Segmento_Detalles();
                            pSeg.codsegmento = cod;
                            nSegmento = DAHistorico.CrearDetalleSegmento(pSeg, usuario, 1);//crear
                        }
                    }
                    ts.Complete();
                }

                return vSegment;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "CrearSegmento", ex);
                return null;
            }
        }

        public void EliminarSegmentos(int consecutivo, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAHistorico.EliminarSegmentos(consecutivo, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "EliminarSegmentos", ex);
            }
        }
        public tipoVariable ModificarVariable(tipoVariable vVariable, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vVariable = DAHistorico.ModificarVariable(vVariable, usuario);
                    ts.Complete();
                }

                return vVariable;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ModificarVariable", ex);
                return null;
            }
        }

        public Segmentos ModificarSegmento(Segmentos vSegment, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vSegment = DAHistorico.ModificarSegmento(vSegment, usuario);
                    int cod = vSegment.codsegmento;
                    if (vSegment.lstDetalle != null && vSegment.lstDetalle.Count > 0)
                    {
                        foreach (Segmento_Detalles pSeg in vSegment.lstDetalle)
                        {
                            Segmento_Detalles nSegmento = new Segmento_Detalles();
                            pSeg.codsegmento = cod;
                            if (pSeg.idcondiciontran <= 0)
                                nSegmento = DAHistorico.CrearDetalleSegmento(pSeg, usuario, 1);//crear
                            else
                                nSegmento = DAHistorico.CrearDetalleSegmento(pSeg, usuario, 2);//Modificar
                        }
                    }
                    ts.Complete();
                }

                return vSegment;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "ModificarSegmento", ex);
                return null;
            }
        }

        public void EliminarSegmentoDetalle(int conseID, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAHistorico.EliminarSegmentoDetalle(conseID, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionBusiness", "EliminarSegmentoDetalle", ex);
            }
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(string pTipo = "R", Usuario pUsuario = null)
        {
            List<Xpinn.Comun.Entities.Cierea> LstCierre = new List<Xpinn.Comun.Entities.Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DAHistorico.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Xpinn.Comun.Entities.Cierea pCierre = new Xpinn.Comun.Entities.Cierea();
            pCierre.tipo = pTipo;
            pCierre.estado = "D";
            pCierre = DAHistorico.FechaUltimoCierre(pCierre, "", pUsuario);
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

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Xpinn.Comun.Entities.Cierea cieRea = new Xpinn.Comun.Entities.Cierea();
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
