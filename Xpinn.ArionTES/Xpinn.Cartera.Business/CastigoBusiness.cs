using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Cartera.Data;
using Xpinn.Cartera.Entities;
using System.Web;


namespace Xpinn.Cartera.Business
{
    public class CastigoBusiness : GlobalData
    {

        private CastigoData DACastigo;

        /// <summary>
        /// Constructor del objeto de negocio para Castigo
        /// </summary>
        public CastigoBusiness()
        {
            DACastigo = new CastigoData();
        }

        /// <summary>
        /// Método para listar los créditos a Castigo
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public List<Xpinn.FabricaCreditos.Entities.Credito> ListarCredito(Usuario pUsuario, String filtro)
        {
            try
            {
                return DACastigo.ListarCredito(pUsuario, filtro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CastigoBusiness", "ListarCredito", ex);
                return null;
            }
        }


        public Castigo CrearCastigo(Castigo pCastigo, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Castigo entidad = new Castigo();
                    entidad = DACastigo.CrearCastigo(pCastigo, pUsuario);
                    ts.Complete();
                    return entidad;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CastigoBusiness", "CrearCastigo", ex);
                return null;
            }
        }

    }
}
