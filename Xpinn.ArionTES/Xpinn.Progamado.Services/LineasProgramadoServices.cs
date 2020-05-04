using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Business;

namespace Xpinn.Programado.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class LineasProgramadoServices
    {
        private LineasProgramadoBusiness BOLineasPro;
        private ExcepcionBusiness BOExcepcion;

        public LineasProgramadoServices()
        {
            BOLineasPro = new LineasProgramadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "220107"; } }


        public LineasProgramado CrearMod_LineasProgramado(LineasProgramado pLineas,List<LineaProgramado_Rango> ListaRango,Usuario vUsuario, Int32 opcion)
        {
            try
            {
                return BOLineasPro.CrearMod_LineasProgramado(pLineas, ListaRango, vUsuario,opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "CrearMod_LineasProgramado", ex);
                return null;
            }
        }

        public void EliminarLineaProgramado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOLineasPro.EliminarLineaProgramado(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "EliminarLineaProgramado", ex);
            }
        }

        public List<LineasProgramado> ListarLineasProgramado(String pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ListarLineasProgramado(pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ListarLineasProgramado", ex);
                return null;
            }
        }

        public LineasProgramado ConsultarLineasProgramado(Int64 pId, ref List<LineaProgramado_Rango> ListaRango, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarLineasProgramado(pId, ref ListaRango, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ConsultarLineasProgramado", ex);
                return null;
            }
        }


        public LineasProgramado ConsultarLineas_Programado(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOLineasPro.ConsultarLineas_Programado(pId,  vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ConsultarLineas_Programado", ex);
                return null;
            }
        }
        public LineaProgramado_Tasa ConsultarLineaProgramado_tasa(Int64 pId_rango, string p_idlinea, Usuario vUsuario)
        {
            try
            {
                LineaProgramado_TasaBusiness tasabusiness = new LineaProgramado_TasaBusiness();
                return tasabusiness.ConsultarLineaProgramado_tasa(pId_rango, p_idlinea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ConsultarLineaProgramado_tasa", ex);
                return null;
            }
        }
        public List<LineaProgramado_Requisito> ListarLineasProgramado_Requisito(Int64 pId_rango, string p_idlinea, Usuario vUsuario)
        {
            try
            {
                LineaProgramado_RequisitoBusiness requisitobusiness = new LineaProgramado_RequisitoBusiness();
                return requisitobusiness.ListarLineaProgramado_Requisito(pId_rango, p_idlinea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ListarLineasProgramado_Requisito", ex);
                return null;
            }
        }

        public LineaProgramado_Requisito ConsultarLineaProgramadoRango(Int64 pId_plazo, string p_idlinea, Usuario vUsuario)
        {
            try
            {
                LineaProgramado_RequisitoBusiness requisitobusiness = new LineaProgramado_RequisitoBusiness();
                return requisitobusiness.ConsultarLineaProgramadoRango(pId_plazo, p_idlinea, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineasProgramadoServices", "ConsultarLineaProgramadoRango", ex);
                return null;
            }
        }

    }
}
