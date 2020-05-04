using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Riesgo.Business;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Services
{
    public class IdentificacionServices : GlobalService
    {
        private IdentificacionBusiness BOParametrosEntidad;

        /// <summary>
        /// Constructor para el acceso a la capa de negocios
        /// </summary>
        public IdentificacionServices()
        {
            BOParametrosEntidad = new IdentificacionBusiness();
        }

        public string CodigoProgramaP { get { return "270301"; } }
        public string CodigoProgramaS { get { return "270302"; } }
        public string CodigoProgramaC { get { return "270304"; } }
        public string CodigoProgramaA { get { return "270303"; } }
        public string CodigoProgramaCa { get { return "270305"; } }
        public string CodigoProgramaF { get { return "270306"; } }

        /// <summary>
        /// Servicio para crear un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                pProceso = BOParametrosEntidad.CrearProcesoEntidad(pProceso, vUsuario);
                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con los datos del proceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                pProceso = BOParametrosEntidad.ModificarProcesoEntidad(pProceso, vUsuario);
                return pProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro del proceso de una entidad
        /// </summary>
        /// <param name="pProceso">Objeto con el código del proceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarProcesoEntidad(Identificacion pProceso, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarProcesoEntidad(pProceso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarProcesoEntidad", ex);
            }
        }

        /// <summary>
        /// Servicio para crear registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                pSubProceso = BOParametrosEntidad.CrearSubProcesoEntidad(pSubProceso, vUsuario);
                return pSubProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearSubProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con los datos del subproceso</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                pSubProceso = BOParametrosEntidad.ModificarSubProcesoEntidad(pSubProceso, vUsuario);
                return pSubProceso;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarSubProcesoEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar registro del subproceso de una entidad
        /// </summary>
        /// <param name="pSubProceso">Objeto con el código del subproceso</param>
        /// <param name="vUsuario"></param>
        public void EliminarSubProcesoEntidad(Identificacion pSubProceso, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarSubProcesoEntidad(pSubProceso, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarSubProcesoEntidad", ex);
            }
        }

        /// <summary>
        /// Servicio para crear registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                pArea = BOParametrosEntidad.CrearAreaFuncional(pArea, vUsuario);
                return pArea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearAreaFuncional", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="pArea">Objeto con los datos del area</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                pArea = BOParametrosEntidad.ModificarAreaFuncional(pArea, vUsuario);
                return pArea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarAreaFuncional", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de un area funcional en la entidad
        /// </summary>
        /// <param name="Area">Objeto con el código del area</param>
        /// <param name="vUsuario"></param>
        public void EliminarAreaFuncional(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarAreaFuncional(pArea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarAreaFuncional", ex);
            }
        }

        /// <summary>
        /// Servicio para crear registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                pCargo = BOParametrosEntidad.CrearCargo(pCargo, vUsuario);
                return pCargo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearCargo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con los datos del cargo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                pCargo = BOParametrosEntidad.ModificarCargo(pCargo, vUsuario);
                return pCargo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarCargo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de un cargo del organigrama de la entidad
        /// </summary>
        /// <param name="pCargo">Objeto con el código del cargo</param>
        /// <param name="vUsuario"></param>
        public void EliminarCargo(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarCargo(pCargo, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarCargo", ex);
            }
        }

        /// <summary>
        /// Servicio para crear registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                pCausa = BOParametrosEntidad.CrearCausa(pCausa, vUsuario);
                return pCausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearCausa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                pCausa = BOParametrosEntidad.ModificarCausa(pCausa, vUsuario);
                return pCausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarCausa", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de una causa de riesgo
        /// </summary>
        /// <param name="pCausa">Objeto con el código de la causa</param>
        /// <param name="vUsuario"></param>
        public void EliminarCausa(Identificacion pCausa, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarCausa(pCausa, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarCausa", ex);
            }
        }

        /// <summary>
        /// Servicio para crear registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos del factor de riesgo</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion CrearFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                pFactor = BOParametrosEntidad.CrearFactorRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "CrearFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con los datos de la causa</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ModificarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                pFactor = BOParametrosEntidad.ModificarFactorRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ModificarFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para eliminar un registro de un factor de riesgo
        /// </summary>
        /// <param name="pFactor">Objeto con el código del factor</param>
        /// <param name="vUsuario"></param>
        public void EliminarFactorRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                BOParametrosEntidad.EliminarFactorRiesgo(pFactor, vUsuario);                 
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "EliminarFactorRiesgo", ex);
            }
        }

        /// <summary>
        /// Servicio para consultar parametro especifico
        /// </summary>
        /// <param name="pParametro">Objeto con datos para realizar el filtro</param>
        /// <param name="TipoParametro">Define cual tabla se va a consultar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarProcesoEntidad(Identificacion pCargo, int TipoParametro, Usuario vUsuario)
        {
            try
            {
                pCargo = BOParametrosEntidad.ConsultarProcesoEntidad(pCargo, TipoParametro, vUsuario);
                return pCargo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarParametroEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar area especifica
        /// </summary>
        /// <param name="pArea">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarAreasEntidad(Identificacion pArea, Usuario vUsuario)
        {
            try
            {
                pArea = BOParametrosEntidad.ConsultarAreasEntidad(pArea, vUsuario);
                return pArea;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarAreasEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar un cargo especifica
        /// </summary>
        /// <param name="pCargo">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarCargosEntidad(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                pCargo = BOParametrosEntidad.ConsultarCargosEntidad(pCargo, vUsuario);
                return pCargo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarCargosEntidad", ex);
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
                pCausa = BOParametrosEntidad.ConsultarCausa(pCausa, vUsuario);
                return pCausa;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarCausa", ex);
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
                pFactor = BOParametrosEntidad.ConsultarFactorRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarFactorRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para consultar sistema de riesgo especifico
        /// </summary>
        /// <param name="pFactor">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public Identificacion ConsultarSistemaRiesgo(Identificacion pFactor, Usuario vUsuario)
        {
            try
            {
                pFactor = BOParametrosEntidad.ConsultarSistemaRiesgo(pFactor, vUsuario);
                return pFactor;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarSistemaRiesgo", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para listar parametros basado en un filtro
        /// </summary>
        /// <param name="pParametro">Objeto con datos para realizar el filtro</param>
        /// <param name="TipoParametro">Define cual tabla se va a consultar</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarProcesosEntidad(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstParametros = new List<Identificacion>();
                lstParametros = BOParametrosEntidad.ListarProcesosEntidad(pCargo, vUsuario);
                return lstParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ConsultarParametroEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para listar subprocesos basado en un filtro
        /// </summary>
        /// <param name="pSubproceso">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarSubProcesosEntidad(Identificacion pSubproceso, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstParametros = new List<Identificacion>();
                lstParametros = BOParametrosEntidad.ListarSubProcesosEntidad(pSubproceso, vUsuario);
                return lstParametros;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ListarSubProcesosEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para listar areas basado en un filtro
        /// </summary>
        /// <param name="pAreas">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarAreasEntidad(Identificacion pAreas, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstAreas = new List<Identificacion>();
                lstAreas = BOParametrosEntidad.ListarAreasEntidad(pAreas, vUsuario);
                return lstAreas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ListarAreasEntidad", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para listar cargos basado en un filtro
        /// </summary>
        /// <param name="pCargo">Objeto con datos para realizar el filtro</param>
        /// <param name="vUsuario"></param>
        /// <returns></returns>
        public List<Identificacion> ListarCargosEntidad(Identificacion pCargo, Usuario vUsuario)
        {
            try
            {
                List<Identificacion> lstCargos = new List<Identificacion>();
                lstCargos = BOParametrosEntidad.ListarCargosEntidad(pCargo, vUsuario);
                return lstCargos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ListarCargosEntidad", ex);
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
                lstCausas = BOParametrosEntidad.ListarCausas(pCausa, filtro, vUsuario);
                return lstCausas;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ListarCausas", ex);
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
                lstFactores = BOParametrosEntidad.ListarFactoresRiesgo(pFactor, filtro, vUsuario);
                return lstFactores;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IdentificacionServices", "ListarFactoresRiesgo", ex);
                return null;
            }
        }
    }
}
