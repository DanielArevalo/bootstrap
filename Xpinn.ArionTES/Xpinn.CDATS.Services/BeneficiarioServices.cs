using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.CDATS.Business;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Services
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

      

        #region BENEFICIARIO cdat
        public List<Beneficiario> ConsultarBeneficiarioCdat(String pId, Usuario pUsuario)
        {
            try
            {
                return BOBeneficiario.ConsultarBeneficiarioCdat(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioService", "ConsultarBeneficiarioCdat", ex);
                return null;
            }
        }
        public void EliminarBeneficiarioCdat(String pFiltro, Int64 pNumero_Cuenta, Usuario vUsuario)
        {
            try
            {
                BOBeneficiario.EliminarBeneficiarioCdat(pFiltro, pNumero_Cuenta, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBeneficiarioCdat", ex);
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

        #endregion
    }
}