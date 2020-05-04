using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
namespace Xpinn.CDATS.Business
{
    public class RepCierreCDATBusiness : GlobalBusiness
    {

        private RepCierreCDATData DALCdat;

        public RepCierreCDATBusiness()
        {
            DALCdat = new RepCierreCDATData();
        }


        public List<AdministracionCDAT> ListarCdatReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DALCdat.ListarCDATReporteCierre(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RepCierreCDATBusiness", "ListarCdatReporteCierre", ex);
                return null;
            }
        }
    }
}