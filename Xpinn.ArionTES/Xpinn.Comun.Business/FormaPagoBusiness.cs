using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Comun.Data;
using Xpinn.Comun.Entities;
using System.Web;

namespace Xpinn.Comun.Business
{
    public class FormaPagoBusiness : GlobalData
    {

        private FormaPagoData DAFormaPago;

        /// <summary>
        /// Constructor del objeto de negocio para cierre histórico
        /// </summary>
        public FormaPagoBusiness()
        {
            DAFormaPago = new FormaPagoData();
        }

        /// <summary>
        /// Listar cierres realizados
        /// </summary>
        /// <param name="pTipo"></param>
        /// <returns></returns>
        public List<FormaPago> ListarFormaPago(FormaPago pTipo, Usuario pUsuario)
        {
            return DAFormaPago.ListarFormaPago(pTipo, pUsuario);
        }

        

    }
}
