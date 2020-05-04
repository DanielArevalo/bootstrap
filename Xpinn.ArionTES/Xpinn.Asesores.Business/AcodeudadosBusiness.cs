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
    public class AcodeudadosBusiness : GlobalBusiness
    {
        private AcodeudadosData acodeudadosData;

        public AcodeudadosBusiness(){
            acodeudadosData = new AcodeudadosData();
        }

        public List<Acodeudados> ListarAcodeudados(Cliente pCliente, Usuario pUsuario)
        {
            try{
                return acodeudadosData.ListarAcodeudados(pCliente, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Acodeudados> ListarAcodeudadoss(Cliente pCliente, Usuario pUsuario)
        {
            try
            {
                return acodeudadosData.ListarAcodeudadoss(pCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadosBusiness", "ListarAcodeudados", ex);
                return null;
            }
        }
    }
}
