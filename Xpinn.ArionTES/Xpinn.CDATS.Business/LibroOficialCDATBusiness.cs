using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Data;
using Xpinn.CDATS.Entities;
using System.Transactions;


namespace Xpinn.CDATS.Business
{
    public class LibroOficialCDATBusiness : GlobalBusiness
    {
        LibroOficialCDATData BALibro;

        public LibroOficialCDATBusiness()
        {
            BALibro = new LibroOficialCDATData();
        }



        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaIni, DateTime FechaFin, Usuario vUsuario)
        {
            try
            {
                return BALibro.ListarCdat(filtro, FechaIni, FechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LibroOficialCDATBusiness", "ListarCdat", ex);
                return null;
            }
        }       

    }
}
