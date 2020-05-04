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
    public class MovGralCreditoBusiness : GlobalData 
    {
        private PersonaData dataPersona;
        private ProductoData dataProducto;

        public MovGralCreditoBusiness()
        {
            dataPersona = new PersonaData();
            dataProducto = new ProductoData();
        }

        public List<Persona> ListarMovGralCredito(Persona pEntityPersona, Usuario pUsuario)
        {
            try
            {
                return dataPersona.Listar(pEntityPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("MovGralCreditoBusiness", "ListarMovGralCredito", ex);
                return null;
            }
        }

    }
}
