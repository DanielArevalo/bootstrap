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
    public class IngresoPersonalService
    {

        private IngresoPersonalBusiness BOIngresoPersonal;
        private ExcepcionBusiness BOExcepcion;

        public IngresoPersonalService()
        {
            BOIngresoPersonal = new IngresoPersonalBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250202"; } }

        public IngresoPersonal CrearIngresoPersonal(IngresoPersonal pIngresoPersonal, List<ConceptosFijosNominaEmpleados> listaConceptosFijos, Usuario pusuario)
        {
            try
            {
                pIngresoPersonal = BOIngresoPersonal.CrearIngresoPersonal(pIngresoPersonal, listaConceptosFijos, pusuario);
                return pIngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "CrearIngresoPersonal", ex);
                return null;
            }
        }


        public IngresoPersonal ModificarIngresoPersonal(IngresoPersonal pIngresoPersonal, List<ConceptosFijosNominaEmpleados> listaConceptosFijos, Usuario pusuario)
        {
            try
            {
                pIngresoPersonal = BOIngresoPersonal.ModificarIngresoPersonal(pIngresoPersonal, listaConceptosFijos, pusuario);
                return pIngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ModificarIngresoPersonal", ex);
                return null;
            }
        }


        public void EliminarIngresoPersonal(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOIngresoPersonal.EliminarIngresoPersonal(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "EliminarIngresoPersonal", ex);
            }
        }

        public List<IngresoPersonal> ListarContratosDeUnEmpleado(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ListarContratosDeUnEmpleado(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarContratosDeUnEmpleado", ex);
                return null;
            }
        }

        public IngresoPersonal ConsultarIngresoPersonal(Int64 pId, Usuario pusuario)
        {
            try
            {
                IngresoPersonal IngresoPersonal = new IngresoPersonal();
                IngresoPersonal = BOIngresoPersonal.ConsultarIngresoPersonal(pId, pusuario);
                return IngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarIngresoPersonal", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarIngresoPersonal(string filtro, Usuario pusuario)
        {
            try
            {
                return BOIngresoPersonal.ListarIngresoPersonal(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarIngresoPersonal", ex);
                return null;
            }
        }

        public ConceptosFijosNominaEmpleados CrearConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                pConceptosFijosNominaEmpleados = BOIngresoPersonal.CrearConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);
                return pConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "CrearConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public ConceptosFijosNominaEmpleados ModificarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                pConceptosFijosNominaEmpleados = BOIngresoPersonal.ModificarConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);
                return pConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ModificarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public void EliminarConceptosFijosNominaEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOIngresoPersonal.EliminarConceptosFijosNominaEmpleados(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "EliminarConceptosFijosNominaEmpleados", ex);
            }
        }


        public ConceptosFijosNominaEmpleados ConsultarConceptosFijosNominaEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptosFijosNominaEmpleados ConceptosFijosNominaEmpleados = new ConceptosFijosNominaEmpleados();
                ConceptosFijosNominaEmpleados = BOIngresoPersonal.ConsultarConceptosFijosNominaEmpleados(pId, pusuario);
                return ConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public List<ConceptosFijosNominaEmpleados> ListarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                return BOIngresoPersonal.ListarConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarAreas(IngresoPersonal pAreas, Usuario pusuario)
        {
            try
            {
                return BOIngresoPersonal.ListarAreas(pAreas, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarAreas", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarContratacion(IngresoPersonal pcontrato, Usuario pusuario)
        {
            try
            {
                return BOIngresoPersonal.ListarContratacion(pcontrato, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarContratacion", ex);
                return null;
            }
        }


        public List<IngresoPersonal> ListarEmpresaRecaudoPersona(Int64 pId, Usuario pusuario)
        {
            try
            {
                return BOIngresoPersonal.ListarEmpresaRecaudoPersona(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ListarEmpresaRecaudoPersona", ex);
                return null;
            }
        }
        


        public bool VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(IngresoPersonal empleado, Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(empleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina", ex);
                return false;
            }
        }

        public DateTime? ConsultarFechaIngresoSegunNominaYEmpleado(long codigoNomina, long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarFechaIngresoSegunNominaYEmpleado(codigoNomina, codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarFechaIngresoSegunNominaYEmpleado", ex);
                return null;
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(long codigoempleado, long codigonomina, Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(codigoempleado, codigonomina, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina", ex);
                return null;
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoPorCodigoIngreso(long codigoingresopersonal, Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarInformacionDeContratoPorCodigoIngreso(codigoingresopersonal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarInformacionDeContratoPorCodigoIngreso", ex);
                return null;
            }
        }


        
        public IngresoPersonal ConsultarInformacionFechaRegresovacaciones(String pfechainicial,  long codigoempleado, long pdias,Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarInformacionFechaRegresovacaciones(pfechainicial,  codigoempleado, pdias,usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarInformacionFechaRegresovacaciones", ex);
                return null;
            }


        }

        public IngresoPersonal ConsultarInformacionFechaFinvacaciones(String pfechainicial, long dias, long codigoempleado,  Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarInformacionFechaFinvacaciones(pfechainicial, dias, codigoempleado,  usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarInformacionFechaFinvacaciones", ex);
                return null;
            }


        }


        public IngresoPersonal ConsultarInformacionDiaslegales( Usuario usuario)
        {
            try
            {
                return BOIngresoPersonal.ConsultarInformacionDiaslegales(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalService", "ConsultarInformacionDiaslegales", ex);
                return null;
            }


        }

    }
}