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
    public class AprobacionServiciosBusiness :GlobalBusiness
    {
        private AprobacionServiciosData DAServicio;

        public AprobacionServiciosBusiness()
        {
            DAServicio = new AprobacionServiciosData();
        }

        public List<Servicio> ListarOficinas(Servicio pPerso, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ListarOficinas(pPerso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AperturaCDATBusiness", "ListarOficinas", ex);
                return null;
            }
        }

        public List<Servicio> Reportemovimiento(Servicio pAhorroVista, Usuario vUsuario)
        {
            try
            {
                return DAServicio.Reportemovimiento(pAhorroVista, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "Reportemovimiento", ex);
                return null;
            }
        }

        public Servicio ModificarSolicitudServicio(Servicio pServicio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
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
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ModificarSolicitudServicio", ex);
                return null;
            }
        }



        public List<Servicio> ListarServicios(string filtro, string pOrden, DateTime pFechaIni, DateTime pFecPago, Usuario vUsuario, int estadoCuenta = 0)
        {
            try
            {
                return DAServicio.ListarServicios(filtro,pOrden, pFechaIni, pFecPago, vUsuario, estadoCuenta);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ListarServicios", ex);
                return null;
            }
        }

        public List<Servicio> ListarServiciosClubAhorrador(Int64 pCodPersona, string pFiltro, Boolean pResult, Usuario pUsuario)
        {
            try
            {
                return DAServicio.ListarServiciosClubAhorrador(pCodPersona, pFiltro, pResult, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ListarServiciosClubAhorrador", ex);
                return null;
            }
        }

        public List<Servicio> ListarCuentasPersona(Int64 pCod_Persona, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ListarCuentasPersona(pCod_Persona, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ListarCuentasPersona", ex);
                return null;
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
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ConsultarSERVICIO", ex);
                return null;
            }
        }



        public void EliminarDETALLESERVICIO(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    DAServicio.EliminarDETALLESERVICIO(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "EliminarDETALLESERVICIO", ex);
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
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ConsultarDETALLESERVICIO", ex);
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
                BOExcepcion.Throw("AprobacionServiciosBusiness", "CargarPlanXLinea", ex);
                return null;
            }
        }



        public CONTROLSERVICIOS CrearControlServicios(CONTROLSERVICIOS pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    pControl = DAServicio.CrearControlServicios(pControl, vUsuario);
                    ts.Complete();
                }
                return pControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "CrearControlServicios", ex);
                return null;
            }
        }

        public CONTROLSERVICIOS ConsultarControlServicio(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DAServicio.ConsultarControlServicio(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ConsultarControlServicio", ex);
                return null;
            }
        }

       

        public void AprobarSolicitud(List<Servicio> lstServicio, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    if (lstServicio.Count > 0)
                    {
                        foreach (Servicio nServi in lstServicio)
                        {
                            CONTROLSERVICIOS pEntidad = new CONTROLSERVICIOS();
                            CONTROLSERVICIOS pControl = new CONTROLSERVICIOS();
                            pControl.idcontrol_servicios = 0;
                            pControl.numero_servicio = nServi.numero_servicio;
                            pControl.codtipo_proceso = 2;
                            pControl.fechaproceso = DateTime.Now;
                            pEntidad = DAServicio.CrearControlServicios(pControl, vUsuario);

                            Servicio pServicio = new Servicio();
                            pServicio = DAServicio.AprobarSolicitud(nServi, vUsuario);
                        }
                    }


                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "AprobarSolicitud", ex);
            }
        }


        //MODIFICACION DE SERVICIOS
        public bool ModificarServiciosActivos(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion,Servicio pServicio, Usuario vUsuario, bool generaComprobante = true)
        {
            try
            {

                TransactionOptions opc = new TransactionOptions();
                opc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    //GRABACION DE LA OPERACION

                    int Cod = pServicio.numero_servicio;

                    if (generaComprobante)
                    {
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

                        pOperacion = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                        COD_OPE = pOperacion.cod_ope;

                        Servicio vServicio = new Servicio();
                        vServicio.numero_servicio = Cod;
                        vServicio.cod_ope = COD_OPE;
                        vServicio.cod_cliente = pServicio.cod_persona;
                        vServicio.cod_linea_servicio = pServicio.cod_linea_servicio;
                        vServicio.valor_total = pServicio.valor_total;

                        vServicio = DAServicio.CrearTranServicios(vServicio, vUsuario);
                    }

                    pServicio = DAServicio.ModificarServiciosActivos(pServicio, vUsuario);
                    
                    if (pServicio.lstDetalle != null)
                    {
                        int num = 0;
                        foreach (DetalleServicio eServi in pServicio.lstDetalle)
                        {
                            eServi.numero_servicio = Cod;
                            DetalleServicio nProgramacion = new DetalleServicio();
                            if (eServi.codserbeneficiario <= 0)
                                nProgramacion = DAServicio.CrearDetalleServicio(eServi, vUsuario);
                            else
                                nProgramacion = DAServicio.ModificarDetalleServicio(eServi, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                    return true;
                }

               
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ModificarServiciosActivos", ex);
                return false;
            }
        }


        //CARGAR SERVICIOS
        public void RegistrarServiciosCargados(ref Int64 vCod_Ope,Xpinn.Tesoreria.Entities.Operacion pOperacion,string Cod_Linea,List<Servicio> lstServicios, Usuario vUsuario)
        {
            try
            {
                TransactionOptions opc = new TransactionOptions();
                opc.Timeout = TimeSpan.MaxValue;

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    SolicitudServiciosData SolicitarData = new SolicitudServiciosData();
                    DesembolsoServicioData BADesembolso = new DesembolsoServicioData();
                    if (lstServicios != null && lstServicios.Count > 0)
                    {
                        //OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Tesoreria.Data.OperacionData();
                        pOperacion = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                        vCod_Ope = pOperacion.cod_ope;

                        foreach (Servicio eServi in lstServicios)
                        {
                            if (eServi.cod_persona != 0 && eServi.valor_total != 0)
                            {
                                Servicio nServicio = new Servicio();
                                eServi.numero_servicio = 0;
                                eServi.cod_linea_servicio = Cod_Linea;
                                eServi.cuotas_pendientes = Convert.ToInt32(eServi.numero_cuotas);
                                nServicio = SolicitarData.CrearSolicitudServicio(eServi, vUsuario);

                                // TRANSACCION
                                DesembosoServicios pDesembolso = new DesembosoServicios();
                                pDesembolso.numero_transaccion = 0;
                                pDesembolso.numero_servicio = nServicio.numero_servicio;
                                pDesembolso.cod_ope = vCod_Ope;
                                pDesembolso.fecha_desembolso = pOperacion.fecha_oper;
                                pDesembolso.fecha_primer_pago = nServicio.fecha_proximo_pago;
                                pDesembolso.cod_cliente = nServicio.cod_persona;
                                pDesembolso.cod_linea_servicio = Cod_Linea;
                                pDesembolso.tipo_tran = 1;
                                pDesembolso.cod_det_lis = 0; // *
                                pDesembolso.cod_atr = 1; //*
                                pDesembolso.valor = Convert.ToDecimal(nServicio.valor_total);
                                pDesembolso.estado = 1;
                                pDesembolso.num_tran_anula = 0;//*
                                pDesembolso = BADesembolso.CrearTransaccionDesembolso(pDesembolso, vUsuario);

                                //RENOVACION
                                CausacionServiciosData DARENOVACION = new CausacionServiciosData();
                                RenovacionServicios entidad = new RenovacionServicios();
                                RenovacionServicios pRenova = new RenovacionServicios();
                                pRenova.idrenovacion = 0;
                                pRenova.numero_servicio = nServicio.numero_servicio;
                                pRenova.fecha_renovacion = DateTime.Now;
                                pRenova.cod_ope = vCod_Ope;

                                pRenova.fecha_inicial_vigencia = eServi.fecha_inicio_vigencia;

                                pRenova.fecha_final_vigencia = eServi.fecha_final_vigencia;

                                eServi.valor_total = eServi.valor_total != null ? eServi.valor_total : 0;
                                pRenova.valor_total = eServi.valor_total;

                                eServi.valor_cuota = eServi.valor_cuota != null ? eServi.valor_cuota : 0;
                                pRenova.valor_cuota = eServi.valor_cuota;

                                eServi.numero_cuotas = eServi.numero_cuotas != null ? eServi.numero_cuotas : 0;
                                pRenova.plazo = eServi.numero_cuotas;
                                pRenova.tipo = 3;
                                entidad = DARENOVACION.CrearRenovacionServicios(pRenova, vUsuario);
                            }
                        }
                    }

                    ts.Complete();
                    
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "RegistrarServiciosCargados", ex);
            }
        }


        //EXCLUIR SERVICIOS
        public void ExcluirServicios(ref Int64 Cod_ope, Servicio pServicio,Xpinn.Tesoreria.Entities.Operacion pOperacion, ExclusionServicios pExclusion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Tesoreria.Entities.Operacion();
                    //CREACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    Cod_ope = vOpe.cod_ope; 
                    //ACTUALIZACION DE LA TABLA SERVICIOS
                    Servicio pServi = new Servicio();
                    pServi = DAServicio.ModificarServiciosExcluidos(pServicio, vUsuario);

                    //INSERCION EN LA TABLA EXCLUSION_SERVICIOS
                    pExclusion.cod_ope = Convert.ToInt32(vOpe.cod_ope);
                    ExclusionServicios pEntidad = new ExclusionServicios();
                    pEntidad = DAServicio.CrearExclusionServicios(pExclusion, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "ExcluirServicios", ex);
            }
        }


        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return DAServicio.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public Servicio CrearTranServicios(Servicio pControl, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    pControl = DAServicio.CrearTranServicios(pControl, vUsuario);
                    ts.Complete();
                }
                return pControl;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "CrearControlServicios", ex);
                return null;
            }
        }
        
        public string CancelarServicio(Int64 NumServicio, Usuario vUsuario)
        {
            try
            {
                return DAServicio.CancelarServicio(NumServicio, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AprobacionServiciosBusiness", "CancelarServicio", ex);
                return "";
            }
        }
    }
}
