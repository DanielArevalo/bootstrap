using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Seguridad.Entities;
using Xpinn.Seguridad.Data;
using Xpinn.Util;

namespace Xpinn.Seguridad.Business
{

    public class ArchivosBusiness
    {
        //esta variable está en la clase global... aquí se usa pero en el proyecto real no.
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();

        private ArchivosData archivosData;


        public ArchivosBusiness()
        {
            archivosData = new ArchivosData();
        }

        public BaseDatosEntidad infoBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {
            try
            {
                return archivosData.infoBaseDatos(baseDatosEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivosBusiness", "infoBaseDatos", ex);
                return null;
            }
        }
        public BaseDatosEntidad infoBaseDatosColumnas(BaseDatosEntidad baseDatosEntidad, String nombreTabla, Usuario pUsuario)
        {
            try
            {
                return archivosData.infoBaseDatosColumnas(baseDatosEntidad,nombreTabla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivosBusiness", "infoBaseDatosColumnas", ex);
                return null;
            }
        }
       
        public BaseDatosEntidad infoEsquemasBaseDatos(BaseDatosEntidad baseDatosEntidad, Usuario pUsuario)
        {
            try
            {
                return archivosData.infoEsquemasBaseDatos(baseDatosEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivosBusiness", "infoEsquemasBaseDatos", ex);
                return null;
            }
        }

        public String consultarusuariobd(Usuario pUsuario)
        {
            try
            {
                return archivosData.consultarusuariobd(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivosBusiness", "consultarusuariobd", ex);
                return null;
            }
        }
        public BaseDatosEntidad ingresarDatosBaseDatos(Seguridad.Entities.TablaEntidadExt tablaentidadex, System.Data.DataTable tablaorigen, Usuario pUsuario, BaseDatosEntidad basedatosin)
        {
            try
            {
                return archivosData.ingresarDatosBaseDatos(tablaentidadex, tablaorigen, pUsuario, basedatosin);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ArchivosBusiness", "ingresarDatosBaseDatos", ex);
                return null;
            }
        }
    }
}
