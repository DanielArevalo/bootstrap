using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xpinn.Util;
using System.Data;
using System.Data.Common;
using Xpinn.FabricaCreditos.Entities;
using Xpinn.Tesoreria.Entities;

namespace Xpinn.Tesoreria.Data
{
    public class RecaudosMasivosData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        public RecaudosMasivosData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public void AplicarPago(RecaudosMasivos recaudosmasivos, Int64 pcod_ope, Usuario pUsuario, Int64 pnerror, ref string Error)
        {
            pnerror = 0;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        if (recaudosmasivos.numero_producto == "") pn_num_producto.Value = DBNull.Value; else pn_num_producto.Value = recaudosmasivos.numero_producto;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        if (recaudosmasivos.cod_cliente == null) pn_cod_cliente.Value = DBNull.Value; else pn_cod_cliente.Value = recaudosmasivos.cod_cliente;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_ope = cmdTransaccionFactory.CreateParameter();
                        pn_cod_ope.ParameterName = "pn_cod_ope";
                        if (pcod_ope == Int64.MinValue) pn_cod_ope.Value = DBNull.Value; else pn_cod_ope.Value = pcod_ope;
                        pn_cod_ope.DbType = DbType.Int64;
                        pn_cod_ope.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        if (recaudosmasivos.fecha_aplicacion == null) pf_fecha_pago.Value = DateTime.MinValue; else pf_fecha_pago.Value = recaudosmasivos.fecha_aplicacion;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        if (recaudosmasivos.valor == decimal.MinValue) pn_valor_pago.Value = DBNull.Value; else pn_valor_pago.Value = recaudosmasivos.valor;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_num_cuotas = cmdTransaccionFactory.CreateParameter();
                        pn_num_cuotas.ParameterName = "pn_num_cuotas";
                        if (recaudosmasivos.num_cuotas == decimal.MinValue) pn_num_cuotas.Value = DBNull.Value; else pn_num_cuotas.Value = recaudosmasivos.num_cuotas;
                        pn_num_cuotas.DbType = DbType.Double;
                        pn_num_cuotas.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";

