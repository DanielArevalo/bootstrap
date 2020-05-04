using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Business
{
    /// <summary>
    /// Objeto de negocio para Aporte
    /// </summary>
    public class RevalorizacionAporteBusiness : GlobalBusiness
    {
        private RevalorizacionAporteData DARevalorizacionAporte;
        private List<RevalorizacionAportes> DetalleRevalorizacion = new List<RevalorizacionAportes>();

        /// <summary>
        /// Constructor del objeto de negocio para Aporte
        /// </summary>
        public RevalorizacionAporteBusiness()
        {
            DARevalorizacionAporte = new RevalorizacionAporteData();
        }

        /// <summary>
        /// Obtiene una RevalorizacionAportes
        /// </summary>
        /// <param name="pId">identificador del Reintegro</param>
        /// <returns>RevalorizacionAportes consultada</returns>
        public RevalorizacionAportes ConsultarFecUltCierre(Usuario pUsuario)
        {
            try
            {
                return DARevalorizacionAporte.ConsultarFecUltCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAportesBusiness", "ConsultarFecUltCierre", ex);
                return null;
            }
        }

       

        public List<RevalorizacionAportes> Listar(RevalorizacionAportes pEntidad, ref List<RevalorizacionAportes> lstNoCalculados, Usuario pUsuario)
        {
            try
            {
                DetalleRevalorizacion = DARevalorizacionAporte.Listar(pEntidad, ref lstNoCalculados, pUsuario);
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                //    DetalleRevalorizacion = DARevalorizacionAporte.Listar(pEntidad, pUsuario);

                //    ts.Complete();
                //}

                return DetalleRevalorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAportesBusiness", "Listar", ex);
                return null;
            }
        }

        public List<RevalorizacionAportes> ListarRevalorizacion(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return DARevalorizacionAporte.ListarRevalorizacion(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAportesBusiness", "ListarRevalorizacion", ex);
                return null;
            }
        }

        public List<RevalorizacionAportes> ListarDatosComprobante(String filtro, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DetalleRevalorizacion = DARevalorizacionAporte.ListarDatosComprobante(filtro, pUsuario);

                    ts.Complete();
                }

                return DetalleRevalorizacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RevalorizacionAportesBusiness", "ListarDatosComprobante", ex);
                return null;
            }
        }
        public RevalorizacionAportes GrabarRevalorizacion(RevalorizacionAportes pEntidad,ref Int64 vCod_ope, ref string Error, Usuario pUsuario)
        {
            Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
            Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();

            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                //{
                    int num = 0;

                    if (pEntidad.lstRevalorizacionAportes != null)
                    {
                        // Crear la operación
                        pOperacion.cod_ope = 0;
                        pOperacion.tipo_ope = 19;
                        pOperacion.cod_usu = pUsuario.codusuario;
                        pOperacion.cod_ofi = pUsuario.cod_oficina;
                        pOperacion.fecha_oper = pEntidad.fecha;
                        pOperacion.fecha_calc = pEntidad.fecharevalorizacion;
                        pOperacion.num_lista = 0;
                        pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion,ref Error, pUsuario);
                        vCod_ope = pOperacion.cod_ope;
                        pEntidad.cod_ope = vCod_ope;
                    if (vCod_ope == 0 && !string.IsNullOrEmpty(Error))
                        return null;

                    foreach (RevalorizacionAportes eProg in pEntidad.lstRevalorizacionAportes)
                        {
                            eProg.cod_ope = vCod_ope;
                            eProg.fecha = pEntidad.fecha;                            
                            eProg.fecharevalorizacion = pEntidad.fecharevalorizacion;
                            DARevalorizacionAporte.GrabarRevalorizacion(eProg, pUsuario);
                        }
                        num += 1;
                    }

                    //ts.Complete();
                //}
               
            }
                 
            catch (Exception ex)
            {
                Error = ex.Message;
                //return null;              
            }
            return pEntidad;

        }

       


    }
}
