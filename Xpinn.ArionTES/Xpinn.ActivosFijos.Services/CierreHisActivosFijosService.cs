using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.ActivosFijos.Entities;
using Xpinn.ActivosFijos.Business;
using System.Web;
using Xpinn.Util;

namespace Xpinn.ActivosFijos.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CierreHisActivosFijosService
    {
        private CierreHisActivosFijosBusiness BOCierreHistorico;
        private CierreHisActivosFijosBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CierreHisActivosFijosService()
        {
            BOCierreHistorico = new CierreHisActivosFijosBusiness();
            BOExcepcion = new CierreHisActivosFijosBusiness();
        }

        public string CodigoPrograma { get { return "50107"; } }
       
        public void CierreHistorico(string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
            BOCierreHistorico.CierreHistorico(estado, fecha, cod_usuario, ref serror, pUsuario);               
        }
        
        

        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierre(pUsuario);
        }

      

    }
}

