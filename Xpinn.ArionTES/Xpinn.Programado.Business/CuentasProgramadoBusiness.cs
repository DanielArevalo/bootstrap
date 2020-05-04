using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using Xpinn.FabricaCreditos.Data;
using Xpinn.Ahorros.Business;

namespace Xpinn.Programado.Business
{
    public class CuentasProgramadoBusiness : GlobalBusiness
    {
        private CuentasProgramadoData DACuentasPro;

        public CuentasProgramadoBusiness()
        {
            DACuentasPro = new CuentasProgramadoData();
        }


        public CuentasProgramado Crear_ModAhorroProgramado(CuentasProgramado pCuentas, Usuario vUsuario, Int32 opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCuentas = DACuentasPro.Crear_ModAhorroProgramado(pCuentas, vUsuario, opcion);

                    BeneficiarioData objBeneficiarioDATA = new BeneficiarioData();
                    foreach (Beneficiario objBeneficiario in pCuentas.lstBeneficiarios)
                    {
                        if (objBeneficiario.nombre_ben != "" && objBeneficiario.identificacion_ben != "")
                        {
                            objBeneficiario.numero_programado = pCuentas.numero_programado;
                            if (objBeneficiario.idbeneficiario > 0)
                                objBeneficiarioDATA.ModificarBeneficiarioAhorroProgramado(objBeneficiario, vUsuario);
                            else
                                objBeneficiarioDATA.CrearBeneficiarioAhorroProgramado(objBeneficiario, vUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CrearMod_LineasProgramado", ex);
                return null;
            }
        }

        public void EliminarAhorroProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BeneficiarioData DABeneficiario = new BeneficiarioData();
                    DABeneficiario.EliminarTodosBeneficiariosAhorroProgramado(pId, vUsuario);

                    DACuentasPro.EliminarAhorroProgramado(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "EliminarAhorroProgramado", ex);
            }
        }


        public List<CuentasProgramado> ListarAhorrosProgramado(String pFiltro, DateTime pFechaApe, Usuario vUsuario)
        {
            try
            {
                return DACuentasPro.ListarAhorrosProgramado(pFiltro, pFechaApe, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ListarAhorrosProgramado", ex);
                return null;
            }
        }

        public CuentasProgramado ConsultarAhorroProgramado(String pId, Usuario vUsuario)
        {
            try
            {
                CuentasProgramado objCuenta = DACuentasPro.ConsultarAhorroProgramado(pId, vUsuario);
                BeneficiarioData DABeneficiario = new BeneficiarioData();
                objCuenta.lstBeneficiarios = DABeneficiario.ConsultarBeneficiarioAhorroProgramado(pId, vUsuario);

                objCuenta.lstcuotasExtras = DACuentasPro.ConsultarCuotasExtrasProgramado(pId, vUsuario);


                return objCuenta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ConsultarAhorroProgramado", ex);
                return null;
            }
        }

        public CuentasProgramado ConsultarNumeracionProgramado(CuentasProgramado pAhorroProgramado, Usuario vUsuario)
        {
            try
            {
                return DACuentasPro.ConsultarNumeracionProgramado(pAhorroProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ConsultarNumeracionProgramado", ex);
                return null;
            }
        }
        public CuentasProgramado Consultartasayplazos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DACuentasPro.Consultartasayplazos(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "Consultartasayplazos", ex);
                return null;
            }
        }

