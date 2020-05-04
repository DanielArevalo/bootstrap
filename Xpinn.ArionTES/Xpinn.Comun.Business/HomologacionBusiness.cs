using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Comun.Entities;
using Xpinn.Comun.Data;
using Xpinn.Util;

namespace Xpinn.Comun.Business
{
    public class HomologacionBusiness : GlobalBusiness
    {
        private HomologacionData DAHomologacion;

        public HomologacionBusiness()
        {
            DAHomologacion = new HomologacionData();
        }

        public Homologacion ConsultarHomologacionTipoIdentificacionPorCodigoPersona(string cod_persona, Usuario usuario)
        {
            try
            {
                return DAHomologacion.ConsultarHomologacionTipoIdentificacionPorCodigoPersona(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionBusiness", "ConsultarHomologacionTipoIdentificacionPorCodigoPersona", ex);
                return null;
            }
        }

        public string ValorHomologacionTipoCuentaBanco(TipoBanco tipoBanco, TipoCuentaBanco tipoCuenta)
        {
            string valor = string.Empty;

            switch (tipoBanco)
            {
                case TipoBanco.BancoBogota:
                    if (tipoCuenta == TipoCuentaBanco.Ahorro)
                        valor = "2";
                    else if (tipoCuenta == TipoCuentaBanco.Corriente)
                        valor = "1";
                    break;
                case TipoBanco.BancoOccidente:
                    if (tipoCuenta == TipoCuentaBanco.Ahorro)
                        valor = "A";
                    else if (tipoCuenta == TipoCuentaBanco.Corriente)
                        valor = "C";
                    break;
                case TipoBanco.Bancolombia:
                    if (tipoCuenta == TipoCuentaBanco.Ahorro)
                        valor = "S";
                    else if (tipoCuenta == TipoCuentaBanco.Corriente)
                        valor = "D";
                    break;
            }

            return valor;
        }

        public string ValorHomologacionFechaBancos(TipoBanco tipoBanco)
        {
            string formatoFecha = string.Empty;
            switch (tipoBanco)
            {
                case TipoBanco.BancoBogota:
                    formatoFecha = "yyyyMMdd";
                    break;
                case TipoBanco.BancoOccidente:
                    formatoFecha = "yyyyMMdd";
                    break;
                case TipoBanco.Bancolombia:
                    formatoFecha = "yyMMdd";
                    break;
            }

            return formatoFecha;
        }

        public Homologacion ConsultarHomologacionTipoIdentificacion(string tipoIdentificacion, Usuario usuario)
        {
            try
            {
                return DAHomologacion.ConsultarHomologacionTipoIdentificacion(tipoIdentificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionBusiness", "ConsultarHomologacionTipoIdentificacion", ex);
                return null;
            }
        }
        
        public Persona PersonaDetalle(string cod_persona, Usuario usuario)
        {
            try
            {
                return DAHomologacion.PersonaDetalle(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HomologacionBusiness", "PersonaDetalle", ex);
                return null;
            }
        }
    }
}
