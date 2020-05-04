using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.ConciliacionBancaria.Business;
using Xpinn.ConciliacionBancaria.Entities;
using System.Web;

namespace Xpinn.ConciliacionBancaria.Services
{

    public class EstructuraExtractoServices
    {

        private EstructuraExtractoBusiness BOEstructuraBusiness;
        private ExcepcionBusiness BOExcepcion;


        public EstructuraExtractoServices()
        {
            BOEstructuraBusiness = new EstructuraExtractoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "40805"; } }



        public EstructuraExtracto CrearEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            try
            {
                return BOEstructuraBusiness.CrearEstructuraCarga(pEstructura, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "CrearEstructuraCarga", ex);
                return null;
            }
        }

        public EstructuraExtracto ModificarEstructuraCarga(EstructuraExtracto pEstructura, Usuario vUsuario)
        {
            try
            {
                return BOEstructuraBusiness.ModificarEstructuraCarga(pEstructura, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ModificarEstructuraCarga", ex);
                return null;
            }
        }



        public void EliminarEstructuraCarga(Int64 pId, Usuario vUsuario)
        {
            try
            {
                BOEstructuraBusiness.EliminarEstructuraCarga(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "EliminarEstructuraCarga", ex);
            }
        }

        public EstructuraExtracto ConsultarEstructuraCarga(EstructuraExtracto pEntidad, Usuario vUsuario)
        {
            try
            {
               return BOEstructuraBusiness.ConsultarEstructuraCarga(pEntidad, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ConsultarEstructuraCarga", ex);
                return null;
            }
        }


        public List<EstructuraExtracto> ListarEstructuraExtracto(String filtro,Usuario vUsuario)
        {
            try
            {
                return BOEstructuraBusiness.ListarEstructuraExtracto(filtro, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ListarEstructuraExtracto", ex);
                return null;
            }
        }


        public List<DetEstructuraExtracto> ListarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            try
            {
                return BOEstructuraBusiness.ListarEstructuraDetalle(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


        public void EliminarEstructuraDetalle(Int32 pId, Usuario vUsuario)
        {
            try
            {
                BOEstructuraBusiness.EliminarEstructuraDetalle(pId, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "EliminarEstructuraDetalle", ex);
            }
        }
    }
}
