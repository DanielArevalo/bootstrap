using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.Tesoreria.Data;
using Xpinn.FabricaCreditos.Entities;
using System.Web;
using System.IO;

namespace Xpinn.FabricaCreditos.Business
{
    public class GarantiasComunitariasBusiness : GlobalBusiness
    {
        private GarantiasComunitariasData garantiaData;
        private StreamReader strReader;

        public GarantiasComunitariasBusiness()
        {
            garantiaData = new GarantiasComunitariasData();
        }


        public Boolean CargarArchivo(GarantiasComunitarias pEntityGarantiasComunitarias, ref string error, Usuario pUsuario)
        {
            Boolean output = true;
            String readLine;
            int contador = 0;
            try
            {
                using (strReader = new StreamReader(pEntityGarantiasComunitarias.stream))
                {
                    while (strReader.Peek() >= 0)
                    {
                        readLine = strReader.ReadLine();

                        Cargar(readLine, pEntityGarantiasComunitarias, contador, ref error, pUsuario);
                        contador = contador + 1;
                    }

                }

            }
            catch (IOException ex)
            {
                //BOExcepcion.Throw("BusinessEjecutivoMeta", "CrearCliente", ex);
                error = ex.Message;
            }
            return output;
        }

        private void Cargar(String lineFile, GarantiasComunitarias pEntityGarantiasComunitarias, int contador, ref string perror, Usuario pUsuario)
        {
            perror = "";
            try
            {
                GarantiasComunitarias grantias = new GarantiasComunitarias();
                if (lineFile.Trim() != "")
                {
                    String[] arrayline = lineFile.Split(';');
                    grantias.NUMERO_RADICACION = Convert.ToString(Convert.ToInt64(arrayline[0].ToString()));
                    grantias.NITENTIDAD = Convert.ToString(arrayline[1].ToString());
                    grantias.FECHAPROXPAGO = Convert.ToString(Convert.ToDateTime(arrayline[2].ToString()).ToShortDateString());
                    try { grantias.CUOTAS_RECLAMAR = Convert.ToString(arrayline[3].ToString()); }
                    catch (Exception ex) { perror = ex.Message; return; }
                    grantias.CAPITAL = Convert.ToDouble(Convert.ToDouble(arrayline[4].ToString()));
                    grantias.RECLAMACION = Convert.ToString(arrayline[5].ToString());
                    grantias.IDENTIFICACION = Convert.ToString(arrayline[6].ToString());
                    grantias.DIASMORA = "0";
                    grantias.INT_CORRIENTES = 0;
                    grantias.INT_MORA = 0;

                   // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                    {
                        if (contador == 0)
                        {
                            garantiaData.CrearReclamacionPago(grantias, pUsuario);
                            pEntityGarantiasComunitarias.numero_reclamacion = grantias.numero_reclamacion;
                        }

                        garantiaData.CrearReclamacionPagoDetalle(grantias, pUsuario);

                       // ts.Complete();
                    }
                }

            }
            catch (TransactionException ex)
            {
                //BOExcepcion.Throw("BusinessEjecutivoMeta", "CargarEjecutivoMetaPorLineaArchivo", ex);
                perror = ex.Message;
            }
        }

        public void CrearReclamacion(GarantiasComunitarias reclamaciones, Usuario pUsuario, string fecha, int encabezado)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (encabezado <= 0)
                        garantiaData.CrearReclamacion(pUsuario, fecha);

                    garantiaData.CrearReclamacionDetalle(reclamaciones, pUsuario, fecha);

                 //   ts.Complete();
                }

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("", "", ex);

            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamaciones(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return garantiaData.consultargarantiascomunitariasReclamaciones(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitarias(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return garantiaData.consultargarantiascomunitarias(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasReclamacionesdetalle(string fechaini, int reclamacion, Usuario pUsuario)
        {
            try
            {
                return garantiaData.consultargarantiascomunitariasReclamacionesdetalle(fechaini, reclamacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(Int64 numero_reclamacion, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ConsultarGarantiasComunitariasReclamacionesDetalle_Pago(numero_reclamacion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ConsultarGarantiasComunitariasReclamacionesDetalle_Pago", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiasconsultarReclamacion(string fechaini, Usuario pUsuario)
        {
            try
            {
                return garantiaData.consultargarantiasconsultarReclamacion(fechaini, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }

        public List<GarantiasComunitarias> consultargarantiascomunitariasCartera(string fechaini, string fechafin, int cod, Usuario pUsuario)
        {
            try
            {
                return garantiaData.consultargarantiascomunitariasCartera(fechaini, fechafin, cod, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return null;
            }
        }


        public Boolean AplicarPago(Int64 numero_reclamacion, DateTime fecha_reclamacion, List<GarantiasComunitarias> lstReclamaciones, Usuario pUsuario, ref string Error, ref Int64 pCodOpe)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Int64 error = 0;
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 3;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = fecha_reclamacion;
                    pOperacion.fecha_calc = fecha_reclamacion;
                    pOperacion.num_lista = numero_reclamacion;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    pCodOpe = pOperacion.cod_ope;

                    // Aplica las reclamaciones en cada crédito
                    Xpinn.FabricaCreditos.Data.CuentaPorCobrarCreData DACuentaCobrar = new Xpinn.FabricaCreditos.Data.CuentaPorCobrarCreData();
                    if (Error.Trim() == "")
                    {
                        foreach (GarantiasComunitarias gReclamacion in lstReclamaciones)
                        {
                            // Determinar datos de la cuenta por cobrar
                            Xpinn.FabricaCreditos.Entities.CuentaPorCobrarCre pCuentaCobrar = new Xpinn.FabricaCreditos.Entities.CuentaPorCobrarCre();
                            pCuentaCobrar.numero_radicacion = Convert.ToInt64(gReclamacion.NUMERO_RADICACION);
                            pCuentaCobrar.tipo_cta = 1;                            
                            pCuentaCobrar.fecha_cta = fecha_reclamacion;
                            pCuentaCobrar.cod_usuario = pUsuario.codusuario;
                            double saldo_anterior = DACuentaCobrar.ConsultarCuentaPorCobrar(pCuentaCobrar, pUsuario, ref Error);
                            pCuentaCobrar.total = gReclamacion.VALOR_PAGADO + saldo_anterior;
                            pCuentaCobrar.cod_ope = pOperacion.cod_ope;
                            // Cancelar cuentas por cobrar anteriores                            
                            DACuentaCobrar.CerrarCuentaPorCobrar(pCuentaCobrar, pUsuario, ref Error);
                            // Aplicar el pago al crédito de la reclamación
                            garantiaData.AplicarPago(gReclamacion, pOperacion.cod_ope, pUsuario, error, ref Error);
                            if (Error.Trim() == "")
                            {
                                // Crear la nueva cuenta por cobrar por el valor de la reclamación                                                  
                                DACuentaCobrar.CrearCuentaPorCobrar(pCuentaCobrar, pUsuario, ref Error);
                            }
                            else
                            {
                                return false;
                            }
                        }
                       // ts.Complete();
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return false;
            }

        }

        public Boolean Validar(DateTime fecha_reclamacion, List<GarantiasComunitarias> lstReclamaciones, Usuario pUsuario, ref string Error)
        {
            try
            {
              //  using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    foreach (GarantiasComunitarias gReclamacion in lstReclamaciones)
                    {
                        garantiaData.Validar(gReclamacion, pUsuario, ref Error);
                        if (Error.Trim() != "")
                        {
                            return false;
                        }
                    }
                   // ts.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasService", "ListarGarantias", ex);
                return false;
            }

        }

    }
}
