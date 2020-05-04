using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Programado.Data;
using Xpinn.Programado.Entities;


namespace Xpinn.Programado.Business
{
    public class CierreCuentasBusiness : GlobalBusiness
    {
        private CierreCuentaData DALCuentas;

         public CierreCuentasBusiness()
        {
            DALCuentas = new CierreCuentaData();
        }


        public List<CuentasProgramado> ListarProgramadoReporteCierre(DateTime pFecha, Usuario vUsuario)
        {
            try
            {
                return DALCuentas.ListarProgramadoReporteCierre(pFecha, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CierreCuentasBusiness", "ListarProgramadoReporteCierre", ex);
                return null;
            }
        }

    }

    
}
