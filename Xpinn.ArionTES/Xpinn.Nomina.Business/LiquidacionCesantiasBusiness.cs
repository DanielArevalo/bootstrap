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

    public class LiquidacionCesantiasBusiness : GlobalBusiness
    {

        private LiquidacionCesantiasData DALiquidacionCesantias;
        private LiquidacionNominaData DALiquidacionNomina;

        public LiquidacionCesantiasBusiness()
        {
            DALiquidacionCesantias = new LiquidacionCesantiasData();
        }

        public LiquidacionCesantias CrearLiquidacionCesantias
            (
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
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10)))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();

                    entidadop = operacion.GrabarOperacion(pOperacion, pusuario);
                    pLiquidacionCesantias.cod_ope = entidadop.cod_ope;
                   
                    pLiquidacionCesantias = DALiquidacionCesantias.CrearLiquidacionCesantias(pLiquidacionCesantias, pusuario);

                    // Guardamos el detalle de la liquidacion (la liquidacion de cada empleado)
                    foreach (LiquidacionCesantiasDetalle empleado in listaEmpleados)
                    {
                        // Codigo de la liquidacion base (la que se acaba de crear)
                        empleado.codigoliquidacionCesantias = pLiquidacionCesantias.consecutivo;
                        empleado.cod_ope = entidadop.cod_ope;
                        empleado.liquidaCesantias = pLiquidacionCesantias.liquidacesantias;
                        empleado.liquidainteres = pLiquidacionCesantias.liquidainteres;


                        DALiquidacionCesantias.CrearLiquidacionCesantiasDetalle(empleado, pusuario);

                        // Guardamos los detalles para este empleado
                        foreach (LiquidacionCesantiasDetEmpleado detalleEmpleado in listaDetalleEmpleados.Where(x => x.codigoempleado == empleado.codigoempleado))
                        {
                            // Codigo de la liquidacion del empleado base (la que se acaba de crear)
                            detalleEmpleado.codigoliquidacioncesantiasdetalle = empleado.consecutivo;
                            detalleEmpleado.cod_ope = pLiquidacionCesantias.cod_ope;

                            detalleEmpleado.liquidaCesantias = empleado.liquidaCesantias;
                            detalleEmpleado.liquidainteres = empleado.liquidainteres;
                            detalleEmpleado.interes = empleado.interes;
                        
                            if (detalleEmpleado.liquidainteres ==1)
                            {
                                    detalleEmpleado.liquidaCesantias = 0;
                                    DALiquidacionCesantias.CrearLiquidacionCesantiasDetEmpleado(detalleEmpleado, pusuario);
                                    LiquidacionCesantiasDetEmpleado listaLiquidacionDetalle1 = new LiquidacionCesantiasDetEmpleado();
                                    DALiquidacionCesantias.CrearLiquidacionNominaInterfaz(detalleEmpleado, pLiquidacionCesantias, pusuario);

                            }                           

                          
                        }


                        foreach (LiquidacionCesantiasDetEmpleado detalleEmpleado in listaDetalleEmpleados.Where(x => x.codigoempleado == empleado.codigoempleado))
                        {
                            // Codigo de la liquidacion del empleado base (la que se acaba de crear)
                            detalleEmpleado.codigoliquidacioncesantiasdetalle = empleado.consecutivo;
                            detalleEmpleado.cod_ope = pLiquidacionCesantias.cod_ope;

                            detalleEmpleado.liquidaCesantias = empleado.liquidaCesantias;
                            detalleEmpleado.liquidainteres = empleado.liquidainteres;
                            detalleEmpleado.interes = empleado.interes;

                         


                            if (detalleEmpleado.liquidaCesantias == 1)
                            {
                                detalleEmpleado.liquidainteres = 0;
                                DALiquidacionCesantias.CrearLiquidacionCesantiasDetEmpleado(detalleEmpleado, pusuario);

                                LiquidacionCesantiasDetEmpleado listaLiquidacionDetalle1 = new LiquidacionCesantiasDetEmpleado();
                                DALiquidacionCesantias.CrearLiquidacionNominaInterfaz(detalleEmpleado, pLiquidacionCesantias, pusuario);

                            }
                        }

                    }

                    // Marcamos todas las novedades aplicadas como pagadas
                    foreach (NovedadPrima novedad in listaNovedadesAplicadas)
                    {
                        DALiquidacionCesantias.AplicarNovedadPagada(novedad, pusuario);
                    }

                    


                    DALiquidacionCesantias.CrearGirosDeLiquidacionNomina(pLiquidacionCesantias, pusuario);


                    ts.Complete();
                }

                return pLiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "CrearLiquidacionCesantias", ex);
                return null;
            }
        }


        public LiquidacionCesantias ModificarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiquidacionCesantias = DALiquidacionCesantias.ModificarLiquidacionCesantias(pLiquidacionCesantias, pusuario);

                    ts.Complete();

                }

                return pLiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ModificarLiquidacionCesantias", ex);
                return null;
            }
        }


        public void EliminarLiquidacionCesantias(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionCesantias.EliminarLiquidacionCesantias(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "EliminarLiquidacionCesantias", ex);
            }
        }


        public LiquidacionCesantias ConsultarLiquidacionCesantias(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionCesantias LiquidacionCesantias = new LiquidacionCesantias();
                LiquidacionCesantias = DALiquidacionCesantias.ConsultarLiquidacionCesantias(pId, pusuario);
                return LiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ConsultarLiquidacionCesantias", ex);
                return null;
            }
        }

        public LiquidacionCesantias ConsultarUltpago(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionCesantias LiquidacionCesantias = new LiquidacionCesantias();
                LiquidacionCesantias = DALiquidacionCesantias.ConsultarUltpago(pId, pusuario);
                return LiquidacionCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ConsultarUltpago", ex);
                return null;
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionCesantias liquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionCesantias.VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(liquidacion, pusuario); ;
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
                List<LiquidacionCesantias> listaLiquidacion = DALiquidacionCesantias.ListarLiquidacionCesantias(filtro, pusuario);

                foreach (LiquidacionCesantias entidad in listaLiquidacion)
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
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarLiquidacionCesantias", ex);
                return null;
            }
        }

        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            try
            {
                return DALiquidacionCesantias.ConsultarTipoCalculoNovedadDeUnTipoNovedad(codigoTipoNovedad, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionCesantiasDetalle>, List<LiquidacionCesantiasDetEmpleado>, List<NovedadCesantias>> GenerarLiquidacionCesantias(LiquidacionCesantias pLiquidacionCesantias, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionCesantias.GenerarLiquidacionCesantias(pLiquidacionCesantias, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "GenerarLiquidacionCesantias", ex);
                return null;
            }
        }

        public List<LiquidacionCesantiasDetalle> ListarLiquidacionCesantiasDetalle(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionCesantias.ListarLiquidacionCesantiasDetalle(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarLiquidacionCesantiasDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarLiquidacionCesantiasDetEmpleado(long codigoLiquidacion, Usuario pusuario)
        {
            try
            {
                return DALiquidacionCesantias.ListarLiquidacionCesantiasDetEmpleado(codigoLiquidacion, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarLiquidacionCesantiasDetEmpleado", ex);
                return null;
            }
        }

        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleado(long paño,   long pempleado, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionCesantias.ListarNovedadesCesantiasDetEmpleado(paño,  pempleado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarNovedadesCesantiasDetEmpleado", ex);
                return null;
            }
        }

        public List<NovedadPrima> ListarNovedadesCesantias(long paño,    Usuario vUsuario)
        {
            try
            {
                return DALiquidacionCesantias.ListarCesantiasNovedades(paño,  vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarNovedadesCesantias", ex);
                return null;
            }
        }


        public List<LiquidacionCesantiasDetEmpleado> ListarNovedadesCesantiasDetEmpleadoAplicada(long paño,  long pempleado, Usuario vUsuario)
        {
            try
            {
                return DALiquidacionCesantias.ListarNovedadesCesantiasDetEmpleadoAplicada(paño,  pempleado, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "ListarNovedadesCesantiasDetEmpleadoAplicada", ex);
                return null;
            }
        }


        public NovedadCesantias CrearNovedadCesantias(NovedadCesantias pNovedadCesantias, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pNovedadCesantias = DALiquidacionCesantias.CrearNovedadCesantias(pNovedadCesantias, pusuario);

                    ts.Complete();
                }

                return pNovedadCesantias;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionCesantiasBusiness", "CrearNovedadCesantias", ex);
                return null;
            }
        }


    }
}
