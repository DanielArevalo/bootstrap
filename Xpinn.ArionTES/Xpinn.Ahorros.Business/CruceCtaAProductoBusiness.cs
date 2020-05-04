using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Ahorros.Data;
using Xpinn.Ahorros.Entities;

namespace Xpinn.Ahorros.Business
{
    /// <summary>
    /// Objeto de negocio para Destinacion
    /// </summary>
    public class CruceCtaAProductoBusiness : GlobalBusiness
    {
        private CruceCtaAProductoData DAAhorroVista;

        /// <summary>
        /// Constructor del objeto de negocio para Destinacion
        /// </summary>
        public CruceCtaAProductoBusiness()
        {
            DAAhorroVista = new CruceCtaAProductoData();
        }


        public Boolean CrearSolicitud_cruce_ahorro(List<Solicitud_cruce_ahorro> lstSolicitud, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstSolicitud != null)
                    {
                        if (lstSolicitud.Count > 0)
                        {
                            foreach (Solicitud_cruce_ahorro nSolicitud in lstSolicitud)
                            {
                                Solicitud_cruce_ahorro pEntidad = new Solicitud_cruce_ahorro();
                                pEntidad = DAAhorroVista.CrearSolicitud_cruce_ahorro(nSolicitud, ref pError, vUsuario);
                                if (pError != "")
                                {
                                    return false;
                                }
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


        public void EliminarSolicitud_Cruce_ahorro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAAhorroVista.EliminarSolicitud_Cruce_ahorro(pId, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoBusiness", "EliminarSolicitud_Cruce_ahorro", ex);
            }
        }


        public Solicitud_cruce_ahorro ConsultarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            try
            {
                Solicitud_cruce_ahorro pResult = new Solicitud_cruce_ahorro();
                pResult = DAAhorroVista.ConsultarSolicitud_cruce(pFiltro, vUsuario);
                return pResult;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoBusiness", "ConsultarSolicitud_cruce", ex);
                return null;
            }
        }

        public List<Solicitud_cruce_ahorro> ListarSolicitud_cruce(string pFiltro, Usuario vUsuario)
        {
            try
            {
                return DAAhorroVista.ListarSolicitud_cruce(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoBusiness", "ListarSolicitud_cruce", ex);
                return null;
            }
        }

        public void CambiarEstado_Solicitud_Cruce_ahorro(Solicitud_cruce_ahorro p_solicitud, Usuario _usuario)
        {
            try
            {
                DAAhorroVista.CambiarEstadoSolicitud_CruceAhorro(p_solicitud, _usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CruceCtaAProductoBusiness", "CambiarEstado_Solicitud_Cruce_ahorro", ex);
                return;
            }
        }

        public Boolean AplicarCruceProducto(List<Solicitud_cruce_ahorro> lstSolicitud, Usuario pUsuario, Int64 pProcesoCont, ref List<Xpinn.Tesoreria.Entities.Operacion> lstOperaciones, ref string Error)
        {
            Xpinn.Tesoreria.Data.PagosVentanillaData DAPagosVentanilla = new Xpinn.Tesoreria.Data.PagosVentanillaData();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                     Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();

                    if (lstSolicitud != null && lstSolicitud.Count > 0)
                    {
                        foreach (Solicitud_cruce_ahorro nSolicitud in lstSolicitud)
                        {
                            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Tesoreria.Entities.Operacion();
                            //CREANDO OPERACION
                            Int64 pCod_operacion = 0;
                            pOperacion.cod_ope = 0;
                            pOperacion.tipo_ope = 125;
                            pOperacion.fecha_oper = DateTime.Now;
                            pOperacion.fecha_calc = DateTime.Now;
                            pOperacion.observacion = "Aprobación de Cruce Cuenta a Producto";
                            pOperacion.cod_proceso = null;
                            try
                            {
                                pOperacion = DATesoreria.GrabarOperacion(pOperacion, pUsuario);
                                pCod_operacion = pOperacion.cod_ope;
                            }
                            catch (Exception ex)
                            {
                                Error = ex.Message;
                            }
                            //CREANDO LAS TRANSACCIONES
                            if (Error.Trim() == "")
                            {
                                Xpinn.Caja.Entities.TransaccionCaja pTransac = new Xpinn.Caja.Entities.TransaccionCaja();
                                pTransac.num_producto = Convert.ToInt64(nSolicitud.num_producto);
                                pTransac.cod_persona = nSolicitud.cod_persona;
                                pTransac.cod_ope = pCod_operacion;
                                pTransac.fecha_cierre = Convert.ToDateTime(nSolicitud.fecha_pago);
                                pTransac.valor_pago = nSolicitud.valor_pago;
                                pTransac.tipo_pago = nSolicitud.tipo_tran;
                                pTransac.cod_cajero = pUsuario.codusuario;
                                pTransac.baucher = null;
                                pTransac = DAPagosVentanilla.AplicarTransaccion(pTransac, pUsuario, ref Error);
                                
                                if (Error.Trim() != "")
                                    return false;

                                if (!string.IsNullOrEmpty(pTransac.error))
                                {
                                    Error = pTransac.error;
                                    nSolicitud.estado = 2;
                                    DAAhorroVista.ModificarSolicitud_CruceAhorro(nSolicitud, ref Error, pUsuario);
                                    return false;
                                }

                                AhorroVistaData DAAhorros = new AhorroVistaData();
                                if (nSolicitud.valor_pago > 0)
                                {
                                    AhorroVista pEntidad = new AhorroVista();
                                    pEntidad.numero_cuenta = nSolicitud.numero_cuenta;
                                    pEntidad.valor_a_aplicar = nSolicitud.valor_pago;
                                    DAAhorros.Aplicar(pEntidad, pCod_operacion, pUsuario);
                                }

                                //ACTUALIZANDO SOLICITUD                                
                                nSolicitud.estado = 1; //Aprobar
                                DAAhorroVista.ModificarSolicitud_CruceAhorro(nSolicitud, ref Error, pUsuario);
                            }

                            Xpinn.Contabilidad.Business.ComprobanteBusiness comprobanteBusiness = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
                            Int64 pnum_comp = 0, ptipo_comp = 0;
                            //CREANDO EL COMPROBANTE
                            if (comprobanteBusiness.GenerarComprobante(pCod_operacion, 125, Convert.ToDateTime(nSolicitud.fecha_pago), pUsuario.cod_oficina, nSolicitud.cod_persona, pProcesoCont, ref pnum_comp, ref ptipo_comp, ref Error, pUsuario))
                            {
                                Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                                oper.cod_ope = pCod_operacion;
                                oper.num_comp = pnum_comp;
                                oper.tipo_comp = ptipo_comp;
                                lstOperaciones.Add(oper);
                            }
                        }
                    }                    
                }
                return true;
            }
            catch (Exception ex)
            {
                Error = Error + ex.Message;
                return false;
            }
        }


    }
}