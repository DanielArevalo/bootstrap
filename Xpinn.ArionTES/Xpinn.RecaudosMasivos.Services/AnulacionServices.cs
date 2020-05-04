using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Services
{
    public class AnulacionServices : GlobalData
    {

        private AnulacionBusiness BOAnulacion;
        private ExcepcionBusiness BOExcepcion;


        public string CodigoPrograma { get { return "180106"; } }

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public AnulacionServices()
        {
            BOAnulacion = new AnulacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }




        public List<AnulacionOperaciones> listaranulaciones(string ObtenerFiltro, Usuario pUsuario)
        {

            try
            {
                return BOAnulacion.listaranulaciones(ObtenerFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionServices", "listaranulaciones", ex);
                return null;
            }
        }

        public List<AnulacionOperaciones> listaranulacionesNuevo(string[] pfiltros, Usuario pUsuario)
        {
            try
            {
                return BOAnulacion.listaranulacionesNuevo(pfiltros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionServices", "listaranulacionesNuevo", ex);
                return null;
            }
        }


        public AnulacionOperaciones listaranulacionesentidadnuevo(string ObtenerFiltro, Usuario pUsuario)
        {
            try
            {
                return BOAnulacion.listaranulacionesentidadnuevo(ObtenerFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionServices", "listaranulacionesentidadnuevo", ex);
                return null;
            }
        }

        public int RealizarAnulacion(DateTime fecha_anula, List<AnulacionOperaciones> lstTransacciones, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                return BOAnulacion.RealizarAnulacion(fecha_anula, lstTransacciones, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref error, pUsuario);
            }
            catch
            {
                return 0;
            }
        }


    }
}


