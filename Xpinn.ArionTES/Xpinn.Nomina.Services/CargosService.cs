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
    public class CargosService:GlobalService
    {
        private CargosBussiness BOCargosEntities;
        private ExcepcionBusiness BOExcepcion;

        public CargosService()
        {
            BOCargosEntities = new CargosBussiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250620"; } }

        public Cargos CrearCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                pCargoEntities = BOCargosEntities.CrearCargo(pCargoEntities, pusuario);
                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "CrearCargoEntities", ex);
                return null;
            }
        }
        public Cargos ModificarCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                pCargoEntities = BOCargosEntities.ModificarCargo(pCargoEntities, pusuario);
                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "ModificarCargoEntities", ex);
                return null;
            }
        }
        public Cargos EliminarCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                pCargoEntities = BOCargosEntities.EliminarCargo(pCargoEntities, pusuario);
                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "EliminarCargoEntities", ex);
                return null;
            }
        }


         public Int64 ConsultaMax(Usuario pUsuario)
        {
            try
            {
                return BOCargosEntities.ConsultaMax(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "ConsultaMax", ex);
                return 0;
            }

        }
        public List<Cargos> ListarCargos(String filtro ,Usuario pusuario)
        {
            try
            {
                return BOCargosEntities.ListarCargos(filtro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "ListarAreas", ex);
                return null;
            }
        }
        public Cargos ListarCargos(Int64 IdArea,Usuario pusuario)
        {
            try
            {
                return BOCargosEntities.ListarCargos(IdArea,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargoService", "ListarCargos", ex);
                return null;
            }
        }
    }
}
