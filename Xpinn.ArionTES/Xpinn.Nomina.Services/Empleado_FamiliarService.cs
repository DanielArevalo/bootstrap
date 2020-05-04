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
    public class Empleado_FamiliarService
    {

        private Empleado_FamiliarBusiness BOEmpleado_Familiar;
        private ExcepcionBusiness BOExcepcion;

        public Empleado_FamiliarService()
        {
            BOEmpleado_Familiar = new Empleado_FamiliarBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return ""; } }

        public Empleado_Familiar CrearEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                pEmpleado_Familiar = BOEmpleado_Familiar.CrearEmpleado_Familiar(pEmpleado_Familiar, pusuario);
                return pEmpleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarService", "CrearEmpleado_Familiar", ex);
                return null;
            }
        }


        public Empleado_Familiar ModificarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                pEmpleado_Familiar = BOEmpleado_Familiar.ModificarEmpleado_Familiar(pEmpleado_Familiar, pusuario);
                return pEmpleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarService", "ModificarEmpleado_Familiar", ex);
                return null;
            }
        }


        public void EliminarEmpleado_Familiar(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEmpleado_Familiar.EliminarEmpleado_Familiar(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarService", "EliminarEmpleado_Familiar", ex);
            }
        }


        public Empleado_Familiar ConsultarEmpleado_Familiar(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empleado_Familiar Empleado_Familiar = new Empleado_Familiar();
                Empleado_Familiar = BOEmpleado_Familiar.ConsultarEmpleado_Familiar(pId, pusuario);
                return Empleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarService", "ConsultarEmpleado_Familiar", ex);
                return null;
            }
        }


        public List<Empleado_Familiar> ListarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                return BOEmpleado_Familiar.ListarEmpleado_Familiar(pEmpleado_Familiar, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarService", "ListarEmpleado_Familiar", ex);
                return null;
            }
        }


    }
}