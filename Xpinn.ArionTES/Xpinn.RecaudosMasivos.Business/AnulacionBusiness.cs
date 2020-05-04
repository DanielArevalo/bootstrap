using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para TipoListaRecaudo
    /// </summary>
    public class AnulacionBusiness : GlobalBusiness
    {
        private AnulacionData DAAnulacion;
        private TipoListaRecaudoDetalleData DAAnulacionDetalle;

        /// <summary>
        /// Constructor del objeto de negocio para TipoListaRecaudo
        /// </summary>
        public AnulacionBusiness()
        {
            DAAnulacion = new AnulacionData();
            DAAnulacionDetalle = new TipoListaRecaudoDetalleData();
        }


        public List<AnulacionOperaciones> listaranulaciones(string ObtenerFiltro, Usuario pUsuario)
        {
          
            try
            {
                return DAAnulacion.listaranulaciones(ObtenerFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionBusiness", "listaranulaciones", ex);
                return null;
            }
        }

        public List<AnulacionOperaciones> listaranulacionesNuevo(string[] pfiltros, Usuario pUsuario)
        {
            try
            {
                return DAAnulacion.listaranulacionesNuevo(pfiltros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionBusiness", "listaranulacionesNuevo", ex);
                return null;
            }
        }


        public AnulacionOperaciones listaranulacionesentidadnuevo(string ObtenerFiltro, Usuario pUsuario)
        {
            try
            {
                return DAAnulacion.listaranulacionesentidadnuevo(ObtenerFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AnulacionBusiness", "listaranulacionesentidadnuevo", ex);
                return null;
            }
        }


        public int RealizarAnulacion(DateTime fecha_anula, List<AnulacionOperaciones> lstTransacciones, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string error, Usuario pUsuario)
        {
            try
            {
                int resultado = 1;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    // Crear la operación
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 50;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_anula;
                    pOperacion.fecha_calc = fecha_anula;
                    pOperacion.num_lista = null;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref error, pUsuario);
                    if (pOperacion.cod_ope <= 0)
                        resultado = 0;

                    // Realizar la transaccion a Notas debito
                    foreach (AnulacionOperaciones transac in lstTransacciones)
                    {
                        Xpinn.Cartera.Data.NotasAProductoData DANaProducto = new Xpinn.Cartera.Data.NotasAProductoData();
                        List<AnulacionOperaciones> lstanulacion = new List<AnulacionOperaciones>();
                        lstanulacion = DAAnulacion.ListarTransacciones(transac.COD_OPE, transac.TIPO_PRODUCTO, transac.NUMERO_RADICACION, pUsuario);
                        foreach (AnulacionOperaciones i in lstanulacion)
                        {
                            string Error = "";
                            DANaProducto.CrearTransaccion(i.NUMERO_RADICACION.ToString(), Convert.ToInt64(i.CLIENTE), pOperacion.cod_ope, fecha_anula, Convert.ToDecimal(i.VALOR), 1, Convert.ToInt64(i.TIPO_PRODUCTO), i.NUMERO_RADICACION.ToString(), 0, int.Parse(i.ATRIBUTO), pUsuario,false, ref Error);
                            if (Error.Trim() != "")
                                resultado = 0;
                        }
                    }
                    
                    // Generar el comprobante   
                    if (resultado == 1)
                    {
                        string sError = "";
                        Xpinn.Contabilidad.Business.ComprobanteBusiness BOComprobante = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
                        if (BOComprobante.GenerarComprobante(pOperacion.cod_ope, 7, fecha_anula, pUsuario.cod_oficina, 0, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, pUsuario) == false)
                        {
                            return -1;
                        }
                        ts.Complete();
                    }
                    else
                    {
                        ts.Dispose();                        
                    }
                    
                }
                return resultado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReestructuracionBusiness", "RealizarAnulacion", ex);
                error = ex.Message;
                return 0;
            }
        }

       
    }
}