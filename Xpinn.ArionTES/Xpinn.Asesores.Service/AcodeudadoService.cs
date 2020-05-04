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
    public class AcodeudadoService
    {
        private AcodeudadosBusiness BOAcodeudados;
        private ExcepcionBusiness BOExcepcion;

        public AcodeudadoService(){
            BOAcodeudados = new AcodeudadosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "110115"; } }


        public List<Acodeudados> ListarAcodeudados(Cliente pCliente, Usuario pUsuario)
        {
            try{
                return BOAcodeudados.ListarAcodeudados(pCliente, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("AcodeudadoService", "ListarAcodeudados", ex);
                return null;
            }
        }

        public List<Acodeudados> ListarAcodeudadoss(Cliente pCliente, Usuario pUsuario)
        {
            try
            {
                return BOAcodeudados.ListarAcodeudadoss(pCliente, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AcodeudadoService", "ListarAcodeudados", ex);
                return null;
            }
        }
    }
}
