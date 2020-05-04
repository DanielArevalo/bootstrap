using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;

namespace Xpinn.Cartera.Business
{
    public class ProvisionBusiness : GlobalData
    {

        private ProvisionData DAProvisionCartera;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ProvisionBusiness()
        {
            DAProvisionCartera = new ProvisionData();
        }


        #region PROVISION DE CREDITOS

        public List<Provision> ListarProvisiones(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAProvisionCartera.ListarProvisiones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionBusiness", "ListarProvisiones", ex);
                return null;
            }
        }

        public void ModificacionProvision(List<Provision> lstProv, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstProv.Count > 0)
                    {
                        foreach (Provision nProvision in lstProv)
                        {
                            Provision pEntidad = new Provision();
                            //MODIFICANDO TABLA PROVISION
                            pEntidad = DAProvisionCartera.ModificarProvision(nProvision, vUsuario);
                            //INSERTANDO TABLA AUD_PROVISION
                            pEntidad = new Provision();
                            pEntidad = DAProvisionCartera.CrearAuditoriaProvision(nProvision, vUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }

        #endregion

        public List<Provision> ListarClasificaciones(string filtro, Usuario pUsuario)
        {
            try
            {
                return DAProvisionCartera.ListarClasificaciones(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProvisionService", "ListarClasificaciones", ex);
                return null;
            }
        }

        public void ModificarClasificacion(List<Provision> lstProv, ref string pError, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (lstProv.Count > 0)
                    {
                        foreach (Provision nProvision in lstProv)
                        {
                            Provision pEntidad = new Provision();
                            //MODIFICANDO TABLA HISTORICO_CRE
                            pEntidad = DAProvisionCartera.ModificarClasificacion(nProvision, vUsuario);
                        }
                    }
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                pError = ex.Message;
            }
        }


    }
}
