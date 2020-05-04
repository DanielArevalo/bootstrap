using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Business
{
    /// <summary>
    /// Objeto de negocio para Interfaz de Nomina
    /// </summary>
    public class InterfazNominaBusiness : GlobalBusiness
    {
        private InterfazNominaData DAInterfazNomina;

        /// <summary>
        /// Constructor del objeto de negocio para Interfaz de Nomina
        /// </summary>
        public InterfazNominaBusiness()
        {
            DAInterfazNomina = new InterfazNominaData();
        }

        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarNomina(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarNomina", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarNomina(Int64 piden_periodo, String pidentificacion, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarNomina(piden_periodo, pidentificacion, pFiltro, "", pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarNomina", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarNominaConsolidado(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarNominaConsolidado(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarNominaConsolidado", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodos(Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarPeriodos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarPeriodos", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodos(string pTipo, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarPeriodos(pTipo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarPeriodos", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosPrima(Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarPeriodosPrima(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarPeriodosPrima", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCreditos(Int64 piden_periodo, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarCreditos(piden_periodo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarCreditos", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCreditosDelEmpleado(string pIdentificacion, string pConcepto, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarCreditosDelEmpleado(pIdentificacion, pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarCreditosDelEmpleado", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarCuentasConcepto(string pConcepto, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarCuentasConcepto(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarCuentasConcepto", ex);
                return null;
            }
        }

        public Boolean AplicarCreditos(Int64 pPeriodo, DateTime fecha_aplicacion, List<InterfazNomina> lstCreditos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                DateTime? fecha_periodo = null;
                fecha_periodo = DAInterfazNomina.FechaDelPeriodo(pPeriodo, pUsuario);
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 error = 0;
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 119;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_aplicacion;
                    pOperacion.fecha_calc = fecha_aplicacion;
                    pOperacion.num_lista = null;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    pCodOpe = pOperacion.cod_ope;

                    // Aplica los conceptos de nomina
                    foreach (InterfazNomina gCreditos in lstCreditos)
                    {
                        gCreditos.fecha_aplicacion = fecha_aplicacion;
                        DAInterfazNomina.AplicarPago(gCreditos, pOperacion.cod_ope, pUsuario, error, ref Error);
                    }

                    // Insertar registro del cierre                    
                    Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
                    pCierea.tipo = "B";
                    pCierea.estado = "D";
                    pCierea.fecha = (fecha_periodo == null ? fecha_aplicacion : Convert.ToDateTime(fecha_periodo));
                    pCierea.campo1 = Convert.ToString(pPeriodo);
                    pCierea.codusuario = pUsuario.codusuario;
                    DAInterfazNomina.CrearCierea(pCierea, pUsuario);

                    ts.Complete();
                    return true;

                }
            }
            catch (Exception ex)
            {
                // BOExcepcion.Throw("InterfazNominaBusiness", "AplicarCreditos", ex);
                Error += ex.Message;
                return false;
            }
        }

        public Boolean GenerarComprobantePorEmpleado(Int64 pPeriodo, DateTime fecha_aplicacion, List<InterfazNomina> lstCreditos, Int64 pCodProceso, string pFiltro, string pTipo, Usuario pUsuario, ref string Error, ref List<Xpinn.Tesoreria.Entities.Operacion> lstOperacion)
        {
            try
            {
                // Validar el período
                if (DAInterfazNomina.ExistePeriodo(pPeriodo, pTipo, pUsuario) == true)
                {
                    Error = "Ya se hizo la contabilización del período";
                    return false;                    
                }
                // Determinar fecha del período
                DateTime? fecha_periodo = null;
                fecha_periodo = DAInterfazNomina.FechaDelPeriodo(pPeriodo, pUsuario);
                // Grabar los datos
                List<InterfazNomina> lstConceptos;
                if (pTipo == "T")
                    lstConceptos = DAInterfazNomina.ListarNomina(pPeriodo, "", pFiltro, pTipo, pUsuario);
                else
                    lstConceptos = DAInterfazNomina.ListarNomina(pPeriodo, pFiltro, pUsuario);
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    foreach (InterfazNomina gCreditos in lstCreditos)
                    {
                        Int64 error = 0;
                        Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                        // Crear la operación
                        pOperacion.cod_ope = 0;
                        pOperacion.tipo_ope = 109;
                        pOperacion.cod_usu = pUsuario.codusuario;
                        pOperacion.cod_ofi = pUsuario.cod_oficina;
                        pOperacion.fecha_oper = fecha_aplicacion;
                        pOperacion.fecha_calc = fecha_aplicacion;
                        pOperacion.num_lista = null;
                        pOperacion.observacion = Convert.ToString(pPeriodo);
                        pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                        if (Error.Trim() != "" && pOperacion.cod_ope == 0)
                        {
                            Error = Error.Trim() == "" ? "Código de Operación " + pOperacion.cod_ope : Error.Trim();
                            return false;
                        }
                        // Graba los conceptos de nomina             
                        foreach (InterfazNomina gConceptos in lstConceptos)
                        {
                            if (gConceptos.identificacion == gCreditos.identificacion && gConceptos.estado == 0)
                            {
                                gConceptos.fecha_aplicacion = fecha_aplicacion;
                                gConceptos.cod_cliente = gCreditos.cod_cliente;
                                List<Xpinn.Contabilidad.Entities.InterfazNomina> lstCuentas = new List<Xpinn.Contabilidad.Entities.InterfazNomina>();
                                lstCuentas = DAInterfazNomina.ListarCuentasConcepto(gConceptos.iden_concepto, pUsuario);
                                if (lstCuentas.Count >= 1)
                                {
                                    Xpinn.Contabilidad.Entities.InterfazNomina eCuentas = new InterfazNomina();
                                    eCuentas = lstCuentas[0];
                                    gConceptos.cod_cuenta = eCuentas.cod_cuenta;
                                    gConceptos.cod_cuenta_gasto = "";
                                    if (eCuentas.tipo_tercero == "0")
                                        if (eCuentas.iden_tercero != "")
                                            gConceptos.tercero = eCuentas.iden_tercero;
                                    if (eCuentas.tipo_tercero == "1")
                                        gConceptos.tercero = gConceptos.iden_fsalud;
                                    if (eCuentas.tipo_tercero == "2")
                                        gConceptos.tercero = gConceptos.iden_fpension;
                                    if (eCuentas.tipo_tercero == "3")
                                        gConceptos.tercero = gConceptos.iden_fpensionvoluntaria;
                                    if (eCuentas.tipo_tercero == "4")
                                        gConceptos.tercero = gConceptos.iden_fsolidaridad;
                                }
                                DAInterfazNomina.GrabarNomina(gConceptos, pOperacion.cod_ope, pUsuario, error, ref Error);
                                gConceptos.estado = 1;
                            }
                        }

                        Int64 pnum_comp = 0, ptipo_comp = 0;
                        //CREANDO EL COMPROBANTE
                        Xpinn.Contabilidad.Data.ComprobanteData DAComprobante = new Xpinn.Contabilidad.Data.ComprobanteData();
                        if (DAComprobante.GenerarComprobante(pOperacion.cod_ope, pOperacion.tipo_ope, fecha_aplicacion, pUsuario.cod_oficina, Convert.ToInt64(gCreditos.cod_cliente), pCodProceso, ref pnum_comp, ref ptipo_comp, ref Error, pUsuario))
                        {
                            // LLenando listado de comprobantes generados
                            Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                            oper.cod_ope = pOperacion.cod_ope;
                            oper.num_comp = pnum_comp;
                            oper.tipo_comp = ptipo_comp;
                            oper.observacion = gCreditos.identificacion + " " + gCreditos.nombre1 + " " + gCreditos.apellido1;
                            lstOperacion.Add(oper);
                        }
                        else
                        {
                            ts.Dispose();
                            return false;
                        }
                    }

                    // Insertar registro del cierre                    
                    Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
                    pCierea.tipo = pTipo;
                    pCierea.estado = "D";
                    pCierea.fecrea = DateTime.Now;
                    pCierea.fecha = (fecha_periodo == null ? fecha_aplicacion : Convert.ToDateTime(fecha_periodo));
                    pCierea.campo1 = Convert.ToString(pPeriodo);
                    pCierea.codusuario = pUsuario.codusuario;
                    DAInterfazNomina.CrearCierea(pCierea, pUsuario);

                    ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // BOExcepcion.Throw("InterfazNominaBusiness", "AplicarCreditos", ex);
                Error += ex.Message;
                return false;
            }
        }

        public Boolean GenerarComprobante(Int64 pPeriodo, DateTime fecha_aplicacion, Int64 pCodProceso, List<InterfazNomina> lstCreditos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                DateTime? fecha_periodo = null;
                fecha_periodo = DAInterfazNomina.FechaDelPeriodo(pPeriodo, pUsuario);
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    Int64 error = 0;
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 122;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_aplicacion;
                    pOperacion.fecha_calc = fecha_aplicacion;
                    pOperacion.num_lista = null;
                    pOperacion.observacion = "F" + Convert.ToString(pPeriodo);
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() != "" && pOperacion.cod_ope == 0)
                    {
                        Error = Error.Trim() == "" ? "Código de Operación " + pOperacion.cod_ope : Error.Trim();
                        return false;
                    }
                    pCodOpe = pOperacion.cod_ope;

                    // Graba los conceptos de nomina
                    foreach (InterfazNomina gCreditos in lstCreditos)
                    {
                        gCreditos.fecha_aplicacion = fecha_aplicacion;
                        DAInterfazNomina.GrabarNomina(gCreditos, pOperacion.cod_ope, pUsuario, error, ref Error);
                    }

                    Int64 pnum_comp = 0, ptipo_comp = 0;
                    //CREANDO EL COMPROBANTE
                    Xpinn.Contabilidad.Data.ComprobanteData comprobanteBusiness = new Xpinn.Contabilidad.Data.ComprobanteData();
                    if (!comprobanteBusiness.GenerarComprobante(pOperacion.cod_ope, pOperacion.tipo_ope, fecha_aplicacion, pUsuario.cod_oficina, 0, pCodProceso, ref pnum_comp, ref ptipo_comp, ref Error, pUsuario))
                    {
                        return false;
                    }
                    if (Error.Trim() != "")
                    {
                        return false;
                    }

                    // Insertar registro del cierre                    
                    Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
                    pCierea.tipo = "F";
                    pCierea.estado = "D";
                    pCierea.fecha = (fecha_periodo == null ? fecha_aplicacion : Convert.ToDateTime(fecha_periodo));
                    pCierea.fecrea = DateTime.Now;
                    pCierea.campo1 = Convert.ToString(pPeriodo);
                    pCierea.codusuario = pUsuario.codusuario;
                    DAInterfazNomina.CrearCierea(pCierea, pUsuario);

                    ts.Complete();
                    return true;

                }
            }
            catch (Exception ex)
            {
                // BOExcepcion.Throw("InterfazNominaBusiness", "AplicarCreditos", ex);
                Error += ex.Message;
                return false;
            }
        }

        public Int64? CodigoDelEmpleado(string pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.CodigoDelEmpleado(pIdentificacion, pUsuario);
            }
            catch 
            {
                return null;
            }
        }

        public List<InterfazNomina> ListarNominaProvision(Int64 pAño, Int64 pMes, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarNominaProvision(pAño, pMes, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarNominaProvision", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosProvision(Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarPeriodosProvision(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarPeriodosProvision", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarSaludPension(Int64 piden_periodo, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarSaludPension(piden_periodo, pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarSaludPension", ex);
                return null;
            }
        }

        public Boolean GenerarComprobanteSaludPension(Int64 pPeriodo, DateTime fecha_aplicacion, Int64 pCodProceso, List<InterfazNomina> lstConceptos, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                DateTime? fecha_periodo = null;
                fecha_periodo = DAInterfazNomina.FechaDelPeriodo(pPeriodo, pUsuario);
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    Int64 error = 0;
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 123;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_aplicacion;
                    pOperacion.fecha_calc = fecha_aplicacion;
                    pOperacion.num_lista = null;
                    pOperacion.observacion = "D" + Convert.ToString(pPeriodo);
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() != "" && pOperacion.cod_ope == 0)
                    {
                        Error = Error.Trim() == "" ? "Código de Operación " + pOperacion.cod_ope : Error.Trim();
                        return false;
                    }
                    pCodOpe = pOperacion.cod_ope;

                    // Graba los datos en interfaz de nomina
                    foreach (InterfazNomina gConceptos in lstConceptos)
                    {
                        gConceptos.fecha_aplicacion = fecha_aplicacion;
                        DAInterfazNomina.GrabarNomina(gConceptos, pOperacion.cod_ope, pUsuario, error, ref Error);
                    }

                    Int64 pnum_comp = 0, ptipo_comp = 0;
                    //CREANDO EL COMPROBANTE
                    Xpinn.Contabilidad.Data.ComprobanteData comprobanteBusiness = new Xpinn.Contabilidad.Data.ComprobanteData();
                    if (!comprobanteBusiness.GenerarComprobante(pOperacion.cod_ope, pOperacion.tipo_ope, fecha_aplicacion, pUsuario.cod_oficina, 0, pCodProceso, ref pnum_comp, ref ptipo_comp, ref Error, pUsuario))
                    {
                        Error += "..";
                        return false;
                    }
                    if (Error.Trim() != "")
                    {
                        Error += "...";
                        return false;
                    }

                    // Insertar registro del cierre                           
                    Xpinn.Comun.Entities.Cierea pCierea = new Xpinn.Comun.Entities.Cierea();
                    pCierea.tipo = "D";
                    pCierea.estado = "D";
                    pCierea.fecha = (fecha_periodo == null ? fecha_aplicacion : Convert.ToDateTime(fecha_periodo));
                    pCierea.campo1 = Convert.ToString(pPeriodo);
                    pCierea.codusuario = pUsuario.codusuario;
                    if (!DAInterfazNomina.ExistePeriodo(pPeriodo, pCierea.tipo, pUsuario))
                    {
                        DAInterfazNomina.CrearCierea(pCierea, pUsuario);
                    }
                    else
                    {
                        Error = "Ya fue registrado un comprobante para el período " + pPeriodo.ToString() + " fecha " + pCierea.fecha;
                        return false;
                    }

                    ts.Complete();
                    return true;

                }
            }
            catch (Exception ex)
            {
                // BOExcepcion.Throw("InterfazNominaBusiness", "AplicarCreditos", ex);
                Error += ex.Message;
                return false;
            }
        }

        public List<InterfazNomina> ListarCuentasContraConcepto(string pConcepto, Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarCuentasContraConcepto(pConcepto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarCuentasContraConcepto", ex);
                return null;
            }
        }

        public List<InterfazNomina> ListarPeriodosSaludPension(Usuario pUsuario)
        {
            try
            {
                return DAInterfazNomina.ListarPeriodosSaludPension(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InterfazNominaBusiness", "ListarPeriodosSaludPension", ex);
                return null;
            }
        }


    }

}