        // reporte periodico bussine
        public List<ReporteCuentasPeriodico> ReportePeriodico(string pCodLinea, DateTime pFechaInicial, DateTime pFechaFinal, Int64 pCodOficina, Usuario vUsuario)
        {
            try
            {
                return DACuentasPro.ReportePeriodico(pCodLinea, pFechaInicial, pFechaFinal, pCodOficina, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ReportePeriodico", ex);
                return null;
            }
        }

        // carga la griv en la vista
        public List<CierreCuentaAhorroProgramado> cerrarCuentaProgramadobussine(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.cerrarCuentaProgramado(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ReportePeriodico", ex);
                return null;
            }
        }

        public List<CierreCuentaAhorroProgramado> ListarProrrogas(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.ListarProrrogas(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ListarProrrogas", ex);
                return null;
            }
        }

        public List<CierreCuentaAhorroProgramado> ListarRenovaciones(DateTime PfechaApertura, String Filtro, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.ListarRenovaciones(PfechaApertura, Filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ListarRenovaciones", ex);
                return null;
            }
        }

        //cargar el detalle
        public cierreCuentaDetalle cerrarCuentaDetalle(String pcodigo, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.cerrarCuentaProgramadoData(pcodigo, pusuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CerraCuentaDetalle", ex);
                return null;
            }
        }

        public cierreCuentaDetalle ProrrogarCuenta(String pcodigo, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.ProrrogarCuentaProgramadoData(pcodigo, pusuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ProrrogarCuenta", ex);
                return null;
            }
        }

        //carga los campos y las operaciones
        public cierreCuentaDetalle cerrarCuentasBusiness(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.cerrarCuenta(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CerraCuentaDetalle", ex);
                return null;
            }
        }

        public cierreCuentaDetalle AperturaCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.AperturaCuentasServices(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "AperturaCuentasServices", ex);
                return null;
            }
        }

        public cierreCuentaDetalle CrearRenovacionCuentasServices(cierreCuentaDetalle entidad, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.CrearRenovacionCuentasServices(entidad, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CrearRenovacionCuentasServices", ex);
                return null;
            }
        }

