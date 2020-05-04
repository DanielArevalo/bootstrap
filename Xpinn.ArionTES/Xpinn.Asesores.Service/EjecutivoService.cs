using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EjecutivoService
    {
        private EjecutivoBusiness busEjecutivo;
        private ExcepcionBusiness excepBusinnes;

        public EjecutivoService()
        {
            busEjecutivo = new EjecutivoBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110102"; } }

        public Ejecutivo CrearEjecutivo(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.CrearEjecutivo(pAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "CrearEjecutivo", ex);
                return null;
            }
        }

        public void EliminarEjecutivo(Int64 pIdEjecutivo, Usuario pUsuario)
        {
            try
            {
                busEjecutivo.EliminarEjecutivo(pIdEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "EliminarEjecutivo", ex);
            }
        }
        public string DetalleBarriosEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.DetalleBarriosEjecutivo(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "DetalleBarriosEjecutivo", ex);
                return "";
            }
        }
        public string DetalleZonasEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.DetalleZonasEjecutivo(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ConsultarEjecutivo", ex);
                return "";
            }
        }
        public Ejecutivo ConsultarEjecutivo(Int64 pIdAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ConsultarEjecutivo(pIdAseEntiEjecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ConsultarEjecutivo", ex);
                return null;
            }
        }

        public Ejecutivo ConsultarDatosEjecutivo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ConsultarDatosEjecutivo(pId, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ConsultarDatosEjecutivo", ex);
                return null;
            }
        }

        public Ejecutivo ConsultarDatosDirector(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ConsultarDatosDirector(pId, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ConsultarDatosDirector", ex);
                return null;
            }
        }
        public List<Ejecutivo> guardarZonasEjecutivo(List<Ejecutivo> zonas, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.guardarZonasEjecutivo(zonas, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "guardarZonasEjecutivo", ex);
                return null;
            }
        }
        public List<Ejecutivo> guardarBarriosEjecutivo(List<Ejecutivo> barrios, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.guardarBarriosEjecutivo(barrios, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "guardarBarriosEjecutivo", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListarZonasEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarZonasEjecutivo(idEjecutivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarZonasEjecutivo", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarZonasDeEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarZonasDeEjecutivo(idEjecutivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarZonasEjecutivo", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarBarriosEjecutivo(Int64 idEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarBarriosEjecutivo(idEjecutivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarBarriosEjecutivo", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListarEjecutivo(Ejecutivo pAseEntiEjecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarEjecutivo(pAseEntiEjecutivo, pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarCliente", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListarEjecutivo(Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarEjecutivo(pUsuario); ;
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarCliente", ex);
                return null;
            }
        }

        public Ejecutivo ActualizarCliente(Ejecutivo pEntityEjecutivo, Usuario pUsuario)
        {
            //try
            //{
                return busEjecutivo.ActualizarEjecutivo(pEntityEjecutivo, pUsuario);
            //}
            //catch (Exception ex)
            //{
            //    excepBusinnes.Throw("EjecutivoService", "ActualizarCliente", ex);
            //    return null;
            //}
        }

        public List<Ejecutivo> ListarAsesores(Ejecutivo ejecutivo, Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListarAsesores(ejecutivo, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarAsesoresTabla", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListartodosAsesores(Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListartodosAsesores(pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarAsesoresTabla", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListartodosAsesoresRuta(Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListartodosAsesoresRuta(pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListartodosAsesoresRuta", ex);
                return null;
            }
        }
        public List<Ejecutivo> ListarAsesoresgeoreferencia(Ejecutivo ejecutivo, Usuario pUsuario,string filtro)
        {
            try
            {
                return busEjecutivo.ListarAsesoresgeoreferencia(ejecutivo, pUsuario,filtro);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListarAsesoresTabla", ex);
                return null;
            }
        }

        public List<Ejecutivo> ListartodosUsuarios(Usuario pUsuario)
        {
            try
            {
                return busEjecutivo.ListartodosUsuarios(pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivo", "ListartodosUsuarios", ex);
                return null;
            }
        }
    }

}