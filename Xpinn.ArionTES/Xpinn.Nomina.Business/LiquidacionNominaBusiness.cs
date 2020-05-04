using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;
using System.Linq;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Nomina.Business
{

    public class LiquidacionNominaBusiness : GlobalBusiness
    {

        private LiquidacionNominaData DALiquidacionNomina;

        public LiquidacionNominaBusiness()
        {
            DALiquidacionNomina = new LiquidacionNominaData();
        }

        public bool CrearLiquidacionNomina(LiquidacionNomina liquidacion, List<LiquidacionNominaDetalle> listaLiquidacion, List<LiquidacionNominaDetaEmpleado> listaLiquidacionDetalle, List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades, List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedadescargadas, List<ConceptosOpcionesLiquidados> listaConceptosOpcionesLiquidados, Usuario usuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            Int64 codigoliquidacionnominadetalle = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10)))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();

                    if (liquidacion.codigocentrocosto == null || liquidacion.codigocentrocosto < 0)
                    {
                        liquidacion.codigocentrocosto = 0;
                    }
                    if (liquidacion.estado == "D")
                    {
                        entidadop = operacion.GrabarOperacion(pOperacion, usuario);
                        liquidacion.cod_ope = entidadop.cod_ope;
                    }

                    liquidacion = DALiquidacionNomina.CrearLiquidacionNomina(liquidacion, usuario);

                    foreach (LiquidacionNominaDetalle detalleEmpleado in listaLiquidacion)
                    {
                        detalleEmpleado.codigoliquidacionnomina = liquidacion.consecutivo;
                        DALiquidacionNomina.CrearLiquidacionNominaDetalle(detalleEmpleado, usuario);
                    }

                    if (listaLiquidacionDetalle != null)
                    {
                        foreach (LiquidacionNominaDetaEmpleado detalleConcepto in listaLiquidacionDetalle)
                        {
                            detalleConcepto.codigoliquidacionnominadetalle = listaLiquidacion.Where(x => x.codigoempleado == detalleConcepto.codigoempleado).Select(x => x.consecutivo).FirstOrDefault();
                            DALiquidacionNomina.CrearLiquidacionNominaDetaEmpleado(detalleConcepto, usuario);
                            codigoliquidacionnominadetalle = detalleConcepto.codigoliquidacionnominadetalle;

                        }
                    }


                   if (listaLiquidacionNovedades != null)
                    {
                        foreach (LiquidacionNominaNoveEmpleado novedad2 in listaLiquidacionNovedades)
                        {

                            novedad2.codigoliquidacionnominadetalle = listaLiquidacionNovedades.Where(x => x.codigoempleado == novedad2.codigoempleado).Select(x => x.consecutivo).FirstOrDefault();
                            // DALiquidacionNomina.CrearLiquidacionNominaDetaEmpleado2(novedad2, usuario);
                        }
                    }

                    if (listaLiquidacionNovedadescargadas != null)
                    {
                        foreach (LiquidacionNominaNoveEmpleado novedadcargada in listaLiquidacionNovedadescargadas)
                        {
                            //novedadcargada.codigoliquidacionnominadetalle = listaLiquidacionNovedades.Where(x => x.codigoempleado == novedadcargada.codigoempleado).Select(x => x.consecutivo).FirstOrDefault();
                            //DALiquidacionNomina.CrearLiquidacionNominaDetaEmpleado2(novedadcargada, usuario);
                            DALiquidacionNomina.CrearLiquidacionNominaNoveEmpleado(novedadcargada, usuario, codigoliquidacionnominadetalle);
                        }
                    }


                    if (listaConceptosOpcionesLiquidados != null && liquidacion.estado == "D")
                    {

                        foreach (ConceptosOpcionesLiquidados conceptosLiquidados in listaConceptosOpcionesLiquidados)
                        {
                            conceptosLiquidados.fecha = liquidacion.fechainicio;
                            DALiquidacionNomina.AplicarConceptoLiquidado(conceptosLiquidados, usuario);
                        }
                    }
                    //Si está definitiva la liquidacion se inserta en interfaz 
                    if (liquidacion.estado == "D")
                    {
                        if (listaLiquidacionDetalle != null)
                        {
                            LiquidacionNominaDetaEmpleado listaLiquidacionDetalle1 = new LiquidacionNominaDetaEmpleado();
                            DALiquidacionNomina.CrearLiquidacionNominaInterfaz(listaLiquidacionDetalle1, liquidacion, usuario);

                        }
                    }



                    if (liquidacion.estado == "D")
                    {


                        DALiquidacionNomina.CrearGirosDeLiquidacionNomina(liquidacion, usuario);
                    }
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "CrearLiquidacionNomina", ex);
                return false;
            }
        }

        public List<ParConceptosPlanillaLiq> ListarConceptosParametrizadosSegunColumna(int codigoColumna, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarConceptosParametrizadosSegunColumna(codigoColumna, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarConceptosParametrizadosSegunColumna", ex);
                return null;
            }
        }

        public ParColumnasPlanillaLiq ConsultarParametrizacionColumnas(int codigoColumna, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ConsultarParametrizacionColumnas(codigoColumna, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarParametrizacionColumnas", ex);
                return null;
            }
        }

        public bool CrearParametrizacionColumnasLiquidacion(ParColumnasPlanillaLiq parametrizacion, List<ParConceptosPlanillaLiq> listaConceptos, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionNomina.CrearParametrizacionColumnasLiquidacion(parametrizacion, usuario);

                    DALiquidacionNomina.EliminarParConceptosPlanillaLiqDeUnaColumna(parametrizacion, usuario);

                    foreach (ParConceptosPlanillaLiq concepto in listaConceptos)
                    {
                        DALiquidacionNomina.CrearParConceptosPlanillaLiq(concepto, usuario);
                    }

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "CrearParametrizacionColumnasLiquidacion", ex);
                return false;
            }
        }

        public LiquidacionNomina ModificarLiquidacionNomina(LiquidacionNomina pLiquidacionNomina, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiquidacionNomina = DALiquidacionNomina.ModificarLiquidacionNomina(pLiquidacionNomina, pusuario);

                    ts.Complete();

                }

                return pLiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ModificarLiquidacionNomina", ex);
                return null;
            }
        }


        public void EliminarLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionNomina.EliminarLiquidacionNomina(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "EliminarLiquidacionNomina", ex);
            }
        }


        public void EliminarNovedadesNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionNomina.EliminarNovedadesNomina(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "EliminarNovedadesNomina", ex);
            }
        }

        public LiquidacionNomina ConsultarLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = DALiquidacionNomina.ConsultarLiquidacionNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarLiquidacionNomina", ex);
                return null;
            }
        }

        public Nomina_Entidad ConsultarDatosLiquidacion(Int64 pId, Usuario pusuario, Int64 pCodEMpleado)
        {
            try
            {
                Nomina_Entidad LiquidacionNomina = new Nomina_Entidad();
                LiquidacionNomina = DALiquidacionNomina.ConsultarDatosLiquidacion(pId, pusuario, pCodEMpleado);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarDatosLiquidacion", ex);
                return null;
            }
        }



        public List<LiquidacionNomina> ListarLiquidacionNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNomina", ex);
                return null;
            }
        }


        public List<LiquidacionNomina> ListarReportesNomina(LiquidacionNomina liquidacion, ref string pError, Usuario pusuario)
        {
            try
            {
                return DALiquidacionNomina.ListarReportesNomina(liquidacion,  ref pError, pusuario);

            }

            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarReportesNomina", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetalle> ListarLiquidacionNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaDetalle(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaDetalle", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaConceptos(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaConceptos(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaConceptos", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedades(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaNovedades(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaNovedades", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetaEmpleado> ListarLiquidacionNominaNovedadesRecibo(long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaNovedadesRecibo(codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaNovedadesRecibo", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesAplicadas(long consecutivo, long codigoEmpleado, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaNovedadesAplicadas(consecutivo, codigoEmpleado, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaNovedadesAplicadas", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodas(Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaNovedadesTodas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaNovedadesTodas", ex);
                return null;
            }
        }

        public List<LiquidacionNominaNoveEmpleado> ListarLiquidacionNominaNovedadesTodasAplicadas(Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarLiquidacionNominaNovedadesTodasAplicadas(usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarLiquidacionNominaNovedadesTodasAplicadas", ex);
                return null;
            }
        }

        public bool ConsultarSiConceptoEsHoraExtra(long codigoConcepto, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ConsultarSiConceptoEsHoraExtra(codigoConcepto, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarSiConceptoEsHoraExtra", ex);
                return false;
            }
        }

        public int ConsultarUnidadConceptoNomina(long codigoConcepto, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ConsultarUnidadConceptoNomina(codigoConcepto, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarUnidadConceptoNomina", ex);
                return 0;
            }
        }

        public bool VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar(fechaInicio, fechaFinal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "VerificarQueNoHallaUnaLiquidacionPreviaParaEstasFechasALiquidar", ex);
                return false;
            }
        }

        public LiquidacionNominaDetaEmpleado CalcularValorConceptoNominaDeUnEmpleado(int codigoconcepto, long codigoempleado, long codigoNomina, DateTime fechaInicio, DateTime fechaFin, decimal cantidad, decimal valor, Usuario usuario, Int16 origen)
        {
            try
            {
                return DALiquidacionNomina.CalcularValorConceptoNominaDeUnEmpleado(codigoconcepto, codigoempleado, codigoNomina, fechaInicio, fechaFin, cantidad, valor, usuario, origen);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "CalcularValorConceptoNominaDeUnEmpleado", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacion(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)
        {
            try
            {
                if (listaEmpleados == null)
                {
                    return DALiquidacionNomina.GenerarLiquidacionDefinitiva(liquidacion, usuario);
                }
                else
                {
                    List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                    List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();

                    foreach (Empleados empleados in listaEmpleados)
                    {
                        Tuple<LiquidacionNominaDetalle, LiquidacionNominaDetaEmpleado> tuple = DALiquidacionNomina.GenerarLiquidacionPorEmpleado(liquidacion, empleados, usuario);

                        liquidacionDetalle.Add(tuple.Item1);
                        liquidacionDetalleEmpleados.Add(tuple.Item2);
                    }

                    // No necesito llevar el control de los codigos de los conceptos que liquide ya que esta liquidacion es meramente informativa
                    // La informacion de esos codigos conceptos liquidados es para marcarlos como pagados al crear la liquidacion
                    return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados, default(List<ConceptosOpcionesLiquidados>));
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "GenerarLiquidacion", ex);
                return null;
            }
        }


        public LiquidacionNomina ConsultarUltimaFechaLiquidacionNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = DALiquidacionNomina.ConsultarUltimaFechaLiquidacionNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarUltimaFechaLiquidacionNomina", ex);
                return null;
            }
        }

        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticipos(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)
        {
            try
            {
                if (listaEmpleados == null)
                {
                    return DALiquidacionNomina.GenerarAnticipos(liquidacion, usuario);
                }
                else
                {
                    List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                    List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();

                    foreach (Empleados empleados in listaEmpleados)
                    {
                        Tuple<LiquidacionNominaDetalle, LiquidacionNominaDetaEmpleado> tuple = DALiquidacionNomina.GenerarAncticipsPorEmpleado(liquidacion, empleados, usuario);

                        liquidacionDetalle.Add(tuple.Item1);
                        liquidacionDetalleEmpleados.Add(tuple.Item2);
                    }

                    // No necesito llevar el control de los codigos de los conceptos que liquide ya que esta liquidacion es meramente informativa
                    // La informacion de esos codigos conceptos liquidados es para marcarlos como pagados al crear la liquidacion
                    return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados);
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "GenerarAnticipos", ex);
                return null;
            }
        }
        public Tuple<List<LiquidacionNominaDetalle>, List<LiquidacionNominaDetaEmpleado>> GenerarAnticiposInd(LiquidacionNomina liquidacion, List<Empleados> listaEmpleados, Usuario usuario)
        {
            try
            {
                if (listaEmpleados == null)
                {
                    return DALiquidacionNomina.GenerarAnticiposInd(liquidacion, usuario);
                }
                else
                {
                    List<LiquidacionNominaDetalle> liquidacionDetalle = new List<LiquidacionNominaDetalle>();
                    List<LiquidacionNominaDetaEmpleado> liquidacionDetalleEmpleados = new List<LiquidacionNominaDetaEmpleado>();

                    foreach (Empleados empleados in listaEmpleados)
                    {
                        Tuple<LiquidacionNominaDetalle, LiquidacionNominaDetaEmpleado> tuple = DALiquidacionNomina.GenerarAncticipsPorEmpleado(liquidacion, empleados, usuario);

                        liquidacionDetalle.Add(tuple.Item1);
                        liquidacionDetalleEmpleados.Add(tuple.Item2);
                    }

                    // No necesito llevar el control de los codigos de los conceptos que liquide ya que esta liquidacion es meramente informativa
                    // La informacion de esos codigos conceptos liquidados es para marcarlos como pagados al crear la liquidacion
                    return Tuple.Create(liquidacionDetalle, liquidacionDetalleEmpleados);
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "GenerarAnticipos", ex);
                return null;
            }
        }

        public bool CrearAnticiposNomina(LiquidacionNomina liquidacion, List<LiquidacionNominaDetalle> listaLiquidacion, Usuario usuario, Xpinn.Tesoreria.Entities.Operacion pOperacion)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10)))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();

                    entidadop = operacion.GrabarOperacion(pOperacion, usuario);
                    foreach (LiquidacionNominaDetalle detalleEmpleado in listaLiquidacion)
                    {
                        detalleEmpleado.codigoliquidacionnomina = liquidacion.consecutivo;
                        liquidacion.salario = detalleEmpleado.salario;
                        liquidacion.porcentaje_anticipo = detalleEmpleado.porcentaje_anticipo;
                        liquidacion.valor_anticipo = detalleEmpleado.valor_anticipo;
                        liquidacion.porcentaje_anticipo_sub = detalleEmpleado.porcentaje_anticipo_sub;

                        liquidacion.valor_anticipo_sub = detalleEmpleado.valor_anticipo_sub;
                        liquidacion.dias_liquidados = detalleEmpleado.dias;
                        liquidacion.cod_ope = entidadop.cod_ope;
                        liquidacion.codigoempleado = detalleEmpleado.codigoempleado;

                        liquidacion = DALiquidacionNomina.CrearAnticiposNomina(liquidacion, usuario);
                        if (liquidacion.valor_anticipo > 0)
                        {
                            liquidacion = DALiquidacionNomina.CrearAnticiposNominaInterfaz(liquidacion, usuario);
                        }
                        if (liquidacion.valor_anticipo_sub > 0)
                        {
                            liquidacion = DALiquidacionNomina.CrearAnticiposNominaInterfazSubsidio(liquidacion, usuario);
                        }

                    }
                    if (liquidacion.cod_ope > 0)
                    {
                        DALiquidacionNomina.CrearGirosDeAnticiposNomina(liquidacion, usuario);

                    }
                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "CrearAnticiposNomina", ex);
                return false;
            }
        }

        public List<LiquidacionNomina> ListarAnticiposNomina(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionNomina.ListarAnticiposNomina(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarAnticiposNomina", ex);
                return null;
            }
        }

        public List<LiquidacionNomina> ListarAnticiposNominaInd(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionNomina.ListarAnticiposNominaInd(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarAnticiposNominaInd", ex);
                return null;
            }
        }

        public LiquidacionNomina ConsultarAnticiposNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = DALiquidacionNomina.ConsultarAnticiposNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarAnticiposNomina", ex);
                return null;
            }
        }

        public List<LiquidacionNominaDetalle> ListarAnticiposNominaDetalle(LiquidacionNomina liquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionNomina.ListarAnticiposNominaDetalle(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ListarAnticiposNominaDetalle", ex);
                return null;
            }
        }


        public LiquidacionNomina ConsultarUltimaFechaAnticiposNomina(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = DALiquidacionNomina.ConsultarUltimaFechaAnticiposNomina(pId, pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarUltimaFechaAnticiposNomina", ex);
                return null;
            }
        }

        public LiquidacionNomina ConsultarUltimoAnticiposNomina(Usuario pusuario)
        {
            try
            {
                LiquidacionNomina LiquidacionNomina = new LiquidacionNomina();
                LiquidacionNomina = DALiquidacionNomina.ConsultarUltimoAnticiposNomina(pusuario);
                return LiquidacionNomina;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "ConsultarUltimoAnticiposNomina", ex);
                return null;
            }
        }


        public bool CrearNovedadesNomina(List<LiquidacionNominaNoveEmpleado> listaLiquidacionNovedades, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, TimeSpan.FromMinutes(10)))
                {



                    if (listaLiquidacionNovedades != null)
                    {

                        foreach (LiquidacionNominaNoveEmpleado novedad in listaLiquidacionNovedades)
                        {
                            DALiquidacionNomina.CrearLiquidacionNominaNoveEmpleado(novedad, usuario, 0);
                        }
                    }

                    ts.Complete();
                }

                return true;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionNominaBusiness", "CrearNovedadesNomina", ex);
                return false;
            }
        }




    }
}