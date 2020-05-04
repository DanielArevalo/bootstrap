using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Transactions;
using System.Data;
using Xpinn.NIIF.Entities;
using Xpinn.NIIF.Data;
using Xpinn.Comun.Business;
using Xpinn.Util;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.NIIF.Business
{
    public class BalanceNIIFBusiness
    {
        protected ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        /// Objeto de negocio para Credito
        /// 
        private BalanceNIIFData DABalance;
        private FechasBusiness BOFechas;

        public BalanceNIIFBusiness()
        {
            DABalance = new BalanceNIIFData();
            BOFechas = new FechasBusiness();
        }

        public void CrearBalanceNIIF(ref string pError, DateTime pFechaCorte, List<BalanceNIIF> lstBalance, Usuario vUsuario)
        {
            int cont = 0;
            BalanceNIIF asd = new BalanceNIIF();
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    if(lstBalance.Count > 0)
                    {
                        foreach (BalanceNIIF nBalance in lstBalance)
                        {
                        cont++;
                            nBalance.fecha = pFechaCorte;
                            BalanceNIIF pEntidad = new BalanceNIIF();
                        asd = nBalance;
                            pEntidad = DABalance.CrearBalanceNIIF(nBalance, vUsuario);
                        }
                    } 
                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                pError = "Fila Nro :" + cont +"/" + asd.cod_cuenta_niif
                    + "/"+ asd.centro_costo + "/"+ asd.tipo_moneda  + "/" + asd.cod_persona
                    + "/" + asd.saldo + "/" +  ex.Message;
            }
              
        }

        public string VerificarComprobantesYCuentasNIIF(DateTime fechaCorte, Usuario pUsuario)
        {
            try
            {
                return DABalance.VerificarComprobantesYCuentasNIIF(fechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFBusiness", "VerificarComprobantesYCuentasNIIF", ex);
                return null;
            }
        }


        public BalanceNIIF ModificarBalanceNIIF(BalanceNIIF pBalanceNIIF, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pBalanceNIIF = DABalance.ModificarBalanceNIIF(pBalanceNIIF, vUsuario);

                    ts.Complete();
                }

                return pBalanceNIIF;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFNegocio", "ModificarBalanceNIIF", ex);
                return null;
            }
        }

        public void EliminarBalance_NIIF(DateTime pFecha, Usuario vUsuario)
        {

            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABalance.EliminarBalance_NIIF(pFecha, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "EliminarBalance_NIIF", ex);
            }
            
        }


        public BalanceNIIF ConsultarBalanceNIIF(BalanceNIIF pEntidad, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DABalance.ConsultarBalanceNIIF(pEntidad, vUsuario);

                    ts.Complete();
                }

                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFNegocio", "ConsultarBalanceNIIF", ex);
                return null;
            }
        }


        public Boolean GenerarBalance_NIIF(DateTime pFecha, ref int opcion, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    DABalance.GenerarBalance_NIIF(pFecha, ref opcion, ref pError, vUsuario);
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                //return false;  
                //BOExcepcion.Throw("Balance_NIIFNegocio", "GenerarBalance_NIIF", ex);
                return false;
                            
            }
            
        }

        public List<BalanceNIIF> ListarBalance_NIIF( DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DABalance.ListarBalance_NIIF(pFecha,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ListarBalance_NIIF", ex);
                return null;
            }
        }


        public Boolean ReclasificarBalanceNIIF(string pCodOrigen, string pCodDestino, Decimal pValor, int pTipoAjuste, string pObservacion, DateTime pfechafiltro, Int64 pCentroCosto, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABalance.ReclasificarBalanceNIIF(pCodOrigen, pCodDestino, pValor, pTipoAjuste, pObservacion, pfechafiltro, pCentroCosto, ref pError, vUsuario);
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }


        public Boolean ReclasificarBalancesNIIF(List<BalanceNIIF> lstAjustes,ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstAjustes != null)
                    {
                        if (lstAjustes.Count > 0)
                        {
                            foreach (BalanceNIIF nAjuste in lstAjustes)
                            {
                                DABalance.ReclasificarBalanceNIIF(nAjuste.cod_cuentaOrigen_niif, nAjuste.cod_cuenta_niif, nAjuste.saldo,
                                    Convert.ToInt32(nAjuste.ajuste), nAjuste.nombre, nAjuste.fecha, nAjuste.centro_costo, ref pError, vUsuario);
                                if (!string.IsNullOrEmpty(pError))
                                    return false;
                            }
                        }
                    }
                    ts.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }


        public List<PlanCuentasNIIF> ListaPlan_Cuentas(PlanCuentasNIIF pPlanCuenta, Usuario vUsuario)
        {

            try
            {
                return DABalance.ListaPlan_Cuentas(pPlanCuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ListarPlanCuentas_NIIF", ex);
                return null;
            }
        }


        public void ModificarFechaNIIF(DateTime pfechafiltro,int tipo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABalance.ModificarFechaNIIF(pfechafiltro,tipo, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ModificarFechaNIIF", ex);
            }
            
        }

        public void EliminarFechaBalanceGeneradoNIIF(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABalance.EliminarFechaBalanceGeneradoNIIF(pFecha, vUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "EliminarFechaBalanceGeneradoNIIF", ex);
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasLocal(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DABalance.ListarPlanCuentasLocal(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasAuxiliares", ex);
                return null;
            }
        }

        public List<PlanCuentasNIIF> ListarPlanCuentasNIIF(PlanCuentasNIIF pPlanCuentas, Usuario pUsuario, String pfiltro)
        {
            try
            {
                return DABalance.ListarPlanCuentasNIIF(pPlanCuentas, pUsuario, pfiltro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PlanCuentasBusiness", "ListarPlanCuentasAuxiliares", ex);
                return null;
            }
        }

        public void GenerarPlanCuentasNIIF(List<PlanCuentasNIIF> pLstPlan,ref List<PlanCuentasNIIF> lstNoregistrados, Usuario pUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                foreach (PlanCuentasNIIF cuenta in pLstPlan)
                {
                    PlanCuentasNIIF pPlanCta = new PlanCuentasNIIF();
                    if (cuenta.cod_moneda == null)
                        cuenta.cod_moneda = 0;
                    if (cuenta.cod_cuenta_niif == null)
                        cuenta.cod_cuenta_niif = cuenta.cod_cuenta;
                    //CREACION DEL PLAN CUENTAS NIIF
                    try
                    {
                        pPlanCta = DABalance.CrearPlanCuentasNIIF(cuenta, pUsuario);
                    }
                    catch
                    {
                        lstNoregistrados.Add(cuenta);
                    }
                }

                //    ts.Complete();
                //}
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "EliminarFechaBalanceGeneradoNIIF", ex);
            }
        }

        public List<PlanCuentasNIIF> CargarArchivo(DateTime pfecha, Stream pstream, ref string perror, ref List<PlanCuentasNIIF> plstErrores, Usuario pUsuario)
        {
            // Inicializar datos
            Configuracion conf = new Configuracion();
            string sSeparadorDecimal = conf.ObtenerSeparadorDecimalConfig();
            List<PlanCuentasNIIF> lstRecaudos = new List<PlanCuentasNIIF>();
            string readLine;
            const char separador = '|';
            StreamReader strReader;
            plstErrores.Clear();

            try
            {
                using (strReader = new StreamReader(pstream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        readLine = strReader.ReadLine();
                        PlanCuentasNIIF entidad = new PlanCuentasNIIF();
                        string[] arrayline = readLine.Split(Convert.ToChar(separador));
                        bool bError = false;
                        int cantReg = 0;
                        for (int codigo_campo = 0; codigo_campo < arrayline.Length; codigo_campo++)
                        {
                            cantReg += codigo_campo + 1;
                            if (codigo_campo == 0)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() == "")
                                    {
                                        bError = true;
                                        entidad.error += "Columna: " + cantReg.ToString() + "Error: El campo  es obligatorio";
                                    }
                                    else
                                        entidad.cod_cuenta_niif = Convert.ToString(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 1)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() == "")
                                    {
                                        bError = true;
                                        entidad.error += "Columna: " + cantReg.ToString() + "Error: El campo del nombre de la cuenta es obligatorio";
                                    }
                                    else
                                        entidad.nombre = Convert.ToString(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString().Trim() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 2)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() == "")
                                    {
                                        bError = true;
                                        entidad.error += "Columna: " + cantReg.ToString() + "Error: El campo del Tipo es obligatorio";
                                    }
                                    else
                                        entidad.tipo = Convert.ToString(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 3)
                            {
                                try
                                {
                                    if(arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.nivel = Convert.ToInt32(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                    else
                                    {
                                        bError = true;
                                        entidad.error += "Columna: " + cantReg.ToString() + "Error: El campo del Nivel es obligatorio";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 4)
                            {
                                try
                                {
                                    entidad.depende_de = Convert.ToString(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 5)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.cod_moneda = Convert.ToInt32(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 6)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.estado = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                    else
                                        entidad.estado = 1;
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 7)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.maneja_ter = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 8)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.maneja_cc = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 9)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.maneja_sc = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 10)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.impuesto = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 11)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.maneja_gir = Convert.ToInt64(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 12)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.base_minima = Convert.ToDecimal(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 13)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.porcentaje_impuesto = Convert.ToDecimal(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 14)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.cod_cuenta = Convert.ToString(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 15)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.corriente = Convert.ToInt32(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                    else
                                        entidad.corriente = 0;
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (codigo_campo == 16)
                            {
                                try
                                {
                                    if (arrayline[codigo_campo].ToString().Trim() != "")
                                        entidad.nocorriente = Convert.ToInt32(arrayline[codigo_campo].ToString().Replace(separador.ToString(), "").Trim());
                                    else
                                        entidad.nocorriente = 0;
                                }
                                catch (Exception ex)
                                {
                                    bError = true;
                                    entidad.error += "Columna: " + cantReg.ToString() + "Error: " + ex.Message;
                                }
                            }
                            if (bError)
                            {
                                plstErrores.Add(entidad);
                                break;
                            }
                        }
                        if (bError == false)
                            lstRecaudos.Add(entidad);
                    }
                }
            }
            catch (Exception ex)
            {
                perror = ex.Message;
            }
            return lstRecaudos;
        }



        public PlanCuentasNIIF Eliminarniff(Int64 Id, Usuario vUsuario)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    DABalance.Eliminarniff(Id, vUsuario);
                //    ts.Complete();
                //}
                return null;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFNegocio", "CrearBalanceNIIF", ex);
                return null;
            }
              
        }


        public List<BalanceNIIF> listarbalancereporteXBLR(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            try
            {
                return DABalance.listarbalancereporteXBLR(entidad,pBalanceNiif,vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ListarBalance_NIIF", ex);
                return null;
            }
        }

        public List<BalanceNIIF> listarbalancereporteXBLRConceros(BalanceNIIF entidad, string pBalanceNiif, Usuario vUsuario)
        {
            try
            {
                return DABalance.listarbalancereporteXBLRConceros(entidad, pBalanceNiif, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ListarBalance_NIIF", ex);
                return null;
            }
        }
        


        public List<BalanceNIIF> Consultardatosdecierea(Usuario vUsuario)
        {
            try
            {
                return DABalance.Consultardatosdecierea(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Balance_NIIFNegocio", "ListarBalance_NIIF", ex);
                return null;
            }
        }

        public List<BalanceNIIF> ListarBalance(BalanceNIIF pDatos, Usuario pUsuario)
        {
            return DABalance.ListarBalance(pDatos, pUsuario);
        }

        public List<BalanceNIIF> ListarFechaCierre(Usuario vUsuario)
        {
            // Listar períodos contables
            List<BalanceNIIF> lstBalancePrueba = new List<BalanceNIIF>();
            lstBalancePrueba = DABalance.ListarFechaCierre(vUsuario);

            return lstBalancePrueba;
        }

        public List<BalanceNIIF> ListarFechasParaCierre(Usuario pUsuario)
        {
            List<BalanceNIIF> LstCierre = new List<BalanceNIIF>();
            DateTime FecIni = DABalance.FechaUltimoCierre(pUsuario);
            if (FecIni == DateTime.MinValue)
                return null;
            int dias_cierre = 0;
            int tipo_calendario = 0;
            DABalance.PeriodicidadCierre(ref dias_cierre, ref tipo_calendario, pUsuario);
            DateTime FecCieIni = DateTime.MinValue;
            DateTime FecCie = DateTime.MinValue;
            if (dias_cierre == 30)
            {
                FecCieIni = BOFechas.FecUltDia(FecIni).AddDays(1);
                FecIni = FecCieIni;
                FecCieIni = BOFechas.SumarMeses(FecCieIni, 1);
                FecCie = FecCieIni.AddDays(-1);
            }
            else
            {
                FecCieIni = BOFechas.FecSumDia(FecIni, dias_cierre, tipo_calendario);
            }
            while (FecCieIni <= DateTime.Now.AddDays(15))
            {
                if (dias_cierre == 30)
                {
                    BalanceNIIF CieMen = new BalanceNIIF();
                    FecCieIni = FecCieIni.AddDays(-1);
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.SumarMeses(FecCieIni.AddDays(1), 1);
                }
                else
                {
                    BalanceNIIF CieMen = new BalanceNIIF();
                    CieMen.fecha = FecCieIni;
                    LstCierre.Add(CieMen);
                    FecCieIni = BOFechas.FecSumDia(FecCieIni, dias_cierre, tipo_calendario);
                }
            }
            return LstCierre;
        }

        public BalanceNIIF CrearCierreMensual(BalanceNIIF pCierreMensual, Usuario pUsuario)
        {
            try
            {
                return DABalance.CrearCierremensual(pCierreMensual, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFBusiness", "CrearCierreMensual", ex);
                return null;
            }
        }


        #region METODO DE BALANCE COMPROBACION

        public Xpinn.Contabilidad.Entities.BalancePrueba ConsultarBalanceMes13(Xpinn.Contabilidad.Entities.BalancePrueba pDatos, Usuario pUsuario)
        {
            return DABalance.ConsultarBalanceMes13(pDatos, pUsuario);
        }

        public List<Xpinn.Contabilidad.Entities.BalancePrueba> ListarBalanceComprobacionNiif(Xpinn.Contabilidad.Entities.BalancePrueba pDatos, ref Double TotDeb, ref Double TotCre, Usuario pUsuario)
        {
            return DABalance.ListarBalanceComprobacionNiif(pDatos, ref  TotDeb, ref TotCre, pUsuario);
        }


        #endregion


        public BalanceNIIF ConsultarBalanceMes13(BalanceNIIF pDatos, Usuario pUsuario)
        {
            try
            {
                return DABalance.ConsultarBalanceMes13(pDatos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFBusiness", "ConsultarBalanceMes13", ex);
                return null;
            }
        }


        public List<BalancePrueba> ListarBalanceComprobacionTerNiif(BalancePrueba pEntidad, Usuario pUsuario)
        {
            try
            {
                return DABalance.ListarBalanceComprobacionTerNiif(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BalanceNIIFBusiness", "ListarBalanceComprobacionTerNiif", ex);
                return null;
            }
        }


    }
}
