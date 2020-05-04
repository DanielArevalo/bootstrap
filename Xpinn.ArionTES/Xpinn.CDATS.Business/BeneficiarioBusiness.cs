using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;

namespace Xpinn.CDATS.Business
{
    /// <summary>
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class BeneficiarioBusiness : GlobalBusiness
    {
        private BeneficiarioData DABeneficiario;

        
        public BeneficiarioBusiness()
        {
            DABeneficiario = new BeneficiarioData();
        }



        #region BENEFICIARIO CDAT

        public List<Beneficiario> ConsultarBeneficiarioCdat(String pId, Usuario vUsuario)
        {
            try
            {
                return DABeneficiario.ConsultarBeneficiarioCdat(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ConsultarBeneficiarioCdat", ex);
                return null;
            }
        }

        public List<Beneficiario> ListarParentesco(Beneficiario pBeneficiario, Usuario pUsuario)
        {
            try
            {
                return DABeneficiario.ListarParentesco(pBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ListarParentesco", ex);
                return null;
            }
        }

        public void EliminarBeneficiarioCdat(String pFiltro, Int64 pNumero_Cuenta, Usuario vUsuario)
        {
            try
            {
                DABeneficiario.EliminarBeneficiarioCdat(pFiltro,  pNumero_Cuenta,  vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("$Programa$Service", "EliminarBeneficiarioCdat", ex);
            }
        }

        #endregion


    }
}