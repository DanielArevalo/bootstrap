using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Reporteador.Business;
using Xpinn.Reporteador.Entities;
using Xpinn.Tesoreria.Entities;
using Xpinn.Util;

namespace Xpinn.Reporteador.Services
{
    public class AFIANCOLService
    {
        private AFIANCOLBusiness BOReporte = new AFIANCOLBusiness();
        private ExcepcionBusiness BOExcepcion = new ExcepcionBusiness();
        public string CodigoProgramaReporteAfiancol { get { return "200115"; } }

        public List<FechaCorte> ListarFecha(Usuario pUsuario)
        {
            try
            {
                return BOReporte.ListarFecha(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLService", "ListarFecha", ex);
                return null;
            }
        }

        public bool LlenarTablaAfiancol(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return BOReporte.LlenarTablaAfiancol(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "LlenarTablaAfiancol", ex);
                return false;
            }
        }

        public List<AFIANCOL_Reporte> ListarReporte(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return BOReporte.ListarReporte(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLService", "ListarReporte", ex);
                return null;
            }
        }

        public Operacion CausarAfiancol(Operacion pOperacion, DateTime FechaCorte, ref Int64 pnum_comp, ref int ptipo_comp, ref string perror, Usuario vUsuario)
        {
            try
            {
                Operacion opr = BOReporte.CausarAfiancol(pOperacion, FechaCorte, ref pnum_comp, ref ptipo_comp, ref perror, vUsuario);
                return opr;
            }
            catch (Exception e)
            {
                BOExcepcion.Throw("AFIANCOLService", "CausarAfiancol", e);
                return null;
            }
        }

        public AFIANCOL_Reporte ValidarList(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return BOReporte.ValidarList(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "ListarReporte", ex);
                return null;
            }
        }

    }
}
