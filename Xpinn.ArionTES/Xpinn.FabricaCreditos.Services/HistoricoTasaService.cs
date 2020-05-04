using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.FabricaCreditos.Business;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Util;
using System.Data;

namespace Xpinn.FabricaCreditos.Services
{
    /// <summary>
    /// Servicio para Aprobador
    /// </summary>
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HistoricoTasaService
    {
        private HistoricoTasaBusiness BOHistoricoTasaBusiness;
        private ExcepcionBusiness BOExcepcion;

        /// <summary>
        /// Constructor del servicio para Caja
        /// </summary>
        public HistoricoTasaService()
        {
            BOHistoricoTasaBusiness = new HistoricoTasaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "100208"; } }


        public IList<HistoricoTasa> listarhistorico(string tipo, Usuario pUsuario)
        {
            try
            {
                return BOHistoricoTasaBusiness.listarhistorico(tipo, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public List<HistoricoTasa> ListarTasasHistoricas(HistoricoTasa pentidad, Usuario pUsuario)
        {
            try
            {
                return BOHistoricoTasaBusiness.ListarTasasHistoricas(pentidad, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LineaCreditoService", "ListarTasasHistoricas", ex);
                return null;
            }
        }
     


        public HistoricoTasa obtenermod(string cod, Usuario pUsuario)
        {
            try
            {
                return BOHistoricoTasaBusiness.obtenermod(cod, pUsuario);
            }
            catch
            {
                return null;
            }
        }
        public void ModHistorico(HistoricoTasa historico, Usuario pUsuario)
        {
            try
            {
                 BOHistoricoTasaBusiness.ModHistorico(historico, pUsuario);
            }
            catch
            {

            }
        }
        public void CrearHistorico(HistoricoTasa historico, Usuario pUsuario)
        {
            try
            {
                 BOHistoricoTasaBusiness.CrearHistorico(historico, pUsuario);
            }
            catch
            {

            }
        }
        public void EliminarHistorico(long cod, Usuario pUsuario)
        {
            try
            {
                BOHistoricoTasaBusiness.EliminarHistorico(cod, pUsuario);
            }
            catch
            {

            }
        }
        public IList<HistoricoTasa> tipohistorico(Usuario pUsuario)
        {
            try
            {
                return BOHistoricoTasaBusiness.tipohistorico(pUsuario);
            }
            catch
            {
                return null;
            }
        }
    }
}
