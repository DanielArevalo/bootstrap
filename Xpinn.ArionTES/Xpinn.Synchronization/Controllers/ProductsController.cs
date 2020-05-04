using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;
using Xpinn.Util;

namespace Xpinn.Synchronization.Controllers
{
    public class ProductsController : GlobalApiController
    {
        SyncProductosServices ServicesProductos;
        public ProductsController()
        {
            ServicesProductos = new SyncProductosServices();
        }

        public IHttpActionResult ListarProductosPersona(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<Producto> lstProductos = ServicesProductos.ListarProductosPersona(pFilter, Usuario);
                return Ok(lstProductos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ListarTiraProductosPersona(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            if (pEntity.NroRegisterAdd == 0)
                return BadRequest("No se especifico el tipo de producto a la que se consultara los productos");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<ObjectString> lstProductos = ServicesProductos.ListarTiraProductosPersona(pEntity.NroRegisterAdd, pFilter, Usuario);
                return Ok(lstProductos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ConsultarCantidadProductos(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            try
            {
                EntityGlobal pResult = ServicesProductos.SyncCantidadProductos(pEntity.NroRegisterAdd, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult ListarTiraProductosPendientes(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            if (pEntity.fechaGenerica == DateTime.MinValue)
                return BadRequest("No se especifico la fecha para realizar la petición de productos");
            if (string.IsNullOrEmpty(pEntity.Filter))
                return BadRequest("No se especifico el tipo de producto a la que se consultara los productos");
            try
            {
                List<ObjectString> lstProductos = ServicesProductos.ListarTiraProductosPendientes(pEntity.fechaGenerica, pEntity.Filter, Usuario);
                return Ok(lstProductos);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        //CREACION DE REGISTRO DE OPERACIONES
        public IHttpActionResult CrearSyncOperacion(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");
            try
            {
                //CONSULTAR SI YA SE GENERO LA OPERACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.RegistroOperacion.ToString() + "'";

                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                {
                    //GENERANDO LA OPERACION
                    pResult = ServicesProductos.CrearSyncOperacion(pOperacion, Usuario);
                }
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult CrearSyncConsignacion(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");
            try
            {
                //CONSULTAR SI YA SE GENERO LA CONSIGNACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.ConsignacionOperacion.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                    pResult = ServicesProductos.CrearSyncConsignacion(pOperacion, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult CrearSyncChequeCanje(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");

            try
            {
                //CONSULTAR SI YA SE GENERO LA CONSIGNACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.ChequeCanje.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                    pResult = ServicesProductos.CrearSyncCanjeCheque(pOperacion, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult CrearSyncTrasladoDinero(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");

            try
            {
                //CONSULTAR SI YA SE GENERO LA CONSIGNACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.TrasladoDinero.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                    pResult = ServicesProductos.CrearSyncTrasladoDinero(pOperacion, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult CrearSyncRecepcionDinero(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");

            try
            {
                //CONSULTAR SI YA SE GENERO LA CONSIGNACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.RecepcionDinero.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                    pResult = ServicesProductos.CrearSyncRecepcionDinero(pOperacion, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult CrearSyncReversionOperacion(SyncOperacion pOperacion)
        {
            if (pOperacion == null) return BadRequest("Objeto de Operacion nulo!.");
            if (pOperacion.cod_ope == 0) return BadRequest("El código de la operacion es inválido!.");

            try
            {
                //CONSULTAR SI YA SE GENERO LA CONSIGNACION
                string pFiltro = " WHERE H.TABLA = 'OPERACION' AND H.CAMPO_TABLA = 'COD_OPE' AND H.CODIGO_LOCAL = '" + pOperacion.cod_ope + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.ReversionOperacion.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);

                EntityGlobal pResult = new EntityGlobal();
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pHomologa.codigo_principal;
                    pResult.num_comp = pHomologa.num_comp;
                    pResult.tipo_comp = pHomologa.tipo_comp;
                }
                else
                    pResult = ServicesProductos.CrearSyncReversionOperacion(pOperacion, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
