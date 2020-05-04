using System;
using System.Collections.Generic;
using System.Text;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Entities;
using Xpinn.Nomina.Business;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class InactividadesService
    {

        private InactividadesBusiness BOInactividades;
        private ExcepcionBusiness BOExcepcion;

        public InactividadesService()
        {
            BOInactividades = new InactividadesBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250301"; } }

        public Inactividades CrearInactividades(Inactividades pInactividades, Usuario pusuario)
        {
            try
            {
                pInactividades = BOInactividades.CrearInactividades(pInactividades, pusuario);
                return pInactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesService", "CrearInactividades", ex);
                return null;
            }
        }


        public Inactividades ModificarInactividades(Inactividades pInactividades, Usuario pusuario)
        {
            try
            {
                pInactividades = BOInactividades.ModificarInactividades(pInactividades, pusuario);
                return pInactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesService", "ModificarInactividades", ex);
                return null;
            }
        }


        public void EliminarInactividades(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOInactividades.EliminarInactividades(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesService", "EliminarInactividades", ex);
            }
        }


        public Inactividades ConsultarInactividades(Int64 pId, Usuario pusuario)
        {
            try
            {
                Inactividades Inactividades = new Inactividades();
                Inactividades = BOInactividades.ConsultarInactividades(pId, pusuario);
                return Inactividades;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesService", "ConsultarInactividades", ex);
                return null;
            }
        }


        public List<Inactividades> ListarInactividades(string filtro, Usuario pusuario)
        {
            try
            {
                return BOInactividades.ListarInactividades(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InactividadesService", "ListarInactividades", ex);
                return null;
            }
        }


    }
}