using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
{
    public class LineaServiciosServices
    {
        public string CodigoPrograma { get { return "80201"; } }

        LineaServiciosBusiness BOLinea;
        ExcepcionBusiness BOExcepcion;

        public LineaServiciosServices()
        {
            BOLinea = new LineaServiciosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public LineaServicios CrearLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            try
            {
                return BOLinea.CrearLineaServicio(pLinea, foto, banner, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "CrearLineaServicio", ex);
                return null;
            }
        }


        public LineaServicios ModificarLineaServicio(LineaServicios pLinea, byte[] foto, byte[] banner, Usuario vUsuario)
        {
            try
            {
                return BOLinea.ModificarLineaServicio(pLinea, foto, banner, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "ModificarLineaServicio", ex);
                return null;
            }
        }



        public List<LineaServicios> ListarLineaServicios(LineaServicios pLinea, Usuario vUsuario, string filtro)
        {
            try
            {
                return BOLinea.ListarLineaServicios(pLinea, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "ListarLineaServicios", ex);
                return null;
            }
        }


        public void EliminarLineaServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOLinea.EliminarLineaServicio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "EliminarLineaServicio", ex);
            }
        }


        public LineaServicios ConsultarLineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLinea.ConsultarLineaSERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "ConsultarLineaSERVICIO", ex);
                return null;
            }
        }



        public void EliminarDETALLELineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                BOLinea.EliminarDETALLELineaSERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "EliminarDETALLELineaSERVICIO", ex);
            }
        }


        public List<planservicios> ConsultarDETALLELineaSERVICIO(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLinea.ConsultarDETALLELineaSERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosServices", "ConsultarDETALLELineaSERVICIO", ex);
                return null;
            }
        }

        #region destinacion
        public List<LineaServicios> ConsultarDestinacion_Linea(string pId, Usuario vUsuario)
        {
            try
            {
                return BOLinea.ConsultarDestinacion_Linea(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteServices", "Listardestinacion", ex);
                return null;
            }
        }

        public LineaServ_Destinacion consultaDestinacionservicio(string pId, string pIdLin, Usuario vUsuario)
        {
            try
            {
                return BOLinea.consultaDestinacionservicio(pId, pIdLin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpresaExcluyenteServices", "consultaDestinacionservicio", ex);
                return null;
            }
        }


        #endregion
    }
}
