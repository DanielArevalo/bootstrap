using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{

    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ConciliacionBancariaServices
    {
        private ConciliacionBancariaBusiness BOConcilia;
        private ExcepcionBusiness BOExcepcion;


        public ConciliacionBancariaServices()
        {
            BOConcilia = new ConciliacionBancariaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40803"; } }


        public ConciliacionBancaria CrearConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.CrearConciliacion(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "CrearConciliacion", ex);
                return null;
            }
        }


        public ConciliacionBancaria ModificarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ModificarConciliacion(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ModificarConciliacion", ex);
                return null;
            }
        }



        public void EliminarConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOConcilia.EliminarConciliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "EliminarConciliacion", ex);
            }
        }


        public List<ConciliacionBancaria> ListarConciliacion(String filtro, DateTime pFechaIni, DateTime pFechaFin, Usuario vUsuario)
        {

            try
            {
                return BOConcilia.ListarConciliacion(filtro,pFechaIni,pFechaFin, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarConciliacion", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarConciliacion(ConciliacionBancaria pConcili, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ConsultarConciliacion(pConcili, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ConsultarConciliacion", ex);
                return null;
            }
        }



        public List<ConciliacionBancaria> ListarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarCuentasBancarias(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarCuentasBancarias", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarCuentasBancarias(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ConsultarCuentasBancarias(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ConsultarCuentasBancarias", ex);
                return null;
            }
        }


        public List<ConciliacionBancaria> ListarPlanCuentas(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarPlanCuentas(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarPlanCuentas", ex);
                return null;
            }
        }


        public List<ConciliacionBancaria> ListarExtracto(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarExtracto(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarExtracto", ex);
                return null;
            }
        }


        public ConciliacionBancaria ConsultarExtracto(ConciliacionBancaria pConci, int pId, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ConsultarExtracto(pConci, pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ConsultarExtracto", ex);
                return null;
            }
        }


        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BOConcilia.ObtenerSiguienteCodigo(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ObtenerSiguienteCodigo", ex);
                return 0;
            }
        }


        public ConciliacionBancaria GenerarConciliacion(ConciliacionBancaria pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.GenerarConciliacion(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "GenerarConciliacion", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_RESUMEN> ListarTemporalResumen(CONCBANCARIA_RESUMEN pConci, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarTemporalResumen(pConci, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarTemporalResumen", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarTemporalDetalle(CONCBANCARIA_DETALLE pConci, int opcion, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarTemporalDetalle(pConci,opcion, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarTemporalDetalle", ex);
                return null;
            }
        }

        public List<CONCBANCARIA_RESUMEN> ListarResumenConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarResumenConciliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarResumenConciliacion", ex);
                return null;
            }
        }


        public List<CONCBANCARIA_DETALLE> ListarDetalleConciliacion(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOConcilia.ListarDetalleConciliacion(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConciliacionBancariaServices", "ListarDetalleConciliacion", ex);
                return null;
            }
        }


    }
}