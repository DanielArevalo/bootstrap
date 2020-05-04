using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Reporteador.Data;

namespace Xpinn.Reporteador.Entities
{
    public class ExtractoBusiness : GlobalBusiness
    {
        private ExtractoData DAExtracto;

        public ExtractoBusiness()
        {
            DAExtracto = new ExtractoData();
        }


        public List<Extracto> ListarExtracto(string filtro, DateTime pFechaCorte, DateTime pFechaPago, DateTime pFecDetaPagoIni, DateTime pFecDetaPagoFin,
                    DateTime pFecVenAporIni, DateTime pFecVenAporFin, DateTime pFecVenCredIni, DateTime pFecVenCredFin, Usuario pUsuario)
        {
            try
            {
                return DAExtracto.ListarExtracto(filtro, pFechaCorte, pFechaPago, pFecDetaPagoIni, pFecDetaPagoFin, pFecVenAporIni, pFecVenAporFin, pFecVenCredIni, pFecVenCredFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBusiness", "ListarExtracto", ex);
                return null;
            }
        }


        public List<Extracto> ListarDetalleExtracto(Int64 cod_pesona, DateTime pFechaPago, Usuario pUsuario)
        {
            try
            {
                return DAExtracto.ListarDetalleExtracto(cod_pesona, pFechaPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBusiness", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public Extracto BuscarExtractoAnualPersona(int cod_persona, DateTime fechaCorte, Usuario usuario)
        {
            try
            {
                DateTimeHelper dateHelper = new DateTimeHelper();
                DateTime fechaInicial = dateHelper.PrimerDiaDelAño(fechaCorte);
                DateTime fechaFinal = dateHelper.UltimoDiaDelAño(fechaCorte);

                Extracto extracto = new Extracto();
                extracto.revalorizacion = DAExtracto.ValorRevalorizacion(cod_persona, fechaInicial, fechaFinal, usuario);
                extracto.lista_extracto_aportes = DAExtracto.ListarExtractoAnualAporte(cod_persona, fechaInicial, fechaFinal, usuario);
                extracto.lista_extracto_creditos = DAExtracto.ListarExtractoAnualCreditos(cod_persona, fechaInicial, fechaFinal, usuario);
                extracto.lista_extracto_ahorros = DAExtracto.ListarExtractoAnualAhorros(cod_persona, fechaInicial, fechaFinal, usuario);
                //Listar extracto anual CDAT
                extracto.lista_extracto_cdats = DAExtracto.ListarExtractoAnualCDAT(cod_persona, fechaInicial, fechaFinal, usuario);
                //Listar extracto anual ahorro programado
                extracto.lista_extracto_programado = DAExtracto.ListarExtractoAnualProgramado(cod_persona, fechaInicial, fechaFinal, usuario);
                return extracto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBusiness", "BuscarExtractoAnualPersona", ex);
                return null;
            }
        }

        public List<Persona1Ext> ConsultarPersonasAfiliadasExt(string filtro, Usuario usuario)
        {
            try
            {
                return DAExtracto.ConsultarPersonasAfiliadasExt(filtro, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Persona1Service", "ConsultarPersonasAfiliadas", ex);
                return null;
            }
        }


    }
}
