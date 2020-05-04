using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Contabilidad.Business;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    public class OperacionBusiness : GlobalData
    {

        private OperacionData DAOperacion;

        public OperacionBusiness()
        {
            DAOperacion = new OperacionData();
        }

        public List<AnulacionOperaciones> cobocomprobantes(Usuario pUsuario)
        {
            try
            {
                return DAOperacion.cobocomprobantes(pUsuario);
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
                return DAOperacion.combooperacion(pUsuario);
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
                return DAOperacion.listaranulacionesNuevo(id, pUsuario);
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
                return DAOperacion.listaranulacionesentidadnuevo(filtro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Método para realizar la anulación
        /// </summary>
        /// <param name="cod_ope"></param>
        /// <param name="fecha_anula"></param>
        /// <param name="cod_usuario"></param>
        /// <param name="cod_ofi"></param>
        /// <param name="cod_motivo_anula"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int RealizarAnulacion(Int64 pcod_ope, DateTime fecha_anula, string cod_usuario, Int64 cod_ofi, long cod_motivo_anula, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                int resultado = 0;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // Realizar la anulación
                    resultado = DAOperacion.RealizarAnulacion(pcod_ope, fecha_anula, cod_usuario, cod_ofi, cod_motivo_anula, ref error, pUsuario);
                    // Generar el comprobante   
                    if (resultado == 1)
                    {
                        string sError = "";
                        Xpinn.Contabilidad.Business.ComprobanteBusiness BOComprobante = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
                        if (BOComprobante.GenerarComprobanteSinCommit(pcod_ope, 7, fecha_anula, pUsuario.cod_oficina, pUsuario.codusuario, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, pUsuario) == false)
                        {
                            return -1;
                        }
                    }
                    ts.Complete();
                }
                return resultado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReestructuracionBusiness", "CrearReestructurar", ex);
                error = ex.Message;
                return 0;
            }
        }


        public List<AnulacionOperaciones> listaranulaciones(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAOperacion.listaranulaciones(filtro, pUsuario);
            }
            catch
            {
                return null;
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
                return DAOperacion.ListarOperacionAnula(filtro, pUsuario);
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
                return DAOperacion.ListarOperacion(poperacion, pUsuario);
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
                return DAOperacion.ListarOperacion(poperacion, pfiltro, pUsuario);
            }
            catch
            {
                return null;
            }
        }


        public List<Operacion> ContabilizarOperacion(List<Operacion> pLstOperacion, ref String pError, Usuario pUsuario)
        {
            try
            {
                Int64 pcod_ope;
                Int64 ptip_ope;
                DateTime pfecha;
                Int64 pcod_ofi;
                Int64 pcod_persona;
                Int64? pcod_proceso;
                Int64 pnum_comp;
                Int64 ptipo_comp;
                int contador = 0;
                TransactionOptions topcio = new TransactionOptions();
                topcio.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, topcio))
                {
                    foreach (Operacion rFila in pLstOperacion)
                    {
                        if (rFila.seleccionar == true)
                        {
                            pcod_ope = rFila.cod_ope;
                            ptip_ope = rFila.tipo_ope;
                            pcod_ofi = rFila.cod_ofi;
                            pcod_persona = rFila.cod_cliente;
                            pfecha = rFila.fecha_oper;
                            if (rFila.cod_proceso == null)
                                pcod_proceso = null;
                            else
                                pcod_proceso = Convert.ToInt64(rFila.cod_proceso);
                            pnum_comp = -2;
                            ptipo_comp = -2;
                            if (DAOperacion.GenerarComprobante(pcod_ope, ptip_ope, pfecha, pcod_ofi, pcod_persona, pcod_proceso, ref pnum_comp, ref ptipo_comp, pUsuario) == false)
                                ts.Dispose();
                            pLstOperacion[contador].num_comp = pnum_comp;
                            pLstOperacion[contador].tipo_comp = ptipo_comp;
                        }
                        contador += 1;
                    }
                    ts.Complete();
                }
                return pLstOperacion;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return pLstOperacion;
            }
        }

        public Boolean ReversarOperacion(GridView gvOperaciones, Usuario pUsuario)
        {
            try
            {
                return DAOperacion.ReversarOperacion(gvOperaciones, pUsuario);
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
                return DAOperacion.GrabarOperacion(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Operacion", "GrabarOperacion", ex);
                return null;
            }
        }


        public List<AnulacionOperaciones> ListarUltimosMovimientosXpersona(Int64 id, Usuario pUsuario)
        {
            try
            {
                return DAOperacion.ListarUltimosMovimientosXpersona(id, pUsuario);
            }
            catch
            {
                return null;
            }
        }


    }
}


