using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;


namespace Xpinn.Nomina.Services
{
    public class AreaService:GlobalService
    {
        private AreaBussines BOAreaEntities;
        private ExcepcionBusiness BOExcepcion;

        public AreaService()
        {
            BOAreaEntities = new AreaBussines();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250103"; } }

        public Area CrearAreaEntities(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                pAreaEntities = BOAreaEntities.CrearArea(pAreaEntities, pusuario);
                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "CrearAreaEntities", ex);
                return null;
            }
        }
        public Area ModificarAreaEntities(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                pAreaEntities = BOAreaEntities.ModificarArea(pAreaEntities, pusuario);
                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "ModificarAreaEntities", ex);
                return null;
            }
        }
        public Area EliminarAreaEntities(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                pAreaEntities = BOAreaEntities.EliminarArea(pAreaEntities, pusuario);
                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "EliminarAreaEntities", ex);
                return null;
            }
        }

        public Area Area_CtaContable_crear(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                pAreaEntities = BOAreaEntities.Area_Ctacontable_crear(pAreaEntities, pusuario);
                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "Area_CtaContable_crear", ex);
                return null;
            }
        }
        public Area Area_CtaContable_modificar(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                pAreaEntities = BOAreaEntities.Area_Ctacontable_modificar(pAreaEntities, pusuario);
                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "Area_CtaContable_modificar", ex);
                return null;
            }
        }
        public Int64 ConsultaMax(Usuario pUsuario)
        {
            try
            {
                return BOAreaEntities.ConsultaMax(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "ConsultaMax", ex);
                return 0;
            }

        }
        public List<Area> ListarAreas(Usuario pusuario)
        {
            try
            {
                return BOAreaEntities.ListarAreas(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "ListarAreas", ex);
                return null;
            }
        }
        public List<Area> ListarAreasContable(Int64 idparametro,Usuario pusuario)
        {
            try
            {
                return BOAreaEntities.ListarAreasContable(idparametro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "ListarAreasContable", ex);
                return null;
            }
        }
        public Area ListarArea(Int64 IdArea,Usuario pusuario)
        {
            try
            {
                return BOAreaEntities.ListarArea(IdArea,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaService", "ListarArea", ex);
                return null;
            }
        }
    }
}
