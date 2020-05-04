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
    public class HorasExtrasEmpleadosService
    {

        private HorasExtrasEmpleadosBusiness BOHorasExtrasEmpleados;
        private ExcepcionBusiness BOExcepcion;

        public HorasExtrasEmpleadosService()
        {
            BOHorasExtrasEmpleados = new HorasExtrasEmpleadosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250107"; } }

        public HorasExtrasEmpleados CrearHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario pusuario)
        {
            try
            {
                pHorasExtrasEmpleados = BOHorasExtrasEmpleados.CrearHorasExtrasEmpleados(pHorasExtrasEmpleados, pusuario);
                return pHorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "CrearHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public HorasExtrasEmpleados ModificarHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario pusuario)
        {
            try
            {
                pHorasExtrasEmpleados = BOHorasExtrasEmpleados.ModificarHorasExtrasEmpleados(pHorasExtrasEmpleados, pusuario);
                return pHorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "ModificarHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public void EliminarHorasExtrasEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOHorasExtrasEmpleados.EliminarHorasExtrasEmpleados(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "EliminarHorasExtrasEmpleados", ex);
            }
        }


        public HorasExtrasEmpleados ConsultarHorasExtrasEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                HorasExtrasEmpleados HorasExtrasEmpleados = new HorasExtrasEmpleados();
                HorasExtrasEmpleados = BOHorasExtrasEmpleados.ConsultarHorasExtrasEmpleados(pId, pusuario);
                return HorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "ConsultarHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public List<HorasExtrasEmpleados> ListarHorasExtrasEmpleados(string filtro, Usuario pusuario)
        {
            try
            {
                return BOHorasExtrasEmpleados.ListarHorasExtrasEmpleados(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "ListarHorasExtrasEmpleados", ex);
                return null;
            }
        }


    }
}