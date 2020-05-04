using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ViabilidadFinancieraService
    {
        private ViabilidadFinancieraBusiness BOViabilidadFinanciera;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para ViabilidadFinanciera
        /// </summary>
        public ViabilidadFinancieraService()
        {
            BOViabilidadFinanciera = new ViabilidadFinancieraBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100102"; } } //100105

        /// <summary>
        /// Servicio para crear ViabilidadFinanciera
        /// </summary>
        /// <param name="pEntity">Entidad ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera creada</returns>
        public ViabilidadFinanciera CrearViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.CrearViabilidadFinanciera(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "CrearViabilidadFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para modificar ViabilidadFinanciera
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera modificada</returns>
        public ViabilidadFinanciera ModificarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.ModificarViabilidadFinanciera(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "ModificarViabilidadFinanciera", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para Eliminar ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">identificador de ViabilidadFinanciera</param>
        public void EliminarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOViabilidadFinanciera.EliminarViabilidadFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarViabilidadFinanciera", ex);
            }
        }

        /// <summary>
        /// Servicio para obtener ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">identificador de ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera</returns>
        public ViabilidadFinanciera ConsultarViabilidadFinanciera(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.ConsultarViabilidadFinanciera(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "ConsultarViabilidadFinanciera", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener ViabilidadFinanciera
        /// </summary>
        /// <param name="pId">identificador de ViabilidadFinanciera</param>
        /// <returns>Entidad ViabilidadFinanciera</returns>
        public ViabilidadFinanciera ConsultarViabilidadFin_Control(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.ConsultarViabilidadFin_Control(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "ConsultarViabilidadFin_Control", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ViabilidadFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinanciera(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.ListarViabilidadFinanciera(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "ListarViabilidadFinanciera", ex);
                return null;
            }
        }
        /// <summary>
        /// Servicio para obtener lista de ViabilidadFinancieras a partir de unos filtros
        /// </summary>
        /// <param name="pViabilidadFinanciera">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de ViabilidadFinanciera obtenidos</returns>
        public List<ViabilidadFinanciera> ListarViabilidadFinancieraRepo(ViabilidadFinanciera pViabilidadFinanciera, Usuario pUsuario)
        {
            try
            {
                return BOViabilidadFinanciera.ListarViabilidadFinancieraRepo(pViabilidadFinanciera, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ViabilidadFinancieraService", "ListarViabilidadFinancieraRepo", ex);
                return null;
            }
        }




        


    }
}