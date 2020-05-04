using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EjecutivoMetaService
    {
        private EjecutivoMetaBusiness busEjecutivoMeta;
        private ExcepcionBusiness excepBusinnes;

        public EjecutivoMetaService()
        {
            busEjecutivoMeta = new EjecutivoMetaBusiness();
            excepBusinnes = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110109"; } }
        public string CodigoPrograma2 { get { return "110110"; } }
        public string CodigoProgMetas { get { return "110125"; } }

        public Boolean CargarArchivoEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            return busEjecutivoMeta.CargarArchivoEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
        }

        public List<EjecutivoMeta> ListarEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarEjecutivoMeta", ex);
                return null;
            }
        }
        public List<EjecutivoMeta> ListarEjecutivos(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarEjecutivos(pEntityEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "ListarEjecutivos", ex);
                return null;
            }
        }


        public EjecutivoMeta ConsultarMeta(Usuario pUsuario,String filtro)
        {
            try
            {
                return busEjecutivoMeta.ConsultarMeta(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ConsultarMeta", ex);
                return null;
            }
        }
        public EjecutivoMeta ConsultarMetas(Usuario pUsuario, String idobjeto)
        {
            try
            {
                return busEjecutivoMeta.ConsultarMetas(pUsuario, idobjeto);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ConsultarMeta", ex);
                return null;
            }
        }

        public List<EjecutivoMeta> ListarMeta(Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarMeta(pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ListarMeta", ex);
                return null;
            }
        }
        public List<EjecutivoMeta> ListarMetas(EjecutivoMeta pEntityMeta,Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarMetas(pEntityMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ListarMeta", ex);
                return null;
            }
        }

        public List<EjecutivoMeta> ListarMetasFiltro(String filtro, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarMetasFiltro(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ListarMetasFiltro", ex);
                return null;
            }
        }
        public List<EjecutivoMeta> ListarPeriodicidad(Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ListarPeriodicidad(pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceEjecutivoMeta", "ListarPeriodicidad", ex);
                return null;
            }
        }


        public MemoryStream DescargarArchivoEjecutivoMeta(EjecutivoMeta pEntityEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.DescargarArchivoEjecutivoMeta(pEntityEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("ServiceParametrica", "DescargarArchivoEjecutivoMeta", ex);
                return null;
            }
        }

        public void EliminarEjecutivoMeta(Int64 pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                busEjecutivoMeta.EliminarEjecutivoMeta(pIdEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("AsesoresServices", "EliminarCliente", ex);
            }
        }

        public void EliminarMeta(Int64 pIdMeta, Usuario pUsuario)
        {
            try
            {
                busEjecutivoMeta.EliminarMeta(pIdMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("AsesoresServices", "EliminarMeta", ex);
            }
        }

        public EjecutivoMeta ActualizarEjecutivoMeta(EjecutivoMeta pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ActualizarEjecutivoMeta(pIdEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("EjecutivoMetaService", "ActualizarEjecutivoMeta", ex);
                return null;
            }
        }
    
     public EjecutivoMeta CrearEjecutivoMeta(EjecutivoMeta pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.CrearEjecutivoMeta(pIdEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("EjecutivoMetaService", "CrearEjecutivoMeta", ex);
                return null;
            }
        }
        public EjecutivoMeta ModificarMeta(EjecutivoMeta pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.ModificarMeta(pIdEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("EjecutivoMetaService", "ModificarMeta", ex);
                return null;
            }
        }
    
 
     public EjecutivoMeta CrearMeta(EjecutivoMeta pIdEjecutivoMeta, Usuario pUsuario)
        {
            try
            {
                return busEjecutivoMeta.CrearMeta(pIdEjecutivoMeta, pUsuario);
            }
            catch (Exception ex)
            {
                excepBusinnes.Throw("EjecutivoMetaService", "CrearEjecutivoMeta", ex);
                return null;
            }
        }
    }//end class


}//end namespace