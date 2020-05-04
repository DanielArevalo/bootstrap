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
    public class IndicadoresAhorrosBusiness : GlobalData
    {

        private IndicadoresAhorrosData BOIndicadoresAhorrosData;

        public IndicadoresAhorrosBusiness()
        {
            BOIndicadoresAhorrosData = new IndicadoresAhorrosData();
        }
        public List<IndicadoresAhorros> consultarfecha(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.consultarfecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CarteraOficinasBusiness", "consultarfecha", ex);
                return null;
            }
        }
        public List<IndicadoresAhorros> ListarTipoProducto( Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.ListarTipoProducto( pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "ListarTipoProducto", ex);
                return null;
            }
        }

        public List<IndicadoresAhorros> ListarLineaAhorro(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.ListarLineaAhorro(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "ListarLineaAhorro", ex);
                return null;
            }
        }

        public List<IndicadoresAhorros> ListarLineaProgramado(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.ListarLineaProgramado(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "ListarLineaProgramado", ex);
                return null;
            }
        }
       


        public List<IndicadoresAhorros> ListarLineaCdat(Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.ListarLineaCdat(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "ListarLineaCdat", ex);
                return null;
            }
        }
        public List<IndicadoresAhorros> consultarAhorros(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.consultarAhorros(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "consultarAhorros", ex);
                return null;
            }
        }

        public List<IndicadoresAhorros> consultarProgramado(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.consultarProgramado(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "consultarProgramado", ex);
                return null;


            }
        }

        public List<IndicadoresAhorros> consultarCdat(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.consultarCdat(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "consultarCdat", ex);
                return null;
            }
        }
        public List<IndicadoresAhorros> consultarAhorrosVariacion(string fechaini, string fechafin, Usuario pUsuario)
        {
            try
            {
                return BOIndicadoresAhorrosData.consultarAhorrosVariacion(fechaini, fechafin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IndicadoresAhorrosBusiness", "consultarAhorrosVariacion", ex);
                return null;
            }
        }
    }
}


