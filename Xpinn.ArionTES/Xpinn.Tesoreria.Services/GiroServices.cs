using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Services
{

    public class GiroServices
    {

        private GiroBusiness BOGiroBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Acceso
        /// </summary>
        public GiroServices()
        {
            BOGiroBusiness = new GiroBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaConsulta { get { return "40304"; } }
        public string CodigoProgramaConcilia { get { return "40308"; } }
        public string CodigoPrograma { get { return "40303"; } }

        public List<Giro> ListarGiro(Giro pGiro,string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOGiroBusiness.ListarGiro(pGiro,pFiltro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<Giro> ListarGiroConsulta(Giro pGiro, string pFiltro, string pOrdenar, Usuario pUsuario)
        {
            try
            {
                return BOGiroBusiness.ListarGiroConsulta(pGiro, pFiltro, pOrdenar,pUsuario);
            }
            catch
            {
                return null;
            }
        }
        public Giro ConsultarGiro(string pId, Usuario vUsuario)
        {
            try
            {
                return BOGiroBusiness.ConsultarGiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroServices", "ConsultarGiro", ex);
                return null;
            }
        }

        public Giro Crear_ModGiro(Giro pGiro, Usuario vUsuario, int opcion)
        {
            try
            {
                return BOGiroBusiness.Crear_ModGiro(pGiro, vUsuario, opcion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroServices", "Crear_ModGiro", ex);
                return null;
            }
        }

        public void AnularGiro(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOGiroBusiness.AnularGiro(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroServices", "AnularGiro", ex);
                return;
            }
        }

        public List<Giro> ConciliarGiro(Giro pGiro, string pFiltro, Usuario vUsuario)
        {
            try
            {
                return BOGiroBusiness.ConciliarGiro(pGiro, pFiltro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GiroServices", "ConciliarGiro", ex);
                return null;
            }
        }



    }
}
