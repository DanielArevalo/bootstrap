using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using Xpinn.Caja.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
     public  class ConvenioRecaudoBusiness:GlobalBusiness
    {
        private ConvenioRecaudoData BAConvenio;

        public ConvenioRecaudoBusiness()
        {
            BAConvenio = new ConvenioRecaudoData();
        }

        public List<ConvenioRecaudo> ListarConvenios(string filtro, Usuario vUsuario)
        {

            try
            {
                return BAConvenio.ListarConvenios(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ListarConvenios", ex);
                return null;
            }

        }

        public ConvenioRecaudo ConsultarConvenio(string id, Usuario vUsuario)

        {
            try
            {
                return BAConvenio.ConsultarConvenio(id, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ConsultarConvenio", ex);
                return null;

            }
        }

        public ConvenioRecaudo Cre_Mod_Convenio(ConvenioRecaudo Pconvenio, int opcion, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Pconvenio = BAConvenio.Cre_Mod_Convenio(Pconvenio, opcion, pUsuario);
                    ts.Complete();
                }
                return Pconvenio;

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "CrearDevolucion", ex);
                return null;
            }
        }

        public Int64 EliminarConvenio(Int64 id, Usuario pUsuario)
        {
            try
            {
                return BAConvenio.EliminarConvenio(id, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoBusiness", "EliminarConvenio", ex);
                return 0;
            }
        }
        public List<ConvenioRecaudo> ConsultarNumeroProducto(Int64 codpersona, Int64 tipoproducto, Usuario vUsuario)
        {

            try
            {
                return BAConvenio.ConsultarNumeroProducto(codpersona, tipoproducto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("DevolucionBusiness", "ConsultarNumeroProducto", ex);
                return null;
            }

        }

    }


}
