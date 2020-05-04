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
    public class LiquidacionNominaService
    {

        private LiquidacionNominaBusiness BOLiquidacionNomina;
        private ExcepcionBusiness BOExcepcion;

        public LiquidacionNominaService()
        {
            BOLiquidacionNomina = new LiquidacionNominaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250209"; } }
        public string CodigoPrograma2 { get { return "250612"; } }
        public string CodigoProgramaanticipos { get { return "250219"; } }
        public string CodigoProgramareportes { get { return "250222"; } }

        public string CodigoProgramaAntInd { get { return "250209"; } }
        public bool CrearLiquidacionNomina(LiquidacionNomina liquidacion, List<LiquidacionNominaDetalle> listaLiquidacion, List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle, List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades, List<LiquidacionNominaNoveEmpleado>  listaLiquidacionNovedadescargadas, List<ConceptosOpcionesLiquidados> listaConceptosOpcionesLiquidados, Usuario usuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            try
            {
                return BOLiquidacionNomina.CrearLiquidacionNomina(liquidacion, listaLiquidacion, listaLiquidacionDetalle, listaLiquidacionNovedades, listaLiquidacionNovedadescargadas,listaConceptosOpcionesLiquidados, usuario, pOperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "CrearLiquidacionNomina", ex);
                return false;
            }
        }

        public ParColumnasPlanillaLiq ConsultarParametrizacionColumnas(int codigoColumna, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ConsultarParametrizacionColumnas(codigoColumna, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarParametrizacionColumnas", ex);
                return null;
            }
        }

        public bool CrearParametrizacionColumnasLiquidacion(ParColumnasPlanillaLiq parametrizacion, List<ParConceptosPlanillaLiq> listaConceptos, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.CrearParametrizacionColumnasLiquidacion(parametrizacion, listaConceptos, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "CrearParametrizacionColumnasLiquidacion", ex);
                return false;
            }
        }

        public List<ParConceptosPlanillaLiq> ListarConceptosParametrizadosSegunColumna(int codigoColumna, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarConceptosParametrizadosSegunColumna(codigoColumna, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarConceptosParametrizadosSegunColumna", ex);
                return null;
            }
        }

        public LiquidacionNomina ModificarLiquidacionNomina(LiquidacionNomina pLiquidacionNomina, Usuario pusuario)
        {
            try
            {
                pLiquidacionNomina = BOLiquidacionNomina.ModificarLiquidacionNomina(pLiquidacionNomina, pusuario);
                return pLiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ModificarLiquidacionNomina", ex);
                return null;
            }
        }


        public void EliminarLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionNomina.EliminarLiquidacionNomina(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "EliminarLiquidacionNomina", ex);
            }
        }

        public void EliminarNovedadesNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionNomina.EliminarNovedadesNomina(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "EliminarNovedadesNomina", ex);
            }
        }



        public LiquidacionNomina ConsultarLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarLiquidacionNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarLiquidacionNomina", ex);
                return null;
            }
        }

        public Nomina_Entidad ConsultarDatosLiquidacion(Int64 pId, Usuario pusuario, Int64 pCodEMpleado)
        {
            try
            {
                Nomina_Entidad LiquidacionNomina = new Nomina_Entidad();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarDatosLiquidacion(pId, pusuario, pCodEMpleado);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarDatosLiquidacion", ex);
                return null;
            }
        }



        public List<LiquidacionNomina> ListarLiquidacionNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNomina", ex);
                return null;
            }
        }

        public List<LiquidacionNomina> ListarReportesNomina(LiquidacionNomina liquidacion, ref string pError, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarReportesNomina(liquidacion, ref pError, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarReportesNomina", ex);
                return null;
            }
        }


        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacion(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.GenerarLiquidacion(liquidacion, listaEmpleados, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "GenerarLiquidacion", ex);
                return null;
            }
        }

        public LiquidacionNominaDetaEmpleado CalcularValorConceptoNominaDeUnEmpleado(int codigoconcepto, long codigoempleado, long codigoNomina, DateTime fechaInicio, DateTime fechaFin, decimal cantidad, decimal valor, Usuario usuario,Int16 origen)
        {
            try
            {
                return BOLiquidacionNomina.CalcularValorConceptoNominaDeUnEmpleado(codigoconcepto, codigoempleado, codigoNomina, fechaInicio, fechaFin, cantidad, valor, usuario, origen);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "CalcularValorConceptoNominaDeUnEmpleado", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetalle> ListarLiquidacionNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaDetalle(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaConceptos(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaConceptos(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaConceptos", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedades(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaNovedades(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaNovedades", ex);
                return null;
            }
        }


        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaNovedadesRecibo(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaNovedadesRecibo(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaNovedadesRecibo", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesAplicadas(long consecutivo,long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaNovedadesAplicadas(consecutivo,codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaNovedadesAplicadas", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodas( Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaNovedadesTodas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaNovedadesTodas", ex);
                return null;
            }
        }

       
        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodasAplicadas(Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarLiquidacionNominaNovedadesTodasAplicadas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarLiquidacionNominaNovedadesTodasAplicadas", ex);
                return null;
            }
        }

        public bool ConsultarSiConceptoEsHoraExtra(long codigoConcepto, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ConsultarSiConceptoEsHoraExtra(codigoConcepto, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarSiConceptoEsHoraExtra", ex);
                return false;
            }
        }

        public int ConsultarUnidadConceptoNomina(long codigoConcepto, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ConsultarUnidadConceptoNomina(codigoConcepto, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarUnidadConceptoNomina", ex);
                return 0;
            }
        }

        public bool VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(fechaInicio, fechaFinal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar", ex);
                return false;
            }
        }

        public LiquidacionNomina ConsultarUltimaFechaLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarUltimaFechaLiquidacionNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarUltimaFechaLiquidacionNomina", ex);
                return null;
            }
        }


         public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticipos(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)

        {
            try
            {
                return BOLiquidacionNomina.GenerarAnticipos(liquidacion, listaEmpleados, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "GenerarAnticipos", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticiposInd(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)

        {
            try
            {
                return BOLiquidacionNomina.GenerarAnticiposInd(liquidacion, listaEmpleados, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "GenerarAnticiposInd", ex);
                return null;
            }
        }


        public bool CrearAnticiposNomina(LiquidacionNomina liquidacion, List<LiquidacionNominaDetalle> listaLiquidacion, Usuario usuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            try
            {
                return BOLiquidacionNomina.CrearAnticiposNomina(liquidacion, listaLiquidacion, usuario, pOperacion);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "CrearAnticiposNomina", ex);
                return false;
            }
        }

        public List<LiquidacionNomina> ListarAnticiposNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarAnticiposNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarAnticiposNomina", ex);
                return null;
            }
        }

        public List<LiquidacionNomina> ListarAnticiposNominaInd(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarAnticiposNominaInd(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarAnticiposNominaInd", ex);
                return null;
            }
        }


        public LiquidacionNomina ConsultarAnticiposNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarAnticiposNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarAnticiposNomina", ex);
                return null;
            }
        }


        public List<LiquidacionNominaDetalle> ListarAnticiposNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.ListarAnticiposNominaDetalle(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ListarAnticiposNominaDetalle", ex);
                return null;
            }
        }


        public LiquidacionNomina ConsultarUltimaFechaAnticiposNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarUltimaFechaAnticiposNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarUltimaFechaAnticiposNomina", ex);
                return null;
            }
        }


        public LiquidacionNomina ConsultarUltimoAnticiposNomina(Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = BOLiquidacionNomina.ConsultarUltimoAnticiposNomina( pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "ConsultarUltimoAnticiposNomina", ex);
                return null;
            }
        }


        public bool CrearNovedadesNomina(List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades, Usuario usuario)
        {
            try
            {
                return BOLiquidacionNomina.CrearNovedadesNomina(listaLiquidacionNovedades, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaService", "CrearNovedadesNomina", ex);
                return false;
            }
        }

    }
}