using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Xpinn.Asesores.Business;
using Xpinn.Asesores.Entities;
using Xpinn.Asesores.Entities.Common;
using Xpinn.Util;

namespace Xpinn.Asesores.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ExtractoService
    {
        private ExtractoBusiness BOExtracto;
        private ExcepcionBusiness BOExcepcion;

        ClientePotencialService serviceCliente = new ClientePotencialService();

        public string CodigoPrograma { get { return "110112"; } }
        public string CodigoProgramaCertifAnual { get { return "200108"; } }

        public ExtractoService()
        {
            BOExtracto = new ExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public List<Extracto> ListarExtracto(string filtro, DateTime pFechaCorte, DateTime pFechaPago, DateTime pFecDetaPagoIni, DateTime pFecDetaPagoFin,
                    DateTime pFecVenAporIni, DateTime pFecVenAporFin, DateTime pFecVenCredIni, DateTime pFecVenCredFin, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ListarExtracto(filtro, pFechaCorte, pFechaPago, pFecDetaPagoIni, pFecDetaPagoFin, pFecVenAporIni, pFecVenAporFin, pFecVenCredIni, pFecVenCredFin, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarExtracto", ex);
                return null;
            }
        }


        public List<Extracto> ListarDetalleExtracto(Int64 cod_pesona, DateTime pFechaPago, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ListarDetalleExtracto(cod_pesona,pFechaPago, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "ListarDetalleExtracto", ex);
                return null;
            }
        }

        public Extracto BuscarExtractoAnualPersona(int cod_persona, DateTime fechaCorte, Usuario usuario)
        {
            try
            {
                return BOExtracto.BuscarExtractoAnualPersona(cod_persona, fechaCorte, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoService", "BuscarExtractoAnualPersona", ex);
                return null;
            }
        }
    }
}
