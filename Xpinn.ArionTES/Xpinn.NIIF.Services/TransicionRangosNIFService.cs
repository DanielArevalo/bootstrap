using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.NIIF.Business;
using Xpinn.NIIF.Entities;
using Xpinn.Util;
using System.Data;
using System.ServiceModel;

namespace Xpinn.NIIF.Services
{
    public class TransicionRangosNIFService
    {

        private TransicionRangosNIFBusiness BOTrans;
        private ExcepcionBusiness BOExcepcion;

        public TransicionRangosNIFService()
        {
            BOTrans = new TransicionRangosNIFBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public string CodigoPrograma { get { return ""; } }

       

        public TransicionRangosNIF ModificarTransicionRangos(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            try
            {
                return BOTrans.ModificarTransicionRangos(pRango, vUsuario);                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFService", "ModificarTransicionRangos", ex);
                return null;
            }
        }

        

        public List<TransicionRangosNIF> ListarTransicionRango(TransicionRangosNIF pRango, Usuario vUsuario)
        {
            try
            {
                return BOTrans.ListarTransicionRango(pRango, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFService", "ListarTransicionRango", ex);
                return null;
            }
        }



        public void EliminarTransicionRango(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOTrans.EliminarTransicionRango(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TransicionRangosNIFService", "EliminarTransicionRango", ex);
            }
        }


    }     

}
