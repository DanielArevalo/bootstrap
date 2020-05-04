using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Asesores.Data;
using Xpinn.Util;
using System.IO;

namespace Xpinn.Asesores.Business
{
    public class NegocioBusiness : GlobalData
    {
        private NegocioData dataNegocio;
        private PersonaData dataPersona;

        public NegocioBusiness()
        {
            dataNegocio = new NegocioData();
            dataPersona = new PersonaData();
        }

        public List<Negocio> ListarNegocios(Negocio pEntityNegocio, Usuario pUsuario)
        {
            try
            {
                var list = dataNegocio.Listar(pEntityNegocio, pUsuario);

                foreach (var nodeNegocio in list)
                {
                    nodeNegocio.Persona = dataPersona.Consultar(Convert.ToInt64(nodeNegocio.Persona.IdPersona), pUsuario);
                }
                return list;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NegocioBusiness", "ListarNegocios", ex);
                return null;
            }
        }
    }
}
