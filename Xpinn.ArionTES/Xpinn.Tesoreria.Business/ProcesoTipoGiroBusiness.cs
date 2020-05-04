using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{

    public class ProcesoTipoGiroBusiness : GlobalBusiness
    {

        private ProcesoTipoGiroData DAProcesoTipoGiro;

        public ProcesoTipoGiroBusiness()
        {
            DAProcesoTipoGiro = new ProcesoTipoGiroData();
        }

        public ProcesoTipoGiro CrearProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesoTipoGiro = DAProcesoTipoGiro.CrearProcesoTipoGiro(pProcesoTipoGiro, pusuario);

                    ts.Complete();

                }

                return pProcesoTipoGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroBusiness", "CrearProcesoTipoGiro", ex);
                return null;
            }
        }


        public ProcesoTipoGiro ModificarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pProcesoTipoGiro = DAProcesoTipoGiro.ModificarProcesoTipoGiro(pProcesoTipoGiro, pusuario);

                    ts.Complete();

                }

                return pProcesoTipoGiro;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroBusiness", "ModificarProcesoTipoGiro", ex);
                return null;
            }
        }


        public void EliminarProcesoTipoGiro(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAProcesoTipoGiro.EliminarProcesoTipoGiro(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroBusiness", "EliminarProcesoTipoGiro", ex);
            }
        }


        public ProcesoTipoGiro ConsultarProcesoTipoGiro(Int64 pId, Usuario pusuario)
        {
            try
            {
                return DAProcesoTipoGiro.ConsultarProcesoTipoGiro(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroBusiness", "ConsultarProcesoTipoGiro", ex);
                return null;
            }
        }


        public List<ProcesoTipoGiro> ListarProcesoTipoGiro(ProcesoTipoGiro pProcesoTipoGiro, Usuario pusuario)
        {
            try
            {
                return DAProcesoTipoGiro.ListarProcesoTipoGiro(pProcesoTipoGiro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ProcesoTipoGiroBusiness", "ListarProcesoTipoGiro", ex);
                return null;
            }
        }


    }
}