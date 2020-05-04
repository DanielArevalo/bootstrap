using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Data;
using Xpinn.Util;
using System.Transactions;

namespace Xpinn.Servicios.Business
{
    public class SolicitudServiciosBusiness :GlobalBusiness
    {
        private SolicitudServiciosData DAServicio;

        public SolicitudServiciosBusiness()
        {
            DAServicio = new SolicitudServiciosData();
        }


        public Servicio CrearSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int cod;
                    pServicio = DAServicio.CrearSolicitudServicio(pServicio, vUsuario);

                    cod = pServicio.numero_servicio;

                    if (pServicio.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (DetalleServicio eServ in pServicio.lstDetalle)
                        {
                            DetalleServicio nDetalle = new DetalleServicio();
                            eServ.numero_servicio = cod;
                            nDetalle = DAServicio.CrearDetalleServicio(eServ, vUsuario);
                            num += 1;
                        }
                    }                    

                    ts.Complete();
                }

                return pServicio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "CrearSolicitudServicio", ex);
                return null;
            }
        }


        public Servicio ModificarSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pServicio = DAServicio.ModificarSolicitudServicio(pServicio, vUsuario);

                    int Cod;
                    Cod = pServicio.numero_servicio;

                    if (pServicio.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (DetalleServicio eServi in pServicio.lstDetalle)
                        {
                            eServi.numero_servicio = Cod;
                            DetalleServicio nProgramacion = new DetalleServicio();
                            if (eServi.codserbeneficiario <= 0 || eServi.codserbeneficiario == null)
                                nProgramacion = DAServicio.CrearDetalleServicio(eServi, vUsuario);
                            else
                                nProgramacion = DAServicio.ModificarDetalleServicio(eServi, vUsuario);
                            num += 1;
                        }
                    }
                   
                    ts.Complete();
                }

                return pServicio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ModificarSolicitudServicio", ex);
                return null;
            }
        }

        public Servicio ConsultarDatosPlanDePagos(Servicio servicio, Usuario usuario)
        {
            try
            {
                return DAServicio.ConsultarDatosPlanDePagos(servicio, usuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ConsultarDatosPlanDePagos", ex);
                return null;
            }
        }

        public List<Servicio> ListarSolicitudServicio(string filtro, Usuario usuario)
        {
            try
            {
                return DAServicio.ListarSolicitudServicio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaServiciosBusiness", "ListarSolicitudServicio", ex);
                return null;
            }
        }

        public List<Servicio> ListarServicios(Servicio pServicio,DateTime pFechaIni,Usuario vUsuario,string filtro)
        {
            try
            {
                return DAServicio.ListarServicios(pServicio,pFechaIni,vUsuario,filtro);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ListarServicios", ex);
                return null;
            }
        }


        public void EliminarServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAServicio.EliminarServicio(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "EliminarServicio", ex);
            }
        }


        public Servicio ConsultarSERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ConsultarSERVICIO(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ConsultarSERVICIO", ex);
                return null;
            }
        }

        public bool ConsultarEstadoPersona(Int64? pCodPersona, string pIdentificacion, string pEstado, Usuario pUsuario)
        {
            try
            {
                return DAServicio.ConsultarEstadoPersona(pCodPersona, pIdentificacion, pEstado, pUsuario);

            }
            catch 
            {
                return false;
            }
        }


        public void EliminarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAServicio.EliminarDETALLESERVICIO(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "EliminarDETALLESERVICIO", ex);
            }
        }

        public int ConsultarNumeroServiciosPersona(string cod_persona, string cod_linea, Usuario usuario)
        {
            try
            {
                return DAServicio.ConsultarNumeroServiciosPersona(cod_persona, cod_linea, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ConsultarNumeroServiciosPersona", ex);
                return 0;
            }
        }

        public List<DetalleServicio> ConsultarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ConsultarDETALLESERVICIO(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ConsultarDETALLESERVICIO", ex);
                return null;
            }
        }


        public List<Servicio> CargarPlanXLinea(Int64 pVar, Usuario vUsuario)
        {
            try
            {
                return DAServicio.CargarPlanXLinea(pVar, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "CargarPlanXLinea", ex);
                return null;
            }
        }

        public bool ModificarEstadoSolicitudservicio(Servicio solicitud, Usuario usuario)
        {
            try
            {
                return DAServicio.ModificarEstadoSolicitudServicio(solicitud, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarEstadoSolicitudservicio", ex);
                return false;
            }
        }

        public Servicio ConsultaProveedorXlinea(Int32 pVar, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ConsultaProveedorXlinea(pVar, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "ConsultaProveedorXlinea", ex);
                return null;
            }
        }




        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAServicio.ObtenerSiguienteCodigo(pUsuario);
            }
            catch 
            {
                return 1;
            }
        }

        public ServicioEntity CrearServicioDesembolsoPorWS(Servicio pServicio, Xpinn.Tesoreria.Entities.Operacion pOperacion, int pTipo_servicio, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, int pCodProceso, Usuario vUsuario)
        {
            ServicioEntity pResult = new ServicioEntity();
            pResult.esCorrecto = false;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    int cod;
                    Int64 pCodOpe = 0;

                    //CREANDO EL SERVICIO
                    pServicio = DAServicio.CrearSolicitudServicio(pServicio, vUsuario);
                    cod = pServicio.numero_servicio;
                    pResult.numero_servicio = cod;

                    //CREANDO BENEFICIARIOS
                    if (pServicio.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (DetalleServicio eServ in pServicio.lstDetalle)
                        {
                            DetalleServicio nDetalle = new DetalleServicio();
                            eServ.numero_servicio = cod;
                            nDetalle = DAServicio.CrearDetalleServicio(eServ, vUsuario);
                            num += 1;
                        }
                    }
                    
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData OperacionData = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOperacion = new Tesoreria.Entities.Operacion();
                    vOperacion = OperacionData.GrabarOperacion(pOperacion, vUsuario);
                    pCodOpe = vOperacion.cod_ope;
                    pResult.cod_ope = pCodOpe;

                    //GRABAR TRANSACCION_SERVICIO Y ACTUALIZAR LA TABLA SERVICIOS
                    DesembolsoServicioData BADesembolso = new DesembolsoServicioData();
                    DesembosoServicios pTran = new DesembosoServicios();
                    pTran.numero_transaccion = 0;
                    pTran.numero_servicio = cod;
                    pTran.cod_ope = pCodOpe;
                    pTran.cod_cliente = pServicio.cod_persona;
                    pTran.cod_linea_servicio = pServicio.cod_linea_servicio;
                    pTran.tipo_tran = 1;
                    pTran.cod_det_lis = 0; 
                    pTran.cod_atr = 1;
                    // Si el servicio es diferente de tipo Orden de Servicio entonces el valor es por el total en caso contrario es por la cuota
                    pTran.valor = pTipo_servicio != 5 ? Convert.ToDecimal(pServicio.valor_total) : Convert.ToDecimal(pServicio.valor_cuota);
                    pTran.fecha_desembolso = vOperacion.fecha_oper;
                    pTran = BADesembolso.CrearTransaccionDesembolso(pTran, vUsuario);

                    //RENOVACION
                    CausacionServiciosData DARENOVACION = new CausacionServiciosData();
                    RenovacionServicios entidad = new RenovacionServicios();
                    RenovacionServicios pRenova = new RenovacionServicios();
                    pRenova.idrenovacion = 0;
                    pRenova.numero_servicio = Convert.ToInt64(cod);
                    pRenova.fecha_renovacion = DateTime.Now;
                    pRenova.cod_ope = pCodOpe;
                    pRenova.fecha_inicial_vigencia = pServicio.fecha_inicio_vigencia;
                    pRenova.fecha_final_vigencia = pServicio.fecha_final_vigencia;
                    pRenova.valor_total = pServicio.valor_total;
                    pRenova.valor_cuota = pServicio.valor_cuota;
                    pRenova.plazo = pServicio.numero_cuotas;
                    pRenova.tipo = 2;
                    entidad = DARENOVACION.CrearRenovacionServicios(pRenova, vUsuario);

                    Int64 pnum_comp = 0, ptipo_comp = 0, pProcesoCont = 0;
                    Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();

                    //CONSULTANDO VALOR DE LA CUOTA
                    Servicio pEntServicio = DAServicio.ConsultarSERVICIO(Convert.ToInt64(cod), vUsuario);


                    //CAPTURANDO EL CODIGO DE PROCESO CONTABLE
                    Xpinn.Contabilidad.Data.ProcesoContableData procesoContable = new Xpinn.Contabilidad.Data.ProcesoContableData();
                    Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();
                    eproceso.tipo_ope = 110;
                    List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProcesoContable = procesoContable.ListarProcesoContable(eproceso, vUsuario);

                    if (lstProcesoContable.Count == 0)
                    {
                        pResult = new ServicioEntity();
                        pResult.esCorrecto = false;
                        pResult.mensaje = "No se encontraron procesos contables para realizar el desembolso de servicio Tipo de Operación = 110 ";
                        ts.Dispose();
                        return pResult;
                    }

                    bool ExistRegister = false;
                    if (pCodProceso > 0)
                    {
                        ExistRegister = lstProcesoContable.Where(x => x.cod_proceso == Convert.ToInt64(pCodProceso)).Any();
                        if (ExistRegister)
                            ExistRegister = true;
                    }
                    
                    //INSERTANDO REGISTROS EN TRANSFERENCIA_SOLIDARIA
                    TransferenciaSolidariaData DATransferencia = new TransferenciaSolidariaData();
                    TransferenciaSolidaria pTransSolid = new TransferenciaSolidaria();
                    pTransSolid.cod_persona = Convert.ToInt32(pServicio.cod_persona);
                    pTransSolid.num_producto = cod.ToString();
                    pTransSolid.cod_linea_producto = Convert.ToInt32(pServicio.cod_linea_servicio);
                    pTransSolid.tipo_producto = Convert.ToInt32(TipoDeProducto.Servicios);
                    pTransSolid.cod_destinacion = null;
                    pTransSolid.monto = pEntServicio.valor_total != null ? Convert.ToDecimal(pEntServicio.valor_total) : 0;
                    pTransSolid.fecha_transferencia = pOperacion.fecha_oper;
                    pTransSolid.valor_compra = pVr_compra;
                    pTransSolid.beneficio = pVr_beneficio;
                    pTransSolid.valor_mercado = pVr_Mercado;
                    pTransSolid.cod_ope = pCodOpe;

                    // GENERO VALOR BOOLEAN PARA IDENTIFICAR EL TIPO DE CONTABILIZACION
                    pTransSolid.es_bono_contribucion = pCodProceso != 0 && ExistRegister ? 1 : 0;
                    pTransSolid = DATransferencia.CrearTransferenciaSolidaria(pTransSolid, vUsuario);
                    
                    if (!ExistRegister)
                    {
                        if (lstProcesoContable.Count > 1)
                        {
                            //CONSULTANDO PARAMETRO GENERAL EL PROCESO CONTABLE
                            Xpinn.Comun.Data.GeneralData DAGeneral = new Comun.Data.GeneralData();
                            Xpinn.Comun.Entities.General pGeneral = new Comun.Entities.General();
                            pGeneral = DAGeneral.ConsultarGeneral(90166, vUsuario);
                            if (pGeneral != null)
                            {
                                if (!string.IsNullOrEmpty(pGeneral.valor))
                                    pProcesoCont = Convert.ToInt64(pGeneral.valor);
                                else
                                    pProcesoCont = lstProcesoContable[0].cod_proceso;
                            }
                            else
                                pProcesoCont = lstProcesoContable[0].cod_proceso;
                        }
                        else
                            pProcesoCont = lstProcesoContable[0].cod_proceso;
                    }
                    else
                        pProcesoCont = Convert.ToInt64(lstProcesoContable.Where(x => x.cod_proceso == Convert.ToInt64(pCodProceso)).Select(y => y.cod_proceso).FirstOrDefault());

                    string pError = string.Empty;
                    Int64 pCod_Proveedor = 0;
                    if (pCodProceso != 0 && ExistRegister)
                        pCod_Proveedor = pServicio.cod_persona;
                    else
                        pCod_Proveedor = pServicio.codigo_proveedor != null ? Convert.ToInt64(pServicio.codigo_proveedor) : 0;

                    //CREANDO EL COMPROBANTE
                    if (comprobanteData.GenerarComprobante(pCodOpe, 110, pOperacion.fecha_oper, vUsuario.cod_oficina, pCod_Proveedor, pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, vUsuario))
                    {
                        pResult.esCorrecto = true;
                        pResult.num_comp = pnum_comp;
                        pResult.tipo_comp = ptipo_comp;
                    }
                    else
                    {
                        // Si no pudo generar el comprobante enviar error
                        if (pError.Trim() != "")
                        {
                            pResult = new ServicioEntity();
                            pResult.esCorrecto = false;
                            pResult.mensaje = pError;
                            ts.Dispose();
                            return pResult;
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pResult = new ServicioEntity();
                pResult.esCorrecto = false;
                pResult.mensaje = ex.Message;
            }
            return pResult;
        }

        public Servicio CrearSolicitudServicioOficinaVirtual(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pServicio = DAServicio.CrearSolicitudServicioOficinaVirtual(pServicio, vUsuario);                    
                    ts.Complete();
                }
                return pServicio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudServiciosBusiness", "CrearSolicitudServicioOficinaVirtual", ex);
                return null;
            }
        }


    }
}
