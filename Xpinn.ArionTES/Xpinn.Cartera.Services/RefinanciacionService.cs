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
    public class RefinanciacionService
    {
        private RefinanciacionBusiness BORefinanciacion;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para CierreHistorio
        /// </summary>
        public RefinanciacionService()
        {
            BORefinanciacion = new RefinanciacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "60112"; } }

        /// <summary>
        /// Método para listado de créditos a refinanciar
        /// </summary>
        /// <param name="pusuario"></param>
        /// <param name="sfiltro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(DateTime? pFecha, string sfiltro, Usuario pusuario )
        {
            try
            {
                return BORefinanciacion.ListarCredito(pFecha, sfiltro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RefinanciacionService", "ListarCredito", ex);
                return null;
            }
        }

        public decimal TotalaPagarCredito(Int64 pNumeroRadicacion, DateTime pFecha, Usuario pusuario)
        {
            try
            {
                return BORefinanciacion.TotalaPagarCredito(pNumeroRadicacion, pFecha, pusuario);
            }
            catch
            { return pNumeroRadicacion; }
        }

        public Refinanciacion CrearRefinanciacion(Refinanciacion pRefinanciacion, Usuario pUsuario)
        {
            try
            {
                return BORefinanciacion.CrearRefinanciacion(pRefinanciacion, pUsuario);
            }
            catch(Exception ex)
            {
                BOExcepcion.Throw("RefinanciacionService", "CrearRefinanciacion", ex);
                return null;
            }
        }

    }

}
