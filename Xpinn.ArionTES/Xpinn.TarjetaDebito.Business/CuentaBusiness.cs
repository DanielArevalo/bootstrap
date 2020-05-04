using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Reflection;
using System.Linq;
using Xpinn.TarjetaDebito.Entities;
using Xpinn.TarjetaDebito.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Data;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Services;
using Xpinn.Util;

namespace Xpinn.TarjetaDebito.Business
{
    public class CuentaBusiness : GlobalBusiness
    {
        private CuentaData DACuenta;
        private AhorroVistaData DAAhorroVista;
        private LineaAhorroData DALineaAhorro;

        public CuentaBusiness()
        {
            DACuenta = new CuentaData();
            DAAhorroVista = new AhorroVistaData();
            DALineaAhorro = new LineaAhorroData();
        }
  

        public List<Cuenta> ListarCuenta(Cuenta pCuenta, string pgeneral, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarCuenta(pCuenta, pgeneral, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarCuenta", ex);
                return null;
            }
        }

        public Boolean AplicarMovimientos(string pConvenio, DateTime fecha, List<Movimiento> lstMovimiento, Usuario pUsuario, ref string Error, ref Int64 pCodOpe, int pTipoAplicacion)
        {
            string cuenta = "", proceso = "", tipotransaccion = "", tipocuenta = "", tarjeta = "";
            int numero_aplicados = 0;
            try
            {
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    // Crear la operación
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 124;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha;
                    pOperacion.fecha_calc = fecha;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() != "" || pOperacion.cod_ope == 0)
                        return false;
                    pCodOpe = pOperacion.cod_ope;

                    // Aplica las transacciones de cada cuenta
                    foreach (Movimiento gMov in lstMovimiento)
                    {
                        // Inicializar variables
                        cuenta = "";
                        tipotransaccion = "";
                        tipocuenta = "";
                        proceso = "";
                        tarjeta = "";
                        // Asignar valores
                        proceso = "sin validar";
                        cuenta = gMov.nrocuenta;
                        tarjeta = gMov.tarjeta;
                        bool bValidado = true;
                        // Determinar el monto a validar
                        decimal valor = 0;
                        // Determinar validación para la cuota de manejo
                        if (gMov.tipotransaccion == "M")
                        {
                            valor = gMov.monto;
                            tipotransaccion = gMov.tipotransaccion;
                        }
                        else
                        {
                            valor = 0;
                            tipotransaccion = "";
                        }
                        // Validar la tarjeta-cuenta. 
                        if (gMov.documento != "no existe" && gMov.descripcion != "TRANSACCION SIN CONCILIAR")
                        {
                            if (!ValidarCuenta(pConvenio, gMov.tarjeta, gMov.nrocuenta, tipotransaccion, valor, gMov.fecha, false, ref Error, pUsuario) && !gMov.esdatafono)
                            {
                                proceso = "No valido";
                                bValidado = false;
                                // Si no valido y es cuota de manejo continuar con el proceso pero sin aplicar.
                                if (gMov.tipotransaccion != "M")
                                {
                                    ts.Dispose();
                                    Error = "No se pudo validar la tarjeta " + gMov.tarjeta + " cuenta " + gMov.nrocuenta;
                                    return false;
                                }
                            }
                            else
                            {
                                proceso = "valido";
                            }
                            // Aplica las transacciones si fueron validadas
                            if (bValidado)
                            {
                                // Determinar el tipo de cuenta y el código del cliente y los datos de la tarjeta
                                #region datos de la tarjeta
                                proceso = "consultar tarjeta";
                                Tarjeta eTarjeta = new Tarjeta();
                                eTarjeta = DACuenta.ConsultarTarjeta(gMov.tarjeta, gMov.nrocuenta, pUsuario);
                                if (eTarjeta == null)
                                {
                                    ts.Dispose();
                                    Error += "NO EXISTE. Tarjeta:" + gMov.tarjeta + " Cuenta:" + gMov.nrocuenta;
                                    return false;
                                }
                                if (eTarjeta.tipo_cuenta == "1" || eTarjeta.tipo_cuenta == "A")        // Cuentas de Ahorro a la Vista
                                {
                                    gMov.tipocuenta = "A";
                                }
                                else if (eTarjeta.tipo_cuenta == "2" || eTarjeta.tipo_cuenta == "C")   // Cuentas de Crédito Rotativo
                                {
                                    gMov.tipocuenta = "C";
                                }
                                else
                                {
                                    gMov.tipocuenta = "A";              // Si no tiene el tipo de cuenta entonces por defecto Ahorros
                                }
                                tipocuenta = gMov.tipocuenta + "-" + eTarjeta.tipo_cuenta;
                                #endregion

                                // Consultar datos de la cuenta
                                #region datos de la cuenta
                                if (eTarjeta == null)
                                {
                                    proceso = "consultar cuenta";
                                    eTarjeta = DACuenta.ConsultarCuenta(gMov.tipocuenta, gMov.nrocuenta, pUsuario);
                                    eTarjeta.numtarjeta = gMov.tarjeta;
                                }
                                gMov.cod_cliente = eTarjeta.cod_persona;
                                gMov.idtarjeta = eTarjeta.idtarjeta;
                                gMov.numero_tarjeta = eTarjeta.numtarjeta;
                                gMov.cod_ope = pCodOpe;
                                gMov.saldo_total = eTarjeta.saldo_total;
                                #endregion

                                // Determinar tipo de transacción
                                gMov.tipo_tran = HomologaTipoTran(gMov.tipocuenta, gMov.tipotransaccion);

                                // Determinar si la transacción de tarjeta ya quedo registrada
                                #region verficar trantarjeta
                                int? num_tran_tarjeta = null;
                                proceso = "consultar transacción tarjeta";
                                Movimiento emov = new Movimiento();
                                emov = DACuenta.ConsultarMovimiento(0, gMov.tarjeta, gMov.operacion, gMov.tipotransaccion, gMov.documento, gMov.fecha, 0, pUsuario);
                                if (emov == null)
                                {
                                    // Registrar la transacción (tabla TRAN_TARJETA)
                                    proceso = "crear movimiento tarjeta";
                                    gMov.num_tran_tarjeta = DACuenta.CrearMovimiento(gMov, pUsuario);
                                    emov = DACuenta.ConsultarMovimiento(0, gMov.tarjeta, gMov.operacion, gMov.tipotransaccion, gMov.documento, gMov.fecha, 0, pUsuario);
                                }
                                if (emov != null)
                                {
                                    num_tran_tarjeta = Convert.ToInt32(emov.num_tran);
                                    gMov.num_tran_tarjeta = emov.num_tran;
                                }
                                #endregion

                                // Aplicar el movimiento. Validar que la transacción no exista o ya fuera aplicada.     
                                #region aplicar monto                           
                                if (gMov.monto != 0 && !gMov.esdatafono)
                                {
                                    // Si la transacción no ha sido aplicada entonces crear la transacción y aplicar el movimiento.
                                    string error = "";
                                    proceso = "consultar movimiento monto";
                                    Movimiento eAplicacion = new Movimiento();
                                    eAplicacion = DACuenta.DatosDeAplicacion(emov.num_tran, gMov.nrocuenta, gMov.cod_ope, gMov.cod_cliente, ConvertirStringToDate(gMov.fecha), gMov.monto, gMov.tipo_tran, gMov.operacion, ref error, pUsuario);
                                    if (eAplicacion == null || eAplicacion.num_tran_apl == null || eAplicacion.num_tran_apl == 0)
                                    {
                                        gMov.monto = Math.Abs(gMov.monto);
                                        if (gMov.tipocuenta == "C" || gMov.tipocuenta == "R")
                                        {
                                            GeneralService generalService = new GeneralService();
                                            General Plazo = generalService.ConsultarGeneral(1400, pUsuario);
                                            if (Plazo.valor == "")
                                                Plazo.valor = "12";
                                            int numerocuotas = ConvertirStringToInt(Plazo.valor);
                                            if (numerocuotas <= 0)
                                                numerocuotas = 12;
                                            proceso = "aplicar monto rotativo.. Cuenta#:" + gMov.nrocuenta + " Cliente:" + gMov.cod_cliente + " Operación:" + pOperacion.cod_ope + " Fecha:" + fecha + " Monto:" + gMov.monto + " TipoTran:" + gMov.tipo_tran + " Usuario:" + pUsuario.codusuario + " IP:" + pUsuario.IP + " Oper.:" + gMov.operacion + " Num.Tran:" + gMov.num_tran_tarjeta + "<<<<";
                                            DACuenta.CrearCreditoAvanceTarjeta(gMov.nrocuenta, gMov.cod_cliente, pOperacion.cod_ope, fecha, gMov.monto, numerocuotas, gMov.tipo_tran, gMov.operacion, gMov.num_tran_tarjeta, pUsuario);
                                        }
                                        else
                                        {
                                            proceso = "aplicar monto ahorros";
                                            DACuenta.AplicarMovimiento(fecha, gMov, pOperacion.cod_ope, pUsuario, ref Error);
                                        }
                                    }
                                }
                                #endregion

                                // Aplicar la comisión. Validar que la transacción no exista o ya fuera aplicada.
                                #region aplicar comision
                                if (gMov.comision != 0 && !gMov.esdatafono)
                                {
                                    // Ajustar tipo de transacción para la comisión                                   
                                    gMov.tipo_tran = TipoTranComision(gMov.tipocuenta, gMov.tipo_tran);
                                    // Si la transacción no ha sido aplicada entonces crear la transacción y aplicar el movimiento.
                                    bool bAplicarComision = false;
                                    string error = "";
                                    proceso = "consultar movimiento comisión";
                                    Movimiento eAplicacionCom = new Movimiento();
                                    eAplicacionCom = DACuenta.DatosDeAplicacion(num_tran_tarjeta, gMov.nrocuenta, gMov.cod_ope, gMov.cod_cliente, ConvertirStringToDate(gMov.fecha), gMov.comision, gMov.tipo_tran, gMov.operacion, ref error, pUsuario);
                                    if (eAplicacionCom == null)
                                    {
                                        bAplicarComision = true;
                                    }
                                    else
                                    {
                                        if (eAplicacionCom.num_tran_apl == null || eAplicacionCom.num_tran_apl == 0)
                                        {
                                            bAplicarComision = true;
                                        }
                                    }
                                    if (bAplicarComision)
                                    {
                                        proceso = "aplicar comision";
                                        gMov.monto = Math.Abs(gMov.comision);
                                        gMov.comision = Math.Abs(gMov.comision);
                                        // Aplicar según el tipo de cuenta
                                        if (gMov.tipocuenta == "C" || gMov.tipocuenta == "R")
                                        {
                                            proceso = "aplicar comision rotativo";
                                            DACuenta.CrearCreditoAvanceTarjeta(gMov.nrocuenta, gMov.cod_cliente, pOperacion.cod_ope, fecha, gMov.comision, 1, gMov.tipo_tran, gMov.operacion, gMov.num_tran_tarjeta, pUsuario);
                                        }
                                        else
                                        {
                                            proceso = "aplicar comision ahorros. Cuenta#:" + gMov.nrocuenta + " Cliente:" + gMov.cod_cliente + " Operación:" + pOperacion.cod_ope + " Fecha:" + fecha + " Monto:" + gMov.monto + " TipoTran:" + gMov.tipo_tran + " Usuario:" + pUsuario.codusuario + " IP:" + pUsuario.IP + " Oper.:" + gMov.operacion + " Num.Tran:" + gMov.num_tran_tarjeta + "<--";
                                            DACuenta.AplicarMovimiento(fecha, gMov, pOperacion.cod_ope, pUsuario, ref Error);
                                        }
                                    }
                                }
                                #endregion

                                // Marca el registro como conciliado o aplicado
                                if (num_tran_tarjeta != null && pTipoAplicacion == 1)
                                {
                                    DACuenta.ActualizarMovimiento(Convert.ToInt64(num_tran_tarjeta), gMov.fecha_corte, pUsuario);
                                }
                            }

                        }
                    }

                    // Verificar que se aplicaron transacciones
                    if (Error.Trim() == "")
                    { 
                        if (DACuenta.VerificarSiGeneroTransacciones(pCodOpe, pUsuario) <= 0)
                        {
                            Error = "No se generaron transacciones";
                            ts.Dispose();
                            return false;
                        }
                    }

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                Error += ex.Message + " Tarjeta:" + tarjeta + " Cuenta:" + cuenta + " Tipo Transacción:" + tipotransaccion + " Tipo Cuenta:" + tipocuenta + " Proceso:" + proceso;
                return false;
            }
        }

