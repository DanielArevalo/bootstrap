using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class Empleado_EstudiosBusiness : GlobalBusiness
    {

        private Empleado_EstudiosData DAEmpleado_Estudios;

        public Empleado_EstudiosBusiness()
        {
            DAEmpleado_Estudios = new Empleado_EstudiosData();
        }

        public Empleado_Estudios CrearEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleado_Estudios = DAEmpleado_Estudios.CrearEmpleado_Estudios(pEmpleado_Estudios, pusuario);

                    ts.Complete();

                }

                return pEmpleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosBusiness", "CrearEmpleado_Estudios", ex);
                return null;
            }
        }


        public Empleado_Estudios ModificarEmpleado_Estudios(Empleado_Estudios pEmpleado_Estudios, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleado_Estudios = DAEmpleado_Estudios.ModificarEmpleado_Estudios(pEmpleado_Estudios, pusuario);

                    ts.Complete();

                }

                return pEmpleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosBusiness", "ModificarEmpleado_Estudios", ex);
                return null;
            }
        }


        public void EliminarEmpleado_Estudios(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpleado_Estudios.EliminarEmpleado_Estudios(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosBusiness", "EliminarEmpleado_Estudios", ex);
            }
        }


        public Empleado_Estudios ConsultarEmpleado_Estudios(Int64 pId, Usuario pusuario)
        {
            try
            {
                Empleado_Estudios Empleado_Estudios = new Empleado_Estudios();
                Empleado_Estudios = DAEmpleado_Estudios.ConsultarEmpleado_Estudios(pId, pusuario);
                return Empleado_Estudios;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosBusiness", "ConsultarEmpleado_Estudios", ex);
                return null;
            }
        }


        public List<Empleado_Estudios> ListarEmpleado_Estudios(long cod_persona, Usuario pusuario)
        {
            try
            {
                return DAEmpleado_Estudios.ListarEmpleado_Estudios(cod_persona, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("Empleado_EstudiosBusiness", "ListarEmpleado_Estudios", ex);
                return null;
            }
        }


    }
}

