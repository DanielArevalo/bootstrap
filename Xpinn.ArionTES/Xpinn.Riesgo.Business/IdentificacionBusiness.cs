using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;
using System.Transactions;


namespace Xpinn.Riesgo.Business
{
    public class IdentificacionBusiness : GlobalBusiness
    {
        private IdentificacionData DAIdentificacion;

        /// <summary>
        /// Constructor para el acceso a la capa Data
        /// </summary>
        public IdentificacionBusiness()
        {
            DAIdentificacion = new IdentificacionData();
        }

        /// <summary>
        /// Crear un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAIdentificacion.CrearProcesoEntidad(pProceso, vUsuario);
                    ts.Complete();
                    return pProceso;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modificar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProceso = DAIdentificacion.ModificarProcesoEntidad(pProceso, vUsuario);
                    ts.Complete();
                    return pProceso;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con el código del proceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarProcesoEntidad(pProceso, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarProcesoEntidad", ex);
            }
        }

        /// <summary>
        /// Crear registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSubProceso = DAIdentificacion.CrearSubProcesoEntidad(pSubProceso, vUsuario);
                    ts.Complete();
                    return pSubProceso;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearSubProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Modificar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSubProceso = DAIdentificacion.ModificarSubProcesoEntidad(pSubProceso, vUsuario);
                    ts.Complete();
                    return pSubProceso;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarSubProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con el código del subproceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarSubProcesoEntidad(pSubProceso, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarSubProcesoEntidad", ex);
            }
        }

        /// <summary>
        /// Crear registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArea = DAIdentificacion.CrearAreaFuncional(pArea, vUsuario);
                    ts.Complete();
                    return pArea;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearAreaFuncional", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pArea = DAIdentificacion.ModificarAreaFuncional(pArea, vUsuario);
                    ts.Complete();
                    return pArea;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarAreaFuncional", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="Area">Objeto con el código del area</param>
        /// <param name="vUsuario"></param>
        public void EliminarAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarAreaFuncional(pArea, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarAreaFuncional", ex);
            }
        }

        /// <summary>
        /// Crear registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargo = DAIdentificacion.CrearCargo(pCargo, vUsuario);
                    ts.Complete();
                    return pCargo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearCargo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargo = DAIdentificacion.ModificarCargo(pCargo, vUsuario);
                    ts.Complete();
                    return pCargo;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarCargo", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con el código del cargo</param>
        /// <param name="vUsuario"></param>
        public void EliminarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarCargo(pCargo, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarCargo", ex);
            }
        }

        /// <summary>
        /// Crear registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCausa = DAIdentificacion.CrearCausa(pCausa, vUsuario);
                    ts.Complete();
                    return pCausa;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearCausa", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCausa = DAIdentificacion.ModificarCausa(pCausa, vUsuario);
                    ts.Complete();
                    return pCausa;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarCausa", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con el código de la causa</param>
        /// <param name="vUsuario"></param>
        public void EliminarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarCausa(pCausa, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarCausa", ex);
            }
        }

        /// <summary>
        /// Crear registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFactor = DAIdentificacion.CrearFactorRiesgo(pFactor, vUsuario);
                    ts.Complete();
                    return pFactor;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "CrearFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Modifica un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pFactor = DAIdentificacion.ModificarFactorRiesgo(pFactor, vUsuario);
                    ts.Complete();
                    return pFactor;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ModificarFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Eliminar un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIdentificacion.EliminarFactorRiesgo(pFactor, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "EliminarFactorRiesgo", ex);
            }
        }
        
        /// <summary>
        /// Consultar proceso especifico
        /// </summary>
        /// <param name="pProceso">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarProcesoEntidad(Identificacion pProceso, int TipoParametro, Usuario vUsuario)
        {
            try
            {
                pProceso = DAIdentificacion.ConsultarProcesoEntidad(pProceso, TipoParametro, vUsuario);
                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar area especifica
        /// </summary>
        /// <param name="pArea">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarAreasEntidad(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                pArea = DAIdentificacion.ConsultarAreasEntidad(pArea, vUsuario);
                return pArea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarAreasEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar un cargo especifica
        /// </summary>
        /// <param name="pCargo">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarCargosEntidad(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                pCargo = DAIdentificacion.ConsultarCargosEntidad(pCargo, vUsuario);
                return pCargo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarCargosEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar causa especifica
        /// </summary>
        /// <param name="pCausa">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                pCausa = DAIdentificacion.ConsultarCausa(pCausa,  vUsuario);
                return pCausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarCausa", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar factor de riesgo especifico
        /// </summary>
        /// <param name="pFactor">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                pFactor = DAIdentificacion.ConsultarFactorRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Consultar sistema de riesgo especifico
        /// </summary>
        /// <param name="pFactor">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarSistemaRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                pFactor = DAIdentificacion.ConsultarSistemaRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ConsultarSistemaRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar procesos basado en un filtro
        /// </summary>
        /// <param name="pProceso">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarProcesosEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstParametros = new List<Identificacion>();
                lstParametros = DAIdentificacion.ListarProcesosEntidad(pProceso, vUsuario);
                return lstParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarProcesosEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar subprocesos basado en un filtro
        /// </summary>
        /// <param name="pSubproceso">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarSubProcesosEntidad(Identificacion pSubproceso, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstParametros = new List<Identificacion>();
                lstParametros = DAIdentificacion.ListarSubProcesosEntidad(pSubproceso, vUsuario);
                return lstParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarSubProcesosEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar areas basado en un filtro
        /// </summary>
        /// <param name="pAreas">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarAreasEntidad(Identificacion pAreas, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstAreas = new List<Identificacion>();
                lstAreas = DAIdentificacion.ListarAreasEntidad(pAreas, vUsuario);
                return lstAreas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarAreasEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar cargos basado en un filtro
        /// </summary>
        /// <param name="pCargo">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarCargosEntidad(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstCargos = new List<Identificacion>();
                lstCargos = DAIdentificacion.ListarCargosEntidad(pCargo, vUsuario);
                return lstCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarCargosEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar causas basado en un filtro
        /// </summary>
        /// <param name="pCausa">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarCausas(Identificacion pCausa, string filtro, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstCausas = new List<Identificacion>();
                lstCausas = DAIdentificacion.ListarCausas(pCausa, filtro, vUsuario);
                return lstCausas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarCausas", ex);
                return null;
            }
        }

        /// <summary>
        /// Listar factores de riesgo basado en un filtro
        /// </summary>
        /// <param name="pFactor">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarFactoresRiesgo(Identificacion pFactor, string filtro, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstFactores = new List<Identificacion>();
                lstFactores = DAIdentificacion.ListarFactoresRiesgo(pFactor, filtro, vUsuario);
                return lstFactores;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionBusiness", "ListarFactoresRiesgo", ex);
                return null;
            }
        }
    }
}
