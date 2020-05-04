using System;
using System.Collections.Generic;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Asesores.Data;
using Xpinn.Asesores.Entities;
using Xpinn.ActivosFijos.Entities;

namespace Xpinn.Asesores.Business
{
    public class GarantiasBusiness : GlobalBusiness
    {
        private GarantiaData garantiaData;

        public GarantiasBusiness(){
            garantiaData = new GarantiaData();
        }

        public List<Garantia> ListarGarantia(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            try{
                return garantiaData.Listar(pNumeroRadicacion, pUsuario);
            }
            catch (Exception ex){
                BOExcepcion.Throw("GarantiasBusiness", "ListarGarantias", ex);
                return null;
            }
        }

        public List<Garantia> ListarSinGarantias(string filtro, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ListarSinGarantias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ListarGarantias", ex);
                return null;
            }
        }

        /// <summary>
        /// Obtiene la lista de Garantias Maestro dados unos filtros
        /// </summary>
        /// <param name="pTransaccionCaja">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Garantias obtenidas CRUD</returns>
        public List<Garantia> ListarFullGarantias(string filtro, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ListarFullGarantias(filtro, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ListarFullGarantias", ex);
                return null;
            }
        }

        public List<Garantia> Listaractivos(string cod_persona, Usuario pUsuario)
        {
            try
            {
                return garantiaData.Listaractivos(cod_persona, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ListarFullGarantias", ex);
                return null;
            }
        }

        public string ConsultarCliente(string nradicacion, Usuario _usuario)
        {
            try
            {
                return garantiaData.ConsultarCliente(nradicacion, _usuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ConsultarCliente", ex);
                return null;
            }
        }


        public ActivoFijo CrearActivoFijoPersonal(ActivoFijo pActivosFijos, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivosFijos.estado = 1;
                    pActivosFijos = garantiaData.CrearActivoFijoPersonal(pActivosFijos, pUsuario);

                    ts.Complete();
                }

                return pActivosFijos;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "CrearActivoFijoPersonal", ex);
                return null;
            }
        }

        public bool EliminarActivoFijo(Int64 pId, Int64 pNum_Radicacion, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    bool valor = false;
                    valor = garantiaData.EliminarActivoFijo(pId, pNum_Radicacion, ref pError, pUsuario);
                    ts.Complete();

                    return valor;
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "EliminarActivoFijo", ex);
                return false;
            }
        }

        public ActivoFijo ConsultarActivoFijoPersonal(long idActivoFijo, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ConsultarActivoFijoPersonal(idActivoFijo, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ConsultarActivoFijoPersonal", ex);
                return null;
            }
        }

        public void EliminarGarantia(long pId, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    garantiaData.EliminarGarantia(pId, pUsuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "EliminarGarantia", ex);
            }
        }

        /// <summary>
        /// Crea una Garantia
        /// </summary>
        /// <param name="pEntity">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia CrearGarantia(Garantia pGarantia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    // CREAR OPERACION 
                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                    pOperacion.cod_ope = 0;
                    pOperacion.tipo_ope = 137;
                    pOperacion.fecha_oper = pGarantia.FechaGarantia;
                    pOperacion.fecha_calc = pGarantia.FechaGarantia;
                    pOperacion.observacion = "Operacion - Contabilización de Garantias " + DateTime.Now;

                    Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, pUsuario);
                    if (pOperacion == null)
                    {
                        ts.Dispose();
                        return null;
                    }
                    if (pOperacion.cod_ope != 0)
                    {
                        // REGISTRANDO LA GARANTIA
                        pGarantia.cod_ope = pOperacion.cod_ope;
                        pGarantia = garantiaData.InsertarGarantia(pGarantia, pUsuario);
                    }

                    // GENERAR CONTABILIZACION
                    Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();
                    Int64 pnum_comp = 0, ptipo_comp = 0;
                    string pError = string.Empty;

                    // CONSULTANDO PROCESO CONTABLE
                    List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProcesos = comprobanteData.ConsultaProcesoUlt(pGarantia.cod_ope, 137, pGarantia.FechaGarantia, pUsuario);
                    long pProcesoCont = lstProcesos != null && lstProcesos.Count > 0 ? Convert.ToInt64(lstProcesos[0].cod_proceso) : 0;

                    if (comprobanteData.GenerarComprobante(pGarantia.cod_ope, 137, pGarantia.FechaGarantia, pUsuario.cod_oficina, Convert.ToInt64(pGarantia.cod_persona), pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, pUsuario))
                    {
                        pGarantia.num_comp = pnum_comp;
                        pGarantia.tipo_comp = ptipo_comp;
                    }
                    else
                    {
                        // Si no pudo generar el comprobante enviar error
                        if (pError.Trim() != "")
                        {
                            ts.Dispose();
                            return null;
                        }
                    }
                    ts.Complete();
                }

                return pGarantia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "CrearGarantia", ex);
                return null;
            }
        }


        /// <summary>
        /// Modificar una Garantia
        /// </summary>
        /// <param name="pEntity">Entidad Garantia</param>
        /// <returns>Entidad creada</returns>
        public Garantia ModificarGarantia(Int16 origen,Garantia pGarantia, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pGarantia.cod_ope == 0)
                    {
                        // CREAR OPERACION 
                        Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                        pOperacion.cod_ope = 0;
                        pOperacion.tipo_ope = 137;
                        pOperacion.fecha_oper = pGarantia.FechaGarantia;
                        pOperacion.fecha_calc = pGarantia.FechaGarantia;
                        pOperacion.observacion = "Operacion - Contabilización de Garantias ";

                        Xpinn.Tesoreria.Data.OperacionData DAOperacion = new Xpinn.Tesoreria.Data.OperacionData();
                        pOperacion = DAOperacion.GrabarOperacion(pOperacion, pUsuario);
                        if (pOperacion == null)
                        {
                            ts.Dispose();
                            return null;
                        }
                        pGarantia.cod_ope = pOperacion.cod_ope;
                    }

                    if (pGarantia.cod_ope != 0)
                    {
                        // GENERAR LA NOTA 
                        // MODIFICAR LA GARANTIA
                        // PRIMERO GENERO LA NOTA PARA TENER EL DATO DEL VALOR ACTUAL DE LA GARANTIA, SI MODIFICO PRIMERO PIERDO ESTE DATO.
                        // RECUPERAR EN ENTIDAD GARANTIA EL COMPROBANTE Y EL NUMERO DE COMPROBANTE

                        // CONSULTANDO PROCESO CONTABLE
                        Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();
                        List<Xpinn.Contabilidad.Entities.ProcesoContable> lstProcesos = comprobanteData.ConsultaProcesoUlt(pGarantia.cod_ope, 137, pGarantia.FechaGarantia, pUsuario);
                        long pProcesoCont = lstProcesos != null && lstProcesos.Count > 0 ? Convert.ToInt64(lstProcesos[0].cod_proceso) : 0;
                        pGarantia.cod_proceso = pProcesoCont;

                        pGarantia = garantiaData.GenerarCompModificarGarantia(origen,pGarantia, pUsuario);
                    }

                    ts.Complete();
                }

                return pGarantia;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ModificarGarantia", ex);
                return null;
            }
        }


        /// <summary>
        /// Obtiene una Garantia
        /// </summary>
        /// <param name="pId">identificador del Cajero</param>
        /// <returns>Caja consultada</returns>
        public Garantia ConsultarGarantia(Int64 pId, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ConsultarGarantia(pId, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ConsultarGarantia", ex);
                return null;
            }
        }

        public void ModificarActivoFijoPersonal(ActivoFijo pActivoFijo, Usuario usuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pActivoFijo = garantiaData.ModificarActivoFijoPersonal(pActivoFijo, usuario);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ModificarActivoFijoPersonal", ex);
            }
        }

        public Garantia ConsultarCreditoCliente(Int64 pnum, Usuario pUsuario)
        {
            try
            {
                return garantiaData.ConsultarCreditoCliente(pnum, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("GarantiasBusiness", "ConsultarCreditoCliente", ex);
                return null;
            }
        }

    }
}
