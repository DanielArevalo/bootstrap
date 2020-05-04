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
    public class NominaEmpleadoService
    {

        private NominaEmpleadoBusiness BONominaEmpleado;
        private ExcepcionBusiness BOExcepcion;

        public NominaEmpleadoService()
        {
            BONominaEmpleado = new NominaEmpleadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250606"; } }

        public NominaEmpleado CrearNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario pusuario)
        {
            try
            {
                pNominaEmpleado = BONominaEmpleado.CrearNominaEmpleado(pNominaEmpleado, pusuario);
                return pNominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoService", "CrearNominaEmpleado", ex);
                return null;
            }
        }


        public NominaEmpleado ModificarNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario pusuario)
        {
            try
            {
                pNominaEmpleado = BONominaEmpleado.ModificarNominaEmpleado(pNominaEmpleado, pusuario);
                return pNominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoService", "ModificarNominaEmpleado", ex);
                return null;
            }
        }


        public void EliminarNominaEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                BONominaEmpleado.EliminarNominaEmpleado(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoService", "EliminarNominaEmpleado", ex);
            }
        }


        public NominaEmpleado ConsultarNominaEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                NominaEmpleado NominaEmpleado = new NominaEmpleado();
                NominaEmpleado = BONominaEmpleado.ConsultarNominaEmpleado(pId, pusuario);
                return NominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoService", "ConsultarNominaEmpleado", ex);
                return null;
            }
        }


        public List<NominaEmpleado> ListarNominaEmpleado(string filtro, Usuario pusuario)
        {
            try
            {
                return BONominaEmpleado.ListarNominaEmpleado(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoService", "ListarNominaEmpleado", ex);
                return null;
            }
        }


    }
}