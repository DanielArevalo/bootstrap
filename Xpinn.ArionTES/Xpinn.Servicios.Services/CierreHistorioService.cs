using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Servicios.Entities;
using Xpinn.Servicios.Business;
using System.Web;
using Xpinn.Util;

namespace Xpinn.Servicios.Services
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

        public string CodigoPrograma { get { return "80111"; } }       


        public CierreHistorico CierreHistorico(CierreHistorico pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            try
            {
                return BOCierreHistorico.CierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("CierreHistoricoBusiness", "CierreHistorico", ex);
                return null;
            }
        
        }

       

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierre(pUsuario);
        }

        public CierreHistorico ConsultarCierreServicios(Usuario vUsuario)
        {
            try
            {
                return BOCierreHistorico.ConsultarCierreServicio(vUsuario);
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }


    }
}

