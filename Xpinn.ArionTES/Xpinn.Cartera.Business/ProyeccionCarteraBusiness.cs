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
    public class ProyeccionCarteraBusiness : GlobalData
    {

        private ProyeccionCarteraData DAProyeccionCartera;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public ProyeccionCarteraBusiness()
        {
            DAProyeccionCartera = new ProyeccionCarteraData();
        }


        public List<ProyeccionCartera> ListarProyeccionCartera(DateTime pfecha, DateTime pfechafinal, Usuario pUsuario)
        {
            return DAProyeccionCartera.ListarProyeccionCartera(pfecha, pfechafinal, pUsuario);
        }

        public Int64 ValidarProyeccionCartera(DateTime pfecha, Usuario pUsuario)
        {
            return DAProyeccionCartera.ValidarProyeccionCartera(pfecha, pUsuario);
        }

        public void Proyeccion(DateTime pfecha, Usuario pUsuario, ref string serror)
        {
            try
            {
                //using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                //{
                    DAProyeccionCartera.Proyeccion(pfecha, pUsuario, ref serror);
                //}
            }
            catch (Exception ex)
            {
                serror = ex.Message;
                BOExcepcion.Throw("ReestructuracionBusiness", "CrearReestructurar", ex);
            }
        }

    }
}
