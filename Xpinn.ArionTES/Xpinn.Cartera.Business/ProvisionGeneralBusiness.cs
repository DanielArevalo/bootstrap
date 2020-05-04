using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
 
namespace Xpinn.Cartera.Business
{
    public class ProvisionGeneralBusiness : GlobalBusiness
    {
 
        private ProvisionGeneralData DAProvisionGeneral;
 
        public ProvisionGeneralBusiness()
        {
            DAProvisionGeneral = new ProvisionGeneralData();
        }
 
        public ProvisionGeneral CrearProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProvisionGeneral = DAProvisionGeneral.CrearProvisionGeneral(pProvisionGeneral, pusuario);
 
                    ts.Complete();
 
                }
 
                return pProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "CrearProvisionGeneral", ex);
                return null;
            }
        }
 
 
        public ProvisionGeneral ModificarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProvisionGeneral = DAProvisionGeneral.ModificarProvisionGeneral(pProvisionGeneral, pusuario);
 
                    ts.Complete();
 
                }
 
                return pProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "ModificarProvisionGeneral", ex);
                return null;
            }
        }
 
 
        public void EliminarProvisionGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProvisionGeneral.EliminarProvisionGeneral(pId, pusuario);
 
                    ts.Complete();
 
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "EliminarProvisionGeneral", ex);
            }
        }
 
 
        public ProvisionGeneral ConsultarProvisionGeneral(Int64 pId, Usuario pusuario)
        {
            try
            {
                ProvisionGeneral ProvisionGeneral = new ProvisionGeneral();
                ProvisionGeneral = DAProvisionGeneral.ConsultarProvisionGeneral(pId, pusuario);
                return ProvisionGeneral;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "ConsultarProvisionGeneral", ex);
                return null;
            }
        }
 
 
        public List<ProvisionGeneral> ListarProvisionGeneral(ProvisionGeneral pProvisionGeneral, Usuario pusuario)
        {
            try
            {
                return DAProvisionGeneral.ListarProvisionGeneral(pProvisionGeneral, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "ListarProvisionGeneral", ex);
                return null;
            }
        }

        public List<ProvisionGeneral> ProvisionGeneral(DateTime pFechaCorte, Usuario pUsuario)
        {
            try
            {
                return DAProvisionGeneral.ProvisionGeneral(pFechaCorte, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "ProvisionGeneral", ex);
                return null;
            }
        }

        public bool GuardarProvisionGeneral(DateTime pfecha, List<ProvisionGeneral> pLstProvisionGeneral, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // Guardar cada registro de la provisión
                    foreach (ProvisionGeneral eRegistro in pLstProvisionGeneral)
                    {
                        eRegistro.forma_pago = 1;
                        eRegistro.valor_total = eRegistro.valor_sinlibranza;
                        eRegistro.provision_acumulada = eRegistro.total_provision_sinlibranza;
                        eRegistro.valor_provision = eRegistro.provision_sinlibranza;
                        if (DAProvisionGeneral.CrearProvisionGeneral(eRegistro, pusuario) == null)
                            return false;
                        eRegistro.forma_pago = 2;
                        eRegistro.valor_total = eRegistro.valor_conlibranza;
                        eRegistro.provision_acumulada = eRegistro.total_provision_conlibranza;
                        eRegistro.valor_provision = eRegistro.provision_conlibranza;
                        if (DAProvisionGeneral.CrearProvisionGeneral(eRegistro, pusuario) == null)
                            return false;
                    }
                    // Crear el registro de control.
                    Xpinn.Comun.Data.CiereaData DACierea = new Comun.Data.CiereaData();
                    Xpinn.Comun.Entities.Cierea cieRea = new Comun.Entities.Cierea();
                    cieRea.fecha = pfecha;
                    cieRea.tipo = "J";
                    cieRea.estado = "D";
                    cieRea.codusuario = pusuario.codusuario;
                    cieRea.fecrea = DateTime.Now;
                    cieRea.campo1 = " ";
                    cieRea.campo2 = " ";
                    DACierea.CrearCierea(cieRea, pusuario);

                    ts.Complete();

                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionGeneralBusiness", "CrearProvisionGeneral", ex);                
            }
            return false;
        }
 



    }
    
}
