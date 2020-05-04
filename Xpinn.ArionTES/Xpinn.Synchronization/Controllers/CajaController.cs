using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;
using Xpinn.Util;

namespace Xpinn.Synchronization.Controllers
{
    public class CajaController : GlobalApiController
    {
        SyncCajaServices ServicesCaja;
        SyncProductosServices ServicesProductos;
        public CajaController()
        {
            ServicesCaja = new SyncCajaServices();
            ServicesProductos = new SyncProductosServices();
        }

        public IHttpActionResult CrearModSyncCaja(EntityGlobal pObjCaja)
        {
            if (pObjCaja == null) return BadRequest("Caja nulo!.");
            if (string.IsNullOrEmpty(pObjCaja.Message)) return BadRequest("No se realizó de manera correcta los datos de la caja!.");
            if (pObjCaja.NroRegisterAdd != 1 && pObjCaja.NroRegisterAdd != 2) return BadRequest("Error en el envio de datos, Opcion incorrecta!.");
            try
            {
                //GENERANDO OBJETOS RECIBIDOS

                string[] strObject = pObjCaja.Message.Split('_');
                string[] objCaja = null;
                string[] objLstTopes = null;
                string[] objLstAtribuciones = null;

                objCaja = strObject[0].Split('|');

                if (string.IsNullOrEmpty(objCaja[0]) || objCaja[0] == "0")
                    return BadRequest("Los datos de la caja no son válidos!.");

                objLstTopes = strObject[1].Contains(";") ? strObject[1].Split(';') : new string[] { strObject[1] };
                objLstAtribuciones = strObject[2].Contains(";") ? strObject[2].Split(';') : new string[] { strObject[2] };

                EntityGlobal pResult = new EntityGlobal();
                if (pObjCaja.NroRegisterAdd == 1)
                {
                    //CONSULTAR SI YA SE CREO LA CAJA
                    string pFiltro = " WHERE H.TABLA = 'CAJA' AND H.CAMPO_TABLA = 'COD_CAJA' AND H.CODIGO_LOCAL = '" + objCaja[0] + "'";
                    pFiltro += " AND H.PROCESO = '" + ProcesosOffline.CreacionCaja.ToString() + "'";
                    SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);
                    if (pHomologa != null)
                    {
                        pResult.Success = true;
                        pResult.CodigoGenerado = pHomologa.codigo_principal;
                        return Ok(pResult);
                    }
                }

                pResult = ServicesCaja.CrearModSyncCaja(objCaja, objLstTopes, objLstAtribuciones, pObjCaja.NroRegisterAdd, Usuario);
                if (string.IsNullOrEmpty(pResult.CodigoGenerado))
                    return InternalServerError(new Exception("Se generó un error interno al realizar el proceso de la Caja"));
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult AperturaCierreCaja(EntityGlobal pObjCaja)
        {
            if (pObjCaja == null) return BadRequest("Caja nulo!.");
            if (string.IsNullOrEmpty(pObjCaja.Message)) return BadRequest("No se realizó de manera correcta los datos de la caja!.");
            try
            {
                string[] objResult = pObjCaja.Message.Split('_');
                string objCaja = objResult[0];
                string objSaldo = objResult[1];

                EntityGlobal pResult = ServicesCaja.AperturaCierreSyncCaja(objCaja, objSaldo, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult EliminarCaja(SyncCaja pCaja)
        {
            if (pCaja == null)
                return BadRequest("El objeto pCaja no puede ser nulo");
            if (pCaja.cod_caja <= 0)
                return BadRequest("El código de la caja es incorrecto");
            try
            {
                EntityGlobal pResult = ServicesCaja.EliminarSyncCaja(pCaja, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [HttpGet]
        public IHttpActionResult ListarCajas()
        {
            string pFiltro = "";
            try
            {
                List<SyncCaja> lstCajas = ServicesCaja.ListarSyncCaja(pFiltro, Usuario);
                return Ok(lstCajas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ListaTopesCaja(EntityGlobal pEntidad)
        {
            if (pEntidad == null)
                return BadRequest("El objeto no puede ser nulo");
            string pFiltro = pEntidad.Filter != "" ? pEntidad.Filter : "";
            try
            {
                List<SyncTopesCaja> lstCajas = ServicesCaja.ListarSyncTopesCaja(pFiltro, Usuario);
                return Ok(lstCajas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ListarAtribucionCaja(EntityGlobal pEntidad)
        {
            if (pEntidad == null)
                return BadRequest("El objeto no puede ser nulo");
            string pFiltro = pEntidad.Filter != "" ? pEntidad.Filter : "";
            try
            {
                pFiltro = pFiltro.Contains("WHERE") ? pFiltro + " AND TIPO_OPERACION IN (SELECT TIPO_TRAN FROM TIPO_TRAN)" : " WHERE TIPO_OPERACION IN (SELECT TIPO_TRAN FROM TIPO_TRAN)";
                List <SyncAtribucionCaja> lstCajas = ServicesCaja.ListarSyncAtribucionCaja(pFiltro, Usuario);
                return Ok(lstCajas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult ListarTiposTran(EntityGlobal pEntidad)
        {
            if (pEntidad == null)
                return BadRequest("El objeto no puede ser nulo");
            try
            {
                List<SyncTipoTran> lstTransacciones = ServicesCaja.ListarSyncTiposTran(pEntidad.NroRegisterAffected, Usuario);
                return Ok(lstTransacciones);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
