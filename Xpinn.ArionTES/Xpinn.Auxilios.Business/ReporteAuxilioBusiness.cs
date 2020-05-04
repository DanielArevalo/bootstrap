using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Auxilios.Data;
using Xpinn.Auxilios.Entities;
using Xpinn.Auxilios.Entities;
using System.Web;
using System.IO;

namespace Xpinn.Auxilios.Business
{
    public class ReporteAuxilioBusiness : GlobalBusiness
    {

        private ReporteAuxilioData DAReporteAuxilio;

        public ReporteAuxilioBusiness()
        {
            DAReporteAuxilio = new ReporteAuxilioData();
        }

        public ReporteAuxilio CrearAuxilio(ReporteAuxilio pReporteAuxilio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReporteAuxilio = DAReporteAuxilio.CrearAuxilio(pReporteAuxilio, pusuario);

                    ts.Complete();

                }

                return pReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "CrearReporteAuxilio", ex);
                return null;
            }
        }


        public ReporteAuxilio ModificarAuxilio(ReporteAuxilio pReporteAuxilio, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pReporteAuxilio = DAReporteAuxilio.ModificarAuxilio(pReporteAuxilio, pusuario);

                    ts.Complete();

                }

                return pReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "ModificarReporteAuxilio", ex);
                return null;
            }
        }


        public void EliminarAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporteAuxilio.EliminarAuxilio(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "EliminarReporteAuxilio", ex);
            }
        }


        public ReporteAuxilio ConsultarAuxilio(Int64 pId, Usuario pusuario)
        {
            try
            {
                ReporteAuxilio ReporteAuxilio = new ReporteAuxilio();
                ReporteAuxilio = DAReporteAuxilio.ConsultarAuxilio(pId, pusuario);
                return ReporteAuxilio;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "ConsultarReporteAuxilio", ex);
                return null;
            }
        }


        public List<ReporteAuxilio> ListarAuxilio(String filtro,DateTime pFechaIni, DateTime pFechaFin, Usuario pusuario)
        {
            try
            {
                return DAReporteAuxilio.ListarAuxilio(filtro,pFechaIni,pFechaFin, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "ListarReporteAuxilio", ex);
                return null;
            }
        }

        public List<ReporteAuxilio> ListarAuxilioPorAnular(String filtro, Usuario vUsuario)
        {
            try
            {
                return DAReporteAuxilio.ListarAuxilioPorAnular(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "ListarAuxilioPorAnular", ex);
                return null;
            }
        }


        public void GenerarAnulacionAuxilio(ReporteAuxilio pData, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAReporteAuxilio.GenerarAnulacionAuxilio(pData, vUsuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ReporteAuxilioBusiness", "GenerarAnulacionAuxilio", ex);
            }
        }

    }
}
