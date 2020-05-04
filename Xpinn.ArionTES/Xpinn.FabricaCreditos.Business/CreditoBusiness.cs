using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.FabricaCreditos.Data;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Data;
using Xpinn.Auxilios.Data;
using Xpinn.Auxilios.Entities;
using Xpinn.Asesores.Entities;
using Xpinn.Ahorros.Business;
using Xpinn.Ahorros.Entities;
using System.IO;

namespace Xpinn.FabricaCreditos.Business
{
    /// <summary>
    /// Objeto de negocio para Credito
    /// </summary>
    public class CreditoBusiness : GlobalData
    {
        private CreditoData DACredito;

        /// <summary>
        /// Constructor del objeto de negocio para Credito
        /// </summary>
        public CreditoBusiness()
        {
            DACredito = new CreditoData();
        }

        /// <summary>
        /// Crea un Credito
        /// </summary>
        /// <param name="pCredito">Entidad Credito</param>
        /// <returns>Entidad Credito creada</returns>
        public Credito CrearCredito(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.CrearCredito(pCredito, pUsuario);

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearCredito", ex);
                return null;
            }
        }

        public Credito CrearCreditoDesdeFuncionImportacion(Credito credito, Usuario _usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    codeudoresData codeudoresService = new codeudoresData();
                    DocumentoData documentoService = new DocumentoData();

                    DACredito.CrearCreditoDesdeFuncionImportacion(credito, _usuario);

                    documentoService.CrearDocumentoGenerado(credito.Documento_Garantia, credito.numero_radicacion, _usuario);

                    if (credito.Codeudor1.codpersona != 0)
                    {
                        codeudoresService.CrearCodeudoresCredito(credito.Codeudor1, _usuario);
                    }
                    if (credito.Codeudor2.codpersona != 0)
                    {
                        codeudoresService.CrearCodeudoresCredito(credito.Codeudor2, _usuario);
                    }
                    if (credito.Codeudor3.codpersona != 0)
                    {
                        codeudoresService.CrearCodeudoresCredito(credito.Codeudor3, _usuario);
                    }

                    if (credito.AtributosCredito1.tasa != null && credito.AtributosCredito1.tasa != 0)
                    {
                        DACredito.CrearAtributosCredito(credito.AtributosCredito1, _usuario);
                    }
                    if (credito.AtributosCredito2.tasa != null && credito.AtributosCredito2.tasa != 0)
                    {
                        DACredito.CrearAtributosCredito(credito.AtributosCredito2, _usuario);
                    }
                    if (credito.AtributosCredito3.tasa != null && credito.AtributosCredito3.tasa != 0)
                    {
                        DACredito.CrearAtributosCredito(credito.AtributosCredito3, _usuario);
                    }
                    if (credito.AtributosCredito4.tasa != null && credito.AtributosCredito4.tasa != 0)
                    {
                        DACredito.CrearAtributosCredito(credito.AtributosCredito4, _usuario);
                    }

                    ts.Complete();
                }