        public void cerrarCuentaCambiarEstadoBusiness(cierreCuentaDetalle entidad, ref Int64 cod_Operacion, Operacion poperacionentiti, Usuario pusuario, int tipo, Xpinn.CDATS.Entities.Cdat cdat, Xpinn.Tesoreria.Entities.Giro giro)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Grabacion de Operacion
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    entidadop = operacion.GrabarOperacion(poperacionentiti, pusuario);
                    cod_Operacion = entidadop.cod_ope;
                    if (tipo == 0)
                    {
                        Xpinn.Tesoreria.Data.GiroData GiroData = new Xpinn.Tesoreria.Data.GiroData();
                        //Grabacion del Giro
                        giro.cod_ope = cod_Operacion;
                        TipoFormaDesembolso formaPago = giro.forma_pago.ToEnum<TipoFormaDesembolso>();
                        if (formaPago != TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                            GiroData.Crear_ModGiro(giro, pusuario, 1);
                        else
                        {
                            AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista
                            {
                                numero_cuenta = giro.num_cuenta,
                                cod_persona = giro.cod_persona,
                                cod_ope = cod_Operacion,
                                fecha_cierre = entidad.fecha_Liquida,
                                V_Traslado = giro.valor,
                                codusuario = pusuario.codusuario
                            };

                            ahorroBusiness.IngresoCuenta(ahorro, pusuario);
                        }
                    }
                    else
                    {
                        Xpinn.CDATS.Data.AperturaCDATData CdatData = new Xpinn.CDATS.Data.AperturaCDATData();
                        Int64 cod = 0;
                        cdat = CdatData.CrearAperturaCDAT(cdat, pusuario);
                        cod = cdat.codigo_cdat;
                        //Creacion de Titular CDAT
                        if (cdat.lstDetalle != null && cdat.lstDetalle.Count > 0)
                        {
                            foreach (Xpinn.CDATS.Entities.Detalle_CDAT pDeta in cdat.lstDetalle)
                            {
                                Xpinn.CDATS.Entities.Detalle_CDAT nDetalle = new Xpinn.CDATS.Entities.Detalle_CDAT();
                                pDeta.codigo_cdat = cod;
                                nDetalle = CdatData.CrearTitularCdat(pDeta, pusuario, 1);
                            }
                        }
                        //Creacion de transaccion CDAT
                        cdat = CdatData.CrearTranCdat(cdat, cod_Operacion, pusuario);
                    }
                    //Generando el Cierre de la cuenta
                    DACuentasPro.CambiarEstadoCuenta(entidad, entidadop.cod_ope, pusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "cerrarCuentaCambiarEstado", ex);
            }
        }
        public void CerrarCuentaProgramadoServices(cierreCuentaDetalle entidad, ref Int64 cod_Operacion, Operacion poperacionentiti, Usuario pusuario, int tipo, Xpinn.Tesoreria.Entities.Giro giro)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Grabacion de Operacion
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    entidadop = operacion.GrabarOperacion(poperacionentiti, pusuario);
                    cod_Operacion = entidadop.cod_ope;
                    if (tipo == 0)
                    {
                        Xpinn.Tesoreria.Data.GiroData GiroData = new Xpinn.Tesoreria.Data.GiroData();
                        //Grabacion del Giro
                        giro.cod_ope = cod_Operacion;
                        TipoFormaDesembolso formaPago = giro.forma_pago.ToEnum<TipoFormaDesembolso>();
                        if (formaPago != TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                            GiroData.Crear_ModGiro(giro, pusuario, 1);
                        else
                        {
                            AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                            Xpinn.Ahorros.Entities.AhorroVista ahorro = new Xpinn.Ahorros.Entities.AhorroVista
                            {
                                numero_cuenta = giro.num_cuenta,
                                cod_persona = giro.cod_persona,
                                cod_ope = cod_Operacion,
                                fecha_cierre = entidad.fecha_Liquida,
                                V_Traslado = giro.valor,
                                codusuario = pusuario.codusuario
                            };

                            ahorroBusiness.IngresoCuenta(ahorro, pusuario);
                        }
                    }


                    //Generando el Cierre de la cuenta
                    DACuentasPro.CambiarEstadoCuenta(entidad, entidadop.cod_ope, pusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CerrarCuentaProgramadoServices", ex);
            }
        }

        public void Prorroga_programadoBusiness(cierreCuentaDetalle entidad, ref Int64 cod_Operacion, Operacion poperacionentiti, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Grabacion de Operacion
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    entidadop = operacion.GrabarOperacion(poperacionentiti, pusuario);
                    cod_Operacion = entidadop.cod_ope;                  
                    
                    //Generando el Cierre de la cuenta
                    DACuentasPro.Prorroga_programado(entidad, entidadop.cod_ope, pusuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "Prorroga_programadoBusiness", ex);
            }
        }

        public List<ELiquidacionInteres> getCuentasLiquidarBusinnes(DateTime pfechaLiquidacion, String pcodLinea, Usuario pusuario)
        {
            try
            {
                return DACuentasPro.getListaCuentasLiquidar(pfechaLiquidacion, pcodLinea, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "cerrarCuentaCambiarEstadoBusiness", ex);
                return null;
            }
        }

        public void guardarDatosLiquidacion(List<Etran_Programado> datosIntere, List<Etran_Programado> datosRetafuentes, Operacion pOperacion, Usuario pUsuario, ref Int64 codigo, DateTime fechaoperacion)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                pOperacion.fecha_oper = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                pOperacion.fecha_calc = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                pOperacion.fecha_real = DateTime.Now;
                entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);

                for (int i = 0; i < datosRetafuentes.Count && i < datosIntere.Count; i++)
                {
                    datosIntere[i].COD_OPE = entidadop.cod_ope;
                    datosRetafuentes[i].COD_OPE = entidadop.cod_ope;

                    if (datosIntere[i].VALOR != 0)
                        DACuentasPro.InsertLiquidacion(datosIntere[i].COD_OPE, datosIntere[i].NUMERO_PROGRAMADO, datosIntere[i].COD_CLIENTE, datosIntere[i].TIPO_TRAN, datosIntere[i].VALOR, pUsuario, datosIntere[i].ESTADO, datosIntere[i].Fecha_Interes);

                    if (datosRetafuentes[i].VALOR != 0)
                        DACuentasPro.InsertLiquidacion(datosRetafuentes[i].COD_OPE, datosRetafuentes[i].NUMERO_PROGRAMADO, datosRetafuentes[i].COD_CLIENTE, datosRetafuentes[i].TIPO_TRAN, datosRetafuentes[i].VALOR, pUsuario, datosRetafuentes[i].ESTADO, datosIntere[i].Fecha_Interes);

                }

                ts.Complete();
            }
        }

        public List<Xpinn.Ahorros.Entities.ReporteMovimiento> ListarDetalleExtracto(String cod_pesona, DateTime pFechaPago, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.ListarDetalleExtracto(cod_pesona, pFechaPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarAhorroExtractos(CuentasProgramado cuentasProgramado, Usuario usuario, String filtro)
        {
            try
            {
                return DACuentasPro.ListarAhorroExtractos(cuentasProgramado, usuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public void cierreHistoricoBusines(Usuario pUsuario, DateTime fechaCierre, String pEstado, ref String sError)
        {
            try
            {
                DACuentasPro.cierreHistorico(pUsuario, fechaCierre, pEstado, ref sError);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "cierreHistoricoBusines", ex);
            }
        }

        public DateTime verificaFechaBusines(Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.verificaFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "verificaFechaBusines", ex);
                return DateTime.MinValue;
            }
        }

        public DateTime getFechaPosCierreCon(Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.getfechaUltimoCierreConta(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "verificaFechaBusines", ex);
                return DateTime.MinValue;
            }
        }

        public DateTime getFechaposProgra(Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.getfechaUltimaCierreAhorroP(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "verificaFechaBusines", ex);
                return DateTime.MinValue;
            }
        }

        public void trasaction(Usuario pUsuario, cierreCuentaDetalle entidacuneta, ref Int64 codi_Opera, Operacion pOperacion, Xpinn.FabricaCreditos.Entities.Giro pGiro, decimal pValor, DateTime fechaoperacion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
                    pOperacion.fecha_oper = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                    pOperacion.fecha_calc = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                    pOperacion.fecha_real = DateTime.Now;

                    entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);
                    codi_Opera = entidadop.cod_ope;
                    pGiro.cod_ope = codi_Opera;

                    Xpinn.FabricaCreditos.Data.AvanceData GiroData = new Xpinn.FabricaCreditos.Data.AvanceData();
                    GiroData.CrearGiro(pGiro, pUsuario, 1);
                    DACuentasPro.UpdateRetiroAhorro(pUsuario, entidacuneta.NumeroProgramado, entidacuneta.Cod_persona, codi_Opera, entidacuneta.Fecha_Proximo_Pago, pValor);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "trasaction", ex);
            }
        }

        public Operacion deposiTrasaccion(ref string pError, Usuario pUsuario, List<CuentasProgramado> list, ref Int64 codigo_op, Operacion pOperacion, String pnuemroP, Int64 pcodUsuario, DateTime pfecha_pago, decimal valorpag, DateTime fechaoperacion)
        {
            Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();

                    pOperacion.fecha_oper = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                    pOperacion.fecha_calc = Convert.ToDateTime(fechaoperacion.ToShortDateString());
                    pOperacion.fecha_real = DateTime.Now;

                    entidadop = operacion.GrabarOperacion(pOperacion, pUsuario);
                    codigo_op = entidadop.cod_ope;

                    DACuentasPro.trasladoCuenta(pnuemroP, pcodUsuario, codigo_op, pfecha_pago, valorpag, 358, pUsuario);
                    foreach (Xpinn.Programado.Entities.CuentasProgramado item in list)
                    {
                        decimal valor = Convert.ToDecimal(item.V_Traslado);
                        DACuentasPro.trasladoCuenta(item.numero_cuenta, pcodUsuario, codigo_op, pfecha_pago, valor, 359, pUsuario);
                    }

                    ts.Complete();
                }
                return pOperacion;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                BOExcepcion.Throw("AhorroVistaBusinnes", "CierreLiquidacionAhorro", ex);
                return null; BOExcepcion.Throw("", "trasaction", ex);
            }
        }

        public string CrearSolicitudAhorroProgramado(CuentasProgramado pAhoProgra, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.CrearSolicitudAhorroProgramado(pAhoProgra, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "CrearSolicitudAhorroProgramado", ex);
                return null;
            }
        }

        public List<CuentasProgramado> ListarParametrizacionCuentas(CuentasProgramado pNumeracionAhorros, Usuario pUsuario)
        {
            try
            {
                return DACuentasPro.ListarParametrizacionCuentas(pNumeracionAhorros, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ListarParametrizacionCuentas", ex);
                return null;
            }
        }
        public CuentasProgramado ConsultarNumeracion(CuentasProgramado pahorro, Usuario vUsuario)
        {
            try
            {
                CuentasProgramado pConsu = new CuentasProgramado();
                pConsu = DACuentasPro.ConsultarNumeracion(pahorro, vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ConsultarNumeracion", ex);
                return null;
            }
        }

        public ELiquidacionInteres CrearLiquidacionProgramado(ELiquidacionInteres pLiqui, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pLiqui.lstLista != null && pLiqui.lstLista.Count > 0)
                    {

                        foreach (ELiquidacionInteres rOpe in pLiqui.lstLista)
                        {
                            ELiquidacionInteres nLiquidacio = new ELiquidacionInteres();
                            nLiquidacio = DACuentasPro.CrearLiquidacionProgramado(rOpe, vUsuario);


                        }


                    }
                    ts.Complete();
                }
                return pLiqui;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusinnes", "CrearLiquidacionProgramado", ex);
                return null;
            }
        }


        public List<Cierea> ListarFechaCierreCausacion(Usuario pUsuario)
        {
            Xpinn.Comun.Business.FechasBusiness BOFechas = new Comun.Business.FechasBusiness();
            List<Cierea> LstCierre = new List<Cierea>();
            // Determinar la periodicidad de cierre
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DACuentasPro.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            // Determinar la fecha del último cierre realizado
            Cierea pCierre = new Cierea();
            pCierre.tipo = "V";
            pCierre.estado = "D";
            pCierre = DACuentasPro.FechaUltimoCierre(pCierre, "", pUsuario);
            if (pCierre == null)
            {
                int año = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                if (mes <= 1)
                {
                    mes = 12;
                    año = año - 1;
                }
                else
                {
                    mes = mes - 1;
                }
                pCierre = new Cierea();
                pCierre.fecha = new DateTime(año, mes, 1, 0, 0, 0).AddDays(-1);
            }
            DateTime FecIni = pCierre.fecha;
            if (FecIni == DateTime.MinValue)
                return null;
            if (FecIni > DateTime.Now.AddDays(15))
                return null;
            // Calcular fechas de cierre inicial
            DateTime FecFin = DateTime.MinValue;
            FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

            if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
            {
                bool control = true;
                do
                {
                    FecFin = FecFin.AddDays(1);
                    if (FecFin.Day == 1)
                    {
                        FecFin = FecFin.AddDays(-1);
                        control = false;
                    }
                } while (control == true);
            }

            // Determinar los periodos pendientes por cerrar
            while (FecFin <= DateTime.Now.AddDays(15))
            {
                Cierea cieRea = new Cierea();
                cieRea.fecha = FecFin;
                LstCierre.Add(cieRea);
                FecIni = FecFin;
                FecFin = BOFechas.FecSumDia(FecIni, dias_cierre, 1);

                if (dias_cierre == 30 || (dias_cierre == 15 && FecFin.Day > 15))
                {
                    bool control = true;
                    do
                    {
                        FecFin = FecFin.AddDays(1);
                        if (FecFin.Day == 1)
                        {
                            FecFin = FecFin.AddDays(-1);
                            control = false;
                        }
                    } while (control == true);
                }
            }
            return LstCierre;
        }

        public List<provision_programado> ListarProvision(DateTime pFechaIni, provision_programado pProvision, Usuario vUsuario)
        {
            try
            {


                provision_programado Traslado = new provision_programado();
                Traslado.codusuario = vUsuario.codusuario;
                Traslado.cod_linea_programado = pProvision.cod_linea_programado;
                Traslado.cod_oficina = pProvision.cod_oficina;


                return DACuentasPro.ListarProvision(pFechaIni, pProvision, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ListarProvision", ex);
                return null;
            }
        }

        public void InsertarDatos(provision_programado Insertar_cuenta, List<provision_programado> lstInsertar, Xpinn.Tesoreria.Entities.Operacion pinsertar, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (Insertar_cuenta.numero_programado != "")
                    {
                        //CREACION DE LA OPERACION
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

                        pinsertar = DAOperacion.GrabarOperacion(pinsertar, vUsuario);


                        //HACER EL INGRESO DE LA CUENTA. 
                        foreach (provision_programado Insertar in lstInsertar)
                        {

                            provision_programado pEntidad = new provision_programado();

                            Insertar.idprovision = 0;
                            Insertar.cod_ope = pinsertar.cod_ope;


                            pEntidad = DACuentasPro.InsertarDatos(Insertar, vUsuario);
                        }
                    }

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "IngresoCuenta", ex);
                return;
            }

        }

        public void Crearcierea(Xpinn.Comun.Entities.Cierea pcierea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DACuentasPro.Crearcierea(pcierea, vUsuario);
                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiereaBusiness", "Crearcierea", ex);
                return;
            }
        }

        public CuentasProgramado ConsultarPeriodicidadProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                CuentasProgramado objCuenta = DACuentasPro.ConsultarPeriodicidadProgramado(pId, vUsuario);

                return objCuenta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ConsultarPeriodicidadProgramado", ex);
                return null;
            }
        }


        public CuentasProgramado ConsultarCierreAhorroProgramado(Usuario vUsuario)
        {
            try
            {
                CuentasProgramado pConsu = new CuentasProgramado();
                pConsu = DACuentasPro.ConsultarCierreAhorroProgramado(vUsuario);

                return pConsu;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "ConsultarCierreAhorroProgramado", ex);
                return null;
            }
        }
        public CuentasProgramado CrearNovedadCambio(CuentasProgramado vAporte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vAporte = DACuentasPro.CrearNovedadCambio(vAporte, pUsuario);

                    ts.Complete();
                }

                return vAporte;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "CrearNovedadCambio", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarPrograClubAhorradores(Int64 pCod_persona, string pFiltro, Boolean pResult, Usuario vUsuario)
        {
            try
            {
                return DACuentasPro.ListarPrograClubAhorradores(pCod_persona, pResult, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteMovimientoBusiness", "ListarAhorroVistaClubAhorrador", ex);
                return null;
            }
        }
        public List<CuentasProgramado> ListarPrograNovedadesCambio(string filtro, Usuario usuario)
        {
            try
            {
                return DACuentasPro.ListarPrograNovedadesCambio(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ListarPrograNovedadesCambio", ex);
                return null;
            }
        }
        public void ModificarNovedadCuotaProgra(CuentasProgramado ahorro, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuentasPro.ModificarNovedadCuotaProgra(ahorro, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AhorroVistaBusiness", "ModificarNovedadCuotaAhorro", ex);
            }
        }
        public DateTime? FechaPrimerPago(CuentasProgramado vProgramado, Usuario vUsuario)
        {
            DateTime? pFecInicio = null;
            try
            {
                pFecInicio = DACuentasPro.FechaPrimerPago(vProgramado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentasProgramadoBusiness", "FechaPrimerPago", ex);
                return null;
            }
            return pFecInicio;

        }
    }
}

