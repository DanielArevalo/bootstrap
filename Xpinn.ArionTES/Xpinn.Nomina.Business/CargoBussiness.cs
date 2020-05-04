using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;


namespace Xpinn.Nomina.Business
{
    public class CargosBussiness:GlobalBusiness
    {
        private CargosData DACargo;

        public CargosBussiness()
        {
            DACargo = new CargosData();
        }
        public Cargos CrearCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargoEntities = DACargo.CrearCargo(pCargoEntities, pusuario);

                    ts.Complete();

                }

                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "CrearArea", ex);
                return null;
            }
        }

        public Cargos ModificarCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargoEntities = DACargo.ModificarCargo(pCargoEntities, pusuario);

                    ts.Complete();

                }

                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "ModificarCargo", ex);
                return null;
            }
        }
        public Cargos EliminarCargo(Cargos pCargoEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pCargoEntities = DACargo.EliminarCargo(pCargoEntities, pusuario);

                    ts.Complete();

                }

                return pCargoEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "EliminarCargo", ex);
                return null;
            }
        }
      public Int64 ConsultaMax(Usuario pUsuario)
        {
            try
            {
                return DACargo.ConsultarMaxID(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "ModificarArea", ex);
                return 0;
            }
           
        }
        public List<Cargos> ListarCargos(String filtro,Usuario pusuario)
        {
            try
            {
                return DACargo.ListarCargos(filtro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "ListarCargos", ex);
                return null;
            }
        }
         public Cargos ListarCargos(Int64 IdArea,Usuario pusuario)
        {
            try
            {
                return DACargo.ListarCargo(IdArea,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("CargosBussiness", "ListarCargos", ex);
                return null;
            }
        }

    }
}
