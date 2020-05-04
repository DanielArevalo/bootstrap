using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Tesoreria.Entities;
using Xpinn.Tesoreria.Business;

namespace Xpinn.Tesoreria.Services
{
    public class ConvenioRecaudoService
    {
        private ConvenioRecaudoBusiness BOConvenio;
        private ExcepcionBusiness BOExcepcion;

        public ConvenioRecaudoService()
        {
            BOConvenio = new ConvenioRecaudoBusiness();
        }

        public string CodigoPrograma { get { return "220113"; } } //Recordar Cambiar

        public List<ConvenioRecaudo> ListarConvenios(string filtro, Usuario vUsuario)
        {

            try
            {
                return BOConvenio.ListarConvenios(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoService", "ListarConvenios", ex);
                return null;
            }

        }

        public ConvenioRecaudo ConsultarConvenio(string id, Usuario vUsuario)

        {
            try
            {
                return BOConvenio.ConsultarConvenio(id, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoService", "ConsultarConvenio", ex);
                return null;

            }
        }

        public ConvenioRecaudo Cre_Mod_Convenio(ConvenioRecaudo Pconvenio, int opcion, Usuario pUsuario)
        {
            try
            {
                return BOConvenio.Cre_Mod_Convenio(Pconvenio, opcion, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoService", "CrearDevolucion", ex);
                return null;
            }
        }

        public Int64 EliminarConvenio(Int64 id, Usuario pUsuario)
        {
            try
            {
                return BOConvenio.EliminarConvenio(id, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoService", "EliminarConvenio", ex);
                return 0;
            }
        }
        public List<ConvenioRecaudo> ConsultarNumeroProducto(Int64 codpersona, Int64 tipoproducto, Usuario vUsuario)
        {

            try
            {
                return BOConvenio.ConsultarNumeroProducto(codpersona, tipoproducto, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ConvenioRecaudoService", "ConsultarNumeroProducto", ex);
                return null;
            }

        }
    }
}

