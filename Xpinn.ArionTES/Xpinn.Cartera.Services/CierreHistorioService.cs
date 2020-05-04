using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Cartera.Entities;
using Xpinn.Cartera.Business;
using System.Web;
using Xpinn.Util;

namespace Xpinn.Cartera.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CierreHistorioService
    {
        private CierreHistoricoBusiness BOCierreHistorico;
        private CierreHistoricoBusiness BOExcepcion;
        private ExcepcionBusiness BOException;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CierreHistorioService()
        {
            BOCierreHistorico = new CierreHistoricoBusiness();
            BOExcepcion = new CierreHistoricoBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60301"; } }
        public string CodigoProgramaCausacion { get { return "60302"; } }
        public string CodigoProgramaProvision { get { return "60303"; } }

        public CierreHistorico CierreHistorico(CierreHistorico pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            try
            {
                return BOCierreHistorico.CierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("CierreHistoricoServices", "CierreHistorico", ex);
                return null;
            }

        }

        public void Causacion(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            BOCierreHistorico.Causacion(estado, fecha, cod_usuario, ref serror, pUsuario);
        }

        public void Provision(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            BOCierreHistorico.Provision(estado, fecha, cod_usuario, ref serror, pUsuario);
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierre("R", pUsuario);
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre1(string pTipo = "R", Usuario pUsuario = null)
        {
            return BOCierreHistorico.ListarFechaCierre(pTipo, pUsuario);
        }

    }
}

