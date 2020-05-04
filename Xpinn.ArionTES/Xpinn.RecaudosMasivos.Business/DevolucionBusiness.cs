using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    public class DevolucionBusiness : GlobalData
    {

        private DevolucionData BADevolucion;

        public DevolucionBusiness()
        {
            BADevolucion = new DevolucionData();
        }


        public Devolucion Crear_Mod_Devolucion(Devolucion pDevol, Usuario pUsuario, int opcion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDevol = BADevolucion.Crear_Mod_Devolucion(pDevol, pUsuario, opcion);

                    if (opcion == 1)
                    {
                        // Crear la operación                    
                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new OperacionData();
                        Operacion vOpe = new Operacion();
                        vOpe.cod_ope = 0;
                        vOpe.tipo_ope = 105;
                        vOpe.cod_caja = 0;
                        vOpe.cod_cajero = 0;
                        vOpe.observacion = "Grabacion de Operacion-Creación Devolucion";
                        vOpe.cod_proceso = null;
                        vOpe.fecha_oper = DateTime.Now;
                        vOpe.fecha_calc = DateTime.Now;
                        vOpe.cod_ofi = pUsuario.cod_oficina;
                        vOpe = DAOperacion.GrabarOperacion(vOpe, pUsuario);
                        pDevol.cod_ope = vOpe.cod_ope;
                        // Grabar la transacción
                        if (vOpe.cod_ope != null)
                        {
                            pDevol.numero_transaccion = 0;
                            pDevol.tipo_tran = 905;
                            BADevolucion.CrearTransaccionDevolucion(pDevol, pUsuario);
                        }
                    }

                    ts.Complete();
                }

                return pDevol;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "CrearDevolucion", ex);
                return null;
            }
        }



        public Int64 EliminarDevolucion(Int64 pId, decimal pValor, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    BADevolucion.EliminarDevolucion(pId, pUsuario);
                    // Crear la operación                    
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new OperacionData();
                    Operacion vOpe = new Operacion();
                    vOpe.cod_ope = 0;
                    vOpe.tipo_ope = 104;
                    vOpe.cod_caja = 0;
                    vOpe.cod_cajero = 0;
                    vOpe.observacion = "Operacion-Anulacion de Devoluciones";
                    vOpe.cod_proceso = null;
                    vOpe.fecha_oper = DateTime.Now;
                    vOpe.fecha_calc = DateTime.Now;
                    vOpe.cod_ofi = pUsuario.cod_oficina;
                    vOpe = DAOperacion.GrabarOperacion(vOpe, pUsuario);

                    // Grabar la transacción
                    if (vOpe != null)
                    {                        
                        // Grabar la transacción
                        if (vOpe.cod_ope != 0)
                        {
                            Devolucion pDevol = new Devolucion();
                            pDevol.numero_transaccion = 0;
                            pDevol.cod_ope = vOpe.cod_ope;
                            pDevol.num_devolucion = Convert.ToInt32(pId);
                            pDevol.tipo_tran = 904;
                            pDevol.valor = pValor;
                            pDevol.estado = "1";
                            BADevolucion.CrearTransaccionDevolucion(pDevol, pUsuario);
                        }
                    }

                    ts.Complete();

                    return vOpe.cod_ope;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "EliminarDevolucion", ex);
                return 0;
            }
        }

        public List<Devolucion> ListarDevolucion(Devolucion pDevolucion,DateTime pFecha, Usuario vUsuario,string filtro)
        {
            try
            {
                return BADevolucion.ListarDevolucion(pDevolucion,pFecha,vUsuario,filtro);
            }
            catch
            {
                return null;
            }
        }

        public Devolucion ConsultarDevolucion(int pId, Usuario vUsuario)
        {
            try
            {
                return BADevolucion.ConsultarDevolucion(pId, vUsuario);

            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ConsultarDevolucion", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BADevolucion.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }


        public Devolucion ConsultarDetalleRecaudo(int pId, Usuario vUsuario)
        {
            try
            {
                return BADevolucion.ConsultarDetalleRecaudo(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ConsultarDetalleRecaudo", ex);
                return null;
            }
        }


        public TransaccionCaja AplicarDevoluciones(TransaccionCaja pTransaccionCaja, PersonaTransaccion perTran, GridView gvTransacciones, List<Devolucion> lstDevolucion, string pObservacion, Usuario pUsuario, ref string Error)
        {
            PagosVentanillaData DAPagosVentanilla = new PagosVentanillaData();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTransaccionCaja = DAPagosVentanilla.AplicarPagoVentanilla(pTransaccionCaja, perTran, gvTransacciones, null, null, pObservacion, pUsuario, ref Error);
                    foreach (Devolucion eDevol in lstDevolucion)
                    {
                        if (eDevol.valor_a_aplicar != 0)
                            BADevolucion.AplicarDevolucion(eDevol, pTransaccionCaja.cod_ope, pUsuario);
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

        public List<Devolucion> ConsultarDevolucionDetalle(int pId, Usuario vUsuario)
        {
            try
            {
                return BADevolucion.ConsultarDevolucionDetalle(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ConsultarDevolucionDetalle", ex);
                return null;
            }
        }
        

    }
}


