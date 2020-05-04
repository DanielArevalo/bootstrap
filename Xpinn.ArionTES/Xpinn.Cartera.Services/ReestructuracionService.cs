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
    public class ReestructuracionService
    {
        private ReestructuracionBusiness BOReestructuracion;
        private ReestructuracionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public ReestructuracionService()
        {
            BOReestructuracion = new ReestructuracionBusiness();
            BOExcepcion = new ReestructuracionBusiness();
        }

        public string CodigoPrograma { get { return "60102"; } }

        /// <summary>
        /// Método para listado de créditos a refinanciar
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOReestructuracion.ListarCredito(pusuario, sfiltro);
            }
            catch
            { return null; }
        }

        /// <summary>
        /// Método para listado de personas a re-estructurar
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarPersonas(Usuario pusuario, string sfiltro)
        {
            try
            {
                return BOReestructuracion.ListarPersonas(pusuario, sfiltro);
            }
            catch
            { return null; }
        }


        public void CrearReestructurar(Reestructuracion vReestructuracion, List<Xpinn.FabricaCreditos.Entities.CreditoRecoger> ListRecoge, ref Int64 numero_radicacion, ref string error, Usuario pusuario)
        {
            try
            {
                BOReestructuracion.CrearReestructurar(vReestructuracion, ListRecoge, ref numero_radicacion, ref error, pusuario);
            }
            catch
            {
                return;
            }
        }


    }

}