                return credito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearCreditoDesdeFuncionImportacion", ex);
                return null;
            }
        }

        public Analisis_Credito CrearAnalisisCredito(Analisis_Credito pAnalisis, Usuario pUsuario)
        {
            try
            {
                AnalisisCreditoData analisisData = new AnalisisCreditoData();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAnalisis = analisisData.CrearAnalisis_Credito(pAnalisis, pUsuario);

                    // Actualizo estado del credito
                    DACredito.ActualizarEstadoCredito(pAnalisis.numero_radicacion, pUsuario);

                    ts.Complete();
                }

                return pAnalisis;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearAnalisisCredito", ex);
                return null;
            }
        }

        public AtributosCredito CrearAtributoCredito(AtributosCredito pAtributo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAtributo = DACredito.CrearAtributosCredito(pAtributo, pUsuario);

                    ts.Complete();
                }

                return pAtributo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearAtributoCredito", ex);
                return null;
            }
        }

        public Analisis_Capacidad_Pago CrearAnalisisCapacidadPago(Analisis_Capacidad_Pago pAnalisis, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    AnalisisCapacidadPagoData analisisData = new AnalisisCapacidadPagoData();
                    pAnalisis = analisisData.CrearAnalisis_Capacidad_Pago(pAnalisis, pUsuario);

                    ts.Complete();
                }

                return pAnalisis;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearAnalisisCapacidadPago", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un Credito
        /// </summary>
        /// <param name="pCredito">Entidad Credito</param>
        /// <returns>Entidad Credito modificada</returns>
        /// public void GuardarDatos2 (int codpersona,int numerocuenta,int tipocuenta,int cod_banco)
        public void GuardarGiro(Int64 numero_radicacion, Int64 cod_ope, long formadesembolso, DateTime fecha_desembolso, double monto,
        int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, Int64 codperson, string usuario, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DACredito.GuardarGiro(numero_radicacion, cod_ope, formadesembolso, fecha_desembolso, monto, idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, codperson, usuario, pUsuario);
                ts.Complete();
            }
        }

        public void GuardarCuentaBancariaCliente(Int64 codpersona, string numerocuenta, Int64 tipocuenta, Int64 cod_banco, Usuario pUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                DACredito.GuardarCuentaBancariaCliente(codpersona, numerocuenta, tipocuenta, cod_banco, pUsuario);
                ts.Complete();
            }
        }

        public Credito ModificarCredito(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.ModificarCredito(pCredito, pUsuario);

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarCredito", ex);
                return null;
            }
        }

        public AtributosCredito ModificarAtributosFinanciados(AtributosCredito pCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.ModificarAtributosFinanciados(pCredito, pUsuario);

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarAtributosFinanciados", ex);
                return null;
            }
        }

        public void ModificarFechaDesembolsoCredito(DateTime fechadesembolso, Credito pCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACredito.ModificarFechaDesembolsoCredito(fechadesembolso, pCredito, pUsuario);

                    ts.Complete();
                }


            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarFechaDesembolsoCredito", ex);

            }
        }


        public Credito ModificarCreditoUlt(Credito pCredito, Usuario pUsuario)
        {
            try
            {
                // Tuvo que quitarse el transaction porque en COOACEDED con Oracle 10G genera un error.
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                pCredito = DACredito.ModificarCreditoUlt(pCredito, pUsuario);
                if (pCredito.lstDescuentosCredito != null)
                {
                    foreach (DescuentosCredito edesc in pCredito.lstDescuentosCredito)
                    {
                        DACredito.ModificarDescuentosCredito(edesc, pUsuario);
                    }
                }
                // Guarda datos de la lista de Pagos Pendientes FabricaCredito/ModificacionCredito/Nuevo.aspx
                if (pCredito.lstAmortizaCre != null && pCredito.lstAmortizaCre.Count() > 0)
                {
                    AmortizaCreData DAAmortiza = new AmortizaCreData();
                    foreach (AmortizaCre eAmor in pCredito.lstAmortizaCre)
                    {
                        if ((OperacionCRUD)eAmor.idamortiza == OperacionCRUD.Crear)
                        {
                            DAAmortiza.CrearAmortizaCre(eAmor, pUsuario);
                            continue;
                        }

                        DAAmortiza.ModificarAmortizaCre(eAmor, pUsuario);
                    }
                }
                // ADICION DE REGISTRO DE LOS CODEUDORES
                if (pCredito.lstCodeudores != null)
                {
                    if (pCredito.lstCodeudores.Count > 0)
                    {
                        codeudoresData DACodeudores = new codeudoresData();
                        if (DACodeudores.BorrarCodeudoresCredito(pCredito.numero_radicacion, pUsuario) == true)
                        {
                            codeudores entidad;
                            foreach (codeudores eCodeudores in pCredito.lstCodeudores)
                            {
                                if (eCodeudores.codpersona != 0)
                                {
                                    entidad = DACodeudores.CrearCodeudoresCredito(eCodeudores, pUsuario);
                                }
                            }
                        }
                    }
                }

                //    ts.Complete();
                //}

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarCredito", ex);
                return null;
            }
        }


        public Credito ModificarCupoRotativo(Credito pCredito, Usuario pUsuario)
        {
            try
            {

                pCredito = DACredito.ModificarCupoRotativo(pCredito, pUsuario);

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarCupoRotativo", ex);
                return null;
            }
        }


        public Avance ModificarDescuentos(Avance pCredito, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // pCredito = DACredito.ModificarCreditoUlt(pCredito, pUsuario);
                    if (pCredito.lstDescuentosCredito != null)
                    {
                        foreach (DescuentosCredito edesc in pCredito.lstDescuentosCredito)
                        {
                            DACredito.ModificarDescuentosCredito(edesc, pUsuario);
                        }
                    }
                    ts.Complete();
                }
                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ModificarDescuentos", ex);
                return null;
            }
        }

        public void cambiotasa(string radicacion, string calculo_atr, string tasa, string tipotasa, string desviacion, string tipoHisto, Usuario pUsuario, string codart, string op)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACredito.cambiotasa(radicacion, calculo_atr, tasa, tipotasa, desviacion, tipoHisto, codart, pUsuario, op);
                    ts.Complete();
                }
            }
            catch
            { }
        }

        public void cambiolinea(Int64 radicacion, string cod_linea, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACredito.cambiolinea(radicacion, cod_linea, pUsuario);
                    ts.Complete();
                }
            }
            catch
            { }
        }


        public void cambiotasa_fecha(string tasa, string tipotasa, string radicacion, DateTime fechaIni, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACredito.cambiotasa_fecha(tasa, tipotasa, radicacion, fechaIni, pUsuario);
                    ts.Complete();
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarCredito(Credito pCredito, bool opcion, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                {
                    if (pCredito.num_preimpreso != null)
                    {
                        CreditoOrdenServicio pEntidad = new CreditoOrdenServicio();
                        String pFiltro = " WHERE NUMERO_PREIMPRESO = " + pCredito.num_preimpreso;
                        pEntidad = DACredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);
                        if (pEntidad.idordenservicio > 0) //Si ya existe el Num_PreImpreso
                            return null;
                    }
                    decimal pMontoDesembolso = 0;
                    // Guardar los valores a deducir del crédito 
                    if (pCredito.lstDescuentos != null)
                    {
                        foreach (DescuentosDesembolso variable in pCredito.lstDescuentos)
                        {
                            variable.numero_obligacion = Convert.ToString(pCredito.numero_radicacion.ToString().Trim());
                            guardardescuento(variable, pUsuario);
                        }
                    }
                    // Desembolsar el crédito
                    pCredito.cod_ope = DACredito.DesembolsarCredito(pCredito, ref pMontoDesembolso, ref Error, pUsuario);
                    if (Error.Trim() != "")
                        return pCredito;
                    if (pCredito.num_preimpreso != null)
                    {
                        DACredito.ModificarCreditoOrdenServicio(pCredito.numero_radicacion, Convert.ToInt64(pCredito.num_preimpreso), pUsuario);
                    }

                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();
                    if (opcion == true)
                    {
                        //consultar si la Linea del credito es Educativo
                        LineasCreditoData DALinea = new LineasCreditoData();
                        LineasCredito pLinea = new LineasCredito();
                        pLinea = DALinea.ConsultaLineaCredito(pCredito.cod_linea_credito, pUsuario);
                        if (pLinea != null && pLinea.credito_educativo == 1 && pLinea.maneja_auxilio == 1)
                            pMontoDesembolso = pMontoDesembolso + pCredito.monto;
                        DACredito.GuardarGiro(pCredito.numero_radicacion, pCredito.cod_ope, formadesembolso, pCredito.fecha_desembolso, Convert.ToDouble(pMontoDesembolso), idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, pCredito.cod_deudor, pUsuario.nombre, pUsuario);
                    }
                    else if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {
                            numero_cuenta = pCredito.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pCredito.cod_deudor,
                            cod_ope = pCredito.cod_ope,
                            fecha_cierre = pCredito.fecha_desembolso,
                            V_Traslado = pMontoDesembolso,
                            codusuario = pUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, pUsuario);

                        if (ahorro.numero_cuenta != "" || ahorro.numero_cuenta != null)
                        {
                            Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                            List<Xpinn.Ahorros.Entities.CuentasCobrar> lstCuentasCobrar = new List<Ahorros.Entities.CuentasCobrar>();
                            lstCuentasCobrar = DAAhorroVistaData.ListarCuentasCobrar(ahorro.numero_cuenta, pUsuario);

                            foreach (Xpinn.Ahorros.Entities.CuentasCobrar CuetaxCobrar in lstCuentasCobrar)
                            {
                                Xpinn.Caja.Entities.TransaccionCaja pTransaccionCaja = new Xpinn.Caja.Entities.TransaccionCaja();
                                pTransaccionCaja.cod_ope = ahorro.cod_ope;
                                pTransaccionCaja.fecha_movimiento = Convert.ToDateTime(ahorro.fecha_cierre);
                                pTransaccionCaja.cod_persona = Convert.ToInt64(ahorro.cod_persona);
                                CuetaxCobrar.tipo_tran = 257;
                                DAAhorroVistaData.ProcesoCuentasCobrar(pTransaccionCaja, CuetaxCobrar, pUsuario);
                            }

                        }
                    }

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "DesembolsarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Realizar el desembolso del crèdito
        /// </summary>
        /// <param name="pCredito"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public Credito DesembolsarCreditoMasivo(Credito pCredito, bool opcion, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pCredito.num_preimpreso != null)
                    {
                        CreditoOrdenServicio pEntidad = new CreditoOrdenServicio();
                        String pFiltro = " WHERE NUMERO_PREIMPRESO = " + pCredito.num_preimpreso;
                        pEntidad = DACredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);
                        if (pEntidad.idordenservicio > 0) //Si ya existe el Num_PreImpreso
                            return null;
                    }
                    decimal pMontoDesembolso = 0;
                    // Guardar los valores a deducir del crédito 
                    if (pCredito.lstDescuentos != null)
                    {
                        foreach (DescuentosDesembolso variable in pCredito.lstDescuentos)
                        {
                            variable.numero_obligacion = Convert.ToString(pCredito.numero_radicacion.ToString().Trim());
                            guardardescuento(variable, pUsuario);
                        }
                    }
                    // Desembolsar el crédito
                    pCredito.cod_ope = DACredito.DesembolsarCreditoMasivo(pCredito, ref pMontoDesembolso, pUsuario);
                    if (pCredito.num_preimpreso != null)
                        DACredito.ModificarCreditoOrdenServicio(pCredito.numero_radicacion, Convert.ToInt64(pCredito.num_preimpreso), pUsuario);

                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();
                    if (opcion == true)
                    {
                        //consultar si la Linea del credito es Educativo
                        LineasCreditoData DALinea = new LineasCreditoData();
                        LineasCredito pLinea = new LineasCredito();
                        pLinea = DALinea.ConsultaLineaCredito(pCredito.cod_linea_credito, pUsuario);
                        if (pLinea != null && pLinea.credito_educativo == 1 && pLinea.maneja_auxilio == 1)
                            pMontoDesembolso = pMontoDesembolso + pCredito.monto;
                        DACredito.GuardarGiro(pCredito.numero_radicacion, pCredito.cod_ope, formadesembolso, pCredito.fecha_desembolso, Convert.ToDouble(pMontoDesembolso), idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, pCredito.cod_deudor, pUsuario.nombre, pUsuario);
                    }
                    else if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {
                            numero_cuenta = pCredito.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pCredito.cod_deudor,
                            cod_ope = pCredito.cod_ope,
                            fecha_cierre = pCredito.fecha_desembolso,
                            V_Traslado = pMontoDesembolso,
                            codusuario = pUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, pUsuario);
                    }

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "DesembolsarCredito", ex);
                return null;
            }
        }

        public Analisis_Credito ListarAnalisisCredito(Analisis_Credito analisisCredito, Usuario _usuario)
        {
            try
            {
                AnalisisCreditoData analisisData = new AnalisisCreditoData();

                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    analisisCredito = analisisData.ListarAnalisis_Credito(analisisCredito, _usuario);

                    // Actualizo estado del credito

                    ts.Complete();
                }

                return analisisCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearAnalisisCredito", ex);
                return null;
            }


        }

        /// <summary>
        /// Elimina un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <param name="idDeudor">identificador del deudor</param>
        public void EliminarCredito(Int64 pId, long idDeudor, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DACredito.EliminarCredito(pId, idDeudor, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "EliminarCredito", ex);
            }
        }

        /// <summary>
        /// Obtiene un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCredito(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Credito eCredito = new Credito();
                eCredito = DACredito.ConsultarCredito(pId, pUsuario);
                if (eCredito != null)
                {
                    if (eCredito.numero_radicacion > 0)
                    {
                        CreditoOrdenServicio eOrden = new CreditoOrdenServicio();
                        eOrden = DACredito.ConsultarCreditoOrdenServicio(eCredito.numero_radicacion, pUsuario);
                        if (eOrden.cod_persona != null)
                        {
                            eCredito.cod_proveedor = eOrden.cod_persona;
                            eCredito.idenprov = eOrden.idproveedor;
                            eCredito.nomprov = eOrden.nomproveedor;
                        }
                    }
                }
                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCredito", ex);
                return null;
            }
        }

        //  Business para consultar el analisis promedio (Modulo Analisis Credito)
        public List<AnalisisPromedio> ConsultarAnalisisPromedio(string cod_linea, Int64 pId, Usuario pUsuario)
        {
            try
            {
                LineasCreditoData lineasData = new LineasCreditoData();

                List<AnalisisPromedio> lstAnalisis = DACredito.ConsultarAnalisisPromedio(pId, pUsuario);
                LineasCredito entidad = lineasData.Calcular_Cupo(cod_linea, pId, DateTime.Today.ToShortDateString(), pUsuario);

                foreach (var analisis in lstAnalisis)
                {
                    analisis.cupo_disponible = Convert.ToInt64(entidad.Monto_Maximo ?? 0);
                }

                return lstAnalisis;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarAnalisisPromedio", ex);
                return null;
            }
        }



        public List<AnalisisPromedio> ConsultarCalificacionHistorial(Int64 pId, Usuario pUsuario)
        {
            try
            {

                List<AnalisisPromedio> lstAnalisis = DACredito.ConsultarCalificacionHistorial(pId, pUsuario);


                return lstAnalisis;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarAnalisisPromedio", ex);
                return null;
            }
        }


        public List<Credito> ListarCreditosPorFiltro(string filtroDefinido, string filtroGrilla, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCreditosPorFiltro(filtroDefinido, filtroGrilla, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditosPorFiltro", ex);
                return null;
            }
        }

        public Credito ConsultarCreditoModSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Credito eCredito = new Credito();
                eCredito = DACredito.ConsultarCreditoModSolicitud(pId, pUsuario);
                if (eCredito != null)
                {
                    if (eCredito.numero_radicacion != null)
                    {
                        CreditoOrdenServicio eOrden = new CreditoOrdenServicio();
                        eOrden = DACredito.ConsultarCreditoOrdenServicio(eCredito.numero_radicacion, pUsuario);
                        eCredito.cod_proveedor = eOrden.cod_persona;
                        eCredito.idenprov = eOrden.idproveedor;
                        eCredito.nomprov = eOrden.nomproveedor;
                    }
                    if (eCredito.numero_radicacion != null)
                    {
                        eCredito.lstDescuentosCredito = new List<DescuentosCredito>();
                        DescuentosCredito eDescCred = new DescuentosCredito();
                        eDescCred.numero_radicacion = eCredito.numero_radicacion;
                        eCredito.lstDescuentosCredito = DACredito.ListarDescuentosCredito(eDescCred, pUsuario);
                    }
                }
                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoModSolicitud", ex);
                return null;
            }
        }


        public Credito ConsultarCreditoModCupoRotativo(Int64 pId, Usuario pUsuario)
        {
            try
            {
                Credito eCredito = new Credito();
                eCredito = DACredito.ConsultarCreditoModCupoRotativo(pId, pUsuario);

                return eCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoModCupoRotativo", ex);
                return null;
            }
        }


        public CreditoOrdenServicio ConsultarCREDITO_OrdenServ(String pFiltro, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCREDITO_OrdenServ", ex);
                return null;
            }
        }

        public List<Credito> ConsultarCuotas(long radicacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCuotas(radicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoService", "ConsultarCredito", ex);
                return null;
            }
        }

        public Credito MODIFICARcredito(Credito pCREDITO, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCREDITO = DACredito.MODIFICARcredito(pCREDITO, pusuario);

                    ts.Complete();

                }

                return pCREDITO;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CREDITOBusiness", "ModificarCREDITO", ex);
                return null;
            }
        }

        public List<Cliente> ListarCodeudores(Int64 pnumeroradicacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCodeudores(pnumeroradicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCodeudores", ex);
                return null;
            }

        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCredito(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCredito(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoActivos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCreditosActivos(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCarteraActiva(long codPersona, Usuario usuario)
        {
            try
            {
                return DACredito.ListarCarteraActiva(codPersona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCarteraActiva", ex);
                return null;
            }
        }

        public List<Credito> ConsultarCreditoTerminado(Int64 pCredito, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditoTerminado(pCredito, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoAsociados(Int64 pCodPersona, DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCreditoAsociados(pCodPersona, pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoAsociados", ex);
                return null;
            }
        }

        //Anderson Acuña-- Reporte Creditos Desembolsados
        public List<Credito> ListarCreditosDesembolsados(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCreditosDesembolsados(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditos", ex);
                return null;
            }
        }


        //Anderson Acuña-- Reporte Cartera
        public List<Credito> ListarCartera(Credito pCredito, Usuario pUsuario, String filtro, String fechaAct)
        {
            try
            {
                return DACredito.ListarCartera(pCredito, pUsuario, filtro, fechaAct);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditos", ex);
                return null;
            }
        }

        //Guarda información Analisis de credito
        public bool CrearAnalisis_Info(AnalisisInfo analisisInfo, Usuario pUsuario)
        {
            try
            {
                AnalisisCreditoData analisisData = new AnalisisCreditoData();
                return analisisData.CrearAnalisis_Info(analisisInfo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearAnalisis_Info", ex);
                return false;
            }
        }
        //Consultar Informacion Analisis de credito
        public AnalisisInfo ConsultarAnalisis_Info(long numeroRadicacion, int tipoPersona, Usuario pUsuario)
        {
            try
            {
                AnalisisCreditoData analisisData = new AnalisisCreditoData();
                return analisisData.ConsultarAnalisis_Info(numeroRadicacion, tipoPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarAnalisis_Info", ex);
                return null;
            }
        }

        // </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoDocumRequeridos(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCreditoDocumRequeridos(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoDocumRequeridos", ex);
                return null;
            }
        }


        public List<LineasCredito> ListarAtributosFinanciados(long pNumRadicacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarAtributosFinanciados(pNumRadicacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarAtributosFinanciados", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditoMasivo(Credito pCredito, DateTime pFechaIni, DateTime pFechaFin, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.ListarCreditoMasivo(pCredito, pFechaIni, pFechaFin, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCredito", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCreditoAsesor(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditoAsesor(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoAsesor", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener el parametro para Habeas Data
        /// </summary>
        /// <param name="pId">identificador de Habeas Data
        /// <returns>Entidad ParametroHabeas Data</returns>
        public Credito ConsultarParametroHabeas(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroHabeas(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroHabeas", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para CobroJuridico
        /// </summary>
        /// <param name="pId">identificador de CobroJuridico
        /// <returns>Entidad ParametroCobroJuridico</returns>
        public Credito ConsultarParametroCobroJuridico(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroCobroJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroCobroJuridico", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener el parametro para Cobro PreJuridico
        /// </summary>
        /// <param name="pId">identificador de Cobro PreJuridico
        /// <returns>Entidad ParametroCobroPreJuridico</returns>
        public Credito ConsultarParametroCobroPreJuridico(Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarParametroCobroPreJuridico(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarParametroCobroPreJuridico", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pCredito">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Credito obtenidos</returns>
        public List<Credito> ListarCreditoAsesor(Credito pCredito, Usuario pUsuario, String filtro, String orden)
        {
            try
            {
                return DACredito.ListarCreditoAsesor(pCredito, pUsuario, filtro, orden);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoAsesor", ex);
                return null;
            }
        }

        public List<Credito> Consultardescuentos(Usuario pUsuario)
        {
            try
            {
                return DACredito.Consultardescuentos(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditoAsesor", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene un Credito
        /// </summary>
        /// <param name="pId">Identificador de Credito</param>
        /// <returns>Entidad Credito</returns>
        public Credito ConsultarCreditoPorObligacion(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditoPorObligacion(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoPorObligacion", ex);
                return null;
            }
        }

        public Credito ConsultarCreditoSolicitud(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DACredito.ConsultarCreditoSolicitud(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoSolicitud", ex);
                return null;
            }
        }

        public Credito consultarinterescredito(Int64 pnumero_radicacion, DateTime pfecha_pago, Usuario pUsuario, long numeroRadicacion)
        {
            try
            {
                return DACredito.consultarinterescredito(pnumero_radicacion, pfecha_pago, pUsuario, numeroRadicacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "consultarinterescredito", ex);
                return null;
            }
        }

        public Decimal AmortizarCredito(Int64 pnumero_radicacion, Int64 ptipo_pago, DateTime pfecha_pago, Usuario pUsuario)
        {
            try
            {
                return DACredito.AmortizarCredito(pnumero_radicacion, ptipo_pago, pfecha_pago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "AmortizarCredito", ex);
                return 0;
            }
        }

        public Decimal AmortizarCreditoNumCuotas(Int64 pnumero_radicacion, DateTime pfecha_pago, int pnum_cuotas, Usuario pUsuario)
        {
            try
            {
                return DACredito.AmortizarCreditoNumCuotas(pnumero_radicacion, pfecha_pago, pnum_cuotas, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "AmortizarCreditoNumCuotas", ex);
                return 0;
            }
        }

        public List<Atributos> AmortizarCreditoDetalle(Int64 pnumero_radicacion, DateTime pfecha_pago, Double pvalor_pago, Int64 ptipo_pago, ref Int64 pError, Usuario pUsuario)
        {
            try
            {
                return DACredito.AmortizarCreditoDetalle(pnumero_radicacion, pfecha_pago, pvalor_pago, ptipo_pago, ref pError, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "AmortizarCreditoDetalle", ex);
                return null;
            }
        }

        public void guardardescuento(DescuentosDesembolso variable, Usuario pUsuario)
        {
            try
            {
                DACredito.guardardescuento(variable, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCreditoSolicitud", ex);

            }
        }

        public string ValidarCredito(Credito pEntidad, Usuario pUsuario)
        {
            try
            {
                return DACredito.ValidarCredito(pEntidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ValidarCrédito", ex);
                return ex.Message;
            }
        }

        public CreditoEmpresaRecaudo CrearModEmpresa_Recaudo(CreditoEmpresaRecaudo pEmpresa, Usuario vUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpresa = DACredito.CrearModEmpresa_Recaudo(pEmpresa, vUsuario, opcion);

                    ts.Complete();
                }
                return pEmpresa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "EliminarCredito", ex);
                return null;
            }
        }

        public Int64? AutorizarCredito(Int64 pnumero_radicacion, Int64 pcod_deudor, DateTime pfecha_desembolso, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64? numero = DACredito.AutorizarCredito(pnumero_radicacion, pcod_deudor, pfecha_desembolso, ref pError, pUsuario);

                    ts.Complete();

                    return numero;
                }
            }
            catch
            {
                return null;
            }
        }

        public Int32 VerificarAutorizacion(int pTipoProducto, string pNumeroProducto, DateTime pFecha, String pIp, String pAutorizacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.VerificarAutorizacion(pTipoProducto, pNumeroProducto, pFecha, pIp, pAutorizacion, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public Int32 VerificarAutorizacion(String pAutorizacion, Usuario pUsuario)
        {
            try
            {
                return DACredito.VerificarAutorizacion(pAutorizacion, pUsuario);
            }
            catch
            {
                return 0;
            }
        }

        public DateTime? FechaInicioDESEMBOLSO(Int64 pNumero_radicacion, Usuario vUsuario)
        {
            try
            {
                return DACredito.FechaInicioDESEMBOLSO(pNumero_radicacion, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "FechaInicioDESEMBOLSO", ex);
                return null;
            }
        }

        public DateTime? FechaInicioCredito(DateTime pFecha_desembolso, Int32 pCodperiodicidad, Int32 pFormapago, Int32? pCodEmpresa, string pCodLinea, ref string pError, ref Boolean pRpta, Usuario vUsuario)
        {
            try
            {
                return DACredito.FechaInicioCredito(pFecha_desembolso, pCodperiodicidad, pFormapago, pCodEmpresa, pCodLinea, ref pError, ref pRpta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "FechaInicioCredito", ex);
                return null;
            }
        }

        public void CrearSolicitudCredito(Usuario pUsuario, CreditoOrdenServicio pCredOrden, Auxilio_Orden_Servicio pAuxOrden, CreditoEducativoEntit pLineaCredito, Reestructuracion vReestructuracion, ref Int64 numero_radicacion, ref string error, SolicitudAuxilio pServicio, List<DetalleSolicitudAuxilio> lstDetalle)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // Crear el crédito
                    numero_radicacion = DACredito.InsertSolicitudCredito(pUsuario, pLineaCredito);

                    // Crear la orden de servicio del crédito
                    CreditoOrdenServicio OrdenServicio = new CreditoOrdenServicio();
                    if (pCredOrden.idproveedor != null && pCredOrden.nomproveedor != null && pCredOrden.cod_persona != null)
                    {
                        pCredOrden.numero_radicacion = numero_radicacion;
                        OrdenServicio = DACredito.CrearCreditoOrdenServicio(pCredOrden, pUsuario);
                    }

                    // Insertar los codeudores
                    Xpinn.FabricaCreditos.Data.codeudoresData DACodeudores = new Xpinn.FabricaCreditos.Data.codeudoresData();
                    if (vReestructuracion.lstCodeudores.Count > 0)
                        foreach (Xpinn.FabricaCreditos.Entities.codeudores cCodeudores in vReestructuracion.lstCodeudores)
                        {
                            codeudores entidad = new codeudores();
                            cCodeudores.numero_radicacion = numero_radicacion;
                            entidad = DACodeudores.CrearCodeudoresCredito(cCodeudores, pUsuario);
                        }

                    // Crear solicitud de auxilio                    
                    SolicitudAuxilioData DASolici = new SolicitudAuxilioData();
                    SolicitudAuxilio SoliEntity = new SolicitudAuxilio();
                    int cod = 0;
                    if (pServicio != null)
                    {
                        pServicio.numero_radicacion = numero_radicacion;
                        SoliEntity = DASolici.CrearSolicitudAuxilio(pServicio, pUsuario);

                        // Crear la orden de servicio para el auxilio
                        Auxilio_Orden_Servicio nAuxServicio = new Auxilio_Orden_Servicio();
                        if (pAuxOrden.idproveedor != null && pAuxOrden.nomproveedor != null && pAuxOrden.cod_persona != null)
                        {
                            pAuxOrden.numero_auxilio = SoliEntity.numero_auxilio;
                            nAuxServicio = DASolici.CrearAuxilioOdenServicio(pAuxOrden, pUsuario);
                        }
                        cod = SoliEntity.numero_auxilio;
                    }

                    if (lstDetalle != null)
                    {
                        foreach (DetalleSolicitudAuxilio eServ in lstDetalle)
                        {
                            DetalleSolicitudAuxilio nDetalle = new DetalleSolicitudAuxilio();
                            if (pServicio != null)
                            {
                                eServ.numero_auxilio = cod;
                                nDetalle = DASolici.CrearDetalleAuxilio(eServ, pUsuario);
                            }
                            //else
                            //    nDetalle = DASolici.ModificarDetalleAuxilio(eServ, pUsuario);
                            Credito_Beneficiario pBenefi = new Credito_Beneficiario();
                            pBenefi.codbeneficiariocre = 0;
                            pBenefi.numero_radicacion = numero_radicacion;
                            pBenefi.identificacion = eServ.identificacion;
                            pBenefi.nombre = eServ.nombre;
                            pBenefi.cod_parentesco = eServ.cod_parentesco;
                            pBenefi.porcentaje_beneficiario = eServ.porcentaje_beneficiario;

                            pBenefi = DACredito.CrearCredito_Beneficiario(pBenefi, pUsuario);
                        }
                    }


                    // Realizar la liquidación del crédito
                    Xpinn.Cartera.Data.ReestructuracionData DACartera = new ReestructuracionData();
                    DACartera.LiquidarCredito(numero_radicacion, ref error, pUsuario);
                    ts.Complete();
                }
            }

            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CrearSolicitudCredito", ex);
            }
        }

        public List<Credito> RealizarPreAnalisis(bool preanalisis, DateTime pfecha, Int64 pCodPersona, decimal pDisponible, Int64 pNumeroCuotas, decimal pMontoSolicitado, Int32 pCodPeriodicidad, bool pEducativo, Usuario pUsuario)
        {
            try
            {
                return DACredito.RealizarPreAnalisis(preanalisis, pfecha, pCodPersona, pDisponible, pNumeroCuotas, pMontoSolicitado, pCodPeriodicidad, pEducativo, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "RealizarPreAnalisis", ex);
                return null;
            }
        }

        public Int64 ObtenerNumeroPreImpreso(Usuario pUsuario)
        {
            try
            {
                return DACredito.ObtenerNumeroPreImpreso(pUsuario);
            }
            catch
            {
                return 1;
            }
        }

        ///AGREGADO
        ///
        public Credito CREARCREDITOANALISIS(Credito pCredito, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCredito = DACredito.CREARCREDITOANALISIS(pCredito, pusuario);

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "CREARCREDITOANALISIS", ex);
                return null;
            }
        }

        public decimal ObtenerSaldoTotalXpersona(Int64 pCodPersona, Usuario pUsuario)
        {
            try
            {
                return DACredito.ObtenerSaldoTotalXpersona(pCodPersona, pUsuario);
            }
            catch
            {
                return 1;
            }
        }
        public Credito DesembolsarAvances(Credito pCredito, bool pRealizarGiro, long formadesembolso, int idCtaBancaria, int cod_banco, string numerocuenta, int tipo_cuenta, ref string Error, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // Generar numero para orden de servicio
                    if (pCredito.num_preimpreso != null)
                    {
                        CreditoOrdenServicio pEntidad = new CreditoOrdenServicio();
                        String pFiltro = " WHERE NUMERO_PREIMPRESO = " + pCredito.num_preimpreso;
                        pEntidad = DACredito.ConsultarCREDITO_OrdenServ(pFiltro, pUsuario);
                        if (pEntidad.idordenservicio > 0) //Si ya existe el Num_PreImpreso
                            return null;
                    }
                    decimal pMontoDesembolso = 0;
                    // Guardar los valores a deducir del crédito 
                    if (pCredito.lstDescuentos != null)
                    {
                        foreach (DescuentosDesembolso variable in pCredito.lstDescuentos)
                        {
                            variable.numero_obligacion = Convert.ToString(pCredito.numero_radicacion.ToString().Trim());
                            guardardescuento(variable, pUsuario);
                        }
                    }
                    // Desembolsar el crédito
                    string sError = "";
                    pCredito.cod_ope = DACredito.DesembolsarAvances(pCredito, ref pMontoDesembolso, ref sError, pUsuario);                    
                    if (pCredito.cod_ope == 0)
                    {
                        Error = "No se pudo crear la operaciòn. Error:" + sError;
                        return null;
                    }
                    if (pCredito.num_preimpreso != null)
                        DACredito.ModificarCreditoOrdenServicio(pCredito.numero_radicacion, Convert.ToInt64(pCredito.num_preimpreso), pUsuario);
                    // Realizar el giro
                    if (pRealizarGiro == true)
                    {
                        //consultar si la Linea del credito es Educativo
                        LineasCreditoData DALinea = new LineasCreditoData();
                        LineasCredito pLinea = new LineasCredito();
                        pLinea = DALinea.ConsultaLineaCredito(pCredito.cod_linea_credito, pUsuario);
                        if (pLinea != null && pLinea.credito_educativo == 1 && pLinea.maneja_auxilio == 1)
                            pMontoDesembolso = pMontoDesembolso + pCredito.monto;
                        if (pMontoDesembolso == 0)
                            pMontoDesembolso = pCredito.monto;
                        DACredito.GuardarGiro(pCredito.numero_radicacion, pCredito.cod_ope, formadesembolso, pCredito.fecha_desembolso, Convert.ToDouble(pMontoDesembolso), idCtaBancaria, cod_banco, numerocuenta, tipo_cuenta, pCredito.cod_deudor, pUsuario.nombre, pUsuario);
                    }
                    // Si es transferencia a cuenta de ahorros realizarla.
                    TipoFormaDesembolso tipoFormaDesembolso = formadesembolso.ToEnum<TipoFormaDesembolso>();
                    if (tipoFormaDesembolso == TipoFormaDesembolso.TranferenciaAhorroVistaInterna)
                    {
                        if (pCredito.monto_desembolsado == 0 || pCredito.monto_desembolsado == null)
                        {
                            pCredito.monto_desembolsado = pCredito.monto;
                        }
                        AhorroVistaBusiness ahorroBusiness = new AhorroVistaBusiness();
                        AhorroVista ahorro = new AhorroVista
                        {
                            numero_cuenta = pCredito.numero_cuenta_ahorro_vista.ToString(),
                            cod_persona = pCredito.cod_deudor,
                            cod_ope = pCredito.cod_ope,
                            fecha_cierre = pCredito.fecha_desembolso,
                            V_Traslado = pCredito.monto_desembolsado,
                            codusuario = pUsuario.codusuario
                        };

                        ahorroBusiness.IngresoCuenta(ahorro, pUsuario);

                        if (ahorro.numero_cuenta != "" || ahorro.numero_cuenta != null)
                        {
                            Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                            List<Xpinn.Ahorros.Entities.CuentasCobrar> lstCuentasCobrar = new List<Ahorros.Entities.CuentasCobrar>();
                            lstCuentasCobrar = DAAhorroVistaData.ListarCuentasCobrar(ahorro.numero_cuenta, pUsuario);

                            foreach (Xpinn.Ahorros.Entities.CuentasCobrar CuetaxCobrar in lstCuentasCobrar)
                            {
                                Xpinn.Caja.Entities.TransaccionCaja pTransaccionCaja = new Xpinn.Caja.Entities.TransaccionCaja();
                                pTransaccionCaja.cod_ope = ahorro.cod_ope;
                                pTransaccionCaja.fecha_movimiento = Convert.ToDateTime(ahorro.fecha_cierre);
                                pTransaccionCaja.cod_persona = Convert.ToInt64(ahorro.cod_persona);
                                CuetaxCobrar.tipo_tran = 257;
                                DAAhorroVistaData.ProcesoCuentasCobrar(pTransaccionCaja, CuetaxCobrar, pUsuario);
                            }
                        }
                    }

                    ts.Complete();
                }

                return pCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "DesembolsarAvances", ex);
                return null;
            }
        }

        public List<Credito> ListarCreditosEducativos(DateTime pFecha, string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DACredito.ListarCreditosEducativos(pFecha, pFiltro, pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ListarCreditosEducativos", ex);
                return null;
            }
        }


        //Anderson Acuña-- Reporte Creditos Desembolsados
        public List<Credito> Reporte1026(Credito pCredito, Usuario pUsuario, String filtro)
        {
            try
            {
                return DACredito.Reporte_1026(pCredito, pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "Reporte1026", ex);
                return null;
            }
        }



        public void RegistrarError(int pNumeroLinea, string pRegistro, string pError, string pDato, ref List<Entities.ErroresCarga> plstErrores)
        {
            if (pNumeroLinea == -1)
            {
                plstErrores.Clear();
                return;
            }
            Entities.ErroresCarga registro = new Entities.ErroresCarga();
            registro.numero_registro = pNumeroLinea.ToString();
            registro.datos = pDato;
            registro.error = " Campo No.:" + pRegistro + " Error:" + pError;
            plstErrores.Add(registro);
        }

        // Anderson Acuña--Cargue Masivo  de creditos   
        private StreamReader strReader;
        public void CargarCreditos(ref string pError, Stream pstream, ref List<Credito> lstCreditos, ref List<Entities.ErroresCarga> plstErrores, Usuario pUsuario)
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

                        Credito pEntidad = new Credito();
                        for (int i = 0; i <= 9; i++)
                        {
                            if (i == 0) { try { pEntidad.identificacion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 1) { try { pEntidad.monto = Convert.ToDecimal(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 2) { try { pEntidad.numero_cuotas = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 3) { try { pEntidad.fecha_prim_pago = Convert.ToDateTime(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 4) { try { pEntidad.cod_linea_credito = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 5) { try { pEntidad.periodicidad = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 6) { try { pEntidad.Cod_Destinacion = Convert.ToInt32(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 7) { try { pEntidad.forma_pago = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 8) { try { pEntidad.idenprov = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }
                            if (i == 9) { try { pEntidad.descrpcion = Convert.ToString(arrayline[i].ToString().Trim()); } catch (Exception ex) { RegistrarError(contadorreg, i.ToString(), ex.Message, readLine.ToString(), ref plstErrores); break; } }

                            contadorreg++;
                        }

                        // Validar la persona en vacaciones
                        if (pEntidad.identificacion != null)
                            if (pEntidad.identificacion.Trim() != "")
                            {
                                if (DACredito.ConsultarVacaciones(pEntidad.identificacion, Convert.ToDateTime(pEntidad.fecha_prim_pago), pUsuario))
                                {
                                    RegistrarError(contadorreg, "", "La persona " + pEntidad.identificacion + " esta en vacaciones", readLine.ToString(), ref plstErrores);
                                }
                            }

                        if (!(pEntidad.identificacion.Trim() == "" && pEntidad.monto == 0 && pEntidad.numero_cuotas == 0 && pEntidad.cod_linea_credito == null))
                            lstCreditos.Add(pEntidad);
                    }
                }
            }
            catch (IOException ex)
            {
                pError = ex.Message;
            }
        }


        public void CrearImportacionCred(List<Credito> lstCreditos, ref string pError, ref List<Credito> lst_Num_cred, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstCreditos != null && lstCreditos.Count > 0)
                    {
                        foreach (Credito itemcred in lstCreditos)
                        {
                            /*Insertamos los valores por defecto*/
                            DateTime fecha = DateTime.Now;

                            itemcred.fecha_solicitud = Convert.ToDateTime(fecha.ToString("dd/MM/yyyy"));
                            itemcred.fecha_creacion = Convert.ToDateTime(fecha.ToString("dd/MM/yyyy"));

                            Credito pEntidad = new Credito();
                            pEntidad = DACredito.CrearCreditoImportado(itemcred, pUsuario);

                            if (pEntidad.numero_radicacion == 0)
                            {
                                //lst_Num_cred.Add(pEntidad.identificacion);                             
                                lst_Num_cred.Add(pEntidad);
                            }

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




        public Credito ConsultarCierreCartera(Usuario vUsuario)
        {
            try
            {
                Credito Credito = new Credito();

                Credito = DACredito.ConsultarCierreCartera(vUsuario);

                return Credito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCierreCartera", ex);
                return null;
            }
        }

        public bool ModificarDescuentos(List<DescuentosCredito> plstDescuentosCredito, Usuario pUsuario)
        {
            try
            {
                // Tuvo que quitarse el transaction porque en COOACEDED con Oracle 10G genera un error.
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (plstDescuentosCredito != null)
                    {
                        foreach (DescuentosCredito edesc in plstDescuentosCredito)
                        {
                            DACredito.ModificarDescuentosCredito(edesc, pUsuario);
                        }
                        ts.Complete();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CreditoBusiness", "ConsultarCierreCartera", ex);
                return false;
            }
        }


    }
}