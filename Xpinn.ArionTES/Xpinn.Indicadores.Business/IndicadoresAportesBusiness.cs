using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Indicadores.Data;
using Xpinn.Indicadores.Entities;

namespace Xpinn.Indicadores.Business
{
    public class IndicadoresAportesBusiness : GlobalData
    {

        private IndicadoresAportesData BOIndicadoresAportesData;

        public IndicadoresAportesBusiness()
        {
            BOIndicadoresAportesData = new IndicadoresAportesData();
        }

        public List<IndicadoresAportes> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesData.consultarfecha(pUsuario);
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
                return BOIndicadoresAportesData.consultarfechaAfiliacion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfechaAfiliacion", ex);
                return null;
            }
        }
        public List<IndicadoresAportes> consultarAportes(string fechaini, string fechafin, Usuario pUsuario,Int64 codlinea)
        {
            try
            {
                return BOIndicadoresAportesData.consultarAportes(fechaini, fechafin, pUsuario, codlinea);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesBusiness", "consultarAportes", ex);
                return null;
            }
        }

        public List<IndicadoresAportes> consultarAfiliacion( string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesData.consultarAfiliacion( fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesBusiness", "consultarAfiliacion", ex);
                return null;
            }



        }

        public List<IndicadoresAportes> consultarRetiro(string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesData.consultarRetiro(fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesBusiness", "consultarRetiro", ex);
                return null;
            }



        }
        public List<IndicadoresAportes> consultarAportesVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAportesData.consultarAportesVariacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAportesBusiness", "consultarAportesVariacion", ex);
                return null;
            }
        }
    }
}


