using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Riesgo.Data;
using Xpinn.Riesgo.Entities;

namespace Xpinn.Riesgo.Business
{
   public class ActDatosBusiness : GlobalBusiness

    {
        ActDatosData DAActDatosData;


        public ActDatosBusiness()
        {
            DAActDatosData = new ActDatosData();
        }


        public List<ActDatos> ListarActDatos(string Fechaini,string Fechafin ,ActDatos pActDatos, Usuario usuario)
        {
            try
            {
                return DAActDatosData.ListarActDatos(Fechaini, Fechafin, pActDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActDatosBusiness", "ListarActDatos", ex);
                return null;
            }
        }


        public List<ActDatos> ListarActDatosNoActualizado( ActDatos pActDatos, Usuario usuario)
        {
            try
            {
                return DAActDatosData.ListarActDatosNoActualizado(pActDatos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ActDatosBusiness", "ListarActDatosNoActualizado", ex);
                return null;
            }
        }

    }
}
