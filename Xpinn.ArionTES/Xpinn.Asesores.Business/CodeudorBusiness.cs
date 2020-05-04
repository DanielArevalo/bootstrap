using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Business
{
    public class CodeudoresBusiness : GlobalBusiness
    {
        private CodeudorData codeudorData;

        public CodeudoresBusiness(){
            codeudorData = new CodeudorData();
        }

        public List<Codeudor> ListarCodeudores(DetalleProducto pEntityDetalleProducto, Usuario pUsuario)
        {
            try{
                return codeudorData.Listar(pEntityDetalleProducto, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("CodeudoresBusiness", "ListarCodeudores", ex);
                return null;
            }
        }
    }
}
