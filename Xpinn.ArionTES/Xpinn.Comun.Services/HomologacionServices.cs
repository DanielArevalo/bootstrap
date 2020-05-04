
using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Business;

namespace Xpinn.Comun.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class HomologacionServices
    {
        HomologacionBusiness BOHomologacion;
        ExcepcionBusiness BOExcepcion;

        public HomologacionServices()
        {
            BOHomologacion = new HomologacionBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public Homologacion ConsultarHomologacionTipoIdentificacionPorCodigoPersona(string cod_persona, Usuario usuario)
        {
            try
            {
                return BOHomologacion.ConsultarHomologacionTipoIdentificacionPorCodigoPersona(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionServices", "ConsultarHomologacionTipoIdentificacionPorCodigoPersona", ex);
                return null;
            }
        }

        public string ValorHomologacionTipoCuentaBanco(TipoBanco tipoBanco, TipoCuentaBanco tipoCuenta)
        {
            try
            {
                return BOHomologacion.ValorHomologacionTipoCuentaBanco(tipoBanco, tipoCuenta);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionServices", "ValorHomologacionTipoCuentaBanco", ex);
                return null;
            }
        }

        public string ValorHomologacionFechaBancos(TipoBanco tipoBanco)
        {
            try
            {
                return BOHomologacion.ValorHomologacionFechaBancos(tipoBanco);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionServices", "ValorHomologacionFechaBancos", ex);
                return null;
            }
        }

        public Homologacion ConsultarHomologacionTipoIdentificacion(string tipoIdentificacion, Usuario usuario)
        {
            try
            {
                return BOHomologacion.ConsultarHomologacionTipoIdentificacion(tipoIdentificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionServices", "ConsultarHomologacionTipoIdentificacion", ex);
                return null;
            }
        }

        public Persona PersonaDetalle(string cod_persona, Usuario usuario)
        {
            try
            {
                return BOHomologacion.PersonaDetalle(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionServices", "PersonaDetalle", ex);
                return null;
            }
        }
    }
}