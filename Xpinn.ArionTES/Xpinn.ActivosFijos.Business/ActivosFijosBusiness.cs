using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.ActivosFijos.Data;
using Xpinn.ActivosFijos.Entities;
 


namespace Xpinn.ActivosFijos.Business
{
    /// <summary>
    /// Objeto de negocio para ActivosFijos
    /// </summary>
    public class ActivosFijosBusiness : GlobalBusiness
    {
        private ActivosFijosData DAActivosFijos;

        /// <summary>
        /// Constructor del objeto de negocio para ActivosFijos
        /// </summary>
        public ActivosFijosBusiness()
        {
            DAActivosFijos = new ActivosFijosData();
        }

        /// <summary>
        /// Crea un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos creada</returns>
        public ActivoFijo CrearActivosFijos(ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos = DAActivosFijos.CrearActivoFijo(pActivosFijos, pUsuario);
                    pActivosFijos = DAActivosFijos.CrearDatosAdicionales(pActivosFijos, pUsuario);
                    if (pActivosFijos.num_poliza != null && pActivosFijos.valor != null && pActivosFijos.fecha_poliza != null)
                        pActivosFijos = DAActivosFijos.CrearPolizaActivo(pActivosFijos, pUsuario);
                    //AGREGADO
                    if (pActivosFijos.numero_cuotas != null && (pActivosFijos.valor_cuota != null)) 
                        pActivosFijos = DAActivosFijos.CrearLEASING(pActivosFijos, pUsuario);

                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "CrearActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un ActivosFijos
        /// </summary>
        /// <param name="pActivosFijos">Entidad ActivosFijos</param>
        /// <returns>Entidad ActivosFijos modificada</returns>
        public ActivoFijo ModificarActivosFijos(ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos = DAActivosFijos.ModificarActivoFijo(pActivosFijos, pUsuario);

                    ActivoFijo vActivos = new ActivoFijo();
                    vActivos = DAActivosFijos.ConsultarDatosAdicionales(pActivosFijos, pUsuario);
                    if (vActivos == null || vActivos.bDatosAdicionales == false)
                        pActivosFijos = DAActivosFijos.CrearDatosAdicionales(pActivosFijos, pUsuario);
                    else
                        pActivosFijos = DAActivosFijos.ModificarDatosAdicionales(pActivosFijos, pUsuario);

                    ActivoFijo cActivos = new ActivoFijo();
                    cActivos.consecutivo = pActivosFijos.consecutivo;
                    cActivos = DAActivosFijos.ConsultarPolizaActivo(cActivos, pUsuario);
                    if (cActivos == null || cActivos.bPoliza == false)
                    {
                        if (pActivosFijos.num_poliza != null && pActivosFijos.valor != null && pActivosFijos.fecha_poliza != null)
                            pActivosFijos = DAActivosFijos.CrearPolizaActivo(pActivosFijos, pUsuario);
                    }
                    else
                        pActivosFijos = DAActivosFijos.ModificarPolizaActivo(pActivosFijos, pUsuario);
                                       
                    ActivoFijo mActivos = new ActivoFijo();
                    mActivos.consecutivo = pActivosFijos.consecutivo;
                    mActivos = DAActivosFijos.ConsultarLEASING(mActivos, pUsuario);
                    if (mActivos == null || mActivos.bLeasing == false)
                    {                       
                        pActivosFijos = DAActivosFijos.CrearLEASING(pActivosFijos, pUsuario);
                    }
                    else
                        pActivosFijos = DAActivosFijos.ModificarLEASING(pActivosFijos, pUsuario);


                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ModificarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Elimina un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        public void EliminarActivosFijos(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAActivosFijos.EliminarActivoFijo(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "EliminarActivosFijos", ex);
            }
        }

        /// <summary>
        /// Obtiene un ActivosFijos
        /// </summary>
        /// <param name="pId">Identificador de ActivosFijos</param>
        /// <returns>Entidad ActivosFijos</returns>
        public ActivoFijo ConsultarActivosFijos(Int64 pId, Usuario vUsuario)
        {
            try
            {
                ActivoFijo ActivosFijos = new ActivoFijo();
                ActivosFijos = DAActivosFijos.ConsultarActivoFijo(pId, vUsuario);

                ActivosFijos = DAActivosFijos.ConsultarDatosAdicionales(ActivosFijos, vUsuario);

                ActivosFijos = DAActivosFijos.ConsultarPolizaActivo(ActivosFijos, vUsuario);

                ActivosFijos = DAActivosFijos.ConsultarLEASING(ActivosFijos, vUsuario);

                 return ActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ConsultarActivosFijos", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Programas dados unos filtros
        /// </summary>
        /// <param name="pActivosFijos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ActivosFijos obtenidos</returns>
        public List<ActivoFijo> ListarActivosFijos(ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ListarActivoFijo(pActivosFijos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivosFijos", ex);
                return null;
            }
        }

        public List<ActivoFijo> ListarTipoActivoFijo(Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ListarTipoActivoFijo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarTipoActivoFijo", ex);
                return null;
            }
        }
        

        public List<ActivoFijo> ListarActivoFijoDepre(DateTime pFechaDepreciacion, ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ListarActivoFijoDepre(pFechaDepreciacion, pActivosFijos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivoFijoDepre", ex);
                return null;
            }
        }


        public List<ActivoFijo> ListarActivoDeterioroNif(DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ListarActivoDeterioroNif(pFecha , pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivoDeterioroNif", ex);
                return null;
            }
        }


        public List<ActivoFijo> ListarActivoFijoReporteCierre(DateTime pFecha, Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ListarActivoFijoReporteCierre(pFecha, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ListarActivoFijoReporteCierre", ex);
                return null;
            }
        }





        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return DAActivosFijos.ObtenerSiguienteCodigo(pUsuario);
            }
            catch 
            {
                return 1;
            }
        }

