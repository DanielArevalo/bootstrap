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
    public class BeneficiarioService
    {
        private BeneficiarioBusiness BOBeneficiario;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Beneficiario
        /// </summary>
        public BeneficiarioService()
        {
            BOBeneficiario = new BeneficiarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "30704"; } }

        /// <summary>
        /// Servicio para crear Beneficiario
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiario</param>
        /// <returns>Entidad Beneficiario creada</returns>
       

        public bool GrabarDatosTabBeneficiario(ref string pError, ref List<Beneficiario> lstBeneficiario, ref List<Xpinn.Aportes.Entities.PersonaParentescos> lstPersonaParent, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.GrabarDatosTabBeneficiario(ref pError, ref lstBeneficiario, ref lstPersonaParent, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioService", "CrearBeneficiario", ex);
                return false;
            }
        }

        /// <summary>
        /// Servicio para Eliminar Beneficiario
        /// </summary>
        /// <param name="pId">identificador de Beneficiario</param>
        public void EliminarBeneficiario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOBeneficiario.EliminarBeneficiario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBeneficiario", ex);
            }
        }

       
        public List<Beneficiario> ConsultarBeneficiario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.ConsultarBeneficiario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioService", "ConsultarBeneficiario", ex);
                return null;
            }
        }

        /// <summary>
        /// Servicio para obtener lista de Beneficiarios a partir de unos filtros
        /// </summary>
        /// <param name="pBeneficiario">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Beneficiario obtenidos</returns>
        public List<Beneficiario> ListarBeneficiario(Beneficiario vBeneficiario, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.ListarBeneficiario(vBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioService", "ListarBeneficiario", ex);
                return null;
            }
        }


        public List<Beneficiario> ListarParentesco(Beneficiario pBeneficiario, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.ListarParentesco(pBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ListarParentesco", ex);
                return null;
            }
        }

        #region BENEFICIARIO DE AHORRO VISTA

        public List<Beneficiario> ConsultarBeneficiarioAhorroVista(String pId, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.ConsultarBeneficiarioAhorroVista(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioService", "ConsultarBeneficiarioAhorroVista", ex);
                return null;
            }
        }

        #endregion

        #region BENEFICIARIO DE AHORRO PROGRAMADO

        public void EliminarBeneficiarioAhorroProgramado(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOBeneficiario.EliminarBeneficiarioAhorroProgramado(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBeneficiario", ex);
            }
        }

        #endregion

        #region BENEFICIADO DE APORTE
        public void EliminarBeneficiarioAporte(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOBeneficiario.EliminarBeneficiarioAporte(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBeneficiarioAporte", ex);
            }
        }

        #endregion


    }
}