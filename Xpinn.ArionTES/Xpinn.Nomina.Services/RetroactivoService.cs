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
    public class RetroactivoService
    {

        private RetroactivoBusiness BORetroactivo;
        private ExcepcionBusiness BOExcepcion;

        public RetroactivoService()
        {
            BORetroactivo = new RetroactivoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250208"; } }

        public Retroactivo CrearRetroactivo(Retroactivo pRetroactivo, Usuario pusuario)
        {
            try
            {
                pRetroactivo = BORetroactivo.CrearRetroactivo(pRetroactivo, pusuario);
                return pRetroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoService", "CrearRetroactivo", ex);
                return null;
            }
        }


        public Retroactivo ModificarRetroactivo(Retroactivo pRetroactivo, Usuario pusuario)
        {
            try
            {
                pRetroactivo = BORetroactivo.ModificarRetroactivo(pRetroactivo, pusuario);
                return pRetroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoService", "ModificarRetroactivo", ex);
                return null;
            }
        }


        public void EliminarRetroactivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                BORetroactivo.EliminarRetroactivo(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoService", "EliminarRetroactivo", ex);
            }
        }


        public Retroactivo ConsultarRetroactivo(Int64 pId, Usuario pusuario)
        {
            try
            {
                Retroactivo Retroactivo = new Retroactivo();
                Retroactivo = BORetroactivo.ConsultarRetroactivo(pId, pusuario);
                return Retroactivo;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoService", "ConsultarRetroactivo", ex);
                return null;
            }
        }


        public List<Retroactivo> ListarRetroactivo(string filtro, Usuario pusuario)
        {
            try
            {
                return BORetroactivo.ListarRetroactivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RetroactivoService", "ListarRetroactivo", ex);
                return null;
            }
        }


    }
}