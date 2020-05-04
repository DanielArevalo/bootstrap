using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;


namespace Xpinn.CDATS.Business
{
    public class LiquidacionCDATBusiness : GlobalBusiness
    {
        LiquidacionCDATData BALiqui;

        public LiquidacionCDATBusiness()
        {
            BALiqui = new LiquidacionCDATData();
        }

        public void GENERAR_LiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BALiqui.GENERAR_LiquidacionCDAT(pLiqui, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "GENERAR_LiquidacionCDAT", ex);
            }
        }


        public List<LiquidacionCDAT> ListarTemporal_LiquidacionCDAT(LiquidacionCDAT pTemp, DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return BALiqui.ListarTemporal_LiquidacionCDAT(pTemp, pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "ListarTemporal_LiquidacionCDAT", ex);
                return null;
            }
        }




        public LiquidacionCDAT CrearLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {

                        foreach (LiquidacionCDAT rOpe in pLiqui.lstLista)
                        {
                            Xpinn.CDATS.Data.AperturaCDATData xApertura = new Xpinn.CDATS.Data.AperturaCDATData();
                            LiquidacionCDAT nLiquidacio = new LiquidacionCDAT();
                            nLiquidacio = BALiqui.CrearLiquidacionCDAT(rOpe, vUsuario);

                            Int64 cod = rOpe.codigo_cdat;

                            //CREACIÓN DE AUDITORIA
                            CDAT_AUDITORIA Audi = new CDAT_AUDITORIA();

                            Audi.cod_auditoria_cdat = 0;
                            Audi.codigo_cdat = cod;
                            Audi.tipo_registro_aud = 4;
                            Audi.fecha_aud = DateTime.Now;
                            Audi.cod_usuario_aud = vUsuario.codusuario;
                            Audi.ip_aud = vUsuario.IP;

                            xApertura.CrearAuditoriaCdat(Audi, vUsuario);//CREAR
                        }


                    }
                    ts.Complete();
                }
                return pLiqui;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "CrearOperacion", ex);
                return null;
            }
        }


        public LiquidacionCDAT CalculoLiquidacionCDAT(LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiqui = BALiqui.CalculoLiquidacionCDAT(pLiqui, vUsuario);
                    ts.Complete();
                }
                return pLiqui;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "CalculoLiquidacionCDAT", ex);
                return null;
            }
        }


        public void CierreLiquidacionCDAT(ref Int64 COD_OPE, ref string pError, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso,Xpinn.FabricaCreditos.Entities.Giro pGiro, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    COD_OPE = vOpe.cod_ope;
                    if (opcion == true)
                    {
                        //GRABAR NUEVO GIRO
                        Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                        Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }
                    //GRABAR LA FORMA DE PAGO DEL CDAT
                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();
                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pLiqui.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pLiqui.cod_deudor,
                            cod_ope = COD_OPE,
                            fecha_cierre = pLiqui.fecha_liquidacion,
                            V_Traslado = pLiqui.valor_pagar - pLiqui.valor_gmf,
                            codusuario = vUsuario.codusuario
                        };

                        Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                        DAAhorroVistaData.IngresoCuenta(ahorro, vUsuario);

                        if (ahorro.numero_cuenta != "" || ahorro.numero_cuenta != null)
                        {             
                            //DETERMINAR SI LA CUENTA DE AHORROS TIENE CUENTAS POR COBRAR PENDIENTES               
                            List<Xpinn.Ahorros.Entities.CuentasCobrar> lstCuentasCobrar = new List<Ahorros.Entities.CuentasCobrar>();
                            lstCuentasCobrar = DAAhorroVistaData.ListarCuentasCobrar(ahorro.numero_cuenta, vUsuario);

                            foreach (Xpinn.Ahorros.Entities.CuentasCobrar CuetaxCobrar in lstCuentasCobrar)
                            {
                                Xpinn.Caja.Entities.TransaccionCaja pTransaccionCaja = new Xpinn.Caja.Entities.TransaccionCaja();
                                pTransaccionCaja.cod_ope = ahorro.cod_ope;
                                pTransaccionCaja.fecha_movimiento = Convert.ToDateTime(ahorro.fecha_cierre);
                                pTransaccionCaja.cod_persona = Convert.ToInt64(ahorro.cod_persona);
                                CuetaxCobrar.tipo_tran = 257;
                                DAAhorroVistaData.ProcesoCuentasCobrar(pTransaccionCaja, CuetaxCobrar, vUsuario);
                            }
                        }
                    }

                    //GENERANDO CIERRE CDAT
                    pLiqui.cod_ope = vOpe.cod_ope;
                    BALiqui.CierreLiquidacionCDAT(pLiqui, ref pError, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }
        public void GuardarLiquidacionCDAT(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();

                    if (vOpe.cod_ope == 0 )
                    {
                      vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);

                    }

                    COD_OPE = vOpe.cod_ope;                 

                    Xpinn.FabricaCreditos.Data.Persona1Data personaServices = new Xpinn.FabricaCreditos.Data.Persona1Data();
                   
                    if (opcion == true)
                    {
                        //GRABAR NUEVO GIRO
                        Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                        Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        pGiro.valor = Convert.ToInt64(pLiqui.valor_pagar);
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }
                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();

                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {

                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pLiqui.cta_ahorros.ToString(),
                            cod_persona = pLiqui.cod_deudor,
                            cod_ope = COD_OPE,
                            fecha_cierre = pLiqui.fecha_liquidacion,
                            V_Traslado = pLiqui.valor_pagar,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, vUsuario);
                    }


                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {
                        foreach (LiquidacionCDAT rOpe in pLiqui.lstLista)
                        {                         

                            //GENERANDO LIQUIDACION INTERES  CDAT
                            rOpe.cod_ope = vOpe.cod_ope;
                            BALiqui.LiquidacionInteresCDAT(rOpe, vUsuario);
                        }
                        ts.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "CierreLiquidacionCDAT", ex);
            }
        }


        public void GuardarLiquidacionCDATMasivos(ref Int64 COD_OPE, Xpinn.Tesoreria.Entities.Operacion pOperacion, bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();


                    
                    Xpinn.FabricaCreditos.Data.Persona1Data personaServices = new Xpinn.FabricaCreditos.Data.Persona1Data();

                    if (opcion == true)
                    {
                        //GRABAR NUEVO GIRO
                        Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                        Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.cod_ope = COD_OPE;
                        pGiro.valor = Convert.ToInt64(pLiqui.valor_pagar);
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }
                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();

                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {

                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pLiqui.cta_ahorros.ToString(),
                            cod_persona = pLiqui.cod_deudor,
                            cod_ope = COD_OPE,
                            fecha_cierre = pLiqui.fecha_liquidacion,
                            V_Traslado = pLiqui.valor_pagar,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuentamasivo(ahorro, vUsuario);
                    }


                    //GENERANDO LIQUIDACION INTERES  CDAT
                            pLiqui.cod_ope = COD_OPE;
                            BALiqui.LiquidacionInteresCDAT(pLiqui, vUsuario);
                       
                        ts.Complete();
                    
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "GuardarLiquidacionCDATMasivos", ex);
            }
        }
        public void GuardarLiquidacionCDATPrroroga(bool opcion, long formadesembolso, Xpinn.FabricaCreditos.Entities.Giro pGiro, ref Int64 COD_OPE, Cdat pCdatModificar, ProrrogaCDAT pProrroga, CDAT_AUDITORIA pAuditoria, Xpinn.Tesoreria.Entities.Operacion pOperacion, LiquidacionCDAT pLiqui, Usuario vUsuario)
        {
            try
            {
                ProrrogaCDATData BAProrro = new ProrrogaCDATData();
                AperturaCDATData BAApertura = new AperturaCDATData();
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABACION DE LA OPERACION
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    pCdatModificar.cod_ope = vOpe.cod_ope;

                    //MODIFICANDO LOS CAMBIOS DEL CDAT
                    pCdatModificar = BAProrro.ModificarCDATProrroga(pCdatModificar, vUsuario);

                    pProrroga.cod_ope = vOpe.cod_ope;
                    //REGISTRANDO LA PRORROGA
                    pProrroga = BAProrro.CrearCDATProrroga(pProrroga, vUsuario);

                    //REGISTRANDO AUDITORIA - NO ES LA AUDITORIA QUE SOLO ANULA
                    pAuditoria = BAApertura.CrearAuditoriaCdat(pAuditoria, vUsuario);

                    //REALIZANDO LA LIQUIDACION
                    COD_OPE = vOpe.cod_ope;
                    pLiqui.cod_ope = COD_OPE;
                    BALiqui.LiquidacionInteresCDAT(pLiqui, vUsuario);

                    if (opcion == true)
                    {
                        //GRABAR NUEVO GIRO
                        Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                        Xpinn.FabricaCreditos.Entities.Giro vGiroEntidad = new Xpinn.FabricaCreditos.Entities.Giro();
                        pGiro.cod_ope = vOpe.cod_ope;
                        vGiroEntidad = GiroData.CrearGiro(pGiro, vUsuario, 1);
                    }
                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();

                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {

                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {

                            numero_cuenta = pLiqui.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pLiqui.cod_deudor,
                            cod_ope = COD_OPE,
                            fecha_cierre = pLiqui.fecha_liquidacion,
                            V_Traslado = pLiqui.valor_pagar,
                            codusuario = vUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, vUsuario);
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "CierreLiquidacionCDAT", ex);
            }
        }


        public LiquidacionCDAT Listartitular(LiquidacionCDAT pLiqui, Usuario pUsuario)
        {
            try
            {
                return BALiqui.Listartitular(pLiqui, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "Listartitular", ex);
                return null;
            }
        }


        public LiquidacionCDAT ConsultarCierreCdats(Usuario pUsuario)
        {
            try
            {
                return BALiqui.ConsultarCierreCdats(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCDATBusiness", "ConsultarCierreCdats", ex);
                return null;
            }
        }




    }
}
