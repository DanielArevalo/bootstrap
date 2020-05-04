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
    public class NovedadPrimaService
    {

        private NovedadPrimaBusiness BONovedadPrima;
        private ExcepcionBusiness BOExcepcion;

        public NovedadPrimaService()
        {
            BONovedadPrima = new NovedadPrimaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250213"; } }

        public NovedadPrima CrearNovedadPrima(NovedadPrima pNovedadPrima, Usuario pusuario)
        {
            try
            {
                pNovedadPrima = BONovedadPrima.CrearNovedadPrima(pNovedadPrima, pusuario);
                return pNovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaService", "CrearNovedadPrima", ex);
                return null;
            }
        }


        public NovedadPrima ModificarNovedadPrima(NovedadPrima pNovedadPrima, Usuario pusuario)
        {
            try
            {
                pNovedadPrima = BONovedadPrima.ModificarNovedadPrima(pNovedadPrima, pusuario);
                return pNovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaService", "ModificarNovedadPrima", ex);
                return null;
            }
        }


        public void EliminarNovedadPrima(long pId, Usuario pusuario)
        {
            try
            {
                BONovedadPrima.EliminarNovedadPrima(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaService", "EliminarNovedadPrima", ex);
            }
        }


        public NovedadPrima ConsultarNovedadPrima(long pId, Usuario pusuario)
        {
            try
            {
                NovedadPrima NovedadPrima = new NovedadPrima();
                NovedadPrima = BONovedadPrima.ConsultarNovedadPrima(pId, pusuario);
                return NovedadPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaService", "ConsultarNovedadPrima", ex);
                return null;
            }
        }


        public List<NovedadPrima> ListarNovedadPrima(string filtro, Usuario pusuario)
        {
            try
            {
                return BONovedadPrima.ListarNovedadPrima(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NovedadPrimaService", "ListarNovedadPrima", ex);
                return null;
            }
        }


    }
}