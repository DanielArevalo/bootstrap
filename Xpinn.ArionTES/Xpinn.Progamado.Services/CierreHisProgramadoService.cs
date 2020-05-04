using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Programado.Entities;
using Xpinn.Programado.Business;
using System.Web;
using Xpinn.Util;
using Xpinn.Comun.Entities;



namespace Xpinn.Programado.Services
{
    /// <summary>
    /// Servicio para Cajero
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class CierreHisProgramadoService
    {
        private CierreHisProgramadoBusiness BOCierreHistorico;
        private CierreHisProgramadoBusiness BOExcepcion;
        ExcepcionBusiness BOException;
        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public CierreHisProgramadoService()
        {
            BOCierreHistorico = new CierreHisProgramadoBusiness();
            BOExcepcion = new CierreHisProgramadoBusiness();
            BOException = new ExcepcionBusiness();
        }

        public string CodigoProgramaHis { get { return "220406"; } }

        public CierreHistorico CierreHistorico(CierreHistorico pentidad,string estado, DateTime fecha, int cod_usuario, ref string serror, Usuario pUsuario)
        {
           

            try
            {
                return BOCierreHistorico.CierreHistorico(pentidad, estado, fecha, cod_usuario, ref serror, pUsuario);
            }
            catch (Exception ex)
            {
                BOException.Throw("AhorroVistaBusiness", "CrearCierreMensual", ex);
                return null;
            }


        }


        public List<Xpinn.Comun.Entities.Cierea> ListarFechaCierre(Usuario pUsuario)
        {
            return BOCierreHistorico.ListarFechaCierre(pUsuario);
        }

    }
}

