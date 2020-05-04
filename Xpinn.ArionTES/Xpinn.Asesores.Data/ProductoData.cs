using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Asesores.Entities;

namespace Xpinn.Asesores.Data
{
    /// <summary>
    /// Objeto de acceso a datos para la tabla v_productos
    /// </summary>
    public class ProductoData : GlobalData
    {
        protected ConnectionDataBase dbConnectionFactory;

        /// <summary>
        /// Constructor del objeto de acceso a datos para la tabla v_productos
        /// </summary>
        public ProductoData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public List<Producto> ListarProductosPorEstados(Producto pEntityProducto, Usuario pUsuario, String FiltroEstados)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstProductos = new List<Producto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        // Se muestran los datos de los créditos. Se quito condición del saldo de capital porque se usa filtros de estado.
                        if (pEntityProducto.Persona.IdPersona != 0)
                            if (pEntityProducto.Cuota == 0)
                                sql = "SELECT VAsesoresClienteProductos.*,VAsesoresClienteProductos.FECHA_PROXIMO_PAGO H_PAGO, VAsesoresClienteProductos.Saldo_capital SALDO, CALCULAR_VR_DESC_NOMINA(VAsesoresClienteProductos.numero_radicacion, VAsesoresClienteProductos.cod_deudor) AS VR_ULT_DSCTO, CONSULTAR_PAGADURIAS(VAsesoresClienteProductos.numero_radicacion) AS PAGADURIAS ,CALCULAR_VR_CUOEXTRAS(VAsesoresClienteProductos.numero_radicacion,sysdate)  as ValorCE FROM  VAsesoresClienteProductos WHERE " + FiltroEstados + " AND cod_deudor = " + pEntityProducto.Persona.IdPersona + " ORDER BY saldo_capital DESC, estado asc";
                            else
                                sql = @"Select v.*, h.saldo_capital Saldo_capital, h.fecha_proximo_pago h_pago,
                                        CALCULAR_VR_DESC_NOMINA(V.numero_radicacion,
                                        V.cod_deudor) AS VR_ULT_DSCTO, CONSULTAR_PAGADURIAS(V.numero_radicacion) AS PAGADURIAS ,
                                        CALCULAR_VR_CUOEXTRAS(V.numero_radicacion,sysdate)  as ValorCE
                                        From Historico_cre h inner join VAsesoresClienteProductos v on v.numero_radicacion = h.numero_radicacion inner join Lineascredito l on l.cod_linea_credito = h.cod_linea_credito
                                        inner join Credito c on c.numero_radicacion = h.numero_radicacion
                                        left join Garantias g on g.Numero_radicacion = h.Numero_radicacion
                                        Where " + FiltroEstados + @"  and h.cod_cliente = " + pEntityProducto.Persona.IdPersona + @" Order by h.saldo_capital desc, h.estado asc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Producto entidad = new Producto();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.CodRadicacion = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Persona.IdPersona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO"] != DBNull.Value)
                                entidad.Proceso = Convert.ToString(resultado["PROCESO"]);
                            else
                                entidad.Proceso = "                    ";
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.SaldoCapital = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["GARANTIAS_COMUNITARIAS"] != DBNull.Value) entidad.garantiacomunitaria = Convert.ToInt64(resultado["GARANTIAS_COMUNITARIAS"]);
                            if (resultado["cuota"] != DBNull.Value) entidad.ValorCuota = Convert.ToInt64(resultado["cuota"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.OtrosSaldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CODEUDORES"] != DBNull.Value) entidad.Codeudor = Convert.ToInt64(resultado["CODEUDORES"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.Garantia = Convert.ToInt64(resultado["GARANTIA"]);
                            if (resultado["h_pago"] != DBNull.Value)
                            {
                                entidad.FechaProximoPago = Convert.ToDateTime(resultado["h_pago"]);
                            }
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.ValorAPagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.ValorTotalAPagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CALIF_PROMEDIO"] != DBNull.Value) entidad.CalifPromedio = Convert.ToInt64(resultado["CALIF_PROMEDIO"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.Pagare = resultado["pagare"].ToString();
                            if (resultado["CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["cuota"]);
                            if (resultado["ATRIBUTOS"] != DBNull.Value) entidad.Atributos = Convert.ToInt64(resultado["atributos"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["cod_linea_credito"].ToString());
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.CuotasPendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"].ToString());
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.TipoLiquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"].ToString());
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.PeriodoGracia = Convert.ToInt64(resultado["PERIODO_GRACIA"].ToString());
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.CodClasifica = Convert.ToInt64(resultado["COD_CLASIFICA"].ToString());
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.Ejecutivo.IdEjecutivo = Convert.ToInt64(resultado["COD_ASESOR_COM"].ToString());
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.TipoCredito = resultado["TIPO_CREDITO"].ToString();
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.NumRadicOrigen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"].ToString());
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.Empresa.IdEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"].ToString());
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.CodPagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"].ToString());
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.Gradiente = Convert.ToDecimal(resultado["GRADIENTE"].ToString());
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicion = Convert.ToInt64(resultado["NUMERO_RADICACION"].ToString());
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["VR_ULT_DSCTO"] != DBNull.Value) entidad.vr_ult_dscto = Convert.ToDecimal(resultado["VR_ULT_DSCTO"]);
                            if (resultado["PAGADURIAS"] != DBNull.Value) entidad.pagadurias = Convert.ToString(resultado["PAGADURIAS"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.FechaVencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.TipoLinea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (resultado["TASAINTCTE"] != DBNull.Value) entidad.Tasainteres = Convert.ToDecimal(resultado["TASAINTCTE"]);
                            if (resultado["Fecha_Reestructurado"] != DBNull.Value) entidad.FechaReestructurado = resultado["Fecha_Reestructurado"].ToString();
                            if (resultado["ValorCE"] != DBNull.Value) entidad.valorapagarCE = Convert.ToDecimal(resultado["ValorCE"].ToString());

                            entidad.ValorTotalAPagarCE = entidad.ValorAPagar + entidad.valorapagarCE;


                            //rotativo
                            if (entidad.TipoLinea == 2 & entidad.SaldoCapital == 0 & entidad.MontoAprobado != 0)
                            {
                                entidad.Estado = "Desembolsado";
                            }
                            if (entidad.TipoLinea == 2)
                                entidad.Disponible = entidad.MontoAprobado - entidad.SaldoCapital;
                            else
                                entidad.Disponible = 0;

                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosPorEstados", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla v_productos dados unos filtros
        /// </summary>
        /// <param name="pv_productos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductos(Producto pEntityProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstProductos = new List<Producto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "";
                        if (pEntityProducto.Persona.IdPersona != null && pEntityProducto.Persona.IdPersona != 0)
                            sql = "SELECT VAsesoresClienteProductos.*,CALCULAR_VR_DESC_NOMINA(VAsesoresClienteProductos.numero_radicacion,VAsesoresClienteProductos.cod_deudor) AS VR_ULT_DSCTO,CONSULTAR_PAGADURIAS(VAsesoresClienteProductos.numero_radicacion) AS PAGADURIAS ,CALCULAR_VR_CUOEXTRAS(VAsesoresClienteProductos.numero_radicacion,sysdate)  as ValorCE FROM  VAsesoresClienteProductos WHERE estado != 'TERMINADO' AND cod_deudor = " + pEntityProducto.Persona.IdPersona + " ORDER BY saldo_capital DESC, estado";
                        else
                            sql = "SELECT VAsesoresClienteProductos.*,CALCULAR_VR_DESC_NOMINA(VAsesoresClienteProductos.numero_radicacion,VAsesoresClienteProductos.cod_deudor) AS VR_ULT_DSCTO,CONSULTAR_PAGADURIAS(VAsesoresClienteProductos.numero_radicacion) AS PAGADURIAS ,CALCULAR_VR_CUOEXTRAS(VAsesoresClienteProductos.numero_radicacion,sysdate)  as ValorCE FROM  VAsesoresClienteProductos WHERE estado != 'TERMINADO' ORDER BY saldo_capital DESC, estado";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Producto entidad = new Producto();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.CodRadicacion = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Persona.IdPersona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO"] != DBNull.Value) entidad.Proceso = Convert.ToString(resultado["PROCESO"]);
                            else
                                entidad.Proceso = "                    ";
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.SaldoCapital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["GARANTIAS_COMUNITARIAS"] != DBNull.Value) entidad.garantiacomunitaria = Convert.ToInt64(resultado["GARANTIAS_COMUNITARIAS"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.ValorCuota = Convert.ToInt64(resultado["cuota"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.OtrosSaldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CODEUDORES"] != DBNull.Value) entidad.Codeudor = Convert.ToInt64(resultado["CODEUDORES"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.Garantia = Convert.ToInt64(resultado["GARANTIA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.FechaProximoPago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.ValorAPagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.ValorTotalAPagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CALIF_PROMEDIO"] != DBNull.Value) entidad.CalifPromedio = Convert.ToInt64(resultado["CALIF_PROMEDIO"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.Pagare = resultado["PAGARE"].ToString();
                            if (resultado["CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["ATRIBUTOS"] != DBNull.Value) entidad.Atributos = Convert.ToInt64(resultado["ATRIBUTOS"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["COD_LINEA_CREDITO"].ToString());
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.CuotasPendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"].ToString());
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.TipoLiquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"].ToString());
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.PeriodoGracia = Convert.ToInt64(resultado["PERIODO_GRACIA"].ToString());
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.CodClasifica = Convert.ToInt64(resultado["COD_CLASIFICA"].ToString());
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.Ejecutivo.IdEjecutivo = Convert.ToInt64(resultado["COD_ASESOR_COM"].ToString());
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.TipoCredito = resultado["TIPO_CREDITO"].ToString();
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.NumRadicOrigen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"].ToString());
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.Empresa.IdEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"].ToString());
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.CodPagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"].ToString());
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.Gradiente = Convert.ToDecimal(resultado["GRADIENTE"].ToString());
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicion = Convert.ToInt64(resultado["NUMERO_RADICACION"].ToString());
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["VR_ULT_DSCTO"] != DBNull.Value) entidad.vr_ult_dscto = Convert.ToDecimal(resultado["VR_ULT_DSCTO"]);
                            if (resultado["PAGADURIAS"] != DBNull.Value) entidad.pagadurias = Convert.ToString(resultado["PAGADURIAS"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.FechaVencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["Fecha_Reestructurado"] != DBNull.Value) entidad.FechaReestructurado = resultado["Fecha_Reestructurado"].ToString();
                            if (resultado["ValorCE"] != DBNull.Value) entidad.valorapagarCE = Convert.ToDecimal(resultado["ValorCE"].ToString());

                            entidad.ValorTotalAPagarCE = entidad.ValorAPagar + entidad.valorapagarCE;
                            //rotativo 

                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.TipoLinea = Convert.ToInt64(resultado["TIPO_LINEA"]);
                            if (entidad.TipoLinea == 2)
                                entidad.Disponible = entidad.MontoAprobado - entidad.SaldoCapital;
                            else
                                entidad.Disponible = 0;

                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductos", ex);
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Obtiene una lista de Entidades de la tabla v_productos dados unos filtros
        /// </summary>
        /// <param name="pv_productos">Entidad con los filtros solicitados</param>
        /// <returns>Conjunto de Productos obtenidos</returns>
        public List<Producto> ListarProductosTodos(Producto pEntityProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstProductos = new List<Producto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VAsesoresClienteProductos WHERE cod_deudor = " + pEntityProducto.Persona.IdPersona + " and saldo_capital>0 order by SALDO_CAPITAL DESC, ESTADO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Producto entidad = new Producto();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.CodRadicacion = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Persona.IdPersona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO"] != DBNull.Value) entidad.Proceso = Convert.ToString(resultado["PROCESO"]);
                            else
                                entidad.Proceso = "                    ";
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.SaldoCapital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["GARANTIAS_COMUNITARIAS"] != DBNull.Value) entidad.garantiacomunitaria = Convert.ToInt64(resultado["GARANTIAS_COMUNITARIAS"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.ValorCuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.OtrosSaldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CODEUDORES"] != DBNull.Value) entidad.Codeudor = Convert.ToInt64(resultado["CODEUDORES"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.Garantia = Convert.ToInt64(resultado["GARANTIA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value)
                            {
                                entidad.FechaProximoPago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            }
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.ValorAPagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.ValorTotalAPagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CALIF_PROMEDIO"] != DBNull.Value) entidad.CalifPromedio = Convert.ToInt64(resultado["CALIF_PROMEDIO"]);
                            if (resultado["pagare"] != DBNull.Value) entidad.Pagare = resultado["pagare"].ToString();
                            if (resultado["cuota"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["cuota"]);
                            if (resultado["atributos"] != DBNull.Value) entidad.Atributos = Convert.ToInt64(resultado["atributos"]);
                            if (resultado["cod_linea_credito"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["cod_linea_credito"].ToString());
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.CuotasPendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"].ToString());
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.TipoLiquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"].ToString());
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.PeriodoGracia = Convert.ToInt64(resultado["PERIODO_GRACIA"].ToString());
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.CodClasifica = Convert.ToInt64(resultado["COD_CLASIFICA"].ToString());
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.Ejecutivo.IdEjecutivo = Convert.ToInt64(resultado["COD_ASESOR_COM"].ToString());
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.TipoCredito = resultado["TIPO_CREDITO"].ToString();
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.NumRadicOrigen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"].ToString());
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.Empresa.IdEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"].ToString());
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.CodPagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"].ToString());
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.Gradiente = Convert.ToDecimal(resultado["GRADIENTE"].ToString());
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.NumRadicion = Convert.ToInt64(resultado["NUMERO_RADICACION"].ToString());
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.FechaDesembolso = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosTodos", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProductoResumen> ListarProductosResumen(string IdPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoResumen> lstProductos = new List<ProductoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT VAsesoresClienteProductos.*,CONSULTAR_PAGADURIAS(VAsesoresClienteProductos.numero_radicacion) AS PAGADURIAS   from VAsesoresClienteProductos WHERE cod_deudor = " + IdPersona + " AND estado Not In ('TERMINADO', 'ANULADO', 'PAGADO') ORDER BY fecha_proximo_pago";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductoResumen entidad = new ProductoResumen();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valorapagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["TASAINTCTE"] != DBNull.Value) entidad.Tasainteres = Convert.ToDecimal(resultado["TASAINTCTE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["PAGADURIAS"] != DBNull.Value) entidad.pagadurias = Convert.ToString(resultado["PAGADURIAS"]);


                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosResumen", ex);
                        return null;
                    }
                }
            }
        }


        public List<ProductoResumen> ListarCreditosClubAhorrador(string IdPersona, Boolean pResult, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoResumen> lstProductos = new List<ProductoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = string.Empty;
                        if (pResult)
                        {
                            sql += @"SELECT V.*, 'PROPIO' as TIPO_REGISTRO, p.nombre as NOM_PERSONA,CONSULTAR_PAGADURIAS(V.numero_radicacion) AS PAGADURIAS,
                                     d.descripcion as DESTINACION
                                     FROM VASESORESCLIENTEPRODUCTOS V
                                     inner join v_persona p on p.cod_persona = v.cod_deudor
                                     inner join lineascredito l on L.Cod_Linea_Credito = V.Cod_Linea_Credito
                                     inner join credito c on C.Numero_Radicacion = V.Numero_Radicacion
                                     left join Destinacion d on D.Cod_Destino = C.Destinacion
                                WHERE V.COD_DEUDOR = " + IdPersona + " AND V.ESTADO NOT IN ('TERMINADO', 'ANULADO', 'PAGADO','NEGADO') order by V.NUMERO_RADICACION DESC, V.FECHA_PROXIMO_PAGO, V.FECHA_DESEMBOLSO desc";
                        }
                        else
                        {
                            sql += @"SELECT V.*, 'PROPIO' as TIPO_REGISTRO, p.nombre as NOM_PERSONA,CONSULTAR_PAGADURIAS(V.numero_radicacion) AS PAGADURIAS FROM VASESORESCLIENTEPRODUCTOS V inner join v_persona p on p.cod_persona = v.cod_deudor
                                     WHERE V.COD_DEUDOR = " + IdPersona + " AND V.ESTADO NOT IN ('TERMINADO', 'ANULADO', 'PAGADO') ";
                            sql += @"UNION ALL 
                                     SELECT V.*,'CLUB AHORRADOR' as TIPO_REGISTRO, p.nombre as NOM_PERSONA,CONSULTAR_PAGADURIAS(V.numero_radicacion) AS PAGADURIAS FROM VASESORESCLIENTEPRODUCTOS V inner join v_persona p on p.cod_persona = v.cod_deudor 
                                     WHERE V.ESTADO NOT IN ('TERMINADO', 'ANULADO', 'PAGADO') 
                                     AND V.COD_DEUDOR IN (SELECT R.COD_PERSONA FROM PERSONA_RESPONSABLE R WHERE R.COD_PERSONA_TUTOR = " + IdPersona + ")order by 15, 32 DESC";
                        }

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        ProductoResumen entidad;
                        while (resultado.Read())
                        {
                            entidad = new ProductoResumen();
                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_CREDITO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.monto = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["FECHA_DESEMBOLSO"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(resultado["FECHA_DESEMBOLSO"]);
                            if (resultado["CUOTA"] != DBNull.Value) entidad.valorcuota = Convert.ToInt64(resultado["CUOTA"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valorapagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["TIPO_REGISTRO"] != DBNull.Value) entidad.tipo_registro = Convert.ToString(resultado["TIPO_REGISTRO"]);
                            if (resultado["NOM_PERSONA"] != DBNull.Value) entidad.nombre = Convert.ToString(resultado["NOM_PERSONA"]);
                            if (resultado["TASAINTCTE"] != DBNull.Value) entidad.Tasainteres = Convert.ToDecimal(resultado["TASAINTCTE"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"]);
                            if (resultado["PAGADURIAS"] != DBNull.Value) entidad.pagadurias = Convert.ToString(resultado["PAGADURIAS"]);
                            if (resultado["FECHA_VENCIMIENTO"] != DBNull.Value) entidad.fecha_vencimiento = Convert.ToDateTime(resultado["FECHA_VENCIMIENTO"]);
                            if (resultado["NOM_FORMA_PAGO"] != DBNull.Value) entidad.nom_forma_pago = Convert.ToString(resultado["NOM_FORMA_PAGO"]);
                            if (resultado["COD_PERIODICIDAD"] != DBNull.Value) entidad.cod_periodicidad = Convert.ToString(resultado["COD_PERIODICIDAD"]);
                            if (resultado["NOM_PERIODICIDAD"] != DBNull.Value) entidad.nom_periodicidad = Convert.ToString(resultado["NOM_PERIODICIDAD"]);
                            if (resultado["TIPO_LINEA"] != DBNull.Value) entidad.TipoLinea = Convert.ToInt32(resultado["TIPO_LINEA"]);
                            if (resultado["DESTINACION"] != DBNull.Value) entidad.linea = entidad.linea + " - " + Convert.ToString(resultado["DESTINACION"]);
                            if (entidad.TipoLinea == 2)
                                entidad.CupoDisponible = entidad.monto - entidad.saldo;
                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosResumen", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProductoResumen> ListarProductosAporte(string IdPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoResumen> lstProductos = new List<ProductoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT aporte.*, lineaaporte.nombre, Calcular_VrAPagarAporte(aporte.numero_aporte, SYSDATE) As valor_a_pagar, 0 As valor_total_a_pagar, oficina.nombre As oficina, CALCULAR_CUOTA_APORTE(CUOTA) as CUOTA_CALCULADA 
                                        FROM aporte 
                                        INNER JOIN lineaaporte ON aporte.cod_linea_aporte = lineaaporte.cod_linea_aporte 
                                        INNER JOIN persona ON aporte.cod_persona = persona.cod_persona
                                        LEFT JOIN oficina ON aporte.cod_oficina = oficina.cod_oficina
                                        WHERE persona.identificacion = '" + IdPersona + "' AND aporte.estado = 1 ORDER BY aporte.cod_linea_aporte";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductoResumen entidad = new ProductoResumen();

                            if (resultado["NUMERO_APORTE"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_APORTE"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fechaapertura = Convert.ToDateTime(resultado["FECHA_APERTURA"]);
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO"]);
                            if (resultado["CUOTA_CALCULADA"] != DBNull.Value) entidad.valorcuota = Convert.ToInt64(resultado["CUOTA_CALCULADA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valorapagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["COD_OFICINA"] != DBNull.Value) entidad.oficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["COD_LINEA_APORTE"] != DBNull.Value) entidad.cod_linea = Convert.ToString(resultado["COD_LINEA_APORTE"].ToString());
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.cod_empresa = Convert.ToInt64(resultado["COD_EMPRESA"].ToString());

                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductos", ex);
                        return null;
                    }
                }
            }
        }

        public List<Producto> ListarProductoscredito(Int64 credito, decimal saldo, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<Producto> lstProductos = new List<Producto>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT * FROM  VAsesoresClienteProductos 
                                        WHERE numero_radicacion = " + credito + " AND saldo_capital >= " + saldo + " ORDER BY estado";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            Producto entidad = new Producto();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.CodRadicacion = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.Persona.IdPersona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["ESTADO"] != DBNull.Value) entidad.Estado = Convert.ToString(resultado["ESTADO"]);
                            if (resultado["PROCESO"] != DBNull.Value)
                                entidad.Proceso = Convert.ToString(resultado["PROCESO"]);
                            else
                                entidad.Proceso = "                    ";
                            if (resultado["LINEA"] != DBNull.Value) entidad.Linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_SOLICITUD"] != DBNull.Value) entidad.FechaSolicitud = Convert.ToDateTime(resultado["FECHA_SOLICITUD"]);
                            if (resultado["MONTO_APROBADO"] != DBNull.Value) entidad.MontoAprobado = Convert.ToInt64(resultado["MONTO_APROBADO"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.SaldoCapital = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["cuota"] != DBNull.Value) entidad.ValorCuota = Convert.ToInt64(resultado["cuota"]);
                            if (resultado["OTROS_SALDOS"] != DBNull.Value) entidad.OtrosSaldos = Convert.ToInt64(resultado["OTROS_SALDOS"]);
                            if (resultado["PLAZO"] != DBNull.Value) entidad.Plazo = Convert.ToInt64(resultado["PLAZO"]);
                            if (resultado["CUOTAS_PAGADAS"] != DBNull.Value) entidad.CuotasPagadas = Convert.ToInt64(resultado["CUOTAS_PAGADAS"]);
                            if (resultado["CODEUDORES"] != DBNull.Value) entidad.Codeudor = Convert.ToInt64(resultado["CODEUDORES"]);
                            if (resultado["GARANTIA"] != DBNull.Value) entidad.Garantia = Convert.ToInt64(resultado["GARANTIA"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.FechaProximoPago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.ValorAPagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.ValorTotalAPagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);
                            if (resultado["OFICINA"] != DBNull.Value) entidad.Oficina.NombreOficina = Convert.ToString(resultado["OFICINA"]);
                            if (resultado["CALIF_PROMEDIO"] != DBNull.Value) entidad.CalifPromedio = Convert.ToInt64(resultado["CALIF_PROMEDIO"]);
                            if (resultado["PAGARE"] != DBNull.Value) entidad.Pagare = resultado["PAGARE"].ToString();
                            if (resultado["CUOTA"] != DBNull.Value) entidad.Cuota = Convert.ToInt64(resultado["CUOTA"]); //*se cambia el campo couta por que este campo debe ser de ciudad
                            if (resultado["ATRIBUTOS"] != DBNull.Value) entidad.Atributos = Convert.ToInt64(resultado["ATRIBUTOS"]);
                            if (resultado["COD_LINEA_CREDITO"] != DBNull.Value) entidad.CodLineaCredito = Convert.ToString(resultado["COD_LINEA_CREDITO"].ToString());
                            if (resultado["CUOTAS_PENDIENTES"] != DBNull.Value) entidad.CuotasPendientes = Convert.ToDecimal(resultado["CUOTAS_PENDIENTES"].ToString());
                            if (resultado["TIPO_LIQUIDACION"] != DBNull.Value) entidad.TipoLiquidacion = Convert.ToInt64(resultado["TIPO_LIQUIDACION"].ToString());
                            if (resultado["PERIODO_GRACIA"] != DBNull.Value) entidad.PeriodoGracia = Convert.ToInt64(resultado["PERIODO_GRACIA"].ToString());
                            if (resultado["COD_CLASIFICA"] != DBNull.Value) entidad.CodClasifica = Convert.ToInt64(resultado["COD_CLASIFICA"].ToString());
                            if (resultado["COD_ASESOR_COM"] != DBNull.Value) entidad.Ejecutivo.IdEjecutivo = Convert.ToInt64(resultado["COD_ASESOR_COM"].ToString());
                            if (resultado["TIPO_CREDITO"] != DBNull.Value) entidad.TipoCredito = resultado["TIPO_CREDITO"].ToString();
                            if (resultado["NUM_RADIC_ORIGEN"] != DBNull.Value) entidad.NumRadicOrigen = Convert.ToInt64(resultado["NUM_RADIC_ORIGEN"].ToString());
                            if (resultado["COD_EMPRESA"] != DBNull.Value) entidad.Empresa.IdEmpresa = Convert.ToInt64(resultado["COD_EMPRESA"].ToString());
                            if (resultado["COD_PAGADURIA"] != DBNull.Value) entidad.CodPagaduria = Convert.ToInt64(resultado["COD_PAGADURIA"].ToString());
                            if (resultado["GRADIENTE"] != DBNull.Value) entidad.Gradiente = Convert.ToDecimal(resultado["GRADIENTE"].ToString());

                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductos", ex);
                        return null;
                    }
                }
            }
        }

        public List<ProductoResumen> ListarProductosCreditoResumen(int credito, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoResumen> lstProductos = new List<ProductoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM  VAsesoresClienteProductos WHERE  numero_radicacion = " + credito + "and saldo_capital>0 order by ESTADO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductoResumen entidad = new ProductoResumen();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_producto = Convert.ToString(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["SALDO_CAPITAL"] != DBNull.Value) entidad.saldo = Convert.ToInt64(resultado["SALDO_CAPITAL"]);
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fechaproximopago = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]);
                            if (resultado["VALOR_A_PAGAR"] != DBNull.Value) entidad.valorapagar = Convert.ToInt64(resultado["VALOR_A_PAGAR"]);
                            if (resultado["VALOR_TOTAL_A_PAGAR"] != DBNull.Value) entidad.valortotalapagar = Convert.ToInt64(resultado["VALOR_TOTAL_A_PAGAR"]);

                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosCreditoResumen", ex);
                        return null;
                    }
                }
            }
        }

        public List<MovimientoResumen> ListarMovimientoResumen(int credito, int numero, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoResumen> lstProductos = new List<MovimientoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select t.numero_radicacion, c.cod_deudor, l.nombre As linea, o.fecha_oper, o.num_comp, tc.descripcion AS tipo_comp, Sum(t.valor) As valor
                                        From credito c Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito
                                        Inner Join tran_cred t On c.numero_radicacion = t.numero_radicacion 
                                        Inner Join operacion o On t.cod_ope = o.cod_ope Left Join tipo_comp tc On o.tipo_comp = tc.tipo_comp
                                        Where t.numero_radicacion = " + credito.ToString() + " And t.tipo_tran != 1 " +
                                        "Group by t.numero_radicacion, c.cod_deudor, l.nombre, o.fecha_oper, o.num_comp, tc.descripcion Order By o.fecha_oper Desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int contador = 1;
                        while (resultado.Read() && contador <= numero)
                        {
                            MovimientoResumen entidad = new MovimientoResumen();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstProductos.Add(entidad);
                            contador += 1;
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosCreditoResumen", ex);
                        return null;
                    }
                }
            }
        }

        public List<MovimientoResumen> ListarMovimientoPersonaResumen(string pIdentificacion, int pNumero, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<MovimientoResumen> lstProductos = new List<MovimientoResumen>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"Select t.numero_radicacion, c.cod_deudor, l.nombre As linea, o.fecha_oper, o.num_comp, tc.descripcion AS tipo_comp, Sum(t.valor) As valor
                                        From credito c Inner Join lineascredito l On c.cod_linea_credito = l.cod_linea_credito
                                        Inner Join v_persona p On c.cod_deudor = p.cod_persona
                                        Inner Join tran_cred t On c.numero_radicacion = t.numero_radicacion 
                                        Inner Join operacion o On t.cod_ope = o.cod_ope Left Join tipo_comp tc On o.tipo_comp = tc.tipo_comp
                                        Where p.identificacion = '" + pIdentificacion.Trim() + "' And t.tipo_tran != 1 " +
                                        "Group by t.numero_radicacion, c.cod_deudor, l.nombre, o.fecha_oper, o.num_comp, tc.descripcion Order By o.fecha_oper Desc";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        int contador = 1;
                        while (resultado.Read() && contador <= pNumero)
                        {
                            MovimientoResumen entidad = new MovimientoResumen();

                            if (resultado["NUMERO_RADICACION"] != DBNull.Value) entidad.numero_radicacion = Convert.ToInt64(resultado["NUMERO_RADICACION"]);
                            if (resultado["COD_DEUDOR"] != DBNull.Value) entidad.cod_deudor = Convert.ToInt64(resultado["COD_DEUDOR"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]);
                            if (resultado["FECHA_OPER"] != DBNull.Value) entidad.fecha_oper = Convert.ToDateTime(resultado["FECHA_OPER"]);
                            if (resultado["NUM_COMP"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["NUM_COMP"]);
                            if (resultado["TIPO_COMP"] != DBNull.Value) entidad.tipo_comp = Convert.ToString(resultado["TIPO_COMP"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToInt64(resultado["VALOR"]);

                            lstProductos.Add(entidad);
                            contador += 1;
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosCreditoResumen", ex);
                        return null;
                    }
                }
            }
        }

        public Producto ConsultarCreditosTerminados(Int64 pIdEntityProducto, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            Producto EntityProducto = new Producto();
            Cliente aseEntiCliente = new Cliente();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT  COUNT(*) as cantidad FROM CREDITO WHERE SALDO_CAPITAL=0 AND ESTADO='T' AND  COD_DEUDOR =" + pIdEntityProducto;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["cantidad"] != DBNull.Value) EntityProducto.numerocreditos = Convert.ToInt64(resultado["cantidad"].ToString());

                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return EntityProducto;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductoData", "ConsultarCreditosTerminados", ex);
                        return null;
                    }
                }
            }

        }

        public List<ProductoPersonaAPP> ListarProductosXPersonaAPP(Int64 codPersona, Usuario pUsuario)
        {
            DbDataReader resultado = default(DbDataReader);
            List<ProductoPersonaAPP> lstProductos = new List<ProductoPersonaAPP>();

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(pUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = "SELECT * FROM V_PRODUCTOS_CLIENTE WHERE COD_PERSONA = " + codPersona.ToString() + " ORDER BY TIPO_PRODUCTO,NUMERO_PRODUCTO";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            ProductoPersonaAPP entidad = new ProductoPersonaAPP();
                            if (resultado["COD_TIPO_PRODUCTO"] != DBNull.Value) entidad.cod_tipo_producto = Convert.ToInt32(resultado["COD_TIPO_PRODUCTO"]);
                            if (resultado["TIPO_PRODUCTO"] != DBNull.Value) entidad.tipo_producto = Convert.ToString(resultado["TIPO_PRODUCTO"]);
                            if (resultado["COD_PERSONA"] != DBNull.Value) entidad.cod_persona = Convert.ToInt64(resultado["COD_PERSONA"]);
                            if (resultado["NUMERO_PRODUCTO"] != DBNull.Value) entidad.numero_producto = Convert.ToInt64(resultado["NUMERO_PRODUCTO"]);
                            if (resultado["LINEA"] != DBNull.Value) entidad.linea = Convert.ToString(resultado["LINEA"]); else entidad.linea = "";
                            if (resultado["NOMBRE_LINEA"] != DBNull.Value) entidad.nombre_linea = Convert.ToString(resultado["NOMBRE_LINEA"]); else entidad.nombre_linea = "";
                            if (resultado["FECHA_APERTURA"] != DBNull.Value) entidad.fecha_aperturaAPP = Convert.ToDateTime(resultado["FECHA_APERTURA"]).ToShortDateString(); else entidad.fecha_aperturaAPP = "";
                            if (resultado["CUOTA"] != DBNull.Value) entidad.cuota = Convert.ToInt32(resultado["CUOTA"]).ToString("n0"); else entidad.cuota = "0";
                            if (resultado["SALDO"] != DBNull.Value) entidad.saldo = Convert.ToDecimal(resultado["SALDO"]).ToString("n0"); else entidad.saldo = "0";
                            if (resultado["FECHA_PROXIMO_PAGO"] != DBNull.Value) entidad.fecha_proximo_pagoAPP = Convert.ToDateTime(resultado["FECHA_PROXIMO_PAGO"]).ToShortDateString(); else entidad.fecha_proximo_pagoAPP = "";
                            if (resultado["TOTAL_A_PAGAR"] != DBNull.Value) entidad.total_a_pagar = Convert.ToInt32(resultado["TOTAL_A_PAGAR"]).ToString("n0"); else entidad.total_a_pagar = "0";
                            lstProductos.Add(entidad);
                        }

                        return lstProductos;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("ProductosData", "ListarProductosXPersonaAPP", ex);
                        return null;
                    }
                }
            }
        }

    }
}