        public DateTime ConvertirStringToDate(String pCadena)
        {
            try
            {
                Configuracion conf = new Configuracion();
                return DateTime.ParseExact(pCadena, conf.ObtenerFormatoFecha(), null);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public int ConvertirStringToInt(String pCadena)
        {
            if (pCadena == "")
                return 0;
            try
            {
                Configuracion conf = new Configuracion();
                return Convert.ToInt32(pCadena.Replace("$", "").Replace(conf.ObtenerSeparadorDecimalConfig(), ""));
            }
            catch
            {
                return 0;
            }
        }

        public Tarjeta CrearTarjeta(Tarjeta tarjeta, Usuario usuario)
        {
            try
            {
                return DACuenta.CrearTarjeta(tarjeta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "CrearTarjeta", ex);
                return null;
            }
        }

        public bool VerificarSiTarjetaExiste(Tarjeta tarjeta, Usuario usuario)
        {
            try
            {
                return DACuenta.VerificarSiTarjetaExiste(tarjeta, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "VerificarSiTarjetaExiste", ex);
                return false;
            }
        }

        public Movimiento CrearMovimiento(Movimiento movimiento, long cod_ope, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    movimiento.cod_ope = cod_ope;

                    // Registrar la transacción (tabla TRAN_TARJETA)
                    movimiento.num_tran_tarjeta = DACuenta.CrearMovimiento(movimiento, pUsuario);

                    ts.Complete();

                    return movimiento;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaService", "CrearMovimiento", ex);
                return null;
            }
        }

        public Boolean ValidarCuenta(string pConvenio, string pTarjeta, string pCuenta, string pTipoTran, Decimal pValor, string pFecha, bool bValidarSaldo, ref string Error, Usuario pUsuario)
        {
            try
            {
                ConnectionDataBase dbConnectionFactory = new ConnectionDataBase();
                using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
                {
                    connection.Open();
                    Tarjeta eTarjeta = new Tarjeta();
                    eTarjeta = DACuenta.ConsultarTarjeta(pTarjeta, pCuenta, pUsuario);

                    #region Validar la cuenta de ahorros o crédito
                    if (pTipoTran != "5" && pTipoTran != "8" && pTipoTran != "")
                    {
                        AhorroVista AhorroVista = new AhorroVista();
                        AhorroVista = DAAhorroVista.ConsultaAhorroVista(pCuenta, pUsuario);
                        if (AhorroVista != null)
                        {
                            LineaAhorro LineaAhorro = new LineaAhorro();
                            LineaAhorro = DALineaAhorro.ConsultarLineaAhorro(Convert.ToInt64(AhorroVista.cod_linea_ahorro), pUsuario);
                            if (bValidarSaldo)
                            {
                                if (AhorroVista.saldo_total < pValor)
                                {
                                    Error = "La tarjeta " + pTarjeta + " con la cuenta " + pCuenta + " no tiene fondos suficientes para la transaccion";
                                    if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                    return false;
                                }
                                if (AhorroVista.saldo_total - pValor < LineaAhorro.saldo_minimo)
                                {
                                    Error = "La tarjeta " + pTarjeta + " con la cuenta " + pCuenta + " no cumple el valor del saldo minimo";
                                    if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                    return false;
                                }
                            }
                            // Si no existe la tarjeta
                            if (eTarjeta == null)
                            {
                                eTarjeta.idtarjeta = 0;
                                eTarjeta.numtarjeta = pTarjeta;
                                eTarjeta.tipo_cuenta = "A";
                                eTarjeta.numero_cuenta = pCuenta;
                                eTarjeta.cuenta_homologa = pCuenta;
                                eTarjeta.cod_persona = Convert.ToInt64(AhorroVista.cod_persona);
                                eTarjeta.fecha_asignacion = DateTime.Now;
                                //eTarjeta.cod_convenio = pCod_Convenio;
                                eTarjeta.fecha_proceso = DateTime.Now;
                                eTarjeta.fecha_activacion = DateTime.Now;
                                eTarjeta.cod_oficina = AhorroVista.cod_oficina;
                                eTarjeta.saldo_total = AhorroVista.saldo_total;
                                eTarjeta.saldo_disponible = AhorroVista.saldo_total - AhorroVista.saldo_canje;
                                eTarjeta.saldo_canje = AhorroVista.saldo_canje;
                                eTarjeta.max_tran = 9999;
                                eTarjeta.cobra_cuota_manejo = 1;
                                eTarjeta.estado = "1";
                                eTarjeta.cod_asesor = AhorroVista.cod_asesor;
                                TarjetaData DATarjeta = new TarjetaData();
                                DATarjeta.CrearAsignacionTarjeta(eTarjeta, pUsuario);
                            }
                        }
                        else
                        {
                            // Mirar si corresponde a una cuenta de crédito rotativo
                            TDCreditoSolicitado credRotativo = new TDCreditoSolicitado();
                            credRotativo = DACuenta.ConsultarCreditoRotativo(Convert.ToInt64(pCuenta.Trim()), pUsuario);
                            if (credRotativo != null)
                            {
                                if (bValidarSaldo)
                                {
                                    if (credRotativo.MontoAprobado - credRotativo.saldo_capital >= pValor)
                                    {
                                        Error = "La tarjeta " + pTarjeta + " con el crédito rotativo " + pCuenta + " no tiene fondos suficientes para la transaccion";
                                        if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                        return false;
                                    }
                                    // Si no existe la tarjeta
                                    if (eTarjeta == null)
                                    {
                                        eTarjeta.idtarjeta = 0;
                                        eTarjeta.numtarjeta = pTarjeta;
                                        eTarjeta.tipo_cuenta = "R";
                                        eTarjeta.numero_cuenta = pCuenta;
                                        eTarjeta.cuenta_homologa = pCuenta;
                                        eTarjeta.cod_persona = Convert.ToInt64(credRotativo.Cod_deudor);
                                        eTarjeta.fecha_asignacion = DateTime.Now;
                                        //eTarjeta.cod_convenio = pCod_Convenio;
                                        eTarjeta.fecha_proceso = DateTime.Now;
                                        eTarjeta.fecha_activacion = DateTime.Now;
                                        eTarjeta.cod_oficina = credRotativo.cod_oficina;
                                        eTarjeta.saldo_total = credRotativo.MontoAprobado - credRotativo.saldo_capital;
                                        eTarjeta.saldo_disponible = credRotativo.MontoAprobado - credRotativo.saldo_capital;
                                        eTarjeta.saldo_canje = 0;
                                        eTarjeta.max_tran = 9999;
                                        eTarjeta.cobra_cuota_manejo = 1;
                                        eTarjeta.estado = "1";
                                        eTarjeta.cod_asesor = credRotativo.Cod_asesor;
                                        TarjetaData DATarjeta = new TarjetaData();
                                        DATarjeta.CrearAsignacionTarjeta(eTarjeta, pUsuario);
                                    }
                                }
                            }
                            else
                            {
                                Error = "La cuenta o crédito rotativo " + pCuenta + " no existe";
                                if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                return false;
                            }
                        }
                    }
                    #endregion
                    
                    if (eTarjeta == null)
                    {
                        Error = "La tarjeta " + pTarjeta + " con la cuenta o crédito rotativo " + pCuenta + " no existen";
                        if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    if (eTarjeta.cod_persona == Int64.MinValue)
                    {
                        Error = "La tarjeta " + pTarjeta + " con la cuenta o crédito rotativo " + pCuenta + " no tiene una persona asignada";
                        if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                        return false;
                    }
                    if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                }                

                return true;
            }
            catch (Exception ex)
            {
                Error += "ValidarCuenta:" + ex.Message;
                return false;
            }
        }

        public int? TipoTranComision(string ptipo_cuenta, int? ptipo_tran, int? _tipo_convenio = 0)
        {
            if (_tipo_convenio == 1)
            {
                if (ptipo_tran == 234 || ptipo_tran == 983) // Para reversionesse acredita la comisión
                    return ptipo_cuenta == "C" || ptipo_cuenta == "R" ? 985 : 244;
                else
                    return ptipo_cuenta == "C" || ptipo_cuenta == "R" ? 984 : 243;
            }
            else 
            { 
                if (ptipo_tran == 234 || ptipo_tran == 237 || ptipo_tran == 983) // Para reversiones y consignaciones se acredita la comisión
                    return ptipo_cuenta == "C" || ptipo_cuenta == "R" ? 985 : 244;
                else
                    return ptipo_cuenta == "C" || ptipo_cuenta == "R" ? 984 : 243;
            }
        }

        public int? HomologaTipoTran(string ptipocuenta, string tipotransaccion, int? _tipo_convenio = 0)
        {
            int? tipo_tran = null;
            if (_tipo_convenio == 1)
            {
                // Coopcentral
                string estado = "";
                if (tipotransaccion.StartsWith("E") || tipotransaccion.StartsWith("D") || tipotransaccion.StartsWith("R") || tipotransaccion.StartsWith("S"))
                { 
                    estado = tipotransaccion.Substring(0, 1);
                    tipotransaccion = tipotransaccion.Substring(1, 2);
                }
                else
                {
                    estado = tipotransaccion.Substring(2, 1);
                    tipotransaccion = tipotransaccion.Substring(0, 2);
                }
                if (ptipocuenta == "C" || ptipocuenta == "R" || ptipocuenta == "20" || ptipocuenta == "50")
                {
                    if ((tipotransaccion == "21" || tipotransaccion == "22") && (estado == "E" || estado == "S"))  
                        tipo_tran = 983;
                    else
                        tipo_tran = 982;
                }
                else
                {
                    if (tipotransaccion == "00")  // Compra.                        Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 235 : 234;
                    if (tipotransaccion == "01")  // Retiro.                        Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 231 : 234;
                    if (tipotransaccion == "02")  // Ajuste Débito.                 Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 232 : 234;
                    if (tipotransaccion == "03")  // Retiro.                        Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 231 : 234;
                    if (tipotransaccion == "20")  // Anulaciòn compra.              Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 234 : 247;
                    if (tipotransaccion == "21")  // Consignaciòn.                  Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 237 : 247;
                    if (tipotransaccion == "22")  // Ajuste Crédito.                Crédito
                        tipo_tran = (estado == "E" || estado == "S") ? 246 : 247;
                    if (tipotransaccion == "31")  // Consulta Saldo.                Crédito
                        tipo_tran = (estado == "E" || estado == "S") ? 230 : 234;
                    if (tipotransaccion == "35")  // Consulta Movim.                Crédito
                        tipo_tran = (estado == "E" || estado == "S") ? 240 : 234;
                    if (tipotransaccion == "40")  // transferencia.                 Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 246 : 234;
                    if (tipotransaccion == "41")  // transferencia.                 Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 246 : 234;
                    if (tipotransaccion == "44")  // Transferencia interbancaria.   Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 236 : 234;
                    if (tipotransaccion == "46")  // transferencia In.              Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 236 : 234;
                    if (tipotransaccion == "89")  // Consulta costo transacción.    Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 240 : 234;
                    if (tipotransaccion == "98")  // Cobro Plástico.                Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 242 : 234;
                    if (tipotransaccion == "99")  // Cuota de Manejo.               Débito
                        tipo_tran = (estado == "E" || estado == "S") ? 241 : 234;
                }
            }
            else
            {
                // Banco de Bogotá
                if (ptipocuenta == "C" || ptipocuenta == "R")
                {
                    if (tipotransaccion == "5" || tipotransaccion == "8")  // Ajuste Crédito   Crédito
                        tipo_tran = 983;
                    else
                        tipo_tran = 982;
                }
                else
                {
                    if (tipotransaccion == "1")  // Consulta.        Débito
                        tipo_tran = 230;
                    if (tipotransaccion == "2")  // Retiro.          Débito
                        tipo_tran = 231;
                    if (tipotransaccion == "3")  // Pago o Compra    Débito
                        tipo_tran = 235;
                    if (tipotransaccion == "4")  // Declinada        Débito
                        tipo_tran = 233;
                    if (tipotransaccion == "5")  // Ajuste Crédito   Crédito
                        tipo_tran = 234;
                    if (tipotransaccion == "6")  // Ajuste Débito    Débito
                        tipo_tran = 232;
                    if (tipotransaccion == "7")  // Otras            Débito
                        tipo_tran = 236;
                    if (tipotransaccion == "8")  // Consignación     Crédito
                        tipo_tran = 237;
                    if (tipotransaccion == "9")  // Pago Servicios P.Débito    
                        tipo_tran = 238;
                    if (tipotransaccion == "A")  // Cambio Pin       Débito
                        tipo_tran = 239;
                    if (tipotransaccion == "B")  // Consulta Costo   Débito
                        tipo_tran = 240;
                    if (tipotransaccion == "M")  // Cuota Manejo     Débito
                        tipo_tran = 241;
                    if (tipotransaccion == "P")  // Cobro Plástico   Débito
                        tipo_tran = 242;
                }
            }
            return tipo_tran;
        }

        public Movimiento ConsultarMovimiento(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ConsultarMovimiento(ptipo_convenio, pTarjeta, pOperacion, ptipoTransaccion, pDocumento, pFecha, pValor, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ConsultarMovimiento", ex);
                return null;
            }
        }

        public Movimiento DatosDeAplicacion(Int32? pnum_tran, string pnumero_cuenta, Int64? pcod_ope, Int64? pcod_persona, DateTime pFecha, decimal pValor, int? pTipoTran, string pOperacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                return DACuenta.DatosDeAplicacion(pnum_tran, pnumero_cuenta, pcod_ope, pcod_persona, pFecha, pValor, pTipoTran, pOperacion, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "DatosDeAplicacion", ex);
                return null;
            }
        }

        public List<Tarjeta> ListarTarjetas(Tarjeta pTarjeta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarTarjetas(pTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarTarjetas", ex);
                return null;
            }
        }

        public string ConsultarTipoCuenta(string pConvenio, string pCuenta, ref int? pCodOfi, ref string Error, Usuario pUsuario)
        {
            try
            {
                pCodOfi = null;
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultaAhorroVista(pCuenta, pUsuario);
                if (AhorroVista != null)
                {
                    pCodOfi = AhorroVista.cod_oficina;
                    return "A";
                }
                else
                {
                    // Mirar si corresponde a una cuenta de crédito rotativo
                    TDCreditoSolicitado credRotativo = new TDCreditoSolicitado();
                    credRotativo = DACuenta.ConsultarCreditoRotativo(Convert.ToInt64(pCuenta), pUsuario);
                    if (credRotativo != null)
                    {
                        pCodOfi = credRotativo.cod_oficina;
                        return "R";
                    }
                    else
                    {
                        Error = "La cuenta " + pCuenta + " no existe";
                    }
                }

            }
            catch (Exception ex)
            {
                Error += ex.Message;
            }

            return "";

        }

        public decimal ConsultarSaldoCuenta(string pConvenio, string pCuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                AhorroVista AhorroVista = new AhorroVista();
                AhorroVista = DAAhorroVista.ConsultaAhorroVista(pCuenta, pUsuario);
                if (AhorroVista != null)
                {
                    return AhorroVista.saldo_total;
                }
                else
                {
                    // Mirar si corresponde a una cuenta de crédito rotativo
                    TDCreditoSolicitado credRotativo = new TDCreditoSolicitado();
                    credRotativo = DACuenta.ConsultarCreditoRotativo(Convert.ToInt64(pCuenta.Trim()), pUsuario);
                    if (credRotativo != null)
                    {
                        return credRotativo.MontoAprobado - credRotativo.saldo_capital;
                    }
                    else
                    {
                        Error = "La cuenta o crédito rotativo " + pCuenta + " no existe";
                    }
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }

            return 0;

        }

        public bool ComprobanteValorBanco(Int64 pNumComp, Int64 pTipoComp, ref decimal pValor, ref DateTime? pFecha, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ComprobanteValorBanco(pNumComp, pTipoComp, ref pValor, ref pFecha, pUsuario);
            }
            catch
            {
                return false;
            }
        }
                

