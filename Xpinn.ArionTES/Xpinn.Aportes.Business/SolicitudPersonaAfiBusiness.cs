using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
using Xpinn.FabricaCreditos.Entities;

namespace Xpinn.Aportes.Business
{
        public class SolicitudPersonaAfiBusiness : GlobalBusiness
        {

            private SolicitudPersonaAfiData DASolicitud;

            public SolicitudPersonaAfiBusiness()
            {
                DASolicitud = new SolicitudPersonaAfiData();
            }


            public SolicitudPersonaAfi CrearSolicitudPersonaAfi(SolicitudPersonaAfi pSolicitudPersonaAfi, Usuario vUsuario, int pOpcion)
            {
                try
                {
                    using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.MaxValue))
                    {
                        pSolicitudPersonaAfi = DASolicitud.CrearSolicitudPersonaAfi(pSolicitudPersonaAfi, vUsuario, pOpcion);
                        ts.Complete(); 
                    }

                    return pSolicitudPersonaAfi;
                }
                catch (Exception ex)
                {
                    BOExcepcion.Throw("SolicitudPersonaAfiBusiness", "CrearSolicitudPersonaAfi", ex);
                    return null;
                }
            }
        public SolicitudPersonaAfi ListarPersonasRepresentante(Int64 pIdent, Usuario vUsuario)
        {
            try
            {
                return DASolicitud.ListarPersonasRepresentante(pIdent, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiBusiness", "ListarPersonasRepresentante", ex);
                return null;
            }
        }

        public void guardarPersonaTema(List<Persona1> lstTemas, Usuario vUsuario)
        {
            try
            {
                foreach (Persona1 tema in lstTemas)
                {
                    DASolicitud.guardarPersonaTema(tema, vUsuario);
                }                
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiBusiness", "guardarPersonaTema", ex);
            }
        }

        public SolicitudPersonaAfi ConsultarPersona1(string filtro, Usuario usuario)
        {
            try
            {
                SolicitudPersonaAfi persona = DASolicitud.ConsultarPersona1(filtro, usuario);
                if(persona != null  && persona.id_persona > 0)
                {
                    //Consultar beneficiarios
                    persona.lstBeneficiarios = DASolicitud.consultarBeneficiarios(persona.id_persona, usuario);
                }
                return persona;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("SolicitudPersonaAfiBusiness", "ConsultarPersona1", ex);
                return null;
            }
        }
    }

}