using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Aportes.Entities;
using Xpinn.Aportes.Business;
using System.Web;
using Xpinn.Util;
using Xpinn.Comun;

namespace Xpinn.Aportes.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CierreHisAporteService
    {
        private CierreHisAportesBusiness BOCierreHistorico;
        private CierreHisAportesBusiness BOExcepcion;
        private ExcepcionBusiness BOException;
        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CierreHisAporteService()
        {
            BOCierreHistorico = new CierreHisAportesBusiness();
            BOExcepcion = new CierreHisAportesBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "170105"; } }
        public string CodigoProgramaCausacion { get { return "60302"; } }
        public string CodigoProgramaProvision { get { return "60303"; } }
        public string CodigoProgramaCHPersona { get { return "170120"; } }
        public string CodigoProgramaCausacionPerm { get { return "170127"; } }

        public Aporte CierreHistorico(Aporte pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            try
            {
                return BOCierreHistorico.CierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("CierreHisAporteService", "CierreHistorico", ex);
                return null;
            }
        }
        
        
        public void CierreHistoricoPersonas(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            BOCierreHistorico.CierreHistoricoPersonas(estado, fecha, cod_usuario, ref serror, pUsuario);
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierre(pUsuario);
        }

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierrePersona(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierrePersona(pUsuario);
        }

        public List<CausacionPermanente> ListarCausacionPermanentes(DateTime pFechaIni, CausacionPermanente pAhorroPerm, ref string pError, Usuario vUsuario)
        {
            return BOCierreHistorico.ListarCausacionPermanentes(pFechaIni, pAhorroPerm, ref pError, vUsuario);
        }

    }
}

