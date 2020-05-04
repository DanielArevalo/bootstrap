using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
 
     /// <summary>
    /// Objeto de negocio para Beneficiarios
    /// </summary>
    /// 
    public class BeneficiariosBusiness : GlobalData
    {

        private BeneficiariosData DABeneficiarios;

        /// <summary>
        /// Constructor del objeto de negocio para Beneficiarios
        /// </summary>
        public BeneficiariosBusiness()
        {
            DABeneficiarios = new BeneficiariosData();
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
                return DABeneficiarios.ListarBeneficiarios(pBeneficiarios, pUsuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ListarBeneficiarios", ex);
                return null;
            }
        }
        /// <summary>
        /// Modifica un Beneficiario
        /// /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad modificada</returns>
        public Beneficiarios ModificarBeneficiarios(Beneficiarios pBeneficiarios, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {
                    pBeneficiarios = DABeneficiarios.ModificarBeneficiarios(pBeneficiarios, pUsuario);

                  //  ts.Complete();
              //  }

                return pBeneficiarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ModificarBeneficiarios", ex);
                return null;
            }

        }

        /// <summary>
        /// Elimina un Beneficiario
        /// </summary>
        /// <param name="pId">identificador de  Beneficiarios</param>
        public void EliminarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{

                    DABeneficiarios.EliminarBeneficiarios(pId, pUsuario);

                 //   ts.Complete();
               // }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "EliminarBeneficiarios", ex);
            }
        }

        /// <summary>
        /// Obtiene un Beneficiario
        /// </summary>
        /// <param name="pId">identificador del Beneficiarios</param>
        /// <returns>Beneficiarios consultada</returns>
        public Beneficiarios ConsultarBeneficiarios(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return DABeneficiarios.ConsultarBeneficiarios(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ConsultarBeneficiarios", ex);
                return null;
            }
        }


        public List<Beneficiarios> ConsultarBeneficiariosAUXILIOS(Int64 pId, Usuario pUsuario)
        {
            try
            {
                CreditoData DACredito = new CreditoData();
                List<Beneficiarios> lstReturn = new List<Beneficiarios>();
                lstReturn = DABeneficiarios.ConsultarBeneficiariosAUXILIOS(pId, pUsuario);
                if (lstReturn == null || lstReturn.Count == 0)
                    lstReturn = DACredito.ConsultarBeneficiariosCredito(pId,pUsuario);

                return lstReturn;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "ConsultarBeneficiarios", ex);
                return null;
            }
        }
        
        /// <summary>
        /// Crea un Beneficiarios
        /// </summary>
        /// <param name="pEntity">Entidad Beneficiarios</param>
        /// <returns>Entidad creada</returns>
        public Beneficiarios CrearBeneficiarios(Beneficiarios pBeneficiarios, Usuario pUsuario)
        {
            try
            {
               // using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
               // {
                    pBeneficiarios = DABeneficiarios.InsertarBeneficiarios(pBeneficiarios, pUsuario);

                //    ts.Complete();
                //}

                return pBeneficiarios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiariosBusiness", "CrearBeneficiarios", ex);
                return null;
            }
        }

    }
}
       