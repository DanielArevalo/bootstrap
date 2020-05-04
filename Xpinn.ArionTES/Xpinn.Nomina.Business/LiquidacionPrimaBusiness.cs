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

    public class LiquidacionPrimaBusiness : GlobalBusiness
    {

        private LiquidacionPrimaData DALiquidacionPrima;
        private LiquidacionNominaData DALiquidacionNomina;

        public LiquidacionPrimaBusiness()
        {
            DALiquidacionPrima = new LiquidacionPrimaData();
        }

        public LiquidacionPrima CrearLiquidacionPrima
            (
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10)))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();

                    entidadop = operacion.GrabarOperacion(pOperacion, pusuario);
                    pLiquidacionPrima.cod_ope = entidadop.cod_ope;

                    pLiquidacionPrima = DALiquidacionPrima.CrearLiquidacionPrima(pLiquidacionPrima, pusuario);

                    // Guardamos el detalle de la liquidacion (la liquidacion de cada empleado)
                    foreach (LiquidacionPrimaDetalle empleado in listaEmpleados)
                    {
                        // Codigo de la liquidacion base (la que se acaba de crear)
                        empleado.codigoliquidacionprima = pLiquidacionPrima.consecutivo;
                        empleado.cod_ope = entidadop.cod_ope;

                        DALiquidacionPrima.CrearLiquidacionPrimaDetalle(empleado, pusuario);

                        // Guardamos los detalles para este empleado
                        foreach (LiquidacionPrimaDetEmpleado detalleEmpleado in listaDetalleEmpleados.Where(x => x.codigoempleado == empleado.codigoempleado))
                        {
                            // Codigo de la liquidacion del empleado base (la que se acaba de crear)
                            detalleEmpleado.codigoliquidacionprimadetalle = empleado.consecutivo;
                            detalleEmpleado.cod_ope = pLiquidacionPrima.cod_ope;
                            DALiquidacionPrima.CrearLiquidacionPrimaDetEmpleado(detalleEmpleado, pusuario);

                            LiquidacionPrimaDetEmpleado listaLiquidacionDetalle1 = new LiquidacionPrimaDetEmpleado();
                            DALiquidacionPrima.CrearLiquidacionNominaInterfaz(detalleEmpleado, pLiquidacionPrima, pusuario);


                        }
                    }

                    // Marcamos todas las novedades aplicadas como pagadas
                    foreach (NovedadPrima novedad in listaNovedadesAplicadas)
                    {
                        DALiquidacionPrima.AplicarNovedadPagada(novedad, pusuario);
                    }


                    DALiquidacionPrima.CrearGirosDeLiquidacionNomina(pLiquidacionPrima, pusuario);


                    ts.Complete();
                }

                return pLiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "CrearLiquidacionPrima", ex);
                return null;
            }
        }


        public LiquidacionPrima ModificarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiquidacionPrima = DALiquidacionPrima.ModificarLiquidacionPrima(pLiquidacionPrima, pusuario);

                    ts.Complete();

                }

                return pLiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ModificarLiquidacionPrima", ex);
                return null;
            }
        }


        public void EliminarLiquidacionPrima(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionPrima.EliminarLiquidacionPrima(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "EliminarLiquidacionPrima", ex);
            }
        }


        public LiquidacionPrima ConsultarLiquidacionPrima(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionPrima LiquidacionPrima = new LiquidacionPrima();
                LiquidacionPrima = DALiquidacionPrima.ConsultarLiquidacionPrima(pId, pusuario);
                return LiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ConsultarLiquidacionPrima", ex);
                return null;
            }
        }

        public LiquidacionPrima ConsultarUltpago(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionPrima LiquidacionPrima = new LiquidacionPrima();
                LiquidacionPrima = DALiquidacionPrima.ConsultarUltpago(pId, pusuario);
                return LiquidacionPrima;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ConsultarUltpago", ex);
                return null;
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionPrima liquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionPrima.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, pusuario); ;
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
                List<LiquidacionPrima> listaLiquidacion = DALiquidacionPrima.ListarLiquidacionPrima(filtro, pusuario);

                foreach (LiquidacionPrima entidad in listaLiquidacion)
                {
                    if (entidad.semestre == 1) // Primer Semestre
                    {
                        entidad.fechaInicio = new DateTime(Convert.ToInt32(entidad.anio), 1, 1);
                        entidad.fechaFin = new DateTime(Convert.ToInt32(entidad.anio), 6, 30);
                    }
                    else if (entidad.semestre == 2) // Segundo Semestre
                    {
                        entidad.fechaInicio = new DateTime(Convert.ToInt32(entidad.anio), 7, 1);
                        entidad.fechaFin = new DateTime(Convert.ToInt32(entidad.anio), 12, 30);
                    }
                }

                return listaLiquidacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarLiquidacionPrima", ex);
                return null;
            }
        }

        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            try
            {
                return DALiquidacionPrima.ConsultarTipoCalculoNovedadDeUnTipoNovedad(codigoTipoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionPrimaDetalle>, List<LiquidacionPrimaDetEmpleado>, List<NovedadPrima>> GenerarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionPrima.GenerarLiquidacionPrima(pLiquidacionPrima, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "GenerarLiquidacionPrima", ex);
                return null;
            }
        }

        public List<LiquidacionPrimaDetalle> ListarLiquidacionPrimaDetalle(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionPrima.ListarLiquidacionPrimaDetalle(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarLiquidacionPrimaDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarLiquidacionPrimaDetEmpleado(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionPrima.ListarLiquidacionPrimaDetEmpleado(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarLiquidacionPrimaDetEmpleado", ex);
                return null;
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleado(long paño, long psemestre, long pempleado, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionPrima.ListarNovedadesPrimaDetEmpleado(paño, psemestre, pempleado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarNovedadesPrimaDetEmpleado", ex);
                return null;
            }
        }

        public List<NovedadPrima> ListarNovedadesPrima(long paño, long psemestre,  Usuario vUsuario)
        {
            try
            {
                return DALiquidacionPrima.ListarNovedadesPrima(paño, psemestre, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarNovedadesPrimaDetEmpleado", ex);
                return null;
            }
        }


        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleadoAplicada(long paño, long psemestre, long pempleado, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionPrima.ListarNovedadesPrimaDetEmpleadoAplicada(paño, psemestre, pempleado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionPrimaBusiness", "ListarNovedadesPrimaDetEmpleadoAplicada", ex);
                return null;
            }
        }

    }
}
