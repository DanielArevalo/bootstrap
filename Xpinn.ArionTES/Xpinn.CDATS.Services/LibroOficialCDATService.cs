using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.CDATS.Entities;
using Xpinn.CDATS.Business;


namespace Xpinn.CDATS.Services
{
    public class LibroOficialCDATService
    {
        LibroOficialCDATBusiness BOLibro ;
        ExcepcionBusiness BOException;

        public LibroOficialCDATService()
        {
            BOLibro = new LibroOficialCDATBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaLIB { get { return "220308"; } }

        public List<AdministracionCDAT> ListarCdat(string filtro, DateTime FechaIni, DateTime FechaFin, Usuario vUsuario)
        {
            try
            {
                return BOLibro.ListarCdat(filtro, FechaIni, FechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("LibroOficialCDATService", "ListarCdat", ex);
                return null;
            }
        }     
        

    }
}
