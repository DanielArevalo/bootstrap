using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class HorasExtrasEmpleadosBusiness : GlobalBusiness
    {

        private HorasExtrasEmpleadosData DAHorasExtrasEmpleados;

        public HorasExtrasEmpleadosBusiness()
        {
            DAHorasExtrasEmpleados = new HorasExtrasEmpleadosData();
        }

        public HorasExtrasEmpleados CrearHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHorasExtrasEmpleados = DAHorasExtrasEmpleados.CrearHorasExtrasEmpleados(pHorasExtrasEmpleados, pusuario);

                    ts.Complete();

                }

                return pHorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "CrearHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public HorasExtrasEmpleados ModificarHorasExtrasEmpleados(HorasExtrasEmpleados pHorasExtrasEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pHorasExtrasEmpleados = DAHorasExtrasEmpleados.ModificarHorasExtrasEmpleados(pHorasExtrasEmpleados, pusuario);

                    ts.Complete();

                }

                return pHorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "ModificarHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public void EliminarHorasExtrasEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAHorasExtrasEmpleados.EliminarHorasExtrasEmpleados(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "EliminarHorasExtrasEmpleados", ex);
            }
        }


        public HorasExtrasEmpleados ConsultarHorasExtrasEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                HorasExtrasEmpleados HorasExtrasEmpleados = new HorasExtrasEmpleados();
                HorasExtrasEmpleados = DAHorasExtrasEmpleados.ConsultarHorasExtrasEmpleados(pId, pusuario);
                return HorasExtrasEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "ConsultarHorasExtrasEmpleados", ex);
                return null;
            }
        }


        public List<HorasExtrasEmpleados> ListarHorasExtrasEmpleados(string filtro, Usuario pusuario)
        {
            try
            {
                return DAHorasExtrasEmpleados.ListarHorasExtrasEmpleados(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "ListarHorasExtrasEmpleados", ex);
                return null;
            }
        }


    }
}