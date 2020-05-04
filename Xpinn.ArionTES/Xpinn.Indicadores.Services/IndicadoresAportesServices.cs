using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Indicadores.Business;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class IndicadoresAportesService
    {


        private IndicadoresAportesBusiness BOIndicadoresAportesBusiness;
        private ExcepcionBusiness BOExcepcion;

        public string CodigoPrograma { get { return "140601"; } }
        public string CodigoProgramaGestionAsociados { get { return "140602"; } }

        public List<IndicadoresAportes> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarfecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfecha", ex);
                return null;
            }
        }

        public List<IndicadoresAportes> consultarfechaAfiliacion(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarfechaAfiliacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfechaAfiliacion", ex);
                return null;
            }
        }


        /// <summary>
        /// Constructor del servicio para ComponenteAdicional
        /// </summary>
        public IndicadoresAportesService()
        {
            BOIndicadoresAportesBusiness = new IndicadoresAportesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<IndicadoresAportes> consultarAportes(string fechaini, string fechafin, Usuario pUsuario,Int64 cod_linea)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarAportes(fechaini, fechafin, pUsuario,  cod_linea);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesService", "consultarAportes", ex);
                return null;
            }
        }

       public List<IndicadoresAportes> consultarRetiro( string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarRetiro( fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesService", "consultarRetiro", ex);
                return null;
            }
        }
        public List<IndicadoresAportes> consultarAfiliacion(string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarAfiliacion(fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesService", "consultarAfiliaciones", ex);
                return null;
            }
        }
        public List<IndicadoresAportes> consultarAportesVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesBusiness.consultarAportesVariacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesService", "consultarAportesVariacion", ex);
                return null;
            }
        }
    }
}