        public List<Cuenta> ListarCuentaAsignacion(Cuenta pCuenta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarCuentaAsignacion(pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarCuentaAsignacion", ex);
                return null;
            }
        }

        public Int64 AsignarCuenta(Cuenta pCuenta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.AsignarCuenta(pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "AsignarCuenta", ex);
                return 0;
            }
        }

        public void ActualizarMovimiento(string pConvenio, Movimiento pMovimiento, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuenta.ActualizarMovimiento(pConvenio, pMovimiento, pUsuario, ref Error);

                    ts.Complete();

                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ActualizarMovimiento", ex);
                return;
            }
        }

        public List<TransaccionFinancial> ListarTransaccionesPendientesAplicarEnpacto(int ptipo_convenio, string pConvenio, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarTransaccionesPendientesAplicarEnpacto(ptipo_convenio, pConvenio, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarTransaccionesPendientesAplicarEnpacto", ex);
                return null;
            }
        }

        public bool CrearControlOperacion(string pConvenio, TransaccionFinancial pMovimiento, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuenta.CrearControlOperacion(pConvenio, pMovimiento, ref pError, pUsuario);

                    ts.Complete();

                    return true;
                }
            }
            catch 
            {                
                return false;
            }
        }

        public int TiposAplicacionEnpacto(int ptipo_convenio, string pConvenio, ref string pError, Usuario pUsuario)
        {
            try
            {
                return DACuenta.TiposAplicacionEnpacto(ptipo_convenio, pConvenio, ref pError, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public bool ActualizarMovimientoConciliacion(Int64 pnumTran, string pfechaCorte, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuenta.ActualizarMovimiento(pnumTran, pfechaCorte, pUsuario);
                    ts.Complete();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Movimiento> ListaTransaccionesSinConciliar(string pConvenio, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListaTransaccionesSinConciliar(pConvenio, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public string HomologarCuentas(string pTarjeta, string pCuenta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.HomologarCuentas(pTarjeta, pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "HomologarCuentas", ex);
                return null;
            }
        }

        public List<Movimiento> ListarTipoTran(Int64 pNumTranTarjeta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarTipoTran(pNumTranTarjeta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarTipoTran", ex);
                return null;
            }
        }

        public decimal ConsultarValor(Int64 pNumTranTarjeta, Int32 pTipoTran, Usuario pUsuario)
        {
            return DACuenta.ConsultarValor(pNumTranTarjeta, pTipoTran, pUsuario);
        }

        public void AjustarMovimiento(string pConvenio, string pTipocuenta, Int64 pNumTranTarjeta, Int32 pTipoTran, decimal pValor, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACuenta.AjustarMovimiento(pConvenio, pTipocuenta, pNumTranTarjeta, pTipoTran, pValor, pUsuario, ref Error);
                    ts.Complete();
                    return;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "AjustarMovimiento", ex);
                return;
            }
        }

        public List<TransaccionFinancial> ListarTransaccionesFinancial(string pConvenio, DateTime pFecha, Usuario pUsuario)
        {
            return DACuenta.ListarTransaccionesFinancial(pConvenio, pFecha, pUsuario);
        }

        #region Coopcentral

        /// <summary>
        /// Generar listado de cuentas para reportar a COOPCENTRAL
        /// </summary>
        /// <param name="pCuenta"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<CuentaCoopcentral> ListarCuentaCoopcentral(Cuenta pCuenta, Usuario pUsuario)
        {
            try
            {
                return DACuenta.ListarCuentaCoopcentral(pCuenta, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CuentaBusiness", "ListarCuentaCoopcentral", ex);
                return null;
            }
        }

        public string DeterminarTipoCuenta(string ptipocuenta)
        {
            string gMovtipocuenta = "";
            if (ptipocuenta == "1" || ptipocuenta == "A" || ptipocuenta == "10")                              // Cuentas de Ahorro a la Vista
            {
                gMovtipocuenta = "A";
            }
            else if (ptipocuenta == "2" || ptipocuenta == "C" || ptipocuenta == "20" || ptipocuenta == "50")   // Cuentas de Crédito Rotativo
            {
                gMovtipocuenta = "C";
            }
            else
            {
                gMovtipocuenta = "A";              // Si no tiene el tipo de cuenta entonces por defecto Ahorros
            }
            return gMovtipocuenta;
        }

        public Int64 CrearOperacionCoopcentral(DateTime pfecha, int pTipoOpe, Int64 pCodUsu, Int64 pCodOfi, Int64 pCodCaja, ref string pError, Usuario pUsuario)
        {
            Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
            // Cuando se aplica por opciones diferentes al del SWITCH genera una ùnica operaciòn
            pOperacion.cod_ope = 0;
            pOperacion.tipo_ope = pTipoOpe;
            pOperacion.cod_usu = pCodUsu;
            pOperacion.cod_ofi = pCodOfi;
            pOperacion.cod_caja = pCodCaja;
            pOperacion.fecha_oper = pfecha;
            pOperacion.fecha_calc = pfecha;
            pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref pError, pUsuario);
            if (pError.Trim() != "" || pOperacion.cod_ope == 0)
            {
                pError = "Error al crear operación." + pError + "Usu:" + pOperacion.cod_usu + " Ofi:" + pOperacion.cod_ofi;
                return 0;
            }
            return pOperacion.cod_ope;
        }

        /// <summary>
        /// Permita aplicar un listado de movimientos de tarjeta de COOPCENTRAL
        /// </summary>
        /// <param name="pConvenio"></param>
        /// <param name="fecha"></param>
        /// <param name="lstMovimiento"></param>
        /// <param name="pUsuario"></param>
        /// <param name="Error"></param>
        /// <param name="pCodOpe"></param>
        /// <param name="pTipoAplicacion"></param>
        /// <returns></returns>
        public Boolean AplicarMovimientosCoopcentral(string pConvenio, DateTime fecha, List<MovimientoCoopcentral> lstMovimiento, Usuario pUsuario, ref string pError, ref Int64 pCodOpe, int pTipoAplicacion)
        {
            string cuenta = "", proceso = "", tipotransaccion = "", tipocuenta = "", tarjeta = "";
            decimal valor = 0;
            try
            {
                // Determinar listado de datafonos
                List<Datafono> lstDatafonos = ListarDatafono(pUsuario);
                // Aplicar las transacciones
                TransactionOptions tranopc = new TransactionOptions();
                tranopc.Timeout = TimeSpan.MaxValue;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                {
                    // Crear la operación
                    if (pTipoAplicacion != 0)
                    {
                        pCodOpe = CrearOperacionCoopcentral(fecha, 124, pUsuario.codusuario, pUsuario.cod_oficina, 0, ref pError, pUsuario);
                        if (pError.Trim() != "" || pCodOpe == 0)
                        {
                            pError = "Error al crear operación." + pError + "Usu:" + pUsuario.codusuario + " Ofi:" + pCodOpe;
                            return false;
                        }
                    }
                    // Aplica las transacciones de cada cuenta
                    foreach (MovimientoCoopcentral gMov in lstMovimiento)
                    {
                        pError = "";
                        // Crear la operación cuando se genera desde el Switch
                        if (pTipoAplicacion == 0)
                        {
                            // Determinar si es DATAFONO para saber de que oficina y caja fue realizado.
                            Int64 _cod_ofi = pUsuario.cod_oficina, _cod_caja = 0;
                            int _tipo_ope = 124;
                            if (lstDatafonos != null)
                                if (lstDatafonos.Count > 0)
                                {
                                    if (gMov.tipo_terminal == "10" || gMov.tipo_terminal == "41")
                                    {
                                        var query = (from p in lstDatafonos where gMov.codigo_terminal.Contains(p.cod_datafono) select p).FirstOrDefault();
                                        if (query != null)
                                        {
                                            if ((Datafono)query != null)
                                            {
                                                _tipo_ope = 120;
                                                _cod_caja = ConvertirStringToInt(((Datafono)query).cod_caja);
                                                _cod_ofi  = ((Datafono)query).cod_oficina;
                                            }
                                        }
                                    }
                                }
                            // Crear la operación
                            pCodOpe = CrearOperacionCoopcentral(fecha, _tipo_ope, pUsuario.codusuario, _cod_ofi, _cod_caja, ref pError, pUsuario);
                            if (pError.Trim() != "" || pCodOpe == 0)
                            {
                                pError = "Error al crear operación." + pError + "Usu:" + pUsuario.codusuario + " Ofi:" + pCodOpe;
                                return false;
                            }
                        }
                        // Asignar valores                        
                        proceso = "sin validar";
                        bool bValidado = true;
                        tipocuenta = "";
                        cuenta = gMov.cuenta_origen;
                        tarjeta = gMov.tarjeta;
                        valor = gMov.valor;
                        tipotransaccion = gMov.transaccion;
                        // Validar la tarjeta-cuenta. 
                        if (gMov.secuencia != "no existe" && gMov.descripcion != "TRANSACCION SIN CONCILIAR")
                        {
                            if (!ValidarCuentaCoopcentral(pConvenio, gMov.cuenta_origen, tipotransaccion, valor, gMov.fecha_contable, false, ref tipocuenta, ref pError, pUsuario))
                            {
                                proceso = "No valido";
                                bValidado = false;
                            }
                            else
                            {
                                proceso = "valido";
                            }
                            // Aplica las transacciones si fueron validadas
                            if (bValidado)
                            {
                                gMov.tipocuenta = tipocuenta;
                                // Consultar datos de la cuenta
                                #region datos de la cuenta
                                Tarjeta eTarjeta = new Tarjeta();
                                proceso = "consultar cuenta";
                                eTarjeta = DACuenta.ConsultarCuenta(gMov.tipocuenta, gMov.cuenta_origen, pUsuario);
                                if (eTarjeta != null)
                                { 
                                    gMov.cod_cliente = eTarjeta.cod_persona;
                                    gMov.idtarjeta = eTarjeta.idtarjeta;
                                    gMov.numero_tarjeta = gMov.tarjeta;
                                    gMov.saldo_total = eTarjeta.saldo_total;
                                }
                                gMov.cod_ope = pCodOpe;
                                AjustarFormatoFecha(gMov.fecha_contable);
                                #endregion

                                // Determinar si la transacción de tarjeta ya quedo registrada (tabla TRAN_TARJETA)
                                #region verficar trantarjeta
                                string _errorcrear = "";
                                int? num_tran_tarjeta = null;
                                proceso = "consultar transacción tarjeta";
                                Movimiento emov = new Movimiento();
                                emov = DACuenta.ConsultarMovimientoCoopcentral(1, gMov.tarjeta, gMov.secuencia, gMov.transaccion, gMov.secuencia, gMov.fecha_contable, 0, pUsuario);
                                if (emov == null)
                                {
                                    // Registrar la transacción
                                    proceso = "crear movimiento tarjeta";                                    
                                    gMov.num_tran_tarjeta = DACuenta.CrearMovimientoCoopcentral(gMov, pUsuario);
                                    emov = DACuenta.ConsultarMovimientoCoopcentral(1, gMov.tarjeta, gMov.secuencia, gMov.transaccion, gMov.secuencia, gMov.fecha_contable, 0, pUsuario, ref _errorcrear);
                                }
                                if (emov != null)
                                {
                                    num_tran_tarjeta = Convert.ToInt32(emov.num_tran);
                                    gMov.num_tran_tarjeta = emov.num_tran;
                                }
                                #endregion

                                // Aplicar el movimiento. Validar que la transacción no exista o ya fuera aplicada.     
                                #region aplicar monto                           
                                if (gMov.valor != 0)
                                {
                                    // Determinar tipo de transacción
                                    gMov.tipo_tran = HomologaTipoTran(gMov.tipocuenta, gMov.transaccion, 1);
                                    // Si la transacción no ha sido aplicada entonces crear la transacción y aplicar el movimiento.
                                    string error = "";
                                    proceso = "consultar movimiento monto." + (emov != null ? " Num.Tran:" + emov.num_tran : " num.tran es nulo " + (gMov.cod_ope != null ? " Cod.Ope:" : " ope nulo " + gMov.cod_ope));                                    
                                    Movimiento eAplicacion = new Movimiento();
                                    eAplicacion = DACuenta.DatosDeAplicacion(emov.num_tran, gMov.cuenta_origen, gMov.cod_ope, gMov.cod_cliente, convertirFechaCoopcentral(gMov.fecha_contable), gMov.valor, gMov.tipo_tran, gMov.secuencia, ref error, pUsuario);
                                    proceso = "consultar movimiento monto. Tran:" + num_tran_tarjeta + (eAplicacion == null ? " eAplicacion es nulo" : " eAplicacion no es nulo");
                                    proceso += (eAplicacion != null ? " Num.Tran.Apl:" + eAplicacion.num_tran_apl : "");
                                    if (eAplicacion == null || eAplicacion.num_tran_apl == null || eAplicacion.num_tran_apl == 0)
                                    {
                                        gMov.valor = Math.Abs(gMov.valor);
                                        if (gMov.tipocuenta == "C" || gMov.tipocuenta == "R" || gMov.tipocuenta == "20" || gMov.tipocuenta == "50")
                                        {
                                            GeneralService generalService = new GeneralService();
                                            General Plazo = generalService.ConsultarGeneral(1400, pUsuario);
                                            if (Plazo.valor == "")
                                                Plazo.valor = "12";
                                            int numerocuotas = ConvertirStringToInt(Plazo.valor);
                                            if (numerocuotas <= 0)
                                                numerocuotas = 12;
                                            proceso = "aplicar monto rotativo. Cuenta#:" + gMov.cuenta_origen + " Cliente:" + gMov.cod_cliente + " Operación:" + pCodOpe + " Fecha:" + fecha + " Monto:" + gMov.valor + " TipoTran:" + gMov.tipo_tran + " Usuario:" + pUsuario.codusuario + " IP:" + pUsuario.IP + " Oper.:" + gMov.secuencia + " Num.Tran:" + gMov.num_tran_tarjeta + "<<<<";
                                            DACuenta.CrearCreditoAvanceTarjeta(gMov.cuenta_origen, gMov.cod_cliente, pCodOpe, fecha, gMov.valor, numerocuotas, gMov.tipo_tran, gMov.secuencia, gMov.num_tran_tarjeta, pUsuario);
                                        }
                                        else
                                        {
                                            proceso = "aplicar monto ahorros. Cod.Ope:" + pCodOpe;
                                            DACuenta.AplicarMovimiento(fecha, HomologarMovimiento(gMov), pCodOpe, pUsuario, ref pError);
                                            if (pError.Trim() != "")
                                            {
                                                pError += proceso + ".";
                                                return false;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                // Aplicar la comisión. Validar que la transacción no exista o ya fuera aplicada.
                                #region aplicar comision
                                if (gMov.comision_asociado != 0)
                                {
                                    // Ajustar tipo de transacción para la comisión                                   
                                    gMov.tipo_tran = TipoTranComision(gMov.tipocuenta, gMov.tipo_tran, 1);
                                    // Si la transacción no ha sido aplicada entonces crear la transacción y aplicar el movimiento.
                                    bool bAplicarComision = false;
                                    string error = "";
                                    proceso = "consultar movimiento comisión";
                                    Movimiento eAplicacionCom = new Movimiento();
                                    eAplicacionCom = DACuenta.DatosDeAplicacion(num_tran_tarjeta, gMov.cuenta_origen, gMov.cod_ope, gMov.cod_cliente, convertirFechaCoopcentral(gMov.fecha_contable), gMov.comision_asociado, gMov.tipo_tran, gMov.secuencia, ref error, pUsuario);
                                    if (eAplicacionCom == null)
                                    {
                                        bAplicarComision = true;
                                    }
                                    else
                                    {
                                        if (eAplicacionCom.num_tran_apl == null || eAplicacionCom.num_tran_apl == 0)
                                        {
                                            bAplicarComision = true;
                                        }
                                    }
                                    if (bAplicarComision)
                                    {
                                        proceso = "aplicar comision";
                                        gMov.valor = Math.Abs(gMov.comision_asociado);
                                        gMov.comision_asociado = Math.Abs(gMov.comision_asociado);
                                        // Aplicar según el tipo de cuenta
                                        if (gMov.tipocuenta == "C" || gMov.tipocuenta == "R" || gMov.tipocuenta == "20" || gMov.tipocuenta == "50")
                                        {
                                            proceso = "aplicar comision rotativo";
                                            DACuenta.CrearCreditoAvanceTarjeta(gMov.cuenta_origen, gMov.cod_cliente, pCodOpe, fecha, gMov.comision_asociado, 1, gMov.tipo_tran, gMov.secuencia, gMov.num_tran_tarjeta, pUsuario);
                                        }
                                        else
                                        {
                                            proceso = "aplicar comision ahorros.";
                                            DACuenta.AplicarMovimiento(fecha, HomologarMovimiento(gMov), pCodOpe, pUsuario, ref pError);
                                            if (pError.Trim() != "")
                                                pError += proceso + ".";
                                        }
                                    }
                                }
                                #endregion

                                // Marca el registro como conciliado o aplicado
                                if (num_tran_tarjeta != null && pTipoAplicacion == 1)
                                {
                                    DACuenta.ActualizarMovimiento(Convert.ToInt64(num_tran_tarjeta), gMov.fecha_corte, pUsuario);
                                }
                            }
                        }
                    }

                    // Verificar que se aplicaron transacciones
                    if (pError.Trim() == "")
                    {
                        if (DACuenta.VerificarSiGeneroTransacciones(pCodOpe, pUsuario) <= 0)
                        {
                            pError = "No se generaron transacciones";
                            ts.Dispose();
                            return false;
                        }
                    }

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                pError += ex.Message + " Tarjeta:" + tarjeta + " Cuenta:" + cuenta + " Tipo Transacción:" + tipotransaccion + " Tipo Cuenta:" + tipocuenta + " Proceso:" + proceso;
                return false;
            }
        }

        public Movimiento HomologarMovimiento(MovimientoCoopcentral pMovimiento)
        {
            Movimiento eMov = new Movimiento();
            eMov.nrocuenta = pMovimiento.cuenta_origen;
            eMov.tarjeta = pMovimiento.tarjeta;
            eMov.cod_cliente = pMovimiento.cod_cliente;
            eMov.monto = pMovimiento.valor;
            eMov.tipo_tran = pMovimiento.tipo_tran;
            eMov.documento = pMovimiento.secuencia;
            eMov.operacion = (pMovimiento.ubicacion_terminal != "" ? pMovimiento.ubicacion_terminal : pMovimiento.secuencia);
            eMov.num_tran_tarjeta = pMovimiento.num_tran_tarjeta;
            return eMov;
        }

        public DateTime convertirFechaCoopcentral(string pfecha)
        {
            DateTime _fecha = DateTime.MinValue;
            int año, mes, dia;
            if (pfecha.Contains("-") || pfecha.Contains("/") || pfecha.Length >= 10)
            { 
                año = (pfecha.Length >= 4 ? ConvertirStringToInt(pfecha.Substring(0, 4)) : DateTime.Now.Year);
                mes = (pfecha.Length >= 6 ? ConvertirStringToInt(pfecha.Substring(5, 2)) : DateTime.Now.Month);
                dia = (pfecha.Length >= 8 ? ConvertirStringToInt(pfecha.Substring(8, 2)) : DateTime.Now.Day);
            }
            else
            {
                año = (pfecha.Length >= 4 ? ConvertirStringToInt(pfecha.Substring(0, 4)) : DateTime.Now.Year);
                mes = (pfecha.Length >= 6 ? ConvertirStringToInt(pfecha.Substring(4, 2)) : DateTime.Now.Month);
                dia = (pfecha.Length >= 8 ? ConvertirStringToInt(pfecha.Substring(6, 2)) : DateTime.Now.Day);
            }
            _fecha = new DateTime(año, mes, dia);
            return _fecha;
        }

        public string AjustarFormatoFecha(string pfecha)
        {
            DateTime _fecha = convertirFechaCoopcentral(pfecha);
            return _fecha.Year + "-" + _fecha.Month + "-" +_fecha.Day;
        }

        /// <summary>
        /// Convierte el list con la información de las cuentas a un stream para descargarlo a archivo
        /// </summary>
        /// <param name="lstConsulta"></param>
        /// <param name="separador"></param>
        /// <param name="newfile"></param>
        /// <param name="_usuario"></param>
        /// <returns></returns>
        public StreamWriter GenerarArchivoClientesCoopcentral(List<CuentaCoopcentral> lstConsulta, string separador, StreamWriter newfile, Usuario _usuario)
        {
            if (newfile == null)
                return null;
            if (lstConsulta == null)
                return null;
            foreach (CuentaCoopcentral entidad in lstConsulta)
            {
                // Validar la dirección
                if (entidad.direccion_casa == null || entidad.direccion_casa.Trim() == "" || entidad.direccion_casa.Trim() == "0")
                    entidad.direccion_casa = _usuario.direccion_empresa;
                if (entidad.telefono_casa == null || entidad.telefono_casa.Trim() == "" || entidad.telefono_casa.Trim() == "0")
                    entidad.telefono_casa = _usuario.telefono;
                // Validar valores negativos
                decimal saldoTotal = 0;
                if (entidad.saldototal < 0)
                    saldoTotal = 0;
                else
                    saldoTotal = entidad.saldototal;
                decimal saldoDisponible = 0;
                if (entidad.saldodisponible < 0)
                    saldoDisponible = 0;
                else
                    saldoDisponible = entidad.saldodisponible;
                decimal pagoMinimo = 0;
                if (entidad.pagominimo < 0)
                    pagoMinimo = 0;
                else
                    pagoMinimo = Convert.ToDecimal(entidad.pagominimo);
                decimal pagoTotal = 0;
                if (entidad.pagototal < 0)
                    pagoTotal = 0;
                else
                    pagoTotal = Convert.ToDecimal(entidad.pagototal);
                // Generar la línea
                string linea = "";
                linea = entidad.identificacion + separador + entidad.tipo_identificacion + separador + VerificarCampo(entidad.primer_nombre) + separador + VerificarCampo(entidad.segundo_nombre) + separador + VerificarCampo(entidad.primer_apellido) + separador +
                        VerificarCampo(entidad.segundo_apellido) + separador + VerificarCampo(entidad.direccion_casa) + separador + VerificarCampo(entidad.direccion_trabajo) + separador + VerificarCampo(entidad.telefono_casa) + separador +
                        VerificarCampo(entidad.telefono_trabajo) + separador + VerificarCampo(entidad.telefono_movil) + separador + Convert.ToDateTime(entidad.fecha_nacimiento).ToString("yyyyMMdd") + separador + entidad.sexo + separador +
                        entidad.correo_electronico + separador + entidad.pais_residencia + separador + entidad.dpto_residencia + separador + entidad.ciudad_residencia + separador +
                        entidad.pais_nacimiento + separador + entidad.dpto_nacimiento + separador + entidad.ciudad_nacimiento + separador +
                        entidad.cuenta + separador + entidad.tipo_cuenta + separador + saldoDisponible + separador + saldoTotal + separador +
                        Convert.ToDateTime(entidad.fecha_actualizacion).ToString("yyyyMMdd") + separador + Convert.ToDateTime(entidad.fecha_expedicion).ToString("yyyyMMdd") + separador;
                if (entidad.tipo_cuenta == "20")
                    linea += pagoMinimo + separador + pagoTotal + separador + Convert.ToDateTime(entidad.fecha_vencimiento).ToString("yyyyMMdd");
                else
                    linea += "" + separador + "" + separador + "";
                linea = VerificarTexto(linea, separador);
                newfile.WriteLine(linea);
            }
            return newfile;
        }

        public string VerificarCampo(string pcampo)
        {
            return pcampo == null ? "" : pcampo.Replace(",", " ");
        }

        public string VerificarTexto(string pcampo, string pseparador)
        {
            if (pcampo == null)
                return null;
            pcampo = pcampo.Replace("á", "a");
            pcampo = pcampo.Replace("é", "e");
            pcampo = pcampo.Replace("í", "i");
            pcampo = pcampo.Replace("ó", "o");
            pcampo = pcampo.Replace("ú", "u");
            pcampo = pcampo.Replace("ñ", "n");
            pcampo = pcampo.Replace("Á", "A");
            pcampo = pcampo.Replace("É", "E");
            pcampo = pcampo.Replace("Í", "I");
            pcampo = pcampo.Replace("Ó", "O");
            pcampo = pcampo.Replace("Ú", "U");
            pcampo = pcampo.Replace("Ñ", "N");
            return pcampo;
        }

        /// <summary>
        /// Leer el archivo de movimientos de COOPCENTRAL y pasarlo a un list
        /// </summary>
        /// <param name="pStream"></param>
        /// <param name="pseparador_miles"></param>
        /// <returns></returns>
        public List<MovimientoCoopcentral> CargarArchivoMovCoopcentral(string[] data, string pseparador_miles, string pseparador_decimal = "")
        {
            List<MovimientoCoopcentral> lstConsulta = new List<MovimientoCoopcentral>();
            int linea = 0;
            while (linea < data.Length)
            {
                if (data[linea] != null)
                {
                    if (data[linea].Trim() != "")
                    {
                        string readLine = data[linea];
                        string[] arrayline = readLine.Split(Convert.ToChar(","));
                        MovimientoCoopcentral entidad = new MovimientoCoopcentral();
                        FieldInfo[] propiedades = entidad.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                        int i = 0;
                        foreach (FieldInfo f in propiedades)
                        {
                            String sCampo = f.Name;
                            object valorObject = f.GetValue(entidad);
                            if (i < arrayline.Length)
                            {
                                if (arrayline[i] != null)
                                {
                                    if (f.FieldType.Name == "decimal" || f.FieldType.Name == "Decimal")
                                        f.SetValue(entidad, ConvertirStringToDecimal(arrayline[i].Replace(".", pseparador_decimal), pseparador_miles));
                                    else if (f.FieldType.Name == "Nullable`1")
                                        if (f.FieldType.FullName.Contains("System.Int64"))
                                            f.SetValue(entidad, ConvertirStringToIntN(arrayline[i], pseparador_miles));
                                        else if (f.FieldType.FullName.Contains("System.Decimal") || f.FieldType.FullName.Contains("System.decimal"))
                                            f.SetValue(entidad, ConvertirStringToDecimalN(arrayline[i], pseparador_miles));
                                        else
                                            f.SetValue(entidad, ConvertirStringToInt32N(arrayline[i], pseparador_miles));
                                    else if (f.FieldType.Name == "Boolean")
                                        f.SetValue(entidad, (arrayline[i] == "False" ? false : true));
                                    else
                                        f.SetValue(entidad, arrayline[i]);
                                }
                            }
                            i += 1;
                        }
                        lstConsulta.Add(entidad);
                    }
                }
                linea += 1;
            }
            return lstConsulta;
        }

        public decimal ConvertirStringToDecimal(String pCadena, String pseparador_miles)
        {
            if (pCadena == "")
                return 0;
            try
            {
                return Convert.ToDecimal(pCadena.Replace("$", "").Replace(pseparador_miles, ""));
            }
            catch
            {
                return 0;
            }
        }

        public Int64? ConvertirStringToIntN(String pCadena, String pseparador_miles)
        {
            if (pCadena == "")
                return null;
            try
            {
                return Convert.ToInt64(pCadena.Replace("$", "").Replace(pseparador_miles, ""));
            }
            catch
            {
                return 0;
            }
        }

        public decimal? ConvertirStringToDecimalN(String pCadena, String pseparador_miles)
        {
            if (pCadena == "")
                return null;
            try
            {
                return Convert.ToDecimal(pCadena.Replace("$", "").Replace(pseparador_miles, ""));
            }
            catch
            {
                return 0;
            }
        }

        public Int32? ConvertirStringToInt32N(String pCadena, String pseparador_miles)
        {
            if (pCadena == "")
                return null;
            try
            {
                return Convert.ToInt32(pCadena.Replace("$", "").Replace(pseparador_miles, ""));
            }
            catch
            {
                return 0;
            }
        }

        public string ValidarArchivoCargaCoopcentral(int? _tipo_convenio, string convenio, List<MovimientoCoopcentral> lstConsulta, bool bValidar, Usuario pusuario)
        {
            string _error = "";
            int contar = 0;
            // Homologando tipo de transacción y validando la cuenta              
            foreach (MovimientoCoopcentral entidad in lstConsulta)
            {
                // Si el tipo de cuenta no esta especificado por defecto ahorros
                string errorcon = "";
                int? _cod_ofi = null;
                entidad.tipocuenta = ConsultarTipoCuenta(convenio, entidad.cuenta_origen, ref _cod_ofi, ref errorcon, pusuario);
                if (entidad.tipocuenta == "")
                    entidad.tipocuenta = "A";
                entidad.cod_ofi = _cod_ofi;
                // Determinar el tipo de transacción                        
                entidad.tipo_tran = HomologaTipoTran(entidad.tipocuenta, entidad.transaccion, _tipo_convenio);
                // Las consultas, declinadas, otras, cambio  de pin y consulta de costo no aplicar el valor
                string _tipotrans = entidad.transaccion.Substring(0, 2);
                if (_tipotrans == "30" || _tipotrans == "31" || _tipotrans == "36" ||
                    _tipotrans == "32" || _tipotrans == "89" || _tipotrans == "90")
                    entidad.valor = 0;
                else
                    entidad.valor = Math.Abs(entidad.valor);
                // Verificar si la transacción ya fue aplicada
                if (bValidar)
                {
                    bool bAplicado = true;
                    // Verificar si la transaccion fue registrada
                    Movimiento emov = new Movimiento();
                    emov = DACuenta.ConsultarMovimiento(1, entidad.tarjeta, entidad.secuencia, entidad.transaccion, "", entidad.fecha_contable, 0, pusuario);
                    if (emov == null)
                    {
                        bAplicado = false;
                    }
                    else
                    {
                        // Verificar si el monto fue aplicado
                        string error = "";
                        if (entidad.valor != 0)
                        {
                            Movimiento eAplicacion = new Movimiento();
                            eAplicacion = DACuenta.DatosDeAplicacion(emov.num_tran, entidad.cuenta_origen, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha_contable), entidad.valor, entidad.tipo_tran, entidad.secuencia, ref error, pusuario);

                            if (eAplicacion == null)
                            {
                                bAplicado = false;
                            }
                            else
                            {
                                if (eAplicacion.num_tran_apl == null || eAplicacion.num_tran_apl == 0)
                                    bAplicado = false;
                                else
                                    _error = "eAplicacion.num_tran_apl" + eAplicacion.num_tran_apl;
                            }
                        }
                        // Verificar si la comision fue aplicado
                        if (entidad.comision_asociado != 0)
                        {
                            int? tipo_tran = TipoTranComision(emov.tipocuenta, emov.tipo_tran);
                            Movimiento eAplicacionCom = new Movimiento();
                            eAplicacionCom = DACuenta.DatosDeAplicacion(emov.num_tran, entidad.cuenta_origen, emov.cod_ope, emov.cod_cliente, ConvertirStringToDate(entidad.fecha_contable), entidad.comision_asociado, tipo_tran, entidad.secuencia, ref error, pusuario);                            
                            if (eAplicacionCom == null)
                            {
                                bAplicado = false;
                            }
                            else
                            {
                                if (eAplicacionCom.num_tran_apl == null || eAplicacionCom.num_tran_apl == 0)
                                    bAplicado = false;
                                else
                                    _error = "eAplicacionCom.num_tran_apl" + eAplicacionCom.num_tran_apl;
                            }
                        }
                    }
                    // Si esta pendiente por aplicar entonces validar la cuenta
                    if (!bAplicado)
                    {
                        contar += 1;
                    }
                }
                else
                {
                    contar += 1;
                }
            }
            // Verificar si hay transacciones para aplicar
            if (contar == 0)
            {
                _error = "No hay transacciones pendientes por aplicar";
                return _error;
            }

            return _error;
        }

        public Boolean ValidarCuentaCoopcentral(string pConvenio, string pCuenta, ref string tipoCuenta, ref string Error, Usuario pUsuario)
        {
            return ValidarCuentaCoopcentral(pConvenio, pCuenta, "", 0, "", false, ref tipoCuenta, ref Error, pUsuario);
        }

        public Boolean ValidarCuentaCoopcentral(string pConvenio, string pCuenta, string pTipoTran, Decimal pValor, string pFecha, bool bValidarSaldo, ref string tipoCuenta, ref string Error, Usuario pUsuario)
        {
            tipoCuenta = "";
            try
            {
                ConnectionDataBase dbConnectionFactory = new ConnectionDataBase();
                using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
                {
                    connection.Open();

                    AhorroVista AhorroVista = new AhorroVista();
                    AhorroVista = DAAhorroVista.ConsultaAhorroVista(pCuenta, pUsuario);
                    if (AhorroVista != null)
                    {
                        tipoCuenta = "A";
                        LineaAhorro LineaAhorro = new LineaAhorro();
                        LineaAhorro = DALineaAhorro.ConsultarLineaAhorro(Convert.ToInt64(AhorroVista.cod_linea_ahorro), pUsuario);
                        if (bValidarSaldo)
                        {
                            if (AhorroVista.saldo_total < pValor)
                            {
                                Error = "La cuenta " + pCuenta + " no tiene fondos suficientes para la transaccion";
                                if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                return false;
                            }
                            if (AhorroVista.saldo_total - pValor < LineaAhorro.saldo_minimo)
                            {
                                Error = "La cuenta " + pCuenta + " no cumple el valor del saldo minimo";
                                if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        // Mirar si corresponde a una cuenta de crédito rotativo
                        TDCreditoSolicitado credRotativo = new TDCreditoSolicitado();
                        credRotativo = DACuenta.ConsultarCreditoRotativo(Convert.ToInt64(pCuenta.Trim()), pUsuario);
                        if (credRotativo != null)
                        {
                            tipoCuenta = "C";
                            if (bValidarSaldo)
                            {
                                if (credRotativo.MontoAprobado - credRotativo.saldo_capital >= pValor)
                                {
                                    Error = "El crédito rotativo " + pCuenta + " no tiene fondos suficientes para la transaccion";
                                    if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            Error = "La cuenta o crédito rotativo " + pCuenta + " no existe";
                            if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                            return false;
                        }
                    }
                    if (connection.State == ConnectionState.Closed) dbConnectionFactory.CerrarConexion(connection);
                }

                return true;
            }
            catch (Exception ex)
            {
                Error += "ValidarCuenta:" + ex.Message;
                return false;
            }
        }

        public Movimiento ConsultarMovimientoCoopcentral(int ptipo_convenio, string pTarjeta, string pOperacion, string ptipoTransaccion, string pDocumento, string pFecha, decimal pValor, Usuario vUsuario)
        {
            return DACuenta.ConsultarMovimientoCoopcentral(ptipo_convenio, pTarjeta, pOperacion, ptipoTransaccion, pDocumento, pFecha, pValor, vUsuario);
        }

        public List<Datafono> ListarDatafono(Usuario pUsuario)
        {
            return DACuenta.ListarDatafono(pUsuario);
        }

        #endregion




    }
}
