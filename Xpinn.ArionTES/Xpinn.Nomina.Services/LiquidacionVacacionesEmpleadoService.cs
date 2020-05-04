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
    public class LiquidacionVacacionesEmpleadoService
    {

        private LiquidacionVacacionesEmpleadoBusiness BOLiquidacionVacacionesEmpleado;
        private ExcepcionBusiness BOExcepcion;

        public LiquidacionVacacionesEmpleadoService()
        {
            BOLiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleadoBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250215"; } }
        public string CodigoPrograma2 { get { return "250220"; } }

        public LiquidacionVacacionesEmpleado CrearLiquidacionVacacionesEmpleado
        (
            LiquidacionVacacionesEmpleado liquidacion,
            List<LiquidacionVacacionesDetalleEmpleado> listaDetalleEmplado,
            List<ConceptosOpcionesLiquidados> listaConceptosNominaLiquidados,
            Usuario usuario,
            Xpinn.Tesoreria.Entities.Operacion pOperacion,
            Xpinn.FabricaCreditos.Entities.Giro pGiro,
            ref Int64 pIdGiro
        )
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.CrearLiquidacionVacacionesEmpleado(liquidacion, listaDetalleEmplado, listaConceptosNominaLiquidados, usuario, pOperacion, pGiro, ref pIdGiro);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "CrearLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ModificarLiquidacionVacacionesEmpleado(LiquidacionVacacionesEmpleado pLiquidacionVacacionesEmpleado, Usuario pusuario)
        {
            try
            {
                pLiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ModificarLiquidacionVacacionesEmpleado(pLiquidacionVacacionesEmpleado, pusuario);
                return pLiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ModificarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }


        public void EliminarLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionVacacionesEmpleado.EliminarLiquidacionVacacionesEmpleado(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "EliminarLiquidacionVacacionesEmpleado", ex);
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarLiquidacionVacacionesEmpleado(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarUltLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarUltLiquidacionVacacionesEmpleado(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarUltLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }


        public List<LiquidacionVacacionesEmpleado> ListarLiquidacionVacacionesEmpleado(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.ListarLiquidacionVacacionesEmpleado(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ListarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }

        public List<LiquidacionVacacionesDetalleEmpleado> ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(long codigoLiquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(codigoLiquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion", ex);
                return null;
            }
        }

        public bool VerificarSiExisteVacacionesParaEstasFechas(long codigoEmpleado, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.VerificarSiExisteVacacionesParaEstasFechas(codigoEmpleado, fechaInicio, fechaFinal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "VerificarSiExisteVacacionesParaEstasFechas", ex);
                return false;
            }
        }

        public Tuple<List<LiquidacionVacacionesDetalleEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacionVacacionesParaUnEmpleado(LiquidacionVacacionesEmpleado liquidacion, Usuario usuario)
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.GenerarLiquidacionVacacionesParaUnEmpleado(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "GenerarLiquidacionVacacionesParaUnEmpleado", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleadoXCodigo(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarLiquidacionVacacionesEmpleadoXCodigo(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarLiquidacionVacacionesEmpleadoXCodigo", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarPagaVacacionesAnticipadas(Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarPagaVacacionesAnticipadas(pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarPagaVacacionesAnticipadas", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarDiasVacaciones(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarDiasVacaciones(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarDiasVacaciones", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado CrearDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario pusuario)
        {
            try
            {
                pDiasvacacionesEmpleados = BOLiquidacionVacacionesEmpleado.CrearDiasVacacionesEmpleados(pDiasvacacionesEmpleados, pusuario);
                return pDiasvacacionesEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "CrearDiasVacacionesEmpleados", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ModificarDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario pusuario)
        {
            try
            {
                pDiasvacacionesEmpleados = BOLiquidacionVacacionesEmpleado.ModificarDiasVacacionesEmpleados(pDiasvacacionesEmpleados, pusuario);
                return pDiasvacacionesEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "ModificarDiasVacacionesEmpleados", ex);
                return null;
            }
        }

        public List<LiquidacionVacacionesEmpleado> ListarDiasVacaciones(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionVacacionesEmpleado.ListarDiasVacaciones(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosService", "ListarDiasVacaciones", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarDiasVacacionesNovedades(Int64 pId,DateTime fechainicial,DateTime fechafinal, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = BOLiquidacionVacacionesEmpleado.ConsultarDiasVacacionesNovedades(pId, fechainicial, fechafinal ,pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoService", "ConsultarDiasVacacionesNovedades", ex);
                return null;
            }
        }
    }
}