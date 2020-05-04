using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Empleado_FamiliarBusiness : GlobalBusiness
    {

        private Empleado_FamiliarData DAEmpleado_Familiar;

        public Empleado_FamiliarBusiness()
        {
            DAEmpleado_Familiar = new Empleado_FamiliarData();
        }

        public Empleado_Familiar CrearEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleado_Familiar = DAEmpleado_Familiar.CrearEmpleado_Familiar(pEmpleado_Familiar, pusuario);

                    ts.Complete();

                }

                return pEmpleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarBusiness", "CrearEmpleado_Familiar", ex);
                return null;
            }
        }


        public Empleado_Familiar ModificarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleado_Familiar = DAEmpleado_Familiar.ModificarEmpleado_Familiar(pEmpleado_Familiar, pusuario);

                    ts.Complete();

                }

                return pEmpleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarBusiness", "ModificarEmpleado_Familiar", ex);
                return null;
            }
        }


        public void EliminarEmpleado_Familiar(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpleado_Familiar.EliminarEmpleado_Familiar(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarBusiness", "EliminarEmpleado_Familiar", ex);
            }
        }


        public Empleado_Familiar ConsultarEmpleado_Familiar(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empleado_Familiar Empleado_Familiar = new Empleado_Familiar();
                Empleado_Familiar = DAEmpleado_Familiar.ConsultarEmpleado_Familiar(pId, pusuario);
                return Empleado_Familiar;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarBusiness", "ConsultarEmpleado_Familiar", ex);
                return null;
            }
        }


        public List<Empleado_Familiar> ListarEmpleado_Familiar(Empleado_Familiar pEmpleado_Familiar, Usuario pusuario)
        {
            try
            {
                return DAEmpleado_Familiar.ListarEmpleado_Familiar(pEmpleado_Familiar, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_FamiliarBusiness", "ListarEmpleado_Familiar", ex);
                return null;
            }
        }


    }
}

