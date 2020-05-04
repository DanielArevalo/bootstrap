using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Reporteador.Entities;
using Xpinn.Util;
using Xpinn.Reporteador.Data;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Reporteador.Business
{

    public class AFIANCOLBusiness : GlobalData
    {
        private AFIANCOLData DAReporte = new AFIANCOLData();
        private Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();

        public bool LlenarTablaAfiancol(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return DAReporte.LlenarTablaAfiancol(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "LlenarTablaAfiancol", ex);
                return false;
            }
        }

        public List<FechaCorte> ListarFecha(Usuario vUsuario)
        {
            try
            {
                return DAReporte.ListarFecha(vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "ListarFecha", ex);
                return null;
            }
        }

        public List<AFIANCOL_Reporte> ListarReporte(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return DAReporte.ListarReporte(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "ListarReporte", ex);
                return null;
            }
        }

        public Operacion CausarAfiancol(Operacion pOperacion, DateTime FechaCorte, ref Int64 pnum_comp, ref int ptipo_comp, ref string perror, Usuario vUsuario)
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
            {
                try
                {
                    Xpinn.Tesoreria.Entities.Operacion vOpe = new Xpinn.Tesoreria.Entities.Operacion();
                    vOpe = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    if (vOpe == null)
                    {
                        ts.Dispose();
                        return null;
                    }
                    if (!DAReporte.CausarAfiancol(vUsuario, FechaCorte, vOpe, ref pnum_comp, ref ptipo_comp, ref perror))
                    {
                        ts.Dispose();
                        return null;
                    }

                    ts.Complete();
                    return vOpe;
                }
                catch (Exception e)
                {
                    BOExcepcion.Throw("AFIANCOLBusiness", "CausarAfiancol", e);
                    return null;
                }
            }
        }

        public AFIANCOL_Reporte ValidarList(Usuario vUsuario, DateTime FechaCorte)
        {
            try
            {
                return DAReporte.ValidarList(vUsuario, FechaCorte);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AFIANCOLBusiness", "ListarReporte", ex);
                return null;
            }
        }
    }
}
