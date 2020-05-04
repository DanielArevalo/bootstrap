using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class NominaEmpleadoBusiness : GlobalBusiness
    {

        private NominaEmpleadoData DANominaEmpleado;

        public NominaEmpleadoBusiness()
        {
            DANominaEmpleado = new NominaEmpleadoData();
        }

        public NominaEmpleado CrearNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNominaEmpleado = DANominaEmpleado.CrearNominaEmpleado(pNominaEmpleado, pusuario);

                    ts.Complete();

                }

                return pNominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoBusiness", "CrearNominaEmpleado", ex);
                return null;
            }
        }


        public NominaEmpleado ModificarNominaEmpleado(NominaEmpleado pNominaEmpleado, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNominaEmpleado = DANominaEmpleado.ModificarNominaEmpleado(pNominaEmpleado, pusuario);

                    ts.Complete();

                }

                return pNominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoBusiness", "ModificarNominaEmpleado", ex);
                return null;
            }
        }


        public void EliminarNominaEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DANominaEmpleado.EliminarNominaEmpleado(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoBusiness", "EliminarNominaEmpleado", ex);
            }
        }


        public NominaEmpleado ConsultarNominaEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                NominaEmpleado NominaEmpleado = new NominaEmpleado();
                NominaEmpleado = DANominaEmpleado.ConsultarNominaEmpleado(pId, pusuario);
                return NominaEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoBusiness", "ConsultarNominaEmpleado", ex);
                return null;
            }
        }


        public List<NominaEmpleado> ListarNominaEmpleado(string filtro, Usuario pusuario)
        {
            try
            {
                return DANominaEmpleado.ListarNominaEmpleado(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("NominaEmpleadoBusiness", "ListarNominaEmpleado", ex);
                return null;
            }
        }


    }
}