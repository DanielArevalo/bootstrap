using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
    public class RangoPerfilBusiness : GlobalBusiness
    {
        RangoPerfilData DARangoPerfill;

        public RangoPerfilBusiness()
        {
            DARangoPerfill = new RangoPerfilData();
        }
        public List<RangoPerfil> ListarRangosPerfil(RangoPerfil pRangosPerfil, Usuario usuario)
        {
            try
            {
                return DARangoPerfill.ListarRangosPerfil(pRangosPerfil, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangoperfilBusiness", "ListarRangosPerfil", ex);
                return null;
            }
        }
        public RangoPerfil ModificarRangoPerfil(RangoPerfil vRangoPerfil, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    vRangoPerfil = DARangoPerfill.ModificarRangoPerfil(vRangoPerfil, usuario);
                    ts.Complete();
                }

                return vRangoPerfil;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RangoperfilBusiness", "ModificarRangoPerfil", ex);
                return null;
            }
        }
    }
}
