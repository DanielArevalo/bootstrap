using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Transactions;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
using Xpinn.Util;

namespace Xpinn.Nomina.Business
{
    public class SeguridadSocialBusiness : GlobalBusiness
    {
        private SeguridadSocialData DASeguridadSocial;

        public SeguridadSocialBusiness()
        {
            DASeguridadSocial = new SeguridadSocialData();
        }
        public SeguridadSocial CrearSeguridadSocial(SeguridadSocial pSeguridadSocial,Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSeguridadSocial = DASeguridadSocial.CrearSeguridadSocial(pSeguridadSocial, vUsuario);



                    ts.Complete();
                }

                return pSeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocial", "CrearSeguridadSocial", ex);
                return null;
            }
        }
        public SeguridadSocial ConsultarSeguridadSocial(Usuario vUsuario)
        {
            try
            {
                SeguridadSocial SeguridadSocial = DASeguridadSocial.ConsultarSeguridadSocial(vUsuario);
                return SeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocial", "ConsultarSeguridadSocial", ex);
                return null;
            }
        }
        public SeguridadSocial ModificarSeguridadSocial(SeguridadSocial pSeguridadSocial, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pSeguridadSocial = DASeguridadSocial.ModificarSeguridadSocial(pSeguridadSocial, vUsuario);



                    ts.Complete();
                }

                return pSeguridadSocial;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SeguridadSocial", "CrearSeguridadSocial", ex);
                return null;
            }
        }
    }
}
