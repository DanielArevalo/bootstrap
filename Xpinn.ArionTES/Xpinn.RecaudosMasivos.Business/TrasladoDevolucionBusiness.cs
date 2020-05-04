using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;


namespace Xpinn.Tesoreria.Business
{
    public class TrasladoDevolucionBusiness : GlobalData
    {

        private TrasladoDevolucionData BATraslado;

        public TrasladoDevolucionBusiness()
        {
            BATraslado = new TrasladoDevolucionData();
        }


        public TrasladoDevolucion Crear_TrasladoDevolucion(TrasladoDevolucion pTraslado, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pTraslado = BATraslado.Crear_TrasladoDevolucion(pTraslado, vUsuario);

                    ts.Complete();
                }
                return pTraslado;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoDevolucionBusiness", "Crear_TrasladoDevolucion", ex);
                return null;
            }
        }


        public void Crear_TrasladoDevolucionALL(ref Int64 pCOd_OPE, Operacion pOperacion, List<TrasladoDevolucion> lstTraslado, Xpinn.FabricaCreditos.Entities.Giro pGiro, Usuario vUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABAR LA OPERACION
                    OperacionData DAOperacion = new OperacionData();
                    pOperacion = DAOperacion.GrabarOperacion(pOperacion, vUsuario);
                    pCOd_OPE = pOperacion.cod_ope;

                    //GRABAR LAS TRANSACCIONES
                    foreach (TrasladoDevolucion pTrans in lstTraslado)
                    {
                        pTrans.cod_ope = pCOd_OPE;
                        TrasladoDevolucion pTranslado = new TrasladoDevolucion();
                        pTranslado = BATraslado.Crear_TrasladoDevolucion(pTrans, vUsuario);
                    }
                   
                    //GRABACION DEL GIRO
                    pGiro.cod_ope = pCOd_OPE;
                    Xpinn.FabricaCreditos.Data.AvanceData DAGiro = new FabricaCreditos.Data.AvanceData();
                    Xpinn.FabricaCreditos.Entities.Giro pGiroEnti = new FabricaCreditos.Entities.Giro();
                    pGiroEnti = DAGiro.CrearGiro(pGiro, vUsuario, 1);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("TrasladoDevolucionBusiness", "Crear_TrasladoDevolucionALL", ex);
            }
        }
          


        public List<TrasladoDevolucion> ListarTrasladoDevolucion(String orden,string filtro, Usuario vUsuario)
        {
            try
            {
                return BATraslado.ListarTrasladoDevolucion(orden,filtro, vUsuario);
            }
            catch
            {
                return null;
            }
        }


        public List<TrasladoDevolucion> ConsultarTrasladoDevolucion(Int64 cod_persona, Usuario vUsuario)
        {
            try
            {
                return BATraslado.ConsultarTrasladoDevolucion(cod_persona, vUsuario);
            }
            catch
            {
                return null;
            }
        }



        public Int64 ObtenerSiguienteCodigo(Usuario pUsuario)
        {
            try
            {
                return BATraslado.ObtenerSiguienteCodigo(pUsuario);
            }
            catch
            {
                return 1;
            }
        }


        public List<TrasladoDevolucion> ListarDevolucionesMenores(String orden, string filtro, decimal valor_menor, Usuario vUsuario)
        {
            try
            {
                return BATraslado.ListarDevolucionesMenores(orden, filtro, valor_menor, vUsuario);
            }
            catch
            {
                return null;
            }
        }



        public List<TrasladoDevolucion> ListarDevolucionesMasivas(String orden, string filtro, Usuario vUsuario)
        {
            try
            {
                return BATraslado.ListarDevolucionesMasivas(orden, filtro,  vUsuario);
            }
            catch
            {
                return null;
            }
        }
        public List<TrasladoDevolucion> ListarDevolucionesPersona(String pIdentificacion, Usuario pUsuario)
        {
            try
            {
                return BATraslado.ListarDevolucionesPersona(pIdentificacion, pUsuario);
            }
            catch
            {
                return null;
            }
        }

        public bool TrasladoDevolucionesMenores(DateTime pFecha, List<TrasladoDevolucion> plstTraslado, int pTipoOpe, ref Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    //GRABAR LA OPERACION
                    OperacionData DAOperacion = new OperacionData();
                    Operacion eOperacion = new Operacion();
                    eOperacion.tipo_ope = pTipoOpe;
                    eOperacion.fecha_real = DateTime.Now;
                    eOperacion.fecha_oper = pFecha;
                    eOperacion.fecha_calc = pFecha;
                    eOperacion.cod_ofi = pUsuario.cod_oficina;
                    eOperacion.cod_usu = pUsuario.codusuario;
                    eOperacion.estado = 1;
                    eOperacion.observacion = "Traslado de Devoluciones Menores";
                    eOperacion = DAOperacion.GrabarOperacion(eOperacion, pUsuario);
                    pCodOpe = eOperacion.cod_ope;

                    //GRABAR LAS TRANSACCIONES
                    foreach (TrasladoDevolucion pTrans in plstTraslado)
                    {
                        pTrans.cod_ope = pCodOpe;
                        BATraslado.Crear_TrasladoDevolucion(pTrans, pUsuario);
                    }

                    ts.Complete();                    
                }
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return false;
            }

            return true;
        }





        public bool AplicacionMasivaDevoluciones(DateTime pFecha, List<TrasladoDevolucion> plstDevoluciones, int pTipoOpe, ref Int64 pCodOpe, ref string pError, Usuario pUsuario)
        {
            try
            {

                // Crear la operación de las aplicaciones
                OperacionData DAOperacion = new OperacionData();
                Operacion eOperacion = new Operacion();
                eOperacion.tipo_ope = pTipoOpe;
                eOperacion.fecha_real = DateTime.Now;
                eOperacion.fecha_oper = pFecha;
                eOperacion.fecha_calc = pFecha;
                eOperacion.cod_ofi = pUsuario.cod_oficina;
                eOperacion.cod_usu = pUsuario.codusuario;
                eOperacion.estado = 1;
                eOperacion.observacion = "Aplicacion Masiva Devoluciones";
                eOperacion = DAOperacion.GrabarOperacion(eOperacion, pUsuario);
                pCodOpe = eOperacion.cod_ope;
 
                // Aplicar cada una de las devoluciones
                foreach (TrasladoDevolucion pTrans in plstDevoluciones)
                {
                    pTrans.cod_ope = pCodOpe;
                    BATraslado.Crear_AplicacionDevolucion(pTrans, pUsuario);
                }
            }
            catch (Exception ex)
            {
                pError += ex.Message;
                return false;
            }

            return true;
        }


    }
}


