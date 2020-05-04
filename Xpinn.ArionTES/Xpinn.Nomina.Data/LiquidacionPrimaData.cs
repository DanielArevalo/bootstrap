using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Xpinn.Util;
using Xpinn.Nomina.Entities;

namespace Xpinn.Nomina.Data
{
    public class LiquidacionPrimaData : GlobalData
    {

        protected ConnectionDataBase dbConnectionFactory;

        public LiquidacionPrimaData()
        {
            dbConnectionFactory = new ConnectionDataBase();
        }

        public Tuple<List<LiquidacionPrimaDetalle>, List<LiquidacionPrimaDetEmpleado>, List<NovedadPrima>> GenerarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                using (DbCommand cmdTransaccionFactorySecundaria = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter p_codigoNomina = cmdTransaccionFactory.CreateParameter();
                        p_codigoNomina.ParameterName = "p_codigoNomina";
                        p_codigoNomina.Value = pLiquidacionPrima.codigonomina;
                        p_codigoNomina.Direction = ParameterDirection.Input;
                        p_codigoNomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_codigoNomina);

                        DbParameter p_anio = cmdTransaccionFactory.CreateParameter();
                        p_anio.ParameterName = "p_anio";
                        p_anio.Value = pLiquidacionPrima.anio;
                        p_anio.Direction = ParameterDirection.Input;
                        p_anio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(p_anio);

