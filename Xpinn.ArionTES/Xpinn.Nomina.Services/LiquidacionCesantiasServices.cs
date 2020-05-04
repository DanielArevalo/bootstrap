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
    public class LiquidacionCesantiasService
    {

        private LiquidacionCesantiasBusiness BOLiquidacionCesantias;
        private ExcepcionBusiness BOExcepcion;

        public LiquidacionCesantiasService()
        {
            BOLiquidacionCesantias = new LiquidacionCesantiasBusiness();
            BOExcepcion = new ExcepcionBusiness();
        }

        public string CodigoPrograma { get { return "250221"; } }

        public LiquidacionCesantias CrearLiquidacionCesantias(
                LiquidacionCesantias pLiquidacionCesantias,
                List<LiquidacionCesantiasDetalle> listaEmpleados,
                List<LiquidacionCesantiasDetEmpleado> listaDetalleEmpleados,
                List<NovedadPrima> listaNovedadesAplicadas,
                Usuario pusuario,
                Xpinn.Tesoreria.Entities.Operacion pOperacion
            )
        {
            try
            {
                pLiquidacionCesantias = BOLiquidacionCesantias.CrearLiquidacionCesantias(pLiquidacionCesantias, listaEmpleados, listaDetalleEmpleados, listaNovedadesAplicadas, pusuario, pOperacion);
                return pLiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "CrearLiquidacionCesantias", ex);
                return null;
            }
        }


        public LiquidacionCesantias ModificarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario pusuario)
        {
            try
            {
                pLiquidacionCesantias = BOLiquidacionCesantias.ModificarLiquidacionCesantias(pLiquidacionCesantias, pusuario);
                return pLiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ModificarLiquidacionCesantias", ex);
                return null;
            }
        }


        public void EliminarLiquidacionCesantias(Int64 pId, Usuario pusuario)
        {
            try
            {
                BOLiquidacionCesantias.EliminarLiquidacionCesantias(pId, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "EliminarLiquidacionCesantias", ex);
            }
        }


        public LiquidacionCesantias ConsultarLiquidacionCesantias(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionCesantias LiquidacionCesantias = new LiquidacionCesantias();
                LiquidacionCesantias = BOLiquidacionCesantias.ConsultarLiquidacionCesantias(pId, pusuario);
                return LiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ConsultarLiquidacionCesantias", ex);
                return null;
            }
        }

        public LiquidacionCesantias ConsultarUltpago(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionCesantias LiquidacionCesantias = new LiquidacionCesantias();
                LiquidacionCesantias = BOLiquidacionCesantias.ConsultarUltpago(pId, pusuario);
                return LiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ConsultarUltpago", ex);
                return null;
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionCesantias liquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, pusuario); ;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo", ex);
                return false;
            }
        }


        public List<LiquidacionCesantias> ListarLiquidacionCesantias(string filtro, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarLiquidacionCesantias(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarLiquidacionCesantias", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionCesantiasDetalle>, List<LiquidacionCesantiasDetEmpleado>, List<NovedadCesantias>>
            GenerarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            try
            {
                return BOLiquidacionCesantias.GenerarLiquidacionCesantias(pLiquidacionCesantias, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "GenerarLiquidacionCesantias", ex);
                return null;
            }
        }

        public List<LiquidacionCesantiasDetalle> ListarLiquidacionCesantiasDetalle(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarLiquidacionCesantiasDetalle(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarLiquidacionCesantiasDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarLiquidacionCesantiasDetEmpleado(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarLiquidacionCesantiasDetEmpleado(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarLiquidacionCesantiasDetEmpleado", ex);
                return null;
            }
        }


        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleado(long paño, long pempleado, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarNovedadesCesantiasDetEmpleado(paño, pempleado, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarNovedadesCesantiasDetEmpleado", ex);
                return null;
            }
        }

        public List<NovedadPrima> ListarNovedadesCesantias(long paño,  Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarNovedadesCesantias(paño,  pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarNovedadesCesantias", ex);
                return null;
            }
        }
        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleadoAplicada(long paño,  long pempleado, Usuario pusuario)
        {
            try
            {
                return BOLiquidacionCesantias.ListarNovedadesCesantiasDetEmpleadoAplicada(paño,  pempleado, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ListarNovedadesCesantiasDetEmpleadoAplicada", ex);
                return null;
            }
        }
        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            try
            {
                return BOLiquidacionCesantias.ConsultarTipoCalculoNovedadDeUnTipoNovedad(codigoTipoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                return null;
            }
        }


        public NovedadCesantias CrearNovedadCesantias(NovedadCesantias pNovedadCesantias, Usuario pusuario)
        {
            try
            {
                pNovedadCesantias = BOLiquidacionCesantias.CrearNovedadCesantias(pNovedadCesantias, pusuario);
                return pNovedadCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasService", "CrearNovedadCesantias", ex);
                return null;
            }
        }



    }
}