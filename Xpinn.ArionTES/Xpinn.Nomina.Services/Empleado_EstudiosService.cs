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
    public class Empleado_EstudiosService
    {

        private Empleado_EstudiosBusiness BOEmpleado_Estudios;
        private ExcepcionBusiness BOExcepcion;

        public Empleado_EstudiosService()
        {
            BOEmpleado_Estudios = new Empleado_EstudiosBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public Empleado_Estudios CrearEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario pusuario)
        {
            try
            {
                pEmpleado_Estudios = BOEmpleado_Estudios.CrearEmpleado_Estudios(pEmpleado_Estudios, pusuario);
                return pEmpleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosService", "CrearEmpleado_Estudios", ex);
                return null;
            }
        }


        public Empleado_Estudios ModificarEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario pusuario)
        {
            try
            {
                pEmpleado_Estudios = BOEmpleado_Estudios.ModificarEmpleado_Estudios(pEmpleado_Estudios, pusuario);
                return pEmpleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosService", "ModificarEmpleado_Estudios", ex);
                return null;
            }
        }


        public void EliminarEmpleado_Estudios(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEmpleado_Estudios.EliminarEmpleado_Estudios(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosService", "EliminarEmpleado_Estudios", ex);
            }
        }


        public Empleado_Estudios ConsultarEmpleado_Estudios(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empleado_Estudios Empleado_Estudios = new Empleado_Estudios();
                Empleado_Estudios = BOEmpleado_Estudios.ConsultarEmpleado_Estudios(pId, pusuario);
                return Empleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosService", "ConsultarEmpleado_Estudios", ex);
                return null;
            }
        }


        public List<Empleado_Estudios> ListarEmpleado_Estudios(long cod_persona, Usuario pusuario)
        {
            try
            {
                return BOEmpleado_Estudios.ListarEmpleado_Estudios(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosService", "ListarEmpleado_Estudios", ex);
                return null;
            }
        }


    }
}