        public Boolean DepreciarActivosFijos(DateTime pFechaDepreciacion, List<ActivoFijo> LstActivosFijos, Int64 pcod_proceso, ref Int64 pnum_comp, ref Int64 ptipo_comp, ref string Error, ref Int64 pCodOpe, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 23;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = pFechaDepreciacion;
                    pOperacion.fecha_calc = pFechaDepreciacion;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref Error, pUsuario);
                    if (Error.Trim() == "")
                    {
                        pCodOpe = pOperacion.cod_ope;
                        foreach (ActivoFijo pActFij in LstActivosFijos)
                        {
                            pActFij.fecha_depreciacion = pFechaDepreciacion;
                            if (pActFij.valor_a_depreciar != 0 && pActFij.valor_a_depreciar != null)
                                if (DAActivosFijos.DepreciarActivoFijo(pActFij, pCodOpe, ref Error, pUsuario) == false)
                                    return false;
                        }
                        // Generar el comprobante   
                        string sError = "";
                        Xpinn.Contabilidad.Business.ComprobanteBusiness BOComprobante = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
                        if (BOComprobante.GenerarComprobanteSinCommit(pCodOpe, 23, pFechaDepreciacion, pUsuario.cod_oficina, 0, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, pUsuario) == false)
                            return false;
                    }
                    else
                    {
                        return false;
                    }
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "DepreciarActivosFijos", ex);
                return false;
            }
        }

        public Boolean BajaActivoFijo(ActivoFijo pActivosFijos, ref Int64 pCodOpe, ref String pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 26;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = Convert.ToDateTime(pActivosFijos.fecha_baja);
                    pOperacion.fecha_calc = Convert.ToDateTime(pActivosFijos.fecha_baja);
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref pError, pUsuario);
                    pCodOpe = pOperacion.cod_ope;
                    // Dar de baja el activo fijo
                    DAActivosFijos.BajaActivoFijo(pActivosFijos, pCodOpe, ref pError, pUsuario);

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                BOExcepcion.Throw("ActivosFijosBusiness", "CrearActivosFijos", ex);
                return false;
            }
        }

        public Boolean VentaActivoFijo(ActivoFijo pActivosFijos, ref Int64 pCodOpe, ref String pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 22;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = Convert.ToDateTime(pActivosFijos.fecha_venta);
                    pOperacion.fecha_calc = Convert.ToDateTime(pActivosFijos.fecha_venta);
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion, ref pError, pUsuario);
                    pCodOpe = pOperacion.cod_ope;
                    // Dar de baja el activo fijo
                    DAActivosFijos.VentaActivoFijo(pActivosFijos, pCodOpe, ref pError, pUsuario);

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                BOExcepcion.Throw("ActivosFijosBusiness", "CrearActivosFijos", ex);
                return false;
            }
        }

        public ActivoFijo ModificarMantenimientoNif(ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos = DAActivosFijos.ModificarMantenimientoNif(pActivosFijos, pUsuario);
                     
                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ModificarMantenimientoNif", ex);
                return null;
            }
        }



        public void CrearDeterioroNIF(DateTime pFechaDepre, List<ActivoFijo> plstActivos, ref Int64 pCodOpe, ref String pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    // Crear la operación
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 106;
                    pOperacion.cod_usu = pUsuario.codusuario;
                    pOperacion.cod_ofi = pUsuario.cod_oficina;
                    pOperacion.fecha_oper = pFechaDepre;
                    pOperacion.fecha_calc = pFechaDepre;
                    pOperacion.cod_ope = DATesoreria.CrearOperacion(pOperacion,ref pError, pUsuario);
                    pCodOpe = pOperacion.cod_ope;
                    if (pError.Trim() == "")
                    {
                        foreach (ActivoFijo nActivos in plstActivos)
                        {
                            nActivos.fecha = pFechaDepre;
                            if (nActivos.valor_deterioro != 0 && nActivos.valor_deterioro != null)
                                DAActivosFijos.CrearDeterioroNIF(nActivos, Convert.ToInt32(pCodOpe), pUsuario);
                        }

                        // Generar el comprobante   
                        //string sError = "";
                        //Xpinn.Contabilidad.Business.ComprobanteBusiness BOComprobante = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
                        //if (BOComprobante.GenerarComprobante(pCodOpe, 106, pFechaDepre, pUsuario.cod_oficina, 0, pcod_proceso, ref pnum_comp, ref ptipo_comp, ref sError, pUsuario) == false)
                        //    return false;
                    }                    
                    
                    ts.Complete();
                    
                }
               
                
            }
            catch (Exception ex)
            {
                
                BOExcepcion.Throw("ActivosFijosBusiness", "CrearDeterioroNIF", ex);               
            }
        }




        public Int64 CrearCOMPRA_ACTIVO(ActivoFijo pCOMPRA_ACTIVO, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    Usuario usuap = new Usuario();

                    // CREAR OPERACION
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 21;
                    pOperacion.cod_caja = 0;
                    pOperacion.cod_cajero = 0;
                    pOperacion.observacion = "Operacion-ActivosFijos";
                    pOperacion.cod_proceso = null;
                    pOperacion.fecha_calc = DateTime.Now;
                    pOperacion.cod_ofi = usuap.cod_oficina;
                    pOperacion.cod_ope = 0;
                    pOperacion.fecha_oper = DateTime.Now;
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pusuario);
                    pCOMPRA_ACTIVO.cod_ope = pOperacion.cod_ope;
                    pCOMPRA_ACTIVO = DAActivosFijos.CrearCOMPRA_ACTIVO(pCOMPRA_ACTIVO, pusuario);
                    

                    ts.Complete();

                }

                return pCOMPRA_ACTIVO.cod_ope;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("COMPRA_ACTIVOBusiness", "CrearCOMPRA_ACTIVO", ex);
                return 0;
            }
        }


        public string ValidarDeterioroNiif(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                string pMensaje = string.Empty;
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pMensaje = DAActivosFijos.ValidarDeterioroNiif(pFecha, vUsuario);
                    ts.Complete();
                }
                return pMensaje;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActivosFijosBusiness", "ValidarDeterioroNiif", ex);
                return null;
            }
        }




        public ActivoFijo ConsultarCierreActivosFijos(Usuario vUsuario)
        {
            try
            {
                ActivoFijo ActivoFijo = new ActivoFijo();

                ActivoFijo = DAActivosFijos.ConsultarCierreActivosFijos(vUsuario);

                return ActivoFijo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ConsultarCierreActivosFijos", ex);
                return null;
            }
        }

    }
}