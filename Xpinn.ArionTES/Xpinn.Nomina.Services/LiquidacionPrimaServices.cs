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
    public class LiquidacionPrimaService
    {

        private LiquidacionPrimaBusiness BOLiquidacionPrima;
        private ExcepcionBusiness BOExcepcion;

        public LiquidacionPrimaService()
        {
            BOLiquidacionPrima = new LiquidacionPrimaBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250212"; } }

        public LiquidacionPrima CrearLiquidacionPrima(
                LiquidacionPrima pLiquidacionPrima,
                List<LiquidacionPrimaDetalle> listaEmpleados,
                List<LiquidacionPrimaDetEmpleado> listaDetalleEmpleados,
                List<NovedadPrima> listaNovedadesAplicadas,
                Usuario pusuario,
                Xpinn.Tesoreria.Entities.Operacion pOperacion
            )
        {
            try
            {
                pLiquidacionPrima = BOLiquidacionPrima.CrearLiquidacionPrima(pLiquidacionPrima, listaEmpleados, listaDetalleEmpleados, listaNovedadesAplicadas, pusuario, pOperacion);
                return pLiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "CrearLiquidacionPrima", ex);
                return null;
            }
        }


        public LiquidacionPrima ModificarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario pusuario)
        {
            try
            {
                pLiquidacionPrima = BOLiquidacionPrima.ModificarLiquidacionPrima(pLiquidacionPrima, pusuario);
                return pLiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ModificarLiquidacionPrima", ex);
                return null;
            }
        }


        public void EliminarLiquidacionPrima(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionPrima.EliminarLiquidacionPrima(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "EliminarLiquidacionPrima", ex);
            }
        }


        public LiquidacionPrima ConsultarLiquidacionPrima(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionPrima LiquidacionPrima = new LiquidacionPrima();
                LiquidacionPrima = BOLiquidacionPrima.ConsultarLiquidacionPrima(pId, pusuario);
                return LiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ConsultarLiquidacionPrima", ex);
                return null;
            }
        }

        public LiquidacionPrima ConsultarUltpago(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionPrima LiquidacionPrima = new LiquidacionPrima();
                LiquidacionPrima = BOLiquidacionPrima.ConsultarUltpago(pId, pusuario);
                return LiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ConsultarUltpago", ex);
                return null;
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionPrima liquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, pusuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo", ex);
                return false;
            }
        }


        public List<LiquidacionPrima> ListarLiquidacionPrima(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarLiquidacionPrima(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarLiquidacionPrima", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionPrimaDetalle>, List<LiquidacionPrimaDetEmpleado>, List<NovedadPrima>> GenerarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            try
            {
                return BOLiquidacionPrima.GenerarLiquidacionPrima(pLiquidacionPrima, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "GenerarLiquidacionPrima", ex);
                return null;
            }
        }

        public List<LiquidacionPrimaDetalle> ListarLiquidacionPrimaDetalle(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarLiquidacionPrimaDetalle(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarLiquidacionPrimaDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarLiquidacionPrimaDetEmpleado(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarLiquidacionPrimaDetEmpleado(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarLiquidacionPrimaDetEmpleado", ex);
                return null;
            }
        }


        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleado(long paño, long psemestre, long pempleado, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarNovedadesPrimaDetEmpleado(paño, psemestre, pempleado, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarLiquidacionPrimaDetEmpleado", ex);
                return null;
            }
        }

        public List<NovedadPrima> ListarNovedadesPrima(long paño, long psemestre, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarNovedadesPrima(paño, psemestre, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarNovedadesPrima", ex);
                return null;
            }
        }
        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleadoAplicada(long paño, long psemestre, long pempleado, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionPrima.ListarNovedadesPrimaDetEmpleadoAplicada(paño, psemestre, pempleado, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ListarNovedadesPrimaDetEmpleadoAplicadas", ex);
                return null;
            }
        }
        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            try
            {
                return BOLiquidacionPrima.ConsultarTipoCalculoNovedadDeUnTipoNovedad(codigoTipoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaService", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                return null;
            }
        }
    }
}