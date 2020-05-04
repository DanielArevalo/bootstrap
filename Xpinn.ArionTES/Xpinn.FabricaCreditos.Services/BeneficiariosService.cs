using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.ServiceModel;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Beneficiarios
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class BeneficiariosService
    {
        private BeneficiariosBusiness BOBeneficiarios;
        private ExcepcionBusiness BOExcepcion;
        public int consecutivo;
        /// <summary>
        /// Constructor del servicio para PolizasSeguros
        /// </summary>
        public BeneficiariosService()
        {
            BOBeneficiarios = new BeneficiariosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }
        /// <summary>
        /// Obtiene la lista de Beneficiarios dados unos filtros
        /// </summary>
        /// <param name="pEntidad">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Beneficiarios obtenidos</returns>
        public List<Beneficiarios> ListarBeneficiarios(Beneficiarios pBeneficiarios, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiarios.ListarBeneficiarios(pBeneficiarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosService", "ListarBeneficiarios", ex);
                return null;
            }
        }

        /// <summary>
        /// Crea un Beneficiario
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad creada</returns>
        public Beneficiarios CrearBeneficiarios(Beneficiarios pBeneficiarios, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiarios.CrearBeneficiarios(pBeneficiarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosService", "CrearBeneficiarios", ex);
                return null;
            }
        }


        /// <summary>
        /// Modifica  un Beneficiario
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad modificada</returns>
        public Beneficiarios ModificarBeneficiarios(Beneficiarios pBeneficiarios, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiarios.ModificarBeneficiarios(pBeneficiarios, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosService", "ModificarBeneficiarios", ex);
                return null;
            }
        }


        /// <summary>
        /// Elimina un Beneficiario
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad eliminada</returns>
        public void EliminarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOBeneficiarios.EliminarBeneficiarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosService", "EliminarBeneficiarios", ex);

            }
        }

        public List<Beneficiarios> ConsultarBeneficiariosAUXILIOS(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiarios.ConsultarBeneficiariosAUXILIOS(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ConsultarBeneficiarios", ex);
                return null;
            }
        }

        public Beneficiarios ConsultarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiarios.ConsultarBeneficiarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ConsultarBeneficiarios", ex);
                return null;
            }
        }
    }
}