using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Transactions;
using Xpinn.Util;
using Xpinn.Tesoreria.Data;
using Xpinn.Tesoreria.Entities;
using System.Web;
using System.Web.UI.WebControls;

namespace Xpinn.Tesoreria.Business
{
    public class InventariosBusiness : GlobalBusiness
    {
        private InventariosData DAInventarios;

        /// <summary>
        /// Constructor del objeto de negocio para TransaccionCaja
        /// </summary>
        public InventariosBusiness()
        {
            DAInventarios = new InventariosData();
        }

        public List<ivcategoria> ListarCategoriasProductos(ivcategoria pIVCategoria, string pFiltro, Usuario pUsuario)
        {
            List<ivcategoria> lstProducto = new List<ivcategoria>();

            try
            { 
                lstProducto = DAInventarios.ListarCategoriasProductos(pIVCategoria, pFiltro, pUsuario);
            }
            catch
            {
                return null;
            }

            try
            {
                foreach (ivcategoria item in lstProducto)
                {
                    string _cod_cuenta = "";
                    string _cod_cuenta_niif = "";
                    string _cod_cuenta_ingreso = "";
                    string _cod_cuenta_gasto = "";
                    DAInventarios.ConsultarParametroContable(Convert.ToInt64(item.id_categoria), ref _cod_cuenta, ref _cod_cuenta_niif, ref _cod_cuenta_ingreso, ref _cod_cuenta_gasto, pUsuario);
                    item.cod_cuenta = _cod_cuenta;
                    item.cod_cuenta_niif = _cod_cuenta_niif;
                    item.cod_cuenta_ingreso = _cod_cuenta_ingreso;
                    item.cod_cuenta_gasto = _cod_cuenta_gasto;
                }
            }
            catch
            {
                return lstProducto;
            }

            return lstProducto;
        }

        public ParCueInventarios ConsultarParCueInventarios(Int64 pId, Usuario pUsuario)
        {
            ivcategoria _categoria = new ivcategoria();
            _categoria = DAInventarios.ConsultarCategoriasProductos(pId, pUsuario);
            ParCueInventarios _parametros = new ParCueInventarios();
            _parametros = DAInventarios.ConsultarParCueInventarios(pId, pUsuario);
            _parametros.id_categoria = Convert.ToInt64(_categoria.id_categoria);
            _parametros.nombre = _categoria.nombre;
            return _parametros; 
        }

