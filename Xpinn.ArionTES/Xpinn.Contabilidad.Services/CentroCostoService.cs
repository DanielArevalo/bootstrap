using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Contabilidad.Business;
using Xpinn.Contabilidad.Entities;

namespace Xpinn.Contabilidad.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CentroCostoService
    {
        private CentroCostoBusiness BOCentroCosto;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para PlanCuentas
        /// </summary>
        public CentroCostoService()
        {
            BOCentroCosto = new CentroCostoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30402"; } }

        /// <summary>
        /// Servicio para crear CentroCosto
        /// </summary>
        /// <param name="pEntity">Entidad CentroCosto</param>
        /// <returns>Entidad CentroCosto creada</returns>
        public CentroCosto CrearCentroCosto(CentroCosto vCentroCosto, Usuario pUsuario)
        {
            try
            {
                return BOCentroCosto.CrearCentroCosto(vCentroCosto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoService", "CrearCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar CentroCosto
        /// </summary>
        /// <param name="pCentroCosto">Entidad CentroCosto</param>
        /// <returns>Entidad CentroCosto modificada</returns>
        public CentroCosto ModificarCentroCosto(CentroCosto vCentroCosto, Usuario pUsuario)
        {
            try
            {
                return BOCentroCosto.ModificarCentroCosto(vCentroCosto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoService", "ModificarCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar CentroCosto
        /// </summary>
        /// <param name="pId">identificador de CentroCosto</param>
        public void EliminarCentroCosto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOCentroCosto.EliminarCentroCosto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarCentroCosto", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener CentroCosto
        /// </summary>
        /// <param name="pId">identificador de CentroCosto</param>
        /// <returns>Entidad CentroCosto</returns>
        public CentroCosto ConsultarCentroCosto(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOCentroCosto.ConsultarCentroCosto(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoService", "ConsultarCentroCosto", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de CentroCostos a partir de unos filtros
        /// </summary>
        /// <param name="pCentroCosto">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de CentroCosto obtenidos</returns>
        public List<CentroCosto> ListarCentroCosto(CentroCosto vCentroCosto, Usuario pUsuario)
        {
            try
            {
                return BOCentroCosto.ListarCentroCosto(vCentroCosto, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CentroCostoService", "ListarCentroCosto", ex);
                return null;
            }
        }


        /// <summary>
        /// Método para listar centros de costo
        /// </summary>
        /// <param name="vUsuario"></param>
        /// <param name="sFiltro"></param>
        /// <returns></returns>
        public List<CentroCosto> ListarCentroCosto(Usuario vUsuario, string sFiltro)
        {
            return BOCentroCosto.ListarCentroCosto(vUsuario, sFiltro);
        }

        /// <summary>
        /// Método que trae el centro de costo inicial y el final
        /// </summary>
        /// <param name="CenIni"></param>
        /// <param name="CenFin"></param>
        public void RangoCentroCosto(ref Int64 CenIni, ref Int64 CenFin, Usuario pUsuario)
        {
            BOCentroCosto.RangoCentroCosto(ref CenIni, ref CenFin, pUsuario);
        }

    }
}