using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
using System.Linq;

namespace Xpinn.Nomina.Business
{

    public class IngresoPersonalBusiness : GlobalBusiness
    {

        private IngresoPersonalData DAIngresoPersonal;

        public IngresoPersonalBusiness()
        {
            DAIngresoPersonal = new IngresoPersonalData();
        }

        public IngresoPersonal CrearIngresoPersonal(IngresoPersonal pIngresoPersonal, List<ConceptosFijosNominaEmpleados> listaConceptosFijos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngresoPersonal = DAIngresoPersonal.CrearIngresoPersonal(pIngresoPersonal, pusuario);

                    if (listaConceptosFijos != null)
                    {
                        foreach (ConceptosFijosNominaEmpleados conceptoFijo in listaConceptosFijos.Where(x => x.codigoconcepto > 0).ToList())
                        {
                            conceptoFijo.codigoIngresoPersonal = pIngresoPersonal.consecutivo;
                            conceptoFijo.codigoempleado = pIngresoPersonal.codigoempleado;

                            DAIngresoPersonal.CrearConceptosFijosNominaEmpleados(conceptoFijo, pusuario);
                        }
                    }

                    ts.Complete();
                }

                return pIngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "CrearIngresoPersonal", ex);
                return null;
            }
        }


        public IngresoPersonal ModificarIngresoPersonal(IngresoPersonal pIngresoPersonal, List<ConceptosFijosNominaEmpleados> listaConceptosFijos, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pIngresoPersonal = DAIngresoPersonal.ModificarIngresoPersonal(pIngresoPersonal, pusuario);

                    if (listaConceptosFijos != null)
                    {
                        foreach (ConceptosFijosNominaEmpleados concepto in listaConceptosFijos.Where(x => x.codigoconcepto > 0).ToList())
                        {
                            concepto.codigoIngresoPersonal = pIngresoPersonal.consecutivo;
                            concepto.codigoempleado = pIngresoPersonal.codigoempleado;

                            if (concepto.consecutivo > 0)
                            {
                                DAIngresoPersonal.ModificarConceptosFijosNominaEmpleados(concepto, pusuario);
                            }
                            else
                            {
                                DAIngresoPersonal.CrearConceptosFijosNominaEmpleados(concepto, pusuario);
                            }
                        }
                    }

                    ts.Complete();
                }

                return pIngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ModificarIngresoPersonal", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarContratosDeUnEmpleado(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ListarContratosDeUnEmpleado(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarContratosDeUnEmpleado", ex);
                return null;
            }
        }

        public void EliminarIngresoPersonal(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIngresoPersonal.EliminarIngresoPersonal(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "EliminarIngresoPersonal", ex);
            }
        }


        public IngresoPersonal ConsultarIngresoPersonal(Int64 pId, Usuario pusuario)
        {
            try
            {
                IngresoPersonal IngresoPersonal = new IngresoPersonal();
                IngresoPersonal = DAIngresoPersonal.ConsultarIngresoPersonal(pId, pusuario);
                return IngresoPersonal;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarIngresoPersonal", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarIngresoPersonal(string filtro, Usuario pusuario)
        {
            try
            {
                return DAIngresoPersonal.ListarIngresoPersonal(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarIngresoPersonal", ex);
                return null;
            }
        }

        public ConceptosFijosNominaEmpleados CrearConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptosFijosNominaEmpleados = DAIngresoPersonal.CrearConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);

                    ts.Complete();

                }

                return pConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "CrearConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public ConceptosFijosNominaEmpleados ModificarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptosFijosNominaEmpleados = DAIngresoPersonal.ModificarConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);

                    ts.Complete();

                }

                return pConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ModificarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }

        public bool VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(IngresoPersonal empleado, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina(empleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "VerificarQueEmpleadoNoTengaUnContratoActivoYaParaEstaNomina", ex);
                return false;
            }
        }

        public DateTime? ConsultarFechaIngresoSegunNominaYEmpleado(long codigoNomina, long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarFechaIngresoSegunNominaYEmpleado(codigoNomina, codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarFechaIngresoSegunNominaYEmpleado", ex);
                return null;
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(long codigoempleado, long codigonomina, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina(codigoempleado, codigonomina, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarInformacionDeContratoActivoDeUnEmpleadoSegunNomina", ex);
                return null;
            }
        }

        public IngresoPersonal ConsultarInformacionDeContratoPorCodigoIngreso(long codigoingresopersonal, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarInformacionDeContratoPorCodigoIngreso(codigoingresopersonal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarInformacionDeContratoPorCodigoIngreso", ex);
                return null;
            }
        }

        public void EliminarConceptosFijosNominaEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAIngresoPersonal.EliminarConceptosFijosNominaEmpleados(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "EliminarConceptosFijosNominaEmpleados", ex);
            }
        }


        public ConceptosFijosNominaEmpleados ConsultarConceptosFijosNominaEmpleados(Int64 pId, Usuario pusuario)
        {
            try
            {
                ConceptosFijosNominaEmpleados ConceptosFijosNominaEmpleados = new ConceptosFijosNominaEmpleados();
                ConceptosFijosNominaEmpleados = DAIngresoPersonal.ConsultarConceptosFijosNominaEmpleados(pId, pusuario);
                return ConceptosFijosNominaEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public List<ConceptosFijosNominaEmpleados> ListarConceptosFijosNominaEmpleados(ConceptosFijosNominaEmpleados pConceptosFijosNominaEmpleados, Usuario pusuario)
        {
            try
            {
                return DAIngresoPersonal.ListarConceptosFijosNominaEmpleados(pConceptosFijosNominaEmpleados, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarConceptosFijosNominaEmpleados", ex);
                return null;
            }
        }


        public List<IngresoPersonal> ListarAreas(IngresoPersonal pAreas, Usuario pusuario)
        {
            try
            {
                return DAIngresoPersonal.ListarAreas(pAreas, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarAreas", ex);
                return null;
            }
        }


        public List<IngresoPersonal> ListarContratacion(IngresoPersonal pcontrato, Usuario pusuario)
        {
            try
            {
                return DAIngresoPersonal.ListarContratacion(pcontrato, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarContratacion", ex);
                return null;
            }
        }

        public List<IngresoPersonal> ListarEmpresaRecaudoPersona(Int64 pId, Usuario pusuario)
        {
            try
            {
                return DAIngresoPersonal.ListarEmpresaRecaudoPersona(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ListarEmpresaRecaudoPersona", ex);
                return null;
            }
        }
        public IngresoPersonal ConsultarInformacionFechaFinvacaciones(String pfechainicial, long dias, long codigoingresopersonal, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarInformacionFechaFinvacaciones(pfechainicial,dias,codigoingresopersonal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarInformacionFechaFinvacaciones", ex);
                return null;
            }
        }

       

        public IngresoPersonal ConsultarInformacionFechaRegresovacaciones(String pfechainicial,  long codigoingresopersonal, long pdias, Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarInformacionFechaRegresovacaciones(pfechainicial,   codigoingresopersonal, pdias, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarInformacionFechaRegresovacaciones", ex);
                return null;
            }
        }


        public IngresoPersonal ConsultarInformacionDiaslegales(Usuario usuario)
        {
            try
            {
                return DAIngresoPersonal.ConsultarInformacionDiaslegales(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("IngresoPersonalBusiness", "ConsultarInformacionDiaslegales", ex);
                return null;
            }
        }

    }
}