        public ParCueInventarios CrearParCueInventarios(ParCueInventarios pConceptoCta, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoCta = DAInventarios.CrearParCueInventarios(pConceptoCta, pusuario);
                    ts.Complete();
                }
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "CrearParCueInventarios", ex);
                return null;
            }
        }


        public ParCueInventarios ModificarParCueInventarios(ParCueInventarios pConceptoCta, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    pConceptoCta = DAInventarios.ModificarParCueInventarios(pConceptoCta, pusuario);
                    ts.Complete();
                }
                return pConceptoCta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ModificarParCueInventarios", ex);
                return null;
            }
        }


        public void EliminarParCueInventarios(Int64 pId, Usuario pusuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInventarios.EliminarParCueInventarios(pId, pusuario);
                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "EliminarParCueInventarios", ex);
            }
        }

        public List<ivimpuesto> ListarImpuestos(ivimpuesto pIVProducto, string pFiltro, Usuario pUsuario)
        {
            List<ivimpuesto> lstProducto = new List<ivimpuesto>();
            try
            {
                lstProducto = DAInventarios.ListarImpuestos(pIVProducto, "", pUsuario);

            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarImpuestos", ex);
                return null;
            }

            try
            {
                foreach (ivimpuesto item in lstProducto)
                {
                    string _cod_cuenta = "";
                    string _cod_cuenta_niif = "";
                    DAInventarios.ConsultarParametroImpContable(Convert.ToInt64(item.id_impuesto), ref _cod_cuenta, ref _cod_cuenta_niif, pUsuario);
                    item.cod_cuenta = _cod_cuenta;
                    item.cod_cuenta_niif = _cod_cuenta_niif;
                }
            }
            catch
            {
                return lstProducto;
            }

            return lstProducto;
        }

        public ParCueIvimpuesto ModificarParCueIvimpuesto(ParCueIvimpuesto pParCueIvimpuesto, Usuario pUsuario)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required))
                {
                    DAInventarios.ModificarParCueIvimpuesto(pParCueIvimpuesto, pUsuario);
                    ts.Complete();
                }
                return pParCueIvimpuesto;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "EliminarParCueInventarios", ex);
                return null;
            }
        }

        public List<ivmovimiento> ListarMovimiento(ivmovimiento pMovimiento, string pFiltro, Usuario pUsuario)
        {
            List<ivmovimiento> lstProducto = new List<ivmovimiento>();
            try
            {
                lstProducto = DAInventarios.ListarMovimiento(pMovimiento, "", pUsuario);
                foreach (ivmovimiento item in lstProducto)
                {
                    if (item.id_persona != null)
                        item.cod_persona = DAInventarios.ConsultarPersona(item.id_persona, pUsuario);
                    if (item.cod_persona == null)
                    {
                        item.observaciones = "La persona no esta creada";
                    }
                }
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarMovimiento", ex);
                return null;
            }

            return lstProducto;
        }

        public List<ivmovimiento> ContabilizarOperacion(List<ivmovimiento> pLstCpnsulta, ref string pError, Usuario pusuario)
        {
            try
            {                
                foreach (ivmovimiento factura in pLstCpnsulta)
                {                    
                    // Determinar el proceso contable
                    Xpinn.Contabilidad.Entities.ProcesoContable _proceso = new Contabilidad.Entities.ProcesoContable();
                    _proceso = DAInventarios.ProcesoContable(Convert.ToInt64(factura.cod_proceso), pusuario);
                    Int64 _ciudad = Convert.ToInt64(DAInventarios.DeterminarCiudad(pusuario.cod_oficina, pusuario));

                    // Obtener detalle de la factura
                    List<ivmovimientodetalle> lstdetalle = new List<ivmovimientodetalle>();
                    lstdetalle = DAInventarios.ListarDetalleMovimiento(factura.id_movimiento, factura.tipo_movimiento, pusuario);

                    if (lstdetalle != null)
                    {
                        TransactionOptions tranopc = new TransactionOptions();
                        tranopc.Timeout = TimeSpan.MaxValue;
                        using (TransactionScope ts = new TransactionScope(TransactionScopeOption.Required, tranopc))
                        {
                            ///////////////////////////////////////////////////////////////////////
                            // Crear el encabezado del comprobante

                            #region encabezado
                            Xpinn.Contabilidad.Data.ComprobanteData DAComprobante = new Contabilidad.Data.ComprobanteData();
                            Xpinn.Contabilidad.Entities.Comprobante pComprobante = new Contabilidad.Entities.Comprobante();
                            pComprobante.num_comp = 0;
                            pComprobante.tipo_comp = _proceso.tipo_comp;
                            pComprobante.num_consig = factura.id_movimiento.ToString();
                            pComprobante.n_documento = factura.id_movimiento.ToString();
                            pComprobante.fecha = factura.fecha_movimiento;
                            pComprobante.concepto = _proceso.concepto;
                            pComprobante.ciudad = _ciudad;
                            pComprobante.tipo_pago = null;
                            pComprobante.entidad = null;
                            pComprobante.totalcom = factura.total;
                            pComprobante.tipo_benef = "A";
                            pComprobante.cod_benef = Convert.ToInt64(factura.cod_persona);
                            pComprobante.cod_elaboro = pusuario.codusuario;
                            pComprobante.cod_aprobo = pusuario.codusuario;
                            pComprobante.estado = "A";
                            pComprobante.observaciones = "MOVIMIENTO INVENTARIOS. Almacen:" + factura.id_almacen + "-" + factura.almacenname + " Factura No.:" + factura.id_movimiento;
                            pComprobante.cuenta = null;
                            pComprobante = DAComprobante.CrearComprobante(pComprobante, pusuario);
                            #endregion
                            if (pComprobante != null)
                            {
                                ///////////////////////////////////////////////////////////////////////
                                // Crear el detalle del comprobante     
                                ///////////////////////////////////////////////////////////////////////
                                #region detalle                     
                                decimal total = 0;
                                foreach (ivmovimientodetalle item in lstdetalle)
                                {
                                    item.num_comp = pComprobante.num_comp;
                                    item.tipo_comp = pComprobante.tipo_comp;
                                    item.tercero = factura.cod_persona;
                                    item.id_movimiento = factura.id_movimiento;
                                    item.id_tipo_movimiento = factura.tipo_movimiento;
                                    item.factura = factura.numero_factura;
                                    item.codigo = DAInventarios.ContabilizaMovimiento(item, pusuario);
                                    if (item.id_tipo_movimiento == 3)
                                        total -= (item.precio_total + item.valor_impuesto);
                                    else
                                        total += (item.precio_total + item.valor_impuesto);
                                }
                                #endregion
                                ///////////////////////////////////////////////////////////////////////
                                // Crear impuestos
                                ///////////////////////////////////////////////////////////////////////
                                #region impuestos de retencion
                                if (factura.LstRetencion != null)
                                {
                                    foreach (ivordenconcepto item in factura.LstRetencion)
                                    {
                                        if (item.valor != 0)
                                        {
                                            Xpinn.Contabilidad.Data.ComprobanteData comprobanteservicioret = new Contabilidad.Data.ComprobanteData();
                                            Xpinn.Contabilidad.Entities.DetalleComprobante _comprobanteret = new Contabilidad.Entities.DetalleComprobante();
                                            _comprobanteret.codigo = 0;
                                            _comprobanteret.num_comp = pComprobante.num_comp;
                                            _comprobanteret.tipo_comp = pComprobante.tipo_comp;
                                            _comprobanteret.cod_cuenta = item.cod_cuenta;
                                            _comprobanteret.moneda = 1;
                                            _comprobanteret.centro_costo = 1;
                                            _comprobanteret.centro_gestion = null;
                                            _comprobanteret.detalle = "Inventarios " + item.nombre;
                                            _comprobanteret.tipo = "C";
                                            _comprobanteret.valor = Convert.ToDecimal(item.valor);
                                            _comprobanteret.tercero = factura.cod_persona;
                                            _comprobanteret.base_comp = factura.total;
                                            _comprobanteret.porcentaje = Convert.ToDecimal(item.porcentaje_calculo);
                                            _comprobanteret.cod_tipo_producto = 0;
                                            _comprobanteret.numero_transaccion = null;
                                            comprobanteservicioret.CrearDetalleComprobante(_comprobanteret, pusuario);
                                            total -= Convert.ToDecimal(item.valor);
                                        }
                                    }
                                }
                                #endregion
                                ///////////////////////////////////////////////////////////////////////
                                // Crear contrapartida
                                ///////////////////////////////////////////////////////////////////////
                                #region contrapartida
                                foreach (ivdatospago item in factura.LstDatosPago)
                                { 
                                    Xpinn.Contabilidad.Data.ComprobanteData comprobanteservicio = new Contabilidad.Data.ComprobanteData();
                                    Xpinn.Contabilidad.Entities.DetalleComprobante _comprobante = new Contabilidad.Entities.DetalleComprobante();
                                    _comprobante.codigo = 0;
                                    _comprobante.num_comp = pComprobante.num_comp;
                                    _comprobante.tipo_comp = pComprobante.tipo_comp;
                                    if (item.id_forma_pago == 0)
                                    {
                                        _comprobante.cod_cuenta = _proceso.cod_cuenta;
                                    }
                                    else
                                    {
                                        string _cod_cuenta = "", _cod_cuenta_niif = "";
                                        DAInventarios.ConsultarParamtroFPagContable(_proceso.cod_proceso, item.id_forma_pago, ref _cod_cuenta, ref _cod_cuenta_niif, pusuario);
                                        _comprobante.cod_cuenta = _cod_cuenta;
                                        _comprobante.cod_cuenta_niif = _cod_cuenta_niif;
                                    }
                                    _comprobante.moneda = 1;
                                    _comprobante.centro_costo = 1;
                                    _comprobante.centro_gestion = null;
                                    _comprobante.detalle = "Inventarios";
                                    _comprobante.tipo = _proceso.tipo_mov == 1 ? "D" : "C";
                                    if (total < 0)
                                    {
                                        if (_comprobante.tipo == "D")
                                            _comprobante.tipo = "C";
                                        else
                                            _comprobante.tipo = "D";
                                    }
                                    if (item.id_forma_pago == 0)
                                    { 
                                        _comprobante.valor = Math.Abs(total);
                                    }
                                    else
                                    { 
                                        _comprobante.valor = Convert.ToDecimal(Math.Abs(total));
                                    }
                                    _comprobante.tercero = factura.cod_persona;
                                    _comprobante.base_comp = 0;
                                    _comprobante.porcentaje = 0;
                                    _comprobante.cod_tipo_producto = 0;
                                    _comprobante.numero_transaccion = null;
                                    comprobanteservicio.CrearDetalleComprobante(_comprobante, pusuario);
                                }
                                #endregion
                                ///////////////////////////////////////////////////////////////////////
                                // Crear el giro para la orden de compra
                                ///////////////////////////////////////////////////////////////////////
                                #region crear el giro
                                if (factura.tipo_movimiento == 20)
                                {
                                    Xpinn.Tesoreria.Data.OperacionData DATesoreria = new Xpinn.Tesoreria.Data.OperacionData();
                                    Xpinn.Tesoreria.Entities.Operacion pOperacion = new Xpinn.Tesoreria.Entities.Operacion();
                                    pOperacion.cod_ope = 0;
                                    pOperacion.tipo_ope = 24;
                                    pOperacion.cod_caja = 0;
                                    pOperacion.cod_cajero = 0;
                                    pOperacion.observacion = "Operacion-Inventarios Orden de Compra";
                                    pOperacion.cod_proceso = factura.cod_proceso;
                                    pOperacion.fecha_oper = factura.fecha_movimiento;
                                    pOperacion.fecha_calc = DateTime.Now;
                                    pOperacion.cod_ofi = pusuario.cod_oficina;
                                    pOperacion.num_comp = pComprobante.num_comp;
                                    pOperacion.tipo_comp = pComprobante.tipo_comp;
                                    pOperacion = DATesoreria.GrabarOperacion(pOperacion, pusuario);

                                    if (pOperacion != null)
                                    {
                                        Xpinn.Tesoreria.Data.GiroData GiroData = new Xpinn.Tesoreria.Data.GiroData();
                                        Xpinn.Tesoreria.Entities.Giro pGiro = new Xpinn.Tesoreria.Entities.Giro();
                                        pGiro.idgiro = 0;
                                        pGiro.cod_persona = factura.cod_persona;
                                        pGiro.forma_pago = 1;
                                        pGiro.tipo_acto = 11;
                                        pGiro.fec_reg = factura.fecha_movimiento;
                                        pGiro.fec_giro = DateTime.Now;
                                        pGiro.numero_radicacion = 0;
                                        pGiro.usu_gen = pusuario.nombre;
                                        pGiro.usu_apli = null;
                                        pGiro.estadogi = 0;
                                        pGiro.usu_apro = null;
                                        pGiro.idctabancaria = 0;
                                        pGiro.cod_banco = 0;
                                        pGiro.num_cuenta = "";
                                        pGiro.tipo_cuenta = -1;
                                        pGiro.fec_apro = DateTime.MinValue;
                                        pGiro.cob_comision = 0;
                                        pGiro.cod_ope = pOperacion.cod_ope;
                                        pGiro.valor = Convert.ToInt64(total);
                                        pGiro = GiroData.CrearGiro(pGiro, pusuario, 1);
                                    }
                                }
                                #endregion
                            }
                            factura.num_comp = pComprobante.num_comp;
                            factura.tipo_comp = pComprobante.tipo_comp;
                            ts.Complete();
                        }
                    }
                }
                return pLstCpnsulta;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ContabilizarOperacion", ex);
                return null;
            }
        }

        public List<ivalmacen> ListarAlmacen(ivalmacen pIVAlmacen, Usuario pUsuario)
        {
            try
            {
                return DAInventarios.ListarAlmacen(pIVAlmacen, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarAlmacen", ex);
                return null;
            }
        }

        public List<ivpersonas_autoret> ListarRetencion(string pPerDocumento, decimal pBase, Usuario pUsuario)
        {
            try
            {
                List<ivpersonas_autoret> lstRetencion = new List<ivpersonas_autoret>();
                lstRetencion = DAInventarios.ListarRetencion(pPerDocumento, pBase, pUsuario);
                foreach (ivpersonas_autoret item in lstRetencion)
                {
                    string cod_cuenta = "";
                    string cod_cuenta_niif = "";
                    DAInventarios.ConsultarParametroImpContable(item.tipo_retencion, ref cod_cuenta, ref cod_cuenta_niif, pUsuario);
                    item.cod_cuenta = cod_cuenta;

                }
                return lstRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarRetencion", ex);
                return null;
            }
        }


        public List<ivconceptos> ListarConceptos(ivconceptos pConceptos, Usuario pUsuario)
        {
            List<ivconceptos> LstConceptos = new List<ivconceptos>();
            try
            {
                LstConceptos = DAInventarios.ListarConceptos(pConceptos, pUsuario);
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarConceptos", ex);
                return null;
            }
            try
            {
                foreach (ivconceptos item in LstConceptos)
                {
                    string _cod_cuenta = "";
                    string _cod_cuenta_niif = "";
                    DAInventarios.ConsultarParametroConContable(Convert.ToInt64(item.id_concepto), ref _cod_cuenta, ref _cod_cuenta_niif, pUsuario);
                    item.cod_cuenta = _cod_cuenta;
                    item.cod_cuenta_niif = _cod_cuenta_niif;
                }
            }
            catch
            {
                return LstConceptos;
            }
            return LstConceptos;
        }

        public List<ivordenconcepto> ListarOrdenConceptos(Int64 pidOrden, double pBase, Usuario pUsuario)
        {
            try
            {
                List<ivordenconcepto> lstRetencion = new List<ivordenconcepto>();
                lstRetencion = DAInventarios.ListarOrdenConceptos(pidOrden, pBase, pUsuario);
                foreach (ivordenconcepto item in lstRetencion)
                {
                    string cod_cuenta = "";
                    string cod_cuenta_niif = "";
                    DAInventarios.ConsultarParametroConContable(item.id_concepto, ref cod_cuenta, ref cod_cuenta_niif, pUsuario);
                    item.cod_cuenta = cod_cuenta;
                    item.cod_cuenta_niif = cod_cuenta_niif;
                }
                return lstRetencion;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("InventariosBusiness", "ListarOrdenConceptos", ex);
                return null;
            }
        }

        public List<ivdatospago> ListarDatosPago(Int64 pCodProceso, Int64 pIdMovimiento, Usuario pUsuario)
        {
            List<ivdatospago> lstdatos = new List<ivdatospago>();
            lstdatos = DAInventarios.ListarDatosPago(pIdMovimiento, pUsuario);
            if (lstdatos != null)
            { 
                foreach (ivdatospago item in lstdatos)
                {
                    string cod_cuenta = "", cod_cuenta_niif = "";
                    DAInventarios.ConsultarParamtroFPagContable(pCodProceso, item.id_forma_pago, ref cod_cuenta, ref cod_cuenta_niif, pUsuario);
                }
            }
            return lstdatos;
        }


    }
}
