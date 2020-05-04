using System;
using System.Collections.Generic;
using System.ServiceModel;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;
using Xpinn.Comun;
using Xpinn.Util;

namespace Xpinn.Riesgo.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>


    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HistoricoSegmentacionService
    {
        private HistoricoSegmentacionBusiness BOHistorico;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para DefinirVariables
        /// </summary>
        public HistoricoSegmentacionService()
        {
            BOHistorico = new HistoricoSegmentacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "270101"; } }
        public string CodigoPrograma2 { get { return "270102"; } }
        public string CodigoPrograma3 { get { return "270203"; } }
        public string CodigoPrograma4 { get { return "270104";  } }
        public string CodigoPrograma5 { get { return "270209"; } }


        public void CierreSegmentacion(DateTime fechaCierre, string estado, Usuario usuario)
        {
            try
            {
                BOHistorico.CierreSegmentacion(fechaCierre, estado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "CierreSegmentacion", ex);
            }
        }

        public List<HistoricoSegmentacion> ListarFechaCierreYaHechas(string pEstado = "D", Usuario usuario = null)
        {
            try
            {
                return BOHistorico.ListarFechaCierreYaHechas(pEstado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarFechaCierreYaHechas", ex);
                return null;
            }
        }

        public List<HistoricoSegmentacion> ListarHistoricosSegmentacion(string filtro, Usuario usuario)
        {
            try
            {
                return BOHistorico.ListarHistoricosSegmentacion(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarHistoricosSegmentacion", ex);
                return null;
            }
        }

        public Segmentos ConsultarSegmento(Segmentos vDatos, Usuario usuario)
        {
            try
            {
                return BOHistorico.ConsultarSegmento(vDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ConsultarSegmento", ex);
                return null;
            }
        }

        public List<Segmento_Detalles> ListarDetalleSegmentos(int consecutivo, Usuario usuario)
        {
            try
            {
                return BOHistorico.ListarDetalleSegmentos(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarDetalleSegmentos", ex);
                return null;
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoActual(string consecutivo, Usuario usuario)
        {
            try
            {
                return BOHistorico.ConsultarHistoricoSegmentoActual(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ConsultarHistoricoSegmentoActual", ex);
                return null;
            }
        }
        public List<tipoVariable> ListarTiposVariable(tipoVariable variables, Usuario usuario)
        {
            try
            {
                return BOHistorico.ListarTiposVariable(variables, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarSegmentos", ex);
                return null;
            }
        }
        public List<Segmentos> ListarSegmentos(Segmentos segmentos, Usuario usuario)
        {
            try
            {
                return BOHistorico.ListarSegmentos(segmentos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarSegmentos", ex);
                return null;
            }
        }
        public List<listaMultiple> ListarActividadesMultiple(Usuario usuario)
        {
            try
            {
                return BOHistorico.ListarActividadesMultiple(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ListarSegmentos", ex);
                return null;
            }
        }
        public void GuardarAnalisisOficialDeHistoricoSegmentacion(HistoricoSegmentacion historico, Usuario usuario)
        {
            try
            {
                BOHistorico.GuardarAnalisisOficialDeHistoricoSegmentacion(historico, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "GuardarAnalisisOficialDeHistoricoSegmentacion", ex);
            }
        }

        public Segmentos CrearSegmento(Segmentos vSegment, Usuario usuario)
        {
            try
            {
                return BOHistorico.CrearSegmento(vSegment, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "CrearSegmento", ex);
                return null;
            }
        }

        public HistoricoSegmentacion ConsultarHistoricoSegmentoAnterior(string consecutivo, Usuario usuario)
        {
            try
            {
                return BOHistorico.ConsultarHistoricoSegmentoAnterior(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ConsultarHistoricoSegmentoAnterior", ex);
                return null;
            }
        }
        public tipoVariable ModificarVariable(tipoVariable vVariable, Usuario usuario)
        {
            try
            {
                return BOHistorico.ModificarVariable(vVariable, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ModificarVariable", ex);
                return null;
            }
        }

        public Segmentos ModificarSegmento(Segmentos vSegment, Usuario usuario)
        {
            try
            {
                return BOHistorico.ModificarSegmento(vSegment, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "ModificarSegmento", ex);
                return null;
            }
        }

        public void EliminarSegmentos(int consecutivo, Usuario usuario)
        {
            try
            {
                BOHistorico.EliminarSegmentos(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "EliminarSegmentos", ex);
            }
        }

        public void EliminarSegmentoDetalle(int conseID, Usuario usuario)
        {
            try
            {
                BOHistorico.EliminarSegmentoDetalle(conseID, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HistoricoSegmentacionService", "EliminarSegmentoDetalle", ex);
            }
        }
        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre1(string pTipo = "R", Usuario pUsuario = null)
        {
            return BOHistorico.ListarFechaCierre(pTipo, pUsuario);
        }



    }
}
