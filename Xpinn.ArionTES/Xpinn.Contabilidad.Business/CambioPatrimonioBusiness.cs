using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Contabilidad.Data;
using Xpinn.Contabilidad.Entities;


namespace Xpinn.Contabilidad.Business
{
    public class CambioPatrimonioBusiness : GlobalBusiness
    {
        CambioPatrimonioData DACambioPatrimonioData;

        public CambioPatrimonioBusiness() 
        {
            DACambioPatrimonioData = new CambioPatrimonioData();
        }

        public List<CambioPatrimonio> getListaComboBusiness(Usuario pUsuario)
        {
            try
            {
                return DACambioPatrimonioData.getListaCombo(pUsuario);
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("CambioPatrimonioBusiness", "getListaCombosBusiness",e);
                return null;
            }
        }

        public List<CambioPatrimonio> getListaCambioPatrimonioBusines(Usuario pUsuario,int pOpcion)
        {
            try
            {
                return DACambioPatrimonioData.getListaCambioPatrimonio(pUsuario, pOpcion);
            }
            catch (Exception)
            {
                BOExcepcion.Throw("CambioPatrimonioBusiness", "getListaCambioPatrimonioBusiness", new Exception());
                return null;
            }
        }
    }
}