                        DbParameter p_semestre = cmdTransaccionFactory.CreateParameter();
                        p_semestre.ParameterName = "p_semestre";
                        p_semestre.Value = pLiquidacionPrima.semestre;
                        p_semestre.Direction = ParameterDirection.Input;
                        p_semestre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(p_semestre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_GENERARLIQPRIMA";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        List<LiquidacionPrimaDetalle> liquidacionPrimaDetalle = new List<LiquidacionPrimaDetalle>();
                        List<LiquidacionPrimaDetEmpleado> liquidacionPrimaDetalleEmpleados = new List<LiquidacionPrimaDetEmpleado>();
                        List<NovedadPrima> novedadesLiquidadas = new List<NovedadPrima>();

                        string sql = @"select temp.*, ING.CODIGOCARGO, car.DESCRIPCION as desc_cargo
                                        from TEMP_PRIMAEMP temp
                                        JOIN INGRESOPERSONAL ING on ING.CODIGOEMPLEADO = temp.CONSECUTIVOEMPLEADO and ING.CODIGONOMINA = " + pLiquidacionPrima.codigonomina +
                                        " JOIN CARGO_nomina car on car.idcargo = ing.codigocargo WHERE ING.ESTAACTIVOCONTRATO = 1  order by ING.CODIGOEMPLEADO  asc";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        DbDataReader resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetalle entidad = new LiquidacionPrimaDetalle();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["IDENTIFICACION"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["IDENTIFICACION"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["NOMBRE"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DIASLIQUIDAR"] != DBNull.Value) entidad.diasliquidar = Convert.ToInt64(resultado["DIASLIQUIDAR"]);
                            if (resultado["SALARIO"] != DBNull.Value) entidad.salario = Convert.ToInt64(resultado["SALARIO"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);

                            liquidacionPrimaDetalle.Add(entidad);
                        }

                        sql = @"select * from TEMP_PRIMAEMPDET";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetEmpleado entidad = new LiquidacionPrimaDetEmpleado();

                            if (resultado["CONSECUTIVOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CONSECUTIVOEMPLEADO"]);
                            if (resultado["DESCRIPCIONNOVEDAD"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCIONNOVEDAD"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["TIPOCALCULONOVEDAD"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPOCALCULONOVEDAD"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);

                            liquidacionPrimaDetalleEmpleados.Add(entidad);
                        }

                        sql = @"select * from TEMP_PRIMANOVLIQUIDADAS";

                        cmdTransaccionFactorySecundaria.Connection = connection;
                        cmdTransaccionFactorySecundaria.CommandType = CommandType.Text;
                        cmdTransaccionFactorySecundaria.CommandText = sql;
                        resultado = cmdTransaccionFactorySecundaria.ExecuteReader();

                        while (resultado.Read())
                        {
                            NovedadPrima entidad = new NovedadPrima();

                            if (resultado["CONSECUTIVONOVEDAD"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVONOVEDAD"]);

                            novedadesLiquidadas.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);

                        return Tuple.Create(liquidacionPrimaDetalle, liquidacionPrimaDetalleEmpleados, novedadesLiquidadas);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "GenerarLiquidacionPrima", ex);
                        return null;
                    }
                }
            }
        }

        public int? ConsultarTipoCalculoNovedadDeUnTipoNovedad(long codigoTipoNovedad, Usuario usuario)
        {
            DbDataReader resultado;
            int? tipoCalculo = null;
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT tipo FROM concepto_nomina WHERE CONSECUTIVO = " + codigoTipoNovedad;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["tipo"] != DBNull.Value) tipoCalculo = Convert.ToInt32(resultado["tipo"]);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return tipoCalculo;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ConsultarTipoCalculoNovedadDeUnTipoNovedad", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionPrima CrearLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = vUsuario.codusuario;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = pLiquidacionPrima.fechageneracion = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionPrima.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionPrima.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionPrima.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        pfechapago.Value = pLiquidacionPrima.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pLiquidacionPrima.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        psemestre.Value = pLiquidacionPrima.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIPRIMA_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionPrima.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "CrearLiquidacionPrima", ex);
                        return null;
                    }
                }
            }
        }

        public void AplicarNovedadPagada(NovedadPrima novedad, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = novedad.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_APLICARNOVEDPRIM";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "AplicarNovedadPagada", ex);
                    }
                }
            }
        }


        public LiquidacionPrimaDetalle CrearLiquidacionPrimaDetalle(LiquidacionPrimaDetalle pLiquidacionPrimaDetalle, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionPrimaDetalle.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacionprima = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacionprima.ParameterName = "p_codigoliquidacionprima";
                        pcodigoliquidacionprima.Value = pLiquidacionPrimaDetalle.codigoliquidacionprima;
                        pcodigoliquidacionprima.Direction = ParameterDirection.Input;
                        pcodigoliquidacionprima.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacionprima);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionPrimaDetalle.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionPrimaDetalle.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter pdiasliquidar = cmdTransaccionFactory.CreateParameter();
                        pdiasliquidar.ParameterName = "p_diasliquidar";
                        pdiasliquidar.Value = pLiquidacionPrimaDetalle.diasliquidar;
                        pdiasliquidar.Direction = ParameterDirection.Input;
                        pdiasliquidar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pdiasliquidar);

                        DbParameter pfechainicio = cmdTransaccionFactory.CreateParameter();
                        pfechainicio.ParameterName = "p_fechainicio";
                        pfechainicio.Value = pLiquidacionPrimaDetalle.fechainicio;
                        pfechainicio.Direction = ParameterDirection.Input;
                        pfechainicio.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechainicio);

                        DbParameter pfechaterminacion = cmdTransaccionFactory.CreateParameter();
                        pfechaterminacion.ParameterName = "p_fechaterminacion";
                        pfechaterminacion.Value = pLiquidacionPrimaDetalle.fechaterminacion;
                        pfechaterminacion.Direction = ParameterDirection.Input;
                        pfechaterminacion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechaterminacion);

                        DbParameter p_salario = cmdTransaccionFactory.CreateParameter();
                        p_salario.ParameterName = "p_salario";
                        p_salario.Value = pLiquidacionPrimaDetalle.salario;
                        p_salario.Direction = ParameterDirection.Input;
                        p_salario.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(p_salario);

                        DbParameter PCOD_OPE = cmdTransaccionFactory.CreateParameter();
                        PCOD_OPE.ParameterName = "P_COD_OPE";
                        PCOD_OPE.Value = pLiquidacionPrimaDetalle.cod_ope;
                        PCOD_OPE.Direction = ParameterDirection.Input;
                        PCOD_OPE.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(PCOD_OPE);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQPRIMDET_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionPrimaDetalle.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionPrimaDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "CrearLiquidacionPrimaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionPrimaDetEmpleado CrearLiquidacionPrimaDetEmpleado(LiquidacionPrimaDetEmpleado pLiquidacionPrimaDetEmpleado, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionPrimaDetEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigoliquidacionnprimadet = cmdTransaccionFactory.CreateParameter();
                        pcodigoliquidacionnprimadet.ParameterName = "p_codigoliquidacionnprimadet";
                        pcodigoliquidacionnprimadet.Value = pLiquidacionPrimaDetEmpleado.codigoliquidacionprimadetalle;
                        pcodigoliquidacionnprimadet.Direction = ParameterDirection.Input;
                        pcodigoliquidacionnprimadet.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoliquidacionnprimadet);

                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionPrimaDetEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);

                        DbParameter pcodigotiponovedad = cmdTransaccionFactory.CreateParameter();
                        pcodigotiponovedad.ParameterName = "p_codigotiponovedad";
                        pcodigotiponovedad.Value = pLiquidacionPrimaDetEmpleado.codigotiponovedad;
                        pcodigotiponovedad.Direction = ParameterDirection.Input;
                        pcodigotiponovedad.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigotiponovedad);

                        DbParameter pesnovedadcreadamanual = cmdTransaccionFactory.CreateParameter();
                        pesnovedadcreadamanual.ParameterName = "p_esnovedadcreadamanual";
                        pesnovedadcreadamanual.Value = pLiquidacionPrimaDetEmpleado.esnovedadcreadamanual;
                        pesnovedadcreadamanual.Direction = ParameterDirection.Input;
                        pesnovedadcreadamanual.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pesnovedadcreadamanual);

                        DbParameter pvalor = cmdTransaccionFactory.CreateParameter();
                        pvalor.ParameterName = "p_valor";
                        pvalor.Value = pLiquidacionPrimaDetEmpleado.valor;
                        pvalor.Direction = ParameterDirection.Input;
                        pvalor.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalor);

                


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_PRIMDETEMP_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionPrimaDetEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionPrimaDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "CrearLiquidacionPrimaDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public LiquidacionPrima ModificarLiquidacionPrima(LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        DbParameter pcodigousuariocreacion = cmdTransaccionFactory.CreateParameter();
                        pcodigousuariocreacion.ParameterName = "p_codigousuariocreacion";
                        pcodigousuariocreacion.Value = pLiquidacionPrima.codigousuariocreacion;
                        pcodigousuariocreacion.Direction = ParameterDirection.Input;
                        pcodigousuariocreacion.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigousuariocreacion);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = pLiquidacionPrima.fechageneracion;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);

                        DbParameter pcodigonomina = cmdTransaccionFactory.CreateParameter();
                        pcodigonomina.ParameterName = "p_codigonomina";
                        pcodigonomina.Value = pLiquidacionPrima.codigonomina;
                        pcodigonomina.Direction = ParameterDirection.Input;
                        pcodigonomina.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigonomina);

                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionPrima.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);

                        DbParameter pvalortotalpagar = cmdTransaccionFactory.CreateParameter();
                        pvalortotalpagar.ParameterName = "p_valortotalpagar";
                        pvalortotalpagar.Value = pLiquidacionPrima.valortotalpagar;
                        pvalortotalpagar.Direction = ParameterDirection.Input;
                        pvalortotalpagar.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalortotalpagar);

                        DbParameter pfechapago = cmdTransaccionFactory.CreateParameter();
                        pfechapago.ParameterName = "p_fechapago";
                        pfechapago.Value = pLiquidacionPrima.fechapago;
                        pfechapago.Direction = ParameterDirection.Input;
                        pfechapago.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechapago);

                        DbParameter panio = cmdTransaccionFactory.CreateParameter();
                        panio.ParameterName = "p_anio";
                        panio.Value = pLiquidacionPrima.anio;
                        panio.Direction = ParameterDirection.Input;
                        panio.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(panio);

                        DbParameter psemestre = cmdTransaccionFactory.CreateParameter();
                        psemestre.ParameterName = "p_semestre";
                        psemestre.Value = pLiquidacionPrima.semestre;
                        psemestre.Direction = ParameterDirection.Input;
                        psemestre.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(psemestre);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIPRIMA_MOD";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                        return pLiquidacionPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ModificarLiquidacionPrima", ex);
                        return null;
                    }
                }
            }
        }


        public void EliminarLiquidacionPrima(Int64 pId, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        LiquidacionPrima pLiquidacionPrima = new LiquidacionPrima();
                        pLiquidacionPrima = ConsultarLiquidacionPrima(pId, vUsuario);

                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionPrima.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Input;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQUIPRIMA_ELIMI";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "EliminarLiquidacionPrima", ex);
                    }
                }
            }
        }


        public LiquidacionPrima ConsultarLiquidacionPrima(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionPrima entidad = new LiquidacionPrima();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT l.*,u.nombre FROM LiquidacionPrima l left join usuarios u on u.CODUSUARIO= l.CODIGOUSUARIOCREACION  WHERE l.CONSECUTIVO = " + pId.ToString();
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAGENERACION"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHAGENERACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["SEMESTRE"]);
                            if (resultado["NOMBRE"] != DBNull.Value) entidad.desc_usuario = Convert.ToString(resultado["NOMBRE"]);
                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ConsultarLiquidacionPrima", ex);
                        return null;
                    }
                }
            }
        }
        public LiquidacionPrima ConsultarUltpago(Int64 pId, Usuario vUsuario)
        {
            DbDataReader resultado;
            LiquidacionPrima entidad = new LiquidacionPrima();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"select max(ANIO) AS YEAR,semestre,fechapago,CASE semestre WHEN 1 THEN 'Primer Semestre' WHEN 2 THEN 'Segundo Semestre' END AS DESCSEMESTRE from LiquidacionPrima where codigonomina= " + pId.ToString() + " GROUP BY ANIO,semestre,fechapago order by ANIO desc,semestre desc ";
                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        if (resultado.Read())
                        {
                            if (resultado["YEAR"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["YEAR"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["SEMESTRE"]);
                            if (resultado["DESCSEMESTRE"] != DBNull.Value) entidad.desc_semestre = Convert.ToString(resultado["DESCSEMESTRE"]);
                            if (resultado["fechapago"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["fechapago"]);


                        }
                        else
                        {
                            throw new ExceptionBusiness("El registro no existe. Verifique por favor.");
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return entidad;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ConsultarUltpago", ex);
                        return null;
                    }
                }
            }
        }

        public bool VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo(LiquidacionPrima liquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            bool existe = false;

            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT CONSECUTIVO 
                                        FROM LIQUIDACIONPRIMA
                                        WHERE CODIGONOMINA = " + liquidacion.codigonomina +
                                        " AND SEMESTRE = " + liquidacion.semestre + 
                                        " AND ANIO = " + liquidacion.anio;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        if (resultado.Read())
                        {
                            if (resultado["CONSECUTIVO"] != DBNull.Value)
                                existe = true;
                        }
                        
                        dbConnectionFactory.CerrarConexion(connection);
                        return existe;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "VerificarQueNoExistaUnaLiquidacionPreviaParaEstePeriodo", ex);
                        return false;
                    }
                }
            }
        }

        public List<LiquidacionPrima> ListarLiquidacionPrima(string filtro, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionPrima> lstLiquidacionPrima = new List<LiquidacionPrima>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, nom.DESCRIPCION as desc_nomina, usu.NOMBRE as desc_usuario, cst.DESCRIPCION as desc_centro_costo,
                                        CASE liq.Semestre WHEN 1 THEN 'Primer Semestre' WHEN 2 THEN 'Segundo Semestre' END as desc_semestre,O.cod_ope,o.num_comp,o.tipO_comp
                                        FROM LIQUIDACIONPRIMA liq
                                        JOIN NOMINA_EMPLEADO nom on nom.consecutivo = liq.CodigoNomina
                                        JOIN USUARIOS usu on usu.CODUSUARIO = liq.CODIGOUSUARIOCREACION
                                        JOIN CENTRO_COSTO cst on cst.CENTRO_COSTO = liq.CODIGOCENTROCOSTO
                                        LEFT JOIN (SELECT cod_ope,codigoliquidacionprima  FROM  LIQUIDACIONPRIMADETALLE GROUP BY cod_ope,codigoliquidacionprima) A ON A.codigoliquidacionprima = liq.consecutivo
                                        LEFT JOIN OPERACION o on o.cod_ope = a.cod_ope
                                        " + filtro + " ORDER BY liq.CONSECUTIVO desc ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();

                        while (resultado.Read())
                        {
                            LiquidacionPrima entidad = new LiquidacionPrima();

                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOUSUARIOCREACION"] != DBNull.Value) entidad.codigousuariocreacion = Convert.ToInt32(resultado["CODIGOUSUARIOCREACION"]);
                            if (resultado["FECHAGENERACION"] != DBNull.Value) entidad.fechageneracion = Convert.ToDateTime(resultado["FECHAGENERACION"]);
                            if (resultado["CODIGONOMINA"] != DBNull.Value) entidad.codigonomina = Convert.ToInt64(resultado["CODIGONOMINA"]);
                            if (resultado["CODIGOCENTROCOSTO"] != DBNull.Value) entidad.codigocentrocosto = Convert.ToInt64(resultado["CODIGOCENTROCOSTO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["FECHAPAGO"] != DBNull.Value) entidad.fechapago = Convert.ToDateTime(resultado["FECHAPAGO"]);
                            if (resultado["ANIO"] != DBNull.Value) entidad.anio = Convert.ToInt64(resultado["ANIO"]);
                            if (resultado["SEMESTRE"] != DBNull.Value) entidad.semestre = Convert.ToInt32(resultado["SEMESTRE"]);
                            if (resultado["desc_nomina"] != DBNull.Value) entidad.desc_nomina = Convert.ToString(resultado["desc_nomina"]);
                            if (resultado["desc_usuario"] != DBNull.Value) entidad.desc_usuario = Convert.ToString(resultado["desc_usuario"]);
                            if (resultado["desc_centro_costo"] != DBNull.Value) entidad.desc_centro_costo = Convert.ToString(resultado["desc_centro_costo"]);
                            if (resultado["desc_semestre"] != DBNull.Value) entidad.desc_semestre = Convert.ToString(resultado["desc_semestre"]);
                            if (resultado["COD_OPE"] != DBNull.Value) entidad.cod_ope = Convert.ToInt64(resultado["COD_OPE"]);
                            if (resultado["num_comp"] != DBNull.Value) entidad.num_comp = Convert.ToInt64(resultado["num_comp"]);
                            if (resultado["tipo_comp"] != DBNull.Value) entidad.tipo_comp = Convert.ToInt64(resultado["tipo_comp"]);

                            lstLiquidacionPrima.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrima;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ListarLiquidacionPrima", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionPrimaDetalle> ListarLiquidacionPrimaDetalle(long codigoLiquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionPrimaDetalle> lstLiquidacionPrimaDetalle = new List<LiquidacionPrimaDetalle>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liq.*, per.identificacion, per.nombre, ing.CODIGOCARGO, car.DESCRIPCION as desc_cargo,ing.fechaingreso
                                        FROM LiquidacionPrimaDetalle liq
                                        JOIN EMPLEADOS emp on liq.CODIGOEMPLEADO = emp.consecutivo
                                        JOIN V_PERSONA per on per.cod_persona = emp.COD_PERSONA
                                        join ingresopersonal ing on  liq.CONSECUTIVOCONTRATO =ing.CONSECUTIVO AND  emp.consecutivo = ing.CODIGOEMPLEADO and ing.CODIGONOMINA =  
                                        (
                                            SELECT CODIGONOMINA FROM LIQUIDACIONPRIMA WHERE CONSECUTIVO = " + codigoLiquidacion +
                                     @" )
                                        join CARGO_NOMINA car on car.IDCARGO = ing.codigocargo
                                        WHERE liq.CODIGOLIQUIDACIONPRIMA = " + codigoLiquidacion + " order by ING.CODIGOEMPLEADO asc  ";

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetalle entidad = new LiquidacionPrimaDetalle();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONPRIMA"] != DBNull.Value) entidad.codigoliquidacionprima = Convert.ToInt64(resultado["CODIGOLIQUIDACIONPRIMA"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["VALORTOTALPAGAR"] != DBNull.Value) entidad.valortotalpagar = Convert.ToDecimal(resultado["VALORTOTALPAGAR"]);
                            if (resultado["DIASLIQUIDAR"] != DBNull.Value) entidad.diasliquidar = Convert.ToInt64(resultado["DIASLIQUIDAR"]);
                            if (resultado["FECHAINICIO"] != DBNull.Value) entidad.fechainicio = Convert.ToDateTime(resultado["FECHAINICIO"]);
                            if (resultado["FECHATERMINACION"] != DBNull.Value) entidad.fechaterminacion = Convert.ToDateTime(resultado["FECHATERMINACION"]);
                            if (resultado["identificacion"] != DBNull.Value) entidad.identificacion_empleado = Convert.ToString(resultado["identificacion"]);
                            if (resultado["nombre"] != DBNull.Value) entidad.nombre_empleado = Convert.ToString(resultado["nombre"]);
                            if (resultado["salario"] != DBNull.Value) entidad.salario = Convert.ToDecimal(resultado["salario"]);
                            if (resultado["CODIGOCARGO"] != DBNull.Value) entidad.codigo_cargo = Convert.ToInt64(resultado["CODIGOCARGO"]);
                            if (resultado["desc_cargo"] != DBNull.Value) entidad.desc_cargo = Convert.ToString(resultado["desc_cargo"]);
                            if (resultado["fechaingreso"] != DBNull.Value) entidad.fechaingreso = Convert.ToDateTime(resultado["fechaingreso"]);

                            lstLiquidacionPrimaDetalle.Add(entidad);
                        }

                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetalle;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "ListarLiquidacionPrimaDetalle", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarLiquidacionPrimaDetEmpleado(long codigoLiquidacion, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionPrimaDetEmpleado> lstLiquidacionPrimaDetEmpleado = new List<LiquidacionPrimaDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT liqDetEmp.*, tip.DESCRIPCION,tip.TIPO
                                        FROM LiquidacionPrimaDetalle liqDet 
                                        JOIN LiquidacionPrimaDetEmpleado liqDetEmp ON liqDet.CONSECUTIVO = liqDetEmp.CODIGOLIQUIDACIONPRIMADETALLE
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = liqDetEmp.CODIGOTIPONOVEDAD
                                         JOIN INGRESOPERSONAL IP ON IP.CONSECUTIVO=liqDet.CONSECUTIVOCONTRATO        AND IP.CODIGOEMPLEADO=liqDet.CODIGOEMPLEADO
                                        WHERE liqDet.CODIGOLIQUIDACIONPRIMA = " + codigoLiquidacion;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetEmpleado entidad = new LiquidacionPrimaDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            if (resultado["CODIGOLIQUIDACIONPRIMADETALLE"] != DBNull.Value) entidad.codigoliquidacionprimadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONPRIMADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionPrimaDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaDetEmpleadoData", "ListarLiquidacionPrimaDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleado(long paño,long psemestre,long pempleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionPrimaDetEmpleado> lstLiquidacionPrimaDetEmpleado = new List<LiquidacionPrimaDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADPRIMA nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=0  and nov.codigoempleado =" + pempleado
                                        + " and nov.anio ="   + paño   + " and nov.semestre =" + psemestre ;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetEmpleado entidad = new LiquidacionPrimaDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                           // if (resultado["CODIGOLIQUIDACIONPRIMADETALLE"] != DBNull.Value) entidad.codigoliquidacionprimadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONPRIMADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                           // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionPrimaDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaDetEmpleadoData", "ListarNovedadesPrimaDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }


        public List<NovedadPrima> ListarNovedadesPrima(long paño, long psemestre, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<NovedadPrima> lstLiquidacionPrimaDetEmpleado = new List<NovedadPrima>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADPRIMA nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=0   and nov.anio =" + paño + " and nov.semestre =" + psemestre;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            NovedadPrima entidad = new NovedadPrima();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            // if (resultado["CODIGOLIQUIDACIONPRIMADETALLE"] != DBNull.Value) entidad.codigoliquidacionprimadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONPRIMADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionPrimaDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaDetEmpleadoData", "ListarNovedadesPrima", ex);
                        return null;
                    }
                }
            }
        }

        public List<LiquidacionPrimaDetEmpleado> ListarNovedadesPrimaDetEmpleadoAplicada(long paño, long psemestre, long pempleado, Usuario vUsuario)
        {
            DbDataReader resultado;
            List<LiquidacionPrimaDetEmpleado> lstLiquidacionPrimaDetEmpleado = new List<LiquidacionPrimaDetEmpleado>();
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        string sql = @"SELECT nov.*, tip.DESCRIPCION, tip.TIPO
                                        FROM NOVEDADPRIMA nov 
                                        JOIN CONCEPTO_NOMINA tip on tip.CONSECUTIVO = nov.CODIGOTIPONOVEDAD
                                        WHERE nov.NOVEDADFUEPAGADA=1  and nov.codigoempleado =" + pempleado
                                        + " and nov.anio =" + paño + " and nov.semestre =" + psemestre;

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.Text;
                        cmdTransaccionFactory.CommandText = sql;
                        resultado = cmdTransaccionFactory.ExecuteReader();
                        while (resultado.Read())
                        {
                            LiquidacionPrimaDetEmpleado entidad = new LiquidacionPrimaDetEmpleado();
                            if (resultado["CONSECUTIVO"] != DBNull.Value) entidad.consecutivo = Convert.ToInt64(resultado["CONSECUTIVO"]);
                            // if (resultado["CODIGOLIQUIDACIONPRIMADETALLE"] != DBNull.Value) entidad.codigoliquidacionprimadetalle = Convert.ToInt64(resultado["CODIGOLIQUIDACIONPRIMADETALLE"]);
                            if (resultado["CODIGOEMPLEADO"] != DBNull.Value) entidad.codigoempleado = Convert.ToInt64(resultado["CODIGOEMPLEADO"]);
                            if (resultado["CODIGOTIPONOVEDAD"] != DBNull.Value) entidad.codigotiponovedad = Convert.ToInt64(resultado["CODIGOTIPONOVEDAD"]);
                            // if (resultado["ESNOVEDADCREADAMANUAL"] != DBNull.Value) entidad.esnovedadcreadamanual = Convert.ToInt32(resultado["ESNOVEDADCREADAMANUAL"]);
                            if (resultado["VALOR"] != DBNull.Value) entidad.valor = Convert.ToDecimal(resultado["VALOR"]);
                            if (resultado["DESCRIPCION"] != DBNull.Value) entidad.descripcionNovedad = Convert.ToString(resultado["DESCRIPCION"]);
                            if (resultado["TIPO"] != DBNull.Value) entidad.tipoCalculoNovedad = Convert.ToInt32(resultado["TIPO"]);

                            lstLiquidacionPrimaDetEmpleado.Add(entidad);
                        }
                        dbConnectionFactory.CerrarConexion(connection);
                        return lstLiquidacionPrimaDetEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaDetEmpleadoData", "ListarNovedadesPrimaDetEmpleado", ex);
                        return null;
                    }
                }
            }
        }

        public void CrearGirosDeLiquidacionNomina(LiquidacionPrima liquidacion, Usuario usuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(usuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {

                        DbParameter porigen = cmdTransaccionFactory.CreateParameter();
                        porigen.ParameterName = "PORIGEN";
                        porigen.Value = liquidacion.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);

                        DbParameter pCod_ope = cmdTransaccionFactory.CreateParameter();
                        pCod_ope.ParameterName = "P_COD_OPE";
                        pCod_ope.Value = liquidacion.cod_ope;
                        pCod_ope.Direction = ParameterDirection.Input;
                        pCod_ope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCod_ope);


                        DbParameter pCodigoNominaGirar = cmdTransaccionFactory.CreateParameter();
                        pCodigoNominaGirar.ParameterName = "pCodigoNominaGirar";
                        pCodigoNominaGirar.Value = liquidacion.consecutivo;
                        pCodigoNominaGirar.Direction = ParameterDirection.Input;
                        pCodigoNominaGirar.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoNominaGirar);

                        DbParameter pCodigoUsuario = cmdTransaccionFactory.CreateParameter();
                        pCodigoUsuario.ParameterName = "pCodigoUsuario";
                        pCodigoUsuario.Value = usuario.codusuario;
                        pCodigoUsuario.Direction = ParameterDirection.Input;
                        pCodigoUsuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pCodigoUsuario);

                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_LIQGIROCREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();

                        dbConnectionFactory.CerrarConexion(connection);
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "CrearGirosDeLiquidacionNomina", ex);
                    }
                }
            }
        }


        public LiquidacionPrimaDetEmpleado CrearLiquidacionNominaInterfaz(LiquidacionPrimaDetEmpleado pLiquidacionNominaDetaEmpleado, LiquidacionPrima pLiquidacionPrima, Usuario vUsuario)
        {
            using (DbConnection connection = dbConnectionFactory.ObtenerConexion(vUsuario))
            {
                using (DbCommand cmdTransaccionFactory = dbConnectionFactory.dbProveedorFactory.CreateCommand())
                {
                    try
                    {
                        DbParameter pconsecutivo = cmdTransaccionFactory.CreateParameter();
                        pconsecutivo.ParameterName = "p_consecutivo";
                        pconsecutivo.Value = pLiquidacionNominaDetaEmpleado.consecutivo;
                        pconsecutivo.Direction = ParameterDirection.Output;
                        pconsecutivo.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pconsecutivo);



                        DbParameter pcodigoempleado = cmdTransaccionFactory.CreateParameter();
                        pcodigoempleado.ParameterName = "p_codigoempleado";
                        pcodigoempleado.Value = pLiquidacionNominaDetaEmpleado.codigoempleado;
                        pcodigoempleado.Direction = ParameterDirection.Input;
                        pcodigoempleado.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigoempleado);


                        DbParameter pcodigoconcepto = cmdTransaccionFactory.CreateParameter();
                        pcodigoconcepto.ParameterName = "p_codigoconcepto";
                        pcodigoconcepto.Value = pLiquidacionNominaDetaEmpleado.codigotiponovedad;
                        pcodigoconcepto.Direction = ParameterDirection.Input;
                        pcodigoconcepto.DbType = DbType.Int32;
                        cmdTransaccionFactory.Parameters.Add(pcodigoconcepto);

                        DbParameter pvalorconcepto = cmdTransaccionFactory.CreateParameter();
                        pvalorconcepto.ParameterName = "p_valorconcepto";
                        pvalorconcepto.Value = pLiquidacionNominaDetaEmpleado.valor;
                        pvalorconcepto.Direction = ParameterDirection.Input;
                        pvalorconcepto.DbType = DbType.Decimal;
                        cmdTransaccionFactory.Parameters.Add(pvalorconcepto);


                        DbParameter pcodope = cmdTransaccionFactory.CreateParameter();
                        pcodope.ParameterName = "p_cod_ope";
                        if (pLiquidacionNominaDetaEmpleado.cod_ope == 0)
                        {
                            pcodope.Value = pLiquidacionPrima.cod_ope;
                        }
                        else
                        {
                            pcodope.Value = pLiquidacionNominaDetaEmpleado.cod_ope;
                        }
                        pcodope.Direction = ParameterDirection.Input;
                        pcodope.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodope);

                        DbParameter pfechageneracion = cmdTransaccionFactory.CreateParameter();
                        pfechageneracion.ParameterName = "p_fechageneracion";
                        pfechageneracion.Value = DateTime.Now;
                        pfechageneracion.Direction = ParameterDirection.Input;
                        pfechageneracion.DbType = DbType.DateTime;
                        cmdTransaccionFactory.Parameters.Add(pfechageneracion);


                        DbParameter pcodigocentrocosto = cmdTransaccionFactory.CreateParameter();
                        pcodigocentrocosto.ParameterName = "p_codigocentrocosto";
                        pcodigocentrocosto.Value = pLiquidacionPrima.codigocentrocosto;
                        pcodigocentrocosto.Direction = ParameterDirection.Input;
                        pcodigocentrocosto.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pcodigocentrocosto);



                        DbParameter pusuario = cmdTransaccionFactory.CreateParameter();
                        pusuario.ParameterName = "p_usuario";
                        pusuario.Value = vUsuario.codusuario;
                        pusuario.Direction = ParameterDirection.Input;
                        pusuario.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(pusuario);


                        DbParameter porigen = cmdTransaccionFactory.CreateParameter();
                        porigen.ParameterName = "p_origen";
                        porigen.Value = pLiquidacionPrima.codorigen;
                        porigen.Direction = ParameterDirection.Input;
                        porigen.DbType = DbType.Int64;
                        cmdTransaccionFactory.Parameters.Add(porigen);


                        connection.Open();
                        cmdTransaccionFactory.Connection = connection;
                        cmdTransaccionFactory.CommandType = CommandType.StoredProcedure;
                        cmdTransaccionFactory.CommandText = "USP_XPINN_NOM_INTERFAZ_CREAR";
                        cmdTransaccionFactory.ExecuteNonQuery();
                        dbConnectionFactory.CerrarConexion(connection);

                        pLiquidacionNominaDetaEmpleado.consecutivo = pconsecutivo.Value != DBNull.Value ? Convert.ToInt64(pconsecutivo.Value) : 0;

                        return pLiquidacionNominaDetaEmpleado;
                    }
                    catch (Exception ex)
                    {
                        BOExcepcion.Throw("LiquidacionPrimaData", "CrearLiquidacionNominaInterfaz", ex);
                        return null;
                    }
                }
            }
        }


    }
}