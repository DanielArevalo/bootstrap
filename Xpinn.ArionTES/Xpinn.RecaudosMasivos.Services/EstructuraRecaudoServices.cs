using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Xpinn.Util;
using Xpinn.Tesoreria.Business;
using Xpinn.Tesoreria.Entities;
using System.Web;

namespace Xpinn.Tesoreria.Services
{

    public class EstructuraRecaudoServices
    {

        private EstructuraRecaudoBusiness BOEstructuraBusiness;
        private ExcepcionBusiness BOExcepcion;

        
        public EstructuraRecaudoServices()
        {
            BOEstructuraBusiness = new EstructuraRecaudoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "180202"; } }



        public Estructura_Carga CrearEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
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

        public Estructura_Carga ModificarEstructuraCarga(Estructura_Carga pEstructura, Usuario vUsuario)
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

        public Estructura_Carga ConsultarEstructuraCarga(Estructura_Carga pEntidad, Usuario vUsuario)
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


        public List<Estructura_Carga> ListarEstructuraRecaudo(Estructura_Carga pEstructura, Usuario vUsuario, String filtro,int op)
        {
            try
            {
                return BOEstructuraBusiness.ListarEstructuraRecaudo(pEstructura, vUsuario,filtro,op);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ListarEstructuraRecaudo", ex);
                return null;
            }
        }


        public List<Estructura_Carga_Detalle> ListarEstructuraDetalle(Estructura_Carga_Detalle pEstructura, string pOrden, Usuario vUsuario)
        {
            try
            {
                return BOEstructuraBusiness.ListarEstructuraDetalle(pEstructura,pOrden, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EstructuraRecaudoService", "ListarEstructuraDetalle", ex);
                return null;
            }
        }


        public void EliminarEstructuraDetalle(Int64 pId, Usuario vUsuario)
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
