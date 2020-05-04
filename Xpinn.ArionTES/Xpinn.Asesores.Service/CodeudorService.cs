using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Util;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;


namespace Xpinn.Asesores.Services
{
    public class CodeudorService
    {
        private CodeudoresBusiness BOCodeudores;
        private ExcepcionBusiness BOExcepcion;

        public CodeudorService(){
            BOCodeudores = new CodeudoresBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110117"; } }


        public List<Codeudor> ListarCodeudores( DetalleProducto pEntityDetalleProducto, Usuario pUsuario)
        {
            try{
                return BOCodeudores.ListarCodeudores(pEntityDetalleProducto, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("CodeudoresService", "ListarCodeudores", ex);
                return null;
            }
        }
    }
}
