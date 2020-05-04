using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;
//Prueba s  1257

namespace Xpinn.FabricaCreditos.Business
{
    public class HistoricoTasaBusiness : GlobalData
    {
        private HistoricoTasaData DAHistoricoTasaData;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public HistoricoTasaBusiness()
        {
            DAHistoricoTasaData = new HistoricoTasaData();
        }

        public IList<HistoricoTasa> listarhistorico(string tipo, Usuario pUsuario)
        {
            try
            {
                return DAHistoricoTasaData.listarhistorico(tipo, pUsuario);
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
                return DAHistoricoTasaData.ListarTasasHistoricas(pentidad, pUsuario);
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
                return DAHistoricoTasaData.obtenermod(cod, pUsuario);
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
               using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                 {
                     DAHistoricoTasaData.ModHistorico(historico, pUsuario);

                     ts.Complete();
                 }
            }
            catch
            {

            }
        }
        public void CrearHistorico(HistoricoTasa historico, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAHistoricoTasaData.CrearHistorico(historico, pUsuario);

                    ts.Complete();
                }
             
            }
            catch
            {

            }
        }
        public void EliminarHistorico(long cod, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {

                    DAHistoricoTasaData.EliminarHistorico(cod, pUsuario);
                    ts.Complete();
                }
            }
            catch
            {

            }
        }
        public IList<HistoricoTasa> tipohistorico(Usuario pUsuario)
        {
            try
            {
                return DAHistoricoTasaData.tipohistorico(pUsuario);
            }
            catch
            {
                return null;
            }
        }
    }
}
