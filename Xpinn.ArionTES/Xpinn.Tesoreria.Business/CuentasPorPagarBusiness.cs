using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Tesoreria.Business
{

    public class CuentasPorPagarBusiness : GlobalBusiness
    {
        private CuentasPorPagarData DACuentas;
        private AprobacionCtasPorPagarData DAAprobacion;

        public CuentasPorPagarBusiness()
        {
            DACuentas = new CuentasPorPagarData();
            DAAprobacion = new AprobacionCtasPorPagarData();
        }

        public CUENTAXPAGAR_ANTICIPO CrearCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {

                        //GRABAR NUEVO GIRO
                        Xpinn.Tesoreria.Entities.Giro pgiro = new Xpinn.Tesoreria.Entities.Giro();
                        CuentasPorPagarData GiroData = new CuentasPorPagarData();
                        Xpinn.Tesoreria.Entities.Giro vGiroEntidad = new Xpinn.Tesoreria.Entities.Giro();
                        pGiro.cod_ope = pOperacion.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, pusuario, 1);

                        //GRABAR DATOS
                        pCUENTAXPAGAR_ANTICIPO = DACuentas.CrearCUENTAXPAGAR_ANTICIPO(pGiro,pOperacion,  pCUENTAXPAGAR_ANTICIPO, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pCUENTAXPAGAR_ANTICIPO;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOBusiness", "CrearCUENTAXPAGAR_ANTICIPO", ex);
                    return null;
                }
            }


        public CUENTAXPAGAR_ANTICIPO ModificarCUENTAXPAGAR_ANTICIPO(Xpinn.Tesoreria.Entities.Giro pGiro, Xpinn.Tesoreria.Entities.Operacion pOperacion, CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        //GRABAR NUEVO GIRO
                        Xpinn.Tesoreria.Entities.Giro pgiro = new Xpinn.Tesoreria.Entities.Giro();
                        CuentasPorPagarData GiroData = new CuentasPorPagarData();
                        Xpinn.Tesoreria.Entities.Giro vGiroEntidad = new Xpinn.Tesoreria.Entities.Giro();
                        pGiro.cod_ope = pOperacion.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, pusuario,1);

                        //GRABAR DATOS
                        pCUENTAXPAGAR_ANTICIPO = DACuentas.ModificarCUENTAXPAGAR_ANTICIPO(pGiro, pOperacion , pCUENTAXPAGAR_ANTICIPO, pusuario);
 
                        ts.Complete();
 
                    }
 
                    return pCUENTAXPAGAR_ANTICIPO;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOBusiness", "ModificarCUENTAXPAGAR_ANTICIPO", ex);
                    return null;
                }
            }
 
 
            public void EliminarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario pusuario)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        DACuentas.EliminarCUENTAXPAGAR_ANTICIPO(pId, pusuario);
 
                        ts.Complete();
 
                    }
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOBusiness", "EliminarCUENTAXPAGAR_ANTICIPO", ex);
                }
            }
 
 
            public CUENTAXPAGAR_ANTICIPO ConsultarCUENTAXPAGAR_ANTICIPO(Int64 pId, Usuario pusuario)
            {
                try
                {
                    CUENTAXPAGAR_ANTICIPO CUENTAXPAGAR_ANTICIPO = new CUENTAXPAGAR_ANTICIPO();
                    CUENTAXPAGAR_ANTICIPO = DACuentas.ConsultarCUENTAXPAGAR_ANTICIPO(pId, pusuario);
                    return CUENTAXPAGAR_ANTICIPO;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOBusiness", "ConsultarCUENTAXPAGAR_ANTICIPO", ex);
                    return null;
                }
            }
 
 
            public List<CUENTAXPAGAR_ANTICIPO> ListarCUENTAXPAGAR_ANTICIPO(CUENTAXPAGAR_ANTICIPO pCUENTAXPAGAR_ANTICIPO, Usuario pusuario)
            {
                try
                {
                   return DACuentas.ListarCUENTAXPAGAR_ANTICIPO(pCUENTAXPAGAR_ANTICIPO, pusuario);
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("CUENTAXPAGAR_ANTICIPOBusiness", "ListarCUENTAXPAGAR_ANTICIPO", ex);
                    return null;
                }
            }




     public CuentasPorPagar CrearCuentasXpagar(CuentasPorPagar pCuentas, Operacion pOperacion, bool opcion, long formadesembolso, Giro pGiro, Usuario vUsuario)
        {
            int idcuentabancaria = 0;
            int cod_banco = 0;
            String num_cuenta = "";
            int tipo_cuenta = 0;
            int coddetalleimp = 0;

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    pCuentas.cod_ope = vOpe.cod_ope; 
                    pCuentas = DACuentas.CrearCuentasXpagar(pCuentas,  vUsuario);
                    int cod = pCuentas.codigo_factura;
                    if (pCuentas.lstDetalle != null)
                    {
                        coddetalleimp = 0;
                        foreach (CuentaXpagar_Detalle eCuent in pCuentas.lstDetalle)
                        {
                            CuentaXpagar_Detalle nCuentasXp = new CuentaXpagar_Detalle();
                            eCuent.codigo_factura = cod;
                            nCuentasXp = DACuentas.CrearCuentasxPagarDetalle(eCuent, vUsuario);
                           
                            //GRABANDO LOS IMPUESTOS POR CADA CONCEPTO
                            if (eCuent.lstImpuesto != null && eCuent.lstImpuesto.Count > 0)
                            {
                                foreach (Concepto_CuentasXpagarImp Impuesto in eCuent.lstImpuesto)
                                {
                                    CuentasXpagarImpuesto Imp = new CuentasXpagarImpuesto();

                                    CuentasXpagarImpuesto pEntidad = new CuentasXpagarImpuesto();
                                    pEntidad.coddetalleimp = 0;
                                    pEntidad.coddetallefac = nCuentasXp.coddetallefac;
                                    pEntidad.codigo_factura = pCuentas.codigo_factura;
                                    pEntidad.cod_tipo_impuesto = Convert.ToInt32(Impuesto.cod_tipo_impuesto);
                                    pEntidad.porcentaje = Convert.ToDecimal(Impuesto.porcentaje_impuesto);
                                    pEntidad.idcuentadetalleimp = Convert.ToInt32(Impuesto.coddetalleimp);

                                    pEntidad.base_vr = nCuentasXp.valor_total;
                                  
                                    decimal ValorTotal = pEntidad.base_vr != null ? Convert.ToDecimal(pEntidad.base_vr) : 0;
                                    decimal Porcentaje = pEntidad.porcentaje != null ? Convert.ToDecimal(pEntidad.porcentaje) : 0;
                                    pEntidad.valor = Math.Round(ValorTotal * (Porcentaje / 100));
                                    if(pEntidad.valor>0)
                                    {
                                        Imp = DACuentas.CrearCuentaXpagarImpuesto(pEntidad, vUsuario);
                                    }
                                }
                            }
                        }
                    }

                    if (pCuentas.lstFormaPago != null)
                    {
                        foreach (CuentaXpagar_Pago eCuent in pCuentas.lstFormaPago)
                        {
                            CuentaXpagar_Pago nCuentasXp = new CuentaXpagar_Pago();
                            eCuent.codigo_factura = cod;
                            eCuent.estado = 0;
                            eCuent.porc_descuento = eCuent.porc_descuento == null ? 0 : eCuent.porc_descuento;
                            nCuentasXp = DACuentas.CrearCuentasXpagar_Pago(eCuent, vUsuario);
                        }
                    }

                    if (opcion == true)
                    {
                        //GRABAR NUEVO GIRO
                        GiroData GiroData = new GiroData();
                        Giro vGiroEntidad = new Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        pGiro.valor = Convert.ToInt64(pCuentas.valor_total);
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }



                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();

                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {

                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pCuentas.cta_ahorros.ToString(),
                            cod_persona = pCuentas.cod_persona,
                            cod_ope = vOpe.cod_ope,
                            fecha_cierre = pCuentas.fecha_crea,
                            V_Traslado = pCuentas.valor_total,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, vUsuario);
                    }

                   


                    ts.Complete();
                }
                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "CrearCuentasXpagar", ex);
                return null;
            }
        }


        public CuentasPorPagar ModificarCuentasXpagar(CuentasPorPagar pCuentas,Operacion pOperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    //Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    //vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    pCuentas = DACuentas.ModificarCuentasXpagar(pCuentas,  vUsuario);
                    int Cod = pCuentas.codigo_factura;

                    if (pCuentas.lstDetalle != null)
                    {
                        foreach (CuentaXpagar_Detalle eCuent in pCuentas.lstDetalle)
                        {
                            eCuent.codigo_factura = Cod;
                            CuentaXpagar_Detalle nCuentasXp = new CuentaXpagar_Detalle();
                            if (eCuent.coddetallefac <= 0 || eCuent.coddetallefac == null)
                                nCuentasXp = DACuentas.CrearCuentasxPagarDetalle(eCuent, vUsuario);
                            else
                                nCuentasXp = DACuentas.ModificarCuentasxPagarDetalle(eCuent, vUsuario);

                            //GRABANDO LOS IMPUESTOS POR CADA CONCEPTO
                            if (eCuent.lstImpuesto != null && eCuent.lstImpuesto.Count > 0)
                            {
                                foreach (Concepto_CuentasXpagarImp Impuesto in eCuent.lstImpuesto)
                                {
                                    CuentasXpagarImpuesto Imp = new CuentasXpagarImpuesto();

                                    CuentasXpagarImpuesto pEntidad = new CuentasXpagarImpuesto();
                                    pEntidad.coddetalleimp = Impuesto.coddetalleimp;
                                    pEntidad.coddetallefac = nCuentasXp.coddetallefac;
                                    pEntidad.codigo_factura = pCuentas.codigo_factura;
                                    pEntidad.cod_tipo_impuesto = Convert.ToInt32(Impuesto.cod_tipo_impuesto);
                                    pEntidad.porcentaje = Convert.ToDecimal(Impuesto.porcentaje_impuesto);
                                    pEntidad.base_vr = nCuentasXp.valor_total;
                                    decimal ValorTotal = pEntidad.base_vr != null ? Convert.ToDecimal(pEntidad.base_vr) : 0;
                                    decimal Porcentaje = pEntidad.porcentaje != null ? Convert.ToDecimal(pEntidad.porcentaje) : 0;
                                    pEntidad.valor = ValorTotal * (Porcentaje / 100);

                                    if (pEntidad.coddetalleimp > 0)
                                        Imp = DACuentas.ModificarCuentaXpagarImpuesto(pEntidad, vUsuario);
                                    else
                                        Imp = DACuentas.CrearCuentaXpagarImpuesto(pEntidad, vUsuario);
                                }
                            }
                        }
                    }

                    if (pCuentas.lstFormaPago != null)
                    {
                        int num = 0;
                        foreach (CuentaXpagar_Pago eCuent in pCuentas.lstFormaPago)
                        {
                            eCuent.codigo_factura = Cod;
                            CuentaXpagar_Pago nCuentasXp = new CuentaXpagar_Pago();
                            if (eCuent.codpagofac <= 0 || eCuent.codpagofac == null)
                                nCuentasXp = DACuentas.CrearCuentasXpagar_Pago(eCuent, vUsuario);
                            else
                                nCuentasXp = DACuentas.ModificarCuentasXpagar_Pago(eCuent, vUsuario);
                            num += 1;
                        }
                    }

                    ts.Complete();
                }
                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ModificarCuentasXpagar", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }


        public Int64 ObtenerSiguienteEquivalente(Usuario pUsuario)
        {
            try
            {
                return DACuentas.ObtenerSiguienteEquivalente(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        public void EliminarCuentasXpagar(Int32 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentas.EliminarCuentasXpagar(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "EliminarChequera", ex);
            }
        }

        public List<CuentasPorPagar> ListarAnticipos(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario, String filtro)
        {
            try
            {
                return DACuentas.ListarAnticipos(pCuentas, pFechaIni, pFechaFin, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarService", "ListarCuentasXpagar", ex);
                return null;
            }
        }

        public List<CuentasPorPagar> ListarCuentasXpagar(CuentasPorPagar pCuentas, DateTime pFechaIni, DateTime pFechaFin, DateTime pVencIni, DateTime pVencFin, Usuario vUsuario, String filtro)
        {
            try
            {
                return DACuentas.ListarCuentasXpagar(pCuentas, pFechaIni, pFechaFin, pVencIni, pVencFin, vUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ListarCuentasXpagar", ex);
                return null;
            }
        }


        public List<CuentaXpagar_Detalle> ConsultarDetalleCuentasXpagar(CuentaXpagar_Detalle pCuentas, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarDetalleCuentasXpagar(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarDetalleCuentasXpagar", ex);
                return null;
            }
        }

        public List<CuentaXpagar_Pago> ConsultarDetalleFormaPago(CuentaXpagar_Pago pCuentas, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarDetalleFormaPago(pCuentas, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarDetalleFormaPago", ex);
                return null;
            }
        }

        


        public CuentasPorPagar ConsultarCuentasXpagar(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuentas = DACuentas.ConsultarCuentasXpagar(pCuentas, vUsuario);

                    ts.Complete();
                }

                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarCuentasXpagar", ex);
                return null;
            }
        }




        public CuentasPorPagar CONSULTARANTICIPOS(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuentas = DACuentas.CONSULTARANTICIPOS(pCuentas, vUsuario);

                    ts.Complete();
                }

                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarCuentasXpagar", ex);
                return null;
            }
        }


        public CuentasPorPagar ConsultarDatosReporte(CuentasPorPagar pCuentas, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuentas = DACuentas.ConsultarDatosReporte(pCuentas, vUsuario);

                    ts.Complete();
                }

                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarDatosReporte", ex);
                return null;
            }
        }



        public void EliminarCuentasXpagarDetalles(Int32 pId, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentas.EliminarCuentasXpagarDetalles(pId, vUsuario,opcion);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "EliminarCuentasXpagarDetalles", ex);
            }
        }

        public List<CuentasXpagarImpuesto> ConsultarDetImpuestosXConcepto(CuentasXpagarImpuesto pImpuesto, Usuario vUsuario)
        {
            try
            {
                return DACuentas.ConsultarDetImpuestosXConcepto(pImpuesto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarDetImpuestosXConcepto", ex);
                return null;
            }
        }


        public CuentasPorPagar ConsultarGiro(CuentasPorPagar pOperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOperacion = DACuentas.ConsultarGiro(pOperacion, vUsuario);

                    ts.Complete();
                }

                return pOperacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarGiro", ex);
                return null;
            }
        }


        public CuentasPorPagar ConsultarGiroXfactura(CuentasPorPagar pOperacion, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pOperacion = DACuentas.ConsultarGiroXfactura(pOperacion, vUsuario);

                    ts.Complete();
                }

                return pOperacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasPorPagarBusiness", "ConsultarGiroXfactura", ex);
                return null;
            }
        }



    }
}