using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Business;
using Xpinn.Util;

namespace Xpinn.Seguridad.Services
{

    public class ArchivoServices
    {
        ArchivosBusiness archivoBusiness;
        ExcepcionBusiness BOExcepcion;

        public ArchivoServices()
        {
            
            archivoBusiness = new ArchivosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "90112"; } }
		
		//--20161227
		public BaseDatosEntidad infoBaseDatosColumnas(BaseDatosEntidad baseDatosEntidad, String nombreTabla, Usuario pUsuario)
        {
            try
            {
                return archivoBusiness.infoBaseDatosColumnas(baseDatosEntidad,nombreTabla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivoServices", "infoBaseDatos", ex);
                return null;
            }
        }
		
		//--20161227
        public BaseDatosEntidad infoBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {
            try
            {
                return archivoBusiness.infoBaseDatos(baseDatosEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivoServices", "infoBaseDatos", ex);
                return null;
            }
        }

        public BaseDatosEntidad infoEsquemasBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {
            try
            {
                return archivoBusiness.infoEsquemasBaseDatos(baseDatosEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivoServices", "infoEsquemasBaseDatos", ex);
                return null;
            }
        }


        public String consultarusuariobd(Usuario pUsuario)
        {
            try
            {
                return archivoBusiness.consultarusuariobd(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivoServices", "consultarusuario", ex);
                return null;
            }
        }
        public BaseDatosEntidad ingresarDatosBaseDatos(Seguridad.Entities.TablaEntidadExt tablaentidadex, System.Data.DataTable tablaorigen, Usuario pUsuario,BaseDatosEntidad basedatosin)
        {

            try
            {
                return archivoBusiness.ingresarDatosBaseDatos(tablaentidadex, tablaorigen, pUsuario, basedatosin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivoServices", "ingresarDatosBaseDatos", ex);
                return null;
            }
        }
    }
}
