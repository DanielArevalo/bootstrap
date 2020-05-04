using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.FabricaCreditos.Data;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.FabricaCreditos.Business
{
    public class PersonaBusiness : GlobalData
    {
        private PersonaData DAPersona;

        /// <summary>
        /// Constructor del objeto de negocio para Caja
        /// </summary>
        public PersonaBusiness()
        {
            DAPersona = new PersonaData();
        }

        /// <summary>
        /// Obtiene la lista de Lineas de credito
        /// </summary>
        /// <param name="pEntidad">Entidad</param>
        /// <returns>Conjunto de Lineas de credito obtenidas</returns>        
        public List<Persona> ListarPersonas(Persona pPersona, Usuario pUsuario)
        {
            try
            {
                return DAPersona.ListarPersonas(pPersona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("PersonaBusiness", "ListarPersonas", ex);
                return null;
            }
        }
    }
}
