using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Services
{
    public class OperacionServices
    {
        private OperacionBusiness BOOperacionBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public OperacionServices()
        {
            BOOperacionBusiness = new OperacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40801"; } }
        public string CodigoProgramaComprobante { get { return "40103"; } }
        public string CodigoProgramaReversa { get { return "40104"; } }


        public List<AnulacionOperaciones> cobocomprobantes(Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.cobocomprobantes(pUsuario);
            }
            catch
            {
                return null;
            }

        }

        public List<AnulacionOperaciones> combooperacion(Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.combooperacion(pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<AnulacionOperaciones> listaranulacionesNuevo(string id, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.listaranulacionesNuevo(id, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<AnulacionOperaciones> listaranulaciones(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.listaranulaciones(filtro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public AnulacionOperaciones listaranulacionesentidadnuevo(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.listaranulacionesentidadnuevo(filtro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public int RealizarAnulacion(Int64 cod_ope, DateTime fecha_anula, string cod_usuario, Int64 cod_ofi, long cod_motivo_anula, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.RealizarAnulacion(cod_ope, fecha_anula, cod_usuario, cod_ofi, cod_motivo_anula, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref error, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para consultar el usuario que anuló una operación
        /// </summary>
        /// <param name="ObtenerFiltro"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public AnulacionOperaciones ListarOperacionAnula(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ListarOperacionAnula(filtro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Método para listar las operaciones
        /// </summary>
        /// <param name="poperacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Operacion> ListarOperacion(Operacion poperacion, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ListarOperacion(poperacion, pUsuario);
            }
            catch
            {
                return null;
            }
        }
        public List<Operacion> ListarOperacion(Operacion poperacion, string pfiltro, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ListarOperacion(poperacion, pfiltro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Contabilizar las operaciones
        /// </summary>
        /// <param name="poperacion"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Operacion> ContabilizarOperacion(List<Operacion> pLstOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ContabilizarOperacion(pLstOperacion, ref pError, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Boolean ReversarOperacion(GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ReversarOperacion(gvOperaciones, pUsuario);
            }
            catch
            {
                return false;
            }
        }



        public Operacion GrabarOperacion(Operacion pEntidad, Usuario pUsuario)
        {

            try
            {
                return BOOperacionBusiness.GrabarOperacion(pEntidad, pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("Operacion", "GrabarOperacion", ex);
                return null;
            }
        }

        public List<AnulacionOperaciones> ListarUltimosMovimientosXpersona(Int64 id, Usuario pUsuario)
        {
            try
            {
                return BOOperacionBusiness.ListarUltimosMovimientosXpersona(id, pUsuario);
            }
            catch
            {
                return null;
            }
        }


    }
}
