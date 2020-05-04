using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;
using Xpinn.Tesoreria.Entities;
using Xpinn.NIIF.Entities;
using System.IO;
using Xpinn.Reporteador.Data;
using Xpinn.Reporteador.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para PlanCuentas
    /// </summary>
    public class PlanCuentasBusiness : GlobalData
    {
        private PlanCuentasData DAPlanCuentas;

        /// <summary>
        /// Constructor del objeto de negocio para Usuarios
        /// </summary>
        public PlanCuentasBusiness()
        {
            DAPlanCuentas = new PlanCuentasData();
        }

       
        public PlanCuentas CrearPlanCuentas(PlanCuentas pPlanCuentas, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario,ExogenaReport pExogena)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanCuentas = DAPlanCuentas.CrearPlanCuentas(pPlanCuentas, vUsuario);

                    if (pPlanCuentas.lstImpuestos != null && pPlanCuentas.lstImpuestos.Count > 0)
                    {
                        PlanCuentasImpuestoData DAImpuestos = new PlanCuentasImpuestoData();
                        foreach (PlanCuentasImpuesto rImpu in pPlanCuentas.lstImpuestos)
                        {
                            PlanCuentasImpuesto nImpuesto = new PlanCuentasImpuesto();
                            rImpu.cod_cuenta = pPlanCuentas.cod_cuenta;
                            nImpuesto = DAImpuestos.CrearPlanCuentasImpuesto(rImpu, vUsuario);
                        }
                    }
                    if (lstData.Count > 0)
                    {
                        Xpinn.NIIF.Data.BalanceNIIFData DaNiif = new Xpinn.NIIF.Data.BalanceNIIFData();
                        foreach (PlanCtasHomologacionNIF pCuenta in lstData)
                        {
                            PlanCtasHomologacionNIF pEntidad = new PlanCtasHomologacionNIF();
                            pEntidad = DaNiif.CrearPlanCtasHomologacion(pCuenta, vUsuario);
                        }
                    }
                    if (pExogena.codconcepto!=0)
                    {
                        ExogenaReportData DaExogena = new ExogenaReportData();
                        DaExogena.CrearPlanCtasHomologacionDIAN(pExogena, vUsuario);
                        
                    }
                    if (!string.IsNullOrWhiteSpace(pPlanCuentas.cod_cuenta_centro_costo) && !string.IsNullOrWhiteSpace(pPlanCuentas.cod_cuenta_contrapartida))
                    {
                        pPlanCuentas.idofcuenta = DAPlanCuentas.ConsultarIDCuentaContableOficina(pPlanCuentas.cod_cuenta, vUsuario);

                        if (pPlanCuentas.idofcuenta != 0)
                        {
                            DAPlanCuentas.ModificarCuentaContableOficina(pPlanCuentas, vUsuario);
                        }
                        else
                        {
                            DAPlanCuentas.CrearCuentaContableOficina(pPlanCuentas, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pPlanCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "CrearPlanCuentas", ex);
                return null;
            }
        }


        public PlanCuentas ModificarPlanCuentas(PlanCuentas pPlanCuentas, List<PlanCtasHomologacionNIF> lstData, Usuario vUsuario, ExogenaReport pExogena)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPlanCuentas = DAPlanCuentas.ModificarPlanCuentas(pPlanCuentas, vUsuario);
                    PlanCuentasImpuestoData DAImpuestos = new PlanCuentasImpuestoData();
                    if (pPlanCuentas.lstImpuestos != null && pPlanCuentas.lstImpuestos.Count > 0)
                    {                        
                        foreach (PlanCuentasImpuesto rImpu in pPlanCuentas.lstImpuestos)
                        {
                            PlanCuentasImpuesto nImpuesto = new PlanCuentasImpuesto();
                            rImpu.cod_cuenta = pPlanCuentas.cod_cuenta;
                            if (rImpu.idimpuesto > 0 && rImpu.idimpuesto != null)
                            {
                                nImpuesto = DAImpuestos.ModificarPlanCuentasImpuesto(rImpu, vUsuario);
                            }
                            else
                            {
                                nImpuesto = DAImpuestos.CrearPlanCuentasImpuesto(rImpu, vUsuario);
                            }
                        }
                    }
                    else
                    {
                        DAImpuestos.EliminarPlanCuentaImpuestoXCuenta(pPlanCuentas.cod_cuenta, vUsuario);
                    }
                    if (lstData.Count > 0)
                    {
                        Xpinn.NIIF.Data.BalanceNIIFData DaNiif = new Xpinn.NIIF.Data.BalanceNIIFData();
                        foreach (PlanCtasHomologacionNIF pCuenta in lstData)
                        {
                            PlanCtasHomologacionNIF pEntidad = new PlanCtasHomologacionNIF();
                            if (pCuenta.idhomologa > 0)
                                pEntidad = DaNiif.ModificarPlanCtasHomologacion(pCuenta, vUsuario);
                            else
                                pEntidad = DaNiif.CrearPlanCtasHomologacion(pCuenta, vUsuario);
                        }
                    }
                    if (pExogena.codconcepto!=0 || pExogena.Formato!="0")
                    {

                   
                    if (pExogena.idhomologa > 0)
                    {
                        ExogenaReportData DaExogena = new ExogenaReportData();
                        DaExogena.MODPlanCtasHomologacionDIAN(pExogena, vUsuario);
                    }
                    else
                    {
                        ExogenaReportData DaExogena = new ExogenaReportData();
                        DaExogena.CrearPlanCtasHomologacionDIAN(pExogena, vUsuario);
                    }
                    }
                    if (!string.IsNullOrWhiteSpace(pPlanCuentas.cod_cuenta_centro_costo) && !string.IsNullOrWhiteSpace(pPlanCuentas.cod_cuenta_contrapartida))
                    {
                        pPlanCuentas.idofcuenta = DAPlanCuentas.ConsultarIDCuentaContableOficina(pPlanCuentas.cod_cuenta, vUsuario);

                        if (pPlanCuentas.idofcuenta != 0)
                        {
                            DAPlanCuentas.ModificarCuentaContableOficina(pPlanCuentas, vUsuario);
                        }
                        else
                        {
                            DAPlanCuentas.CrearCuentaContableOficina(pPlanCuentas, vUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pPlanCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ModificarPlanCuentas", ex);
                return null;
            }
        }

        public void EliminarPlanCuentas(string pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAPlanCuentas.EliminarPlanCuentas(pId, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "EliminarPlanCuentas", ex);
                return;
            }
        }
        public bool VerficarAuxiliar(string pId, Usuario vUsuario)
        {
            try
            {
                return DAPlanCuentas.VerficarAuxiliar(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "VerficarAuxiliar", ex);
                return false;
            }
        }

        public PlanCuentas ConsultarPlanCuentas(String pcod_cuenta, Usuario vUsuario)
        {
            try
            {
                PlanCuentas pPlanCuentas = new PlanCuentas();

                pPlanCuentas = DAPlanCuentas.ConsultarPlanCuentas(pcod_cuenta, vUsuario);

                return pPlanCuentas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ConsultarComprobante", ex);
                return null;
            }
        }

        public PlanCuentasNIIF ConsultarPlanCuentasNIIF(String pcod_cuenta, Usuario pUsuario)
        {
            try
            {
                PlanCuentasNIIF pPlanCuentasNif = new PlanCuentasNIIF();

                pPlanCuentasNif = DAPlanCuentas.ConsultarPlanCuentasNIIF(pcod_cuenta, pUsuario);

                return pPlanCuentasNif;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ConsultarComprobante", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<PlanCuentas> ListarPlanCuentasLocal(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasLocal(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasAuxiliares", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentasNif(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasNif(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasAuxiliares", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentasAmbos(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasAmbos(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasTotal", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarPlanCuentas(PlanCuentas pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentas(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentas", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pUsuarios">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Usuarios obtenidos</returns>
        public List<PlanCuentas> ListarPlanCuentasTerceros(Usuario pUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarPlanCuentasxterc(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasTerceros", ex);
                return null;
            }
        }

        public List<PlanCuentas> ListarTipoImpuesto(PlanCuentas pTipo, Usuario pUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarTipoImpuesto(pTipo,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarTipoImpuesto", ex);
                return null;
            }
        }



        private StreamReader strReader;
        public void CargaPlanCuentasYbalance(ref string pError,int pTipoCarga, string sformato_fecha, Stream pstream, ref List<PlanCuentas> lstPlanBalance,ref List<BalanceGeneral> lstBalance, ref List<ErroresCargaContabil> plstErrores, Usuario pUsuario)
        {
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();

            string readLine;

            // Inicializar control de errores
            RegistrarError(-1, "", "", "", ref plstErrores);

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    //recorriendo las filas del archivo
                    while (strReader.Peek() >= 0)
                    {
                        //BAJANDO LA FILA A UNA VARIABLE
                        readLine = strReader.ReadLine();
                        string Separador = "|";

                        //Separando la data a un array
                        string[] arrayline = readLine.Split(Convert.ToChar(Separador));
                        int contadorreg = 0;

                        if (pTipoCarga == 1) // CARGAR PLAN DE CUENTAS
                        {
                            PlanCuentas pEntidad = new PlanCuentas();
                            for (int i = 0; i <= 9; i++)
                            {
                                if (i == 0) { try { pEntidad.cod_cuenta = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 1) { try { pEntidad.nombre = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 2) { try { pEntidad.tipo = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 3) { try { pEntidad.nivel = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 4) { try { pEntidad.depende_de = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 5) { try { pEntidad.cod_moneda = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 6) { try { pEntidad.maneja_cc = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 7) { try { pEntidad.maneja_ter = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 8) { try { pEntidad.maneja_sc = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 9) { try { pEntidad.estado = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 10) { try { pEntidad.impuesto = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 11) { try { pEntidad.maneja_gir = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 12) { try { pEntidad.porcentaje_impuesto = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 13) { try { pEntidad.base_minima = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 14) { try { pEntidad.corriente = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 15) { try { pEntidad.nocorriente = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 16) { try { pEntidad.tipo_distribucion = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 17) { try { pEntidad.porcentaje_distribucion = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 18) { try { pEntidad.valor_distribucion = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                //if (i == 19) { try { pEntidad.cod_tipo_impuesto = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                contadorreg++;
                            }
                            lstPlanBalance.Add(pEntidad);
                        }
                        else // CARGAR BALANCE
                        {
                            BalanceGeneral pEntidad = new BalanceGeneral();
                            for (int i = 0; i <= 3; i++)
                            {
                                if (i == 0) { try { pEntidad.centro_costo = Convert.ToInt64(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 1) { try { pEntidad.cod_cuenta = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 2) { try { pEntidad.fecha = DateTime.ParseExact(arrayline[i].ToString().Trim(), sformato_fecha, null); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                if (i == 3) { try { pEntidad.valor = Convert.ToDouble(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                                contadorreg++;
                            }
                            lstBalance.Add(pEntidad);
                        }
                        
                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }


        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<ErroresCargaContabil> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            ErroresCargaContabil registro = new ErroresCargaContabil();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }

        public void CrearPlanBalanceImportacion(DateTime pFechaCarga, ref string pError, int pTipoCarga, List<PlanCuentas> lstPlanCta, List<BalanceGeneral> lstBalance, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pTipoCarga == 1)
                    {
                        foreach (PlanCuentas nPlanCuenta in lstPlanCta)
                        {
                            nPlanCuenta.impuesto = 0;
                            nPlanCuenta.maneja_gir = 0;
                            nPlanCuenta.porcentaje_impuesto = 0;
                            nPlanCuenta.base_minima = 0;
                            nPlanCuenta.cod_cuenta_niif = null;
                            nPlanCuenta.nombre_niif = null;
                            nPlanCuenta.depende_de_niif = null;
                            nPlanCuenta.corriente = 0;
                            nPlanCuenta.nocorriente = 0;
                            nPlanCuenta.tipo_distribucion = -1;
                            nPlanCuenta.porcentaje_distribucion = -1;
                            nPlanCuenta.valor_distribucion = -1;
                            nPlanCuenta.cod_tipo_impuesto = -1;
                            nPlanCuenta.maneja_traslado = 0;
                            PlanCuentas pEntnidad = new PlanCuentas();
                            pEntnidad = DAPlanCuentas.CrearPlanCuentas(nPlanCuenta, pUsuario);
                        }
                    }
                    else
                    {
                        BalanceGeneralData DABalanceG = new BalanceGeneralData();
                        foreach (BalanceGeneral nPlanCuenta in lstBalance)
                        {
                            nPlanCuenta.estado = "D";
                            nPlanCuenta.moneda = 1;
                            BalanceGeneral pEntnidad = new BalanceGeneral();
                            pEntnidad = DABalanceG.CrearBalance(nPlanCuenta, pUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }
        
        public List<PlanCuentas> ListarCuentasTraslado(string filtro, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DAPlanCuentas.ListarCuentasTraslado(filtro, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarCuentasTraslado", ex);
                return null;
            }
        }
        public bool EsPlanCuentasNIIF(Usuario pUsuario)
        {
            try
            {
                return DAPlanCuentas.EsPlanCuentasNIIF(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "EsPlanCuentasNIIF", ex);
                return false;
            }
        }

    }
}