using System;
using System.Collections.Generic;
using Xpinn.Util;
using System.ServiceModel;
using Xpinn.Nomina.Business;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Services
{
    [ServiceBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmpleadoService
    {

        private EmpleadoBusiness BOEmpleados;
        private ExcepcionBusiness BOExcepcion;

        public EmpleadoService()
        {
            BOEmpleados = new EmpleadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250201"; } }

        public Empleados CrearEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                pEmpleados = BOEmpleados.CrearEmpleados(pEmpleados, pusuario);
                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "CrearEmpleados", ex);
                return null;
            }
        }

        public Empleados CrearPersonaEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                pEmpleados = BOEmpleados.CrearPersonaEmpleados(pEmpleados, pusuario);
                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "CrearPersonaEmpleados", ex);
                return null;
            }
        }


        public Empleados ModificarEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                pEmpleados = BOEmpleados.ModificarEmpleados(pEmpleados, pusuario);
                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ModificarEmpleados", ex);
                return null;
            }
        }

        public Empleados ModificarPersonaEmpleados(Empleados pEmpleados, Usuario pusuario)
        {
            try
            {
                pEmpleados = BOEmpleados.ModificarPersonaEmpleados(pEmpleados, pusuario);
                return pEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ModificarPersonaEmpleados", ex);
                return null;
            }
        }


        public void EliminarEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOEmpleados.EliminarEmpleados(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "EliminarEmpleados", ex);
            }
        }


        public Empleados ConsultarEmpleadosCodigoPersona(string pId, Usuario pusuario)
        {
            try
            {
                Empleados Empleados = BOEmpleados.ConsultarEmpleadosCodigoPersona(pId, pusuario);
                return Empleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarEmpleadosCodigoPersona", ex);
                return null;
            }
        }

        public Empleados ConsultarEmpleadosCodigoEmpleado(string pId, Usuario pusuario)
        {
            try
            {
                Empleados Empleados = BOEmpleados.ConsultarEmpleadosCodigoEmpleado(pId, pusuario);
                return Empleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarEmpleadosCodigoEmpleado", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarConceptoNominasQueSeanHorasExtas(Usuario usuario)
        {
            try
            {
                return BOEmpleados.ListarConceptoNominasQueSeanHorasExtas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarConceptoNominasQueSeanHorasExtas", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarNominasALasQuePerteneceUnEmpleadoYTengaContratoActivo", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominasALasQuePerteneceUnEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ListarNominasALasQuePerteneceUnEmpleado(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarNominasALasQuePerteneceUnEmpleado", ex);
                return null;
            }
        }

        public List<Empleados> ListarEmpleados(string filtro, Usuario pusuario)
        {
            try
            {
                return BOEmpleados.ListarEmpleados(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarEmpleados", ex);
                return null;
            }
        }

        public List<Empleados> ListarEmpleadosConContratoActivo(string filtro, Usuario pusuario)
        {
            try
            {
                return BOEmpleados.ListarEmpleadosConContratoActivo(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarEmpleadosConContratoActivo", ex);
                return null;
            }
        }

        public List<NominaEmpleado> ListarNominaEmpleados(Usuario pusuario)
        {
            try
            {
                return BOEmpleados.ListarNominaEmpleados(pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ListarNominaEmpleados", ex);
                return null;
            }
        }

        public bool VerificarPersonaQueNoSeaEmpleadoYa(string cod_persona, Usuario usuario)
        {
            try
            {
               return BOEmpleados.VerificarPersonaQueNoSeaEmpleadoYa(cod_persona, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "VerificarPersonaQueNoSeaEmpleadoYa", ex);
                return false;
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleado(long consecutivoEmpleado, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ConsultarInformacionPersonaEmpleado(consecutivoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarInformacionPersonaEmpleado", ex);
                return null;
            }
        }

        public Empleados ConsultarInformacionPersonaEmpleadoPorIdentificacion(string identificacion, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ConsultarInformacionPersonaEmpleadoPorIdentificacion(identificacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarInformacionPersonaEmpleadoPorIdentificacion", ex);
                return null;
            }
        }


        public Empleados ConsultarInformacioPorcentajeArl(long consecutivo, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ConsultarInformacioPorcentajeArl(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarInformacioPorcentajeArl", ex);
                return null;
            }
        }

        public Empleados ConsultarInformacioPorcentajeArlContrato(long consecutivo, Usuario usuario)
        {
            try
            {
                return BOEmpleados.ConsultarInformacioPorcentajeArlContrato(consecutivo, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("EmpleadosService", "ConsultarInformacioPorcentajeArlContrato", ex);
                return null;
            }
        }

    }
}