using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Aportes.Data;
using Xpinn.Aportes.Entities;
 
namespace Xpinn.Aportes.Business
{
        public class Persona_infcertificadoBusiness : GlobalBusiness
        {
 
            private Persona_infcertificadoData DAPersona;

            public Persona_infcertificadoBusiness()
            {
                DAPersona = new Persona_infcertificadoData();
            }

            public List<Int32> ListarAniosPersonaCertificado(Int64 pCodAsociado, Usuario pUsuario)
            {
                try
                {
                    return DAPersona.ListarAniosPersonaCertificado(pCodAsociado, pUsuario);
                }
                catch
                {
                    return null;
                }
            }


            public List<Persona_infcertificado> ListarInformacionCertificado(Persona_infcertificado pInfor, string pFiltro, Usuario vUsuario)
            {
                try
                {
                    return DAPersona.ListarInformacionCertificado(pInfor, pFiltro, vUsuario);
                }
                catch(Exception ex)
                {
                    BOExcepcion.Throw("Persona_infcertificadoBusiness", "ListarInformacionCertificado", ex);
                    return null;
                }
            }
 
 
        }
    
}