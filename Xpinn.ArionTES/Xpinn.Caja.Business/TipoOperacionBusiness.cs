using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Caja.Data;
using Xpinn.Caja.Entities;

namespace Xpinn.Caja.Business
{
    /// <summary>
    /// Objeto de negocio para TipoOperacion
    /// </summary>
    public class TipoOperacionBusiness : GlobalData
    {
        private TipoOperacionData DATipOpe;

        /// <summary>
        /// Constructor del objeto de negocio para TipoOperacion
        /// </summary>
        public TipoOperacionBusiness()
        {
            DATipOpe = new TipoOperacionData();
        }

        /// <summary>
        /// Crea un procesoOficina
        /// </summary>
        /// <param name="pEntity">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public TipoOperacion InsertarFactura(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DATipOpe.InsertarFactura(pEntidad, pUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InsertarFacturaBusiness", "InsertarFactura", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de TipoOperacion-Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion - Caja obtenidos</returns>
        public TipoOperacion ConsultarTipoOpeCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarTipOpeCaja(pId, pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTipoOpeCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Oficinas dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion obtenidos</returns>
        public List<TipoOperacion> ListarTipoTransaccion(Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoTransaccion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperBusiness", "ListarTipoOperacion", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Tipo de TRansaccion Permitidas para una Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion obtenidos</returns>
        public List<TipoOperacion> ListarTipoOpeTransac(TipoOperacion pTipoOpe, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoOpeTransac(pTipoOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperBusiness", "ListarTipoOpeTransac", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVent(TipoOperacion pTipoOpe, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoOpeTransacVent(pTipoOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperBusiness", "ListarTipoOpeTransacVent", ex);
                return null;
            }
        }
        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo(TipoOperacion pTipoOpe, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoOpeTransacVentRotativo(pTipoOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperBusiness", "ListarTipoOpeTransacVentRotativo", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo1(TipoOperacion pTipoOpe, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoOpeTransacVentRotativo1(pTipoOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperBusiness", "ListarTipoOpeTransacVentRotativo1", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene la lista de TipoOperacion-Tran Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion - Caja obtenidos</returns>
        public TipoOperacion ConsultarTipOpeTranCaja(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarTipOpeTranCaja(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTipOpeTranCaja", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene los datos del tipo de operacion
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion - Caja obtenidos</returns>
        public TipoOperacion ConsultarTipoOperacion(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarTipoOperacion(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTipoProducto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de TipoOperacion-Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion - Caja obtenidos</returns>
        public List<TipoOperacion> ConsultarTranCred(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                string parametro = DATipOpe.ParametroGeneral(pUsuario);
                if (parametro == "1")
                {
                    return DATipOpe.ConsultarTranCreditosboucher(pEntidad, pUsuario);
                }
                else
                {
                    return DATipOpe.ConsultarTranCred(pEntidad, pUsuario);
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTranCred", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de TipoOperacion-Caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Operacion - Caja obtenidos</returns>
        public TipoOperacion ConsultarValIva(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarValIva(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarValIva", ex);
                return null;
            }
        }

        // ---------------------------------------------------------------------------------------------------------------------------------- //

        public List<TipoOperacion> ListarTipoProducto(Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoProducto(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ListarTipoProducto", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoProductoCaja(Usuario pUsuario, Int64 caja)
        {
            try
            {
                return DATipOpe.ListarTipoProductoCaja(pUsuario, caja);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ListarTipoProductoCaja", ex);
                return null;
            }
        }

        public TipoProducto ConsultarTipoProducto(TipoProducto pEntidad, Usuario pUsuario)
        {
            try
            {
                TipoOperacion tipOpe = DATipOpe.ConsultarTipoProducto(pEntidad.cod_tipo_producto, pUsuario);
                pEntidad.descripcion = tipOpe.nom_tipo_producto;

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTipoProducto", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpe(Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoOpe(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ListarTipoOpe", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpGridBusines(Usuario pUsuario, String Filtro)
        {
            try
            {
                return DATipOpe.ListarTipoOpGrid(pUsuario, Filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ListarTipoOpe", ex);
                return null;
            }
        }

        public void EliminarTipoOpeBusinnes(Int64 id, Usuario pUsuario)
        {
            try
            {
                DATipOpe.EliminarTipoOpe(id, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "EliminarTipoOpeBusinnes", ex);
            }
        }

        public void insertTipoOPBusines(TipoOperacion entidad, Usuario pUsuario)
        {
            try
            {
                DATipOpe.insertTipoOP(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "insertTipoOPBusines", ex);
            }
        }

        public void ModificaTipoOpBusines(TipoOperacion entidad, Usuario pUsuario)
        {
            try
            {
                DATipOpe.ModificaTipoOp(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ModificaTipoOpBusines", ex);
            }
        }

        public List<TipoOperacion> validaDatosBusinne(Usuario pUsuario, Int64 TipoTra, String desc, Int64 opc)
        {
            try
            {
                return DATipOpe.validaDatos(pUsuario, TipoTra, desc, opc);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "validaDatosBusinne", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoProductoAhorros(Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ListarTipoProductoAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ListarTipoProductoAhorros", ex);
                return null;
            }
        }

        public TipoProducto ConsultarTipoProductoAhorros(TipoProducto pEntidad, Usuario pUsuario)
        {
            try
            {
                TipoOperacion tipOpe = new TipoOperacion();
                tipOpe = DATipOpe.ConsultarTipoProductoAhorros(pEntidad.cod_tipo_producto, pUsuario);
                pEntidad.descripcion = tipOpe.nom_tipo_producto;
                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionBusiness", "ConsultarTipoProductoAhorros", ex);
                return null;
            }
        }

        public string ConsultarFactura(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarFactura(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<TipoOperacion> ConsultarSaldoProductos(Int64 pCodOpe, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarSaldoProductos(pCodOpe, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public TipoOperacion ConsultarFacturaCompleta(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DATipOpe.ConsultarFacturaCompleta(pId, pUsuario);
            }
            catch
            {
                return null;
            }
        }
    }
}
