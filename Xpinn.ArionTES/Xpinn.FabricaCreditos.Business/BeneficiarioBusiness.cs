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
    /// Objeto de negocio para Beneficiario
    /// </summary>
    public class BeneficiarioBusiness : GlobalBusiness
    {
        private BeneficiarioData DABeneficiario;

        
        public BeneficiarioBusiness()
        {
            DABeneficiario = new BeneficiarioData();
        }

        
        public bool GrabarDatosTabBeneficiario(ref string pError, ref List<Beneficiario> lstBeneficiario, ref List<Xpinn.Aportes.Entities.PersonaParentescos> lstPersonaParent, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstBeneficiario != null)
                    {
                        if (lstBeneficiario.Count > 0)
                        {
                            Beneficiario nBeneficiario = null;
                            foreach (Beneficiario eBenef in lstBeneficiario)
                            {
                                nBeneficiario = new Beneficiario();
                                if (eBenef.idbeneficiario <= 0)
                                {
                                    nBeneficiario = DABeneficiario.CrearBeneficiario(eBenef, pUsuario);
                                    eBenef.idbeneficiario = nBeneficiario.idbeneficiario;
                                }
                                else
                                    nBeneficiario = DABeneficiario.ModificarBeneficiario(eBenef, pUsuario);
                            }
                        }
                    }
                    if (lstPersonaParent != null)
                    {
                        if (lstPersonaParent.Count > 0)
                        {
                            Xpinn.Aportes.Data.AfiliacionData DAAfiliacionParent = new Aportes.Data.AfiliacionData();
                            Xpinn.Aportes.Entities.PersonaParentescos pParentesco = null;
                            foreach (Xpinn.Aportes.Entities.PersonaParentescos parentesco in lstPersonaParent)
                            {
                                pParentesco = new Aportes.Entities.PersonaParentescos();
                                if (parentesco.consecutivo > 0)
                                    pParentesco = DAAfiliacionParent.ModificarPersonaParentesco(parentesco, pUsuario);
                                else
                                {
                                    pParentesco = DAAfiliacionParent.CrearPersonaParentesco(parentesco, pUsuario);
                                    parentesco.consecutivo = pParentesco.consecutivo;
                                }
                            }
                        }
                    }
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                pError = ex.Message;
                return false;
            }
        }



        public void EliminarBeneficiario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiario(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiario", ex);
            }
        }

        
        public List<Beneficiario> ConsultarBeneficiario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return DABeneficiario.ConsultarBeneficiario(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }

        

        public List<Beneficiario> ListarBeneficiario(Beneficiario pBeneficiario, Usuario pUsuario)
        {
            try
            {
                return DABeneficiario.ListarBeneficiario(pBeneficiario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "ListarBeneficiario", ex);
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

        #region BENEFICIARIO POR AHORRO VISTA

        public List<Beneficiario> ConsultarBeneficiarioAhorroVista(String pId, Usuario vUsuario)
        {
            try
            {
                return DABeneficiario.ConsultarBeneficiarioAhorroVista(pId, vUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActividadPersonaBusiness", "ConsultarActividad", ex);
                return null;
            }
        }

        #endregion

        #region BENEFICIARIO POR AHORRO PROGRAMAD

        public void EliminarBeneficiarioAhorroProgramado(long pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiarioAhorroProgramado(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiario", ex);
            }
        }

        #endregion

        #region BENEFICIARIO DE APORTE

        public void EliminarBeneficiarioAporte(long pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DABeneficiario.EliminarBeneficiarioAporte(pId, pUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("BeneficiarioBusiness", "EliminarBeneficiarioAporte", ex);
            }
        }

        #endregion 

    }
}