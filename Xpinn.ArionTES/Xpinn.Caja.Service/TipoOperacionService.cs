using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Caja.Business;
using Xpinn.Caja.Entities;
using Xpinn.Util;


namespace Xpinn.Caja.Services
{
    /// <summary>
    /// Servicio para TipoOperacion
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class TipoOperacionService
    {
        private Xpinn.Caja.Business.TipoOperacionBusiness BOTipOpe;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del Servicio para TipoOperacion
        /// </summary>
        public TipoOperacionService()
        {
            BOTipOpe = new TipoOperacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public int CodigoTipOpe;

        public string CodigoPrograma { get { return "40205"; } }

        /// <summary>
        /// Crea una Factura de Registro de Operaciones
        /// </summary>
        /// <param name="pEntity">Entidad ProcesoOficina</param>
        /// <returns>Entidad creada</returns>
        public TipoOperacion InsertarFactura(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.InsertarFactura(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InsertarFacturaService", "InsertarFactura", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene un TipoOperacion-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoOperacion-Caja</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public TipoOperacion ConsultarTipoOpeCaja(Int64 pId, Int64 pCaja, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTipoOpeCaja(pId, pCaja, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarOficina", ex);
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
                return BOTipOpe.ListarTipoTransaccion(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOperacion", ex);
                return null;
            }
        }



        /// <summary>
        /// Obtiene la lista de Tipos de TRansaccion segun la caja dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Tipos de Transaccion segun la caja y segun las opciones de transaccion habilitadas</returns>
        public List<TipoOperacion> ListarTipoOpeTransac(TipoOperacion pTipOpe, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoOpeTransac(pTipOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpeTransac", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVent(TipoOperacion pTipOpe, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoOpeTransacVent(pTipOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpeTransacVent", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TipoOperacion-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoOperacion-Caja</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public TipoOperacion ConsultarTipOpeTranCaja(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTipOpeTranCaja(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarTipOpeTranCaja", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TipoOperacion-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoOperacion-Caja</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public TipoOperacion ConsultarTipoOperacion(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTipoOperacion(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarTipoProducto", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un TipoOperacion-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoOperacion-Caja</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public List<TipoOperacion> ConsultarTranCred(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTranCred(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarTranCred", ex);
                return null;
            }
        }

        
        /// <summary>
        /// Obtiene un TipoOperacion-Caja
        /// </summary>
        /// <param name="pId">identificador del TipoOperacion-Caja</param>
        /// <returns>TipoOperacion-Caja consultada</returns>
        public TipoOperacion ConsultarValIva(TipoOperacion pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarValIva(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarValIva", ex);
                return null;
            }
        }


        // ---------------------------------------------------------------------------------------------------------------------------------- //

        public List<TipoOperacion> ListarTipoProducto(Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoProducto(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoProducto", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoProductoCaja(Usuario pUsuario, Int64 caja)
        {
            try
            {
                return BOTipOpe.ListarTipoProductoCaja(pUsuario, caja);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoProductoCaja", ex);
                return null;
            }
        }

        public TipoProducto ConsultarTipoProducto(TipoProducto pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTipoProducto(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarTipoProducto", ex);
                return null;
            }
        }


        public List<TipoOperacion> ListarTipoOpe(Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoOpe(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpe", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpGridServices(Usuario pUsuario, String Filtro) 
        {
            try
            {
                return BOTipOpe.ListarTipoOpGridBusines(pUsuario, Filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpGridBusines", ex);
                return null;
            }
        }

        public void EliminarTipoOpeServices(Int64 id, Usuario pUsuario) 
        {
            try
            {
                BOTipOpe.EliminarTipoOpeBusinnes(id, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "EliminarTipoOpeServices", ex);
            }
        }

        public void insertTipoOPBusines(TipoOperacion entidad, Usuario pUsuario) 
        {
            try
            {
                BOTipOpe.insertTipoOPBusines(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "EliminarTipoOpeServices", ex);
            }
        }

        public void ModificaTipoOpServices(TipoOperacion entidad, Usuario pUsuario) 
        {
            try
            {
                BOTipOpe.ModificaTipoOpBusines(entidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ModificaTipoOpServices", ex);
            }
        }

        public List<TipoOperacion> validaDatosBusinne(Usuario pUsuario, Int64 TipoTra, String desc, Int64 opc) 
        {
            try
            {
                return BOTipOpe.validaDatosBusinne(pUsuario, TipoTra, desc, opc);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ModificaTipoOpServices", ex);
                return null;
            }
        }


        public List<TipoOperacion> ListarTipoProductoAhorros(Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoProductoAhorros(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoProductoAhorros", ex);
                return null;
            }
        }


        public TipoProducto ConsultarTipoProductoAhorros(TipoProducto pEntidad, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarTipoProductoAhorros(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarTipoProductoAhorros", ex);
                return null;
            }
        }

        public string ConsultarFactura(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return null;
            }
            catch (Exception ex)            
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarFactura", ex);
                return null;
            }
        }

        public string ConsultarFactura(Int64 pId, bool pDatos, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarFactura(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarFactura", ex);
                return null;
            }
        }

        public List<TipoOperacion> ConsultarSaldoProductos(Int64 pCodOpe, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarSaldoProductos(pCodOpe, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo(TipoOperacion pTipOpe, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoOpeTransacVentRotativo(pTipOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpeTransacVentRotativo", ex);
                return null;
            }
        }

        public List<TipoOperacion> ListarTipoOpeTransacVentRotativo1(TipoOperacion pTipOpe, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ListarTipoOpeTransacVentRotativo1(pTipOpe, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ListarTipoOpeTransacVentRotativo1", ex);
                return null;
            }
        }


        public TipoOperacion ConsultarFacturaCompleta(Int64 pId, bool pDatos, Usuario pUsuario)
        {
            try
            {
                return BOTipOpe.ConsultarFacturaCompleta(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TipoOperacionService", "ConsultarFactura", ex);
                return null;
            }
        }

    }
}
