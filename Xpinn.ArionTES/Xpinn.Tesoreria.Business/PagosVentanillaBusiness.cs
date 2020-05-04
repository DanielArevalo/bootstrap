using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para PagosVentanilla
    /// </summary>
    public class PagosVentanillaBusiness : GlobalBusiness
    {
        private PagosVentanillaData DAPagosVentanilla;

        /// <summary>
        /// Constructor del objeto de negocio para TransaccionCaja
        /// </summary>
        public PagosVentanillaBusiness()
        {
            DAPagosVentanilla = new PagosVentanillaData();
        }

        /// <summary>
        /// Realizar la aplicación del pago
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad TransaccionCaja</param>
        /// <returns>Entidad TransaccionCaja creada</returns>
        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //Validar Fecha Transaciones                    
                    string NumError = "";
                    string ErrorTitular = "";
                    string NumProducto;
                    int TipoProduc;
                    string FechaAperProd = "";
                    string TipoPagoPro;
                    int producto;
                    List<string> ListCuentasXcobrar = new List<string>();


                    //Validamos fecha Apertura de los productos que entran en la transacción
                    foreach (GridViewRow fila in gvTransacciones.Rows)
                    {
                        TipoProduc = Convert.ToInt32(fila.Cells[4].Text);
                        NumProducto = Convert.ToString(fila.Cells[8].Text);
                        TipoPagoPro = Convert.ToString(fila.Cells[12].Text);

                        if (TipoProduc == 3)
                        {
                            if (TipoPagoPro == "203")
                            {
                                ListCuentasXcobrar.Add(NumProducto);
                            }
                        }

                        FechaAperProd = DAPagosVentanilla.FechasApertura(NumProducto, TipoProduc, pUsuario);

                        if (FechaAperProd != "" && FechaAperProd != null)
                        {
                            if (pTransaccionCaja.fecha_aplica < Convert.ToDateTime(FechaAperProd))
                            {
                                if (NumError != "")
                                    NumError += ", " + NumProducto;
                                else
                                    NumError += NumProducto;
                            }
                        }

                        //validamos el producto con su asociado
                        producto = DAPagosVentanilla.TitularProducto(pTransaccionCaja.cod_persona, NumProducto, TipoProduc, pUsuario);
                        if (TipoProduc == 6 || TipoProduc == 7 || TipoProduc == 8 || TipoProduc == 10) // 6:Afiliación, 7: Otros, 8:devoluciones, 10:Giros
                        {
                            producto = 1;
                        }

                        if (producto == 0)
                        {
                            if (ErrorTitular != "")
                                ErrorTitular += ", " + NumProducto;
                            else
                                ErrorTitular += NumProducto;
                        }

                    }

                    if (NumError != "")
                    {
                        Error = "La fecha de la operación es menor a la de apertura de los productos: " + NumError;
                        return null;
                    }

                    if (ErrorTitular != "")
                    {
                        Error = "Los productos: " + ErrorTitular + ", no pertenecen a la persona que se le registra la operación";
                        return null;
                    }

                    if (perTran == null)
                        pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
                    else
                        pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);

                    //Para Aplicar pago a las cuentas por cobrar
                    if (ListCuentasXcobrar.Count > 0)
                    {
                        foreach (string item in ListCuentasXcobrar)
                        {
                            Xpinn.Ahorros.Data.AhorroVistaData DAAhorroVistaData = new Ahorros.Data.AhorroVistaData();
                            List<Xpinn.Ahorros.Entities.CuentasCobrar> lstCuentasCobrar = new List<Ahorros.Entities.CuentasCobrar>();
                            lstCuentasCobrar = DAAhorroVistaData.ListarCuentasCobrar(item, pUsuario);

                            if (lstCuentasCobrar.Count > 0)
                            {
                                foreach (Xpinn.Ahorros.Entities.CuentasCobrar CuetaxCobrar in lstCuentasCobrar)
                                {
                                    CuetaxCobrar.tipo_tran = 257;
                                    DAAhorroVistaData.ProcesoCuentasCobrar(pTransaccionCaja, CuetaxCobrar, pUsuario);
                                }
                            }
                        }
                    }

                    if (Error.Trim() != "")
                        return null;
                    ts.Complete();
                }
                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

        public TransaccionCaja AplicarPagoVentanilla(TransaccionCaja pTransaccionCaja, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {                
                return AplicarPagoVentanilla(pTransaccionCaja, null, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

        public TransaccionCaja AplicarPagoCruceAporte(Xpinn.Aportes.Entities.Aporte pAportes, TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, GridView gvFormaPago, GridView gvCheques, string pObservacion, Usuario pUsuario, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, gvFormaPago, gvCheques, pObservacion, pUsuario, ref Error);
                    Int64 cod_ope = pTransaccionCaja.cod_ope;
                    if (pAportes.lstAporte != null && pAportes.lstAporte.Count > 0)
                    {
                        Xpinn.Aportes.Data.AporteData nDataAporte = new Xpinn.Aportes.Data.AporteData();
                        foreach (Xpinn.Aportes.Entities.Aporte nApor in pAportes.lstAporte)
                        {
                            Xpinn.Aportes.Entities.Aporte xAporte = new Xpinn.Aportes.Entities.Aporte();
                            nApor.cod_ope = cod_ope;
                            xAporte = nDataAporte.CrearRetiroAporte(nApor, pUsuario);
                        }
                    }
                    ts.Complete();
                }
                return pTransaccionCaja;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }


        public TransaccionCaja ActualizarSaldoPersonaAfiliacion(TransaccionCaja pEntidad, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEntidad = DAPagosVentanilla.ActualizarSaldoPersonaAfiliacion(pEntidad, pUsuario);
                    ts.Complete();
                }
                return pEntidad;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PagosVentanillaBusiness", "ActualizarSaldoPersonaAfiliacion", ex);
                return null;
            }
        }

        public string ParametroGeneral(Int64 pCodigo, Usuario pUsuario)
        {
            return DAPagosVentanilla.ParametroGeneral(pCodigo, pUsuario);
        }


    }

}