                        if (recaudosmasivos.tipo_producto == "ahorro programado" || recaudosmasivos.tipo_producto.ToUpper() == "AHORRO PROGRAMADO")
                        {
                            pn_tipo_tran.Value = 305;
                        }
                        if (recaudosmasivos.tipo_producto == "Ahorros a la Vista" || recaudosmasivos.tipo_producto.ToUpper() == "AHORROS A LA VISTA" || recaudosmasivos.tipo_producto.ToUpper() == "DEPOSITOS")
                        {
                            pn_tipo_tran.Value = 203;
                        }
                        if (recaudosmasivos.tipo_producto == "Aportes" || recaudosmasivos.tipo_producto.ToUpper() == "APORTES")
                        {
                            if (recaudosmasivos.tipo_tran == 0)
                                pn_tipo_tran.Value = 101;
                            else
                                pn_tipo_tran.Value = recaudosmasivos.tipo_tran;
                        }
                        if (recaudosmasivos.tipo_producto == "Creditos - Cuotas Extras" || recaudosmasivos.tipo_producto.ToUpper().StartsWith("CREDITOS - CUOTAS EXTRAS"))
                        {
                            pn_tipo_tran.Value = 12;
                        }
                        if (recaudosmasivos.tipo_producto == "Creditos" || recaudosmasivos.tipo_producto.ToUpper() == "CREDITOS")
                        {
                            if (recaudosmasivos.tipo_aplicacion == "Pago Total")
                                pn_tipo_tran.Value = 2;
                            else if (recaudosmasivos.tipo_aplicacion == "Por Valor a Capital")
                                pn_tipo_tran.Value = 32;
                            else if (recaudosmasivos.tipo_aplicacion == "Abono a Capital")
                                pn_tipo_tran.Value = 6;
                            else
                                pn_tipo_tran.Value = 4;
                        }
                        if (recaudosmasivos.tipo_producto == "Servicios" || recaudosmasivos.tipo_producto.ToUpper() == "SERVICIOS")
                        {
                            pn_tipo_tran.Value = 36;
                        }
                        if (recaudosmasivos.tipo_producto == "Devolucion" || recaudosmasivos.tipo_producto.ToUpper() == "DEVOLUCION")
                        {
                            pn_tipo_tran.Value = 904;
                        }
                        if (recaudosmasivos.tipo_producto == "Afiliacion" || recaudosmasivos.tipo_producto.ToUpper() == "AFILIACION")
                        {
                            pn_tipo_tran.Value = 402;
                        }
                        pn_tipo_tran.DbType = DbType.Int64;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        if (pUsuario.codusuario == Int64.MinValue) pn_cod_usu.Value = DBNull.Value; else pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_recuado = cmdTransaccionFactory.CreateParameter();
                        pn_cod_recuado.ParameterName = "pn_cod_recuado";
                        if (recaudosmasivos.iddetalle == Int64.MinValue) pn_cod_recuado.Value = DBNull.Value; else pn_cod_recuado.Value = recaudosmasivos.iddetalle;
                        pn_cod_recuado.DbType = DbType.Int64;
                        pn_cod_recuado.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        if (recaudosmasivos.sobrante == decimal.MinValue) rn_sobrante.Value = DBNull.Value; else rn_sobrante.Value = recaudosmasivos.sobrante;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        DbParameter n_error = cmdTransaccionFactory.CreateParameter();
                        n_error.ParameterName = "n_error";
                        if (pnerror == Int64.MinValue) n_error.Value = DBNull.Value; else n_error.Value = pnerror;
                        n_error.DbType = DbType.Int64;
                        n_error.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_ope);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_num_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_recuado);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);
                        cmdTransaccionFactory.Parameters.Add(n_error);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_APLICARPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }

        public void Validar(RecaudosMasivos recaudos, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_num_producto = cmdTransaccionFactory.CreateParameter();
                        pn_num_producto.ParameterName = "pn_num_producto";
                        pn_num_producto.Value = recaudos.numero_producto;
                        pn_num_producto.DbType = DbType.String;
                        pn_num_producto.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_cliente = cmdTransaccionFactory.CreateParameter();
                        pn_cod_cliente.ParameterName = "pn_cod_cliente";
                        pn_cod_cliente.Value = recaudos.cod_cliente;
                        pn_cod_cliente.DbType = DbType.Int64;
                        pn_cod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pf_fecha_pago = cmdTransaccionFactory.CreateParameter();
                        pf_fecha_pago.ParameterName = "pf_fecha_pago";
                        pf_fecha_pago.Value = recaudos.fecha_aplicacion;
                        pf_fecha_pago.DbType = DbType.DateTime;
                        pf_fecha_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_valor_pago = cmdTransaccionFactory.CreateParameter();
                        pn_valor_pago.ParameterName = "pn_valor_pago";
                        pn_valor_pago.Value = recaudos.valor;
                        pn_valor_pago.DbType = DbType.Double;
                        pn_valor_pago.Direction = ParameterDirection.Input;

                        DbParameter pn_tipo_tran = cmdTransaccionFactory.CreateParameter();
                        pn_tipo_tran.ParameterName = "pn_tipo_tran";
                        if (recaudos.tipo_producto == "Aportes")
                        {
                            pn_tipo_tran.Value = 101;
                        }
                        if (recaudos.tipo_producto == "Creditos")
                        {
                            pn_tipo_tran.Value = 4;
                        }
                        if (recaudos.tipo_producto == "Devolucion")
                        {
                            pn_tipo_tran.Value = 904;
                        }
                        if (recaudos.tipo_producto == "Afiliacion")
                        {
                            pn_tipo_tran.Value = 402;
                        }
                        pn_tipo_tran.DbType = DbType.Int64;
                        pn_tipo_tran.Direction = ParameterDirection.Input;

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pn_cod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;

                        DbParameter rn_sobrante = cmdTransaccionFactory.CreateParameter();
                        rn_sobrante.ParameterName = "rn_sobrante";
                        rn_sobrante.Value = recaudos.sobrante;
                        rn_sobrante.DbType = DbType.Int64;
                        rn_sobrante.Direction = ParameterDirection.InputOutput;

                        cmdTransaccionFactory.Parameters.Add(pn_num_producto);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pf_fecha_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_valor_pago);
                        cmdTransaccionFactory.Parameters.Add(pn_tipo_tran);
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);
                        cmdTransaccionFactory.Parameters.Add(rn_sobrante);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_VALIDARPAGO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        //BOExcepcion.Throw("", "", ex);
                    }

                }
            }
        }

        public List<EmpresaRecaudo> ListarEmpresaRecaudo(EmpresaRecaudo pEmpresaRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<EmpresaRecaudo> lstEmpresaRecaudo = new List<EmpresaRecaudo>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  EMPRESA_RECAUDO " + ObtenerFiltro(pEmpresaRecaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            EmpresaRecaudo entidad = new EmpresaRecaudo();

                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);

                            lstEmpresaRecaudo.Add(entidad);
                        }

                        return lstEmpresaRecaudo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarEmpresaRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListadoProductosEnMora(DateTime pFecha, RecaudosMasivos ProductosEnMora, string filtros, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstProductosmora = new List<RecaudosMasivos>();
            Configuracion conf = new Configuracion();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = " select * from VReporteproductos " + filtros;


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NOMBRES"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) entidad.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["NOM_LINEA_PRODUCTO"] != DBNull.Value) entidad.nom_linea = Convert.ToString(resultado["NOM_LINEA_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["DIAS_MORA"] != DBNull.Value) entidad.valor_aplicado = Convert.ToInt32(resultado["DIAS_MORA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.num_cuotas = Convert.ToInt32(resultado["CUOTA"]);
                            if (resultado["VALORPAGAR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALORPAGAR"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["OFICINA"]);
                            lstProductosmora.Add(entidad);
                        }

                        return lstProductosmora;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "lstProductosmora", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Mètodo para listar los productos de la persona para poder hacer la distribuciòn
        /// </summary>
        /// <param name="pCodEmpresa"></param>
        /// <param name="pFecha"></param>
        /// <param name="pIdentificacion"></param>
        /// <param name="pConcepto"></param>
        /// <param name="pnumero_producto"></param>
        /// <param name="pUsuario"></param>
        /// <returns></returns>
        public List<Producto> ListarProductos(Int64 pCodEmpresa, DateTime pFecha, string pIdentificacion, string pConcepto, string pnumero_producto, int? pforma_cobro, Usuario pUsuario, int? paplica_mora, int? ptipo_recaudo)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstCredito = new List<Producto>();
            Configuracion conf = new Configuracion();

            try
            {

                if (paplica_mora == 0)
                {
                    using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
                    {
                        using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                        {

                            string sql;
                            string forma_cobro;
                            // --- Aportes
                            forma_cobro = pforma_cobro == 2 ? " a.cuota " : " Calcular_VrAPagarGrupoAporte(a.numero_aporte, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')) ";
                            sql = @"Select 1 As tipo_producto, 'Aportes' As nom_tipo_producto, to_char(a.numero_aporte) As numero_producto, a.cod_persona, p.identificacion, p.nombre, to_char(a.cod_linea_aporte) As cod_linea, a.fecha_proximo_pago, " + forma_cobro + @" As valor_a_pagar, 0 As total_a_pagar,
                                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 1 And e.cod_linea = a.cod_linea_aporte), 9) As prioridad             
                                                    From aporte a Inner Join v_persona p On a.cod_persona = p.cod_persona Where p.identificacion = '" + pIdentificacion + @"' And a.estado = '1'
                                                    And a.cod_linea_aporte Not In (Select g.cod_linea_aporte From Grupo_LineaAporte g Where g.principal = 0)";
                            if (pCodEmpresa != Int32.MinValue)
                                sql += " And a.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And a.numero_aporte = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And a.cod_linea_aporte In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = 1 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";

                            // --- Crèditos
                            forma_cobro = pforma_cobro == 2 ? " c.valor_cuota " : " Calcular_VrAPagar(c.numero_radicacion, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')) ";
                            sql += " Union All ";
                            sql += @"Select 2 As tipo_producto, 'Creditos' As nom_tipo_producto, to_char(c.numero_radicacion) As numero_producto, c.cod_deudor, p.identificacion, p.nombre, c.cod_linea_credito As cod_linea, c.fecha_proximo_pago, " + forma_cobro + @" As valor_a_pagar, Calcular_TotalAPagar(c.numero_radicacion, To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + @"')) As total_a_pagar, 
                                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 2 And e.cod_linea = c.cod_linea_credito), 8) As prioridad 
                                                    From credito c Inner Join v_persona p On c.cod_deudor = p.cod_persona And p.identificacion = '" + pIdentificacion + "' And c.estado = 'C' ";
                            if (pCodEmpresa != Int32.MinValue)
                                sql += " And c.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And c.numero_radicacion = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And c.cod_linea_credito In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = 2 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";
                            if (ptipo_recaudo == 1)
                                sql += " And c.forma_pago In ('2', 'N') ";

                            // --- Afiliaciòn
                            forma_cobro = pforma_cobro == 2 ? " Case Nvl(c.cuotas, 0) When 0 Then c.saldo Else Round(c.valor/c.cuotas) End " : " c.saldo ";
                            sql += " Union All ";
                            sql += @"Select 6 As tipo_producto, 'Afiliacion' As nom_tipo_producto, to_char(c.idafiliacion) As numero_producto, c.cod_persona, p.identificacion, p.nombre, '' As cod_linea, c.fecha_proximo_pago, c.saldo As valor_a_pagar, c.saldo As total_a_pagar, 
                                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 6), 7) As prioridad 
                                                    From persona_afiliacion c Inner Join v_persona p On c.cod_persona = p.cod_persona And p.identificacion = '" + pIdentificacion + "' And c.estado = 'A' ";
                            if (pCodEmpresa != Int32.MinValue)
                                sql += " And c.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And c.idafiliacion = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And 6 In (Select e.tipo_producto From empresa_recaudo_concepto e Where e.tipo_producto = 6 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";

                            // --- Ahorros a la vista 
                            forma_cobro = pforma_cobro == 2 ? "0 " : " a.saldo_total";
                            sql += " Union All ";
                            sql += @"Select 3 As tipo_producto, 'ahorros a la vista' As nom_tipo_producto, a.numero_cuenta As numero_producto, a.cod_persona, p.identificacion, p.nombre, to_char(a.cod_linea_ahorro) As cod_linea, a.fecha_proximo_pago, " + forma_cobro + @" As valor_a_pagar, 0 As total_a_pagar,
                                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 3 And e.cod_linea = a.cod_linea_ahorro), 9) As prioridad 
                                                    From ahorro_vista a Inner Join v_persona p On a.cod_persona = p.cod_persona And p.identificacion = '" + pIdentificacion + "' And a.estado = '1'";
                            // if (pCodEmpresa != Int32.MinValue)
                            //sql += " And a.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And a.numero_cuenta = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And a.cod_linea_ahorro In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = 3 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";

                            // --- Servicios
                            forma_cobro = pforma_cobro == 2 ? " c.valor_cuota " : " c.valor_cuota ";
                            sql += " Union All ";
                            sql += @"Select 4 As tipo_producto, 'Servicios' As nom_tipo_producto, to_char(c.numero_servicio) As numero_producto, c.cod_persona, p.identificacion, p.nombre, c.cod_linea_servicio As cod_linea, c.fecha_proximo_pago, " + forma_cobro + @" As valor_a_pagar, c.saldo As total_a_pagar, 
                                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 4 And e.cod_linea = c.cod_linea_servicio), 8) As prioridad 
                                                    From servicios c Inner Join v_persona p On c.cod_persona = p.cod_persona And p.identificacion = '" + pIdentificacion + "' And c.estado = 'C' And c.saldo != 0 ";
                            if (pCodEmpresa != Int32.MinValue)
                                sql += " And c.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And c.numero_servicio = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And c.cod_linea_servicio In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = 4 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";
                            if (ptipo_recaudo == 1)
                                sql += " And c.forma_pago In ('2', 'N') ";

                            sql += " Union All ";
                            sql += @"Select 9 As tipo_producto, 'Ahorro Programado' As nom_tipo_producto, to_char(c.numero_programado) As numero_producto, c.cod_persona, p.identificacion, p.nombre, c.cod_linea_programado as cod_linea, c.fecha_proximo_pago,  c.valor_cuota As valor_a_pagar, c.valor_cuota as total_a_pagar,
                                                Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = 9 And e.cod_linea = c.cod_linea_programado), 999) As prioridad
                                                From ahorro_programado c Inner Join v_persona p On c.cod_persona = p.cod_persona And p.identificacion = '" + pIdentificacion + "' And c.estado = '1'";
                            if (pCodEmpresa != Int32.MinValue)
                                sql += " And c.cod_empresa = " + pCodEmpresa;
                            if (pnumero_producto != "")
                                sql += " And c.numero_programado = " + pnumero_producto;
                            if (pConcepto.Trim() != "")
                                sql += " And c.cod_linea_programado In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = 9 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )";

                            sql += " Order by 11, 8";

                            connection.Open();
                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                Producto entidad = new Producto();

                                if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                                if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                                if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                                if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                                if (resultado["TOTAL_A_PAGAR"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["TOTAL_A_PAGAR"]);
                                if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                                if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt64(resultado["PRIORIDAD"]);

                                // Determinar personas en vacaciones

                                lstCredito.Add(entidad);
                            }
                        }
                    }
                }



                if (paplica_mora == 1)
                {
                    using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
                    {
                        connection.Open();

                        using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                        {

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                            DbParameter p_empresa = cmdTransaccionFactory.CreateParameter();
                            p_empresa.ParameterName = "P_EMPRESA";
                            p_empresa.Value = pCodEmpresa;
                            p_empresa.Direction = ParameterDirection.Input;
                            p_empresa.DbType = DbType.Int64;
                            cmdTransaccionFactory.Parameters.Add(p_empresa);

                            DbParameter p_cod_persona = cmdTransaccionFactory.CreateParameter();
                            p_cod_persona.ParameterName = "p_cod_persona";
                            p_cod_persona.Value = Convert.ToInt64(pIdentificacion);
                            p_cod_persona.Direction = ParameterDirection.Input;
                            p_cod_persona.DbType = DbType.Int64;
                            cmdTransaccionFactory.Parameters.Add(p_cod_persona);

                            DbParameter p_fecha_corte = cmdTransaccionFactory.CreateParameter();
                            p_fecha_corte.ParameterName = "p_fecha_corte";
                            p_fecha_corte.Value = pFecha.ToString(conf.ObtenerFormatoFecha());
                            p_fecha_corte.Direction = ParameterDirection.Input;
                            p_fecha_corte.DbType = DbType.DateTime;
                            cmdTransaccionFactory.Parameters.Add(p_fecha_corte);


                            cmdTransaccionFactory.CommandText = "XPF_PRIORIDAD_MORAS";
                            cmdTransaccionFactory.ExecuteNonQuery();

                        };


                        using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                        {
                            cmdTransaccionFactory.Connection = connection;


                            string sql;
                            sql = @"Select tipo_producto, nombre_producto, numero_producto, v.cod_persona, v.identificacion, v.nombre, p.linea, p.PERIODO fecha_proximo_pago, p.TOTAL valor_a_pagar, 0 total_a_pagar, prioridad From prioridad_productos p 
                                    Inner join v_persona v on p.cod_persona = v.cod_persona  And v.cod_persona = '" + pIdentificacion + "' Where periodo <= To_Date('" + pFecha.ToString(conf.ObtenerFormatoFecha()) + "','" + conf.ObtenerFormatoFecha() + "')";

                            cmdTransaccionFactory.Connection = connection;
                            cmdTransaccionFactory.CommandType = CommandType.Text;
                            cmdTransaccionFactory.CommandText = sql;
                            resultado = cmdTransaccionFactory.ExecuteReader();

                            while (resultado.Read())
                            {
                                Producto entidad = new Producto();

                                if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                                if (resultado["nombre_producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["nombre_producto"]);
                                if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                                if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                                if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                                if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                                if (resultado["LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["LINEA"]);
                                if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                                if (resultado["TOTAL_A_PAGAR"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["TOTAL_A_PAGAR"]);
                                if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                                if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt64(resultado["PRIORIDAD"]);

                                // Determinar personas en vacaciones

                                lstCredito.Add(entidad);
                            }

                            dbConnectionFactory.CerrarConexion(connection);

                        }

                    }
                }

                return lstCredito;
            }
            catch (Exception ex)
            {
                BOExcepcion.Throw("RecaudoMasivosData", "ListarProductos", ex);
                return null;
            }


        }

        public List<Producto> ListarProductosNovedad(Int64 pCodEmpresa, DateTime pFecha, string pIdentificacion, string pConcepto, string pnumero_producto, Int64 pNumeroNovedad, int? paplicar_refinanciados, Usuario pUsuario, int? paplica_mora)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstCredito = new List<Producto>();
            Configuracion conf = new Configuracion();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql;
                        sql = @"Select CODIGOPRODUCTO(a.tipo_producto) As tipo_producto, a.tipo_producto As nom_tipo_producto, a.numero_producto, a.cod_cliente, p.identificacion, p.nombre, a.cod_linea_producto As cod_linea, '' As fecha_proximo_pago, a.valor As valor_a_pagar, a.valor As total_a_pagar,
                                    Nvl((Select e.prioridad From empresa_recaudo_concepto e Where e.cod_empresa = " + pCodEmpresa + @" And e.tipo_producto = CODIGOPRODUCTO(a.tipo_producto) And e.cod_linea = a.cod_linea_producto), 0) As prioridad ,vacaciones
                                    From empresa_novedad e Inner Join detempresa_novedad a On e.numero_novedad = a.numero_novedad Inner Join v_persona p On a.cod_cliente = p.cod_persona And p.identificacion = '" + pIdentificacion + @"' 
                                    Where a.numero_novedad = " + pNumeroNovedad.ToString() + " And a.estado = '1'";
                        if (pCodEmpresa != Int32.MinValue)
                            sql += " And e.cod_empresa = " + pCodEmpresa;
                        if (pnumero_producto != "")
                            sql += " And a.numero_producto = " + pnumero_producto;
                        if (pConcepto.Trim() != "")
                            sql += " And ((CODIGOPRODUCTO(a.tipo_producto) != 6 And a.cod_linea_producto In (Select e.cod_linea From empresa_recaudo_concepto e Where e.tipo_producto = CODIGOPRODUCTO(a.tipo_producto) And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' ))" +
                                   "   Or (CODIGOPRODUCTO(a.tipo_producto)  = 6 And 6 In (Select e.tipo_producto From empresa_recaudo_concepto e Where e.tipo_producto = 6 And e.cod_empresa = " + pCodEmpresa + " And e.cod_concepto = '" + pConcepto + "' )) )";

                        sql += " Order by 11, 8";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Producto entidad = new Producto();

                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToInt64(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NOM_TIPO_PRODUCTO"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["NOM_TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.num_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_LINEA"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valor_a_pagar = Convert.ToDecimal(resultado["VALOR_A_PAGAR"]);
                            if (resultado["TOTAL_A_PAGAR"] != DBNull.Value) entidad.total_a_pagar = Convert.ToDecimal(resultado["TOTAL_A_PAGAR"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["PRIORIDAD"] != DBNull.Value) entidad.prioridad = Convert.ToInt64(resultado["PRIORIDAD"]);
                            if (resultado["VACACIONES"] != DBNull.Value) entidad.vacaciones = Convert.ToInt32(resultado["vacaciones"].ToString());

                            entidad.refinanciado = 0;

                            if (paplicar_refinanciados == 1 && entidad.tipo_producto != null && entidad.tipo_producto == 2)
                            {
                                if (entidad.num_producto != null && entidad.num_producto != "")
                                {
                                    try
                                    {
                                        Credito eCredito = new Credito();
                                        eCredito = ConsultarCredito(Convert.ToInt64(entidad.num_producto), pUsuario);
                                        if (eCredito.estado != "C")
                                        {
                                            eCredito = ConsultarCreditoRecoge(Convert.ToInt64(entidad.num_producto), pUsuario);
                                            if (eCredito != null)
                                            {
                                                string sParametro = LineaCreditoRecoge(eCredito.cod_linea_credito, "1", pUsuario);
                                                if (sParametro != "1")
                                                {
                                                    entidad.num_producto = Convert.ToString(eCredito.numero_radicacion);
                                                    entidad.cod_linea = eCredito.cod_linea_credito;
                                                    entidad.fecha_proximo_pago = eCredito.fecha_prox_pago;
                                                    entidad.total_a_pagar = eCredito.saldo_capital;
                                                    entidad.refinanciado = 1;
                                                }
                                            }
                                        }
                                    }
                                    catch { }
                                }
                            }
                            else
                            {
                                // Verificar el estado del aporte
                                if (entidad.tipo_producto == 1)
                                {
                                    if (EstadoProducto(Convert.ToInt32(entidad.tipo_producto), entidad.num_producto, pUsuario) != "1")
                                    {
                                        entidad.tipo_producto = 0;
                                        entidad.nom_tipo_producto = "Devolucion";
                                        entidad.valor_a_pagar = 0;
                                        entidad.total_a_pagar = 0;
                                    }
                                }
                                // Verificar el estado del crédito
                                if (entidad.tipo_producto == 2)
                                {
                                    if (EstadoProducto(Convert.ToInt32(entidad.tipo_producto), entidad.num_producto, pUsuario) == "T")
                                    {
                                        entidad.valor_a_pagar = 0;
                                        entidad.total_a_pagar = 0;
                                    }
                                }
                            }


                            lstCredito.Add(entidad);
                        }

                        return lstCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarProductos", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoDeUnPeriodoYEmpresa(string codigoEmpresaRecaudadora, string fechaPeriodoNovedad, Usuario usuario)
        {
            List<RecaudosMasivos> lista = new List<RecaudosMasivos>();
            DbDataReader resultado = default(DbDataReader);

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Format(@"SELECT dt.*, per.nombre, per.identificacion, per.COD_NOMINA
                                                     from DETEMPRESA_NOVEDAD dt
                                                     join v_persona per on dt.COD_CLIENTE = per.COD_PERSONA
                                                     where dt.NUMERO_NOVEDAD = 
                                                     (
                                                         SELECT MAX(NUMERO_NOVEDAD) 
                                                         FROM EMPRESA_NOVEDAD 
                                                         WHERE COD_EMPRESA = {0}
                                                         AND TRUNC(PERIODO_CORTE) = TRUNC(to_date('{1}', 'dd/MM/yyyy'))
                                                     ) ", codigoEmpresaRecaudadora, fechaPeriodoNovedad);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["iddetalle"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["iddetalle"]);
                            if (resultado["numero_novedad"] != DBNull.Value) entidad.numero_novedad = Convert.ToInt64(resultado["numero_novedad"]);
                            if (resultado["cod_cliente"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["cod_cliente"]);
                            if (resultado["tipo_producto"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["tipo_producto"]);
                            if (resultado["numero_producto"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["numero_producto"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            if (resultado["tipo_novedad"] != DBNull.Value) entidad.tipo_novedad = Convert.ToString(resultado["tipo_novedad"]);
                            if (resultado["estado"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["estado"]);
                            if (resultado["cod_linea_producto"] != DBNull.Value) entidad.cod_linea_producto = Convert.ToInt32(resultado["cod_linea_producto"]);
                            if (resultado["fechacreacion"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["fechacreacion"]);
                            if (resultado["usuariocreacion"] != DBNull.Value) entidad.usuariocreacion = Convert.ToString(resultado["usuariocreacion"]);
                            if (resultado["fecultmod"] != DBNull.Value) entidad.fecultmod = Convert.ToDateTime(resultado["fecultmod"]);
                            if (resultado["usuultmod"] != DBNull.Value) entidad.usuultmod = Convert.ToString(resultado["usuultmod"]);
                            if (resultado["saldo_producto"] != DBNull.Value) entidad.saldo_producto = Convert.ToDecimal(resultado["saldo_producto"]);
                            if (resultado["fecha_proximo_pago"] != DBNull.Value) entidad.fecha_proximo_pago = Convert.ToDateTime(resultado["fecha_proximo_pago"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["nombre"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["identificacion"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);

                            entidad.fecha_recaudo = Convert.ToDateTime(fechaPeriodoNovedad);

                            lista.Add(entidad);
                        }

                        return lista;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleRecaudoDeUnPeriodoYEmpresa", ex);
                        return null;
                    }
                }
            }
        }

        public int ConsultarCuotasPersonaEnVacaciones(long? CodPersona, long cod_pagaduria, string pidentificacion, DateTime fecha, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            int numeroCuotas = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT NUMERO_CUOTAS 
                                        FROM VACACIONES 
                                        WHERE CODIGO_PAGADURIA = " + cod_pagaduria +
                                        @" AND FECHA_NOVEDAD = to_date('" + fecha.ToShortDateString() + @"', 'dd/MM/yyyy')
                                        ";

                        if (!string.IsNullOrWhiteSpace(pidentificacion))
                        {
                            sql += @" and IDENTIFICACION = '" + pidentificacion + "'";
                        }
                        else
                        {
                            sql += @" and COD_PERSONA = " + CodPersona;
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_CUOTAS"] != DBNull.Value) numeroCuotas = Convert.ToInt32(resultado["NUMERO_CUOTAS"]);
                        }

                        return numeroCuotas;
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }

        public string EstadoProducto(int pTipoProducto, string pNumeroProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string estado = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        string sql = "";
                        if (pTipoProducto == 1)
                            sql = "SELECT ESTADO FROM APORTE WHERE NUMERO_APORTE = " + pNumeroProducto + " ";
                        else
                            sql = "SELECT ESTADO FROM CREDITO WHERE NUMERO_RADICACION = " + pNumeroProducto + " ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["ESTADO"] != DBNull.Value) estado = Convert.ToString(resultado["ESTADO"]);
                        }

                        return estado;
                    }
                    catch
                    {
                        return estado;
                    }
                }
            }
        }

        public Persona1 ConsultarPersona(string pIdentificacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Persona1 ePersona = new Persona1();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_PERSONA WHERE IDENTIFICACION = '" + pIdentificacion + "' ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_PERSONA"] != DBNull.Value) ePersona.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NOMBRE"] != DBNull.Value) ePersona.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PERSONA"] != DBNull.Value) ePersona.tipo_persona = Convert.ToString(resultado["TIPO_PERSONA"]);
                            if (resultado["NOMBRES"] != DBNull.Value) ePersona.nombres = Convert.ToString(resultado["NOMBRES"]);
                            if (resultado["APELLIDOS"] != DBNull.Value) ePersona.apellidos = Convert.ToString(resultado["APELLIDOS"]);
                        }

                        return ePersona;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarPersona", ex);
                        return null;
                    }
                }
            }
        }

        public Credito ConsultarCredito(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito eCredito = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM CREDITO WHERE numero_radicacion = " + pNumeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["COD_DEUDOR"] != DBNull.Value) eCredito.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) eCredito.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) eCredito.fecha_prox_pago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) eCredito.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) eCredito.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) eCredito.estado = Convert.ToString(resultado["ESTADO"]);
                        }

                        return eCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarCredito", ex);
                        return null;
                    }
                }
            }
        }


        public Credito ConsultarCreditoRecoge(Int64 pNumeroRadicacion, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Credito eCredito = new Credito();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT C.* FROM CREDITO C INNER JOIN CREDITOSRECOGIDOS R ON C.NUMERO_RADICACION = R.NUMERO_RADICACION WHERE R.NUMERO_RECOGE = " + pNumeroRadicacion.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) eCredito.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) eCredito.cod_linea_credito = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) eCredito.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) eCredito.saldo_capital = Convert.ToDecimal(resultado["SALDO_CAPITAL"]);
                            if (resultado["VALOR_CUOTA"] != DBNull.Value) eCredito.valor_cuota = Convert.ToDecimal(resultado["VALOR_CUOTA"]);
                            if (resultado["ESTADO"] != DBNull.Value) eCredito.estado = Convert.ToString(resultado["ESTADO"]);
                        }

                        return eCredito;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarCreditoRecoge", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Método para crear encabezado de recaudos
        /// </summary>
        /// <param name="recaudosmasivos"></param>
        /// <param name="pcod_ope"></param>
        /// <param name="pUsuario"></param>
        /// <param name="pnerror"></param>
        /// <param name="Error"></param>
        public Int64 CrearRecaudo(RecaudosMasivos recaudosmasivos, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = recaudosmasivos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;
                        pnumero_recaudo.Direction = ParameterDirection.InputOutput;

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = recaudosmasivos.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int64;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = recaudosmasivos.cod_empresa;
                        pcod_empresa.DbType = DbType.Int64;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        pperiodo_corte.Value = recaudosmasivos.periodo_corte;
                        pperiodo_corte.DbType = DbType.DateTime;
                        pperiodo_corte.Direction = ParameterDirection.Input;

                        DbParameter pfecha_recaudo = cmdTransaccionFactory.CreateParameter();
                        pfecha_recaudo.ParameterName = "pfecha_recaudo";
                        pfecha_recaudo.Value = recaudosmasivos.fecha_recaudo;
                        pfecha_recaudo.DbType = DbType.DateTime;
                        pfecha_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pfecha_aplicacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aplicacion.ParameterName = "pfecha_aplicacion";
                        pfecha_aplicacion.Value = recaudosmasivos.fecha_aplicacion;
                        pfecha_aplicacion.DbType = DbType.DateTime;
                        pfecha_aplicacion.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = recaudosmasivos.estado;
                        pestado.DbType = DbType.Int64;
                        pestado.Direction = ParameterDirection.Input;

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "pnumero_novedad";
                        if (recaudosmasivos.numero_novedad == null)
                            pnumero_novedad.Value = DBNull.Value;
                        else
                            pnumero_novedad.Value = recaudosmasivos.numero_novedad;
                        pnumero_novedad.DbType = DbType.Int64;
                        pnumero_novedad.Direction = ParameterDirection.Input;

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        pusuariocreacion.Value = pUsuario.nombre;
                        pusuariocreacion.DbType = DbType.String;
                        pusuariocreacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(pperiodo_corte);
                        cmdTransaccionFactory.Parameters.Add(pfecha_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_aplicacion);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_RECAUDO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        long numeroRecaudo = Convert.ToInt64(pnumero_recaudo.Value);

                        DAauditoria.InsertarLog(recaudosmasivos, "RECAUDO_MASIVO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.RecaudosMasivos, "Creacion de recaudo masivo con numero de recaudo " + numeroRecaudo);

                        return numeroRecaudo;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                        return Int64.MinValue;
                    }

                }
            }
        }


        public Boolean ModificarRecaudo(RecaudosMasivos recaudosmasivos, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = recaudosmasivos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;
                        pnumero_recaudo.Direction = ParameterDirection.Input;

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = recaudosmasivos.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int64;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_empresa = cmdTransaccionFactory.CreateParameter();
                        pcod_empresa.ParameterName = "pcod_empresa";
                        pcod_empresa.Value = recaudosmasivos.cod_empresa;
                        pcod_empresa.DbType = DbType.Int64;
                        pcod_empresa.Direction = ParameterDirection.Input;

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        pperiodo_corte.Value = recaudosmasivos.periodo_corte;
                        pperiodo_corte.DbType = DbType.DateTime;
                        pperiodo_corte.Direction = ParameterDirection.Input;

                        DbParameter pfecha_recaudo = cmdTransaccionFactory.CreateParameter();
                        pfecha_recaudo.ParameterName = "pfecha_recaudo";
                        pfecha_recaudo.Value = recaudosmasivos.fecha_recaudo;
                        pfecha_recaudo.DbType = DbType.DateTime;
                        pfecha_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pfecha_aplicacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aplicacion.ParameterName = "pfecha_aplicacion";
                        pfecha_aplicacion.Value = recaudosmasivos.fecha_aplicacion;
                        pfecha_aplicacion.DbType = DbType.DateTime;
                        pfecha_aplicacion.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = recaudosmasivos.estado;
                        pestado.DbType = DbType.Int64;
                        pestado.Direction = ParameterDirection.Input;

                        DbParameter pnumero_novedad = cmdTransaccionFactory.CreateParameter();
                        pnumero_novedad.ParameterName = "pnumero_novedad";
                        if (recaudosmasivos.numero_novedad == null)
                            pnumero_novedad.Value = DBNull.Value;
                        else
                            pnumero_novedad.Value = recaudosmasivos.numero_novedad;
                        pnumero_novedad.DbType = DbType.Int64;
                        pnumero_novedad.Direction = ParameterDirection.Input;

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        pusuariocreacion.Value = pUsuario.nombre;
                        pusuariocreacion.DbType = DbType.String;
                        pusuariocreacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(ptipo_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_empresa);
                        cmdTransaccionFactory.Parameters.Add(pperiodo_corte);
                        cmdTransaccionFactory.Parameters.Add(pfecha_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_aplicacion);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pnumero_novedad);
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_RECAUDO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                        return false;
                    }

                }
            }
        }


        public Boolean ModificarEstadoRecaudo(int pNumeroRecaudo, DateTime pFechaAplicacion, int pEstado, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = pNumeroRecaudo;
                        pnumero_recaudo.DbType = DbType.Int64;
                        pnumero_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pfecha_aplicacion = cmdTransaccionFactory.CreateParameter();
                        pfecha_aplicacion.ParameterName = "pfecha_aplicacion";
                        pfecha_aplicacion.Value = pFechaAplicacion;
                        pfecha_aplicacion.DbType = DbType.DateTime;
                        pfecha_aplicacion.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = pEstado;
                        pestado.DbType = DbType.Int64;
                        pestado.Direction = ParameterDirection.Input;

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        pusuariocreacion.Value = pUsuario.nombre;
                        pusuariocreacion.DbType = DbType.String;
                        pusuariocreacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pfecha_aplicacion);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_RECAUDO_EST";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                        return false;
                    }

                }
            }
        }


        public Int64 CrearDetalleRecaudo(RecaudosMasivos recaudosmasivos, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "piddetalle";
                        piddetalle.Value = recaudosmasivos.iddetalle;
                        piddetalle.DbType = DbType.Int64;
                        piddetalle.Direction = ParameterDirection.InputOutput;

                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = recaudosmasivos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = recaudosmasivos.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int64;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pcod_cliente";
                        if (recaudosmasivos.cod_cliente == null || recaudosmasivos.cod_cliente == 0)
                            pcod_cliente.Value = DBNull.Value;
                        else
                            pcod_cliente.Value = recaudosmasivos.cod_cliente;
                        pcod_cliente.DbType = DbType.Int64;
                        pcod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        if (recaudosmasivos.periodo_corte == null)
                            pperiodo_corte.Value = DBNull.Value;
                        else
                            pperiodo_corte.Value = recaudosmasivos.periodo_corte;
                        pperiodo_corte.DbType = DbType.DateTime;
                        pperiodo_corte.Direction = ParameterDirection.Input;

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "pidentificacion";
                        if (recaudosmasivos.identificacion == null || recaudosmasivos.identificacion == "")
                            pidentificacion.Value = DBNull.Value;
                        else
                            pidentificacion.Value = recaudosmasivos.identificacion;
                        pidentificacion.DbType = DbType.String;
                        pidentificacion.Direction = ParameterDirection.Input;

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        if (recaudosmasivos.nombre == null || recaudosmasivos.nombre == "")
                            recaudosmasivos.nombre = " ";
                        if (recaudosmasivos.nombre.Length > 240)
                            pnombre.Value = recaudosmasivos.nombre.Substring(0, 240);
                        else
                            pnombre.Value = recaudosmasivos.nombre + " ";
                        pnombre.DbType = DbType.String;
                        pnombre.Direction = ParameterDirection.Input;

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "ptipo_producto";
                        ptipo_producto.Value = recaudosmasivos.tipo_producto;
                        ptipo_producto.DbType = DbType.String;
                        ptipo_producto.Direction = ParameterDirection.Input;

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "pnumero_producto";
                        if (recaudosmasivos.numero_producto == "")
                            pnumero_producto.Value = DBNull.Value;
                        else
                            pnumero_producto.Value = recaudosmasivos.numero_producto;
                        pnumero_producto.DbType = DbType.String;
                        pnumero_producto.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = recaudosmasivos.valor;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        DbParameter pnum_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnum_cuotas.ParameterName = "pnum_cuotas";
                        if (recaudosmasivos.num_cuotas == 0)
                            pnum_cuotas.Value = DBNull.Value;
                        else
                            pnum_cuotas.Value = recaudosmasivos.num_cuotas;
                        pnum_cuotas.DbType = DbType.Int64;
                        pnum_cuotas.Direction = ParameterDirection.Input;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        if (recaudosmasivos.num_cuotas == 0)
                            pobservaciones.Value = DBNull.Value;
                        else
                            pobservaciones.Value = recaudosmasivos.num_cuotas;
                        pobservaciones.DbType = DbType.String;
                        pobservaciones.Size = 240;
                        pobservaciones.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = recaudosmasivos.estadod;
                        pestado.DbType = DbType.Int64;
                        pestado.Direction = ParameterDirection.Input;

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        if (pUsuario.nombre.Length > 50)
                            pusuariocreacion.Value = pUsuario.nombre.Substring(0, 50);
                        else
                            pusuariocreacion.Value = pUsuario.nombre;
                        pusuariocreacion.DbType = DbType.String;
                        pusuariocreacion.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(piddetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);
                        cmdTransaccionFactory.Parameters.Add(pnombre);
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_DETRECAUDO_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return Convert.ToInt64(piddetalle.Value);
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message + " Codigo:" + recaudosmasivos.cod_cliente + " Identificacion:" + recaudosmasivos.identificacion;
                        BOExcepcion.Throw("", "", ex);
                        return Int64.MinValue;
                    }

                }
            }
        }


        public Boolean ModificarDetalleRecaudo(RecaudosMasivos recaudosmasivos, Usuario pUsuario, ref string Error)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter piddetalle = cmdTransaccionFactory.CreateParameter();
                        piddetalle.ParameterName = "piddetalle";
                        piddetalle.Value = recaudosmasivos.iddetalle;
                        piddetalle.DbType = DbType.Int64;

                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = recaudosmasivos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;

                        DbParameter ptipo_recaudo = cmdTransaccionFactory.CreateParameter();
                        ptipo_recaudo.ParameterName = "ptipo_recaudo";
                        ptipo_recaudo.Value = recaudosmasivos.tipo_recaudo;
                        ptipo_recaudo.DbType = DbType.Int64;
                        ptipo_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_cliente = cmdTransaccionFactory.CreateParameter();
                        pcod_cliente.ParameterName = "pcod_cliente";
                        pcod_cliente.Value = recaudosmasivos.cod_cliente;
                        pcod_cliente.DbType = DbType.Int64;
                        pcod_cliente.Direction = ParameterDirection.Input;

                        DbParameter pperiodo_corte = cmdTransaccionFactory.CreateParameter();
                        pperiodo_corte.ParameterName = "pperiodo_corte";
                        pperiodo_corte.Value = recaudosmasivos.periodo_corte;
                        pperiodo_corte.DbType = DbType.DateTime;
                        pperiodo_corte.Direction = ParameterDirection.Input;

                        DbParameter pidentificacion = cmdTransaccionFactory.CreateParameter();
                        pidentificacion.ParameterName = "pidentificacion";
                        pidentificacion.Value = recaudosmasivos.identificacion;
                        pidentificacion.DbType = DbType.String;
                        pidentificacion.Direction = ParameterDirection.Input;

                        DbParameter pnombre = cmdTransaccionFactory.CreateParameter();
                        pnombre.ParameterName = "pnombre";
                        if (recaudosmasivos.nombre == null)
                            recaudosmasivos.nombre = " ";
                        if (recaudosmasivos.nombre.Length > 240)
                            pnombre.Value = recaudosmasivos.nombre.Substring(0, 240);
                        else
                            pnombre.Value = recaudosmasivos.nombre + " ";
                        pnombre.DbType = DbType.String;
                        pnombre.Direction = ParameterDirection.Input;

                        DbParameter ptipo_producto = cmdTransaccionFactory.CreateParameter();
                        ptipo_producto.ParameterName = "ptipo_producto";
                        ptipo_producto.Value = recaudosmasivos.tipo_producto;
                        ptipo_producto.DbType = DbType.String;
                        ptipo_producto.Direction = ParameterDirection.Input;

                        DbParameter pnumero_producto = cmdTransaccionFactory.CreateParameter();
                        pnumero_producto.ParameterName = "pnumero_producto";
                        pnumero_producto.Value = recaudosmasivos.numero_producto;
                        pnumero_producto.DbType = DbType.String;
                        pnumero_producto.Direction = ParameterDirection.Input;

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "pvalor";
                        pvalor.Value = recaudosmasivos.valor;
                        pvalor.DbType = DbType.Decimal;
                        pvalor.Direction = ParameterDirection.Input;

                        DbParameter pnum_cuotas = cmdTransaccionFactory.CreateParameter();
                        pnum_cuotas.ParameterName = "pnum_cuotas";
                        pnum_cuotas.Value = recaudosmasivos.num_cuotas;
                        pnum_cuotas.DbType = DbType.Int64;
                        pnum_cuotas.Direction = ParameterDirection.Input;

                        DbParameter pobservaciones = cmdTransaccionFactory.CreateParameter();
                        pobservaciones.ParameterName = "pobservaciones";
                        pobservaciones.Value = recaudosmasivos.num_cuotas;
                        pobservaciones.DbType = DbType.String;
                        pobservaciones.Size = 240;
                        pobservaciones.Direction = ParameterDirection.Input;

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "pestado";
                        pestado.Value = recaudosmasivos.estadod;
                        pestado.DbType = DbType.Int64;
                        pestado.Direction = ParameterDirection.Input;

                        DbParameter pusuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pusuariocreacion.ParameterName = "pusuariocreacion";
                        if (recaudosmasivos.nombre.Length > 50)
                            pusuariocreacion.Value = pUsuario.nombre.Substring(0, 50);
                        else
                            pusuariocreacion.Value = pUsuario.nombre;
                        pusuariocreacion.DbType = DbType.String;
                        pusuariocreacion.Direction = ParameterDirection.Input;

                        DbParameter pcambioestado = cmdTransaccionFactory.CreateParameter();
                        pcambioestado.ParameterName = "pcambioestado";
                        pcambioestado.Value = recaudosmasivos.cambioestado;
                        pcambioestado.DbType = DbType.Int64;
                        pcambioestado.Direction = ParameterDirection.Input;

                        cmdTransaccionFactory.Parameters.Add(piddetalle);
                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_cliente);
                        cmdTransaccionFactory.Parameters.Add(pidentificacion);
                        cmdTransaccionFactory.Parameters.Add(pnombre);
                        cmdTransaccionFactory.Parameters.Add(ptipo_producto);
                        cmdTransaccionFactory.Parameters.Add(pnumero_producto);
                        cmdTransaccionFactory.Parameters.Add(pvalor);
                        cmdTransaccionFactory.Parameters.Add(pnum_cuotas);
                        cmdTransaccionFactory.Parameters.Add(pobservaciones);
                        cmdTransaccionFactory.Parameters.Add(pestado);
                        cmdTransaccionFactory.Parameters.Add(pusuariocreacion);
                        cmdTransaccionFactory.Parameters.Add(pcambioestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_DETRECAUDO_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        return true;
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                        BOExcepcion.Throw("", "", ex);
                        return false;
                    }

                }
            }
        }


        public List<RecaudosMasivos> ListarRecaudo(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql, opc;
                        if (pRecaudo.cod_empresa != 0 || pRecaudo.tipo_lista != 0 || pRecaudo.fecha_recaudo != DateTime.MinValue)
                            opc = "and";
                        else
                            opc = "where";
                        if (pRecaudo.periodo_corte == DateTime.MinValue || pRecaudo.periodo_corte == null)
                        {
                            sql = @"SELECT A.*, B.NOM_EMPRESA, CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO,
                                    CASE A.ESTADO WHEN '1' THEN 'Pendiente' WHEN '2' THEN 'Aplicado' END AS NOM_ESTADO, SUM(D.VALOR) AS TOTAL_RECAUDO, O.NUM_COMP
                                    FROM RECAUDO_MASIVO A LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA 
                                    INNER JOIN DETRECAUDO_MASIVO D ON A.NUMERO_RECAUDO = D.NUMERO_RECAUDO 
                                    LEFT JOIN OPERACION O ON A.NUMERO_RECAUDO = O.NUM_LISTA "
                                    + ObtenerFiltro(pRecaudo, "A.") +
                                    @" GROUP BY A.NUMERO_RECAUDO, A.TIPO_RECAUDO, A.COD_EMPRESA, A.TIPO_LISTA, A.PERIODO_CORTE, A.FECHA_RECAUDO,
                                    A.FECHA_APLICACION, A.ESTADO, A.NUMERO_NOVEDAD, A.FECHACREACION, A.USUARIOCREACION, A.FECULTMOD, 
                                    A.USUULTMOD, A.COD_OFICINA, B.NOM_EMPRESA,O.NUM_COMP ORDER BY A.NUMERO_RECAUDO";
                        }
                        else
                        {
                            sql = @"SELECT A.*, B.NOM_EMPRESA, CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO,
                                    CASE A.ESTADO WHEN '1' THEN 'Pendiente' WHEN '2' THEN 'Aplicado' END AS NOM_ESTADO, SUM(D.VALOR) AS TOTAL_RECAUDO, O.NUM_COMP
                                    FROM RECAUDO_MASIVO A LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA
                                    INNER JOIN DETRECAUDO_MASIVO D ON A.NUMERO_RECAUDO = D.NUMERO_RECAUDO 
                                    LEFT JOIN OPERACION O ON A.NUMERO_RECAUDO = O.NUM_LISTA "
                                   + ObtenerFiltro(pRecaudo, "A.");
                            if (sql.ToUpper().Contains("WHERE") == true)
                                opc = " and ";
                            else
                                opc = " where ";
                            sql += opc + " trunc(PERIODO_CORTE) = To_Date('" + Convert.ToDateTime(pRecaudo.periodo_corte).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') "

                                + @" GROUP BY A.NUMERO_RECAUDO, A.TIPO_RECAUDO, A.COD_EMPRESA, A.TIPO_LISTA, A.PERIODO_CORTE, A.FECHA_RECAUDO,
                                    A.FECHA_APLICACION, A.ESTADO, A.NUMERO_NOVEDAD, A.FECHACREACION, A.USUARIOCREACION, A.FECULTMOD, 
                                    A.USUULTMOD, A.COD_OFICINA, B.NOM_EMPRESA,O.NUM_COMP ORDER BY A.NUMERO_RECAUDO";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["NOM_TIPO_RECAUDO"] != DBNull.Value) entidad.nom_tipo_recaudo = Convert.ToString(resultado["NOM_TIPO_RECAUDO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) entidad.fecha_recaudo = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);
                            if (resultado["TOTAL_RECAUDO"] != DBNull.Value) entidad.valor_novedad = Convert.ToDecimal(resultado["TOTAL_RECAUDO"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUM_COMP"]);
                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<RecaudosMasivos> ListarAportesPorAplicar(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql;

                        sql = @"SELECT A.*, B.NOM_EMPRESA, CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO,
                               CASE A.ESTADO WHEN '1' THEN 'Pendiente' WHEN '2' THEN 'Aplicado' END AS NOM_ESTADO
                               FROM RECAUDO_MASIVO A LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA " + pFiltro.ToString();

                        sql += "ORDER BY A.NUMERO_RECAUDO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["NOM_TIPO_RECAUDO"] != DBNull.Value) entidad.nom_tipo_recaudo = Convert.ToString(resultado["NOM_TIPO_RECAUDO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) entidad.fecha_recaudo = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public int ConsultarProgresoRecaudos(long numero_reclamacion, Usuario usuario)
        {
            DbDataReader resultado = default(DbDataReader);
            int contador = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT COUNT(*) AS CONTADOR 
                                        FROM DETRECAUDO_MASIVO
                                        WHERE NUMERO_RECAUDO = " + numero_reclamacion + " AND ESTADO = 2";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONTADOR"] != DBNull.Value) contador = Convert.ToInt32(resultado["CONTADOR"]);
                        }

                        return contador;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarProgresoRecaudos", ex);
                        return 0;
                    }
                }
            }
        }

        public RecaudosMasivos ConsultarRecaudo(string pNumeroRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            RecaudosMasivos eRecaudosMasivos = new RecaudosMasivos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  RECAUDO_MASIVO WHERE NUMERO_RECAUDO = " + pNumeroRecaudo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) eRecaudosMasivos.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) eRecaudosMasivos.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["TIPO_LISTA"] != DBNull.Value) eRecaudosMasivos.tipo_lista = Convert.ToInt32(resultado["TIPO_LISTA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) eRecaudosMasivos.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) eRecaudosMasivos.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) eRecaudosMasivos.fecha_recaudo = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) eRecaudosMasivos.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) eRecaudosMasivos.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NUMERO_NOVEDAD"] != DBNull.Value) eRecaudosMasivos.numero_novedad = Convert.ToInt64(resultado["NUMERO_NOVEDAD"]);
                        }

                        return eRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public RecaudosMasivos ConsultarRecaudo(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            RecaudosMasivos eRecaudosMasivos = new RecaudosMasivos();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  RECAUDO_MASIVO " + ObtenerFiltro(pRecaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) eRecaudosMasivos.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) eRecaudosMasivos.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["TIPO_LISTA"] != DBNull.Value) eRecaudosMasivos.tipo_lista = Convert.ToInt32(resultado["TIPO_LISTA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) eRecaudosMasivos.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) eRecaudosMasivos.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) eRecaudosMasivos.fecha_recaudo = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) eRecaudosMasivos.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) eRecaudosMasivos.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NUMERO_NOVEDAD"] != DBNull.Value) eRecaudosMasivos.numero_novedad = Convert.ToInt64(resultado["NUMERO_NOVEDAD"]);
                        }

                        return eRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ConsultarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudo(int pNumeroRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT A.* FROM DETRECAUDO_MASIVO A WHERE A.NUMERO_RECAUDO = " + pNumeroRecaudo + " ORDER BY IDDETALLE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUM_CUOTAS"] != DBNull.Value) entidad.num_cuotas = Convert.ToInt32(resultado["NUM_CUOTAS"]);
                            if (entidad.num_cuotas == 1)
                                entidad.tipo_aplicacion = "Abono a Capital";
                            else
                                entidad.tipo_aplicacion = "Por Valor a Capital";
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadod = Convert.ToInt32(resultado["ESTADO"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleRecaudo", ex);
                        return null;
                    }
                }
            }
        }


        public List<RecaudosMasivos> ListarDetalleAportePendientes(string pFiltro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstAportesPend = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT D.*, A.TIPO_TRAN, A.COD_OPE
                                        FROM DETRECAUDO_MASIVO D INNER JOIN APORTE_POR_APLICAR A ON A.COD_DET_LIST = D.IDDETALLE
                                        " + pFiltro.ToString() + " ORDER BY D.IDDETALLE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            entidad.tipo_aplicacion = "Por Valor a Capital";
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["NUM_CUOTAS"] != DBNull.Value) entidad.num_cuotas = Convert.ToInt32(resultado["NUM_CUOTAS"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadod = Convert.ToInt32(resultado["ESTADO"]);
                            if (resultado["TIPO_TRAN"] != DBNull.Value) entidad.tipo_tran = Convert.ToInt32(resultado["TIPO_TRAN"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            lstAportesPend.Add(entidad);
                        }

                        return lstAportesPend;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleAportePendientes", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoConsulta(int pNumeroRecaudo, bool bDetallado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            DbDataReader resultadoDet = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (bDetallado)
                            sql = @"SELECT DISTINCT A.IDDETALLE, A.NUMERO_RECAUDO, A.COD_CLIENTE, A.IDENTIFICACION, A.NOMBRE,  A.TIPO_PRODUCTO||'-'||
                                    (select lc.nombre from credito c inner join LINEASCREDITO lc on c.COD_LINEA_CREDITO = lc.cod_linea_credito where c.NUMERO_RADICACION = A.NUMERO_PRODUCTO)
                                    as TIPO_PRODUCTO, CODIGOPRODUCTO(A.TIPO_PRODUCTO||'-'||
                                    (select lc.nombre from credito c inner join LINEASCREDITO lc on c.COD_LINEA_CREDITO = lc.cod_linea_credito where c.NUMERO_RADICACION = A.NUMERO_PRODUCTO)
                                    )as TIPO_PRO_NUM,
                                    A.NUMERO_PRODUCTO, A.VALOR, A.NUM_CUOTAS, A.OBSERVACIONES, A.ESTADO, A.FECHACREACION, A.USUARIOCREACION, A.FECULTMOD, A.USUULTMOD,
                                    (Select Sum(c.valor) From DEVOLUCION C Where A.NUMERO_RECAUDO = C.NUM_RECAUDO AND A.IDDETALLE = C.IDDETALLE) As DEVOLUCION,
                                    OP.COD_OPE                                    
                                    FROM DETRECAUDO_MASIVO A 
                                    INNER JOIN RECAUDO_MASIVO R ON A.NUMERO_RECAUDO = R.NUMERO_RECAUDO
                                    INNER JOIN OPERACION OP ON R.NUMERO_RECAUDO = OP.NUM_LISTA
                                    WHERE A.NUMERO_RECAUDO = " + pNumeroRecaudo;
                        else
                            sql = @"SELECT A.IDDETALLE, A.NUMERO_RECAUDO, A.COD_CLIENTE, A.IDENTIFICACION, A.NOMBRE, A.TIPO_PRODUCTO||'-'||
                                    case A.TIPO_PRODUCTO 
                                    when 'Creditos' then
                                    (select lc.nombre from credito c inner join LINEASCREDITO lc on c.COD_LINEA_CREDITO = lc.cod_linea_credito where c.NUMERO_RADICACION = A.NUMERO_PRODUCTO)
                                    when 'Aportes' then
                                    (select la.NOMBRE from aporte x inner join lineaaporte la on x.COD_LINEA_APORTE = la.COD_LINEA_APORTE where x.NUMERO_APORTE = A.NUMERO_PRODUCTO)
                                    when 'ahorro programado' then
                                    (select lp.NOMBRE from AHORRO_PROGRAMADO p inner join LINEAPROGRAMADO lp on p.COD_LINEA_PROGRAMADO = lp.COD_LINEA_PROGRAMADO where p.NUMERO_PROGRAMADO = A.NUMERO_PRODUCTO)
                                    WHEN 'ahorros a la Vista' THEN
                                    (select lv.DESCRIPCION from AHORRO_VISTA v inner join LINEAAHORRO lv on v.COD_LINEA_AHORRO = lv.COD_LINEA_AHORRO where v.NUMERO_CUENTA = A.NUMERO_PRODUCTO)
                                    when 'Servicios' then
                                    (select ls.nombre from SERVICIOS s inner join LINEASSERVICIOS ls on s.COD_LINEA_SERVICIO = ls.COD_LINEA_SERVICIO where s.NUMERO_SERVICIO = A.NUMERO_PRODUCTO)
                                    when 'CDATS' then
                                    (select lc.DESCRIPCION from CDAT c inner join LINEACDAT lc on c.COD_LINEACDAT = lc.COD_LINEACDAT where c.NUMERO_CDAT = A.NUMERO_PRODUCTO)
                                    end as TIPO_PRODUCTO, 0 as TIPO_PRO_NUM,
                                    A.NUMERO_PRODUCTO, A.VALOR, A.NUM_CUOTAS, A.OBSERVACIONES, A.ESTADO, A.FECHACREACION, A.USUARIOCREACION, A.FECULTMOD, A.USUULTMOD, 0 As CAPITAL, 0 As INTCTE, 0 As INTMORA, 0 As SEGURO, 0 As LEYMIPYME, 0 As IVALEYMIPYME, 0 As OTROS, 0 As DEVOLUCION,
                                    OP.COD_OPE
                                    FROM DETRECAUDO_MASIVO A
                                    INNER JOIN RECAUDO_MASIVO R ON A.NUMERO_RECAUDO = R.NUMERO_RECAUDO
                                    INNER JOIN OPERACION OP ON R.NUMERO_RECAUDO = OP.NUM_LISTA 
                                    WHERE A.NUMERO_RECAUDO = " + pNumeroRecaudo + " ORDER BY A.IDDETALLE";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["TIPO_PRO_NUM"] != DBNull.Value) entidad.tipo_productotemp = Convert.ToInt32(resultado["TIPO_PRO_NUM"]);
                            entidad.tipo_aplicacion = "Por Valor";
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["DEVOLUCION"] != DBNull.Value) entidad.devolucion = Convert.ToDecimal(resultado["DEVOLUCION"]);
                            if (resultado["NUM_CUOTAS"] != DBNull.Value) entidad.num_cuotas = Convert.ToInt32(resultado["NUM_CUOTAS"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estadod = Convert.ToInt32(resultado["ESTADO"]);

                            if (entidad.tipo_productotemp != 0)
                            {
                                string sqldet = "";
                                if (entidad.tipo_productotemp == 1)//Aportes
                                {
                                    sqldet = @"select 
                                                Sum(Decode(t.cod_atr, 1, t.valor)) As capital,
                                                Sum(Decode(t.cod_atr, 2, t.valor)) As intcte, 
                                                Sum(Decode(t.cod_atr, 3, t.valor)) As intmora,
                                                Sum(Decode(t.cod_atr, 7, t.valor)) As seguro, 
                                                Sum(Decode(t.cod_atr, 25, t.valor, 0) + Decode(t.cod_atr, 40, t.valor, 0)) As LeyMiPyme,
                                                Sum(Decode(t.cod_atr, 26, t.valor, 0) + Decode(t.cod_atr, 41, t.valor, 0)) As IvaLeyMiPyme,
                                                VLRAHORROXAPORTE(t.NUMERO_APORTE, t.COD_PERSONA, t.COD_OPE, t.COD_DET_LIS) As Otros
                                                from tran_aporte t
                                                Inner Join operacion o On t.cod_ope = o.cod_ope
                                                inner join APORTE a on t.NUMERO_APORTE = a.NUMERO_APORTE
                                                inner join LINEAAPORTE LA on a.COD_LINEA_APORTE = LA.COD_LINEA_APORTE and LA.TIPO_APORTE = 1
                                                where t.COD_OPE = " + entidad.cod_ope + " and t.NUMERO_APORTE = " + entidad.numero_producto + " and t.COD_DET_LIS = " + entidad.iddetalle +
                                                "group by t.NUMERO_APORTE, t.COD_OPE, t.COD_DET_LIS, t.COD_PERSONA";
                                }
                                else if (entidad.tipo_productotemp == 2)//Creditos
                                {
                                    sqldet = @"Select t.cod_det_lis, t.NUMERO_RADICACION, t.COD_OPE, Sum(Decode(t.cod_atr, 1, t.valor)) As capital, Sum(Decode(t.cod_atr, 2, t.valor)) As intcte, Sum(Decode(t.cod_atr, 3, t.valor)) As intmora,
                                                Sum(Decode(t.cod_atr, 7, t.valor)) As seguro, Sum(Decode(t.cod_atr, 25, t.valor, 0)+Decode(t.cod_atr, 40, t.valor, 0)) As LeyMiPyme, 
                                                Sum(Decode(t.cod_atr, 26, t.valor, 0)+Decode(t.cod_atr, 41, t.valor, 0)) As IvaLeyMiPyme, Sum(Decode(t.cod_atr, 1, 0, 2, 0, 3, 0, 7, 0, 25, 0, 26, 0, 40, 0, 41, 0, t.valor)) As Otros
                                                From tran_cred t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE =  " + entidad.cod_ope + " and t.NUMERO_RADICACION =" + entidad.numero_producto + "and t.COD_DET_LIS = " + entidad.iddetalle +
                                                "Group by t.cod_det_lis, t.NUMERO_RADICACION, t.COD_OPE";
                                }
                                else if (entidad.tipo_productotemp == 3)//Aho. Vista
                                {
                                    sqldet = @"Select t.COD_OPE,t.NUMERO_CUENTA, Sum (t.valor) As capital, 0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros
                                                From TRAN_AHORRO t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE= " + entidad.cod_ope + " and t.NUMERO_CUENTA =  " + entidad.numero_producto +
                                                "Group by t.cod_ope, t.NUMERO_CUENTA";
                                }
                                else if (entidad.tipo_productotemp == 4)//Servicios
                                {
                                    sqldet = @"Select t.COD_OPE,t.NUMERO_SERVICIO, Sum(Decode(t.cod_atr, 1, t.valor)) As capital, Sum(Decode(t.cod_atr, 2, t.valor)) As intcte, Sum(Decode(t.cod_atr, 3, t.valor)) As intmora,
                                                Sum(Decode(t.cod_atr, 7, t.valor)) As seguro, Sum(Decode(t.cod_atr, 25, t.valor, 0)+Decode(t.cod_atr, 40, t.valor, 0)) As LeyMiPyme, 
                                                Sum(Decode(t.cod_atr, 26, t.valor, 0)+Decode(t.cod_atr, 41, t.valor, 0)) As IvaLeyMiPyme, Sum(Decode(t.cod_atr, 1, 0, 2, 0, 3, 0, 7, 0, 25, 0, 26, 0, 40, 0, 41, 0, t.valor)) As Otros
                                                From TRAN_SERVICIOS t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE = " + entidad.cod_ope + " and t.NUMERO_SERVICIO = " + entidad.numero_producto +
                                                "Group by t.cod_ope, t.NUMERO_SERVICIO";
                                }
                                else if (entidad.tipo_productotemp == 5)//Cdat
                                {
                                    sqldet = @"Select t.COD_OPE, t.CODIGO_CDAT, Sum (t.valor) As capital, 0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros
                                                From TRAN_CDAT t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE = " + entidad.cod_ope + " and t.CODIGO_CDAT = " + entidad.numero_producto +
                                                "Group by t.cod_ope, t.CODIGO_CDAT";
                                }
                                else if (entidad.tipo_productotemp == 6)//Afiliación
                                {
                                    sqldet = @"Select t.COD_OPE, t.COD_DET_LIS, Sum (t.valor) As capital, 0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros
                                                From tran_afiliacion t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE = 0 and t.COD_DET_LIS =0
                                                Group by t.COD_OPE, t.COD_DET_LIS;Select t.COD_OPE, t.COD_DET_LIS, Sum (t.valor) As capital
                                                From tran_afiliacion t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE =  " + entidad.cod_ope + "  and t.COD_DET_LIS =" + entidad.iddetalle +
                                                "Group by t.COD_OPE, t.COD_DET_LIS";
                                }
                                else if (entidad.tipo_productotemp == 8)//Devoluciones
                                {
                                    sqldet = @"Select nvl(Sum(t.valor),0) As capital, 0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros
                                                From DEVOLUCION t where t.NUM_RECAUDO= " + pNumeroRecaudo + " and t.IDDETALLE =" + entidad.iddetalle;
                                }
                                else if (entidad.tipo_productotemp == 9)// Aho. Programado
                                {
                                    sqldet = @"Select t.COD_OPE,t.NUMERO_PROGRAMADO, Sum (t.valor) As capital,  0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros
                                                From TRAN_PROGRAMADO t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE = " + entidad.cod_ope + " and t.NUMERO_PROGRAMADO =" + entidad.numero_producto +
                                                "Group by t.cod_ope, t.NUMERO_PROGRAMADO";
                                }
                                else if (entidad.tipo_productotemp == 10)//Creditos Cuotas Extras
                                {
                                    sqldet = @"Select t.cod_det_lis, t.NUMERO_RADICACION, t.COD_OPE, Sum(Decode(t.cod_atr, 1, t.valor)) As capital, Sum(Decode(t.cod_atr, 2, t.valor)) As intcte, Sum(Decode(t.cod_atr, 3, t.valor)) As intmora,
                                                Sum(Decode(t.cod_atr, 7, t.valor)) As seguro, Sum(Decode(t.cod_atr, 25, t.valor, 0)+Decode(t.cod_atr, 40, t.valor, 0)) As LeyMiPyme, 
                                                Sum(Decode(t.cod_atr, 26, t.valor, 0)+Decode(t.cod_atr, 41, t.valor, 0)) As IvaLeyMiPyme, Sum(Decode(t.cod_atr, 1, 0, 2, 0, 3, 0, 7, 0, 25, 0, 26, 0, 40, 0, 41, 0, t.valor)) As Otros
                                                From tran_cred t Inner Join operacion o On t.cod_ope = o.cod_ope And o.tipo_ope = 119
                                                Where o.COD_OPE =  " + entidad.cod_ope + " and t.NUMERO_RADICACION =" + entidad.numero_producto + "and t.COD_DET_LIS = " + entidad.iddetalle +
                                                "Group by t.cod_det_lis, t.NUMERO_RADICACION, t.COD_OPE";
                                }
                                else
                                {
                                    sqldet = @"Select 0 As capital, 0 as intcte, 0 as intmora, 0 as seguro, 0 as LeyMiPyme, 0 as IvaLeyMiPyme, 0 as Otros from dual";
                                }

                                cmdTransaccionFactory.Connection = connection;
                                cmdTransaccionFactory.CommandType = CommandType.Text;
                                cmdTransaccionFactory.CommandText = sqldet;
                                resultadoDet = cmdTransaccionFactory.ExecuteReader();
                            }
                            else
                            {
                                try
                                {
                                    if (resultado["CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["CAPITAL"]);
                                    if (resultado["INTCTE"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["INTCTE"]);
                                    if (resultado["INTMORA"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["INTMORA"]);
                                    if (resultado["SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultado["SEGURO"]);
                                    if (resultado["LEYMIPYME"] != DBNull.Value) entidad.leymipyme = Convert.ToDecimal(resultado["LEYMIPYME"]);
                                    if (resultado["IVALEYMIPYME"] != DBNull.Value) entidad.ivaleymipyme = Convert.ToDecimal(resultado["IVALEYMIPYME"]);
                                    if (resultado["OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["OTROS"]);
                                }
                                catch { }
                            }

                            if (bDetallado)
                            {
                                if (resultadoDet != null)
                                {
                                    if (resultadoDet.Read())
                                    {
                                        if (resultadoDet["CAPITAL"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultadoDet["CAPITAL"]);
                                        if (resultadoDet["INTCTE"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultadoDet["INTCTE"]);
                                        if (resultadoDet["INTMORA"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultadoDet["INTMORA"]);
                                        if (resultadoDet["SEGURO"] != DBNull.Value) entidad.seguro = Convert.ToDecimal(resultadoDet["SEGURO"]);
                                        if (resultadoDet["LEYMIPYME"] != DBNull.Value) entidad.leymipyme = Convert.ToDecimal(resultadoDet["LEYMIPYME"]);
                                        if (resultadoDet["IVALEYMIPYME"] != DBNull.Value) entidad.ivaleymipyme = Convert.ToDecimal(resultadoDet["IVALEYMIPYME"]);
                                        if (resultadoDet["OTROS"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultadoDet["OTROS"]);
                                    }
                                }
                            }

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleRecaudoConsulta", ex);
                        return null;
                    }
                }
            }
        }



        public void EliminarRecaudosMasivos(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        RecaudosMasivos pRecaudosMasivos = new RecaudosMasivos();
                        pRecaudosMasivos = ConsultarRecaudo(Convert.ToString(pId), vUsuario);

                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "p_numero_recaudo";
                        pnumero_recaudo.Value = pRecaudosMasivos.numero_recaudo;
                        pnumero_recaudo.Direction = ParameterDirection.Input;
                        pnumero_recaudo.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_RECAUDO_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudosMasivosData", "EliminarRecaudosMasivos", ex);
                    }
                }
            }
        }


        public List<RecaudosMasivos> ListarDetalleReporte(int pNumeroRecaudo, int pNumeroNovedad, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        // Se cruza por el código del cliente, tipo y número de producto.
                        string sql = @"Select Nvl(r.cod_cliente, e.cod_cliente) as cod_cliente, Nvl(r.identificacion, e.identificacion) as identificacion, Nvl(r.nombre, e.nombre) as nombre, 
                                        Nvl(Upper(r.tipo_producto), Upper(e.tipo_producto)) as tipo_producto, Nvl(r.numero_producto, e.numero_producto) as numero_producto, r.valor as valor_aplicado, e.valor as valor_novedad 
                                        From (Select r.cod_empresa, r.numero_novedad, d.* From recaudo_masivo r, detrecaudo_masivo d Where r.numero_recaudo = d.numero_recaudo and d.numero_recaudo = " + pNumeroRecaudo.ToString() + @") r 
                                        Full Outer Join (Select e.cod_empresa, de.*, p.identificacion, p.primer_nombre ||' '|| p.segundo_nombre||' '|| p.primer_apellido ||' '|| p.segundo_apellido as nombre 
                                                          From empresa_novedad e inner join detempresa_novedad de on e.numero_novedad = de.numero_novedad 
                                                          left join Persona p on p.cod_persona = de.cod_cliente
                                                          Where e.numero_novedad = " + pNumeroNovedad.ToString() + @") e 
                                        On r.cod_empresa = e.cod_empresa 
                                        And r.numero_novedad = e.numero_novedad
                                        And r.cod_cliente = e.cod_cliente
                                        And CodigoProducto(Upper(r.tipo_producto)) = CodigoProducto(Upper(e.tipo_producto)) 
                                        And r.numero_producto = e.numero_producto ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();
                            if (resultado["COD_CLIENTE"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_CLIENTE"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["VALOR_APLICADO"] != DBNull.Value) entidad.valor_aplicado = Convert.ToDecimal(resultado["VALOR_APLICADO"]);
                            if (resultado["VALOR_NOVEDAD"] != DBNull.Value) entidad.valor_novedad = Convert.ToDecimal(resultado["VALOR_NOVEDAD"]);
                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleReporte", ex);
                        return null;
                    }
                }
            }
        }


        public String AplicarRecaudoDividir(RecaudosMasivos recaudosmasivos, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pnumero_recaudo";
                        pnumero_recaudo.Value = recaudosmasivos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;
                        pnumero_recaudo.Direction = ParameterDirection.Input;

                        DbParameter pcod_oficina = cmdTransaccionFactory.CreateParameter();
                        pcod_oficina.ParameterName = "pcod_oficina";
                        pcod_oficina.Value = pUsuario.cod_oficina;
                        pcod_oficina.DbType = DbType.Int64;
                        pcod_oficina.Direction = ParameterDirection.Input;

                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "pusuario";
                        pusuario.Value = pUsuario.nombre;
                        pusuario.DbType = DbType.String;
                        pusuario.Direction = ParameterDirection.Input;

                        DbParameter pNumerosRecaudo = cmdTransaccionFactory.CreateParameter();
                        pNumerosRecaudo.ParameterName = "pNumerosRecaudo";
                        string sAux = "";
                        for (int i = 1; i < 1000; i++) { sAux += " "; };
                        pNumerosRecaudo.Value = sAux;
                        pNumerosRecaudo.DbType = DbType.String;
                        pnumero_recaudo.Size = 1000;
                        pNumerosRecaudo.Direction = ParameterDirection.Output;

                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);
                        cmdTransaccionFactory.Parameters.Add(pcod_oficina);
                        cmdTransaccionFactory.Parameters.Add(pusuario);
                        cmdTransaccionFactory.Parameters.Add(pNumerosRecaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_RECAUDODIVIDIR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        recaudosmasivos.numero_producto = Convert.ToString(pNumerosRecaudo.Value);
                        return recaudosmasivos.numero_producto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "AplicarRecaudoDividir", ex);
                        return null;
                    }

                }
            }
        }



        public List<RecaudosMasivos> ListarTEMP_Consolidado(RecaudosMasivos pRecaudos, ref string pError, Usuario vUsuario)
        {
            pError = "";
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstConsolidado = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                connection.Open();

                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;

                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "pNumeroRecaudo";
                        pnumero_recaudo.Value = pRecaudos.numero_recaudo;
                        pnumero_recaudo.DbType = DbType.Int64;
                        pnumero_recaudo.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);

                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_REC_REPCONSOLIDADO";
                        cmdTransaccionFactory.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        pError = ex.Message;
                        return null;
                    }
                };


                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        cmdTransaccionFactory.Connection = connection;
                        Configuracion conf = new Configuracion();

                        string sql = @"SELECT T.NUMERO_RECAUDO, T.COD_EMPRESA, T.PERIODO, T.FECHA_APLICACION, T.PRODUCTO, T.CONCEPTO, T.NOMBRE_CONCEPTO, T.COD_ATR, T.NOMBRE_ATRIBUTO,
                                        T.COD_OFICINA,O.NOMBRE AS NOM_OFICINA, SUM(T.VALOR) AS VALOR
                                        From TEMP_REPORTERECAUDO t LEFT JOIN OFICINA O ON O.COD_OFICINA = T.COD_OFICINA
                                        GROUP BY T.NUMERO_RECAUDO, T.COD_EMPRESA, T.PERIODO, T.FECHA_APLICACION, T.PRODUCTO, T.CONCEPTO, T.NOMBRE_CONCEPTO, T.COD_ATR, T.NOMBRE_ATRIBUTO,
                                        t.cod_oficina,O.NOMBRE
                                        Order by producto, concepto";

                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["numero_recaudo"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["numero_recaudo"]);
                            if (resultado["cod_empresa"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["cod_empresa"]);
                            if (resultado["periodo"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["periodo"]);
                            if (resultado["fecha_aplicacion"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["fecha_aplicacion"]);
                            if (resultado["producto"] != DBNull.Value) entidad.nom_tipo_producto = Convert.ToString(resultado["producto"]);
                            if (resultado["concepto"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["concepto"]);
                            if (resultado["nombre_concepto"] != DBNull.Value) entidad.nombres = Convert.ToString(resultado["nombre_concepto"]);
                            if (resultado["cod_atr"] != DBNull.Value) entidad.cod_atr = Convert.ToString(resultado["cod_atr"]);
                            if (resultado["nombre_atributo"] != DBNull.Value) entidad.nom_atr = Convert.ToString(resultado["nombre_atributo"]);
                            if (resultado["cod_oficina"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["cod_oficina"]);
                            if (resultado["NOM_OFICINA"] != DBNull.Value) entidad.nom_oficina = Convert.ToString(resultado["NOM_OFICINA"]);
                            if (resultado["valor"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["valor"]);
                            lstConsolidado.Add(entidad);
                        }

                        return lstConsolidado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarTEMP_Consolidado", ex);
                        return null;
                    }
                }
            }
        }


        public void AplicarRecaudo(Int64 numero_recaudo, DateTime fechaaplicacion, Boolean pAportePend, ref Int64 pcod_ope, ref string Error, Usuario pUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pn_numero_recuado = cmdTransaccionFactory.CreateParameter();
                        pn_numero_recuado.ParameterName = "pnumero_recaudo";
                        pn_numero_recuado.Value = numero_recaudo;
                        pn_numero_recuado.DbType = DbType.Int64;
                        pn_numero_recuado.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_numero_recuado);

                        DbParameter pf_fecha = cmdTransaccionFactory.CreateParameter();
                        pf_fecha.ParameterName = "pfecha";
                        if (fechaaplicacion == null && fechaaplicacion != DateTime.MinValue) pf_fecha.Value = DBNull.Value; else pf_fecha.Value = fechaaplicacion;
                        pf_fecha.DbType = DbType.DateTime;
                        pf_fecha.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pf_fecha);

                        DbParameter pn_cod_usu = cmdTransaccionFactory.CreateParameter();
                        pn_cod_usu.ParameterName = "pcod_usu";
                        pn_cod_usu.Value = pUsuario.codusuario;
                        pn_cod_usu.DbType = DbType.Int64;
                        pn_cod_usu.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pn_cod_usu);

                        DbParameter p_ip = cmdTransaccionFactory.CreateParameter();
                        p_ip.ParameterName = "p_ip";
                        if (!string.IsNullOrWhiteSpace(pUsuario.IP))
                        {
                            p_ip.Value = pUsuario.IP;
                        }
                        else
                        {
                            p_ip.Value = DBNull.Value;
                        }
                        cmdTransaccionFactory.Parameters.Add(p_ip);

                        DbParameter pncod_ope = cmdTransaccionFactory.CreateParameter();
                        pncod_ope.ParameterName = "pcod_ope";
                        pncod_ope.Value = 0;
                        pncod_ope.DbType = DbType.Int64;
                        pncod_ope.Direction = ParameterDirection.Output;
                        cmdTransaccionFactory.Parameters.Add(pncod_ope);

                        DbParameter pAportePendiente = cmdTransaccionFactory.CreateParameter();
                        pAportePendiente.ParameterName = "pAportePendiente";
                        pAportePendiente.Value = pAportePend == true ? 1 : 0;
                        pAportePendiente.DbType = DbType.Int32;
                        pAportePendiente.Direction = ParameterDirection.Input;
                        cmdTransaccionFactory.Parameters.Add(pAportePendiente);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_APLICARRECAUDO";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        if (pncod_ope.Value != DBNull.Value)
                            pcod_ope = Convert.ToInt64(pncod_ope.Value);

                        RecaudosMasivos recaudoAplicado = new RecaudosMasivos
                        {
                            numero_recaudo = numero_recaudo,
                            fecha_aplicacion = fechaaplicacion,
                            codigo_usuario = pUsuario.codusuario,
                            usuario_ip = pUsuario.IP,
                            cod_ope = pcod_ope,
                            aporte_pendiente = pAportePend
                        };

                        DAauditoria.InsertarLog(recaudoAplicado, "RECAUDO_MASIVO", pUsuario, Accion.Crear.ToString(), TipoAuditoria.RecaudosMasivos, "Creacion de aplicacion recaudo masivo con numero de recaudo " + numero_recaudo + " y codigo de operacion " + pcod_ope);

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        Error = ex.Message;
                    }
                }
            }
        }

        public int RegistrosAplicados(Int64 pNumeroRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            int cantidad = 0;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select Count(*) As numero From detrecaudo_masivo Where numero_recaudo = " + pNumeroRecaudo;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["NUMERO"] != DBNull.Value) cantidad = Convert.ToInt32(resultado["NUMERO"]);
                        }

                        return cantidad;
                    }
                    catch
                    {
                        return cantidad;
                    }
                }
            }
        }

        public string LineaCreditoRecoge(string pCod_Linea_Credito, string pCod_Parametro, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string eCredito = "";

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT VALOR FROM PARAMETROS_LINEA WHERE COD_LINEA_CREDITO = '" + pCod_Linea_Credito.ToString() + "' AND COD_PARAMETRO = " + pCod_Parametro;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["VALOR"] != DBNull.Value) eCredito = Convert.ToString(resultado["VALOR"]);
                        }

                        return eCredito;
                    }
                    catch
                    {
                        return eCredito;
                    }
                }
            }
        }



        public AportePendientes CrearAportePendientes(AportePendientes pAportePendientes, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_transaccion";
                        pnumero_transaccion.Value = pAportePendientes.numero_transaccion;
                        pnumero_transaccion.Direction = ParameterDirection.Output;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pAportePendientes.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pcod_persona = cmdTransaccionFactory.CreateParameter();
                        pcod_persona.ParameterName = "p_cod_persona";
                        if (pAportePendientes.cod_persona == null)
                            pcod_persona.Value = DBNull.Value;
                        else
                            pcod_persona.Value = pAportePendientes.cod_persona;
                        pcod_persona.Direction = ParameterDirection.Input;
                        pcod_persona.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_persona);

                        DbParameter pnumero_aporte = cmdTransaccionFactory.CreateParameter();
                        pnumero_aporte.ParameterName = "p_numero_aporte";
                        pnumero_aporte.Value = pAportePendientes.numero_aporte;
                        pnumero_aporte.Direction = ParameterDirection.Input;
                        pnumero_aporte.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_aporte);

                        DbParameter pcod_atr = cmdTransaccionFactory.CreateParameter();
                        pcod_atr.ParameterName = "p_cod_atr";
                        if (pAportePendientes.cod_atr == null)
                            pcod_atr.Value = DBNull.Value;
                        else
                            pcod_atr.Value = pAportePendientes.cod_atr;
                        pcod_atr.Direction = ParameterDirection.Input;
                        pcod_atr.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_atr);

                        DbParameter pcod_det_list = cmdTransaccionFactory.CreateParameter();
                        pcod_det_list.ParameterName = "p_cod_det_list";
                        if (pAportePendientes.cod_det_list == null)
                            pcod_det_list.Value = DBNull.Value;
                        else
                            pcod_det_list.Value = pAportePendientes.cod_det_list;
                        pcod_det_list.Direction = ParameterDirection.Input;
                        pcod_det_list.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcod_det_list);

                        DbParameter ptipo_tran = cmdTransaccionFactory.CreateParameter();
                        ptipo_tran.ParameterName = "p_tipo_tran";
                        if (pAportePendientes.tipo_tran == null)
                            ptipo_tran.Value = DBNull.Value;
                        else
                            ptipo_tran.Value = pAportePendientes.tipo_tran;
                        ptipo_tran.Direction = ParameterDirection.Input;
                        ptipo_tran.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(ptipo_tran);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        if (pAportePendientes.valor == null)
                            pvalor.Value = DBNull.Value;
                        else
                            pvalor.Value = pAportePendientes.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        if (pAportePendientes.estado == null)
                            pestado.Value = DBNull.Value;
                        else
                            pestado.Value = pAportePendientes.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        DbParameter pnum_tran_anula = cmdTransaccionFactory.CreateParameter();
                        pnum_tran_anula.ParameterName = "p_num_tran_anula";
                        if (pAportePendientes.num_tran_anula == null)
                            pnum_tran_anula.Value = DBNull.Value;
                        else
                            pnum_tran_anula.Value = pAportePendientes.num_tran_anula;
                        pnum_tran_anula.Direction = ParameterDirection.Input;
                        pnum_tran_anula.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnum_tran_anula);

                        DbParameter pnumero_recaudo = cmdTransaccionFactory.CreateParameter();
                        pnumero_recaudo.ParameterName = "p_numero_recaudo";
                        if (pAportePendientes.numero_recaudo == null)
                            pnumero_recaudo.Value = DBNull.Value;
                        else
                            pnumero_recaudo.Value = pAportePendientes.numero_recaudo;
                        pnumero_recaudo.Direction = ParameterDirection.Input;
                        pnumero_recaudo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_recaudo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PORAPLICAR_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pAportePendientes.numero_transaccion = Convert.ToInt64(pnumero_transaccion.Value);
                        return pAportePendientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudosMasivosData", "CrearAportePendientes", ex);
                        return null;
                    }
                }
            }
        }



        public AportePendientes AplicarAportePendientes(AportePendientes pAportePendientes, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pcod_ope = cmdTransaccionFactory.CreateParameter();
                        pcod_ope.ParameterName = "p_cod_ope";
                        pcod_ope.Value = pAportePendientes.cod_ope;
                        pcod_ope.Direction = ParameterDirection.Input;
                        pcod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcod_ope);

                        DbParameter pnumero_transaccion = cmdTransaccionFactory.CreateParameter();
                        pnumero_transaccion.ParameterName = "p_numero_recaudo";
                        pnumero_transaccion.Value = pAportePendientes.numero_recaudo;
                        pnumero_transaccion.Direction = ParameterDirection.Input;
                        pnumero_transaccion.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pnumero_transaccion);

                        DbParameter pestado = cmdTransaccionFactory.CreateParameter();
                        pestado.ParameterName = "p_estado";
                        pestado.Value = pAportePendientes.estado;
                        pestado.Direction = ParameterDirection.Input;
                        pestado.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pestado);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_TES_PORAPLICAR_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pAportePendientes;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudosMasivosData", "AplicarAportePendientes", ex);
                        return null;
                    }
                }
            }
        }


        //EXTRACTOS
        public List<RecaudosMasivos> ListarRecaudoExtracto(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        Configuracion conf = new Configuracion();
                        string sql, opc;

                        if (pRecaudo.cod_empresa != 0 || pRecaudo.tipo_lista != 0 || pRecaudo.fecha_recaudo != DateTime.MinValue)
                            opc = "and";
                        else
                            opc = "where";
                        if (pRecaudo.periodo_corte == DateTime.MinValue || pRecaudo.periodo_corte == null && pRecaudo.estado == "2")
                            sql = @"SELECT A.*, B.NOM_EMPRESA, CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO,
                                    CASE A.ESTADO WHEN '1' THEN 'Pendiente' WHEN '2' THEN 'Aplicado' END AS NOM_ESTADO
                                    FROM RECAUDO_MASIVO A LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA " + ObtenerFiltro(pRecaudo, "A.") + " ORDER BY A.NUMERO_RECAUDO";
                        else
                        {
                            sql = @"select A.NUMERO_NOVEDAD as NUMERO_RECAUDO, A.TIPO_RECAUDO,
                                    CASE A.TIPO_RECAUDO WHEN 0 THEN 'Bancarios' WHEN 1 THEN 'Nomina' END AS NOM_TIPO_RECAUDO,
                                    A.COD_EMPRESA, B.NOM_EMPRESA, A.PERIODO_CORTE, A.FECHA_GENERACION AS FECHA_RECAUDO, NULL AS FECHA_APLICACION, A.ESTADO,
                                    CASE A.ESTADO WHEN 1 THEN 'Pendiente' WHEN 2 THEN 'Aplicado' END AS NOM_ESTADO, A.USUARIO_CREACION as USUARIOCREACION, 0 as COD_OFICINA
                                    from EMPRESA_NOVEDAD A
                                    LEFT JOIN EMPRESA_RECAUDO B ON A.COD_EMPRESA = B.COD_EMPRESA  " + ObtenerFiltro(pRecaudo, "A.");
                            if (sql.ToUpper().Contains("WHERE") == true)
                                opc = "";
                            // opc = " and ";
                            else
                                opc = " where "
                                //sql += opc + " trunc(PERIODO_CORTE) = To_Date('" + Convert.ToDateTime(pRecaudo.periodo_corte).ToString(conf.ObtenerFormatoFecha()) + "', '" + conf.ObtenerFormatoFecha() + "') "
                                + " ORDER BY A.NUMERO_NOVEDAD";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["TIPO_RECAUDO"] != DBNull.Value) entidad.tipo_recaudo = Convert.ToInt64(resultado["TIPO_RECAUDO"]);
                            if (resultado["NOM_TIPO_RECAUDO"] != DBNull.Value) entidad.nom_tipo_recaudo = Convert.ToString(resultado["NOM_TIPO_RECAUDO"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_RECAUDO"] != DBNull.Value) entidad.fecha_recaudo = Convert.ToDateTime(resultado["FECHA_RECAUDO"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOM_ESTADO"] != DBNull.Value) entidad.nom_estado = Convert.ToString(resultado["NOM_ESTADO"]);
                            if (resultado["USUARIOCREACION"] != DBNull.Value) entidad.usuario = Convert.ToString(resultado["USUARIOCREACION"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.cod_oficina = Convert.ToInt64(resultado["COD_OFICINA"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtracto(int pNumeroRecaudo, string estadoNom, bool bDetallado, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (estadoNom == "1")
                        {
                            sql = @"SELECT DISTINCT p.IDENTIFICACION, A.NUMERO_NOVEDAD as NUMERO_RECAUDO,
                                p.NOMBRE, count(A.TIPO_PRODUCTO) as TIPO_PRODUCTO, EMN.PERIODO_CORTE as PERIODO
                                from DETEMPRESA_NOVEDAD A
                                inner join v_persona p on A.COD_CLIENTE = p.COD_PERSONA
                                INNER JOIN EMPRESA_NOVEDAD EMN ON A.NUMERO_NOVEDAD = EMN.NUMERO_NOVEDAD
                                --inner join RECAUDO_MASIVO rm on A.NUMERO_NOVEDAD = rm.NUMERO_NOVEDAD
                                where A.NUMERO_NOVEDAD =  " + pNumeroRecaudo + " group by p.IDENTIFICACION, A.NUMERO_NOVEDAD, p.NOMBRE,  EMN.PERIODO_CORTE ORDER BY ORDER BY EMN.PERIODO_CORTE desc, p.identificacion";
                        }
                        else
                        {
                            sql = @"SELECT DISTINCT A.IDENTIFICACION, A.NUMERO_RECAUDO, A.NOMBRE, count(A.TIPO_PRODUCTO) as TIPO_PRODUCTO, B.PERIODO_CORTE AS PERIODO
                                FROM DETRECAUDO_MASIVO A
                                INNER JOIN RECAUDO_MASIVO B ON A.NUMERO_RECAUDO = B.NUMERO_RECAUDO
                                WHERE A.NUMERO_RECAUDO = " + pNumeroRecaudo + " group by A.IDENTIFICACION, A.NUMERO_RECAUDO, A.NOMBRE, B.PERIODO_CORTE ORDER BY B.PERIODO_CORTE desc, a.identificacion";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["PERIODO"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["PERIODO"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDetalleRecaudoConsultaExtractoxPersona(RecaudosMasivos pRecaudo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pRecaudo.estado == "2")
                        {
                            sql = @"SELECT DISTINCT A.IDENTIFICACION, A.NUMERO_RECAUDO, A.NOMBRE, count(A.TIPO_PRODUCTO) as TIPO_PRODUCTO, max(A.FECHACREACION) as FECHACREACION, RM.PERIODO_CORTE AS PERIODO
                                FROM DETRECAUDO_MASIVO A 
                                LEFT JOIN RECAUDO_MASIVO RM ON A.NUMERO_RECAUDO = RM.NUMERO_RECAUDO";

                            if (pRecaudo.identificacion != null)
                            {
                                sql = sql + "  where A.IDENTIFICACION = " + pRecaudo.identificacion;
                            }
                            if (pRecaudo.numero_recaudo != 0)
                            {
                                sql = sql + "  AND A.NUMERO_RECAUDO =" + pRecaudo.numero_recaudo;
                            }
                            if (pRecaudo.periodo_corte != null)
                            {
                                sql = sql + "  AND RM.PERIODO_CORTE =" + pRecaudo.periodo_corte;
                            }
                            if (pRecaudo.cod_empresa != 0)
                            {
                                sql = sql + "  AND RM.COD_EMPRESA =" + pRecaudo.cod_empresa;
                            }
                            sql = sql + "  AND RM.estado = " + pRecaudo.estado + "  group by A.IDENTIFICACION, A.NUMERO_RECAUDO, A.NOMBRE, RM.PERIODO_CORTE ORDER BY RM.PERIODO_CORTE DESC ";
                        }
                        else
                        {
                            sql = @"select P.IDENTIFICACION, A.NUMERO_NOVEDAD AS NUMERO_RECAUDO, P.NOMBRE, COUNT(A.TIPO_PRODUCTO) AS TIPO_PRODUCTO, max(A.FECHACREACION) as FECHACREACION, RM.PERIODO_CORTE AS PERIODO 
                                    from DETEMPRESA_NOVEDAD A
                                    left join EMPRESA_NOVEDAD RM ON A.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD
                                    LEFT JOIN V_PERSONA P ON A.COD_CLIENTE = P.COD_PERSONA
                                    LEFT JOIN RECAUDO_MASIVO R ON RM.NUMERO_NOVEDAD = R.NUMERO_NOVEDAD";

                            if (pRecaudo.identificacion != null)
                            {
                                sql = sql + "  where P.IDENTIFICACION = '" + pRecaudo.identificacion + "' ";
                            }
                            if (pRecaudo.numero_recaudo != 0)
                            {
                                sql = sql + "  AND RM.NUMERO_NOVEDAD =" + pRecaudo.numero_recaudo;
                            }
                            if (pRecaudo.periodo_corte != null)
                            {
                                sql = sql + "  AND RM.PERIODO_CORTE =" + pRecaudo.periodo_corte;
                            }
                            if (pRecaudo.cod_empresa != 0)
                            {
                                sql = sql + "  AND RM.COD_EMPRESA =" + pRecaudo.cod_empresa;
                            }
                            sql = sql + "  AND RM.estado = " + pRecaudo.estado + " group by P.IDENTIFICACION, A.NUMERO_NOVEDAD, P.NOMBRE,  RM.PERIODO_CORTE ORDER BY RM.PERIODO_CORTE DESC ";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            if (resultado["NUMERO_RECAUDO"] != DBNull.Value) entidad.numero_recaudo = Convert.ToInt64(resultado["NUMERO_RECAUDO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["PERIODO"] != DBNull.Value) entidad.fechacreacion = Convert.ToDateTime(resultado["PERIODO"]);


                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDetalleRecaudo", ex);
                        return null;
                    }
                }
            }
        }

        public List<RecaudosMasivos> ListarDeduccionesxPersona(RecaudosMasivos pRecaudo, ref string pError, Usuario vUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            string sql = "";
            List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        if (pRecaudo.estado == "2")
                        {
                            sql = @"WITH APLICA AS (SELECT TC.NUM_LISTA, TC.TIPO_PRODUCTO, TC.NUMERO_CUENTA AS NUMERO_PRODUCTO, TC.COD_CLIENTE, TC.COD_DET_LIS AS IDDETALLE,
                            SUM(CASE WHEN TC.COD_ATR = 1 THEN TC.VALOR ELSE 0 END) AS CAPITAL, 
                            SUM(CASE WHEN TC.COD_ATR = 2 THEN TC.VALOR ELSE 0 END) AS INTERES, 
                            SUM(CASE WHEN TC.COD_ATR = 3 THEN TC.VALOR ELSE 0 END) AS INTMORA, 
                            SUM(CASE WHEN TC.COD_ATR NOT IN (1, 2, 3) THEN TC.VALOR ELSE 0 END) AS OTROS
                            FROM V_TRANSACCIONES TC
                            WHERE TC.TIPO_OPE = 119
                            GROUP BY TC.NUM_LISTA, TC.TIPO_PRODUCTO, TC.NUMERO_CUENTA, TC.COD_CLIENTE, TC.COD_DET_LIS)
                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
                            COALESCE(VP.DESCRIPCION, VP.NOMBRE, DT.TIPO_PRODUCTO) AS TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, ER.NOM_EMPRESA,
                            TC.CAPITAL as capitalFnl, TC.INTERES as intcteFnl, TC.INTMORA as intmoraFnl, TC.OTROS As OtrosFnl,
                            VP.SALDO as SaldoFinal,
                            (SELECT SUM(DTEM.VALOR) FROM DETEMPRESA_NOVEDAD DTEM WHERE DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND CODIGOPRODUCTO(DTEM.TIPO_PRODUCTO) = CODIGOPRODUCTO(DT.TIPO_PRODUCTO)) as VALOR_COBRADO
                            FROM RECAUDO_MASIVO RM INNER JOIN DETRECAUDO_MASIVO DT ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
                            INNER JOIN V_PRODUCTO VP ON (CASE WHEN CODIGOPRODUCTO(DT.TIPO_PRODUCTO) = 10 THEN 2 ELSE CODIGOPRODUCTO(DT.TIPO_PRODUCTO) END) = VP.TIPO_PRODUCTO AND DT.NUMERO_PRODUCTO = VP.NUMERO_PRODUCTO
                            LEFT JOIN APLICA TC ON TC.NUM_LISTA = DT.NUMERO_RECAUDO AND (CASE WHEN CODIGOPRODUCTO(DT.TIPO_PRODUCTO) = 10 THEN 2 ELSE CODIGOPRODUCTO(DT.TIPO_PRODUCTO) END) = TC.TIPO_PRODUCTO AND TC.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND TC.COD_CLIENTE = DT.COD_CLIENTE 
                            AND ((CODIGOPRODUCTO(DT.TIPO_PRODUCTO) IN (2, 10) AND TC.IDDETALLE = DT.IDDETALLE) OR CODIGOPRODUCTO(DT.TIPO_PRODUCTO) NOT IN (2, 10))
                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
                            " AND P.IDENTIFICACION   = '" + pRecaudo.identificacion + "'" +                             
                            @" --DEVOLUCIONES
                            UNION
                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
                            ER.NOM_EMPRESA,
                            NVL(TD.VALOR, 0) as capitalFnl, 
                            0 as intcteFnl,0 as intmoraFnl, 0 as OtrosFnl,
                            0 as SaldoFinal, DTEM.VALOR as VALOR_COBRADO
                            FROM DETRECAUDO_MASIVO DT
                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
                            INNER JOIN DEVOLUCION D ON DT.NUMERO_PRODUCTO = D.NUM_DEVOLUCION AND DT.TIPO_PRODUCTO = 'Devolucion' AND D.ESTADO = 2
                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO
                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
                            LEFT OUTER JOIN TRAN_DEVOLUCION TD ON OP.COD_OPE = TD.COD_OPE AND DT.NUMERO_PRODUCTO = TD.NUM_DEVOLUCION 
                            lEFT OUTER JOIN TRAN_AFILIACION TAF ON  DT.IDDETALLE = TAF.COD_DET_LIS
                            LEFT OUTER JOIN OPERACION OP ON TAF.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
                            "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'" +
                            @"--AFILIACION
                            UNION
                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
                            ER.NOM_EMPRESA,
                            NVL(TAF.VALOR, 0) as capitalFnl, 
                            0 as intcteFnl, 0 as intmoraFnl,
                            0 as OtrosFnl, 0 as SaldoFinal,
                            DTEM.VALOR as VALOR_COBRADO
                            FROM DETRECAUDO_MASIVO DT
                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
                            INNER JOIN PERSONA_AFILIACION PAF ON DT.NUMERO_PRODUCTO = PAF.IDAFILIACION AND DT.TIPO_PRODUCTO = 'Afiliacion' AND PAF.ESTADO = 'A'
                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO
                            lEFT OUTER JOIN TRAN_AFILIACION TAF ON  DT.IDDETALLE = TAF.COD_DET_LIS
                            LEFT OUTER JOIN OPERACION OP ON TAF.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
                            "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'"; 
                        }
                        else
                        {
                            sql = @"select EM.NOM_EMPRESA, P.NOMBRE, P.COD_NOMINA, EN.PERIODO_CORTE, EN.FECHA_GENERACION AS FECHA_APLICACION,
                                    CASE 
                                    WHEN DT.TIPO_PRODUCTO LIKE '%SERVICIOS%'   THEN 
                                    (SELECT LC.NOMBRE FROM LINEASSERVICIOS LC 
                                    INNER JOIN SERVICIOS C ON LC.COD_LINEA_SERVICIO = C.COD_LINEA_SERVICIO WHERE C.NUMERO_SERVICIO = DT.NUMERO_PRODUCTO)                                    
                                    WHEN DT.TIPO_PRODUCTO = 'CREDITOS' THEN 
                                    (SELECT LC.NOMBRE FROM LINEASCREDITO LC 
                                    INNER JOIN CREDITO C ON LC.COD_LINEA_CREDITO = C.COD_LINEA_CREDITO WHERE C.NUMERO_RADICACION = DT.NUMERO_PRODUCTO)                                    
                                    WHEN DT.TIPO_PRODUCTO = 'DEPOSITOS' THEN 
                                    'AHORROS A LA VISTA'
                                    WHEN DT.TIPO_PRODUCTO = 'APORTES' THEN
                                    'APORTES SOCIALES Y AHORRO PERMANENTE'
                                    ELSE
                                    DT.TIPO_PRODUCTO
                                    END AS TIPO_PRODUCTO,
                                    DT.NUMERO_PRODUCTO, DT.IDDETALLE, P.COD_PERSONA, 
                                    DT.SALDO_PRODUCTO AS capitalFnl,
                                    0 AS intcteFnl,
                                    0 AS intmoraFnl,
                                    0 AS OtrosFnl,
                                    0 AS SaldoFinal,
                                    DT.VALOR,
                                    0 AS VALOR_COBRADO
                                    from DETEMPRESA_NOVEDAD DT 
                                    LEFT JOIN EMPRESA_NOVEDAD EN ON DT.NUMERO_NOVEDAD=EN.NUMERO_NOVEDAD
                                    LEFT JOIN EMPRESA_RECAUDO EM ON EN.COD_EMPRESA = EM.COD_EMPRESA
                                    INNER JOIN V_PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
                                    --INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD
                                    WHERE DT.NUMERO_NOVEDAD = " + pRecaudo.numero_recaudo +
                                    "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'";
                        }
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            RecaudosMasivos entidad = new RecaudosMasivos();

                            //ENCABEZADO
                            if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
                            //if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
                            entidad.identificacion = pRecaudo.identificacion;
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
                            if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
                            if (resultado["FECHA_APLICACION"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);

                            //DETALLE DEL EXTRACTO
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["capitalFnl"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["capitalFnl"]);
                            if (resultado["intcteFnl"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["intcteFnl"]);
                            if (resultado["intmoraFnl"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["intmoraFnl"]);
                            if (resultado["OtrosFnl"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["OtrosFnl"]);
                            if (resultado["SaldoFinal"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["SaldoFinal"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor_aplicado = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["VALOR_COBRADO"] != DBNull.Value) entidad.valor_novedad = Convert.ToDecimal(resultado["VALOR_COBRADO"]);

                            lstRecaudosMasivos.Add(entidad);
                        }

                        return lstRecaudosMasivos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("RecaudoMasivosData", "ListarDeduccionesxPersona", ex);
                        return null;
                    }
                }
            }
        }
        //public List<RecaudosMasivos> ListarDeduccionesxPersona(RecaudosMasivos pRecaudo, ref string pError, Usuario vUsuario)
        //{
        //    DbDataReader resultado = default(DbDataReader);
        //    List<RecaudosMasivos> lstRecaudosMasivos = new List<RecaudosMasivos>();

        //    using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
        //    {
        //        using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
        //        {
        //            try
        //            {

        //                string sql = "";
        //                if (pRecaudo.estado == "2")
        //                {
        //                    sql = @"--CREDITOS
        //                            SELECT distinct DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            COALESCE(DN.DESCRIPCION, LC.NOMBRE , DT.TIPO_PRODUCTO) AS TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(sum(Decode(TC.cod_atr, 1, TC.valor)), 0) as capitalFnl, 
        //                            NVL(sum(Decode(TC.cod_atr, 2, TC.valor)), 0) as intcteFnl,
        //                            NVL(sum(Decode(TC.cod_atr, 3, TC.valor)), 0) as intmoraFnl,
        //                            NVL(Sum(Decode(TC.cod_atr,  1, 0, 2, 0, 3, 0, 7, 0, 25, 0, 26, 0, 40, 0, 41, 0, TC.valor)), 0) As OtrosFnl,
        //                            CR.SALDO_CAPITAL as SaldoFinal,
        //                            --DTEM.VALOR as VALOR_COBRADO
        //                            NULL  as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN CREDITO CR ON DT.NUMERO_PRODUCTO = CR.NUMERO_RADICACION AND   (DT.TIPO_PRODUCTO like '%Creditos%' or DT.TIPO_PRODUCTO like '%CREDITOS%'  )
        //                            AND CR.ESTADO NOT IN('N','B','M','Z')
        //                            LEFT OUTER JOIN LINEASCREDITO LC ON CR.COD_LINEA_CREDITO = LC.COD_LINEA_CREDITO
        //                            LEFT OUTER JOIN SOLICITUDCRED SC ON CR.NUMERO_OBLIGACION = SC.NUMEROSOLICITUD 
        //                            LEFT OUTER JOIN DESTINACION DN ON SC.DESTINO = DN.COD_DESTINO  
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND UPPER(DTEM.TIPO_PRODUCTO) LIKE '%CREDITO%'
        //                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
        //                            LEFT OUTER JOIN TRAN_CRED TC ON OP.COD_OPE = TC.COD_OPE AND P.COD_PERSONA = TC.COD_CLIENTE AND DT.NUMERO_PRODUCTO = TC.NUMERO_RADICACION
        //                            --LEFT OUTER JOIN TRAN_CRED TC ON  DT.IDDETALLE = TC.COD_DET_LIS
        //                            --LEFT OUTER JOIN OPERACION OP ON TC.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                " AND P.IDENTIFICACION   = '" + pRecaudo.identificacion + "'" +
        //                                @" group by DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA, DN.DESCRIPCION, LC.NOMBRE, DT.TIPO_PRODUCTO,
        //                            DT.NUMERO_PRODUCTO, DT.VALOR, RM.PERIODO_CORTE, RM.FECHA_APLICACION, ER.NOM_EMPRESA, CR.SALDO_CAPITAL,DTEM.VALOR
        //                            UNION
        //                             --APORTES
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            LA.NOMBRE AS TIPO_PRODUCTO, DT.NUMERO_PRODUCTO, 
        //                            case when LA.NOMBRE = 'AHORRO PERMANENTE' then 0 else DT.VALOR end as VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(sum(Decode(TA.cod_atr, 1, TA.valor)), 0) as capitalFnl, 
        //                            NVL(sum(Decode(TA.cod_atr, 2, TA.valor)), 0) as intcteFnl,
        //                            NVL(sum(Decode(TA.cod_atr, 3, TA.valor)), 0) as intmoraFnl,
        //                            NVL(Sum(Decode(TA.cod_atr, 1, 0, 2, 0, 3, 0, 7, 0, 25, 0, 26, 0, 40, 0, 41, 0, TA.valor)), 0) As OtrosFnl,
        //                            AP.SALDO as SaldoFinal,
        //                            case when LA.NOMBRE = 'AHORRO PERMANENTE' then 0 else DTEM.VALOR end as VALOR_COBRADO 
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND UPPER(DTEM.TIPO_PRODUCTO) LIKE '%APORTE%'
        //                            --LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
        //                            --LEFT OUTER JOIN TRAN_APORTE TA ON OP.COD_OPE = TA.COD_OPE AND P.COD_PERSONA = TA.COD_PERSONA AND DT.NUMERO_PRODUCTO = TA.NUMERO_APORTE
        //                            LEFT OUTER JOIN TRAN_APORTE TA ON  DT.IDDETALLE = TA.COD_DET_LIS
        //                            INNER JOIN APORTE AP ON TA.NUMERO_APORTE = AP.NUMERO_APORTE AND AP.ESTADO = 1
        //                            INNER JOIN LINEAAPORTE LA ON AP.COD_LINEA_APORTE = LA.COD_LINEA_APORTE  
        //                            LEFT OUTER JOIN OPERACION OP ON TA.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                " AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'" +
        //                                @" group by DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA, LA.NOMBRE, 
        //                            DT.NUMERO_PRODUCTO, DT.VALOR, RM.PERIODO_CORTE, RM.FECHA_APLICACION, ER.NOM_EMPRESA, AP.SALDO, DTEM.VALOR                                   
        //                            UNION                                 
        //                             --SERVICIOS
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            LS.NOMBRE TIPO_PRODUCTO,
        //                            DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(sum(Decode(TS.cod_atr, 1, TS.valor)), 0) as capitalFnl, 
        //                            NVL(sum(Decode(TS.cod_atr, 2, TS.valor)), 0) as intcteFnl,
        //                            0 as intmoraFnl,
        //                            0 As OtrosFnl,
        //                            S.SALDO as SaldoFinal,
        //                            DTEM.VALOR as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN SERVICIOS S ON DT.NUMERO_PRODUCTO = S.NUMERO_SERVICIO 
        //                            AND   (DT.TIPO_PRODUCTO like '%Servicios%' or DT.TIPO_PRODUCTO like '%SERVICIOS%'  ) -- AND S.ESTADO <> 'T'
        //                            INNER JOIN   LS ON LS.COD_LINEA_SERVICIO = S.COD_LINEA_SERVICIO
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND UPPER(DTEM.TIPO_PRODUCTO) LIKE '%SERVICIO%'
        //                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119
        //                            LEFT OUTER JOIN TRAN_SERVICIOS TS ON OP.COD_OPE = TS.COD_OPE AND P.COD_PERSONA = TS.COD_CLIENTE AND DT.NUMERO_PRODUCTO = TS.NUMERO_SERVICIO 
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                               "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'" +
        //                                @" group by DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,  LS.NOMBRE, 
        //                            DT.NUMERO_PRODUCTO, DT.VALOR, RM.PERIODO_CORTE, RM.FECHA_APLICACION, ER.NOM_EMPRESA, 0, 0, S.SALDO, DTEM.VALOR
        //                             --AHORRO A LA VISTA
        //                            UNION
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(TAH.VALOR, 0) as capitalFnl, 
        //                            0 as intcteFnl,
        //                            0 as intmoraFnl,
        //                            0 as OtrosFnl,
        //                            AHV.SALDO_TOTAL as SaldoFinal,
        //                            DTEM.VALOR as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN AHORRO_VISTA AHV ON DT.NUMERO_PRODUCTO = AHV.NUMERO_CUENTA
        //                            AND (DT.TIPO_PRODUCTO LIKE '%ahorros a la Vista%' OR DT.TIPO_PRODUCTO LIKE '%DEPOSITOS%'  ) AND AHV.ESTADO NOT IN(2, 3, 4)
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND CODIGOPRODUCTO(DTEM.TIPO_PRODUCTO) = 3
        //                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
        //                            LEFT OUTER JOIN TRAN_AHORRO TAH ON OP.COD_OPE = TAH.COD_OPE AND P.COD_PERSONA = TAH.COD_CLIENTE AND DT.NUMERO_PRODUCTO = TAH.NUMERO_CUENTA AND (UPPER(DTEM.TIPO_PRODUCTO) LIKE '%DEPOSITOS%' OR UPPER(DTEM.TIPO_PRODUCTO) LIKE '%VISTA%')
        //                            --lEFT JOIN TRAN_AHORRO TAH ON DT.IDDETALLE = TAH.COD_DET_LIS
        //                            --LEFT OUTER JOIN OPERACION OP ON TAH.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                "AND P.IDENTIFICACION   = '" + pRecaudo.identificacion + "'" +
        //                                @" --AHORRO PROGRAMADO
        //                            UNION
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(TAHP.VALOR, 0) as capitalFnl, 
        //                            0 as intcteFnl,
        //                            0 as intmoraFnl,
        //                            0 as OtrosFnl,
        //                            AHP.SALDO as SaldoFinal,
        //                            DTEM.VALOR as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN AHORRO_PROGRAMADO AHP ON DT.NUMERO_PRODUCTO = AHP.NUMERO_PROGRAMADO 
        //                            AND (DT.TIPO_PRODUCTO LIKE '%ahorro programado%' OR DT.TIPO_PRODUCTO LIKE '%AHORRO PROGRAMADO%') AND AHP.ESTADO NOT IN(2, 3, 4)
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO AND CODIGOPRODUCTO(DTEM.TIPO_PRODUCTO) = 9
        //                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
        //                            LEFT OUTER JOIN TRAN_PROGRAMADO TAHP ON OP.COD_OPE = TAHP.COD_OPE AND P.COD_PERSONA = TAHP.COD_CLIENTE AND DT.NUMERO_PRODUCTO = TAHP.NUMERO_PROGRAMADO AND UPPER(DTEM.TIPO_PRODUCTO) LIKE '%PROGRAMA%'
        //                            --lEFT OUTER JOIN TRAN_PROGRAMADO TAHP ON  DT.IDDETALLE = TAHP.COD_DET_LIS
        //                            --LEFT OUTER JOIN OPERACION OP ON TAHP.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                "AND P.IDENTIFICACION   ='" + pRecaudo.identificacion + "'" +
        //                                @" --DEVOLUCIONES
        //                            UNION
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(TD.VALOR, 0) as capitalFnl, 
        //                            0 as intcteFnl,
        //                            0 as intmoraFnl,
        //                            0 as OtrosFnl,
        //                            0 as SaldoFinal,
        //                            DTEM.VALOR as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN DEVOLUCION D ON DT.NUMERO_PRODUCTO = D.NUM_DEVOLUCION AND DT.TIPO_PRODUCTO = 'Devolucion' AND D.ESTADO = 2
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO
        //                            LEFT OUTER JOIN OPERACION OP ON DT.NUMERO_RECAUDO = OP.NUM_LISTA AND OP.TIPO_OPE = 119 
        //                            LEFT OUTER JOIN TRAN_DEVOLUCION TD ON OP.COD_OPE = TD.COD_OPE AND DT.NUMERO_PRODUCTO = TD.NUM_DEVOLUCION 
        //                            --lEFT OUTER JOIN TRAN_AFILIACION TAF ON  DT.IDDETALLE = TAF.COD_DET_LIS
        //                            --LEFT OUTER JOIN OPERACION OP ON TAF.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'" +
        //                                @"--AFILIACION
        //                            UNION
        //                            SELECT DT.NOMBRE, P.COD_NOMINA, DT.IDDETALLE, P.COD_PERSONA,
        //                            DT.TIPO_PRODUCTO, DT.NUMERO_PRODUCTO,  DT.VALOR,
        //                            RM.PERIODO_CORTE, RM.FECHA_APLICACION, 
        //                            ER.NOM_EMPRESA,
        //                            NVL(TAF.VALOR, 0) as capitalFnl, 
        //                            0 as intcteFnl,
        //                            0 as intmoraFnl,
        //                            0 as OtrosFnl,
        //                            0 as SaldoFinal,
        //                            DTEM.VALOR as VALOR_COBRADO
        //                            FROM DETRECAUDO_MASIVO DT
        //                            INNER JOIN PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            INNER JOIN PERSONA_AFILIACION PAF ON DT.NUMERO_PRODUCTO = PAF.IDAFILIACION AND DT.TIPO_PRODUCTO = 'Afiliacion' AND PAF.ESTADO = 'A'
        //                            INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_RECAUDO = RM.NUMERO_RECAUDO
        //                            INNER JOIN EMPRESA_RECAUDO ER ON RM.COD_EMPRESA = ER.COD_EMPRESA
        //                            INNER JOIN DETEMPRESA_NOVEDAD DTEM on DTEM.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD AND DTEM.NUMERO_PRODUCTO = DT.NUMERO_PRODUCTO
        //                            lEFT OUTER JOIN TRAN_AFILIACION TAF ON  DT.IDDETALLE = TAF.COD_DET_LIS
        //                            LEFT OUTER JOIN OPERACION OP ON TAF.COD_OPE = OP.COD_OPE  And OP.TIPO_OPE = 119
        //                            WHERE DT.NUMERO_RECAUDO = " + pRecaudo.numero_recaudo +
        //                                "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'";
        //                }
        //                else
        //                {
        //                    sql = @"select EM.NOM_EMPRESA, P.NOMBRE, P.COD_NOMINA, EN.PERIODO_CORTE, EN.FECHA_GENERACION AS FECHA_APLICACION,
        //                            CASE 
        //                            WHEN DT.TIPO_PRODUCTO LIKE '%SERVICIOS%'   THEN 
        //                            (SELECT LC.NOMBRE FROM LINEASSERVICIOS LC 
        //                            INNER JOIN SERVICIOS C ON LC.COD_LINEA_SERVICIO = C.COD_LINEA_SERVICIO WHERE C.NUMERO_SERVICIO = DT.NUMERO_PRODUCTO)                                    
        //                            WHEN DT.TIPO_PRODUCTO = 'CREDITOS' THEN 
        //                            (SELECT LC.NOMBRE FROM LINEASCREDITO LC 
        //                            INNER JOIN CREDITO C ON LC.COD_LINEA_CREDITO = C.COD_LINEA_CREDITO WHERE C.NUMERO_RADICACION = DT.NUMERO_PRODUCTO)                                    
        //                            WHEN DT.TIPO_PRODUCTO = 'DEPOSITOS' THEN 
        //                            'AHORROS A LA VISTA'
        //                            WHEN DT.TIPO_PRODUCTO = 'APORTES' THEN
        //                            'APORTES SOCIALES Y AHORRO PERMANENTE'
        //                            ELSE
        //                            DT.TIPO_PRODUCTO
        //                            END AS TIPO_PRODUCTO,
        //                            DT.NUMERO_PRODUCTO, DT.IDDETALLE, P.COD_PERSONA, 
        //                            DT.SALDO_PRODUCTO AS capitalFnl,
        //                            0 AS intcteFnl,
        //                            0 AS intmoraFnl,
        //                            0 AS OtrosFnl,
        //                            0 AS SaldoFinal,
        //                            DT.VALOR,
        //                            0 AS VALOR_COBRADO
        //                            from DETEMPRESA_NOVEDAD DT 
        //                            LEFT JOIN EMPRESA_NOVEDAD EN ON DT.NUMERO_NOVEDAD=EN.NUMERO_NOVEDAD
        //                            LEFT JOIN EMPRESA_RECAUDO EM ON EN.COD_EMPRESA = EM.COD_EMPRESA
        //                            INNER JOIN V_PERSONA P ON DT.COD_CLIENTE = P.COD_PERSONA
        //                            --INNER JOIN RECAUDO_MASIVO RM ON DT.NUMERO_NOVEDAD = RM.NUMERO_NOVEDAD
        //                            WHERE DT.NUMERO_NOVEDAD = " + pRecaudo.numero_recaudo +
        //                            "AND P.IDENTIFICACION = '" + pRecaudo.identificacion + "'";
        //                }

        //                connection.Open();
        //                cmdTransaccionFactory.Connection = connection;
        //                cmdTransaccionFactory.CommandType = CommandType.Text;
        //                cmdTransaccionFactory.CommandText = sql;
        //                resultado = cmdTransaccionFactory.ExecuteReader();

        //                while (resultado.Read())
        //                {
        //                    RecaudosMasivos entidad = new RecaudosMasivos();

        //                    //ENCABEZADO
        //                    if (resultado["NOM_EMPRESA"] != DBNull.Value) entidad.nom_empresa = Convert.ToString(resultado["NOM_EMPRESA"]);
        //                    //if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion = Convert.ToString(resultado["IDENTIFICACION"]);
        //                    entidad.identificacion = pRecaudo.identificacion;
        //                    if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOMBRE"]);
        //                    if (resultado["COD_NOMINA"] != DBNull.Value) entidad.cod_nomina_empleado = Convert.ToString(resultado["COD_NOMINA"]);
        //                    if (resultado["PERIODO_CORTE"] != DBNull.Value) entidad.periodo_corte = Convert.ToDateTime(resultado["PERIODO_CORTE"]);
        //                    if (resultado["FECHA_APLICACION"] != DBNull.Value) entidad.fecha_aplicacion = Convert.ToDateTime(resultado["FECHA_APLICACION"]);

        //                    //DETALLE DEL EXTRACTO
        //                    if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
        //                    if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_PRODUCTO"]);
        //                    if (resultado["IDDETALLE"] != DBNull.Value) entidad.iddetalle = Convert.ToInt64(resultado["IDDETALLE"]);
        //                    if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_cliente = Convert.ToInt64(resultado["COD_PERSONA"]);
        //                    if (resultado["capitalFnl"] != DBNull.Value) entidad.capital = Convert.ToDecimal(resultado["capitalFnl"]);
        //                    if (resultado["intcteFnl"] != DBNull.Value) entidad.intcte = Convert.ToDecimal(resultado["intcteFnl"]);
        //                    if (resultado["intmoraFnl"] != DBNull.Value) entidad.intmora = Convert.ToDecimal(resultado["intmoraFnl"]);
        //                    if (resultado["OtrosFnl"] != DBNull.Value) entidad.otros = Convert.ToDecimal(resultado["OtrosFnl"]);
        //                    if (resultado["SaldoFinal"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["SaldoFinal"]);
        //                    if (resultado["VALOR"] != DBNull.Value) entidad.valor_aplicado = Convert.ToDecimal(resultado["VALOR"]);
        //                    if (resultado["VALOR_COBRADO"] != DBNull.Value) entidad.valor_novedad = Convert.ToDecimal(resultado["VALOR_COBRADO"]);

        //                    lstRecaudosMasivos.Add(entidad);
        //                }

        //                return lstRecaudosMasivos;
        //            }
        //            catch (Exception ex)
        //            {
        //                BOExcepcion.Throw("RecaudoMasivosData", "ListarDeduccionesxPersona", ex);
        //                return null;
        //            }
        //        }
        //    }
        //}

        public bool LineaEsAporte(Int64 pNumeroAporte, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            bool esAporte = true;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT L.TIPO_APORTE FROM APORTE A JOIN LINEAAPORTE L ON A.COD_LINEA_APORTE = L.COD_LINEA_APORTE 
                                        WHERE A.NUMERO_APORTE = " + pNumeroAporte.ToString();

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            int? _tipo_aporte = null;

                            if (resultado["TIPO_APORTE"] != DBNull.Value) _tipo_aporte = Convert.ToInt32(resultado["TIPO_APORTE"]);

                            if (_tipo_aporte != null)
                                if (_tipo_aporte != 3)
                                    esAporte = false;
                        }

                        return esAporte;
                    }
                    catch (Exception ex)
                    {
                        return esAporte;
                    }
                }
            }
        }




    }
}