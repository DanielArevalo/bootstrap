using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
using Xpinn.Util;

namespace Xpinn.Nomina.Business
{
    public class EmpleadoBusiness : GlobalBusiness
    {
        private EmpleadoData DAEmpleados;

        public EmpleadoBusiness()
        {
            DAEmpleados = new EmpleadoData();
        }

        public Empleados CrearEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleados = DAEmpleados.CrearEmpleados(pEmpleados, pusuario);

                    ts.Complete();
                }

                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "CrearEmpleados", ex);
                return null;
            }
        }


        public Empleados CrearPersonaEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleados = DAEmpleados.CrearPersonaEmpleados(pEmpleados, pusuario);

                    ts.Complete();
                }

                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "CrearPersonaEmpleados", ex);
                return null;
            }
        }

        public Empleados ModificarEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleados = DAEmpleados.ModificarEmpleados(pEmpleados, pusuario);

                    ts.Complete();

                }

                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ModificarEmpleados", ex);
                return null;
            }
        }
        public Empleados ModificarPersonaEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pEmpleados = DAEmpleados.ModificarPersonaEmpleados(pEmpleados, pusuario);

                    ts.Complete();

                }

                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ModificarPersonaEmpleados", ex);
                return null;
            }
        }


        public void EliminarEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAEmpleados.EliminarEmpleados(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "EliminarEmpleados", ex);
            }
        }


        public Empleados ConsultarEmpleadosCodigoPersona(string pId, Usuario pusuario)
        {
            try
            {
                Empleados  Empleados = DAEmpleados.ConsultarEmpleadosCodigoPersona(pId, pusuario);
                return Empleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarEmpleadosCodigoPersona", ex);
                return null;
            }
        }

        public Empleados ConsultarEmpleadosCodigoEmpleado(string pId, Usuario pusuario)
        {
            try
            {
                Empleados Empleados = DAEmpleados.ConsultarEmpleadosCodigoEmpleado(pId, pusuario);
                return Empleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarEmpleadosCodigoEmpleado", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarConceptoNominasQueSeanHorasExtas(Usuario usuario)
        {
            try
            {
                return DAEmpleados.ListarConceptoNominasQueSeanHorasExtas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarConceptoNominasQueSeanHorasExtas", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ListarNominasALasQuePerteneceUnEmpleado(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarNominasALasQuePerteneceUnEmpleado", ex);
                return null;
            }
        }

        public List<Empleados> ListarEmpleados(string filtro, Usuario pusuario)
        {
            try
            {
                return DAEmpleados.ListarEmpleados(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarEmpleados", ex);
                return null;
            }
        }

        public List<Empleados> ListarEmpleadosConContratoActivo(string filtro, Usuario pusuario)
        {
            try
            {
                return DAEmpleados.ListarEmpleadosConContratoActivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarEmpleadosConContratoActivo", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominaEmpleados(Usuario pusuario)
        {
            try
            {
                return DAEmpleados.ListarNominaEmpleados(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ListarNominaEmpleados", ex);
                return null;
            }
        }

        public bool VerificarPersonaQueNoSeaEmpleadoYa(string cod_persona, Usuario usuario)
        {
            try
            {
                return DAEmpleados.VerificarPersonaQueNoSeaEmpleadoYa(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "VerificarPersonaQueNoSeaEmpleadoYa", ex);
                return false;
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ConsultarInformacionPersonaEmpleado(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarInformacionPersonaEmpleado", ex);
                return null;
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleadoPorIdentificacion(string identificacion, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ConsultarInformacionPersonaEmpleadoPorIdentificacion(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarInformacionPersonaEmpleadoPorIdentificacion", ex);
                return null;
            }
        }

        public Empleados ConsultarInformacioPorcentajeArl(long consecutivo, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ConsultarInformacioPorcentajeArl(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarInformacioPorcentajeArl", ex);
                return null;
            }

        }
        public Empleados ConsultarInformacioPorcentajeArlContrato(long consecutivo, Usuario usuario)
        {
            try
            {
                return DAEmpleados.ConsultarInformacioPorcentajeArlContrato(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosBusiness", "ConsultarInformacioPorcentajeArlContrato", ex);
                return null;
            }

        }
    }
}