using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.Util;

namespace Xpinn.Aportes.Business
{
    public class PersonaActDatosBusiness : GlobalBusiness
    {
        private PersonaActDatosData DAPersona;

        public PersonaActDatosBusiness()
        {
            DAPersona = new PersonaActDatosData();
        }

        public PersonaActDatos CrearPersonaActDatos(PersonaActDatos pPersonaActDatos, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersonaActDatos = DAPersona.CrearPersonaActDatos(pPersonaActDatos, vUsuario);
                    ts.Complete();
                }

                return pPersonaActDatos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "CrearNovedadCambio", ex);
                return null;
            }
        }

        public SolicitudPersonaAfi ActualizarDatosPersona(SolicitudPersonaAfi pPersona, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pPersona = DAPersona.ActualizarDatosPersona(pPersona, pUsuario);
                    ts.Complete();
                }

                return pPersona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AporteBusiness", "ActualizarDatosPersona", ex);
                return null;
            }
        }
    }
}
