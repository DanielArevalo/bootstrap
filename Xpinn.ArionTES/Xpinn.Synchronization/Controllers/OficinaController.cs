using System;
using System.Collections.Generic;
using System.Web.Http;
using Xpinn.Sincronizacion.Entities;
using Xpinn.Sincronizacion.Services;
using Xpinn.Util;

namespace Xpinn.Synchronization.Controllers
{
    public class OficinaController : GlobalApiController
    {
        SyncOficinaServices ServicesOficina;
        SyncProductosServices ServicesProductos;
        public OficinaController()
        {
            ServicesOficina = new SyncOficinaServices();
            ServicesProductos = new SyncProductosServices();
        }

        public IHttpActionResult GenerarAperturaCierre(EntityGlobal pOficina)
        {
            if (pOficina == null) return BadRequest("Oficina nulo!.");
            if (string.IsNullOrEmpty(pOficina.Message)) return BadRequest("No se realizó el envío de manera correcta!.");
            try
            {
                string[] strProceso = pOficina.Message.Split('|');
                EntityGlobal pResult = new EntityGlobal();
                // CONSULTAR SI EXISTE REGISTRO GENERADO EN FINANCIAL
                string[] pCampos = pOficina.Message.Split('|');
                if (string.IsNullOrEmpty(pCampos[1]) && string.IsNullOrEmpty(pCampos[4]))
                {
                    pResult.Success = false;
                    pResult.Message = "Error al enviar los datos de apertura o cierre de oficina";
                    return Ok(pResult);
                }

                string pFiltro = " WHERE COD_OFICINA = " + pCampos[1];
                pFiltro += " AND TO_CHAR(FECHA_PROCESO, 'dd/MM/yyyy') = '" + pCampos[2];
                pFiltro += "' AND TIPO_PROCESO = " + pCampos[4];
                SyncProcesoOficina pProceso = ServicesOficina.ConsultarSyncProcesoOficina(pFiltro, Usuario);
                if (pProceso != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = pProceso.consecutivo.ToString();
                    return Ok(pResult);
                }

                //CONSULTAR SI YA SE CREO EL PROCESO DE OFICINA
                string pCondi = strProceso[1] + "|" + strProceso[4] + "|" + strProceso[2];
                pFiltro = " WHERE H.TABLA = 'PROCESOOFICINA' AND H.CODIGO_LOCAL = '" + pCondi + "'";
                pFiltro += " AND H.PROCESO = '" + ProcesosOffline.AperturaCierreOficina.ToString() + "'";
                SyncHomologaOperacion pHomologa = ServicesProductos.ConsultarHomologacionOperacion(pFiltro, Usuario);
                if (pHomologa != null)
                {
                    pResult.Success = true;
                    pResult.CodigoGenerado = strProceso[1];
                    return Ok(pResult);
                }
                
                pResult = ServicesOficina.GenerarAperturaCierre(pOficina.Message, Usuario);

                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult CrearSyncOficina(Oficina pOficina)
        {
            if (pOficina == null) return BadRequest("Oficina nulo!.");
            try
            {
                Oficina pResult = ServicesOficina.CrearModSyncOficina(pOficina, 1, Usuario);
                if (pResult.cod_oficina == 0)
                    return InternalServerError(new Exception("Se generó un error interno al realizar la creación de la oficina"));
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        public IHttpActionResult ModificarSyncOficina(Oficina pOficina)
        {
            if (pOficina == null) return BadRequest("Oficina nulo!.");
            try
            {
                Oficina pResult = ServicesOficina.CrearModSyncOficina(pOficina, 2, Usuario);
                if (pResult.cod_oficina == 0)
                    return InternalServerError(new Exception("Se generó un error interno al realizar la creación de la oficina"));
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult EliminarOficina(Oficina pOficina)
        {
            if (pOficina == null)
                return BadRequest("El objeto pOficina no puede ser nulo");
            if (pOficina.cod_oficina == 0)
                return BadRequest("El código de la Oficina es incorrecto");

            try
            {
                EntityGlobal pResult = ServicesOficina.EliminarSyncOficina(pOficina, Usuario);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
        
        public IHttpActionResult ConsultarUltCierreOficina(EntityGlobal pEntidad)
        {
            if (pEntidad == null) return BadRequest("Objeto nulo!.");
            if (pEntidad.NroRegisterAffected == 0) return BadRequest("No se realizó el envío de manera correcta!.");
            try
            {
                EntityGlobal pResult = new EntityGlobal();
                DateTime? pFecha = ServicesOficina.ConsultarFecUltCierre(pEntidad.NroRegisterAffected, Usuario);
                pResult.Success = pFecha == null ? false : true;
                if(pResult.Success)
                    pResult.fechaGenerica = Convert.ToDateTime(pFecha);
                return Ok(pResult);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult ListarOficinas()
        {
            try
            {
                List<Oficina> lstOficinas = ServicesOficina.ListarSyncOficina("", Usuario);
                return Ok(lstOficinas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        public IHttpActionResult ListarHorariosOficina()
        {
            try
            {
                List<SyncHorarioOficina> lstOficinas = ServicesOficina.ListarSyncHorarioOficina("", Usuario);
                return Ok(lstOficinas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        public IHttpActionResult ListarProcesoOficinas(EntityGlobal pEntity)
        {
            if (pEntity == null)
                return BadRequest("El objeto pEntity no puede ser nulo");
            string pFilter = !string.IsNullOrEmpty(pEntity.Filter) ? pEntity.Filter : "";
            try
            {
                List<SyncProcesoOficina> lstOficinas = ServicesOficina.ListarSyncProcesoOficina(pFilter, Usuario);
                return Ok(lstOficinas);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}
