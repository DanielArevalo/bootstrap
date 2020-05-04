using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Nomina.Data;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Business
{

    public class LiquidacionVacacionesEmpleadoBusiness : GlobalBusiness
    {

        private LiquidacionVacacionesEmpleadoData DALiquidacionVacacionesEmpleado;

        public LiquidacionVacacionesEmpleadoBusiness()
        {
            DALiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleadoData();
        }

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
            Double valorapagar = 0;
            DateTime fecha;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    Xpinn.Tesoreria.Data.OperacionData operacion = new Tesoreria.Data.OperacionData();
                    Xpinn.Tesoreria.Entities.Operacion entidadop = new Tesoreria.Entities.Operacion();                 
                    entidadop = operacion.GrabarOperacion(pOperacion, usuario);


                    liquidacion.cod_ope = entidadop.cod_ope;
                    // Guardamos la liquidacion
                    liquidacion = DALiquidacionVacacionesEmpleado.CrearLiquidacionVacacionesEmpleado(liquidacion, usuario);

                    Int64 consecutivo = liquidacion.consecutivo;


                    // Guardamos el detalle del empleado en la liquidacion
                    foreach (LiquidacionVacacionesDetalleEmpleado detalleEmpleado in listaDetalleEmplado)
                    {
                        detalleEmpleado.codigoliquidacionvacacionesemp = consecutivo;
                    

                        DALiquidacionVacacionesEmpleado.CrearLiquidacionVacacionesDetalleEmpleado(detalleEmpleado, usuario);
                        
                        //INTERFAZ 
                        liquidacion.cod_concepto =  detalleEmpleado.codigoConcepto;
                        liquidacion.valor = detalleEmpleado.valor;
                        DALiquidacionVacacionesEmpleado.CrearLiquidContratoNominaInterfaz(liquidacion, usuario);
                        valorapagar = Convert.ToDouble(liquidacion.valortotalpagar);


                    }

                    // Aplicamos los conceptos de nomina liquidados
                    // Estos conceptos hacen referencia a las novedades que tenga el empleado en el periodo trabajado antes de salir a vacaciones (SI LO HAY)
                    // Ejemplo: Las horas extras que pudo hacer el empleado antes de salir de vacaciones (Si la persona sale de vacaciones el 15, significa que ha trabajado del 1 al 14 y puede tener novedades)
                    if (listaConceptosNominaLiquidados != null)
                    {
                        LiquidacionNominaData DALiquidacionNomina = new LiquidacionNominaData();
                        foreach (ConceptosOpcionesLiquidados conceptosLiquidados in listaConceptosNominaLiquidados)
                        {
                            DALiquidacionNomina.AplicarConceptoLiquidado(conceptosLiquidados, usuario);
                        }
                    }

                    // INSERTAR EL GIRO
                    Xpinn.FabricaCreditos.Data.CreditoData DAgiro = new Xpinn.FabricaCreditos.Data.CreditoData();
                    Xpinn.FabricaCreditos.Entities.Credito egiro = new Xpinn.FabricaCreditos.Entities.Credito();
                    fecha = liquidacion.fechaPago;
                    pGiro.fec_reg = liquidacion.fechaPago;

                    egiro.fecha_desembolso = liquidacion.fechaPago;
                    DAgiro.GuardarGiro(egiro.numero_radicacion, liquidacion.cod_ope, Convert.ToInt32(pGiro.forma_pago), Convert.ToDateTime(egiro.fecha_desembolso), Convert.ToDouble(pGiro.valor), Convert.ToInt32(pGiro.idctabancaria), pGiro.cod_banco, pGiro.num_cuenta, pGiro.tipo_cuenta, Convert.ToInt64(pGiro.cod_persona), usuario.nombre, usuario);



                    ts.Complete();
                }

                return liquidacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "CrearLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ModificarLiquidacionVacacionesEmpleado(LiquidacionVacacionesEmpleado pLiquidacionVacacionesEmpleado, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pLiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ModificarLiquidacionVacacionesEmpleado(pLiquidacionVacacionesEmpleado, pusuario);

                    ts.Complete();

                }

                return pLiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ModificarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }


        public void EliminarLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DALiquidacionVacacionesEmpleado.EliminarLiquidacionVacacionesEmpleado(pId, pusuario);

                    ts.Complete();

                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "EliminarLiquidacionVacacionesEmpleado", ex);
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarLiquidacionVacacionesEmpleado(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarUltLiquidacionVacacionesEmpleado(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarUltLiquidacionVacacionesEmpleado(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarUltLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }


        public List<LiquidacionVacacionesEmpleado> ListarLiquidacionVacacionesEmpleado(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionVacacionesEmpleado.ListarLiquidacionVacacionesEmpleado(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ListarLiquidacionVacacionesEmpleado", ex);
                return null;
            }
        }

        public List<LiquidacionVacacionesDetalleEmpleado> ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(long codigoLiquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionVacacionesEmpleado.ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion(codigoLiquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ListarLiquidacionVacacionesEmpleadoDeUnaLiquidacion", ex);
                return null;
            }
        }

        public bool VerificarSiExisteVacacionesParaEstasFechas(long codigoEmpleado, DateTime fechaInicio, DateTime fechaFinal, Usuario usuario)
        {
            try
            {
                return DALiquidacionVacacionesEmpleado.VerificarSiExisteVacacionesParaEstasFechas(codigoEmpleado, fechaInicio, fechaFinal, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "VerificarSiExisteVacacionesParaEstasFechas", ex);
                return false;
            }
        }

        public Tuple<List<LiquidacionVacacionesDetalleEmpleado>, List<ConceptosOpcionesLiquidados>> GenerarLiquidacionVacacionesParaUnEmpleado(LiquidacionVacacionesEmpleado liquidacion, Usuario usuario)
        {
            try
            {
                return DALiquidacionVacacionesEmpleado.GenerarLiquidacionVacacionesParaUnEmpleado(liquidacion, usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "GenerarLiquidacionVacacionesParaUnEmpleado", ex);
                return null;
            }
        }

        public LiquidacionVacacionesEmpleado ConsultarLiquidacionVacacionesEmpleadoXCodigo(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarLiquidacionVacacionesEmpleadoXCodigo(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarLiquidacionVacacionesEmpleadoXCodigo", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarDiasVacaciones(Int64 pId, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarDiasVacaciones(pId, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarDiasVacaciones", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ConsultarPagaVacacionesAnticipadas(Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarPagaVacacionesAnticipadas( pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarPagaVacacionesAnticipadas", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado CrearDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiasvacacionesEmpleados = DALiquidacionVacacionesEmpleado.CrearDiasVacacionesEmpleados(pDiasvacacionesEmpleados, pusuario);

                    ts.Complete();

                }

                return pDiasvacacionesEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "CrearDiasVacacionesEmpleados", ex);
                return null;
            }
        }


        public LiquidacionVacacionesEmpleado ModificarDiasVacacionesEmpleados(LiquidacionVacacionesEmpleado pDiasvacacionesEmpleados, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pDiasvacacionesEmpleados = DALiquidacionVacacionesEmpleado.ModificarDiasVacacionesEmpleados(pDiasvacacionesEmpleados, pusuario);

                    ts.Complete();

                }

                return pDiasvacacionesEmpleados;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "ModificarDiasVacacionesEmpleados", ex);
                return null;
            }
        }

        public List<LiquidacionVacacionesEmpleado> ListarDiasVacaciones(string filtro, Usuario pusuario)
        {
            try
            {
                return DALiquidacionVacacionesEmpleado.ListarDiasVacaciones(filtro, pusuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("HorasExtrasEmpleadosBusiness", "ListarDiasVacaciones", ex);
                return null;
            }
        }



        public LiquidacionVacacionesEmpleado ConsultarDiasVacacionesNovedades(Int64 pId, DateTime fechainicial, DateTime fechafinal, Usuario pusuario)
        {
            try
            {
                LiquidacionVacacionesEmpleado LiquidacionVacacionesEmpleado = new LiquidacionVacacionesEmpleado();
                LiquidacionVacacionesEmpleado = DALiquidacionVacacionesEmpleado.ConsultarDiasVacacionesNovedades(pId, fechainicial, fechafinal, pusuario);
                return LiquidacionVacacionesEmpleado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("LiquidacionVacacionesEmpleadoBusiness", "ConsultarDiasVacacionesNovedades", ex);
                return null;
            }
        }
    }

}