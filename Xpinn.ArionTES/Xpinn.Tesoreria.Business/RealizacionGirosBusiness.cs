using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Business
{
    /// <summary>
    /// Objeto de negocio para AreasCaj
    /// </summary>
    public class RealizacionGirosBusiness : GlobalBusiness
    {
        private RealizacionGirosData DARealizacion;

        /// <summary>
        /// Constructor del objeto de negocio para AreasCaj
        /// </summary>
        public RealizacionGirosBusiness()
        {
            DARealizacion = new RealizacionGirosData();
        }


        public List<Giro> ListarGiroAprobados(Giro pGiro, String Orden, DateTime pFechaGiro, DateTime pFechaAprobacion, Boolean Forma_Pago, Usuario vUsuario)
        {
            try
            {
                return DARealizacion.ListarGiroAprobados(pGiro, Orden, pFechaGiro, pFechaAprobacion,Forma_Pago, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosBusiness", "ListarGiroAprobados", ex);
                return null;
            }
        }

        protected Int64 GrabarOperacion( DateTime Fecha,Usuario vUsuario)
        {
            //CREACION DE LA OPERACION
            OperacionData DAOperacion = new OperacionData();
            Operacion vOpe = new Operacion();
            vOpe.cod_ope = 0;
            vOpe.tipo_ope = 103;
            vOpe.cod_caja = 0;
            vOpe.cod_cajero = 0;
            vOpe.observacion = "Operacion-Realización de Giro";
            vOpe.cod_proceso = null;
            vOpe.fecha_oper = Fecha;
            vOpe.fecha_calc = DateTime.Now;
            vOpe.cod_ofi = vUsuario.cod_oficina;
            vOpe = DAOperacion.GrabarOperacion(vOpe, vUsuario);
            return vOpe.cod_ope;
        }

        public List<Xpinn.Tesoreria.Entities.Operacion> RealizarGiro(bool pParametro, Giro pGiroTot, DateTime Fecha, Int64 pProcesoCont, Boolean rptaArchivo, String NombreArchivo, ref string pError, Usuario vUsuario)
        {
            Xpinn.Contabilidad.Business.ComprobanteBusiness comprobanteBusiness = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
            Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();
            List<Operacion> lstOperacion = new List<Operacion>();
            Int64 codOpe = 0;
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pGiroTot.lstGiro != null && pGiroTot.lstGiro.Count > 0)
                    {
                        //codOpe = vOpe.cod_ope;
                        if (pParametro == false)
                            codOpe = GrabarOperacion(Fecha, vUsuario);

                        Int64 pnum_comp = 0, ptipo_comp = 0;

                        foreach (Giro nGiro in pGiroTot.lstGiro)
                        {
                            //GRABAR OPERACION SI SE REQUIERE POR CADA GIRO.
                            if (pParametro == true)
                                codOpe = GrabarOperacion(Fecha, vUsuario);
                            //ACTUALIZAR TABLA GIRO
                            Giro pEntidad = new Giro();
                            nGiro.estado = 2;
                            pEntidad = DARealizacion.RealizarGiro(nGiro, vUsuario);
                            //INSERTAR EN LA TABLA GIRO REALIZADO
                            GiroRealizado pRealizado = new GiroRealizado();
                            GiroRealizado pGiroRealizado = new GiroRealizado();
                            pGiroRealizado.idgiro = pEntidad.idgiro;
                            pGiroRealizado.fec_realiza = Fecha;
                            pGiroRealizado.usu_realiza = vUsuario.nombre;
                            pGiroRealizado.cod_ope = codOpe;
                            if (rptaArchivo == true)
                            {
                                pGiroRealizado.archivo = NombreArchivo;
                            }
                            else
                            {
                                pGiroRealizado.archivo = null;
                            }
                            //INSERTAR EN TABLA COMO GIRO REALIZADO.
                            pRealizado = DARealizacion.CrearGiroRealizado(pGiroRealizado, vUsuario);
                            //GENERAR DATOS DE COMPROBANTE.
                            pnum_comp = 0; ptipo_comp = 0;
                            if (pParametro == true)
                            {
                                //CREANDO EL COMPROBANTE
                                if (comprobanteBusiness.GenerarComprobanteSinCommit(codOpe, 103, Fecha, vUsuario.cod_oficina, Convert.ToInt64(nGiro.cod_persona), pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, vUsuario))
                                {
                                    Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                                    oper.cod_ope = codOpe;
                                    oper.num_comp = pnum_comp;
                                    oper.tipo_comp = ptipo_comp;
                                    //consultar el cheque del comprobante creado...
                                    oper.num_cheque = null;
                                    Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Contabilidad.Entities.Comprobante();
                                    vComprobante = comprobanteData.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                                    if (vComprobante != null && vComprobante.n_documento != null)
                                        oper.num_cheque = vComprobante.n_documento;
                                    lstOperacion.Add(oper);
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
                            }
                        }
                        if (pParametro == false)
                        {
                            pnum_comp = 0; ptipo_comp = 0;
                            //CREANDO EL COMPROBANTE
                            if (comprobanteBusiness.GenerarComprobanteSinCommit(codOpe, 103, Fecha, vUsuario.cod_oficina, 0, pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, vUsuario))
                            {
                                Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                                oper.cod_ope = codOpe;
                                oper.num_comp = pnum_comp;
                                oper.tipo_comp = ptipo_comp;
                                //consultar el cheque del comprobante creado...
                                oper.num_cheque = null;
                                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Contabilidad.Entities.Comprobante();
                                vComprobante = comprobanteData.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                                if (vComprobante != null && vComprobante.n_documento != null)
                                    oper.num_cheque = vComprobante.n_documento;
                                lstOperacion.Add(oper);
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
                        }
                    }
                    ts.Complete();
                }
                return lstOperacion;
            }
            catch (Exception ex)
            {
                // BOExcepcion.Throw("RealizacionGirosBusiness", "AprobarGiro", ex);
                pError = ex.Message;
                return null;
            }
        }


        public List<Xpinn.Tesoreria.Entities.Operacion> RealizarGiroOtros(Giro pGiroTot, DateTime Fecha,Int64 pProcesoCont,ref string pError, Usuario vUsuario)
        {
            Xpinn.Contabilidad.Business.ComprobanteBusiness comprobanteBusiness = new Xpinn.Contabilidad.Business.ComprobanteBusiness();
            Xpinn.Contabilidad.Data.ComprobanteData comprobanteData = new Xpinn.Contabilidad.Data.ComprobanteData();
            Int64 codOpe = 0;
            List<Operacion> lstOperacion = new List<Operacion>();
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    if (pGiroTot.lstGiro != null && pGiroTot.lstGiro.Count > 0)
                    {
                        foreach (Giro nGiro in pGiroTot.lstGiro)
                        {
                            //CREACION DE LA OPERACION
                            OperacionData DAOperacion = new OperacionData();
                            Operacion vOpe = new Operacion();
                            vOpe.cod_ope = 0;
                            vOpe.tipo_ope = 103;
                            vOpe.cod_caja = 0;
                            vOpe.cod_cajero = 0;
                            vOpe.observacion = "Operacion-Realización de Giro";
                            vOpe.cod_proceso = null;
                            vOpe.fecha_oper = Fecha;
                            vOpe.fecha_calc = DateTime.Now;
                            vOpe.cod_ofi = vUsuario.cod_oficina;
                            vOpe = DAOperacion.GrabarOperacion(vOpe, vUsuario);
                            codOpe = vOpe.cod_ope;

                            //ACTUALIZAR TABLA GIRO
                            Giro pEntidad = new Giro();
                            nGiro.estado = 2;
                            pEntidad = DARealizacion.RealizarGiro(nGiro, vUsuario);

                            //INSERTAR EN LA TABLA GIRO REALIZADO
                            GiroRealizado pRealizado = new GiroRealizado();
                            GiroRealizado pGiroRealizado = new GiroRealizado();
                            pGiroRealizado.idgiro = pEntidad.idgiro;
                            pGiroRealizado.fec_realiza = Fecha;
                            pGiroRealizado.usu_realiza = vUsuario.nombre;
                            pGiroRealizado.cod_ope = vOpe.cod_ope;
                            pGiroRealizado.archivo = null;                            
                            pRealizado = DARealizacion.CrearGiroRealizado(pGiroRealizado, vUsuario);

                            Int64 pnum_comp = 0, ptipo_comp = 0;
                            //CREANDO EL COMPROBANTE
                            if (comprobanteBusiness.GenerarComprobanteSinCommit(codOpe, 103, Fecha, vUsuario.cod_oficina, Convert.ToInt64(nGiro.cod_persona), pProcesoCont, ref pnum_comp, ref ptipo_comp, ref pError, vUsuario))
                            {
                                Xpinn.Tesoreria.Entities.Operacion oper = new Xpinn.Tesoreria.Entities.Operacion();
                                oper.cod_ope = codOpe;
                                oper.num_comp = pnum_comp;
                                oper.tipo_comp = ptipo_comp;
                                //consultar el cheque del comprobante creado...
                                oper.num_cheque = null;
                                Xpinn.Contabilidad.Entities.Comprobante vComprobante = new Contabilidad.Entities.Comprobante();
                                vComprobante = comprobanteData.ConsultarComprobante(pnum_comp, ptipo_comp, vUsuario);
                                if (vComprobante != null && vComprobante.n_documento != null)
                                    oper.num_cheque = vComprobante.n_documento;
                                lstOperacion.Add(oper);
                            }
                        }

                    }
                    ts.Complete();
                }
                return lstOperacion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosBusiness", "RealizarGiroOtros", ex);
                return null;
            }
        }


        public void ReemplazarConsultaSQL(string pConsulta, ref string pResult, ref string pError, Usuario vUsuario)
        {
            try
            {
                DARealizacion.ReemplazarConsultaSQL(pConsulta,ref pResult,ref pError, vUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RealizacionGirosBusiness", "ReemplazarConsultaSQL", ex);                
            }
        }



    }
}