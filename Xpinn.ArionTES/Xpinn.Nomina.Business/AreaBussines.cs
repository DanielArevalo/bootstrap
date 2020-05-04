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
    public class AreaBussines:GlobalBusiness
    {
        private AreaData DAArea;

        public AreaBussines()
        {
            DAArea = new AreaData();
        }
        public Area CrearArea(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreaEntities = DAArea.CrearArea(pAreaEntities, pusuario);

                    ts.Complete();

                }

                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "CrearArea", ex);
                return null;
            }
        }

        public Area ModificarArea(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreaEntities = DAArea.ModificarArea(pAreaEntities, pusuario);

                    ts.Complete();

                }

                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "ModificarArea", ex);
                return null;
            }
        }
        public Area EliminarArea(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreaEntities = DAArea.EliminarrArea(pAreaEntities, pusuario);

                    ts.Complete();

                }

                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "EliminarArea", ex);
                return null;
            }
        }
        public Area Area_Ctacontable_crear(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreaEntities = DAArea.Area_Ctacontable_crear(pAreaEntities, pusuario);

                    ts.Complete();

                }

                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "Area_Ctacontable_crear", ex);
                return null;
            }
        }
        public Area Area_Ctacontable_modificar(Area pAreaEntities, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pAreaEntities = DAArea.Area_Ctacontable_MOD(pAreaEntities, pusuario);

                    ts.Complete();

                }

                return pAreaEntities;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "Area_Ctacontable_modificar", ex);
                return null;
            }
        }
        public Int64 ConsultaMax(Usuario pUsuario)
        {
            try
            {
                return DAArea.ConsultarMaxID(pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "ModificarArea", ex);
                return 0;
            }
           
        }
        public List<Area> ListarAreas(Usuario pusuario)
        {
            try
            {
                return DAArea.ListarAreas(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "ListarArea", ex);
                return null;
            }
        }
        public List<Area> ListarAreasContable(Int64 idparametro,Usuario pusuario)
        {
            try
            {
                return DAArea.ListarAreasContable(idparametro,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "ListarAreasContable", ex);
                return null;
            }
        }
        public Area ListarArea(Int64 IdArea,Usuario pusuario)
        {
            try
            {
                return DAArea.ListarArea(IdArea,pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("AreaBussines", "ListarArea", ex);
                return null;
            }
        }

    }
}
