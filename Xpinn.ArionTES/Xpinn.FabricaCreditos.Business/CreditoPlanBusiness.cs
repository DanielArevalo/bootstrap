using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Data;

namespace Xpinn.FabricaCreditos.Business
{
    public class CreditoPlanBusiness : GlobalData
    {
        private CreditoPlanData DACreditoPlan;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
         public CreditoPlanBusiness()
        {
            DACreditoPlan = new CreditoPlanData();
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarCreditoPlan(CreditoPlan pCreditoPlan, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACreditoPlan.ListarCredito(pCreditoPlan, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanBusiness", "ListarCreditoPlan", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public CreditoPlan ConsultarCredito(Int64 pId, Boolean btasa, Usuario pUsuario)
        {
            try
            {
                return DACreditoPlan.ConsultarCredito(pId, btasa, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanBusiness", "ConsultarCredito", ex);
                return null;
            }
        }


        public DatosSolicitud ConsultarProveedorXCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACreditoPlan.ConsultarProveedorXCredito(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanBusiness", "ConsultarProveedorXCredito", ex);
                return null;
            }
        }



        /// <summary>
        /// Servicio para Liquidar Crédito
        /// </summary>
        /// <param name="pEntity">Entidad Liquidar</param>
        /// <returns>Entidad creada</returns>
        public CreditoPlan Liquidar(CreditoPlan pCredito, Usuario pUsuario)
        {
            try
            {
                return DACreditoPlan.Liquidar(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlan", "Liquidar", ex);
                return null;
            }
        }

        public CreditoEntity LiquidarWS(CreditoPlan pCredito, bool pManejaTransferencia, decimal pVr_compra, decimal pVr_beneficio, decimal pVr_Mercado, int pCodProceso, Usuario pUsuario, bool pGeneraDesembolso)
        {
            CreditoEntity pResult = new CreditoEntity();
            try
            {
                CreditoData DACredito = new CreditoData();
                CreditoSolicitadoData DACreditoSolicitado = new CreditoSolicitadoData();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    Credito pCreditoDesembolso = null;
                    CreditoPlan pCreditoPlan = null;
                    pCreditoPlan = DACreditoPlan.Liquidar(pCredito, pUsuario);

                    //CONSULTANDO CREDITO
                    CreditoSolicitado eCredito = new CreditoSolicitado();
                    eCredito.NumeroCredito = pCreditoPlan.Numero_Radicacion;
                    eCredito = DACreditoSolicitado.ConsultarCredito(eCredito, pUsuario);

                    pResult.numero_radicacion = eCredito.NumeroCredito;
                    // PROCESO DE GENERACION DE DEBEMBOLSO
                    if (pGeneraDesembolso)
                    {
                        // REALIZANDO CONVERSION DE FORMA DE PAGO
                        eCredito.forma_pago = eCredito.forma_pago == "1" || eCredito.forma_pago == "C" ? "C" : "N";
                        // CREANDO EMPRESA RECAUDO
                        // SE ASIGNARA POR DEFAULT EL 100% A LA EMPRESA RECAUDADORA A LA QUE PERTENECE EL CREDITO
                        if (eCredito.forma_pago == "N")
                        {
                            CreditoEmpresaRecaudo pEmpRecaudo = new CreditoEmpresaRecaudo();
                            pEmpRecaudo.numero_radicacion = eCredito.NumeroCredito;
                            pEmpRecaudo.cod_empresa = Convert.ToInt32(eCredito.cod_empresa);
                            pEmpRecaudo.porcentaje = 100;
                            pEmpRecaudo.valor = eCredito.Cuota;
                            DACredito.CrearModEmpresa_Recaudo(pEmpRecaudo, pUsuario, 1);
                        }

                        DateTime? FechaInicio = null;
                        if (eCredito.fecha_primer_pago == null)
                            FechaInicio = DACredito.FechaInicioDESEMBOLSO(eCredito.NumeroCredito, pUsuario);

                        // MODIFICANDO DATOS DE APROBACION
                        eCredito.Nombres = pUsuario.nombre;
                        eCredito.observaciones = null;
                        eCredito.fecha = DateTime.Now;
                        eCredito.reqpoliza = 0;
                        eCredito.fecha_primer_pago = FechaInicio;
                        eCredito.ObservacionesAprobacion = "APROBACION PROCESO BOLETERIA";
                        eCredito.cod_Periodicidad = Convert.ToInt32(eCredito.Cod_Periodicidad);

                        eCredito = DACreditoSolicitado.AprobarCreditoModificando(eCredito, pUsuario);


                        // GENERANDO DESEMBOLSO
                        decimal pMontoDesembolso = 0;
                        string _error = string.Empty;
                        pCreditoDesembolso = new Credito();
                        pCreditoDesembolso.numero_radicacion = eCredito.NumeroCredito;
                        pCreditoDesembolso.fecha_prim_pago = FechaInicio;
                        pCreditoDesembolso.estado = "C";
                        pCreditoDesembolso.fecha_desembolso = DateTime.Now;

                        long pCodOpe = DACredito.DesembolsarCredito(pCreditoDesembolso, ref pMontoDesembolso, ref _error, pUsuario);
                        if (_error.Trim() != "")
                        {
                            pResult = new CreditoEntity();
                            pResult.esCorrecto = false;
                            pResult.mensaje = _error;
                            ts.Dispose();
                            return pResult;
                        }
                        pResult.cod_ope = pCodOpe;

                        // CONSULTANDO SI ES ORDEN DE SERVICIO
                        string pFiltro = " WHERE NUMERO_RADICACION = " + eCredito.NumeroCredito;
                        CreditoOrdenServicio pCreditoOrden = DACredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);

                        bool generaGiro = pCreditoOrden.idordenservicio != 0 && pCreditoOrden.idproveedor != null ? false : true;
                        // GIRO A REALIZAR POR DEFECTO EN EFECTIVO
                        if (generaGiro)
                        {
                            // SE ESTABLECE POR DEFAULT DESEMBOLSO EN EFECTIVO
                            DACredito.GuardarGiro(eCredito.NumeroCredito, pCodOpe, 0, DateTime.Now, Convert.ToDouble(pMontoDesembolso), 0, 0, null, -1, eCredito.CodigoCliente, pUsuario.nombre, pUsuario);
                        }
                        else
                        {
                            // MODIFICANDO EL NUMERO DE PRE IMPRESION PARA LA ORDEN
                            DACredito.ModificarCreditoOrdenServicio(eCredito.NumeroCredito, eCredito.NumeroCredito, pUsuario);
                        }

                        //INSERTANDO REGISTROS EN TRANSFERENCIA_SOLIDARIA
                        if (pManejaTransferencia)
                        {
                            TransferenciaSolidariaData DATransferencia = new TransferenciaSolidariaData();
                            TransferenciaSolidaria pTransSolid = new TransferenciaSolidaria();
                            pTransSolid.cod_persona = Convert.ToInt32(eCredito.CodigoCliente);
                            pTransSolid.num_producto = eCredito.NumeroCredito.ToString();
                            pTransSolid.cod_linea_producto = Convert.ToInt32(eCredito.cod_linea_credito);
                            pTransSolid.tipo_producto = Convert.ToInt32(TipoDeProducto.Credito);
                            pTransSolid.cod_destinacion = null;
                            pTransSolid.monto = Convert.ToDecimal(eCredito.Monto);
                            pTransSolid.fecha_transferencia = DateTime.Today;
                            pTransSolid.valor_compra = pVr_compra;
                            pTransSolid.beneficio = pVr_beneficio;
                            pTransSolid.valor_mercado = pVr_Mercado;
                            pTransSolid.cod_ope = pCodOpe;
                            pTransSolid = DATransferencia.CrearTransferenciaSolidaria(pTransSolid, pUsuario);
                        }

                        Int64 pnum_comp = 0, ptipo_comp = 0, pProcesoCont = 0;

                        //CAPTURANDO EL CODIGO DE PROCESO CONTABLE
                        Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();
                        Xpinn.Contabilidad.Data.ProcesoContableData procesoContable = new Xpinn.Contabilidad.Data.ProcesoContableData();
                        Xpinn.Contabilidad.Entities.ProcesoContable eproceso = new Xpinn.Contabilidad.Entities.ProcesoContable();

                        eproceso.tipo_ope = 1;
                        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProcesoContable = procesoContable.ListarProcesoContable(eproceso, pUsuario);
                        if (lstProcesoContable.Count == 0)
                        {
                            pResult = new CreditoEntity();
                            pResult.esCorrecto = false;
                            pResult.mensaje = "No se encontraron procesos contables para realizar el desembolso de servicio Tipo de Operación = 1 (Desembolsos) ";
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

                        if (!ExistRegister)
                        {
                            if (lstProcesoContable.Count > 1)
                            {
                                //CONSULTANDO PARAMETRO GENERAL EL PROCESO CONTABLE
                                Xpinn.Comun.Data.GeneralData DAGeneral = new Comun.Data.GeneralData();
                                Xpinn.Comun.Entities.General pGeneral = new Comun.Entities.General();
                                pGeneral = DAGeneral.ConsultarGeneral(90168, pUsuario);
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
                        
                        //CREANDO EL COMPROBANTE
                        if (comprobanteData.GenerarComprobante(pCodOpe, 1, DateTime.Now, pUsuario.cod_oficina, eCredito.CodigoCliente, pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario))
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
                                pResult = new CreditoEntity();
                                pResult.esCorrecto = false;
                                pResult.mensaje = pError;
                                ts.Dispose();
                                return pResult;
                            }
                        }
                    }
                    ts.Complete();
                }

            }
            catch (Exception ex)
            {
                pResult = new CreditoEntity();
                pResult.esCorrecto = false;
                pResult.mensaje = ex.Message;
            }
            return pResult;
        }



        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<CreditoPlan> ListarScoringCredito(CreditoPlan pCreditoPlan, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACreditoPlan.ListarScoringCredito(pCreditoPlan, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoPlanBusiness", "ListarCreditoPlan", ex);
                return null;
            }
        }
    }
}
