using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;
using Xpinn.Util;

namespace Xpinn.Synchronization.Controllers
{
    public class CajeroController : GlobalApiController
    {
        SyncCajeroServices ServicesCajero;
        SyncProductosServices ServicesProductos;
        public CajeroController()
        {
            ServicesCajero = new SyncCajeroServices();
            ServicesProductos = new SyncProductosServices();
        }

        public IHttpActionResult CrearModSyncCajero(EntityGlobal pCajero)
        {
            if (pCajero == null) return BadRequest("Cajero nulo!.");
            if (string.IsNullOrEmpty(pCajero.Message)) return BadRequest("No se realizó el envío de manera correcta!.");
            if (pCajero.NroRegisterAdd != 1 && pCajero.NroRegisterAdd != 2) return BadRequest("Error en el envio de datos, Opcion incorrecta!.");
            try
            {
                string[] strCajero = pCajero.Message.Split('|');

                EntityGlobal pResult = new EntityGlobal();
                if (pCajero.NroRegisterAdd == 1)
                {
                    //CONSULTAR SI YA SE CREO EL CAJERO
                    string pFiltro = " WHERE H.TABLA = 'CAJERO' AND H.CAMPO_TABLA = 'COD_CAJERO' AND H.CODIGO_LOCAL = '" + strCajero[0] + "'";
                    pFiltro += " AND H.PROCESO = '" + ProcesosOffline.CreacionCajero.ToString() + "'";
                    SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);
                    if (pHomologa != null)
                    {
                        pResult.Success = true;
                        pResult.CodigoGenerado = pHomologa.codigo_principal;
                        return Ok(pResult);
                    }
                }

                pResult = ServicesCajero.CrearModSyncCajero(strCajero, pCajero.NroRegisterAdd, Usuario);
                if (string.IsNullOrEmpty(pResult.CodigoGenerado))
                    return InternalServerError(new Exception("Se generó un error interno al realizar el proceso del Cajero"));
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult EliminarCajero(EntityGlobal pObjCajero)
        {
            if (pObjCajero == null)
                return BadRequest("El objeto pCajero no puede ser nulo");
            if (pObjCajero.NroRegisterAffected == 0)
                return BadRequest("El código del cajero es incorrecto");

            try
            {
                SyncCajero pCajero = new SyncCajero();
                pCajero.cod_cajero = Convert.ToInt64(pObjCajero.NroRegisterAffected);
                EntityGlobal pResult = ServicesCajero.EliminarSyncCajero(pCajero, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult AsignarCajero(EntityGlobal pObjCajero)
        {
            if (pObjCajero == null)
                return BadRequest("El objeto pCajero no puede ser nulo");
            if (string.IsNullOrEmpty(pObjCajero.Message))
                return BadRequest("El código del cajero es incorrecto");
            try
            {
                EntityGlobal pResult = ServicesCajero.AsignarSyncCajero(pObjCajero.Message, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult ListarCajeros()
        {
            try
            {
                List<SyncCajero> lstCajeros = ServicesCajero.ListarSyncCajero("", Usuario);
                return Ok(lstCajeros);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
