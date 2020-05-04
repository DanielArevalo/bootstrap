using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Business;
using Xpinn.ConciliacionBancaria.Entities;

namespace Xpinn.ConciliacionBancaria.Services
{
    public class ExtractoBancarioServices
    {
        public string CodigoPrograma { get { return "40804"; } }

        private ExtractoBancarioBusiness BOExtracto;
        private ExcepcionBusiness BOExcepcion;

        public ExtractoBancarioServices()
        {
            BOExtracto = new ExtractoBancarioBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }


        public ExtractoBancario CrearExtractoBancario(ExtractoBancario pExtractoBancario, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.CrearExtractoBancario(pExtractoBancario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "CrearExtractoBancario", ex);
                return null;
            }
        }


        public ExtractoBancario ModificarExtractoBancario(ExtractoBancario pExtractoBancario, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ModificarExtractoBancario(pExtractoBancario, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "ModificarExtractoBancario", ex);
                return null;
            }
        }


        public List<ExtractoBancario> ListarExtractoBancario(string pFiltro, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ListarExtractoBancario(pFiltro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "ListarExtractoBancario", ex);
                return null;
            }
        }


        public void EliminarExtractoBancario(Int64 pId, Usuario pUsuario)
        {
            try
            {
                BOExtracto.EliminarExtractoBancario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "EliminarExtractoBancario", ex);
            }
        }


        public ExtractoBancario ConsultarExtractoBancario(Int64 pId, Usuario vUsuario)
        {
            try
            {
                return BOExtracto.ConsultarExtractoBancario(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "ConsultarExtractoBancario", ex);
                return null;
            }
        }

        public List<DetExtractoBancario> ListarDetExtractoBancario(Int32 pId, Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ListarDetExtractoBancario(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "ListarDetExtractoBancario", ex);
                return null;
            }
        }

        public List<DetExtractoBancario> ListarConceptos_Bancarios(Usuario pUsuario)
        {
            try
            {
                return BOExtracto.ListarConceptos_Bancarios(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "ListarConceptos_Bancarios", ex);
                return null;
            }
        }


        public void EliminarDetExtractoBancario(Int32 pId, Usuario pUsuario)
        {
            try
            {
                BOExtracto.EliminarDetExtractoBancario(pId,pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("ExtractoBancarioServices", "EliminarDetExtractoBancario", ex);
            }
        }

    }
}
