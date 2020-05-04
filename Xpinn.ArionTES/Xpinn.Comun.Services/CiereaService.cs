using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;
using System.Web;

namespace Xpinn.Comun.Services
{
    /// <summary>
    /// Servicios para Programa
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CiereaService
    {
        private CiereaBusiness BOCierea;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CiereaService()
        {
            BOCierea = new CiereaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoProgramaC { get { return "60311"; } }

        public List<Cierea> ListarCierea(Cierea pTipo, Usuario pUsuario)
        {
            try
            {
                return BOCierea.ListarCierea(pTipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public Boolean ExisteCierre(DateTime pFecha, string pTipo, Usuario pUsuario)
        {
            return BOCierea.ExisteCierre(pFecha, pTipo, pUsuario);
        }

        public DateTime FechaUltimoCierre(string ptipo, Usuario pUsuario)
        {
            return BOCierea.FechaUltimoCierre(ptipo, pUsuario);
        }

        public List<Cierea> ConsultaGeneralCierea(String pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOCierea.ConsultaGeneralCierea(pFiltro, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<Cierea> ListarCiereaFecha(String pTipo,String pEstado, Usuario pUsuario)
        {
            try
            {
                return BOCierea.ListarCiereaFecha(pTipo, pEstado, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<Cierea> ListarCiereaFechaComp(String pTipo, String pEstado, Usuario pUsuario)
        {
            try
            {
                return BOCierea.ListarCiereaFechaComp(pTipo, pEstado, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        //Agregado para seguimiento de cierres
        public Cierea ConsultarSigCierre(Usuario pUsuario)
        {
            try
            {
                return BOCierea.ConsultarSigCierre(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiereaService", "ConsultarSigCierre", ex);
                return null;
            }
        }

        public List<Cierea> ListarControlCierres(string filtro, Usuario pUsuario)
        {
            try
            {
                return BOCierea.ListarControlCierres(filtro,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiereaService", "ListarControlCierres", ex);
                return null;
            }
        }

        public List<string> ListarPeriodosCierres(Usuario pUsuario)
        {
            try
            {
                return BOCierea.ListarPeriodosCierres(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CiereaService", "ListarPeriodosCierres", ex);
                return null;
            }
        }
    }
}
