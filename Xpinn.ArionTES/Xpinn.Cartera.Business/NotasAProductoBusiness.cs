using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;
using System.Web.UI.WebControls;
using Xpinn.Caja.Entities;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Cartera.Business
{
    public class NotasAProductoBusiness : GlobalData
    {

        private Xpinn.Tesoreria.Data.PagosVentanillaData DANotas;
        private NotasAProductoData DANaProducto;

        /// <summary>
        /// Constructor del objeto de negocio para Castigo
        /// </summary>
        public NotasAProductoBusiness()
        {
            DANotas = new Xpinn.Tesoreria.Data.PagosVentanillaData();
            DANaProducto = new NotasAProductoData();
        }


        public TransaccionCaja AplicarNotasAProductos(Devolucion pDevol, Boolean GeneraDevolucion, TransaccionCaja pOperacion, GridView gvTransacciones, Usuario pUsuario,Boolean Pendientes, ref string Error)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //CREAR LA OPERACION
                    pOperacion = DANaProducto.CrearOperacionGeneral(pOperacion, pUsuario);
                    Int64 COD_OPE = pOperacion.cod_ope;

                    //GRABANDO LAS TRANSACCIONES
                    long moneda = 0;
                    long tipoproducto = 0;
                    //string nomtipomov = "";
                    string nroprod = "";
                    long tipotran = 0;
                    decimal valor = 0;
                    string nroRef = "0";
                    string tippago = "";

                    foreach (GridViewRow fila in gvTransacciones.Rows)
                    {
                        moneda = long.Parse(fila.Cells[2].Text);
                        tipotran = long.Parse(fila.Cells[12].Text);
                        tipoproducto = long.Parse(fila.Cells[3].Text); // no se usa
                        nroprod = Convert.ToString(fila.Cells[8].Text);
                        valor = decimal.Parse(fila.Cells[9].Text);
                        nroRef = fila.Cells[8].Text;
                        tippago = fila.Cells[12].Text;
                        string nomtiponota = fila.Cells[6].Text;
                        int tiponota = 0;
                        if (nomtiponota == "Nota D&#233;bito")
                            tiponota = 0;
                        else if (nomtiponota == "Nota Cr&#233;dito")
                            tiponota = 1;
                        else if (nomtiponota == "Pago por Valor")
                            tiponota = 2;
                        else if (nomtiponota == "Pago Total")
                            tiponota = 3;
                        int cod_atr = 1;
                        if (fila.Cells[13].Text.Trim() != "")
                            try { cod_atr = Convert.ToInt32(fila.Cells[13].Text); }
                            catch { }
                        //CREANDO LAS TRANSACCIONES
                        DANaProducto.CrearTransaccion(nroRef, pOperacion.cod_persona, COD_OPE, pOperacion.fecha_cierre, valor, moneda, tipoproducto, nroprod, tiponota, cod_atr, pUsuario,Pendientes, ref Error);
                    }
                    
                    //GENERAR DEVOLUCION
                    if (GeneraDevolucion == true)
                    {
                        Xpinn.Tesoreria.Data.DevolucionData BADevolucion = new Xpinn.Tesoreria.Data.DevolucionData();
                        pDevol = BADevolucion.Crear_Mod_Devolucion(pDevol, pUsuario, 1); //Crear devolucion

                        pDevol.cod_ope = COD_OPE;
                        // GRABAR LA TRANSACCION
                        if (COD_OPE != null)
                        {
                            pDevol.numero_transaccion = 0;
                            pDevol.tipo_tran = 905;
                            BADevolucion.CrearTransaccionDevolucion(pDevol, pUsuario);
                        }
                    }

                    ts.Complete();
                }
                return pOperacion;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return null;
            }
        }

    }
}
