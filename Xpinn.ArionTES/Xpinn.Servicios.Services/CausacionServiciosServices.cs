using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class CausacionServiciosServices
    {
        public string CodigoProgramaCausa { get { return "80107"; } }
        public string CodigoProgramaRenova { get { return "80108"; } }

        CausacionServiciosBusiness BOServicios;
        ExcepcionBusiness BOExcepcion;

        public CausacionServiciosServices()
        {
            BOServicios = new CausacionServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        
    
        public List<Servicio> ListarServiciosCausacion(string filtro, DateTime pFechaCausa, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ListarServiciosCausacion(filtro, pFechaCausa, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosServices", "ListarServiciosCausacion", ex);
                return null;
            }
        }
        

        public void CrearCausacionServicios(ref Int64 COD_OPE, ref string pError, Xpinn.Tesoreria.Entities.Operacion pOperacion, string pCod_linea_servicio, List<CausacionServicios> lstCausacion, Usuario vUsuario)
        {
            try
            {
                BOServicios.CrearCausacionServicios(ref COD_OPE, ref pError, pOperacion, pCod_linea_servicio, lstCausacion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosServices", "CrearCausacionServicios", ex);
            }
        }

        //RENOVACION
        public List<Servicio> ListarServiciosRenovacion(string filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ListarServiciosRenovacion(filtro, pFechaIni, pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosServices", "ListarServiciosCausacion", ex);
                return null;
            }
        }


        public void CrearRenovacionServicios(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, List<Servicio> lstServicio, Servicio pDatosServ, RenovacionServicios pDatosRenova, Usuario vUsuario)
        {
            try
            {
                BOServicios.CrearRenovacionServicios(ref COD_OPE, pOperacion, lstServicio,pDatosServ,pDatosRenova, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosServices", "CrearRenovacionServicios", ex);
            }
        }
        
        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            return BOServicios.ListarFechaCierreCausacion(pUsuario);
        }

        public string ValidarCausacionXFecha(DateTime pFechaCausacion, Int64 cod_linea_servicio, Usuario vUsuario)
        {
            try
            {
                return BOServicios.ValidarCausacionXFecha(pFechaCausacion, cod_linea_servicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CausacionServiciosServices", "ValidarCausacionXFecha", ex);
                return null;
            }
        }

    }
}
