using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Integracion.Business;
using Xpinn.Integracion.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.Integracion.Services
{
    public class ImoneyService
    {

        private ImoneyBusiness BOAuth;
        private ExcepcionBusiness BOExcepcion;

        public ImoneyService()
        {
            BOAuth = new ImoneyBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }              

        public Auth getAuthorization(Usuario pUsuario)
        {
            try
            {
                return BOAuth.getAuthorization(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImoneyService", "getAuthorization", ex);
                return null;
            }
        }

        public List<Xpinn.Integracion.Entities.Operators> getOperators(Usuario usuario)
        {
            try
            {
                return BOAuth.getOperators(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImoneyService", "getOperators", ex);
                return null;
            }
        }

        public List<Package> getPackages(string id_operator, Usuario usuario)
        {
            try
            {
                return BOAuth.getPackages(id_operator, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImoneyService", "getPackages", ex);
                return null;
            }
        }

        public Fullmovil createFullMovilTransaction(Fullmovil transact, Usuario usuario)
        {
            try
            {
                return BOAuth.createFullMovilTransaction(transact, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImoneyService", "createFullMovilTransaction", ex);
                return null;
            }
        }

        public List<Fullmovil> getFulltransactionList(string cod_persona, Usuario usuario)
        {
            try
            {
                return BOAuth.getFulltransactionList(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ImoneyService", "getFulltransactionList", ex);
                return null;
            }
        }
    }
}